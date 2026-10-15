

-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que incluye dentro de otro servicio
-- =============================================

CREATE PROCEDURE [Billing].[SP_IncludeInOtherService]
	@xmlData xml
AS
BEGIN
	SET NOCOUNT ON;
	
	begin try

		declare @revenueControlDetailId int, @serviceOrderDetailSelected int
		declare @tmpDetails table(ServiceOrderDetailId int primary key)
			
		select 
		@revenueControlDetailId = t.x.value('RevenueControlDetailId[1]','int'),
		@serviceOrderDetailSelected = t.x.value('serviceOrderDetailSelected[1]','int')	
		from @xmlData.nodes('/Main') t(x)

		--- Inserto los detalles
		insert into @tmpDetails(ServiceOrderDetailId)
		select 
		t.x.value('serviceOrderDetailId[1]', 'int')	
		from @xmlData.nodes('/Main/Detail') t(x)

		IF EXISTS(SELECT 1
					FROM Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
					join Billing.RevenueControlDetail rcd WITH(NOLOCK) ON sodd.RevenueControlDetailId = rcd.Id
					join @tmpDetails tmp ON sodd.ServiceOrderDetailId = tmp.ServiceOrderDetailId
					where rcd.Status=2) BEGIN

					select '999' as CodeResult, 'Se esta tratando de afectar un folio Facturado' as MessageResult
					RETURN
		END
		
		update Billing.ServiceOrderDetail set SettlementType = 3, IncludeServiceOrderDetailId = @serviceOrderDetailSelected,
			SubTotalSalesPrice = 0, ThirdPartyDiscount = 0, ThirdPartyDiscountPercentage = 0, TotalSalesPrice = 0, GrandTotalSalesPrice = 0,GrossValue=0,TaxValue=0
		where Id in (select distinct ServiceOrderDetailId from @tmpDetails)

		update Billing.ServiceOrderDetailDistribution set GrandTotalSalesPrice = 0, GrandTotalDiscount = 0, ThirdPartySalesPrice = 0,
			PatientPercentage = 0, SubTotalPatientSalesPrice = 0, RecoveryFeeType = 1, ApplyRecoveryFee = 1	, SubTotalSalesPrice =0,GrandTotalTaxes=0,DeductibleValue=0,InsurerCoveredValue=0	
		where ServiceOrderDetailId in (select distinct ServiceOrderDetailId from @tmpDetails)

		update Billing.ServiceOrderDetailSurgical set TotalSalesPrice = 0
		from Billing.ServiceOrderDetail sod with(nolock)
		where Billing.ServiceOrderDetailSurgical.ServiceOrderDetailId = sod.Id And sod.Presentation = 2 
			And ServiceOrderDetailId in (select distinct ServiceOrderDetailId from @tmpDetails)

		declare @updateResult table(StatusResult bit, MessageResult varchar(max))

		if NOT EXISTS(SELECT 1 from Billing.SettingsBilling where LiquidateMasterAccount =1) BEGIN
			insert into @updateResult
			exec [Billing].[SP_UpdateRevenueControlDetailValues] @revenueControlDetailId, NULL
		END		

		select '0' as CodeResult, '' as MessageResult

	end try
	begin catch
		select '999' as CodeResult, error_message() as MessageResult
	end catch
		
END