

CREATE VIEW [Treasury].[View_SchedulePayment]
AS
SELECT AccountPayableId as Id,* 
FROM (
	SELECT  
		s.Id as SupplierId, 
		s.IdThirdParty as ThirdId, 
		s.Name as SupplierName, 
		s.Code as SupplierCode, 
		tp.Nit as SupplierNit, 
		dl.Description as DescriptionLine, 
		dl.Id as DistributionLineId, 
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
		ap.Id as AccountPayableId, 
		aps.Id as AccountPayableShareId, 
		ap.Code AS AccountPayableCode,
		ap.Value As CXPValue,
		iif(ap.Value = (aps.Balance + isnull(pnd.AdjusmentValue,0)),isnull(ppd.RangeName,''''),'''') AS RangeNameDiscount,
		iif(ap.Value = (aps.Balance + isnull(pnd.AdjusmentValue,0)),cast(isnull(ppd.DiscountRate,0) as DECIMAL(5,2)),0) AS DiscountRate,
		iif(ap.Value = (aps.Balance + isnull(pnd.AdjusmentValue,0)),cast((iif(gdvap.BaseValue is null or gdvap.BaseValue =0,(ap.Value+(ap.InvoiceValue-ap.Value))+(isnull(pnd.AdjusmentValueToAffectBase,0)),gdvap.BaseValue+(isnull(pnd.AdjusmentValueToAffectBase,0)))* isnull(ppd.DiscountRate,0))/100 as decimal(18,2)),0) AS DiscountValue,
		ap.ServicePeriodDate as CxPRadicateDate,
		iif(gdvap.BaseValue is null or gdvap.BaseValue =0,((ap.Value+(ap.InvoiceValue-ap.Value))+(isnull(pnd.AdjusmentValueToAffectBase,0))),(gdvap.BaseValue+(isnull(pnd.AdjusmentValueToAffectBase,0)))) as BaseValue,
		gdvap.ValueTax,
		gdvap.RateValue,
		CAST(Common.GETDATE() AS DATETIME) AS PaymentDate,
		ISNULL(pnd.AdjusmentValue,0) ValueNote,
		ISNULL(AdjusmentValueToAffectBase,0) AdjusmentValueToAffectBase
	FROM Payments.AccountPayable AS ap WITH (NOLOCK)
	JOIN Common.Supplier AS s WITH (NOLOCK) ON ap.IdSupplier = s.Id
	OUTER APPLY (	SELECT  ppd.RangeName, ppd.DiscountRate
					from Common.PromptPaymentDiscount AS ppd  WITH (NOLOCK)
					where SupplierId =s.Id and (DATEDIFF(DAY,ap.ServicePeriodDate,CAST(Common.GETDATE() AS DATETIME))) BETWEEN ppd.InitialRank and ppd.EndRank
					) AS ppd
	OUTER APPLY (SELECT * from Payments.GetDetailValueAccountPayable(ap.EntityId,ap.EntityName,ap.BillNumber,ap.IdSupplier)) gdvap
	OUTER APPLY (SELECT	sum(iif(pn.AllowDiscountPromptPayment=1,pnapa.AdjusmentValue*iif(pn.Nature=1,1,-1),0)) AdjusmentValue,
						sum(iif(pn.AffectBaseToDiscount=1,pnapa.AdjusmentValue*iif(pn.Nature=1,-1,1),0)) AdjusmentValueToAffectBase
						FROM Payments.PaymentNotesAccountPayableAdvance pnapa 
						JOIN Payments.PaymentNotes pn on pnapa.PaymentNoteId=pn.Id
						where pnapa.AccountPayableId=ap.Id) pnd
	JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = s.IdThirdParty
	JOIN Common.SuppliersDistributionLines AS spd WITH (NOLOCK) ON ap.IdSuppliersDistributionLines = spd.Id
	JOIN Common.DistributionLines AS dl WITH (NOLOCK) ON  spd.IdDistributionLine = dl.Id
	JOIN Treasury.ExpenseConcepts AS ec WITH (NOLOCK) ON ec.Id = dl.ExpensesConceptId
	JOIN Common.SupplierType AS st WITH (NOLOCK) ON ap.SupplierTypeId = st.Id 
	JOIN Payments.AccountPayableShares AS aps ON aps.IdAccountPayable = ap.Id and aps.Balance > 0 
	JOIN Payments.SettingPayments as SP on SP.IdOperatingUnit = AP.IdOperatingUnit
	LEFT JOIN Payments.AgesPayments as AGP on AGP.SettingPaymentId = SP.Id and (DATEDIFF(DAY, APS.DateExpires, [Common].[GETDATE]()) + 1) BETWEEN AGP.InitialRange  and AGP.EndRange
	WHERE AP.Balance > 0 and AP.Status = 2) sc
	GROUP by	sc.SupplierId,sc.ThirdId,sc.SupplierName,sc.SupplierCode,sc.SupplierNit,sc.DescriptionLine,sc.DistributionLineId,sc.NatureExpenseConcept,
				sc.SupplierTypeId,sc.SupplierTypeName,sc.Invoice,sc.ExpirationDate,sc.AgePayment,sc.InvoiceBalance,sc.Share,sc.ShareExpirationDate,sc.BalanceShare,
				sc.AccountPayableId,sc.AccountPayableShareId,sc.AccountPayableCode,sc.CXPValue,sc.DiscountRate,sc.DiscountValue,sc.CxPRadicateDate,sc.BaseValue,
				sc.ValueTax,sc.RateValue,sc.PaymentDate,sc.ValueNote,sc.AdjusmentValueToAffectBase,sc.RangeNameDiscount
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cronograma o agenda de pagos pendientes a proveedores: consolida las cuentas por pagar confirmadas con saldo mayor a cero, sus cuotas vencidas o próximas a vencer, y calcula en tiempo real el descuento por pronto pago aplicable según el rango de días transcurridos desde la radicación de la factura. Integra datos del proveedor (NIT, nombre, tipo, línea de distribución, concepto de gasto), la factura o documento (número, valor, saldo, fecha de vencimiento, cuota), la base imponible y valores tributarios obtenidos desde la función de detalle de cuentas por pagar, y los ajustes de notas de pago (notas débito/crédito) que afectan el saldo base del descuento. Sirve para que el área de tesorería visualice qué facturas de proveedores están listas para pagar, cuánto descuento por pronto pago corresponde aplicar y cuál es el saldo real a cancelar por cuota, agrupado por antigüedad del vencimiento según la configuración de rangos de pagos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'View_SchedulePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'VIEW', @level1name = N'View_SchedulePayment';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la programación de pagos a proveedores listando cuotas vigentes de cuentas por pagar con su antigüedad, descuentos por pronto pago aplicables, ajustes por notas y bases tributarias para gestión de tesorería.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'View_SchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen cuentas por pagar (Payments.AccountPayable) con Balance > 0 y Status = 2 (estado activo/aprobado); Existen cuotas en Payments.AccountPayableShares con Balance > 0 asociadas a la cuenta por pagar; El proveedor está asociado a líneas de distribución (Common.SuppliersDistributionLines) y a un tipo de proveedor (Common.SupplierType); La unidad operativa de la cuenta por pagar tiene configuración en Payments.SettingPayments', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'View_SchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cuentas por pagar con saldo positivo (AP.Balance > 0) y estado = 2; Solo se incluyen cuotas con saldo pendiente (aps.Balance > 0); El descuento por pronto pago solo se calcula cuando la cuota cubre el total de la CxP ajustada por notas que permiten descuento; La fecha de pago expuesta es siempre la fecha actual del sistema (Common.GETDATE); Las tasas de descuento se exponen como DECIMAL(5,2) y los valores monetarios calculados como DECIMAL(18,2); El rango de antigüedad se cuenta desde la fecha de vencimiento de la cuota más 1 día; Las notas de pago solo afectan el cálculo cuando sus banderas AllowDiscountPromptPayment o AffectBaseToDiscount están activas, y su signo depende de pn.Nature', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'View_SchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar; Cuotas de pago; Proveedor; Tercero (NIT); Descuento por pronto pago; Antigüedad de pago (aging); Notas de pago (débito/crédito); Anticipos; Línea de distribución contable; Concepto de gasto; Retención / base tributaria; Programación de pagos / tesorería; Factura', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'View_SchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.View_SchedulePayment: Devuelve un set agrupado por las claves de cuenta por pagar/cuota/proveedor con valores de cuota, descuento por pronto pago, base de retención y antigüedad de pago', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'View_SchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ap.Value = (aps.Balance + ISNULL(pnd.AdjusmentValue,0)) — la cuota representa el total de la CxP ajustada por notas que permiten descuento → Se aplica el descuento por pronto pago: se exponen RangeNameDiscount, DiscountRate y se calcula DiscountValue sobre la base else RangeNameDiscount queda vacío y DiscountRate/DiscountValue quedan en 0 (no aplica descuento por pronto pago); si gdvap.BaseValue IS NULL OR gdvap.BaseValue = 0 → BaseValue se calcula como (ap.Value + (ap.InvoiceValue - ap.Value)) + AdjusmentValueToAffectBase de las notas else BaseValue se toma de gdvap.BaseValue + AdjusmentValueToAffectBase; si PaymentNotes.AllowDiscountPromptPayment = 1 → El AdjusmentValue de la nota se acumula (con signo según pn.Nature: +1 si Nature=1, -1 en otro caso) para ajustar el saldo comparado contra ap.Value else La nota no afecta el cálculo de aplicabilidad del descuento por pronto pago; si PaymentNotes.AffectBaseToDiscount = 1 → El AdjusmentValue de la nota se acumula (con signo invertido respecto a Nature) en AdjusmentValueToAffectBase para modificar la base del descuento else La nota no afecta la base del descuento; si DATEDIFF(DAY, ap.ServicePeriodDate, hoy) BETWEEN ppd.InitialRank AND ppd.EndRank para el SupplierId → Se asigna el rango de descuento por pronto pago correspondiente del proveedor else No hay descuento por pronto pago disponible (ppd queda nulo); si DATEDIFF(DAY, aps.DateExpires, hoy) + 1 BETWEEN AGP.InitialRange AND AGP.EndRange → Se asigna el AgePayment (tramo de antigüedad) correspondiente a la cuota else AgePayment queda NULL (LEFT JOIN sin coincidencia)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'View_SchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payments.GetDetailValueAccountPayable', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'View_SchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Common.Supplier; Common.PromptPaymentDiscount; Payments.GetDetailValueAccountPayable; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentNotes; Common.ThirdParty; Common.SuppliersDistributionLines; Common.DistributionLines; Treasury.ExpenseConcepts; Common.SupplierType; Payments.AccountPayableShares; Payments.SettingPayments; Payments.AgesPayments', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'View_SchedulePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'VIEW', @level1name=N'View_SchedulePayment';
GO
