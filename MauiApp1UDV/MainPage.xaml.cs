using Microsoft.Maui.Controls;

namespace MauiApp1UDV
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            UpdatePhoneColor();
        }

        private void OnAgeSliderChanged(object sender, ValueChangedEventArgs e)
        {
            AgeStepper.Value = e.NewValue;
            UpdateAgeLabel();
            OnFormDataChanged(sender, e);
        }

        private void OnAgeStepperChanged(object sender, ValueChangedEventArgs e)
        {
            AgeSlider.Value = e.NewValue;
            UpdateAgeLabel();
            OnFormDataChanged(sender, e);
        }

        private void UpdateAgeLabel()
        {
            int age = (int)AgeSlider.Value;
            AgeLabel.Text = $"{age} {GetAgeSuffix(age)}";
        }

        private string GetAgeSuffix(int age)
        {
            if (age % 10 == 1 && age % 100 != 11)
                return "год";
            else if (age % 10 >= 2 && age % 10 <= 4 && (age % 100 < 10 || age % 100 >= 20))
                return "года";
            else
                return "лет";
        }

        private void OnPhoneTextChanged(object sender, TextChangedEventArgs e)
        {
            string cleaned = new string(e.NewTextValue?.Where(char.IsDigit).ToArray());

            if (cleaned != e.NewTextValue)
            {
                PhoneEntry.Text = cleaned;
            }

            UpdatePhoneColor();
            OnFormDataChanged(sender, e);
        }

        private void UpdatePhoneColor()
        {
            string phone = PhoneEntry.Text ?? "";

            if (phone.Length == 11)
            {
                PhoneEntry.TextColor = Colors.Green;
                PhoneHintLabel.Text = "✓ Корректный номер";
                PhoneHintLabel.TextColor = Colors.Green;
            }
            else
            {
                PhoneEntry.TextColor = Colors.Red;
                PhoneHintLabel.Text = "Введите 11 цифр";
                PhoneHintLabel.TextColor = Colors.Red;
            }
        }

        private void OnAgreementToggled(object sender, ToggledEventArgs e)
        {
            UpdateSubmitButton();
        }

        private void OnAgreementLabelTapped(object sender, EventArgs e)
        {
            AgreementSwitch.IsToggled = !AgreementSwitch.IsToggled;
        }

        private void OnFormDataChanged(object sender, EventArgs e)
        {
        }

        private void UpdateSubmitButton()
        {
            if (AgreementSwitch.IsToggled)
            {
                SubmitButton.IsEnabled = true;
                SubmitButton.BackgroundColor = Color.FromArgb("#512BD4");
            }
            else
            {
                SubmitButton.IsEnabled = false;
                SubmitButton.BackgroundColor = Colors.LightGray;
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
                string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
                string.IsNullOrWhiteSpace(PhoneEntry.Text) ||
                PhoneEntry.Text.Length != 11 ||
                (!MaleRadio.IsChecked && !FemaleRadio.IsChecked))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все обязательные поля корректно", "OK");
                return;
            }

            string gender = MaleRadio.IsChecked ? "Мужской" : "Женский";

            string message = $@"Данные анкеты:

            Фамилия: {LastNameEntry.Text}
            Имя: {FirstNameEntry.Text}
            Отчество: {MiddleNameEntry.Text ?? "Не указано"}
            Возраст: {AgeLabel.Text}
            Телефон: {PhoneEntry.Text}
            Пол: {gender}
            Согласие: {(AgreementSwitch.IsToggled ? "Да" : "Нет")}";

            await DisplayAlert("Анкета отправлена", message, "OK");
        }

        private void ClearForm()
        {
            LastNameEntry.Text = string.Empty;
            FirstNameEntry.Text = string.Empty;
            MiddleNameEntry.Text = string.Empty;
            AgeSlider.Value = 13;
            AgeStepper.Value = 13;
            PhoneEntry.Text = string.Empty;
            MaleRadio.IsChecked = false;
            FemaleRadio.IsChecked = false;
            AgreementSwitch.IsToggled = false;
        }
    }
}