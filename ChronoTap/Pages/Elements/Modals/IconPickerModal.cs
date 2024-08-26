
using ChronoTap.Storage;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ChronoTap.Pages.Elements.Modals
{
    internal class IconPickerModal : ContentPage
    {
        public int columns { get; set; }

        ScrollView scrollBody = new ScrollView();
        VerticalStackLayout contentStack = new VerticalStackLayout();


        public IconPickerModal()
        {
            Title = "Icon Picker";
            this.columns = 4;
            int counter = 0;



            var iconList = LoadIcons();

            while (counter < iconList.Count)
            {
                var gridRow = new Grid
                {

                    RowSpacing = 3,
                    ColumnSpacing = 4,
                    ColumnDefinitions =
                    {
                        new ColumnDefinition{ Width = GridLength.Star },
                        new ColumnDefinition{ Width = GridLength.Star },
                        new ColumnDefinition{ Width = GridLength.Star },
                        new ColumnDefinition{ Width = GridLength.Star }
                    },
                    RowDefinitions =
                    {
                        new RowDefinition{ Height = GridLength.Auto }
                    }
                };


                for (int i = 0; i < this.columns; i++)
                {
                    if (counter == iconList.Count) { break; }
                    VerticalStackLayout colStack = new VerticalStackLayout();
                    TapGestureRecognizer tapper = new TapGestureRecognizer();
                    colStack.GestureRecognizers.Add(tapper);
                    var fileName = iconList[counter].FileName;
                    tapper.Tapped += (s, e) => { this.Click_on_IconTab(fileName); };

                    colStack.BackgroundColor = Colors.Aqua;
                    colStack.Padding = 16;
                    colStack.BackgroundColor = Colors.White;
                    colStack.Margin = 2;

                    Image image = new Image();
                    image.Source = "ico/" + iconList[counter].FileName;
                    image.HeightRequest = 42;
                    image.WidthRequest = 42;
                    image.BackgroundColor = Colors.White;

                    //Aspect = Aspect.AspectFit,
                    colStack.Children.Add(image);

                    Label label = new Label();
                    label.Text = iconList[counter].Title;
                    label.FontSize = 13;
                    label.Padding = 3;
                    label.HorizontalTextAlignment = TextAlignment.Center;
                    colStack.Add(label);

                    gridRow.Children.Add(colStack);
                    Grid.SetColumn(colStack, i);



                    counter++;
                    //colStack.SetDynamicResource(iconList[counter].FileName, "iconName");

                }

                this.contentStack.Children.Add(gridRow);
            }


            this.scrollBody.Content = contentStack;
            this.Content = scrollBody;


        }

        private async void Click_on_IconTab(string itemName)
        {
            //await DisplayAlert("Alert", itemName, "OK");
            LocalStorage.SelectedIcon = itemName;
            await Navigation.PopAsync();
        }

        private List<CronoIcon> LoadIcons()
        {
            var icons = new List<CronoIcon>([


                new CronoIcon("Adjustable wrench", "adjustable_wrench.svg", "adjustable wrench",""),
                new CronoIcon("Air ball", "air_ball.svg", "air ball",""),
                new CronoIcon("Arrows rolled", "arrows_rolled.svg", "arrows rolled",""),
                new CronoIcon("Arrow camera", "arrow_camera.svg", "arrow camera",""),
                new CronoIcon("Arrow crossed", "arrow_crossed.svg", "arrow crossed",""),
                new CronoIcon("Arrow infinity", "arrow_infinity.svg", "arrow infinity",""),
                new CronoIcon("Arrow rolled", "arrow_rolled.svg", "arrow rolled",""),
                new CronoIcon("Arrow spring", "arrow_spring.svg", "arrow spring",""),
                new CronoIcon("Atom links", "atom_links.svg", "atom links",""),
                new CronoIcon("Baby todler", "baby_todler.svg", "baby todler",""),
                new CronoIcon("Bag case", "bag_case.svg", "bag case",""),
                new CronoIcon("Bag shopping", "bag_shopping.svg", "bag shopping",""),
                new CronoIcon("Bag travel", "bag_travel.svg", "bag travel",""),
                new CronoIcon("Beer mug", "beer_mug.svg", "beer mug",""),
                new CronoIcon("Bended sheet", "bended_sheet.svg", "bended sheet",""),
                new CronoIcon("Billiard ball", "billiard_ball.svg", "billiard ball",""),
                new CronoIcon("Bird pigeon", "bird_pigeon.svg", "bird pigeon",""),
                new CronoIcon("Bone", "bone.svg", "bone",""),
                new CronoIcon("Bookmark down", "bookmark_down.svg", "bookmark down",""),
                new CronoIcon("Book papered", "book_papered.svg", "book papered",""),
                new CronoIcon("Boo mask", "boo_mask.svg", "boo mask",""),
                new CronoIcon("Bow tie", "bow_tie.svg", "bow tie",""),
                new CronoIcon("Bow with arrows", "bow_with_arrows.svg", "bow with arrows",""),
                new CronoIcon("Bulb lamp", "bulb_lamp.svg", "bulb lamp",""),
                new CronoIcon("Bus stop", "bus_stop.svg", "bus stop",""),
                new CronoIcon("Butterfly simple", "butterfly_simple.svg", "butterfly simple",""),
                new CronoIcon("Calendar list", "calendar_list.svg", "calendar list",""),
                new CronoIcon("Camera arrow", "camera_arrow.svg", "camera arrow",""),
                new CronoIcon("Card arrow down", "card_arrow_down.svg", "card arrow down",""),
                new CronoIcon("Car jeep", "car_jeep.svg", "car jeep",""),
                new CronoIcon("Chart bars", "chart_bars.svg", "chart bars",""),
                new CronoIcon("Chart graphic", "chart_graphic.svg", "chart graphic",""),
                new CronoIcon("Chat empty", "chat_empty.svg", "chat empty",""),
                new CronoIcon("Chat filled", "chat_filled.svg", "chat filled",""),
                new CronoIcon("Chat zzz", "chat_zzz.svg", "chat zzz",""),
                new CronoIcon("Checkbox checked", "checkbox_checked.svg", "checkbox checked",""),
                new CronoIcon("Chemical flask", "chemical_flask.svg", "chemical flask",""),
                new CronoIcon("Chemical tube", "chemical_tube.svg", "chemical tube",""),
                new CronoIcon("Chicken leg", "chicken_leg.svg", "chicken leg",""),
                new CronoIcon("City building", "city_building.svg", "city building",""),
                new CronoIcon("Clock round", "clock_round.svg", "clock round",""),
                new CronoIcon("Clothes hanger", "clothes_hanger.svg", "clothes hanger",""),
                new CronoIcon("Cloud in arrow", "cloud_in_arrow.svg", "cloud in arrow",""),
                new CronoIcon("Cloud thunder", "cloud_thunder.svg", "cloud thunder",""),
                new CronoIcon("Coffee cup", "coffee_cup.svg", "coffee cup",""),
                new CronoIcon("Coffer arrowin", "coffer_arrowin.svg", "coffer arrowin",""),
                new CronoIcon("Coffer docs", "coffer_docs.svg", "coffer docs",""),
                new CronoIcon("Cog double", "cog_double.svg", "cog double",""),
                new CronoIcon("Cog single", "cog_single.svg", "cog single",""),
                new CronoIcon("Console joystick", "console_joystick.svg", "console joystick",""),
                new CronoIcon("Cup hot", "cup_hot.svg", "cup hot",""),
                new CronoIcon("Dice", "dice.svg", "dice",""),
                new CronoIcon("Dog house", "dog_house.svg", "dog house",""),
                new CronoIcon("Dress sock", "dress_sock.svg", "dress sock",""),
                new CronoIcon("Dress tshirt", "dress_tshirt.svg", "dress tshirt",""),
                new CronoIcon("Dumbell", "dumbell.svg", "dumbell",""),
                new CronoIcon("Eye opened", "eye_opened.svg", "eye opened",""),
                new CronoIcon("Faders", "faders.svg", "faders",""),
                new CronoIcon("Farm tractor", "farm_tractor.svg", "farm tractor",""),
                new CronoIcon("File cabinet", "file_cabinet.svg", "file cabinet",""),
                new CronoIcon("Film babine", "film_babine.svg", "film babine",""),
                new CronoIcon("Film cassete", "film_cassete.svg", "film cassete",""),
                new CronoIcon("Film doublecut", "film_doublecut.svg", "film doublecut",""),
                new CronoIcon("Film triple cut", "film_triple_cut.svg", "film triple cut",""),
                new CronoIcon("Film tub", "film_tub.svg", "film tub",""),
                new CronoIcon("Fire extinguisher", "fire_extinguisher.svg", "fire extinguisher",""),
                new CronoIcon("Flag down", "flag_down.svg", "flag down",""),
                new CronoIcon("Flask bulb", "flask_bulb.svg", "flask bulb",""),
                new CronoIcon("Flyer double", "flyer_double.svg", "flyer double",""),
                new CronoIcon("Flyer single", "flyer_single.svg", "flyer single",""),
                new CronoIcon("Fork knife", "fork_knife.svg", "fork knife",""),
                new CronoIcon("Fortress tower", "fortress_tower.svg", "fortress tower",""),
                new CronoIcon("Furniture nightstand", "furniture_nightstand.svg", "furniture nightstand",""),
                new CronoIcon("Gas station", "gas_station.svg", "gas station",""),
                new CronoIcon("Gift box", "gift_box.svg", "gift box",""),
                new CronoIcon("Glasses closed", "glasses_closed.svg", "glasses closed",""),
                new CronoIcon("Glasses rounded", "glasses_rounded.svg", "glasses rounded",""),
                new CronoIcon("Glasses square", "glasses_square.svg", "glasses square",""),
                new CronoIcon("Goblet pot", "goblet_pot.svg", "goblet pot",""),
                new CronoIcon("Golf flag", "golf_flag.svg", "golf flag",""),
                new CronoIcon("Grocery cart", "grocery_cart.svg", "grocery cart",""),
                new CronoIcon("Hammer mallet", "hammer_mallet.svg", "hammer mallet",""),
                new CronoIcon("Headphones", "headphones.svg", "headphones",""),
                new CronoIcon("Heartbeat sinus", "heartbeat_sinus.svg", "heartbeat sinus",""),
                new CronoIcon("Horn speaker", "horn_speaker.svg", "horn speaker",""),
                new CronoIcon("House door", "house_door.svg", "house door",""),
                new CronoIcon("House onwheel", "house_onwheel.svg", "house onwheel",""),
                new CronoIcon("House wooden", "house_wooden.svg", "house wooden",""),
                new CronoIcon("Human group", "human_group.svg", "human group",""),
                new CronoIcon("Ice cream", "ice_cream.svg", "ice cream",""),
                new CronoIcon("Insect bug", "insect_bug.svg", "insect bug",""),
                new CronoIcon("Ipod player", "ipod_player.svg", "ipod player",""),
                new CronoIcon("Jasons mask", "jasons_mask.svg", "jasons mask",""),
                new CronoIcon("Key single", "key_single.svg", "key single",""),
                new CronoIcon("Laptop mac", "laptop_mac.svg", "laptop mac",""),
                new CronoIcon("Lifebuoy", "lifebuoy.svg", "lifebuoy",""),
                new CronoIcon("Lock locked", "lock_locked.svg", "lock locked",""),
                new CronoIcon("Mail letter", "mail_letter.svg", "mail letter",""),
                new CronoIcon("Man running single", "man_running_single.svg", "man running single",""),
                new CronoIcon("Man walking", "man_walking.svg", "man walking",""),
                new CronoIcon("Map mark", "map_mark.svg", "map mark",""),
                new CronoIcon("Map path", "map_path.svg", "map path",""),
                new CronoIcon("Map pin", "map_pin.svg", "map pin",""),
                new CronoIcon("Meal fastfood", "meal_fastfood.svg", "meal fastfood",""),
                new CronoIcon("Medal star", "medal_star.svg", "medal star",""),
                new CronoIcon("Medical case", "medical_case.svg", "medical case",""),
                new CronoIcon("Medical patch", "medical_patch.svg", "medical patch",""),
                new CronoIcon("Medical plus", "medical_plus.svg", "medical plus",""),
                new CronoIcon("Microphone ontable", "microphone_ontable.svg", "microphone ontable",""),
                new CronoIcon("Mill house", "mill_house.svg", "mill house",""),
                new CronoIcon("Mixer faders", "mixer_faders.svg", "mixer faders",""),
                new CronoIcon("Moon half", "moon_half.svg", "moon half",""),
                new CronoIcon("Newspapers", "newspapers.svg", "newspapers",""),
                new CronoIcon("Newspapers top", "newspapers_top.svg", "newspapers top",""),
                new CronoIcon("Note page", "note_page.svg", "note page",""),
                new CronoIcon("Palette", "palette.svg", "palette",""),
                new CronoIcon("Paperclip", "paperclip.svg", "paperclip",""),
                new CronoIcon("Path navigation", "path_navigation.svg", "path navigation",""),
                new CronoIcon("Paw print", "paw_print.svg", "paw print",""),
                new CronoIcon("Pc display", "pc_display.svg", "pc display",""),
                new CronoIcon("Pc display2", "pc_display2.svg", "pc display2",""),
                new CronoIcon("Pc display wide", "pc_display_wide.svg", "pc display wide",""),
                new CronoIcon("Phone handset", "phone_handset.svg", "phone handset",""),
                new CronoIcon("Phone smart", "phone_smart.svg", "phone smart",""),
                new CronoIcon("Photo camera", "photo_camera.svg", "photo camera",""),
                new CronoIcon("Piano keys", "piano_keys.svg", "piano keys",""),
                new CronoIcon("Picture hanged", "picture_hanged.svg", "picture hanged",""),
                new CronoIcon("Piggy bank", "piggy_bank.svg", "piggy bank",""),
                new CronoIcon("Pill capsule", "pill_capsule.svg", "pill capsule",""),
                new CronoIcon("Pipette", "pipette.svg", "pipette",""),
                new CronoIcon("Planet jupiter", "planet_jupiter.svg", "planet jupiter",""),
                new CronoIcon("Plane flight", "plane_flight.svg", "plane flight",""),
                new CronoIcon("Price tag shortcut", "price_tag_shortcut.svg", "price tag shortcut",""),
                new CronoIcon("Puzzle chunk", "puzzle_chunk.svg", "puzzle chunk",""),
                new CronoIcon("Rechargeble battery", "rechargeble_battery.svg", "rechargeble battery",""),
                new CronoIcon("Roadsign arrow", "roadsign_arrow.svg", "roadsign arrow",""),
                new CronoIcon("Round blacknwhite", "round_blacknwhite.svg", "round blacknwhite",""),
                new CronoIcon("Round compas", "round_compas.svg", "round compas",""),
                new CronoIcon("Sailboat", "sailboat.svg", "sailboat",""),
                new CronoIcon("Scheme path", "scheme_path.svg", "scheme path",""),
                new CronoIcon("Sealed flask", "sealed_flask.svg", "sealed flask",""),
                new CronoIcon("Search lens", "search_lens.svg", "search lens",""),
                new CronoIcon("Sega joystick", "sega_joystick.svg", "sega joystick",""),
                new CronoIcon("Shape heart", "shape_heart.svg", "shape heart",""),
                new CronoIcon("Shape star", "shape_star.svg", "shape star",""),
                new CronoIcon("Shoots polaroid", "shoots_polaroid.svg", "shoots polaroid",""),
                new CronoIcon("Shot glass", "shot_glass.svg", "shot glass",""),
                new CronoIcon("Simple calculator", "simple_calculator.svg", "simple calculator",""),
                new CronoIcon("Skull bones", "skull_bones.svg", "skull bones",""),
                new CronoIcon("Skull only", "skull_only.svg", "skull only",""),
                new CronoIcon("Socket outlet", "socket_outlet.svg", "socket outlet",""),
                new CronoIcon("Sound note", "sound_note.svg", "sound note",""),
                new CronoIcon("Speedometer", "speedometer.svg", "speedometer",""),
                new CronoIcon("Statue liberty", "statue_liberty.svg", "statue liberty",""),
                new CronoIcon("Stopwatch", "stopwatch.svg", "stopwatch",""),
                new CronoIcon("Sun dni", "sun_dni.svg", "sun dni",""),
                new CronoIcon("Sun star", "sun_star.svg", "sun star",""),
                new CronoIcon("Tablet pc", "tablet_pc.svg", "tablet pc",""),
                new CronoIcon("Tag man mark", "tag_man_mark.svg", "tag man mark",""),
                new CronoIcon("Tall buildings", "tall_buildings.svg", "tall buildings",""),
                new CronoIcon("Target aim", "target_aim.svg", "target aim",""),
                new CronoIcon("Target darts", "target_darts.svg", "target darts",""),
                new CronoIcon("Target darts goal", "target_darts_goal.svg", "target darts goal",""),
                new CronoIcon("Telescope", "telescope.svg", "telescope",""),
                new CronoIcon("Thunder", "thunder.svg", "thunder",""),
                new CronoIcon("Ticket", "ticket.svg", "ticket",""),
                new CronoIcon("Tie dress", "tie_dress.svg", "tie dress",""),
                new CronoIcon("Tree fir", "tree_fir.svg", "tree fir",""),
                new CronoIcon("Tv set", "tv_set.svg", "tv set",""),
                new CronoIcon("Ufo", "ufo.svg", "ufo",""),
                new CronoIcon("Ukulele guitar", "ukulele_guitar.svg", "ukulele guitar",""),
                new CronoIcon("Umbrella", "umbrella.svg", "umbrella",""),
                new CronoIcon("User card", "user_card.svg", "user card",""),
                new CronoIcon("Viking hemlet", "viking_hemlet.svg", "viking hemlet",""),
                new CronoIcon("Wall picture", "wall_picture.svg", "wall picture",""),
                new CronoIcon("Way signs", "way_signs.svg", "way signs",""),
                new CronoIcon("Wrench tool", "wrench_tool.svg", "wrench tool",""),
                ]);

           

            //icons.Add(new CronoIcon("Arrow crossed", "arrow_crossed.svg"));
            //icons.Add(new CronoIcon("Arrow infinity", "arrow_infinity.svg"));
            //icons.Add(new CronoIcon("Arrow crossed", "arrow_crossed.svg"));
            //icons.Add(new CronoIcon("Arrows rolled", "arrows_rolled.svg"));
            //icons.Add(new CronoIcon("Arrow spring", "arrow_spring.svg"));
            //icons.Add(new CronoIcon("Bag case", "bag_case.svg"));
            //icons.Add(new CronoIcon("Bag shopping", "bag_shopping.svg"));
            //icons.Add(new CronoIcon("Bag travel", "bag_travel.svg"));
            //icons.Add(new CronoIcon("Bird pigeon", "bird_pigeon.svg"));
            //icons.Add(new CronoIcon("Bookmark down", "bookmark_down.svg"));
            //icons.Add(new CronoIcon("Bus stop", "bus_stop.svg"));
            //icons.Add(new CronoIcon("Jeep car", "car_jeep.svg"));
            ////icons.Add(new CronoIcon("Jeep car", "jeep.svg"));
            //icons.Add(new CronoIcon("Card arrow", "card_arrow_down.svg"));
            //icons.Add(new CronoIcon("Chart bars", "chart_bars.svg"));
            //icons.Add(new CronoIcon("Chart graphic", "chart_graphic.svg"));
            //icons.Add(new CronoIcon("Chat empty", "chat_empty.svg"));
            //icons.Add(new CronoIcon("Chat filled", "chat_filled.svg"));
            //icons.Add(new CronoIcon("Clock round", "clock_round.svg"));
            //icons.Add(new CronoIcon("Cloud", "cloud_in_arrow.svg"));
            //icons.Add(new CronoIcon("Cloud thunder", "cloud_thunder.svg"));
            //icons.Add(new CronoIcon("Coffer", "coffer_arrowin.svg"));
            //icons.Add(new CronoIcon("Coffer docs", "coffer_docs.svg"));
            //icons.Add(new CronoIcon("Cogs", "cog_double.svg"));
            //icons.Add(new CronoIcon("Cup", "cup_hot.svg"));
            //icons.Add(new CronoIcon("T-shirt", "dress_tshirt.svg"));
            //icons.Add(new CronoIcon("Eye", "eye_opened.svg"));

            return icons;
        }
    }

    public class CronoIcon
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string FileName { get; set; }


        public CronoIcon() { }
        public CronoIcon(string title, string filename)
        {
            this.FileName = filename;
            this.Title = title;
        }

        public CronoIcon(string title, string filename, string tags, string description)
        {
            this.FileName = filename;
            this.Title = title;
        }
    }
}