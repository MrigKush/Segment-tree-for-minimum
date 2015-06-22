using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rangequery
{
    class Program
    {
        static int[] st,ty;
        static void Main(string[] args)
        {
            ty = new int[] { 2, 5, 1, 4, 9, 3 };
            Program ui = new Program();
            ui.constructST(ty, 6);
            Console.WriteLine(ui.RMQ(6, 1, 3));

            ui.updateValue(6, 3, 90);
            ui.updateValue(6,4,1);
            Console.WriteLine(ui.RMQ(6, 1, 4));
            Console.ReadLine();
        }

        void constructST(int[] arr, int n)
        {
            int x = (int)(Math.Ceiling(Math.Log(n)));
            int max_size = (2 * n)+1;
            st = new int[max_size];
            constructSTUtil(arr, 0, n-1, st, 0);
        }

        int constructSTUtil(int []arr, int ss, int se, int []st, int si)
        {
            if (ss == se)
            {
                st[si] = arr[ss];
                return arr[ss];
            }
            int mid = (se + ss) / 2;
            st[si] =  minVal(constructSTUtil(arr, ss, mid, st, si*2+1),constructSTUtil(arr, mid+1, se, st, si*2+2));
            return st[si];
        }

        int RMQ(int n, int qs, int qe)
        {
            qs = qs - 1;
            qe = qe - 1;
            // Check for erroneous input values
            if (qs < 0 || qe > n - 1 || qs > qe)
            {
                return -1;
            }
            return RMQUtil( 0, n - 1, qs, qe, 0);
        }
        
        private int minVal(int p, int p_2)
         {
             return (p > p_2) ? p_2 : p;
         }

        int RMQUtil( int ss, int se, int qs, int qe, int index)
        {
            // If segment of this node is a part of given range, then return the
            // min of the segment
            if (qs <= ss && qe >= se)
                return st[index];

            // If segment of this node is outside the given range
            if (se < qs || ss > qe)
                return int.MaxValue;

            // If a part of this segment overlaps with the given range
            int mid = (ss+ se)/2;
            return minVal(RMQUtil(ss, mid, qs, qe, 2 * index + 1),RMQUtil(mid + 1, se, qs, qe, 2 * index + 2));
        }

        void updateValue( int n, int i, int new_val)
        {
            if (i < 0 || i > n-1)
            {
                return;
            }
            
            ty[i] = new_val;
            // Update the values of nodes in segment tree
            updateValueUtil(0, n - 1, i-1,0 ,new_val);
        }

        int updateValueUtil(int ss, int se, int i,int index ,int diff)
        {
            if (i == ss && i == se)
            {
                st[index] = diff;
                return st[index];
            }

            if (se < i || ss > i)
                return st[i+1];
            int mid = (ss + se) / 2;
            st[index] = minVal(updateValueUtil(ss, mid, i, index * 2 + 1, diff), updateValueUtil(mid + 1, se, i, index * 2 + 2, diff));
            return st[index];
        }
    }
}
