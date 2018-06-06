using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarScales
{
    class ScaleG
    {
        public static Dictionary<string, List<Notes>> Scales = new Dictionary<string, List<Notes>>();
        //public static List<Notes> scale = new List<Notes>();

        public enum Notes
        {
            C,
            Cis,
            D,
            Dis,
            E,
            F,
            Fis,
            G,
            Gis,
            A,
            Ais,
            H,
        };

        static public void CreateScales()
        {
            for (int i = 0; i < 12; i++)
            {
                List<Notes> tmp = new List<Notes>();

                for (int j = 0; j < 7; j++)
                {
                    if (j > 2)
                    {
                        tmp.Add((Notes)((i + 2 * j - 1) % 12));
                    }
                    else
                        tmp.Add((Notes)((i + 2 * j) % 12));
                }
                Scales.Add(((Notes)i).ToString() + "-Dur", tmp);
            }
        }

        static public string CompareToScales(List<Notes> scale)
        {
            string tmp = null;
            bool fit = false;
            string FittingScales = null;

            foreach (List<Notes> s in Scales.Values)
            {
                foreach (Notes n in scale)
                {
                    if (!s.Contains(n))
                    {
                        fit = false;
                        tmp = null;
                        break;
                    }
                    else
                    {
                        fit = true;
                    }
                }

                if (fit)
                {
                    foreach (Notes note in s)
                    {
                        tmp += note.ToString() + "\t";
                    }
                    tmp += "\n";
                }
                FittingScales += tmp;
                tmp = null;
            }

            if (FittingScales.Length == 0 && scale.Count != 0)
            {
                FittingScales = "No matching scales";
            }
            return FittingScales;
        }
    }
}
