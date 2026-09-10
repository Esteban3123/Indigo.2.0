//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 16/09/2014
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
    [Persistent("Inventory.AdjustmentConcept")]
    public class AdjustmentConceptXpo : XPLiteObject
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

        byte fStatus;
        [Persistent("Status")]
        public byte Status
        {
            get { return fStatus; }
            set { SetPropertyValue<byte>("Status", ref fStatus, value); }
        }

        int fMovementClass;
        [Persistent("MovementClass")]
        public int MovementClass
        {
            get { return fMovementClass; }
            set { SetPropertyValue<int>("MovementClass", ref fMovementClass, value); }
        }

        bool fAffectsAverageCost;
        [Persistent("AffectsAverageCost")]
        public bool AffectsAverageCost
        {
            get { return fAffectsAverageCost; }
            set { SetPropertyValue<bool>("AffectsAverageCost", ref fAffectsAverageCost, value); }
        }

        int fConceptType;
        [Persistent("ConceptType")]
        public int ConceptType
        {
            get { return fConceptType; }
            set { SetPropertyValue<int>("ConceptType", ref fConceptType, value); }
        }

        int fCostCenterId;
        [Persistent("CostCenterId")]
        public int CostCenterId
        {
            get { return fConceptType; }
            set { SetPropertyValue<int>("CostCenterId", ref fCostCenterId, value); }
        }

        [PersistentAlias("Iif(ConceptType = 1, 'Entrada', Iif(ConceptType = 2, 'Salida', ''))")]
        public string ConceptTypeName
        {
            get
            {
                return Convert.ToString(this.EvaluateAlias("ConceptTypeName"));
                //switch (fConceptType)
                //{
                //    case 1:
                //        return "Entrada";
                //    case 2:
                //        return "Salida";
                //}
                //return "";
            }
        }

        [PersistentAlias("Iif(MovementClass = 1, 'Ajuste de inventario', Iif(MovementClass = 2, 'Traslado por consumo', ''))")]
        public string MovementClassName
        {
            get
            {
                return Convert.ToString(this.EvaluateAlias("MovementClassName"));
                //    switch (fMovementClass)
                //    {
                //        case 1:
                //            return "Ajuste de inventario";
                //        case 2:
                //            return "Traslado por consumo";
                //    }
                //    return "";
            }
        }

        [Size(50)]
        [PersistentAlias("concat(Code,' - ',Name)")]
        public string CodeName
        {
            get { return Convert.ToString(this.EvaluateAlias("CodeName")); }
        }

        #endregion

        #region Associations
        
        [Association(@"Inventory_AdjustmentConceptUser_AdjustmentConcept", typeof(AdjustmentConceptuserXpo))]
        public XPCollection<AdjustmentConceptuserXpo> Inventory_AdjustmentConceptUsers { get { return GetCollection<AdjustmentConceptuserXpo>("Inventory_AdjustmentConceptUsers"); } }


        #endregion

        #region Builders

        public AdjustmentConceptXpo(Session session) : base(session)
        {
        }

        public AdjustmentConceptXpo()
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
