using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent(@"Common.Person")]
    public class InventoryCommonPersonXpo : XPLiteObject
    {
        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }
        string fIdentificationNumber;
        //[Indexed(Name = @"IX_Person_IdentificationNumber", Unique = true)]
        [Size(15)]
        public string IdentificationNumber
        {
            get { return fIdentificationNumber; }
            set { SetPropertyValue<string>("IdentificationNumber", ref fIdentificationNumber, value); }
        }
        int fIdentificationType;
        public int IdentificationType
        {
            get { return fIdentificationType; }
            set { SetPropertyValue<int>("IdentificationType", ref fIdentificationType, value); }
        }
        InventoryCommonCityXpo fIdentificacionCityId;
        [Association(@"Common_PersonReferencesCommon_City1")]
        public InventoryCommonCityXpo IdentificacionCityId
        {
            get { return fIdentificacionCityId; }
            set { SetPropertyValue<InventoryCommonCityXpo>("IdentificacionCityId", ref fIdentificacionCityId, value); }
        }
        DateTime fIdentificationExpeditionDate;
        public DateTime IdentificationExpeditionDate
        {
            get { return fIdentificationExpeditionDate; }
            set { SetPropertyValue<DateTime>("IdentificationExpeditionDate", ref fIdentificationExpeditionDate, value); }
        }
        int fMilitaryCardId;
        public int MilitaryCardId
        {
            get { return fMilitaryCardId; }
            set { SetPropertyValue<int>("MilitaryCardId", ref fMilitaryCardId, value); }
        }
        string fMilitaryCardNumber;
        [Size(15)]
        public string MilitaryCardNumber
        {
            get { return fMilitaryCardNumber; }
            set { SetPropertyValue<string>("MilitaryCardNumber", ref fMilitaryCardNumber, value); }
        }
        string fFirstName;
        [Size(50)]
        public string FirstName
        {
            get { return fFirstName; }
            set { SetPropertyValue<string>("FirstName", ref fFirstName, value); }
        }
        string fSecondName;
        [Size(50)]
        public string SecondName
        {
            get { return fSecondName; }
            set { SetPropertyValue<string>("SecondName", ref fSecondName, value); }
        }
        string fFirstLastName;
        [Size(50)]
        public string FirstLastName
        {
            get { return fFirstLastName; }
            set { SetPropertyValue<string>("FirstLastName", ref fFirstLastName, value); }
        }
        string fSecondLastName;
        [Size(50)]
        public string SecondLastName
        {
            get { return fSecondLastName; }
            set { SetPropertyValue<string>("SecondLastName", ref fSecondLastName, value); }
        }
        DateTime fBirthDate;
        public DateTime BirthDate
        {
            get { return fBirthDate; }
            set { SetPropertyValue<DateTime>("BirthDate", ref fBirthDate, value); }
        }
        InventoryCommonCityXpo fBirthCityId;
        [Association(@"Common_PersonReferencesCommon_City")]
        public InventoryCommonCityXpo BirthCityId
        {
            get { return fBirthCityId; }
            set { SetPropertyValue<InventoryCommonCityXpo>("BirthCityId", ref fBirthCityId, value); }
        }
        DateTime fDeathDate;
        public DateTime DeathDate
        {
            get { return fDeathDate; }
            set { SetPropertyValue<DateTime>("DeathDate", ref fDeathDate, value); }
        }
        byte fGender;
        public byte Gender
        {
            get { return fGender; }
            set { SetPropertyValue<byte>("Gender", ref fGender, value); }
        }
        string fBloodGroup;
        [Size(2)]
        public string BloodGroup
        {
            get { return fBloodGroup; }
            set { SetPropertyValue<string>("BloodGroup", ref fBloodGroup, value); }
        }
        char fRH;
        public char RH
        {
            get { return fRH; }
            set { SetPropertyValue<char>("RH", ref fRH, value); }
        }
        byte[] fFingerprint;
        [Size(SizeAttribute.Unlimited)]
        public byte[] Fingerprint
        {
            get { return fFingerprint; }
            set { SetPropertyValue<byte[]>("Fingerprint", ref fFingerprint, value); }
        }
        byte fSonNumber;
        public byte SonNumber
        {
            get { return fSonNumber; }
            set { SetPropertyValue<byte>("SonNumber", ref fSonNumber, value); }
        }
        byte fDependents;
        public byte Dependents
        {
            get { return fDependents; }
            set { SetPropertyValue<byte>("Dependents", ref fDependents, value); }
        }
        byte fMaritalStatus;
        public byte MaritalStatus
        {
            get { return fMaritalStatus; }
            set { SetPropertyValue<byte>("MaritalStatus", ref fMaritalStatus, value); }
        }
        bool fState;
        public bool State
        {
            get { return fState; }
            set { SetPropertyValue<bool>("State", ref fState, value); }
        }
        [Association(@"Common_AddressReferencesCommon_Person", typeof(InventoryCommonAddressXpo))]
        public XPCollection<InventoryCommonAddressXpo> Common_Addresss { get { return GetCollection<InventoryCommonAddressXpo>("Common_Addresss"); } }
        [Association(@"Common_PhoneReferencesCommon_Person", typeof(InventoryCommonPhoneXpo))]
        public XPCollection<InventoryCommonPhoneXpo> Common_Phones { get { return GetCollection<InventoryCommonPhoneXpo>("Common_Phones"); } }
        [Association(@"Common_ThirdPartyReferencesCommon_Person", typeof(InventoryCommonThirdPartyXpo))]
        public XPCollection<InventoryCommonThirdPartyXpo> Common_ThirdPartys { get { return GetCollection<InventoryCommonThirdPartyXpo>("Common_ThirdPartys"); } }

        public InventoryCommonPersonXpo(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

    }
}
