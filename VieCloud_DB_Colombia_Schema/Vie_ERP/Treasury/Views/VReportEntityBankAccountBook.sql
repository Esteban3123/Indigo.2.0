CREATE VIEW [Treasury].[VReportEntityBankAccountBook]
AS
SELECT	v.Row,
		v.NameVoucher,
		v.EntityBankId AS EntityBankAccountId,
		v.EntityBankAccountCode,
		v.EntityBankAccountName,
		v.Type,
		v.EntityBankAccountNumber,
		v.EntityBankStatus,
		v.Number,
		v.ThirdPartyNit,
		v.ThirdPartyName,
		v.Code,
		v.DocumentDate,
		v.Detail,
		v.VoucherType,
		IIF(v.Nature = 1, v.Value, 0) AS ValueDebit,
		IIF(v.Nature = 1, 0, v.Value) AS ValueCredit,
		v.Status
FROM [Treasury].[VReportTreasuryNewsletterEntityBankAccount] v WITH (NOLOCK)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Libro auxiliar de cuentas bancarias de la entidad para reportes de tesorería. Presenta el movimiento detallado de cada cuenta bancaria institucional, separando los valores en columnas de débito y crédito según la naturaleza del movimiento (naturaleza 1 = débito, cualquier otra = crédito). Consume la vista VReportTreasuryNewsletterEntityBankAccount y reorganiza su información incluyendo datos del banco, número de cuenta, tercero (NIT y nombre del proveedor o beneficiario), comprobante, fecha del documento, detalle de la transacción y estado del movimiento. Sirve para generar el libro banco o extracto contable-tesorero con débitos y créditos claramente diferenciados, útil en conciliaciones bancarias y reportes financieros de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportEntityBankAccountBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportEntityBankAccountBook';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Presenta el libro auxiliar de movimientos por cuenta bancaria de entidad, separando el valor del movimiento en columnas de débito y crédito según la naturaleza contable.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportEntityBankAccountBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir y ser consultable la vista Treasury.VReportTreasuryNewsletterEntityBankAccount, que provee los movimientos de cuentas bancarias con su naturaleza contable.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportEntityBankAccountBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Para cada fila, exactamente uno entre ValueDebit y ValueCredit es distinto de cero, determinado por la naturaleza (Nature=1 ⇒ débito; en otro caso ⇒ crédito).; El identificador EntityBankId de la fuente se expone como EntityBankAccountId en la vista.; La consulta a la vista origen se realiza con NOLOCK, por lo que los resultados pueden incluir lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportEntityBankAccountBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cuenta bancaria de entidad; libro auxiliar bancario; comprobante (voucher); tercero (NIT); débito y crédito; naturaleza contable', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportEntityBankAccountBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.VReportEntityBankAccountBook: Cuando Nature = 1, IIF asigna Value a ValueDebit y 0 a ValueCredit; cuando Nature ≠ 1, asigna 0 a ValueDebit y Value a ValueCredit.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportEntityBankAccountBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si v.Nature = 1 → El valor del movimiento se reporta como ValueDebit y ValueCredit queda en 0 else El valor del movimiento se reporta como ValueCredit y ValueDebit queda en 0', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportEntityBankAccountBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.VReportTreasuryNewsletterEntityBankAccount', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportEntityBankAccountBook';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportEntityBankAccountBook';
GO
