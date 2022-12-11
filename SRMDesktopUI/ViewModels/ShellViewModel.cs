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
        private IEventAggregator _events;
        SalesViewModel _saleVM;

        public ShellViewModel(IEventAggregator events, SalesViewModel saleVM)
        {
            _events = events;
            _saleVM = saleVM;

            _events.Subscribe(this);

            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_saleVM);
        }
    }
}
