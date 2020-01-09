namespace BloomFilter.Console
{
    public interface  IBloomStore
    {
        /// <summary>
        /// 设置容器大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        bool Init(uint size);
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        bool SetState(uint index,bool state);
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool GetState(uint index);
    }
}