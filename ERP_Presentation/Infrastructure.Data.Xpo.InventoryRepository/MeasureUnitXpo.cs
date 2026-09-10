//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 05-04-2014
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.InventoryMeasurementUnit")]
    public class MeasureUnitXpo : XPLiteObject
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

        string fCode;
        [Size(20)]
        [Persistent("Code")]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fName;
        [Size(100)]
        [Persistent("Name")]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        byte fUnitType;
        [Persistent("UnitType")]
        public byte UnitType
        {
            get { return fUnitType; }
            set { SetPropertyValue<byte>("UnitType", ref fUnitType, value); }
        }

        string fAbbreviation;
        [Size(10)]
        [Persistent("Abbreviation")]
        public string Abbreviation
        {
            get { return fAbbreviation; }
            set { SetPropertyValue<string>("Abbreviation", ref fAbbreviation, value); }
        }

        decimal fCostValue;
        [Persistent("CostValue")]
        public decimal CostValue
        {
            get { return fCostValue; }
            set { SetPropertyValue<decimal>("CostValue", ref fCostValue, value); }
        }

        bool fAllowEditCostValue;
        [Persistent("AllowEditCostValue")]
        public bool AllowEditCostValue
        {
            get { return fAllowEditCostValue; }
            set { SetPropertyValue<bool>("AllowEditCostValue", ref fAllowEditCostValue, value); }
        }

        byte fStatus;
        [Persistent("Status")]
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        [Size(50)]
        [PersistentAlias("concat(concat(Code,' - '),Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        [Association(@"InventoryMeasurementUnit", typeof(InventoryProductXpo))]
        public XPCollection<InventoryProductXpo> InventoryProductXpo { get { return GetCollection<InventoryProductXpo>("InventoryProductXpo"); } }

        [Association("ATCEReferencesWeightMeasurementUnit", typeof(ATCXpo))]
        public XPCollection<ATCXpo> ATCXpo
        {
            get { return GetCollection<ATCXpo>("ATCXpo"); }
        }

        [Association("ATCEReferencesVolumeMeasurementUnit", typeof(ATCXpo))]
        public XPCollection<ATCXpo> ATCXpo2
        {
            get { return GetCollection<ATCXpo>("ATCXpo2"); }
        }

        #endregion

        #region Builders

        public MeasureUnitXpo(Session session) : base(session)
        {
        }

        public MeasureUnitXpo()
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
