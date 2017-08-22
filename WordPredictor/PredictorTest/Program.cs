using System;
using System.Collections.Generic;
using System.IO;
using WordPredictor;

namespace PredictorTest
{
    class Program
    {
        static void Main(string[] args)
        {

            // Instantiate predictor and train it
            // ------------------------------------------------
            Predictor target = new Predictor();
            BookTrain(target, "/Users/tyler.w.sriver/Downloads/training.txt");
            bool finish = false;

            // -- Accept predict entries
            // ------------------------------------------
            while (!finish)
            {
                // Get Line and Print:
                // ------------------------------------
                string word = Console.ReadLine();
                PrintPredictions(CleanEntries(target.Predict(word)));

                // Ending?:
                // -----------------------------------
                if (word == "end")
                {
                    finish = true;
                }
            }

        } // END

        public static void BookTrain(Predictor target, string fileLocation)
        {
            string line;
            int counter = 0;
            StreamReader file = new StreamReader(fileLocation);
            var beginTime = DateTime.Now;

            while ((line = file.ReadLine()) != null)
            {
                target.Predict(line);
                Console.WriteLine("Predictor Trained on ... " + line);
                counter++;
            }
            var endTime = DateTime.Now;
            var duration = endTime - beginTime;

            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Trained " + counter + " lines for " + duration.TotalSeconds + "s");
            Console.WriteLine("***************************************************************************");
            Console.WriteLine("***************************************************************************");
        }

        public static void PrintPredictions(List<string> predict)
        {
            Console.WriteLine("***************************************************************************");
            Console.WriteLine("Total Predictions: " + predict.Count);
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine();
            for (int i = 0; i < predict.Count; i++)
            {
                if (i < 3)
                {
                    Console.Write(predict[i] + ", ");
                }
                else
                {
                    Console.Write(predict[i]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine();

        }

        public static List<string> CleanEntries(List<string> stringList)
        {
            List<string> clean = new List<string>();
            foreach (string word in stringList)
            {
                string cleanWord = word.Replace(".", "");
                cleanWord = word.Replace(",", "");
                cleanWord = word.Replace("'", "");
                cleanWord = word.Replace("\"", "");
                cleanWord = word.Replace(")", "");
                cleanWord = word.Replace("(", "");
                cleanWord = word.Replace(":", "");
                cleanWord = word.Replace(";", "");
                cleanWord = word.Replace("?", "");
                cleanWord = word.Replace("!", "");
                cleanWord = word.Replace("\\", "");
                cleanWord = word.Replace("/", "");
                cleanWord = word.Replace("`", "");
                cleanWord = word.Replace("-", "");
                cleanWord = word.Replace("~", "");
                cleanWord = word.Replace("*", "");
                cleanWord = word.Replace("=", "");
                cleanWord = word.Replace("+", "");
                clean.Add(cleanWord);

            }
            return clean;
        } 
    }
}