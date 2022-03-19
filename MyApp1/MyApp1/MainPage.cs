using Xamarin.Forms;
using Xamarin.Essentials;
using System;
public class MainPage : ContentPage
{
    Entry phoneNumberText;
    Button translateButton;
    Button callButton;
    string translatedNumber;
    public MainPage()
    {
        this.Padding = new Thickness(20, 20, 20, 20);

        StackLayout panel = new StackLayout
        {
            Spacing = 15
        };

        panel.Children.Add(new Label
        {
            Text = "Enter a Phoneword:",
            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
        });

        panel.Children.Add(phoneNumberText = new Entry
        {
            Text = "1-855-XAMARIN",
        });

        panel.Children.Add(translateButton = new Button
        {
            Text = "Translate"
        });

        panel.Children.Add(callButton = new Button
        {
            Text = "Call",
            IsEnabled = false,
        });


        translateButton.Clicked += OnTranslate;
        callButton.Clicked += OnCall;
        this.Content = panel;

    }
    private void OnTranslate(object sender, System.EventArgs e)
    {
        string enteredNumber = phoneNumberText.Text;
        translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

        if (!string.IsNullOrEmpty(translatedNumber))
        {
            //TOD0 :
            callButton.IsEnabled = true;
            callButton.Text = "Call " + translatedNumber;

        }
        else
        {
            //TODO :
            callButton.IsEnabled = false;
            callButton.Text = "Call";
        }

    }
    async void OnCall(object sender, System.EventArgs e)
    {
        if (await this.DisplayAlert(
            "Dial a number",
            "Would you like to call " + translatedNumber + "?",
            "Yes",
            "No"
            ))
        {
            try
            {
                PhoneDialer.Open(translatedNumber);
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to dial", "Phone Number was not valid.", "OK");
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Unable to dial call", "Phone Dialing not supported.", "OK");
            }
            catch (Exception)
            {
                //other error has occured
                await DisplayAlert("Unable to Dial", "Phone Dialing Failed.", "OK");
            }
        }

    }

}