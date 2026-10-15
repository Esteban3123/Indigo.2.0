

-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-04-23
-- Description:	Liquida cuota recuperacion a paciente
-- =============================================

CREATE Procedure [Billing].[SP_LiquidateRecoveryFee]
	@AdmissionCode Varchar(20),
	@RevenueControlDetailId Int,
	@TotalsItemsApplyRecoveryFee Decimal(18, 0),
	@LiquidationType Int,
	@serviceOrderDetailDistributionDetailId Int,
	@ServiceDistributionListXml Xml,
	@ListItemsApplyRecoveryFeeXml Xml,
	@Liquidate Bit = 0
AS
Begin
	Set Nocount On;
	
	Begin Try
		
		--Propiedades
		Declare @ActionResult As Table([StatusResult] Bit, [MessageResult] Varchar(1000))
		Declare @RevenueControlId Int, @PatientCode Varchar(20), @TotalFolio Decimal(18, 0)
		Declare @serviceorderDetailDistributionListId As Table(Id Int Primary Key, GradTotal Decimal(18, 0))
		
		Select Top 1 @RevenueControlId = rc.Id, @PatientCode = rc.PatientCode,
			@TotalFolio = rcd.TotalFolio
			From Billing.RevenueControlDetail rcd With(Nolock)
			Inner Join Billing.RevenueControl rc With(Nolock) On rcd.RevenueControlId = rc.Id
			Where rcd.Id = @RevenueControlDetailId			

		If @LiquidationType = 0 Begin
			--AllItems
			If @Liquidate = 1 BEGIN
				
				Insert Into @serviceorderDetailDistributionListId	
				Select sodd.Id, sodd.GrandTotalSalesPrice
				From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
				Where sodd.RevenueControlDetailId = @RevenueControlDetailId And sodd.ApplyRecoveryFee >= 1
				Order By sodd.GrandTotalSalesPrice Asc
			END
			Else BEGIN
				Insert Into @serviceorderDetailDistributionListId	
				Select sodd.Id, sodd.GrandTotalSalesPrice
				From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
				Where sodd.RevenueControlDetailId = @RevenueControlDetailId And sodd.ApplyRecoveryFee = 2
				Order By sodd.GrandTotalSalesPrice Asc
			END
		End
		Else If @LiquidationType = 1 Begin
			--OnlyOne
			Insert Into @serviceorderDetailDistributionListId	
			Select @serviceOrderDetailDistributionDetailId, 0
		End
		Else If @LiquidationType = 2 Begin
			--MultiSelectItems
			Insert Into @serviceorderDetailDistributionListId
			Select Distinct t.x.value('Id[1]','int'), 0			
			From @ServiceDistributionListXml.nodes('/ServiceDistributionList') t(x)
		End

		Declare @serviceorderDetailDistributionListIdXml As Xml= (Select Id From @serviceorderDetailDistributionListId Order By GradTotal Asc
					for xml path('ServiceorderDetailDistribution'), elements)
					
		Declare @MessageResult As Varchar(50) = ''
		If @Liquidate = 1 Begin
							
			Insert Into @ActionResult
			exec [Billing].[SP_LiquidatePatientShare] @RevenueControlId, 
				@TotalsItemsApplyRecoveryFee, 
				@TotalFolio, 
				@AdmissionCode,
				@serviceorderDetailDistributionListIdXml,
				@ListItemsApplyRecoveryFeeXml

			If (Select Top 1 StatusResult From @ActionResult) = 0 Begin
				Select * From @ActionResult
				Return
			End
			Set @MessageResult = 'Cuota paciente generada correctamente'
		End
		Else Begin
			
			Insert Into @ActionResult
			exec [Billing].[SP_DeletePatientShare] @RevenueControlId, @serviceorderDetailDistributionListIdXml

			Set @MessageResult = 'Cuota paciente eliminada correctamente'
		End
		
		Declare @tmpResult As Table(StatusResult Bit, MessageResult Varchar(100))				
		Insert Into @tmpResult
		exec Billing.SP_UpdateRevenueControlDetailValues @RevenueControlDetailId, NULL

		Select Convert(Bit, 1) As StatusResult, @MessageResult As MessageResult
	End Try
	Begin Catch
		Select Convert(Bit, 0) As StatusResult, ERROR_MESSAGE() As MessageResult
	End Catch
	
End