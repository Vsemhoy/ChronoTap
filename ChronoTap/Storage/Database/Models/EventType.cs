
using ChronoTap.Storage.Database.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Storage.Database.Models
{
    class EventType
    {
        [PrimaryKey, Unique, NotNull]
        public string Id { get; set; }
        [MaxLength(25), NotNull]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(64)]
        public string CategoryId { get; set; }
        [MaxLength(64)]
        public string GroupId { get; set; }
        [NotNull]
        public int Position { get; set; } = 0;
        public int DurationLimit { get; set; } = 0;
        [MaxLength(64)]
        public string? Icon { get; set; } = null;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsArchieved { get; set; } = false;

        public bool GetLocationStart { get; set; } = false;
        public bool GetLocationEnd { get; set; } = false;
        public bool GetLocationTrack { get; set; } = false;
        public int LocationSensitivity { get; set; } = 0;

        public bool BeepOnDurationLimit { get; set; } = false;
        public bool StopOnDurationLimit { get; set; } = false;
        [MaxLength(64)]
        public string BeepRingtone { get; set; } = string.Empty;
        [MaxLength(25)]
        public string? NextTrack { get; set; } = null;
        public bool RunNextTrackOnDurationLimit { get; set; } = false;
        public bool RunNextTrackOnStopByUser { get; set; } = false;







        /// <summary>
        /// Insert an element and return object if Success
        /// OR return object with Empty ID
        /// </summary>
        /// <returns>EventType</returns>
        public static async Task<EventType> InsertItemAsync(EventType item)
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
        /// <returns>EventType</returns>
        public static async Task<EventType> GetLastinserted()
        {
            return await DatabaseService.DB.Table<EventType>()
                              .OrderByDescending(item => item.CreatedAt)
                              .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Generate unique ID for new row
        /// </summary>
        /// <returns></returns>
        public static async Task<string> MakeId()
        {
            string _id = DatabaseService.GenerateHashId(DatabaseService.RandomString(25));
            EventType evt = await DatabaseService.DB.Table<EventType>().Where(i => i.Id == _id).FirstOrDefaultAsync();
            while (evt != null)
            {
                _id = DatabaseService.GenerateHashId(DatabaseService.RandomString(25));
                evt = await DatabaseService.DB.Table<EventType>().Where(i => i.Id == _id).FirstOrDefaultAsync();
            }
            return _id;
        }


        /// <summary>
        /// Returns only one Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<EventType> GetItemByIdAsync(string id)
        {
            return await DatabaseService.DB.Table<EventType>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Get collection of active items
        /// </summary>
        /// <returns></returns>
        public static async Task<List<EventType>> GetAllActiveItemsAsync()
        {
            return await DatabaseService.DB.Table<EventType>().Where(i => i.IsArchieved == false).OrderBy(x => x.Position).ToListAsync();
        }


        public static async Task<List<EventType>> GetAllItemsAsync()
        {
            return await DatabaseService.DB.Table<EventType>().OrderByDescending(x => x.Position).ToListAsync();
        }


        public static async Task<EventType> GetItemByTitleAsync(string title)
        {
            return await DatabaseService.DB.Table<EventType>().Where(i => i.Title == title).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Get collection of active items
        /// </summary>
        /// <returns></returns>
        public static async Task<List<EventType>> GetAllActiveItemsByCategoryAsync(string category_id)
        {
            return await DatabaseService.DB.Table<EventType>()
                .Where(i => i.IsArchieved == false)
                .Where(y => y.CategoryId == category_id)
                .OrderBy(x => x.Position).ToListAsync();
        }



        /// <summary>
        /// Get count of all rows
        /// </summary>
        /// <param name="onlyActive"></param>
        /// <returns></returns>
        public static async Task<int> CountAll(bool onlyActive = true)
        {
            var call = DatabaseService.DB.Table<EventCategory>();
            if (onlyActive)
            {
                call.Where(i => i.IsArchieved == false);
            }
            return await call.CountAsync();
        }


        /// <summary>
        /// GEt collection of items filtered by category ID
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        public static async Task<List<EventType>> GetAllActiveItemsFromCategoryAsync(string category_id)
        {
            return await DatabaseService.DB.Table<EventType>()
                .Where(i => i.IsArchieved == false).Where(y => y.CategoryId == category_id)
                .OrderBy(x => x.Position).ToListAsync();
        }


        /// <summary>
        /// Update fields inside one item by it's ID (should be defined within object)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static async Task<int> UpdateItemAsync(EventType item)
        {
            return await DatabaseService.DB.UpdateAsync(item);
        }


        /// <summary>
        /// Remove item by it's ID (should be defined within object)
        /// </summary>
        /// <typeparam name="EventType"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Task<int> DeleteItemAsync<EventType>(EventType item)
        {
            return DatabaseService.DB.DeleteAsync(item);
        }


        /// <summary>
        /// Remove collection of items filtered by category ID
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        public static async Task<int> DeleteAllActiveItemsFromCategoryAsync(string category_id)
        {
            return await DatabaseService.DB.Table<EventType>()
                .Where(y => y.CategoryId == category_id).DeleteAsync();

        }




    }
}
