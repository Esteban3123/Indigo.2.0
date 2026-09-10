-- Stored Procedure

CREATE PROCEDURE [dbo].[SPREP_CH_CensoHospitalarioFiltraCentroyUnidad]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(100)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.CODICAMAS AS 'CODIGO CAMA', RTRIM(B.DESCCAMAS) AS 'DESCRIPCION CAMA', 
C.CODENTIDA AS 'CODIGO ENTIDAD',A.FECINIEST AS 'FECHA DE INICIO', dbo.DiferenciaDias(A.FECINIEST) AS 'DIAS TRANSCURRIDOS', A.CODTIPEST AS 'TIPO ESTANCIA', A.NUMINGRES AS INGRESO, 
RTRIM(F.DESTIPEST) AS ESTANCIA, COALESCE (NULLIF (A.IPCODPACI, ''), '') AS 'CODIGO PACIENTE', RTRIM(C.IPNOMCOMP) AS 'NOMBRE PACIENTE', C.CODENTIDA AS 'CODIGO ENTIDAD', G.NOMENTIDA AS 'NOMBRE ENTIDAD',D.NOMCENATE AS 'CENTRO DE ATENCION', E.UFUDESCRI AS 'UNIDAD FUNCIONAL',
B.UFUCODIGO 
FROM  CHREGESTA A with(nolock)
INNER JOIN CHCAMASHO B with(nolock) ON A.CODICAMAS=B.CODICAMAS AND A.REGESTADO = 1 
INNER JOIN INPacient C with(nolock) ON A.IPCODPACI=C.IPCODPACI
INNER JOIN ADcenaten D with(nolock) ON B.CODCENATE=D.CODCENATE 
INNER JOIN INUNIFUNC E with(nolock) ON B.UFUCODIGO=E.UFUCODIGO
INNER JOIN CHTIPESTA F with(nolock) ON A.CODTIPEST=F.CODTIPEST
INNER JOIN INENTIDAD G with(nolock) ON C.CODENTIDA=G.CODENTIDA
WHERE B.CODCENATE = @CentroAtencion AND B.UFUCODIGO IN (@UnidadFuncional)

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el censo hospitalario en tiempo real filtrando por un centro de atención y una unidad funcional específicos. Muestra los pacientes actualmente ocupando camas (estado activo en CHREGESTA), incluyendo el código y descripción de la cama, número de ingreso, código y nombre del paciente, entidad aseguradora o pagadora (EPS/ARS), tipo y descripción de la estancia, fecha de inicio de la estancia y la cantidad de días transcurridos desde el ingreso calculada con la función DiferenciaDias. Integra información de camas (CHCAMASHO), datos del paciente (INPACIENT), centro de atención (ADCENATEN), unidad funcional (INUNIFUNC), tipo de estancia (CHTIPESTA) y entidad (INENTIDAD) para producir un reporte operativo de ocupación hospitalaria. Se utiliza para monitoreo de camas, control de estancias, gestión de hospitalización y urgencias por sala o servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las camas ocupadas (censo hospitalario) de un centro de atención y unidad funcional, mostrando datos de paciente, entidad, estancia y tiempo transcurrido desde el ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y la unidad funcional deben existir en sus catálogos.; Las camas deben estar asociadas al centro y unidad funcional indicados.; Solo se consideran registros de estancia con REGESTADO = 1 (estancia activa).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna estancias activas (REGESTADO = 1).; Requiere que el paciente, la entidad, la cama, el centro, la unidad funcional y el tipo de estancia existan (uso de INNER JOIN).; El código de paciente nulo o vacío se normaliza a cadena vacía.; Calcula días transcurridos desde la fecha de inicio de la estancia mediante dbo.DiferenciaDias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'censo hospitalario; cama hospitalaria; paciente; entidad (aseguradora); centro de atención; unidad funcional; tipo de estancia; ingreso hospitalario; días de estancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve filas de camas ocupadas filtradas por CODCENATE = @CentroAtencion y UFUCODIGO IN (@UnidadFuncional), con estancia activa (REGESTADO = 1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DiferenciaDias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INPacient; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHTIPESTA; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroyUnidad';
-- GO
