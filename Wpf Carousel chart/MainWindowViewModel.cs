using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Wpf_Carousel_chart
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private int picCount;

        public MainWindowViewModel()
        {
            PicUrl = new ObservableCollection<string>();
            PicUrl.Add("image/ShizukuXmas2014_2560x1600.png");
            PicUrl.Add("image/ShizukuWP22_2560x1600.png");
            PicUrl.Add("image/ShizukuWP21_2560x1600.png");
            PicUrl.Add("image/ShizukuWP20A_2560x1600.png");
            PicUrl.Add("image/ShizukuWP8B_2560x1600.png");
            PicUrl.Add("image/ShizukuWP3B_2560x1600.png");
            PicUrl.Add("image/ShizukuWP3A_2560x1600.png");

            DotCollection = new ObservableCollection<string>(PicUrl);

            //将最后一张图加在最前面
            PicUrl.Insert(0, PicUrl[PicUrl.Count - 1]);
            //将第一张图片添在最后面
            PicUrl.Add(PicUrl[1]);

            picCount = PicUrl.Count();
        }

        [ObservableProperty]
        ObservableCollection<string> picUrl;

        [ObservableProperty]
        ObservableCollection<string> dotCollection;

        [ObservableProperty]
        int selectedPicIndex;

        [ObservableProperty]
        int selectedDotIndex = 0;

        [RelayCommand]
        void Pre()
        {
            if (SelectedPicIndex == 0)
            {
                SelectedPicIndex = picCount - 2;
            }
            SelectedPicIndex--;
            SelectedDotIndex = DotCollection.IndexOf(PicUrl[SelectedPicIndex]);
        }

        [RelayCommand]
        void Next()
        {
            if (SelectedPicIndex == picCount - 1)
            {
                SelectedPicIndex = 1;
            }
            SelectedPicIndex++;

            SelectedDotIndex = DotCollection.IndexOf(PicUrl[SelectedPicIndex]);
        }

        [RelayCommand]
        void PicChanged(string obj)
        {
            SelectedPicIndex = PicUrl.IndexOf(obj);
            SelectedDotIndex = DotCollection.IndexOf(obj);
        }

        [RelayCommand]
        async Task CarouselListBoxLoaed()
        {
            SelectedPicIndex = 1;
            PeriodicTimer Timer = new PeriodicTimer(TimeSpan.FromMilliseconds(4000));
            while (await Timer.WaitForNextTickAsync())
            {
                Next();
            }
        }
    }
}
