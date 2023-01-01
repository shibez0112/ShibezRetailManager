using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SRMDesktopUI.EventModels;
using SRMDesktopUI.Library.Api;
using SRMDesktopUI.Library.Models;

namespace SRMDesktopUI.ViewModels
{
    internal class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        SalesViewModel _saleVM;
        ILoggedInUserModel _user;
        IAPIHelper _apiHelper;

        public ShellViewModel(IEventAggregator events, SalesViewModel saleVM, ILoggedInUserModel user, IAPIHelper apiHelper)
        {
            _events = events;
            _saleVM = saleVM;
            _user = user;
            _apiHelper = apiHelper;

            _events.Subscribe(this);

            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public bool IsLoggedIn
        {
            get
            {
                bool output = false;

                if (string.IsNullOrWhiteSpace(_user.Token) == false)
                {
                    output = true;
                }
                return output;
            }
        }

        public void ExitApplication()
        {
            TryClose();
        }

        public void UserManagement()
        {
            ActivateItem(IoC.Get<UserDisplayViewModel>());
        }

        public void LogOut()
        {
            _user.ResetUserModel();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }


        public void Handle(LogOnEvent message)
        {
            ActivateItem(_saleVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
