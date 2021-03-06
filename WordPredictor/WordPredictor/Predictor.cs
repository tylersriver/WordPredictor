﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordPredictor
{
    public class Predictor
    {
        // -- Variables
        // -------------------------------------------------------------------------------------------------------------
        private readonly Dictionary<string, Dictionary<string, int>> 
            _items = new Dictionary<string, Dictionary<string, int>>(); // Dictonary of strings to lookup
        
        private readonly char[] _tokenDelimeter = {' '}; // Delimeter between words
        
        // -- Methods
        // -------------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Train the predictor with a string, and return the 
        /// predictions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<string> Predict(string input)
        {
            // -- Variables -- //
            var tokens = input.Split(_tokenDelimeter, StringSplitOptions.RemoveEmptyEntries); // words in input
            var previousBuilder = new StringBuilder(); //
            Dictionary<string, int> nextFullList;
            
            // Tokens are the words in the given input //
            foreach (var token in tokens)
            {
                nextFullList = GetOrCreate(_items, previousBuilder.ToString());
                if (nextFullList.ContainsKey(token))
                {
                    nextFullList[token] += 1;
                }
                else
                {
                    nextFullList.Add(token, 1);
                }

                if (previousBuilder.Length > 0)
                {
                    previousBuilder.Append(" ");
                }
                previousBuilder.Append(token);
            }
            
            nextFullList = GetOrCreate(_items, previousBuilder.ToString());
            
            var prediction = (from x in nextFullList
                orderby x.Value descending
                select x.Key).ToList();

            return CleanEntries(prediction);
        }
        
        /// <summary>
        /// Get or create the prediction
        /// </summary>
        /// <param name="d"></param>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T GetOrCreate<T>(Dictionary<string, T> d, string key)
        {
            if (d.ContainsKey(key))
            {
                return d[key];
            }
            var t = Activator.CreateInstance<T>();
            d.Add(key, t);
            return t;
        }

        /// <summary>
        /// Clean predictions of erroneous special characters
        /// </summary>
        /// <param name="stringList"></param>
        /// <returns></returns>
        public static List<string> CleanEntries(List<string> stringList)
        {
            var clean = new List<string>();
            foreach (var word in stringList)
            {
                var cleanWord = word.Replace(".", "");
                cleanWord = cleanWord.Replace(",", "");
                cleanWord = cleanWord.Replace("'", "");
                cleanWord = cleanWord.Replace("\"", "");
                cleanWord = cleanWord.Replace(")", "");
                cleanWord = cleanWord.Replace("(", "");
                cleanWord = cleanWord.Replace(":", "");
                cleanWord = cleanWord.Replace(";", "");
                cleanWord = cleanWord.Replace("?", "");
                cleanWord = cleanWord.Replace("!", "");
                cleanWord = cleanWord.Replace("\\", "");
                cleanWord = cleanWord.Replace("/", "");
                cleanWord = cleanWord.Replace("`", "");
                cleanWord = cleanWord.Replace("-", "");
                cleanWord = cleanWord.Replace("~", "");
                cleanWord = cleanWord.Replace("*", "");
                cleanWord = cleanWord.Replace("=", "");
                cleanWord = cleanWord.Replace("+", "");
                clean.Add(cleanWord);
            }
            return clean;
        }
    }
}