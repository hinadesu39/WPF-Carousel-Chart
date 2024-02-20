using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using System.Diagnostics;

namespace Mypple_Music.Extensions
{
    /// <summary>
    /// 通过添加动画使得item切换的时候更加线性
    /// </summary>
    public static class ScrollViewerExtension
    {
        // 定义一个附加依赖属性 HorizontalOffsetProperty
        public static readonly DependencyProperty HorizontalOffsetProperty =
            DependencyProperty.RegisterAttached("HorizontalOffset",
                                                typeof(double),
                                                typeof(ScrollViewerExtension),
                                                new UIPropertyMetadata(0.0, OnHorizontalOffsetChanged));

        // 获取 HorizontalOffsetProperty 的值
        public static double GetHorizontalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(HorizontalOffsetProperty);
        }

        // 设置 HorizontalOffsetProperty 的值
        public static void SetHorizontalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(HorizontalOffsetProperty, value);
        }

        // 当 HorizontalOffsetProperty 的值发生变化时，更新 ScrollViewer 的水平偏移量
        private static void OnHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = d as ScrollViewer;
            if (viewer != null)
            {
                viewer.ScrollToHorizontalOffset((double)e.NewValue);
            }
            Debug.WriteLine((double)e.NewValue);
        }


        // 定义一个扩展方法 ScrollToHorizontalOffsetWithAnimation
        public static void ScrollToHorizontalOffsetWithAnimation(this ScrollViewer viewer, double offset, TimeSpan duration, IEasingFunction easingFunction)
        {
            // 创建一个 DoubleAnimation 对象
            var animation = new DoubleAnimation();
            animation.To = offset; // 设置目标的水平偏移量
            animation.Duration = duration; // 设置持续时间
            animation.EasingFunction = easingFunction; // 设置缓动函数
                                                       // 将动画应用到 HorizontalOffsetProperty 上
            Debug.WriteLine(GetHorizontalOffset(viewer));
            viewer.BeginAnimation(HorizontalOffsetProperty, animation);
        }

        public static void ScrollToHorizontalOffsetWithoutAnimation(this ScrollViewer viewer, double offset)
        {
            //结束上一个动画，不这么做对于HorizontalOffset的修改将会失效
            viewer.BeginAnimation(HorizontalOffsetProperty, null);
            Debug.WriteLine(GetHorizontalOffset(viewer));
            SetHorizontalOffset(viewer, offset);
            Debug.WriteLine(GetHorizontalOffset(viewer));

        }
    }
}
