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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el ciclo de vida completo de una modificación presupuestaria: permite crear, actualizar, confirmar o anular modificaciones al presupuesto institucional. Opera sobre el encabezado de modificación (BudgetModification) y sus líneas de detalle (BudgetModificationDetail), validando que la fecha del documento coincida con la vigencia presupuestal activa, que el origen del documento sea válido (ingreso o egreso), y que los detalles no hayan sido alterados de forma inconsistente. Utiliza el encabezado de presupuesto (BudgetHeader) para asociar cada modificación a la vigencia y tipo presupuestal correspondiente, y registra el usuario y la fecha de modificación o anulación usando la función de fecha del sistema. Recibe los datos en formato XML y devuelve códigos y mensajes de resultado para informar al sistema frontend sobre el éxito o el motivo del rechazo de la operación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBudgetModification_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBudgetModification_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste una modificación presupuestal (creación, edición, confirmación o anulación) validando vigencia, rubros y saldos, y aplicando los efectos sobre las líneas de presupuesto y el control documental.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@BudgetModificationXml debe contener un nodo /BudgetModification con la cabecera (Id, Code, OperatingUnitId, BudgetaryValidityId, DocumentDate, DocumentSource, etc.); Si @Id > 0 debe existir el registro en Budget.BudgetModification y estar en Status = 1 (Pendiente) para poder modificarse; Debe existir un Budget.BudgetHeader para el BudgetaryValidityId y Type = DocumentSource; Debe existir Budget.BudgetaryValidity con Year = año del DocumentDate y mes (IncomeMonth o ExpenseMonth) coincidente según DocumentSource; DocumentSource debe ser 1 (Ingresos) o 2 (Gastos); Debe llegar al menos un detalle en el XML o existir detalles previos; Las CategoryId y RevenueTypeId de los detalles deben pertenecer a la BudgetaryValidityId indicada; No deben existir detalles duplicados por (CategoryId, RevenueTypeId); Nature de los detalles debe ser 1 (débito) o 2 (crédito); Los valores deben respetar el signo según origen: Ingresos >=0, Gastos >0; En débitos, Value no puede superar el Balance actual de la línea Budget asociada', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una modificación en estado distinto a Pendiente (Status<>1) no puede ser modificada nuevamente; DocumentSource solo admite 1 (Ingresos) o 2 (Gastos); Nature de cada detalle solo puede ser 1 (débito) o 2 (crédito); Para Ingresos (DocumentSource=1) los valores deben ser >= 0; para Gastos (<>1) deben ser > 0; El valor débito (Nature=1) de un detalle no puede exceder el Balance disponible de la línea Budget asociada; No puede existir más de un detalle con la misma combinación CategoryId+RevenueTypeId dentro de la misma modificación; Los detalles existentes no pueden cambiar de ModificationId ni de BudgetId respecto al original; La fecha del documento debe corresponder al año de la vigencia y al mes de Ingreso o Gasto según el DocumentSource; Las CategoryId y RevenueTypeId de los detalles deben pertenecer a la misma BudgetaryValidityId del documento; Al confirmar (Status=2), TotalBudget y Balance se ajustan: -débito +crédito por cada detalle; BudgetControl se mantiene únicamente mientras la modificación esté en estado Pendiente (Status=1); Al crear, el código del documento se obtiene siempre vía Common.SP_GetSequence para FormType 200', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de presupuesto; Vigencia presupuestal; Presupuesto de Ingresos; Presupuesto de Gastos; Rubro / Categoría presupuestal; Tipo de ingreso (RevenueType); Fuente de financiación (FinancialSource); Naturaleza débito/crédito; Saldo presupuestal (Balance); Confirmación y anulación de documento; Control presupuestario; Secuencia numérica del documento', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe BudgetModification con el Id y Status <> 1 (no está en Pendiente) → Retorna error 999 indicando que la modificación está Confirmada (2) o Anulada (3) y aborta else Continúa el flujo; si @Status = 3 (Anulación) → Solo actualiza BudgetModification fijando Status, ModificationUser/Date y AnnulmentUser/Date; salta validaciones y procesamiento de detalles else Ejecuta validaciones, normalización de detalles y persistencia de cabecera/detalles/Budget; si @Id = 0 (creación nueva) y @Status <> 3 → Llama Common.SP_GetSequence (FormType 200, IdForm 208/221 según DocumentSource) para obtener @Code y luego INSERT en BudgetModification else UPDATE de la cabecera BudgetModification existente; si @DocumentSource = 1 (Ingresos) → Usa IdForm=208, DocumentTypeControl=1 y valida que bv.IncomeMonth coincida con MONTH(@DocumentDate); permite valores >=0 else Si =2 (Gastos): IdForm=221, DocumentTypeControl=31, valida bv.ExpenseMonth y exige Value > 0; si @Status = 2 (Confirmación) → Aplica los movimientos sobre Budget: suma DebitValueModification/CreditValueModification y recalcula TotalBudget y Balance; además calcula @AuxiliaryResult comparando totales de Ingresos vs Gastos de la vigencia else No afecta saldos del presupuesto; si @Status = 1 (Pendiente/Guardado) → Inserta en Budget.BudgetControl si no existe registro con ese DocumentType+DocumentNumber else DELETE de Budget.BudgetControl para ese DocumentType+DocumentNumber (libera el control en confirmación o anulación); si Detalle entrante con Id=0 y sin BudgetId pero existe línea Budget con misma CategoryId/RevenueTypeId bajo el BudgetHeader → Asocia el detalle a la línea existente (UPDATE BudgetId) else Crea nueva línea en Budget.Budget con valores en 0 y luego asocia el BudgetId', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.BudgetModification; Budget.BudgetModificationDetail; Budget.BudgetHeader; Budget.Budget; Budget.BudgetaryValidity; Budget.Category; Budget.RevenueType; Budget.FinancialSource; Budget.BudgetControl', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification_Output';
-- GO
