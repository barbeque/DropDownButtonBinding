using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DropDownButtonBindingBug
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Random _rand;

        /// <summary>
        /// Invoked by selecting it from the menu
        /// </summary>
        public ICommand ShowEntityCommand { get; private set; }

        public ICommand AddEntityCommand { get; private set; }

        public ObservableCollection<EntityViewModel> Stuff { get; private set; }

        public MainWindowViewModel()
        {
            Stuff = new ObservableCollection<EntityViewModel>();

            Stuff.Add(new EntityViewModel { Age = 17, Name = "Seventeen" });
            Stuff.Add(new EntityViewModel { Age = 21, Name = "Twenty-One" });
            Stuff.Add(new EntityViewModel { Age = 32, Name = "Thirty-Two" });
            Stuff.Add(new EntityViewModel { Age = 64, Name = "Sixty-Four" });

            _rand = new Random();

            AddEntityCommand = new RelayCommand(() =>
            {
                // Make up some fake crap
                Stuff.Add(new EntityViewModel() { Age = _rand.Next(), Name = Guid.NewGuid().ToString() });
            });

            ShowEntityCommand = new RelayCommand<EntityViewModel>(x =>
            {
                MessageBox.Show($"Name = {x.Name}, their age is {x.Age}");
            });
        }
    }
}
