using MiniFRC_FMS.Modules.Game.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.DataSaving
{
    internal class DataSavingItem<T> where T : class, new() 
    {
        private readonly string Filepath;

        private List<T> items = new List<T>();

        public DataSavingItem(string filepath)
        {
            Filepath = filepath;
            LoadItems();
        }

        public void SaveItems()
        {
            if (!File.Exists(Filepath)) File.Create(Filepath).Close();
            File.WriteAllText(Filepath, JsonConvert.SerializeObject(items));
        }

        private void LoadItems()
        {
            if (!File.Exists(Filepath)) return;
            items = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(Filepath));
        }

        public void Add(T item)
        {
            items.Add(item);
            SaveItems();
        }

        public bool Remove(T item)
        {
            bool res = items.Remove(item);
            SaveItems();
            return res;
        }

        public void Clear()
        {
            items.Clear();
            SaveItems();
        }
        public bool Contains(T item) => items.Contains(item);

        public IReadOnlyList<T> GetAll()
        {
            return items.AsReadOnly();
        }

        public IReadOnlyList<T> GetWhere(Func<T, bool> condition)
        {
            return items.Where(condition).ToList().AsReadOnly();
        }


        
    }
}
