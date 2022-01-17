using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ImgComparer
{
    class History
    {
        public enum OrderBy
        {
            RECENT,
            FREQUENT
        }

        private LinkedHashSet<string> recentCollection = new LinkedHashSet<string>();
        private IDictionary<string, int> frequentCollection = new Dictionary<string, int>();
        private int depth = -1;
        OrderBy orderBy = OrderBy.RECENT;

        private static Regex regexFreqPath = new Regex(@"([0-9]+) (.+)", RegexOptions.Compiled);
        private static Regex regexOrderBy = new Regex(@"orderby=([0-9]+)", RegexOptions.Compiled);


        public History(int depth)
        {
            this.depth = depth;
        }

        public History(int depth, OrderBy orderBy)
        {
            this.depth = depth;
            this.orderBy = orderBy;
        }

        public History(OrderBy orderBy)
        {
            this.orderBy = orderBy;
        }

        public History()
        {
        }

        private void Add(string element, int freq)
        {
            if (element != null)
            {
                switch (orderBy)
                {
                    case OrderBy.RECENT:
                        recentCollection.Remove(element);
                        recentCollection.Add(element);
                        if (depth > 0 && recentCollection.Count > depth)
                        {
                            recentCollection.Remove(recentCollection.ElementAt(0));
                        }
                        break;
                    case OrderBy.FREQUENT:
                        {
                            if (frequentCollection.ContainsKey(element))
                            {
                                frequentCollection[element]++;
                            }
                            else
                            {
                                frequentCollection.Add(element, freq);
                            }
                        }
                        break;
                }
            }
        }

        public void Add(string element)
        {
            Add(element, 1);
        }

        public void Clear()
        {
            recentCollection.Clear();
        }

        public void Remove(string element)
        {
            recentCollection.Remove(element);
        }

        public int Count
        {
            get 
            {
                switch(orderBy)
                {
                    case OrderBy.RECENT:
                        return recentCollection.Count;
                    case OrderBy.FREQUENT:
                        return frequentCollection.Count;
                }
                return 0; 
            }
        }

        public IEnumerable<string> ToList()
        {
            switch(orderBy)
            {
                case OrderBy.RECENT:
                    return recentCollection.Reverse();
                case OrderBy.FREQUENT:
                    {
                        List<string> list = new List<string>();
                        IOrderedEnumerable<KeyValuePair<string,int>> ordered = frequentCollection.OrderBy(entry => entry.Value);
                        foreach(KeyValuePair<string,int> kv in ordered)
                        {
                            list.Insert(0, kv.Key);
                        }
                        return list;
                    }
            }
            return null;            
        }

        public static History FromFile(string path)
        {
            OrderBy orderBy = OrderBy.RECENT;
            History history = null;
            if (System.IO.File.Exists(path))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(path))
                {
                    string line = sr.ReadLine();
                    Match matchOrderBy = regexOrderBy.Match(line);
                    if (matchOrderBy.Success)
                    {
                        Enum.TryParse(matchOrderBy.Groups[1].Value, out orderBy);
                        history = new History(orderBy);

                        switch (orderBy)
                        {
                            case OrderBy.RECENT:
                                while ((line = sr.ReadLine()) != null)
                                {
                                    history.Add(line);
                                }
                                break;
                            case OrderBy.FREQUENT:
                                {
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        Match match = regexFreqPath.Match(line);
                                        if (match.Success)
                                        {
                                            string strFreq = match.Groups[1].Value;
                                            int freq = 1;
                                            int.TryParse(strFreq, out freq);
                                            string filePath = match.Groups[2].Value;
                                            history.Add(filePath, freq);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            return history;
        }

        public virtual void SaveToFile(string filePath)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, false))
            {
                sw.WriteLine("orderby=" + orderBy.ToString("d"));
                switch(orderBy)
                {
                    case OrderBy.RECENT:
                        foreach (string line in recentCollection)
                        {
                            sw.WriteLine(line);
                        }
                        break;

                    case OrderBy.FREQUENT:
                        foreach (KeyValuePair<string,int> kv in frequentCollection)
                        {
                            sw.WriteLine(kv.Value.ToString() + " " + kv.Key);
                        }
                        break;
                }
                
            }
        }

        public void SetOrderBy(OrderBy orderBy)
        {
            if (this.orderBy != orderBy)
            {
                if (this.orderBy == OrderBy.RECENT && orderBy == OrderBy.FREQUENT)
                {
                    // copy from recent to frequent
                    frequentCollection.Clear();
                    foreach(string path in recentCollection)
                    {
                        frequentCollection.Add(path, 1);
                    }
                    recentCollection.Clear();
                }
                else if (this.orderBy == OrderBy.FREQUENT && orderBy == OrderBy.RECENT)
                {
                    // copy from frequent to recent
                    recentCollection.Clear();
                    foreach(string path in frequentCollection.Keys)
                    {
                        recentCollection.Add(path);
                    }
                    frequentCollection.Clear();
                }
                this.orderBy = orderBy;
            }
        }
    }
}
