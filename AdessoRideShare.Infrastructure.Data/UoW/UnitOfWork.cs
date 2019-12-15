using AdessoRideShare.Domain.Interfaces;
using AdessoRideShare.Infrastructure.Data.Context;

namespace AdessoRideShare.Infrastructure.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdessoRideShareContext _context;

        public UnitOfWork(AdessoRideShareContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
