using System.Web.Mvc;

namespace Artwork_Sharing_Platform.Areas.Audience
{
    public class AudienceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Audience";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Audience_default",
                "Audience/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}