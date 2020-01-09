using System.Collections;
namespace BloomFilter.Console
{
    public class BitArrayBloomStore : IBloomStore
    {
        private BitArray bitArray;

        public bool GetState(uint index)
        {
           return bitArray[(int)index];
        }

        public bool Init(uint size)
        {
            var current=(int)size;
            bitArray=new BitArray(current,false);
            return true;
        }

        public bool SetState(uint index, bool state)
        {
            bitArray.Set((int)index,state);
            return true;
        }
    }
}