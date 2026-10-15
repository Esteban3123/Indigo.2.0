-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-24
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una orden de pago
-- =============================================
CREATE PROCEDURE [Budget].[SP_SavePaymentOrder_Output]
    @PaymentOrderXml AS XML,
	@PaymentOrderDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeMessageResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	@IdResult INT OUTPUT,
	@CodeResult VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT,
			@OperatingUnitId INT,
			@Code VARCHAR(20),
			@BudgetaryValidityId INT,
			@DocumentDate DATETIME,
			@ThirdPartyId INT,
			@Document VARCHAR(100),			
			@PaymentOrderType TINYINT,
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm VARCHAR(5) = '237',
			@DocumentType INT = 16,
			------------------------------
			@errors VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		Id INT,
		PaymentOrderId INT,
		ObligationDetailId INT,
		ExpiredDate DATETIME,
		InitialValue DECIMAL(18,2)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),			
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@ThirdPartyId = t.x.value('ThirdPartyId[1]','int'),
			@Document = t.x.value('Document[1]','varchar(100)'),
			@PaymentOrderType = t.x.value('PaymentOrderType[1]','tinyint'),
			@Observations = t.x.value('Observations[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @PaymentOrderXml.nodes('/PaymentOrder') t(x)

		IF EXISTS (SELECT 1 FROM Budget.PaymentOrder om WHERE om.Id = @Id AND om.Status <> 1)
		BEGIN
			SELECT @CodeMessageResult = 999, 
				   @MessageResult = 'La Orden de Pago se encuentra en estado: ' + IIF(om.Status = 2, 'Confirmado', 'Anulado'), 
				   @IdResult = 0, 
				   @CodeResult = '' 
			FROM Budget.PaymentOrder om 
			WHERE om.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Budget].[PaymentOrder]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id

			UPDATE [Budget].[PaymentOrderDetail]
				SET [DebitModificationValue] = [InitialValue],
					[TotalPaymentOrder] = 0,
					[Balance] = 0
			WHERE PaymentOrderId = @Id
		END
		ELSE
		BEGIN
			--Eliminamos los detalles indicados
			DELETE od
			FROM @PaymentOrderDetailForDeleteXml.nodes('/PaymentOrderDetail') t(x)
			JOIN Budget.PaymentOrderDetail od ON t.x.value('Id[1]','int') = od.Id
			WHERE od.PaymentOrderId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('PaymentOrderId[1]','int'),
					t.x.value('ObligationDetailId[1]','int'),
					t.x.value('ExpiredDate[1]','datetime'),
					t.x.value('InitialValue[1]','decimal(18,2)')
				FROM @PaymentOrderXml.nodes('/PaymentOrder/PaymentOrderDetail') t(x)

			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					od.Id, 
					od.PaymentOrderId, 
					od.ObligationDetailId,
					od.ExpiredDate,
					od.InitialValue
				FROM Budget.PaymentOrderDetail od
				LEFT JOIN @Details d ON od.Id = d.Id
				WHERE od.PaymentOrderId = @Id AND ISNULL(d.Id, 0) = 0

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
				SELECT @errors = 'La Fecha de la Orden de Pago (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.ExpenseMonth AS VARCHAR), 2)) + ').'
				FROM Budget.BudgetaryValidity bv
				WHERE bv.Id = @BudgetaryValidityId

				SELECT @CodeMessageResult = 999, 
					   @MessageResult = ISNULL(@errors, 'Periodo Presupuestal no encontrado.'), 
					   @IdResult = 0, 
					   @CodeResult = ''
				RETURN
			END

			--- Valido que existan detalles
			IF NOT EXISTS (SELECT 1 FROM @Details)
			BEGIN
				SELECT @CodeMessageResult = 999, 
					   @MessageResult = 'La Orden de Pago no tiene detalles.', 
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.PaymentOrderDetail od JOIN @Details d ON od.Id = d.Id WHERE od.PaymentOrderId <> @Id OR ISNULL(od.ObligationDetailId, 0) <> ISNULL(d.ObligationDetailId, 0)) 
			BEGIN
				SELECT @CodeMessageResult = 999, 
					   @MessageResult = 'Los detalles de la Orden de Pago han sido alterados.', 
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.ObligationDetailId HAVING COUNT(*) > 1)
			BEGIN
				SELECT @CodeMessageResult = 999, 
					   @MessageResult = 'La Orden de Pago tiene detalles duplicados.', 
					   @IdResult = 0, 
					   @CodeResult = '' 
				RETURN
			END

			--- Valido el tipo de orden de pago
			IF @PaymentOrderType <> 1
			BEGIN
				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'El Orden de Pago debe ser de tipo Orden de Pago.', 
						@IdResult = 0, 
						@CodeResult = '' 
				RETURN
			END
			
			IF EXISTS 
			(
				SELECT 1 
				FROM 
				(
					SELECT 
						d.ObligationDetailId,
						SUM(d.InitialValue) Value
					FROM @Details d 
					WHERE d.ObligationDetailId IS NOT NULL
					GROUP BY d.ObligationDetailId
				) od 
				JOIN Budget.ObligationDetail cd WITH (NOLOCK) ON od.ObligationDetailId = cd.Id
				WHERE od.Value > cd.Balance
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
						FROM 
						(
							SELECT 
								d.ObligationDetailId,
								SUM(d.InitialValue) Value
							FROM @Details d 
							WHERE d.ObligationDetailId IS NOT NULL
							GROUP BY d.ObligationDetailId
						) od 
						JOIN Budget.ObligationDetail cd WITH (NOLOCK) ON od.ObligationDetailId = cd.Id
						JOIN Budget.Category c WITH (NOLOCK) ON cd.CategoryId = c.Id
						JOIN Budget.RevenueType rt WITH (NOLOCK) ON cd.RevenueTypeId = rt.Id					
						WHERE od.Value > cd.Balance
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeMessageResult = 999, 
						@MessageResult = 'El valor del detalle de los siguientes rubros de la Orden de Pago no puede ser mayor que el saldo de la obligación: ' + CHAR(13) + CHAR(10) + ISNULL(@errors, ''), 
						@IdResult = 0, 
						@CodeResult = '' 
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
							od.CategoryId,
							SUM(d.InitialValue) Value
						FROM @Details d 
						JOIN Budget.ObligationDetail od WITH (NOLOCK) ON d.ObligationDetailId = od.Id
						WHERE d.ObligationDetailId IS NOT NULL
						GROUP BY od.CategoryId
					) od 
					LEFT JOIN Budget.AnnualizedCashFlow acf ON od.CategoryId = acf.CategoryId AND MONTH(@DocumentDate) = acf.Month
					WHERE od.Value > ISNULL(acf.Balance, 0)
				)
				BEGIN
					SELECT @errors = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ': Balance (' + FORMAT(ISNULL(acf.Balance, 0), 'C0', 'es-CO') + ') - Orden de Pago (' + FORMAT(od.Value, 'C0', 'es-CO') + ')'
							FROM 
							(
								SELECT 
									od.CategoryId,
									SUM(d.InitialValue) Value
								FROM @Details d 
								JOIN Budget.ObligationDetail od WITH (NOLOCK) ON d.ObligationDetailId = od.Id
								GROUP BY od.CategoryId
							) od 
							LEFT JOIN Budget.AnnualizedCashFlow acf ON od.CategoryId = acf.CategoryId AND MONTH(@DocumentDate) = acf.Month
							JOIN Budget.Category c WITH (NOLOCK) ON od.CategoryId = c.Id
							WHERE od.Value > ISNULL(acf.Balance, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeMessageResult = 999, 
							@MessageResult = 'El valor del detalle de los siguientes rubros de la Orden de Pago no puede ser mayor que el saldo del PAC del mes (' + CAST(MONTH(@DocumentDate) AS VARCHAR(20)) + '): ' + CHAR(13) + CHAR(10) + ISNULL(@errors, ''), 
							@IdResult = 0, 
							@CodeResult = '' 
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
					--Consultamos si la secuencia es con O o OU
					DECLARE @pattern VARCHAR(300),
							@NextS INT,
							@idSequenceDetail INT
				
					-- Consultamos la secuencia numerica del formulario
					SELECT @pattern = cs.Pattern, 
						@NextS = bsd.[Next], 
						@idSequenceDetail = bsd.Id  
					FROM Budget.BudgetSequenceDetail bsd 
					JOIN Budget.BudgetSequence bs ON bs.Id = bsd.IdSequenseBudgetC
					JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
					WHERE bs.IdForm = @IdForm 
						AND 
						(
							(bs.Scope = 'O')
							OR
							(bs.Scope <> 'O' AND bsd.IdOperatingUnit = @OperatingUnitId)
						)

					IF (@idSequenceDetail IS NULL)
					BEGIN
						SELECT @CodeMessageResult = 999, 
							   @MessageResult = 'Secuencia de Orden de Pago no encontrada', 
							   @IdResult = 0, 
							   @CodeResult = '' 
						RETURN
					END

					SELECT @Code = dbo.GetSequence('', @pattern, @NextS)
					UPDATE Budget.BudgetSequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail

					--Se inserta la cabecera
					INSERT INTO [Budget].[PaymentOrder]
					(
						[Code],[BudgetaryValidityId],[DocumentDate],[ThirdPartyId],[Document],[PaymentOrderType],[Observations],[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
						[EntityId],[EntityCode],[EntityName]
					)
					SELECT @Code,@BudgetaryValidityId,@DocumentDate,@ThirdPartyId,@Document,@PaymentOrderType,@Observations,@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
						@EntityId,@EntityCode,@EntityName

					--Obtengo el id de la cabcera
					SET @Id = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[PaymentOrder]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[ThirdPartyId] = @ThirdPartyId,						
						[Document] = @Document,
						[PaymentOrderType] = @PaymentOrderType,
						[Observations] = @Observations,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id
			END

			/*************************************************************************************/

			INSERT INTO Budget.PaymentOrderDetail 
				(
					PaymentOrderId, ObligationDetailId, ExpiredDate, InitialValue, DebitModificationValue, CreditModificationValue, TotalPaymentOrder, ExecutedValue, Balance
				)
				SELECT
					@Id PaymentOrderId,
					d.ObligationDetailId,
					d.ExpiredDate,
					d.InitialValue, 0, 0,
					d.InitialValue, 0, d.InitialValue
				FROM @Details d
				WHERE ISNULL(d.Id, 0) = 0

			UPDATE od
				SET od.ExpiredDate = d.ExpiredDate,
					od.InitialValue = d.InitialValue, 
					od.TotalPaymentOrder = d.InitialValue,
					od.Balance = d.InitialValue
			FROM Budget.PaymentOrderDetail od
			JOIN @Details d ON od.Id = d.Id

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
							od.CategoryId,
							SUM(d.InitialValue) Value
						FROM @Details d 
						JOIN Budget.ObligationDetail od WITH (NOLOCK) ON d.ObligationDetailId = od.Id
						GROUP BY od.CategoryId
					) od 
					JOIN Budget.AnnualizedCashFlow acf ON od.CategoryId = acf.CategoryId AND MONTH(@DocumentDate) = acf.Month
				END

				UPDATE cd
					SET
						cd.ExecutedValue = cd.ExecutedValue + d.InitialValue,
						cd.Balance = cd.Balance - d.InitialValue
				FROM @Details d
				JOIN Budget.ObligationDetail cd ON d.ObligationDetailId = cd.Id
			END
		END

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

		SELECT @CodeMessageResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la Orden de Pago con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la Orden de Pago con código ', @Code)
				   ELSE CONCAT('Se guardó la Orden de Pago con código ', @Code)
			   END, 
			   @IdResult = @Id, 
			   @CodeResult = @Code
		RETURN
	END TRY
	BEGIN CATCH
		SELECT @CodeMessageResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)), 
			   @IdResult = 0, 
			   @CodeResult = ''
	END CATCH
END