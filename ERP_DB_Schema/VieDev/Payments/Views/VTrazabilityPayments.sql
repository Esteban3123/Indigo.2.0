






CREATE VIEW [Payments].[VTrazabilityPayments]
AS
SELECT      AT.id as Row, A.Id as AccountPayableId,T.Nit + ' - ' + T.Name AS ThirdParty, F.Code + ' - ' + F.Name AS FilingUnit, O.UnitCode + ' - ' + O.UnitName AS OperatingUnit, 
                         CASE A.status WHEN 1 THEN 'Radicado' WHEN 2 THEN 'Causado Sin Confirmar' WHEN 3 THEN 'Causado Confirmado' WHEN 4 THEN 'Anulado' END AS State, AT.Code AS CodeTransfer, 
                         ISNULL(FO.Code + ' - ' + FO.Name,F.Code + ' - ' + F.Name) AS FilingSource, FD.Code + ' - ' + FD.Name AS FilingTarget, 
                         CASE AT.status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Confirmado' WHEN 3 THEN 'Anulado' WHEN 4 THEN 'Evaluado' END AS StateTraslate, AT.CreationUser AS CreateUserOfficeTranfer, 
                         AT.ConfirmationDate AS ConfirmationDateOfficeTranfer, AT.ConfirmationUser AS ConfirmationUserOfficeTranfer, AT.AnnulmentDate AS AnnulmentDateOfficeTranfer, 
                         AT.AnnulmentUser AS AnnulmentUserOfficeTranfer, CASE ATD.status WHEN 1 THEN 'Pendiente por Aceptacion' WHEN 2 THEN 'Aceptada' WHEN 3 THEN 'Rechazada' WHEN 4 THEN 'Evaluado' END AS StateDetailTraslate, 
                         ATD.AcceptanceDate AS AcceptanceDateDetailTranfer, ATD.AcceptanceUser AS AcceptanceUserDetailTranfer, ATD.RejectionDate AS RejectionDateDetailTranfer, ATD.RejectionUser AS RejectionUserDetailTranfer, 
                         RR.Code + ' - ' + RR.Name AS RejectionReason, ATD.RejectionDescription,
						 A.code as AccountPayableCode,
						 A.BillNumber 
FROM            Payments.AccountPayable AS A  with (nolock) INNER JOIN
                         Common.Supplier AS S with (nolock) ON S.Id = A.IdSupplier INNER JOIN
                         Common.ThirdParty AS T with (nolock) ON T.Id = A.IdThirdParty INNER JOIN
                         Payments.FilingUnit AS F with (nolock) ON F.Id = A.FilingUnitId INNER JOIN
                         Common.OperatingUnit AS O with (nolock) ON O.Id = A.IdOperatingUnit LEFT OUTER JOIN
                         Payments.AccountPayableTransferDetail AS ATD with (nolock) ON A.Id = ATD.AccountPayableId LEFT OUTER JOIN
                         Payments.AccountPayableTransfer AS AT with (nolock) ON AT.Id = ATD.AccountPayableTransferId LEFT OUTER JOIN
                         Payments.FilingUnit AS FO with (nolock) ON FO.Id = AT.FilingUnitSourceId LEFT OUTER JOIN
                         Payments.FilingUnit AS FD with (nolock) ON FD.Id = AT.FilingUnitTargetId LEFT OUTER JOIN
                         Payments.AccountPayableRejectionReason AS RR with (nolock) ON RR.Id = ATD.RejectionReasonId