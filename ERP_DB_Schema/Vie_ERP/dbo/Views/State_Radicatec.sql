

create VIEW [dbo].[State_Radicatec]
AS
SELECT DISTINCT 
                         RC.RadicatedConsecutive AS Consecutivo, C.Nit, C.Name AS Entidad, DET.ContractCode AS Contrato, 
                         RC.DocumentDate AS FechaDocumento, RC.ConfirmDate AS FechaConfirmacion, 
                         CASE WHEN RC.State = '1' THEN 'Sin Confirmar' WHEN RC.State = '2' THEN 'Confirmado' WHEN RC.State = '4' THEN 'Anulado' END AS Estado, 
                         CAST(DET.Total AS money) AS TotalRadicado
FROM            Portfolio.RadicateInvoiceC AS RC INNER JOIN
                         Common.Customer AS C ON C.Id = RC.CustomerId INNER JOIN
                             (SELECT        RadicateInvoiceCId, ContractCode, SUM(BalanceInvoice) AS Total
                               FROM            Portfolio.RadicateInvoiceD
                               GROUP BY RadicateInvoiceCId, ContractCode) AS DET ON RC.Id = DET.RadicateInvoiceCId