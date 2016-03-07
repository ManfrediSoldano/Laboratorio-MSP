using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;
using System.Net;
using Windows.Web.Http;


namespace TestScuola
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private const int PIN = 19;


        private GpioPinValue pinValue = GpioPinValue.Low;


        private GpioPin rpin;


        private DispatcherTimer timer;
        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                rpin = null;

                return;
            }

            rpin = gpio.OpenPin(PIN);

            rpin.SetDriveMode(GpioPinDriveMode.Output);


            rpin.Write(pinValue);


        }


        public MainPage()
        {
            // ...

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += Timer_Tick;

            InitGPIO();


            if (rpin != null)
            {
                timer.Start();
            }

            // ...
        }

        private void Timer_Tick(object sender, object e)
        {


            if (pinValue == GpioPinValue.High)
            {
                pinValue = GpioPinValue.Low;
                rpin.Write(pinValue);


            }
            else
            {
                pinValue = GpioPinValue.High;
                rpin.Write(pinValue);


            }

        }

    }
}