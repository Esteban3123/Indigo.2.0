#region "Imports"

using System;
using DevExpress.Xpo;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewEntranceVoucherDevolution")]
    public partial class ViewEntranceVoucherDevolutionXpo : XPLiteObject
    {
        #region "Members"

        string fId;
        [Key(true)]
        public string Id
        {
            get { return fId; }
            set { SetPropertyValue<string>("Id", ref fId, value); }
        }

        int fEntranceVoucherDevolutionId;
        public int EntranceVoucherDevolutionId
        {
            get { return fEntranceVoucherDevolutionId; }
            set { SetPropertyValue<int>("EntranceVoucherDevolutionId", ref fEntranceVoucherDevolutionId, value); }
        }

        int fEntranceVoucherId;
        public int EntranceVoucherId
        {
            get { return fEntranceVoucherId; }
            set { SetPropertyValue<int>("EntranceVoucherId", ref fEntranceVoucherId, value); }
        }

        int fEntranceVoucherDetailId;
        public int EntranceVoucherDetailId
        {
            get { return fEntranceVoucherDetailId; }
            set { SetPropertyValue<int>("EntranceVoucherDetailId", ref fEntranceVoucherDetailId, value); }
        }

        decimal fSubTotalValue;
        public decimal SubTotalValue
        {
            get { return fSubTotalValue; }
            set { SetPropertyValue<decimal>("SubTotalValue", ref fSubTotalValue, value); }
        }

        decimal fDiscountValue;
        public decimal DiscountValue
        {
            get { return fDiscountValue; }
            set { SetPropertyValue<decimal>("DiscountValue", ref fDiscountValue, value); }
        }

        decimal fIvaValue;
        public decimal IvaValue
        {
            get { return fIvaValue; }
            set { SetPropertyValue<decimal>("IvaValue", ref fIvaValue, value); }
        }

        decimal fRTFValue;
        public decimal RTFValue
        {
            get { return fRTFValue; }
            set { SetPropertyValue<decimal>("RTFValue", ref fRTFValue, value); }
        }

        #endregion

        #region Builders

        public ViewEntranceVoucherDevolutionXpo(Session session) : base(session)
        {
        }

        public ViewEntranceVoucherDevolutionXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
