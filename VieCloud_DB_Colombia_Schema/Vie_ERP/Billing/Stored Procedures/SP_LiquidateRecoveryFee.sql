

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Liquida o revierte la cuota de recuperación a cargo del paciente dentro del módulo de facturación. Según el tipo de liquidación indicado (todos los ítems, uno solo o una selección múltiple), identifica los registros de distribución financiera de órdenes de servicio que aplican cuota de recuperación y los procesa contra el control de ingresos del ingreso (admisión) correspondiente. Si se indica liquidar, invoca SP_LiquidatePatientShare para generar la cuota del paciente; si se indica revertir, invoca SP_DeletePatientShare para eliminarla; en ambos casos finaliza actualizando los totales del detalle de control de ingresos mediante SP_UpdateRevenueControlDetailValues.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidateRecoveryFee';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidateRecoveryFee';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Liquida o revierte la cuota de recuperación del paciente sobre los ítems facturables seleccionados de un detalle de control de ingresos, delegando el cálculo o eliminación y refrescando los totales del detalle.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateRecoveryFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un RevenueControlDetail con su RevenueControl asociado para el Id recibido (se obtienen RevenueControlId, PatientCode y TotalFolio).; Para liquidar todos los ítems (LiquidationType=0) deben existir filas en ServiceOrderDetailDistribution con ApplyRecoveryFee>=1 (al liquidar) o =2 (al revertir) para el detalle.; Para LiquidationType=2 el XML ServiceDistributionListXml debe contener nodos /ServiceDistributionList con Id.; Para LiquidationType=1 se requiere un serviceOrderDetailDistributionDetailId válido.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateRecoveryFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La selección de ítems para liquidación masiva siempre se hace en orden ascendente de GrandTotalSalesPrice.; ApplyRecoveryFee=2 identifica ítems que ya tienen cuota de recuperación liquidada (criterio usado para reversión).; Tras cualquier operación exitosa (liquidar o revertir) se refrescan los totales del RevenueControlDetail.; Cualquier excepción se captura y se devuelve como resultado con StatusResult=0 sin propagar el error.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateRecoveryFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuota de recuperación; Cuota del paciente; Control de ingresos; Folio de facturación; Distribución de orden de servicio; Admisión del paciente; Liquidación; Reversión de liquidación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateRecoveryFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.RevenueControlDetail: Siempre al final invoca SP_UpdateRevenueControlDetailValues para recalcular/actualizar los totales del detalle de control de ingresos afectado.; [UPDATE] Billing.ServiceOrderDetailDistribution: Cuando @Liquidate=1 invoca SP_LiquidatePatientShare para generar la cuota del paciente sobre los ítems seleccionados (orden ascendente por GrandTotalSalesPrice).; [DELETE] Billing.ServiceOrderDetailDistribution: Cuando @Liquidate=0 invoca SP_DeletePatientShare para eliminar la cuota del paciente previamente liquidada sobre los ítems seleccionados.; [RETURN_RESULT] ResultSet: Si SP_LiquidatePatientShare retorna StatusResult=0, devuelve el resultado de error y termina sin actualizar totales ni continuar.; [RETURN_RESULT] ResultSet: En éxito retorna StatusResult=1 con mensaje ''Cuota paciente generada correctamente'' (liquidar) o ''Cuota paciente eliminada correctamente'' (revertir).; [RETURN_RESULT] ResultSet: En el CATCH retorna StatusResult=0 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateRecoveryFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @LiquidationType = 0 (AllItems) y @Liquidate = 1 → Selecciona todos los ServiceOrderDetailDistribution del detalle con ApplyRecoveryFee >= 1, ordenados ascendentemente por GrandTotalSalesPrice.; si @LiquidationType = 0 (AllItems) y @Liquidate = 0 → Selecciona los ServiceOrderDetailDistribution del detalle con ApplyRecoveryFee = 2 (los previamente liquidados) para revertir.; si @LiquidationType = 1 (OnlyOne) → Procesa únicamente el ítem identificado por @serviceOrderDetailDistributionDetailId.; si @LiquidationType = 2 (MultiSelectItems) → Procesa los Ids extraídos del XML @ServiceDistributionListXml en /ServiceDistributionList.; si @Liquidate = 1 → Ejecuta SP_LiquidatePatientShare; si su StatusResult es 0, devuelve el error y termina. else Ejecuta SP_DeletePatientShare para eliminar la cuota del paciente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateRecoveryFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_LiquidatePatientShare; Billing.SP_DeletePatientShare; Billing.SP_UpdateRevenueControlDetailValues', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateRecoveryFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.RevenueControl; Billing.ServiceOrderDetailDistribution', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateRecoveryFee';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidateRecoveryFee';
-- GO
