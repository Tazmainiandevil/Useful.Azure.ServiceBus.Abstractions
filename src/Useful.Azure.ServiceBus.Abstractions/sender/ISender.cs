using System.Collections.Generic;
using System.Threading.Tasks;

namespace Useful.Azure.ServiceBus.Abstractions.sender
{
    public interface ISender<T>
    {
        /// <summary>
        /// Send message of type T as a json string
        /// </summary>
        /// <param name="data">The data to send</param>
        Task SendAsJsonAsync(T data);

        /// <summary>
        /// Send a list of messages of type T as json strings
        /// </summary>
        /// <param name="dataList">The data to send</param>
        Task SendAsJsonAsync(IList<T> dataList);
    }
}