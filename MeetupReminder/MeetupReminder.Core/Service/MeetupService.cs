using CSharp.Meetup.Connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spring.Social.OAuth1;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace MeetupReminder.Core.Service
{
    public class MeetupService
    {
        private const string MeetupApiKey = "6dc7qvqaj8t315fbgubdokcd7r";
        private const string MeetupSecretKey = "v08sil36utuf72l7e0f3n45ufg";

        private async static Task<OAuthToken> authenicate()
        {
            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupSecretKey);

            var oauthToken = meetupServiceProvider.OAuthOperations.FetchRequestTokenAsync("oob", null).Result;

            var authenicateUrl = meetupServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, null);

            Process.Start(authenicateUrl);
            Console.WriteLine("Enter the pin from meetup.com");
            string pin = Console.ReadLine();

            var requestToken = new AuthorizedRequestToken(oauthToken, pin);
            var oauthAccessToken = meetupServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;

            return oauthAccessToken;
        }

        public async Task GetMeetupsFor(string meetupGroupName)
        {
            var token = await authenicate();
            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupSecretKey);

            var meetup = meetupServiceProvider.GetApi(token.Value, token.Secret);

            Console.WriteLine("Enter the name of the group you want to see meetups for");
            string groupname = Console.ReadLine();

            string json = await meetup.RestOperations.GetForObjectAsync<string>($"https://api.meetup.com/2/events?group_urlname={groupname}");
            var oEvents = JObject.Parse(json)["results"];

            foreach (var oEvent in oEvents)
            {
                Console.WriteLine("Here's a meetup! - " + oEvent["name"]);
            }
        }

    }
}
