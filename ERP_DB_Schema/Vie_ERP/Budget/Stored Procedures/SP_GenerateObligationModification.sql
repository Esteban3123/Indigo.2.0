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