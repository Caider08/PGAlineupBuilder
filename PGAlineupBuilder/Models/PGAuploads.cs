﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGAlineupBuilder.Models
{
    public class PGAuploads
    {
        public static bool IsWeekLoaded = false;

        public static List<string[]> rows = new List<string[]>();

        public static List<string> WeeksGolfers(string tourneyName)
        {
            List<string> Golfers = new List<string>();

            LoadWeek(tourneyName);

            foreach (string[] row in rows)
            {
                Golfers.Add(row[9]);
            }

            return Golfers;

        }


        private static void LoadWeek(string nameOfTourney)
        {

            if (IsWeekLoaded)
            {
                return;
            }

            

            using (StreamReader reader = File.OpenText($"DKuploads/{nameOfTourney}"))
            {
                while (reader.Peek() >= 0)
                {
                    string line = reader.ReadLine();
                    string[] rowArrray = CSVRowToStringArray(line);
                    if (rowArrray.Length > 0)
                    {
                        rows.Add(rowArrray);
                    }
                }
            }
            for (int i=0; i<7; i++)
            {
                string[] junks = rows[i];
                rows.Remove(junks);
            }

            string[] headers = rows[7];
            rows.Remove(headers);


            // Parse each row array into a more friendly Dictionary
            //foreach (string[] row in rows)
            //{
            //  Dictionary<string, string> rowDict = new Dictionary<string, string>();

            //for (int i = 9; i < 12; i++)
            //{
            //   rowDict.Add(headers[i], row[i]);
            //  }
            // AllJobs.Add(rowDict);
            //   }
            

            IsWeekLoaded = true;
          
        }


        /*
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',', char stringSeparator = '\"')
        {
            bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator && !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    if (c == stringSeparator)
                    {
                        isBetweenQuotes = !isBetweenQuotes;
                    }
                    else
                    {
                        valueBuilder.Append(c);
                    }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }
    }

}