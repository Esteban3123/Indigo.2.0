

CREATE view [Treasury].[ViewListCrossingAccountDetailCxP]
as

select 
	dcp.Id,
	dcp.CrossingAccountId,
	dcp.AccountPayableId, 
	dcp.MainAccountId,
	dcp.CrossingValue, 
	dcp.Detail, 
	dcp.IdCashFlowConcept,
	ma.Number + ' - ' + ma.Name MainAccountDescription,
	ap.BillNumber, ap.Value, ap.Balance, 
	t.Nit + ' - ' + t.Name ThirdPartyDescription, cc.Code + ' - ' + cc.NameConcept CodeNameCashFlowConcept,
	ap.CurrencyId,
	c.Abbreviation AS CurrencyAbbreviation
from Treasury.CrossingAccountDetailCxP dcp
inner join GeneralLedger.MainAccounts ma on ma.Id = dcp.MainAccountId
inner join Payments.AccountPayable ap on ap.Id = dcp.AccountPayableId
inner join Common.ThirdParty t on t.Id = ap.IdThirdParty
left join Treasury.CashFlowConcept cc on cc.Id = dcp.IdCashFlowConcept
LEFT JOIN Common.Currency c ON c.Id = ap.CurrencyId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista detallada de los cruces de cuentas con cuentas por pagar (CxP) en tesorería. Combina cada línea de cruce con la información de la cuenta contable principal (número y nombre), la factura o documento por pagar del proveedor (número de factura, valor total y saldo pendiente), los datos del tercero o proveedor (NIT y nombre), el concepto de flujo de caja asociado y la moneda de la factura con su abreviatura. Sirve para consultar y reportar cómo se aplican o cruzan los saldos de cuentas por pagar contra cuentas contables en el módulo de tesorería, facilitando la conciliación de pagos y el seguimiento de saldos pendientes con proveedores.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'ViewListCrossingAccountDetailCxP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'ViewListCrossingAccountDetailCxP';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de cruces de cuentas por pagar enriquecido con descripciones legibles de la cuenta contable, el tercero proveedor, el concepto de flujo de caja y la moneda asociada.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan detalles de cruce que tengan cuenta principal (MainAccount) y cuenta por pagar (AccountPayable) existentes, debido a los INNER JOIN.; Solo se incluyen cruces cuya cuenta por pagar esté asociada a un tercero existente en Common.ThirdParty (INNER JOIN).; El concepto de flujo de caja y la moneda son opcionales: se muestran como NULL si no existe correspondencia (LEFT JOIN).; La descripción de cuenta principal se construye concatenando ''Number - Name''; la del tercero como ''Nit - Name''; y la del concepto de flujo como ''Code - NameConcept''.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de cuentas; Cuentas por pagar; Cuenta contable principal; Tercero/Proveedor; Concepto de flujo de caja; Moneda', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve cada línea de Treasury.CrossingAccountDetailCxP junto con la descripción concatenada de la cuenta principal, datos de la cuenta por pagar (BillNumber, Value, Balance), descripción del tercero (Nit + Name), descripción del concepto de flujo de caja y abreviatura de moneda.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CrossingAccountDetailCxP; GeneralLedger.MainAccounts; Payments.AccountPayable; Common.ThirdParty; Treasury.CashFlowConcept; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'ViewListCrossingAccountDetailCxP';
GO
