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
            target = TrainingFactory.BookTrain(target, trainFile, true);
            bool finish = false;

            do // ** Accept predict entries ** //
            {
                // Get Line and Print: 
                string word = Console.ReadLine(); // Read input
                var predictions = target.Predict(word, true); // Get Predictions
                TrainingFactory.ConsoleLogPredictions(predictions); // Print the predictions

                // Ending?
                if (word == "000")
                {
                    finish = true;
                }
            } while (!finish);
            Console.WriteLine("Shutting down ... ");

        } // END MAIN
    }
}