
CREATE VIEW [Treasury].[ViewListCrossingAccountDetailCxC]
AS
SELECT	dcc.Id, 
		dcc.CrossingAccountId, 
		dcc.AccountReceivableId, 
		dcc.AccountReceivableAccountingId, 
		ar.InvoiceNumber BillNumber, 
		dcc.MainAccountId, 
		ma.Number + ' - ' + ma.Name MainAccountDescription, 
		t.Nit + ' - ' + t.Name ThirdPartyDescription, 
		ar.Value, ar.Balance, 
		dcc.CrossingValue, 
		dcc.Detail, 
		dcc.IdCashFlowConcept,
		cc.Code + ' - ' + cc.NameConcept CodeNameCashFlowConcept,
		ar.CurrencyId,
		c.Abbreviation AS CurrencyAbbreviation
FROM Treasury.CrossingAccountDetailCxC dcc
JOIN GeneralLedger.MainAccounts ma on ma.Id = dcc.MainAccountId
JOIN Portfolio.AccountReceivableAccounting ara on ara.Id = dcc.AccountReceivableAccountingId
JOIN Portfolio.AccountReceivable ar on ar.Id = ara.AccountReceivableId
JOIN Common.ThirdParty t on t.Id = ar.ThirdPartyId
LEFT JOIN Treasury.CashFlowConcept cc on cc.Id = dcc.IdCashFlowConcept
LEFT JOIN Common.Currency c ON c.Id = ar.CurrencyId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra el detalle de los cruces de cuentas por cobrar en tesorería, combinando cada línea de cruce con la información de la factura o cuenta de cobro (número de factura, valor total, saldo pendiente y valor aplicado en el cruce), la cuenta contable principal involucrada, el tercero o cliente responsable del cobro (NIT y nombre), el concepto de flujo de caja que clasifica el movimiento y la moneda utilizada. Integra los módulos de Tesorería, Cartera y Contabilidad General para ofrecer una vista consolidada que permite auditar y reportar cómo se aplican los cruces entre cuentas por cobrar y sus registros contables, facilitando el seguimiento del recaudo y la gestión de cartera.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'ViewListCrossingAccountDetailCxC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'ViewListCrossingAccountDetailCxC';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el detalle de cruces de cuentas por cobrar en tesorería, enriqueciendo cada línea con datos de la cuenta contable, tercero, factura, concepto de flujo de caja y moneda asociados.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todo detalle de cruce debe referenciar una cuenta contable principal existente en GeneralLedger.MainAccounts.; Todo detalle de cruce debe estar asociado a un registro contable de cuenta por cobrar existente en Portfolio.AccountReceivableAccounting.; El registro contable debe referenciar una cuenta por cobrar existente en Portfolio.AccountReceivable.; La cuenta por cobrar debe tener un tercero registrado en Common.ThirdParty.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles de cruce cuyo MainAccount, AccountReceivableAccounting, AccountReceivable y ThirdParty existan (INNER JOIN obligatorio).; El BillNumber expuesto proviene del InvoiceNumber de la cuenta por cobrar original, no del registro contable intermedio.; Value y Balance reportados corresponden a la cuenta por cobrar (AccountReceivable), mientras que CrossingValue corresponde al monto efectivamente aplicado en el cruce.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de cuentas por cobrar; Detalle de cruce en tesorería; Cuenta por cobrar; Registro contable de cartera; Cuenta contable principal (PUC); Tercero; Factura (InvoiceNumber); Concepto de flujo de caja; Moneda/divisa; Saldo pendiente', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.ViewListCrossingAccountDetailCxC: Devuelve una fila por cada detalle de cruce de CxC con descripciones concatenadas: ''Numero - Nombre'' para la cuenta contable, ''Nit - Nombre'' para el tercero y ''Code - NameConcept'' para el concepto de flujo de caja.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEFT JOIN sobre Treasury.CashFlowConcept por dcc.IdCashFlowConcept → Permite que el detalle de cruce no tenga concepto de flujo de caja asignado; en ese caso CodeNameCashFlowConcept es NULL. else Si existe el concepto, se devuelve ''Code - NameConcept''.; si LEFT JOIN sobre Common.Currency por ar.CurrencyId → Permite cuentas por cobrar sin moneda asignada; CurrencyAbbreviation queda NULL. else Si la CxC tiene moneda, se devuelve su abreviatura.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CrossingAccountDetailCxC; GeneralLedger.MainAccounts; Portfolio.AccountReceivableAccounting; Portfolio.AccountReceivable; Common.ThirdParty; Treasury.CashFlowConcept; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxC';
GO
