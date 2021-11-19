using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using PM2E2GRUPO_4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2E2GRUPO_4.Controller
{
   public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://apis-movil-2-default-rtdb.firebaseio.com/ExamenBD");
        static FirebaseStorage stroageImage = new FirebaseStorage("apis-movil-2.appspot.com");

        public async Task AddPerson(String personId, string name, string descripcion, string DescripcionCorta, string img)
        {

            await firebase
              .Child("ExamenBD")
              .PostAsync(new Ubicaciones() { latitud = personId, longitud = name, descripcion= descripcion, DescripcionCorta = DescripcionCorta, img= img });
        }

        public static async Task<string> UploadFile(Stream fileStream, string fileName)
        {
            var imageUrl = await stroageImage
                .Child("ExamenImagenes")
                .Child(fileName)
                .PutAsync(fileStream);
            return imageUrl;
        }

        public async Task AddUbicacion(string latitud, string longitud, string descripcion)
        {
            Ubicaciones i = new Ubicaciones() { latitud = latitud, longitud = longitud, descripcion = descripcion };
            await firebase.Child("ExamenBD")
                .PostAsync(i);
        }

        public ObservableCollection<Ubicaciones> getUbicaciones()
        {
            var itemData = firebase.Child("ExamenBD").AsObservable<Ubicaciones>()
                .AsObservableCollection();
            return itemData;
        }
        public async Task DeleteUbicacion(string latitud, string longitud, string descripcion)
        {
            var toDeleteUbicacion = (await firebase.Child("ExamenBD")
                .OnceAsync<Ubicaciones>()).FirstOrDefault(a => a.Object.latitud == latitud
                || a.Object.longitud == longitud || a.Object.descripcion == descripcion);
            await firebase.Child("ExamenBD").Child(toDeleteUbicacion.Key).DeleteAsync();
        }
        public async Task UpdateUbicaciones(string latitud, string longitud, string descripcion)
        {
            var toUpdateUbicaciones = (await firebase.
                Child("ExamenBD").OnceAsync<Ubicaciones>()).FirstOrDefault
                (a => a.Object.latitud == latitud
                || a.Object.longitud == longitud || a.Object.descripcion == descripcion);
            Ubicaciones u = new Ubicaciones() { latitud = latitud, longitud = longitud, descripcion = descripcion };
            await firebase.Child("ExamenBD").Child(toUpdateUbicaciones.Key).PutAsync(u);
        }
    }
}
