using DevExpress.Xpo;
using System;

namespace Infrastructure.Data.Xpo.InventoryRepository.Relation
{
    [Persistent(@"MixingStation.CampaignReports")]
    public class MixingStationCampaignReportsXpo : XPLiteObject
    {
        #region Properties

        int fId;
        [Key(true)]
        public int Id
        {
            get { return fId; }
            set { SetPropertyValue<int>("Id", ref fId, value); }
        }

        int fCampaignDetailId;
        public int CampaignDetailId
        {
            get { return fCampaignDetailId; }
            set { SetPropertyValue<int>("CampaignDetailId", ref fCampaignDetailId, value); }
        }

        int fEntityId;
        public int EntityId
        {
            get { return fEntityId; }
            set { SetPropertyValue<int>("EntityId", ref fEntityId, value); }
        }
        
        string fEntityName;
        public string EntityName
        {
            get { return fEntityName; }
            set { SetPropertyValue<string>("EntityName", ref fEntityName, value); }
        }

        #endregion

        #region Builders

        public MixingStationCampaignReportsXpo(Session session) : base(session) { }

        #endregion
    }
}
