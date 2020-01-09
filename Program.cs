using System.Threading;
using System.Threading.Tasks;
using System;
using Microsoft.Data.SqlClient;
using Dapper;
namespace BloomFilter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
         
            var store=new BitArrayBloomStore();
            var count=0;
            BloomFilter filter=new BloomFilter(5100000,0.0005f,store);
            Parallel.For(1,2000000,item=>{
                filter.Set(item.ToString());
            });
            Parallel.For(2000000,3000000,item=>{
                if(filter.isExits(item.ToString()))
                {
                    Interlocked.Increment(ref count);
                }
            });
            System.Console.WriteLine($"this repeat count:{count}");
            

            
        }
    }
}
