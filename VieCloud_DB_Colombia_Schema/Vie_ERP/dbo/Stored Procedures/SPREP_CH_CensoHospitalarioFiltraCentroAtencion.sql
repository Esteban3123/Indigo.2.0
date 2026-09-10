CREATE PROCEDURE [dbo].[SPREP_CH_CensoHospitalarioFiltraCentroAtencion]
(
@CentroAtencion Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.CODICAMAS AS 'CODIGO CAMA',B.UFUCODIGO, RTRIM(B.DESCCAMAS) AS 'DESCRIPCION CAMA', 
C.CODENTIDA AS 'CODIGO ENTIDAD',A.FECINIEST AS 'FECHA DE INICIO', dbo.DiferenciaDias(A.FECINIEST) AS 'DIAS TRANSCURRIDOS', A.CODTIPEST AS 'TIPO ESTANCIA', A.NUMINGRES AS INGRESO, 
RTRIM(F.DESTIPEST) AS ESTANCIA, COALESCE (NULLIF (A.IPCODPACI, ''), '') AS 'CODIGO PACIENTE', RTRIM(C.IPNOMCOMP) AS 'NOMBRE PACIENTE', G.NOMENTIDA AS 'NOMBRE ENTIDAD',D.NOMCENATE AS 'CENTRO DE ATENCION', E.UFUDESCRI AS 'UNIDAD FUNCIONAL',  K.NOMENTIDA AS 'NOMBRE ENTIDAD INGRESO'
FROM  CHREGESTA A with(nolock)
INNER JOIN CHCAMASHO B with(nolock) ON A.CODICAMAS=B.CODICAMAS AND A.REGESTADO = 1 
INNER JOIN INPacient C with(nolock) ON A.IPCODPACI=C.IPCODPACI
INNER JOIN ADcenaten D with(nolock) ON B.CODCENATE=D.CODCENATE 
INNER JOIN INUNIFUNC E with(nolock) ON B.UFUCODIGO=E.UFUCODIGO
INNER JOIN CHTIPESTA F with(nolock) ON A.CODTIPEST=F.CODTIPEST
INNER JOIN INENTIDAD G with(nolock) ON C.CODENTIDA=G.CODENTIDA
INNER JOIN ADINGRESO J with(nolock) ON A.NUMINGRES=J.NUMINGRES 
INNER JOIN INENTIDAD K with(nolock) ON J.CODENTIDA=K.CODENTIDA 
WHERE B.CODCENATE = @CentroAtencion order by B.DESCCAMAS ASC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el censo hospitalario activo filtrado por un centro de atención específico, mostrando todas las camas ocupadas en ese momento con los pacientes asignados. Combina información de camas (CHCAMASHO), estados de estancia activos (CHREGESTA con estado=1), datos del paciente (INPACIENT), tipo de estancia (CHTIPESTA), unidad funcional (INUNIFUNC) y entidad aseguradora tanto del paciente como del ingreso (INENTIDAD, ADINGRESO). Devuelve por cada cama ocupada: código y descripción de cama, paciente (cédula y nombre), entidad del paciente y entidad del ingreso, tipo y nombre de estancia, número de ingreso, fecha de inicio de la estancia y días transcurridos en cama. Se utiliza para reportes de ocupación hospitalaria, monitoreo de camas y control de estancias por sede o clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el censo hospitalario (camas activas con su paciente, estancia y entidades) filtrado por un centro de atención específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en ADcenaten y coincidir con CHCAMASHO.CODCENATE.; Solo se consideran registros de estancia activos (CHREGESTA.REGESTADO = 1).; El paciente, la unidad funcional, el tipo de estancia, el ingreso y las entidades del paciente y del ingreso deben existir para que la fila aparezca (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros de estancia activos (REGESTADO = 1).; Solo se reportan camas pertenecientes al centro de atención solicitado.; Los días transcurridos se calculan con dbo.DiferenciaDias sobre la fecha de inicio de estancia.; Si el código de paciente está vacío se devuelve cadena vacía en lugar de NULL.; Se reportan dos entidades distintas: la del paciente y la del ingreso.; Resultado ordenado por descripción de cama ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Censo hospitalario; Cama hospitalaria; Centro de atención; Unidad funcional; Paciente; Estancia hospitalaria; Tipo de estancia; Ingreso; Entidad (aseguradora/responsable); Días transcurridos de estancia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] CHREGESTA: Cuando CHCAMASHO.CODCENATE = parámetro y CHREGESTA.REGESTADO = 1, retorna el listado de camas ocupadas con datos de paciente, estancia, ingreso y entidades, ordenado por descripción de cama ascendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DiferenciaDias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INPacient; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHTIPESTA; dbo.INENTIDAD; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltraCentroAtencion';
-- GO
