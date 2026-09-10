

CREATE VIEW [Payments].[VReportDeferredCausation]
AS
select
dcs.Id as deferredCausationId, 
dcs.PaymentMonth  as paymentMonth,
dcs.PaymentYear as paymentYear, 
ap.Code as accountPayableCode, 
ap.BillNumber as accountPayableBillNumber, 
tp.Nit + ' - ' + tp.Name as supplierDescription,
case dc.PeriodsNumber
	when 1 then cast(dc.PeriodsNumber as varchar(2)) + ' Mes' 
	else cast(dc.PeriodsNumber as varchar(2)) + ' Meses' 
end as periodMonth,
dcs.Value as valuePeriod, 
ISNULL((select 
			Sum(dcsna.Value) 
			from Payments.DeferredCausationShare dcsna 
			where dcsna.DeferredCausationId = dcs.DeferredCausationId ),0)  - 
			ISNULL((select
			Sum(dcsa.Value) 
			from Payments.DeferredCausationShare dcsa
			where dcsa.DeferredCausationId = dcs.DeferredCausationId AND dcsa.Amortized=1
			And cast(('01/' + RIGHT('0'+cast(dcsa.PaymentMonth as varchar(2)),2) 
			+ '/'+ cast(dcsa.PaymentYear as varchar(4))) as date) <= cast(('01/' + 
			RIGHT('0'+cast(dcs.PaymentMonth as varchar(2)),2) + '/'+ cast(dcs.PaymentYear as varchar(4))) as date)),0)  as valueNotArmotize,
ISNULL((select
		Sum(dcsa.Value) 
		from Payments.DeferredCausationShare dcsa
		where dcsa.DeferredCausationId = dcs.DeferredCausationId AND dcsa.Amortized=1
		And cast(('01/' + RIGHT('0'+cast(dcsa.PaymentMonth as varchar(2)),2) 
		+ '/'+ cast(dcsa.PaymentYear as varchar(4))) as date) <= cast(('01/' + 
		RIGHT('0'+cast(dcs.PaymentMonth as varchar(2)),2) + '/'+ cast(dcs.PaymentYear as varchar(4))) as date)),0) as valueArmotize,
		c.Id as CurrencyId,
		c.Abbreviation as CurrencyAbbreviation,
		dcs.Amortized
from Payments.DeferredCausationShare dcs 
inner join Payments.DeferredCausation dc on dc.Id = dcs.DeferredCausationId
inner join Payments.AccountPayable ap on ap.Id = dc.IdAccountPayable inner join Common.Supplier s on s.Id = ap.IdSupplier
inner join Common.ThirdParty tp on tp.Id = s.IdThirdParty
left join Common.Currency c on c.Id = ap.CurrencyId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de causación diferida de cuentas por pagar. Muestra cada cuota mensual programada de un pago diferido (distribuido en varios períodos contables), indicando el mes y año de causación, el código y número de factura de la cuenta por pagar, el NIT y nombre del proveedor, la cantidad total de meses del diferido, el valor de la cuota del período, el saldo pendiente por amortizar acumulado hasta ese mes y el valor ya amortizado acumulado hasta ese mes. Integra las cuotas de causación diferida con sus cabeceras, las cuentas por pagar, los proveedores, los terceros y la moneda, siendo el insumo principal para reportes contables de seguimiento y control del diferido de pagos a proveedores.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VReportDeferredCausation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VReportDeferredCausation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta, por cada cuota de causación diferida, los valores acumulados amortizados y no amortizados hasta su período, junto con datos del proveedor, cuenta por pagar y moneda.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportDeferredCausation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada cuota (DeferredCausationShare) debe estar asociada a una causación diferida existente.; Cada causación diferida debe estar asociada a una cuenta por pagar existente con proveedor y tercero válidos.; Los campos PaymentMonth y PaymentYear deben ser convertibles a una fecha válida con día 01.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportDeferredCausation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor amortizado solo considera cuotas marcadas como Amortized=1 cuya fecha (día 01 + mes + año) sea menor o igual a la fecha de la cuota actual del reporte.; El valor no amortizado se calcula como el total de cuotas de la causación diferida menos el valor amortizado acumulado hasta la fecha de la cuota actual.; Si no existen cuotas amortizadas o totales, los valores se devuelven como 0 (ISNULL).; La descripción del proveedor siempre se construye concatenando NIT y Nombre del tercero separados por '' - ''.; Toda fila del reporte requiere existencia de causación diferida, cuenta por pagar, proveedor y tercero (INNER JOIN); la moneda es opcional (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportDeferredCausation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causación diferida; Cuota de causación diferida; Cuenta por pagar; Proveedor; Tercero; Moneda; Amortización; Período de pago (mes/año)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportDeferredCausation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada cuota de causación diferida con su valor del período, valor amortizado acumulado y valor pendiente, calculados a la fecha (mes/año) de cada cuota.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportDeferredCausation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PeriodsNumber = 1 → Etiqueta el período como ''Mes'' (singular) else Etiqueta el período como ''Meses'' (plural)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportDeferredCausation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.DeferredCausationShare; Payments.DeferredCausation; Payments.AccountPayable; Common.Supplier; Common.ThirdParty; Common.Currency', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportDeferredCausation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VReportDeferredCausation';
GO
