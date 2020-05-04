using Newtonsoft.Json;
using SQLite;
using SuperDogApp.Extensions;
using SuperDogApp.Models;
using SuperDogApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                    await SeedData();
                    initialized = true;
                }
            }
        }

        public async Task SeedData()
        {
            var anyData = await Database.Table<ComicCon>().FirstOrDefaultAsync();
            if (anyData == null)
            {
                string jsonFileName = "seed.JSON";
                List<ComicCon> seedList = new List<ComicCon>();
                var assembly = typeof(MainPage).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
                using (var reader = new StreamReader(stream))
                {
                    var jsonString = reader.ReadToEnd();

                    seedList = JsonConvert.DeserializeObject<List<ComicCon>>(jsonString);
                }

                await Database.InsertAllAsync(seedList);
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
