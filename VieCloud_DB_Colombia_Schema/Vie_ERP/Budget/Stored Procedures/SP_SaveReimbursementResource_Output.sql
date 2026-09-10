-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-19
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un reembolso (Los reembolsos son de naturaleza debito)
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveReimbursementResource_Output]
    @ReimbursementResourceXml AS XML,
	@ReimbursementResourceDetailForDeleteXml AS XML,
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
			@BudgetaryValidityId INT,
			@DocumentDate DATETIME,
			@PaymentOrderId INT,
			@UpTo TINYINT,
			@Document VARCHAR(100),
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			------------------------------
			@IdForm INT = 238,
			@DocumentTypeControl INT = 19,
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
		PaymentOrderDetailId INT NOT NULL,
		Value DECIMAL(18,2)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),			
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@PaymentOrderId = t.x.value('PaymentOrderId[1]','int'),
			@UpTo = t.x.value('UpTo[1]','tinyint'),
			@Document = t.x.value('Document[1]','varchar(100)'),
			@Observations = t.x.value('Observations[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)')
		FROM @ReimbursementResourceXml.nodes('/ReimbursementResource') t(x)

		IF EXISTS (SELECT 1 FROM Budget.ReimbursementResource om WHERE om.Id = @Id AND om.Status <> 1)
		BEGIN
			SELECT @CodeResult = 999, 
				   @MessageResult = 'El Reintegro se encuentra en estado: ' + IIF(om.Status = 2, 'Confirmado', 'Anulado')
			FROM Budget.ReimbursementResource om 
			WHERE om.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Budget].[ReimbursementResource]
				SET [Status] = @Status,
					[ModificationUser] = @CodeUser,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @CodeUser,
					[AnnulmentDate] = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			--Eliminamos los detalles indicados
			DELETE rrd
			FROM @ReimbursementResourceDetailForDeleteXml.nodes('/ReimbursementResourceDetail') t(x)
			JOIN Budget.ReimbursementResourceDetaill rrd ON t.x.value('Id[1]','int') = rrd.Id
			WHERE rrd.ReimbursementResourceId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('PaymentOrderDetailId[1]','int'),
					t.x.value('Value[1]','decimal(18,2)')
				FROM @ReimbursementResourceXml.nodes('/ReimbursementResource/ReimbursementResourceDetail') t(x)

			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					rrd.Id, 
					rrd.PaymentOrderDetailId, 
					rrd.Value
				FROM Budget.ReimbursementResourceDetaill rrd
				LEFT JOIN @Details d ON rrd.Id = d.Id
				WHERE rrd.ReimbursementResourceId = @Id AND ISNULL(d.Id, 0) = 0

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
				SELECT @Message = 'La Fecha del Reintegro (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.ExpenseMonth AS VARCHAR), 2)) + ').'
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
					   @MessageResult = 'El Reintegro no tiene detalles.'
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.ReimbursementResourceDetaill rrd JOIN @Details d ON rrd.Id = d.Id WHERE rrd.ReimbursementResourceId <> @Id OR rrd.PaymentOrderDetailId <> d.PaymentOrderDetailId) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles del Reintegro han sido alterados.'
				RETURN
			END

			--- Valido que las categorias existan
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				JOIN Budget.PaymentOrderDetail pod ON d.PaymentOrderDetailId = pod.Id 
				JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id 
				LEFT JOIN Budget.Category c ON od.CategoryId = c.Id AND @BudgetaryValidityId = c.BudgetaryValidityId 
				WHERE c.Id IS NULL
			)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen rubros que no existen o no pertenecen a la vigencia del Reintegro.'
				RETURN
			END

			--- Valido que los tipos existan
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				JOIN Budget.PaymentOrderDetail pod ON d.PaymentOrderDetailId = pod.Id 
				JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id 
				LEFT JOIN Budget.RevenueType rt ON od.RevenueTypeId = rt.Id AND @BudgetaryValidityId = rt.BudgetaryValidityId 
				WHERE rt.Id IS NULL
			)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles poseen tipos que no existen o no pertenecen a la vigencia del Reintegro.'
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.PaymentOrderDetailId HAVING COUNT(*) > 1)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El Reintegro tiene detalles duplicados.'
				RETURN
			END

			--- Valido que no existan detalles con valores en 0 o negativos
			IF EXISTS (SELECT 1 FROM @Details d WHERE d.Value <= 0)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Existen Rubros del Reintegro con el valor menor o igual a 0.'
				RETURN
			END

			--- Valido hasta donde se liberarán recursos
			IF @UpTo NOT IN (1, 2, 3, 4)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El nivel del Reintegro hasta la que se liberaran recursos no es valido.'
				RETURN
			END

			--- Valido detalles debitos
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d 
				LEFT JOIN Budget.PaymentOrderDetail pod WITH (NOLOCK) ON d.PaymentOrderDetailId = pod.Id
				WHERE d.Value > ISNULL(pod.Balance, 0)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + IIF(fs.Id IS NULL, '', ', Recurso ' + fs.Code) + ', tipo ' + rt.Code + ': Balance (' + FORMAT(ISNULL(pod.Balance, 0), 'C0', 'es-CO') + ') - Modificación(' + FORMAT(d.Value, 'C0', 'es-CO') + ')'
						FROM @Details d
						JOIN Budget.PaymentOrderDetail pod WITH (NOLOCK) ON d.PaymentOrderDetailId = pod.Id
						JOIN Budget.ObligationDetail od WITH (NOLOCK) ON pod.ObligationDetailId = od.Id
						JOIN Budget.Category c WITH (NOLOCK) ON od.CategoryId = c.Id
						JOIN Budget.RevenueType rt WITH (NOLOCK) ON od.RevenueTypeId = rt.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						WHERE d.Value > ISNULL(pod.Balance, 0)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'El valor de los siguientes rubros del Reintegro no pueden ser mayor que el saldo de la orden de pago: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @CodeUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN --Si se esta insertando por primera vez se consulta la secuencia numerica
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 200, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Reintegro')
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[ReimbursementResource]
				(
					[Code],[BudgetaryValidityId],[DocumentDate],[PaymentOrderId],[UpTo],[Document],[Observations],[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate]
				)
				SELECT @Code,@BudgetaryValidityId,@DocumentDate,@PaymentOrderId,@UpTo,@Document,@Observations,@Status,@CodeUser,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[ReimbursementResource]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[PaymentOrderId] = @PaymentOrderId,
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

			UPDATE rrd
				SET rrd.Value = d.Value
			FROM Budget.ReimbursementResourceDetaill rrd
			JOIN @Details d ON rrd.Id = d.Id

			INSERT INTO Budget.ReimbursementResourceDetaill (ReimbursementResourceId, PaymentOrderDetailId, Value)
				SELECT	@Id ReimbursementResourceId,
						d.PaymentOrderDetailId,
						d.Value
				FROM @Details d
				WHERE ISNULL(d.Id, 0) = 0

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
							od.CategoryId,
							SUM(d.Value) Value
						FROM @Details d
						JOIN Budget.PaymentOrderDetail pod ON d.PaymentOrderDetailId = pod.Id
						JOIN Budget.ObligationDetail od WITH (NOLOCK) ON pod.ObligationDetailId = od.Id
						GROUP BY od.CategoryId
					) od 
					JOIN Budget.AnnualizedCashFlow acf ON od.CategoryId = acf.CategoryId AND MONTH(@DocumentDate) = acf.Month
				END

				UPDATE pod
					SET pod.DebitModificationValue = pod.DebitModificationValue + d.Value,
						pod.TotalPaymentOrder = pod.TotalPaymentOrder - d.Value,
						pod.Balance = pod.Balance - d.Value
				FROM @Details d
				JOIN Budget.PaymentOrderDetail pod ON d.PaymentOrderDetailId = pod.Id

				UPDATE od
					SET od.ExecutedValue = od.ExecutedValue - rrd.Value,
						od.Balance = od.Balance + rrd.Value
				FROM Budget.ReimbursementResourceDetaill rrd
				JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
				JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
				WHERE rrd.ReimbursementResourceId = @Id
			
				IF @UpTo > 1
				BEGIN
					--Si afecta el presupuesto, se llama al procedimiento almacenado encargado de la modificación de presupuesto con el fin de liberar recursos
					DECLARE @ObligationRows INT = 1,
							@ObligationId INT = 0

					WHILE @ObligationRows > 0
					BEGIN
						SELECT TOP 1
							@ObligationId = cd.ObligationId
						FROM Budget.ReimbursementResourceDetaill rrd
						JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
						JOIN Budget.ObligationDetail cd ON pod.ObligationDetailId = cd.Id
						WHERE rrd.ReimbursementResourceId = @Id
							AND cd.ObligationId > @ObligationId
						ORDER BY cd.ObligationId

						SET @ObligationRows = @@ROWCOUNT
						IF @ObligationRows = 0 
						BEGIN
							BREAK
						END
						
						SELECT @SubXml = CONVERT
						(
							XML, 
							(
								SELECT 
									0 Id,
									@OperatingUnitId OperatingUnitId,
									'' Code,
									ObligationModification.BudgetaryValidityId,
									ObligationModification.DocumentDate,
									@ObligationId ObligationId,
									ObligationModification.UpTo,
									ObligationModification.Document,
									ObligationModification.Observations,
									ObligationModification.Status,
									@Id EntityId,
									@Code EntityCode,
									'ReimbursementResource' EntityName,
									ObligationModificationDetail.*
								FROM Budget.ReimbursementResource ObligationModification
								JOIN
								( 
									SELECT
										rrd.ReimbursementResourceId,
										od.Id ObligationDetailId,
										od.CommitmentDetailId,
										od.CategoryId,
										od.RevenueTypeId,
										od.ExpiredDate,
										1 Nature,
										rrd.Value,
										0 IsLogBase
									FROM Budget.ReimbursementResourceDetaill rrd
									JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
									JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
									WHERE rrd.ReimbursementResourceId = @Id
										AND od.ObligationId = @ObligationId
								) ObligationModificationDetail ON ObligationModification.Id = ObligationModificationDetail.ReimbursementResourceId
								WHERE ObligationModification.Id = @Id
								For xml AUTO,TYPE, ELEMENTS
							)
						)

						EXEC [Budget].[SP_SaveObligationModification_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, @AuxiliaryResult OUT, NULL, NULL

						IF @Code_Output <> 0
						BEGIN
							SELECT @CodeResult = 999, 
									@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la modificación de la obligación')
							RETURN
						END

						SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
					END
				END

				IF NOT EXISTS (SELECT 1 FROM Budget.PaymentOrderDetail WHERE PaymentOrderId = @PaymentOrderId AND Balance > 0)
				BEGIN
					UPDATE po
						SET po.Status = 4
					FROM Budget.PaymentOrder po
					WHERE po.Id = @PaymentOrderId
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
				   WHEN 2 THEN CONCAT('Se guardó y confirmó el Reintegro con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló el Reintegro con código ', @Code)
				   ELSE CONCAT('Se guardó el Reintegro con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, ''))
		RETURN
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite guardar, actualizar, confirmar o anular un reembolso (reintegro) presupuestal de naturaleza débito en el módulo de presupuesto. Recibe la cabecera y el detalle del reintegro en formato XML, junto con la lista de detalles a eliminar, y aplica validaciones de negocio como coherencia con la vigencia presupuestal, existencia de rubros (categorías y tipos), integridad de los detalles de la orden de pago y límites de valores antes de persistir los cambios. Gestiona los tres estados posibles del reintegro: borrador/edición (estado 1), confirmado (estado 2) y anulado (estado 3), registrando en cada caso el usuario y la fecha de modificación o anulación. Trabaja sobre las tablas Budget.ReimbursementResource (cabecera del reintegro) y Budget.ReimbursementResourceDetaill (líneas de detalle vinculadas a órdenes de pago), y valida la vigencia presupuestal contra Budget.BudgetaryValidity.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveReimbursementResource_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveReimbursementResource_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, actualiza, confirma o anula un reintegro presupuestal (movimiento débito sobre una orden de pago), validando vigencia, rubros y saldos, y propagando la liberación de recursos a obligación, PAC y modificaciones presupuestales superiores cuando se confirma.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El reintegro a editar no debe estar en Status Confirmado (2) ni Anulado (3); solo se permite continuar si Status = 1 (borrador) o si es nuevo; La vigencia presupuestal indicada debe existir y cubrir el año y mes (ExpenseMonth ≤ MONTH(DocumentDate)) de la fecha del documento; Debe existir al menos un detalle (sumando los del XML y los previamente persistidos no eliminados); Los detalles editados deben conservar su ReimbursementResourceId y PaymentOrderDetailId originales; Cada PaymentOrderDetail referenciado debe estar vinculado a una Category y a un RevenueType vigentes en la BudgetaryValidity; No pueden existir detalles duplicados por PaymentOrderDetailId; Todos los valores de detalle deben ser > 0; UpTo debe pertenecer a {1,2,3,4}; El valor de cada detalle no puede superar el Balance actual del PaymentOrderDetail correspondiente', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un reintegro en estado Confirmado (2) o Anulado (3) no puede volver a editarse: se rechaza con código 999; El reintegro es de naturaleza débito: nunca permite valores ≤ 0 en sus detalles; El valor de cada detalle no puede exceder el Balance del PaymentOrderDetail asociado; La fecha del reintegro debe estar dentro de la vigencia presupuestal (mismo año y mes ≥ ExpenseMonth de la vigencia); No se permiten detalles duplicados por PaymentOrderDetailId; No se puede alterar el PaymentOrderDetailId ni el ReimbursementResourceId de detalles previamente persistidos; Los rubros (Category) y tipos (RevenueType) referenciados deben pertenecer a la misma BudgetaryValidity del reintegro; UpTo solo admite valores 1, 2, 3 o 4; Solo se afectan saldos presupuestales (PaymentOrderDetail, ObligationDetail, AnnualizedCashFlow) cuando Status = 2; Solo se invoca SP_SaveObligationModification_Output cuando UpTo > 1 y Status = 2; ConfirmationUser/ConfirmationDate solo se llenan cuando Status = 2; AnnulmentUser/Date solo cuando Status = 3; El control presupuestario (BudgetControl) sólo existe mientras el reintegro esté en estado 1 (borrador); Cualquier excepción se captura y devuelve CodeResult=999 con el mensaje y línea del error', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reintegro/Reembolso presupuestal; Vigencia presupuestal; Orden de pago; Obligación presupuestal; Rubro/Categoría presupuestal; Tipo de ingreso (RevenueType); Fuente de financiación; PAC (Programa Anual de Caja) y flujo de caja anualizado; Modificación de obligación; Control presupuestario (BudgetControl); Secuencia numérica de documentos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status = 3 (anulación) → Solo actualiza ReimbursementResource fijando Status, AnnulmentUser/Date y ModificationUser/Date; no toca detalles ni saldos; si Status <> 3 (guardar/confirmar) y Id = 0 → Solicita secuencia vía Common.SP_GetSequence (tipo 200, formulario 238) e inserta cabecera nueva en ReimbursementResource else Si Id <> 0, actualiza la cabecera existente; si Status = 2 (confirmado) → Reversa saldos: actualiza PaymentOrderDetail (DebitModificationValue, TotalPaymentOrder, Balance), ObligationDetail (ExecutedValue, Balance) y, si BudgetaryValidity.PACControl=1, AnnualizedCashFlow (ExecutedValue, Balance) del mes del documento; si Status = 2 y UpTo > 1 → Itera por cada ObligationId afectado y llama a Budget.SP_SaveObligationModification_Output para liberar recursos hacia niveles superiores (compromiso/disponibilidad); si Status = 2 y no quedan PaymentOrderDetail con Balance > 0 para la orden de pago → Marca Budget.PaymentOrder.Status = 4 (orden de pago pagada/cerrada); si Status = 1 (borrador) y no existe registro en BudgetControl con DocumentType=19 y DocumentNumber=@Code → Inserta fila de control en Budget.BudgetControl else Si Status <> 1, elimina la fila de Budget.BudgetControl correspondiente', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Budget.SP_SaveObligationModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.ReimbursementResource; Budget.ReimbursementResourceDetaill; Budget.BudgetaryValidity; Budget.PaymentOrderDetail; Budget.ObligationDetail; Budget.Category; Budget.RevenueType; Budget.FinancialSource; Budget.AnnualizedCashFlow; Budget.PaymentOrder; Budget.BudgetControl', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource_Output';
-- GO
