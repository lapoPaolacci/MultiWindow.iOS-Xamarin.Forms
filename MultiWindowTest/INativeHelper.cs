using System;

using Xamarin.Forms;

namespace MultiWindowTest
{
    public interface INativeHelper
    {
        void ShowToast(string message, double duration);
    }
}

