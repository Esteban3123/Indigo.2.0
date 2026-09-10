CREATE VIEW [dbo].[HCVistaMedicamentosDenominacionComunMezclas]
AS
SELECT DISTINCT A.CODDCIMED AS Codigo, RTRIM(A.DESDCIMED) AS Medicamento, RTRIM(A.CODDCIMED) + ' - ' + RTRIM(A.DESDCIMED) AS CodigoDescripcion
FROM            dbo.IHDCIMEDI AS A INNER JOIN
                         dbo.IHLISTPRO AS B ON A.CODDCIMED = B.CODDCIMED AND B.TIPFORMED IN ('1', '2', '3') AND B.TIPPRODUC IN ('1', '3')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de denominaciones comunes internacionales (DCI) de medicamentos que corresponden a mezclas o preparaciones farmacéuticas. Combina el catálogo de denominaciones comunes (IHDCIMEDI) con el catálogo maestro de productos (IHLISTPRO), filtrando únicamente los productos de tipo medicamento o mezcla (TIPPRODUC 1 y 3) y con formas farmacéuticas específicas (TIPFORMED 1, 2 y 3). Se usa en historia clínica para seleccionar o buscar medicamentos por su nombre genérico o DCI al momento de prescribir mezclas y preparaciones, mostrando el código, el nombre del medicamento y una descripción combinada código-nombre para facilitar la búsqueda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaMedicamentosDenominacionComunMezclas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaMedicamentosDenominacionComunMezclas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo de medicamentos por Denominación Común Internacional (DCI) elegibles para mezclas, restringido a ciertos tipos de fórmula y de producto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunMezclas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia por código DCI entre el catálogo de medicamentos y la lista de productos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunMezclas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone medicamentos cuyo tipo de fórmula esté entre ''1'', ''2'' o ''3''.; Solo expone medicamentos cuyo tipo de producto sea ''1'' o ''3''.; Devuelve resultados únicos por código DCI (DISTINCT).; Las descripciones se entregan sin espacios sobrantes a la derecha (RTRIM).; Cada medicamento debe estar relacionado al menos con un producto en la lista de productos para ser visible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunMezclas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamentos; Denominación Común Internacional (DCI); Mezclas farmacéuticas; Catálogo de productos farmacéuticos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunMezclas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IHDCIMEDI: Retorna el código y descripción del medicamento (y su concatenación ''codigo - descripción'') solo cuando existe en IHLISTPRO con TIPFORMED IN (''1'',''2'',''3'') y TIPPRODUC IN (''1'',''3'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunMezclas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHDCIMEDI; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunMezclas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComunMezclas';
GO
