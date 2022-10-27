using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MultiWindowTest
{
    public partial class MainPage : ContentPage
    {
        double stepValue = 1;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            DependencyService.Get<INativeHelper>().ShowToast("test", slider.Value);
        }

        void Slider_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / stepValue);
            slider.Value = newStep * stepValue;

            timeToastEntry.Text = slider.Value.ToString();
        }

        void Button_Clicked_3(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        void Button_Clicked_4(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
