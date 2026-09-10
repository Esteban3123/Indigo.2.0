//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.InventoryRepository
//' Author           : Carlos Mario Arias Rubiano
//' Created          : 29/04/2019
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ViewListATCEntityRelatedDCI")]
    public class ViewListATCEntityRelatedDCIXpo : XPLiteObject
    {
        #region Members

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fATCId;
        public int ATCId
        {
            get { return fATCId; }
            set { SetPropertyValue<int>("ATCId", ref fATCId, value); }
        }

        string fCode;
        [Size(20)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>("Code", ref fCode, value); }
        }

        string fName;
        [Size(100)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>("Name", ref fName, value); }
        }

        string fCodeName;
        [Size(100)]
        public string CodeName
        {
            get { return fCodeName; }
            set { SetPropertyValue<string>("CodeName", ref fCodeName, value); }
        }

        int fDCIId;
        public int DCIId
        {
            get { return fDCIId; }
            set { SetPropertyValue<int>("DCIId", ref fDCIId, value); }
        }

        #endregion

        #region Builders

        public ViewListATCEntityRelatedDCIXpo(Session session) : base(session)
        {
        }

        public ViewListATCEntityRelatedDCIXpo()
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
