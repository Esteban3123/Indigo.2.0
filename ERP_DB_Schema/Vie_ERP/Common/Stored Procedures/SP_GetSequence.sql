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
			FROM MedicalFees.MedicalFeesSecuence s WITH (NOLOCK)
			LEFT JOIN MedicalFees.MedicalFeesSecuenceDetail sd WITH (NOLOCK) ON s.Id = sd.SequenseMedicalFeesId
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Payroll.PayrollSequence s WITH (NOLOCK)
			LEFT JOIN Payroll.PayrollSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.PayrollSequenceId
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM GeneralLedger.GeneralLedgerSequence s WITH (NOLOCK)
			LEFT JOIN GeneralLedger.GeneralLedgerSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseAccountingC
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Maintenance.MaintenanceSequence s WITH (NOLOCK)
			LEFT JOIN Maintenance.MaintenanceSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseMaintenanceC
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Payments.PaymentsSecuence s WITH (NOLOCK)
			LEFT JOIN Payments.PaymentsSecuenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequensePaymentsC
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Portfolio.PortfolioSequence s WITH (NOLOCK)
			LEFT JOIN Portfolio.PortfolioSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequensePortfolioC
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Billing.BillingSequence s WITH (NOLOCK)
			LEFT JOIN Billing.BillingSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseBillingC
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Inventory.InventorySequence s WITH (NOLOCK)
			LEFT JOIN Inventory.InventorySequenceDetail sd WITH (NOLOCK) ON s.Id = sd.InventorySequenceId
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Budget.BudgetSequence s WITH (NOLOCK)
			LEFT JOIN Budget.BudgetSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseBudgetC
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM FixedAsset.FixedAssetSequence s WITH (NOLOCK)
			LEFT JOIN FixedAsset.FixedAssetSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseFixedAssetC
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Treasury.TreasurySequence s WITH (NOLOCK)
			LEFT JOIN Treasury.TreasurySequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseTreasuryC
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Contract.ContractSequence s WITH (NOLOCK)
			LEFT JOIN Contract.ContractSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.ContractSequenceId
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM Cost.CostSecuence s WITH (NOLOCK)
			LEFT JOIN Cost.CostSecuenceDetail sd WITH (NOLOCK) ON s.Id = sd.SequenseInteropCostId
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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
			FROM MixingStation.MixingStationSequence s WITH (NOLOCK)
			LEFT JOIN MixingStation.MixingStationSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseMixingStationC
			LEFT JOIN Common.Sequense cs WITH (NOLOCK) ON sd.IdSequense = cs.Id
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