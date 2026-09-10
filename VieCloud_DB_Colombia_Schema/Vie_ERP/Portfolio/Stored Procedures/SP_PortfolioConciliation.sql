
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para el formulario de conciliación de cartera de una factura específica. Dado un número de factura y una fecha de corte, consolida el estado financiero de la cuenta por cobrar: saldos, valores débito/crédito, retenciones, radicación y estado en cartera, combinando datos de terceros pagadores (EPS/aseguradoras), categoría de factura, grupo de atención, contrato, cuenta contable principal y sede de atención del ingreso asociado. Además, incorpora el detalle de glosas relacionadas con la factura (valores glosados, aceptados en primera y segunda instancia, reiterados, pagados y saldo por conciliar), ajustando cada valor según si la fecha de evaluación de la glosa es anterior o igual a la fecha de corte. Sirve para que el área de cartera visualice el estado completo de conciliación de una factura frente a la aseguradora, incluyendo el nombre del paciente, NIT y estado descriptivo de la glosa (pendiente, conciliada, cobro jurídico, entre otros).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_PortfolioConciliation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_PortfolioConciliation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, para una factura y a una fecha de cierre dadas, la información consolidada de cartera (datos del documento, tercero, contrato, centro de atención y cuenta contable) junto con el estado y valores de glosa aplicables al corte, para soportar la conciliación de cartera.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un número de factura (@InvoiceNumber) y una fecha de cierre (@ClosingDate) válidos.; La función Portfolio.GetAccountReceivableByAge debe estar disponible y poder calcular la cartera a la fecha de cierre.; Las tablas maestras de terceros, personas, categorías de factura, contratos, cuentas contables, glosas, ingresos y centros de atención deben existir y ser consultables.; Formato de fecha esperado DMY (SET DATEFORMAT DMY).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores de glosa (aceptados, pagos, reiterados, balance) solo se reflejan si su fecha de evaluación es anterior o igual a la fecha de cierre; en caso contrario son 0.; Solo se incluyen registros de glosa cuya RadicatedDate (cast a DATE) sea menor o igual a @ClosingDate.; El conjunto de resultados se restringe a una única factura (ar.InvoiceNumber = @InvoiceNumber).; Si ar.ContractId es NULL, se utiliza cg.ContractId para enlazar el contrato (ISNULL(ar.ContractId, cg.ContractId)).; Los valores monetarios de glosa nunca se devuelven NULL: se aplica ISNULL(...,0).; Cualquier excepción se captura y se devuelve un resultset con Code=''999'', el mensaje y la línea de error en lugar de propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera (Account Receivable); Conciliación de cartera; Factura; Glosa; Reiteración de glosa; Pago parcial; Cobro jurídico; Tercero / NIT; Régimen y categoría de facturación; Centro de atención; Paciente / Admisión (ingreso); Cuenta contable principal (PUC); Radicación de cuenta', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Cuando ar.InvoiceNumber = @InvoiceNumber, retorna una fila por registro de cartera con datos enriquecidos de tercero, contrato, centro de atención, cuenta contable y métricas de glosa al corte @ClosingDate.; [RETURN_RESULT] (resultset de error): Si ocurre una excepción durante la ejecución, retorna un resultset alternativo con Code=''999'', ERROR_MESSAGE() y ERROR_LINE() en lugar de los datos de cartera.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si gpg.EvaluationDateGlosa <= @ClosingDate → Se reportan ValueAcceptedFirstInstance, ValuePayments y BalanceReconcile con su valor real else Se devuelven en 0 (no se consideran porque la evaluación de glosa es posterior al cierre); si gpg.EvaluationDateReiteration <= @ClosingDate → Se reportan ValueAcceptedSecondInstance y ValueReiterated con su valor real else Se devuelven en 0 (la reiteración aún no aplica al corte); si CASE sobre gpg.State (1..15) → Traduce el código de estado de glosa a nombre legible (Pendiente Confirmar Glosa, Conciliada, Cobro juridico, etc.) else Si State no coincide con los códigos definidos, se reporta ''No esta Glosada''', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetAccountReceivableByAge', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.GetAccountReceivableByAge; Common.ThirdParty; Common.Person; Billing.InvoiceCategories; Contract.CareGroup; Contract.Contract; GeneralLedger.MainAccounts; Glosas.GlosaPortfolioGlosada; dbo.ADINGRESO; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_PortfolioConciliation';
-- GO
