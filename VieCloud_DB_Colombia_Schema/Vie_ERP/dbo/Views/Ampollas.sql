

create VIEW [dbo].[Ampollas]
AS
SELECT     CODPRODUC, DESPRODUC, CONCENMED, PRESENMED, TIEESTMED, TIPFORMED, PESTOTMED, CODUNIPES, VOLTOTMED, CODUNIVOL, ABRPROMEZ
FROM         dbo.IHLISTPRO
WHERE     (PRESENMED LIKE 'Ampoll%')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista filtrada de medicamentos en presentación de ampolla, obtenida del catálogo maestro de productos farmacéuticos (IHLISTPRO). Muestra únicamente los productos cuya presentación comienza con ''Ampoll'' (ampollas inyectables), incluyendo código del producto, descripción, concentración, presentación, tipo de estado del medicamento, forma farmacéutica, peso total, unidad de peso, volumen total, unidad de volumen y abreviatura para mezclas. Se usa en farmacia clínica y dispensación para consultar rápidamente los medicamentos inyectables en ampolla disponibles en el sistema, facilitando la prescripción, preparación de mezclas y control de inventario de este tipo de presentación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Ampollas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Ampollas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el subconjunto de productos del catálogo farmacéutico cuya presentación corresponde a ampollas, con sus atributos de concentración, presentación, peso y volumen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Ampollas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El catálogo de productos debe tener registros con el campo de presentación poblado para poder filtrar por ''Ampoll%''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Ampollas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen productos cuya presentación comienza con ''Ampoll''.; Nunca se exponen productos con otra presentación distinta de ampollas.; La vista es de solo lectura sobre el catálogo de productos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Ampollas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto farmacéutico; Ampolla; Presentación de medicamento; Concentración; Forma farmacéutica; Unidad de peso; Unidad de volumen', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Ampollas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IHLISTPRO: Cuando la presentación del producto inicia con ''Ampoll'' (LIKE ''Ampoll%'') se retorna el producto con sus datos de concentración, presentación, forma, peso, volumen y unidades.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Ampollas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Ampollas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'Ampollas';
GO
