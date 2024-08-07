using System;

namespace FairyGUI
{
    [Flags]
    public enum UIFlags
    {
        #region 内置变量，请不要使用

        __DestroyByViewFlag__ = 0x0001,
        __EUIViewFlag__ = 0x0002,
        __OthersPushStackViewFlag__ = 0x0004,
        #endregion

        DontDestroyOnClose = 0x0100,
        IgnoreByCloseAll = 0x0200,
        DontDestroyOnEscape = 0x0400, // esc 按下时不要关闭
        IsDisposing = 0x0800, // 销毁中
        GuideIgnore = 0x1000, // 忽略引导

        Default = 0, // 默认值
    }

    public static class UIFlagsEx
    {
        /// <summary>
        /// 判定该ui是否包含flags状态
        /// </summary>
        /// <param name="this"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool Has(this UIFlags @this, UIFlags flags) => (@this & flags) == flags;

        /// <summary>
        /// 增加标签
        /// </summary>
        /// <param name="self"></param>
        /// <param name="flags"></param>
        public static void Add(this ref UIFlags self, UIFlags flags)
        {
            self |= flags;
        }

        /// <summary>
        /// 移除标签
        /// </summary>
        /// <param name="self"></param>
        /// <param name="flags"></param>
        public static void Del(this ref UIFlags self, UIFlags flags)
        {
            self ^= flags;
        }
    }
}