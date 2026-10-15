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