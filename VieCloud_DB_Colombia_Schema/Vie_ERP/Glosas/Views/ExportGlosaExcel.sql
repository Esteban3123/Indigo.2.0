

CREATE VIEW [Glosas].[ExportGlosaExcel]
AS
select ROW_NUMBER() OVER(ORDER BY invoicenumber,ServiceCode ASC) as Row ,* from (
select
d.Id, 
por.id as idInvoice ,
m.Id as CodeMovement,
d.InvoiceNumber, 
d.ServiceCode,
d.ServiceName,
'' as ServiceNameQX ,
d.CostCenterCode, 
d.CostCenterName,
m.MainGlosa, 
m.CodeGlosa,
conG.NameSpecific,
m.RationaleGlosa, 
m.ValueGlosado, 
m.ValueAcceptedFirstInstance, 
m.ValueReiterated, 
m.ValueReiterationBalance, 
m.ValueAcceptedSecondInstance,
m.ValueAcceptedIPSconciliation, 
m.ValueAcceptedEAPBconciliation,
m.ValuePendingConciliation, 
objc.RadicatedConsecutive,
objc.DocumentDate,
m.ValuePayments, 
por.InvoiceDate, 
por.RadicatedNumber,
por.RadicatedDate,
con.Code as CodeConceptEvaluation, 
con.NameSpecific as NameSpecificEvaluation, 
m.JustificationGlosaText,
por.IngressNumber,
por.IngressDate, 
por.PatientCode
from 
[Glosas].[GlosaObjectionsReceptionC] ObjC with (nolock) 
inner join [Glosas].[GlosaObjectionsReceptionD] objD with (nolock)  on objD.glosaObjectionsREceptionCid = ObjC.Id
inner join [Glosas].[GlosaPortfolioGlosada] Por with (nolock)  on por.id  =ObjD.portfolioGlosaId
inner join glosas.[GlosaInvoiceDetail] d with (nolock)  on d.ObjectionsReceptionDId = objD.id
inner join [Glosas].[GlosaMovementGlosa]  m with (nolock)  on m.invoiceDetailId = d.id
inner join [Common].[ConceptGlosas] conG with (nolock)  on cong.id  =m.CodeGlosaId
left join [Common].[ConceptGlosas] con with (nolock)  on con.id  =m.IdGlosaEvaluation
where m.InvoiceDetailIdqx is null 
Union all
select 
d.Id, 
por.id as idInvoice,
m.Id as CodeMovement,
d.InvoiceNumber, 
d.ServiceCode,
d.ServiceName,
qx.ServiceName as ServiceNameQX ,
d.CostCenterCode, 
d.CostCenterName,
m.MainGlosa, 
m.CodeGlosa,
conG.NameSpecific,
m.RationaleGlosa, 
m.ValueGlosado, 
m.ValueAcceptedFirstInstance, 
m.ValueReiterated, 
m.ValueReiterationBalance, 
m.ValueAcceptedSecondInstance,
m.ValueAcceptedIPSconciliation, 
m.ValueAcceptedEAPBconciliation,
m.ValuePendingConciliation, 
objc.RadicatedConsecutive,
objc.DocumentDate,
m.ValuePayments, 
por.InvoiceDate, 
por.RadicatedNumber,
por.RadicatedDate,
con.code as CodeConceptEvaluation, 
con.NameSpecific as NameSpecificEvaluation, 
m.JustificationGlosaText,
por.IngressNumber,
por.IngressDate, 
por.PatientCode
from 
[Glosas].[GlosaObjectionsReceptionC] ObjC with (nolock) 
inner join [Glosas].[GlosaObjectionsReceptionD] objD  with (nolock) on objD.glosaObjectionsREceptionCid = ObjC.Id
inner join [Glosas].[GlosaPortfolioGlosada] Por  with (nolock) on por.id  =ObjD.portfolioGlosaId
inner join glosas.[GlosaInvoiceDetail] d  with (nolock) on d.ObjectionsReceptionDId = objD.id
inner join [Glosas].[GlosaInvoiceDetailQX] qx  with (nolock) on d.id =qx.InvoiceDetailId 
inner join [Glosas].[GlosaMovementGlosa]  m  with (nolock) on m.invoiceDetailIdqx  = qx.id
inner join [Common].[ConceptGlosas] conG  with (nolock) on cong.id  =m.CodeGlosaId
left join [Common].[ConceptGlosas] con  with (nolock) on con.id  =m.IdGlosaEvaluation
) as Datos
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de exportación a Excel del proceso completo de glosas y objeciones. Integra en una sola consulta plana todos los movimientos de glosa asociados a ítems de factura, combinando los encabezados y detalles de recepciones de objeciones (radicados, fechas de documentos), la cartera de facturas glosadas (número de factura, fecha de radicación, número de ingreso, código del paciente) y los valores del ciclo completo de glosa: valor glosado, aceptado en primera instancia, reiterado, aceptado en segunda instancia, conciliado por IPS y EAPB, pendiente de conciliación y pagado. Incluye también el concepto de glosa aplicado y el concepto de evaluación (auditoría) con su código y nombre, así como la justificación textual de la glosa. Maneja dos tipos de servicios mediante UNION: servicios generales y servicios quirúrgicos (QX), mostrando en este último caso el nombre del procedimiento quirúrgico asociado. Sirve como fuente principal para reportes y exportaciones a Excel del área de cartera y auditoría de glosas, permitiendo el seguimiento financiero de cada ítem glosado ante aseguradoras (EPS/EAPB).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ExportGlosaExcel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ExportGlosaExcel';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista, lista para exportar a Excel, los movimientos de glosa con su detalle de factura, recepción de objeción, cartera glosada y conceptos asociados, diferenciando ítems no quirúrgicos y quirúrgicos (QX).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada movimiento de glosa debe tener un concepto de glosa válido en Common.ConceptGlosas (join interno por CodeGlosaId).; Cada detalle de factura debe estar ligado a un detalle de recepción de objeción, a una cartera glosada y a una recepción cabecera.; Para la rama quirúrgica, el detalle de factura debe tener registro en GlosaInvoiceDetailQX y el movimiento debe referenciarlo por InvoiceDetailIdqx.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un mismo movimiento de glosa aparece exactamente en una de las dos ramas: la no-QX cuando InvoiceDetailIdqx es NULL, y la QX cuando se enlaza por InvoiceDetailIdqx.; El concepto principal de la glosa (CodeGlosaId) siempre existe (INNER JOIN), mientras que el concepto de evaluación (IdGlosaEvaluation) puede ser nulo (LEFT JOIN).; Todas las consultas se hacen con NOLOCK, por lo que la vista admite lecturas sucias.; La numeración Row se reinicia/ordena globalmente sobre la unión de ambas ramas por InvoiceNumber, ServiceCode.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Objeción de glosa; Recepción de objeciones; Cartera glosada; Factura; Detalle de factura; Servicio quirúrgico (QX); Concepto de glosa; Radicado; Valor glosado; Valor aceptado primera instancia; Valor reiterado; Saldo de reiteración; Valor aceptado segunda instancia; Conciliación IPS; Conciliación EAPB; Valor pendiente de conciliación; Pagos; Centro de costo; Paciente; Ingreso', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un conjunto numerado con ROW_NUMBER() ordenado por InvoiceNumber y ServiceCode ascendente, uniendo movimientos no-QX y QX.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si m.InvoiceDetailIdqx IS NULL (movimiento de glosa no asociado a servicio quirúrgico) → Se incluye en la primera rama del UNION ALL con ServiceNameQX vacío y sin join a GlosaInvoiceDetailQX. else Se incluye en la segunda rama uniendo con GlosaInvoiceDetailQX para tomar ServiceName del registro quirúrgico (ServiceNameQX).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaObjectionsReceptionC; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaPortfolioGlosada; Glosas.GlosaInvoiceDetail; Glosas.GlosaMovementGlosa; Glosas.GlosaInvoiceDetailQX; Common.ConceptGlosas', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcel';
GO
