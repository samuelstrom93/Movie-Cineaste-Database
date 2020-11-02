using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.ViewModel
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorMessage { get; set; }

        public ErrorViewModel(Exception exception, string controllerName, string actionName)
        {
            ErrorMessage = exception.Message.ToString();
        }


    }

}
