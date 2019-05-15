using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UserProject.Models;

namespace UserProject.DAL
{
    public class UnitOfWork:IDisposable
    {
        #region Variables

        UserProjectEntites context;
        GenericRepository<User> userUnitRepository = null;
        GenericRepository<Project> projectRepository = null;
        GenericRepository<Project_UserMapping> project_userRepository = null;

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

        public GenericRepository<Project> ProjectRepository
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new GenericRepository<Project>(context);
                return projectRepository;
            }
        }

        public GenericRepository<Project_UserMapping> Project_UserRepository
        {
            get
            {
                if (project_userRepository == null)
                    project_userRepository = new GenericRepository<Project_UserMapping>(context);
                return project_userRepository;
            }
        }


        #endregion

        #region Constructor

        public UnitOfWork()
        {
            context = new UserProjectEntites();
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