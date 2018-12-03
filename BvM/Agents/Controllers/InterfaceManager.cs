using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BvM.Controllers
{
    /// <summary>
    /// Handler for all communication with the user - getting input, providing output
    /// [thinking about giving the InterfaceManager the task of handling the file data; see MENACE logs]
    /// </summary>
    public class InterfaceManager
    {
        private const string dataFilePath = "../../../Data/";
        Dictionary<string, string> fileNames;

        /// <summary>
        /// Blank constructor, loading default files
        /// </summary>
        public InterfaceManager()
        {
            fileNames = new Dictionary<string, string>();
            Console.WriteLine(RecordSystemCheck());
        }

        [StatusAttribute(ElementStatus.UnderConstruction)]
        private bool RecordSystemCheck()
        {
            fileNames.Add("temp", "tempData.bvm");
            fileNames.Add("logBook", "logBook.bvm");

            string logBookPath = FullPath("logBook");
            bool continuousGame = false;

            // open logbook and try to get the game status (old/new/reset) and the codes and names of all the other files
            // TODO : INTERFACE: Try-Catch for loading the files
            try
            {
                StreamReader reader = new StreamReader(logBookPath);
                continuousGame = reader.ReadLine() == "old"; // TODO : REWORK! : turn this into a three case switch
                // read the last 3 file names
                for (int counter = 0; counter < 3; counter++)
                {
                    try
                    {
                        string[] fileInfo = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        fileNames.Add(fileInfo[0], fileInfo[1]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        throw new ArgumentException(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException(ex.Message);
            }

            // TODO : Check if all files are present if needed
            if (continuousGame)
            {
                try
                {
                    string statsPath = FullPath(FileNameList.history.ToString());
                    string historyPath = FullPath(FileNameList.history.ToString());
                    string menacePath = FullPath(FileNameList.menace.ToString());
                    StreamReader statsReader = new StreamReader(statsPath);
                    StreamReader historyReader = new StreamReader(historyPath);
                    StreamReader menaceReader = new StreamReader(menacePath);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
            else // TODO : HERE : if there should be a new game -- reset history, DOT NOT RESET menace
            {
                bool resetHistory = ResetFile(FullPath(FileNameList.history.ToString()));
            }

            return true;
        }

        private bool ResetFile(string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath, false);
            writer.Write("");
            writer.Flush();
            return true;
        }

        private string FullPath(string fileName)
        {
            return Path.Combine(dataFilePath, fileNames[fileName]);
        }

        /// <summary>
        /// Records data in a text file
        /// </summary>
        /// <param name="append">'True' to append the file, 'False' to overwrite it</param>
        /// <param name="repo">Name of the repository - stats, history, menace</param>
        /// <param name="data">String to be appendedwritten in the repo</param>
        /// <returns></returns>
        public string Record(bool append, FileNameList repo, string data)
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
            string fullPath = FullPath(repo.ToString());
            StreamWriter recorder = new StreamWriter(fullPath, append);
            string timeStamp = TimeStamp("something wicked this way comes");
            recorder.WriteLine(timeStamp);
            recorder.WriteLine(data);
            recorder.Flush();
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

    /// <summary>
    /// An ENUM listing the types of record files to be kept
    /// </summary>
    public enum FileNameList
    {
        logBook = 0,
        stats = 1,
        history = 2,
        menace = 3,
        temp = 4
    }
}
