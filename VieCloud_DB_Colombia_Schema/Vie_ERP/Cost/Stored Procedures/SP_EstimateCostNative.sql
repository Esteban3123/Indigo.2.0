-- =============================================
-- Author:         Carlos Mario Arias Rubiano
-- Create date:	   2016-11-09
-- Description:    Procedimiento almacenado para estimar los costos
-- =============================================
CREATE PROCEDURE [Cost].[SP_EstimateCostNative]
	@Year INT,
	@Month INT,
    @DistributionType TINYINT, --1 - primaria, 2 - Secundaria, 3 - Final    
    @OnlySimulate BIT, -- 1 - Solo simular; 0 - Ejecutar
	@ContainPayroll BIT, --1 - Contiene el módulo de nómina, 0 - sin módulo nómina
    @DataXML XML, --Los datos anteriormente simulados para comparar con los nuevos y así saber si cambiaron o no, si cambiaron se realimentan los nuevos valores como si fuera solo simulacion
    @UserCode VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

	/*********************************************** VARIABLES GLOBALES ***********************************************/

	DECLARE @SettingsId INT,
			@CostEstimateLabor TINYINT,
			@ValidateActivities BIT,
			-----------------------
			@errors VARCHAR(MAX) = '',
			-----------------------
			@CodeMessage INT,
			@Message VARCHAR(MAX)

    DECLARE @tmpCost AS TABLE
	(
        ProductionCenterId INT,
        ProductionCenterName VARCHAR(500),
		TotalSales DECIMAL(18,2) DEFAULT 0,
		TotalSalesSecondary DECIMAL(18,2) DEFAULT 0,
        DirectCostDistribution DECIMAL(18,2) DEFAULT 0,
        AutoCostDistribution DECIMAL(18,2) DEFAULT 0,
        CostAccountingAdjustment DECIMAL(18,2) DEFAULT 0,
        ManPowerDistributionDirect DECIMAL(18,2) DEFAULT 0,
        ManPowerDistributionIndirect DECIMAL(18,2) DEFAULT 0,
		ManPowerAccountingAdjustment DECIMAL(18,2) DEFAULT 0,
        ManPowerDistributionTotal DECIMAL(18,2) DEFAULT 0, -- Suma del valor Directo e Indirecto        
        FixedAssetDistribution DECIMAL(18,2) DEFAULT 0,
        FixedAssetAccountingAdjustment DECIMAL(18,2) DEFAULT 0,
        DispensingDistribution DECIMAL(18,2) DEFAULT 0,
		DispensingAccountingAdjustment DECIMAL(18,2) DEFAULT 0,
        TransferDistribution DECIMAL(18,2) DEFAULT 0,
		TransferAccountingAdjustment DECIMAL(18,2) DEFAULT 0,
        InitialDistribution DECIMAL(18,2) DEFAULT 0,
        IntermediateDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryDirectCostDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryAutoCostDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryManPowerDistributionDirect DECIMAL(18,2) DEFAULT 0,
        SecondaryManPowerDistributionIndirect DECIMAL(18,2) DEFAULT 0,
        SecondaryFixedAssetDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryDispensingDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryTransferDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryDistribution DECIMAL(18,2) DEFAULT 0
    )

	DECLARE @tmpDistributionSecondary AS TABLE
	(
		Id INT IDENTITY(1,1),
		ProductionCenterSourceId INT,
		ProductionCenterTargetId INT,
		SecondaryDirectCostDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryAutoCostDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryManPowerDistributionDirect DECIMAL(18,2) DEFAULT 0,
        SecondaryManPowerDistributionIndirect DECIMAL(18,2) DEFAULT 0,
        SecondaryFixedAssetDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryDispensingDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryTransferDistribution DECIMAL(18,2) DEFAULT 0,
        SecondaryDistribution DECIMAL(18,2) DEFAULT 0
	)

    BEGIN TRY

		/*********************************************** OBTENCION DE DATOS GLOBALES ***********************************************/

		SELECT TOP 1
			@SettingsId = Id,
			@CostEstimateLabor = CostEstimateLabor,
			@ValidateActivities = ValidateActivities
		FROM Cost.CostSetting
		WHERE Year = @Year AND Month = @Month

		/*********************************************** VALIDACIONES GLOBALES ***********************************************/

		IF @SettingsId IS NULL
		BEGIN
			INSERT INTO @tmpCost (ProductionCenterId, ProductionCenterName) VALUES (1,'')
				SELECT *, '999' AS CodeResult, 'El periodo (' + CAST(@Year AS VARCHAR) + '/' + CAST(@Month AS VARCHAR) + ') no es el periodo actual de costos' AS MessageResult
				FROM @tmpCost
				RETURN
		END

		IF NOT (@DistributionType = 1 AND @OnlySimulate = 1)
		BEGIN
			IF EXISTS (SELECT 1 FROM GeneralLedger.ClosedMonth WHERE [Year] = @Year AND [Month] = @Month AND [Status] = 1)
			BEGIN
				INSERT INTO @tmpCost (ProductionCenterId, ProductionCenterName) VALUES (1,'')
				SELECT *, '999' AS CodeResult, 'El periodo (' + CAST(@Year AS VARCHAR) + '/' + CAST(@Month AS VARCHAR) + ') debe estar cerrado en contabilidad' AS MessageResult
				FROM @tmpCost
				RETURN
			END
		END

        IF @DistributionType = 1 -- DISTRIBUCION INICIAL 
		BEGIN
			
			/*********************************************** VARIABLES DEL TIPO ***********************************************/

			DECLARE @tmpPreviusData AS TABLE
			(
				ProductionCenterId INT,
				DirectCostDistribution NUMERIC(20,2),
				AutoCostDistribution NUMERIC(20,2),
				ManPowerDistributionDirect NUMERIC(20,2),
				ManPowerDistributionInDirect NUMERIC(20,2),
				FixedAssetDistribution NUMERIC(20,2),
				DispensingDistribution NUMERIC(20,2),
				TransferDistribution NUMERIC(20,2),
				InitialDistribution NUMERIC(20,2),
				IntermediateDistribution NUMERIC(20,2),
				SecondaryDistribution NUMERIC(20,2)
			)

			DECLARE @tmpGeneralLedgerBalance AS TABLE
			(
				ProductionCenterId INT,
				HomologationType TINYINT,
				Balance DECIMAL(18,2)
			)

			DECLARE @tmpGeneralLedgerBalanceWithAllowSecondaryDistribution AS TABLE
			(
				ProductionCenterId INT,
				Balance DECIMAL(18,2)
			)

			DECLARE @CostDistributionManpower AS TABLE
			(
				ManpowerType TINYINT, EntityId INT,
				GroupId INT, GroupCodeName VARCHAR(500),
				EmployeeId INT, ThirdPartyId INT, ThirdPartyNitName VARCHAR(500),
				PositionId INT, PositionCodeName VARCHAR(500),
				--------------------------------------
				ProductionCenterId INT, ProductionCenterCodeName VARCHAR(500),
				HoursQuantity INT DEFAULT(0),
				TotalAccrued DECIMAL(18,2) DEFAULT(0),
				TotalProvision DECIMAL(18,2) DEFAULT(0),
				TotalEmployerContribution DECIMAL(18,2) DEFAULT(0),
				TotalParafiscal DECIMAL(18,2) DEFAULT(0)
			)

			DECLARE @CostDistributionFixedAsset AS TABLE
			(
				FixedAssetPhysicalAssetId INT,
				ProductionCenterId INT,
				DepreciatedDays INT, 
				HoursQuantity INT, 
				DepreciationValue DECIMAL(18,2)
			)

			/*********************************************** CARGUE DE DATOS PREVIOS ***********************************************/

			INSERT INTO @tmpPreviusData
			SELECT
				t.x.value('ProductionCenterId[1]','INT'),
				t.x.value('DirectCostDistribution[1]','NUMERIC(20,2)'),
				t.x.value('AutoCostDistribution[1]','NUMERIC(20,2)'),
				t.x.value('ManPowerDistributionDirect[1]','NUMERIC(20,2)'),
				t.x.value('ManPowerDistributionInDirect[1]','NUMERIC(20,2)'),
				t.x.value('FixedAssetDistribution[1]','NUMERIC(20,2)'),
				t.x.value('DispensingDistribution[1]','NUMERIC(20,2)'),
				t.x.value('TransferDistribution[1]','NUMERIC(20,2)'),
				t.x.value('InitialDistribution[1]','NUMERIC(20,2)'),
				t.x.value('IntermediateDistribution[1]','NUMERIC(20,2)'),
				t.x.value('SecondaryDistribution[1]','NUMERIC(20,2)')
			FROM @DataXML.nodes('/header/Detail') t(x)

			IF @ContainPayroll = 1 
			BEGIN
				INSERT INTO @CostDistributionManpower
					EXEC Cost.SP_GetDistributionManpower @Year, @Month, NULL, NULL
			END
			
			INSERT INTO @CostDistributionFixedAsset
				SELECT 
					fadd.FixedAssetPhysicalAssetId,
					cpccc.ProductionCenterId,
					SUM(fadd.DepreciatedDays) DepreciatedDays,
					SUM(CAST(faddc.DepreciatedDays * fal.UseTime AS INT)) HoursQuantity,
					SUM(faddc.DepreciationValue) DepreciationValue					
				FROM FixedAsset.FixedAssetDepreciation fad
				JOIN FixedAsset.FixedAssetDepreciationDetail fadd ON fad.Id = fadd.FixedAssetDepreciationId
				JOIN GeneralLedger.LegalBook lb ON fadd.LegalBookId = lb.Id AND lb.OfficialBook = 1
				JOIN FixedAsset.FixedAssetDepreciationDetailCost faddc ON fadd.Id =  faddc.FixedAssetDepreciationDetailId
				JOIN FixedAsset.FixedAssetLocation fal ON faddc.LocationId = fal.Id				
				JOIN Cost.CostProductionCenterCostCenter cpccc ON faddc.CostCenterId = cpccc.CostCenterId
				where fad.ClosingYear = @Year  AND fad.ClosingMonth = @Month AND faddc.DepreciationValue > 0
				GROUP BY fadd.FixedAssetPhysicalAssetId, cpccc.ProductionCenterId

			/*********************************************** DISTRIBUCION INICIAL ***********************************************/

			INSERT INTO @tmpCost (ProductionCenterId, ProductionCenterName)
				SELECT Id,Code+ ' - '+ Name
				FROM Cost.CostProductionCenter 
				--WHERE Status = 1

			/*************************************************** VALIDACIONES ***************************************************/
        
			IF EXISTS (SELECT 1 FROM Cost.CostEstimationNative WHERE [Year] = @Year AND [Month] = @Month AND InitialDistribution <> 0)
			BEGIN
				IF @OnlySimulate = 0
				BEGIN
					SELECT *, '999' AS CodeResult, 'La Distribucion Primaria ya fue confirmada y no puede ser reemplazada' AS MessageResult
					FROM @tmpCost
					RETURN
				END
				ELSE
				BEGIN
					DELETE FROM @tmpCost

					INSERT INTO @tmpCost
					(
						ProductionCenterId, ProductionCenterName, 
						DirectCostDistribution, AutoCostDistribution, 
						ManPowerDistributionDirect, ManPowerDistributionIndirect, ManPowerDistributionTotal,
						DispensingDistribution, TransferDistribution, 				
						FixedAssetDistribution, 
						InitialDistribution, IntermediateDistribution
					)
					SELECT 
						ProductionCenterId, pc.Code + ' - ' + pc.Name, 
						DirectCostDistribution, AutoCostDistribution, 
						ManPowerDistributionDirect, ManPowerDistributionIndirect, (ManPowerDistributionDirect + ManPowerDistributionIndirect),
						DispensingDistribution, TransferDistribution, 				
						FixedAssetDistribution, 
						InitialDistribution, IntermediateDistribution
					FROM Cost.CostEstimationNative ce
					JOIN Cost.CostProductionCenter pc ON pc.Id = ce.ProductionCenterId
					WHERE ce.Year = @Year AND ce.Month = @Month

					SELECT *, '000' as CodeResult, 'Ok' as MessageResult
					FROM @tmpCost
					ORDER BY ProductionCenterName
					RETURN
				END
            END

			-- Valido que todas las distribuciones esten confirmadas
            IF EXISTS (SELECT 1 FROM Cost.CostDistributionDirectCost WHERE [Year] = @Year AND [Month] = @Month AND [Status] = 1)
			BEGIN
                SELECT @errors=STUFF((
					SELECT N';  ' + Code
					FROM Cost.CostDistributionDirectCost where [Year] = @Year and [Month] = @Month and [Status] = 1
					FOR XML path(N''), TYPE).value(N'.[1]', N'nVARCHAR(MAX)'), 1, 2, N'')

                SELECT *, '999' as CodeResult, 'Actualmente Existen Distribuciones de Elementos del Costo sin Confirmar Codigos: ' + @errors as MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Valido que todas las distribuciones no tengan un elemento de costo inactivo
            IF EXISTS (SELECT 1 FROM Cost.CostDistributionDirectCost cdc JOIN Cost.CostGeneralExpense cge ON cdc.GeneralExpenseId = cge.Id WHERE [Year] = @Year AND [Month] = @Month AND cdc.[Status] = 2 AND cge.Status = 0)
			BEGIN
                SELECT @errors=STUFF((SELECT N';  ' + cdc.Code + ' (Elemento del Costo: ' + cge.Code + ')'
                FROM Cost.CostDistributionDirectCost cdc 
				JOIN Cost.CostGeneralExpense cge ON cdc.GeneralExpenseId = cge.Id 
				WHERE [Year] = @Year AND [Month] = @Month AND cdc.[Status] = 2 AND cge.Status = 0
                FOR XML PATH(N''), TYPE).value(N'.[1]', N'nVARCHAR(MAX)'), 1, 2, N'')

                SELECT *, '999' AS CodeResult, 'Existen Distribuciones de Elementos del Costo Confirmadas asociadas a elementos de costo inactivos: ' + @errors AS MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Valido si existen elementos con distribuciones directas y (calculadas o buscadas)
            IF EXISTS 
			(
				SELECT 1 
				FROM Cost.CostDistributionDirectCost cdc 
				JOIN Cost.CostGeneralExpense cge ON cdc.GeneralExpenseId = cge.Id
				JOIN Cost.CostDistributionBase cdbd ON cge.Id = cdbd.GeneralExpenseId AND cdbd.DistributionType = 1
				JOIN Cost.CostDistributionBase cdbc ON cge.Id = cdbc.GeneralExpenseId AND cdbc.DistributionType <> 1
				WHERE [Year] = @Year and [Month] = @Month and cdc.[Status] = 2
			)
			BEGIN
                SELECT @errors = stuff((
					SELECT DISTINCT N';  ' + cdc.Code + ' (Elemento del Costo: ' + cge.Code + ')'
					FROM Cost.CostDistributionDirectCost cdc 
					JOIN Cost.CostGeneralExpense cge ON cdc.GeneralExpenseId = cge.Id
					JOIN Cost.CostDistributionBase cdbd ON cge.Id = cdbd.GeneralExpenseId AND cdbd.DistributionType = 1
					JOIN Cost.CostDistributionBase cdbc ON cge.Id = cdbc.GeneralExpenseId AND cdbc.DistributionType <> 1
					WHERE [Year] = @Year and [Month] = @Month and cdc.[Status] = 2
					FOR XML path(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                SELECT *, '999' AS CodeResult, 'Existen Distribuciones de Elementos del Costo Confirmadas asociadas a elementos de costo con distribuciones directas y calculadas o buscadas: ' + @errors AS MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Si el modulo de nomina es integrado valido todos los grupos de nomina se encuentren liquidados
			IF @ContainPayroll = 1 
			BEGIN                 
               IF EXISTS
				(
					SELECT 1
					FROM Payroll.[Group] g
					JOIN 
					(
						SELECT 
							COUNT(1) [Count],
							GroupId,
							PayrollDateLiquidated
						FROM Payroll.Liquidation WITH (NOLOCK)
						WHERE RegisterStatus = ''
						AND YEAR(PayrollDateLiquidated) = @Year AND MONTH(PayrollDateLiquidated) = @Month
						GROUP BY GroupId, PayrollDateLiquidated
					) l ON l.GroupId = g.Id
					WHERE g.State = 1
				)
				BEGIN
                     SELECT @errors = STUFF((
							SELECT  N';  ' + g.Code
							FROM Payroll.[Group] g
							JOIN 
							(
								SELECT 
								COUNT(1) Cont,
								GroupId,
								PayrollDateLiquidated
								FROM Payroll.Liquidation WITH (NOLOCK)
								WHERE RegisterStatus = ''
									AND YEAR(PayrollDateLiquidated) = @Year AND MONTH(PayrollDateLiquidated) = @Month
								GROUP BY GroupId, PayrollDateLiquidated
							) l ON l.GroupId = g.Id
							WHERE g.State = 1
							FOR XML path(N''), TYPE).value(N'.[1]', N'nvarchar(100)'), 1, 2, N'')

                    SELECT *, '999' AS CodeResult, 'Faltan por liquidar los grupos de nomina (' + @errors  + ')' AS MessageResult
                    FROM @tmpCost
                    RETURN
                END

				IF EXISTS
				(
					SELECT 1
					FROM @CostDistributionManpower d
					LEFT JOIN Cost.CostDistributionManpower cdm ON d.ManpowerType = cdm.ManpowerType AND d.EntityId = cdm.EntityId 
						AND cdm.Year = @Year AND cdm.Month = @Month
					WHERE cdm.Id IS NULL

				)
				BEGIN
					SELECT *, '999' AS CodeResult, 'Faltan Terceros por distribución de Mano de Obra' AS MessageResult
                    FROM @tmpCost
                    return
				END
			END

			IF EXISTS
			(
				SELECT 1
				FROM @CostDistributionFixedAsset d
				LEFT JOIN Cost.CostDistributionFixedAsset cdm ON d.FixedAssetPhysicalAssetId = cdm.FixedAssetPhysicalAssetId
					AND cdm.Year = @Year AND cdm.Month = @Month
				WHERE cdm.Id IS NULL

			)
			BEGIN
				SELECT *, '999' AS CodeResult, 'Faltan Activos por distribución de Activos Fijos' AS MessageResult
                FROM @tmpCost
                return
			END

			/********************************************** INFORMACION CONTABLE ***********************************************/
			
			DECLARE @tmpCostGeneralBalance AS TABLE
			(
				ProductionCenterId INT,
				HomologationType TINYINT,
				Nature tinyint,
				DebitValue decimal(18, 2),
				CreditValue decimal(18, 2),
				AllowSecondaryDistribution bit,
				MainAccountClassCode varchar(20)
			)
			delete from @tmpCostGeneralBalance

			INSERT INTO @tmpCostGeneralBalance
				SELECT 
					ph.ProductionCenterId,
					ph.[HomologationType],
					cla.Nature,
					s.DebitValue,
					s.CreditValue,
					ph.AllowSecondaryDistribution,
					cla.Code
				FROM Cost.CostProductionCenterHomologation ph
				JOIN GeneralLedger.MainAccounts c ON ph.[AccountOriginId] = c.Id
				JOIN GeneralLedger.MainAccountClasses cla ON cla.Id = c.IdAccountClass
				JOIN GeneralLedger.GeneralLedgerBalance s ON s.IdMainAccount = c.Id AND s.Year = @Year AND s.[Month] = @Month
				JOIN Cost.CostProductionCenterCostCenter pccc ON pccc.CostCenterId = s.IdCostCenter and pccc.ProductionCenterId = ph.ProductionCenterId

			INSERT INTO @tmpGeneralLedgerBalance
				SELECT 
					tb.ProductionCenterId,
					tb.[HomologationType],
					SUM
					(
						IIF(tb.Nature = 1, (tb.DebitValue - tb.CreditValue), (tb.CreditValue - tb.DebitValue))
					) as Diference
				FROM @tmpCostGeneralBalance tb
				GROUP BY tb.ProductionCenterId, tb.[HomologationType]

			INSERT INTO @tmpGeneralLedgerBalanceWithAllowSecondaryDistribution
				SELECT 
					tb.ProductionCenterId,
					SUM
					(
						IIF(tb.Nature = 1, (tb.DebitValue - tb.CreditValue), (tb.CreditValue - tb.DebitValue))
					) as Diference
				FROM @tmpCostGeneralBalance tb
				where tb.AllowSecondaryDistribution = 1 and tb.HomologationType = 6
				GROUP BY tb.ProductionCenterId

			---- Valor de las ventas por centro de producción de acuerdo como se encuentra en contabilidad en las cuentas parametrizadas
			UPDATE c 
				SET c.TotalSales = b.Balance
            FROM @tmpCost c
            JOIN @tmpGeneralLedgerBalance b on c.ProductionCenterId = b.ProductionCenterId
            WHERE b.HomologationType = 6

			-- solo tomamos las cuentas que permitan distribucion secundaria
			UPDATE c 
					set c.TotalSalesSecondary = b.Balance
            FROM @tmpCost c
            JOIN @tmpGeneralLedgerBalanceWithAllowSecondaryDistribution b on c.ProductionCenterId = b.ProductionCenterId	

			---- Ahora comparo contra los saldos en contabilidad para realizar los ajustes por gastos generales
			UPDATE c 
				SET c.CostAccountingAdjustment = b.Balance, 
					c.AutoCostDistribution = b.Balance
            FROM @tmpCost c
            JOIN @tmpGeneralLedgerBalance b on c.ProductionCenterId = b.ProductionCenterId
            WHERE b.HomologationType = 4 AND b.Balance <> (c.DirectCostDistribution + c.AutoCostDistribution)

			----- Ahora comparo contra los saldos en contabilidad para realizar los ajustes por mano de obra
			UPDATE c 
				SET c.ManPowerAccountingAdjustment = b.Balance, 
					c.ManPowerDistributionIndirect = b.Balance
            FROM @tmpCost c
            JOIN @tmpGeneralLedgerBalance b on c.ProductionCenterId = b.ProductionCenterId
            WHERE b.HomologationType = 1 AND b.Balance <> 0

			----- Ahora comparo contra los saldos en contabilidad para realizar los ajustes por suministros
			UPDATE c 
				SET c.DispensingAccountingAdjustment = b.Balance - (c.DispensingDistribution),
					c.DispensingDistribution = c.DispensingDistribution + (b.Balance - (c.DispensingDistribution))
            FROM @tmpCost c
            JOIN @tmpGeneralLedgerBalance b on c.ProductionCenterId = b.ProductionCenterId
			WHERE b.HomologationType = 2 AND b.Balance <> (c.DispensingDistribution)

			----- Ahora comparo contra los saldos en contabilidad para realizar los ajustes por consumo
			UPDATE c 
				SET c.TransferAccountingAdjustment = b.Balance - (c.TransferDistribution),
					c.TransferDistribution = c.TransferDistribution + (b.Balance - (c.TransferDistribution))
            FROM @tmpCost c
            JOIN @tmpGeneralLedgerBalance b on c.ProductionCenterId = b.ProductionCenterId
			WHERE b.HomologationType = 3 AND b.Balance <> (c.TransferDistribution)

			 ----- Ahora comparo contra los saldos en contabilidad para realizar los ajustes por activos fijos
			 UPDATE c 
				SET c.FixedAssetAccountingAdjustment = b.Balance, 
					c.FixedAssetDistribution = b.Balance
            FROM @tmpCost c
            JOIN @tmpGeneralLedgerBalance b on c.ProductionCenterId = b.ProductionCenterId
            WHERE b.HomologationType = 5 AND b.Balance <> (c.FixedAssetDistribution)   

			/***************************************** DISTRIBUCION DE GASTOS DIRECTOS *****************************************/

			-- Calculamos otros gastos de acuerdo a si es directo o si es calculada y buscada
            UPDATE c
				SET c.DirectCostDistribution = c.DirectCostDistribution + x.Directvalue,
					c.AutoCostDistribution = c.AutoCostDistribution - x.Directvalue,
					c.CostAccountingAdjustment = c.CostAccountingAdjustment - x.Directvalue
            FROM @tmpCost c
			JOIN 
			(
                SELECT
					dcd.ProductionCenterId,					
					SUM(CAST(dcd.Value AS DECIMAL(18,2))) AS Directvalue
                FROM Cost.CostDistributionDirectCost dc                
                JOIN Cost.CostDistributionDirectCostDetail dcd ON dc.Id = dcd.DistributionDirectCostId
				JOIN Cost.CostGeneralExpense ge ON dc.GeneralExpenseId = ge.Id
				JOIN
				(
					SELECT cdb.GeneralExpenseId, MIN(cdb.DistributionType) DistributionType
					FROM Cost.CostDistributionBase cdb
					GROUP BY cdb.GeneralExpenseId
				) cdb ON ge.Id = cdb.GeneralExpenseId
                WHERE dc.Year = @Year AND dc.Month = @Month AND dc.Status = 2 
					AND ge.ElementCostType = 5 AND cdb.DistributionType = 1
                GROUP BY dcd.ProductionCenterId
            ) AS x ON c.ProductionCenterId = x.ProductionCenterId
			
			/****************************************** DISTRIBUCIÓN DE MANO DE OBRA *******************************************/

			-- Si el modulo de nomina es integrado buscamos el valor a distribuir de cada una de las cuentas para el periodo en cuestion
            IF @ContainPayroll = 1 
			BEGIN
			--	--Deshacemos movimientos iniciales
			--	UPDATE c 
			--		SET c.ManPowerAccountingAdjustment = c.ManPowerAccountingAdjustment - cdm.Value, 
			--			c.ManPowerDistributionIndirect = c.ManPowerDistributionIndirect - cdm.Value
			--	FROM @tmpCost c
			--	JOIN 
			--	(
			--		SELECT cdm.ProductionCenterId,
			--			SUM
			--			(
			--				cdm.TotalAccrued +
			--				IIF(@CostEstimateLabor >= 2, cdm.TotalEmployerContribution, 0) +
			--				IIF(@CostEstimateLabor >= 3, cdm.TotalParafiscal, 0) +
			--				IIF(@CostEstimateLabor >= 4, cdm.TotalProvision, 0)
			--			) Value
			--		FROM @CostDistributionManpower cdm 
			--		GROUP BY cdm.ProductionCenterId
			--	) cdm on c.ProductionCenterId = cdm.ProductionCenterId

			--	--Ingresamos los distribuidos
   --             UPDATE c 
			--		SET c.ManPowerDistributionDirect = c.ManPowerDistributionDirect + IIF(pc.CenterType = 1, cdm.Value, 0), 
			--			c.ManPowerDistributionIndirect = c.ManPowerDistributionIndirect + IIF(pc.CenterType = 1, 0, cdm.Value)
   --             FROM @tmpCost AS c
			--	JOIN Cost.CostProductionCenter pc ON pc.Id = c.ProductionCenterId
			--	JOIN
			--	(
			--		SELECT cdmd.ProductionCenterId,
			--			SUM
			--			(
			--				cdmd.TotalAccrued +
			--				IIF(@CostEstimateLabor >= 2, cdmd.TotalEmployerContribution, 0) +
			--				IIF(@CostEstimateLabor >= 3, cdmd.TotalParafiscal, 0) +
			--				IIF(@CostEstimateLabor >= 4, cdmd.TotalProvision, 0)
			--			) Value
			--		FROM Cost.CostDistributionManpower cdm
   --                 JOIN Cost.CostDistributionManpowerDetail cdmd ON cdm.Id = cdmd.DistributionManpowerId
			--		WHERE cdm.Year = @Year AND cdm.Month = @Month --AND cdm.Status = 1
			--		GROUP BY cdmd.ProductionCenterId
   --             ) cdm ON c.ProductionCenterId = cdm.ProductionCenterId

				IF @OnlySimulate = 0
				BEGIN
					INSERT INTO [Cost].[CostDistributionManpowerInitial]
						SELECT @Year, @Month, ManpowerType, EntityId, GroupId, EmployeeId, ThirdPartyId, PositionId, ProductionCenterId, HoursQuantity, TotalAccrued, TotalProvision, TotalEmployerContribution, TotalParafiscal
						FROM @CostDistributionManpower
				END 
            END

			--/****************************************** DISTRIBUCION DE ACTIVOS FIJOS ******************************************/

			----Deshacemos movimientos iniciales
			--UPDATE c 
			--	SET c.FixedAssetAccountingAdjustment = c.FixedAssetAccountingAdjustment - cdm.Value, 
			--		c.FixedAssetDistribution = c.FixedAssetDistribution - cdm.Value
			--FROM @tmpCost c
			--JOIN 
			--(
			--	SELECT cdm.ProductionCenterId, SUM(cdm.DepreciationValue) Value
			--	FROM @CostDistributionFixedAsset cdm 
			--	GROUP BY cdm.ProductionCenterId
			--) cdm on c.ProductionCenterId = cdm.ProductionCenterId

   --         UPDATE c 
			--	SET c.FixedAssetDistribution = c.FixedAssetDistribution + x.DeprecationValue
   --         FROM @tmpCost AS c
			--JOIN 
			--(
   --             SELECT fd.ProductionCenterId, SUM(fd.DepreciationValue) AS DeprecationValue
   --             FROM Cost.CostDistributionFixedAsset f
   --             JOIN Cost.CostDistributionFixedAssetDetail fd ON fd.DistributionFixedAssetId = f.Id
   --             WHERE f.Year = @Year AND f.Month = @Month --AND f.Status = 1
   --             GROUP BY fd.ProductionCenterId
   --         ) AS x ON c.ProductionCenterId = x.ProductionCenterId

			IF @OnlySimulate = 0
			BEGIN
				INSERT INTO [Cost].[CostDistributionFixedAssetInitial]
					SELECT @Year, @Month, FixedAssetPhysicalAssetId, ProductionCenterId, DepreciatedDays, HoursQuantity, DepreciationValue
					FROM @CostDistributionFixedAsset
			END 

			/***************************** INSERTAR O ACTUALIZAR REGISTROS ***************************/

            UPDATE c 
				SET c.InitialDistribution = 
					ISNULL(c.DirectCostDistribution, 0) + 
					ISNULL(c.AutoCostDistribution, 0) + 
					ISNULL(c.ManPowerDistributionDirect, 0) + 
					ISNULL(c.ManPowerDistributionInDirect, 0) + 
					ISNULL(c.DispensingDistribution, 0) + 
					ISNULL(c.TransferDistribution, 0) + 
					ISNULL(c.FixedAssetDistribution, 0), 
					c.ManPowerDistributionTotal = ISNULL(c.ManPowerDistributionDirect, 0) + ISNULL(c.ManPowerDistributionInDirect, 0)
			FROM @tmpCost AS c

			IF @OnlySimulate = 1
			BEGIN
                --Comparamos los nuevos datos con los datos previos
                IF EXISTS
				(
					SELECT 1
					FROM @tmpCost c
					JOIN @tmpPreviusData p ON c.ProductionCenterId = p.ProductionCenterId
					WHERE c.DirectCostDistribution <> p.DirectCostDistribution 
						OR c.AutoCostDistribution <> p.AutoCostDistribution 
						OR c.ManPowerDistributionDirect <> p.ManPowerDistributionDirect 
						OR c.ManPowerDistributionIndirect <> p.ManPowerDistributionInDirect 
						OR c.DispensingDistribution <> p.DispensingDistribution 
						OR c.TransferDistribution <> p.TransferDistribution 
						OR c.FixedAssetDistribution <> p.FixedAssetDistribution 
				)
				BEGIN
                    SELECT *, '999' as CodeResult, 'Los registros han cambiado, vuelva a verificar los datos antes de confirmar' as MessageResult
                    FROM @tmpCost
                    RETURN
                END
            END
            ELSE 
			BEGIN
				UPDATE cen
					SET cen.TotalSales = c.TotalSales,
						cen.ManPowerDistributionDirect = c.ManPowerDistributionDirect, 
						cen.ManPowerDistributionInDirect = c.ManPowerDistributionIndirect, 
						cen.ManPowerAccountingAdjustment = c.ManPowerAccountingAdjustment,
						cen.DispensingDistribution = c.DispensingDistribution, 
						cen.DispensingAccountingAdjustment = c.DispensingAccountingAdjustment,
						cen.TransferDistribution = c.TransferDistribution, 
						cen.TransferAccountingAdjustment = c.TransferAccountingAdjustment,
						cen.DirectCostDistribution = c.DirectCostDistribution, 
						cen.AutoCostDistribution = c.AutoCostDistribution, 
						cen.CostAccountingAdjustment = c.CostAccountingAdjustment,
						cen.FixedAssetDistribution = c.FixedAssetDistribution, 
						cen.FixedAssetAccountingAdjustment = c.FixedAssetAccountingAdjustment,
						cen.InitialDistribution = c.InitialDistribution, 
						cen.IntermediateDistribution = c.IntermediateDistribution, 
						cen.SecondaryDistribution = c.SecondaryDistribution, 
						cen.ModificationUser = @UserCode, 
						cen.ModificationDate = [Common].[GETDATE](),
						cen.TotalSalesSecondary = c.TotalSalesSecondary
                FROM @tmpCost c
				JOIN Cost.CostEstimationNative cen ON @Year = cen.Year AND @Month = cen.Month AND c.ProductionCenterId = cen.ProductionCenterId

                INSERT INTO Cost.CostEstimationNative 
				(
					Year,Month,ProductionCenterId, TotalSales,
					ManPowerDistributionDirect,ManPowerDistributionInDirect,ManPowerAccountingAdjustment,	--Mano de Obra
					DispensingDistribution,DispensingAccountingAdjustment,									--Dispensacion
					TransferDistribution,TransferAccountingAdjustment,										--Consumo
					DirectCostDistribution,AutoCostDistribution,CostAccountingAdjustment,					--Gastos Directos y Variables
					FixedAssetDistribution,FixedAssetAccountingAdjustment,									--Activos Fijos
					InitialDistribution,IntermediateDistribution,SecondaryDistribution,
					CreationUser,CreationDate,TotalSalesSecondary
				)
                SELECT 
					@Year, @Month, c.ProductionCenterId, c.TotalSales,
					c.ManPowerDistributionDirect, c.ManPowerDistributionIndirect, c.ManPowerAccountingAdjustment,
					c.DispensingDistribution, c.DispensingAccountingAdjustment,
					c.TransferDistribution, c.TransferAccountingAdjustment,
					c.DirectCostDistribution, c.AutoCostDistribution, c.CostAccountingAdjustment,
					c.FixedAssetDistribution, c.FixedAssetAccountingAdjustment,
					c.InitialDistribution, c.IntermediateDistribution, c.SecondaryDistribution, 
					@UserCode, [Common].[GETDATE](), c.TotalSalesSecondary
                FROM @tmpCost c
				LEFT JOIN Cost.CostEstimationNative  cen ON @Year = cen.Year AND @Month = cen.Month AND c.ProductionCenterId = cen.ProductionCenterId
				WHERE cen.Id IS NULL
            END 
        END
        ELSE IF @DistributionType = 2 -- DISTRIBUCION SECUNDARIA
		BEGIN 

			/*********************************************** CARGUE DE DATOS PREVIOS ***********************************************/
            
			INSERT INTO @tmpCost
			(
				ProductionCenterId, ProductionCenterName, 
				-----------------------------------------------------------------------------------
				DirectCostDistribution, AutoCostDistribution, 
				ManPowerDistributionDirect, ManPowerDistributionIndirect, ManPowerDistributionTotal,
				DispensingDistribution, TransferDistribution, 				
				FixedAssetDistribution, 
				-----------------------------------------------------------------------------------
				SecondaryDirectCostDistribution, SecondaryAutoCostDistribution,
				SecondaryManPowerDistributionDirect, SecondaryManPowerDistributionIndirect,
				SecondaryDispensingDistribution, SecondaryTransferDistribution,
				SecondaryFixedAssetDistribution,
				-----------------------------------------------------------------------------------
				InitialDistribution, SecondaryDistribution, IntermediateDistribution
			)
            SELECT 
				ce.ProductionCenterId, pc.Code + ' - ' + pc.[Name], 
				ce.DirectCostDistribution, ce.AutoCostDistribution, 
				ce.ManPowerDistributionDirect, ce.ManPowerDistributionInDirect, (ce.ManPowerDistributionDirect + ce.ManPowerDistributionInDirect),
				ce.DispensingDistribution, ce.TransferDistribution, 				
				ce.FixedAssetDistribution, 
				-----------------------------------------------------------------------------------
				ce.SecondaryDirectCostDistribution, ce.SecondaryAutoCostDistribution,
				ce.SecondaryManPowerDistributionDirect, ce.SecondaryManPowerDistributionInDirect,
				ce.SecondaryDispensingDistribution, ce.SecondaryTransferDistribution,
				ce.SecondaryFixedAssetDistribution,
				-----------------------------------------------------------------------------------
				ce.InitialDistribution, ce.SecondaryDistribution, ce.IntermediateDistribution
            FROM Cost.CostEstimationNative ce
            JOIN Cost.CostProductionCenter pc ON pc.Id = ce.ProductionCenterId
            WHERE ce.[Year] = @Year AND ce.[Month] = @Month

			/*************************************************** VALIDACIONES ***************************************************/

			-- Valido que exista una distribucion inicial
			IF NOT EXISTS (SELECT 1 FROM @tmpCost)
			BEGIN
                INSERT INTO @tmpCost (ProductionCenterId, ProductionCenterName) VALUES (1,'')

                SELECT *, '999' as CodeResult, 'No existe una distribucion inicial' as MessageResult
                FROM @tmpCost
                RETURN
            END

			IF EXISTS (SELECT 1 FROM Cost.CostEstimationNative WHERE [Year] = @Year AND [Month] = @Month AND SecondaryDistribution <> 0)
			BEGIN
				IF @OnlySimulate = 0
				BEGIN
					SELECT *, '999' AS CodeResult, 'La Distribucion Secundaria ya fue confirmada y no puede ser reemplazada' AS MessageResult
					FROM @tmpCost
					RETURN
				END
				ELSE
				BEGIN
					SELECT *, '000' as CodeResult, 'Ok' as MessageResult
					FROM @tmpCost
					WHERE SecondaryDistribution <> 0
					ORDER BY ProductionCenterName
					RETURN
				END
            END

			-- Valido que todas las distribuciones secundarias del periodo esten confirmadas
            IF EXISTS
			(
				SELECT 1
				FROM Cost.CostDirectDistributionSecondary cdds
				WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 1
			)
			BEGIN
                SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cdds.Code + ', '
					FROM Cost.CostDirectDistributionSecondary cdds
					WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                SELECT *, '999' as CodeResult, 'Las siguientes distribuciones Secundarias no esta confirmadas '+ cast(@Month as VARCHAR(10)) +': ' +  CHAR(13) + CHAR(10) + @errors as MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Valido que se haya distribuido todos los centros no operativos
            IF EXISTS
			(
				SELECT 1
				FROM Cost.CostEstimationNative cen
				JOIN Cost.CostProductionCenter cpc ON cen.ProductionCenterId = cpc.Id AND cpc.CenterType <> 1
				LEFT JOIN
				(
					SELECT cds.ProductionCenterId
					FROM Cost.CostDistributionSecondary cds 
					JOIN Cost.CostDirectDistributionSecondary cdds ON cds.Id = cdds.DistributionSecondaryId 
					WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2
					GROUP BY cds.ProductionCenterId
				) cds ON cen.ProductionCenterId = cds.ProductionCenterId
				WHERE cen.Year = @Year AND cen.Month = @Month AND cen.InitialDistribution <> 0 AND cds.ProductionCenterId IS NULL
			)
			BEGIN
                SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + CONCAT(cpc.Code, ' - ', cpc.Name) + ', '
					FROM Cost.CostEstimationNative cen
					JOIN Cost.CostProductionCenter cpc ON cen.ProductionCenterId = cpc.Id AND cpc.CenterType <> 1
					LEFT JOIN
					(
						SELECT cds.ProductionCenterId
						FROM Cost.CostDistributionSecondary cds 
						JOIN Cost.CostDirectDistributionSecondary cdds ON cds.Id = cdds.DistributionSecondaryId 
						WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2
						GROUP BY cds.ProductionCenterId
					) cds ON cen.ProductionCenterId = cds.ProductionCenterId
					WHERE cen.Year = @Year AND cen.Month = @Month AND cen.InitialDistribution <> 0 AND cds.ProductionCenterId IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                SELECT *, '999' as CodeResult, 'Las siguientes centros de producción no tienen una distribución secundaria: ' +  CHAR(13) + CHAR(10) + @errors as MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Valido que no existan distribuciones secundarias duplicadas (Mismo centro de distribucion)
            IF EXISTS
			(
				SELECT 1
				FROM Cost.CostDirectDistributionSecondary cdds
				JOIN Cost.CostDistributionSecondary cds ON cdds.DistributionSecondaryId = cds.Id
				WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2
				GROUP BY cds.ProductionCenterId
				HAVING COUNT(*) > 1
			)
			BEGIN
                SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - ' + CONCAT(cpc.Code, ' - ', cpc.Name, ': ') + STUFF
						(
							(
								SELECT ', ' + dds.Code
								FROM Cost.CostDirectDistributionSecondary dds
								JOIN Cost.CostDistributionSecondary ds ON dds.DistributionSecondaryId = ds.Id 
								WHERE ds.ProductionCenterId = cpc.Id
									AND dds.Year = @Year AND dds.Month = @Month AND dds.Status = 2
								FOR XML PATH(''),TYPE
							).value('(./text())[1]','VARCHAR(MAX)'), 1, 2,''
						) AS Distributions
					FROM
					(
						SELECT 
							cpc.Id, cpc.Code, cpc.Name
						FROM Cost.CostDirectDistributionSecondary cdds
						JOIN Cost.CostDistributionSecondary cds ON cdds.DistributionSecondaryId = cds.Id
						JOIN Cost.CostProductionCenter cpc ON cds.ProductionCenterId = cpc.Id
						WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2
						GROUP BY cpc.Id, cpc.Code, cpc.Name
						HAVING COUNT(*) > 1
					) cpc
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                SELECT *, '999' as CodeResult, 'Los siguientes centros de producción tienen más de una distribución secundaria '+ cast(@Month as VARCHAR(10)) +': ' +  CHAR(13) + CHAR(10) + @errors as MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Valido que todos la distribuciones secundarias se hayan realizado en centros de produccion operativo
            IF EXISTS
			(
				SELECT 1
				FROM Cost.CostDirectDistributionSecondary cdds
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
				LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
				LEFT JOIN Cost.CostProductionCenter cpc ON ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId) = cpc.Id	
				WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2 AND ISNULL(cpc.CenterType, 2) <> 1 AND cddsd.Value <> 0
			)
			BEGIN
                SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cdds.Code + ' (Centro de Produccion: ' + cpc.Code + '), '
					FROM Cost.CostDirectDistributionSecondary cdds
					JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
					LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
					LEFT JOIN Cost.CostProductionCenter cpc ON ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId) = cpc.Id				
					WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2 AND ISNULL(cpc.CenterType, 2) <> 1 AND cddsd.Value <> 0
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                SELECT *, '999' as CodeResult, 'Las siguientes Distribuciones Secundarias estan distribuidas en centros no operativos : ' +  CHAR(13) + CHAR(10) + @errors as MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Valido que todos los centros de distribucion distribuidos tengan una distribucion primaria
            IF EXISTS
			(
				SELECT 1
				FROM Cost.CostDistributionSecondary cds
				JOIN Cost.CostDirectDistributionSecondary cdds ON cds.Id = cdds.DistributionSecondaryId
				LEFT JOIN @tmpCost c ON cds.ProductionCenterId = c.ProductionCenterId AND c.InitialDistribution <> 0
				WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2 AND c.ProductionCenterId IS NULL
			)
			BEGIN
                SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cdds.Code + ', '
					FROM Cost.CostDistributionSecondary cds
					JOIN Cost.CostDirectDistributionSecondary cdds ON cds.Id = cdds.DistributionSecondaryId
					LEFT JOIN @tmpCost c ON cds.ProductionCenterId = c.ProductionCenterId AND c.InitialDistribution <> 0
					WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2 AND c.ProductionCenterId IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                SELECT *, '999' as CodeResult, 'Las siguientes Distribuciones Secundarias no tienen distribucion primaria: ' +  CHAR(13) + CHAR(10) + @errors as MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Valido que se haya distribuido completamenta los costos en los centros operativos
            IF EXISTS
			(
				SELECT 1
				FROM Cost.CostDirectDistributionSecondary cdds
				LEFT JOIN
				(
					SELECT cddsd.DirectDistributionSecondaryId, SUM(ISNULL(cddsdr.Value, cddsd.Value)) Value
					FROM Cost.CostDirectDistributionSecondaryDetail cddsd
					LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
					GROUP BY cddsd.DirectDistributionSecondaryId
				) cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
				WHERE cdds.Year = @Year AND cdds.Month = @Month 
					AND cdds.Status = 2 
					AND cdds.Value <> ISNULL(cdds.Value, 0)
			)
			BEGIN
                SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cdds.Code + ', '
					FROM Cost.CostDirectDistributionSecondary cdds
					LEFT JOIN
					(
						SELECT cddsd.DirectDistributionSecondaryId, SUM(ISNULL(cddsdr.Value, cddsd.Value)) Value
						FROM Cost.CostDirectDistributionSecondaryDetail cddsd
						LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
						GROUP BY cddsd.DirectDistributionSecondaryId
					) cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
					WHERE cdds.Year = @Year AND cdds.Month = @Month 
						AND cdds.Status = 2 
						AND cdds.Value <> ISNULL(cdds.Value, 0)
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                SELECT *, '999' as CodeResult, 'Las siguientes Distribuciones Secundarias no fueron realizadas por el total de la distribución Primaria: ' +  CHAR(13) + CHAR(10) + @errors as MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Valido que se haya distribuido completamenta los costos de los centros administrativos y logisticos
            IF EXISTS
			(
				SELECT 1
				FROM 
				(
					SELECT cen.ProductionCenterId, SUM(cen.InitialDistribution) Value
					FROM Cost.CostEstimationNative cen
					JOIN Cost.CostProductionCenter cpc ON cen.ProductionCenterId = cpc.Id
					WHERE cen.Year = @Year AND cen.Month = @Month
						AND cpc.CenterType <> 1
					GROUP BY cen.ProductionCenterId
				) cen
				FULL JOIN
				(
					SELECT cds.ProductionCenterId, SUM(cdds.Value) Value
					FROM Cost.CostDistributionSecondary cds
					JOIN Cost.CostDirectDistributionSecondary cdds ON cds.Id = cdds.DistributionSecondaryId
					WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2 
					GROUP BY cds.ProductionCenterId
				) cdds ON cen.ProductionCenterId = cdds.ProductionCenterId
				WHERE cdds.Value <> ISNULL(cdds.Value, 0)
			)
			BEGIN
                SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cpc.Code + ': Estimado (' + CAST(cen.EstimationValue AS VARCHAR(50)) + ') - Distribuido (' + CAST(cen.DistributionValue AS VARCHAR(50)) + '), '
					FROM Cost.CostProductionCenter cpc
					JOIN
					(
						SELECT 
							ISNULL(cen.ProductionCenterId, cdds.ProductionCenterId) ProductionCenterId,
							ISNULL(cen.Value, 0) EstimationValue,
							ISNULL(cdds.Value, 0) DistributionValue
						FROM 
						(
							SELECT cen.ProductionCenterId, SUM(cen.InitialDistribution) Value
							FROM Cost.CostEstimationNative cen
							JOIN Cost.CostProductionCenter cpc ON cen.ProductionCenterId = cpc.Id
							WHERE cen.Year = @Year AND cen.Month = @Month
								AND cpc.CenterType <> 1
							GROUP BY cen.ProductionCenterId
						) cen
						FULL JOIN
						(
							SELECT cds.ProductionCenterId, SUM(cdds.Value) Value
							FROM Cost.CostDistributionSecondary cds
							JOIN Cost.CostDirectDistributionSecondary cdds ON cds.Id = cdds.DistributionSecondaryId
							WHERE cdds.Year = @Year AND cdds.Month = @Month AND cdds.Status = 2 
							GROUP BY cds.ProductionCenterId
						) cdds ON cen.ProductionCenterId = cdds.ProductionCenterId
						WHERE cdds.Value <> ISNULL(cdds.Value, 0)
					) cen ON cpc.Id = cen.ProductionCenterId
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

                SELECT *, '999' as CodeResult, 'Las siguientes Distribuciones Secundarias no fueron realizadas por el total de la distribución Primaria: ' +  CHAR(13) + CHAR(10) + @errors as MessageResult
                FROM @tmpCost
                RETURN
            END

			/*************************************************** DISTRIBUCION ***************************************************/
			INSERT INTO @tmpDistributionSecondary
				SELECT	ceno.ProductionCenterId, cend.ProductionCenterId,
						iif(ceno.InitialDistribution = 0, 0, FLOOR(ceno.DirectCostDistribution * ISNULL(cddsdr.[Value], cddsd.[Value]) / ceno.InitialDistribution * 100) / 100),
						iif(ceno.InitialDistribution = 0, 0, FLOOR(ceno.AutoCostDistribution * ISNULL(cddsdr.[Value], cddsd.[Value]) / ceno.InitialDistribution * 100) / 100),
						iif(ceno.InitialDistribution = 0, 0, FLOOR(ceno.ManPowerDistributionDirect * ISNULL(cddsdr.[Value], cddsd.[Value]) / ceno.InitialDistribution * 100) / 100),
						iif(ceno.InitialDistribution = 0, 0, FLOOR(ceno.ManPowerDistributionIndirect * ISNULL(cddsdr.[Value], cddsd.[Value]) / ceno.InitialDistribution * 100) / 100),
						iif(ceno.InitialDistribution = 0, 0, FLOOR(ceno.FixedAssetDistribution * ISNULL(cddsdr.[Value], cddsd.[Value]) / ceno.InitialDistribution * 100) / 100),
						iif(ceno.InitialDistribution = 0, 0, FLOOR(ceno.DispensingDistribution * ISNULL(cddsdr.[Value], cddsd.[Value]) / ceno.InitialDistribution * 100) / 100),
						iif(ceno.InitialDistribution = 0, 0, FLOOR(ceno.TransferDistribution * ISNULL(cddsdr.[Value], cddsd.[Value]) / ceno.InitialDistribution * 100) / 100),
						ISNULL(cddsdr.[Value], cddsd.[Value])
				FROM @tmpCost ceno
				JOIN Cost.CostDistributionSecondary cds ON ceno.ProductionCenterId = cds.ProductionCenterId AND cds.[Status] = 1
				JOIN Cost.CostDirectDistributionSecondary cdds ON cds.Id = cdds.DistributionSecondaryId AND cdds.[Status] = 2 AND cdds.Year = @Year AND cdds.Month = @Month
				JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
				LEFT JOIN Cost.CostDirectDistributionSecondaryDetailRedistribution cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
				JOIN @tmpCost cend ON ISNULL(cddsdr.ProductionCenterId, cddsd.ProductionCenterId) = cend.ProductionCenterId

			UPDATE @tmpDistributionSecondary
				SET SecondaryDistribution = SecondaryDistribution - 
					(SecondaryDirectCostDistribution + SecondaryAutoCostDistribution + SecondaryManPowerDistributionDirect + SecondaryManPowerDistributionIndirect + SecondaryFixedAssetDistribution + SecondaryDispensingDistribution + SecondaryTransferDistribution)

			------------------------------------------------------------------------------------------------------------------------

			DECLARE @tmpDistributionSecondaryRows INT = 1,
					@tmpDistributionSecondaryId INT = 0,
					@ProductionCenterSourceId INT,
					@ProductionCenterTargetId INT,
					@Difference DECIMAL(18,2),
					@AdjustedValue DECIMAL(18,2),
					@DistributionSource DECIMAL(18,2)

			WHILE @tmpDistributionSecondaryRows > 0
			BEGIN
				SELECT TOP 1
					@tmpDistributionSecondaryId = t.Id,
					-------------------------------
					@ProductionCenterSourceId = t.ProductionCenterSourceId,
					@ProductionCenterTargetId = t.ProductionCenterTargetId,
					@Difference = t.SecondaryDistribution,
					@AdjustedValue = 0
				FROM @tmpDistributionSecondary t
				WHERE t.SecondaryDistribution <> 0
					AND t.Id > @tmpDistributionSecondaryId
				ORDER BY t.Id

				SET @tmpDistributionSecondaryRows = @@ROWCOUNT
				IF @tmpDistributionSecondaryRows = 0 
				BEGIN
					BREAK
				END

				---------------------------------------------------------------

				SELECT @AdjustedValue = IIF
				(
					ABS(@Difference) > ABS((ceno.DirectCostDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0))),
					(ceno.DirectCostDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0)),
					@Difference
				)
				FROM @tmpCost ceno
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryDirectCostDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterSourceId = @ProductionCenterSourceId
				) minus ON ceno.ProductionCenterId = minus.ProductionCenterId
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryDirectCostDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterTargetId = @ProductionCenterSourceId
				) more ON ceno.ProductionCenterId = more.ProductionCenterId
				WHERE ceno.ProductionCenterId = @ProductionCenterSourceId

				UPDATE @tmpDistributionSecondary
					SET SecondaryDirectCostDistribution += @AdjustedValue,
						SecondaryDistribution -= @AdjustedValue
				WHERE Id = @tmpDistributionSecondaryId

				SET @Difference -= @AdjustedValue

				-----------------------------------------------------------------

				SELECT @AdjustedValue = IIF
				(
					ABS(@Difference) > ABS((ceno.AutoCostDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0))),
					(ceno.AutoCostDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0)),
					@Difference
				)
				FROM @tmpCost ceno
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryAutoCostDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterSourceId = @ProductionCenterSourceId
				) minus ON ceno.ProductionCenterId = minus.ProductionCenterId
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryAutoCostDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterTargetId = @ProductionCenterSourceId
				) more ON ceno.ProductionCenterId = more.ProductionCenterId
				WHERE ceno.ProductionCenterId = @ProductionCenterSourceId

				UPDATE @tmpDistributionSecondary
					SET SecondaryAutoCostDistribution += @AdjustedValue,
						SecondaryDistribution -= @AdjustedValue
				WHERE Id = @tmpDistributionSecondaryId

				SET @Difference -= @AdjustedValue

				-----------------------------------------------------------------

				SELECT @AdjustedValue = IIF
				(
					ABS(@Difference) > ABS((ceno.ManPowerDistributionDirect - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0))),
					(ceno.ManPowerDistributionDirect - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0)),
					@Difference
				)
				FROM @tmpCost ceno
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryManPowerDistributionDirect) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterSourceId = @ProductionCenterSourceId
				) minus ON ceno.ProductionCenterId = minus.ProductionCenterId
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryManPowerDistributionDirect) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterTargetId = @ProductionCenterSourceId
				) more ON ceno.ProductionCenterId = more.ProductionCenterId
				WHERE ceno.ProductionCenterId = @ProductionCenterSourceId

				UPDATE @tmpDistributionSecondary
					SET SecondaryManPowerDistributionDirect += @AdjustedValue,
						SecondaryDistribution -= @AdjustedValue
				WHERE Id = @tmpDistributionSecondaryId

				SET @Difference -= @AdjustedValue

				-----------------------------------------------------------------

				SELECT @AdjustedValue = IIF
				(
					ABS(@Difference) > ABS((ceno.ManPowerDistributionIndirect - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0))),
					(ceno.ManPowerDistributionIndirect - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0)),
					@Difference
				)
				FROM @tmpCost ceno
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryManPowerDistributionIndirect) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterSourceId = @ProductionCenterSourceId
				) minus ON ceno.ProductionCenterId = minus.ProductionCenterId
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryManPowerDistributionIndirect) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterTargetId = @ProductionCenterSourceId
				) more ON ceno.ProductionCenterId = more.ProductionCenterId
				WHERE ceno.ProductionCenterId = @ProductionCenterSourceId

				UPDATE @tmpDistributionSecondary
					SET SecondaryManPowerDistributionIndirect += @AdjustedValue,
						SecondaryDistribution -= @AdjustedValue
				WHERE Id = @tmpDistributionSecondaryId

				SET @Difference -= @AdjustedValue

				-----------------------------------------------------------------

				SELECT @AdjustedValue = IIF
				(
					ABS(@Difference) > ABS((ceno.FixedAssetDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0))),
					(ceno.FixedAssetDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0)),
					@Difference
				)
				FROM @tmpCost ceno
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryFixedAssetDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterSourceId = @ProductionCenterSourceId
				) minus ON ceno.ProductionCenterId = minus.ProductionCenterId
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryFixedAssetDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterTargetId = @ProductionCenterSourceId
				) more ON ceno.ProductionCenterId = more.ProductionCenterId
				WHERE ceno.ProductionCenterId = @ProductionCenterSourceId

				UPDATE @tmpDistributionSecondary
					SET SecondaryFixedAssetDistribution += @AdjustedValue,
						SecondaryDistribution -= @AdjustedValue
				WHERE Id = @tmpDistributionSecondaryId

				SET @Difference -= @AdjustedValue

				-----------------------------------------------------------------

				SELECT @AdjustedValue = IIF
				(
					ABS(@Difference) > ABS((ceno.DispensingDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0))),
					(ceno.DispensingDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0)),
					@Difference
				)
				FROM @tmpCost ceno
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryDispensingDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterSourceId = @ProductionCenterSourceId
				) minus ON ceno.ProductionCenterId = minus.ProductionCenterId
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryDispensingDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterTargetId = @ProductionCenterSourceId
				) more ON ceno.ProductionCenterId = more.ProductionCenterId
				WHERE ceno.ProductionCenterId = @ProductionCenterSourceId

				UPDATE @tmpDistributionSecondary
					SET SecondaryDispensingDistribution += @AdjustedValue,
						SecondaryDistribution -= @AdjustedValue
				WHERE Id = @tmpDistributionSecondaryId

				SET @Difference -= @AdjustedValue

				-----------------------------------------------------------------

				SELECT @AdjustedValue = IIF
				(
					ABS(@Difference) > ABS((ceno.TransferDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0))),
					(ceno.TransferDistribution - ISNULL(minus.CostDistribution, 0) + ISNULL(more.CostDistribution, 0)),
					@Difference
				)
				FROM @tmpCost ceno
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryTransferDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterSourceId = @ProductionCenterSourceId
				) minus ON ceno.ProductionCenterId = minus.ProductionCenterId
				LEFT JOIN
				(
					SELECT @ProductionCenterSourceId ProductionCenterId, SUM(SecondaryTransferDistribution) CostDistribution
					FROM @tmpDistributionSecondary
					WHERE ProductionCenterTargetId = @ProductionCenterSourceId
				) more ON ceno.ProductionCenterId = more.ProductionCenterId
				WHERE ceno.ProductionCenterId = @ProductionCenterSourceId

				UPDATE @tmpDistributionSecondary
					SET SecondaryTransferDistribution += @AdjustedValue,
						SecondaryDistribution -= @AdjustedValue
				WHERE Id = @tmpDistributionSecondaryId

				SET @Difference -= @AdjustedValue
			END

		------------------------------------------------------------------------------------------------------------------------		

			UPDATE cen 
				SET cen.SecondaryDirectCostDistribution -= t.SecondaryDirectCostDistribution,
					cen.SecondaryAutoCostDistribution -= t.SecondaryAutoCostDistribution,
					cen.SecondaryManPowerDistributionDirect -= t.SecondaryManPowerDistributionDirect,
					cen.SecondaryManPowerDistributionIndirect -= t.SecondaryManPowerDistributionIndirect,
					cen.SecondaryFixedAssetDistribution -= t.SecondaryFixedAssetDistribution,
					cen.SecondaryDispensingDistribution -= t.SecondaryDispensingDistribution,
					cen.SecondaryTransferDistribution -= t.SecondaryTransferDistribution
            FROM @tmpCost cen
			JOIN 
			(
				SELECT	ProductionCenterSourceId ProductionCenterId,
						SUM(SecondaryDirectCostDistribution) SecondaryDirectCostDistribution,
						SUM(SecondaryAutoCostDistribution) SecondaryAutoCostDistribution,
						SUM(SecondaryManPowerDistributionDirect) SecondaryManPowerDistributionDirect,
						SUM(SecondaryManPowerDistributionIndirect) SecondaryManPowerDistributionIndirect,
						SUM(SecondaryFixedAssetDistribution) SecondaryFixedAssetDistribution,
						SUM(SecondaryDispensingDistribution) SecondaryDispensingDistribution,
						SUM(SecondaryTransferDistribution) SecondaryTransferDistribution
				FROM @tmpDistributionSecondary 
				GROUP BY ProductionCenterSourceId
			) t ON cen.ProductionCenterId = t.ProductionCenterId
			
			UPDATE cen 
				SET cen.SecondaryDirectCostDistribution += t.SecondaryDirectCostDistribution,
					cen.SecondaryAutoCostDistribution += t.SecondaryAutoCostDistribution,
					cen.SecondaryManPowerDistributionDirect += t.SecondaryManPowerDistributionDirect,
					cen.SecondaryManPowerDistributionIndirect += t.SecondaryManPowerDistributionIndirect,
					cen.SecondaryFixedAssetDistribution += t.SecondaryFixedAssetDistribution,
					cen.SecondaryDispensingDistribution += t.SecondaryDispensingDistribution,
					cen.SecondaryTransferDistribution += t.SecondaryTransferDistribution
            FROM @tmpCost cen
			JOIN 
			(
				SELECT	ProductionCenterTargetId ProductionCenterId,
						SUM(SecondaryDirectCostDistribution) SecondaryDirectCostDistribution,
						SUM(SecondaryAutoCostDistribution) SecondaryAutoCostDistribution,
						SUM(SecondaryManPowerDistributionDirect) SecondaryManPowerDistributionDirect,
						SUM(SecondaryManPowerDistributionIndirect) SecondaryManPowerDistributionIndirect,
						SUM(SecondaryFixedAssetDistribution) SecondaryFixedAssetDistribution,
						SUM(SecondaryDispensingDistribution) SecondaryDispensingDistribution,
						SUM(SecondaryTransferDistribution) SecondaryTransferDistribution
				FROM @tmpDistributionSecondary 
				GROUP BY ProductionCenterTargetId
			) t ON cen.ProductionCenterId = t.ProductionCenterId

			UPDATE cen
				SET cen.SecondaryDistribution = 
						cen.SecondaryDirectCostDistribution + 
						cen.SecondaryAutoCostDistribution + 
						cen.SecondaryManPowerDistributionDirect + 
						cen.SecondaryManPowerDistributionInDirect + 
						cen.SecondaryFixedAssetDistribution + 
						cen.SecondaryDispensingDistribution + 
						cen.SecondaryTransferDistribution + 
						cen.InitialDistribution
			FROM @tmpCost cen

			--------------------------------------------------------------------------------------

			IF @OnlySimulate = 0
			BEGIN
				update Cost.CostEstimationNative 
					set SecondaryDirectCostDistribution = t.SecondaryDirectCostDistribution,
						SecondaryAutoCostDistribution = t.SecondaryAutoCostDistribution,
						SecondaryManPowerDistributionDirect = t.SecondaryManPowerDistributionDirect,
						SecondaryManPowerDistributionInDirect = t.SecondaryManPowerDistributionInDirect,
						SecondaryFixedAssetDistribution = t.SecondaryFixedAssetDistribution,
						SecondaryDispensingDistribution = t.SecondaryDispensingDistribution,
						SecondaryTransferDistribution = t.SecondaryTransferDistribution,
						SecondaryDistribution = t.SecondaryDistribution,
						ModificationUser = @UserCode,
						ModificationDate = [Common].[GETDATE]()
				from @tmpCost t
				inner join Cost.CostEstimationNative ce on ce.ProductionCenterId = t.ProductionCenterId
				where ce.[Year] = @year And ce.[Month] = @month
			END

			--------------------------------------------------------------------------------------

			--- Elimino los centros de costos que no son operativos
            delete from @tmpCost where SecondaryDistribution = 0
        END		
		ELSE IF @DistributionType = 3 -- DISTRIBUCION FINAL
		BEGIN

			/*********************************************** VARIABLES DEL TIPO ***********************************************/

			-- Tabla temporal para almacenar los movimientos fuente -> destino de la
			-- Distribucion Intermedia confirmada del periodo.
			DECLARE @tmpDistributionIntermediate AS TABLE
			(
				ProductionCenterSourceId INT,
				ProductionCenterTargetId INT,
				Value DECIMAL(18,2) DEFAULT 0
			)
			
			/*********************************************** CARGUE DE DATOS PREVIOS ***********************************************/
            
			INSERT INTO @tmpCost
			(
				ProductionCenterId, ProductionCenterName, 
				DirectCostDistribution, AutoCostDistribution, 
				ManPowerDistributionDirect, ManPowerDistributionIndirect, ManPowerDistributionTotal,
				DispensingDistribution, TransferDistribution, 				
				FixedAssetDistribution, 
				InitialDistribution, IntermediateDistribution
			)
            SELECT 
				ProductionCenterId, pc.Code + ' - ' + pc.Name, 
				DirectCostDistribution, AutoCostDistribution, 
				ManPowerDistributionDirect, ManPowerDistributionIndirect, (ManPowerDistributionDirect + ManPowerDistributionIndirect),
				DispensingDistribution, TransferDistribution, 				
				FixedAssetDistribution, 
				InitialDistribution, IntermediateDistribution
            FROM Cost.CostEstimationNative ce
            JOIN Cost.CostProductionCenter pc ON pc.Id = ce.ProductionCenterId
            WHERE ce.Year = @Year AND ce.Month = @Month

			/*************************************************** VALIDACIONES ***************************************************/

			-- Valido que exista una distribucion inicial
			IF NOT EXISTS (SELECT 1 FROM @tmpCost)
			BEGIN
                INSERT INTO @tmpCost (ProductionCenterId, ProductionCenterName) VALUES (1,'')

                SELECT *, '999' as CodeResult, 'No existe una distribucion inicial' as MessageResult
                FROM @tmpCost
                RETURN
            END

			--Se valida que hayan hecho la estimación secundaria
			IF NOT EXISTS (SELECT 1 FROM Cost.CostEstimationNative WHERE [Year] = @Year AND [Month] = @Month AND SecondaryDistribution > 0)
			BEGIN
                SELECT *, '999' as CodeResult, 'La Distribución Secundaria no ha sido confirmada' as MessageResult
                FROM @tmpCost
                RETURN
            END

			-- Validacion: todas las distribuciones intermedias del periodo deben estar confirmadas (Status = 2).
			IF EXISTS (SELECT 1 FROM Cost.CostDistributionIntermediate WHERE [Year] = @Year AND [Month] = @Month AND [Status] <> 2)
			BEGIN
				SELECT @errors = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + cdi.Code
					FROM Cost.CostDistributionIntermediate cdi
					WHERE cdi.[Year] = @Year AND cdi.[Month] = @Month AND cdi.[Status] <> 2
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT *, '999' AS CodeResult,
					'No se han confirmado Distribuciones Intermedias asociadas a :' + CHAR(13) + CHAR(10) + @errors AS MessageResult
				FROM @tmpCost
				RETURN
			END

			-- Verificar si la Estimacion Final ya fue confirmada (IntermediateDistribution persistido).
			IF EXISTS (SELECT 1 FROM Cost.CostEstimationNative WHERE [Year] = @Year AND [Month] = @Month AND IntermediateDistribution <> 0)
			BEGIN
				IF @OnlySimulate = 0
				BEGIN
					SELECT *, '999' AS CodeResult, 'La Estimacion Final ya fue confirmada y no puede ser reemplazada' AS MessageResult
					FROM @tmpCost
					RETURN
				END
			END

			/*********************************************** CALCULO DEL EFECTO DE DISTRIBUCION INTERMEDIA ***********************************************/

			INSERT INTO @tmpDistributionIntermediate (ProductionCenterSourceId, ProductionCenterTargetId, Value)
				SELECT
					cdi.ProductionCenterId,
					cdid.ProductionCenterId,
					cdid.Value
				FROM Cost.CostDistributionIntermediate cdi
				JOIN Cost.CostDistributionIntermediateDetail cdid ON cdi.Id = cdid.DistributionIntermediateId
				WHERE cdi.[Year] = @Year AND cdi.[Month] = @Month AND cdi.[Status] = 2

			-- Enceramos para evitar acumular sobre valores viejos guardados de la simulacion o ejecucion anterior.
			UPDATE @tmpCost SET IntermediateDistribution = 0

			-- Centros fuente: se resta el total entregado por cada centro en distribuciones intermedias.
			UPDATE c
				SET c.IntermediateDistribution = c.IntermediateDistribution - t.TotalGiven
			FROM @tmpCost c
			JOIN
			(
				SELECT ProductionCenterSourceId, SUM(Value) TotalGiven
				FROM @tmpDistributionIntermediate
				GROUP BY ProductionCenterSourceId
			) t ON c.ProductionCenterId = t.ProductionCenterSourceId

			-- Centros destino: se suma el total recibido por cada centro desde distribuciones intermedias.
			UPDATE c
				SET c.IntermediateDistribution = c.IntermediateDistribution + t.TotalReceived
			FROM @tmpCost c
			JOIN
			(
				SELECT ProductionCenterTargetId, SUM(Value) TotalReceived
				FROM @tmpDistributionIntermediate
				GROUP BY ProductionCenterTargetId
			) t ON c.ProductionCenterId = t.ProductionCenterTargetId

			/******************************* PERSISTENCIA ********************************/

			-- Al confirmar: persistir IntermediateDistribution, cerrar mes y avanzar periodo en una sola transaccion.
			IF @OnlySimulate = 0
			BEGIN
				BEGIN TRAN

				-- 1. Persistir efecto neto de distribucion intermedia
				UPDATE cen
					SET cen.IntermediateDistribution = c.IntermediateDistribution,
						cen.ModificationUser = @UserCode,
						cen.ModificationDate = [Common].[GETDATE]()
				FROM @tmpCost c
				JOIN Cost.CostEstimationNative cen
					ON cen.ProductionCenterId = c.ProductionCenterId
					AND cen.[Year] = @Year AND cen.[Month] = @Month

				-- 2. Cierre del mes de costos
				DECLARE @ClosedMonthCode INT, @ClosedMonthMessage VARCHAR(MAX)

				EXEC [Cost].[SP_ClosedMonth]
					@SettingsId         = @SettingsId,
					@Year               = @Year,
					@Month              = @Month,
					@CostEstimateLabor  = @CostEstimateLabor,
					@ValidateActivities = @ValidateActivities,
					@UserCode           = @UserCode,
					@CodeMessage        = @ClosedMonthCode OUTPUT,
					@Message            = @ClosedMonthMessage OUTPUT

				IF ISNULL(@ClosedMonthCode, 999) = 999
				BEGIN
					ROLLBACK TRAN
					SELECT *, '999' AS CodeResult, @ClosedMonthMessage AS MessageResult
					FROM @tmpCost
					RETURN
				END

				-- 3. Avance al siguiente periodo en CostSetting
				UPDATE cs
					SET cs.[Month] = IIF(@Month = 12, 1, @Month + 1),
						cs.[Year] = IIF(@Month = 12, @Year + 1, @Year)
				FROM Cost.CostSetting cs
				WHERE cs.Id = @SettingsId

				COMMIT TRAN
			END
		END

        SELECT *, '000' as CodeResult, 'Ok' as MessageResult
        FROM @tmpCost
        ORDER BY ProductionCenterName

    end try
    begin catch
        SELECT *, '999' as CodeResult, error_message() + ' Linea: ' + cast(ERROR_LINE() as VARCHAR(20)) as MessageResult
        FROM @tmpCost
        order by ProductionCenterName
    end catch

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta la estimación y distribución de costos por centro de producción para un período contable (año/mes) determinado. Gestiona tres tipos de distribución de costos: primaria (inicial), secundaria e intermedia, integrando información de nómina/mano de obra, activos fijos y dispensación de medicamentos. Puede operar en modo simulación (solo calcular sin guardar) o en modo ejecución real, validando previamente que el período esté configurado en costos y cerrado en contabilidad. Compone datos de saldo contable del libro oficial, depreciación de activos fijos, distribución de mano de obra y ajustes contables por centro de producción para generar el costo estimado nativo de cada unidad o servicio de la organización.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_EstimateCostNative';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_EstimateCostNative';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Centro de costo; Distribución primaria/inicial de costos; Distribución secundaria de costos; Distribución final / cierre mensual de costos; Mano de obra directa e indirecta; Nómina (devengado, provisión, aportes patronales, parafiscales); Activos fijos y depreciación; Dispensación y consumo (suministros); Gastos directos y autocosto; Ajustes contables por homologación; Cierre contable mensual; Homologación de cuentas contables a centros de producción; Saldo contable (débito/crédito); Elementos del costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCostNative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @SettingsId IS NULL (no existe configuración de costos para el periodo) → Retorna error 999: el periodo no es el periodo actual de costos; si NOT (@DistributionType=1 AND @OnlySimulate=1) y existe ClosedMonth con Status=1 para el periodo → Retorna error 999: el periodo debe estar cerrado en contabilidad; si @DistributionType = 1 (Distribución Primaria/Inicial) → Carga datos previos del XML, ejecuta validaciones (distribuciones confirmadas, elementos activos, nómina liquidada, activos distribuidos), homologa saldos contables, calcula ajustes por gastos/mano de obra/suministros/consumo/activos, calcula InitialDistribution e inserta o actualiza en CostEstimationNative; si @DistributionType=1 y ya existe CostEstimationNative con InitialDistribution<>0 y @OnlySimulate=0 → Retorna error 999: la distribución primaria ya fue confirmada y no puede ser reemplazada else Si @OnlySimulate=1 retorna los registros existentes con código 000; si @DistributionType=1, @OnlySimulate=1 y los nuevos cálculos difieren de @tmpPreviusData (XML) → Retorna error 999: los registros han cambiado, vuelva a verificar los datos antes de confirmar; si @DistributionType = 2 (Distribución Secundaria) → Carga estimación previa, valida distribución inicial existente y secundarias confirmadas/no duplicadas/en centros operativos/totales correctos, prorratea costos desde centros fuente a centros destino y actualiza CostEstimationNative; si @DistributionType=2 y ya existe CostEstimationNative con SecondaryDistribution<>0 y @OnlySimulate=0 → Retorna error 999: la distribución secundaria ya fue confirmada y no puede ser reemplazada else Si @OnlySimulate=1 retorna los registros existentes; si @DistributionType = 3 (Distribución Final) → Valida existencia de distribución inicial y secundaria, ejecuta SP_ClosedMonth y avanza el periodo (mes/año) de CostSetting; si En distribución final, @Month = 12 → Avanza CostSetting a Mes=1 y Year=@Year+1 else Avanza Mes=@Month+1 manteniendo el mismo Year; si @ContainPayroll = 1 → Obtiene mano de obra vía SP_GetDistributionManpower, valida grupos de nómina liquidados y terceros distribuidos; si @OnlySimulate=0 inserta en CostDistributionManpowerInitial; si @OnlySimulate = 0 (Distribución 1) → Persiste en CostDistributionManpowerInitial, CostDistributionFixedAssetInitial y hace UPDATE/INSERT en CostEstimationNative else Solo simula y retorna resultados sin persistir; si Resultado de SP_ClosedMonth con @CodeMessage <> ''000'' → Retorna error 999 con el mensaje devuelto por el cierre', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCostNative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_GetDistributionManpower; Cost.SP_ClosedMonth; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCostNative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostSetting; GeneralLedger.ClosedMonth; Cost.CostEstimationNative; Cost.CostDistributionDirectCost; Cost.CostGeneralExpense; Cost.CostDistributionBase; Cost.CostDistributionDirectCostDetail; Payroll.Group; Payroll.Liquidation; Cost.CostDistributionManpower; Cost.CostDistributionFixedAsset; FixedAsset.FixedAssetDepreciation; FixedAsset.FixedAssetDepreciationDetail; GeneralLedger.LegalBook; FixedAsset.FixedAssetDepreciationDetailCost; FixedAsset.FixedAssetLocation; Cost.CostProductionCenterCostCenter; Cost.CostProductionCenter; Cost.CostProductionCenterHomologation; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; GeneralLedger.GeneralLedgerBalance; Cost.CostDirectDistributionSecondary; Cost.CostDistributionSecondary; Cost.CostDirectDistributionSecondaryDetail; Cost.CostDirectDistributionSecondaryDetailRedistribution', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCostNative';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_EstimateCostNative';
GO
