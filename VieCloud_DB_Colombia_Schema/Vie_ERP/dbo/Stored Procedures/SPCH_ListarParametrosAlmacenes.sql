

CREATE PROCEDURE [dbo].[SPCH_ListarParametrosAlmacenes]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT EXIHISING,EVOULTFOL,EVOHISING,CONFOLESP,NOTAEVOLU,NOTEVOINT,EVOHISINT ,HCFOLIOESPAMB
	FROM dbo.HCUNITHIS 
	WHERE CODCENATE=@centroatencion AND UFUCODIGO=@UnidadFuncional AND CODTIPHIS<>'ENF'
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los parámetros de configuración clínica habilitados para una unidad funcional (piso, servicio o sala) dentro de un centro de atención específico, excluyendo las configuraciones de tipo enfermería. Retorna indicadores que controlan qué funcionalidades están activas en esa unidad: existencia de historia de ingreso, evoluciones en folio, evoluciones de historia, configuración de folios especiales, notas de evolución, notas de evolución integradas, evolución de historia integrada y folio especial ambulatorio. Se usa para que los módulos de historia clínica y almacenes clínicos conozcan qué opciones están disponibles según el contexto de atención del usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarParametrosAlmacenes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarParametrosAlmacenes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera parámetros de configuración de manejo de historia clínica y folios para una unidad funcional dentro de un centro de atención, excluyendo la configuración de enfermería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarParametrosAlmacenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir configuración en HCUNITHIS para el centro de atención y unidad funcional indicados; El tipo de historia configurado no debe ser ''ENF'' (enfermería)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarParametrosAlmacenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se devuelven configuraciones cuyo tipo de historia sea ''ENF'' (enfermería); El filtro siempre se aplica por la combinación centro de atención + unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarParametrosAlmacenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Unidad funcional; Tipo de historia clínica; Historia clínica de ingreso; Folios de evolución; Notas de evolución; Folio de espera ambulatorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarParametrosAlmacenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCUNITHIS: Cuando CODCENATE y UFUCODIGO coinciden y CODTIPHIS<>''ENF'', se retornan los parámetros de existencia/uso de folios e historia (EXIHISING, EVOULTFOL, EVOHISING, CONFOLESP, NOTAEVOLU, NOTEVOINT, EVOHISINT, HCFOLIOESPAMB)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarParametrosAlmacenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCUNITHIS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarParametrosAlmacenes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarParametrosAlmacenes';
-- GO
