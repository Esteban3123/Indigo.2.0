#region "Imports"

using System;
using DevExpress.Xpo;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewManualMovements")]
    public partial class ViewManualMovementsXpo : XPLiteObject
    {
        #region "Members"

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        string fFunctionalUnit;
        public string FunctionalUnit
        {
            get { return fFunctionalUnit; }
            set { SetPropertyValue<string>("FunctionalUnit", ref fFunctionalUnit, value); }
        }

        string fPatient;
        public string Patient
        {
            get { return fPatient; }
            set { SetPropertyValue<string>("Patient", ref fPatient, value); }
        }

        string fPatientCode;
        public string PatientCode
        {
            get { return fPatientCode; }
            set { SetPropertyValue<string>("PatientCode", ref fPatientCode, value); }
        }

        DateTime fDocumentDate;
        public DateTime DocumentDate
        {
            get { return fDocumentDate; }
            set { SetPropertyValue<DateTime>("DocumentDate", ref fDocumentDate, value); }
        }

        string fCreationUser;
        public string CreationUser
        {
            get { return fCreationUser; }
            set { SetPropertyValue<string>("CreationUser", ref fCreationUser, value); }
        }

        int fProductId;
        public int ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int>("ProductId", ref fProductId, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        string fAdmissionNumber;
        public string AdmissionNumber
        {
            get { return fAdmissionNumber; }
            set { SetPropertyValue<string>("AdmissionNumber", ref fAdmissionNumber, value); }
        }

        string fCodeProduct;
        public string CodeProduct
        {
            get { return fCodeProduct; }
            set { SetPropertyValue<string>("CodeProduct", ref fCodeProduct, value); }
        }

        string fMovementType;
        public string MovementType
        {
            get { return fMovementType; }
            set { SetPropertyValue<string>("MovementType", ref fMovementType, value); }
        }


        #endregion

        #region Builders

        public ViewManualMovementsXpo(Session session) : base(session)
        {
        }

        public ViewManualMovementsXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
