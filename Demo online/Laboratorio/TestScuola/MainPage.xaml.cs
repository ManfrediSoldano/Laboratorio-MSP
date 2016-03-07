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
        //Modifica il pin con quello che preferisci
        //Qui la mappatura dei pin con un Pi2 con W10: https://ms-iot.github.io/content/images/PinMappings/RP2_Pinout.png 
        private const int PIN = 19;
        //Avrei potuto usarne solo uno e cambiargli lo stato (come ho fatot con la demo OffLine), am qui mi è risultato più veloce!
        private GpioPinValue pinValue = GpioPinValue.High;
        private GpioPinValue pinValueL = GpioPinValue.Low;


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


            rpin.Write(pinValueL);


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

        private async void Timer_Tick(object sender, object e)
        {
            System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();

            // Modifica il sito web con quello creato grazie ad Azure4Dreamspark!

            System.Net.Http.HttpResponseMessage response = await http.GetAsync("http://lavoratoriomsp.azurewebsites.net/stato.txt");

            string webresponse = await response.Content.ReadAsStringAsync();

            if (webresponse.Equals("Acceso"))
            {

                rpin.Write(pinValue);

            }
            if (webresponse.Equals("Spento"))
            {
                rpin.Write(pinValueL);

            }

        }
    }
}