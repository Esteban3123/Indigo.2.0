
CREATE VIEW [Treasury].[VSchedulePayment]
AS
SELECT DISTINCT 
	s.Id as SupplierId, 
	s.IdThirdParty as ThirdId, 
	s.Name as SupplierName, 
	s.Code as SupplierCode, 
	tp.Nit as SupplierNit, 
	dl.Description as DescriptionLine, 
	dl.Id as DistributionLineId, 
	dl.ExpensesConceptId as ExpenseConceptIdDistributionLine, 
	dl.IdMainAccount as MainAccountIdDistributionLine,
	ec.Nature as NatureExpenseConcept, 
	st.Id as SupplierTypeId, 
	st.Name as SupplierTypeName, 
	ap.BillNumber as Invoice, 
	ap.ExpirationDate as ExpirationDate, 
	agp.Name as AgePayment, 
	ap.Balance as InvoiceBalance, 
	aps.Share as Share, 
	aps.DateExpires as ShareExpirationDate, 
	aps.Balance as BalanceShare, 
	0.0 as PayValue, 
	0 as PaymentConceptId, 
	ap.Id as AccountPayableId, 
	aps.Id as AccountPayableShareId, 
	0 as SchedulePaymentDetailId, 
	0.0 as PaymentPercent, 
	ap.Code AS AccountPayableCode
FROM Payments.AccountPayable AS ap WITH (NOLOCK)
JOIN Common.Supplier AS s WITH (NOLOCK) ON ap.IdSupplier = s.Id
JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = s.IdThirdParty
JOIN Common.SuppliersDistributionLines AS spd WITH (NOLOCK) ON ap.IdSuppliersDistributionLines = spd.Id
JOIN Common.DistributionLines AS dl WITH (NOLOCK) ON  spd.IdDistributionLine = dl.Id
JOIN Treasury.ExpenseConcepts AS ec WITH (NOLOCK) ON ec.Id = dl.ExpensesConceptId
JOIN Common.SupplierType AS st WITH (NOLOCK) ON ap.SupplierTypeId = st.Id 
JOIN Payments.AccountPayableShares AS aps ON aps.IdAccountPayable = ap.Id and aps.Balance > 0 
JOIN Payments.SettingPayments as SP on SP.IdOperatingUnit = AP.IdOperatingUnit
LEFT JOIN Payments.AgesPayments as AGP on AGP.SettingPaymentId = SP.Id and (DATEDIFF(DAY, APS.DateExpires, GETDATE()) + 1) BETWEEN AGP.InitialRange  and AGP.EndRange
WHERE ap.Status = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cronograma de pagos programados a proveedores: consolida las cuotas pendientes (saldo mayor a cero) de facturas o cuentas por pagar activas (estado 2), integrando datos del proveedor (nombre, NIT, código), la línea de distribución contable asignada, el concepto de gasto y tipo de proveedor, junto con el detalle de cada cuota (monto, fecha de vencimiento, saldo) y su tramo de antigüedad (aging) según los rangos configurados en la unidad operativa. Sirve como base para la programación y visualización del flujo de egresos, permitiendo identificar qué facturas están por vencer o vencidas, clasificadas por proveedor, línea presupuestal y antigüedad de la deuda.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VSchedulePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'VSchedulePayment';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las cuotas pendientes de pago de cuentas por pagar activas, enriquecidas con datos del proveedor, línea de distribución, concepto de gasto y rango de antigüedad, para alimentar la programación de pagos.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VSchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta por pagar debe tener Status = 2 (estado considerado válido para programar pago).; La cuota (AccountPayableShares) debe tener saldo positivo (Balance > 0).; Debe existir configuración de pagos (SettingPayments) para la unidad operativa de la cuenta por pagar.; El proveedor debe tener línea de distribución asociada con concepto de gasto definido.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VSchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los campos PayValue, PaymentConceptId, SchedulePaymentDetailId y PaymentPercent siempre se inicializan en 0 (no provienen de tablas).; La antigüedad se calcula desde la fecha de vencimiento de la cuota, no de la factura, sumando 1 día al DATEDIFF.; El aging se obtiene de la configuración de pagos correspondiente a la unidad operativa (IdOperatingUnit) de cada cuenta por pagar.; Solo se consideran proveedores con línea de distribución vigente vinculada a un concepto de gasto de tesorería.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VSchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Proveedor; Tercero; Cuota de pago; Saldo pendiente; Antigüedad de cartera (aging); Concepto de gasto; Línea de distribución contable; Tipo de proveedor; NIT; Programación de pagos; Unidad operativa; Fecha de vencimiento', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VSchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.VSchedulePayment: Devuelve una fila por cada cuota (AccountPayableShares) con saldo > 0 perteneciente a una cuenta por pagar con Status = 2, inicializando PayValue, PaymentConceptId, SchedulePaymentDetailId y PaymentPercent en cero (campos a diligenciar por el proceso consumidor).', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VSchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ap.Status = 2 → Solo se incluyen cuentas por pagar en este estado (típicamente aprobadas/activas para pago). else Excluidas del resultado.; si aps.Balance > 0 → Solo se listan cuotas con saldo pendiente. else Cuotas saldadas se excluyen.; si (DATEDIFF(DAY, APS.DateExpires, GETDATE()) + 1) BETWEEN AGP.InitialRange AND AGP.EndRange → Asocia el rango de antigüedad (AgePayment) correspondiente a los días vencidos de la cuota. else AgePayment queda NULL (LEFT JOIN) si ningún rango configurado aplica.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VSchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Common.Supplier; Common.ThirdParty; Common.SuppliersDistributionLines; Common.DistributionLines; Treasury.ExpenseConcepts; Common.SupplierType; Payments.AccountPayableShares; Payments.SettingPayments; Payments.AgesPayments', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VSchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'VSchedulePayment';
GO
