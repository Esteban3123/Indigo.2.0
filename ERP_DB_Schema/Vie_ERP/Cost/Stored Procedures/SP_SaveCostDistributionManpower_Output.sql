-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-16
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución de mano de obra
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveCostDistributionManpower_Output]
    @CostDistributionManpowerXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeMessageResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@IdResult INT OUTPUT,
	@CodeResult VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT,
			@OperatingUnitId INT,
			@Code VARCHAR(20),
			@Year INT,
			@Month INT,
			@ManpowerType TINYINT,
			@EntityId INT,
			@EmployeeId INT,
			@ThirdPartyId INT,
			@PositionId INT,
			@GroupId INT,
			@Description VARCHAR(300),
			@HoursWorked INT,
			@TotalAccrued DECIMAL(20,4),
			@TotalProvision DECIMAL(20,4),
			@TotalEmployerContribution DECIMAL(20,4),
			@TotalParafiscal DECIMAL(20,4),
			@Status BIT,
			------------------------------
			@errors VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		Id INT,
		DistributionManpowerId INT,
		ProductionCenterId INT,
		HoursQuantity INT,
		TotalAccrued DECIMAL(20,4),
		TotalProvision DECIMAL(20,4),
		TotalEmployerContribution DECIMAL(20,4),
		TotalParafiscal DECIMAL(20,4)
	)

	BEGIN TRY
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),			
			@Year = t.x.value('Year[1]','int'),
			@Month = t.x.value('Month[1]','int'),
			@ManpowerType = t.x.value('ManpowerType[1]','tinyint'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EmployeeId = t.x.value('EmployeeId[1]','int'),
			@ThirdPartyId = t.x.value('ThirdPartyId[1]','int'),
			@PositionId = t.x.value('PositionId[1]','int'),
			@GroupId = t.x.value('GroupId[1]','int'),
			@Description = t.x.value('Description[1]','varchar(300)'),
			@HoursWorked = t.x.value('HoursWorked[1]','int'),
			@TotalAccrued = t.x.value('TotalAccrued[1]','decimal(20,4)'),
			@TotalProvision = t.x.value('TotalProvision[1]','decimal(20,4)'),
			@TotalEmployerContribution = t.x.value('TotalEmployerContribution[1]','decimal(20,4)'),
			@TotalParafiscal = t.x.value('TotalParafiscal[1]','decimal(20,4)'),
			@Status = t.x.value('Status[1]','bit')
		FROM @CostDistributionManpowerXml.nodes('/CostDistributionManpower') t(x)

		/************************************* VALIDACIONES GENERALES ************************************/

		IF NOT EXISTS (SELECT 1 FROM Cost.CostSetting cs WHERE cs.Year = @Year AND cs.Month = @Month)
		BEGIN
			SELECT TOP 1 @errors = 'El periodo de la Distribución de Mano de Obra ' + CONCAT(@Year, '-', RIGHT('00' + CAST(@Month AS VARCHAR), 2)) + ' no corresponde con el periodo actual de Costos (' + CONCAT(cs.Year, '-', RIGHT('00' + CAST(cs.Month AS VARCHAR), 2)) + ').'
			FROM Cost.CostSetting cs

			SELECT @CodeMessageResult = 999, 
					@MessageResult = ISNULL(@errors, 'Parámetros de Costos no encontrado.'), 
					@IdResult = 0, 
					@CodeResult = ''
			RETURN
		END

		IF EXISTS 
		(
			SELECT 1 
			FROM Cost.ClosedMonth cm
			WHERE cm.Year = @Year AND cm.Month = @Month
		)
		OR EXISTS
		(
			SELECT 1 
			FROM Cost.CostEstimationNative cen
			WHERE cen.Year = @Year AND cen.Month = @Month AND cen.SecondaryDistribution > 0
		)
		BEGIN
			SELECT @CodeMessageResult = 999, 
					@MessageResult = 'El periodo se encuentra cerrado o distribuido', 
					@IdResult = 0, 
					@CodeResult = ''
			RETURN
		END

		IF EXISTS (SELECT 1 FROM Cost.CostDistributionManpower cdm WHERE cdm.Id <> @Id AND cdm.ManpowerType = @ManpowerType AND cdm.EntityId = @EntityId)
		BEGIN
			SELECT @CodeMessageResult = 999, 
				   @MessageResult = 'Ya existe una Distribución de Mano de Obra en estado: ' + IIF(cdm.Status = 0, 'Inactivo', 'Activo'), 
				   @IdResult = 0, 
				   @CodeResult = '' 
			FROM Cost.CostDistributionManpower cdm
			WHERE cdm.ManpowerType = @ManpowerType 
				AND cdm.EntityId = @EntityId
			RETURN
		END
		
		IF @Status = 0
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Cost.CostDistributionManpower WHERE Id = @Id)
			BEGIN
				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'No existe la distribución de ' + @Description + ' a Inactivar', 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END

			UPDATE Cost.CostDistributionManpower
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('DistributionManpowerId[1]','int'),
					t.x.value('ProductionCenterId[1]','int'),
					t.x.value('HoursQuantity[1]','int'),
					t.x.value('TotalAccrued[1]','decimal(20,4)'),
					t.x.value('TotalProvision[1]','decimal(20,4)'),
					t.x.value('TotalEmployerContribution[1]','decimal(20,4)'),
					t.x.value('TotalParafiscal[1]','decimal(20,4)')
				FROM @CostDistributionManpowerXml.nodes('/CostDistributionManpower/CostDistributionManpowerDetail') t(x)

			UPDATE cdmd
				SET cdmd.ProductionCenterId = d.ProductionCenterId,
					cdmd.HoursQuantity = d.HoursQuantity,
					cdmd.TotalAccrued = d.TotalAccrued,
					cdmd.TotalProvision = d.TotalProvision,
					cdmd.TotalEmployerContribution = d.TotalEmployerContribution,
					cdmd.TotalParafiscal = d.TotalParafiscal
			FROM Cost.CostDistributionManpowerDetail cdmd
			JOIN @Details d ON cdmd.Id = d.Id
			WHERE cdmd.DistributionManpowerId = @Id

			DELETE cdmd			
			FROM Cost.CostDistributionManpowerDetail cdmd
			LEFT JOIN @Details d ON cdmd.ProductionCenterId = d.ProductionCenterId
			WHERE cdmd.DistributionManpowerId = @Id AND d.Id IS NULL			

			/************************************* VALIDACIONES DEL DETALLE ************************************/

			IF NOT EXISTS (SELECT 1 FROM @Details d)
			BEGIN
				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'La distribución de ' + @Description + ' no tiene detalles', 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END

			IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.ProductionCenterId HAVING COUNT(*) > 1)
			BEGIN
				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'La distribución de ' + @Description + ' tiene centros de producción duplicados', 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END

			IF EXISTS (SELECT 1 FROM @Details d WHERE d.HoursQuantity <= 0)
			BEGIN
				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'La distribución de ' + @Description + ' tiene centros de producción con horas en 0', 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END

			IF EXISTS (SELECT 1 FROM @Details d WHERE ISNULL(d.ProductionCenterId, 0) = 0)
			BEGIN
				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'La distribución de ' + @Description + ' tiene detalles sin centros de producción', 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END

			IF ISNULL((SELECT SUM(d.HoursQuantity) FROM @Details d), 0) <> ISNULL(@HoursWorked, 0)
			BEGIN
				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'Las horas laboradas de la distribución de ' + @Description + ' no corresponde con sumatoria de horas laboradas de los detalles', 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END

			IF ISNULL((SELECT SUM(d.TotalAccrued + d.TotalEmployerContribution + d.TotalProvision + d.TotalParafiscal) FROM @Details d), 0) <> ISNULL((@TotalAccrued + @TotalEmployerContribution + @TotalProvision + @TotalParafiscal), 0)
			BEGIN
				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'El costo del empleado de la distribución de ' + @Description + ' no corresponde con el valor distribuido', 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END

			/*************************************************************************************/

			IF @Id = 0
			BEGIN
				IF @Code = '' 
				BEGIN
					DECLARE @IsManual BIT,
							@Code_Output INT,
							@Message_Output VARCHAR(MAX)
				
					EXEC Common.SP_GetSequence 510, 1731, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT @CodeMessageResult = 999, 
								@MessageResult = REPLACE(@Message_Output, '{0}', 'Distribución de Mano de Obra'), 
								@IdResult = 0, 
								@CodeResult = ''
						RETURN
					END

					INSERT INTO [Cost].[CostDistributionManpower]
					(
						[Year],[Month],[Code],[ManpowerType],[EntityId],[EmployeeId],[ThirdPartyId],[PositionId],[GroupId],[Description]					
						,[HoursWorked],[TotalAccrued],[TotalProvision],[TotalEmployerContribution],[TotalParafiscal]
						,[Status],[CreationUser],[CreationDate]
					)
					SELECT
						@Year, @Month, @Code, @ManpowerType, @EntityId, @EmployeeId, @ThirdPartyId, @PositionId, @GroupId, @Description
						,@HoursWorked, @TotalAccrued, @TotalProvision, @TotalEmployerContribution, @TotalParafiscal
						,1,@CodeUser,[Common].[GETDATE]()

					SET @Id = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE Cost.CostDistributionManpower
					SET Code = @Code,
						Year = @Year,
						Month = @Month,
						ManpowerType = @ManpowerType,
						EntityId = @EntityId,
						EmployeeId = @EmployeeId,
						ThirdPartyId = @ThirdPartyId,
						PositionId = @PositionId,
						GroupId = @GroupId,
						Description = @Description,
						HoursWorked = @HoursWorked,
						TotalAccrued = @TotalAccrued,
						TotalProvision = @TotalProvision,
						TotalEmployerContribution= @TotalEmployerContribution,
						TotalParafiscal = @TotalParafiscal,
						Status = @Status,
						ModificationUser = @CodeUser,
						ModificationDate = [Common].[GETDATE]()
				WHERE Id = @Id
			END

			INSERT INTO [Cost].[CostDistributionManpowerDetail]
			(
				[DistributionManpowerId],[ProductionCenterId],[HoursQuantity],[TotalAccrued],[TotalProvision],[TotalEmployerContribution],[TotalParafiscal]
			)
			SELECT
				@Id, d.ProductionCenterId, d.HoursQuantity, d.TotalAccrued, d.TotalProvision, d.TotalEmployerContribution, d.TotalParafiscal
			FROM @Details d
			LEFT JOIN Cost.CostDistributionManpowerDetail cdmd ON cdmd.DistributionManpowerId = @Id AND cdmd.ProductionCenterId = d.ProductionCenterId
			WHERE cdmd.Id IS NULL
		END

		SELECT @CodeMessageResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 0 THEN CONCAT('Se inhabilitó la Distribución de Mano de Obra con código ', @Code)
				   ELSE CONCAT('Se guardó la Distribución de Mano de Obra con código ', @Code)
			   END, 
			   @IdResult = @Id, 
			   @CodeResult = @Code
		RETURN
	END TRY
	BEGIN CATCH
		SELECT @CodeMessageResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)), 
			   @IdResult = 0, 
			   @CodeResult = ''
	END CATCH
END