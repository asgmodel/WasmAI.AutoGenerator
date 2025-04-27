using AutoNotificationService.Notifications;
using AutoNotificationService.Services.Email;
using AutoNotificationService.Services.Sms;
using System;
using System.Reflection;

namespace AutoGenerator.Notifications
{

   


    public interface IAutoNotifier: INotifierManager
    {


    }
    public class AutoNotifier : NotifierManager, IAutoNotifier
    {
        public AutoNotifier(IEnumerable<IProviderNotifier> notifiers) : base(notifiers)
        {
        }
    }

}
