using System;
using System.Collections.Generic;
using System.IO;

namespace WordPredictor
{
    public static class TrainingFactory
    {
        /// <summary>
        /// Train the predictor on a large text file as input
        /// </summary>
        /// <param name="target"></param>
        /// <param name="fileLocation"></param>
        /// <param name="isLog"></param>
        public static Predictor BookTrain(Predictor target, string fileLocation, bool isLog)
        {
            string line;
            int counter = 0;
            StreamReader file = new StreamReader(fileLocation);
            var beginTime = DateTime.Now;

            while ((line = file.ReadLine()) != null)
            {
                target.Predict(line, false);
                if(isLog) Console.WriteLine("Predictor Trained on ... " + line);
                counter++;
            }
            var endTime = DateTime.Now;
            var duration = endTime - beginTime;

            if (isLog)
            {
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine("Trained " + counter + " lines for " + duration.TotalSeconds + "s");
                Console.WriteLine("***************************************************************************");
                Console.WriteLine("***************************************************************************");
            }

            return target;
        }
        
        /// <summary>
        /// Print out first 4 predictioins to Console
        /// </summary>
        /// <param name="predict"></param>
        public static void ConsoleLogPredictions(List<string> predict)
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