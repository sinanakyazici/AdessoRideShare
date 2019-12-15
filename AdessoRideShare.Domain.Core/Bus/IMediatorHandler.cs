using System.Threading.Tasks;
using AdessoRideShare.Domain.Core.Commands;
using AdessoRideShare.Domain.Core.Events;


namespace AdessoRideShare.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
