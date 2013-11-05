using System;
using System.Web.Profile;
using System.Web.Security;

namespace MVCForms.Models
{
    public class UserProfile : ProfileBase
    {
        //All validation properties are managed in the ViewModel for profile in this application
        [SettingsAllowAnonymous(false)]
        public string FirstName
        {
            get { return base["FirstName"] as string; }
            set { base["FirstName"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        public string LastName
        {
            get { return base["LastName"] as string; }
            set { base["LastName"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        public int AvailableSubmissions
        {
            get { return base["AvailableSubmissions"] is int ? (int) base["AvailableSubmissions"] : 0; }
            set { base["AvailableSubmissions"] = value; }
        }

        [SettingsAllowAnonymous(false)]
        public DateTime LastSubmission
        {
            get { return base["LastSubmission"] is DateTime ? (DateTime) base["LastSubmission"] : new DateTime(1900, 01, 01); }
            set { base["LastSubmission"] = value; }
        }

        public static UserProfile GetUserProfile(string username)
        {
            return Create(username) as UserProfile;
        }

        public static UserProfile GetUserProfile()
        {
            return Create(Membership.GetUser().UserName) as UserProfile;
        }
    }
}