using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using Mypple_Music.Extensions;
using System.Collections;

namespace Wpf_Carousel_chart
{
    public class CarouselListBox : ListBox
    {
        private static ScrollViewer scrollViewer;

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            CarouselListBox listBox = e.Source as CarouselListBox;

            if (scrollViewer == null)
            {
                scrollViewer = FindVisualChild<ScrollViewer>(listBox);
            }

            if (scrollViewer != null)
            {
                // 获取 ListBoxItem 的引用
                ListBoxItem firstItem =
                    listBox.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem;
                if (firstItem != null)
                {
                    // 获取 ListBoxItem 的 RenderSize 属性
                    Size firstItemSize = firstItem.RenderSize;
                    // 获取 ListBoxItem 的宽度
                    double firstItemWidth = firstItemSize.Width;

                    //获取被取消选中的项
                    var oldItem = e.RemovedItems[0] as string;
                    //获取被选中的项
                    var newItem = e.AddedItems[0] as string;

                    if (oldItem == newItem && listBox.SelectedIndex == 1)
                    {
                        //从最后一张图跳到前面相同的图，完成一次循环
                        scrollViewer.ScrollToHorizontalOffsetWithoutAnimation(firstItemWidth);
                    }
                    else if (oldItem == newItem && listBox.SelectedIndex == listBox.Items.Count - 2)
                    {
                        //从第一张图跳到后面相同的图，完成一次循环
                        scrollViewer.ScrollToHorizontalOffsetWithoutAnimation(firstItemWidth * (listBox.Items.Count - 2));
                    }
                    else
                    {
                        scrollViewer.ScrollToHorizontalOffsetWithAnimation(
                            firstItemWidth * (listBox.SelectedIndex),
                            TimeSpan.FromSeconds(0.4),
                            new CircleEase()
                        );
                    }
                }
            }
        }

        /// <summary>
        /// 通过遍历可视化树获取到Listbox中的ScrollViewer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T FindVisualChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                    {
                        return childItem;
                    }
                }
            }
            return null;
        }
    }
}
