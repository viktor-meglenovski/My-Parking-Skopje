using MyParkingSkopje.Models;
using MyParkingSkopje.ViewModels;
using System.IO;
using System.Web;
using System.Linq;

namespace MyParkingSkopje.Service
{
    //Класа во која се наоѓа бизнис логиката за ProfileController - имплементира Singleton Design Pattern
    public class ProfileService
    {
        private static ProfileService profileService { get; set; }
        private ApplicationDbContext _context { get; set; }
        private ProfileService()
        {
            this._context = new ApplicationDbContext();
        }
        public static ProfileService ProfileServiceInstance()
        {
            if (profileService == null)
                profileService = new ProfileService();
            return profileService;
        }
       
        //Метод кој прави инстанца од EditProfileViewModel врз основа на ID на корисникот кое е испратено како атрибут
        public EditProfileViewModel getEditProfileViewModel(string userId)
        {
            var user = _context.Users.Find(userId);
            return new EditProfileViewModel(user.Id, user.FirstName, user.LastName, user.ProfilePictureUrl);
        }

        //Метод кој го ажурира тековниот корисник
        public void editUserProfile(EditProfileViewModel model,string userId, string basePath, HttpFileCollectionBase requestFiles)
        {
            //Од базата го земаме тековниот корисник
            var toUpdate = _context.Users.Find(userId);

            //Проверуваме дали тековниот корисник одговара со корисникот кој треба да се ажурира
            if (toUpdate.Id != model.UserId)
                //Доколку условот не е исполнет, акцијата не се презема
                return;

            //Ги ажурираме името и пезимето
            toUpdate.FirstName = model.FirstName;
            toUpdate.LastName = model.LastName;

            //Ако била прикачена нова профилна слика
            if (requestFiles.Count != 0)
            {
                //Се зема фајлот
                var file = requestFiles[0];
                if(Path.GetFileName(file.FileName)!="")
                    //Се зачувува во соодветниот фолдер и се ажурира патеката за профилната слика на корисникот
                    toUpdate.ProfilePictureUrl = saveImageToUserFolder(file, basePath,toUpdate.Id);
            }
            //Ги зачувуваме промените во базата
            _context.SaveChanges();
        }
        //Метод кој го зачувува гајлот пуштен како аргумент во соодветниот фолдер на тековниот корисник
        public string saveImageToUserFolder(HttpPostedFileBase file, string basePath, string userId)
        {
            var fileName = Path.GetFileName(file.FileName);
            //Градиме патека за новата слика ~/UserUploads/UserId/fileName и таму ја зачувуваме сликата
            //Забелешка:При секоја регистрација на нов корисник, во фолдерот ~/UserUploads се прави нов фолдер со име еднакво
            //на ID на новиот корисник. Сите прикачени датотеки од тој корисник се прикачуваат во тој фолдер.
            var path = Path.Combine(basePath, userId, fileName);
            file.SaveAs(path);

            //Ја ажурираме патеката на профилната слика со новата патека
            return Path.Combine(@"/UserUploads", userId, fileName);
        }
        //Метод кој го зачувува фајлот пуштен како атрибут на предодредена привремена локација
        public string saveImageToTempFolder(HttpPostedFileBase file, string basePath)
        {
            //Го земаме името на сликата
            var fileName = Path.GetFileName(file.FileName);

            //Се прави патека на ~/UserUploads/Temp/fileName и се зачувува фајлот
            //Забелешка:Овој Temp фолдер се користи само за успешно прикажување на прикачената слика,
            //доколку корисникот ја откажи промената на профилната слика, сликата останува
            //во овој фолдер, но не се поврзува како профилна слика со ниту еден корисник.
            var path = Path.Combine(basePath, fileName);
            file.SaveAs(path);

            //Ја враќаме привремената патека на новата слика
            return Path.Combine(@"/UserUploads/Temp", Path.GetFileName(fileName));
        }
        //Метод кој ја ажурира локацијата на корисникот со даденото ID
        public void updateUserLocation(double lattitude, double longitude, string userId)
        {
            //Проверка дали постои UserLocation објект за дадениот корисник
            var existing = _context.UserLocations.Where(x => x.UserId == userId);
            //Ако не постои таков објект:
            if (existing.Count() == 0)
                //Додади нова инстанца од UserLocation со соодветните вредности
                _context.UserLocations.Add(new UserLocation(userId, lattitude, longitude));
            else
            //Ако веќе постои таков објект:
            {
                //Ажурирај ги вредностите
                var existingLocation = existing.First();
                existingLocation.Lattitude = lattitude;
                existingLocation.Longitude = longitude;
            }
            //Зачувај ги промените во база
            _context.SaveChanges();
        }
    }
}