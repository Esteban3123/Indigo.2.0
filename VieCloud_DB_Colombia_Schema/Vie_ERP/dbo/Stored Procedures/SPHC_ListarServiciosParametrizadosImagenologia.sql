
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarServiciosParametrizadosImagenologia]
(
@TipoServicio int,
@CentroAtencion Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select 
		B.CODCENATE as Centro ,RTRIM(A.CODSERIPS) as Codigo ,RTRIM(A.DESSERIPS) as Descripcion, B.IMAREAEXA as TiempoMaximoRecoleccion, cast(UNITIEREA as int) AS UnidadRecolectada,
		IMAREAEXA2 as ReporteRecolectada,cast(UNITIEREA2 as int) as UnidadReporteRecolectada,IMAENTRES as TiempoMaximoResultado,
		cast(UNITIERES as int) as UnidadResultado,IMAENTRES2 as ReporteResultado,cast(UNITIERES2 as int)  as UnidadReporteResultados , 
		B.ID , B.USUARIOCREACION, B.FECHACREACION, B.USUARIOMODIFICACION, B.FECHAMODIFICACION, 
		Rtrim(U1.CODUSUARI) + ' - ' + Rtrim(U1.NOMUSUARI) AS 'Usuario_Creacion', Rtrim(U2.CODUSUARI) + ' - ' + Rtrim(U2.NOMUSUARI) AS 'Usuario_Modificacion' 
		
	From 
		dbo.INCUPSIPS As A 
		Left Outer Join dbo.HCPARALEIMA As B On A.CODSERIPS = B.CODSERIPS And B.CODCENATE = @CentroAtencion 
		Left Outer Join dbo.SEGusuaru As U1 On B.USUARIOCREACION = U1.CODUSUARI 
		Left Outer Join.dbo.SEGusuaru As U2 On B.USUARIOMODIFICACION = U2.CODUSUARI                                     

	Where 
		A.TIPSERIPS = @TipoServicio 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios de imagenología (CUPS/IPS) que están parametrizados para un centro de atención específico, filtrando por tipo de servicio. Combina el catálogo de servicios de salud (INCUPSIPS) con la parametrización de tiempos de imagenología (HCPARALEIMA), que define los tiempos máximos de recolección y entrega de resultados de cada examen de imagen. Enriquece el resultado con los nombres completos de los usuarios que crearon y modificaron cada parametrización, consultándolos en el registro de usuarios del sistema (SEGusuaru). Se utiliza para administrar y consultar la configuración operativa de servicios de imagenología por sede, incluyendo sus plazos de recolección y reporte de resultados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosParametrizadosImagenologia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosParametrizadosImagenologia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios CUPS de un tipo determinado junto con sus parámetros de tiempos de recolección y entrega de resultados de imagenología, asociados a un centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosImagenologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un tipo de servicio válido en INCUPSIPS (TIPSERIPS).; El centro de atención debe corresponder a un CODCENATE válido para que se enlacen los parámetros de imagenología.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosImagenologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna servicios cuyo TIPSERIPS coincide con el tipo solicitado.; Los parámetros de imagenología (HCPARALEIMA) se incluyen únicamente si pertenecen al centro de atención indicado; servicios sin parametrización aparecen igual por el LEFT OUTER JOIN.; Los datos de usuario creador/modificador se muestran como ''código - nombre'' y son opcionales (LEFT JOIN a SEGusuaru).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosImagenologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicios CUPS; Imagenología; Centro de atención; Tiempo máximo de recolección; Tiempo máximo de entrega de resultado; Auditoría de usuario (creación/modificación)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosImagenologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INCUPSIPS: Devuelve los servicios filtrados por TIPSERIPS = @TipoServicio, con sus parámetros de imagenología cuando coinciden con el centro de atención solicitado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosImagenologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCUPSIPS; dbo.HCPARALEIMA; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosImagenologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosImagenologia';
-- GO
