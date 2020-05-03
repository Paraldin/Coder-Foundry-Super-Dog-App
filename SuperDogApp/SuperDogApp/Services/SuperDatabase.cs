using SQLite;
using SuperDogApp.Extensions;
using SuperDogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDogApp
{
     public class SuperDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
            {
                return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public SuperDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ComicCon).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ComicCon)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<ComicCon>> GetConsAsync()
        {
            return Database.Table<ComicCon>().ToListAsync();
        }

        public Task<ComicCon> GetItemAsync(int id)
        {
            return Database.Table<ComicCon>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ComicCon item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ComicCon item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
