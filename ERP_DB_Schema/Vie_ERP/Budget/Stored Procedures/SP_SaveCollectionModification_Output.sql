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