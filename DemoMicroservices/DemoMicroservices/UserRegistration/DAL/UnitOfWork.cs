using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UserRegistration.Models;

namespace UserRegistration.DAL
{
    public class UnitOfWork:IDisposable
    {
        #region Variables

        UserRegisterEntites context;
        GenericRepository<User> userUnitRepository = null;
        
        #endregion

        #region Public Properties
        
        public GenericRepository<User> UserUnitRepository
        {
            get
            {
                if (userUnitRepository == null)
                    userUnitRepository = new GenericRepository<User>(context);
                return userUnitRepository;
            }
        }

      
        #endregion

        #region Constructor

        public UnitOfWork()
        {
            context = new UserRegisterEntites();
        }

        #endregion

        #region Methods

        public async Task<int> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
        #endregion

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}