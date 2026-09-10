-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-13
-- Description:	Obtener la secuencia numerica
-- =============================================
CREATE PROCEDURE [Common].[SP_GetSequence]
	@Module INT,
	@FormId VARCHAR(5),
	@OperatingUnitId INT,
	@Prefix VARCHAR(4),
	@Type TINYINT,
	@IsManual BIT OUTPUT,
	@Code VARCHAR(20) OUTPUT,
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRY
		SET @IsManual = 0

		/******************************************************** VARIABLES ******************************************************/
		DECLARE @SequenceDetailId INT,
				@Pattern VARCHAR(300),
				@NextS BIGINT

		/************************************************ CONSULTA OPTIMIZADA POR MÓDULO *****************************************/

		-- Módulo 100 - Liquidación de Honorarios Médicos
		IF @Module = 100
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM MedicalFees.MedicalFeesSecuence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN MedicalFees.MedicalFeesSecuenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.SequenseMedicalFeesId
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			-- Actualización optimizada
			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE MedicalFees.MedicalFeesSecuenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 110 - Glosas (Consecutivos)
		ELSE IF @Module = 110
		BEGIN
			DECLARE @tmpConsecutive TABLE (Consecutive BIGINT)
			
			UPDATE Common.Consecutive 
			SET NumberConsecutive += 1 
			OUTPUT inserted.NumberConsecutive INTO @tmpConsecutive
			WHERE Code = @FormId
			
			SELECT TOP 1 @NextS = Consecutive FROM @tmpConsecutive
			SET @IsManual = 0
			SET @SequenceDetailId = 0
			SET @Pattern = '####################'
		END
		-- Módulo 120 - Nómina
		ELSE IF @Module = 120
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM Payroll.PayrollSequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Payroll.PayrollSequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.PayrollSequenceId
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Payroll.PayrollSequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 130 - Contabilidad General
		ELSE IF @Module = 130
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM GeneralLedger.GeneralLedgerSequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN GeneralLedger.GeneralLedgerSequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.IdSequenseAccountingC
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE GeneralLedger.GeneralLedgerSequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 140 - Mantenimiento
		ELSE IF @Module = 140
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM Maintenance.MaintenanceSequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Maintenance.MaintenanceSequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.IdSequenseMaintenanceC
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Maintenance.MaintenanceSequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 150 - Cuentas por Pagar
		ELSE IF @Module = 150
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM Payments.PaymentsSecuence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Payments.PaymentsSecuenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.IdSequensePaymentsC
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Payments.PaymentsSecuenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 160 - Cuentas por Cobrar
		ELSE IF @Module = 160
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM Portfolio.PortfolioSequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Portfolio.PortfolioSequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.IdSequensePortfolioC
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Portfolio.PortfolioSequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 180 - Facturación
		ELSE IF @Module = 180
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM Billing.BillingSequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Billing.BillingSequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.IdSequenseBillingC
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Billing.BillingSequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 190 - Inventarios (con Prefix y Type)
		ELSE IF @Module = 190
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Prefix = ISNULL(sd.Prefix, ''),
				@Pattern = cs.Pattern
			FROM Inventory.InventorySequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Inventory.InventorySequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.InventorySequenceId
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O' AND (ISNULL(sd.Prefix, '') = '' OR ISNULL(sd.Prefix, '') = ISNULL(@Prefix, '')))
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
					OR (s.Scope = 'TO' AND sd.Type = @Type)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Inventory.InventorySequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 200 - Presupuesto
		ELSE IF @Module = 200
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM Budget.BudgetSequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Budget.BudgetSequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.IdSequenseBudgetC
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Budget.BudgetSequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 210 - Activos Fijos
		ELSE IF @Module = 210
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM FixedAsset.FixedAssetSequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN FixedAsset.FixedAssetSequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.IdSequenseFixedAssetC
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE FixedAsset.FixedAssetSequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 220 - Administración de Efectivo (con Prefix y Type)
		ELSE IF @Module = 220
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Prefix = ISNULL(sd.Prefix, ''),
				@Pattern = cs.Pattern
			FROM Treasury.TreasurySequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Treasury.TreasurySequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.IdSequenseTreasuryC
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O' AND ISNULL(sd.Prefix, '') = ISNULL(@Prefix, ''))
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
					OR (s.Scope = 'CC' AND sd.Type = @Type)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Treasury.TreasurySequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 300 - Contratos EAPB
		ELSE IF @Module = 300
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM Contract.ContractSequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Contract.ContractSequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.ContractSequenceId
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Contract.ContractSequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 510 - Costos
		ELSE IF @Module = 510
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM Cost.CostSecuence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN Cost.CostSecuenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.SequenseInteropCostId
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE Cost.CostSecuenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END
		-- Módulo 520 - Central de Mezclas
		ELSE IF @Module = 520
		BEGIN
			SELECT TOP 1
				@IsManual = s.IsManual,
				@SequenceDetailId = sd.Id,
				@Pattern = cs.Pattern
			FROM MixingStation.MixingStationSequence s WITH (READCOMMITTEDLOCK)
			LEFT JOIN MixingStation.MixingStationSequenceDetail sd WITH (READCOMMITTEDLOCK) ON s.Id = sd.IdSequenseMixingStationC
			LEFT JOIN Common.Sequense cs WITH (READCOMMITTEDLOCK) ON sd.IdSequense = cs.Id
			WHERE s.IdForm = @FormId
				AND (
					(s.Scope = 'O')
					OR (s.Scope = 'OU' AND sd.IdOperatingUnit = @OperatingUnitId)
				)
			ORDER BY 
				CASE WHEN s.Scope = 'O' THEN 1 ELSE 2 END,
				CASE WHEN sd.IdOperatingUnit = @OperatingUnitId THEN 1 ELSE 2 END

			IF @IsManual = 0 AND @SequenceDetailId IS NOT NULL
			BEGIN
				UPDATE MixingStation.MixingStationSequenceDetail 
				SET @NextS = [Next] += 1 
				WHERE Id = @SequenceDetailId
			END
		END

		/****************************************************** VALIDACIONES *****************************************************/
		IF @IsManual = 0
		BEGIN
			IF (@SequenceDetailId IS NULL)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'La secuencia para {0} no esta parametrizada'
				RETURN
			END

			SELECT @Code = dbo.GetSequence(ISNULL(@Prefix, ''), @Pattern, @NextS - 1)

			IF @Code = '__ERROR_MAXVALUE__'
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'La secuencia para {0} alcanzó su valor máximo'
				RETURN
			END
		END
		ELSE IF @IsManual = 1 AND ISNULL(@Code, '') = ''
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'La secuencia para {0} es manual y debe tener un código'
			RETURN
		END

		SELECT @CodeResult = 0,
			   @MessageResult = ''
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = 'SP_GetSequence: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera y retorna el siguiente número consecutivo o código secuencial para documentos del sistema según el módulo de negocio solicitado (honorarios médicos, glosas, nómina, contabilidad, mantenimiento, cuentas por pagar, cuentas por cobrar, entre otros). Recibe el módulo, el formulario, la unidad operativa y un prefijo, consulta la configuración de secuencias de cada módulo para determinar si la numeración es manual o automática, e incrementa el contador correspondiente de forma atómica devolviendo el código formateado según el patrón definido. Es el punto central de control de consecutivos del ERP, garantizando que facturas, órdenes, liquidaciones de nómina y demás documentos reciban un número único y correlativo por unidad operativa u organización.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetSequence';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetSequence';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Generar el siguiente código consecutivo de un formulario en función del módulo, alcance (Organización, Unidad Operativa, Tipo) y prefijo, incrementando el contador correspondiente o validando la entrada manual.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El módulo solicitado debe corresponder a uno de los valores soportados (100, 110, 120, 130, 140, 150, 160, 180, 190, 200, 210, 220, 300, 510, 520).; Para módulos distintos de 110 debe existir parametrización de secuencia (registro en la tabla Sequence del módulo y su Detail) cuyo IdForm coincida con el formulario y cuyo Scope aplique para la organización, unidad operativa o tipo solicitado.; Para el módulo 110 debe existir un registro en Common.Consecutive con Code igual al formulario.; Si la secuencia es manual, debe proveerse un código no vacío en el parámetro de salida @Code.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalFees.MedicalFeesSecuence; MedicalFees.MedicalFeesSecuenceDetail; Common.Sequense; Common.Consecutive; Payroll.PayrollSequence; Payroll.PayrollSequenceDetail; GeneralLedger.GeneralLedgerSequence; GeneralLedger.GeneralLedgerSequenceDetail; Maintenance.MaintenanceSequence; Maintenance.MaintenanceSequenceDetail; Payments.PaymentsSecuence; Payments.PaymentsSecuenceDetail; Portfolio.PortfolioSequence; Portfolio.PortfolioSequenceDetail; Billing.BillingSequence; Billing.BillingSequenceDetail; Inventory.InventorySequence; Inventory.InventorySequenceDetail; Budget.BudgetSequence; Budget.BudgetSequenceDetail; FixedAsset.FixedAssetSequence; FixedAsset.FixedAssetSequenceDetail; Treasury.TreasurySequence; Treasury.TreasurySequenceDetail; Contract.ContractSequence; Contract.ContractSequenceDetail; Cost.CostSecuence; Cost.CostSecuenceDetail; MixingStation.MixingStationSequence; MixingStation.MixingStationSequenceDetail', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetSequence';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetSequence';
-- GO
