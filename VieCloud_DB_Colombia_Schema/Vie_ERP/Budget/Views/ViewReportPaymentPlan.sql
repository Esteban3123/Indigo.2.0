CREATE VIEW [Budget].[ViewReportPaymentPlan]
AS
SELECT
 ROW_NUMBER() OVER (ORDER BY bc.Code) AS AutoIncrementing,
 bc.Code,
 bc.Status,
 bc.Name AS 'spendingCategoryName',
 ba.Code AS 'availabilityNumber',
 bcm.Code AS 'numberCommitment',
 bo.Code AS 'obligationNumber',
 bpo.Document AS 'paymentNumber',
 thi.Nit,
 thi.Name,
 bo.Observations,
 bpo.DocumentDate AS 'PaymentDate',
 bad.InitialValue,
 bcmd.InitialValue AS 'commitmentValue',
 bod.InitialValue AS 'obligationValue', 
 bpod.TotalPaymentOrder,
 bod.Balance,
 bbv.Id AS 'Validity',
 bc.Code AS 'Category',
 brt.[Type]
FROM
Budget.AvailabilityDetail AS bad
INNER JOIN Budget.Availability AS ba ON bad.AvailabilityId = ba.Id
INNER JOIN Budget.BudgetaryValidity AS bbv ON ba.BudgetaryValidityId = bbv.Id
INNER JOIN Budget.CommitmentDetail AS bcmd ON bcmd.AvailabilityDetailId = bad.Id
INNER JOIN Budget.RevenueType AS brt ON bcmd.RevenueTypeId = brt.Id
INNER JOIN Budget.Commitment AS bcm ON bcmd.CommitmentId = bcm.Id
INNER JOIN Budget.ObligationDetail AS bod ON bod.CategoryId = bcmd.CategoryId--
--INNER JOIN Budget.ObligationDetail AS bod ON bod.CommitmentDetailId = bcmd.Id
INNER JOIN Budget.Budget AS bb ON bad.BudgetId = bb.Id
INNER JOIN Budget.PaymentOrderDetail AS bpod ON bpod.ObligationDetailId = bod.Id
INNER JOIN Budget.PaymentOrder AS bpo ON  bpod.PaymentOrderId = bpo.Id
INNER JOIN Budget.Obligation AS bo ON bod.ObligationId = bo.Id
INNER JOIN Common.ThirdParty AS thi ON bo.ThirdPartyId = thi.Id--THI
INNER JOIN Budget.Category AS bc ON bb.CategoryId = bc.Id
--INNER JOIN Budget.BudgetaryEntity AS bbe ON bbe.ThirdPartyId = thi.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el ciclo completo de ejecución presupuestal de gastos, integrando en una sola fila el recorrido de cada rubro desde la disponibilidad presupuestal (CDP), pasando por el compromiso, la obligación y la orden de pago final. Combina información de la categoría de gasto, la vigencia presupuestal, el tipo de renta, el tercero beneficiario (proveedor o contratista con su NIT y nombre) y los valores en cada etapa (valor inicial del CDP, valor comprometido, valor obligado, total de la orden de pago y saldo pendiente). Está diseñada para reportería del plan de pagos presupuestales, permitiendo rastrear cuánto se ha disponibilizado, comprometido, obligado y pagado por categoría de gasto, vigencia y tercero dentro del presupuesto institucional.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewReportPaymentPlan';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewReportPaymentPlan';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fila la trazabilidad de la cadena de ejecución presupuestal (CDP → compromiso → obligación → orden de pago) con datos del tercero, vigencia y categoría, para reportes de plan de pagos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportPaymentPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros encadenados en AvailabilityDetail, Availability, BudgetaryValidity, CommitmentDetail, RevenueType, Commitment, ObligationDetail, Budget, PaymentOrderDetail, PaymentOrder, Obligation, ThirdParty y Category; cualquier ausencia en la cadena excluye la fila.; El tercero referenciado por la obligación debe existir en Common.ThirdParty.; La categoría del detalle de compromiso (CategoryId) debe coincidir con la categoría del detalle de obligación para que la fila aparezca.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportPaymentPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El enlace entre compromiso y obligación se realiza por CategoryId (bod.CategoryId = bcmd.CategoryId) y NO por CommitmentDetailId, lo que puede producir múltiples obligaciones por compromiso si comparten categoría.; Solo se incluyen registros que tengan al menos una orden de pago asociada (INNER JOIN con PaymentOrderDetail y PaymentOrder).; El campo Category y spendingCategoryName provienen de Budget.Category vinculada vía Budget.Budget (bb.CategoryId), no directamente del detalle de compromiso/obligación.; La vigencia reportada (Validity) corresponde a la vigencia presupuestal de la disponibilidad (Availability), no de la orden de pago.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportPaymentPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Disponibilidad presupuestal (CDP); Compromiso presupuestal; Obligación presupuestal; Orden de pago; Vigencia presupuestal; Categoría/rubro presupuestal; Tipo de ingreso (RevenueType); Tercero (proveedor/beneficiario); Saldo de obligación; Plan de pagos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportPaymentPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.ViewReportPaymentPlan: Devuelve una fila por combinación válida de detalle de disponibilidad, compromiso, obligación y orden de pago, numerada secuencialmente con ROW_NUMBER ordenado por código de categoría (bc.Code).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportPaymentPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.AvailabilityDetail; Budget.Availability; Budget.BudgetaryValidity; Budget.CommitmentDetail; Budget.RevenueType; Budget.Commitment; Budget.ObligationDetail; Budget.Budget; Budget.PaymentOrderDetail; Budget.PaymentOrder; Budget.Obligation; Common.ThirdParty; Budget.Category', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportPaymentPlan';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewReportPaymentPlan';
GO
