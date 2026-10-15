-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-10
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una modificación de presupuesto
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveBudgetModification_Output]
    @BudgetModificationXml AS XML,
	@BudgetModificationDetailForDeleteXml AS XML,
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
			@DocumentType TINYINT,
			@DocumentSource TINYINT,
			@Document VARCHAR(100),
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT,
			@DocumentTypeControl INT,
			------------------------------
			@Message VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		Id INT,
		BudgetId INT,
		CategoryId INT NOT NULL,
		RevenueTypeId INT NOT NULL,
		Nature TINYINT,
		Value DECIMAL(18,2)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),			
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@DocumentSource = t.x.value('DocumentSource[1]','tinyint'),
			@Document = t.x.value('Document[1]','varchar(100)'),
			@Observations = t.x.value('Observations[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @BudgetModificationXml.nodes('/BudgetModification') t(x)

		IF EXISTS (SELECT 1 FROM Budget.BudgetModification bm WHERE bm.Id = @Id AND bm.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La Modificación de Presupuesto se encuentra en estado: ' + IIF(bm.Status = 2, 'Confirmado', 'Anulado')
			FROM Budget.BudgetModification bm 
			WHERE bm.Id = @Id
			RETURN
		END

		IF @Status = 3
		BEGIN
			UPDATE [Budget].[BudgetModification]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			SELECT @BudgetHeaderId = bh.Id
			FROM Budget.BudgetHeader bh
			WHERE bh.BudgetaryValidityId = @BudgetaryValidityId AND bh.Type = @DocumentSource

			--Eliminamos los detalles indicados
			DELETE bmd
			FROM @BudgetModificationDetailForDeleteXml.nodes('/BudgetModificationDetail') t(x)
			JOIN Budget.BudgetModificationDetail bmd ON t.x.value('Id[1]','int') = bmd.Id
			WHERE bmd.ModificationId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('BudgetId[1]','int'),
					t.x.value('CategoryId[1]','int'),
					t.x.value('RevenueTypeId[1]','int'),
					t.x.value('Nature[1]','int'),
					t.x.value('Value[1]','decimal(18,2)')
				FROM @BudgetModificationXml.nodes('/BudgetModification/BudgetModificationDetail') t(x)

			--Si algun detalle viene sin la relación se busca y se asocia
			UPDATE d
				SET d.BudgetId = b.Id
			FROM @Details d
			JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND d.CategoryId = b.CategoryId AND d.RevenueTypeId = b.RevenueTypeId
			WHERE ISNULL(d.Id, 0) = 0
			
			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					bmd.Id, 
					bmd.BudgetId, 
					b.CategoryId,
					bmd.RevenueTypeId, 
					bmd.Nature, 
					bmd.Value
				FROM Budget.BudgetModificationDetail bmd
				JOIN Budget.Budget b ON bmd.BudgetId = b.Id
				LEFT JOIN @Details d ON bmd.Id = d.Id
				WHERE bmd.ModificationId = @Id AND ISNULL(d.Id, 0) = 0

			/*************************************VALIDACIONES************************************/

			--- Valido el Periodo de la Vigencia
			IF NOT EXISTS 
			(
				SELECT 1 
				FROM Budget.BudgetaryValidity bv 
				WHERE bv.Id = @BudgetaryValidityId
					AND bv.Year = YEAR(@DocumentDate)
					AND
					(
						(@DocumentSource = 1 AND bv.IncomeMonth = MONTH(@DocumentDate))
						OR
						(@DocumentSource = 2 AND bv.ExpenseMonth = MONTH(@DocumentDate))
					)
			)
			BEGIN
				SELECT @Message = 'La Fecha de la Modificación de Presupuesto (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(IIF(@DocumentSource = 1, bv.IncomeMonth, bv.ExpenseMonth) AS VARCHAR), 2)) + ').'
				FROM Budget.BudgetaryValidity bv
				WHERE bv.Id = @BudgetaryValidityId

				SELECT @CodeResult = 999, 
					   @MessageResult = ISNULL(@Message, 'Periodo Presupuestal no encontrado.')
				RETURN
			END

			--- Valido el origen del documento sea valido 
			IF @DocumentSource NOT IN (1, 2)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El origen de la Modificación de Presupuesto no es valido.'
				RETURN
			END

			--- Valido que existan detalles
			IF NOT EXISTS (SELECT 1 FROM @Details)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La Modificación de Presupuesto no tiene detalles.'
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.BudgetModificationDetail bmd JOIN @Details d ON bmd.Id = d.Id WHERE bmd.ModificationId <> @Id OR bmd.BudgetId <> d.BudgetId) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles de la Modificación de Presupuesto han sido alterados.'
				RETURN
			END

			--- Valido que las categorias existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.Category c ON d.CategoryId = c.Id AND @BudgetaryValidityId = c.BudgetaryValidityId WHERE c.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen rubros no existen o no pertenecen a la vigencia de la Modificación de Presupuesto.'
				RETURN
			END

			--- Valido que los tipos existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.RevenueType rt ON d.RevenueTypeId = rt.Id AND @BudgetaryValidityId = rt.BudgetaryValidityId WHERE rt.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen tipos que no existen o no pertenecen a la vigencia de la Modificación de Presupuesto.'
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				GROUP BY d.CategoryId, d.RevenueTypeId 
				HAVING COUNT(*) > 1
			)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La Modificación de Presupuesto tiene detalles duplicados.'
				RETURN
			END

			--- Valido que no existan detalles con valores en 0 o negativos
			IF EXISTS (SELECT 1 FROM @Details d WHERE ((@DocumentSource = 1 AND d.Value < 0) OR (@DocumentSource <> 1 AND d.Value <= 0)))
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = CONCAT('Existen Rubros de la modificación de presupuesto con el valor ', IIF(@DocumentSource = 1, 'menor  a 0.', 'menor o igual a 0.'))
				RETURN
			END

			--- Valido la naturaleza de los detalles
			IF EXISTS (SELECT 1 FROM @Details WHERE Nature NOT IN (1, 2))
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code
						FROM @Details d
						JOIN Budget.Budget b ON d.BudgetId = b.Id
						JOIN Budget.Category c ON b.CategoryId = c.Id						
						JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE Nature NOT IN (1, 2)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'La naturaleza de los siguientes rubros de la Modificación de Presupuesto no son válidas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--- Valido detalles debitos
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				LEFT JOIN Budget.Budget b ON d.BudgetId = b.Id 
				WHERE d.Nature = 1 AND d.Value > ISNULL(b.Balance, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code
						FROM @Details d
						LEFT JOIN Budget.Budget b ON d.BudgetId = b.Id
						LEFT JOIN Budget.Category c ON ISNULL(b.CategoryId, d.CategoryId) = c.Id
						LEFT JOIN Budget.RevenueType rt ON ISNULL(b.RevenueTypeId, d.RevenueTypeId) = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE d.Nature = 1 AND d.Value > ISNULL(b.Balance, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'El valor débito de los siguientes rubros de la Modificación de Presupuesto no pueden ser mayor que el saldo del presupuesto: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			SELECT @DocumentTypeControl = IIF(@DocumentSource = 1, 1, 31),
				   @IdForm = IIF(@DocumentSource = 1, 208, 221)

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT,
						@Code_Output INT, 
						@Message_Output VARCHAR(MAX)
				
				EXEC Common.SP_GetSequence 200, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Modificación de Presupuesto')
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[BudgetModification]
				(
					[Code],[DocumentSource],[DocumentDate],[DocumentType],[BudgetaryValidityId],[Document],[Observations],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[EntityId],[EntityCode],[EntityName]
				)
				SELECT @Code,@DocumentSource,@DocumentDate,@DocumentType,@BudgetaryValidityId,@Document,@Observations,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@EntityId,@EntityCode,@EntityName

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[BudgetModification]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,						
						[DocumentDate] = @DocumentDate,
						[DocumentSource] = @DocumentSource,
						[DocumentType] = @DocumentType,						
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

			UPDATE d
				SET d.BudgetId = b.Id
			FROM @Details d
			JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND d.CategoryId = b.CategoryId AND d.RevenueTypeId = b.RevenueTypeId
			WHERE ISNULL(d.Id, 0) = 0

			/*************************************************************************************/

			UPDATE bmd
				SET bmd.Nature = d.Nature, 
					bmd.Value = d.Value
			FROM Budget.BudgetModificationDetail bmd
			JOIN @Details d ON bmd.Id = d.Id

			INSERT INTO Budget.BudgetModificationDetail (ModificationId, BudgetId, RevenueTypeId, Nature, Value)
				SELECT
					@Id ModificationId,
					d.BudgetId,
					d.RevenueTypeId,
					d.Nature,
					d.Value
				FROM @Details d
				WHERE ISNULL(d.Id, 0) = 0

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				UPDATE b
					SET
						b.DebitValueModification = b.DebitValueModification + IIF(d.Nature = 1, d.Value, 0),
						b.CreditValueModification = b.CreditValueModification + IIF(d.Nature = 2, d.Value, 0),
						b.TotalBudget = b.TotalBudget - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0),
						b.Balance = b.Balance - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0),
						b.ModificationUser = @CodeUser,
						b.ModificationDate = [Common].[GETDATE]()
				FROM Budget.BudgetModificationDetail d
				JOIN Budget.Budget b ON d.BudgetId = b.Id
				WHERE d.ModificationId = @Id

				SELECT @AuxiliaryResult = 'El Presupuesto de Ingresos (' + FORMAT(bi.TotalBudget, 'C0') + ') y el Presupuesto de Gastos (' + FORMAT(be.TotalBudget, 'C0') + ') no son iguales.'
				FROM 
				(
					SELECT bh.BudgetaryValidityId, SUM(b.TotalBudget) TotalBudget
					FROM Budget.BudgetHeader bh
					JOIN Budget.Budget b ON bh.Id = b.BudgetHeaderId
					WHERE bh.BudgetaryValidityId = @BudgetaryValidityId AND bh.Type = 1
					GROUP BY bh.BudgetaryValidityId
				) bi
				FULL JOIN
				(
					SELECT bh.BudgetaryValidityId, SUM(b.TotalBudget) TotalBudget
					FROM Budget.BudgetHeader bh
					JOIN Budget.Budget b ON bh.Id = b.BudgetHeaderId
					WHERE bh.BudgetaryValidityId = @BudgetaryValidityId AND bh.Type = 2
					GROUP BY bh.BudgetaryValidityId
				) be ON bi.BudgetaryValidityId = be.BudgetaryValidityId
				WHERE ISNULL(bi.TotalBudget, 0) <> ISNULL(be.TotalBudget, 0)
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
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Modificación de Presupuesto con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Modificación de Presupuesto con código ', @Code)
				   ELSE CONCAT('Se guardó la Modificación de Presupuesto con código ', @Code)
			   END
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END