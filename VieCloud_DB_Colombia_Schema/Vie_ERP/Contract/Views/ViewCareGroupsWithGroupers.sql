CREATE VIEW [Contract].[ViewCareGroupsWithGroupers]
AS
	SELECT 
		CONCAT('GroupersCareGroup-', gcg.Id) as Row,
		NULL InvoiceEntityCapitatedId,
		gcg.CareGroupId, 
		gcg.GroupersId, 
		g.Code, 
		g.[Description], 
		g.UserNumber, 
		g.UserMin, 
		g.UserMax, 
		gcg.RealCME,
		g.ProjectCME, 
		g.Frequence,
		gcg.EjectEvent,
		gcg.TotalEject, 
		g.TotalContract, 
		g.UserValue,
		gcg.DocumentDate
	FROM [Contract].GroupersCareGroup gcg
	JOIN [Contract].Groupers g ON g.Id = gcg.GroupersId
UNION
	SELECT 
		CONCAT('InvoiceEntityCapitatedGrouper-', iecg.Id) as Row,
		iecg.InvoiceEntityCapitatedId InvoiceEntityCapitatedId,
		gcg.CareGroupId, 
		gcg.GroupersId, 
		iecg.Code, 
		iecg.[Description], 
		g.UserNumber, 
		iecg.UserMin, 
		iecg.UserMax, 
		gcg.RealCME,
		iecg.ProjectCME, 
		g.Frequence,
		gcg.EjectEvent,
		gcg.TotalEject, 
		iecg.TotalContract, 
		g.UserValue,
		gcg.DocumentDate
	FROM [Billing].[InvoiceEntityCapitatedGroupers] iecg
	JOIN [Contract].Groupers g ON iecg.GroupersId = g.Id
	JOIN [Contract].GroupersCareGroup gcg ON g.Id = gcg.GroupersId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los grupos de atención (care groups) con sus agrupadores tarifarios de contrato, combinando dos fuentes: los agrupadores directos de contrato (GroupersCareGroup + Groupers) y los agrupadores asociados a facturas de entidades en modalidad de capitación (InvoiceEntityCapitatedGroupers). Para cada fila expone el identificador del grupo de atención, el agrupador, código y descripción del agrupador, rangos y número de usuarios, el CME real ejecutado, el CME proyectado, la frecuencia, los eventos de eyección, el total ejecutado, el total contratado, el valor por usuario y la fecha de documento. Sirve para reportería y control de ejecución contractual en contratos de capitación, permitiendo comparar lo proyectado versus lo real por grupo de atención y agrupador tarifario.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewCareGroupsWithGroupers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewCareGroupsWithGroupers';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los agrupadores asociados a grupos de atención, combinando los definidos en el contrato con los provenientes de facturas de entidades en modalidad de capitación.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewCareGroupsWithGroupers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre Groupers y GroupersCareGroup por GroupersId.; Para la rama capitada, cada InvoiceEntityCapitatedGroupers debe referenciar un Groupers existente y dicho agrupador debe estar vinculado a un CareGroup en GroupersCareGroup.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewCareGroupsWithGroupers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila se identifica con un prefijo según su origen: ''GroupersCareGroup-'' para agrupadores del contrato base e ''InvoiceEntityCapitatedGrouper-'' para los provenientes de facturas capitadas.; Las filas provenientes del contrato base no llevan referencia a factura capitada (InvoiceEntityCapitatedId queda en NULL).; El uso de UNION (no UNION ALL) elimina duplicados exactos entre ambas fuentes.; Los agrupadores incluidos siempre deben existir en Contract.Groupers (join obligatorio en ambas ramas).; En la rama de facturación capitada, los valores de Code, Description, UserMin, UserMax, ProjectCME y TotalContract provienen de la factura capitada, mientras que UserNumber, Frequence y UserValue siempre se toman del agrupador maestro.; En ambas ramas, RealCME, EjectEvent, TotalEject y DocumentDate provienen siempre de la relación GroupersCareGroup.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewCareGroupsWithGroupers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Agrupadores de contrato; Grupos de atención (Care Groups); Capitación; CME (real y proyectado); Eyección de eventos; Rangos de usuarios (mínimo/máximo); Frecuencia de contrato; Total contratado vs ejecutado', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewCareGroupsWithGroupers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.GroupersCareGroup: Retorna agrupadores de contrato unidos por GroupersCareGroup con Groupers, marcando InvoiceEntityCapitatedId en NULL y prefijo ''GroupersCareGroup-'' en Row.; [RETURN_RESULT] Billing.InvoiceEntityCapitatedGroupers: Retorna agrupadores capitados de facturación cruzados con Groupers y GroupersCareGroup, exponiendo InvoiceEntityCapitatedId y prefijo ''InvoiceEntityCapitatedGrouper-'' en Row, sobreescribiendo Code/Description/UserMin/UserMax/ProjectCME/TotalContract con los valores de la factura capitada.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewCareGroupsWithGroupers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.GroupersCareGroup; Contract.Groupers; Billing.InvoiceEntityCapitatedGroupers', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewCareGroupsWithGroupers';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewCareGroupsWithGroupers';
GO
