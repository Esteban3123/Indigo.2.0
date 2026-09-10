

CREATE VIEW [Treasury].[VReportCashBook]
AS

WITH Cte_temp as (	SELECT top 1
						c.Id CurrencyId, 
						c.Abbreviation as CurrencyAbbreviation 
					from GeneralLedger.CompanySettings cs with(NOLOCK)
					JOIN Common.Currency c WITH(NOLOCK) ON c.Id= cs.OfficialCurrencyId)

SELECT	v.Row,
		v.NameVoucher,
		v.CashRegisterId,
		v.CashRegisterCode,
		v.CashRegisterName,
		v.CashRegisterStatus,		
		v.IdBankAccount,
		v.BankAccountCode,
		v.BankAccountName,
		v.BankAccountStatus,
		v.Number,
		v.ThirdPartyNit,
		v.ThirdPartyName,
		v.Code,
		v.DocumentDate,
		v.Detail,
		v.VoucherType,
		v.PaymentMethod,
		IIF(v.Nature = 1, v.Value, 0) AS ValueDebit,
		IIF(v.Nature = 1, 0, v.Value) AS ValueCredit,
		v.Status,
		v.UserCode,
		v.CodeNameUser,
		ISNULL(v.CurrencyId,cte.CurrencyId) AS CurrencyId,
		ISNULL(v.CurrencyAbbreviation,cte.CurrencyAbbreviation) AS CurrencyAbbreviation
FROM [Treasury].[VReportTreasuryNewsletterCash] v WITH (NOLOCK)
JOIN Cte_temp cte on 1=1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del libro de caja (libro auxiliar de tesorería) que presenta el detalle de todos los comprobantes de caja registrados, separando cada movimiento en columnas de débito y crédito según su naturaleza contable. Integra la información de cajas registradoras, cuentas bancarias, terceros (NIT y nombre), método de pago, estado y usuario responsable, enriqueciendo cada registro con la moneda oficial de la compañía obtenida desde la configuración contable general cuando el comprobante no tiene moneda propia asignada. Sirve para reportería financiera y auditoría de movimientos de efectivo, conciliación de caja y control de ingresos y egresos de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportCashBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportCashBook';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los movimientos del boletín de caja de tesorería desglosando el valor en columnas de débito y crédito según la naturaleza, y completando la moneda con la moneda oficial de la empresa cuando el movimiento no la trae.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en GeneralLedger.CompanySettings con OfficialCurrencyId válido referenciando Common.Currency, de lo contrario el CROSS JOIN con el CTE no produce filas.; La vista Treasury.VReportTreasuryNewsletterCash debe estar disponible y exponer las columnas Nature, Value, CurrencyId y CurrencyAbbreviation entre otras.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Para cada fila, ValueDebit y ValueCredit son mutuamente excluyentes: solo uno tiene valor según Nature, el otro siempre es 0.; Si el movimiento no trae moneda asociada (CurrencyId/Abbreviation NULL), se asume la moneda oficial de la empresa definida en GeneralLedger.CompanySettings.OfficialCurrencyId.; La moneda oficial usada como fallback se obtiene de un único registro (TOP 1) de CompanySettings, asumiendo configuración única por empresa.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Libro de caja; Comprobante de tesorería; Caja registradora; Cuenta bancaria; Débito y crédito contable; Moneda oficial de la empresa; Tercero (NIT); Método de pago', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Cuando v.Nature = 1 retorna el valor en ValueDebit; en caso contrario lo retorna en ValueCredit (la columna opuesta queda en 0).; [RETURN_RESULT] Resultset: Cuando v.CurrencyId o v.CurrencyAbbreviation son NULL, se sustituyen por la moneda oficial obtenida de GeneralLedger.CompanySettings + Common.Currency (TOP 1).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si v.Nature = 1 → El monto se asigna a ValueDebit y ValueCredit queda en 0 (movimiento débito) else El monto se asigna a ValueCredit y ValueDebit queda en 0 (movimiento crédito)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.CompanySettings; Common.Currency; Treasury.VReportTreasuryNewsletterCash', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportCashBook';
GO
