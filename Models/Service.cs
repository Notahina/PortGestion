using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PortGestion.Models
{
    public class Service
    {
        public string NumberToString(string montant)
        {
            
            string[] split = montant.Split(',');
            
            decimal num1 = Convert.ToDecimal(split[0]);
            decimal num2 = Convert.ToDecimal(split[1]);
            return num1 + "." + num2;
        }
        public string[] getRegex(string input)
        {
            List<string> val = new List<string>();
            string pattern = @"\b[QT]\w+";
            Regex rgx = new Regex(pattern);

            MatchCollection matchedAuthors = rgx.Matches(input);
            for (int count = 0; count < matchedAuthors.Count; count++)
            {
                val.Add(matchedAuthors[count].Value);
            }
            return val.ToArray();
        }
        public int Diff2DateMinute(DateTime date2,DateTime date1)
        {
            TimeSpan time = date2 - date1;
            return (int)time.TotalMinutes;
        }
        public DateTime SetHeure(DateTime date, string Heure)
        {
            string[] sub = Heure.Split(':');
            int h = Convert.ToInt32(sub[0]);
            int mn = Convert.ToInt32(sub[1]);
            //  int s = Convert.ToInt32(sub[2]);
            DateTime t = date;
            t = t.AddHours(h);
            t = t.AddMinutes(mn);
            //t= date.AddSeconds(s);
            return t;
        }
        public float CalculMontantPremier(int quai, int events, float total)
        {
            int val = -events;
            float t = 0;
            while ((-val) >= quai)
            {

                val = quai + val;

                t = total + t;

            }
            return t;
        }
    }
}