using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace getDiscordDefaultAvatar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("The token will not appear it's normal ! Check the src code to understand why !\nThe Default Avatar ID is calculated with your Discriminator: (discriminator % 5)\nToken: ");
            string token = ReadPassword().Replace("\"", string.Empty);
            Console.Clear();
            int defaultAvatarID = int.Parse(JObject.Parse(getUser(token))["discriminator"].ToString())%5;
            string finalURI = String.Format("https://cdn.discordapp.com/embed/avatars/{0}.png", defaultAvatarID);
            Process.Start(finalURI);
            Console.WriteLine(String.Format("Default Avatar URI oppened: {0}", finalURI));
            Console.ReadLine();

            //Just to make some pleasure to someone otherwhise it have less lines :flushed:
        }

        public static string getUser(string token)
        {
            WebClient Client = new WebClient();
            Client.Headers.Set("Authorization", token);
            string res = Client.DownloadString($"https://discord.com/api/v9/users/@me");
            if (!res.Contains("id"))
            {
                Console.WriteLine("Invalid Token !");
                Console.ReadLine();
                Environment.Exit(-1);
            }
            return res;
        }

        public static string ReadPassword()
        {
            /* https://stackoverflow.com/a/36029698 */
            IntPtr stdInputHandle = NativeStuff.GetStdHandle(NativeStuff.StdHandle.Input);
            if (stdInputHandle == IntPtr.Zero)
                throw new InvalidOperationException("No console input");

            int previousConsoleMode;
            if (!NativeStuff.GetConsoleMode(stdInputHandle, out previousConsoleMode))
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not get console mode.");

            // disable console input echo
            if (!NativeStuff.SetConsoleMode(stdInputHandle, previousConsoleMode & ~(int)NativeStuff.ConsoleMode.ENABLE_ECHO_INPUT))
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not disable console input echo.");

            // just read the password using standard Console.ReadLine()
            string password = Console.ReadLine();

            // reset console mode to previous
            if (!NativeStuff.SetConsoleMode(stdInputHandle, previousConsoleMode))
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not reset console mode.");

            return password;
        }
    }
}
