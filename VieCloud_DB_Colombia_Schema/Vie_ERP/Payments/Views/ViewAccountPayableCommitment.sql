CREATE VIEW [Payments].[ViewAccountPayableCommitment]
AS
SELECT	CAST(apc.Id AS VARCHAR(20)) Id,
		apc.AccountPayableId,
		b.Id BudgetId,
		c.Code CategoryCode,
		c.Name CategoryName,
		fs.Code FinancialSourceCode,
		fs.Name FinancialSourceName,
		rt.Code RevenueTypeCode,
		rt.Name RevenueTypeName,
		apc.Value
FROM Payments.AccountPayableCommitments apc
JOIN Budget.CommitmentDetail cd ON apc.CommitmentDetailId = cd.Id
JOIN Budget.Budget b ON cd.CategoryId = b.CategoryId AND cd.RevenueTypeId = b.RevenueTypeId
JOIN Budget.Category c ON b.CategoryId = c.Id
JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los compromisos presupuestales asociados a cuentas por pagar, integrando el detalle de cada afectación presupuestal con su categoría de gasto, fuente de financiamiento y tipo de ingreso o renta. Combina los compromisos registrados en cuentas por pagar con el detalle presupuestal, la categoría, la fuente financiera y el tipo de ingreso correspondiente, mostrando el valor comprometido junto con los códigos y nombres descriptivos de cada clasificación presupuestal. Sirve para consultas de reportería financiera y presupuestal que requieran identificar qué rubro, fuente de financiación y tipo de renta respalda cada obligación de pago pendiente.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewAccountPayableCommitment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'VIEW', @level1name = N'ViewAccountPayableCommitment';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los compromisos presupuestales asociados a cuentas por pagar enriquecidos con su categoría, fuente financiera, tipo de ingreso y línea presupuestal correspondiente.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableCommitment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada compromiso de cuenta por pagar debe tener un detalle de compromiso existente.; Debe existir una línea presupuestal (Budget) cuya CategoryId y RevenueTypeId coincidan con los del detalle de compromiso.; La categoría debe tener una fuente financiera (FinancialSource) asociada.; Debe existir el tipo de ingreso (RevenueType) referenciado por la línea presupuestal.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableCommitment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Id del compromiso se expone como cadena (VARCHAR(20)).; Cada fila representa un compromiso de cuenta por pagar único, asociado a una sola línea presupuestal determinada por la combinación CategoryId + RevenueTypeId.; La categoría y la fuente financiera mostradas siempre corresponden a la misma cadena de relación Category → FinancialSource.; Solo se incluyen compromisos con integridad referencial completa en la cadena presupuestal.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableCommitment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Compromiso presupuestal; Detalle de compromiso; Presupuesto; Categoría presupuestal; Fuente de financiación; Tipo de ingreso/renta', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableCommitment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payments.ViewAccountPayableCommitment: Devuelve solo compromisos cuyas relaciones con CommitmentDetail, Budget (por CategoryId y RevenueTypeId), Category, FinancialSource y RevenueType existen (INNER JOIN); si falta cualquier vínculo, el compromiso se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableCommitment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayableCommitments; Budget.CommitmentDetail; Budget.Budget; Budget.Category; Budget.FinancialSource; Budget.RevenueType', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableCommitment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'VIEW', @level1name=N'ViewAccountPayableCommitment';
GO
