CREATE VIEW [Billing].[ViewOpenRevenueReportSummary] AS
SELECT	
		ROW_NUMBER() OVER(ORDER BY v.CareGroupId ASC ) AS Id,
		v.CareGroupId,
		v.CareGroup,
		v.EntityId,
		v.Entity,
		v.CodCareCenter,
		v.CareCenter,
		v.CodCreationUser,
		v.CreationUser,
		v.ConceptStatusFolio,
		v.dateIncome,
		v.Status,
		COUNT(v.Id) AS QuantityFolios,
		RTRIM(v.Income) AS Income
		FROM Billing.ViewOpenRevenueReport v
		GROUP BY	CareGroup,
					Entity,
					CareCenter,
					CreationUser, 
					ConceptStatusFolio,
					v.CareGroupId,
					v.EntityId, 
					v.CodCareCenter,
					v.CodCreationUser,
					dateIncome,
					v.Status,
					v.Income
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resumen agrupado del reporte de ingresos abiertos (folios pendientes de facturación o gestión). Consolida la información de la vista base ViewOpenRevenueReport agrupando por grupo de atención, entidad, centro de atención, usuario creador, concepto de estado del folio, fecha de ingreso y estado, calculando la cantidad de folios por combinación de esos criterios. Sirve para reportería gerencial y de cartera, permitiendo visualizar cuántos folios abiertos existen por centro, entidad aseguradora, usuario y estado en un período determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewOpenRevenueReportSummary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewOpenRevenueReportSummary';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resumen agregado de folios de ingresos abiertos, contando cantidad de folios por grupo de atención, entidad, centro, usuario creador, estado y fecha de ingreso.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReportSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La vista Billing.ViewOpenRevenueReport debe existir y exponer las columnas referenciadas (CareGroupId, CareGroup, EntityId, Entity, CodCareCenter, CareCenter, CodCreationUser, CreationUser, ConceptStatusFolio, dateIncome, Status, Id, Income).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReportSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila resultante representa una combinación única de grupo de atención, entidad, centro de atención, usuario creador, concepto de estado de folio, fecha de ingreso, estado e ingreso.; QuantityFolios refleja el número de folios (Id) agrupados por la combinación de dimensiones indicadas.; Income se entrega sin espacios finales (RTRIM).; La numeración Id se asigna dinámicamente vía ROW_NUMBER ordenando por CareGroupId ascendente, por lo que no es un identificador estable entre ejecuciones.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReportSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Folio; Centro de atención; Grupo de atención; Entidad; Ingreso (Income); Estado de folio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReportSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewOpenRevenueReport: Devuelve el conteo de folios (COUNT(v.Id) AS QuantityFolios) agrupado por CareGroup, Entity, CareCenter, CreationUser, ConceptStatusFolio, CareGroupId, EntityId, CodCareCenter, CodCreationUser, dateIncome, Status e Income.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReportSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ViewOpenRevenueReport', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReportSummary';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReportSummary';
GO
