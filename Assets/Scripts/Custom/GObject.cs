using UnityEngine;

namespace FairyGUI
{
    public partial class GObject
    {
        #region 自定义属性

        public UIFlags Flag = UIFlags.Default;

        #endregion

        /// <summary>
        /// 设置对象为全屏大小（物理屏幕）。
        /// </summary>
        /// <param name="useScale"></param>
        public void MakeFullScreen(bool useScale)
        {
            if (useScale)
            {
                var scale = GRoot.inst.height / this.height;
                if (scale > 1)
                {
                    this.SetScale(scale * this.scaleX, scale * this.scaleY);
                }
            }
            else
            {
                this.SetSize(GRoot.inst.width, GRoot.inst.height);
            }

            if (GRoot.inst.height / GRoot.inst.width > 2)
            {
                this.SetXY(x, GRoot.inst.height * this.pivot.y - UIConfig.fFullScreenGapTop);
            }
            else
            {
                this.SetXY(x, GRoot.inst.height * this.pivot.y);
            }
        }

        public void SetColor(Color color, bool recursive)
        {
            switch (this)
            {
                case GImage gImage:
                    gImage.color = color;
                    break;
                case GButton gButton:
                    gButton.color = color;
                    break;
                case GGraph gGraph:
                    gGraph.color = color;
                    break;
                case GMovieClip gMovieClip:
                    gMovieClip.color = color;
                    break;
                case GLabel gLabel:
                    gLabel.color = color;
                    break;
                case GTextField gTextField:
                    gTextField.color = color;
                    break;
                case GLoader gLoader:
                    gLoader.color = color;
                    break;
                case GLoader3D gLoader3D:
                    gLoader3D.color = color;
                    break;
            }

            if (!recursive || this is not GComponent com) return;
            foreach (var child in com.GetChildren()) child.SetColor(color, true);
        }

        public Color GetColor() => this switch
        {
            GImage image => image.color,
            GButton button => button.color,
            GGraph gGraph => gGraph.color,
            GMovieClip movieClip => movieClip.color,
            GLabel gLabel => gLabel.color,
            GTextField gTextField => gTextField.color,
            GLoader gLoader => gLoader.color,
            GLoader3D gLoader3D => gLoader3D.color,
            _ => Color.white
        };
    }
}