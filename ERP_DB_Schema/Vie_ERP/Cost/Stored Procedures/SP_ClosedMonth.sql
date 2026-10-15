
-- =============================================
-- Author:         Miguel Angel Fonseca Castro
-- Create date:	   2019-06-24
-- Description:    Procedimiento almacenado para encargado del cierre de mes en el módulo de costos (A la vez que el cálculo de actividades)
-- =============================================
CREATE PROCEDURE [Cost].[SP_ClosedMonth]
    @SettingsId INT,			-- Id de la configuración de costos que se esta cerrando
	@Year INT,					-- Año a cerrar
	@Month INT,					-- Mes a cerrar
	@CostEstimateLabor BIT,		-- Indica el metodo del calculo del valor de nomina
	@ValidateActivities BIT,	-- Indica si se deben validar las actividades
    @UserCode VARCHAR(20),		-- Código del usuario que se encuentra realizando el proceso
	----------------------------------------------------------------------------------------
	@CodeMessage INT OUTPUT,
	@Message VARCHAR(MAX) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	/*********************************************** VARIABLES GLOBALES ***********************************************/

	DECLARE @ActivityCalculationBy TINYINT,
			---------------------------------
			@Id INT,
			@ExistsSecondaryDistribution BIT,
			---------------------------------
			@errors VARCHAR(MAX) = ''

    BEGIN TRY

		SELECT @ActivityCalculationBy = ActivityCalculationBy
		FROM Cost.CostSetting
		WHERE Id = @SettingsId

		SELECT @ExistsSecondaryDistribution = 1
		FROM Cost.CostEstimationNative cen 
		WHERE cen.Year = @Year AND cen.Month = @Month

		/*********************************************** VALIDACIONES GLOBALES ***********************************************/

		IF EXISTS (SELECT 1 FROM Cost.ClosedMonth WHERE [Year] = @Year AND [Month] = @Month)
		BEGIN
			SELECT @CodeMessage = '999', 
				   @Message = 'El periodo (' + CAST(@Year AS VARCHAR) + '-' + CAST(@Month AS VARCHAR) + ') ya se encuentra cerrado'
			RETURN
		END        

		/*************************************************** VALIDACIONES ACTIVIDADES ***************************************************/

		IF @ValidateActivities = 1
		BEGIN
			--Todas las actividades del periodo deben haber sido configuradas
			IF EXISTS
			(
				SELECT 1
				FROM Billing.Invoice i
				JOIN Billing.ServiceOrderDetailDistribution sodd ON i.RevenueControlDetailId = sodd.RevenueControlDetailId
				JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
				LEFT JOIN Cost.CostActivity ca ON sod.CUPSEntityId = ca.CUPSEntityId AND ca.Status = 1
				WHERE YEAR(i.InvoiceDate) = @Year AND MONTH(i.InvoiceDate) = @Month
					AND sod.RecordType = 1 AND ca.Id IS NULL
			)
			BEGIN
				SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cu.Code + ' - ' + cu.Description
					FROM Billing.Invoice i
					JOIN Billing.ServiceOrderDetailDistribution sodd ON i.RevenueControlDetailId = sodd.RevenueControlDetailId
					JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
					JOIN Contract.CUPSEntity cu ON sod.CUPSEntityId = cu.Id
					LEFT JOIN Cost.CostActivity ca ON sod.CUPSEntityId = ca.CUPSEntityId AND ca.Status = 1
					WHERE YEAR(i.InvoiceDate) = @Year AND MONTH(i.InvoiceDate) = @Month
						AND sod.RecordType = 1 AND ca.Id IS NULL
					FOR XML path(N''), TYPE).value(N'.[1]', N'nVARCHAR(MAX)'), 1, 2, N'')

				SELECT @CodeMessage = '999', 
					   @Message = 'Las siguientes actividades no se encuentran parametrizadas: ' + CHAR(13) + CHAR(10) + @errors
				RETURN
			END

			-- Todas las actividades deben existir parametrizadas en el centro donde se realizaron
			IF EXISTS
			(
				SELECT 1
				FROM
				(
					SELECT 
						sod.CostCenterId, 
						sod.CUPSEntityId,
						SUM(sod.InvoicedQuantity) Quantity
					FROM Billing.Invoice i
					JOIN Billing.ServiceOrderDetailDistribution sodd ON i.RevenueControlDetailId = sodd.RevenueControlDetailId
					JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
					WHERE YEAR(i.InvoiceDate) = @Year AND MONTH(i.InvoiceDate) = @Month
						AND sod.RecordType = 1
					GROUP BY sod.CostCenterId, sod.CUPSEntityId
				) sod
				LEFT JOIN 
				(
					SELECT cpccc.CostCenterId, ca.CUPSEntityId
					FROM Cost.CostActivity ca
					JOIN Cost.CostActivityProductionCenter capc ON ca.Id = capc.CostActivityId
					JOIN Cost.CostProductionCenterCostCenter cpccc ON capc.CostProductionCenterId = cpccc.ProductionCenterId
					WHERE ca.Status = 1
					GROUP BY cpccc.CostCenterId, ca.CUPSEntityId

				) ca ON sod.CostCenterId = ca.CostCenterId AND sod.CUPSEntityId = ca.CUPSEntityId
				WHERE ca.CostCenterId IS NULL
			)
			BEGIN
				SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cu.Code + ' - ' + cu.Description + ' (' + cc.Code + ' - ' + cc.Name + ')'
					FROM
					(
						SELECT 
							sod.CostCenterId, 
							sod.CUPSEntityId,
							SUM(sod.InvoicedQuantity) Quantity
						FROM Billing.Invoice i
						JOIN Billing.ServiceOrderDetailDistribution sodd ON i.RevenueControlDetailId = sodd.RevenueControlDetailId
						JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
						WHERE YEAR(i.InvoiceDate) = @Year AND MONTH(i.InvoiceDate) = @Month
							AND sod.RecordType = 1
						GROUP BY sod.CostCenterId, sod.CUPSEntityId
					) sod
					JOIN Contract.CUPSEntity cu ON sod.CUPSEntityId = cu.Id
					JOIN Payroll.CostCenter cc ON sod.CostCenterId = cc.Id
					LEFT JOIN 
					(
						SELECT cpccc.CostCenterId, ca.CUPSEntityId
						FROM Cost.CostActivity ca
						JOIN Cost.CostActivityProductionCenter capc ON ca.Id = capc.CostActivityId
						JOIN Cost.CostProductionCenterCostCenter cpccc ON capc.CostProductionCenterId = cpccc.ProductionCenterId
						WHERE ca.Status = 1
						GROUP BY cpccc.CostCenterId, ca.CUPSEntityId

					) ca ON sod.CostCenterId = ca.CostCenterId AND sod.CUPSEntityId = ca.CUPSEntityId
					WHERE ca.CostCenterId IS NULL
					FOR XML path(N''), TYPE).value(N'.[1]', N'nVARCHAR(MAX)'), 1, 2, N'')

				SELECT @CodeMessage = '999', 
					   @Message = 'Las siguientes actividades no se encuentran parametrizadas en los centros donde se realizaron: ' + CHAR(13) + CHAR(10) + @errors
				RETURN
			END
		END

		/****************************************** INSERTAMOS CABECERA ******************************************/

		INSERT INTO [Cost].[ClosedMonth] (Year, Month, CreationUser, CreationDate)
			SELECT @Year, @Month, @UserCode, [Common].[GETDATE]()

		SET @Id = SCOPE_IDENTITY()

		;WITH CTE_CupsInvoiceServiceOrderDetal AS (
			SELECT
				sod.CostCenterId,
				sod.CUPSEntityId,
				ISNULL(id.TotalSalesPrice, 0) AS TotalSales,
				sodd.Quantity AS Quantity
			FROM Billing.Invoice i
			JOIN Billing.ServiceOrderDetailDistribution sodd ON i.RevenueControlDetailId = sodd.RevenueControlDetailId
			JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
			LEFT JOIN Billing.InvoiceDetail id ON i.Id = id.InvoiceId AND sod.Id = id.ServiceOrderDetailId
			WHERE YEAR(i.InvoiceDate) = @Year AND MONTH(i.InvoiceDate) = @Month
				AND sod.RecordType = 1
		)
		/****************************************** VALOR DE VENTA TOTALIZADO POR ACTIVIDAD ******************************************/
		
		INSERT INTO [Cost].[ClosedMonthCUPSEntityByTotalSales] (ClosedMonthId, CUPSEntityId, TotalGlobalSales, Quantity, AverageSales)
			SELECT 
				@Id AS ClosedMonthId,
				cte.CUPSEntityId,
				SUM(cte.TotalSales) AS TotalGlobalSales,
				SUM(cte.Quantity) AS Quantity,
				CASE 
					WHEN SUM(cte.Quantity) > 0 THEN SUM(cte.TotalSales) / SUM(cte.Quantity) 
					ELSE 0 
				END AS AverageSales
			FROM CTE_CupsInvoiceServiceOrderDetal cte
			GROUP BY cte.CUPSEntityId --Ordenamos por CUPS para tener el totalizado con valores combinados sin desagregación por centro de costo

		/****************************************** ACTIVIDADES POR CENTRO DE COSTO ******************************************/

		INSERT INTO [Cost].[ClosedMonthCUPSEntityByCostCenter] (ClosedMonthId, CostCenterId, CUPSEntityId, Quantity, TotalSales)
			SELECT 
				@Id,
				cte.CostCenterId, 
				cte.CUPSEntityId,
				SUM(cte.Quantity) Quantity,
				SUM(cte.TotalSales) TotalSales
			FROM CTE_CupsInvoiceServiceOrderDetal cte
			GROUP BY cte.CostCenterId, cte.CUPSEntityId

		/****************************************** CALCULAR HORAS Y COSTO UNITARIO PROMEDIO ACTIVOS FIJOS ******************************************/
		
		INSERT INTO [Cost].[ClosedMonthFixedAsset] (ClosedMonthId, CostCenterId, FixedAssetItemId, TotalHoursWorked, ValueTotal, AverageCost)
			SELECT
				@Id,
				fu.CostCenterId,
				fapa.ItemId,
				SUM(faddc.DepreciatedDays * fal.UseTime) AS TotalHoursWorked,
				SUM(faddc.DepreciationValue) AS ValueTotal,
				SUM(faddc.DepreciationValue) / SUM(faddc.DepreciatedDays * fal.UseTime) AS AverageCost
			FROM FixedAsset.FixedAssetDepreciation fad
			JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
			JOIN GeneralLedger.LegalBook lb ON fadd.LegalBookId = lb.Id AND lb.OfficialBook = 1
			JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON fadd.FixedAssetPhysicalAssetId = fapa.Id
			JOIN FixedAsset.FixedAssetDepreciationDetailCost faddc ON fadd.Id = faddc.FixedAssetDepreciationDetailId
			JOIN FixedAsset.FixedAssetLocation fal ON faddc.LocationId = fal.Id
			JOIN Payroll.FunctionalUnit fu ON fal.FunctionalUnitId = fu.Id
			WHERE fad.ClosingYear = @Year AND fad.ClosingMonth = @Month AND fad.Status = 2
			GROUP BY fu.CostCenterId, fapa.ItemId

		/****************************************** CALCULAR HORAS Y COSTO UNITARIO PROMEDIO NOMINA ******************************************/

		--Empleados de Nomina
		INSERT INTO [Cost].[ClosedMonthPayroll] (ClosedMonthId, CostCenterId, PositionId, TotalHoursWorked, ValueTotal, AverageCost)
			SELECT 
				@Id,
				fu.CostCenterId,
				c.PositionId AS PositionId,
				SUM(c.HoursDaily * l.DaysWorked) AS TotalHoursWorked,
				SUM
				(
					CASE @CostEstimateLabor
						WHEN 1 THEN l.TotalAccrued
						ELSE
							l.TotalAccrued +
							----------------------------------------------------------------------------------
							ISNULL(l.EmployerHealthContributionValue, 0) +
							ISNULL(l.EmployerPensionContributionValue, 0) +
							ISNULL(l.OccupationalRisksContributionValue, 0) +
							----------------------------------------------------------------------------------
							ISNULL(l.SenaContributionValue, 0) +
							ISNULL(l.FamilyCompensationFundContributionValue, 0) +
							ISNULL(l.ICBFContributionValue, 0) +
							----------------------------------------------------------------------------------
							ISNULL(l.ProvisionVacation, 0) +
							ISNULL(l.ProvisionIncentive, 0) +
							ISNULL(l.ProvisionIncentive, 0) +
							ISNULL(l.ProvisionInterestsUnemployment, 0)
					END
				) AS ValueTotal,
				0
			FROM Payroll.Liquidation l
			JOIN Payroll.Contract c ON l.ContractId = c.Id
			JOIN Payroll.FunctionalUnit fu ON c.FunctionalUnitId = fu.Id
			WHERE YEAR(l.PayrollDateLiquidated) = @Year AND MONTH(l.PayrollDateLiquidated) = @Month AND l.RegisterStatus = 'C'
			GROUP BY fu.CostCenterId, c.PositionId

		--Contratistas
		DECLARE @ClosedMonthPayroll AS TABLE
		(
			AccountPayableId INT,
			CostCenterId INT,
			PositionId INT,
			Hours INT,
			Value DECIMAL(18,2)
		)

		INSERT INTO @ClosedMonthPayroll
			SELECT 
				ap.Id,
				apdc.IdCostCenter,
				ap.PositionId, 
				ap.Hours AS TotalHoursWorked, 
				SUM(apdc.Value * IIF(apdc.Nature = 1, 1, -1)) AS ValueTotal
			FROM Payments.AccountPayable ap
			JOIN Payments.AccountPayableDetailConcept apdc ON ap.Id = apdc.IdAccountPayable
			JOIN GeneralLedger.MainAccounts ma ON apdc.IdAccount = ma.Id
			JOIN GeneralLedger.MainAccountClasses mac ON ma.IdAccountClass = mac.Id
			WHERE ap.Status = 2 
				AND mac.Type = 2
				AND apdc.IdCostCenter IS NOT NULL
				AND ap.PositionId IS NOT NULL AND YEAR(ap.DocumentDate) = @Year AND MONTH(ap.DocumentDate) = @Month
			GROUP BY ap.Id, apdc.IdCostCenter, ap.PositionId, ap.Hours

		DECLARE @AccountPayableRows INT = 1,
				@AccountPayableId INT = 0,
				@HoursTotal INT,
				@ValueTotal DECIMAL(18,2),
				------------------------------------
				@DifferenceHours DECIMAL(20,4),
				------------------------------------
				@CostCenterRows INT,
				@CostCenterId INT,
				@AdjustedHours INT

		WHILE @AccountPayableRows > 0
		BEGIN
			SELECT TOP 1
				@AccountPayableId = cmp.AccountPayableId,
				@HoursTotal = cmp.Hours,		
				@ValueTotal = SUM(cmp.Value),
				-------------------------------------------------
				@DifferenceHours = cmp.Hours,
				-------------------------------------------------
				@CostCenterRows = 1,
				@CostCenterId = 0,
				@AdjustedHours = 0
			FROM @ClosedMonthPayroll cmp
			WHERE cmp.AccountPayableId > @AccountPayableId
			GROUP BY cmp.AccountPayableId, cmp.Hours
			ORDER BY cmp.AccountPayableId

			SET @AccountPayableRows = @@ROWCOUNT
			IF @AccountPayableRows = 0 
			BEGIN
				BREAK
			END

			UPDATE cmp
				SET cmp.Hours = IIF(@ValueTotal = 0, 0, cmp.Hours * cmp.Value / @ValueTotal)
			FROM @ClosedMonthPayroll cmp
			WHERE cmp.AccountPayableId = @AccountPayableId

			SELECT @DifferenceHours = @HoursTotal - SUM(cmp.Hours)
			FROM @ClosedMonthPayroll cmp
			WHERE cmp.AccountPayableId = @AccountPayableId

			IF @DifferenceHours <> 0
			BEGIN
				WHILE @CostCenterRows > 0
				BEGIN
					SELECT TOP 1
						@CostCenterId = cmp.CostCenterId,
						@AdjustedHours = IIF(@DifferenceHours > cmp.Hours, cmp.Hours, @DifferenceHours)
					FROM @ClosedMonthPayroll cmp
					WHERE cmp.AccountPayableId = @AccountPayableId
						AND cmp.CostCenterId > @CostCenterId
						AND @DifferenceHours <> 0
					ORDER BY cmp.CostCenterId

					SET @CostCenterRows = @@ROWCOUNT
					IF @CostCenterRows = 0 
					BEGIN
						BREAK
					END

					UPDATE cmp
						SET cmp.Hours = cmp.Hours + @AdjustedHours
					FROM @ClosedMonthPayroll cmp
					WHERE cmp.AccountPayableId = @AccountPayableId
						AND cmp.CostCenterId = @CostCenterId

					SET @DifferenceHours = @DifferenceHours - @AdjustedHours
				END
			END
		END

		--Actualizamos los existentes
		UPDATE cmp
			SET cmp.TotalHoursWorked = cmp.TotalHoursWorked + c.Hours,
				cmp.ValueTotal = cmp.ValueTotal + c.Value
		FROM Cost.ClosedMonthPayroll cmp
		JOIN @ClosedMonthPayroll c ON cmp.CostCenterId = c.CostCenterId AND cmp.PositionId = c.PositionId
		WHERE cmp.ClosedMonthId = @Id

		--Insertamos los que hagan falta
		INSERT INTO [Cost].[ClosedMonthPayroll] (ClosedMonthId, CostCenterId, PositionId, TotalHoursWorked, ValueTotal, AverageCost)
			SELECT @Id, c.CostCenterId, c.PositionId, c.Hours, c.Value, 0
			FROM @ClosedMonthPayroll c
			LEFT JOIN Cost.ClosedMonthPayroll cmp ON cmp.ClosedMonthId = @Id AND cmp.CostCenterId = c.CostCenterId AND cmp.PositionId = c.PositionId
			WHERE cmp.Id IS NULL

		--Actualizamos el costo promedio de hora
		UPDATE cmp
			SET cmp.AverageCost = IIF(cmp.TotalHoursWorked = 0, 0, cmp.ValueTotal / cmp.TotalHoursWorked)
		FROM Cost.ClosedMonthPayroll cmp
		WHERE cmp.ClosedMonthId = @Id

		/****************************************** CALCULAR COSTO UNITARIO PROMEDIO INVENTARIO ******************************************/

		INSERT INTO [Cost].[ClosedMonthInventory] (ClosedMonthId, CostCenterId, CostInventoryGroupId, AverageCost)
			SELECT
				@Id,
				cigd.CostCenterId,
				cig.Id CostInventoryGroupId,
				IIF(cigd.Quantity = 0, 0, (cigd.ValueTotal / cigd.Quantity)) AverageCost
			FROM Cost.CostInventoryGroup cig
			JOIN
			(
				SELECT 
					icbcc.CostCenterId,
					cigd.CostInventoryGroupId,
					SUM(ISNULL(cmid.CostTotal, cmi.Quantity * cmi.ProductCost) * cigd.Quantity) ValueTotal,
					SUM(cmi.Quantity * cigd.Quantity) Quantity
				FROM Inventory.ClosedMonth cm
				JOIN Inventory.ClosedMonthInventory cmi ON cm.Id = cmi.ClosedMonthId
				JOIN Inventory.ClosedMonthInventoryDetail cmid ON cmid.ClosedMonthInventoryId = cmi.Id
				JOIN Cost.CostInventoryGroupDetail cigd ON cmi.ProductId = cigd.InventoryProductId
				JOIN Cost.InventoryConsumedByCostCenter icbcc ON cm.Year = icbcc.Year AND cm.Month = icbcc.Month AND cmi.ProductId = icbcc.ProductId
				WHERE cm.Year = @Year
					AND cm.Month = @Month
				GROUP BY icbcc.CostCenterId, cigd.CostInventoryGroupId
			) cigd ON cig.Id = cigd.CostInventoryGroupId
			WHERE cig.Status = 1		

		/****************************************** CALCULAR COSTO ACTIVIDADES (CALCULADO de Acuerdo al detalle) ******************************************/

		INSERT INTO [Cost].[ClosedMonthCostActivity] (ClosedMonthId, CostActivityId, CUPSEntityId, CostProductionCenterId, CostCenterId, FixedAssetValue, PayrollValue, InventoryValue, AddictionalValue, UnitValue)
			SELECT
				@Id,
				ca.Id AS CostActivityId,
				ca.CUPSEntityId,
				capc.CostProductionCenterId,
				IIF(@ActivityCalculationBy = 1, NULL, cpccc.CostCenterId) CostCenterId,
				ISNULL(casfa.FixedAssetValue, 0),
				ISNULL(casp.PayrollValue, 0),
				ISNULL(casi.InventoryValue, 0),
				ISNULL(casac.AddictionalValue, 0),
				ISNULL(casfa.FixedAssetValue, 0) + ISNULL(casp.PayrollValue, 0) + ISNULL(casi.InventoryValue, 0) + ISNULL(casac.AddictionalValue, 0) AS UnitValue
			FROM Cost.CostActivity ca
			JOIN Cost.CostActivityProductionCenter capc ON ca.Id = capc.CostActivityId
			JOIN Cost.CostProductionCenterCostCenter cpccc ON capc.CostProductionCenterId = cpccc.ProductionCenterId
			LEFT JOIN
			(
				SELECT
					cas.CostActivityId,
					IIF(@ActivityCalculationBy = 1, NULL, cmfa.CostCenterId) CostCenterId,
					SUM(casfa.Hours * cmfa.AverageCost) AS FixedAssetValue
				FROM Cost.CostActivityStep cas
				JOIN Cost.CostActivityStepFixedAsset casfa ON cas.Id = casfa.CostActivityStepId
				JOIN Cost.ClosedMonthFixedAsset cmfa ON casfa.FixedAssetItemId = cmfa.FixedAssetItemId
				WHERE cmfa.ClosedMonthId = @Id
				GROUP BY cas.CostActivityId, IIF(@ActivityCalculationBy = 1, NULL, cmfa.CostCenterId)
			) casfa ON ca.Id = casfa.CostActivityId AND (@ActivityCalculationBy = 1 OR cpccc.CostCenterId = casfa.CostCenterId)
			LEFT JOIN
			(
				SELECT
					cas.CostActivityId,
					IIF(@ActivityCalculationBy = 1, NULL, cmp.CostCenterId) CostCenterId,
					SUM(casp.Hours * cmp.AverageCost) AS PayrollValue
				FROM Cost.CostActivityStep cas
				JOIN Cost.CostActivityStepPayroll casp ON cas.Id = casp.CostActivityStepId
				JOIN Cost.ClosedMonthPayroll cmp ON casp.PayrollPositionId = cmp.PositionId
				WHERE cmp.ClosedMonthId = @Id
				GROUP BY cas.CostActivityId, IIF(@ActivityCalculationBy = 1, NULL, cmp.CostCenterId)
			) casp ON ca.Id = casp.CostActivityId AND (@ActivityCalculationBy = 1 OR cpccc.CostCenterId = casp.CostCenterId)
			LEFT JOIN
			(
				SELECT
					cas.CostActivityId,
					IIF(@ActivityCalculationBy = 1, NULL, cmi.CostCenterId) CostCenterId,
					SUM(casi.Quantity * cmi.AverageCost) AS InventoryValue
				FROM Cost.CostActivityStep cas
				JOIN Cost.CostActivityStepInventory casi ON cas.Id = casi.CostActivityStepId
				JOIN Cost.ClosedMonthInventory cmi ON casi.CostInventoryGroupId = cmi.CostInventoryGroupId
				WHERE cmi.ClosedMonthId = @Id
				GROUP BY cas.CostActivityId, IIF(@ActivityCalculationBy = 1, NULL, cmi.CostCenterId) 
			) casi ON ca.Id = casi.CostActivityId AND (@ActivityCalculationBy = 1 OR cpccc.CostCenterId = casi.CostCenterId)
			LEFT JOIN
			(
				SELECT
					cas.CostActivityId,
					SUM(casac.Value) AS AddictionalValue
				FROM Cost.CostActivityStep cas
				JOIN Cost.CostActivityStepAddictionalCost casac ON cas.Id = casac.CostActivityStepId
				GROUP BY cas.CostActivityId
			) casac ON ca.Id = casac.CostActivityId
			WHERE ca.Status = 1

		/****************************************** CALCULAR COSTO ACTIVIDADES POR CENTRO DE PRODUCCION (Prorrateo) ******************************************/

		DECLARE @ProductionCenterRows INT = 1,
				@ProductionCenterId INT = 0,
				@DistributionValue DECIMAL(20,4),
				@TotalCalculatedValue DECIMAL(20,4),
				------------------------------------
				@Difference DECIMAL(20,4),
				------------------------------------
				@ClosedMonthCUPSEntityRows INT,
				@ClosedMonthCUPSEntityId INT,
				@AdjustedValue DECIMAL(20,4)

		
		WHILE @ProductionCenterRows > 0
		BEGIN
			SELECT TOP 1
				@ProductionCenterId = cen.ProductionCenterId,
				@DistributionValue = IIF(@ExistsSecondaryDistribution = 1, cen.SecondaryDistribution, cen.InitialDistribution),
				@Difference = 0,
				-------------------------------------------------
				@ClosedMonthCUPSEntityRows = 1,
				@ClosedMonthCUPSEntityId = 0,
				@AdjustedValue = 0
			FROM Cost.CostEstimationNative cen
			WHERE cen.Year = @Year AND cen.Month = @Month
				AND cen.ProductionCenterId > @ProductionCenterId
			ORDER BY cen.ProductionCenterId

			SET @ProductionCenterRows = @@ROWCOUNT
			IF @ProductionCenterRows = 0 
			BEGIN
				BREAK
			END

			SELECT @TotalCalculatedValue = SUM(v.TotalValue)
			FROM Cost.ViewCupsEntityCostByProductionCenter v
			WHERE v.ClosedMonthId = @Id
				AND v.CostProductionCenterId = @ProductionCenterId

			IF @TotalCalculatedValue <> 0
			BEGIN
				INSERT INTO Cost.ClosedMonthCUPSEntity (ClosedMonthId, ProductionCenterId, CUPSEntityId, Quantity, UnitValue, EstimatedValue)
					SELECT
						v.ClosedMonthId, 
						v.CostProductionCenterId, 
						v.CUPSEntityId,
						v.Quantity,
						v.UnitValue,
						IIF(@TotalCalculatedValue = 0, 0, ROUND(@DistributionValue * (v.TotalValue / @TotalCalculatedValue), 4))
					FROM Cost.ViewCupsEntityCostByProductionCenter v
					WHERE v.ClosedMonthId = @Id
						AND v.CostProductionCenterId = @ProductionCenterId

				SELECT @Difference = @DistributionValue - SUM(cmce.EstimatedValue)
				FROM Cost.ClosedMonthCUPSEntity cmce
				WHERE cmce.ClosedMonthId = @Id
					AND cmce.ProductionCenterId = @ProductionCenterId

				IF @Difference <> 0
				BEGIN
					WHILE @ClosedMonthCUPSEntityRows > 0
					BEGIN
						SELECT TOP 1
							@ClosedMonthCUPSEntityId = cmce.Id,
							@AdjustedValue = IIF(@Difference > cmce.EstimatedValue, cmce.EstimatedValue, @Difference)
						FROM Cost.ClosedMonthCUPSEntity cmce
						WHERE cmce.ClosedMonthId = @Id
							AND cmce.ProductionCenterId = @ProductionCenterId
							AND cmce.Id > @ClosedMonthCUPSEntityId
							AND @Difference <> 0
						ORDER BY cmce.Id

						SET @ClosedMonthCUPSEntityRows = @@ROWCOUNT
						IF @ClosedMonthCUPSEntityRows = 0 
						BEGIN
							BREAK
						END

						UPDATE cmce
							SET cmce.EstimatedValue = cmce.EstimatedValue + @AdjustedValue
						FROM Cost.ClosedMonthCUPSEntity cmce
						WHERE cmce.id = @ClosedMonthCUPSEntityId

						SET @Difference = @Difference - @AdjustedValue
					END
				END
			END
		END

		/*************************************************** RETORNO DE RESULTADO ***************************************************/

        SELECT @CodeMessage = '000', 
			   @Message = 'Ok'
    END TRY
    BEGIN CATCH
		SELECT @CodeMessage = '999', 
			   @Message = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
    END CATCH
    
END