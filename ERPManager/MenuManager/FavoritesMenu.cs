using ERPFramework;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

namespace ERPManager.MenuManager
{
    public class FavoritesMenu : List<ApplicationMenuItem>
    {
        public bool HasFavorites { get { return Count > 0; } }
        public bool IsFavorite(ApplicationMenuItem item)
        {
            var result = this.Find(p => p.Namespace.ToString() == item.Namespace.ToString());

            return result != null;
        }

        public void AddItem(ApplicationMenuItem item)
        {
            Add(item);
            Save(this);
        }

        public void RemoveItem(ApplicationMenuItem item, bool save = true)
        {
            var result = this.Find(p => p.Namespace.ToString() == item.Namespace.ToString());
            if (result != null)
                Remove(result);
            if (save)
                Save(this);
        }

        public static FavoritesMenu Load()
        {
            FavoritesMenu favoritesMenu = null;
            if (Properties.Settings.Default.FavoritesMenu.IsEmpty())
            {
                Properties.Settings.Default.Upgrade();
                if (Properties.Settings.Default.FavoritesMenu.IsEmpty())
                    favoritesMenu = new FavoritesMenu();
                return favoritesMenu;
            }
            else
                using (System.IO.MemoryStream sw = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(Properties.Settings.Default.FavoritesMenu)))
                {
                    try
                    {
                        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(FavoritesMenu), new System.Type[] { typeof(NameSpace) });
                        favoritesMenu = serializer.Deserialize(sw) as FavoritesMenu;

                    }
                    catch (Exception)
                    {
                        favoritesMenu = new FavoritesMenu();
                    }
                    finally
                    {
                        sw.Close();
                    }
                }

            return favoritesMenu;
        }

        public static void Save(FavoritesMenu favoritesMenu)
        {
            if (favoritesMenu.Count == 0)
            {
                Properties.Settings.Default.FavoritesMenu = string.Empty;
                Properties.Settings.Default.Save();
                return;
            }

            using (System.IO.MemoryStream sw = new System.IO.MemoryStream())
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(FavoritesMenu), new System.Type[] { typeof(NameSpace) });
                serializer.Serialize(sw, favoritesMenu);
                var s = System.Text.Encoding.UTF8.GetString(sw.ToArray());
                Properties.Settings.Default.FavoritesMenu = System.Text.Encoding.UTF8.GetString(sw.ToArray());
                sw.Close();
            }
            Properties.Settings.Default.Save();
        }

        internal void ChangeOrder(Control.ControlCollection controls)
        {
            //Remove visibble item
            for (int t = controls.Count - 1; t >= 0; t--)
            {
                if (controls[t] is FavoriteButton)
                {
                    var item = (controls[t] as FavoriteButton).Item;
                    RemoveItem(item, false);
                }
            }

            for (int t = controls.Count - 1; t >= 0; t--)
            {
                if (controls[t] is FavoriteButton)
                {
                    var item = (controls[t] as FavoriteButton).Item;
                    Add(item);
                }
            }
            Save(this);
        }
    }
}
