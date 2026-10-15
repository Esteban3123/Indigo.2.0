
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-04-23
-- Description:	Elimina cuota recuperacion a paciente
-- =============================================

CREATE Procedure [Billing].[SP_DeletePatientShare]
	@RevenueControlId Int,
	@ServiceorderDetailDistributionListIdXml Xml
AS
Begin
	Set Nocount On;
		
	Begin Try
		
		Declare @ServiceorderDetailDistributionListId As Table(Id Int Primary key)

		Insert Into @ServiceorderDetailDistributionListId
		Select t.x.value('Id[1]','int')			
		From @ServiceorderDetailDistributionListIdXml.nodes('/ServiceorderDetailDistribution') t(x)

		Declare @ServiceOrderDetailDistributionId Int,
			@RecoveryFeeType Tinyint, @SubTotalPatientSalesPrice Decimal(18, 0),
			@GrandTotalSalesPrice Decimal(18, 0)
		Declare Cursor_ServiceOrderDetailDistribution Cursor For
		Select Id From @ServiceorderDetailDistributionListId
		Open Cursor_ServiceOrderDetailDistribution

		Fetch Next From Cursor_ServiceOrderDetailDistribution Into @ServiceOrderDetailDistributionId
		While @@Fetch_Status = 0
		Begin

			Select @RecoveryFeeType = RecoveryFeeType, @SubTotalPatientSalesPrice = SubTotalPatientSalesPrice,
				@GrandTotalSalesPrice = GrandTotalSalesPrice
			From Billing.ServiceOrderDetailDistribution 
				With(Nolock) Where Id = @ServiceOrderDetailDistributionId

			If @RecoveryFeeType = 2 --Cuota Moderadora
				Update Billing.RevenueControl Set TopEventFeeModerator = TopEventFeeModerator - @SubTotalPatientSalesPrice
				Where Id = @RevenueControlId
			Else --Copago
				Update Billing.RevenueControl Set TopEventCopay = TopEventCopay - @SubTotalPatientSalesPrice
				Where Id = @RevenueControlId				

			Update Billing.ServiceOrderDetailDistribution Set RecoveryFeeType = 1,
				ApplyRecoveryFee = 1,
				PatientPercentage = 0,
				ThirdPartySalesPrice = @GrandTotalSalesPrice,
				ThirdPartyPercentage = 100,
				SubTotalPatientSalesPrice = 0
			Where Id = @ServiceOrderDetailDistributionId

			Fetch Next From Cursor_ServiceOrderDetailDistribution Into @ServiceOrderDetailDistributionId
		End
		Close Cursor_ServiceOrderDetailDistribution
		Deallocate Cursor_ServiceOrderDetailDistribution
		
		Select Convert(Bit, 1) As [StatusResult], '' As [MessageResult]
	End Try
	Begin Catch
		Select Convert(Bit, 0) As [StatusResult], ERROR_MESSAGE() As [MessageResult]
	End Catch
	
End