
CREATE PROCEDURE [dbo].[SPHC_ListadoMedicamentosPrescripcion]
@VersionERP int
AS
BEGIN
	SET NOCOUNT ON;

	IF (@VersionERP=1 or @VersionERP=2)  BEGIN
		--fox o NET - DGH
		SELECT DISTINCT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS',COALESCE(NULLIF(CODDCIMED, CAST('' AS CHAR)),CAST('' AS CHAR)) AS CODDCIMED
		FROM dbo.IHLISTPRO A
			inner join dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
		WHERE TIPPRODUC IN ('1','3') AND PROESTADO = 1 --AND ESPDILPRO='0'
	END ELSE BEGIN
		--VIE ERP
		SELECT DISTINCT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,NOPOSPROD AS 'NO POS',COALESCE(NULLIF(CODDCIMED, CAST('' AS CHAR)),CAST('' AS CHAR)) AS CODDCIMED
		FROM dbo.IHLISTPRO A
		WHERE TIPPRODUC IN ('1','3') AND PROESTADO = 1 --AND ESPDILPRO='0'
	END
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retorna el listado de medicamentos e insumos habilitados para prescripción médica, filtrando únicamente productos activos de tipo medicamento o diluyente (tipos 1 y 3) del catálogo maestro de productos farmacéuticos (IHLISTPRO). Para cada ítem devuelve el código del producto, el nombre del medicamento, si pertenece o no al POS (Plan Obligatorio de Salud) y su código DCIMED. Según la versión del ERP configurada (Fox/NET o Vie ERP), aplica un filtro adicional cruzando con la tabla IHRINDDGH para restringir los productos al grupo DGH correspondiente a esa versión. Se utiliza en el módulo de historia clínica para poblar el selector de medicamentos al momento de realizar una prescripción o receta médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener el listado de medicamentos/insumos activos disponibles para prescripción, adaptando la consulta según la versión del ERP (DGH o VIE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe indicarse la versión del ERP para determinar el origen de datos (DGH vs VIE ERP)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan productos cuyo tipo es ''1'' o ''3'' (medicamentos/insumos prescriptibles); Solo se retornan productos en estado activo (PROESTADO = 1); El código DCI nunca se retorna NULL ni con espacios: se normaliza a cadena vacía cuando no exista; Los resultados son distintos (sin duplicados) y con códigos/descripciones recortados (sin espacios a la derecha)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'medicamento; prescripción; producto POS / NO POS; código DCI (denominación común internacional); catálogo de productos farmacéuticos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IHLISTPRO: Cuando TIPPRODUC IN (''1'',''3'') AND PROESTADO = 1 → retorna código, descripción, indicador NO POS y código DCI del medicamento; [RETURN_RESULT] dbo.IHRINDDGH: Cuando VersionERP IN (1,2) → restringe el listado a productos que existen en IHRINDDGH (INNER JOIN por CODPRODUC)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VersionERP = 1 o 2 (Fox o NET - DGH) → Lista medicamentos cruzando catálogo de productos con la relación de indicadores DGH (IHRINDDGH) else Para otras versiones (VIE ERP), lista medicamentos solo desde el catálogo de productos sin cruce con IHRINDDGH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoMedicamentosPrescripcion';
-- GO
