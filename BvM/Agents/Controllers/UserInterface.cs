using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BvM.Controllers
{
    /// <summary>
    /// Handler for all communication with the user - getting input, providing output
    /// [thinking about giving the UserInterface the task of handling the file data; see MENACE logs]
    /// </summary>
    public class UserInterface
    {
        private string dataFilePath;
        Dictionary<string, string> fileNames;

        /// <summary>
        /// Handler for all communication with the user - getting input, providing output
        /// [thinking about giving the UserInterface the task of handling the file data; see MENACE logs]
        /// </summary>
        public UserInterface()
        {
            this.dataFilePath = "../../../Data/";
            fileNames = new Dictionary<string, string>();
            fileNames.Add("temp", "tempData.bvm");
            fileNames.Add("stats", "stats.bvm");
            fileNames.Add("history", "gameHistory.bvm");
            fileNames.Add("menace", "menaceState.bvm");
        }
        /// <summary>
        /// Records data in a text file
        /// </summary>
        /// <param name="append">True to append the file, False to overwrite it</param>
        /// <param name="repo">Name of the repository - stats, history, menace</param>
        /// <param name="data">String to be appendedwritten in the repo</param>
        /// <returns></returns>
        public string Record(bool append, string repo, string data)
        {
            // get filename
            // Get confirmation for overwriting file
            if (!append)
            {
                bool confirmed = GetConfirmation($"Are you sure you overwrite \"{repo}\"?");
                if (!confirmed)
                {
                    return $"writing in \"{repo}\" cancelled";
                }
            }
            // TODO : TIMESTAMP at some point!!
            string fullPath = Path.Combine(dataFilePath, fileNames[repo]);
            StreamWriter writer = new StreamWriter(fullPath, append);
            writer.WriteLine(data);
            writer.Flush();
            return "Done";
        }

        private bool GetConfirmation(string confirmationMessage)
        {
            Console.WriteLine(confirmationMessage);
            while (true)
            {
                Console.WriteLine("Y/N");
                string responseString = Console.ReadLine().ToLower();
                char response = responseString[0];
                if (response == 'y')
                {
                    return true;
                }
                else if (response == 'n')
                {
                    return false;
                }
            }
        }

        private static string TimeStamp()
        {
            return (DateTime.Now.ToString("[ dd-MM-yy / hh:mm:ss]"));
        }
        private static string TimeStamp(int gameNumber)
        {
            return (DateTime.Now.ToString($"[ {gameNumber} / dd-MM-yy / hh:mm:ss]"));
        }
        private static string TimeStamp(string stamp)
        {
            return (DateTime.Now.ToString($"[ \"{stamp}\" / dd-MM-yy / hh:mm:ss]"));
        }
    }
}
