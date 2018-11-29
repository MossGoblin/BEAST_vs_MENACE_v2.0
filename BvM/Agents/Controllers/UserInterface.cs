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
        private const string dataFilePath = "../../../Data/";
        Dictionary<string, string> fileNames;

        /// <summary>
        /// Handler for all communication with the user - getting input, providing output
        /// [thinking about giving the UserInterface the task of handling the file data; see MENACE logs]
        /// </summary>
        public UserInterface()
        {
            fileNames = new Dictionary<string, string>();
            fileNames.Add("temp", "tempData.bvm");
            fileNames.Add("stats", "stats.bvm");
            fileNames.Add("history", "gameHistory.bvm");
            fileNames.Add("menace", "menaceState.bvm");
        }
        /// <summary>
        /// Records data in a text file
        /// </summary>
        /// <param name="append">'True' to append the file, 'False' to overwrite it</param>
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
            string fullPath = Path.Combine(dataFilePath, fileNames[repo]);
            StreamWriter writer = new StreamWriter(fullPath, append);
            // TODO : TIMESTAMP at some point!!
            string timeStamp = TimeStamp("something wicked this way comes");
            writer.WriteLine(timeStamp);
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

        /// <summary>
        /// Generates the timestamp in format "dd-MM-yy : hh:mm:ss"
        /// </summary>
        /// <returns></returns>
        private static string GetStamp()
        {
            return DateTime.Now.ToString("dd-MM-yy : hh:mm:ss");
        }

        /// <summary>
        /// Generic timestamp: [ dd-MM-yy : hh:mm:ss ]
        /// </summary>
        /// <returns></returns>
        private static string TimeStamp()
        {
            //return (DateTime.Now.ToString($"[ dd-MM-yy : hh:mm:ss ]"));
            return String.Concat("[ ", GetStamp(), " ]");
        }

        /// <summary>
        /// Timestamp including the game number: [ {gameNumber} : dd-MM-yy : hh:mm:ss ]
        /// </summary>
        /// <param name="gameNumber"></param>
        /// <returns></returns>
        private static string TimeStamp(int gameNumber)
        {
            return String.Concat($"[ gm {gameNumber} : ", GetStamp(), " ]");
        }

        /// <summary>
        /// Extended timestamp, including a description string: [ "{stamp}" : dd-MM-yy : hh:mm:ss ]
        /// </summary>
        /// <param name="stamp"></param>
        /// <returns></returns>
        private static string TimeStamp(string stamp)
        {
            return String.Concat($"[ \"{stamp}\" : ", GetStamp(), " ]");
        }
    }
}
