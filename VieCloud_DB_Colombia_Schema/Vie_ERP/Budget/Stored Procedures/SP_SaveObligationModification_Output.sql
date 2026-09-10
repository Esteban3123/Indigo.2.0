-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-11
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una modificación de la obligacion
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveObligationModification_Output]
    @ObligationModificationXml AS XML,
	@ObligationModificationDetailForDeleteXml AS XML,
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
			@ObligationId INT,
			@ObligationType TINYINT,
			@UpTo TINYINT,
			@Document VARCHAR(100),
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT = 234,
			@DocumentTypeControl INT = 15,
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
		ObligationDetailId INT,
		CommitmentDetailId INT,
		CategoryId INT NOT NULL,
		RevenueTypeId INT NOT NULL,
		ExpiredDate DATETIME,
		Nature TINYINT,
		Value DECIMAL(18,2),
		IsLogBase BIT
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),			
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@ObligationId = t.x.value('ObligationId[1]','int'),
			@UpTo = t.x.value('UpTo[1]','tinyint'),
			@Document = t.x.value('Document[1]','varchar(100)'),
			@Observations = t.x.value('Observations[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @ObligationModificationXml.nodes('/ObligationModification') t(x)

		IF EXISTS (SELECT 1 FROM Budget.ObligationModification om WHERE om.Id = @Id AND om.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La Modificación de Obligación se encuentra en estado: ' + IIF(om.Status = 2, 'Confirmado', 'Anulado')
			FROM Budget.ObligationModification om 
			WHERE om.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Budget].[ObligationModification]
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

			--Obtengo el tipo de la obligación
			SELECT @ObligationType  = o.ObligationType
			FROM Budget.Obligation o
			WHERE o.Id = @ObligationId

			--Eliminamos los detalles indicados
			DELETE omd
			FROM @ObligationModificationDetailForDeleteXml.nodes('/ObligationModificationDetail') t(x)
			JOIN Budget.ObligationModificationDetail omd ON t.x.value('Id[1]','int') = omd.Id
			WHERE omd.ObligationModificationId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('ObligationDetailId[1]','int'),
					t.x.value('CommitmentDetailId[1]','int'),
					t.x.value('CategoryId[1]','int'),
					t.x.value('RevenueTypeId[1]','int'),
					t.x.value('ExpiredDate[1]','datetime'),
					t.x.value('Nature[1]','int'),
					t.x.value('Value[1]','decimal(18,2)'),
					t.x.value('IsLogBase[1]','bit')
				FROM @ObligationModificationXml.nodes('/ObligationModification/ObligationModificationDetail') t(x)

			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					omd.Id, 
					omd.ObligationDetailId, 
					od.CommitmentDetailId,
					od.CategoryId,
					od.RevenueTypeId,
					omd.ExpiredDate,
					omd.Nature, 
					omd.Value,
					omd.IsLogBase
				FROM Budget.ObligationModificationDetail omd
				JOIN Budget.ObligationDetail od ON omd.ObligationDetailId = od.Id
				LEFT JOIN @Details d ON omd.Id = d.Id
				WHERE omd.ObligationModificationId = @Id AND ISNULL(d.Id, 0) = 0

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
				SELECT @Message = 'La Fecha de la Modificación de Obligación (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.ExpenseMonth AS VARCHAR), 2)) + ').'
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
					   @MessageResult = 'La Modificación de Obligación no tiene detalles.'
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM Budget.ObligationModificationDetail omd 
				JOIN @Details d ON omd.Id = d.Id 
				WHERE omd.ObligationModificationId <> @Id OR ISNULL(omd.ObligationDetailId, 0) <> ISNULL(d.ObligationDetailId, 0)
			) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles de la Modificación de Obligación han sido alterados.'
				RETURN
			END

			-- Valido el tipo de obligacion
			IF @ObligationType NOT IN (1, 2)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El tipo de obligación no es válido.'
				RETURN
			END

			-- Valido, si el comprimiso original es de tipo compromiso, debe tener asociado un detalle de disponibilidad válido
			IF @ObligationType = 1 AND EXISTS
			(
				SELECT 1
				FROM @Details d
				LEFT JOIN Budget.CommitmentDetail cd ON d.CommitmentDetailId = cd.Id
				WHERE cd.Id IS NULL
			)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Existen detalles de la Modificación de la Obligación que no estan asociados a un compromiso.'
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM Budget.CommitmentDetail cd 
				JOIN @Details d ON cd.Id = d.CommitmentDetailId
				WHERE cd.CategoryId <> d.CategoryId OR cd.RevenueTypeId <> d.RevenueTypeId
			) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los rubros de los detalles de la Modificación de la Obligación no corresponden con los del compromiso.'
				RETURN
			END

			--- Valido que las categorias existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.Category c ON d.CategoryId = c.Id AND @BudgetaryValidityId = c.BudgetaryValidityId WHERE c.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen rubros que no existen o no pertenecen a la vigencia de la Modificación de Obligación.'
				RETURN
			END

			--- Valido que los tipos existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.RevenueType rt ON d.RevenueTypeId = rt.Id AND @BudgetaryValidityId = rt.BudgetaryValidityId WHERE rt.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen tipos que no existen o no pertenecen a la vigencia de la Modificación de Obligación.'
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				GROUP BY d.ObligationDetailId, d.CommitmentDetailId, d.CategoryId, d.RevenueTypeId
				HAVING COUNT(*) > 1
			)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La Modificación de Obligación tiene detalles duplicados.'
				RETURN
			END

			--- Valido que no existan detalles con valores en 0 o negativos
			IF EXISTS (SELECT 1 FROM @Details d WHERE d.Value <= 0)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Existen Rubros de la Modificación de Obligación con el valor menor o igual a 0.'
				RETURN
			END

			--- Valido hasta donde se liberarán recursos
			IF @UpTo NOT IN (2, 3, 4)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El nivel de la Modificación de Obligación hasta la que se liberaran recursos no es valido.'
				RETURN
			END

			--- Valido hasta donde se liberarán recursos
			IF @ObligationType <> 1 AND @UpTo <> 4
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El nivel de la Modificación de Obligación hasta la que se liberaran recursos debe ser hasta el presupuesto.'
				RETURN
			END

			--- Valido la naturaleza de los detalles
			IF EXISTS (SELECT 1 FROM @Details WHERE Nature NOT IN (1, 2))
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code
						FROM @Details d
						JOIN Budget.ObligationDetail od ON d.ObligationDetailId = od.Id						
						JOIN Budget.Category c ON od.CategoryId = c.Id
						JOIN Budget.RevenueType rt ON od.RevenueTypeId = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE d.Nature NOT IN (1, 2)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'La naturaleza de los siguientes rubros de la Modificación de Obligación no son válidas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--- Valido detalles debitos
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				LEFT JOIN Budget.ObligationDetail od ON d.ObligationDetailId = od.Id
				WHERE d.Nature = 1 AND d.Value > ISNULL(od.Balance, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Balance (' + FORMAT(ISNULL(od.Balance, 0), 'C0', 'es-CO') + ') - Modificación(' + FORMAT(d.Value, 'C0', 'es-CO') + ')'
						FROM @Details d
						LEFT JOIN Budget.ObligationDetail od ON d.ObligationDetailId = od.Id
						LEFT JOIN Budget.Category c ON ISNULL(od.CategoryId, d.CategoryId) = c.Id
						LEFT JOIN Budget.RevenueType rt ON ISNULL(od.RevenueTypeId, d.RevenueTypeId) = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE d.Nature = 1 AND d.Value > ISNULL(od.Balance, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'El valor débito de los siguientes rubros de la Modificación de Obligación no pueden ser mayor que el saldo de la obligación: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--- Valido detalles creditos 
			IF @UpTo IN (2) 
			BEGIN -- afectan hasta el compromiso
				IF EXISTS 
				(
					SELECT 1 
					FROM 
					(
						SELECT	d.CommitmentDetailId, d.CategoryId, d.RevenueTypeId,
								SUM(d.Value) Value
						FROM @Details d 
						WHERE d.Nature = 2
						GROUP BY d.CommitmentDetailId, d.CategoryId, d.RevenueTypeId
					) od 
					LEFT JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id
					WHERE od.Value > ISNULL(cd.Balance, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Compromiso ' + ISNULL(cm.Code, 'N/A') + ', Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Balance (' + FORMAT(ISNULL(cd.Balance, 0), 'C0', 'es-CO') + ') - Modificación(' + FORMAT(od.Value, 'C0', 'es-CO') + ')'
							FROM 
							(
								SELECT	d.CommitmentDetailId, d.CategoryId, d.RevenueTypeId,
										SUM(d.Value) Value
								FROM @Details d 
								WHERE d.Nature = 2
								GROUP BY d.CommitmentDetailId, d.CategoryId, d.RevenueTypeId
							) od 
							LEFT JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id
							LEFT JOIN Budget.Commitment cm ON cd.CommitmentId = cm.Id
							LEFT JOIN Budget.Category c ON cd.CategoryId = c.Id
							LEFT JOIN Budget.RevenueType rt ON cd.RevenueTypeId = rt.Id
							LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
							WHERE od.Value > ISNULL(cd.Balance, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = 'El valor crédito de los siguientes rubros de la Modificación de Obligación no pueden ser mayor que el saldo del compromiso: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END
			END
			ELSE
			BEGIN -- Valido detalles creditos (que afectan directamente el presupuesto)
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
					) od
					LEFT JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND od.CategoryId = b.CategoryId AND od.RevenueTypeId = b.RevenueTypeId
					WHERE od.Value > ISNULL(b.Balance, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Balance (' + FORMAT(ISNULL(b.Balance, 0), 'C0', 'es-CO') + ') - Modificación(' + FORMAT(od.Value, 'C0', 'es-CO') + ')'
							FROM 
							(
								SELECT	d.CategoryId, d.RevenueTypeId,
										SUM(d.Value) Value
								FROM @Details d 
								WHERE d.Nature = 2
								GROUP BY d.CategoryId, d.RevenueTypeId
							) od
							LEFT JOIN Budget.Budget b ON @BudgetHeaderId = b.BudgetHeaderId AND od.CategoryId = b.CategoryId AND od.RevenueTypeId = b.RevenueTypeId
							LEFT JOIN Budget.Category c ON od.CategoryId = c.Id
							LEFT JOIN Budget.RevenueType rt ON od.RevenueTypeId = rt.Id
							LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
							WHERE od.Value > ISNULL(b.Balance, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = 'El valor crédito de los siguientes rubros de la Modificación de Obligación no pueden ser mayor que el saldo del presupuesto: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				IF @Code = '' 
				BEGIN
					--Si se esta insertando por primera vez se consulta la secuencia numerica
					DECLARE @IsManual BIT
				
					EXEC Common.SP_GetSequence 200, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

					IF @Code_Output <> 0
					BEGIN
						SELECT	@CodeResult = 999, 
								@MessageResult = REPLACE(@Message_Output, '{0}', 'Modificación de Obligación')
						RETURN
					END

					--Se inserta la cabecera
					INSERT INTO [Budget].[ObligationModification]
					(
						[Code],[BudgetaryValidityId],[DocumentDate],[ObligationId],[UpTo],[Document],[Observations],
						[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
						[EntityId],[EntityCode],[EntityName]
					)
					SELECT @Code,@BudgetaryValidityId,@DocumentDate,@ObligationId,@UpTo,@Document,@Observations,
						@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
						@EntityId,@EntityCode,@EntityName

					--Obtengo el id de la cabcera
					SET @Id = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[ObligationModification]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[ObligationId] = @ObligationId,
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
			LEFT JOIN Budget.Budget b ON d.CategoryId = b.CategoryId AND d.RevenueTypeId = b.RevenueTypeId
			WHERE ISNULL(d.Id, 0) = 0 AND b.Id IS NULL

			/*************************************************************************************/

			INSERT INTO Budget.ObligationDetail
			(
				ObligationId, CommitmentDetailId, CategoryId, RevenueTypeId, ExpiredDate,
				InitialValue, DebitModificationValue, CreditModificationValue, TotalObligation, ExecutedValue, Balance
			)
			SELECT	@ObligationId, d.CommitmentDetailId, d.CategoryId, d.RevenueTypeId, DATEADD(yy, DATEDIFF(yy, 0, @DocumentDate) + 1, -1),
					0, 0, 0, 0, 0, 0
			FROM @Details d
			LEFT JOIN Budget.ObligationDetail od 
				ON @ObligationId = od.ObligationId 
					AND ISNULL(d.CommitmentDetailId, 0) = ISNULL(od.CommitmentDetailId, 0) 
					AND d.CategoryId = od.CategoryId 
					AND d.RevenueTypeId = od.RevenueTypeId
			WHERE ISNULL(d.Id, 0) = 0 AND od.Id IS NULL

			UPDATE d
				SET d.ObligationDetailId = od.Id
			FROM @Details d
			JOIN Budget.ObligationDetail od 
				ON @ObligationId = od.ObligationId 
					AND ISNULL(d.CommitmentDetailId, 0) = ISNULL(od.CommitmentDetailId, 0) 
					AND d.CategoryId = od.CategoryId 
					AND d.RevenueTypeId = od.RevenueTypeId
			WHERE ISNULL(d.Id, 0) = 0 AND ISNULL(d.ObligationDetailId, 0) = 0

			/*************************************************************************************/

			UPDATE omd
				SET omd.ExpiredDate = d.ExpiredDate,
					omd.Nature = d.Nature, 
					omd.Value = d.Value,
					omd.IsLogBase = d.IsLogBase
			FROM Budget.ObligationModificationDetail omd
			JOIN @Details d ON omd.Id = d.Id

			INSERT INTO Budget.ObligationModificationDetail (ObligationModificationId, ObligationDetailId, ExpiredDate, Nature, Value, IsLogBase)
				SELECT
					@Id ObligationModificationId,
					d.ObligationDetailId,
					d.ExpiredDate,
					d.Nature,
					d.Value,
					d.IsLogBase
				FROM @Details d
				WHERE ISNULL(d.Id, 0) = 0

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				UPDATE od
					SET
						od.DebitModificationValue = od.DebitModificationValue + IIF(d.Nature = 1, d.Value, 0),
						od.CreditModificationValue = od.CreditModificationValue + IIF(d.Nature = 2, d.Value, 0),
						od.TotalObligation = od.TotalObligation - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0),
						od.Balance = od.Balance - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0)
				FROM Budget.ObligationModificationDetail d
				JOIN Budget.ObligationDetail od ON d.ObligationDetailId = od.Id
				WHERE d.ObligationModificationId = @Id

				IF @ObligationType = 1
				BEGIN
					UPDATE cd
						SET cd.ExecutedValue = cd.ExecutedValue - od.Value,
							cd.Balance = cd.Balance + od.Value
					FROM 
					(
						SELECT od.CommitmentDetailId, SUM(d.Value) Value
						FROM Budget.ObligationModificationDetail d
						JOIN Budget.ObligationDetail od ON d.ObligationDetailId = od.Id
						WHERE d.ObligationModificationId = @Id AND d.Nature = 1
						GROUP BY od.CommitmentDetailId
					) od
					JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id

					IF @UpTo > 2
					BEGIN
						--Si afecta el presupuesto, se llama al procedimiento almacenado encargado de la modificación de presupuesto con el fin de liberar recursos
						DECLARE @CommitmentRows INT = 1,
								@CommitmentId INT = 0

						WHILE @CommitmentRows > 0
						BEGIN
							SELECT TOP 1
								@CommitmentId = cd.CommitmentId
							FROM Budget.ObligationModificationDetail omd
							JOIN Budget.ObligationDetail od ON omd.ObligationDetailId = od.Id
							JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id
							WHERE omd.ObligationModificationId = @Id
								AND cd.CommitmentId > @CommitmentId
							ORDER BY cd.CommitmentId

							SET @CommitmentRows = @@ROWCOUNT
							IF @CommitmentRows = 0 
							BEGIN
								BREAK
							END
						
							SELECT @SubXml = CONVERT
							(
								XML, 
								(
									SELECT 
										0 Id,										
										'' Code,
										@OperatingUnitId OperatingUnitId,
										CommitmentModification.BudgetaryValidityId,
										CommitmentModification.DocumentDate,
										@CommitmentId CommitmentId,
										CommitmentModification.UpTo,
										CommitmentModification.Document,
										CommitmentModification.Observations,
										CommitmentModification.Status,
										@Id EntityId,
										@Code EntityCode,
										'ObligationModification' EntityName,
										CommitmentModificationDetail.*
									FROM Budget.ObligationModification CommitmentModification
									JOIN
									( 
										SELECT
											omd.ObligationModificationId,
											cd.Id CommitmentDetailId,
											cd.AvailabilityDetailId,
											od.CategoryId,
											od.RevenueTypeId,
											omd.Nature,
											SUM(omd.Value) Value,
											omd.IsLogBase
										FROM Budget.ObligationModificationDetail omd
										JOIN Budget.ObligationDetail od ON omd.ObligationDetailId = od.Id
										JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id
										WHERE omd.ObligationModificationId = @Id
											AND cd.CommitmentId = @CommitmentId
										GROUP BY omd.ObligationModificationId, cd.Id, cd.AvailabilityDetailId, od.CategoryId, od.RevenueTypeId, omd.Nature, omd.IsLogBase
									) CommitmentModificationDetail ON CommitmentModification.Id = CommitmentModificationDetail.ObligationModificationId
									WHERE CommitmentModification.Id = @Id
									For xml AUTO,TYPE, ELEMENTS
								)
							)

							EXEC [Budget].[SP_SaveCommitmentModification_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, @AuxiliaryResult OUT, NULL, NULL

							IF @Code_Output <> 0
							BEGIN
								SELECT @CodeResult = 999, 
										@MessageResult = @Message_Output
								RETURN
							END

							SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
						END
					END

					UPDATE cd
						SET cd.ExecutedValue = cd.ExecutedValue + d.Value,
							cd.Balance = cd.Balance - d.Value
					FROM Budget.ObligationModificationDetail d
					JOIN Budget.ObligationDetail od ON d.ObligationDetailId = od.Id
					JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id
					WHERE d.ObligationModificationId = @Id AND d.Nature = 2
				END
				ELSE
				BEGIN
					UPDATE b
						SET b.ExecutedValue = b.ExecutedValue - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0),
							b.Balance = b.Balance + IIF(d.Nature = 1, d.Value, 0) - IIF(d.Nature = 2, d.Value, 0),
							b.ModificationUser = @CodeUser,
							b.ModificationDate = [Common].[GETDATE]()
					FROM Budget.ObligationModificationDetail d
					JOIN Budget.ObligationDetail od ON d.ObligationDetailId = od.Id
					JOIN Budget.Budget b ON od.CategoryId = b.CategoryId AND od.RevenueTypeId = b.RevenueTypeId
					WHERE d.ObligationModificationId = @Id
				END
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
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Modificación de Obligación con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Modificación de Obligación con código ', @Code)
				   ELSE CONCAT('Se guardó la Modificación de Obligación con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para guardar, actualizar, confirmar o anular una modificación de obligación presupuestaria. Recibe los datos de la modificación en formato XML (encabezado y detalle), valida que la fecha del documento corresponda a la vigencia presupuestal activa, que existan detalles asociados y que la obligación no esté ya confirmada o anulada. Según el estado indicado, puede registrar una nueva modificación, actualizarla, confirmarla afectando el saldo de la obligación presupuestaria, o anularla registrando el usuario y la fecha de anulación. Devuelve el identificador y código del registro procesado, junto con un código y mensaje de resultado para informar al usuario si la operación fue exitosa o si ocurrió algún error de validación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveObligationModification_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveObligationModification_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una Modificación de Obligación en estado distinto de 1 (Pendiente) no puede modificarse: solo se permite operar mientras Status=1.; El año y mes de @DocumentDate deben estar dentro de la vigencia presupuestal (Year=YEAR(DocumentDate) y ExpenseMonth<=MONTH(DocumentDate)).; Solo se aceptan obligaciones cuyo ObligationType ∈ {1,2}.; Si la obligación es de tipo compromiso (=1), todos los detalles deben estar asociados a un CommitmentDetail existente.; Los rubros (CategoryId/RevenueTypeId) de los detalles deben coincidir con los del CommitmentDetail asociado.; Categorías y tipos de ingreso deben pertenecer a la misma BudgetaryValidity de la modificación.; No se permiten detalles duplicados por (ObligationDetailId, CommitmentDetailId, CategoryId, RevenueTypeId).; Todo detalle debe tener Value > 0.; Nature del detalle solo puede ser 1 (débito) o 2 (crédito).; El nivel @UpTo solo admite valores {2,3,4}; si la obligación no es de compromiso (ObligationType<>1) @UpTo debe ser 4 (presupuesto).; Los débitos no pueden superar el Balance del ObligationDetail asociado.; Los créditos agregados no pueden superar el Balance del CommitmentDetail (UpTo=2) o del Budget (UpTo>2).; El consecutivo del documento (Code) solo se solicita cuando se inserta por primera vez (@Id=0 y @Code='''').; ExpiredDate de los nuevos ObligationDetail se fija al último día del año de @DocumentDate.; Para cada (CategoryId,RevenueTypeId) de un detalle nuevo se asegura una fila en Budget.Budget con valores en cero si no existía.; Para cada combinación (ObligationId, CommitmentDetailId, CategoryId, RevenueTypeId) se asegura un Budget.ObligationDetail si no existía.; BudgetControl se sincroniza con el estado: existe mientras Status=1 y se elimina al confirmar (2) o anular (3).; Los detalles editados no pueden cambiar su ObligationModificationId ni su ObligationDetailId originales (anti-tampering).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de obligación presupuestal; Obligación presupuestaria; Compromiso presupuestal; Vigencia presupuestal; Rubro / categoría presupuestal; Tipo de ingreso (RevenueType); Fuente de financiación; Naturaleza débito/crédito; Liberación de recursos hasta compromiso/presupuesto; Control presupuestario de documentos; Anulación y confirmación de documentos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Status = 3 (anulación) → Actualiza ObligationModification marcándola como anulada (Status, AnnulmentUser, AnnulmentDate, ModificationUser/Date) y omite todas las validaciones y movimientos de saldos. else Ejecuta validaciones, persiste cabecera/detalles y, si Status=2, aplica los movimientos de saldos.; si @Id = 0 y @Code = '''' → Solicita un consecutivo vía Common.SP_GetSequence (tipo 200, formulario 234) y luego INSERT en Budget.ObligationModification. else Si @Id<>0, hace UPDATE de la cabecera de Budget.ObligationModification con los datos del XML.; si @Status = 2 (confirmación) → Actualiza saldos en Budget.ObligationDetail (Debit/Credit/Total/Balance) y, según @ObligationType, ajusta Budget.CommitmentDetail o Budget.Budget; setea ConfirmationUser/Date. else No realiza movimiento de saldos sobre obligación, compromiso ni presupuesto.; si @ObligationType = 1 (compromiso) en confirmación → Resta a CommitmentDetail.ExecutedValue / suma a Balance los débitos; si @UpTo>2 invoca Budget.SP_SaveCommitmentModification_Output por cada CommitmentId para liberar recursos hasta el presupuesto; suma ExecutedValue/resta Balance los créditos en CommitmentDetail. else Si @ObligationType<>1, ajusta directamente Budget.Budget (ExecutedValue/Balance) por categoría y tipo de ingreso.; si @UpTo = 2 (afecta hasta el compromiso) → Valida que la suma de créditos por (CommitmentDetailId, CategoryId, RevenueTypeId) no supere el Balance del CommitmentDetail. else Valida que la suma de créditos por (CategoryId, RevenueTypeId) no supere el Balance de Budget.Budget para el BudgetHeader de la vigencia.; si @Status = 1 (borrador/guardado) y no existe registro en BudgetControl con DocumentType=15 y DocumentNumber=@Code → Inserta un registro en Budget.BudgetControl bloqueando el documento. else Elimina el registro de Budget.BudgetControl correspondiente al documento (libera el control).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Budget.SP_SaveCommitmentModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.ObligationModification; Budget.BudgetHeader; Budget.Obligation; Budget.ObligationModificationDetail; Budget.ObligationDetail; Budget.BudgetaryValidity; Budget.CommitmentDetail; Budget.Commitment; Budget.Category; Budget.RevenueType; Budget.FinancialSource; Budget.Budget; Budget.BudgetControl', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligationModification_Output';
-- GO
