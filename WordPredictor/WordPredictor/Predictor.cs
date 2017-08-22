using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordPredictor
{
    public class Predictor
    {
        private readonly Dictionary<string, Dictionary<string, int>> 
            _items = new Dictionary<string, Dictionary<string, int>>();
        private readonly char[] _tokenDelimeter = {' '};
        
        /// <summary>
        /// Train the predictor with a string, and return the 
        /// predictions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<string> Predict(string input)
        {
            var tokens = input.Split(_tokenDelimeter, StringSplitOptions.RemoveEmptyEntries);
            var previousBuilder = new StringBuilder();
            Dictionary<string, int> nextFullList;
            foreach (var token in tokens)
            {
                nextFullList = GetOrCreate(_items, previousBuilder.ToString());
                if (nextFullList.ContainsKey(token))
                    nextFullList[token] += 1;
                else
                    nextFullList.Add(token, 1);

                if (previousBuilder.Length > 0)
                    previousBuilder.Append(" ");
                previousBuilder.Append(token);
            }
            nextFullList = GetOrCreate(_items, previousBuilder.ToString());
            var prediction = (from x in nextFullList
                orderby x.Value descending
                select x.Key).ToList();

            return prediction;
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
    }
}