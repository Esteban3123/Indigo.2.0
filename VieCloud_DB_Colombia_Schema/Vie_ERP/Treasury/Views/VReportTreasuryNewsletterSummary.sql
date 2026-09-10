
CREATE VIEW [Treasury].[VReportTreasuryNewsletterSummary]
AS
SELECT	Row,
		CashRegisterId, CashRegisterCode, CashRegisterName as NameCash, CashRegisterStatus,
		EntityBankAccountId, EntityBankAccountCode, EntityBankAccountName as Name, [Type], EntityBankAccountNumber as Number,EntityBankStatus,
		Code, DocumentDate, Number AS MainAccount, ValueDebit AS SumReceipts, ValueCredit AS SumExpenditures, Status,CurrencyAbbreviation
FROM
(
	SELECT	Row,
			CashRegisterId,CashRegisterCode,CashRegisterName,CashRegisterStatus,
			0 AS EntityBankAccountId,'' AS EntityBankAccountCode,'' AS EntityBankAccountName, 0 Type,'' AS EntityBankAccountNumber, CashRegisterStatus EntityBankStatus,
			Code,DocumentDate,Number,Status,
			IIF(Nature = 1, Value, 0) ValueDebit, IIF(Nature = 1, 0, Value) ValueCredit,CurrencyAbbreviation
	FROM [Treasury].[VReportTreasuryNewsletterCash]
	WHERE Status IN (2, 4)
UNION ALL
	SELECT	Row,
			0 AS CashRegisterId,'' AS CashRegisterCode,'' AS CashRegisterName, EntityBankStatus CashRegisterStatus,
			EntityBankId AS EntityBankAccountId,EntityBankAccountCode,EntityBankAccountName,Type,EntityBankAccountNumber,EntityBankStatus,
			Code,DocumentDate,Number,Status,
			IIF(Nature = 1, Value, 0) ValueDebit, IIF(Nature = 1, 0, Value) ValueCredit,CurrencyAbbreviation
	FROM [Treasury].[VReportTreasuryNewsletterEntityBankAccount]
	WHERE Status IN (2, 4)
) AS Datos
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de resumen del boletín de tesorería que consolida en un único resultado los movimientos aprobados o cerrados (estados 2 y 4) de dos fuentes: cajas registradoras y cuentas bancarias de entidades. Combina mediante unión los comprobantes de caja (VReportTreasuryNewsletterCash) con los movimientos de cuentas bancarias (VReportTreasuryNewsletterEntityBankAccount), separando los valores en columnas de débito (ingresos/recaudos) y crédito (egresos/pagos) según la naturaleza del movimiento. Sirve para reportería ejecutiva de tesorería, permitiendo visualizar el resumen de entradas y salidas de dinero por caja o cuenta bancaria, con su código de documento, fecha, estado y moneda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportTreasuryNewsletterSummary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VReportTreasuryNewsletterSummary';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único conjunto los movimientos de boletín de tesorería provenientes de cajas y de cuentas bancarias de entidad, separando montos en débito y crédito según su naturaleza.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las vistas Treasury.VReportTreasuryNewsletterCash y Treasury.VReportTreasuryNewsletterEntityBankAccount deben existir y exponer las columnas referenciadas (Row, Code, DocumentDate, Number, Status, Nature, Value, CurrencyAbbreviation, etc.).; Los movimientos deben tener Status en (2, 4) para ser incluidos.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen movimientos con Status 2 o 4.; Cada fila del resultado pertenece exclusivamente a una caja o a una cuenta bancaria, nunca a ambas (los identificadores del otro origen quedan en 0/'''').; ValueDebit y ValueCredit son mutuamente excluyentes por fila: uno de los dos siempre es 0.; El campo MainAccount expuesto corresponde al Number del documento de origen, no al número de cuenta bancaria.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tesorería; Boletín de tesorería; Caja registradora; Cuenta bancaria de entidad; Débito/Crédito (naturaleza contable); Ingresos (Receipts) y Egresos (Expenditures); Moneda', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.VReportTreasuryNewsletterSummary: Devuelve la unión (UNION ALL) de movimientos de caja y de cuentas bancarias filtrados por Status IN (2,4), con columnas de caja en cero/'''' cuando el origen es banco y viceversa.; [RETURN_RESULT] Treasury.VReportTreasuryNewsletterSummary: Cuando Nature = 1 el monto se asigna a ValueDebit (SumReceipts); en caso contrario se asigna a ValueCredit (SumExpenditures).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status IN (2, 4) → Incluye el movimiento en el resultado else Excluye el movimiento; si Nature = 1 → Value se reporta como ValueDebit (SumReceipts) y ValueCredit = 0 else Value se reporta como ValueCredit (SumExpenditures) y ValueDebit = 0; si Origen = caja (VReportTreasuryNewsletterCash) → Se llenan campos de CashRegister y se anulan (0/'''') los de EntityBankAccount; EntityBankStatus toma el valor de CashRegisterStatus else Origen = banco: se llenan campos de EntityBankAccount y se anulan los de CashRegister; CashRegisterStatus toma el valor de EntityBankStatus', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.VReportTreasuryNewsletterCash; Treasury.VReportTreasuryNewsletterEntityBankAccount', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VReportTreasuryNewsletterSummary';
GO
