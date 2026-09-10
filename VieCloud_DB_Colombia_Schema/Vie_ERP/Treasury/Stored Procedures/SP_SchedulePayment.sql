-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 15-08-2014
-- Description:	sp para consultar la programacion de pagos
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SchedulePayment]	
	@SupplierTypeIdList varchar(MAX), --contiene los ids de supplierType en formato: 1,2,3,4,5,6
	@PaymentD  VARCHAR(50)
AS
BEGIN	
	SET NOCOUNT ON;

	IF @PaymentD IS NULL
		SET @PaymentD  =[Common].[GETDATE]();
		
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
	ap.Code AS AccountPayableCode,
	ap.Value As CXPValue,
	iif(gppcxp.BaseValue > 1,isnull(ppd.RangeName,''),'') AS RangeNameDiscount,
	iif(gppcxp.BaseValue > 1,cast(isnull(ppd.DiscountRate,0) as DECIMAL(5,2)),0) AS DiscountRate,
	iif(gppcxp.BaseValue > 1, cast(gppcxp.BaseValue * isnull(ppd.DiscountRate,0)/100 as decimal(18,2)),0) AS DiscountValue,
	ap.ServicePeriodDate as CxPRadicateDate,
	gppcxp.BaseValue as BaseValue,
	gppcxp.ValueTax,
	gppcxp.RateValue,
	CAST(@PaymentD AS DATETIME) AS PaymentDate,
	ISNULL(pnd.AdjusmentValue,0) ValueNote,
	ISNULL(pnd.AdjusmentValueToAffectBase,0) AdjusmentValueToAffectBase,
	isnull(c.Id, ccy.Id) as CurrencyId,
	isnull(c.Abbreviation, ccy.Abbreviation) as CurrencyAbbreviation
FROM Payments.AccountPayable AS ap WITH (NOLOCK)
JOIN Common.Supplier AS s WITH (NOLOCK) ON ap.IdSupplier = s.Id
OUTER APPLY (	SELECT  ppd.RangeName, ppd.DiscountRate
				from Common.PromptPaymentDiscount AS ppd  WITH (NOLOCK)
				where SupplierId =s.Id and (DATEDIFF(DAY,ap.ServicePeriodDate,CAST(@PaymentD AS DATETIME))) BETWEEN ppd.InitialRank and ppd.EndRank
				) AS ppd
OUTER APPLY (SELECT * from Payments.GetPromptPaymentCxP(ap.EntityId,ap.EntityName,ap.IdSupplier,ap.BillNumber,ap.Id)) gppcxp
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
LEFT JOIN Common.Currency c on c.Id = ap.CurrencyId
OUTER APPLY (
	SELECT TOP 1 cr.Id, cr.Abbreviation 
	FROM GeneralLedger.CompanySettings cs
	JOIN Common.Currency cr on cs.OfficialCurrencyId = cr.Id
) AS ccy
WHERE AP.Balance > 0 and AP.Status = 2 
and ST.Id in (IIF(@SupplierTypeIdList = '', (ST.Id), (SELECT Item from dbo.SplitStrings_Moden(@SupplierTypeIdList, ','))))
order by ap.BillNumber

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta la programación de pagos pendientes a proveedores, mostrando cada factura (cuenta por pagar) con sus cuotas vigentes, saldos, fechas de vencimiento y envejecimiento de cartera. Integra información del proveedor, su NIT, tipo de proveedor, línea de distribución contable y concepto de gasto asociado a cada factura. Calcula descuentos por pronto pago según la tabla de rangos configurada para cada proveedor (días transcurridos desde la radicación hasta la fecha de pago), así como el impacto de notas de pago (débito y crédito) sobre la base y el valor del descuento. Recibe opcionalmente una fecha de pago y una lista de tipos de proveedor para filtrar, y devuelve el listado ordenado por número de factura, listo para ser usado en la programación y ejecución de pagos a terceros.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SchedulePayment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SchedulePayment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta la programación de pagos a proveedores listando cuentas por pagar pendientes, sus cuotas, antigüedad, descuentos por pronto pago y ajustes por notas, para una fecha de pago dada y filtrando por tipos de proveedor.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas por pagar consideradas deben tener Balance > 0 y Status = 2 (activas/pendientes); Las cuotas (AccountPayableShares) deben tener Balance > 0 para ser incluidas; Debe existir configuración de pagos (SettingPayments) para la unidad operativa de la cuenta por pagar; @SupplierTypeIdList debe venir como cadena con ids separados por coma (formato ''1,2,3'') o vacío para no filtrar; Si @PaymentD es NULL se asume la fecha actual del sistema vía Common.GETDATE()', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El descuento por pronto pago solo aplica cuando los días entre ServicePeriodDate y la fecha de pago caen dentro del rango [InitialRank, EndRank] configurado para el proveedor en PromptPaymentDiscount; La antigüedad (AgePayment) se determina por el rango [InitialRange, EndRange] que contiene (DATEDIFF(día, DateExpires, hoy) + 1) sobre la configuración de pagos de la unidad operativa; Solo se devuelven obligaciones vigentes: factura activa (Status=2) con saldo y al menos una cuota con saldo positivo; Los campos PayValue, PaymentConceptId, SchedulePaymentDetailId y PaymentPercent se devuelven siempre en cero (placeholders para captura posterior); El cálculo de descuento (DiscountValue) se redondea a DECIMAL(18,2) y la tasa a DECIMAL(5,2); La moneda de respaldo siempre es la moneda oficial definida en CompanySettings cuando la CxP no tiene moneda asociada', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar; Cuotas de pago; Proveedores; Tipo de proveedor; Descuento por pronto pago; Notas de pago (débito/crédito); Antigüedad de pagos (aging); Líneas de distribución contable; Conceptos de gasto; Moneda y moneda oficial de la compañía; Programación de pagos; Base gravable y tasa de impuesto', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas DISTINCT con datos del proveedor, factura, cuota, antigüedad de pago, descuento por pronto pago calculado y moneda, solo cuando AP.Balance > 0 AND AP.Status = 2 AND APS.Balance > 0', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @PaymentD IS NULL → Se asigna la fecha actual del sistema vía Common.GETDATE() como fecha de pago else Se conserva la fecha proporcionada; si @SupplierTypeIdList = '''' → No se filtra por tipo de proveedor (se incluyen todos: ST.Id IN (ST.Id)) else Se filtran proveedores cuyo SupplierType.Id esté en la lista parseada por dbo.SplitStrings_Moden; si gppcxp.BaseValue > 1 → Se calculan RangeNameDiscount, DiscountRate y DiscountValue (BaseValue * DiscountRate / 100) por pronto pago else Se devuelven '''', 0 y 0 respectivamente, anulando el descuento por pronto pago; si PaymentNotes.AllowDiscountPromptPayment = 1 → El AdjusmentValue de la nota se suma a ValueNote con signo según pn.Nature (Nature=1 suma, distinto resta) else No aporta a ValueNote; si PaymentNotes.AffectBaseToDiscount = 1 → El AdjusmentValue se acumula en AdjusmentValueToAffectBase con signo invertido según pn.Nature (Nature=1 resta, distinto suma) else No afecta la base de descuento; si ap.CurrencyId no está disponible (LEFT JOIN sin match en Common.Currency) → Se usa la moneda oficial de la compañía obtenida de GeneralLedger.CompanySettings.OfficialCurrencyId else Se usa la moneda propia de la cuenta por pagar', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; Payments.GetPromptPaymentCxP; dbo.SplitStrings_Moden', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Common.Supplier; Common.PromptPaymentDiscount; Payments.PaymentNotesAccountPayableAdvance; Payments.PaymentNotes; Common.ThirdParty; Common.SuppliersDistributionLines; Common.DistributionLines; Treasury.ExpenseConcepts; Common.SupplierType; Payments.AccountPayableShares; Payments.SettingPayments; Payments.AgesPayments; Common.Currency; GeneralLedger.CompanySettings', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SchedulePayment';
-- GO
