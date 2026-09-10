
CREATE PROCEDURE [dbo].[SPHC_ListadoServiciosIPS]
(
@TipoServicio int
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT RTRIM(CODSERIPS) AS Codigo,RTRIM(DESSERIPS) AS Servicio,TIPSERTER AS Terapia,RTRIM(CODSERIPS)+' - '+RTRIM(DESSERIPS) AS CodigoDescripcion,SERREASIT as ServicioRealizaSitio, IPSSERIAD as ServicioSeriado, SERIPSDASH , TIPSERIPS
FROM dbo.INCUPSIPS  
WHERE TIPSERIPS=@TipoServicio AND SIPSESTADO='1'
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que devuelve el catálogo de servicios médicos (CUPS/IPS) disponibles en la institución, filtrado por tipo de servicio y mostrando únicamente los registros activos. Retorna el código del servicio, su descripción, si es una terapia, si se realiza en sitio, si es un servicio seriado y su clasificación para el tablero de control (dashboard). Se usa para poblar listas desplegables o buscadores de servicios en el módulo de historia clínica, órdenes médicas y agendamiento, permitiendo al usuario seleccionar procedimientos, exámenes o consultas según la categoría requerida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoServiciosIPS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoServiciosIPS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el catálogo de servicios IPS activos filtrados por un tipo de servicio dado, para su uso en listados/selección.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoServiciosIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse un tipo de servicio para filtrar el catálogo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoServiciosIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan servicios IPS activos (SIPSESTADO=''1'').; Los códigos y descripciones se entregan sin espacios a la derecha (RTRIM).; Se entrega un campo concatenado ''Código - Descripción'' para visualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoServiciosIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicios IPS; Tipo de servicio; Terapia; Servicio seriado; Servicio que realiza sitio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoServiciosIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INCUPSIPS: Cuando TIPSERIPS coincide con el tipo solicitado y SIPSESTADO=''1'', retorna el servicio IPS con su código, descripción, indicadores de terapia, servicio seriado y servicio que realiza sitio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoServiciosIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoServiciosIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoServiciosIPS';
-- GO
