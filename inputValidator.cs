using System;
using System.Linq;

namespace inputChecker
{
    public class validator
    {
        public static bool[] strInputValidator(object input, string critera)
        {
            bool[] passer = new bool[6] { true, true, true, true, true, true };
            string inAsString = "";
            if (input is null)
            {
                passer[0] = false;
            }
            if (critera.Contains('1') && passer[0])
                {
                    inAsString = input.ToString();
                    if (inAsString == "")
                    {
                        passer[1] = false;
                    }
                }
            if (critera.Contains('2') && passer[0])
            {
                inAsString = input.ToString();
                passer[2] = int.TryParse(inAsString, out int i);
            }
            if (critera.Contains('3') && passer[0])
            {
                inAsString = input.ToString();
                passer[3] = double.TryParse(inAsString, out double i);
            }
            if (critera.Contains('4') && passer[0])
            {
                inAsString = input.ToString();
                passer[4] = false;
                char[] num = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                foreach (char c in num)
                {
                    if (inAsString.Contains(c))
                    {
                        passer[4] = true;
                    }
                }
            }
            if (critera.Contains('5') && passer[0])
                {
                passer[5] = false;
                }
            if (critera.Contains("printMe"))
            {
                for (int i = 0; i < passer.Length; i++)
                {
                    Console.WriteLine(passer[i]);
                }
            }
            return passer;
        }
    }
}