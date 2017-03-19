using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPower
{
    class FastPower
    {
        static void Main(string[] args)
        {
            Console.WriteLine("{0}", DoFastPower(2, 256));
        }

        static int DoFastPower(int a, int b) {
            int ans = 0;

            if (b == 1)
            {
                return a;
            }
            else
            {
                int c = a*a;
                ans = DoFastPower(c, b / 2);

            }

            if (b % 2 != 0)
            {
                return a * ans;
            }
            else
            {
                return ans;
            }
        }
    }
}
