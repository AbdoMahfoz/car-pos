using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ISearch
    {
        Task UpdateIndex();
        IEnumerable<int> Query(string query);
    }
}