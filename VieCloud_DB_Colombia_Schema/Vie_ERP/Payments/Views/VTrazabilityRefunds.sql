
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trazabilidad completa de reembolsos o devoluciones de dinero a lo largo de su ciclo de traslado entre unidades de radicación. Integra cada reembolso registrado en tesorería con su unidad de radicación de origen, el traslado de cuenta por pagar al que fue asociado, la unidad fuente y la unidad destino de dicho traslado, y el detalle de aceptación o rechazo de cada ítem transferido incluyendo el motivo de rechazo. Expone el estado del traslado (Registrado, Confirmado, Anulado, Evaluado) y el estado del detalle (Pendiente por Aceptación, Aceptada, Rechazada, Evaluado), junto con los usuarios y fechas de cada acción (creación, confirmación, anulación, aceptación, rechazo). Sirve para reportería y seguimiento operativo de devoluciones de dinero, permitiendo auditar quién movió cada reembolso, entre qué unidades de radicación circuló y por qué fue rechazado si aplica.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VTrazabilityRefunds';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VTrazabilityRefunds';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de trazabilidad que consolida reembolsos con sus traslados de cuentas por pagar entre unidades de radicación, mostrando estados y responsables de confirmación, aceptación o rechazo.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityRefunds';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen reembolsos en Treasury.Refunds con unidad de radicación válida en Payments.FilingUnit', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityRefunds';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los estados numéricos de traslado (1-4) y de detalle (1-4) tienen un mapeo fijo a etiquetas de negocio; Un reembolso siempre se presenta con su unidad de radicación principal aunque no tenga transferencia asociada; La unidad de radicación destino (FilingTarget) y el motivo de rechazo solo aparecen cuando existe una transferencia/detalle con dichos datos; Las consultas se hacen con NOLOCK, permitiendo lecturas sucias', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityRefunds';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reembolso; Unidad de radicación; Transferencia de cuentas por pagar; Traslado entre unidades; Aceptación/Rechazo de cuenta por pagar; Motivo de rechazo; Confirmación y anulación de traslado; Trazabilidad', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityRefunds';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve un registro por cada combinación reembolso/detalle de transferencia, incluyendo reembolsos sin transferencia asociada (LEFT JOIN sobre AccountPayableTransferDetail)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityRefunds';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AT.status = 1 → Estado de traslado se etiqueta como ''Registrado''; si AT.status = 2 → Estado de traslado se etiqueta como ''Confirmado''; si AT.status = 3 → Estado de traslado se etiqueta como ''Anulado''; si AT.status = 4 → Estado de traslado se etiqueta como ''Evaluado''; si ATD.status = 1 → Estado del detalle se etiqueta como ''Pendiente por Aceptacion''; si ATD.status = 2 → Estado del detalle se etiqueta como ''Aceptada''; si ATD.status = 3 → Estado del detalle se etiqueta como ''Rechazada''; si ATD.status = 4 → Estado del detalle se etiqueta como ''Evaluado''; si FO (FilingUnit origen) es NULL en la transferencia → Se usa la unidad de radicación del reembolso (F) como FilingSource else Se usa la unidad de radicación origen de la transferencia (FO)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityRefunds';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.Refunds; Payments.FilingUnit; Payments.AccountPayableTransferDetail; Payments.AccountPayableTransfer; Payments.AccountPayableRejectionReason', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityRefunds';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityRefunds';
GO
