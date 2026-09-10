-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-22
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una obligacion
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveObligation_Output]
    @ObligationXml AS XML,
	@ObligationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
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
			@ThirdPartyId INT,
			@Document VARCHAR(100),			
			@ObligationType TINYINT,
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@AnnulmentConceptId INT,
			@AnnulmentDescription VARCHAR(MAX),
			@AutomaticPaymentOrder BIT, 
			------------------------------
			@IdForm INT = 235,
			@DocumentType INT = 14,
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
		ObligationId INT,
		CommitmentDetailId INT,
		CategoryId INT,
		RevenueTypeId INT,
		ExpiredDate DATETIME,
		InitialValue DECIMAL(18,2),
		EntityId INT,
		EntityCode VARCHAR(20),
		EntityName VARCHAR(250)
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
			@ObligationType = t.x.value('ObligationType[1]','tinyint'),
			@Observations = t.x.value('Observations[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@AnnulmentConceptId = t.x.value('AnnulmentConceptId[1]','int'),
			@AnnulmentDescription = t.x.value('AnnulmentDescription[1]','varchar(max)'),
			@AutomaticPaymentOrder = ISNULL(t.x.value('AutomaticPaymentOrder[1]','BIT'), 0)
		FROM @ObligationXml.nodes('/Obligation') t(x)

		IF EXISTS (SELECT 1 FROM Budget.Obligation om WHERE om.Id = @Id AND om.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'La Obligación se encuentra en estado: ' + IIF(om.Status = 2, 'Confirmado', 'Anulado')
			FROM Budget.Obligation om 
			WHERE om.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Budget].[Obligation]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE](),
					[AnnulmentConceptId] = @AnnulmentConceptId,
					[AnnulmentDescription] = @AnnulmentDescription
			WHERE Id = @Id

			UPDATE [Budget].[ObligationDetail]
				SET [DebitModificationValue] = [InitialValue],
					[TotalObligation] = 0,
					[Balance] = 0
			WHERE ObligationId = @Id
		END
		ELSE
		BEGIN
			--Eliminamos los detalles indicados
			DELETE od
			FROM @ObligationDetailForDeleteXml.nodes('/ObligationDetail') t(x)
			JOIN Budget.ObligationDetail od ON t.x.value('Id[1]','int') = od.Id
			WHERE od.ObligationId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('ObligationId[1]','int'),
					t.x.value('CommitmentDetailId[1]','int'),
					t.x.value('CategoryId[1]','int'),
					t.x.value('RevenueTypeId[1]','int'),
					t.x.value('ExpiredDate[1]','datetime'),
					t.x.value('InitialValue[1]','decimal(18,2)'),
					t.x.value('EntityId[1]','int'),
					t.x.value('EntityCode[1]','varchar(20)'),
					t.x.value('EntityName[1]','varchar(250)')
				FROM @ObligationXml.nodes('/Obligation/ObligationDetail') t(x)

			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					od.Id, 
					od.ObligationId, 
					od.CommitmentDetailId,
					od.CategoryId,
					od.RevenueTypeId,
					od.ExpiredDate,
					od.InitialValue,
					od.EntityId,
					od.EntityCode,
					od.EntityName
				FROM Budget.ObligationDetail od
				LEFT JOIN @Details d ON od.Id = d.Id
				WHERE od.ObligationId = @Id AND ISNULL(d.Id, 0) = 0

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
				SELECT @Message = 'La Fecha de la Obligación (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.ExpenseMonth AS VARCHAR), 2)) + ').'
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
					   @MessageResult = 'La Obligación no tiene detalles.'
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.ObligationDetail od JOIN @Details d ON od.Id = d.Id WHERE od.ObligationId <> @Id OR ISNULL(od.CommitmentDetailId, 0) <> ISNULL(d.CommitmentDetailId, 0) OR od.CategoryId <> d.CategoryId OR od.RevenueTypeId <> d.RevenueTypeId) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles de la Obligación han sido alterados.'
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.CommitmentDetailId HAVING COUNT(*) > 1)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'La Obligación tiene detalles duplicados.'
				RETURN
			END

			--- Valido el tipo de la obligación
			IF @ObligationType = 1
			BEGIN
				--- Si los detalles afectan un compromiso
				IF EXISTS (SELECT 1 FROM @Details d WHERE d.CommitmentDetailId IS NULL)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'La Obligación tiene detalles sin compromiso.'
					RETURN
				END

				IF EXISTS 
				(
					SELECT 1 
					FROM 
					(
						SELECT 
							d.CommitmentDetailId,
							SUM(d.InitialValue) Value
						FROM @Details d 
						WHERE d.CommitmentDetailId IS NOT NULL
						GROUP BY d.CommitmentDetailId
					) od 
					JOIN Budget.CommitmentDetail cd WITH (NOLOCK) ON od.CommitmentDetailId = cd.Id
					WHERE od.Value > cd.Balance
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
							FROM 
							(
								SELECT 
									d.CommitmentDetailId,
									SUM(d.InitialValue) Value
								FROM @Details d 
								WHERE d.CommitmentDetailId IS NOT NULL
								GROUP BY d.CommitmentDetailId
							) od 
							JOIN Budget.CommitmentDetail cd WITH (NOLOCK) ON od.CommitmentDetailId = cd.Id
							JOIN Budget.Category c WITH (NOLOCK) ON cd.CategoryId = c.Id
							JOIN Budget.RevenueType rt WITH (NOLOCK) ON cd.RevenueTypeId = rt.Id					
							WHERE od.Value > cd.Balance
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = 'El valor del detalle de los siguientes rubros de la Obligación no pueden ser mayor que el saldo del compromiso: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END
			END
			ELSE
			BEGIN
				--- Si los detalles afectan directamente el presupuesto
				IF EXISTS (SELECT 1 FROM @Details d WHERE d.CommitmentDetailId IS NOT NULL)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = 'La Obligación tiene detalles asociadas a un compromiso.'
					RETURN
				END

				IF EXISTS 
				(
					SELECT 1 
					FROM 
					(
						SELECT 
							d.CategoryId,
							d.RevenueTypeId,
							SUM(d.InitialValue) Value
						FROM @Details d
						WHERE d.CommitmentDetailId IS NULL
						GROUP BY d.CategoryId, d.RevenueTypeId
					) od
					LEFT JOIN Budget.Budget b WITH (NOLOCK) ON od.CategoryId = b.CategoryId AND od.RevenueTypeId = b.RevenueTypeId
					WHERE od.Value > ISNULL(b.Balance, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
							FROM 
							(
								SELECT 
									d.CategoryId,
									d.RevenueTypeId,
									SUM(d.InitialValue) Value
								FROM @Details d
								WHERE d.CommitmentDetailId IS NULL
								GROUP BY d.CategoryId, d.RevenueTypeId
							) od
							JOIN Budget.Category c WITH (NOLOCK) ON od.CategoryId = c.Id
							JOIN Budget.RevenueType rt WITH (NOLOCK) ON od.RevenueTypeId = rt.Id
							LEFT JOIN Budget.Budget b WITH (NOLOCK) ON od.CategoryId = b.CategoryId AND od.RevenueTypeId = b.RevenueTypeId
							WHERE od.Value > ISNULL(b.Balance, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT @CodeResult = 999, 
						   @MessageResult = 'El valor del detalle de los siguientes rubros de la Obligación no pueden ser mayor que el saldo del presupuesto: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
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
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Obligación')
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[Obligation]
				(
					[Code],[BudgetaryValidityId],[DocumentDate],[ThirdPartyId],[Document],[ObligationType],[Observations],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[AutomaticPaymentOrder]
				)
				SELECT @Code,@BudgetaryValidityId,@DocumentDate,@ThirdPartyId,@Document,@ObligationType,@Observations,
					@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@AutomaticPaymentOrder

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[Obligation]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[ThirdPartyId] = @ThirdPartyId,						
						[Document] = @Document,
						[ObligationType] = @ObligationType,
						[Observations] = @Observations,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate,
						[AutomaticPaymentOrder] = @AutomaticPaymentOrder
				WHERE Id = @Id
			END

			/*************************************************************************************/

			INSERT INTO Budget.ObligationDetail 
				(
					ObligationId, CommitmentDetailId, CategoryId, RevenueTypeId, ExpiredDate, InitialValue, DebitModificationValue, CreditModificationValue, TotalObligation, ExecutedValue, Balance,
					EntityId, EntityCode, EntityName
				)
				SELECT
					@Id ObligationId,
					d.CommitmentDetailId,
					d.CategoryId,
					d.RevenueTypeId,
					d.ExpiredDate,
					d.InitialValue, 0, 0,
					d.InitialValue, 0, d.InitialValue,
					d.EntityId, d.EntityCode, d.EntityName
				FROM @Details d
				WHERE ISNULL(d.Id, 0) = 0

			UPDATE od
				SET od.ExpiredDate = d.ExpiredDate,
					od.InitialValue = d.InitialValue, 
					od.TotalObligation = d.InitialValue,
					od.Balance = d.InitialValue
			FROM Budget.ObligationDetail od
			JOIN @Details d ON od.Id = d.Id

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				IF @ObligationType = 1
				BEGIN
					UPDATE cd
						SET
							cd.ExecutedValue = cd.ExecutedValue + d.InitialValue,
							cd.Balance = cd.Balance - d.InitialValue
					FROM @Details d
					JOIN Budget.CommitmentDetail cd ON d.CommitmentDetailId = cd.Id
				END
				ELSE
				BEGIN
					UPDATE b
						SET
							b.ExecutedValue = b.ExecutedValue + d.InitialValue,
							b.Balance = b.Balance - d.InitialValue,
							b.ModificationUser = @CodeUser,
							b.ModificationDate = [Common].[GETDATE]()
					FROM @Details d
					JOIN Budget.Budget b ON d.CategoryId = b.CategoryId AND d.RevenueTypeId = b.RevenueTypeId
					WHERE d.CommitmentDetailId IS NULL
				END

				IF @AutomaticPaymentOrder = 1
				BEGIN
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT 
								PaymentOrder.*,
								PaymentOrderDetail.*
							FROM
							(
								SELECT 
									0 Id,
									@OperatingUnitId OperatingUnitId,
									'' Code,
									@BudgetaryValidityId BudgetaryValidityId,
									o.DocumentDate,
									o.ThirdPartyId,
									o.Document,
									1 PaymentOrderType,
									CONCAT('Orden de Pago automática: ', REPLACE(o.Observations, 'Obligación automática: ', '')) Observations,
									2 Status,
									@Id EntityId,
									@Code EntityCode,
									'Obligation' EntityName
								FROM Budget.Obligation o
								WHERE o.Id = @Id
							) PaymentOrder
							JOIN
							( 
								SELECT
									0 PaymentOrderId,
									od.Id ObligationDetailId,
									od.ExpiredDate,
									od.InitialValue
								FROM Budget.ObligationDetail od
								WHERE od.ObligationId = @Id
							) PaymentOrderDetail ON PaymentOrder.Id = PaymentOrderDetail.PaymentOrderId
							For xml AUTO,TYPE, ELEMENTS
						)
					)

					EXEC [Budget].[SP_SavePaymentOrder_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

					IF @Code_Output <> 0
					BEGIN
						SELECT @CodeResult = 999, 
								@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la orden de pago presupuestal')
						RETURN
					END

					SET @Message = ISNULL(@Message, '') + CHAR(13) + CHAR(10) + ISNULL(@Message_Output, 'Orden de Pago generada')
				END
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

		SELECT @CodeResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la obligacion con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la obligacion con código ', @Code)
				   ELSE CONCAT('Se guardó la obligacion con código ', @Code)
			   END + ISNULL(@Message, '')
		RETURN
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el ciclo de vida completo de una obligación presupuestaria: permite crearla, actualizarla, confirmarla y anularla. Recibe los datos de cabecera y detalle en formato XML (incluyendo unidad operativa, vigencia presupuestal, tercero, tipo de obligación y sus ítems de detalle), ejecuta validaciones de negocio como coherencia entre fecha del documento y vigencia presupuestal, integridad de los detalles y disponibilidad de saldo en compromisos, y luego persiste los cambios en las tablas Budget.Obligation y Budget.ObligationDetail. En caso de anulación (estado 3), registra el usuario, fecha, concepto y descripción de la anulación, y reversa los valores de los detalles a saldo cero; en caso de guardado o confirmación, sincroniza los detalles nuevos y existentes actualizando saldos y valores totales de la obligación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveObligation_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveObligation_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@ObligationXml debe contener un nodo /Obligation con los campos de cabecera y nodos /Obligation/ObligationDetail con los detalles.; Si @Id > 0 debe existir el registro en Budget.Obligation y estar en Status=1 (borrador) para ser modificable.; Debe existir una Budget.BudgetaryValidity con Id=@BudgetaryValidityId cuyo Year coincida con YEAR(@DocumentDate) y ExpenseMonth <= MONTH(@DocumentDate).; Para @ObligationType=1 cada detalle debe tener CommitmentDetailId no nulo y referenciar un Budget.CommitmentDetail existente; para otros tipos, todos los detalles deben tener CommitmentDetailId nulo.; Las sumatorias de InitialValue por compromiso (tipo 1) o por (CategoryId, RevenueTypeId) (otros) no deben exceder el Balance disponible.; No deben existir detalles duplicados por CommitmentDetailId.; Los detalles editados no pueden alterar ObligationId, CommitmentDetailId, CategoryId ni RevenueTypeId respecto a los ya persistidos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una obligación solo es modificable mientras Status = 1 (borrador); en estado Confirmado (2) o Anulado (3) se rechaza cualquier cambio.; La fecha del documento debe pertenecer a una vigencia presupuestal abierta (mismo año y con ExpenseMonth <= mes de DocumentDate).; Tipo 1 (con compromiso): todo detalle debe tener CommitmentDetailId; tipo distinto: ningún detalle puede tener CommitmentDetailId.; La suma de InitialValue por compromiso no puede exceder el Balance del CommitmentDetail; cuando no hay compromiso, no puede exceder el Balance del rubro en Budget.Budget.; No se permiten detalles duplicados por CommitmentDetailId dentro de una misma obligación.; Los campos base de un detalle existente (ObligationId, CommitmentDetailId, CategoryId, RevenueTypeId) son inmutables; solo pueden cambiar ExpiredDate, InitialValue, TotalObligation y Balance.; Al confirmar, ExecutedValue y Balance de CommitmentDetail (tipo 1) o de Budget (otros) se ajustan exactamente con InitialValue del detalle.; Al anular, los saldos del detalle quedan en 0 (TotalObligation=0, Balance=0) y DebitModificationValue queda igual a InitialValue.; BudgetControl mantiene un registro mientras la obligación esté en borrador (Status=1) y se elimina cuando pasa a otro estado.; Si AutomaticPaymentOrder=1 y se confirma, se genera automáticamente una Orden de Pago (PaymentOrderType=1, Status=2) ligada a la obligación como entidad origen.; El número (@Code) se asigna mediante Common.SP_GetSequence solo en altas nuevas (Id=0).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe Obligación con Status <> 1 (no en borrador) → Retorna error 999 indicando que está Confirmada (Status=2) o Anulada (Status=3) y aborta; si @Status = 3 (anulación) → Actualiza cabecera con datos de anulación (AnnulmentUser/Date/Concept/Description) y resetea detalles: DebitModificationValue=InitialValue, TotalObligation=0, Balance=0 else Procesa guardado/confirmación con validaciones, sincronización de detalles y eventual generación de orden de pago; si @ObligationType = 1 (afecta compromiso) → Exige CommitmentDetailId no nulo en todos los detalles y valida que la suma por compromiso no exceda CommitmentDetail.Balance else Exige CommitmentDetailId nulo en todos los detalles y valida que la suma por (Category, RevenueType) no exceda Budget.Balance; si @Id = 0 (alta nueva) → Llama Common.SP_GetSequence (tipo 200, IdForm=235) para obtener @Code e inserta cabecera en Budget.Obligation; SCOPE_IDENTITY() asigna @Id else Actualiza la cabecera existente en Budget.Obligation; si @Status = 2 (confirmación) → Setea ConfirmationUser/Date; si ObligationType=1 descuenta InitialValue del Balance y suma a ExecutedValue de CommitmentDetail; si no, lo hace sobre Budget.Budget por (CategoryId, RevenueTypeId); si @Status = 2 AND @AutomaticPaymentOrder = 1 → Construye XML de PaymentOrder/PaymentOrderDetail a partir de la obligación y ejecuta Budget.SP_SavePaymentOrder_Output; si retorna código distinto de 0 aborta con error; si @Status = 1 (borrador) y no existe registro en BudgetControl para (DocumentType=14, @Code) → Inserta registro de control en Budget.BudgetControl else Si @Status <> 1, elimina el registro de BudgetControl para (DocumentType=14, @Code); si Vigencia presupuestal no encontrada o no coincide con año/mes de @DocumentDate (bv.Year=YEAR(DocumentDate) y bv.ExpenseMonth<=MONTH(DocumentDate)) → Retorna error 999 con mensaje sobre fecha vs vigencia y aborta; si No hay detalles en @Details → Retorna error 999 ''La Obligación no tiene detalles''; si Existe detalle editado cuyos campos base (ObligationId, CommitmentDetailId, CategoryId, RevenueTypeId) difieren de los persistidos → Retorna error 999 ''Los detalles de la Obligación han sido alterados''; si Hay detalles con CommitmentDetailId duplicado → Retorna error 999 ''La Obligación tiene detalles duplicados''', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Budget.SP_SavePaymentOrder_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Obligation; Budget.ObligationDetail; Budget.BudgetaryValidity; Budget.CommitmentDetail; Budget.Category; Budget.RevenueType; Budget.Budget; Budget.BudgetControl', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation_Output';
-- GO
