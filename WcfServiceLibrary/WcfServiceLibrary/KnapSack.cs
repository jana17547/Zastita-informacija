using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibrary
{
    class KnapSack
    {
        public  int[] Encrypt(int[] m, int[] pub)
        {
            int[] c = new int[m.Length];
            for (int i = 0; i < m.Length; i++)
            {
                c[i] = 0;
                for (int j = 0; j < pub.Length; j++)
                {
                    if ((m[i] & (1 << j)) != 0)
                    {
                        c[i] += pub[j];
                    }
                }
            }
            return c;
        }

        public  int[] Decrypt(int[] c, int[] priv, int mod)
        {
            int[] m = new int[c.Length];
            for (int i = 0; i < c.Length; i++)
            {
                m[i] = 0;
                int w = c[i];
                for (int j = priv.Length - 1; j >= 0; j--)
                {
                    if (w >= priv[j])
                    {
                        w -= priv[j];
                        m[i] |= 1 << j;
                    }
                }
            }
            return m;
        }


    }
}
