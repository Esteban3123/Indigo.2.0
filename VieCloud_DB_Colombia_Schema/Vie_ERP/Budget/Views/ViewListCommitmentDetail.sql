
CREATE VIEW [Budget].[ViewListCommitmentDetail]
AS
SELECT
	ROW_NUMBER() OVER (ORDER BY cd.Id) Id,
	co.Code CommitmentCode, 
	co.Id CommitmentId, 
	co.ThirdPartyId, 
	co.Status, 
	cd.Id CommitmentDetailId, 
	cd.ExpiredDate, 
	cd.InitialValue, 
	cd.TotalCommitment, 
	cd.Balance,
	ca.Code CategoryCode, 
	ca.Name CategoryName, 
	ca.Code + ' - ' + ca.Name CategoryCodeName,
	rt.Code RevenueTypeCode, 
	rt.Name RevenueTypeName, 
	rt.Code + ' - ' + rt.Name RevenueTypeCodeName,
	co.Code + ' - ' + ca.Code + ' - ' + ca.Name + ' - ' + rt.Code + ' - ' + rt.Name NullText,
	fs.Code FinancialSourceCode,
	fs.Name FinancialSourceName,
	fs.Code + ' - ' + fs.Name FinancialSourceCodeName,
	co.Document,
	co.BudgetaryValidityId
FROM Budget.CommitmentDetail cd
JOIN Budget.Commitment co on co.Id = cd.CommitmentId
JOIN Budget.Category ca on ca.Id = cd.CategoryId
JOIN Budget.RevenueType rt on rt.Id = cd.RevenueTypeId
join Budget.FinancialSource fs on fs.Id = ca.FinancialSourceId
WHERE cd.Balance > 0
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los detalles de compromisos presupuestales que aún tienen saldo disponible (Balance > 0), combinando información del compromiso (código, tercero, estado, documento, vigencia presupuestal) con el desglose por categoría de gasto, tipo de ingreso o renta y fuente de financiación. Integra las tablas de Compromiso, Detalle de Compromiso, Categoría, Tipo de Ingreso y Fuente Financiera para ofrecer una vista consolidada que facilita la consulta y seguimiento de compromisos presupuestales activos con saldo pendiente de ejecutar. Sirve como base para reportería de ejecución presupuestal, control de saldos por categoría y fuente de financiación, y para selección de compromisos disponibles al momento de registrar nuevas ejecuciones o afectaciones presupuestales.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListCommitmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListCommitmentDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de detalles de compromisos presupuestales con saldo disponible, enriquecido con datos del compromiso, categoría, tipo de ingreso y fuente de financiación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada CommitmentDetail debe tener un Commitment, Category y RevenueType existentes (JOIN no LEFT).; La Category debe estar asociada a una FinancialSource existente.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye detalles con Balance <= 0 (filtra cd.Balance > 0).; Asigna un Id secuencial mediante ROW_NUMBER() ordenado por cd.Id, no es un identificador persistente.; Construye etiquetas concatenadas ''Code - Name'' para Categoría, Tipo de Ingreso y Fuente Financiera; y un NullText combinando código de compromiso, categoría y tipo de ingreso.; La fuente de financiación se deriva de la categoría (ca.FinancialSourceId), no directamente del detalle.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Compromiso presupuestal; Detalle de compromiso; Saldo (Balance); Categoría presupuestal; Tipo de ingreso/renta; Fuente de financiación; Vigencia presupuestaria; Tercero', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.CommitmentDetail: Solo retorna filas donde cd.Balance > 0 (detalles de compromiso con saldo disponible).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.CommitmentDetail; Budget.Commitment; Budget.Category; Budget.RevenueType; Budget.FinancialSource', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCommitmentDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListCommitmentDetail';
GO
