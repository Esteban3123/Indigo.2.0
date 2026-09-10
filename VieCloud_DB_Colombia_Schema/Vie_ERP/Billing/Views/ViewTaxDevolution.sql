

CREATE VIEW [Billing].[ViewTaxDevolution]
AS
WITH cte_temp AS(SELECT gli.Id,dbo.Data PaymentMethodTypes
				from GeneralLedger.GeneralLedgerIVA gli WITH(NOLOCK)
				CROSS APPLY dbo.Split(gli.PaymentMethodTypes,',') dbo
				where gli.PaymentMethodTypes is not null
				group by gli.Id,dbo.Data,dbo.Id),

	cte_Sodd AS(SELECT sodd.RevenueControlDetailId, SUM(sodd.GrandTotalSalesPrice) GrandTotalSalesPrice
				FROM Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
				GROUP BY sodd.RevenueControlDetailId)

SELECT	CAST(CONCAT(sodd.RevenueControlDetailId,gli2.PaymentMethodTypes) AS INT) Id,
		sodd.RevenueControlDetailId,
		sum(sodd.taxes) TaxValue,
		cs.GrandTotalSalesPrice - sum(sodd.taxes)  ValueWithTaxDevolutionOfficialCurrency,
		cs.GrandTotalSalesPrice - sum(sodd.taxes)ValueWithTaxDevolution,
		CAST(gli2.PaymentMethodTypes AS TINYINT) PaymentMethodType,
		CASE gli2.PaymentMethodTypes
		WHEN '1' THEN 'Efectivo'
		WHEN '2' THEN 'Cheque'
		WHEN '3' THEN 'Tarjeta'
		WHEN '4' THEN 'Consignación'
		ELSE 'Otro' END PaymentMethodTypeName
FROM GeneralLedger.GeneralLedgerIVA gli WITH(NOLOCK)
JOIN (
		SELECT	rcd.Id RevenueControlDetailId,
				sod.IvaId,
				sum(sodd.GrandTotalTaxes) taxes
		from Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
		JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id=sodd.ServiceOrderDetailId
		JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id=sodd.RevenueControlDetailId
		where sod.IvaId is not null 
		GROUP BY rcd.Id,sod.IvaId) sodd ON sodd.IvaId= gli.Id
JOIN cte_temp gli2 ON gli.Id=gli2.Id
JOIN cte_Sodd cs on sodd.RevenueControlDetailId = cs.RevenueControlDetailId
WHERE gli.ApplyTaxDevolution=1
GROUP BY sodd.RevenueControlDetailId, gli2.PaymentMethodTypes,cs.GrandTotalSalesPrice

UNION ALL

SELECT CAST(CONCAT(cs.RevenueControlDetailId,0) AS INT) Id,
		cs.RevenueControlDetailId,
		0 TaxValue,
		cs.GrandTotalSalesPrice ValueWithTaxDevolutionOfficialCurrency,
		cs.GrandTotalSalesPrice ValueWithTaxDevolution,
		CAST(0 AS TINYINT) PaymentMethodType,
		'Otros'  PaymentMethodTypeName
from cte_Sodd cs
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que calcula la devolución de IVA (impuestos) aplicable a los ítems facturados en órdenes de servicio, agrupando los valores de impuestos y los totales de venta por folio de facturación (RevenueControlDetail) y por método de pago (efectivo, cheque, tarjeta, consignación u otro). Integra la configuración de IVA del libro contable (GeneralLedgerIVA) con el detalle de distribución financiera de órdenes de servicio (ServiceOrderDetailDistribution y ServiceOrderDetail) para determinar el monto de impuestos a devolver y el valor neto de la factura una vez aplicada dicha devolución. Solo aplica para registros donde el IVA tiene habilitada la devolución (ApplyTaxDevolution=1), e incluye además una fila resumen para folios sin devolución de impuesto (método de pago 0 - Otros). Es utilizada en procesos de liquidación, conciliación contable y reportería tributaria de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewTaxDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewTaxDevolution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por cada detalle de control de ingresos, el valor neto (sin impuestos) susceptible de devolución de IVA, desagregado por método de pago habilitado en el IVA y agregando una fila adicional ''Otros'' con el total bruto.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewTaxDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'GeneralLedger.GeneralLedgerIVA.PaymentMethodTypes debe contener una lista separada por comas (procesada por dbo.Split) para que el IVA aporte filas al resultado; Solo se consideran registros de IVA con ApplyTaxDevolution = 1; Billing.ServiceOrderDetail.IvaId no debe ser nulo para que el detalle participe en el cálculo de impuestos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewTaxDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id de cada fila se construye concatenando RevenueControlDetailId con el código de método de pago y casteando a INT (el método 0 corresponde a la fila ''Otros''); ValueWithTaxDevolutionOfficialCurrency y ValueWithTaxDevolution siempre se calculan como GrandTotalSalesPrice menos la suma de impuestos (taxes) del detalle; Los registros con PaymentMethodTypes nulo en GeneralLedgerIVA se excluyen (filtrados en cte_temp); Solo los detalles cuya orden de servicio tiene IvaId asignado contribuyen al TaxValue; PaymentMethodType se castea a TINYINT, limitando los códigos válidos al rango 0-255', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewTaxDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de IVA; Métodos de pago (Efectivo, Cheque, Tarjeta, Consignación, Otros); Control de ingresos / folios de facturación; Distribución de orden de servicio; Impuestos sobre ventas (GrandTotalTaxes)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewTaxDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewTaxDevolution: Por cada combinación (RevenueControlDetailId, método de pago habilitado en GeneralLedgerIVA con ApplyTaxDevolution=1) retorna una fila con el TaxValue (suma de GrandTotalTaxes) y el valor con devolución de IVA (GrandTotalSalesPrice - taxes); [RETURN_RESULT] Billing.ViewTaxDevolution: Mediante UNION ALL agrega una fila adicional por cada RevenueControlDetailId con PaymentMethodType=0, etiqueta ''Otros'', TaxValue=0 y valor igual al GrandTotalSalesPrice total (sin descontar impuestos)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewTaxDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si gli2.PaymentMethodTypes = ''1'' → PaymentMethodTypeName = ''Efectivo''; si gli2.PaymentMethodTypes = ''2'' → PaymentMethodTypeName = ''Cheque''; si gli2.PaymentMethodTypes = ''3'' → PaymentMethodTypeName = ''Tarjeta''; si gli2.PaymentMethodTypes = ''4'' → PaymentMethodTypeName = ''Consignación'' else Cualquier otro valor de PaymentMethodTypes se etiqueta como ''Otro''; si gli.ApplyTaxDevolution = 1 → El IVA participa en el cálculo de la devolución y genera filas por método de pago else El IVA se excluye del primer bloque del UNION (no aporta devolución por método de pago)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewTaxDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerIVA; dbo.Split; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Billing.RevenueControlDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewTaxDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewTaxDevolution';
GO
