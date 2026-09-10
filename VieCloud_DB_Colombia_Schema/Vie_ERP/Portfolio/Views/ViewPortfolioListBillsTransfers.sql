

CREATE VIEW [Portfolio].[ViewPortfolioListBillsTransfers]
--Vista para el DataSource de facturas en el formulario FrmPortfolioTranfers
AS
	WITH OfficialCurrency_CTE AS (	SELECT TOP 1 cs.OfficialCurrencyId,c1.Abbreviation 
								FROM GeneralLedger.CompanySettings cs WITH(NOLOCK)
								JOIN Common.Currency c1 WITH(NOLOCK) ON cs.OfficialCurrencyId= c1.Id)
		SELECT	d.*
		FROM (SELECT	ara.Id,
				ar.InvoiceNumber,
				th.Id as ThirdPartyId,
				ar.InvoiceId,
				CONCAT(th.Nit,' - ',th.Name) as NitName,
				CONCAT(ma.Number,' - ', ma.Name) as NumberName,		
				CASE ara.MainAccountId
					WHEN ar.AccountWithoutRadicateId THEN '1-Sin Radicar'
					WHEN ar.AccountRadicateId THEN '3-Radicada Entidad'
					WHEN ar.AccountObjectionRemediedId THEN '4-Glosada sin Conciliar'
					WHEN ar.AccountConciliationId THEN '12-Glosada Conciliada'
					WHEN ar.AccountHardCollectionId THEN '15-Cuenta de Dificil Recaudo'
					WHEN ar.AccountLegalCollectionId THEN '16-Cobro Juridico'
					ELSE 'N/A'
				END PortfolioStatusName,
				CASE ara.MainAccountId
					WHEN ar.AccountWithoutRadicateId THEN 1
					WHEN ar.AccountRadicateId THEN 3
					WHEN ar.AccountObjectionRemediedId THEN 4
					WHEN ar.AccountConciliationId THEN 12
					WHEN ar.AccountHardCollectionId THEN 15
					WHEN ar.AccountLegalCollectionId THEN 16
					ELSE 0
				END SpecificPortfolioStatus,
				ar.PortfolioStatus,
				ar.Status,
				ar.AccountReceivableType,
				ara.AccountReceivableId,
				ara.Value,	
				ara.Balance,
				ISNULL(c.Id,(SELECT OfficialCurrencyId FROM OfficialCurrency_CTE)) CurrencyId,
				ISNULL(C.Abbreviation,(SELECT Abbreviation FROM OfficialCurrency_CTE)) CurrencyAbbreviation,
				CONCAT(trim(i.PatientCode),' - ',pa.IPNOMCOMP) Patient
				FROM Portfolio.AccountReceivableAccounting ara WITH(NOLOCK)
				JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) ON ara.AccountReceivableId = ar.Id
				JOIN Common.ThirdParty th  WITH(NOLOCK) ON ar.ThirdPartyId = th.Id
				LEFT JOIN Billing.Invoice i WITH(NOLOCK) ON ar.InvoiceId = i.Id
				LEFT JOIN INPACIENT pa WITH(NOLOCK) ON i.PatientCode = pa.IPCODPACI
				LEFT JOIN Common.Currency c WITH(NOLOCK) ON c.Id =ar.CurrencyId
				LEFT JOIN Glosas.GlosaPortfolioGlosada gfg WITH(NOLOCK) ON ar.InvoiceNumber = gfg.InvoiceNumber
				LEFT JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ara.MainAccountId = ma.Id			
				WHERE  ara.Balance > 0  
				) as  d
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de cartera que lista las facturas con saldo pendiente disponibles para ser trasladadas o reclasificadas entre estados de cobro (formulario de traslados de portafolio). Integra los registros contables de cuentas por cobrar con sus facturas de facturación, el tercero pagador (EPS, aseguradora, empresa), la cuenta contable principal del plan de cuentas y el paciente asociado. Para cada registro expone el estado legible de cartera (Sin Radicar, Radicada, Glosada sin Conciliar, Glosada Conciliada, Difícil Recaudo, Cobro Jurídico), el valor y saldo pendiente, y la moneda funcional de la cuenta (o la moneda oficial de la compañía si no tiene una asignada). Solo incluye cuentas por cobrar cuyo saldo contable sea mayor a cero.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioListBillsTransfers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewPortfolioListBillsTransfers';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone facturas y movimientos contables de cuentas por cobrar con saldo pendiente, clasificándolas por estado de cartera (sin radicar, radicada, glosada, conciliada, difícil recaudo, cobro jurídico) para alimentar formularios de traslado de cartera.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioListBillsTransfers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId definido para usarlo como moneda por defecto cuando la cuenta por cobrar no tenga moneda asignada.; Las cuentas por cobrar deben estar vinculadas a un tercero (ThirdParty) existente.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioListBillsTransfers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila retornada tiene Balance > 0.; El estado específico de cartera siempre se deriva comparando MainAccountId del asiento contable con las cuentas de estado configuradas en la cuenta por cobrar (sin radicar, radicada, glosada, conciliada, difícil recaudo, jurídica).; Siempre se retorna una moneda: si la cuenta no tiene una asignada, se usa la moneda oficial de la empresa.; El identificador del paciente se construye como ''PatientCode - Nombre Completo'' usando trim sobre el código.; El identificador del tercero se construye como ''Nit - Nombre'' y el de la cuenta contable como ''Número - Nombre''.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioListBillsTransfers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cartera; cuenta por cobrar; factura; glosa; conciliación; radicación; cobro jurídico; difícil recaudo; tercero; paciente; moneda oficial; cuenta contable principal; saldo pendiente', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioListBillsTransfers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.AccountReceivableAccounting: Solo se retornan registros con ara.Balance > 0 (saldo pendiente de cobro mayor a cero).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioListBillsTransfers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ara.MainAccountId = ar.AccountWithoutRadicateId → Clasifica como ''1-Sin Radicar'' (SpecificPortfolioStatus=1); si ara.MainAccountId = ar.AccountRadicateId → Clasifica como ''3-Radicada Entidad'' (SpecificPortfolioStatus=3); si ara.MainAccountId = ar.AccountObjectionRemediedId → Clasifica como ''4-Glosada sin Conciliar'' (SpecificPortfolioStatus=4); si ara.MainAccountId = ar.AccountConciliationId → Clasifica como ''12-Glosada Conciliada'' (SpecificPortfolioStatus=12); si ara.MainAccountId = ar.AccountHardCollectionId → Clasifica como ''15-Cuenta de Dificil Recaudo'' (SpecificPortfolioStatus=15); si ara.MainAccountId = ar.AccountLegalCollectionId → Clasifica como ''16-Cobro Juridico'' (SpecificPortfolioStatus=16); si MainAccountId no coincide con ninguna cuenta de estado de cartera definida en AccountReceivable → Clasifica como ''N/A'' (SpecificPortfolioStatus=0); si ar.CurrencyId IS NULL (la cuenta por cobrar no tiene moneda asignada) → Usa la OfficialCurrencyId y Abbreviation de GeneralLedger.CompanySettings como moneda por defecto else Usa la moneda asignada en ar.CurrencyId con su abreviatura desde Common.Currency', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioListBillsTransfers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Common.Currency; Portfolio.AccountReceivableAccounting; Portfolio.AccountReceivable; Common.ThirdParty; Billing.Invoice; INPACIENT; Glosas.GlosaPortfolioGlosada; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioListBillsTransfers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewPortfolioListBillsTransfers';
GO
