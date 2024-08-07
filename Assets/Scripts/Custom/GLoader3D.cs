#if FAIRYGUI_SPINE
using UnityEngine;

namespace FairyGUI
{
    public enum ESpineMaterialCloneType
    {
        Auto,
        ForceClone,
        ForceDoNotClone,
    }
    
    public partial class GLoader3D
    {
        string[] _cachedAnimationNames; // 存放当前播放的动画
        bool _cachedLoop;
        int _cachedIndex;
        PlayCompleteCallback _cachedCompleteCallback;


        protected const string ShaderOutlineNamePrefix = "Spine/Outline/";
        protected const string ShaderNormalNamePrefix = "Spine/";
        private Color _outLineClr = Color.yellow;
        private bool _outline = false;
        private int _outlineWith = 0;

        public void EnbleOutLine(Color clr, int outlineWith = 3)
        {
            this._content.CloneMaterial = true;
            this._outLineClr = clr;
            this._outline = true;
            this._outlineWith = outlineWith;
        }


        public void DisableOutLine()
        {
            if (!this._outline)
                return;
            this._outline = false;
            this._outlineWith = 0;
            this._content.CacheRenderers();
        }

        public ESpineMaterialCloneType spineMaterialCloneType;
    }
}

#endif // FAIRYGUI_SPINE