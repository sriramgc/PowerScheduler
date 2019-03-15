using System.Threading.Tasks;

namespace PowerScheduler.Service
{
    public interface IIntraDayReport
    {
        Task Generate();
    }
}