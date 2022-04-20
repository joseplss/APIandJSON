using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace APISandJSONS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //sets up api to be read by link
            var client = new HttpClient();
            #region Excercise 1
            for (int i = 0; i < 5; i++)
            {
                var kanyeURL = "https://api.kanye.rest";
                var kanyeResponse = client.GetStringAsync(kanyeURL).Result;
                //converts api's random quote and converts into string. JSON API return "quote":"value"
                var kanyeQuote = JObject.Parse(kanyeResponse).GetValue("quote").ToString();
                Console.WriteLine($"Kanye: {kanyeQuote}\n");

                //Ron swanson code
                var ronURL = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";
                var ronResponse = client.GetStringAsync(ronURL).Result;
                var ronQuote = JArray.Parse(ronResponse).ToString().Replace('[', ' ').Replace(']', ' ').Replace('"', ' ').Trim();
                Console.WriteLine($"Ron: {ronQuote}");
                Console.WriteLine("--------------------------------------");
            }
            #endregion
            Console.WriteLine("What a nice conversation. Press enter any key to continue...");
            Console.ReadLine();
            Console.Clear();
            #region excercise2
            Console.WriteLine("Now let's find out the weather in my area");
            string apiKeys = File.ReadAllText("appsettings.json");
            string weatherapi = JObject.Parse(apiKeys).GetValue("DefaultKey").ToString();
            string weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip=92675,us&appid={weatherapi}&units=imperial";
            string weatherResponse = client.GetStringAsync(weatherURL).Result;
            //going to make JObject.Parse(weatherResponse) since repeated
            var jo = JObject.Parse(weatherResponse);
            string city = jo.GetValue("name").ToString();
            string country = jo["sys"]["country"].ToString();
            decimal temp = (decimal)jo["main"]["temp"];
            decimal feelTemp = (decimal)jo["main"]["feels_like"];
            int humidity = (int)jo["main"]["humidity"];
            int longitude = (int)jo["coord"]["lon"];
            int latitude = (int)jo["coord"]["lat"];
            string weatherMain = jo["weather"].First["main"].ToString();
            string weatherDesc = jo["weather"].First["description"].ToString();
            Console.WriteLine($"Location: {city}, {country}");
            Console.WriteLine($"Temperature: {temp}. Feels like {feelTemp}. Humidity is {humidity}");
            Console.WriteLine($"Coordinates are: x = {longitude} y = {latitude}");
            Console.WriteLine($"The weather at the moment is {weatherMain}, {weatherDesc} as far as I can tell");
            #endregion
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Nice weather we are having. Press enter any key to continue...");
            Console.ReadLine();
            Console.Clear();
            #region BONUS
            Console.WriteLine("Enter a Call of Duty username (if left blank, will enter one as default):");
            string username = Console.ReadLine();
            string userPlatform;
            if (username == "")
            {
                username = "Amartin743";
            }
            Console.WriteLine("Enter the username's gaming platform:\n1 for Playstation Network\n2 for Steam\n3 for BattleNET (PC)\n4 for Xbox Live\n5 for Activision ID\n(if left blank, will enter default's platform):");
            userPlatform = Console.ReadLine();
            switch (userPlatform)
            {
                case "1":
                    userPlatform = "psn";
                    break;
                case "2":
                    userPlatform = "steam";
                    break;
                case "3":
                    userPlatform = "battle";
                    break;
                case "4":
                    userPlatform = "xbl";
                    break;
                case "5":
                    userPlatform = "acti";
                    break;
                default:
                    userPlatform = "psn";
                    break;
            }
            string codapi = JObject.Parse(apiKeys).GetValue("CODKey").ToString();
            string codURL = $"https://call-of-duty-modern-warfare.p.rapidapi.com/warzone/{username}/{userPlatform}?rapidapi-key={codapi}";
            string codResponse = client.GetStringAsync(codURL).Result;
            var codJo = JObject.Parse(codResponse);

            var codAllKills = (int)codJo["br_all"]["kills"];
            var codAllDeaths = (int)codJo["br_all"]["deaths"];
            decimal codKDRatio = (decimal)codJo["br_all"]["kdRatio"];
            var codGamesPlays = (int)codJo["br_all"]["gamesPlayed"];
            var codTop25 = (int)codJo["br_all"]["topTwentyFive"];
            var codTop10 = (int)codJo["br_all"]["topTen"];
            var codTop5 = (int)codJo["br_all"]["topFive"];
            var codScorePM = (int)codJo["br_all"]["scorePerMinute"];
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Username: {username}\nPlatform: {userPlatform}");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Kills: {codAllKills}\nDeath: {codAllDeaths}\nKD Ratio: {codKDRatio}");
            Console.WriteLine($"Total games played: {codGamesPlays}\nTop 25: {codTop25}\nTop 15: {codTop10}\nTop 5: {codTop5}");
            Console.WriteLine($"Score per minute: {codScorePM}");

            #endregion
        }
    }
}
