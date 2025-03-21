namespace MyProject.Utils
{
    [System.Serializable]
    public struct Boundary1D
    {
        public float min;
        public float max;
        public Boundary1D(float low = 0, float high = 0)
        {
            min = low;
            max = high;
        }
    }
}