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
            const string trainFile = "/Users/tyler.w.sriver/Documents/GitHub Repos/WordPredictor/WordPredictor/PredictorTest/training.txt";
            BookTrain(target, trainFile, true);
            bool finish = false;

            do // ** Accept predict entries ** //
            {
                // Get Line and Print: 
                string word = Console.ReadLine(); // Read input
                var predictions = target.Predict(word); // Get Predictions
                LogPredictions(predictions); // Print the predictions

                // Ending?
                if (word == "000")
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
        /// <param name="isLog"></param>
        public static void BookTrain(Predictor target, string fileLocation, bool isLog)
        {
            string line;
            int counter = 0;
            StreamReader file = new StreamReader(fileLocation);
            var beginTime = DateTime.Now;

            while ((line = file.ReadLine()) != null)
            {
                target.Predict(line);
                if(isLog) Console.WriteLine("Predictor Trained on ... " + line);
                counter++;
            }
            var endTime = DateTime.Now;
            var duration = endTime - beginTime;

            if (!isLog) return;
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("Trained " + counter + " lines for " + duration.TotalSeconds + "s");
            Console.WriteLine("***************************************************************************");
            Console.WriteLine("***************************************************************************");
        }

        /// <summary>
        /// Print out first 4 predictioins to Console
        /// </summary>
        /// <param name="predict"></param>
        public static void LogPredictions(List<string> predict)
        {
            // Assign count based on num entries 
            // To display no more than 4
            int count =
                (predict.Count < 4)
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
    }
}