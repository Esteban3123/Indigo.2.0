#region "Imports"

using DevExpress.Xpo;
using System;

#endregion

namespace Infrastructure.Data.Xpo.InventoryRepository.View
{
    [Persistent(@"Inventory.ViewRequestParam")]
    public class ViewRequestParamXpo : XPLiteObject
    {
        #region "Members"

        string fViewKey;
        [Key(true)]
        public string ViewKey
        {
            get { return fViewKey; }
            set { SetPropertyValue<string>("ViewKey", ref fViewKey, value); }
        }

        int fFunctionalUnitId;
        public int FunctionalUnitId
        {
            get { return fFunctionalUnitId; }
            set { SetPropertyValue<int>("FunctionalUnitId", ref fFunctionalUnitId, value); }
        }

        int fWarehouseId;
        public int WarehouseId
        {
            get { return fWarehouseId; }
            set { SetPropertyValue<int>("WarehouseId", ref fWarehouseId, value); }
        }

        bool fMonday;
        public bool Monday
        {
            get { return fMonday; }
            set { SetPropertyValue<bool>("Monday", ref fMonday, value); }
        }

        bool fTuesday;
        public bool Tuesday
        {
            get { return fTuesday; }
            set { SetPropertyValue<bool>("Tuesday", ref fTuesday, value); }
        }

        bool fWednesday;
        public bool Wednesday
        {
            get { return fWednesday; }
            set { SetPropertyValue<bool>("Wednesday", ref fWednesday, value); }
        }

        bool fThursday;
        public bool Thursday
        {
            get { return fThursday; }
            set { SetPropertyValue<bool>("Thursday", ref fThursday, value); }
        }

        bool fFriday;
        public bool Friday
        {
            get { return fFriday; }
            set { SetPropertyValue<bool>("Friday", ref fFriday, value); }
        }

        bool fSaturday;
        public bool Saturday
        {
            get { return fSaturday; }
            set { SetPropertyValue<bool>("Saturday", ref fSaturday, value); }
        }

        bool fSunday;
        public bool Sunday
        {
            get { return fSunday; }
            set { SetPropertyValue<bool>("Sunday", ref fSunday, value); }
        }

        bool fHoliday;
        public bool Holiday
        {
            get { return fHoliday; }
            set { SetPropertyValue<bool>("Holiday", ref fHoliday, value); }
        }

        byte fType;
        public byte Type
        {
            get { return fType; }
            set { SetPropertyValue<byte>("Type", ref fType, value); }
        }

        int? fSupplieId;
        public int? SupplieId
        {
            get { return fSupplieId; }
            set { SetPropertyValue<int?>("SupplieId", ref fSupplieId, value); }
        }

        int? fProductId;
        public int? ProductId
        {
            get { return fProductId; }
            set { SetPropertyValue<int?>("ProductId", ref fProductId, value); }
        }

        int fQuantity;
        public int Quantity
        {
            get { return fQuantity; }
            set { SetPropertyValue<int>("Quantity", ref fQuantity, value); }
        }

        bool fAproved;
        public bool Aproved
        {
            get { return fAproved; }
            set { SetPropertyValue<bool>("Aproved", ref fAproved, value); }
        }

        int fFrecuency;
        public int Frecuency
        {
            get { return fFrecuency; }
            set { SetPropertyValue<int>("Frecuency", ref fFrecuency, value); }
        }

        string fUnitTime;
        public string UnitTime
        {
            get { return fUnitTime; }
            set { SetPropertyValue<string>("UnitTime", ref fUnitTime, value); }
        }

        string fMeasuryUnitTime;
        public string MeasuryUnitTime
        {
            get { return fMeasuryUnitTime; }
            set { SetPropertyValue<string>("MeasuryUnitTime", ref fMeasuryUnitTime, value); }
        }

        #endregion

        #region Builders

        public ViewRequestParamXpo(Session session) : base(session)
        {
        }

        public ViewRequestParamXpo() : base(Session.DefaultSession)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        #endregion
    }
}
