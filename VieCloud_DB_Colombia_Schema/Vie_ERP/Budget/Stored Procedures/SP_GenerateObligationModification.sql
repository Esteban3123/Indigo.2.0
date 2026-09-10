-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-07-26
-- Description:	Procedimiento que se encarga de generar la modificación de la obligación
-- =====================================================================================
CREATE PROCEDURE [Budget].[SP_GenerateObligationModification]
	@EntityId INT,
	@EntityCode VARCHAR(20),
	@EntityName VARCHAR(250),
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables a utilizar
	DECLARE @OperatingUnitId INT,
			------------------------------
			@DocumentDate DATETIME,
			@UpTo TINYINT,
			@Observations VARCHAR(MAX),
			@Nature TINYINT,
			@EntityFullName VARCHAR(250),
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		BudgetaryValidityId INT,
		ObligationId INT,
		ObligationDetailId INT,
		CommitmentDetailId INT,
		CategoryId INT,
		RevenueTypeId INT,
		AccountPayableId INT,
		ExpiredDate DATETIME,
		Value NUMERIC(18, 0)
	)

	BEGIN TRY
		--Se obtienen los datos dependiendo del documento de origen
		IF @EntityName = 'PaymentNotes'
		BEGIN
			SELECT TOP 1
				@OperatingUnitId = pn.IdOperatingUnit,
				@DocumentDate = pn.NoteDate,
				@UpTo = ISNULL(pnd.UpTo, 2), -- Hasta el compromiso
				@Observations = pn.Comment,
				@Nature = pn.Nature,
				@EntityFullName = 'Nota de cuenta por pagar'
			FROM Payments.PaymentNotes pn
			LEFT JOIN 
			(
				SELECT pnd.IdPaymentsNote, 4 UpTo	-- Hasta presupuesto
				FROM Payments.PaymentsNoteDetails pnd
				JOIN Payments.AccountPayableConceptNotes apcn ON pnd.IdAccountPayableConceptNotes = apcn.Id
				WHERE apcn.AffectBudget = 1 AND apcn.Behavior = 1
			) pnd ON pn.Id = pnd.IdPaymentsNote
			WHERE pn.Id = @EntityId

			INSERT INTO @Details
			(
				BudgetaryValidityId, ObligationId, ObligationDetailId, CommitmentDetailId, CategoryId, RevenueTypeId, AccountPayableId, ExpiredDate, Value
			)
			SELECT
				o.BudgetaryValidityId, o.Id, od.Id ObligationDetailId, od.CommitmentDetailId, od.CategoryId, od.RevenueTypeId, pnapa.AccountPayableId, od.ExpiredDate, SUM(pnapb.Value) Value
			FROM Payments.PaymentNotesAccountPayableAdvance pnapa 
			JOIN Payments.PaymentNoteAccountPayableBudget pnapb ON pnapa.Id = pnapb.PaymentNotesAccountPayableAdvanceId
			JOIN Budget.ObligationDetail od ON pnapb.ObligationDetailId = od.Id
			JOIN Budget.Obligation o ON od.ObligationId = o.Id
			WHERE pnapa.PaymentNoteId = @EntityId
			GROUP BY o.BudgetaryValidityId, o.Id, od.Id, od.CommitmentDetailId, od.CategoryId, od.RevenueTypeId, pnapa.AccountPayableId, od.ExpiredDate
		END

		--Si no esta activa la interfaz de presupuesto, retornamos
		IF NOT EXISTS (SELECT 1 FROM Payments.SettingPayments sp WITH (NOLOCK) WHERE sp.IdOperatingUnit = @OperatingUnitId AND sp.BudgetInterface = 1)
		BEGIN
			IF @EntityName = 'PaymentNotes'
			BEGIN
				DELETE pnapb 
				FROM Payments.PaymentNotesAccountPayableAdvance pnapa 
				JOIN Payments.PaymentNoteAccountPayableBudget pnapb ON pnapa.Id = pnapb.PaymentNotesAccountPayableAdvanceId
				WHERE pnapa.PaymentNoteId = @EntityId
			END

			SET @CodeResult = 0 
			SET @MessageResult = ''
			RETURN
		END

		--Si no hay detalles
		IF NOT EXISTS (SELECT 1 FROM @Details)
		BEGIN
			SET @CodeResult = 0 
			SET @MessageResult = ''
			RETURN
		END

		--Se valida que todos los detalles pertenezcan a la misma vigencia
		IF EXISTS
		(
			SELECT 1
			FROM
			(
				SELECT @EntityId Id, D.BudgetaryValidityId
				FROM @Details d
				GROUP BY d.BudgetaryValidityId
			) d 
			GROUP BY Id
			HAVING COUNT(*) > 1
		)
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Los detalles corresponden a más de una vigencia presupuestal.'
			RETURN
		END

		-------------------------------------------------------------------------------------------------------------------------------------------------------

		--se llama al procedimiento almacenado encargado de la crear la modificacion
		DECLARE @ObligationRows INT = 1,
				@ObligationId INT = 0,
				@BudgetaryValidityId INT,
				--------------------------------------
				@ConcatenatedMessage VARCHAR(MAX) = ''

		WHILE @ObligationRows > 0
		BEGIN
			SELECT TOP 1
				@ObligationId = d.ObligationId,
				@BudgetaryValidityId = d.BudgetaryValidityId
			FROM @Details d
			WHERE d.ObligationId > @ObligationId
			ORDER BY d.ObligationId

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
						ObligationModification.*,
						ObligationModificationDetail.*
					FROM
					(
						SELECT 
							0 Id,
							@OperatingUnitId OperatingUnitId,
							'' Code,
							@BudgetaryValidityId BudgetaryValidityId,
							@DocumentDate DocumentDate,
							@ObligationId ObligationId,
							@UpTo UpTo,
							@EntityCode Document,
							CONCAT(@Observations, CHAR(13) + CHAR(10) + 'Modificación de Obligación generada desde la ', @EntityFullName, ' No. ', @EntityCode) Observations,
							2 Status,
							@EntityId EntityId,
							@EntityCode EntityCode,
							@EntityName EntityName
					) ObligationModification
					JOIN
					( 
						SELECT
							0 ObligationModificationId,
							d.ObligationDetailId,
							d.CommitmentDetailId,
							d.CategoryId,
							d.RevenueTypeId,
							d.ExpiredDate,
							@Nature Nature,
							d.Value,
							0 IsLogBase
						FROM @Details d
						WHERE d.ObligationId = @ObligationId
					) ObligationModificationDetail ON ObligationModification.Id = ObligationModificationDetail.ObligationModificationId
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Budget].[SP_SaveObligationModification_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT @CodeResult = 999, 
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la modificación de la obligación presupuestal')
				RETURN
			END

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera automáticamente una modificación de obligación presupuestal a partir de documentos financieros de origen, principalmente notas de pago a cuentas por pagar (notas débito o crédito sobre proveedores). Consulta las notas de pago registradas en Payments.PaymentNotes y sus detalles de conceptos contables para determinar si el documento afecta el presupuesto, luego recopila las obligaciones y compromisos presupuestales asociados a través de PaymentNoteAccountPayableBudget y ObligationDetail. Valida que la unidad operativa tenga activa la interfaz presupuestal y que todos los detalles pertenezcan a la misma vigencia presupuestal, y finalmente construye un XML estructurado que pasa al procedimiento SP_SaveObligationModification_Output para registrar formalmente la modificación de la obligación en el módulo de presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateObligationModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateObligationModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera modificaciones a obligaciones presupuestales a partir de un documento origen (actualmente notas de pago), construyendo el XML por obligación y delegando su persistencia al SP de guardado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El documento origen debe existir; hoy solo está implementado el flujo cuando @EntityName = ''PaymentNotes''.; La unidad operativa del documento debe tener activa la interfaz de presupuesto en Payments.SettingPayments (BudgetInterface = 1); de lo contrario no se genera modificación.; Deben existir registros en Payments.PaymentNotesAccountPayableAdvance / PaymentNoteAccountPayableBudget asociados a la nota para poblar los detalles.; Todos los detalles deben pertenecer a una única vigencia presupuestal (BudgetaryValidityId).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se genera una modificación de obligación independiente por cada ObligationId distinto presente en los detalles.; El campo Status de la modificación se fija siempre en 2 y el Id en 0 (nuevo registro) en el XML.; Las observaciones de la modificación siempre incluyen el sufijo ''Modificación de Obligación generada desde la <EntidadFullName> No. <EntityCode>''.; El nivel de afectación (UpTo) por defecto es 2 (compromiso) y solo escala a 4 (presupuesto) si el concepto de la nota tiene AffectBudget=1 y Behavior=1.; Si la interfaz presupuestal está apagada, el procedimiento nunca genera modificaciones y además limpia los detalles presupuestales preexistentes de la nota.; Cualquier error capturado en el TRY/CATCH se devuelve como CodeResult=999 con ERROR_MESSAGE y línea.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Obligación presupuestal; Modificación de obligación; Vigencia presupuestal; Compromiso presupuestal; Nota de cuenta por pagar; Anticipo a cuenta por pagar; Concepto de nota (afectación presupuestal); Interfaz de presupuesto; Naturaleza (débito/crédito)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Payments.PaymentNoteAccountPayableBudget: Cuando @EntityName=''PaymentNotes'' y la unidad operativa NO tiene activa la interfaz presupuestal (SettingPayments.BudgetInterface<>1), se eliminan todos los registros de PaymentNoteAccountPayableBudget vinculados a los anticipos (PaymentNotesAccountPayableAdvance) de la nota.; [INSERT] Budget.ObligationModification: Por cada ObligationId distinto en los detalles se construye un XML con cabecera y detalles y se invoca Budget.SP_SaveObligationModification_Output, que materializa la modificación de la obligación con Status=2 y UpTo según el comportamiento del concepto.; [RETURN_RESULT] @CodeResult/@MessageResult: Devuelve 999 con mensaje ''Los detalles corresponden a más de una vigencia presupuestal.'' si se mezclan vigencias; 999 con el mensaje del SP llamado si éste falla; 0 y mensaje vacío/concatenado en éxito o cuando no aplica generar modificación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntityName = ''PaymentNotes'' → Carga cabecera (unidad operativa, fecha, observaciones, naturaleza) desde Payments.PaymentNotes y determina UpTo: 4 (hasta presupuesto) si algún detalle tiene un AccountPayableConceptNotes con AffectBudget=1 y Behavior=1; en caso contrario UpTo=2 (hasta el compromiso). Pobla @Details agregando valores por obligación/detalle. else No se procesan detalles para otros tipos de entidad.; si No existe SettingPayments con BudgetInterface=1 para la unidad operativa → Si es PaymentNotes, borra los PaymentNoteAccountPayableBudget de la nota; retorna CodeResult=0 sin generar modificación. else Continúa el proceso de generación.; si No hay filas en @Details → Retorna CodeResult=0 con mensaje vacío sin generar modificación.; si Los detalles tienen más de un BudgetaryValidityId → Retorna CodeResult=999 con mensaje ''Los detalles corresponden a más de una vigencia presupuestal.''; si Bucle WHILE recorriendo cada ObligationId distinto en @Details → Construye XML con la cabecera ObligationModification y sus ObligationModificationDetail, y ejecuta Budget.SP_SaveObligationModification_Output. else Sale del bucle cuando @@ROWCOUNT = 0.; si @Code_Output <> 0 tras llamar a SP_SaveObligationModification_Output → Retorna CodeResult=999 con el mensaje devuelto, o ''No se pudo generar la modificación de la obligación presupuestal'' si viene NULL. else Concatena el mensaje al acumulado y continúa con la siguiente obligación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveObligationModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.PaymentNotes; Payments.PaymentsNoteDetails; Payments.AccountPayableConceptNotes; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentNoteAccountPayableBudget; Budget.ObligationDetail; Budget.Obligation; Payments.SettingPayments', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateObligationModification';
-- GO
