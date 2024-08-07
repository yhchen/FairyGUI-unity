using UnityEngine;

namespace FairyGUI
{
    public partial class GImage
    {
        public GTweener TweenColor(Color start, Color end, float duration)
        {
            return GTween.To(start, end, duration).SetTarget(this, TweenPropType.Color);
        }
    }
}