
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarServiciosParametrizadosLaboratorios]
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
		B.CODCENATE as Centro ,RTRIM(A.CODSERIPS) as Codigo ,RTRIM(A.DESSERIPS) as Descripcion, B.LABRECMUE as TiempoMaximoRecoleccion, cast(UNITIEREC as int) AS UnidadRecolectada,
		LABRECMUE2 as ReporteRecolectada,cast(UNITIEREC2 as int) as UnidadReporteRecolectada,LABENTRES as TiempoMaximoResultado,
		cast(UNITIEENT as int) as UnidadResultado,LABENTRES2 as ReporteResultado,cast(UNITIEENT2 as int)  as UnidadReporteResultados , 
		B.ID , B.USUARIOCREACION, B.FECHACREACION, B.USUARIOMODIFICACION, B.FECHAMODIFICACION, 
		Rtrim(U1.CODUSUARI) + ' - ' + Rtrim(U1.NOMUSUARI) AS 'Usuario_Creacion', Rtrim(U2.CODUSUARI) + ' - ' + Rtrim(U2.NOMUSUARI) AS 'Usuario_Modificacion' 
			
	From
		dbo.INCUPSIPS As A 
		Left Outer Join dbo.HCPARALELAB As B On A.CODSERIPS = B.CODSERIPS And B.CODCENATE = @CentroAtencion
		Left Outer Join dbo.SEGusuaru As U1 On B.USUARIOCREACION = U1.CODUSUARI 
		Left Outer Join.dbo.SEGusuaru As U2 On B.USUARIOMODIFICACION = U2.CODUSUARI                                     

	Where 
		A.TIPSERIPS = @TipoServicio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los servicios de laboratorio (exámenes y procedimientos CUPS/IPS) que tienen parámetros de tiempos configurados para un centro de atención específico. Recibe como filtros el tipo de servicio y el centro de atención, y cruza el catálogo general de servicios (INCUPSIPS) con la tabla de parámetros de laboratorio (HCPARALELAB) para mostrar, por cada examen, los tiempos máximos de recolección de muestra y entrega de resultados junto con sus unidades de medida. Además enriquece la información con el nombre completo del usuario que creó y del que realizó la última modificación del registro, consultándolos en el maestro de usuarios del sistema (SEGusuaru). Se utiliza en la administración y configuración de tiempos de respuesta del laboratorio clínico por sede o centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosParametrizadosLaboratorios';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarServiciosParametrizadosLaboratorios';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios IPS de un tipo dado junto con sus parámetros de laboratorio (tiempos de recolección y entrega de resultados) configurados para un centro de atención específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosLaboratorios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un tipo de servicio válido para filtrar el catálogo de servicios IPS; Debe indicarse un centro de atención para emparejar los parámetros de laboratorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosLaboratorios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen servicios cuyo TIPSERIPS coincide con el tipo solicitado; La parametrización de laboratorio se obtiene exclusivamente del centro de atención indicado; servicios sin parametrización en ese centro aparecen con valores nulos por el LEFT JOIN; Los datos de usuario creador/modificador se presentan como ''código - nombre'' concatenados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosLaboratorios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicio IPS; Laboratorio clínico; Centro de atención; Tiempo de recolección de muestra; Tiempo de entrega de resultados; Auditoría de usuario (creación/modificación)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosLaboratorios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve todos los servicios IPS cuyo TIPSERIPS coincide con el tipo solicitado, con sus parámetros de laboratorio del centro indicado (LEFT JOIN, por lo que aparecen aun sin parametrización)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosLaboratorios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCUPSIPS; dbo.HCPARALELAB; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosLaboratorios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarServiciosParametrizadosLaboratorios';
-- GO
