-- =====================================================================================
-- Author: Carlos Mario Arias Rubiano
-- Create date: 2019-06-18
-- Description:	Procedimiento que se encarga de generar las obligaciones desde la CxP
-- =====================================================================================
CREATE PROCEDURE [Budget].[SP_GenerateObligationsByAccountPayable]
	@AccountPayableCode VARCHAR(20),
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @OperatingUnitId INT,
			@BudgetaryValidityId INT,
			------------------------------
			@AccountPayableRows INT = 1,			
			@AccountPayableId INT = 0,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT TOP 1
			@OperatingUnitId = ap.IdOperatingUnit,
			@BudgetaryValidityId = c.BudgetaryValidityId
		FROM Payments.AccountPayable ap
		JOIN Payments.AccountPayableCommitments apc ON ap.Id = apc.AccountPayableId
		JOIN Budget.CommitmentDetail cd ON apc.CommitmentDetailId = cd.Id
		JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
		WHERE ap.Code = @AccountPayableCode

		--Si no esta activa la interfaz de presupuesto, retornamos
		IF NOT EXISTS (SELECT 1 FROM Payments.SettingPayments sp WHERE sp.IdOperatingUnit = @OperatingUnitId AND sp.BudgetInterface = 1)
		BEGIN
			SET @CodeResult = 0 
			SET @MessageResult = ''
			RETURN
		END

		--Se valida que existan detalles con compromiso y si la interfaz es obligatoria o no
		IF EXISTS 
		(
			SELECT 1 
			FROM Payments.AccountPayable ap
			LEFT JOIN Payments.AccountPayableCommitments apc ON ap.Id = apc.AccountPayableId
			WHERE ap.Code = @AccountPayableCode AND apc.Id IS NULL
		)
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Payments.SettingPayments sp WHERE sp.IdOperatingUnit = @OperatingUnitId AND sp.ObligationBudgetInterface = 1)
			BEGIN
				SET @CodeResult = 0 
				SET @MessageResult = ''
				RETURN
			END
			ELSE
			BEGIN
				SET @CodeResult = 999 
				SET @MessageResult = 'Existen facturas sin asociar a un compromiso'
				RETURN
			END
		END

		--Se valida que todos los detalles pertenezcan a la misma vigencia
		IF EXISTS
		(
			SELECT 1
			FROM Payments.AccountPayable ap
			JOIN Payments.AccountPayableCommitments apc ON ap.Id = apc.AccountPayableId
			JOIN Budget.CommitmentDetail cd ON apc.CommitmentDetailId = cd.Id
			JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
			WHERE ap.Code = @AccountPayableCode
				AND c.BudgetaryValidityId <> @BudgetaryValidityId
		)
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Los detalles corresponden a más de una vigencia presupuestal.'
			RETURN
		END

		-------------------------------------------------------------------------------------------------------------------------------------------------------

		WHILE @AccountPayableRows > 0
		BEGIN
			SELECT TOP 1
				@AccountPayableId = ap.Id
			FROM Payments.AccountPayable ap
			WHERE ap.Code = @AccountPayableCode
				AND ap.Id > @AccountPayableId
			ORDER BY ap.Id

			SET @AccountPayableRows = @@ROWCOUNT
			IF @AccountPayableRows = 0 
			BEGIN
				BREAK
			END

			/***********************************************************************************************/
						
			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT 
						Obligation.*,
						ObligationDetail.*
					FROM
					(
						SELECT 
							0 Id,
							@OperatingUnitId OperatingUnitId,
							'' Code,
							@BudgetaryValidityId BudgetaryValidityId,
							ap.DocumentDate DocumentDate,
							s.IdThirdParty ThirdPartyId,
							ap.BillNumber Document,
							1 ObligationType,
							CONCAT(ap.Coments, CHAR(13) + CHAR(10) + 'Obligación generada desde la cuenta por pagar No. ', ap.Code) Observations,
							2 Status
						FROM Payments.AccountPayable ap
						JOIN Common.Supplier s ON ap.IdSupplier = s.Id
						WHERE ap.Id = @AccountPayableId
					) Obligation
					JOIN
					( 
						SELECT
							0 ObligationId,
							cd.Id CommitmentDetailId,
							cd.CategoryId,
							cd.RevenueTypeId,
							ap.ExpirationDate ExpiredDate,
							SUM(apc.Value) InitialValue,
							ap.Id EntityId,
							ap.Code EntityCode,
							'AccountPayable' EntityName
						FROM Payments.AccountPayable ap
						JOIN Payments.AccountPayableCommitments apc ON ap.Id = apc.AccountPayableId
						JOIN Budget.CommitmentDetail cd ON apc.CommitmentDetailId = cd.Id
						WHERE apc.AccountPayableId = @AccountPayableId
						GROUP BY ap.Id, ap.Code, ap.ExpirationDate,
							cd.Id, cd.CategoryId, cd.RevenueTypeId
					) ObligationDetail ON Obligation.Id = ObligationDetail.ObligationId
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Budget].[SP_SaveObligation_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la obligación presupuestal')
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