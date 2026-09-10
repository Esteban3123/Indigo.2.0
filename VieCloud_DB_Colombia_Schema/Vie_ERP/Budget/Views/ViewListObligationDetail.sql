

CREATE VIEW [Budget].[ViewListObligationDetail]
AS
SELECT
	od.Id,
	od.CommitmentDetailId,
	c.Code CommitmentCode,
	c.Document CommitmentDocument,
	ct.Id CategoryId,	
	CONCAT(ct.Code, ' - ', ct.Name) CategoryCodeName,
	CONCAT(fs.Code, ' - ', fs.Name) FinancialSourceCodeName,
	rt.Id RevenueTypeId,
	CONCAT(rt.Code, ' - ', rt.Name) RevenueTypeCodeName,
	od.InitialValue,
	od.Balance,
	od.EntityId,
	od.EntityCode,
	od.EntityName
FROM Budget.Commitment c WITH (NOLOCK)
JOIN Budget.CommitmentDetail cd WITH (NOLOCK) ON c.Id = cd.CommitmentId
JOIN Budget.ObligationDetail od WITH (NOLOCK) ON cd.Id = od.CommitmentDetailId
JOIN Budget.Category ct WITH (NOLOCK) ON cd.CategoryId = ct.Id
JOIN Budget.FinancialSource fs WITH (NOLOCK) ON ct.FinancialSourceId = fs.Id
JOIN Budget.RevenueType rt WITH (NOLOCK) ON cd.RevenueTypeId = rt.Id
JOIN Budget.Budget b WITH (NOLOCK) ON ct.Id = b.CategoryId AND rt.Id = b.RevenueTypeId
JOIN Budget.BudgetHeader bh WITH (NOLOCK) ON b.BudgetHeaderId = bh.Id
JOIN Budget.BudgetaryValidity bv WITH (NOLOCK) ON bh.BudgetaryValidityId = bv.Id AND bv.Status = 2
WHERE od.EntityId IS NOT NULL
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de obligaciones presupuestales activas: muestra el desglose de cada obligación vinculada a un compromiso presupuestal, incluyendo el código y documento del compromiso, la categoría de gasto, la fuente de financiación y el tipo de ingreso o renta asociado, junto con el valor inicial y el saldo disponible de la obligación. Integra información de compromisos, sus detalles, categorías presupuestales, fuentes financieras y tipos de ingreso, filtrando únicamente las obligaciones que tienen una entidad tercera asociada (proveedor, contratista u organismo) y que pertenecen a vigencias presupuestales en estado activo (Status = 2). Se utiliza para reportería y seguimiento de la ejecución presupuestal del gasto, permitiendo identificar a qué entidad externa corresponde cada obligación, bajo qué categoría y fuente de financiación fue comprometida.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListObligationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'VIEW', @level1name = N'ViewListObligationDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de obligaciones presupuestarias asociado a terceros, enriquecido con su compromiso, categoría, fuente de financiación y tipo de ingreso, restringido a vigencias presupuestales activas.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen compromisos (Commitment) con su detalle (CommitmentDetail) y obligaciones de detalle (ObligationDetail) enlazadas por CommitmentDetailId.; La categoría del detalle del compromiso debe estar asociada a una línea presupuestal (Budget) cuyo encabezado pertenezca a una vigencia presupuestal (BudgetaryValidity).; La vigencia presupuestal debe encontrarse en estado 2 (bv.Status = 2).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen obligaciones cuya vigencia presupuestal está en Status = 2 (vigencia activa/vigente).; Únicamente se incluyen obligaciones con tercero identificado (EntityId IS NOT NULL).; Cada fila combina la categoría y el tipo de ingreso del detalle del compromiso, garantizando coherencia con la línea presupuestal (Budget) que comparte CategoryId y RevenueTypeId.; Los códigos de categoría, fuente financiera y tipo de ingreso se presentan concatenados como ''Code - Name''.; Todas las lecturas se realizan con NOLOCK (lecturas sucias permitidas).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Obligación presupuestaria; Compromiso presupuestario; Detalle de compromiso; Categoría presupuestal; Fuente de financiación; Tipo de ingreso/renta; Línea presupuestal; Vigencia presupuestal; Tercero/Entidad; Saldo y valor inicial de obligación', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Budget.ObligationDetail: Devuelve solo filas de ObligationDetail cuyo EntityId IS NOT NULL, es decir, obligaciones asociadas a una entidad/tercero identificado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Commitment; Budget.CommitmentDetail; Budget.ObligationDetail; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.Budget; Budget.BudgetHeader; Budget.BudgetaryValidity', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'VIEW', @level1name=N'ViewListObligationDetail';
GO
