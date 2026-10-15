-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 12/07/2016
-- Description:	Procedimiento que se encarga de guardar masivamente la distribución de mano de obra
-- ==================================================================================================
CREATE PROCEDURE [Cost].[SP_ConfirmMasiveCostDistributionManpower] 
	@Year as int,
	@Month as int,
	@OperatingUnitId int,
	@CodeUser as varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionManpowerXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@ConcatenatedMessage VARCHAR(MAX) = ''

	DECLARE @CostDistributionManpower AS TABLE
	(
		ManpowerType TINYINT, EntityId INT,
		GroupId INT, GroupCodeName VARCHAR(500),
		EmployeeId INT, ThirdPartyId INT, ThirdPartyNitName VARCHAR(500),
		PositionId INT, PositionCodeName VARCHAR(500),
		--------------------------------------
		ProductionCenterId INT, ProductionCenterCodeName VARCHAR(500),
		HoursQuantity INT DEFAULT(0),
		TotalAccrued DECIMAL(20,4) DEFAULT(0),
		TotalProvision DECIMAL(20,4) DEFAULT(0),
		TotalEmployerContribution DECIMAL(20,4) DEFAULT(0),
		TotalParafiscal DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		/**************************************************** CARGUE DETALLES ****************************************************/

		INSERT INTO @CostDistributionManpower
			EXEC Cost.SP_GetDistributionManpower @Year, @Month, NULL, NULL

		/**************************************************** DISTRIBUIR HORAS ***************************************************/

		DECLARE @CostDistributionManpowerManpowerTypeRows INT = 1,
				@CostDistributionManpowerManpowerType INT = 0,
				---------------------------------------
				@CostDistributionManpowerEntityRows INT,
				@CostDistributionManpowerEntityId INT				

		WHILE @CostDistributionManpowerManpowerTypeRows > 0
		BEGIN
			SELECT TOP 1
				@CostDistributionManpowerManpowerType = cdm.ManpowerType,
				---------------------------------------
				@CostDistributionManpowerEntityRows = 1,
				@CostDistributionManpowerEntityId = 0
			FROM @CostDistributionManpower cdm
			WHERE cdm.ManpowerType > @CostDistributionManpowerManpowerType
			ORDER BY cdm.ManpowerType

			SET @CostDistributionManpowerManpowerTypeRows = @@ROWCOUNT
			IF @CostDistributionManpowerManpowerTypeRows = 0 
			BEGIN
				BREAK
			END

			-----------------------------------------------------------------------------------------------------------------------

			WHILE @CostDistributionManpowerEntityRows > 0
			BEGIN
				SELECT TOP 1
					@CostDistributionManpowerEntityId = cdm.EntityId
				FROM @CostDistributionManpower cdm
				WHERE cdm.ManpowerType = @CostDistributionManpowerManpowerType
					AND cdm.EntityId > @CostDistributionManpowerEntityId
				ORDER BY cdm.EntityId

				SET @CostDistributionManpowerEntityRows = @@ROWCOUNT
				IF @CostDistributionManpowerEntityRows = 0 
				BEGIN
					BREAK
				END

				-------------------------------------------------------------------------------------------------------------------

				IF NOT EXISTS(SELECT 1 FROM Cost.CostDistributionManpower cdm WHERE cdm.Year = @Year AND cdm.Month = @Month AND cdm.ManpowerType = @CostDistributionManpowerManpowerType AND cdm.EntityId = @CostDistributionManpowerEntityId)
				BEGIN
					SELECT @CostDistributionManpowerXml = CONVERT
					(
						XML, 
						(
							SELECT 
								CostDistributionManpower.*,
								CostDistributionManpowerDetail.*
							FROM
							(
								SELECT	0 Id,
										@OperatingUnitId OperatingUnitId,
										'' Code,
										@Year Year, @Month Month,
										cdm.ManpowerType, cdm.EntityId, 
										cdm.EmployeeId, cdm.ThirdPartyId, cdm.PositionId, cdm.GroupId, 
										cdm.ThirdPartyNitName Description,
										SUM(cdm.HoursQuantity) HoursWorked,
										SUM(cdm.TotalAccrued) TotalAccrued,SUM(cdm.TotalProvision) TotalProvision,SUM(cdm.TotalEmployerContribution) TotalEmployerContribution,SUM(cdm.TotalParafiscal) TotalParafiscal,
										1 Status
								FROM @CostDistributionManpower cdm
								WHERE cdm.ManpowerType = @CostDistributionManpowerManpowerType
									AND cdm.EntityId = @CostDistributionManpowerEntityId
								GROUP BY cdm.ManpowerType, cdm.EntityId, cdm.EmployeeId, cdm.ThirdPartyId, cdm.PositionId, cdm.GroupId, cdm.ThirdPartyNitName
							) CostDistributionManpower
							JOIN
							( 
								SELECT
									0 CostDistributionManpowerId,
									cdm.ProductionCenterId,
									cdm.HoursQuantity,
									cdm.TotalAccrued,
									cdm.TotalProvision,
									cdm.TotalEmployerContribution,
									cdm.TotalParafiscal
								FROM @CostDistributionManpower cdm
								WHERE cdm.ManpowerType = @CostDistributionManpowerManpowerType
									AND cdm.EntityId = @CostDistributionManpowerEntityId
							) CostDistributionManpowerDetail ON CostDistributionManpower.Id = CostDistributionManpowerDetail.CostDistributionManpowerId
							For xml AUTO,TYPE, ELEMENTS
						)
					)

					EXEC [Cost].[SP_SaveCostDistributionManpower_Output] @CostDistributionManpowerXml, @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

					IF @Code_Output <> 0
					BEGIN
						SELECT @Code_Output as CodeMessage, @Message_Output as Message
						RETURN
					END

					SET @ConcatenatedMessage = @ConcatenatedMessage + @Message_Output + CHAR(13) + CHAR(10) 
				END
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