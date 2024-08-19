using ChronoTap.Core;
using ChronoTap.Storage.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoTap.Storage
{
    internal class LocalStorage
    {
        public static List<ChronoEvent> ChronoEventCollection = new List<ChronoEvent>();
        public static List<EventCategory> Categories = new List<EventCategory>();
        public static List<EventType> Types = new List<EventType>();
        //public static List<EentGroup>     

        //public static List<ChronoEvent> ChronoEventCollection = new List<ChronoEvent>();
        public static List<EventCategory> AllCategories = new List<EventCategory>();
        public static List<EventType> AllTypes = new List<EventType>();

        public static EventCategory EditedCategory = null;
        public static EventCategory OpenedCategory = null;
        public static EventCategory ActiveCategory = null;

        public static EventType EditedType = null;
        public static EventType ActiveType = null;

        public static ChronoEvent ActiveChrono = null;
        public static ChronoEvent EditedChrono = null;
        public static ChronoEvent OpenedChrono = null;

        public static string SelectedIcon = null;


        internal async static void Boot()
        {
            Categories = await EventCategory.GetAllActiveItemsAsync();
            Types = await EventType.GetAllActiveItemsAsync();

            LocalStorage.ActiveChrono = await ChronoEvent.GetActiveItem();
            if (LocalStorage.ActiveChrono != null)
            {
                LocalStorage.ActiveType = await EventType.GetItemByIdAsync(LocalStorage.ActiveChrono.TypeId);
                LocalStorage.ActiveCategory = await EventCategory.GetItemByIdAsync(LocalStorage.ActiveChrono.CategoryId);
                //PageManager.MainPage.categoryStack.SetActiveCard();
                //PageManager.MainPage.SetActiveCard();
            }
        }
    }



}

