

create VIEW [dbo].[Tableteria]
AS
SELECT     CODPRODUC, DESPRODUC, CONCENMED, PRESENMED, TIEESTMED, TIPFORMED, PESTOTMED, CODUNIPES, VOLTOTMED, CODUNIVOL, CODUNIADM, 
                      CALCANAUT, PROESTADO
FROM         dbo.IHLISTPRO
WHERE     (TIPPRODUC = '1') AND (PRESENMED LIKE 'Tablet%') OR
                      (PRESENMED LIKE 'gra%') OR
                      (PRESENMED LIKE 'caps%')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que filtra del catálogo maestro de productos farmacéuticos (IHLISTPRO) únicamente los medicamentos sólidos orales: tabletas, grageas y cápsulas. Sirve como referencia rápida para farmacia y dispensación cuando se necesita consultar solo la familia de medicamentos de administración oral sólida, excluyendo líquidos, inyectables y otros. Incluye datos clave del medicamento como código, descripción, concentración, presentación, tiempo de estabilidad, forma farmacéutica, peso, volumen, unidad de administración y estado del producto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Tableteria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Tableteria';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el subconjunto de productos farmacéuticos en presentación sólida oral (tabletas, grageas, cápsulas) del catálogo maestro, con sus atributos clínicos y de dosificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Tableteria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El catálogo maestro de productos debe estar poblado y clasificar los productos por tipo (TIPPRODUC) y presentación (PRESENMED).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Tableteria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo los productos cuya presentación inicia con ''Tablet'', ''gra'' o ''caps'' aparecen en la vista.; El filtro de TIPPRODUC=''1'' solo aplica a la rama de tabletas; debido a la precedencia de OR, las presentaciones ''gra%'' y ''caps%'' se incluyen sin restricción de tipo de producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Tableteria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'medicamento; presentación farmacéutica; tableta; cápsula; gragea; concentración; forma farmacéutica; unidad de administración; cantidad autorizada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Tableteria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IHLISTPRO: Devuelve filas cuando (TIPPRODUC = ''1'' AND PRESENMED LIKE ''Tablet%'') OR PRESENMED LIKE ''gra%'' OR PRESENMED LIKE ''caps%''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Tableteria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPPRODUC = ''1'' AND PRESENMED LIKE ''Tablet%'' → Incluye el producto como tableta else Se evalúan las otras condiciones de presentación; si PRESENMED LIKE ''gra%'' → Incluye el producto como grageas/granulado independientemente del TIPPRODUC; si PRESENMED LIKE ''caps%'' → Incluye el producto como cápsula independientemente del TIPPRODUC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Tableteria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Tableteria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Tableteria';
GO
