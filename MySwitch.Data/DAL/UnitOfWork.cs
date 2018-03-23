using MySwitch.Core.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Data.DAL
{
    public class UnitOfWork : IDisposable
    {
        //private GenericDAO<Channel> channelDAO;
        private ISession session = NHibernateHelper.OpenSession();
        private ITransaction _transaction;
        private ChannelDAO channelDao;
        public UnitOfWork()
        {
            //ChannelDao = new ChannelDAO(session);
            _transaction = session.BeginTransaction();
        }

        public ChannelDAO ChannelDao 
        {
            get
            {
                if (this.channelDao == null)
                {
                    this.channelDao = new ChannelDAO();
                }
                return channelDao;
            }
        }  

        //public GenericDAO<Channel> ChannelDao
        //{
        //    get
        //    {
        //        if (this.channelDAO == null)
        //        {
        //            this.channelDAO = new GenericDAO<Channel>(session);
        //        }
        //        return channelDAO;
        //    }
        //}

        public void SaveChanges()
        {
            _transaction.Commit();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    session.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}
