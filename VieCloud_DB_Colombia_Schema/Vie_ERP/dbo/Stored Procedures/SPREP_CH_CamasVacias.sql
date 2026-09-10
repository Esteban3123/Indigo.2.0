
CREATE PROCEDURE [dbo].[SPREP_CH_CamasVacias]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.CODICAMAS AS 'CODIGO CAMA', RTRIM(A.DESCCAMAS) AS 'DESCRIPCION CAMA'
FROM  CHCAMASHO A WITH(NOLOCK)

WHERE A.CODCENATE = @CentroAtencion AND A.UFUCODIGO IN (@UnidadFuncional) AND ESTADCAMA=1

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las camas hospitalarias que están disponibles (vacías) en un centro de atención y unidad funcional específicos. Recibe como parámetros el centro de atención y la unidad funcional, y retorna el código y la descripción de cada cama cuyo estado indica que está libre. Se utiliza para gestión de camas, asignación de pacientes en hospitalización y seguimiento de disponibilidad en el mapa de camas del hospital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CamasVacias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CamasVacias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las camas hospitalarias disponibles (vacías) de un centro de atención y unidad funcional específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CamasVacias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse el código del centro de atención y de la unidad funcional a consultar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CamasVacias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran camas con ESTADCAMA=1 (estado vacío/disponible).; El resultado se restringe siempre al centro de atención y unidad funcional recibidos.; La descripción de la cama se devuelve sin espacios finales (RTRIM).; Se consulta con NOLOCK, por lo que pueden leerse datos no confirmados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CamasVacias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Centro de atención; Unidad funcional; Estado de cama (disponibilidad)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CamasVacias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] CHCAMASHO: Cuando ESTADCAMA=1 y la cama pertenece al centro de atención y unidad funcional indicados, se retorna su código y descripción como cama vacía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CamasVacias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CamasVacias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CamasVacias';
-- GO
