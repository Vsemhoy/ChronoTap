using ChronoTap.Pages.Elements.Modals;
using ChronoTap.Storage;
using ChronoTap.Storage.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChronoTap.Core
{

    internal class ModalManager
    {
        public static CategoryEditorModal categoryEditorModal = new CategoryEditorModal();
        public static TypeEditorModal typeEditorModal = new TypeEditorModal();
        public static EventEditorModal eventEditorModal = new EventEditorModal();



        // CATEGORY 
        public static async void CategoryEditorModal_ActionCreate(object? sender, EventArgs e)
        {
            // Check for Duplicates
            var duplex = await EventCategory.GetItemByTitleAsync(categoryEditorModal.titleText);
            if (duplex != null)
            {
                categoryEditorModal.Alert("Error", "Category with name '" + categoryEditorModal.titleText + "' already exists...");
                return;
            }
            var newItem = new EventCategory();
            newItem.Title = categoryEditorModal.titleText;
            newItem.Description = categoryEditorModal.descriptionText;
            newItem.BgColor = categoryEditorModal.bgColor.ToHex();
            newItem.TextColor = categoryEditorModal.txColor.ToHex();
            newItem.Position = LocalStorage.Categories.Count;
            newItem.Icon = LocalStorage.SelectedIcon;

            var result = await EventCategory.InsertItemAsync(newItem);
            LocalStorage.SelectedIcon = null;
            if (result.Id != string.Empty)
            {
                // success
                LocalStorage.Categories.Add(result);
                PageManager.MainPage.categoryStack.SetCategoryCards();
                categoryEditorModal.Hide();
                return;
            }
            else
            {
                categoryEditorModal.Alert("Error", "Can't save, something wrong :(");
                return;
            }
        }


        public static async Task CategoryEditorModal_ActionSave(object? sender, EventArgs e)
        {
            var savedItem = LocalStorage.EditedCategory;
            savedItem.Title = categoryEditorModal.titleText;
            savedItem.Description = categoryEditorModal.descriptionText;
            savedItem.BgColor = categoryEditorModal.bgColor.ToHex();
            savedItem.TextColor = categoryEditorModal.txColor.ToHex();
            savedItem.Icon = LocalStorage.SelectedIcon;

            var result = await EventCategory.UpdateItemAsync(savedItem);
            LocalStorage.SelectedIcon = null;
            if (result > 0)
            {
                LocalStorage.Categories = await EventCategory.GetAllActiveItemsAsync();
                LocalStorage.EditedCategory = null;
                categoryEditorModal.Hide();
                return;

            }
            else
            {
                categoryEditorModal.Alert("Error", "Can't save, something wrong :(");
                return;
            }
        }


        public static async Task CategoryEditorModal_ActionDelete(object? sender, EventArgs e)
        {
            var removedItem = LocalStorage.EditedCategory;
            var id = removedItem.Id;

            var result = await EventCategory.DeleteItemAsync(removedItem);
            if (result > 0)
            {
                await ChronoEvent.DeleteAllActiveItemsFromCategoryAsync(id);
                await EventType.DeleteAllActiveItemsFromCategoryAsync(id);


                LocalStorage.Categories = await EventCategory.GetAllActiveItemsAsync();
                LocalStorage.EditedCategory = null;
                categoryEditorModal.Hide();
                return;

            }
            else
            {
                categoryEditorModal.Alert("Error", "Can't remove this, something wrong :(");
                return;
            }
        }






        // TYPE TYPE TYPE
        internal async static Task TypeEditorModal_ActionCreate(object? sender, EventArgs e)
        {
            // Check for Duplicates
            var duplex = await EventType.GetItemByTitleAsync(typeEditorModal.titleText);
            if (duplex != null)
            {
                categoryEditorModal.Alert("Error", "Category with name '" + typeEditorModal.titleText + "' already exists...");
                return;
            }
            var savedItem = LocalStorage.EditedType;
            savedItem.Title = ModalManager.typeEditorModal.titleText;

            savedItem.Description = ModalManager.typeEditorModal.descriptionText;
            savedItem.DurationLimit = ModalManager.typeEditorModal.val_duration_limit;
            savedItem.GetLocationEnd = ModalManager.typeEditorModal.val_getLocationEnd;
            savedItem.GetLocationStart = ModalManager.typeEditorModal.val_getLocationStart;
            savedItem.GetLocationTrack = ModalManager.typeEditorModal.val_getLocationTrack;
            savedItem.BeepOnDurationLimit = ModalManager.typeEditorModal.val_beepOnDurationLimit;
            savedItem.StopOnDurationLimit = ModalManager.typeEditorModal.val_stopOnDurationLimit;
            savedItem.RunNextTrackOnDurationLimit = ModalManager.typeEditorModal.val_runNextTrackOnLimit;
            savedItem.RunNextTrackOnStopByUser = ModalManager.typeEditorModal.val_runNextTrackOnStop;

            savedItem.CategoryId = ModalManager.typeEditorModal.val_select_category;
            savedItem.LocationSensitivity = ModalManager.typeEditorModal.val_select_sensitivity;
            savedItem.NextTrack = ModalManager.typeEditorModal.val_next_track;
            savedItem.Icon = LocalStorage.SelectedIcon;
            LocalStorage.SelectedIcon = null;
            var result = await EventType.InsertItemAsync(savedItem);
            LocalStorage.SelectedIcon = null;

            if (result.Id != string.Empty)
            {
                // success
                LocalStorage.Types.Add(result);
                // NEED UPDATE TYPE LIST INSIDE CATEGORY
                typeEditorModal.Hide();
                return;
            }
            else
            {
                typeEditorModal.Alert("Error", "Can't save, something wrong :(");
                return;
            }
        }


        internal async static Task TypeEditorModal_ActionSave(object? sender, EventArgs e)
        {
            var savedItem = LocalStorage.EditedType;
            savedItem.Title = ModalManager.typeEditorModal.titleText;

            savedItem.Description = ModalManager.typeEditorModal.descriptionText;
            savedItem.DurationLimit = ModalManager.typeEditorModal.val_duration_limit;
            savedItem.GetLocationEnd = ModalManager.typeEditorModal.val_getLocationEnd;
            savedItem.GetLocationStart = ModalManager.typeEditorModal.val_getLocationStart;
            savedItem.GetLocationTrack = ModalManager.typeEditorModal.val_getLocationTrack;
            savedItem.BeepOnDurationLimit = ModalManager.typeEditorModal.val_beepOnDurationLimit;
            savedItem.StopOnDurationLimit = ModalManager.typeEditorModal.val_stopOnDurationLimit;
            savedItem.RunNextTrackOnDurationLimit = ModalManager.typeEditorModal.val_runNextTrackOnLimit;
            savedItem.RunNextTrackOnStopByUser = ModalManager.typeEditorModal.val_runNextTrackOnStop;

            savedItem.CategoryId = ModalManager.typeEditorModal.val_select_category;
            savedItem.LocationSensitivity = ModalManager.typeEditorModal.val_select_sensitivity;
            savedItem.NextTrack = ModalManager.typeEditorModal.val_next_track;
            savedItem.Icon = LocalStorage.SelectedIcon;

            var result = await EventType.UpdateItemAsync(savedItem);
            LocalStorage.SelectedIcon = null;
            if (result > 0)
            {
                // success
                LocalStorage.Types = await EventType.GetAllActiveItemsAsync();
                // NEED UPDATE TYPE LIST INSIDE CATEGORY
                LocalStorage.EditedType = null;
                typeEditorModal.Hide();
                return;
            }
            else
            {
                typeEditorModal.Alert("Error", "Can't save, something wrong :(");
                return;
            }
        }


        public static async Task TypeEditorModal_ActionDelete(object? sender, EventArgs e)
        {
            var removedItem = LocalStorage.EditedType;
            var id = removedItem.Id;

            var result = await EventType.DeleteItemAsync(removedItem);
            if (result > 0)
            {
                await ChronoEvent.DeleteAllActiveItemsFromTypeAsync(id);
                //await EventType.DeleteAllActiveItemsFromCategoryAsync(id);
                LocalStorage.Types = await EventType.GetAllActiveItemsAsync();
                LocalStorage.EditedType = null;
                typeEditorModal.Hide();
                return;
            }
            else
            {
                typeEditorModal.Alert("Error", "Can't remove this, something wrong :(");
                return;
            }
        }
    }
}
