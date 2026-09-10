
CREATE PROCEDURE [dbo].[SP_HC_ListarInsumosParaPrescripcionMedica]
(
@Almacen char(4),
@VersionERP int
)
AS
BEGIN
	SET NOCOUNT ON;
	
IF @VersionERP=1
	
       SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,SUM(CAST(C.IFICANTID AS INT)) AS Disponibles
       FROM dbo.IHLISTPRO A 
       INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
       INNER JOIN dbo.INFISICO C ON B.IPRCODIGO=C.IPRCODIGO 
       WHERE (C.IALCODIGO=@Almacen AND TIPPRODUC = '2') OR (C.IALCODIGO=@Almacen AND ESPDILPRO ='1') 
	   GROUP BY A.CODPRODUC , DESPRODUC

ELSE
	
		/*aca la consulta*/
		SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,SUM(CAST(D.IFICANTID AS INT)) AS Disponibles
       FROM dbo.IHLISTPRO A 
       INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
       INNER JOIN dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
       INNER JOIN dbo.INNFISICO D ON D.INNPRODUC=C.OID 
       INNER JOIN dbo.INNALMACE E ON D.INNALMACE=E.OID 
       WHERE (E.IALCODIGO=@Almacen AND TIPPRODUC = '2') OR (E.IALCODIGO=@Almacen AND ESPDILPRO ='1') 
	   GROUP BY A.CODPRODUC , DESPRODUC
	

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los insumos y medicamentos disponibles en un almacén específico para ser usados en la prescripción médica dentro de la historia clínica. Consulta el catálogo maestro de productos farmacéuticos (IHLISTPRO) y cruza con el inventario físico del almacén indicado para obtener el código del producto, su descripción y la cantidad disponible. Filtra únicamente medicamentos (tipo de producto ''2'') y productos especiales diluyentes, agrupando los resultados por producto. Soporta dos versiones del ERP mediante el parámetro @VersionERP, adaptando las tablas de inventario consultadas (INFISICO para la versión 1, INNFISICO/INNPRODUC/INNALMACE para versiones posteriores).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los insumos/medicamentos disponibles en un almacén para uso en prescripción médica, agrupados por producto con su stock total, soportando dos versiones del ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe indicarse el código de almacén a consultar; Debe indicarse la versión del ERP (1 = esquema legado IN*; otro valor = esquema nuevo INN*)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen productos cuyo tipo es ''2'' o que están marcados como especiales para dilución (ESPDILPRO=''1''); Solo se consideran existencias del almacén indicado; Las cantidades físicas (IFICANTID) se convierten a entero antes de sumarse; El resultado se agrupa por código y descripción del producto; Códigos y descripciones se devuelven sin espacios a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Insumos/medicamentos; Almacén farmacéutico; Stock disponible; Productos especiales para dilución', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IHLISTPRO: Cuando @VersionERP=1, retorna productos del almacén con stock sumado desde INFISICO filtrando por TIPPRODUC=''2'' o ESPDILPRO=''1''; [RETURN_RESULT] dbo.IHLISTPRO: Cuando @VersionERP<>1, retorna productos del almacén con stock sumado desde INNFISICO/INNALMACE filtrando por TIPPRODUC=''2'' o ESPDILPRO=''1''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Consulta el stock usando el esquema legado (INFISICO unido por IPRCODIGO y filtrado por IALCODIGO) else Consulta el stock usando el esquema nuevo (INNPRODUC, INNFISICO, INNALMACE unidos por OID)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INFISICO; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInsumosParaPrescripcionMedica';
-- GO
