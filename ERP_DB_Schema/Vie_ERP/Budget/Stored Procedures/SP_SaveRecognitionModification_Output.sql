-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-11
-- Description:	Procedimiento el cual se encarga de guardar una modificación de un Reconocimiento
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveRecognitionModification_Output]
	@RecognitionModificationXml AS XML,
	@RecognitionModificationDetailForDeleteXml AS XML,
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
			@RecognitionId INT,
			@Document VARCHAR(100),
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT = 214,
			@DocumentType INT = 6,
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
		RecognitionModificationId INT,
		RecognitionDetailId INT,
		CategoryId INT,
		RevenueTypeId INT,
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
			@RecognitionId = t.x.value('RecognitionId[1]','INT'),
			@Document = t.x.value('Document[1]','VARCHAR(100)'),
			@Observations = t.x.value('Observations[1]','VARCHAR(MAX)'),			
			@Status = t.x.value('Status[1]','TINYINT'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @RecognitionModificationXml.nodes('/RecognitionModification') t(x)

		IF EXISTS (SELECT 1 FROM Budget.RecognitionModification r WHERE r.Id = @Id AND r.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La modificación del Reconocimiento se encuentra en estado: ' + IIF(rm.Status = 2, 'Confirmado', 'Anulado'), 
				   @Id = 0, 
				   @CodeResult = '' 
			FROM Budget.RecognitionModification rm 
			WHERE rm.Id = @Id
			RETURN
		END

		IF @Status = 3
		BEGIN
			UPDATE [Budget].[RecognitionModification]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		BEGIN
			--Eliminamos los detalles indicados
			DELETE rmd
			FROM @RecognitionModificationDetailForDeleteXml.nodes('/RecognitionModificationDetail') t(x)
			JOIN Budget.RecognitionModificationDetail rmd ON t.x.value('Id[1]','int') = rmd.Id
			WHERE rmd.RecognitionModificationId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT 
					t.x.value('Id[1]','INT') AS Id,
					t.x.value('RecognitionModificationId[1]','INT') AS RecognitionModificationId,
					t.x.value('RecognitionDetailId[1]','INT') AS RecognitionDetailId,
					t.x.value('CategoryId[1]','INT') AS RecognitionDetailId,
					t.x.value('RevenueTypeId[1]','INT') AS RecognitionDetailId,
					t.x.value('Nature[1]','INT') AS Nature,
					t.x.value('Value[1]','DECIMAL(18, 2)') AS Value
				FROM @RecognitionModificationXml.nodes('/RecognitionModification/RecognitionModificationDetail') t(x)
			
			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					rmd.Id, 
					rmd.RecognitionModificationId, 
					rmd.RecognitionDetailId, 
					rd.CategoryId, 
					rd.RevenueTypeId,
					rmd.Nature,
					rmd.Value
				FROM Budget.RecognitionModificationDetail rmd
				JOIN Budget.RecognitionDetail rd ON rmd.RecognitionDetailId = rd.Id
				LEFT JOIN @Details d ON rmd.Id = d.Id
				WHERE rmd.RecognitionModificationId = @Id AND ISNULL(d.Id, 0) = 0

			UPDATE d
				SET d.RecognitionDetailId = rd.Id
			FROM @Details d
			JOIN Budget.RecognitionDetail rd 
				ON @RecognitionId = rd.RecognitionId
					AND d.CategoryId = rd.CategoryId
					AND d.RevenueTypeId = rd.RevenueTypeId
			WHERE ISNULL(d.RecognitionDetailId, 0) = 0

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
				SELECT @Message = 'La Fecha de la modificación del Reconocimiento (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.IncomeMonth AS VARCHAR), 2)) + ').'
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
					   @MessageResult = 'La modificación del Reconocimiento no tiene detalles.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.RecognitionModificationDetail rmd JOIN @Details d ON rmd.Id = d.Id WHERE rmd.RecognitionModificationId <> @Id OR rmd.RecognitionDetailId <> ISNULL(d.RecognitionDetailId, 0)) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles de la modificación del Reconocimiento han sido alterados.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.RecognitionDetail rd JOIN @Details d ON rd.Id = d.RecognitionDetailId WHERE rd.CategoryId <> ISNULL(d.CategoryId, 0) OR rd.RevenueTypeId <> ISNULL(d.RevenueTypeId, 0)) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los rubros de los detalles de la modificación han sido alterados.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				LEFT JOIN Budget.RecognitionDetail rd ON d.RecognitionDetailId = rd.Id 
				GROUP BY rd.Id, ISNULL(rd.CategoryId, d.CategoryId), ISNULL(rd.RevenueTypeId, d.RevenueTypeId)
				HAVING COUNT(*) > 1
			)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La modificación del Reconocimiento tiene detalles duplicados.', 
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
						JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON d.RecognitionDetailId = rd.Id
						JOIN Budget.Category c WITH (NOLOCK) ON rd.CategoryId = c.Id
						JOIN Budget.RevenueType rt WITH (NOLOCK) ON rd.RevenueTypeId = rt.Id
						WHERE d.Nature NOT IN (1, 2)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'La naturaleza de los siguientes rubros de la Modificación de Reconocimiento no son válidas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, ''), 
					   @Id = 0, 
					   @CodeResult = '' 
				RETURN
			END

			--- Valido las modificaciones debito no superen el saldo del Reconocimiento
			IF EXISTS 
			(
				SELECT 1 
				FROM 
				(
					SELECT 
						d.RecognitionDetailId,
						SUM(d.Value) Value
					FROM @Details d 
					WHERE d.Nature = 1
					GROUP BY d.RecognitionDetailId
				) d 
				LEFT JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON d.RecognitionDetailId = rd.Id
				WHERE d.Value > ISNULL(rd.Balance, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
						FROM 
						(
							SELECT 
								d.RecognitionDetailId, d.CategoryId, d.RevenueTypeId,
								SUM(d.Value) Value
							FROM @Details d 
							WHERE d.Nature = 1
							GROUP BY d.RecognitionDetailId, d.CategoryId, d.RevenueTypeId
						) d 
						LEFT JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON d.RecognitionDetailId = rd.Id
						LEFT JOIN Budget.Category c WITH (NOLOCK) ON ISNULL(rd.CategoryId, d.CategoryId) = c.Id
						LEFT JOIN Budget.RevenueType rt WITH (NOLOCK) ON ISNULL(rd.RevenueTypeId, d.CategoryId) = rt.Id				
						WHERE d.Value > ISNULL(rd.Balance, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	@CodeResult = 999, 
						@MessageResult = 'El valor débito de los siguientes rubros de la modificación del Reconocimiento no pueden ser mayor que el saldo del Reconocimiento: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, ''), 
						@Id = 0, 
						@Code = '' 
				RETURN
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
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Modificacion de Reconocimientos'), 
							@Id = 0, 
							@Code = ''
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[RecognitionModification]
				(
					[Code],[BudgetaryValidityId],[DocumentDate],[RecognitionId],[Document],[Observations],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[EntityId],[EntityCode],[EntityName]
				)
				SELECT @Code,@BudgetaryValidityId,@DocumentDate,@RecognitionId,@Document,@Observations,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@EntityId,@EntityCode,@EntityName

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[RecognitionModification]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[RecognitionId] = @RecognitionId,
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

			INSERT INTO Budget.RecognitionDetail
			(
				RecognitionId, CategoryId, RevenueTypeId, InitialValue, DebitValueModification, CreditValueModification, TotalRecognition, ExecutedValue, Balance
			)
			SELECT
				@RecognitionId, d.CategoryId, d.RevenueTypeId, 0, 0, 0, 0, 0, 0
			FROM @Details d
			LEFT JOIN Budget.RecognitionDetail rd ON @RecognitionId = rd.RecognitionId AND d.RecognitionDetailId = rd.Id
			WHERE rd.Id IS NULL

			UPDATE d
				SET d.RecognitionDetailId = rd.Id
			FROM @Details d
			JOIN Budget.RecognitionDetail rd 
				ON @RecognitionId = rd.RecognitionId
					AND d.CategoryId = rd.CategoryId
					AND d.RevenueTypeId = rd.RevenueTypeId
			WHERE ISNULL(d.Id, 0) = 0

			INSERT INTO Budget.RecognitionModificationDetail 
			(
				RecognitionModificationId, RecognitionDetailId, Nature, Value
			)
			SELECT
				@Id RecognitionModificationId, d.RecognitionDetailId, d.Nature, d.Value
			FROM @Details d
			WHERE ISNULL(d.Id, 0) = 0

			UPDATE cd
				SET cd.Nature = d.Nature,
					cd.Value = d.Value
			FROM Budget.RecognitionModificationDetail cd
			JOIN @Details d ON cd.Id = d.Id
			WHERE cd.RecognitionModificationId = @Id

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				UPDATE rd
					SET
						rd.DebitValueModification = rd.DebitValueModification + IIF(rmd.Nature = 1, rmd.Value, 0),
						rd.CreditValueModification = rd.CreditValueModification + IIF(rmd.Nature = 2, rmd.Value, 0),
						rd.TotalRecognition = rd.TotalRecognition + (rmd.Value * IIF(rmd.Nature = 1, -1, 1)),
						rd.Balance = rd.Balance - (rmd.Value * IIF(rmd.Nature = 1, 1, -1))
				FROM Budget.RecognitionModificationDetail rmd
				JOIN Budget.RecognitionDetail rd ON rmd.RecognitionDetailId = rd.Id
				WHERE rmd.RecognitionModificationId = @Id

				UPDATE b
					SET b.ExecutedValue = b.ExecutedValue - rd.Value,
						b.Balance = b.Balance + rd.Value
				FROM Budget.Budget b
				JOIN 
				(
					SELECT 
						rd.CategoryId, rd.RevenueTypeId,
						SUM(rmd.Value * IIF(rmd.Nature = 1, 1, -1)) Value
					FROM Budget.RecognitionModificationDetail rmd
					JOIN Budget.RecognitionDetail rd ON rmd.RecognitionDetailId = rd.Id
					WHERE rmd.RecognitionModificationId = @Id
					GROUP BY rd.CategoryId, rd.RevenueTypeId
				) rd ON b.CategoryId = rd.CategoryId AND b.RevenueTypeId = rd.RevenueTypeId
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
				   WHEN 2 THEN CONCAT('Se guardó y confirmó La modificación del Reconocimiento con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló La modificación del Reconocimiento con código ', @Code)
				   ELSE CONCAT('Se guardó La modificación del Reconocimiento con código ', @Code)
			   END
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)), 
			   @Id = 0, 
			   @Code = ''
	END CATCH
END