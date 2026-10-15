
create VIEW [Payments].[VTrazabilityRefunds]
AS

SELECT AT.id as Row, R.Id as RefundId, R.Code as RefundCode, F.Code + ' - ' + F.Name AS FilingUnit,  
AT.Code AS CodeTransfer, ISNULL(FO.Code + ' - ' + FO.Name,F.Code + ' - ' + F.Name) AS FilingSource, FD.Code + ' - ' + FD.Name AS FilingTarget, 
CASE AT.status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Confirmado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'Evaluado' END AS StateTraslate, AT.CreationUser AS CreateUserOfficeTranfer, 
AT.ConfirmationDate AS ConfirmationDateOfficeTranfer, AT.ConfirmationUser AS ConfirmationUserOfficeTranfer, AT.AnnulmentDate AS AnnulmentDateOfficeTranfer, 
AT.AnnulmentUser AS AnnulmentUserOfficeTranfer, CASE ATD.status WHEN 1 THEN 'Pendiente por Aceptacion' WHEN 2 THEN 'Aceptada' WHEN 3 THEN 'Rechazada' WHEN 4 THEN 'Evaluado' END AS StateDetailTraslate, 
ATD.AcceptanceDate AS AcceptanceDateDetailTranfer, ATD.AcceptanceUser AS AcceptanceUserDetailTranfer, ATD.RejectionDate AS RejectionDateDetailTranfer, ATD.RejectionUser AS RejectionUserDetailTranfer, 
RR.Code + ' - ' + RR.Name AS RejectionReason, ATD.RejectionDescription 
FROM Treasury.Refunds AS R  with (nolock) INNER JOIN
Payments.FilingUnit AS F with (nolock) ON F.Id = R.FilingUnitId LEFT OUTER JOIN
Payments.AccountPayableTransferDetail AS ATD with (nolock) ON R.Id = ATD.RefundId LEFT OUTER JOIN
Payments.AccountPayableTransfer AS AT with (nolock) ON AT.Id = ATD.AccountPayableTransferId LEFT OUTER JOIN
Payments.FilingUnit AS FO with (nolock) ON FO.Id = AT.FilingUnitSourceId LEFT OUTER JOIN
Payments.FilingUnit AS FD with (nolock) ON FD.Id = AT.FilingUnitTargetId LEFT OUTER JOIN
Payments.AccountPayableRejectionReason AS RR with (nolock) ON RR.Id = ATD.RejectionReasonId