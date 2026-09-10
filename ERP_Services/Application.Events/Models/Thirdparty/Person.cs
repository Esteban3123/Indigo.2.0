namespace Application.Events.Models.Thirdparty
{
    /// <summary>
    /// Datos para la tabla Persona
    /// </summary>
    public class Person
    {
        public string IdentificationNumber;
        public int IdentificationType;
        public string FirstName;
        public string MiddleName;
        public string Surname;
        public string SecondSurname;
        public IdentificacionCity IdentificationCity;
        public MAddress[] Address;
        public Phones[] Phone;
        public Emails[] Email;
        public int State;
    }
}
