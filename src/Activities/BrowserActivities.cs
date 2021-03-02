using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UiPath.Core.Activities;
using System.Activities.Presentation;
using System.Windows;
using System.Runtime.InteropServices.ComTypes;

namespace SampleGovernedActivities.Activities
{
    public class GovernedOpenBrowser : OpenBrowser
    {
        protected List<string> whitelist = new List<string> {"test.com",
                                                             "www.test.com",
                                                             ""};
        protected override Task<object> ExecuteAsync(NativeActivityContext context)
        {
            string url = base.Url.Get(context);
            var uri = new Uri(url);
            string host = uri.Host;

            if (!(whitelist.Contains(host)))
            {
                throw new Exception("Prohibited Url");
            }

            //If no exception is thrown by the validation check, call the base class's "ExecuteAsync"
            //which will send the message per the settings
            return base.ExecuteAsync(context);
        }
    }

    public sealed class GovernedOpenBrowserFactory : IActivityTemplateFactory<GovernedOpenBrowser>
    {
        public GovernedOpenBrowser Create(DependencyObject target, System.Windows.IDataObject dataObject)
        {
            var actv = new GovernedOpenBrowser();
            return actv;        
        }

    }

    public class GovernedNavigateTo : NavigateTo

    {
        protected List<string> whitelist = new List<string> {"test.com",
                                                             "www.test.com",
                                                             ""};
        protected override Task ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            string url = base.Url.Get(context);
            var regular_expression = new Regex(url);
            if (!(whitelist.Where(f => regular_expression.IsMatch(f)).ToList().Any<string>()))
            {
                throw new Exception("Prohibited Url");
            }
            return base.ExecuteAsync(context, cancellationToken);
        }
    }
}