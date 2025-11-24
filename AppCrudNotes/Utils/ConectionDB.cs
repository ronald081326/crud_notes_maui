

namespace AppCrudNotes.Utils
{
    public static class ConectionDB
    {
        public static string getRoute(string nameDb)
        {
            // se puede usar esta ruta generica para todas las plataformas
            return Path.Combine(FileSystem.AppDataDirectory, nameDb);

            /*if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                routeDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                routeDb = Path.Combine(routeDb, nameDb);

            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                routeDb = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                routeDb = Path.Combine(routeDb,"..", "Library", nameDb);
            }
            else 
            {
                routeDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                routeDb = Path.Combine(routeDb, nameDb);
            }*/


           // return routeDb;
        }

    }
}
