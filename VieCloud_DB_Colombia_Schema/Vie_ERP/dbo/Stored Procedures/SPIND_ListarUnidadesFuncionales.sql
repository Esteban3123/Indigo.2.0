
CREATE PROCEDURE [dbo].[SPIND_ListarUnidadesFuncionales]
(
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT UFUTIPUNI, UFUDESCRI 
FROM dbo.INUNIFUNC 
WHERE UFUCODIGO=@UnidadFuncional
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el tipo y la descripción de una unidad funcional específica (servicio, sala o área de atención) a partir de su código. Recibe el código de la unidad funcional como parámetro y retorna su tipo y nombre descriptivo desde el catálogo maestro de unidades funcionales. Se utiliza para identificar y mostrar la información básica de un área o servicio del centro de salud, como urgencias, hospitalización, consulta externa, entre otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPIND_ListarUnidadesFuncionales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPIND_ListarUnidadesFuncionales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera el tipo y la descripción de una unidad funcional específica del catálogo maestro a partir de su código.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPIND_ListarUnidadesFuncionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en el catálogo de unidades funcionales con el código solicitado para obtener resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPIND_ListarUnidadesFuncionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna columnas descriptivas (tipo y descripción) de la unidad funcional, sin modificar datos.; El filtrado se hace exclusivamente por código exacto de unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPIND_ListarUnidadesFuncionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Tipo de unidad funcional; Catálogo de servicios/áreas de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPIND_ListarUnidadesFuncionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INUNIFUNC: Cuando UFUCODIGO coincide con el código recibido, retorna UFUTIPUNI y UFUDESCRI de esa unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPIND_ListarUnidadesFuncionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPIND_ListarUnidadesFuncionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPIND_ListarUnidadesFuncionales';
-- GO
