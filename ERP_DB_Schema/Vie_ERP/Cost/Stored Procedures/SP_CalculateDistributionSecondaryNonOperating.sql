-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date:	2020-05-14
-- Description:	Calcula la distribución secundaria de otros centros de produccion no operativos
-- =============================================
CREATE PROCEDURE [Cost].[SP_CalculateDistributionSecondaryNonOperating]
	@CostDirectDistributionSecondaryId INT,
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	/****************************************************  VARIABLES ****************************************************/

	DECLARE @Year INT,
			@Month INT,
			--------------------------------------------
			@ProductionCenterRows INT = 1,
			@ProductionCenterId INT = 0,
			@ProductionCenterCodeName VARCHAR(200), 
			@CostDistributionSecondaryId INT,
			--------------------------------------------
			@CostDirectDistributionSecondaryDetailRows INT,
			@CostDirectDistributionSecondaryDetailId INT,
			@ValueToDistribute DECIMAL(20, 4),
			@PercentageToDistribute DECIMAL(5, 2),
			--------------------------------------------
			@Message VARCHAR(MAX)

	/**************************************************** ASIGNACIONES **************************************************/

	SELECT	@Year = cdds.Year,
			@Month = cdds.Month
	FROM Cost.CostDirectDistributionSecondary cdds
	WHERE cdds.Id = @CostDirectDistributionSecondaryId

	/**************************************************** VALIDACIONES **************************************************/
	
	-- Valido que existan centros de producción no operativos por redistribuir
	IF NOT EXISTS
	(
		SELECT 1
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
	)
	BEGIN
		SELECT	@CodeResult = 0, 
				@MessageResult = ''
		RETURN
	END

	-- Valido que los centros de producción tengan un elemento de distribución secundaria
	IF EXISTS 
	(
		SELECT 1
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		LEFT JOIN 
		(
			SELECT ProductionCenterId, COUNT(1) Quantity
			FROM Cost.CostDistributionSecondary 
			WHERE Status = 1
			GROUP BY ProductionCenterId
		) cds ON cpc.Id = cds.ProductionCenterId
		WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
			AND ISNULL(cds.Quantity, 0) <> 1
	)
	BEGIN
		SELECT @Message = STUFF((
			SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cpc.Code
			FROM Cost.CostDirectDistributionSecondary cdds
			JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
			JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
			LEFT JOIN 
			(
				SELECT ProductionCenterId, COUNT(1) Quantity
				FROM Cost.CostDistributionSecondary 
				WHERE Status = 1
				GROUP BY ProductionCenterId
			) cds ON cpc.Id = cds.ProductionCenterId
			WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
				AND ISNULL(cds.Quantity, 0) <> 1
			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		SELECT	@CodeResult = 999, 
				@MessageResult = 'Los siguientes centros de producción no tienen o tienen mas de un elemento de distribución secundaria activo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
		RETURN
	END

	-- Valido, si existen centros de producción no administrativos directos, estos ya deben estar distribuidos
	IF EXISTS
	(
		SELECT 1 
		FROM Cost.CostDirectDistributionSecondaryDetail cddsd
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		JOIN Cost.CostDistributionSecondary cds ON cpc.Id = cds.ProductionCenterId AND cds.Status = 1
		LEFT JOIN Cost.CostDistributionSecondaryBase cdsb ON cds.Id = cdsb.DistributionSecondaryId
		LEFT JOIN Cost.CostDirectDistributionSecondary cddst 
			ON cds.Id = cddst.DistributionSecondaryId AND cddst.Year = @Year AND cddst.Month = @Month AND cddst.Status = 2
		WHERE cddsd.DirectDistributionSecondaryId = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 
			AND cpc.CenterType <> 1 AND ISNULL(cdsb.DistributionType, 1) = 1
			AND cddst.Id IS NULL
	)
	BEGIN
		SELECT @Message = STUFF((
			SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cpc.Code
			FROM Cost.CostDirectDistributionSecondaryDetail cddsd
			JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
			JOIN Cost.CostDistributionSecondary cds ON cpc.Id = cds.ProductionCenterId AND cds.Status = 1
			LEFT JOIN Cost.CostDistributionSecondaryBase cdsb ON cds.Id = cdsb.DistributionSecondaryId
			LEFT JOIN Cost.CostDirectDistributionSecondary cddst 
				ON cds.Id = cddst.DistributionSecondaryId AND cddst.Year = @Year AND cddst.Month = @Month AND cddst.Status = 2
			WHERE cddsd.DirectDistributionSecondaryId = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 
				AND cpc.CenterType <> 1 AND ISNULL(cdsb.DistributionType, 1) = 1
				AND cddst.Id IS NULL
			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		SELECT	@CodeResult = 999, 
				@MessageResult = 'Se debe distribuir primero los siguientes centros de producción: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
		RETURN
	END

	/**************************************************** PROCESO **************************************************/

	WHILE @ProductionCenterRows > 0
	BEGIN
		SELECT TOP 1 
			@ProductionCenterId = cpc.Id,
			@ProductionCenterCodeName = CONCAT(cpc.Code, ' - ', cpc.Name),
			@CostDistributionSecondaryId = cds.Id,
			--------------------------------------------
			@CostDirectDistributionSecondaryDetailRows = 1,
			@CostDirectDistributionSecondaryDetailId = 0
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		JOIN Cost.CostDistributionSecondary cds ON cpc.Id = cds.ProductionCenterId AND cds.Status = 1
		WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
			AND cpc.Id > @ProductionCenterId
		ORDER BY cpc.Id

		SET @ProductionCenterRows = @@ROWCOUNT
		IF @ProductionCenterRows = 0 
		BEGIN
			BREAK
		END

		-----------------------------------------------------------------------------

		WHILE @CostDirectDistributionSecondaryDetailRows > 0
		BEGIN
			SELECT TOP 1 
				@CostDirectDistributionSecondaryDetailId = cddsd.Id,
				@ValueToDistribute = cddsd.Value,
				@PercentageToDistribute = cddsd.Percentage
			FROM Cost.CostDirectDistributionSecondaryDetail cddsd
			WHERE cddsd.DirectDistributionSecondaryId = @CostDirectDistributionSecondaryId
				AND cddsd.ProductionCenterId = @ProductionCenterId
				 AND cddsd.Value <> 0
				AND cddsd.Id > @CostDirectDistributionSecondaryDetailId
			ORDER BY cddsd.Id

			SET @CostDirectDistributionSecondaryDetailRows = @@ROWCOUNT
			IF @CostDirectDistributionSecondaryDetailRows = 0 
			BEGIN
				BREAK
			END

			-----------------------------------------------------------------------------

			INSERT INTO Cost.CostDirectDistributionSecondaryDetailRedistribution
			(
				DirectDistributionSecondaryDetailId, ProductionCenterId, Percentage, Value
			)
			SELECT @CostDirectDistributionSecondaryDetailId, ProductionCenterId, SUM(Percentage), SUM(Value)
			FROM [Cost].[GetCalculateDistributionSecondary](@Year, @Month, @CostDistributionSecondaryId, @ValueToDistribute, @PercentageToDistribute)
			WHERE Value <> 0
			GROUP BY ProductionCenterId
		END

		SET @Message = ISNULL(@Message, '') + CHAR(13) + CHAR(10) + @ProductionCenterCodeName
	END

	/**************************************************** VALIDACIONES **************************************************/

	-- Valido que los centros de producción se hayan distribuido completamente
	IF EXISTS 
	(
		SELECT 1
		FROM Cost.CostDirectDistributionSecondary cdds
		JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
		JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
		LEFT JOIN 
		(
			SELECT DirectDistributionSecondaryDetailId, SUM(Value) Value
			FROM Cost.CostDirectDistributionSecondaryDetailRedistribution
			GROUP BY DirectDistributionSecondaryDetailId
		) cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
		WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
			AND cddsd.Value <> ISNULL(cddsdr.Value, 0)
	)
	BEGIN
		SELECT @Message = STUFF((
			SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cpc.Code
			FROM Cost.CostDirectDistributionSecondary cdds
			JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON cdds.Id = cddsd.DirectDistributionSecondaryId
			JOIN Cost.CostProductionCenter cpc ON cddsd.ProductionCenterId = cpc.Id
			LEFT JOIN 
			(
				SELECT DirectDistributionSecondaryDetailId, SUM(Value) Value
				FROM Cost.CostDirectDistributionSecondaryDetailRedistribution
				GROUP BY DirectDistributionSecondaryDetailId
			) cddsdr ON cddsd.Id = cddsdr.DirectDistributionSecondaryDetailId
			WHERE cdds.Id = @CostDirectDistributionSecondaryId AND cddsd.Value <> 0 AND cpc.CenterType <> 1
				AND cddsd.Value <> ISNULL(cddsdr.Value, 0)
			FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

		SELECT	@CodeResult = 999, 
				@MessageResult = 'No se pudo realizar la redistribución completa de los siguientes centros de producción: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
		RETURN
	END
	
	/***************************************************** RESULTADO *****************************************************/

	SELECT	@CodeResult = 0, 
			@MessageResult = 'Se redistribuyeron los siguientes centros de producción no operativos: ' + ISNULL(@Message, '')

END