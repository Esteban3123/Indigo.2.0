-- ==================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 12/07/2016
-- Description:	Procedimiento que se encarga de guardar masivamente la distribución de mano de obra
-- ==================================================================================================
CREATE PROCEDURE [Cost].[SP_ImportCostDistributionManpower]
	@Year INT,
	@Month INT,
	@OperatingUnitId INT,
	@ImportXml AS XML,
	@CodeUser varchar(20)
AS
BEGIN
	SET NOCOUNT ON

	/******************************************************** VARIABLES ******************************************************/

	DECLARE @CostDistributionManpowerXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@ConcatenatedMessage VARCHAR(MAX) = ''

	DECLARE @ManpowerIds AS TABLE
	(
		Id INT
	)

	DECLARE @Details AS TABLE
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

	DECLARE @CostDistributionManpowerDetail AS TABLE
	(
		Id INT IDENTITY(1,1),
		ProductionCenterId INT,
		HoursQuantity INT DEFAULT(0),
		TotalAccrued DECIMAL(20,4) DEFAULT(0),
		TotalProvision DECIMAL(20,4) DEFAULT(0),
		TotalEmployerContribution DECIMAL(20,4) DEFAULT(0),
		TotalParafiscal DECIMAL(20,4) DEFAULT(0)
	)

	BEGIN TRY

		/**************************************************** CARGUE DETALLES ****************************************************/

		INSERT INTO @ManpowerIds
			SELECT DISTINCT
				t.x.value('Id[1]','int')
			FROM @ImportXml.nodes('/Data') t(x)

		INSERT INTO @Details
			EXEC Cost.SP_GetDistributionManpower @Year, @Month, NULL, NULL

		/************************************************* VALIDACIONES GENERALES ************************************************/

		IF EXISTS 
		(
			SELECT 1 
			FROM Cost.CostDistributionManpower dm 
			JOIN @ManpowerIds mi ON dm.Id = mi.Id
			JOIN Cost.CostDistributionManpower cdm ON dm.ManpowerType = cdm.ManpowerType AND dm.ThirdPartyId = cdm.ThirdPartyId AND cdm.Year = @Year AND cdm.Month = @Month
		)
		BEGIN
			SELECT @ConcatenatedMessage = STUFF((
					SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + tp.Nit + ' -' + tp.Name + ' de tipo ' + IIF(cdm.ManpowerType = 1, 'Empleado', 'Contratista') + ' en estado: ' + IIF(cdm.Status = 0, 'Inactivo', 'Activo')
					FROM Cost.CostDistributionManpower dm 
					JOIN @ManpowerIds mi ON dm.Id = mi.Id
					JOIN Cost.CostDistributionManpower cdm ON dm.ManpowerType = cdm.ManpowerType AND dm.ThirdPartyId = cdm.ThirdPartyId AND cdm.Year = @Year AND cdm.Month = @Month
					JOIN Common.ThirdParty tp ON cdm.ThirdPartyId = tp.Id
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT 999 AS CodeMessage, 
				   'Ya existe una Distribución de Mano de Obra en el periodo para los siguientes terceros: ' + CHAR(13) + CHAR(10) + ISNULL(@ConcatenatedMessage, '') AS Message
			RETURN
		END		
		
		/**************************************************** DISTRIBUIR HORAS ***************************************************/

		DECLARE @ManpowerRows INT = 1,
				@ManpowerId INT = 0,
				@ManpowerType TINYINT,
				@ThirdParty INT,
				@TotalHours DECIMAL,
				--------------------------
				@EntityRows INT,
				@EntityId INT,
				@TotalAccrued DECIMAL(20,4),
				@TotalProvision DECIMAL(20,4),
				@TotalEmployerContribution DECIMAL(20,4),
				@TotalParafiscal DECIMAL(20,4),
				--------------------------
				@outstandingAccrued DECIMAL(20,4),
				@outstandingProvision DECIMAL(20,4),
				@outstandingEmployerContribution DECIMAL(20,4),
				@outstandingParafiscal DECIMAL(20,4),				
				--------------------------
				@CostDistributionManpowerDetailRows INT,
				@CostDistributionManpowerDetailId INT,
				@adjustedAccrued DECIMAL(20, 4),
				@adjustedProvision DECIMAL(20, 4),
				@adjustedEmployerContribution DECIMAL(20, 4),
				@adjustedParafiscal DECIMAL(20, 4)

		WHILE @ManpowerRows > 0
		BEGIN
			SELECT TOP 1
				@ManpowerId = cdm.Id,
				@ManpowerType= cdm.ManpowerType,
				@ThirdParty = cdm.ThirdPartyId,
				@TotalHours = cdm.HoursWorked,
				--------------------------
				@EntityRows = 1,
				@EntityId = 0
			FROM @ManpowerIds m
			JOIN Cost.CostDistributionManpower cdm ON m.Id = cdm.Id
			WHERE cdm.Id > @ManpowerId
			ORDER BY cdm.Id

			SET @ManpowerRows = @@ROWCOUNT
			IF @ManpowerRows = 0 
			BEGIN
				BREAK
			END

			-----------------------------------------------------------------------------------------------------------------------

			IF NOT EXISTS (SELECT 1 FROM @Details WHERE ManpowerType = @ManpowerType AND ThirdPartyId = @ThirdParty)
			BEGIN
				SELECT @ConcatenatedMessage = @ConcatenatedMessage + 'No existen distribuciones de mano de obra en el periodo para el tercero: ' + tp.Nit + ' -' + tp.Name + ' de tipo ' + IIF(@ManpowerType = 1, 'Empleado', 'Contratista')
				FROM Common.ThirdParty tp
				WHERE tp.Id = @ThirdParty

				CONTINUE
			END

			-----------------------------------------------------------------------------------------------------------------------

			WHILE @EntityRows > 0
			BEGIN
				SELECT TOP 1
					@EntityId = d.EntityId,
					@TotalAccrued = SUM(d.TotalAccrued),
					@TotalProvision = SUM(d.TotalProvision),
					@TotalEmployerContribution = SUM(d.TotalEmployerContribution),
					@TotalParafiscal = SUM(d.TotalParafiscal),
					-------------------------------------------------------------
					@CostDistributionManpowerDetailRows = 1,
					@CostDistributionManpowerDetailId = 0
				FROM @Details d
				WHERE d.ManpowerType = @ManpowerType
					AND d.ThirdPartyId = @ThirdParty
					AND d.EntityId > @EntityId
				GROUP BY d.EntityId

				SET @EntityRows = @@ROWCOUNT
				IF @EntityRows = 0 
				BEGIN
					BREAK
				END

				-------------------------------------------------------------------------------------------------------------------

				DELETE FROM @CostDistributionManpowerDetail

				-------------------------------------------------------------------------------------------------------------------

				INSERT INTO @CostDistributionManpowerDetail
					SELECT 
						cdmd.ProductionCenterId, 
						cdmd.HoursQuantity, 
						(cdmd.HoursQuantity / @TotalHours * @TotalAccrued) TotalAccrued, 
						(cdmd.HoursQuantity / @TotalHours * @TotalProvision) TotalProvision, 
						(cdmd.HoursQuantity / @TotalHours * @TotalEmployerContribution) TotalEmployerContribution, 
						(cdmd.HoursQuantity / @TotalHours * @TotalParafiscal) TotalParafiscal
					FROM Cost.CostDistributionManpowerDetail cdmd
					WHERE cdmd.DistributionManpowerId = @ManpowerId

				SELECT	@outstandingAccrued = @TotalAccrued - SUM(cdmd.TotalAccrued),
						@outstandingProvision = @TotalProvision - SUM(cdmd.TotalProvision),
						@outstandingEmployerContribution = @TotalEmployerContribution - SUM(cdmd.TotalEmployerContribution),
						@outstandingParafiscal = @TotalParafiscal - SUM(cdmd.TotalParafiscal)
				FROM @CostDistributionManpowerDetail cdmd

				IF @outstandingAccrued <> 0 OR @outstandingProvision <> 0 OR @outstandingParafiscal <> 0 OR @outstandingParafiscal <> 0
				BEGIN
					WHILE @CostDistributionManpowerDetailRows > 0
					BEGIN
						SELECT TOP 1
							@CostDistributionManpowerDetailId = d.Id,
							@adjustedAccrued = Common.CalculateAdjustedValue(@outstandingAccrued, d.TotalAccrued, @outstandingAccrued),
							@adjustedProvision = Common.CalculateAdjustedValue(@outstandingProvision, d.TotalProvision, @outstandingProvision),
							@adjustedEmployerContribution = Common.CalculateAdjustedValue(@outstandingEmployerContribution, d.TotalEmployerContribution, @outstandingEmployerContribution),
							@adjustedParafiscal = Common.CalculateAdjustedValue(@outstandingParafiscal, d.TotalParafiscal, @outstandingParafiscal)
						FROM @CostDistributionManpowerDetail d
						WHERE d.Id > @CostDistributionManpowerDetailId
						ORDER BY d.Id

						SET @CostDistributionManpowerDetailRows = @@ROWCOUNT
						IF @CostDistributionManpowerDetailRows = 0 
						BEGIN
							BREAK
						END

						-------------------------------------------------------------------------------------------------------------------

						UPDATE d
							SET d.TotalAccrued = d.TotalAccrued + @adjustedAccrued,
								d.TotalProvision = d.TotalProvision + @adjustedProvision,
								d.TotalEmployerContribution = d.TotalEmployerContribution + @adjustedEmployerContribution,
								d.TotalParafiscal = d.TotalParafiscal + @adjustedParafiscal
						FROM @CostDistributionManpowerDetail d
						WHERE d.Id = @CostDistributionManpowerDetailId

						SELECT	@outstandingAccrued = @outstandingAccrued - @adjustedAccrued,
								@outstandingProvision = @outstandingProvision - @adjustedProvision,
								@outstandingEmployerContribution = @outstandingEmployerContribution - @adjustedEmployerContribution,
								@outstandingParafiscal = @outstandingParafiscal - @adjustedParafiscal

						IF @outstandingAccrued = 0 AND @outstandingProvision = 0 AND @outstandingParafiscal = 0 AND @outstandingParafiscal = 0
						BEGIN
							BREAK
						END
					END
				END

				/**************************************************** INSERTAR REGISTROS ***************************************************/

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
									@TotalHours HoursWorked,
									SUM(cdm.TotalAccrued) TotalAccrued,SUM(cdm.TotalProvision) TotalProvision,SUM(cdm.TotalEmployerContribution) TotalEmployerContribution,SUM(cdm.TotalParafiscal) TotalParafiscal,
									1 Status
							FROM @Details cdm
							WHERE cdm.ManpowerType = @ManpowerType
								AND cdm.ThirdPartyId = @ThirdParty
								AND cdm.EntityId = @EntityId
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
							FROM @CostDistributionManpowerDetail cdm
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