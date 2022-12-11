using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SRMDesktopUI.EventModels;

namespace SRMDesktopUI.ViewModels
{
    internal class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private LoginViewModel _loginVM;
        private IEventAggregator _events;
        SalesViewModel _saleVM;
        SimpleContainer _container;
        public ShellViewModel(LoginViewModel loginVM, IEventAggregator events, SalesViewModel saleVM, SimpleContainer container)
        {
            _events = events;
            _loginVM = loginVM;
            _saleVM = saleVM;
            _container = container;

            _events.Subscribe(this);

            ActivateItem(_container.GetInstance<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_saleVM);
        }
    }
}
