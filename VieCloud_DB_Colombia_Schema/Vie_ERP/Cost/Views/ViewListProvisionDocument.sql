

CREATE VIEW [Cost].[ViewListProvisionDocument]
AS

SELECT   cd.Id
		,cd.Code
		,cd.GeneralExpenseId
		,cd.SupplierId
		,CONCAT(s.Code, ' - ', s.Name) SupplierCodeName
		,cd.SuppliersDistributionLinesId
		,CONCAT(dl.Code, ' - ', dl.Name) DistributionLineCodeName
		,cd.Value
		,cd.Status
		,CASE cd.Status
			WHEN 1 THEN 'Registrado'
			WHEN 2 THEN 'Confirmado'
			WHEN 3 THEN 'Anulado'
			WHEN 4 THEN 'Reversado'
			WHEN 5 THEN 'Confirmado Sin Legalizar'
			WHEN 6 THEN 'Confirmado Legalizado'
			ELSE '' END StatusName
		,cd.ConfirmUser
		,cd.ConfirmDate
FROM Cost.CostDistributionDirectCost cd WITH(NOLOCK)
JOIN Common.Supplier s WITH(NOLOCK) ON s.Id = cd.SupplierId
JOIN Common.SuppliersDistributionLines sdl WITH(NOLOCK) ON sdl.Id = cd.SuppliersDistributionLinesId
JOIN Common.DistributionLines dl WITH(NOLOCK) ON dl.Id = sdl.IdDistributionLine
WHERE cd.Status = 5 AND cd.ProvisionDocument = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los documentos de provisión de costos directos que están en estado ''Confirmado Sin Legalizar'' y marcados como documento de provisión. Integra información del proveedor (código y nombre), la línea de distribución contable asignada y el valor del costo, permitiendo identificar qué gastos generales han sido confirmados pero aún no han sido legalizados ante contabilidad. Sirve para el seguimiento y gestión del proceso de legalización de provisiones de costos directos por proveedor y línea de distribución presupuestal.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewListProvisionDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'VIEW', @level1name = N'ViewListProvisionDocument';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los costos directos de distribución que están confirmados sin legalizar y marcados como documento de provisión, enriquecidos con datos del proveedor y la línea de distribución.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewListProvisionDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El costo directo debe tener Status = 5 (Confirmado Sin Legalizar); El costo directo debe estar marcado como ProvisionDocument = 1; Debe existir el proveedor relacionado en Common.Supplier; Debe existir la relación proveedor-línea de distribución en Common.SuppliersDistributionLines; Debe existir la línea de distribución en Common.DistributionLines', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewListProvisionDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen costos directos en estado ''Confirmado Sin Legalizar'' (5); Solo se exponen registros marcados como documento de provisión (ProvisionDocument=1); Cada fila combina obligatoriamente un proveedor y una línea de distribución existentes (INNER JOIN); El proveedor se presenta concatenado como ''Código - Nombre'' y la línea de distribución igualmente como ''Código - Nombre''', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewListProvisionDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Costo directo; Distribución de costos; Proveedor; Línea de distribución; Documento de provisión; Legalización; Estado de confirmación', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewListProvisionDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Cost.CostDistributionDirectCost: Devuelve únicamente registros donde Status=5 y ProvisionDocument=1, mapeando el código de estado a su descripción legible (1=Registrado, 2=Confirmado, 3=Anulado, 4=Reversado, 5=Confirmado Sin Legalizar, 6=Confirmado Legalizado).', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewListProvisionDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status del costo directo → Traduce el código numérico al nombre del estado (Registrado/Confirmado/Anulado/Reversado/Confirmado Sin Legalizar/Confirmado Legalizado) else Devuelve cadena vacía', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewListProvisionDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostDistributionDirectCost; Common.Supplier; Common.SuppliersDistributionLines; Common.DistributionLines', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewListProvisionDocument';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'VIEW', @level1name=N'ViewListProvisionDocument';
GO
