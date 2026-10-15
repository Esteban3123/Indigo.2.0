
-- =============================================
-- Author:		Giovanny plazas
-- Create date: 2021-02-08
-- Description:	Procedimiento para formulario conciliacion de cartera
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_PortfolioConciliation]
					
			@InvoiceNumber VARCHAR(20),
			@ClosingDate DATE
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY
	BEGIN TRY
		

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT 
			d.*
		FROM
		(
				SELECT	ar.Id,
						ar.InvoiceNumber AS DocumentCode, 
						ar.AccountReceivableDate, 
						ar.AccountReceivableType,
						ar.PortfolioStatus,
						ar.PortfolioStatusName,
						ar.NumberShares,
						ar.Term,
						ar.OpeningBalance,
						tp.Nit AS ThirdPartyNit, 
						tp.Name AS ThirdPartyName, 
						p.IdentificationType,
						ic.Code + ' - ' + ic.[Name] AS Category, 
						cg.EntityType AS Regimen, 
						ar.AccountWithoutRadicateNumber, 
						ar.RadicatedConsecutive,
						ar.RadicatedUser,
						ar.RadicatedDate,
						ar.RadicatedState,
						ma.Number AS MainAccountNumber, 
						ma.Name AS MainAccountName,
						ar.DocumentValue, 
						ar.RetentionValue,
						ar.InitialValue,
						ar.DebitValue,
						ar.CreditValue,
						ar.TransferValue,
						ar.CashReceiptValue,
						ar.CrossingValue,
						ar.Balance,
						ar.CurrentBalance,
						c.NOMCENATE AS CenterAttention,
						ar.RegimenCalculated,
						gpg.PatientName As PatientName,
						gpg.Nit AS	PatientNit,
						ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
						ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValueAcceptedFirstInstance, 0), 0) ValueAcceptedFirstInstance,
						ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueAcceptedSecondInstance, 0), 0) ValueAcceptedSecondInstance,
						ISNULL(IIF(gpg.EvaluationDateReiteration <= @ClosingDate, gpg.ValueReiterated, 0), 0) ValueReiterated,
						ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.ValuePayments, 0), 0) ValuePayments,
						ISNULL(IIF(gpg.EvaluationDateGlosa <= @ClosingDate, gpg.BalanceGlosa, 0), 0) BalanceReconcile,
						gpg.State GlosaState,
						CASE gpg.state 
							WHEN 1 THEN 'Pendiente Confirmar Glosa'
							WHEN 2 THEN 'Pendiente Evaluacion Glosa'
							WHEN 3 THEN 'Pendiente envio de oficio'
							WHEN 4 THEN 'Pendiente confirmar reiteracion'
							WHEN 5 THEN 'Pendiente evaluacion reitreacion'
							WHEN 6 THEN 'Pendiente conciliacion'
							WHEN 7 THEN 'Pendiente de confirmar Conciliacion'
							WHEN 8 THEN 'Conciliada'
							WHEN 9 THEN 'Conciliada Parcialmente'
							WHEN 11 THEN 'Glosa con Respuesta'
							WHEN 12 THEN 'Reiteracion con respuesta'
							WHEN 13 THEN 'Pendiente confirmar pago parcial'
							WHEN 14 THEN 'Confirmado pago parcial'
							WHEN 15 THEN 'Cobro juridico'
							ELSE 'No esta Glosada'
						END as GlosaStateName
				FROM [Portfolio].[GetAccountReceivableByAge](NULL, @ClosingDate) AS ar
				JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON ar.ThirdPartyId = tp.Id
				JOIN Common.Person AS p WITH (NOLOCK) ON tp.PersonId = p.Id
				LEFT JOIN Billing.InvoiceCategories AS ic WITH (NOLOCK) ON ar.InvoiceCategoryId = ic.Id
				LEFT JOIN Contract.CareGroup AS cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
				LEFT JOIN Contract.Contract AS ct WITH (NOLOCK) ON ISNULL(ar.ContractId, cg.ContractId) = ct.Id
				LEFT JOIN GeneralLedger.MainAccounts AS ma WITH (NOLOCK) ON ma.Id = ar.MainAccountId
				/********************************** ******* **********************************/
				LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH(NOLOCK) ON ar.InvoiceNumber = gpg.InvoiceNumber AND CAST(gpg.RadicatedDate AS DATE) <= @ClosingDate
				LEFT JOIN dbo.ADINGRESO a WITH(NOLOCK) ON ar.AdmissionNumber = a.NUMINGRES
				LEFT JOIN dbo.ADCENATEN c WITH(NOLOCK) ON a.CODCENATE = c.CODCENATE
				/**************************************** FILTROS ****************************************/
				where ar.InvoiceNumber = @InvoiceNumber
			

		) AS d
		ORDER BY 1, 2
	END TRY
	BEGIN CATCH	
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
	END CATCH
END