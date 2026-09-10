
CREATE PROCEDURE [dbo].[SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR]
(
@Almacen char(4),
@Denominacion char(20),
@VersionERP int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	

IF @VersionERP=1

			
		SELECT RTRIM(B.CODPRODUC) AS Codigo,B.Disponibles, RTRIM(A.DESPRODUC) AS Medicamento,A.NOPOSPROD AS 'NO POS',A.TIPFORMED,A.CODGRUFAR
		FROM IHLISTPRO AS A INNER JOIN (
		SELECT A.CODPRODUC, SUM(CAST(COALESCE(NULLIF(C.IFICANTID,0),0) AS INT)) AS Disponibles FROM IHRINDDGH A 
		INNER JOIN dbo.INFISICO C ON A.IPRCODIGO=C.IPRCODIGO  
		WHERE  C.IALCODIGO=@Almacen AND A.CODPRODUC IN 
		(SELECT CODPRODUC FROM IHLISTPRO WHERE CODDCIMED=@Denominacion AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0') GROUP BY A.CODPRODUC) AS B ON A.CODPRODUC = B.CODPRODUC
       
ELSE
	
		SELECT  B.IPRCODIGO,RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS',SUM(CAST(COALESCE(NULLIF(0,0),0) AS INT)) AS Disponibles,TIPFORMED,CODGRUFAR
		FROM dbo.IHLISTPRO A 
		INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
		/*INNER JOIN dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	    INNER JOIN dbo.INNFISICO D ON D.INNPRODUC=C.OID 
	    INNER JOIN dbo.INNALMACE E ON D.INNALMACE=E.OID */
		WHERE CODDCIMED=@Denominacion AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0'
		GROUP BY A.CODPRODUC,DESPRODUC,NOPOSPROD,A.TIPPRODUC,TIPFORMED,CODGRUFAR, B.IPRCODIGO
       
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos disponibles para prescripción médica según una denominación común (DCI) y un almacén específico. Consulta el catálogo maestro de productos farmacéuticos (IHLISTPRO) filtrando por tipo de producto activo y no diluible, y lo cruza con el inventario físico (IHRINDDGH/INFISICO) para calcular las unidades disponibles en bodega. Soporta dos versiones del ERP mediante el parámetro @VersionERP, adaptando la lógica de cálculo de disponibilidad según la estructura de inventario de cada versión. Se utiliza en el módulo de historia clínica al momento de realizar una orden o receta médica, permitiendo al profesional de salud ver qué productos con la misma denominación genérica están en stock.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista productos farmacéuticos activos asociados a una denominación común internacional (DCI) para prescripción médica, mostrando disponibilidad en un almacén según la versión del ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse una denominación (DCI) para filtrar productos.; Debe indicarse la versión del ERP para determinar la rama de consulta.; Para la versión 1 del ERP se requiere un código de almacén válido para calcular disponibilidades.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos cuyo tipo (TIPPRODUC) sea ''1'' o ''3''.; Solo se incluyen productos en estado activo (PROESTADO = 1).; Solo se incluyen productos no marcados como dilución especial (ESPDILPRO = ''0'').; El filtro por denominación común (CODDCIMED) siempre es exacto.; Los valores nulos o cero en la cantidad física se tratan como cero en la suma de disponibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Denominación común internacional (DCI) del medicamento; Productos POS / NO POS; Forma farmacéutica (TIPFORMED); Grupo farmacológico (CODGRUFAR); Inventario disponible por almacén; Dilución especial de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] IHLISTPRO: Cuando @VersionERP=1, retorna productos con TIPPRODUC IN (''1'',''3''), PROESTADO=1 y ESPDILPRO=''0'' filtrados por CODDCIMED=@Denominacion, agregando disponibilidad sumada desde INFISICO para el almacén indicado.; [RETURN_RESULT] IHLISTPRO: Cuando @VersionERP<>1, retorna productos con TIPPRODUC IN (''1'',''3''), PROESTADO=1 y ESPDILPRO=''0'' filtrados por CODDCIMED=@Denominacion, con disponibilidad fija en 0 (lógica de inventario comentada).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Calcula disponibilidad real sumando IFICANTID de INFISICO para el almacén dado, uniendo con IHRINDDGH e IHLISTPRO. else Devuelve el listado de productos sin cálculo real de inventario (disponibilidad forzada a 0); la lógica con INNPRODUC/INNFISICO/INNALMACE está comentada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INFISICO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosParaPrescripcionMedica_ANTERIOR';
-- GO
