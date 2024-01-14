using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace DemoWorker
{
    public class Scraper
    {
        public static List<Dictionary<string, string>> rowValues { get; set; } = new List <Dictionary<string, string>>();
        public static async Task<List<Dictionary<string, string>>> CseScraper () {
            string url = "https://www.cse.com.bd/market/current_price"; // Replace this with your URL
            string[] Columnkeys = new string[] { "SL", "StockCode", "LTP", "Open", "High", "Low", "YCP", "Trade", "Value", "Volume" };


            try
            {
                // Download the HTML content from the URL
                string htmlContent = await  DownloadHtmlContent(url);
                //Console.WriteLine(htmlContent);
                //string outputFilePath = "output.html";
                //File.WriteAllText(outputFilePath, htmlContent);
                //Console.WriteLine("HTML content has been written to '{0}'", outputFilePath);


                // Load the HTML content into the HTML Agility Pack's document
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                // Find the table element by its ID
                HtmlNode table = htmlDocument.GetElementbyId("dataTable");

                if (table != null)
                {
                    // Get all the rows in the table's body
                    HtmlNodeCollection rows = table.SelectNodes(".//tbody/tr");

                    if (rows != null)
                    {
                        
                        foreach (HtmlNode row in rows)
                        {
                            // Get all the cells in the row
                            HtmlNodeCollection cells = row.SelectNodes(".//td");
                            int index = 0;

                            if (cells != null)
                            {
                                var tempDict = new Dictionary<string, string>();
                                foreach (HtmlNode cell in cells)
                                {
                                    // Extract the data from each cell
                                    string cellText = cell.InnerText.Trim();
                                    //Console.Write(Columnkeys[index] + " " +  cellText + "\t");
                                    
                                    tempDict[Columnkeys[index]] = cellText;
                                    
                                    index++;
                                }
                                rowValues.Add(tempDict);
                                

                                //Console.WriteLine(); // Move to the next line after each row
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return rowValues;

        }

        static async Task<string> DownloadHtmlContent(string url)
        {
            using (WebClient client = new WebClient())
            {
                return  client.DownloadString(url);
            }
        }
    }
}
