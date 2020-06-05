using System;
using System.Collections.ObjectModel;

using OrmV2;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using XamXpoV2.Infrastructure;
using System.Linq;
using DevExpress.Xpo;

namespace XamXpoV2.ViewModels
{
    public class MainPageViewModel : AppMapViewModelBase
    {


        public MainPageViewModel(INavigationService navigationService) : base (navigationService)
        {
            this.LoadData();
        }
        ObservableCollection<dynamic> data;
        public ObservableCollection<dynamic> Data
        {
            get { return data; }
            set
            {
                if (data == value)
                    return;
                data = value;
                RaisePropertyChanged();
            }
        }
        private UnitOfWork UoW;
        protected void LoadData()
        {


            var list = from c in this.UoW.Query<Contact>()
                       orderby c.Name
                       select new { Name = c.Name, Phone = c.Phone };
            
            //Data.Clear();

            //foreach (var item in list)
            //{
            //    Data.Add(item);
            //}
            //this.RaisePropertyChanged("Data");
        }
        
    }
}
