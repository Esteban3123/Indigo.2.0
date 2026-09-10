namespace Application.Events.Models
{
    public class MATCAdministrationRoute
    {
        public PharmaceuticalForm PharmaceuticalForm = new PharmaceuticalForm();

        public string Code;
        public string Name;
        public int Status;
        public int IsDelete;

    }
}
