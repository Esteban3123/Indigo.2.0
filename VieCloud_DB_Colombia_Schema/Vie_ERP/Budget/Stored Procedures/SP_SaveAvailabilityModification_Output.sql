
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-11
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una Modificación de Disponibilidad
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveAvailabilityModification_Output]
    @AvailabilityModificationXml AS XML,
	@AvailabilityModificationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@AuxiliaryResult VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @OperatingUnitId INT,
			@BudgetHeaderId INT,
			@BudgetaryValidityId INT,
			@DocumentDate DATETIME,
			@AvailabilityId INT,
			@UpTo TINYINT,
			@Document VARCHAR(100),
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT = 229,
			@DocumentTypeControl INT = 10,
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
		AvailabilityDetailId INT,
		CategoryId INT NOT NULL,
		RevenueTypeId INT NOT NULL,
		Nature TINYINT,
		Value DECIMAL(18,2),
		CPCCodeId INT NULL
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),			
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@AvailabilityId = t.x.value('AvailabilityId[1]','int'),
			@UpTo = t.x.value('UpTo[1]','tinyint'),
			@Document = t.x.value('Document[1]','varchar(100)'),
			@Observations = t.x.value('Observations[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @AvailabilityModificationXml.nodes('/AvailabilityModification') t(x)

		IF EXISTS (SELECT 1 FROM Budget.AvailabilityModification am WHERE am.Id = @Id AND am.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La Modificación de Disponibilidad se encuentra en estado: ' + IIF(am.Status = 2, 'Confirmado', 'Anulado')
			FROM Budget.AvailabilityModification am 
			WHERE am.Id = @Id
			RETURN
		END

		IF @Status = 3
		BEGIN
			UPDATE [Budget].[AvailabilityModification]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			--Obtenemos la cabecera del presupuesto de gastos
			SELECT @BudgetHeaderId = bh.Id
			FROM Budget.BudgetHeader bh
			WHERE bh.BudgetaryValidityId = @BudgetaryValidityId AND bh.Type = 2

			--Eliminamos los detalles indicados
			DELETE amd
			FROM @AvailabilityModificationDetailForDeleteXml.nodes('/AvailabilityModificationDetail') t(x)
			JOIN Budget.AvailabilityModificationDetail amd ON t.x.value('Id[1]','int') = amd.Id
			WHERE amd.AvailabilityModificationId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('AvailabilityDetailId[1]','int'),
					t.x.value('CategoryId[1]','int'),
					t.x.value('RevenueTypeId[1]','int'),
					t.x.value('Nature[1]','int'),
					t.x.value('Value[1]','decimal(18,2)'),
					t.x.value('CPCCodeId[1]','int')
				FROM @AvailabilityModificationXml.nodes('/AvailabilityModification/AvailabilityModificationDetail') t(x)

		--Se obtiene los detalles que vienen en el xml
		UPDATE d
			SET d.AvailabilityDetailId = ad.Id
		FROM @Details d
		JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND d.CategoryId = b.CategoryId AND d.RevenueTypeId = b.RevenueTypeId
		JOIN Budget.AvailabilityDetail ad ON @AvailabilityId = ad.AvailabilityId AND b.Id = ad.BudgetId 
		WHERE ISNULL(d.Id, 0) = 0
		AND ISNULL(d.AvailabilityDetailId, 0) = 0
		and ad.CPCCodeId = d.CPCCodeId 
			
			--Actualizo los datos del detalle si de acuerdo con el detalle de la disponibilidad
			UPDATE d
				SET d.CategoryId = b.CategoryId,
					d.RevenueTypeId = b.RevenueTypeId
			FROM @Details d
			JOIN Budget.AvailabilityDetail ad ON @AvailabilityId = ad.AvailabilityId AND d.AvailabilityDetailId = ad.Id
			JOIN Budget.Budget b ON ad.BudgetId = b.Id
			WHERE d.CategoryId = 0 AND d.RevenueTypeId = 0
			
			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					amd.Id, 
					amd.AvailabilityDetailId, 
					b.CategoryId,
					b.RevenueTypeId,
					amd.Nature, 
					amd.Value,
					ad.CPCCodeId
				FROM Budget.AvailabilityModificationDetail amd
				JOIN Budget.AvailabilityDetail ad ON amd.AvailabilityDetailId = ad.Id
				JOIN Budget.Budget b ON ad.BudgetId = b.Id
				LEFT JOIN @Details d ON amd.Id = d.Id
				WHERE amd.AvailabilityModificationId = @Id AND ISNULL(d.Id, 0) = 0
			
			/*************************************VALIDACIONES************************************/

			--- Valido el Periodo de la Vigencia
			IF NOT EXISTS 
			(
				SELECT 1 
				FROM Budget.BudgetaryValidity bv 
				WHERE bv.Id = @BudgetaryValidityId
					AND bv.Year = YEAR(@DocumentDate)
					AND bv.ExpenseMonth <= MONTH(@DocumentDate)
			)
			BEGIN
				SELECT @Message = 'La Fecha de la Modificación de Disponibilidad (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.ExpenseMonth AS VARCHAR), 2)) + ').'
				FROM Budget.BudgetaryValidity bv
				WHERE bv.Id = @BudgetaryValidityId

				SELECT @CodeResult = 999, 
					   @MessageResult = ISNULL(@Message, 'Periodo Presupuestal no encontrado.')
				RETURN
			END

			--- Valido que existan detalles
			IF NOT EXISTS (SELECT 1 FROM @Details)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La Modificación de Disponibilidad no tiene detalles.'
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.AvailabilityModificationDetail amd JOIN @Details d ON amd.Id = d.Id WHERE amd.AvailabilityModificationId <> @Id OR amd.AvailabilityDetailId <> ISNULL(d.AvailabilityDetailId, 0)) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles de la Modificación de Disponibilidad han sido alterados.'
				RETURN
			END

			--- Valido que las categorias existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.Category c ON d.CategoryId = c.Id AND @BudgetaryValidityId = c.BudgetaryValidityId WHERE c.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen rubros que no existen o no pertenecen a la vigencia de la Modificación de Disponibilidad.'
				RETURN
			END

			--- Valido que los tipos existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.RevenueType rt ON d.RevenueTypeId = rt.Id AND @BudgetaryValidityId = rt.BudgetaryValidityId WHERE rt.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen tipos que no existen o no pertenecen a la vigencia de la Modificación de Disponibilidad.'
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				GROUP BY d.CategoryId, d.RevenueTypeId, d.CPCCodeId 
				HAVING COUNT(*) > 1
			)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La Modificación de Disponibilidad tiene detalles duplicados.'
				RETURN
			END

			--- Valido que no existan detalles con valores en 0 o negativos
			IF EXISTS (SELECT 1 FROM @Details d WHERE d.Value <= 0)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Existen Rubros de la Modificación de Disponibilidad con el valor menor o igual a 0.'
				RETURN
			END

			--- Valido hasta donde se realizará la modificación
			IF @UpTo NOT IN (4)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El nivel de la Modificación de Disponibilidad hasta la que se liberaran recursos no es valido.'
				RETURN
			END

			--- Valido la naturaleza de los detalles
			IF EXISTS (SELECT 1 FROM @Details WHERE Nature NOT IN (1, 2))
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code
						FROM @Details d
						JOIN Budget.AvailabilityDetail ad ON d.AvailabilityDetailId = ad.Id
						JOIN Budget.Budget b ON ad.BudgetId = b.Id
						JOIN Budget.Category c ON b.CategoryId = c.Id
						JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE d.Nature NOT IN (1, 2)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'La naturaleza de los siguientes rubros de la Modificación de Disponibilidad no son válidas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END
			
			--- Valido detalles debitos
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				LEFT JOIN Budget.AvailabilityDetail ad ON d.AvailabilityDetailId = ad.Id
				WHERE d.Nature = 1 AND d.Value > ISNULL(ad.Balance, 0) AND ad.CPCCodeId = d.CPCCodeId
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Balance (' + FORMAT(ISNULL(ad.Balance, 0), 'C0', 'es-CO') + ') - Modificación(' + FORMAT(d.Value, 'C0', 'es-CO') + ')'
						FROM @Details d
						LEFT JOIN Budget.AvailabilityDetail ad ON d.AvailabilityDetailId = ad.Id 
						LEFT JOIN Budget.Budget b ON ad.BudgetId = b.Id
						LEFT JOIN Budget.Category c ON ISNULL(b.CategoryId, d.CategoryId) = c.Id
						LEFT JOIN Budget.RevenueType rt ON ISNULL(b.RevenueTypeId, d.RevenueTypeId) = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE d.Nature = 1 AND d.Value > ISNULL(ad.Balance, 0) 
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'El valor débito de los siguientes rubros de la Modificación de Disponibilidad no pueden ser mayor que el saldo de la disponibilidad: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--- Valido detalles creditos
			IF EXISTS 
			(
				SELECT 1 
				FROM 
				(
					SELECT	d.CategoryId, d.RevenueTypeId,
							SUM(d.Value) Value
					FROM @Details d
					WHERE d.Nature = 2
					GROUP BY d.CategoryId, d.RevenueTypeId
				) ad 
				LEFT JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND ad.CategoryId = b.CategoryId AND ad.RevenueTypeId = b.RevenueTypeId
				WHERE ad.Value > ISNULL(b.Balance, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Balance (' + FORMAT(ISNULL(b.Balance, 0), 'C0', 'es-CO') + ') - Modificación(' + FORMAT(ad.Value, 'C0', 'es-CO') + ')'
						FROM 
						(
							SELECT	d.CategoryId, d.RevenueTypeId,
									SUM(d.Value) Value
							FROM @Details d
							WHERE d.Nature = 2
							GROUP BY d.CategoryId, d.RevenueTypeId
						) ad 
						LEFT JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND ad.CategoryId = b.CategoryId AND ad.RevenueTypeId = b.RevenueTypeId
						LEFT JOIN Budget.Category c ON ISNULL(b.CategoryId, ad.CategoryId) = c.Id
						LEFT JOIN Budget.RevenueType rt ON ISNULL(b.RevenueTypeId, ad.RevenueTypeId) = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE ad.Value > ISNULL(b.Balance, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'El valor crédito de los siguientes rubros de la Modificación de Disponibilidad no pueden ser mayor que el saldo del presupuesto: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN --Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 200, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Modificación de Disponibilidad')
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[AvailabilityModification]
				(
					[Code],[BudgetaryValidityId],[DocumentDate],[AvailabilityId],[UpTo],[Document],[Observations],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[EntityId],[EntityCode],[EntityName]
				)
				SELECT @Code,@BudgetaryValidityId,@DocumentDate,@AvailabilityId,@UpTo,@Document,@Observations,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@EntityId,@EntityCode,@EntityName

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE 
			BEGIN --Si se esta actualizando
				UPDATE [Budget].[AvailabilityModification]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[AvailabilityId] = @AvailabilityId,
						[UpTo] = @UpTo,
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

			INSERT INTO Budget.Budget 
			(
				BudgetHeaderId, CategoryId, RevenueTypeId, 
				InitialValue, DebitValueModification, CreditValueModification, DebitValueTransfer, CreditValueTransfer, TotalBudget, ExecutedValue, SuspendedValue, Balance,
				CreationUser, CreationDate
			)
			SELECT	@BudgetHeaderId, d.CategoryId, d.RevenueTypeId,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					@CodeUser, [Common].[GETDATE]()
			FROM @Details d
			LEFT JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND d.CategoryId = b.CategoryId AND d.RevenueTypeId = b.RevenueTypeId
			WHERE ISNULL(d.Id, 0) = 0 AND b.Id IS NULL

			/*************************************************************************************/

			INSERT INTO Budget.AvailabilityDetail
			(
				AvailabilityId, BudgetId,
				InitialValue, DebitModificationValue, CreditModificationValue, TotalAvailability, ExecutedValue, Balance, CPCCodeId
			)
			SELECT	@AvailabilityId, b.Id,
					0, 0, 0, 0, 0, 0, iif(D.CPCCodeId = 0, null, D.CPCCodeId)
			FROM @Details d
			JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND d.CategoryId = b.CategoryId AND d.RevenueTypeId = b.RevenueTypeId
			LEFT JOIN Budget.AvailabilityDetail ad ON @AvailabilityId = ad.AvailabilityId AND b.Id = ad.BudgetId
			WHERE ISNULL(d.Id, 0) = 0 AND ad.Id IS NULL
			
			UPDATE d
				SET d.AvailabilityDetailId = ad.Id
			FROM @Details d
			JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND d.CategoryId = b.CategoryId AND d.RevenueTypeId = b.RevenueTypeId
			JOIN Budget.AvailabilityDetail ad ON @AvailabilityId = ad.AvailabilityId AND b.Id = ad.BudgetId AND ISNULL(ad.CPCCodeId, 0) = ISNULL(d.CPCCodeId, 0)
			WHERE ISNULL(d.Id, 0) = 0

			/*************************************************************************************/
			UPDATE amd
				SET amd.Nature = d.Nature, 
					amd.Value = d.Value
			FROM Budget.AvailabilityModificationDetail amd
			JOIN @Details d ON amd.Id = d.Id
			

			INSERT INTO Budget.AvailabilityModificationDetail (AvailabilityModificationId, AvailabilityDetailId, Nature, Value)
				SELECT
					@Id AvailabilityModificationId,
					d.AvailabilityDetailId,
					d.Nature,
					d.Value
				FROM @Details d
				JOIN Budget.AvailabilityDetail ad1 ON ad1.Id = d.AvailabilityDetailId
				WHERE ISNULL(d.Id, 0) = 0 
				

			/*************************************************************************************/
			
			IF @Status=2 
			BEGIN
			UPDATE ad
				SET
					ad.DebitModificationValue = ad.DebitModificationValue + IIF(d.Nature = 1, d.Value, 0),
					ad.CreditModificationValue = ad.CreditModificationValue + IIF(d.Nature = 2, d.Value, 0),
					ad.TotalAvailability = ad.TotalAvailability - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0),
					ad.Balance = ad.Balance - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0)
			FROM Budget.AvailabilityModificationDetail d
			JOIN Budget.AvailabilityDetail ad ON d.AvailabilityDetailId = ad.Id
			WHERE d.AvailabilityModificationId = @Id

			-- Actualiza el CPCCodeId del detalle original con el nuevo CPC recibido desde el XML
			UPDATE ad
				SET ad.CPCCodeId = IIF(d.CPCCodeId = 0, NULL, d.CPCCodeId)
			FROM @Details d
			JOIN Budget.AvailabilityDetail ad ON d.AvailabilityDetailId = ad.Id
			WHERE ISNULL(d.AvailabilityDetailId, 0) <> 0

				-- TABLA TEMPORAL PARA GUARDAR EL VALOR TOTAL DEL BALANCE A ACTUALIZAR
				DECLARE @BalanceTable TABLE (
					Id INT NOT NULL,
					Value DECIMAL(18,2),  
					Nature TINYINT
				)
				-- SE INSERTA EL BALANCE TOTAL POR NATURALEZA
				INSERT INTO @BalanceTable (Id, Value, Nature)
				SELECT
					b.Id,
					SUM(d.Value),
					d.Nature
				FROM Budget.AvailabilityModificationDetail d
				JOIN Budget.AvailabilityDetail ad ON d.AvailabilityDetailId = ad.Id
				JOIN Budget.Budget b ON ad.BudgetId = b.Id
				WHERE d.AvailabilityModificationId = @Id
				GROUP BY b.Id, d.Nature
							   				
				UPDATE b
					SET
						b.ExecutedValue = b.ExecutedValue - IIF(bt.Nature = 1, bt.Value, 0) + IIF(bt.Nature = 2, bt.Value, 0),
						b.Balance = b.Balance + IIF(bt.Nature = 1, bt.Value, 0) - IIF(bt.Nature = 2, bt.Value, 0),
						b.ModificationUser = @CodeUser,
						b.ModificationDate = [Common].[GETDATE]()
				FROM Budget.AvailabilityModificationDetail d
				JOIN Budget.AvailabilityDetail ad ON d.AvailabilityDetailId = ad.Id
				JOIN Budget.Budget b ON ad.BudgetId = b.Id
				JOIN @BalanceTable bt ON bt.Id = b.Id
				WHERE d.AvailabilityModificationId = @Id
			END
		END

		IF @Status = 1
		
		BEGIN
		
		IF NOT EXISTS (SELECT 1 FROM Budget.BudgetControl WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Budget.BudgetControl (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentTypeControl, @CodeUser, @DocumentDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Budget.BudgetControl WHERE DocumentType = @DocumentTypeControl AND DocumentNumber = @Code
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Modificación de Disponibilidad con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Modificación de Disponibilidad con código ', @Code)
				   ELSE CONCAT('Se guardó la Modificación de Disponibilidad con código ', @Code)
			   END
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar, confirmar o anular una Modificación de Disponibilidad Presupuestal (ajuste sobre rubros de disponibilidad de presupuesto de gastos). Recibe los datos de cabecera y detalle en formato XML, valida que la modificación esté en estado borrador (estado 1) antes de permitir cambios, y según el estado solicitado puede anular el registro o bien guardar/actualizar los detalles de líneas presupuestales (rubros, naturaleza, valor, código CPC), eliminando los que se indiquen. También verifica que la fecha del documento coincida con la vigencia presupuestal activa, cruzando información de las tablas AvailabilityModification, AvailabilityModificationDetail, BudgetHeader y Budget. Retorna códigos y mensajes de resultado, así como el identificador y código del documento procesado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAvailabilityModification_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAvailabilityModification_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, actualiza, confirma o anula una Modificación de Disponibilidad presupuestal validando vigencia, rubros y saldos, y al confirmar ajusta saldos de presupuesto y disponibilidad.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de cabecera debe contener un único nodo /AvailabilityModification con los campos requeridos (Id, OperatingUnitId, BudgetaryValidityId, DocumentDate, AvailabilityId, UpTo, Status, etc.).; Debe existir una BudgetaryValidity con el Id indicado y un BudgetHeader de Type=2 asociado a esa vigencia.; Si @Id > 0, el registro en Budget.AvailabilityModification debe estar en Status=1 (no confirmado ni anulado) para poder modificarse.; Las categorías y tipos de ingreso enviados deben existir y pertenecer a la BudgetaryValidityId.; Para alta (@Id=0) Common.SP_GetSequence debe poder entregar un código (sequenceType=200, IdForm=229).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una Modificación de Disponibilidad solo puede modificarse mientras su Status = 1 (no Confirmado ni Anulado).; El nivel de liberación (UpTo) solo admite el valor 4.; Cada detalle debe tener Nature ∈ {1=débito, 2=crédito} y Value > 0.; No se permiten detalles duplicados por (CategoryId, RevenueTypeId, CPCCodeId).; La fecha del documento debe pertenecer a la vigencia: Year(@DocumentDate)=bv.Year y bv.ExpenseMonth <= Month(@DocumentDate).; Las categorías y tipos de ingreso del detalle deben pertenecer a la BudgetaryValidity de la modificación.; El BudgetHeader usado siempre corresponde al de tipo 2 (presupuesto de gastos) de la vigencia.; El valor débito por línea no puede exceder el Balance de la AvailabilityDetail; el crédito agregado por (Categoría, Tipo) no puede exceder el Balance del Budget.; AvailabilityModificationId y AvailabilityDetailId originales de un detalle existente no pueden cambiar (anti-tampering).; Solo al confirmar (Status=2) se afectan saldos en Budget y AvailabilityDetail; en otros estados se preservan.; Al confirmar: en AvailabilityDetail el débito reduce TotalAvailability/Balance y en Budget el débito aumenta Balance y reduce ExecutedValue (los créditos al revés).; El control presupuestario (BudgetControl) usa DocumentType=10 para este documento.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de Disponibilidad presupuestal; Vigencia presupuestal; Rubro / Categoría presupuestal; Tipo de ingreso (RevenueType); Fuente de financiación (FinancialSource); Código CPC; Naturaleza débito/crédito; Saldo de disponibilidad; Confirmación y anulación de documento; Control presupuestario (BudgetControl); Secuencia numérica de documento', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe el documento (am.Id=@Id) y su Status <> 1 (no está en borrador/registrado) → Aborta con CodeResult=999 indicando si está Confirmado (2) o Anulado (3) else Continúa el flujo según el @Status recibido; si @Status = 3 (anulación) → Solo actualiza la cabecera marcando AnnulmentUser/AnnulmentDate y ModificationUser/Date; NO toca detalles ni saldos else Procesa detalles, validaciones, inserta/actualiza cabecera y, si confirma, recalcula saldos; si @Id = 0 (alta nueva) → Solicita secuencia con Common.SP_GetSequence (tipo 200, IdForm=229) y hace INSERT en Budget.AvailabilityModification else Hace UPDATE de la cabecera existente; si @Status = 2 (confirmación) → Recalcula AvailabilityDetail (DebitModificationValue/CreditModificationValue/TotalAvailability/Balance) y Budget (ExecutedValue/Balance) sumando débitos (Nature=1) y créditos (Nature=2); además setea ConfirmationUser/Date else No afecta saldos del presupuesto ni de la disponibilidad; si @Status = 1 (guardado en borrador) y no existe registro en BudgetControl con DocumentType=10 y DocumentNumber=@Code → Inserta marca de control en Budget.BudgetControl else Cuando @Status<>1 elimina la marca de control existente para ese documento; si Existen detalles d con d.Nature=1 y d.Value > ad.Balance (mismo CPCCodeId) → Aborta: el débito no puede superar el saldo de la disponibilidad; si Suma de créditos por (CategoryId,RevenueTypeId) > Balance del Budget correspondiente → Aborta: el crédito no puede superar el saldo del presupuesto', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.AvailabilityModification; Budget.AvailabilityModificationDetail; Budget.AvailabilityDetail; Budget.Budget; Budget.BudgetHeader; Budget.BudgetaryValidity; Budget.Category; Budget.RevenueType; Budget.FinancialSource; Budget.BudgetControl', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification_Output';
-- GO
