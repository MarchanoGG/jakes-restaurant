using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace controllers
{
    class ctlOpeningTimes
    {
        static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "times.json");
        public List<doOpeningTimes> listItems;
        public List<doOpeningTimes> GetOpeningTimes()
        {
            listItems = ReadList<doOpeningTimes>(path);
            return listItems;
        }

        public ctlOpeningTimes()
        {
            Load();
        }
        public void Load()
        {
            if (!File.Exists(path))
            {
                // Create products from json 
                var myFile = File.Create(path);
                myFile.Close();
            }

            string json = File.ReadAllText(path);
            if (json != "")
            {
                listItems = JsonSerializer.Deserialize<List<doOpeningTimes>>(json);
            }
            else
            {
                listItems = new List<doOpeningTimes>();
            }
        }

        public bool UpdateTime(doOpeningTimes aTime)
        {
            bool res = false;

            List<doOpeningTimes> existingUsers = ReadList<doOpeningTimes>(path);

            doOpeningTimes myTime = existingUsers.Find(match: i => i.ID == aTime.ID);

            if (myTime != null)
            {
                existingUsers[existingUsers.IndexOf(myTime)] = aTime;

                WriteList(path, existingUsers);

                res = true;
            }

            return res;
        }

        public static T Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);

            T res = JsonSerializer.Deserialize<T>(text);

            return res;
        }

        public static List<T> ReadList<T>(string filePath)
        {
            List<T> res = new List<T>();
            string text = File.ReadAllText(filePath);

            T[] arr = JsonSerializer.Deserialize<T[]>(text);

            foreach (T obj in arr)
            {
                res.Add(obj);
            }

            return res;
        }

        public void WriteList<T>(string filePath, List<T> aList)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(aList));
        }
        public void UpdateFristTime()
        {
            foreach (var a in listItems)
            {
                switch (a.ID)
                {
                    case 1:
                        a.Dayofweek = DayOfWeek.Monday;
                        break;
                    case 2:
                        a.Dayofweek = DayOfWeek.Tuesday;
                        break;
                    case 3:
                        a.Dayofweek = DayOfWeek.Wednesday;
                        break;
                    case 4:
                        a.Dayofweek = DayOfWeek.Thursday;
                        break;
                    case 5:
                        a.Dayofweek = DayOfWeek.Friday;
                        break;
                    case 6:
                        a.Dayofweek = DayOfWeek.Saturday;
                        break;
                    case 7:
                        a.Dayofweek = DayOfWeek.Sunday;
                        break;
                    default:
                        break;
                }
                UpdateTime(a);
            }
        }

        public doOpeningTimes FindByDateTime(DateTime dt)
        {
            return listItems.Find(i => i.Dayofweek == dt.DayOfWeek);
        }
        public bool IsAvailable(DateTime dt)
        {
            bool result = false;
            doOpeningTimes ot = FindByDateTime(dt);
            if (ot != null)
            {
                if (ot.Opened) result = true;
                DateTime baseDate = new DateTime(dt.Year, dt.Month, dt.Day);
                DateTime startTime = baseDate.AddHours(ot.StartingHours);
                DateTime endTime = baseDate.AddHours(ot.ClosingHours);
                if (startTime < dt) result = true;
                if (endTime > dt) result = true;
            }
            return result;
        }
    }
}
