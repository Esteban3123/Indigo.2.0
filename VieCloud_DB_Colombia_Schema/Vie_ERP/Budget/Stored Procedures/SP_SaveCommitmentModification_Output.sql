-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-11
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una modificación del compromiso
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveCommitmentModification_Output]
    @CommitmentModificationXml AS XML,
	@CommitmentModificationDetailForDeleteXml AS XML,
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
			@CommitmentId INT,
			@CommitmentType TINYINT,
			@UpTo TINYINT,
			@Document VARCHAR(100),
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT = 232,
			@DocumentTypeControl INT = 12,
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
		CommitmentDetailId INT,
		AvailabilityDetailId INT,
		CategoryId INT NOT NULL,
		RevenueTypeId INT NOT NULL,
		DateExpired DATETIME,
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
			@CommitmentId = t.x.value('CommitmentId[1]','int'),
			@UpTo = t.x.value('UpTo[1]','tinyint'),
			@Document = t.x.value('Document[1]','varchar(100)'),
			@Observations = t.x.value('Observations[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @CommitmentModificationXml.nodes('/CommitmentModification') t(x)

		IF EXISTS (SELECT 1 FROM Budget.CommitmentModification cm WHERE cm.Id = @Id AND cm.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La Modificación de Compromiso se encuentra en estado: ' + IIF(cm.Status = 2, 'Confirmado', 'Anulado')
			FROM Budget.CommitmentModification cm 
			WHERE cm.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Budget].[CommitmentModification]
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

			--Obtengo el tipo de la compromiso
			SELECT @CommitmentType  = c.CommitmentType
			FROM Budget.Commitment c
			WHERE c.Id = @CommitmentId

			--Eliminamos los detalles indicados
			DELETE cmd
			FROM @CommitmentModificationDetailForDeleteXml.nodes('/CommitmentModificationDetail') t(x)
			JOIN Budget.CommitmentModificationDetail cmd ON t.x.value('Id[1]','int') = cmd.Id
			WHERE cmd.CommitmentModificationId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('CommitmentDetailId[1]','int'),
					t.x.value('AvailabilityDetailId[1]','int'),
					t.x.value('CategoryId[1]','int'),
					t.x.value('RevenueTypeId[1]','int'),
					ISNULL(t.x.value('DateExpired[1]','datetime'), DATEADD(yy, DATEDIFF(yy, 0, @DocumentDate) + 1, -1)),
					t.x.value('Nature[1]','int'),
					t.x.value('Value[1]','decimal(18,2)'),
					t.x.value('IsLogBase[1]','bit')
				FROM @CommitmentModificationXml.nodes('/CommitmentModification/CommitmentModificationDetail') t(x)

			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					cmd.Id, 
					cmd.CommitmentDetailId, 
					cd.AvailabilityDetailId, 
					cd.CategoryId,
					cd.RevenueTypeId,
					DATEADD(yy, DATEDIFF(yy, 0, @DocumentDate) + 1, -1),
					cmd.Nature, 
					cmd.Value,
					cmd.IsLogBase
				FROM Budget.CommitmentModificationDetail cmd
				JOIN Budget.CommitmentDetail cd ON cmd.CommitmentDetailId = cd.Id
				LEFT JOIN @Details d ON cmd.Id = d.Id
				WHERE cmd.CommitmentModificationId = @Id AND ISNULL(d.Id, 0) = 0

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
				SELECT @Message = 'La Fecha de la Modificación de Compromiso (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.ExpenseMonth AS VARCHAR), 2)) + ').'
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
					   @MessageResult = 'La Modificación de Compromiso no tiene detalles.'
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM Budget.CommitmentModificationDetail cmd 
				JOIN @Details d ON cmd.Id = d.Id 
				WHERE cmd.CommitmentModificationId <> @Id OR ISNULL(cmd.CommitmentDetailId, 0) <> ISNULL(d.CommitmentDetailId, 0)
			) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles de la Modificación de Compromiso han sido alterados.'
				RETURN
			END

			-- Valido el tipo de compromiso
			IF @CommitmentType NOT IN (1, 2)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El tipo de compromiso no es válido.'
				RETURN
			END

			-- Valido, si el comprimiso original es de tipo compromiso, debe tener asociado un detalle de disponibilidad válido
			IF @CommitmentType = 1 AND EXISTS
			(
				SELECT 1
				FROM @Details d
				LEFT JOIN Budget.AvailabilityDetail ad ON d.AvailabilityDetailId = ad.Id
				WHERE ad.Id IS NULL
			)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Existen detalles de la Modificación de Compromiso que no estan asociados a una disponibilidad.'
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS 
			(
				SELECT 1 
				FROM Budget.AvailabilityDetail ad 
				JOIN Budget.Budget b ON ad.BudgetId = b.Id 
				JOIN @Details d ON ad.Id = d.AvailabilityDetailId 
				WHERE b.CategoryId <> d.CategoryId OR b.RevenueTypeId <> d.RevenueTypeId
			) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los rubros de los detalles de la Modificación de Compromiso no corresponden con los de la disponibilidad.'
				RETURN
			END

			--- Valido que las categorias existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.Category c ON d.CategoryId = c.Id AND @BudgetaryValidityId = c.BudgetaryValidityId WHERE c.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen rubros que no existen o no pertenecen a la vigencia de la Modificación de Compromiso.'
				RETURN
			END

			--- Valido que los tipos existan
			IF EXISTS (SELECT 1 FROM @Details d LEFT JOIN Budget.RevenueType rt ON d.RevenueTypeId = rt.Id AND @BudgetaryValidityId = rt.BudgetaryValidityId WHERE rt.Id IS NULL)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen tipos que no existen o no pertenecen a la vigencia de la Modificación de Compromiso.'
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				GROUP BY d.AvailabilityDetailId, d.CategoryId, d.RevenueTypeId 
				HAVING COUNT(*) > 1
			)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La Modificación de Compromiso tiene detalles duplicados.'
				RETURN
			END

			--- Valido que no existan detalles con valores en 0 o negativos
			IF EXISTS (SELECT 1 FROM @Details d WHERE d.Value <= 0)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Existen Rubros de la Modificación de Compromiso con el valor menor o igual a 0.'
				RETURN
			END

			--- Valido hasta donde se liberarán recursos
			IF @UpTo NOT IN (3, 4)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El nivel de la Modificación de Compromiso hasta la que se liberaran recursos no es valido.'
				RETURN
			END

			--- Valido hasta donde se liberarán recursos
			IF @CommitmentType <> 1 AND @UpTo <> 4
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El nivel de la Modificación de Compromiso hasta la que se liberaran recursos debe ser hasta el presupuesto.'
				RETURN
			END

			--- Valido la naturaleza de los detalles
			IF EXISTS (SELECT 1 FROM @Details WHERE Nature NOT IN (1, 2))
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code
						FROM @Details d
						JOIN Budget.CommitmentDetail cd ON d.CommitmentDetailId = cd.Id
						JOIN Budget.Category c ON cd.CategoryId = c.Id
						JOIN Budget.RevenueType rt ON cd.RevenueTypeId = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE d.Nature NOT IN (1, 2)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'La naturaleza de los siguientes rubros de la Modificación de Compromiso no son válidas: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--- Valido detalles debitos
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				LEFT JOIN Budget.CommitmentDetail cd ON d.CommitmentDetailId = cd.Id
				WHERE d.Nature = 1 AND d.Value > ISNULL(cd.Balance, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Balance (' + FORMAT(ISNULL(cd.Balance, 0), 'C0', 'es-CO') + ') - Modificación(' + FORMAT(d.Value, 'C0', 'es-CO') + ')'
						FROM @Details d
						LEFT JOIN Budget.CommitmentDetail cd ON d.CommitmentDetailId = cd.Id
						LEFT JOIN Budget.Category c ON ISNULL(cd.CategoryId, d.CategoryId) = c.Id
						LEFT JOIN Budget.RevenueType rt ON ISNULL(cd.RevenueTypeId, d.RevenueTypeId) = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE d.Nature = 1 AND d.Value > ISNULL(cd.Balance, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'El valor débito de los siguientes rubros de la Modificación de Compromiso no pueden ser mayor que el saldo del compromiso: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--- Valido detalles creditos 
			IF @UpTo IN (3) 
			BEGIN -- afectan hasta la disponibilidad
				IF EXISTS 
				(
					SELECT 1 
					FROM 
					(
						SELECT	d.AvailabilityDetailId, d.CategoryId, d.RevenueTypeId,
								SUM(d.Value) Value
						FROM @Details d 
						WHERE d.Nature = 2
						GROUP BY d.AvailabilityDetailId, d.CategoryId, d.RevenueTypeId
					) cd 
					LEFT JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
					WHERE cd.Value > ISNULL(ad.Balance, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Disponibilidad ' + ISNULL(a.Code, 'N/A') + ', Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Balance (' + FORMAT(ISNULL(ad.Balance, 0), 'C0', 'es-CO') + ') - Modificación(' + FORMAT(cd.Value, 'C0', 'es-CO') + ')'
							FROM 
							(
								SELECT	d.AvailabilityDetailId, d.CategoryId, d.RevenueTypeId,
										SUM(d.Value) Value
								FROM @Details d 
								WHERE d.Nature = 2
								GROUP BY d.AvailabilityDetailId, d.CategoryId, d.RevenueTypeId
							) cd 
							LEFT JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
							LEFT JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
							LEFT JOIN Budget.Category c ON cd.CategoryId = c.Id
							LEFT JOIN Budget.RevenueType rt ON cd.RevenueTypeId = rt.Id
							LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
							WHERE cd.Value > ISNULL(ad.Balance, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = 'El valor crédito de los siguientes rubros de la Modificación de Compromiso no pueden ser mayor que el saldo de la disponibilidad: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END
			END
			ELSE
			BEGIN -- Afectan hasta el presupuesto
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
						   @MessageResult = 'El valor crédito de los siguientes rubros de la Modificación de Compromiso no pueden ser mayor que el saldo del presupuesto: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
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
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Modificación de Compromiso')
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[CommitmentModification]
				(
					[Code],[BudgetaryValidityId],[DocumentDate],[CommitmentId],[UpTo],[Document],[Observations],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[EntityId],[EntityCode],[EntityName]
				)
				SELECT @Code,@BudgetaryValidityId,@DocumentDate,@CommitmentId,@UpTo,@Document,@Observations,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@EntityId,@EntityCode,@EntityName

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[CommitmentModification]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[CommitmentId] = @CommitmentId,
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

			INSERT INTO Budget.CommitmentDetail
			(
				CommitmentId, AvailabilityDetailId, CategoryId, RevenueTypeId, ExpiredDate,
				InitialValue, DebitModificationValue, CreditModificationValue, TotalCommitment, ExecutedValue, Balance
			)
			SELECT	@CommitmentId, d.AvailabilityDetailId, d.CategoryId, d.RevenueTypeId, d.DateExpired,
					0, 0, 0, 0, 0, 0
			FROM @Details d
			LEFT JOIN Budget.CommitmentDetail cd 
				ON @CommitmentId = cd.CommitmentId 
					AND ISNULL(d.AvailabilityDetailId, 0) = ISNULL(cd.AvailabilityDetailId, 0) 
					AND d.CategoryId = cd.CategoryId 
					AND d.RevenueTypeId = cd.RevenueTypeId
			WHERE ISNULL(d.Id, 0) = 0 AND cd.Id IS NULL

			UPDATE d
				SET d.CommitmentDetailId = cd.Id
			FROM @Details d
			JOIN Budget.CommitmentDetail cd 
				ON @CommitmentId = cd.CommitmentId 
					AND ISNULL(d.AvailabilityDetailId, 0) = ISNULL(cd.AvailabilityDetailId, 0) 
					AND d.CategoryId = cd.CategoryId 
					AND d.RevenueTypeId = cd.RevenueTypeId
			WHERE ISNULL(d.Id, 0) = 0

			/*************************************************************************************/

			UPDATE cmd
				SET cmd.Nature = d.Nature, 
					cmd.Value = d.Value,
					cmd.IsLogBase = d.IsLogBase
			FROM Budget.CommitmentModificationDetail cmd
			JOIN @Details d ON cmd.Id = d.Id

			INSERT INTO Budget.CommitmentModificationDetail (CommitmentModificationId, CommitmentDetailId, Nature, Value, IsLogBase)
				SELECT
					@Id CommitmentModificationId,
					d.CommitmentDetailId,
					d.Nature,
					d.Value,
					d.IsLogBase
				FROM @Details d
				WHERE ISNULL(d.Id, 0) = 0

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				UPDATE cd
					SET cd.DebitModificationValue = cd.DebitModificationValue + IIF(d.Nature = 1, d.Value, 0),
						cd.CreditModificationValue = cd.CreditModificationValue + IIF(d.Nature = 2, d.Value, 0),
						cd.TotalCommitment = cd.TotalCommitment - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0),
						cd.Balance = cd.Balance - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0)
				FROM Budget.CommitmentModificationDetail d
				JOIN Budget.CommitmentDetail cd ON d.CommitmentDetailId = cd.Id
				WHERE d.CommitmentModificationId = @Id

				IF @CommitmentType = 1
				BEGIN
					UPDATE ad
						SET ad.ExecutedValue = ad.ExecutedValue - d.Value,
							ad.Balance = ad.Balance + d.Value
					FROM Budget.CommitmentModificationDetail d
					JOIN Budget.CommitmentDetail cd ON d.CommitmentDetailId = cd.Id
					JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
					WHERE d.CommitmentModificationId = @Id AND d.Nature = 1

					IF @UpTo > 3
					BEGIN
						--Si afecta el presupuesto, se llama al procedimiento almacenado encargado de la modificación de presupuesto con el fin de liberar recursos
						DECLARE @AvailabilityRows INT = 1,
								@AvailabilityId INT = 0

						WHILE @AvailabilityRows > 0
						BEGIN
							SELECT TOP 1
								@AvailabilityId = ad.AvailabilityId
							FROM Budget.CommitmentModificationDetail cmd
							JOIN Budget.CommitmentDetail cd ON cmd.CommitmentDetailId = cd.Id
							JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
							WHERE cmd.CommitmentModificationId = @Id
								AND ad.AvailabilityId > @AvailabilityId
							GROUP BY ad.AvailabilityId

							SET @AvailabilityRows = @@ROWCOUNT
							IF @AvailabilityRows = 0 
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
										AvailabilityModification.BudgetaryValidityId,
										AvailabilityModification.DocumentDate,
										@AvailabilityId AvailabilityId,
										AvailabilityModification.UpTo,
										AvailabilityModification.Document,								
										AvailabilityModification.Observations,
										AvailabilityModification.Status,
										@Id EntityId,
										@Code EntityCode,
										'CommitmentModification' EntityName,
										AvailabilityModificationDetail.*
									FROM Budget.CommitmentModification AvailabilityModification
									JOIN
									( 
										SELECT
											cmd.CommitmentModificationId,
											ad.Id AvailabilityDetailId,
											cd.CategoryId,
											cd.RevenueTypeId,
											cmd.Nature,
											cmd.Value
										FROM Budget.CommitmentModificationDetail cmd
										JOIN Budget.CommitmentDetail cd ON cmd.CommitmentDetailId = cd.Id
										JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
										WHERE cmd.CommitmentModificationId = @Id
											AND ad.AvailabilityId = @AvailabilityId
									) AvailabilityModificationDetail ON AvailabilityModification.Id = AvailabilityModificationDetail.CommitmentModificationId
									WHERE AvailabilityModification.Id = @Id
									FOR XML AUTO,TYPE, ELEMENTS
								)
							)

							EXEC [Budget].[SP_SaveAvailabilityModification_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, @AuxiliaryResult OUT, NULL, NULL

							IF @Code_Output <> 0
							BEGIN
								SELECT @CodeResult = 999, 
										@MessageResult = @Message_Output
								RETURN
							END

							SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
						END
					END

					UPDATE ad
						SET ad.ExecutedValue = ad.ExecutedValue + d.Value,
							ad.Balance = ad.Balance - d.Value
					FROM Budget.CommitmentModificationDetail d
					JOIN Budget.CommitmentDetail cd ON d.CommitmentDetailId = cd.Id
					JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
					WHERE d.CommitmentModificationId = @Id AND d.Nature = 2
				END
				ELSE
				BEGIN
					UPDATE b
						SET b.ExecutedValue = b.ExecutedValue - IIF(d.Nature = 1, d.Value, 0) + IIF(d.Nature = 2, d.Value, 0),
							b.Balance = b.Balance + IIF(d.Nature = 1, d.Value, 0) - IIF(d.Nature = 2, d.Value, 0),
							b.ModificationUser = @CodeUser,
							b.ModificationDate = [Common].[GETDATE]()
					FROM Budget.CommitmentModificationDetail d
					JOIN Budget.CommitmentDetail cd ON d.CommitmentDetailId = cd.Id
					JOIN Budget.Budget b ON cd.CategoryId = b.CategoryId AND cd.RevenueTypeId = b.RevenueTypeId
					WHERE d.CommitmentModificationId = @Id
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
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Modificación de Compromiso con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Modificación de Compromiso con código ', @Code)
				   ELSE CONCAT('Se guardó la Modificación de Compromiso con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar, confirmar o anular una modificación de compromiso presupuestario de gastos dentro de una vigencia fiscal. Recibe los datos del encabezado y el detalle de la modificación en formato XML, valida que la fecha del documento corresponda al período presupuestal activo, que los detalles tengan saldo disponible y que el compromiso no esté ya confirmado o anulado. Gestiona el ciclo completo del trámite: guarda o actualiza el registro en la tabla CommitmentModification, administra los detalles en CommitmentModificationDetail (insertando nuevos, actualizando existentes y eliminando los indicados), y cuando el estado es de anulación registra el usuario y la fecha de anulación. Se apoya en BudgetHeader para identificar el presupuesto de gastos vigente y en Commitment para determinar el tipo de compromiso que se está modificando.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCommitmentModification_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCommitmentModification_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crear/actualizar), confirma o anula una Modificación de Compromiso presupuestal, validando reglas de vigencia, rubros y saldos, y propagando los efectos a los detalles de compromiso, disponibilidad o presupuesto según el alcance de liberación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La Modificación de Compromiso debe estar en estado 1 (pendiente) para poder modificarse; si Status<>1 se rechaza con mensaje según sea Confirmado(2) o Anulado(3); La fecha del documento debe pertenecer a la vigencia presupuestal indicada (Year y ExpenseMonth de BudgetaryValidity); Debe existir al menos un detalle (en el XML o previamente persistido); Los detalles editados no deben haber alterado su CommitmentModificationId ni su CommitmentDetailId base; El CommitmentType del compromiso original debe ser 1 (compromiso) o 2; Si el compromiso es tipo 1, todos los detalles deben estar asociados a un AvailabilityDetail válido; Las categorías y tipos de ingreso de los detalles deben pertenecer a la vigencia presupuestal; Los rubros (Category/RevenueType) de los detalles deben coincidir con los del Budget asociado a la disponibilidad; No debe haber detalles duplicados por (AvailabilityDetailId, CategoryId, RevenueTypeId); Todos los detalles deben tener Value > 0; UpTo debe ser 3 (hasta disponibilidad) o 4 (hasta presupuesto); si CommitmentType<>1 obligatoriamente UpTo=4; La naturaleza de cada detalle debe ser 1 (débito) o 2 (crédito); El valor débito (Nature=1) no puede superar el Balance del CommitmentDetail original; El valor crédito (Nature=2) agregado por (Availability/Category/RevenueType) no puede superar el Balance de la disponibilidad (UpTo=3) o del presupuesto (UpTo=4)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de compromiso presupuestal; Compromiso presupuestal; Disponibilidad presupuestal; Vigencia presupuestal; Rubro/Categoría presupuestal; Tipo de ingreso (RevenueType); Fuente financiera (Recurso); Naturaleza débito/crédito; Liberación de recursos hasta disponibilidad o presupuesto; Control presupuestario (BudgetControl); Confirmación y anulación de documento', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Budget.SP_SaveAvailabilityModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.CommitmentModification; Budget.BudgetHeader; Budget.Commitment; Budget.CommitmentModificationDetail; Budget.CommitmentDetail; Budget.BudgetaryValidity; Budget.AvailabilityDetail; Budget.Availability; Budget.Budget; Budget.Category; Budget.RevenueType; Budget.FinancialSource; Budget.BudgetControl', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitmentModification_Output';
-- GO
