

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trazabilidad completa del ciclo de vida de las cuentas por pagar y sus traslados entre unidades de radicación. Integra la cuenta por pagar con el tercero o proveedor (NIT y nombre), la unidad de radicación origen y destino, la sede u unidad operativa, y el estado de cada documento (Radicado, Causado Sin Confirmar, Causado Confirmado, Anulado). Muestra el seguimiento detallado de cada transferencia o traslado de cuenta: estado del traslado (Registrado, Confirmado, Anulado, Evaluado), usuarios y fechas de creación, confirmación y anulación del traslado, así como el estado de aceptación o rechazo de cada ítem transferido (Pendiente por Aceptación, Aceptada, Rechazada), incluyendo el motivo y descripción del rechazo. Útil para reportería y auditoría del proceso de pagos a proveedores, control de traslados entre oficinas o sedes, y seguimiento de facturas o documentos de cobro por número de cuenta y número de factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VTrazabilityPayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'VTrazabilityPayments';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la trazabilidad de cuentas por pagar y sus traslados entre unidades de radicación, mostrando estados decodificados, usuarios y fechas de confirmación, aceptación o rechazo.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityPayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta por pagar debe tener proveedor, tercero, unidad de radicación y unidad operativa asociados (INNER JOIN); Los datos de traslado (transferencia, unidad origen/destino, motivo de rechazo) son opcionales: se muestran solo si existen registros relacionados (LEFT OUTER JOIN)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityPayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los estados numéricos de cuenta por pagar, traslado y detalle se traducen siempre a etiquetas legibles en español; Los códigos de tercero, unidad de radicación, unidad operativa y motivo de rechazo se presentan concatenados con su nombre en formato ''Codigo - Nombre''; El FilingSource nunca queda nulo si existe la cuenta por pagar: si no hay traslado, se usa la unidad de radicación original; Solo se incluyen cuentas por pagar con proveedor, tercero, unidad de radicación y unidad operativa válidos', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityPayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Proveedor; Tercero; Unidad de radicación; Unidad operativa; Traslado/transferencia de cuentas por pagar; Aceptación y rechazo de transferencia; Motivo de rechazo; Radicación; Causación; Anulación; Confirmación; Trazabilidad de pagos', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityPayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas combinando cuentas por pagar con sus detalles de transferencia; si una cuenta no tiene transferencia, los campos de traslado quedan en NULL', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityPayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AccountPayable.status = 1 → Estado ''Radicado''; si AccountPayable.status = 2 → Estado ''Causado Sin Confirmar''; si AccountPayable.status = 3 → Estado ''Causado Confirmado''; si AccountPayable.status = 4 → Estado ''Anulado''; si AccountPayableTransfer.status = 1 → Estado del traslado ''Registrado''; si AccountPayableTransfer.status = 2 → Estado del traslado ''Confirmado''; si AccountPayableTransfer.status = 3 → Estado del traslado ''Anulado''; si AccountPayableTransfer.status = 4 → Estado del traslado ''Evaluado''; si AccountPayableTransferDetail.status = 1 → Estado detalle ''Pendiente por Aceptacion''; si AccountPayableTransferDetail.status = 2 → Estado detalle ''Aceptada''; si AccountPayableTransferDetail.status = 3 → Estado detalle ''Rechazada''; si AccountPayableTransferDetail.status = 4 → Estado detalle ''Evaluado''; si Existe FilingUnitSource en la transferencia (FO no nulo) → Se muestra como FilingSource la unidad origen del traslado else Se muestra la unidad de radicación propia de la cuenta por pagar (ISNULL)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityPayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Common.Supplier; Common.ThirdParty; Payments.FilingUnit; Common.OperatingUnit; Payments.AccountPayableTransferDetail; Payments.AccountPayableTransfer; Payments.AccountPayableRejectionReason', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityPayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'VTrazabilityPayments';
GO
