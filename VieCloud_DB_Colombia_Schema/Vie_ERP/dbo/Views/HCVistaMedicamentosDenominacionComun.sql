CREATE VIEW [dbo].[HCVistaMedicamentosDenominacionComun]
AS
/*SELECT        Codigo, Medicamento, CodigoDescripcion, ROW_NUMBER() OVER (ORDER BY base.Codigo) AS NumeroFila
FROM            (SELECT DISTINCT A.CODDCIMED AS Codigo, RTRIM(A.DESDCIMED) AS Medicamento, RTRIM(A.CODDCIMED) + ' - ' + RTRIM(A.DESDCIMED) AS CodigoDescripcion
                          FROM            dbo.IHDCIMEDI AS A INNER JOIN
                                                    dbo.IHLISTPRO AS B ON A.CODDCIMED = B.CODDCIMED AND B.ESPDILPRO = 1 OR B.TIPPRODUC IN ('1', '3')) Base
													*/

SELECT DISTINCT A.CODDCIMED AS Codigo, RTRIM(A.DESDCIMED) AS Medicamento, RTRIM(A.CODDCIMED) + ' - ' + RTRIM(A.DESDCIMED) AS CodigoDescripcion
FROM  dbo.IHDCIMEDI AS A 
	INNER JOIN Inventory.DCI d on a.CODDCIMED = d.DCICrystal and d.Status = 1
	INNER JOIN dbo.IHLISTPRO AS B ON A.CODDCIMED = B.CODDCIMED AND B.ESPDILPRO = 1 OR B.TIPPRODUC IN ('1', '3') AND PROESTADO=1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de medicamentos activos clasificados por su Denominación Común Internacional (DCI), es decir, por principio activo o nombre genérico. Cruza el catálogo de DCI del inventario con los productos farmacéuticos del sistema, filtrando solo los medicamentos vigentes (estado activo) que corresponden a especialidades farmacéuticas o tipos de producto relevantes. Se utiliza para búsqueda y selección de medicamentos en historia clínica usando el nombre genérico, mostrando código DCI, nombre del principio activo y la combinación ''código - descripción'' para facilitar la selección en formularios de prescripción y formulación médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaMedicamentosDenominacionComun';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaMedicamentosDenominacionComun';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado depurado de medicamentos por Denominación Común Internacional activa, mostrando código, nombre y descripción combinada para selección en historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComun';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de DCI en IHDCIMEDI debe coincidir con DCICrystal en Inventory.DCI.; Debe existir relación entre el medicamento y al menos un producto en IHLISTPRO por el código de DCI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComun';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone medicamentos cuya DCI está activa (Status = 1) en el catálogo Inventory.DCI.; Restringe a ítems del catálogo de productos que cumplan ESPDILPRO = 1, o bien que sean de tipo de producto ''1'' o ''3'' con PROESTADO = 1 (según la precedencia de operadores AND/OR aplicada).; Devuelve resultados sin duplicados (DISTINCT) por código de DCI.; Construye una descripción combinada con el formato ''Codigo - Descripción'' eliminando espacios finales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComun';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Denominación Común Internacional (DCI); Catálogo de productos farmacéuticos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComun';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IHDCIMEDI: Retorna medicamentos distintos cruzando IHDCIMEDI con Inventory.DCI (Status=1) y con IHLISTPRO bajo la condición ''ESPDILPRO=1 OR TIPPRODUC IN (1,3) AND PROESTADO=1''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComun';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHDCIMEDI; dbo.IHLISTPRO; Inventory.DCI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComun';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaMedicamentosDenominacionComun';
GO
