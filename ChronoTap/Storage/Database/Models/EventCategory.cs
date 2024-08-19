using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Storage.Database.Models
{
    class EventCategory
    {
        [PrimaryKey, Unique, NotNull]
        public string Id { get; set; }
        [MaxLength(25), NotNull]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [NotNull]
        public int Position { get; set; } = 0;
        [MaxLength(64)]
        public string Icon { get; set; } = null;

        public int CountItems { get; set; } = 0;

        public bool IsArchieved { get; set; } = false;

        [MaxLength(9)]
        public string BgColor { get; set; } = "#FFFFFF";
        public string TextColor { get; set; } = "#000000";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;




        /// <summary>
        /// Insert an element and return object if Success
        /// OR return object with Empty ID
        /// </summary>
        /// <returns>EventCategory</returns>
        public static async Task<EventCategory> InsertItemAsync(EventCategory item)
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
        /// <returns>EventCategory</returns>
        public static async Task<EventCategory> GetLastinserted()
        {
            return await DatabaseService.DB.Table<EventCategory>()
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
            EventCategory evt = await DatabaseService.DB.Table<EventCategory>().Where(i => i.Id == _id).FirstOrDefaultAsync();
            while (evt != null)
            {
                _id = DatabaseService.GenerateHashId(DatabaseService.RandomString(25));
                evt = await DatabaseService.DB.Table<EventCategory>().Where(i => i.Id == _id).FirstAsync();
            }
            return _id;
        }


        /// <summary>
        /// Returns only one Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<EventCategory> GetItemByIdAsync(string id)
        {
            return await DatabaseService.DB.Table<EventCategory>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }


        public static async Task<EventCategory> GetItemByTitleAsync(string title)
        {
            return await DatabaseService.DB.Table<EventCategory>().Where(i => i.Title == title).FirstOrDefaultAsync();
        }


        public static async Task<List<EventCategory>> GetAllItemsAsync()
        {
            return await DatabaseService.DB.Table<EventCategory>().OrderByDescending(x => x.Position).ToListAsync();
        }



        public static async Task<List<EventCategory>> GetAllActiveItemsAsync()
        {
            return await DatabaseService.DB.Table<EventCategory>().Where(i => i.IsArchieved == false).OrderBy(x => x.Position).ToListAsync();
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
        /// Update fields inside one item by it's ID (should be defined within object)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static async Task<int> UpdateItemAsync(EventCategory item)
        {
            return await DatabaseService.DB.UpdateAsync(item);
        }






        /// <summary>
        /// Remove item by it's ID (should be defined within object)
        /// </summary>
        /// <typeparam name="EventCategory"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Task<int> DeleteItemAsync<EventCategory>(EventCategory item)
        {
            return DatabaseService.DB.DeleteAsync(item);
        }
    }


}
