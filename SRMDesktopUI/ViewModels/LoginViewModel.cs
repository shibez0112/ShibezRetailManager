using Caliburn.Micro;
using SRMDesktopUI.Helpers;
using SRMDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRMDesktopUI.ViewModels
{
    internal class LoginViewModel : Screen
    {
        private string _userName;
        private string _password;
        private IAPIHelper _aPIHelper;

        public LoginViewModel(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool CanLogIn
        {
            get
            {
                bool output = false;

                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;

                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set 
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                var result = await _aPIHelper.Authenticate(UserName, Password);

                // Capture more information about the user
                await _aPIHelper.GetLoggedInUserInfo(result.Access_Token);

            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

    }
}
