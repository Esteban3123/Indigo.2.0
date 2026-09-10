
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina o anula la cuota de recuperación (cuota moderadora o copago) asignada al paciente en uno o varios ítems de distribución financiera de órdenes de servicio. Recibe una lista de identificadores de distribuciones en formato XML y, para cada una, revierte el valor cobrado al paciente descontándolo del tope acumulado correspondiente (cuota moderadora o copago) en el control de ingresos del ingreso hospitalario. Luego ajusta la distribución financiera del ítem para que el 100% del valor quede a cargo del tercero pagador (asegurador) y el paciente quede en cero. Se usa en facturación cuando se necesita quitar o corregir el cobro de cuota de recuperación previamente aplicado a un paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_DeletePatientShare';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_DeletePatientShare';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Revierte la cuota de recuperación (moderadora o copago) asignada al paciente en una o varias distribuciones de orden de servicio, trasladando el valor total al tercero pagador y ajustando los topes del control de ingresos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El RevenueControl identificado debe existir para que los UPDATE de topes tengan efecto; Cada Id incluido en el XML debe corresponder a un registro existente en Billing.ServiceOrderDetailDistribution; Los valores SubTotalPatientSalesPrice y GrandTotalSalesPrice deben estar previamente calculados en la distribución antes de invocar el borrado; El XML debe seguir la estructura /ServiceorderDetailDistribution con nodo Id', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'RecoveryFeeType=2 corresponde a Cuota Moderadora; cualquier otro valor se trata como Copago; Tras la eliminación de la cuota, la distribución queda 100% a cargo del tercero pagador (ThirdPartyPercentage=100, PatientPercentage=0); El valor a cargo del paciente queda en cero (SubTotalPatientSalesPrice=0) y ThirdPartySalesPrice toma el GrandTotalSalesPrice; RecoveryFeeType se restablece a 1 y ApplyRecoveryFee=1 al eliminar la cuota; Los topes acumulados del RevenueControl se decrementan en el monto que tenía asignado el paciente antes del borrado; Procesa cada distribución del XML de forma secuencial mediante cursor; En caso de excepción retorna StatusResult=0 y el mensaje de error; en éxito retorna StatusResult=1', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuota de recuperación; Cuota moderadora; Copago; Paciente; Tercero pagador; Distribución de orden de servicio; Control de ingresos (RevenueControl)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.RevenueControl: Cuando RecoveryFeeType=2 (Cuota Moderadora): TopEventFeeModerator = TopEventFeeModerator - SubTotalPatientSalesPrice del registro de distribución, para el RevenueControl indicado; [UPDATE] Billing.RevenueControl: Cuando RecoveryFeeType<>2 (Copago): TopEventCopay = TopEventCopay - SubTotalPatientSalesPrice del registro de distribución, para el RevenueControl indicado; [UPDATE] Billing.ServiceOrderDetailDistribution: Para cada Id procesado: reinicia la distribución asignando RecoveryFeeType=1, ApplyRecoveryFee=1, PatientPercentage=0, ThirdPartyPercentage=100, ThirdPartySalesPrice=GrandTotalSalesPrice y SubTotalPatientSalesPrice=0; [RETURN_RESULT] (resultset): Devuelve StatusResult=1 y MessageResult vacío al completar; si el TRY falla, devuelve StatusResult=0 y ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RecoveryFeeType = 2 (Cuota Moderadora) → Resta SubTotalPatientSalesPrice de Billing.RevenueControl.TopEventFeeModerator else Resta SubTotalPatientSalesPrice de Billing.RevenueControl.TopEventCopay (caso Copago)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetailDistribution', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_DeletePatientShare';
-- GO
