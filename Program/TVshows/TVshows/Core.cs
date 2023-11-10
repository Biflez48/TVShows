using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVshows
{
    class Core
    {
        public static int VOID = -255;
        public static int NONE = -1;
        private static Database.TVshowsEntities _Database;
        public static Database.TVshowsEntities Database
        {
            get
            {
                if (_Database == null)
                {
                    _Database = new Database.TVshowsEntities();
                }
                return _Database;
            }
        }
        public static MainWindow AppMainWindow;
        public static Database.Users CurrentUser;
        public static void CancelAllChanges()
        {
            var ChangedEntries = Database.ChangeTracker.Entries()
                .Where(E => E.State != System.Data.Entity.EntityState.Unchanged)
                .ToList();
            foreach(DbEntityEntry E in ChangedEntries)
            {
                CancelChanges(E);
            }
        }
        public static void CancelChanges(object Entity)
        {
            DbEntityEntry Entry = Database.Entry(Entity);
            try
            {
                switch (Entry.State)
                {
                    case EntityState.Added:
                        Entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                        Entry.CurrentValues.SetValues(Entry.OriginalValues);
                        Entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        Entry.State = EntityState.Unchanged;
                        break;
                }
            }
            catch
            {
                Entry.Reload();
            }
        }
    }
}
