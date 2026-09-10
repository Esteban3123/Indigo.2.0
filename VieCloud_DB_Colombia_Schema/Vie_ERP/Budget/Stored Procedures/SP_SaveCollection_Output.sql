-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-03
-- Description:	Procedimiento el cual se encarga de guardar un Recaudo
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveCollection_Output]
	@CollectionXml AS XML,
	@CollectionDetailForDeleteXml AS XML,
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
			@Observations VARCHAR(MAX),
			@ThirdPartyId INT,
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT = 215,
			@DocumentType INT = 7,
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
		CollectionId INT,
		RecognitionDetailId INT,
		CollectionType TINYINT,
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
			@Observations = t.x.value('Observations[1]','VARCHAR(MAX)'),
			@ThirdPartyId = t.x.value('ThirdPartyId[1]','INT'),
			@Status = t.x.value('Status[1]','TINYINT'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @CollectionXml.nodes('/Collection') t(x)

		IF EXISTS (SELECT 1 FROM Budget.Collection r WHERE r.Id = @Id AND r.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'El Recaudo se encuentra en estado: ' + IIF(r.Status = 2, 'Confirmado', 'Anulado'), 
				   @Id = 0, 
				   @CodeResult = '' 
			FROM Budget.Collection r 
			WHERE r.Id = @Id
			RETURN
		END

		IF @Status = 3
		BEGIN
			UPDATE [Budget].[Collection]
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
			FROM @CollectionDetailForDeleteXml.nodes('/CollectionDetail') t(x)
			JOIN Budget.CollectionDetail rd ON t.x.value('Id[1]','int') = rd.Id
			WHERE rd.CollectionId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT 
					t.x.value('Id[1]','INT') AS Id,
					t.x.value('CollectionId[1]','INT') AS CollectionId,
					t.x.value('RecognitionDetailId[1]','INT') AS RecognitionDetailId,
					t.x.value('CollectionType[1]','INT') AS CollectionType,
					t.x.value('InitialValue[1]','DECIMAL(18, 2)') AS InitialValue
				FROM @CollectionXml.nodes('/Collection/CollectionDetail') t(x)
			
			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					rd.Id, 
					rd.CollectionId, 
					rd.RecognitionDetailId, 
					rd.CollectionType,
					rd.InitialValue
				FROM Budget.CollectionDetail rd
				LEFT JOIN @Details d ON rd.Id = d.Id
				WHERE rd.CollectionId = @Id AND ISNULL(d.Id, 0) = 0

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
				SELECT @Message = 'La Fecha del Recaudo (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.IncomeMonth AS VARCHAR), 2)) + ').'
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
					   @MessageResult = 'El Recaudo no tiene detalles.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.CollectionDetail rd JOIN @Details d ON rd.Id = d.Id WHERE rd.CollectionId <> @Id OR rd.RecognitionDetailId <> d.RecognitionDetailId) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles del Recaudo han sido alterados.', 
					   @Id = 0, 
					   @Code = '' 
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.RecognitionDetailId HAVING COUNT(*) > 1)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El Recaudo tiene detalles duplicados.', 
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
							SUM(d.InitialValue) Value
						FROM @Details d 
						JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON d.RecognitionDetailId = rd.Id
						GROUP BY rd.CategoryId
					) od 
					LEFT JOIN Budget.AnnualizedCashFlow acf ON od.CategoryId = acf.CategoryId AND MONTH(@DocumentDate) = acf.Month
					WHERE od.Value > ISNULL(acf.Balance, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ': Balance (' + FORMAT(ISNULL(acf.Balance, 0), 'C0', 'es-CO') + ') - Recaudo (' + FORMAT(od.Value, 'C0', 'es-CO') + ')'
							FROM 
							(
								SELECT 
									rd.CategoryId,
									SUM(d.InitialValue) Value
								FROM @Details d 
								JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON d.RecognitionDetailId = rd.Id
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
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Recaudos'), 
							@Id = 0, 
							@Code = ''
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[Collection]
				(
					[Code],[BudgetaryValidityId],[DocumentDate],[Observations],[ThirdPartyId],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[EntityId],[EntityCode],[EntityName]
				)
				SELECT @Code,@BudgetaryValidityId,@DocumentDate,@Observations,@ThirdPartyId,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@EntityId,@EntityCode,@EntityName

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[Collection]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[Observations] = @Observations,
						[ThirdPartyId] = @ThirdPartyId,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id
			END

			/*************************************************************************************/

			INSERT INTO Budget.CollectionDetail 
			(
				CollectionId, RecognitionDetailId, CollectionType,
				InitialValue, DebitModificationValue, CreditModificationValue, Balance
			)
			SELECT
				@Id CollectionId, d.RecognitionDetailId, d.CollectionType,
				d.InitialValue, 0, 0, d.InitialValue
			FROM @Details d
			WHERE ISNULL(d.Id, 0) = 0

			UPDATE cd
				SET cd.InitialValue = d.InitialValue,
					cd.DebitModificationValue = 0,
					cd.CreditModificationValue = 0,
					cd.Balance = d.InitialValue
			FROM Budget.CollectionDetail cd
			JOIN @Details d ON cd.Id = d.Id
			WHERE cd.CollectionId = @Id

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				-- Si maneja PAC actualizamos el valor ejecutado
				IF EXISTS (SELECT 1 FROM Budget.BudgetaryValidity bv WHERE bv.Id = @BudgetaryValidityId AND bv.PACControl = 1)
				BEGIN
					UPDATE acf
						SET acf.ExecutedValue = acf.ExecutedValue + od.value,
							acf.Balance = acf.Balance - od.Value
					FROM 
					(
						SELECT 
							rd.CategoryId,
							SUM(d.InitialValue) Value
						FROM @Details d 
						JOIN Budget.RecognitionDetail rd WITH (NOLOCK) ON d.RecognitionDetailId = rd.Id
						GROUP BY rd.CategoryId
					) od
					JOIN Budget.AnnualizedCashFlow acf ON od.CategoryId = acf.CategoryId AND MONTH(@DocumentDate) = acf.Month
				END

				UPDATE rd
					SET rd.ExecutedValue = rd.ExecutedValue + cd.InitialValue,
						rd.Balance = rd.Balance - cd.InitialValue
				FROM Budget.CollectionDetail cd
				JOIN Budget.RecognitionDetail rd ON cd.RecognitionDetailId = rd.Id
				WHERE cd.CollectionId = @Id
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
				   WHEN 2 THEN CONCAT('Se guardó y confirmó el Recaudo con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló el Recaudo con código ', @Code)
				   ELSE CONCAT('Se guardó el Recaudo con código ', @Code)
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda o actualiza un documento de recaudo (cobro) presupuestal, recibiendo los datos de cabecera y detalle en formato XML. Valida que el recaudo esté en estado editable (no confirmado ni anulado), que la fecha del documento coincida con la vigencia presupuestaria activa, que existan detalles sin duplicados ni valores en cero o negativos, y que no se superen los límites del PAC (Programa Anual de Caja) si aplica. Si el estado recibido es anulación (3), actualiza el estado del recaudo en la tabla Budget.Collection registrando usuario y fecha de anulación mediante Common.GETDATE; de lo contrario, gestiona la eliminación de detalles indicados en el XML de eliminación sobre Budget.CollectionDetail y sincroniza los detalles nuevos y existentes. Retorna códigos de resultado, mensajes de error de negocio, el identificador y el código del recaudo procesado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCollection_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCollection_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, confirma o anula un recaudo presupuestal con sus detalles, validando vigencia, duplicados, valores y saldo PAC, y afectando reconocimientos y flujo de caja al confirmar.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @CollectionXml debe contener un nodo /Collection con la cabecera del recaudo y opcionalmente /Collection/CollectionDetail con sus líneas.; El recaudo a modificar debe existir en estado 1 (borrador); si está en estado 2 o 3 se rechaza la operación.; Debe existir una BudgetaryValidity vigente cuyo Year coincida con YEAR(@DocumentDate) y cuyo IncomeMonth sea ≤ MONTH(@DocumentDate).; El XML debe traer al menos un detalle (después de combinar con los preexistentes no eliminados).; Los detalles editados deben conservar su CollectionId y RecognitionDetailId originales.; No deben existir RecognitionDetailId duplicados entre los detalles del recaudo.; Todos los InitialValue de detalles deben ser estrictamente mayores a 0.; Si la vigencia maneja PAC (PACControl=1), debe existir saldo suficiente en AnnualizedCashFlow por CategoryId y mes del documento.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se permite modificar recaudos cuyo Status sea 1 (en borrador); confirmados (2) o anulados (3) son inmutables salvo la propia transición a anulado.; DocumentType para los recaudos en BudgetControl es siempre 7 y el IdForm utilizado para la secuencia es 215.; Al insertar detalles nuevos, DebitModificationValue y CreditModificationValue se inicializan en 0 y Balance = InitialValue.; La fecha del recaudo debe estar dentro de una vigencia presupuestal cuyo Year coincida con el año y cuyo IncomeMonth sea menor o igual al mes del documento.; No se permiten detalles duplicados por RecognitionDetailId ni con InitialValue <= 0.; En recaudos editados, el RecognitionDetailId y el CollectionId de cada detalle no pueden alterarse respecto al registro original.; La afectación al PAC (ExecutedValue +, Balance −) y al RecognitionDetail solo ocurre en confirmación (Status=2).; Cuando hay control PAC, la sumatoria por CategoryId no puede exceder el Balance del mes correspondiente en AnnualizedCashFlow.; BudgetControl mantiene presencia del documento solo mientras el recaudo esté en estado borrador (Status=1).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe el recaudo con Status<>1 (Confirmado=2 o Anulado=3) → Aborta con código 999 indicando estado actual y no realiza cambios else Continúa con el flujo de guardado/anulación; si @Status = 3 (anulación) → Actualiza Collection seteando Status, ModificationUser/Date y AnnulmentUser/Date con el usuario actual; si @Id = 0 (nuevo recaudo) → Solicita secuencia vía Common.SP_GetSequence (tipo 200, IdForm=215) e inserta cabecera en Budget.Collection con SCOPE_IDENTITY() else Actualiza la cabecera existente en Budget.Collection; si BudgetaryValidity.PACControl = 1 → Valida que la suma de InitialValue por CategoryId no supere el Balance del PAC del mes en AnnualizedCashFlow; si supera aborta con detalle por rubro else Omite validación y actualización de PAC; si @Status = 2 (confirmación) → Setea ConfirmationUser/Date; si maneja PAC actualiza ExecutedValue y Balance en AnnualizedCashFlow; actualiza ExecutedValue y Balance en RecognitionDetail por cada detalle; si @Status = 1 (borrador) y no existe registro en BudgetControl para (DocumentType=7, @Code) → Inserta fila de control en Budget.BudgetControl else Elimina la fila correspondiente en Budget.BudgetControl', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Collection; Budget.CollectionDetail; Budget.BudgetaryValidity; Budget.RecognitionDetail; Budget.AnnualizedCashFlow; Budget.Category; Budget.BudgetControl', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection_Output';
-- GO
