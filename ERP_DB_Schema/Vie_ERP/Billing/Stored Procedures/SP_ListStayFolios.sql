
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que devuelve Lista los folios en los que se encuentra distribuida una estancias
-- =============================================

CREATE PROCEDURE [Billing].[SP_ListStayFolios]
	@StayId int,
	@admissionNumber varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	
	select rcd.Id as IdFolio,
		rcd.FolioOrder,
		sodd.GrandTotalSalesPrice as ValueFolio
	from Billing.ServiceOrderDetail sod with(nolock)
	inner join Billing.ServiceOrderDetailDistribution sodd with(nolock) on sod.Id = sodd.ServiceOrderDetailId
	inner join Billing.RevenueControlDetail rcd with(nolock) on sodd.RevenueControlDetailId = rcd.Id
	inner join Billing.RevenueControl rc with(nolock) on rcd.RevenueControlId = rc.Id
	where sod.HospitalStayId is not null And sod.HospitalStayId = @StayId
		And rc.AdmissionNumber = @admissionNumber

END