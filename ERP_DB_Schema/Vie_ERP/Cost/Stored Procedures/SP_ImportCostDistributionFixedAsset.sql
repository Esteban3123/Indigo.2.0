-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 12/07/2016
-- Description:	Procedimiento que se encarga de guardar masivamente la distribución de activos fijos
-- ==================================================================================================
CREATE PROCEDURE [Cost].[SP_ImportCostDistributionFixedAsset]
	@Year INT,
	@Month INT,
	@OperatingUnitId INT,
	@ImportXml AS XML,
	@CodeUser varchar(20)
AS
BEGIN
	SET NOCOUNT ON
	
	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionFixedAssetXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@ConcatenatedMessage VARCHAR(MAX) = ''

	DECLARE @DistributionIds AS TABLE
	(
		Id INT
	)

	DECLARE @Details AS TABLE
	(
		FixedAssetPhysicalAssetId INT,
		Plate VARCHAR(20), FixedAssetItemCodeDescription VARCHAR(500),
		--------------------------------------
		ProductionCenterId INT, ProductionCenterCodeName VARCHAR(500),
		HoursQuantity INT DEFAULT(0),
		DepreciationValue DECIMAL(20,4) DEFAULT(0)
	)

	DECLARE @CostDistributionFixedAssetDetail AS TABLE
	(
		Id INT IDENTITY(1,1),
		ProductionCenterId INT,
		HoursQuantity INT DEFAULT(0),
		DepreciationValue DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		/**************************************************** CARGUE DETALLES ****************************************************/

		INSERT INTO @DistributionIds
			SELECT DISTINCT
				t.x.value('Id[1]','int')
			FROM @ImportXml.nodes('/Data') t(x)

		INSERT INTO @Details
			EXEC Cost.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId @Year, @Month, NULL

		/************************************************* VALIDACIONES GENERALES ************************************************/

		IF EXISTS 
		(
			SELECT 1 
			FROM Cost.CostDistributionFixedAsset dm 
			JOIN @DistributionIds mi ON dm.Id = mi.Id
			JOIN Cost.CostDistributionFixedAsset cdm ON dm.FixedAssetPhysicalAssetId = cdm.FixedAssetPhysicalAssetId AND cdm.Year = @Year AND cdm.Month = @Month
		)
		BEGIN
			SELECT @ConcatenatedMessage = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cdm.Description + ' en estado: ' + IIF(cdm.Status = 0, 'Inactivo', 'Activo')
					FROM Cost.CostDistributionFixedAsset dm 
					JOIN @DistributionIds mi ON dm.Id = mi.Id
					JOIN Cost.CostDistributionFixedAsset cdm ON dm.FixedAssetPhysicalAssetId = cdm.FixedAssetPhysicalAssetId AND cdm.Year = @Year AND cdm.Month = @Month
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 
				   'Ya existe una Distribución de Activos en el periodo para los siguientes artículos: ' + CHAR(13) + CHAR(10) + ISNULL(@ConcatenatedMessage, '') AS Message
			RETURN
		END		
		
		/**************************************************** DISTRIBUIR HORAS ***************************************************/

		DECLARE @DistributionRows INT = 1,
				@DistributionId INT = 0,
				@FixedAssetPhysicalAssetId INT,
				@TotalHours DECIMAL,
				--------------------------
				@DepreciationValue DECIMAL(20,4),
				--------------------------
				@outstandingDepreciationValue DECIMAL(20,4),		
				--------------------------
				@CostDistributionFixedAssetDetailRows INT,
				@CostDistributionFixedAssetDetailId INT,
				@adjusteDepreciationValue DECIMAL(20, 4)

		WHILE @DistributionRows > 0
		BEGIN
			SELECT TOP 1
				@DistributionId = cdm.Id,
				@FixedAssetPhysicalAssetId = cdm.FixedAssetPhysicalAssetId,
				@TotalHours = cdm.HoursWorked
			FROM @DistributionIds m
			JOIN Cost.CostDistributionFixedAsset cdm ON m.Id = cdm.Id
			WHERE cdm.Id > @DistributionId
			ORDER BY cdm.Id

			SET @DistributionRows = @@ROWCOUNT
			IF @DistributionRows = 0 
			BEGIN
				BREAK
			END

			-----------------------------------------------------------------------------------------------------------------------

			IF NOT EXISTS (SELECT 1 FROM @Details WHERE FixedAssetPhysicalAssetId = @FixedAssetPhysicalAssetId)
			BEGIN
				SELECT @ConcatenatedMessage = @ConcatenatedMessage + 'No existen distribuciones de activo fijo en el periodo para el artículo: ' + fapa.Plate + ' -' + fai.Code + ' - ' + fai.Description
				FROM FixedAsset.FixedAssetPhysicalAsset fapa
				JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
				WHERE fapa.Id = @FixedAssetPhysicalAssetId

				CONTINUE
			END

			-----------------------------------------------------------------------------------------------------------------------

			SELECT TOP 1
				@DepreciationValue = SUM(d.DepreciationValue),
				-------------------------------------------------------------
				@CostDistributionFixedAssetDetailRows = 1,
				@CostDistributionFixedAssetDetailId = 0
			FROM @Details d
			WHERE d.FixedAssetPhysicalAssetId = @FixedAssetPhysicalAssetId

			-------------------------------------------------------------------------------------------------------------------

			DELETE FROM @CostDistributionFixedAssetDetail

			-------------------------------------------------------------------------------------------------------------------

			INSERT INTO @CostDistributionFixedAssetDetail
				SELECT 
					cdmd.ProductionCenterId, 
					cdmd.HoursQuantity, 
					(cdmd.HoursQuantity / @TotalHours * @DepreciationValue) DepreciationValue
				FROM Cost.CostDistributionFixedAssetDetail cdmd
				WHERE cdmd.DistributionFixedAssetId = @DistributionId

			SELECT	@outstandingDepreciationValue = @DepreciationValue - SUM(cdmd.DepreciationValue)
			FROM @CostDistributionFixedAssetDetail cdmd

			IF @outstandingDepreciationValue <> 0
			BEGIN
				WHILE @CostDistributionFixedAssetDetailRows > 0
				BEGIN
					SELECT TOP 1
						@CostDistributionFixedAssetDetailId = d.Id,
						@adjusteDepreciationValue = Common.CalculateAdjustedValue(@outstandingDepreciationValue, d.DepreciationValue, @outstandingDepreciationValue)
					FROM @CostDistributionFixedAssetDetail d
					WHERE d.Id > @CostDistributionFixedAssetDetailId
					ORDER BY d.Id

					SET @CostDistributionFixedAssetDetailRows = @@ROWCOUNT
					IF @CostDistributionFixedAssetDetailRows = 0 
					BEGIN
						BREAK
					END

					-------------------------------------------------------------------------------------------------------------------

					UPDATE d
						SET d.DepreciationValue = d.DepreciationValue + @adjusteDepreciationValue
					FROM @CostDistributionFixedAssetDetail d
					WHERE d.Id = @CostDistributionFixedAssetDetailId

					SELECT	@outstandingDepreciationValue = @outstandingDepreciationValue - @adjusteDepreciationValue

					IF @outstandingDepreciationValue = 0
					BEGIN
						BREAK
					END
				END
			END

			/**************************************************** INSERTAR REGISTROS ***************************************************/

			SELECT @CostDistributionFixedAssetXml = CONVERT
			(
				XML, 
				(
					SELECT 
						CostDistributionFixedAsset.*,
						CostDistributionFixedAssetDetail.*
					FROM
					(
						SELECT	0 Id,
								@OperatingUnitId OperatingUnitId,
								'' Code,
								@Year Year, @Month Month,
								cdm.FixedAssetPhysicalAssetId, 
								CONCAT(cdm.Plate, ' - ', cdm.FixedAssetItemCodeDescription) Description,
								@TotalHours HoursWorked,
								@DepreciationValue DepreciationValue,
								1 Status
						FROM @Details cdm
						WHERE cdm.FixedAssetPhysicalAssetId = @FixedAssetPhysicalAssetId
						GROUP BY cdm.FixedAssetPhysicalAssetId, cdm.Plate, cdm.FixedAssetItemCodeDescription
					) CostDistributionFixedAsset
					JOIN
					( 
						SELECT
							0 DistributionFixedAssetId,
							cdm.ProductionCenterId,
							cdm.HoursQuantity,
							0 Proportion,
							cdm.DepreciationValue
						FROM @CostDistributionFixedAssetDetail cdm
					) CostDistributionFixedAssetDetail ON CostDistributionFixedAsset.Id = CostDistributionFixedAssetDetail.DistributionFixedAssetId
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Cost].[SP_SaveCostDistributionFixedAsset_Output] @CostDistributionFixedAssetXml, @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT @Code_Output as CodeMessage, @Message_Output as Message
				RETURN
			END

			SET @ConcatenatedMessage = @ConcatenatedMessage + @Message_Output + CHAR(13) + CHAR(10)
		END

		/******************************************************* RESULTADO *******************************************************/

		IF @ConcatenatedMessage = ''
		BEGIN
			SELECT 999 CodeMessage, 'No se encontraron registros pendientes por confirmar' Message
			RETURN
		END

		SELECT 0 CodeMessage, @ConcatenatedMessage Message
		
	END TRY
	BEGIN CATCH
		SELECT 999 CodeMessage, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) Message
	END CATCH
END