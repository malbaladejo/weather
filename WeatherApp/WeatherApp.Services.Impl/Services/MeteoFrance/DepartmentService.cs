using Microsoft.AspNetCore.Hosting;

namespace WeatherApp.Services
{
    internal class DepartmentService : IDepartmentService
    {
        private bool initialized = false;

        public DepartmentService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public string DepartmentFilePath(string code)
            => Path.Combine(this.environment.WebRootPath, DepartmentConfig.FolderPath, $"{code}.json");

        public Department GetDepartment(string code)
        {
            this.EnsureDepartments();

            return this.GetDepartments().First(d => d.Code == code);
        }

        public IReadOnlyCollection<Department> GetDepartments()
        {
            this.EnsureDepartments();
            return this.departments;
        }

        public void RefreshDepartmentsStatus()
        {
            foreach (var department in this.departments)
            {
                var filePath = this.DepartmentFilePath(department.Code);
                department.StationsLoaded = File.Exists(filePath);
            }
        }

        private void EnsureDepartments()
        {
            if (this.initialized) return;

            this.RefreshDepartmentsStatus();

            this.initialized = true;
        }

        private readonly Department[] departments = new[]{
            new Department("01","Ain"),
            new Department("02","Aisne"),
            new Department("03","Allier"),
            new Department("04","Alpes de Haute-Provence"),
            new Department("05","Hautes-Alpes"),
            new Department("06","Alpes-Maritimes"),
            new Department("07","Ardêche"),
            new Department("08","Ardennes"),
            new Department("09","Ariège"),
            new Department("10","Aube"),
            new Department("11","Aude"),
            new Department("12","Aveyron"),
            new Department("13","Bouches-du-Rhône"),
            new Department("14","Calvados"),
            new Department("15","Cantal"),
            new Department("16","Charente"),
            new Department("17","Charente-Maritime"),
            new Department("18","Cher"),
            new Department("19","Corrèze"),
            new Department("2","Corse"),
            new Department("21","Côte-d'Or"),
            new Department("22","Côtes d'Armor"),
            new Department("23","Creuse"),
            new Department("24","Dordogne"),
            new Department("25","Doubs"),
            new Department("26","Drôme"),
            new Department("27","Eure"),
            new Department("28","Eure-et-Loir"),
            new Department("29","Finistère"),
            new Department("30","Gard"),
            new Department("31","Haute-Garonne"),
            new Department("32","Gers"),
            new Department("33","Gironde"),
            new Department("34","Hérault"),
            new Department("35","Îlle-et-Vilaine"),
            new Department("36","Indre"),
            new Department("37","Indre-et-Loire"),
            new Department("38","Isère"),
            new Department("39","Jura"),
            new Department("40","Landes"),
            new Department("41","Loir-et-Cher"),
            new Department("42","Loire"),
            new Department("43","Haute-Loire"),
            new Department("44","Loire-Atlantique"),
            new Department("45","Loiret"),
            new Department("46","Lot"),
            new Department("47","Lot-et-Garonne"),
            new Department("48","Lozère"),
            new Department("49","Maine-et-Loire"),
            new Department("50","Manche"),
            new Department("51","Marne"),
            new Department("52","Haute-Marne"),
            new Department("53","Mayenne"),
            new Department("54","Meurthe-et-Moselle"),
            new Department("55","Meuse"),
            new Department("56","Morbihan"),
            new Department("57","Moselle"),
            new Department("58","Nièvre"),
            new Department("59","Nord"),
            new Department("60","Oise"),
            new Department("61","Orne"),
            new Department("62","Pas-de-Calais"),
            new Department("63","Puy-de-Dôme"),
            new Department("64","Pyrénées-Atlantiques"),
            new Department("65","Hautes-Pyrénées"),
            new Department("66","Pyrénées-Orientales"),
            new Department("67","Bas-Rhin"),
            new Department("68","Haut-Rhin"),
            new Department("69","Rhône"),
            new Department("70","Haute-Saône"),
            new Department("71","Saône-et-Loire"),
            new Department("72","Sarthe"),
            new Department("73","Savoie"),
            new Department("74","Haute-Savoie"),
            new Department("75","Paris"),
            new Department("76","Seine-Maritime"),
            new Department("77","Seine-et-Marne"),
            new Department("78","Yvelines"),
            new Department("79","Deux-Sèvres"),
            new Department("80","Somme"),
            new Department("81","Tarn"),
            new Department("82","Tarn-et-Garonne"),
            new Department("83","Var"),
            new Department("84","Vaucluse"),
            new Department("85","Vendée"),
            new Department("86","Vienne"),
            new Department("87","Haute-Vienne"),
            new Department("88","Vosges"),
            new Department("89","Yonne"),
            new Department("90","Territoire-de-Belfort"),
            new Department("91","Essonne"),
            new Department("92","Hauts-de-Seine"),
            new Department("93","Seine-Saint-Denis"),
            new Department("94","Val-de-Marne"),
            new Department("95","Val-d'Oise")
        };
        private readonly IWebHostEnvironment environment;
    }
}
