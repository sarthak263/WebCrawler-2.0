using System;
using System.Net;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WikipediaCrawler
{
    internal class Crawler
    {
        #region Class Variables
        protected string title = "";
        protected string WikiUrl = @"http://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&explaintext=1&titles=";
        protected JObject jsonData;
        protected Dictionary<string, int> tags = new Dictionary<string, int>();
        #endregion

        #region Constructor
        public Crawler(string title)
        {
            this.title = title;
            this.WikiUrl += this.title;
            this.getData(); 
            this.countWords();
        }
        #endregion

        #region Method to get the data from Wiki URL in JSON
        private void getData()
        {
            string responseString = string.Empty;
            using (var webClient = new WebClient())
            {
                responseString = webClient.DownloadString(this.WikiUrl);
                this.jsonData = JObject.Parse(responseString);
            }
        }
        #endregion

        #region Method to parse only the history contents of the wikipedia
        private string[] ParseData()
        {
            //gets all the paragraphs contents
            var contents = this.jsonData["query"]["pages"]["19001"]["extract"].ToString();

            #region gets all the words between these two strings
            string s1 = "== History ==";
            string s2 = "== Corporate affairs ==";
            int pos1 = contents.IndexOf(s1) + s1.Length;
            int pos2 = contents.IndexOf(s2);
            string res = contents.Substring(pos1,pos2-pos1);
            return res.Split(" ");
            #endregion

        }
        #endregion

        #region Method to count the Occurances of the words and add to dictionary "tags"
        private void countWords()
        {
            foreach(string val in this.ParseData())
            {
                if (tags.ContainsKey(val))
                {
                    tags[val] = tags[val]+ 1;
                }else
                {
                   tags.Add(val, 1);
                }
            }
        }
        #endregion

        #region Prints the top 10 occurances of the words
        public void printData()
        {
            Console.WriteLine("****WORD COUNT****\n");
            int i = 1;
            foreach(KeyValuePair<string,int> kvp in tags.OrderByDescending(key=>key.Value))
            {
                Console.WriteLine("{0} : {1}",kvp.Key,kvp.Value);
                i++;
                if (i > 10) break;
            }
        }
        #endregion

        #region getters methods
        public string getUrl()
        {
            return this.WikiUrl;
        }

        public JObject getJsonData()
        {
            return this.jsonData;
        }
        #endregion
    }

}