﻿
using MauiStylesDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStylesDemo.ViewModels
{
    public class FormValidationViewModel:ViewModelBase
    {
        private bool showDateError;

        public bool ShowDateError
        {
            get => showDateError;
            set
            {
                showDateError = value;
                OnPropertyChanged("ShowDateError");
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                ValidateDate();
                OnPropertyChanged("Date");
            }
        }

        private string dateError;

        public string DateError
        {
            get => dateError;
            set
            {
                dateError = value;
                OnPropertyChanged("DateError");
            }
        }
        private void ValidateDate()
        {
            double age = CalculaterAge();
            this.showDateError = age <= 13;
        }
        private double CalculaterAge()
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan d = currentDate - date;
            double ageInYears = d.TotalDays / 365.25;
            return ageInYears;

        }
        #region שם
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Name);
        }
        #endregion
        #region גיל
        private bool showAgeError;

        public bool ShowAgeError
        {
            get => showAgeError;
            set
            {
                showAgeError = value;
                OnPropertyChanged("ShowAgeError");
            }
        }

        private double? age;

        public double? Age
        {
            get => age;
            set
            {
                age = value;
                ValidateAge();
                OnPropertyChanged("Age");
            }
        }

        private string ageError;

        public string AgeError
        {
            get => ageError;
            set
            {
                ageError = value;
                OnPropertyChanged("AgeError");
            }
        }

        private void ValidateAge()
        {
            this.ShowAgeError = !Age.HasValue || Age <= 13;
        }
        #endregion
       

        public FormValidationViewModel()
        {
            this.NameError = "זהו שדה חובה";
            this.ShowNameError = false;
            this.AgeError = "הגיל חייב להיות גדול מ 13";
            this.ShowAgeError = false;
            this.SaveDataCommand = new Command(() => SaveData());
            this.showDateError = false;
        }

        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateAge();
            ValidateName();

            //check if any validation failed
            if (ShowAgeError ||
                ShowNameError)
                return false;
            return true;
        }

        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            if (ValidateForm())
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", "הנתונים נבדקו ונשמרו", "אישור", FlowDirection.RightToLeft);
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", "יש בעיה עם הנתונים", "אישור", FlowDirection.RightToLeft);
        }
    }
}
