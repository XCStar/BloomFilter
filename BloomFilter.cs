using System.Text;
using System;
using System.Collections;
using Murmur;
namespace BloomFilter.Console
{
    public class BloomFilter
    {
        /// <summary>
        /// 数据量大小
        /// </summary>
        private readonly long insertSize;
        /// <summary>
        /// 误判率
        /// </summary>
        private readonly float rate;
      
        /// <summary>
        /// hash函数个数
        /// </summary>
        private int hashFunctionNumber;
        /// <summary>
        /// 字节大小
        /// </summary>
        private int arraySize;
        /// <summary>
        /// 存储接口
        /// </summary>
        private IBloomStore _store;
        public  BloomFilter(int number,float rate,IBloomStore store)
        {
            this.insertSize=number;
            this.rate=rate;
            this.arraySize=this.getBitArraySize(insertSize,rate);
            this.hashFunctionNumber=this.getHashFunctionsNumber(insertSize,arraySize);
            System.Console.WriteLine($"arraysize:{arraySize} hashfunctioncount:{this.hashFunctionNumber}");
            this._store=store;
            var size=(uint)this.arraySize;
            store.Init(size);

        }
        public BloomFilter(IBloomStore store):this(100,0.01f,store)
        {
            
        }
        private int getHashFunctionsNumber(long size,long number)
        {
            return Math.Max(1,(int)Math.Round((float)number/size*Math.Log(2)));
        }
        private int getBitArraySize(long number,float rate)
        {
            if(rate==0)
            {
                throw new Exception("not 0");
            }
            var num=(int)(-number*Math.Log(rate)/(Math.Log(2)*Math.Log(2)));
            return num;
        }
        public void Set(string str)
        {
            var hash1=GetHashCode(str);
            var hash2=hash1>>16;
            for (int i = 1; i < hashFunctionNumber; i++)
            {
                long combinedHash=hash1+i*hash2;
                if(combinedHash<0)
                {
                    combinedHash=~combinedHash;
                }
                var index=(uint)(combinedHash%arraySize);
                _store.SetState(index,true);
            }
            
        }
        private uint GetHashCode(string str)
        {
            var hash32= MurmurHash.Create32();
            var bytes= hash32.ComputeHash(Encoding.UTF32.GetBytes(str));
            return BitConverter.ToUInt32(bytes);

        }
        public bool isExits(string str)
        {
            var flag=true;
            var hash1=GetHashCode(str);
            var hash2=hash1>>16;
            for (int i = 0; i < hashFunctionNumber; i++)
            {
                var combinedHash=hash1+i*hash2;
                if(combinedHash<0)
                {
                    combinedHash=~combinedHash;
                }
                var index=(uint)(combinedHash%arraySize);
                flag &=_store.GetState(index);
                if(!flag)
                {
                    return flag;
                }
            }
            return flag;
        }
        public bool GetState(uint index)
        {
            return _store.GetState(index);
        }

    }
}