using System;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.DrugInteraction")]
    public class DrugInteractionXpo : XPLiteObject
    {
        #region Members

        int fId;
        [Key(true)]
        [Persistent("Id")]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        DCIXpo fParentDCIId;
        [Association("DrugInteractionReferencesParentDCI")]
        [Persistent("ParentDCIId")]
        public DCIXpo ParentDCIId
        {
            get { return fParentDCIId; }
            set { SetPropertyValue<DCIXpo>("ParentDCIId", ref fParentDCIId, value); }
        }

        DCIXpo fDCIId;
        [Association("DrugInteractionReferencesDCI")]
        [Persistent("DCIId")]
        public DCIXpo DCIId
        {
            get { return fDCIId; }
            set { SetPropertyValue<DCIXpo>("DCIId", ref fDCIId, value); }
        }

        #endregion

        #region Builders

        public DrugInteractionXpo(Session session) : base(session)
        {
        }

        public DrugInteractionXpo()
            : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
