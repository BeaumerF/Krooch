using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Realms;

namespace epicture
{
    class Favorites
    {
        public List<RealmImage> list = new List<RealmImage>();
        Realm realm;

        public Favorites()
        {
            realm = Realm.GetInstance();
            //realm.Dispose();
            var save = realm.All<RealmImage>().ToList<RealmImage>();

            foreach (var img in save)
                list.Add(img);
        }

        public void Add(String toCopy)
        {
            RealmImage cpy = new RealmImage();
            cpy.image = toCopy;
            cpy.id = 0;

            if (realm.All<RealmImage>().Count<RealmImage>() > 0)
            {
                var save = realm.All<RealmImage>().ToList<RealmImage>();
                cpy.id = save.Last<RealmImage>().id + 1;
            }

            list.Add(cpy);
            realm.Write(() =>
            {
                realm.Add(cpy);
            });
        }

        public void Delete(String url)
        {
            var img = realm.All<RealmImage>().First(b => b.image == url);

            // Delete an object with a transaction
            using (var trans = realm.BeginWrite())
            {
                realm.Remove(img);
                trans.Commit();
            }

            list.Clear();
            list = realm.All<RealmImage>().ToList<RealmImage>();
        }
    }
}
