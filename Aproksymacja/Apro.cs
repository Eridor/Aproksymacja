using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aproksymacja
{
    public class Apro
    {
        // Tablica wartości funkcji
        public static int[] f = { 3, 3, 3, 0, 1, 3, 1, 1, 0, 0, 3, 3, 1, 2, 2, 2 };

        // Wielkość ciągu wyjściowego
        public static int Ydiff = 4;

        // Wielkość ciągu wejściowego
        public static int Xdiff = 16;

        // Ilość bitów wejściowych
        public static int N = 4;

        // Ilość bitów wyjściowych
        public static int M = 2;

        /// <summary>
        /// Funkcje zwracające tablice danych
        /// </summary>
        public int[,] runD()
        {
            return Tdf(Xdiff, Ydiff, f, N, M);
        }
        public int[,] runA()
        {
            return Taf(Xdiff, Ydiff, f, N, M);
        }

        /// <summary>
        /// 
        /// Aproksymacja różnicowa
        /// 
        /// </summary>
        private int[,] Tdf(int xdiff, int ydiff, int[] f, int n, int m)
        {
            // Tworzenie tablicy wyjściowej
            int[,] w = new int[xdiff, ydiff];

            // Dla każdego X'
            for (int ix = 0; ix < xdiff; ix++)
            {
                // Dla każdego Y'
                for (int iy = 0; iy < ydiff; iy++)
                {
                    // Pobierz wartość z funkcji N
                    w[ix,iy] = Ntdf(ix, iy, f, n, m);
                }
            }
            return w;
        }

        /// <summary>
        /// Aproksymacja różnicowa
        /// Funkcja zwracająca wartość pojedynczego elementu tabeli aproksymacji
        /// </summary>
        private int Ntdf(int xdiff, int ydiff, int[] f, int n, int m)
        {
            // Inicjaliacja zmiennej zliczającej
            int w = 0;

            // Pętla przechodząca po każdej wartości bitowej
            for (int x = 0; x < (Math.Pow(2, n)); x++)
            {
                // Wyliczenie wartości fw = f(x) xor f(x xor x')
                int f1 = f[x];
                int f2 = f[x ^ xdiff];
                int fw = f1 ^ f2;

                // Sprawdzenie warunku równości wyniku funkcji oraz y'
                if (fw == ydiff)
                {
                    // Zwiększenie licznika wyniku
                    w++;
                }
            }
            return w;
        }





       /// <summary>
       /// 
       /// Aproksymacja liniowa
       /// 
       /// </summary>
        private int[,] Taf(int xdiff, int ydiff, int[] f, int n, int m)
        {
            // Tworzenie tablicy wyjściowej
            int[,] w = new int[xdiff, ydiff];

            // Dla każdego X'
            for (int ix = 0; ix < xdiff; ix++)
            {
                // Dla każdego Y'
                for (int iy = 0; iy < ydiff; iy++)
                {
                    // Pobierz wartość z funkcji N
                    w[ix, iy] = Ntaf(ix, iy, f, n, m);
                }
            }
            return w;
        }

        /// <summary>
        /// Aproksymacja liniowa
        /// Funkcja zwracająca wartość pojedynczego elementu tabeli aproksymacji
        /// </summary>
        private int Ntaf(int xdiff, int ydiff, int[] f, int n, int m)
        {
            // Inicjaliacja zmiennej zliczającej
            int w = 0;

            // Pętla przechodząca po każdej wartości bitowej
            for (int x = 0; x < (Math.Pow(2, n)); x++)
            {
                // Pobranie wartości funkcji
                int y = f[x];

                // Zmienne pomocnicze - nakładanie maski
                int xand = x & xdiff;
                int yand = y & ydiff;

                // Warunek równości wyników funkcji pomocniczej xorowania bitowego
                if (BIT_XOR(xand, n) == BIT_XOR(yand, m))
                {
                    w++;
                }
            }
            // Zwracanie wyniku pomniejszonego o połowę ilości kombinacji bitowych
            return w - (int)Math.Pow(2,n-1);
        }

        /// <summary>
        /// Pomocnicza funkcja xor-ująca ciąg bitów
        /// </summary>
        private int BIT_XOR(int x, int n)
        {
            int w = 0;
            for (int i = 0; i < n; i++)
            {
                //xor zmiennej w, oraz odpowiedniego bitu od zmiennej x
                w = w ^ ((x >> i) & 1);
            }
            return w;
        }
    }
}
