using SampleGovernedActivities.Activities;
using System.Activities;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;

namespace SampleGovernedActivities
{
    //This class registers the activities so they can be found in Studio(X).
    public class MetaDataProvider : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();

            // In StudioX, an activity's catgory must start with "Business.". 
            // In this case, we'll place the "Mail" activities into the "Mail" category since
            // we want users to use these instead of the included ones

            // Register the activities's categories            
            builder.AddCustomAttributes(typeof(GovernedNavigateTo), new CategoryAttribute("Business.Browser"));
            builder.AddCustomAttributes(typeof(GovernedOpenBrowserFactory), new CategoryAttribute("Business.Browser"));


            //Set their display names
            builder.AddCustomAttributes(typeof(GovernedNavigateTo), new DisplayNameAttribute("Governed Navigate to"));
            builder.AddCustomAttributes(typeof(GovernedOpenBrowser), new DisplayNameAttribute("Governed Open Browser"));
            builder.AddCustomAttributes(typeof(GovernedOpenBrowserFactory), new DisplayNameAttribute("Governed Open Browser"));
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
