


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