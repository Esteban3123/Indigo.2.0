

CREATE VIEW [dbo].[insumosvenopuncion]
AS
SELECT        HCHOGASIN.CODPRODUC, IHLISTPRO.DESPRODUC AS 'PRODUCTO', INUNIFUNC.UFUDESCRI AS 'UNIDAD', INPROFSAL.NOMMEDICO AS 'USUARIO QUE APLICA', HCHOGASIN.FECHAUTIL AS 'fecha Gasto', 
                         HCHOGASIN.NUMINGRES AS 'iNGRESO', HCHOGASIN.IPCODPACI AS 'DOCUMENTO PACIENTE', HCHOGASIN.CANUTIPRO
FROM           HCHOGASIN HCHOGASIN, IHLISTPRO IHLISTPRO, INPROFSAL INPROFSAL, INUNIFUNC INUNIFUNC
WHERE        HCHOGASIN.CODPRODUC = IHLISTPRO.CODPRODUC AND HCHOGASIN.CODPROSAL = INPROFSAL.CODPROSAL AND HCHOGASIN.UFUCODIGO = INUNIFUNC.UFUCODIGO AND ((HCHOGASIN.CODCENATE = '01')) AND 
                         HCHOGASIN.FECHAUTIL > '30/09/2011'
UNION
SELECT        HCCTRVINS.CODPRODUC, IHLISTPRO.DESPRODUC AS 'PRODUCTO', INUNIFUNC.UFUDESCRI AS 'UNIDAD', INPROFSAL.NOMMEDICO AS 'USUARIO QUE APLICA', HCCTRVENP.FECHAINIC AS 'Fecha Gasto', 
                         HCCTRVENP.NUMINGRES AS 'iNGRESO', HCCTRVENP.IPCODPACI AS 'DOCUMENTO PACINETE', HCCTRVINS.CANUTIPRO
FROM          HCCTRVENP HCCTRVENP, HCCTRVINS HCCTRVINS, IHLISTPRO IHLISTPRO, INPROFSAL INPROFSAL, INUNIFUNC INUNIFUNC
WHERE        HCCTRVENP.CODPROSAL = INPROFSAL.CODPROSAL AND HCCTRVINS.CODPRODUC = IHLISTPRO.CODPRODUC AND HCCTRVINS.CONSECUTI = HCCTRVENP.CONSECUTI AND 
                         HCCTRVENP.UFUCODIGO = INUNIFUNC.UFUCODIGO AND ((HCCTRVENP.codcenate = '01'))
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consolida mediante UNION dos fuentes de insumos relacionados con venopunción del centro de atención ''01'': gastos directos registrados en la historia clínica (después del 30/09/2011) y los consumos provenientes de controles de venopunción. Cruza productos del listado de insumos, unidades funcionales y profesionales de salud, permitiendo rastrear por paciente e ingreso qué insumos fueron aplicados, cuándo y por qué usuario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista los insumos utilizados en procedimientos de venopunción, integrando los gastos registrados en hoja de gastos con los insumos vinculados a controles de venopunción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos consumidos deben existir en el catálogo de productos (IHLISTPRO).; El profesional que aplica debe estar registrado en INPROFSAL.; La unidad funcional debe existir en INUNIFUNC.; Para los registros de control de venopunción, debe existir correspondencia por consecutivo entre el encabezado (HCCTRVENP) y el detalle de insumos (HCCTRVINS).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan movimientos del centro de atención ''01''.; Los gastos de hoja de gastos solo se consideran a partir del 01/10/2011.; Por usar UNION (no UNION ALL), se eliminan filas duplicadas entre ambas fuentes.; Cada insumo reportado siempre tiene producto, unidad funcional y profesional que lo aplica resueltos por catálogo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Venopunción; Insumos médicos; Hoja de gastos; Paciente; Ingreso hospitalario; Profesional de la salud; Unidad funcional; Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve gastos de insumos del centro de atención ''01'' con FECHAUTIL > 30/09/2011 desde HCHOGASIN, unidos (UNION) con insumos de venopunción del centro ''01'' desde HCCTRVENP/HCCTRVINS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCHOGASIN.CODCENATE = ''01'' AND HCHOGASIN.FECHAUTIL > ''30/09/2011'' → Incluye el gasto del insumo proveniente de la hoja de gastos.; si HCCTRVENP.codcenate = ''01'' → Incluye los insumos asociados al control de venopunción para ese centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOGASIN; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.HCCTRVENP; dbo.HCCTRVINS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'insumosvenopuncion';
GO
