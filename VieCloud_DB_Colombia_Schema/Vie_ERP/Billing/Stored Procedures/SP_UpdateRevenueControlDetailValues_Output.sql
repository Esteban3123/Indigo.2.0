-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2015-09-18
-- Description:	Recalcula los valores de un folio
-- =============================================
CREATE PROCEDURE [Billing].[SP_UpdateRevenueControlDetailValues_Output]
	-- Add the parameters for the stored procedure here
	@REVENUECONTROLDETAILID AS INT,
	@OperativeUnitId AS INT = NULL,
	@StatusResult Bit Output,
	@MessageResult Varchar(MAX) Output
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @REVENUECONTROLID AS INT
	DECLARE @ParticularHealthAdministratorId INT

	--===VALORES A ACTUALIZAR DEL FOLIO
	DECLARE @ValueCopay AS DECIMAL(20,2) = 0
	DECLARE @ValueFeeModerator AS DECIMAL(20,2) = 0
	DECLARE @ValueBond AS DECIMAL(20,2) = 0
	DECLARE @TotalPatientSalesPrice AS DECIMAL(20,2) = 0
	DECLARE @TotalPatientWithDiscount AS DECIMAL(20,2) = 0
	DECLARE @TotalFolio AS DECIMAL(20,2) = 0
	DECLARE @ThirdPartySalesPriceTotal AS DECIMAL(20,2) = 0
	DECLARE @Countfolios AS INT = 0

	--===VALORES A ACTUALIZAR DE REVENUECONTROL
	DECLARE @RoundingTypeRecoveryFeeType AS INT = 0
	DECLARE @TopEventFeeRecovery AS DECIMAL(20,2) = 0
	DECLARE @TopEventCopay AS DECIMAL(20,2) = 0
	DECLARE @TopEventFeeModerator AS DECIMAL(20,2) = 0

	--===VALORES DE LA CONSULTA
	DECLARE @GRANDTOTALSALESPRICE AS DECIMAL(20,2) = 0
	DECLARE @RECOVERYFEETYPE AS TINYINT = 0
	DECLARE @SUBTOTALPATIENTSALESPRICE AS DECIMAL(20,2) = 0
	DECLARE @PATIENTDISCOUNTPERCENTAGE AS DECIMAL(5,2) = 0

	BEGIN TRY
		IF NOT EXISTS (SELECT 1 FROM Billing.RevenueControlDetail WHERE Id = @RevenueControlDetailId AND Status IN (1)) 
		BEGIN
			SELECT @MessageResult = CONCAT('El folio se encuentra ', CASE Status
																			WHEN 2 THEN 'Facturado'
																			WHEN 3 THEN 'Bloqueado'
																			WHEN 4 THEN 'Anulado'
																			WHEN 5 THEN 'Reconocimiento Ingresos'
																			WHEN 6 THEN 'Factura Asociada'
																		END)
			FROM Billing.RevenueControlDetail
			WHERE Id = @RevenueControlDetailId

			SET @StatusResult = 0
			SET @MessageResult = ISNULL(@MessageResult, 'El folio no existe')
			RETURN
		END
		
		IF @OperativeUnitId IS NULL
		BEGIN
			-- Obtenemos la primera unidad operativa asociada a las ordenes de servicio
			SELECT TOP 1 @OperativeUnitId = so.OperatingUnitId
			FROM Billing.RevenueControlDetail rcd 
			JOIN Billing.ServiceOrderDetailDistribution sodd  ON rcd.Id = sodd.RevenueControlDetailId
			JOIN Billing.ServiceOrderDetail sod  ON sodd.ServiceOrderDetailId = sod.Id
			JOIN Billing.ServiceOrder so  ON sod.ServiceOrderId = so.Id
			WHERE rcd.Id = @RevenueControlDetailId
		END

		--obtengo el valor de redondeo de la cuota de paciente
		SELECT	@RoundingTypeRecoveryFeeType = sb.RoundingTypeRecoveryFeeType,
				@ParticularHealthAdministratorId = sb.ParticularHealthAdministratorId
		FROM Billing.SettingsBilling sb 
		WHERE sb.IdOperatingUnit = @OperativeUnitId

		--- Dejo en cero los valores de los productos que estan incluidos al 100%
		UPDATE sodd
			SET GrandTotalSalesPrice = 0, 
				GrandTotalDiscount = 0, 
				ThirdPartySalesPrice = 0, 
				SubTotalPatientSalesPrice = 0
		FROM Billing.ServiceOrderDetailDistribution sodd
		INNER JOIN Billing.ServiceOrderDetail sod  ON sod.Id = sodd.ServiceOrderDetailId
		WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID
			AND sod.SettlementType = 3
			AND (
				sodd.GrandTotalSalesPrice <> 0
				OR sodd.GrandTotalDiscount <> 0
				OR sodd.ThirdPartySalesPrice <> 0
				OR sodd.SubTotalPatientSalesPrice <> 0
			)

		UPDATE sod
			SET SubTotalSalesPrice = 0, 
				ThirdPartyDiscount = 0, 
				TotalSalesPrice = 0, 
				GrandTotalSalesPrice = 0
		FROM Billing.ServiceOrderDetailDistribution sodd 
		INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = sodd.ServiceOrderDetailId
		WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID
			AND sod.SettlementType = 3
			AND (
				sod.SubTotalSalesPrice <> 0
				OR sod.ThirdPartyDiscount <> 0
				OR sod.TotalSalesPrice <> 0
				OR sod.GrandTotalSalesPrice <> 0
			)

		
		UPDATE sodd
		SET
			sodd.GrandTotalSalesPrice = sod.GrandTotalSalesPrice,
			sodd.ThirdPartySalesPrice = ROUND(sod.GrandTotalSalesPrice * (sodd.ThirdPartyPercentage / 100), 2),
			sodd.SubTotalPatientSalesPrice = sod.GrandTotalSalesPrice - ROUND(sod.GrandTotalSalesPrice * (sodd.ThirdPartyPercentage / 100), 2)
		FROM Billing.ServiceOrderDetailDistribution sodd
			INNER JOIN Billing.ServiceOrderDetail sod  ON sod.Id = sodd.ServiceOrderDetailId
		WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID
			AND sod.SettlementType = 1
			AND sod.IncludeServiceOrderDetailId IS NULL
			AND sod.IsPackage = 0
			AND sod.PackageServiceOrderDetailId IS NULL
			AND sod.GrandTotalSalesPrice <> sodd.GrandTotalSalesPrice

		UPDATE Billing.ServiceOrderDetailDistribution 
			SET ThirdPartySalesPrice = GrandTotalSalesPrice - SubTotalPatientSalesPrice 
		WHERE RevenueControlDetailId = @REVENUECONTROLDETAILID
			AND (ThirdPartySalesPrice + SubTotalPatientSalesPrice) <> GrandTotalSalesPrice

		SELECT @PatientDiscountPercentage = COALESCE(PatientDiscountPercentage,0),
				@REVENUECONTROLID = RevenueControlId 
		FROM Billing.RevenueControlDetail 
		where Id = @REVENUECONTROLDETAILID

		SELECT @TotalPatientSalesPrice = COALESCE(SUM(sodd.SubTotalPatientSalesPrice),0),
				@TotalPatientWithDiscount = COALESCE(SUM(sodd.SubTotalPatientSalesPrice*(1-@PatientDiscountPercentage/100)),0),
				@TotalFolio = COALESCE(SUM(sodd.GrandTotalSalesPrice),0),
				@ThirdPartySalesPriceTotal = COALESCE(SUM(sodd.ThirdPartySalesPrice),0),
				@ValueFeeModerator = COALESCE(SUM(CASE WHEN sodd.RecoveryFeeType = 2 THEN sodd.SubTotalPatientSalesPrice ELSE 0 END), 0),
				@ValueCopay = COALESCE(SUM(CASE WHEN sodd.RecoveryFeeType = 3 THEN sodd.SubTotalPatientSalesPrice ELSE 0 END), 0),
				@ValueBond = COALESCE(SUM(CASE WHEN sodd.RecoveryFeeType = 4 THEN sodd.SubTotalPatientSalesPrice ELSE 0 END), 0)
		FROM Billing.ServiceOrderDetailDistribution sodd 
		INNER JOIN Billing.ServiceOrderDetail sod  ON sodd.ServiceOrderDetailId = sod.Id
		WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID
			AND sod.IsDelete = 0

		 --se actualizan los valores de copago y cuota moderadora del folio
		DECLARE @diferenciaRedondeo DECIMAL(20,2) = ROUND(@TotalPatientWithDiscount, CASE @RoundingTypeRecoveryFeeType
						WHEN 10 THEN -1
						WHEN 100 THEN -2
						WHEN 1000 THEN -3
						ELSE 2
					END) - @TotalPatientWithDiscount
		DECLARE @RedondeoValorPacienteConDescuento DECIMAL(20,2) = ROUND(@TotalPatientWithDiscount, CASE @RoundingTypeRecoveryFeeType
						WHEN 10 THEN -1
						WHEN 100 THEN -2
						WHEN 1000 THEN -3
						ELSE 2
					END)
		DECLARE @RedondeoValorPacienteSinDescuento DECIMAL(20,2) = ROUND(@TotalPatientSalesPrice, CASE @RoundingTypeRecoveryFeeType
						WHEN 10 THEN -1
						WHEN 100 THEN -2
						WHEN 1000 THEN -3
						ELSE 2
					END)

		IF @TotalPatientSalesPrice > 0
		BEGIN
			IF (@TotalFolio - @RedondeoValorPacienteConDescuento) <> @ThirdPartySalesPriceTotal
			BEGIN
				--buscamos el detalle que tenga aplicado valor a paciente con este valor mas alto
				DECLARE @valueEntity DECIMAL(20,2),
						@PatientValue Decimal(20, 2),
						@IdDistribution INT

				SELECT TOP 1 @IdDistribution = sodd.Id
					, @valueEntity = sodd.ThirdPartySalesPrice
					, @PatientValue = sodd.SubTotalPatientSalesPrice
				FROM Billing.ServiceOrderDetailDistribution sodd 
				INNER JOIN Billing.ServiceOrderDetail sod  ON sodd.ServiceOrderDetailId = sod.Id
				WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID
					AND sodd.ApplyRecoveryFee = 2
					AND sod.IsDelete = 0
				ORDER BY sodd.SubTotalPatientSalesPrice DESC
				
				SET @valueEntity = @valueEntity - @diferenciaRedondeo

				UPDATE Billing.ServiceOrderDetailDistribution 
					SET ThirdPartySalesPrice=@valueEntity 
						, SubTotalPatientSalesPrice = @PatientValue + @diferenciaRedondeo						
				WHERE Id = @IdDistribution
			END
		END

		UPDATE rcd
			SET rcd.TotalPatientSalesPrice = iif(rcd.IsMasterAccount<> 0,0,@RedondeoValorPacienteSinDescuento) ,
				rcd.TotalPatientWithDiscount =iif( rcd.IsMasterAccount <> 0,0, @RedondeoValorPacienteConDescuento ) ,
				rcd.TotalFolio = @TotalFolio,
				rcd.ValueFeeModerator = @ValueFeeModerator,
				rcd.ValueCopay = @ValueCopay, 
				rcd.ValueVoucher = @ValueBond,
				rcd.HealthAdministratorId = CASE
											WHEN rcd.FolioType = 3 AND rcd.HealthAdministratorId IS NULL THEN @ParticularHealthAdministratorId
											ELSE rcd.HealthAdministratorId
										END
		FROM Billing.RevenueControlDetail rcd
		JOIN Contract.CareGroup cg  ON rcd.CareGroupId = cg.Id
		WHERE rcd.Id = @REVENUECONTROLDETAILID

		SELECT	@TopEventFeeModerator = ISNULL(SUM(rcd.ValueFeeModerator), 0),
				@TopEventCopay = ISNULL(SUM(rcd.ValueCopay), 0),
				@TopEventFeeRecovery = ISNULL(SUM(rcd.ValueFeeModerator + rcd.ValueCopay), 0)
		FROM Billing.RevenueControlDetail rcd 
		WHERE rcd.RevenueControlId = @REVENUECONTROLID
			AND rcd.Id <> @REVENUECONTROLDETAILID
		
		UPDATE Billing.RevenueControl 
			SET TopEventFeeModerator = (@TopEventFeeModerator+@ValueFeeModerator), 
				TopEventCopay = (@TopEventCopay+@ValueCopay),
				TopEventFeeRecovery = (@TopEventFeeRecovery+@ValueFeeModerator+@ValueCopay) 
		WHERE Id = @REVENUECONTROLID

		SET @StatusResult = 1
		SET @MessageResult = 'Los valores del folio se calcularon exitosamente'

	END TRY
	BEGIN CATCH
		SELECT	@StatusResult = 0,
				@MessageResult = CONCAT('Error recalculando los valores del folio: ',  ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recalcula y actualiza los valores monetarios de un folio de control de ingresos (cuota moderadora, copago, bono, valor del paciente con y sin descuento, valor del tercero pagador y total del folio). Opera sobre el detalle de distribución financiera de las órdenes de servicio asociadas al folio, ajustando los montos según el tipo de liquidación, porcentajes de cobertura del tercero y reglas de redondeo configuradas por unidad operativa. Solo procesa folios en estado ''Pendiente'' (estado 1); si el folio está facturado, bloqueado, anulado u otro estado, retorna un mensaje de error indicando la razón. Se utiliza en el cierre y ajuste de folios de facturación antes de su emisión formal, garantizando que los totales del folio sean consistentes con los ítems de servicio facturados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateRevenueControlDetailValues_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateRevenueControlDetailValues_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recalcula y normaliza los valores monetarios de un folio de facturación (totales paciente, tercero, copago, cuota moderadora, bono y topes de evento), aplicando redondeo configurado y manteniendo la consistencia entre detalle y distribución de las órdenes de servicio.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el folio (RevenueControlDetail) con el Id recibido y estar en Status=1 (activo); Debe existir configuración en Billing.SettingsBilling para la unidad operativa (propia o derivada de la primera orden de servicio del folio) para obtener RoundingTypeRecoveryFeeType y ParticularHealthAdministratorId; Las distribuciones (ServiceOrderDetailDistribution) y los detalles (ServiceOrderDetail) del folio deben existir para poder calcular totales; Los ítems no deben estar marcados como eliminados (sod.IsDelete = 0) para sumar a los totales', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan folios con Status=1 (activos); cualquier otro estado aborta la operación devolviendo mensaje específico; La suma ThirdPartySalesPrice + SubTotalPatientSalesPrice de cada distribución siempre se fuerza a igualar GrandTotalSalesPrice; Los ítems con SettlementType=3 (incluidos 100%) quedan con todos sus valores de venta y distribución en cero; Los ítems con SettlementType=1 no incluidos, no paquetes ni miembros de paquete, mantienen su distribución alineada al total del detalle aplicando ThirdPartyPercentage; ValueFeeModerator agrupa solo distribuciones con RecoveryFeeType=2; ValueCopay con RecoveryFeeType=3; ValueVoucher (bono) con RecoveryFeeType=4; En cuenta maestra (IsMasterAccount<>0) los totales al paciente del folio quedan en cero; Los topes del RevenueControl (TopEventFeeModerator/Copay/FeeRecovery) se recalculan sumando los demás folios del mismo control más los valores recién calculados del folio actual; La diferencia por redondeo se traslada exclusivamente a una distribución con ApplyRecoveryFee=2, preservando GrandTotalSalesPrice; Folios tipo 3 (particular) sin administradora reciben automáticamente la administradora particular configurada en la unidad operativa; Errores en cualquier paso devuelven StatusResult=0 con mensaje que incluye ERROR_MESSAGE y ERROR_LINE; no se eleva la excepción', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio de facturación; control de ingresos; copago; cuota moderadora; bono/voucher; cuota de recuperación; descuento al paciente; tercero pagador; administradora de salud (EPS/aseguradora); unidad operativa; tipo de liquidación; paquete de servicios; cuenta maestra; tope de evento; redondeo de cuota de paciente; particular', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El folio (RevenueControlDetail) no existe o su Status no es 1 (activo) → Retorna StatusResult=0 con mensaje según el estado: 2=Facturado, 3=Bloqueado, 4=Anulado, 5=Reconocimiento Ingresos, 6=Factura Asociada; o ''El folio no existe'' si no hay registro else Continúa con el recálculo; si @OperativeUnitId IS NULL → Toma la primera OperatingUnitId de las órdenes de servicio asociadas al folio vía ServiceOrderDetailDistribution → ServiceOrderDetail → ServiceOrder; si ServiceOrderDetail.SettlementType = 3 (incluido al 100%) → Pone en cero GrandTotalSalesPrice, GrandTotalDiscount, ThirdPartySalesPrice y SubTotalPatientSalesPrice del distribution; y SubTotalSalesPrice, ThirdPartyDiscount, TotalSalesPrice y GrandTotalSalesPrice del detalle; si SettlementType=1 e ítem no incluido, no paquete, ni miembro de paquete, y sod.GrandTotalSalesPrice <> sodd.GrandTotalSalesPrice → Sincroniza el distribution con el total del detalle, recalculando ThirdPartySalesPrice = GrandTotal * ThirdPartyPercentage/100 y SubTotalPatientSalesPrice como el remanente; si (ThirdPartySalesPrice + SubTotalPatientSalesPrice) <> GrandTotalSalesPrice en una distribución del folio → Recalcula ThirdPartySalesPrice = GrandTotalSalesPrice - SubTotalPatientSalesPrice para cuadrar el reparto; si @TotalPatientSalesPrice > 0 y (TotalFolio - valor paciente con descuento redondeado) <> total tercero → Selecciona la distribución con ApplyRecoveryFee=2 de mayor SubTotalPatientSalesPrice y traslada la diferencia de redondeo entre tercero y paciente; si rcd.IsMasterAccount <> 0 → Fija TotalPatientSalesPrice y TotalPatientWithDiscount del folio en 0 (no hay cobro al paciente en cuenta maestra) else Usa los valores redondeados @RedondeoValorPacienteSinDescuento y @RedondeoValorPacienteConDescuento; si rcd.FolioType = 3 y rcd.HealthAdministratorId IS NULL → Asigna HealthAdministratorId = ParticularHealthAdministratorId tomado de SettingsBilling de la unidad operativa else Conserva el HealthAdministratorId existente; si RoundingTypeRecoveryFeeType IN (10,100,1000) → Redondea valores de paciente a -1, -2 o -3 decimales (decenas, centenas, miles) else Redondea a 2 decimales', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Billing.ServiceOrder; Billing.SettingsBilling; Billing.RevenueControl; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValues_Output';
-- GO
