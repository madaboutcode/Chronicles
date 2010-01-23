using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Event;

namespace Chronicles.Framework.Caching
{
    public class NHibernateEventListener : IPostInsertEventListener, IPostUpdateEventListener
    {
        #region Implementation of IPostInsertEventListener

        public void OnPostInsert(PostInsertEvent insertEvent)
        {
            OutputCacheManager.EntityChanged(insertEvent.Entity);
        }

        #endregion

        #region Implementation of IPostUpdateEventListener

        public void OnPostUpdate(PostUpdateEvent updateEvent)
        {
            OutputCacheManager.EntityChanged(updateEvent.Entity);
        }

        #endregion
    }
}
