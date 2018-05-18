using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MetroFramework.Extender
{
    public static class Dictionary
    {
        static List<string> myDictionary = null;

        const string filename = @"Dictionary\mydictionary.dic";
        static bool hasModified = false;

        static string GetFileName
        {
            get
            {
                return Path.Combine(Application.StartupPath, filename);
            }
        }

        public static void  Load()
        {
            myDictionary = new List<string>();
            if (File.Exists(GetFileName))
            {
                string[] file = File.ReadAllLines(GetFileName);
                for (int t = 0; t < file.Length; t++)
                { 
                        int slash = file[t].IndexOf('/');
                        if (slash > 0)
                            myDictionary.Add(file[t].Substring(0, slash));
                        else
                            myDictionary.Add(file[t]);
                }
            }
        }

        public static void Save(string text)
        {
            string[] words = text.Split('.', ' ');
            foreach (string word in words)
            {
                if (word.Length < 4)
                    continue;

                string w = word.ToLower();
                var result = (from s in myDictionary where s.Equals(w, StringComparison.CurrentCultureIgnoreCase) select s);
                if (result.Count<string>() != 0)
                    continue;

                myDictionary.Add(w);
                hasModified = true;
            }
            myDictionary.Sort();

            if (hasModified)
                File.WriteAllLines(GetFileName, myDictionary);

            hasModified = false;
        }

        static public string[] FindWords(string word)
        {
            if (myDictionary == null)
                Load();

            var result = (from s in myDictionary where s.StartsWith(word.ToLower()) select s);

           result.ToList<string>().Sort((s1, s2) => s1.CompareTo(s2));
           var resultArray = result.ToList<string>().ToArray();

           if (Char.IsUpper(word[0]))
            {
                for(int t=0;t<resultArray.Length;t++)
                    resultArray[t] = resultArray[t][0].ToString().ToUpper() + resultArray[t].Remove(0, 1);
            }

            return resultArray;
        }
    }
}
