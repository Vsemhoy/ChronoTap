using ChronoTap.Core.Utils;

using Microsoft.Extensions.Logging.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Storage.Database.Models
{
    class ChronoEvent
    {
        [PrimaryKey, Unique, NotNull]
        public string Id { get; set; }
        [MaxLength(220)]
        public string? Result { get; set; }
        [MaxLength(2200)]
        public string? Description { get; set; }
        [MaxLength(64)]
        public string TypeId { get; set; }
        [MaxLength(64)]
        public string CategoryId { get; set; }
        [MaxLength(64)]
        public string? GroupId { get; set; }

        public DateTime StartAt { get; set; } = DateTime.UtcNow;
        public DateTime EndAt { get; set; }
        public int Duration { get; set; } = 0;


        public bool IsRunning { get; set; }
        public bool IsFinished { get; set; } = false;
        //public bool IsStarred { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [MaxLength(84)]
        public string? StartLocation { get; set; } = null;
        [MaxLength(84)]
        public string? EndLocation { get; set; } = null;
        public string? LocationTrack { get; set; } = null;






        /// <summary>
        /// Insert an element and return object if Success
        /// OR return object with Empty ID
        /// </summary>
        /// <returns>ChronoEvent</returns>
        public static async Task<ChronoEvent> InsertItemAsync(ChronoEvent item)
        {
            item.Id = await MakeId();
            int result = await DatabaseService.DB.InsertAsync(item);
            if (result > 0)
            {
                return await GetLastinserted();
            }
            item.Id = "";
            return item;
        }

        /// <summary>
        /// Returns last inserted item depends on CreatedAt field
        /// </summary>
        /// <returns>ChronoEvent</returns>
        public static async Task<ChronoEvent> GetLastinserted()
        {
            return await DatabaseService.DB.Table<ChronoEvent>()
                              .OrderByDescending(item => item.CreatedAt)
                              .FirstOrDefaultAsync();
        }



        public static async Task<ChronoEvent> GetActiveItem()
        {
            return await DatabaseService.DB.Table<ChronoEvent>()
                              .Where(i => i.IsRunning == true)
                              .FirstOrDefaultAsync();
        }


        /// <summary>
        /// Generate unique ID for new row
        /// </summary>
        /// <returns></returns>
        public static async Task<string> MakeId()
        {
            string _id = DatabaseService.GenerateHashId(DatabaseService.RandomString(25));
            ChronoEvent evt = await DatabaseService.DB.Table<ChronoEvent>().Where(i => i.Id == _id).FirstOrDefaultAsync();
            while (evt != null)
            {
                _id = DatabaseService.GenerateHashId(DatabaseService.RandomString(25));
                evt = await DatabaseService.DB.Table<ChronoEvent>().Where(i => i.Id == _id).FirstOrDefaultAsync();
            }
            return _id;
        }

        /// <summary>
        /// Returns only one Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<ChronoEvent> GetItemByIdAsync(string id)
        {
            return await DatabaseService.DB.Table<ChronoEvent>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Get collection of active items
        /// </summary>
        /// <returns></returns>
        public static async Task<List<ChronoEvent>> GetAllActiveItemsAsync()
        {
            return await DatabaseService.DB.Table<ChronoEvent>().OrderByDescending(x => x.StartAt).ToListAsync();
        }

        /// <summary>
        /// GEt collection of items filtered by category ID
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        public static async Task<List<ChronoEvent>> GetAllItemsFromCategoryAsync(string category_id)
        {
            return await DatabaseService.DB.Table<ChronoEvent>()
                .Where(y => y.CategoryId == category_id)
                .OrderBy(x => x.StartAt).ToListAsync();
        }


        /// <summary>
        /// Update fields inside one item by it's ID (should be defined within object)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static async Task<int> UpdateItemAsync(ChronoEvent item)
        {
            return await DatabaseService.DB.UpdateAsync(item);
        }

        /// <summary>
        /// Remove item by it's ID (should be defined within object)
        /// </summary>
        /// <typeparam name="ChronoEvent"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Task<int> DeleteItemAsync<ChronoEvent>(ChronoEvent item)
        {
            return DatabaseService.DB.DeleteAsync(item);
        }


        public static async Task<int> StopAllTasks()
        {
            var time = DateTime.UtcNow;
            var items = await DatabaseService.DB.Table<ChronoEvent>().Where(i => i.IsRunning == true).ToListAsync();
            int result = -1;
            for (int i = 0; i < items.Count; i++)
            {
                var itoupdate = items[i];
                itoupdate.IsRunning = false;
                itoupdate.EndAt = time;
                var sta = itoupdate.StartAt;
                TimeSpan difference = time - itoupdate.StartAt;
                double totalMinutes = difference.TotalMinutes;
                if (totalMinutes < int.MinValue || totalMinutes > int.MaxValue)
                {
                    itoupdate.Duration = int.MaxValue;
                }
                else
                {
                    itoupdate.Duration = (int)totalMinutes;
                }

                if (itoupdate.EndLocation != null)
                {
                    itoupdate.EndLocation = await LocationService.GetCachedLocation();
                }

                //for (int ix = 0; ix < LocalStorage.Types.Count; ix++)
                //{
                //    if (LocalStorage.Types[ix].Id == itoupdate.TypeId)
                //    {
                //        if (LocalStorage.Types[ix].GetLocationEnd)
                //        {

                //        }
                //    }
                //}

                itoupdate.IsFinished = true;
                result = await ChronoEvent.UpdateItemAsync(itoupdate);
            }
            LocalStorage.ActiveChrono = null;
            return result;
        }


        public static async Task<ChronoEvent> StartTask()
        {
            if (LocalStorage.ActiveCategory != null && LocalStorage.ActiveType != null)
            {
                ChronoEvent newTask = new ChronoEvent();
                newTask.CategoryId = LocalStorage.ActiveCategory.Id;
                newTask.TypeId = LocalStorage.ActiveType.Id;
                if (LocalStorage.ActiveType.GetLocationStart)
                {
                    newTask.StartLocation = await LocationService.GetCachedLocation();
                }
                if (LocalStorage.ActiveType.GetLocationEnd)
                {
                    newTask.EndLocation = await LocationService.GetCachedLocation();
                }
                newTask.StartAt = DateTime.UtcNow;
                newTask.IsRunning = true;

                var result = await ChronoEvent.InsertItemAsync(newTask);
                LocalStorage.ActiveChrono = result;
                return result;
            }
            return new ChronoEvent();
        }


        /// <summary>
        /// Remove collection of items filtered by category ID
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        public static async Task<int> DeleteAllActiveItemsFromCategoryAsync(string category_id)
        {
            return await DatabaseService.DB.Table<ChronoEvent>()
                .Where(y => y.CategoryId == category_id).DeleteAsync();

        }


        public static async Task<int> DeleteAllActiveItemsFromTypeAsync(string type_id)
        {
            return await DatabaseService.DB.Table<ChronoEvent>()
                .Where(y => y.TypeId == type_id).DeleteAsync();
        }
    }
}
