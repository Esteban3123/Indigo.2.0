

CREATE VIEW [Glosas].[ExportGlosaExcelAll]
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
left join [Glosas].[GlosaMovementGlosa]  m with (nolock)  on m.invoiceDetailId = d.id
left join [Common].[ConceptGlosas] conG  with (nolock) on cong.id  =m.CodeGlosaId
left join [Common].[ConceptGlosas] con  with (nolock) on con.id  =m.IdGlosaEvaluation
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
left join [Glosas].[GlosaMovementGlosa]  m  with (nolock) on m.invoiceDetailIdqx  = qx.id
left join [Common].[ConceptGlosas] conG  with (nolock) on cong.id  =m.CodeGlosaId
left join [Common].[ConceptGlosas] con with (nolock)  on con.id  =m.IdGlosaEvaluation
) as Datos
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de exportación integral del ciclo de glosas para reportes en Excel. Consolida en un único resultado toda la información del proceso de objeciones a glosas: combina los documentos de objeción radicados por la entidad pagadora (EPS/aseguradora), el detalle de cada ítem glosado, la cartera de facturas afectadas y los movimientos individuales de glosa con sus valores glosados, aceptados, reiterados y conciliados en cada instancia (primera instancia, reiteración, conciliación IPS y EAPB). Incluye tanto ítems de factura regulares como ítems quirúrgicos (QX) mediante un UNION, enriqueciendo cada fila con el concepto de glosa, el código y nombre del servicio, centro de costo, número de factura, número de radicado, fechas de radicación, número de ingreso y código del paciente. Esta vista es la fuente principal para los informes de seguimiento financiero de glosas, auditoría de facturación y control de cartera glosada ante aseguradoras y EPS.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ExportGlosaExcelAll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'VIEW', @level1name = N'ExportGlosaExcelAll';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado numerado todos los movimientos de glosa (servicios normales y quirúrgicos) con su detalle de factura, recepción de objeción, cartera glosada y conceptos asociados, para exportación a Excel.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcelAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen recepciones de objeciones (cabecera y detalle) ligadas a una cartera glosada (GlosaPortfolioGlosada).; Cada detalle de factura está vinculado al detalle de objeción mediante ObjectionsReceptionDId.; Para la rama quirúrgica debe existir registro en GlosaInvoiceDetailQX asociado al detalle de factura.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcelAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila resultante representa un movimiento de glosa único (quirúrgico o no) sin duplicar entre ramas, dado que la primera excluye los que tienen InvoiceDetailIdqx y la segunda los incluye explícitamente.; Se utiliza NOLOCK en todas las tablas, permitiendo lecturas sucias para reportería.; Los conceptos de glosa (código y evaluación) se resuelven vía LEFT JOIN, por lo que pueden venir nulos sin excluir el movimiento.; La numeración Row es global sobre el conjunto unido, no por rama.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcelAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Objeción de glosa; Cartera glosada; Factura radicada; Concepto de glosa; Servicio quirúrgico (QX); Centro de costo; Conciliación IPS/EAPB; Reiteración de glosa; Aceptación primera/segunda instancia; Paciente; Ingreso', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcelAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un conjunto unificado (UNION ALL) numerado por ROW_NUMBER ordenado por InvoiceNumber y ServiceCode ascendente.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcelAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si GlosaMovementGlosa.InvoiceDetailIdqx IS NULL → Se incluye el movimiento como servicio no quirúrgico, con ServiceNameQX vacío y join al detalle de factura por d.Id. else Se incluye el movimiento como servicio quirúrgico, tomando ServiceName desde GlosaInvoiceDetailQX y enlazando GlosaMovementGlosa por invoiceDetailIdqx.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcelAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaObjectionsReceptionC; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaPortfolioGlosada; Glosas.GlosaInvoiceDetail; Glosas.GlosaMovementGlosa; Glosas.GlosaInvoiceDetailQX; Common.ConceptGlosas', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcelAll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'VIEW', @level1name=N'ExportGlosaExcelAll';
GO
