

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite incluir uno o varios ítems de orden de servicio (procedimientos, medicamentos, insumos) dentro de otro servicio ya existente en el proceso de facturación. Recibe por XML el identificador del folio de control de ingresos y los detalles de orden seleccionados, y antes de realizar cambios valida que ninguno de esos ítems pertenezca a un folio ya facturado (estado 2), evitando afectar documentos contables cerrados. Si la validación pasa, anula los valores económicos de los ítems absorbidos en ServiceOrderDetail y su distribución financiera en ServiceOrderDetailDistribution (precios, descuentos, impuestos, cuotas moderadoras y copagos se llevan a cero), los marca con tipo de liquidación 3 y los vincula al servicio destino. Finalmente, si no está activa la liquidación de cuenta maestra, recalcula los totales del folio de control de ingresos invocando SP_UpdateRevenueControlDetailValues.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_IncludeInOtherService';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_IncludeInOtherService';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Marca un conjunto de ítems de orden de servicio como incluidos dentro de otro servicio destino, anulando sus valores facturables y de distribución, y recalcula el folio cuando no se liquida cuenta maestra.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_IncludeInOtherService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener un nodo /Main con RevenueControlDetailId y serviceOrderDetailSelected; El XML debe traer uno o más nodos /Main/Detail con serviceOrderDetailId de los detalles a incluir; Los ServiceOrderDetail referenciados deben existir en Billing.ServiceOrderDetail y su distribución en Billing.ServiceOrderDetailDistribution; Ninguno de los detalles puede pertenecer a un RevenueControlDetail con Status=2 (folio facturado)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_IncludeInOtherService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca modifica detalles cuya RevenueControlDetail esté en Status=2 (folio ya facturado): aborta con código 999 antes de cualquier UPDATE; Al incluir un detalle dentro de otro servicio, todos sus valores monetarios (subtotales, totales, descuentos, impuestos, deducibles, valor cubierto por asegurador) quedan en cero; Al marcar inclusión, ServiceOrderDetail.SettlementType se fija en 3 y se referencia el ServiceOrderDetail destino en IncludeServiceOrderDetailId; En la distribución, RecoveryFeeType y ApplyRecoveryFee se reinician a 1 al incluir el detalle en otro servicio; El recálculo del RevenueControlDetail solo se dispara cuando la facturación NO está configurada para liquidar cuenta maestra (LiquidateMasterAccount<>1 o ausente); Cualquier excepción se captura y se devuelve como CodeResult=''999'' con el mensaje del error', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_IncludeInOtherService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio facturado; orden de servicio; distribución de venta; tercero pagador; copago/cuota del paciente; servicio quirúrgico; control de ingresos (RevenueControl); liquidación de cuenta maestra', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_IncludeInOtherService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.ServiceOrderDetail: Para cada detalle recibido en el XML: SettlementType=3, IncludeServiceOrderDetailId=serviceOrderDetailSelected y SubTotalSalesPrice, ThirdPartyDiscount, ThirdPartyDiscountPercentage, TotalSalesPrice, GrandTotalSalesPrice, GrossValue, TaxValue se ponen en 0; [UPDATE] Billing.ServiceOrderDetailDistribution: Para los ServiceOrderDetailId recibidos: pone en 0 GrandTotalSalesPrice, GrandTotalDiscount, ThirdPartySalesPrice, PatientPercentage, SubTotalPatientSalesPrice, SubTotalSalesPrice, GrandTotalTaxes, DeductibleValue, InsurerCoveredValue y reinicia RecoveryFeeType=1, ApplyRecoveryFee=1; [UPDATE] Billing.ServiceOrderDetailSurgical: Cuando el ServiceOrderDetail asociado tiene Presentation=2, fija TotalSalesPrice=0 para ese detalle; [UPDATE] Billing.RevenueControlDetail: Si en SettingsBilling no hay registro con LiquidateMasterAccount=1, invoca SP_UpdateRevenueControlDetailValues con el RevenueControlDetailId para recalcular sus totales; [RETURN_RESULT] RESULT: Si algún detalle pertenece a un folio con Status=2 retorna CodeResult=''999'' y MessageResult=''Se esta tratando de afectar un folio Facturado''; en caso de éxito retorna CodeResult=''0'' con mensaje vacío; ante excepción retorna CodeResult=''999'' con error_message()', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_IncludeInOtherService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un detalle de los enviados cuya distribución pertenece a un RevenueControlDetail con Status=2 (folio facturado) → Retorna CodeResult=''999'' con mensaje ''Se esta tratando de afectar un folio Facturado'' y aborta sin aplicar cambios else Procede a poner en cero los valores y marcar los detalles como incluidos en otro servicio; si No existe registro en Billing.SettingsBilling con LiquidateMasterAccount=1 → Ejecuta Billing.SP_UpdateRevenueControlDetailValues sobre el RevenueControlDetail recibido para recalcular sus valores else Omite el recálculo del RevenueControlDetail; si ServiceOrderDetail.Presentation = 2 (quirúrgico) y el detalle está en la lista enviada → Actualiza Billing.ServiceOrderDetailSurgical.TotalSalesPrice = 0 para ese detalle', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_IncludeInOtherService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_UpdateRevenueControlDetailValues', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_IncludeInOtherService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetailDistribution; Billing.RevenueControlDetail; Billing.ServiceOrderDetail; Billing.SettingsBilling', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_IncludeInOtherService';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_IncludeInOtherService';
-- GO
