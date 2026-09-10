-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-11
-- Description:	Procedimiento el cual se encarga de guardar una modificación de un Recaudo
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveCollectionModification_Output]
	@CollectionModificationXml AS XML,
	@CollectionModificationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @OperatingUnitId INT,
			@BudgetaryValidityId INT,
			@DocumentDate DATETIME,
			@CollectionId INT,
			@Document VARCHAR(100),
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT = 216,
			@DocumentType INT = 8,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		Id INT,
		CollectionModificationId INT,
		CollectionDetailId INT,
		Nature TINYINT,
		Value DECIMAL(18, 2)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','INT'),			
			@Code = t.x.value('Code[1]','VARCHAR(20)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
			@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','INT'),			
			@DocumentDate = t.x.value('DocumentDate[1]','DATETIME'),
			@CollectionId = t.x.value('CollectionId[1]','INT'),
			@Document = t.x.value('Document[1]','VARCHAR(100)'),
			@Observations = t.x.value('Observations[1]','VARCHAR(MAX)'),			
			@Status = t.x.value('Status[1]','TINYINT'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @CollectionModificationXml.nodes('/CollectionModification') t(x)

		IF EXISTS (SELECT 1 FROM Budget.CollectionModification r WHERE r.Id = @Id AND r.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La modificación del Recaudo se encuentra en estado: ' + IIF(r.Status = 2, 'Confirmado', 'Anulado'), 
				   @Id = 0, 
				   @CodeResult = '' 
			FROM Budget.CollectionModification r 
			WHERE r.Id = @Id
			RETURN
		END

		IF @Status = 3
		BEGIN
			UPDATE [Budget].[CollectionModification]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		BEGIN
			--Eliminamos los detalles indicados
			DELETE rd
			FROM @CollectionModificationDetailForDeleteXml.nodes('/CollectionModificationDetail') t(x)
			JOIN Budget.CollectionModificationDetail rd ON t.x.value('Id[1]','int') = rd.Id
			WHERE rd.CollectionModificationId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT 
					t.x.value('Id[1]','INT') AS Id,
					t.x.value('CollectionModificationId[1]','INT') AS CollectionModificationId,
					t.x.value('CollectionDetailId[1]','INT') AS CollectionDetailId,
					t.x.value('Nature[1]','INT') AS Nature,
					t.x.value('Value[1]','DECIMAL(18, 2)') AS Value
				FROM @CollectionModificationXml.nodes('/CollectionModification/CollectionModificationDetail') t(x)
			
			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					rd.Id, 
					rd.CollectionModificationId, 
					rd.CollectionDetailId, 
					rd.Nature,
					rd.Value
				FROM Budget.CollectionModificationDetail rd
				LEFT JOIN @Details d ON rd.Id = d.Id
				WHERE rd.CollectionModificationId = @Id AND ISNULL(d.Id, 0) = 0

			/*************************************VALIDACIONES************************************/

			--- Valido el Periodo de la Vigencia
			IF NOT EXISTS 
			(
				SELECT 1 
				FROM Budget.BudgetaryValidity bv 
				WHERE bv.Id = @BudgetaryValidityId
					AND bv.Year = YEAR(@DocumentDate)
					AND bv.IncomeMonth <= MONTH(@DocumentDate)
			)
			BEGIN
				SELECT @Message = 'La Fecha de la modificación del Recaudo (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.IncomeMonth AS VARCHAR), 2)) + ').'
				FROM Budget.BudgetaryValidity bv
				WHERE bv.Id = @BudgetaryValidityId

				SELECT @CodeResult = 999, 
					   @MessageResult = ISNULL(@Message, 'Periodo Presupuestal no encontrado.'), 
					   @Id = 0, 
					   @Code = ''
				RETURN
			END

			--- Valido que existan detalles
			IF NOT EXISTS (SELECT 1 FROM @Details)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La modificación del Recaudo no tiene detalles.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.CollectionModificationDetail rd JOIN @Details d ON rd.Id = d.Id WHERE rd.CollectionModificationId <> @Id OR rd.CollectionDetailId <> d.CollectionDetailId) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles de la modificación del Recaudo han sido alterados.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.CollectionDetailId HAVING COUNT(*) > 1)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La modificación del Recaudo tiene detalles duplicados.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			--- Valido que no existan detalles con valores en 0 o negativos
			IF EXISTS (SELECT 1 FROM @Details d WHERE d.Value <= 0)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Existen Rubros con el valor Inicial menor o igual a 0.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			--- Valido la naturaleza de los detalles
			IF EXISTS (SELECT 1 FROM @Details WHERE Nature NOT IN (1, 2))
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
						FROM @Details d
						JOIN Budget.CollectionDetail cd WITH (NOLOCK) ON d.CollectionDetailId = cd.Id
						JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON cd.RecognitionDetailId = rd.Id
						JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
						JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id
						WHERE d.Nature NOT IN (1, 2)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'La naturaleza de los siguientes rubros de la Modificación de Recaudo no son válidas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, ''), 
					   @Id = 0, 
					   @CodeResult = '' 
				RETURN
			END

			--- Valido las modificaciones debito no superen el saldo del recaudo
			IF EXISTS 
			(
				SELECT 1 
				FROM 
				(
					SELECT 
						d.CollectionDetailId,
						SUM(d.Value) Value
					FROM @Details d 
					WHERE d.Nature = 1
					GROUP BY d.CollectionDetailId
				) d 
				JOIN Budget.CollectionDetail cd WITH (NOLOCK) ON d.CollectionDetailId = cd.Id
				WHERE d.Value > cd.Balance
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
						FROM 
						(
							SELECT 
								d.CollectionDetailId,
								SUM(d.Value) Value
							FROM @Details d 
							WHERE d.Nature = 1
							GROUP BY d.CollectionDetailId
						) d 
						JOIN Budget.CollectionDetail cd WITH (NOLOCK) ON d.CollectionDetailId = cd.Id
						JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON cd.RecognitionDetailId = rd.Id
						JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
						JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id				
						WHERE d.Value > cd.Balance
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999, 
						@MessageResult = 'El valor débito de los siguientes rubros de la modificación del Recaudo no pueden ser mayor que el saldo del Recaudo: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, ''), 
						@Id = 0, 
						@Code = '' 
				RETURN
			END

			--- Valido las modificaciones credito no superen el saldo del reconocimiento
			IF EXISTS 
			(
				SELECT 1 
				FROM 
				(
					SELECT 
						cd.RecognitionDetailId,
						SUM(d.Value) Value
					FROM @Details d 
					JOIN Budget.CollectionDetail cd WITH (NOLOCK) ON d.CollectionDetailId = cd.Id
					WHERE d.Nature = 2
					GROUP BY cd.RecognitionDetailId
				) d 
				JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON d.RecognitionDetailId = rd.Id
				WHERE d.Value > rd.Balance
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
						FROM 
						(
							SELECT 
								cd.RecognitionDetailId,
								SUM(d.Value) Value
							FROM @Details d 
							JOIN Budget.CollectionDetail cd WITH (NOLOCK) ON d.CollectionDetailId = cd.Id
							WHERE d.Nature = 2
							GROUP BY cd.RecognitionDetailId
						) d 
						JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON d.RecognitionDetailId = rd.Id
						JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
						JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id				
						WHERE d.Value > rd.Balance
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999, 
						@MessageResult = 'El valor crédito de los siguientes rubros de la modificación del Recaudo no pueden ser mayor que el saldo del Reconocimiento: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, ''), 
						@Id = 0, 
						@Code = '' 
				RETURN
			END

			-- Si maneja PAC
			IF EXISTS (SELECT 1 FROM Budget.BudgetaryValidity bv WHERE bv.Id = @BudgetaryValidityId AND bv.PACControl = 1)
			BEGIN
				IF EXISTS 
				(
					SELECT 1 
					FROM 
					(
						SELECT 
							rd.CategoryId,
							SUM(d.Value) Value
						FROM @Details d 
						JOIN Budget.CollectionDetail cd WITH (NOLOCK) ON d.CollectionDetailId = cd.Id
						JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON cd.RecognitionDetailId = rd.Id
						WHERE d.Nature = 2
						GROUP BY rd.CategoryId
					) od 
					LEFT JOIN Budget.AnnualizedCashFlow acf ON od.CategoryId = acf.CategoryId AND MONTH(@DocumentDate) = acf.Month
					WHERE od.Value > ISNULL(acf.Balance, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ': Balance (' + FORMAT(ISNULL(acf.Balance, 0), 'C0', 'es-CO') + ') - Modificación (' + FORMAT(od.Value, 'C0', 'es-CO') + ')'
							FROM 
							(
								SELECT 
									rd.CategoryId,
									SUM(d.Value) Value
								FROM @Details d 
								JOIN Budget.CollectionDetail cd WITH (NOLOCK) ON d.CollectionDetailId = cd.Id
								JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON cd.RecognitionDetailId = rd.Id
								WHERE d.Nature = 2
								GROUP BY rd.CategoryId
							) od
							LEFT JOIN Budget.AnnualizedCashFlow acf ON od.CategoryId = acf.CategoryId AND MONTH(@DocumentDate) = acf.Month
							JOIN Budget.Category c WITH (NOLOCK) ON od.CategoryId = c.Id
							WHERE od.Value > ISNULL(acf.Balance, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
							@MessageResult = 'El valor del detalle de los siguientes rubros de la Orden de Pago no puede ser mayor que el saldo del PAC del mes (' + CAST(MONTH(@DocumentDate) AS VARCHAR(20)) + '): ' + CHAR(13) + CHAR(10) + ISNULL(@Message, ''), 
							@Id = 0, 
							@Code = '' 
					RETURN
				END
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 200, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Modificacion de Recaudos'), 
							@Id = 0, 
							@Code = ''
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[CollectionModification]
				(
					[Code],[BudgetaryValidityId],[DocumentDate],[CollectionId],[Document],[Observations],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[EntityId],[EntityCode],[EntityName]
				)
				SELECT @Code,@BudgetaryValidityId,@DocumentDate,@CollectionId,@Document,@Observations,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@EntityId,@EntityCode,@EntityName

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[CollectionModification]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[CollectionId] = @CollectionId,
						[Document] = @Document,
						[Observations] = @Observations,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id
			END

			/*************************************************************************************/

			INSERT INTO Budget.CollectionModificationDetail 
			(
				CollectionModificationId, CollectionDetailId, Nature, Value
			)
			SELECT
				@Id CollectionModificationId, d.CollectionDetailId, d.Nature, d.Value
			FROM @Details d
			WHERE ISNULL(d.Id, 0) = 0

			UPDATE cd
				SET cd.Nature = d.Nature,
					cd.Value = d.Value
			FROM Budget.CollectionModificationDetail cd
			JOIN @Details d ON cd.Id = d.Id
			WHERE cd.CollectionModificationId = @Id

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				-- Si maneja PAC actualizamos el valor ejecutado
				IF EXISTS (SELECT 1 FROM Budget.BudgetaryValidity bv WHERE bv.Id = @BudgetaryValidityId AND bv.PACControl = 1)
				BEGIN
					UPDATE acf
						SET acf.ExecutedValue = acf.ExecutedValue - od.value,
							acf.Balance = acf.Balance + od.Value
					FROM 
					(
						SELECT 
							rd.CategoryId,
							SUM(cmd.Value * IIF(cmd.Nature = 1, 1, -1)) Value
						FROM Budget.CollectionModificationDetail cmd
						JOIN Budget.CollectionDetail cd ON cmd.CollectionDetailId = cd.Id
						JOIN Budget.RecognitionDetail rd ON cd.RecognitionDetailId = rd.Id
						WHERE cmd.CollectionModificationId = @Id
						GROUP BY rd.CategoryId
					) od 
					JOIN Budget.AnnualizedCashFlow acf ON od.CategoryId = acf.CategoryId AND MONTH(@DocumentDate) = acf.Month
				END

				UPDATE cd
					SET
						cd.DebitModificationValue = cd.DebitModificationValue + IIF(cmd.Nature = 1, cmd.Value, 0),
						cd.CreditModificationValue = cd.CreditModificationValue + IIF(cmd.Nature = 2, cmd.Value, 0),
						cd.Balance = cd.Balance - (cmd.Value * IIF(cmd.Nature = 1, 1, -1))
				FROM Budget.CollectionModificationDetail cmd
				JOIN Budget.CollectionDetail cd ON cmd.CollectionDetailId = cd.Id
				WHERE cmd.CollectionModificationId = @Id

				UPDATE rd
					SET rd.ExecutedValue = rd.ExecutedValue - cd.Value,
						rd.Balance = rd.Balance + cd.Value
				FROM Budget.RecognitionDetail rd
				JOIN 
				(
					SELECT 
						cd.RecognitionDetailId,
						SUM(cmd.Value * IIF(cmd.Nature = 1, 1, -1)) Value
					FROM Budget.CollectionModificationDetail cmd
					JOIN Budget.CollectionDetail cd ON cmd.CollectionDetailId = cd.Id
					WHERE cmd.CollectionModificationId = @Id
					GROUP BY cd.RecognitionDetailId
				) cd ON rd.Id = cd.RecognitionDetailId
			END
		END

		/************************************* TABLA DE CONTROL ************************************/

		IF @Status = 1
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Budget.BudgetControl WHERE DocumentType = @DocumentType AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Budget.BudgetControl (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentType, @CodeUser, @DocumentDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Budget.BudgetControl WHERE DocumentType = @DocumentType AND DocumentNumber = @Code
		END	

		/************************************* VALIDACIONES GENERALES ************************************/

		SELECT @CodeResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó La modificación del Recaudo con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló La modificación del Recaudo con código ', @Code)
				   ELSE CONCAT('Se guardó La modificación del Recaudo con código ', @Code)
			   END
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)), 
			   @Id = 0, 
			   @Code = ''
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o anula una modificación de recaudo (cobro presupuestario) a partir de datos enviados en formato XML. Procesa la cabecera de la modificación y sus líneas de detalle: elimina los ítems marcados para borrar, inserta o actualiza los nuevos ajustes de valor por naturaleza de movimiento, y valida que la fecha del documento corresponda a la vigencia presupuestaria activa, que no existan detalles duplicados ni con valores en cero o negativos, y que la modificación no esté ya confirmada o anulada. Cuando el estado indicado es de anulación, registra el usuario y la fecha de anulación en la tabla CollectionModification. Retorna códigos de resultado, mensajes de error y el identificador generado para uso del llamador.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCollectionModification_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCollectionModification_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea, actualiza o anula) una modificación de Recaudo presupuestal con sus detalles, validando vigencia, naturaleza, saldos del recaudo, reconocimiento y PAC, y aplicando los efectos contables al confirmarla.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La modificación referenciada por @Id debe estar en estado 1 (no Confirmado=2 ni Anulado=3) para poder modificarse.; La vigencia presupuestal indicada debe existir y su Year/IncomeMonth ser compatible con la fecha del documento (Year=YEAR(@DocumentDate) y IncomeMonth<=MONTH(@DocumentDate)).; El XML debe traer al menos un detalle (o existir detalles previos no eliminados) para procesar.; Los detalles editados no pueden cambiar su CollectionModificationId ni su CollectionDetailId respecto a lo persistido.; No se permiten detalles duplicados por CollectionDetailId.; Todos los detalles deben tener Value > 0 y Nature ∈ {1=Débito, 2=Crédito}.; El total débito por CollectionDetailId no puede superar CollectionDetail.Balance.; El total crédito agrupado por RecognitionDetailId no puede superar RecognitionDetail.Balance.; Si la vigencia tiene PACControl=1, el total crédito por CategoryId no puede superar el AnnualizedCashFlow.Balance del mes del documento.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Budget.CollectionModification: Si @Status=3 (anulación), marca la cabecera con Status, ModificationUser/Date y AnnulmentUser/Date = usuario y GETDATE actuales.; [DELETE] Budget.CollectionModificationDetail: Elimina los detalles cuyos Id vengan en @CollectionModificationDetailForDeleteXml y pertenezcan a la modificación @Id.; [INSERT] Budget.CollectionModification: Cuando @Id=0, obtiene el código vía Common.SP_GetSequence (forma 216) e inserta la cabecera con CreationUser/Date, y ConfirmationUser/Date solo si @Status=2.; [UPDATE] Budget.CollectionModification: Cuando @Id<>0, actualiza la cabecera con los datos del XML, ModificationUser/Date y ConfirmationUser/Date solo si @Status=2.; [INSERT] Budget.CollectionModificationDetail: Inserta los detalles del XML cuyo Id es 0 (nuevos), asociándolos a la modificación @Id con su Nature y Value.; [UPDATE] Budget.CollectionModificationDetail: Para los detalles existentes incluidos en el XML, actualiza Nature y Value coincidiendo por Id y CollectionModificationId=@Id.; [UPDATE] Budget.AnnualizedCashFlow: Si @Status=2 y la vigencia tiene PACControl=1, ajusta por CategoryId y mes del documento: ExecutedValue -= sumValue y Balance += sumValue, donde sumValue = Σ(Value * (1 si Nature=1 else -1)).; [UPDATE] Budget.CollectionDetail: Si @Status=2, suma al DebitModificationValue cuando Nature=1, al CreditModificationValue cuando Nature=2, y ajusta Balance restando (Value si débito, sumando si crédito).; [UPDATE] Budget.RecognitionDetail: Si @Status=2, ajusta por RecognitionDetailId: ExecutedValue -= netoDébito y Balance += netoDébito, donde neto = Σ(Value * (1 si Nature=1 else -1)).; [INSERT] Budget.BudgetControl: Si @Status=1 y no existe registro con DocumentType=8 y DocumentNumber=@Code, inserta el control con usuario y fecha del documento.; [DELETE] Budget.BudgetControl: Si @Status<>1 (confirmado o anulado), elimina el registro de control cuyo DocumentType=8 y DocumentNumber=@Code.; [RETURN_RESULT] @CodeResult/@MessageResult: Retorna @CodeResult=0 con mensaje de éxito diferenciado (guardado, guardado y confirmado, o anulado); en cualquier validación fallida o excepción retorna @CodeResult=999 con el mensaje de error.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe la cabecera con Status<>1 (ya Confirmada=2 o Anulada=3) → Aborta con código 999 informando el estado actual. else Continúa con el procesamiento.; si @Status = 3 → Actualiza cabecera marcando anulación (AnnulmentUser/Date).; si @Id = 0 (alta nueva) → Obtiene secuencia con Common.SP_GetSequence (formId=216) e INSERTA la cabecera. else UPDATE de la cabecera existente.; si @Status = 2 (confirmación) → Aplica efectos contables: ajusta CollectionDetail (DebitModificationValue/CreditModificationValue/Balance), RecognitionDetail (ExecutedValue/Balance) y, si PACControl=1, AnnualizedCashFlow del mes.; si Vigencia con PACControl = 1 → Valida disponible PAC mensual por categoría antes de confirmar y, al confirmar, actualiza AnnualizedCashFlow.; si @Status = 1 (borrador) vs distinto de 1 → Si =1 inserta en BudgetControl si no existe; si <>1 elimina el control existente para DocumentType=8 y @Code.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification_Output';
-- GO
