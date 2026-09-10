CREATE VIEW [dbo].[HCVistaMedicamentosDenominacionComunLiquidos]
AS
SELECT DISTINCT A.CODDCIMED AS Codigo, RTRIM(A.DESDCIMED) AS Medicamento, RTRIM(A.CODDCIMED) + ' - ' + RTRIM(A.DESDCIMED) AS CodigoDescripcion
FROM            dbo.IHDCIMEDI AS A INNER JOIN
                         dbo.IHLISTPRO AS B ON A.CODDCIMED = B.CODDCIMED AND B.ESPDILPRO = 1 AND B.TIPPRODUC IN ('1', '3')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de medicamentos en presentación líquida (diluibles) identificados por su Denominación Común Internacional (DCI), es decir, el nombre genérico del medicamento. Combina el catálogo de denominaciones comunes (IHDCIMEDI) con el catálogo maestro de productos farmacéuticos (IHLISTPRO), filtrando únicamente los productos que requieren dilución (ESPDILPRO=1) y que corresponden a medicamentos de tipo 1 o 3. Se usa para poblar selectores o buscadores de medicamentos líquidos/diluibles en la historia clínica, recetas o procesos de administración de medicamentos, mostrando el código genérico, el nombre del principio activo y la combinación código-descripción para facilitar la búsqueda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo de medicamentos con denominación común internacional (DCI) restringido a productos en presentación líquida disponibles para dispensación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en el catálogo de medicamentos vinculados al listado de productos por su código DCI; El listado de productos debe tener marcado el indicador de presentación líquida y un tipo de producto válido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo lista medicamentos cuyo producto asociado está marcado como líquido (ESPDILPRO=1); Solo considera tipos de producto ''1'' y ''3''; Elimina duplicados mediante DISTINCT; Concatena código y descripción separados por '' - '' aplicando RTRIM a ambos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Denominación Común Internacional (DCI); Presentación líquida; Tipo de producto farmacéutico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCVistaMedicamentosDenominacionComunLiquidos: Devuelve códigos y descripciones únicas de medicamentos cuando ESPDILPRO=1 y TIPPRODUC IN (''1'',''3'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si B.ESPDILPRO = 1 AND B.TIPPRODUC IN (''1'',''3'') → Incluye el medicamento en el resultado else Se excluye del listado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHDCIMEDI; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunLiquidos';
GO
