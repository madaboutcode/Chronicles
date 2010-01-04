using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronicles.DataAccess.Facade;
using Chronicles.Entities;
using Chronicles.Framework;
using NHibernate;
using Chronicles.DataAccess.Context;

namespace Chronicles.DataAccess
{
    public class CommentRepository:RepositoryBase, ICommentRepository
    {
        private AppConfiguration config;
        public CommentRepository(AppConfiguration config,DbContext ctx) : base(ctx)
        {
            this.config = config;
        }

        public Comment AddComment(Comment comment)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(comment);
                transaction.Commit();
            }
            return comment;
        }

        public Comment UpdateComment(Comment comment)
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                Session.Update(comment);
                transaction.Commit();
            }
            return comment;
        }
    }
}
