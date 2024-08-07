#if FAIRYGUI_SPINE
using System;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace FairyGUI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GLoader3D : GObject
    {
        public static Action<SkeletonAnimation> CustomSpineDestroyMethod;
        
        SkeletonAnimation _spineAnimation;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public SkeletonAnimation spineAnimation
        {
            get { return _spineAnimation; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asset"></param>
        public void SetSpine(SkeletonDataAsset asset)
        {
            SetSpine(asset, _contentItem.width, _contentItem.height, _contentItem.skeletonAnchor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="anchor"></param>
        public void SetSpine(SkeletonDataAsset asset, int width, int height, Vector2 anchor)
        {
            SetSpine(asset, width, height, anchor, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="anchor"></param>
        /// <param name="cloneMaterial"></param>
        public void SetSpine(SkeletonDataAsset asset, int width, int height, Vector2 anchor, bool cloneMaterial)
        {
            if (_spineAnimation != null)
                FreeSpine();

            _content.customCloneMaterials = MaterialOverride;
            _content.customRecoverMaterials = CleanMaterialOverride;

            __onAddSpine(asset);
            _spineAnimation = SkeletonRenderer.NewSpineGameObject<SkeletonAnimation>(asset);
            _spineAnimation.gameObject.name = asset.name;
            Spine.SkeletonData dat = asset.GetSkeletonData(false);
            _spineAnimation.gameObject.transform.localScale = new Vector3(1 / asset.scale, 1 / asset.scale, 1);
            _spineAnimation.gameObject.transform.localPosition = new Vector3(anchor.x, -anchor.y, 0);
            cloneMaterial = spineMaterialCloneType switch
            {
                ESpineMaterialCloneType.Auto => cloneMaterial,
                ESpineMaterialCloneType.ForceClone => true,
                ESpineMaterialCloneType.ForceDoNotClone => false,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (!cloneMaterial)
            {
                if (asset.atlasAssets.Length > 1 || asset.atlasAssets[0] == null || asset.atlasAssets[0].MaterialCount > 1)
                    Debug.LogError($"Spine: {url} Contains More than one materials. my be you should clone materials!");
            }
            SetWrapTarget(_spineAnimation.gameObject, cloneMaterial, width, height);

            _spineAnimation.skeleton.R = _color.r;
            _spineAnimation.skeleton.G = _color.g;
            _spineAnimation.skeleton.B = _color.b;

            OnChangeSpine(null);
        }

        protected void LoadSpine()
        {
            SkeletonDataAsset asset = (SkeletonDataAsset)_contentItem.skeletonAsset;
            if (asset == null)
                return;

            SetSpine(asset, _contentItem.width, _contentItem.height, _contentItem.skeletonAnchor);
            // fixme: 评估是否要删除
            ResumeAnimations();
        }

        protected void OnChangeSpine(string propertyName)
        {
            if (_spineAnimation == null)
                return;

            if (propertyName == "color")
            {
                _spineAnimation.skeleton.R = _color.r;
                _spineAnimation.skeleton.G = _color.g;
                _spineAnimation.skeleton.B = _color.b;
                return;
            }
            // else if (propertyName == "playing" && !this._playing)
            // {
            //     Debug.LogError("spine pause was not support");
            //     return;
            // }

            var skeletonData = _spineAnimation.skeleton.Data;

            var state = _spineAnimation.AnimationState;
            Spine.Animation animationToUse = !string.IsNullOrEmpty(_animationName)
                ? skeletonData.FindAnimation(_animationName)
                : null;
            if (animationToUse != null)
            {
                var trackEntry = state.GetCurrent(0);
                if (trackEntry == null || trackEntry.Animation.Name != _animationName ||
                    trackEntry.IsComplete && !trackEntry.Loop)
                    trackEntry = state.SetAnimation(0, animationToUse, _loop);
                else
                    trackEntry.Loop = _loop;

                if (_playing)
                    trackEntry.TimeScale = 1; // fixme: this.timeScale
                else
                {
                    trackEntry.TimeScale = 0;
                    trackEntry.TrackTime = Mathf.Lerp(0, trackEntry.AnimationEnd - trackEntry.AnimationStart,
                        _frame / 100f);
                }
            }
            else
                state.ClearTrack(0);

            var skin = !string.IsNullOrEmpty(skinName) ? skeletonData.FindSkin(skinName) : skeletonData.DefaultSkin;
            if (skin == null && skeletonData.Skins.Count > 0)
                skin = skeletonData.Skins.Items[0];
            if (_spineAnimation.skeleton.Skin != skin)
            {
                _spineAnimation.skeleton.SetSkin(skin);
                _spineAnimation.skeleton.SetSlotsToSetupPose();
            }
        }

        protected void FreeSpine()
        {
            if (_spineAnimation != null)
            {
                if (CustomSpineDestroyMethod != null)
                {
                    CustomSpineDestroyMethod(_spineAnimation);
                }
                else
                {
                    if (Application.isPlaying)
                        GameObject.Destroy(_spineAnimation.gameObject);
                    else
                        GameObject.DestroyImmediate(_spineAnimation.gameObject);
                }

                _spineAnimation = null;
                _content.customCloneMaterials = null;
                _content.customRecoverMaterials = null;
            }

            __onRemoveSpine(this._dataAsset);
        }

        protected void OnUpdateSpine(UpdateContext context)
        {
            if (_spineAnimation != null)
                _spineAnimation.skeleton.A = context.alpha * _content.alpha;
        }

        private void MaterialOverride(Dictionary<Material, Material> materials)
        {
            if (this.DataAsset == null)
                return;
            if (this.DataAsset.atlasAssets != null)
            {
                foreach (var assetAtlas in this.DataAsset.atlasAssets)
                {
                    if (assetAtlas.MaterialCount == 0)
                        continue;
                    foreach (Material mat in assetAtlas.Materials)
                    {
                        Material newMat;
                        if (!materials.TryGetValue(mat, out newMat))
                        {
                            newMat = new Material(mat);
                            materials[mat] = newMat;
                        }

                        if (mat.renderQueue != 3000) //Set the object rendering in Transparent Queue as UI objects
                            newMat.renderQueue = 3000;
                    }
                }
            }

            if (this._outline)
            {
                foreach (var mat in materials.Values)
                {
                    var shaderName = mat.shader.name;
                    if (shaderName.IndexOf(ShaderOutlineNamePrefix) != -1)
                        return;
                    shaderName = shaderName.Replace(ShaderNormalNamePrefix, ShaderOutlineNamePrefix);
                    mat.shader = Shader.Find(shaderName);
                    mat.SetColor(ID_OutlineColor, this._outLineClr);
                    mat.SetInt(ID_OutlineWith, this._outlineWith);
                }
            }

            if (_spineAnimation != null)
            {
                foreach (var kv in materials)
                {
                    _spineAnimation.CustomMaterialOverride[kv.Key] = kv.Value;
                }
            }
        }

        private void CleanMaterialOverride()
        {
            if (_spineAnimation != null)
                _spineAnimation.CustomMaterialOverride.Clear();
        }
        
#if UNITY_2019_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InitializeOnLoad()
        {
            CustomSpineDestroyMethod = null;
        }
#endif
    }
}

#endif