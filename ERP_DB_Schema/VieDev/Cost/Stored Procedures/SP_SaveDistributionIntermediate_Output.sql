-- =============================================
-- Author:		Diego A. Roldan
-- Create date: 2019-09-02
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución intermedia
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveDistributionIntermediate_Output]
    @DistributionIntermediateXml AS XML,
	@DistributionIntermediateDetailForDeleteXml AS XML,
	@CostLogisticsProductionCenterDetail AS XML,
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
			--@OperatingUnitId INT,
			@Code VARCHAR(20),
			@DistributionIntermediateId INT,
			@CostProductionCenterId INT,
			@Year INT,
			@Month INT,
			@CostEstimationId INT,
			@Value DECIMAL(20,4),
			@Description VARCHAR(300),			
			@Status TINYINT,
			------------------------------			
			@IdForm INT = 1735,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)			

	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		Id INT,
		DirectDistributionSecondaryId INT,
		ProductionCenterId INT,
		--MeasurementUnitId INT,
		Percentage DECIMAL(5,2),
		Value DECIMAL(20,4)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			--@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DistributionIntermediateId = t.x.value('DistributionIntermediateId[1]','int'),
			@Year = t.x.value('Year[1]','int'),
			@Month = t.x.value('Month[1]','int'),
			@CostEstimationId = t.x.value('CostEstimationId[1]','int'),
			@Value = t.x.value('Value[1]','decimal(20,4)'),
			@Description = t.x.value('Description[1]','varchar(300)'),
			@Status = t.x.value('Status[1]','tinyint')
		FROM @DistributionIntermediateXml.nodes('/CostDistributionIntermediate') t(x)

		IF EXISTS (SELECT 1 FROM Cost.CostDistributionIntermediate cdds WHERE cdds.Id = @Id AND cdds.Status <> 1)
		BEGIN
			SELECT @CodeMessageResult = 999, 
				   @MessageResult = 'La Distribución Intermedia se encuentra en estado: ' + IIF(cdds.Status = 2, 'Confirmado', 'Anulado'), 
				   @IdResult = 0, 
				   @CodeResult = '' 
			FROM Cost.CostDistributionIntermediate cdds 
			WHERE cdds.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE Cost.CostDistributionIntermediate
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			--Eliminamos los detalles indicados
			DELETE cddsd
			FROM @DistributionIntermediateDetailForDeleteXml.nodes('/DistributionIntermediateDetail') t(x)
			JOIN Cost.CostDistributionIntermediateDetail cddsd ON t.x.value('Id[1]','int') = cddsd.Id
			WHERE cddsd.DistributionIntermediateId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('DistributionIntermediateId[1]','int'),
					t.x.value('ProductionCenterId[1]','int'),
					--t.x.value('MeasurementUnitId[1]','int'),					
					t.x.value('Percentage[1]','decimal(5,2)'),
					t.x.value('Value[1]','decimal(20,4)')
				FROM @DistributionIntermediateXml.nodes('/CostDistributionIntermediate/CostDistributionIntermediateDetail') t(x)

			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					cddsd.Id, 
					cddsd.DistributionIntermediateId, 
					cddsd.ProductionCenterId,
					--cddsd.MeasurementUnitId,
					cddsd.Proportion,
					cddsd.Value
				FROM Cost.CostDistributionIntermediateDetail cddsd
				LEFT JOIN @Details d ON cddsd.Id = d.Id
				WHERE cddsd.DistributionIntermediateId = @Id AND d.Id IS NULL

			SELECT @CostProductionCenterId = cds.ProductionCenterId
			FROM Cost.CostIntermediateDistribution cds
			WHERE cds.Id = @DistributionIntermediateId

			/*************************************VALIDACIONES************************************/

			-- Valido el Periodo de la Vigencia
			--IF NOT EXISTS 
			--(
			--	SELECT 1 
			--	FROM Cost.CostSetting cs
			--	WHERE cs.Year = @Year AND cs.Month = @Month
			--)
			--BEGIN
			--	SELECT TOP 1 @Message = 'El periodo de la Distribución Intermedia ' + CONCAT(@Year, '-', RIGHT('00' + CAST(@Month AS VARCHAR), 2)) + ' no corresponde con el periodo actual de Costos.' + CONCAT(cs.Year, '-', RIGHT('00' + CAST(cs.Month AS VARCHAR), 2)) + ').'
			--	FROM Cost.CostSetting cs

			--	SELECT @CodeMessageResult = 999, 
			--		   @MessageResult = ISNULL(@Message, 'Parámetros de Costos no encontrado.'), 
			--		   @IdResult = 0, 
			--		   @CodeResult = ''
			--	RETURN
			--END

			-- Valido el Periodo no se encuentre ya cerrado o distribuido
			--IF EXISTS 
			--(
			--	SELECT 1 
			--	FROM Cost.ClosedMonth cm
			--	WHERE cm.Year = @Year AND cm.Month = @Month
			--)
			--OR EXISTS
			--(
			--	SELECT 1 
			--	FROM Cost.CostEstimationNative cen
			--	WHERE cen.Year = @Year AND cen.Month = @Month AND cen.IntermediateDistribution > 0
			--)
			--BEGIN
			--	SELECT @CodeMessageResult = 999, 
			--		   @MessageResult = 'El periodo se encuentra cerrado o distribuido', 
			--		   @IdResult = 0, 
			--		   @CodeResult = ''
			--	RETURN
			--END

			-- Valido que el centro de producción a distribuir no sea operativo
			IF NOT EXISTS (SELECT 1 FROM Cost.CostProductionCenter WHERE Id = @CostProductionCenterId AND CenterType <> 1)
			BEGIN
				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'El centro de producción a distribuir debe ser de Administrativo o Logístico', 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END

			-- Valido que no exista una distribución asociada al centro de producción
			IF EXISTS 
			(
				SELECT 1 
				FROM Cost.CostDistributionIntermediate cdds
				JOIN Cost.CostIntermediateDistribution cds ON cdds.IntermediateDistributionElementId = cds.Id
				WHERE Year = @Year AND Month = @Month 
					AND cdds.Id <> ISNULL(@Id, 0)
					AND cdds.Status <> 3
					AND cds.ProductionCenterId = @CostProductionCenterId
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cdds.Code
						FROM Cost.CostDistributionIntermediate cdds
						JOIN Cost.CostIntermediateDistribution cds ON cdds.IntermediateDistributionElementId = cds.Id
						WHERE Year = @Year AND Month = @Month 
							AND cdds.Id <> ISNULL(@Id, 0)
							AND cdds.Status <> 3
							AND cds.ProductionCenterId = @CostProductionCenterId
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'Ya existen las siguientes distribuciones intermedias asociadas al centro de producción: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, ''), 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END

			-- Valido exista la distribución primaria
			IF NOT EXISTS (SELECT 1 FROM Cost.CostEstimationNative WHERE Id = @CostEstimationId AND Year = @Year AND Month = @Month AND ProductionCenterId = @CostProductionCenterId)
			BEGIN
				SELECT @CodeMessageResult = 999, 
					   @MessageResult = 'No existe la distribución primaria en el periodo.', 
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

			-- Valido que el valor sea el mismo de la distribución primaria
			IF NOT EXISTS (SELECT 1 FROM Cost.CostEstimationNative WHERE Id = @CostEstimationId AND SecondaryDistribution = @Value)
			BEGIN
				SELECT @CodeMessageResult = 999, 
					   @MessageResult = 'El valor a distribuir no corresponde con el valor de la distribución Primaria.', 
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

			-- Valido que el valor a distribuir sea mayor a 0
			IF NOT ISNULL(@Value, 0) <> 0
			BEGIN
				SELECT @CodeMessageResult = 999, 
					   @MessageResult = 'El valor a distribuir debe ser mayor a 0.', 
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

			-- Valido que existan detalles
			IF NOT EXISTS (SELECT 1 FROM @Details)
			BEGIN
				SELECT @CodeMessageResult = 999, 
					   @MessageResult = 'La Distribución Intermedia no tiene detalles.', 
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

			-- Valido que no existan detalles duplicados
			--IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.ProductionCenterId, d.MeasurementUnitId HAVING COUNT(*) > 1)
			--BEGIN
			--	SELECT @CodeMessageResult = 999, 
			--		   @MessageResult = 'La Distribución Intermedia tiene detalles duplicados.', 
			--		   @IdResult = 0, 
			--		   @CodeResult = '' 
			--	RETURN
			--END

			-- Si no es un centro de producción logísitico
			IF EXISTS (SELECT 1 FROM Cost.CostProductionCenter WHERE Id = @CostProductionCenterId AND CenterType <> 3)
			BEGIN
				-- Valido existan los centros de producción
				IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Cost.CostProductionCenter cpc ON d.ProductionCenterId = cpc.Id AND cpc.CenterType = 1 WHERE cpc.Id IS NULL)
				BEGIN
					SELECT @CodeMessageResult = 999, 
							@MessageResult = 'La distribución tiene detalles asociados a centros de producción no existentes o no operativos.', 
							@IdResult = 0, 
							@CodeResult = '' 
					RETURN
				END
			END

			-- Valido que se haya distribuido completamente el centro de producción administrativo
			IF ISNULL((SELECT SUM(d.Value) FROM @Details d), 0) <> @Value
			BEGIN
				SELECT @CodeMessageResult = 999, 
					   @MessageResult = 'No se ha distribuido completamente el Centro de Producción Administrativo. Valor en Detalle ' + (CAST(ISNULL((SELECT SUM(d.Value) FROM @Details d), 0) AS varchar(30))) + ' y Valor en Variable ' + CAST(@Value AS VARCHAR(30)),
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 510, @IdForm, NULL, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeMessageResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Distribución Intermedia'), 
							@IdResult = 0, 
							@CodeResult = ''
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO Cost.CostDistributionIntermediate
				(
					Code,IntermediateDistributionElementId,Description,Year,Month,Status,CreationUser,CreationDate,ModificationUser,ModificationDate,ConfirmUser,ConfirmDate,CostEstimationId,Value
				)
				SELECT @Code,@DistributionIntermediateId,@Description,@Year,@Month,@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,@CostEstimationId,@Value

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE Cost.CostDistributionIntermediate
					SET Code = @Code,
						IntermediateDistributionElementId = @DistributionIntermediateId,
						Description = @Description,
						Year = @Year,
						Month = @Month,
						Status = @Status,
						ModificationUser = @CodeUser,
						ModificationDate = [Common].[GETDATE](),
						ConfirmUser = @ConfirmationUser,
						ConfirmDate = @ConfirmationDate,
						CostEstimationId = @CostEstimationId,
						Value = @Value
				WHERE Id = @Id
			END

			/*************************************************************************************/

			INSERT INTO Cost.CostDistributionIntermediateDetail 
				(
					DistributionIntermediateId, ProductionCenterId, Proportion, Value
				)
				SELECT
					@Id,
					d.ProductionCenterId,
					--d.MeasurementUnitId,
					d.Percentage,
					d.Value
				FROM @Details d
				WHERE d.Id IS NULL

			UPDATE cddsd
				SET cddsd.ProductionCenterId = d.ProductionCenterId,
					cddsd.Proportion = d.Percentage,
					cddsd.Value = d.Value
			FROM Cost.CostDistributionIntermediateDetail cddsd
			JOIN @Details d ON cddsd.Id = d.Id

			/*************************************************************************************/

			--IF @Status = 2
			--BEGIN
			--	IF EXISTS (SELECT 1 FROM Cost.CostProductionCenter WHERE Id = @CostProductionCenterId AND CenterType = 3)
			--	BEGIN
			--		EXEC Cost.SP_UpdateFieldImportCost_Output @CostLogisticsProductionCenterDetail, @Code_Output OUT, @Message_Output OUT

			--		IF @Code_Output <> 0
			--		BEGIN
			--			SELECT @CodeMessageResult = 999, 
			--				   @MessageResult = ISNULL(@Message_Output, 'Error al actualizar la producción de centros logísticos'), 
			--				   @IdResult = 0, 
			--				   @CodeResult = ''
			--			RETURN
			--		END
			--	END

			--	EXEC [Cost].[SP_CalculateDistributionSecondaryNonOperating] @Id, @Code_Output OUT, @Message_Output OUT

			--	IF @Code_Output <> 0
			--	BEGIN
			--		SELECT	@CodeMessageResult = 999, 
			--				@MessageResult = ISNULL(@Message_Output, 'Error al redistribuir los centros no operativos'), 
			--				@IdResult = 0, 
			--				@CodeResult = ''
			--		RETURN
			--	END

			--	SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			--END
		END

		SELECT @CodeMessageResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Distribución Intermedia con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Distribución Intermedia con código ', @Code)
				   ELSE CONCAT('Se guardó la Distribución Intermedia con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, '')), 
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