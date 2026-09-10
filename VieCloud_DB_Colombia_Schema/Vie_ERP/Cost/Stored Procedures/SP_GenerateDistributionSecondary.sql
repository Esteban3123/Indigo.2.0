-- =============================================
-- Author:         Miguel Angel Fonseca Castro
-- Create date:	   2019-09-04
-- Description:    Procedimiento almacenado para generar las distribuciones secundarias
-- =============================================
CREATE PROCEDURE [Cost].[SP_GenerateDistributionSecondary]
	@Year INT,
	@Month INT,
	@OperatingUnitId INT,
    @CodeUser VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

	BEGIN TRY

		DECLARE @errors VARCHAR(MAX),
				---------------------------------------
				@DistributionSecondaryXml XML,
				@CodeResult INT,
				@MessageResult VARCHAR(MAX)

		--Tabla temporal que contiene las estimaciones sin distribución secundaria
		DECLARE @Estimations TABLE
		(
			Id INT,
			ProductionCenterId INT,
			ProductionCenterCodeName VARCHAR(MAX),
			CostEstimationNativeValue DECIMAL(20,4)
		)

		--Tabla temporal de los detalles
		DECLARE @Details TABLE
		(
			ProductionCenterId INT,
			ProductionCenterCodeName VARCHAR(MAX),
			Percentage DECIMAL(5,2),
			Value DECIMAL(20,4)
		)

		/******************************************************** DATOS ******************************************************/

		INSERT INTO @Estimations
			SELECT cen.Id, cen.ProductionCenterId, CONCAT(cpc.Code, ' - ', cpc.Name), cen.InitialDistribution
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

		/**************************************************** VALIDACIONES ***************************************************/

		IF NOT EXISTS(SELECT 1 FROM Cost.CostSetting WHERE Year = @Year AND Month = @Month)
		BEGIN
			SELECT 999 AS CodeResult, 'El periodo (' + CAST(@Year AS VARCHAR) + '/' + CAST(@Month AS VARCHAR) + ') no es el periodo actual de costos' AS MessageResult
			RETURN
		END
		
		IF NOT EXISTS (SELECT 1 FROM Cost.CostEstimationNative WHERE Year = @Year AND Month = @Month)
		BEGIN
            SELECT 999 as CodeResult, 'No existe una distribucion inicial' as MessageResult
            RETURN
        END

		IF EXISTS (SELECT 1 FROM Cost.CostEstimationNative WHERE Year = @Year AND Month = @Month AND SecondaryDistribution <> 0)
		BEGIN
            SELECT 999 as CodeResult, 'Ya existe una distribución Secundaria' as MessageResult
            RETURN
        END

		IF NOT EXISTS (SELECT 1 FROM @Estimations)
		BEGIN
            SELECT 999 as CodeResult, 'No se encontró centros de producción pendientes por distribución Secundaria' as MessageResult
            RETURN
        END

		IF EXISTS 
		(
			SELECT 1
			FROM @Estimations cen
			LEFT JOIN 
			(
				SELECT cds.ProductionCenterId
				FROM Cost.CostDistributionSecondary cds
				JOIN Cost.CostDistributionSecondaryBase cdsb ON cds.Id = cdsb.DistributionSecondaryId AND cdsb.DistributionType IN (2, 3)
				WHERE cds.Status = 1
				GROUP BY cds.ProductionCenterId
			) cds ON cen.ProductionCenterId = cds.ProductionCenterId
			WHERE cds.ProductionCenterId IS NULL
		)
		BEGIN
			SELECT @errors = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cen.ProductionCenterCodeName + ', '
				FROM @Estimations cen
				LEFT JOIN 
				(
					SELECT cds.ProductionCenterId
					FROM Cost.CostDistributionSecondary cds
					JOIN Cost.CostDistributionSecondaryBase cdsb ON cds.Id = cdsb.DistributionSecondaryId AND cdsb.DistributionType IN (2, 3)
					WHERE cds.Status = 1
					GROUP BY cds.ProductionCenterId
				) cds ON cen.ProductionCenterId = cds.ProductionCenterId
				WHERE cds.ProductionCenterId IS NULL
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

            SELECT 999 as CodeResult, 'Los siguientes Centros de Producción no tienen un elemento de Distribución Secundaria de tipo Calculado o Buscado:' +  CHAR(13) + CHAR(10) + @errors as MessageResult
            RETURN
        END

		IF EXISTS 
		(
			SELECT 1
			FROM @Estimations cen
			JOIN 
			(
				SELECT cds.ProductionCenterId, cds.Id
				FROM Cost.CostDistributionSecondary cds
				JOIN Cost.CostDistributionSecondaryBase cdsb ON cds.Id = cdsb.DistributionSecondaryId AND cdsb.DistributionType IN (2, 3)
				WHERE cds.Status = 1
				GROUP BY cds.ProductionCenterId, cds.Id
			) cds ON cen.ProductionCenterId = cds.ProductionCenterId
			GROUP BY cen.ProductionCenterId
			HAVING COUNT(*) > 1
		)
		BEGIN
			SELECT @errors = STUFF((
				SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cen.ProductionCenterCodeName + ', '
				FROM @Estimations cen
				JOIN 
				(
					SELECT cds.ProductionCenterId, cds.Id
					FROM Cost.CostDistributionSecondary cds
					JOIN Cost.CostDistributionSecondaryBase cdsb ON cds.Id = cdsb.DistributionSecondaryId AND cdsb.DistributionType IN (2, 3)
					WHERE cds.Status = 1
					GROUP BY cds.ProductionCenterId, cds.Id
				) cds ON cen.ProductionCenterId = cds.ProductionCenterId
				GROUP BY cen.ProductionCenterId, cen.ProductionCenterCodeName
				HAVING COUNT(*) > 1
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

            SELECT 999 as CodeResult, 'Los siguientes Centros de Producción tienen mas de un elemento activo de Distribución Secundaria de tipo Calculado o Buscado:' +  CHAR(13) + CHAR(10) + @errors as MessageResult
            RETURN
        END

		/****************************************************** PROCESO ******************************************************/

		DECLARE @CostProductionCenterRows INT = 1,				
				@CostProductionCenterId INT = 0,	
				@CostEstimationNativeId INT = 0,
				@CostEstimationNativeValue DECIMAL(20,4),
				@CostDistributionSecondaryId INT,
				@CostDistributionSecondaryDescription VARCHAR(300),
				@ConcatenatedMessage VARCHAR(MAX) = ''

		WHILE @CostProductionCenterRows > 0
		BEGIN
			SELECT TOP 1 
				@CostEstimationNativeId = cen.Id,
				@CostProductionCenterId = cen.ProductionCenterId,
				@CostEstimationNativeValue = cen.CostEstimationNativeValue,
				@CostDistributionSecondaryId = NULL
			FROM @Estimations cen
			WHERE cen.ProductionCenterId > @CostProductionCenterId
			ORDER BY cen.ProductionCenterId

			SET @CostProductionCenterRows = @@ROWCOUNT
			IF @CostProductionCenterRows = 0 
			BEGIN
				BREAK
			END

			DELETE FROM @Details

			SELECT TOP 1
				@CostDistributionSecondaryId = cds.Id,
				@CostDistributionSecondaryDescription = CONCAT(cds.Code, ' - ', cds.Description)
			FROM Cost.CostDistributionSecondary cds
			JOIN Cost.CostDistributionSecondaryBase cdsb ON cds.Id = cdsb.DistributionSecondaryId AND cdsb.DistributionType IN (2, 3)	
			WHERE cds.Status = 1 AND cds.ProductionCenterId = @CostProductionCenterId

			INSERT INTO @Details
				EXEC Cost.SP_CalculateDistributionSecondary @CostDistributionSecondaryId, @Year, @Month

			IF NOT EXISTS (SELECT 1 FROM @Details)
			BEGIN
				CONTINUE
			END
						
			SELECT @DistributionSecondaryXml = CONVERT
			(
				XML, 
				(
					SELECT 
						CostDirectDistributionSecondary.*,
						CostDirectDistributionSecondaryDetail.*
					FROM
					(
						SELECT	0 Id,
								@OperatingUnitId OperatingUnitId,
								'' Code,
								@CostDistributionSecondaryId DistributionSecondaryId,
								@Year Year,
								@Month Month,
								@CostEstimationNativeId CostEstimationId,
								@CostEstimationNativeValue Value,
								@CostDistributionSecondaryDescription Description,
								2 Status
					) CostDirectDistributionSecondary
					JOIN
					( 
						SELECT
							0 DirectDistributionSecondaryId,
							d.ProductionCenterId,
							d.Percentage,
							d.Value
						FROM @Details d
					) CostDirectDistributionSecondaryDetail ON CostDirectDistributionSecondary.Id = CostDirectDistributionSecondaryDetail.DirectDistributionSecondaryId
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Cost].[SP_SaveDistributionSecondary_Output] @DistributionSecondaryXml, '', '', @CodeUser, @CodeResult OUT, @MessageResult OUT, NULL, NULL

			IF @CodeResult <> 0
			BEGIN
				SELECT @CodeResult as CodeResult, @MessageResult as MessageResult
				RETURN
			END

			SET @ConcatenatedMessage = @ConcatenatedMessage + @MessageResult + CHAR(13) + CHAR(10) 
		END

        SELECT 0 as CodeResult, @ConcatenatedMessage as MessageResult

    END TRY
    BEGIN CATCH
        SELECT 999 as CodeResult, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) as MessageResult
    END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera y registra la distribución secundaria de costos para un período (mes y año) determinado, redistribuyendo los costos acumulados en la distribución inicial entre los centros de producción según los inductores o bases de distribución secundaria configuradas (de tipo Calculado o Buscado). Valida que exista un período de costos activo, que haya una distribución inicial previa, que no se haya ejecutado ya la distribución secundaria, y que cada centro de producción tenga exactamente una base de distribución secundaria activa y válida antes de proceder. Consulta las estimaciones nativas de costos (CostEstimationNative), los centros de producción (CostProductionCenter), las definiciones de bases secundarias (CostDistributionSecondaryBase) y los registros de distribución secundaria directa (CostDirectDistributionSecondary) para calcular porcentajes y valores distribuidos, escribiendo los resultados en CostDirectDistributionSecondary y actualizando el campo de distribución secundaria en CostEstimationNative.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateDistributionSecondary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateDistributionSecondary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera automáticamente las distribuciones secundarias de costos para un período (año/mes), iterando los centros de producción pendientes y guardando cada distribución calculada.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El período (año/mes) debe estar registrado en Cost.CostSetting como período actual de costos.; Debe existir una distribución inicial (registros en Cost.CostEstimationNative) para el período.; No debe existir ya una distribución secundaria aplicada para el período (SecondaryDistribution = 0).; Deben existir centros de producción pendientes por distribuir (con InitialDistribution <> 0 y sin distribución secundaria previa en estado 2).; Cada centro de producción pendiente debe tener al menos un elemento activo de distribución secundaria de tipo Calculado o Buscado (DistributionType IN (2,3) y Status=1).; Cada centro de producción pendiente debe tener un único elemento activo de distribución secundaria de tipo Calculado o Buscado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa centros de producción cuyo CenterType <> 1 (excluye un tipo específico, presumiblemente centros finales).; Solo considera estimaciones con InitialDistribution distinta de cero.; Excluye centros que ya tienen distribución secundaria directa con Status=2 en el período.; Solo se usan elementos de distribución secundaria activos (Status=1) y de DistributionType 2 o 3 (Calculado o Buscado).; Cada distribución directa generada se persiste con Status=2 (aplicada).; No se ejecuta el proceso si ya existe una distribución secundaria para el período (idempotencia por período).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución secundaria de costos; Distribución inicial; Centro de producción / centro de costo; Estimación nativa de costos; Período de costos (año/mes); Tipos de distribución: Calculado, Buscado; Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Cost.CostDirectDistributionSecondary: Por cada centro pendiente con detalles calculados, se construye XML con Status=2 y se invoca SP_SaveDistributionSecondary_Output que persiste la distribución secundaria directa.; [RETURN_RESULT] RESULT: Retorna CodeResult=999 con mensaje específico cuando falla cualquier validación previa o cuando el SP de guardado retorna error; retorna CodeResult=0 con mensajes concatenados al finalizar exitosamente.; [RAISERROR] RESULT: En CATCH retorna CodeResult=999 con ERROR_MESSAGE() y línea del error.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe registro en Cost.CostSetting para el año/mes → Retorna error: período no es el actual de costos y termina.; si No existen registros en Cost.CostEstimationNative para el año/mes → Retorna error: no existe distribución inicial y termina.; si Existe algún CostEstimationNative con SecondaryDistribution <> 0 en el período → Retorna error: ya existe distribución secundaria y termina.; si No hay centros de producción pendientes en la tabla temporal de estimaciones → Retorna error: no hay centros pendientes y termina.; si Existen centros pendientes sin elemento de distribución secundaria de tipo Calculado o Buscado activo → Retorna error listando los centros y termina.; si Existen centros pendientes con más de un elemento activo de tipo Calculado o Buscado → Retorna error listando los centros y termina.; si Para el centro iterado, SP_CalculateDistributionSecondary no devuelve detalles → Se omite ese centro (CONTINUE) sin generar distribución.; si SP_SaveDistributionSecondary_Output retorna CodeResult <> 0 → Retorna ese código y mensaje y termina la ejecución. else Acumula el mensaje y continúa con el siguiente centro.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.SP_CalculateDistributionSecondary; Cost.SP_SaveDistributionSecondary_Output', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostEstimationNative; Cost.CostProductionCenter; Cost.CostDistributionSecondary; Cost.CostDirectDistributionSecondary; Cost.CostSetting; Cost.CostDistributionSecondaryBase', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDistributionSecondary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateDistributionSecondary';
-- GO
