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
			FROM Billing.RevenueControlDetail rcd WITH (NOLOCK)
			JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
			JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
			JOIN Billing.ServiceOrder so WITH (NOLOCK) ON sod.ServiceOrderId = so.Id
			WHERE rcd.Id = @RevenueControlDetailId
		END

		--obtengo el valor de redondeo de la cuota de paciente
		SELECT	@RoundingTypeRecoveryFeeType = sb.RoundingTypeRecoveryFeeType,
				@ParticularHealthAdministratorId = sb.ParticularHealthAdministratorId
		FROM Billing.SettingsBilling sb WITH (NOLOCK)
		WHERE sb.IdOperatingUnit = @OperativeUnitId

		--- Dejo en cero los valores de los productos que estan incluidos al 100%
		UPDATE sodd
			SET GrandTotalSalesPrice = 0, 
				GrandTotalDiscount = 0, 
				ThirdPartySalesPrice = 0, 
				SubTotalPatientSalesPrice = 0
		FROM Billing.ServiceOrderDetailDistribution sodd
		INNER JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
		WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID AND sod.SettlementType = 3

		UPDATE sod
			SET SubTotalSalesPrice = 0, 
				ThirdPartyDiscount = 0, 
				TotalSalesPrice = 0, 
				GrandTotalSalesPrice = 0
		FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
		INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = sodd.ServiceOrderDetailId
		WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID and sod.SettlementType = 3

		
		--valido cantidades totales, por valor total
		IF EXISTS(	select 1
					FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
					INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = sodd.ServiceOrderDetailId
					WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID and sod.SettlementType = 1
					AND sod.IncludeServiceOrderDetailId is NULL AND sod.IsPackage=0 and sod.PackageServiceOrderDetailId is NULL
					AND sod.GrandTotalSalesPrice <> sodd.GrandTotalSalesPrice)
		BEGIN
			UPDATE sodd
			SET
				sodd.GrandTotalSalesPrice = sod.GrandTotalSalesPrice,
				sodd.ThirdPartySalesPrice = ROUND(sod.GrandTotalSalesPrice * (sodd.ThirdPartyPercentage / 100), 2),
				sodd.SubTotalPatientSalesPrice = sod.GrandTotalSalesPrice - ROUND(sod.GrandTotalSalesPrice * (sodd.ThirdPartyPercentage / 100), 2)
			FROM Billing.ServiceOrderDetailDistribution sodd
				INNER JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
			WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID 
				AND sod.SettlementType = 1
				AND sod.IncludeServiceOrderDetailId IS NULL 
				AND sod.IsPackage = 0 
				AND sod.PackageServiceOrderDetailId IS NULL
				AND sod.GrandTotalSalesPrice <> sodd.GrandTotalSalesPrice
		end

		UPDATE Billing.ServiceOrderDetailDistribution 
			SET ThirdPartySalesPrice = GrandTotalSalesPrice - SubTotalPatientSalesPrice 
		WHERE id IN 
		(
			SELECT id
			FROM Billing.ServiceOrderDetailDistribution WITH (NOLOCK)
			WHERE RevenueControlDetailId = @REVENUECONTROLDETAILID AND (ThirdPartySalesPrice + SubTotalPatientSalesPrice) <> GrandTotalSalesPrice
		)

		SELECT @PatientDiscountPercentage = COALESCE(PatientDiscountPercentage,0),
				@REVENUECONTROLID = RevenueControlId 
		FROM Billing.RevenueControlDetail WITH (NOLOCK)
		where Id = @REVENUECONTROLDETAILID

		SELECT @TotalPatientSalesPrice = COALESCE(SUM(SubTotalPatientSalesPrice),0),
				@TotalPatientWithDiscount = COALESCE(SUM(SubTotalPatientSalesPrice*(1-@PatientDiscountPercentage/100)),0),
				@TotalFolio = COALESCE(SUM(GrandTotalSalesPrice),0), 
				@ThirdPartySalesPriceTotal = COALESCE(SUM(ThirdPartySalesPrice),0)
		FROM Billing.ServiceOrderDetailDistribution WITH (NOLOCK)
		WHERE Id IN 
		(
			SELECT DISTINCT sodd.id
			FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
			INNER JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id AND sod.IsDelete = 0
			WHERE RevenueControlDetailId = @REVENUECONTROLDETAILID
		)

		SELECT @ValueFeeModerator = COALESCE(SUM(SubTotalPatientSalesPrice), 0) 
		FROM Billing.ServiceOrderDetailDistribution WITH (NOLOCK)
		WHERE Id IN 
		(
			SELECT DISTINCT sodd.id
			FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
			INNER JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id and sod.IsDelete = 0
			WHERE RevenueControlDetailId = @REVENUECONTROLDETAILID AND RecoveryFeeType = 2
		)

		SELECT @ValueCopay = COALESCE(sum(SubTotalPatientSalesPrice), 0) 
		FROM Billing.ServiceOrderDetailDistribution WITH (NOLOCK)
		WHERE Id IN 
		(
			SELECT DISTINCT sodd.id
			FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
			INNER JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id AND sod.IsDelete = 0
			WHERE RevenueControlDetailId = @REVENUECONTROLDETAILID AND RecoveryFeeType = 3
		)

		SELECT @ValueBond = COALESCE(SUM(SubTotalPatientSalesPrice), 0) 
		FROM Billing.ServiceOrderDetailDistribution WITH (NOLOCK)
		WHERE Id IN 
		(
			SELECT DISTINCT sodd.id
			FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
			INNER JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id AND sod.IsDelete = 0
			WHERE RevenueControlDetailId = @REVENUECONTROLDETAILID AND RecoveryFeeType = 4
		)

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

				SELECT TOP 1 @IdDistribution=Id
					, @valueEntity=ThirdPartySalesPrice 
					, @PatientValue = SubTotalPatientSalesPrice
				FROM Billing.ServiceOrderDetailDistribution WITH (NOLOCK)
				WHERE id IN
				(
					SELECT DISTINCT sodd.id
					FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
					INNER JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id AND sod.IsDelete = 0
					WHERE sodd.RevenueControlDetailId = @REVENUECONTROLDETAILID AND sodd.ApplyRecoveryFee = 2
				) 
				ORDER BY SubTotalPatientSalesPrice DESC
				
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
		JOIN Contract.CareGroup cg WITH (NOLOCK) ON rcd.CareGroupId = cg.Id
		WHERE rcd.Id = @REVENUECONTROLDETAILID

		SELECT	@TopEventFeeModerator = ISNULL(SUM(rcd.ValueFeeModerator), 0),
				@TopEventCopay = ISNULL(SUM(rcd.ValueCopay), 0),
				@TopEventFeeRecovery = ISNULL(SUM(rcd.ValueFeeModerator + rcd.ValueCopay), 0)
		FROM Billing.RevenueControl rc WITH (NOLOCK)
		JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON rcd.RevenueControlId = rc.Id AND rcd.Id <> @REVENUECONTROLDETAILID
		WHERE rc.Id = @REVENUECONTROLID
		
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