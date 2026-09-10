//'*************************************************************
//' Assembly         : Infraestructure.Data.Xpo.InventoryRepository
//' Author           : Judy Andrea Díaz Reyes
//' Created          : 27-05-2019
//'
//' Copyright        : (c) . All rights reserved.
//'*************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Infrastructure.CrossCutting.Xpo.Base;

namespace Infrastructure.Data.Xpo.InventoryRepository
{
    [Persistent("Inventory.ShelfType")]
    public class InventoryShelfTypeXpo : XPLiteObject
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

        string fDescription;
        [Size(100)]
        [Persistent("Description")]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>("Description", ref fDescription, value); }
        }

        decimal fLarge;
        [Persistent("Large")]
        public decimal Large
		{
            get { return fLarge; }
            set { SetPropertyValue<decimal>("Large", ref fLarge, value); }
        }

		decimal fWide;
		[Persistent("Wide")]
		public decimal Wide
		{
			get { return fWide; }
			set { SetPropertyValue<decimal>("Wide", ref fWide, value); }
		}

		decimal fDeep;
		[Persistent("Deep")]
		public decimal Deep
		{
			get { return fDeep; }
			set { SetPropertyValue<decimal>("Deep", ref fDeep, value); }
		}

		decimal fPartitionXDeep;
		[Persistent("PartitionXDeep")]
		public decimal PartitionXDeep
		{
			get { return fPartitionXDeep; }
			set { SetPropertyValue<decimal>("PartitionXDeep", ref fPartitionXDeep, value); }
		}

		decimal fPartitions;
		[Persistent("Partitions")]
		public decimal Partitions
		{
			get { return fPartitions; }
			set { SetPropertyValue<decimal>("Partitions", ref fPartitions, value); }
		}

		decimal fLocationXPartition;
		[Persistent("LocationXPartition")]
		public decimal LocationXPartition
		{
			get { return fLocationXPartition; }
			set { SetPropertyValue<decimal>("LocationXPartition", ref fLocationXPartition, value); }
		}

		byte fState;
        [Persistent("State")]
        public byte Status
        {
            get { return fState; }
            set { SetPropertyValue<byte>("State", ref fState, value); }
        }
        #endregion

        #region Builders

        public InventoryShelfTypeXpo(Session session) : base(session)
        {
        }

        public InventoryShelfTypeXpo()
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
