using System;
using CSRedis;
namespace BloomFilter.Console
{
    public class RedisBloomStore : IBloomStore
    {
        private  readonly string key;
        public RedisBloomStore()
        {
            var client=new CSRedisClient("redis");
            RedisHelper.Initialization(client);
            key=Guid.NewGuid().ToString("N");
        }
        public bool GetState(uint index)
        {
            return RedisHelper.GetBit(key,index);
        }

        public bool Init(uint size)
        {
           return RedisHelper.SetBit(key,size,false);
        }

        public bool SetState(uint index, bool state)
        {
           return RedisHelper.SetBit(key,index,state);
        }
    }
}