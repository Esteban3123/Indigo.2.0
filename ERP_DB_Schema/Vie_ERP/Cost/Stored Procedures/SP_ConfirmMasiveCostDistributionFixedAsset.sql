-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 12/07/2016
-- Description:	Procedimiento que se encarga de guardar masivamente la distribución de activos fijos
-- ==================================================================================================
CREATE PROCEDURE [Cost].[SP_ConfirmMasiveCostDistributionFixedAsset]
	@Year as int,
	@Month as int,
	@OperatingUnitId int,
	@CodeUser as varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionFixedAssetXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@ConcatenatedMessage VARCHAR(MAX) = ''

	DECLARE @CostDistributionFixedAsset AS TABLE
	(
		FixedAssetPhysicalAssetId INT, Plate VARCHAR(50),
		FixedAssetItemCodeDescription VARCHAR(500),
		--------------------------------------
		ProductionCenterId INT DEFAULT(0), ProductionCenterCodeName VARCHAR(500),
		HoursQuantity INT DEFAULT(0),
		DepreciationValue DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		/**************************************************** CARGUE DETALLES ****************************************************/

		INSERT INTO @CostDistributionFixedAsset
			EXEC Cost.SP_GetDistributionFixedAssetDataByYearMonthAndPhysicalAssetId @Year, @Month, NULL
		
		/**************************************************** DISTRIBUIR HORAS ***************************************************/
		
		DECLARE @PhysicalAssetRows INT = 1,
				@PhysicalAssetId INT = 0	

		WHILE @PhysicalAssetRows > 0
		BEGIN
			SELECT TOP 1
				@PhysicalAssetId = cdm.FixedAssetPhysicalAssetId
			FROM @CostDistributionFixedAsset cdm
			WHERE cdm.FixedAssetPhysicalAssetId > @PhysicalAssetId
			ORDER BY cdm.FixedAssetPhysicalAssetId

			SET @PhysicalAssetRows = @@ROWCOUNT
			IF @PhysicalAssetRows = 0 
			BEGIN
				BREAK
			END

			-------------------------------------------------------------------------------------------------------------------

			IF NOT EXISTS(SELECT 1 FROM Cost.CostDistributionFixedAsset cdm WHERE cdm.Year = @Year AND cdm.Month = @Month AND cdm.FixedAssetPhysicalAssetId = @PhysicalAssetId)
			BEGIN
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
									SUM(cdm.HoursQuantity) HoursWorked,
									SUM(cdm.DepreciationValue) DepreciationValue,
									1 Status
							FROM @CostDistributionFixedAsset cdm
							WHERE cdm.FixedAssetPhysicalAssetId = @PhysicalAssetId
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
							FROM @CostDistributionFixedAsset cdm
							WHERE cdm.FixedAssetPhysicalAssetId = @PhysicalAssetId
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