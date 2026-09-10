-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-09-02
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una distribución secundaria
-- =============================================
CREATE PROCEDURE [Cost].[SP_SaveDistributionSecondary_Output]
    @DistributionSecondaryXml AS XML,
	@DistributionSecondaryDetailForDeleteXml AS XML,
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
			@OperatingUnitId INT,
			@Code VARCHAR(20),
			@DistributionSecondaryId INT,
			@CostProductionCenterId INT,
			@Year INT,
			@Month INT,
			@CostEstimationId INT,
			@Value DECIMAL(20,4),
			@Description VARCHAR(300),			
			@Status TINYINT,
			------------------------------			
			@IdForm INT = 1938,
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
		MeasurementUnitId INT,
		Percentage DECIMAL(5,2),
		Value DECIMAL(20,4)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DistributionSecondaryId = t.x.value('DistributionSecondaryId[1]','int'),
			@Year = t.x.value('Year[1]','int'),
			@Month = t.x.value('Month[1]','int'),
			@CostEstimationId = t.x.value('CostEstimationId[1]','int'),
			@Value = t.x.value('Value[1]','decimal(20,4)'),
			@Description = t.x.value('Description[1]','varchar(300)'),
			@Status = t.x.value('Status[1]','tinyint')
		FROM @DistributionSecondaryXml.nodes('/CostDirectDistributionSecondary') t(x)

		IF EXISTS (SELECT 1 FROM Cost.CostDirectDistributionSecondary cdds WHERE cdds.Id = @Id AND cdds.Status <> 1)
		BEGIN
			SELECT @CodeMessageResult = 999, 
				   @MessageResult = 'La Distribución Secundaria se encuentra en estado: ' + IIF(cdds.Status = 2, 'Confirmado', 'Anulado'), 
				   @IdResult = 0, 
				   @CodeResult = '' 
			FROM Cost.CostDirectDistributionSecondary cdds 
			WHERE cdds.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE Cost.CostDirectDistributionSecondary
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
			FROM @DistributionSecondaryDetailForDeleteXml.nodes('/DistributionSecondaryDetail') t(x)
			JOIN Cost.CostDirectDistributionSecondaryDetail cddsd ON t.x.value('Id[1]','int') = cddsd.Id
			WHERE cddsd.DirectDistributionSecondaryId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('DirectDistributionSecondaryId[1]','int'),
					t.x.value('ProductionCenterId[1]','int'),
					t.x.value('MeasurementUnitId[1]','int'),					
					t.x.value('Percentage[1]','decimal(5,2)'),
					t.x.value('Value[1]','decimal(20,4)')
				FROM @DistributionSecondaryXml.nodes('/CostDirectDistributionSecondary/CostDirectDistributionSecondaryDetail') t(x)

			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					cddsd.Id, 
					cddsd.DirectDistributionSecondaryId, 
					cddsd.ProductionCenterId,
					cddsd.MeasurementUnitId,
					cddsd.Percentage,
					cddsd.Value
				FROM Cost.CostDirectDistributionSecondaryDetail cddsd
				LEFT JOIN @Details d ON cddsd.Id = d.Id
				WHERE cddsd.DirectDistributionSecondaryId = @Id AND d.Id IS NULL

			SELECT @CostProductionCenterId = cds.ProductionCenterId
			FROM Cost.CostDistributionSecondary cds
			WHERE cds.Id = @DistributionSecondaryId

			/*************************************VALIDACIONES************************************/

			-- Valido el Periodo de la Vigencia
			IF NOT EXISTS 
			(
				SELECT 1 
				FROM Cost.CostSetting cs
				WHERE cs.Year = @Year AND cs.Month = @Month
			)
			BEGIN
				SELECT TOP 1 @Message = 'El periodo de la Distribución Secundaria ' + CONCAT(@Year, '-', RIGHT('00' + CAST(@Month AS VARCHAR), 2)) + ' no corresponde con el periodo actual de Costos.' + CONCAT(cs.Year, '-', RIGHT('00' + CAST(cs.Month AS VARCHAR), 2)) + ').'
				FROM Cost.CostSetting cs

				SELECT @CodeMessageResult = 999, 
					   @MessageResult = ISNULL(@Message, 'Parámetros de Costos no encontrado.'), 
					   @IdResult = 0, 
					   @CodeResult = ''
				RETURN
			END

			-- Valido el Periodo no se encuentre ya cerrado o distribuido
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
				FROM Cost.CostDirectDistributionSecondary cdds
				JOIN Cost.CostDistributionSecondary cds ON cdds.DistributionSecondaryId = cds.Id
				WHERE Year = @Year AND Month = @Month 
					AND cdds.Id <> ISNULL(@Id, 0)
					AND cdds.Status <> 3
					AND cds.ProductionCenterId = @CostProductionCenterId
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + cdds.Code
						FROM Cost.CostDirectDistributionSecondary cdds
						JOIN Cost.CostDistributionSecondary cds ON cdds.DistributionSecondaryId = cds.Id
						WHERE Year = @Year AND Month = @Month 
							AND cdds.Id <> ISNULL(@Id, 0)
							AND cdds.Status <> 3
							AND cds.ProductionCenterId = @CostProductionCenterId
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'Ya existen las siguientes distribuciones secundarias asociadas al centro de producción: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, ''), 
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
			IF NOT EXISTS (SELECT 1 FROM Cost.CostEstimationNative WHERE Id = @CostEstimationId AND InitialDistribution = @Value)
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
					   @MessageResult = 'La Distribución Secundaria no tiene detalles.', 
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

			-- Valido que no existan detalles duplicados
			IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.ProductionCenterId, d.MeasurementUnitId HAVING COUNT(*) > 1)
			BEGIN
				SELECT @CodeMessageResult = 999, 
					   @MessageResult = 'La Distribución Secundaria tiene detalles duplicados.', 
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

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
				
				EXEC Common.SP_GetSequence 510, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeMessageResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Distribución Secundaria'), 
							@IdResult = 0, 
							@CodeResult = ''
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO Cost.CostDirectDistributionSecondary
				(
					Code,DistributionSecondaryId,Description,Year,Month,Status,CreationUser,CreationDate,ModificationUser,ModificationDate,ConfirmUser,ConfirmDate,CostEstimationId,Value
				)
				SELECT @Code,@DistributionSecondaryId,@Description,@Year,@Month,@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,@CostEstimationId,@Value

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE Cost.CostDirectDistributionSecondary
					SET Code = @Code,
						DistributionSecondaryId = @DistributionSecondaryId,
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

			INSERT INTO Cost.CostDirectDistributionSecondaryDetail 
				(
					DirectDistributionSecondaryId, ProductionCenterId, MeasurementUnitId, Percentage, Value
				)
				SELECT
					@Id DirectDistributionSecondaryId,
					d.ProductionCenterId,
					d.MeasurementUnitId,
					d.Percentage,
					d.Value
				FROM @Details d
				WHERE d.Id IS NULL

			UPDATE cddsd
				SET cddsd.ProductionCenterId = d.ProductionCenterId,
					cddsd.MeasurementUnitId = d.MeasurementUnitId, 
					cddsd.Percentage = d.Percentage,
					cddsd.Value = d.Value
			FROM Cost.CostDirectDistributionSecondaryDetail cddsd
			JOIN @Details d ON cddsd.Id = d.Id

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				IF EXISTS (SELECT 1 FROM Cost.CostProductionCenter WHERE Id = @CostProductionCenterId AND CenterType = 3)
				BEGIN
					EXEC Cost.SP_UpdateFieldImportCost_Output @CostLogisticsProductionCenterDetail, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT @CodeMessageResult = 999, 
							   @MessageResult = ISNULL(@Message_Output, 'Error al actualizar la producción de centros logísticos'), 
							   @IdResult = 0, 
							   @CodeResult = ''
						RETURN
					END
				END

				EXEC [Cost].[SP_CalculateDistributionSecondaryNonOperating] @Id, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeMessageResult = 999, 
							@MessageResult = ISNULL(@Message_Output, 'Error al redistribuir los centros no operativos'), 
							@IdResult = 0, 
							@CodeResult = ''
					RETURN
				END

				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			END
		END

		SELECT @CodeMessageResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Distribución Secundaria con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Distribución Secundaria con código ', @Code)
				   ELSE CONCAT('Se guardó la Distribución Secundaria con código ', @Code)
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda, actualiza, confirma o anula una distribución secundaria de costos en el módulo de costeo. Recibe los datos de la cabecera y el detalle en formato XML, validando que el período contable esté activo y no cerrado, que el centro de producción sea administrativo o logístico, y que la distribución no esté ya confirmada o anulada antes de permitir cambios. Opera principalmente sobre las tablas CostDirectDistributionSecondary (cabecera de la distribución secundaria) y CostDirectDistributionSecondaryDetail (detalles por centro de producción, unidad de medida, porcentaje y valor), permitiendo eliminar detalles específicos, insertar o actualizar los nuevos, y en caso de anulación (estado 3) registra el usuario y fecha de anulación. Retorna códigos de resultado y mensajes de error de negocio para informar al sistema llamante si la operación fue exitosa o si se incumplió alguna regla del proceso de distribución de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDistributionSecondary_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDistributionSecondary_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, actualiza, confirma o anula una distribución secundaria directa de costos junto con sus detalles por centro de producción, validando período, valores y dispara recálculos al confirmar.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El registro cabecera, si existe, debe estar en estado 1 (no Confirmado ni Anulado) para permitir modificaciones.; Debe existir configuración de costos (CostSetting) para el año/mes indicados.; El período (año/mes) no debe estar cerrado en ClosedMonth ni tener distribución secundaria ya aplicada en CostEstimationNative (SecondaryDistribution > 0).; El centro de producción a distribuir no puede ser de tipo operativo (CenterType <> 1).; No debe existir otra distribución secundaria activa (Status<>3) para el mismo centro de producción en el mismo período.; Debe existir la distribución primaria (CostEstimationNative) para el centro/periodo indicado.; El valor a distribuir debe coincidir con InitialDistribution de la distribución primaria y ser distinto de 0.; Los detalles deben existir, no estar duplicados por (ProductionCenterId, MeasurementUnitId), y la suma de Value debe igualar el valor de cabecera.; Si el centro no es logístico (CenterType<>3), todos los detalles deben referenciar centros operativos (CenterType=1) existentes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una distribución en estado Confirmado (2) o Anulado (3) no puede modificarse.; Sólo puede haber una distribución secundaria activa (Status<>3) por centro de producción y período.; El total de Value de los detalles siempre debe igualar el Value de la cabecera al guardar.; El centro de producción a distribuir nunca puede ser operativo (CenterType=1).; El valor a distribuir siempre debe coincidir con InitialDistribution de la distribución primaria del mismo centro/período.; No se permite operar sobre períodos cerrados (ClosedMonth) o ya distribuidos secundariamente.; Para centros no logísticos, todos los detalles deben apuntar a centros operativos existentes.; La numeración del código se delega a Common.SP_GetSequence sólo en altas nuevas.; Errores se capturan en CATCH y se devuelven con código 999 y línea de error.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución secundaria de costos; Distribución primaria de costos; Centro de producción (operativo, administrativo, logístico); Período de costos (año/mes); Cierre contable de mes; Confirmación y anulación de distribución; Unidad de medida e inductor de distribución; Redistribución de centros no operativos', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Cost.CostDirectDistributionSecondary: Cuando @Status=3 se anula la cabecera asignando Status, ModificationUser/Date y AnnulmentUser/Date con el usuario y fecha actuales.; [DELETE] Cost.CostDirectDistributionSecondaryDetail: Se eliminan los detalles cuyos Id vienen en @DistributionSecondaryDetailForDeleteXml y pertenecen a la cabecera (DirectDistributionSecondaryId=@Id), siempre que no se esté anulando.; [INSERT] Cost.CostDirectDistributionSecondary: Cuando @Id=0 y pasan validaciones, se inserta nueva cabecera con código obtenido por Common.SP_GetSequence; si @Status=2 se llenan ConfirmUser/ConfirmDate con usuario y fecha actuales.; [UPDATE] Cost.CostDirectDistributionSecondary: Cuando @Id<>0 y no se anula, se actualiza la cabecera con los datos del XML; ConfirmUser/Date se asigna sólo si @Status=2.; [INSERT] Cost.CostDirectDistributionSecondaryDetail: Se insertan los detalles del XML que no tienen Id (nuevos) asociados al Id de cabecera.; [UPDATE] Cost.CostDirectDistributionSecondaryDetail: Se actualizan ProductionCenterId, MeasurementUnitId, Percentage y Value de los detalles existentes que coinciden por Id con los recibidos.; [RETURN_RESULT] resultado: Devuelve CodeMessageResult=0 con mensaje según acción (guardar / guardar y confirmar / anular) más el código generado; en validaciones fallidas devuelve 999 con mensaje específico.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe cabecera con Id=@Id y Status<>1 → Aborta con mensaje indicando si está Confirmado (2) o Anulado (3) y código 999.; si @Status = 3 → Sólo anula la cabecera (sin tocar detalles ni validaciones de período/valores). else Ejecuta el flujo de guardar/actualizar: borra detalles indicados, valida período/centro/valores y persiste cabecera y detalles.; si @Id = 0 (alta nueva) → Obtiene código vía Common.SP_GetSequence (tipo 510, formulario 1938) e inserta cabecera nueva. else Actualiza la cabecera existente.; si @Status = 2 (confirmar) y centro de producción es logístico (CenterType=3) → Ejecuta Cost.SP_UpdateFieldImportCost_Output para actualizar producción de centros logísticos antes de calcular.; si @Status = 2 (confirmar) → Ejecuta Cost.SP_CalculateDistributionSecondaryNonOperating para redistribuir centros no operativos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Cost.SP_UpdateFieldImportCost_Output; Cost.SP_CalculateDistributionSecondaryNonOperating; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDirectDistributionSecondary; Cost.CostDirectDistributionSecondaryDetail; Cost.CostDistributionSecondary; Cost.CostSetting; Cost.ClosedMonth; Cost.CostEstimationNative; Cost.CostProductionCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDistributionSecondary_Output';
-- GO
