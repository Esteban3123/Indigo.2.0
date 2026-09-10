using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
namespace Infrastructure.Data.Xpo.InventoryRepository
{

    public partial class Contract_CareGroup
    {
        public Contract_CareGroup(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
