#region "Imports"

using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewLASAMedication")]
    public partial class ViewLASAMedication : XPLiteObject
    {
        #region "Members"

        string fUUID;
        [Key(true)]
        public string UUID
        {
            get { return fUUID; }
            set { SetPropertyValue<string>("UUID", ref fUUID, value); }
        }

        string fCode;
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fRequestedMedication;
        public string RequestedMedication
        {
            get { return fRequestedMedication; }
            set { SetPropertyValue<string>("RequestedMedication", ref fRequestedMedication, value); }
        }

        string fLikeness;
        public string Likeness
        {
            get { return fLikeness; }
            set { SetPropertyValue<string>("Likeness", ref fLikeness, value); }
        }

        string fSimilarMedicine;
        public string SimilarMedicine
        {
            get { return fSimilarMedicine; }
            set { SetPropertyValue<string>("SimilarMedicine", ref fSimilarMedicine, value); }
        }

        #endregion

        #region Builders

        public ViewLASAMedication(Session session) : base(session)
        {
        }

        public ViewLASAMedication() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
