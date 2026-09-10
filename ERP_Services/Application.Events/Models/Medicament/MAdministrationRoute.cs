namespace Application.Events.Models
{
    /// <summary>
    /// Via de administración
    /// </summary>
    public class MAdministrationRoute
    {
        public PharmaceuticalForm PharmaceuticalForm;
        public string Code;
        public string Name;
        public int Status; //status  = > PharmaceuticalForm;
    }
}
