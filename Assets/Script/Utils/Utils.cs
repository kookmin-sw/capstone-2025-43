using System;

namespace MyProject.Utils
{
    [System.Serializable]
    public struct Boundary1D<T> where T : IComparable<T>
    {
        public T min;
        public T max;
        public Boundary1D(T low, T high)
        {
            min = low;
            max = high;
        }
    }

}