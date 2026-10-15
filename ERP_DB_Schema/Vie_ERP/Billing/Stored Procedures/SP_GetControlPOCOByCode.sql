
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que devuelve los datos ingreso
-- =============================================

CREATE PROCEDURE [Billing].[SP_GetControlPOCOByCode]
	@admissionCode varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	
	select rc.Id,
		rc.FolioQuantity,
		cg.LiquidationType,
		cg.ContractId,
		rcd.Id as RevenueControlDetailId,
		rcd.[Status] as RevenueControlDetailStatus,
		rcd.FolioOrder,
		i.InvoiceNumber,
		case when c.Code is null then '' else concat(c.Code, ' - ', c.ContractName) end as ContractCodeName
	from Billing.RevenueControl rc with(nolock)
	inner join [Billing].[RevenueControlDetail] rcd with(nolock) on rcd.RevenueControlId = rc.Id
	inner join [Contract].[CareGroup] cg with(nolock) on rcd.CareGroupId = cg.Id
	left join [Contract].[Contract] c with(nolock) on c.Id = cg.ContractId
	left join Billing.Invoice i with(nolock) on i.RevenueControlDetailId = rcd.Id And i.[Status] = 1
	where rc.AdmissionNumber = @admissionCode and rcd.IsMasterAccount <> 3

END