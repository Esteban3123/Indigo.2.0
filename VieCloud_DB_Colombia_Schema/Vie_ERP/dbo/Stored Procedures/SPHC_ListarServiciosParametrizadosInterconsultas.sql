
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarServiciosParametrizadosInterconsultas]
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
		B.CODCENATE as Centro ,RTRIM(A.CODSERIPS) as Codigo ,RTRIM(A.DESSERIPS) as Descripcion, B.TIEMAXATE as TiempoMaximoAtencion, cast(UNITIEATE as int) AS UnidadAtencion,
		TIEMAXATE2 as ReporteAtencion,cast(UNITIEATE2 as int) as UnidadReporteAtencion , 
		B.ID , B.USUARIOCREACION, B.FECHACREACION, B.USUARIOMODIFICACION, B.FECHAMODIFICACION, 
		Rtrim(U1.CODUSUARI) + ' - ' + Rtrim(U1.NOMUSUARI) AS 'Usuario_Creacion', Rtrim(U2.CODUSUARI) + ' - ' + Rtrim(U2.NOMUSUARI) AS 'Usuario_Modificacion' 
		
	From 
		dbo.INCUPSIPS As A 
		Left Outer Join	dbo.HCPARALEINT As B On A.CODSERIPS = B.CODSERIPS And B.CODCENATE = @CentroAtencion 
		Left Outer Join dbo.SEGusuaru As U1 On B.USUARIOCREACION = U1.CODUSUARI 
		Left Outer Join.dbo.SEGusuaru As U2 On B.USUARIOMODIFICACION = U2.CODUSUARI                                     
		
	Where 
		A.TIPSERIPS = @TipoServicio 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios (CUPS) parametrizados para interconsultas, filtrando por tipo de servicio y centro de atención. Para cada servicio, muestra el código y descripción del servicio, los tiempos máximos de atención configurados (tiempo principal y tiempo de reporte, con sus respectivas unidades), y los datos de auditoría (usuario y fecha de creación y modificación). Combina el catálogo de servicios CUPS (INCUPSIPS) con los parámetros de tiempos de atención por interconsulta (HCPARALEINT) y enriquece la información con los nombres completos de los usuarios que crearon o modificaron cada parametrización (SEGusuaru). Se utiliza para administrar y consultar los tiempos máximos de respuesta que deben cumplirse en los servicios de interconsulta de cada centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosParametrizadosInterconsultas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosParametrizadosInterconsultas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios CUPS de un tipo dado junto con sus parámetros de tiempos máximos de atención e información de auditoría para un centro de atención específico, en el contexto de interconsultas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse el tipo de servicio a filtrar; Debe proveerse el código del centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se listan todos los servicios del tipo indicado, aunque no tengan parametrización en el centro (LEFT JOIN con HCPARALEINT); La parametrización se asocia únicamente al centro de atención recibido como parámetro; Los códigos de servicio y descripciones se devuelven sin espacios en blanco a la derecha (RTRIM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Interconsulta; Servicios CUPS; Centro de atención; Tiempo máximo de atención; Tiempo máximo de reporte de atención; Auditoría de creación/modificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve servicios cuyo TIPSERIPS coincide con el tipo solicitado, enlazando opcionalmente con la parametrización del centro indicado y los usuarios de creación/modificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCUPSIPS; dbo.HCPARALEINT; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosInterconsultas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosInterconsultas';
-- GO
