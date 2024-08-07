#if FAIRYGUI_SPINE
using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace FairyGUI
{
    public partial class GLoader3D
    {
        public static int ID_OutlineColor;
        public static int ID_OutlineWith;

        static GLoader3D()
        {
            ID_OutlineColor = Shader.PropertyToID("_OutlineColor");
            ID_OutlineWith = Shader.PropertyToID("_OutlineWidth");
        }

        public static Action<GLoader3D, string, bool, int, PlayCompleteCallback> SpinePlay;
        public static Action<GLoader3D, string[], bool, int, PlayCompleteCallback> SpinePlaySeq;
        public static Action<SkeletonDataAsset> SpineUnload;

        private static readonly Dictionary<SkeletonDataAsset, int> DictSkeletonDataAssets = new();

        private SkeletonDataAsset _dataAsset;
        
        public SkeletonDataAsset DataAsset
        {
            get { return this._dataAsset; }
        }

        private void __onAddSpine(SkeletonDataAsset asset)
        {
            _dataAsset = asset;
            if (DictSkeletonDataAssets.TryGetValue(asset, out int count))
                DictSkeletonDataAssets[asset] = count + 1;
            else
                DictSkeletonDataAssets[asset] = 1;
        }

        private void __onRemoveSpine(SkeletonDataAsset asset)
        {
            if (_dataAsset != null)
            {
                if (DictSkeletonDataAssets.TryGetValue(this._dataAsset, out int count))
                {
                    --count;
                    if (count <= 0)
                    {
                        DictSkeletonDataAssets.Remove(this._dataAsset);
                        SpineUnload?.Invoke(_dataAsset);
                    }
                    else
                    {
                        DictSkeletonDataAssets[this._dataAsset] = count;
                    }
                }
                else
                {
                    Debug.LogError($"Skeleton Asset Data: {this._dataAsset.name} not found at cache dict...");
                }

                _dataAsset = null;
            }
        }
        
        protected void ResumeAnimations()
        {
            if (_cachedAnimationNames == null || _cachedAnimationNames.Length <= 0)
                return;

            if (_cachedAnimationNames.Length > 1)
                SpinePlaySeq?.Invoke(this, _cachedAnimationNames, _cachedLoop, _cachedIndex, _cachedCompleteCallback);
            else
                SpinePlay?.Invoke(this, _cachedAnimationNames[0], _cachedLoop, _cachedIndex, _cachedCompleteCallback);
        }
    }
}

#endif