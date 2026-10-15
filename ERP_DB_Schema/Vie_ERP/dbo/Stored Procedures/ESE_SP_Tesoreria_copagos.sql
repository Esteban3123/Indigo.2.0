
CREATE PROCEDURE [dbo].[ESE_SP_Tesoreria_copagos]

@FechaIni DateTime,
@FechaFin DateTime

AS

select  DISTINCT case when PatientType=1 then 'Contributivo'  when PatientType=2 then 'Subsidiado' when PatientType=3 then 'Vinculado'
when PatientType=4 then 'Particular' when PatientType=5 then 'Otro'   when PatientType=6 then 'Desplazado Reg. Contributivo'
when PatientType=7 then 'Desplazado Reg. Subsidiado'  when PatientType=8 then 'Desplazado No Asegurado'  end TipoPaciente, a.code,convert(numeric(18,0),b.value) valor
from [Treasury].[CashReceipts] a
inner join [Treasury].[CashReceiptDetails] b on a.Id=b.IdCashReceipt and a.Status=2 
inner join Treasury.CashReceiptConcepts  c on c.id=b.IdCashReceiptConcept  and c.code in ('019','031')
left join Portfolio.PortfolioAdvance d on d.CashReceiptDetailId=b.Id  and d.CashReceiptId=a.Id
left join [Portfolio].[PortfolioTransfer] e on e.PortfolioAdvanceId=d.id
left join [Portfolio].[PortfolioTransferDetail] f on f.PortfolioTrasferId=e.Id
left join Billing.Invoice h on h.AdmissionNumber=d.AdmissionNumber  and h.status=1  
---left join Portfolio.AccountReceivable g on g.Id=f.AccountReceivableId  and g.InvoiceNumber=h.InvoiceNumber
where a.DocumentDate>= @FechaIni  and  a.DocumentDate<= @FechaFin