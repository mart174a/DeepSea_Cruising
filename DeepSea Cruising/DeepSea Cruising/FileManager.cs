using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeepSea_Cruising
{
    static class FileManager
    {
        public static List<string> GetDataStringListFromFile(string fileName)
        {
            List<string> listToReturn = new List<string>();

            //Converts all text from file to a string
            var fileText = File.ReadAllText(fileName);
            //Splits the string at every '|'
            var fileLines = fileText.Split('|');
            //Adds the the data to the data list
            foreach (string dataLine in fileLines)
            {
                listToReturn.Add(dataLine);
            }

            return listToReturn;
        }
        public static void StoreDataListInFile(string fileName, List<string> dataListToConvert)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int i;
            for (i = 0; i < dataListToConvert.Count - 1; i++)
            {
                stringBuilder.Append(dataListToConvert[i] + "|");
            }
            stringBuilder.Append(dataListToConvert[i]);
            File.WriteAllText(fileName, stringBuilder.ToString());
        }
        public static int GetValidtNewID(string folderName)
        {
            List<int> idList = new List<int>();

            foreach (var filePath in Directory.GetFiles(folderName))
            {
                int id = int.Parse(GetDataStringListFromFile(filePath)[0]);
                idList.Add(id);
            }

            for (int i = 0; i < 999; i++)
            {
                if (!idList.Contains(i))
                {
                    return i;
                }
            }
            return 1000;
        }
    }
}
