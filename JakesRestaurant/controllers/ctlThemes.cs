using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using management;

namespace controllers
{
    public class TctlThemes
    {
        string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\", "themes.json");
        private List<Theme> d_themes;
        public List<Theme> GetThemes()
        {
            return d_themes;
        }
        public TctlThemes()
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
                d_themes = JsonSerializer.Deserialize<List<Theme>>(json);
            }
            else
            {
                d_themes = new List<Theme>();
            }
        }
        public void Write()
        {
            string json = JsonSerializer.Serialize(d_themes);
            File.WriteAllText(path, json);
        }
        public void UpdateList(Theme p)
        {
            int index = d_themes.FindIndex(s => s.ID == p.ID);
            if (index != -1)
            {
                d_themes[index] = p;
            }
            else
            {
                d_themes.Add(p);
            }
            Write();
        }
        public bool Delete(int aID)
        {
            if (d_themes.Find(s => s.ID == aID) != null)
            {
                d_themes.Remove(d_themes.Find(s => s.ID == aID));
                Write();
                return true;
            }
                return false;
        }
        public Theme GetByID(int id)
        {
            return d_themes.Find(i => i.ID == id);
        }
        public int IncrementID()
        {
            if (d_themes.Any())
                return d_themes.Last().ID + 1;
            else
                return 1;
        }
    }
}