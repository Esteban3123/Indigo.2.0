-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-03
-- Description:	Procedimiento el cual se encarga de guardar un reconocimiento
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveRecognition_Output]
	@RecognitionXml AS XML,
	@RecognitionDetailForDeleteXml AS XML,
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
			@Document VARCHAR(100),
			@Observations VARCHAR(MAX),
			@RecognitonType TINYINT,
			@ThirdPartyId INT,
			@DependencyId INT,
			@AutomaticCollection BIT, 
			@Applicant VARCHAR(100), 
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT = 213,
			@DocumentType INT = 5,
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
		RecognitionId INT,
		CategoryId INT,
		RevenueTypeId INT,
		InitialValue DECIMAL(18, 2)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','INT'),			
			@Code = t.x.value('Code[1]','VARCHAR(20)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
			@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','INT'),			
			@DocumentDate = t.x.value('DocumentDate[1]','DATETIME'),
			@Document = t.x.value('Document[1]','VARCHAR(100)'),
			@Observations = t.x.value('Observations[1]','VARCHAR(MAX)'),
			@RecognitonType = t.x.value('RecognitonType[1]','TINYINT'),
			@ThirdPartyId = t.x.value('ThirdPartyId[1]','INT'),
			@DependencyId = t.x.value('DependencyId[1]','INT'),
			@AutomaticCollection = t.x.value('AutomaticCollection[1]','BIT'),
			@Applicant = t.x.value('Applicant[1]','VARCHAR(100)'),
			@Status = t.x.value('Status[1]','TINYINT'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @RecognitionXml.nodes('/Recognition') t(x)

		IF EXISTS (SELECT 1 FROM Budget.Recognition r WHERE r.Id = @Id AND r.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'El Reconocimiento se encuentra en estado: ' + IIF(r.Status = 2, 'Confirmado', 'Anulado'), 
				   @Id = 0, 
				   @CodeResult = '' 
			FROM Budget.Recognition r 
			WHERE r.Id = @Id
			RETURN
		END

		IF @Status = 3
		BEGIN
			UPDATE [Budget].[Recognition]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id

			UPDATE [Budget].[RecognitionDetail] 
				SET DebitValueModification = InitialValue,
					TotalRecognition = 0,
					Balance = 0
			WHERE RecognitionId = @Id
		END
		BEGIN
			IF @Applicant IS NULL
			BEGIN
				SELECT @Applicant = CONCAT(tp.Nit, ' - ', tp.Name)
				FROM Budget.Dependency d 
				JOIN Common.ThirdParty tp ON d.ResponsibleId = tp.Id
				WHERE d.Id = @DependencyId
			END

			--Eliminamos los detalles indicados
			DELETE rd
			FROM @RecognitionDetailForDeleteXml.nodes('/RecognitionDetail') t(x)
			JOIN Budget.RecognitionDetail rd ON t.x.value('Id[1]','int') = rd.Id
			WHERE rd.RecognitionId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT 
					t.x.value('Id[1]','INT') AS Id,
					t.x.value('RecognitionId[1]','INT') AS RecognitionId,
					t.x.value('CategoryId[1]','INT') AS CategoryId,
					t.x.value('RevenueTypeId[1]','INT') AS RevenueTypeId,
					t.x.value('InitialValue[1]','DECIMAL(18, 2)') AS InitialValue
				FROM @RecognitionXml.nodes('/Recognition/RecognitionDetail') t(x)
			
			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					rd.Id, 
					rd.RecognitionId, 
					rd.CategoryId, 
					rd.RevenueTypeId,
					rd.InitialValue
				FROM Budget.RecognitionDetail rd
				LEFT JOIN @Details d ON rd.Id = d.Id
				WHERE rd.RecognitionId = @Id AND ISNULL(d.Id, 0) = 0

			/*************************************VALIDACIONES************************************/

			--- Valido el Periodo de la Vigencia
			IF NOT EXISTS 
			(
				SELECT 1 
				FROM Budget.BudgetaryValidity bv 
				WHERE bv.Id = @BudgetaryValidityId AND
				(
					(ISNULL(@RecognitonType, 0) <> 3 AND bv.Year = YEAR(@DocumentDate) AND bv.IncomeMonth <= MONTH(@DocumentDate))
					OR
					(ISNULL(@RecognitonType, 0) = 3 AND bv.Year > YEAR(@DocumentDate))
				)					
			)
			BEGIN
				SELECT @Message = 'La Fecha del Reconocimiento (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.IncomeMonth AS VARCHAR), 2)) + ').'
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
					   @MessageResult = 'El Reconocimiento no tiene detalles.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.RecognitionDetail rd JOIN @Details d ON rd.Id = d.Id WHERE rd.RecognitionId <> @Id OR rd.CategoryId <> d.CategoryId OR rd.RevenueTypeId <> d.RevenueTypeId) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles del Reconocimiento han sido alterados.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			--- Valido que las categorias existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.Category c ON d.CategoryId = c.Id AND @BudgetaryValidityId = c.BudgetaryValidityId WHERE c.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen rubros que no existen o no pertenecen a la vigencia del Reconocimiento.'
				RETURN
			END

			--- Valido que los tipos existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.RevenueType rt ON d.RevenueTypeId = rt.Id AND @BudgetaryValidityId = rt.BudgetaryValidityId WHERE rt.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen tipos que no existen o no pertenecen a la vigencia del Reconocimiento.'
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.CategoryId, d.RevenueTypeId HAVING COUNT(*) > 1)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El Reconocimiento tiene detalles duplicados.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			--- Valido que no existan detalles con valores en 0 o negativos
			IF EXISTS (SELECT 1 FROM @Details d WHERE d.InitialValue <= 0)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Existen Rubros con el valor Inicial menor o igual a 0.', 
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
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Reconocimientos'), 
							@Id = 0, 
							@Code = ''
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[Recognition]
				(
					[Code],[BudgetaryValidityId],[DocumentDate],[Document],[Observations],
					[RecognitonType],[ThirdPartyId],[DependencyId],[AutomaticCollection],[Applicant],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[EntityId],[EntityCode],[EntityName]
				)
				SELECT @Code,@BudgetaryValidityId,@DocumentDate,@Document,@Observations,
					@RecognitonType,@ThirdPartyId,@DependencyId,@AutomaticCollection,@Applicant,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@EntityId,@EntityCode,@EntityName

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[Recognition]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[Document] = @Document,
						[Observations] = @Observations,
						[RecognitonType] = @RecognitonType,
						[ThirdPartyId] = @ThirdPartyId,
						[DependencyId] = @DependencyId,
						[AutomaticCollection] = @AutomaticCollection,
						[Applicant] = @Applicant,
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
				RecognitionId, CategoryId, RevenueTypeId,
				InitialValue, DebitValueModification, CreditValueModification, TotalRecognition, ExecutedValue, Balance
			)
			SELECT
				@Id RecognitionId, d.CategoryId, d.RevenueTypeId,
				d.InitialValue, 0, 0, d.InitialValue, 0, d.InitialValue
			FROM @Details d
			WHERE ISNULL(d.Id, 0) = 0

			UPDATE rd
				SET rd.InitialValue = d.InitialValue,
					rd.DebitValueModification = 0,
					rd.CreditValueModification = 0,
					rd.TotalRecognition = d.InitialValue,
					rd.ExecutedValue = 0,
					rd.Balance = d.InitialValue
			FROM Budget.RecognitionDetail rd
			JOIN @Details d ON rd.Id = d.Id
			WHERE rd.RecognitionId = @Id

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				INSERT INTO Budget.Budget 
				(
					BudgetHeaderId, CategoryId, RevenueTypeId, 
					InitialValue, DebitValueModification, CreditValueModification, DebitValueTransfer, CreditValueTransfer, TotalBudget, ExecutedValue, SuspendedValue, Balance,
					CreationUser, CreationDate
				)
				SELECT
					bh.Id, rd.CategoryId, rd.RevenueTypeId,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					@CodeUser, [Common].[GETDATE]()
				FROM Budget.RecognitionDetail rd
				JOIN Budget.BudgetHeader bh ON bh.BudgetaryValidityId = @BudgetaryValidityId AND bh.Type = 1
				LEFT JOIN Budget.Budget b ON rd.CategoryId = b.CategoryId AND rd.RevenueTypeId = b.RevenueTypeId
				WHERE rd.RecognitionId = @Id AND b.Id IS NULL

				UPDATE b
					SET b.ExecutedValue = b.ExecutedValue + rd.InitialValue,
						b.Balance = b.Balance - rd.InitialValue,
						b.ModificationUser = @CodeUser,
						b.ModificationDate = [Common].[GETDATE]()
				FROM Budget.RecognitionDetail rd
				JOIN Budget.Budget b ON rd.CategoryId = b.CategoryId AND rd.RevenueTypeId = b.RevenueTypeId
				WHERE rd.RecognitionId = @Id

				IF @AutomaticCollection = 1
				BEGIN
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT 
								0 Id,
								@OperatingUnitId OperatingUnitId,
								'' Code,
								Collection.BudgetaryValidityId,
								Collection.DocumentDate,							
								CONCAT('Recaudo automatico: ', Collection.Observations) Observations,
								Collection.ThirdPartyId,
								Collection.Status,
								@Id EntityId,
								@Code EntityCode,
								'Recognition' EntityName,
								CollectionDetail.*
							FROM Budget.Recognition Collection
							JOIN
							( 
								SELECT
									rd.RecognitionId,
									rd.Id RecognitionDetailId,
									1 CollectionType,
									rd.InitialValue,
									rd.DebitValueModification,
									rd.CreditValueModification,
									rd.Balance
								FROM Budget.RecognitionDetail rd
								WHERE rd.RecognitionId = @Id
							) CollectionDetail ON Collection.Id = CollectionDetail.RecognitionId
							WHERE Collection.Id = @Id
							For XML AUTO,TYPE, ELEMENTS
						)
					)

					EXEC [Budget].[SP_SaveCollection_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

					IF @Code_Output <> 0
					BEGIN
						SELECT @CodeResult = 999, 
								@MessageResult = @Message_Output, 
								@Id = 0, 
								@Code = ''
						RETURN
					END

					SET @Message = ISNULL(@Message, '') + CHAR(13) + CHAR(10) + @Message_Output
				END
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

		/**************************************** RESULTADO ****************************************/

		SELECT @CodeResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó el Reconocimiento con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló el Reconocimiento con código ', @Code)
				   ELSE CONCAT('Se guardó el Reconocimiento con código ', @Code)
			   END + ISNULL(@Message, '')
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)), 
			   @Id = 0, 
			   @Code = ''
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda, actualiza o anula un reconocimiento presupuestario de ingresos, recibiendo los datos del reconocimiento y sus detalles en formato XML. Gestiona el ciclo de vida del reconocimiento: valida que esté en estado borrador antes de modificarlo, permite anularlo (estado 3) ajustando los valores de débito y saldo en los detalles, y realiza inserciones o actualizaciones del encabezado y líneas de detalle en las tablas Budget.Recognition y Budget.RecognitionDetail. Verifica que la fecha del documento corresponda a la vigencia presupuestal activa, que existan detalles válidos y que los valores base de los detalles no hayan sido alterados; si alguna validación falla, retorna un código de error 999 con el mensaje descriptivo. También resuelve automáticamente el solicitante (Applicant) a partir del responsable de la dependencia y el NIT del tercero registrado en Common.ThirdParty cuando dicho campo no viene informado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveRecognition_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveRecognition_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea, actualiza o anula) un reconocimiento presupuestal con sus detalles, valida vigencia/rubros/valores, afecta el presupuesto cuando se confirma y dispara recaudo automático si corresponde.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognition_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El reconocimiento existente debe estar en estado 1 (borrador); si está Confirmado (2) o Anulado, el procedimiento aborta.; La vigencia presupuestal indicada debe existir y, si el tipo de reconocimiento no es 3, su Year debe coincidir con el año del DocumentDate y su IncomeMonth ser ≤ al mes; si es tipo 3, bv.Year debe ser mayor al año del DocumentDate.; Debe existir al menos un detalle (en XML o previamente persistido).; Cada CategoryId y RevenueTypeId del detalle debe existir y pertenecer a la BudgetaryValidityId del reconocimiento.; No deben existir combinaciones (CategoryId, RevenueTypeId) duplicadas en los detalles.; Todo InitialValue de detalle debe ser > 0.; Los detalles previamente persistidos no deben haber cambiado de RecognitionId, CategoryId ni RevenueTypeId.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognition_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Budget.Recognition: Si @Status = 3 (anulación): actualiza Status, ModificationUser/Date y AnnulmentUser/Date con el usuario y la fecha actual.; [UPDATE] Budget.RecognitionDetail: Si @Status = 3: para todos los detalles del reconocimiento, fija DebitValueModification = InitialValue y deja TotalRecognition y Balance en 0 (deshace el reconocimiento).; [INSERT] Budget.Recognition: Si @Id = 0 inserta una nueva cabecera tras obtener el código vía Common.SP_GetSequence (formulario 213); registra CreationUser/Date y, si @Status = 2, también ConfirmationUser/Date.; [UPDATE] Budget.Recognition: Si @Id <> 0 actualiza la cabecera existente con los datos del XML, ModificationUser/Date y ConfirmationUser/Date solo si @Status = 2.; [INSERT] Budget.RecognitionDetail: Inserta los detalles nuevos (Id = 0 en el XML) con DebitValueModification=0, CreditValueModification=0, TotalRecognition=InitialValue, ExecutedValue=0 y Balance=InitialValue.; [UPDATE] Budget.RecognitionDetail: Para detalles existentes recibidos en el XML, reinicia los valores: InitialValue=d.InitialValue, débitos/créditos=0, TotalRecognition=Balance=InitialValue, ExecutedValue=0.; [DELETE] Budget.RecognitionDetail: Elimina los detalles cuyos Id vengan en @RecognitionDetailForDeleteXml y pertenezcan al reconocimiento.; [INSERT] Budget.Budget: Al confirmar (@Status = 2): crea líneas en Budget.Budget en ceros para las combinaciones (CategoryId, RevenueTypeId) del reconocimiento que aún no existen en el BudgetHeader activo (Type=1) de la vigencia.; [UPDATE] Budget.Budget: Al confirmar (@Status = 2): suma rd.InitialValue a ExecutedValue y lo resta de Balance en las líneas presupuestales coincidentes por CategoryId y RevenueTypeId.; [INSERT] Budget.BudgetControl: Si @Status = 1 y no existe registro previo, registra el documento (DocumentType=5) en la tabla de control presupuestario.; [DELETE] Budget.BudgetControl: Si @Status <> 1 (confirmación o anulación), elimina el registro de control presupuestario para DocumentType=5 y el código del reconocimiento.; [RAISERROR] Budget.Recognition: Devuelve @CodeResult=999 con mensajes específicos cuando: el reconocimiento no está en estado 1, la fecha no coincide con la vigencia, no hay detalles, los detalles fueron alterados, hay rubros/tipos inexistentes, hay duplicados, hay valores ≤ 0, falla la secuencia, o falla SP_SaveCollection_Output; también captura cualquier excepción en CATCH retornando ERROR_MESSAGE y la línea.; [RETURN_RESULT] Budget.Recognition: Al finalizar exitosamente devuelve @CodeResult=0 con mensaje ''Se guardó/confirmó/anuló el Reconocimiento con código …'' según @Status (1, 2 o 3).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognition_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRecognition_Output';
-- GO
