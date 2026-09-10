using Domain.Entities;
using System.Collections.Generic;

namespace DistributedService.Report.Models
{
    public class TransferOrderModel
    {
        public string Code { get; set; }
        public int ProductWarehouseId { get; set; }
        public int StockWarehouseId { get; set; }
        public string UserCode { get; set; }
        public int CampaignDetailId { get; set; }
        public string Detail { get; set; }

        public IEnumerable<TransferOrderDetailModel> Details { get; set; }

        public RawMaterialDevolution ToRawMaterialDevolution()
        {
            var details = new TrackableCollection<RawMaterialDevolutionDetail>();

            if (Details != null)
            {
                foreach (var item in Details)
                {
                    details.Add(new RawMaterialDevolutionDetail
                    {
                        CampaignDetailValidationId = item.CampaignDetailValidationId,
                        Quantity = item.Quantity,                        
                    });
                }
            }

            return new RawMaterialDevolution
            {
                Code = this.Code,
                DocumentDate = System.DateTime.Now,
                CampaignDetailId = this.CampaignDetailId,
                Detail = this.Detail,
                RawMaterialDevolutionDetail = details
            };
        }
    }

    public class TransferOrderDetailModel
    {
        public int CampaignDetailValidationId { get; set; }
        public int Quantity { get; set; }
    }
}