using System;
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

        //pulls Name of tournament from specified file (tourneyName) from DKuploads Directory
        public static string WeeksGameInfo(string tourneyName)
        {
            string gameINFO;

            int year;

            year = int.Parse(DateTime.Now.ToString("yyyy"));

            LoadWeek(tourneyName);

            gameINFO = $"{rows[8][12]} {year}";

            return gameINFO;
        }

        //creates List of Golfers from specified file (tourneyName) from DKuploads Directory
        public static List<Golfer> WeeksDKGolfers(string tourneyName)
        {
            List<Golfer> Golfers = new List<Golfer>();

            LoadWeek(tourneyName);

            int year = int.Parse(DateTime.Now.ToString("yyyy"));
            
            foreach (string[] row in rows)
            {
                Golfer newGolfer = new Golfer()
                {
                    Name = row[9],
                    Playerid = int.Parse(row[10]),
                    Salary = int.Parse(row[11]),
                    GameInfo = $"{row[12]} {year}",
                    Website = "DK",
                    YearCreated = year,
             
                };
                Golfers.Add(newGolfer);
            }

            rows = new List<string[]>();
        
           //Golfers.RemoveRange(0, 8);
        
            return Golfers;

        }

        //pull csv file (nameOfTourney) from DKuploads and remove the junk(Directions etc.. from top of csv file)
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

            //remove Directions and other fluff from top of csv file 

            rows.RemoveRange(0, 8);

           // for (int i=0; i<8; i++)
           // {
               // string[] junks = rows[i];
               // rows.Remove(junks);
           // }

            //string[] headers = rows[0];
           // rows.Remove(headers);


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
            

            IsWeekLoaded = false;
          
        }


        /*
         * Parse a single line of a CSV file into a string array
         */
        private static string[] CSVRowToStringArray(string row, char fieldSeparator = ',') //char stringSeparator = '\"')
        {
           // bool isBetweenQuotes = false;
            StringBuilder valueBuilder = new StringBuilder();
            List<string> rowValues = new List<string>();

            // Loop through the row string one char at a time
            foreach (char c in row.ToCharArray())
            {
                if ((c == fieldSeparator)) //&& !isBetweenQuotes))
                {
                    rowValues.Add(valueBuilder.ToString());
                    valueBuilder.Clear();
                }
                else
                {
                    //if (c == stringSeparator)
                    //{
                   //     isBetweenQuotes = !isBetweenQuotes;
                   // }
                   // else
                   // {
                    valueBuilder.Append(c);
                   // }
                }
            }

            // Add the final value
            rowValues.Add(valueBuilder.ToString());
            valueBuilder.Clear();

            return rowValues.ToArray();
        }

        public static List<FDgolfer> WeeksFDgolfers(string FDtourneyName)
        {
            List<FDgolfer> fdGolfers = new List<FDgolfer>();

            FDLoadWeek(FDtourneyName);

            int year = int.Parse(DateTime.Now.ToString("yyyy"));

            foreach (string[] row in rows)
            {
                FDgolfer newGolfer = new FDgolfer()
                {
                    Name = row[13],
                    Playerid = (row[10]),
                    Salary = int.Parse(row[17]),
                    GameInfo = $"{FDtourneyName} {year}",
                    Website = "FD",
                    YearCreated = year,

                };
                fdGolfers.Add(newGolfer);
            }

            rows = new List<string[]>();
            return fdGolfers;

        }

        private static void FDLoadWeek(string FDnameOfTourney)
        {

            if (IsWeekLoaded)
            {
                return;
            }



            using (StreamReader reader = File.OpenText($"FDuploads/{FDnameOfTourney}"))
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
                      

            //remove Directions and other fluff from top of csv file 

            rows.RemoveRange(0, 7);
            


            IsWeekLoaded = false;

        }

        public static List<FDraftGolfer> WeeksFDraftGolfers(string FDraftTourneyName)
        {
            List<FDraftGolfer> fDraftGolfers = new List<FDraftGolfer>();

            FDraftLoadWeek(FDraftTourneyName);

            int year = int.Parse(DateTime.Now.ToString("yyyy"));

            foreach (string[] row in rows)
            {
                FDraftGolfer newGolfer = new FDraftGolfer()
                {
                    Name = row[9],
                    Playerid = (row[10]),
                    Salary = int.Parse(row[15]),
                    GameInfo = $"{FDraftTourneyName} {year}",
                    Website = "FantasyDraft",
                    YearCreated = year,

                };

                fDraftGolfers.Add(newGolfer);
            }

            rows = new List<string[]>();
            return fDraftGolfers;

        }

        private static void FDraftLoadWeek(string FDraftNameOfTourney)
        {

            if (IsWeekLoaded)
            {
                return;
            }



            using (StreamReader reader = File.OpenText($"FDraftUploads/{FDraftNameOfTourney}"))
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

            //remove Directions and other fluff from top of csv file 

            rows.RemoveRange(0, 1);



            IsWeekLoaded = false;

        }

    }

}
