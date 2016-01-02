using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntryPoint
{
    class Assignment1 : IEnumerable
    {
        public Vector2[] listContent;
        public Assignment1() { }
        public Assignment1(Vector2[] vector2Array)
        {
            listContent = vector2Array;
        }
        public IEnumerator GetEnumerator()
        {
            return listContent.GetEnumerator();
        }

        public void MergeSort(Vector2 house)
        {
            MergeSort(house, listContent, listContent.Length);
        }

        private void MergeSort(Vector2 house, Vector2[] array, int length)
        {
            // Check if there's more than 1 element in the array
            if (length < 2)
                return;

            int middle = length / 2; // Will always round towards the smallest possible absolute value

            Vector2[] leftArray = new Vector2[middle];
            Vector2[] rightArray = new Vector2[length - middle]; // Will be the larger array if the array is odd while splitting

            for (int i = 0; i < middle; i++)
            {
                leftArray[i] = array[i];
            }
            for (int i = middle; i < length; i++)
            {
                rightArray[i - middle] = array[i];
            }

            MergeSort(house, leftArray, middle);
            MergeSort(house, rightArray, length - middle);
            Merge(house, array, leftArray, rightArray, middle, length - middle);
        }

        private void Merge(Vector2 house, Vector2[] array, Vector2[] left, Vector2[] right, int leftLength, int rightLength)
        {
            int arrayPosition = 0;
            int leftArrayPosition = 0;
            int rightArrayPosition = 0;

            while (leftArrayPosition < leftLength && rightArrayPosition < rightLength)
            {
                if (Vector2.Distance(left[leftArrayPosition], house) <= Vector2.Distance(right[rightArrayPosition], house))
                    array[arrayPosition++] = left[leftArrayPosition++];
                else
                    array[arrayPosition++] = right[rightArrayPosition++];
            }
            while (leftArrayPosition < leftLength)
                array[arrayPosition++] = left[leftArrayPosition++];
            while (rightArrayPosition < rightLength)
                array[arrayPosition++] = right[rightArrayPosition++];
        }
    }
}
