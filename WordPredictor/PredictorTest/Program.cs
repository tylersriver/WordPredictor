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
            // ** Instantiate predictor and train it ** //
            Predictor target = new Predictor();
            BookTrain(target, "/Users/tyler.w.sriver/Downloads/training.txt");
            bool finish = false;

            do // ** Accept predict entries ** //
            {
                // Get Line and Print: 
                string word = Console.ReadLine(); // Read input
                var predictions = target.Predict(word); // Get Predictions
                predictions = CleanEntries(predictions); // Clean the words
                PrintPredictions(predictions); // Print the predictions

                // Ending?
                if (word == "end")
                {
                    finish = true;
                }
            } while (!finish);
            Console.WriteLine("Shutting down ... ");

        } // END MAIN

        /// <summary>
        /// Train the predictor on a large text file as input
        /// </summary>
        /// <param name="target"></param>
        /// <param name="fileLocation"></param>
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

        /// <summary>
        /// Print out first 4 predictioins to Console
        /// </summary>
        /// <param name="predict"></param>
        public static void PrintPredictions(List<string> predict)
        {
            // Assign count based on num entries 
            // To display no more than 4
            int count = 
                ( predict.Count < 4 ) 
                    ? predict.Count 
                    : 4;
            
            Console.WriteLine("***************************************************************************");
            Console.WriteLine("Total Predictions: " + predict.Count);
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine();
            for (int i = 0; i < count; i++)
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
        }

        /// <summary>
        /// Clean predictions of erroneous special characters
        /// </summary>
        /// <param name="stringList"></param>
        /// <returns></returns>
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