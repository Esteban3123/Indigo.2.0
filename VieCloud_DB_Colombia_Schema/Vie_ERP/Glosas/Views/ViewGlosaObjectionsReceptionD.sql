

CREATE VIEW [Glosas].[ViewGlosaObjectionsReceptionD]
AS
SELECT 
	gord.GlosaObjectionsReceptionCId,
	d.InvoiceNumber, 
	d.Id, 
	cast(0 as varchar) as IdQx, 
	d.CostCenterCode, 
	d.ServiceCode, 
	d.ServiceName, 
	d.ValueServiceManual, 
	d.UnitValue, 
	d.Ammount, 
	ar.Balance BalanceInvoice,
	d.InvoicedValue, 
	ISNULL(IIF(ar.OpeningBalance = 0, id.ThirdPartySalesPrice, NULL), d.InvoicedValue) EntityValue, 
	ISNULL(IIF(ar.OpeningBalance = 0, id.SubTotalPatientSalesPrice, NULL), 0) PatientValue,
	gmg.CodeGlosa codeConcept,
	r.Code codeResponsale,
	gmg.ValueGlosado ValueGlosa,
	isnull(gmg.ValuePendingConciliation,0) ValuePendingConciliation,	
	gmg.RationaleGlosa,
	gord.DocumentType,
	'' comment
FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK) ON ar.AccountReceivableType = 2 AND ar.InvoiceNumber = gord.InvoiceNumber
JOIN Glosas.GlosaInvoiceDetail d WITH (NOLOCK) ON gord.InvoiceNumber = d.InvoiceNumber
LEFT JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON id.Id = d.InvoiceDetailNativeId
LEFT JOIN Glosas.GlosaInvoiceDetailQX qx WITH (NOLOCK) ON d.Id = qx.InvoiceDetailId  
LEFT JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON d.Id = gmg.InvoiceDetailId AND gmg.ValuePendingConciliation > 0
LEFT JOIN Glosas.Responsible r WITH (NOLOCK) ON gmg.ResponsibleId = r.Id
WHERE d.InvoicedValue > 0 AND qx.id IS NULL 

UNION ALL 

SELECT 
	gord.GlosaObjectionsReceptionCId,
	d.InvoiceNumber, 
	d.Id, 
	cast(d.ServiceCode as varchar) as IdQx, 
	qx.CostCenterCode, 
	qx.ServiceCode, 
	qx.ServiceName, 
	qx.ValueServiceManual, 
	qx.UnitValue, 
	qx.Ammount, 
	ar.Balance BalanceInvoice,
	qx.InvoicedValue, 
	ISNULL(IIF(ar.OpeningBalance = 0, id.ThirdPartySalesPrice, NULL), qx.InvoicedValue) EntityValue, 
	ISNULL(IIF(ar.OpeningBalance = 0, id.SubTotalPatientSalesPrice, NULL), 0) PatientValue,
	gmg.CodeGlosa codeConcept,
	r.Code codeResponsale,
	gmg.ValueGlosado ValueGlosa,
	isnull(gmg.ValuePendingConciliation,0) ValuePendingConciliation,	
	gmg.RationaleGlosa,
	gord.DocumentType,
	gmg.RationaleGlosa comment
FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK) ON ar.AccountReceivableType = 2 AND ar.InvoiceNumber = gord.InvoiceNumber
JOIN Glosas.GlosaInvoiceDetail d WITH (NOLOCK) ON gord.InvoiceNumber = d.InvoiceNumber
JOIN Glosas.GlosaInvoiceDetailQX qx WITH (NOLOCK) ON d.Id = qx.InvoiceDetailId
LEFT JOIN Billing.InvoiceDetail id with(NOLOCK) ON id.Id = d.InvoiceDetailNativeId
LEFT JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON d.Id = gmg.InvoiceDetailId AND qx.Id = gmg.InvoiceDetailIdQX AND gmg.ValuePendingConciliation > 0
LEFT JOIN Glosas.Responsible r WITH (NOLOCK) ON gmg.ResponsibleId = r.Id
WHERE qx.InvoicedValue > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de ítems glosados asociados a recepciones de objeciones, integrando información de facturas (servicios generales y procedimientos quirúrgicos de sala de cirugías), cuentas por cobrar de cartera, movimientos de glosa con valores pendientes de conciliación y responsables asignados. Combina mediante UNION ALL dos perspectivas: ítems de factura no quirúrgicos y sus equivalentes quirúrgicos (GlosaInvoiceDetailQX), mostrando para cada línea el número de factura, código y nombre del servicio, valores facturados, saldo de la cuenta por cobrar, valor glosado, valor pendiente de conciliación, justificación de la glosa, tipo de documento y responsable. Sirve como fuente de reportería y auditoría del proceso de objeciones de glosas, permitiendo identificar cuánto dinero está en disputa con la aseguradora o pagador (EPS/EAPB) a nivel de ítem facturado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ViewGlosaObjectionsReceptionD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ViewGlosaObjectionsReceptionD';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el detalle de ítems facturados objetados por glosa (servicios generales y quirúrgicos) con sus valores facturados, glosados, pendientes de conciliación y la distribución entre entidad y paciente, para la recepción de objeciones.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosaObjectionsReceptionD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen cuentas por cobrar en Portfolio.AccountReceivable con AccountReceivableType = 2 (cartera de glosas/terceros); Las facturas referenciadas en Glosas.GlosaObjectionsReceptionD existen en Glosas.GlosaInvoiceDetail por InvoiceNumber; Para la rama quirúrgica, debe existir registro en Glosas.GlosaInvoiceDetailQX vinculado al detalle de factura', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosaObjectionsReceptionD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera cuentas por cobrar de tipo 2 (AccountReceivableType = 2); Excluye ítems con InvoicedValue <= 0 en ambas ramas; Un mismo ítem aparece o como general (sin QX) o como quirúrgico (con QX), nunca duplicado, gracias a la condición qx.id IS NULL en la primera rama; ValuePendingConciliation se expone como 0 cuando es NULL; Solo se vinculan movimientos de glosa con valor pendiente de conciliación mayor a cero; PatientValue es 0 cuando OpeningBalance ≠ 0; la separación entidad/paciente solo aplica a cuentas con saldo inicial cero', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosaObjectionsReceptionD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Objeción de glosa; Recepción de objeciones; Cuenta por cobrar; Factura; Detalle de factura; Servicio quirúrgico (QX); Valor glosado; Valor pendiente de conciliación; Responsable de glosa; Valor entidad vs valor paciente; Centro de costo', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosaObjectionsReceptionD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Solo es vista de lectura; no modifica datos.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosaObjectionsReceptionD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.OpeningBalance = 0 (la cuenta por cobrar fue abierta sin saldo inicial) → EntityValue = id.ThirdPartySalesPrice y PatientValue = id.SubTotalPatientSalesPrice (se separa el valor entre tercero y paciente desde Billing.InvoiceDetail) else EntityValue = InvoicedValue del detalle de glosa y PatientValue = 0 (todo el valor se asigna a la entidad); si El detalle de factura NO tiene registro asociado en GlosaInvoiceDetailQX (qx.id IS NULL) y InvoicedValue > 0 → Se incluye como ítem de servicio general usando los valores de GlosaInvoiceDetail; IdQx se devuelve como ''0'' y comment vacío else Si tiene registro QX e InvoicedValue > 0, se incluye como ítem quirúrgico usando los valores de GlosaInvoiceDetailQX; IdQx = ServiceCode y comment = RationaleGlosa; si gmg.ValuePendingConciliation > 0 en el join con GlosaMovementGlosa → Se asocia el movimiento de glosa (código, responsable, valor glosado, justificación); de lo contrario los campos de glosa quedan NULL por LEFT JOIN', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosaObjectionsReceptionD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaInvoiceDetail; Billing.InvoiceDetail; Glosas.GlosaInvoiceDetailQX; Glosas.GlosaMovementGlosa; Glosas.Responsible', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosaObjectionsReceptionD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ViewGlosaObjectionsReceptionD';
GO
