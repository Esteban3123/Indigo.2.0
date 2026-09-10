

CREATE PROCEDURE [dbo].[SPREP_CH_CensoHospitalario]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT A.CODICAMAS AS 'CODIGO CAMA', RTRIM(B.DESCCAMAS) AS 'DESCRIPCION CAMA', 
A.FECINIEST AS 'FECHA DE INICIO', dbo.DiferenciaDias(A.FECINIEST) AS 'DIAS TRANSCURRIDOS', A.CODTIPEST AS 'TIPO ESTANCIA', A.NUMINGRES AS INGRESO, 
C.CODENTIDA AS 'CODIGO ENTIDAD',RTRIM(F.DESTIPEST) AS ESTANCIA, COALESCE (NULLIF (A.IPCODPACI, ''), '') AS 'CODIGO PACIENTE', RTRIM(C.IPNOMCOMP) AS 'NOMBRE PACIENTE', G.NOMENTIDA AS 'NOMBRE ENTIDAD',D.NOMCENATE AS 'CENTRO DE ATENCION', E.UFUDESCRI AS 'UNIDAD FUNCIONAL',
B.UFUCODIGO AS UFUCODIGO,  K.NOMENTIDA AS 'NOMBRE ENTIDAD INGRESO', DATEDIFF(YEAR, C.IPFECNACI, Common.GETDATE()) as 'Edad', rtrim(Prof.CODPROSAL) + ' - ' + rtrim(Prof.NOMMEDICO)  as Medico,  rtrim(Espe.CODESPECI) + ' - ' + rtrim(Espe.DESESPECI) AS Especialidad,
CASE WHEN X.INDICAPAC = '22' THEN '2 - Pre-alta hospitalaria' WHEN I.NUMINGRES IS NULL THEN '1 - Pacientes en la unidad' ELSE '3 - Pacientes con salida' END AS TipoEstancia
FROM  CHREGESTA A WITH(NOLOCK)
INNER JOIN CHCAMASHO B WITH(NOLOCK) ON A.CODICAMAS=B.CODICAMAS AND A.REGESTADO = 1 
INNER JOIN INPacient C WITH(NOLOCK) ON A.IPCODPACI=C.IPCODPACI
INNER JOIN ADcenaten D WITH(NOLOCK) ON B.CODCENATE=D.CODCENATE 
INNER JOIN INUNIFUNC E WITH(NOLOCK) ON B.UFUCODIGO=E.UFUCODIGO
INNER JOIN CHTIPESTA F WITH(NOLOCK) ON A.CODTIPEST=F.CODTIPEST
INNER JOIN INENTIDAD G WITH(NOLOCK) ON C.CODENTIDA=G.CODENTIDA
INNER JOIN ADINGRESO J WITH(NOLOCK) ON A.NUMINGRES=J.NUMINGRES 
INNER JOIN INENTIDAD K WITH(NOLOCK) ON J.CODENTIDA=K.CODENTIDA 
LEFT OUTER JOIN INPROFSAL Prof ON A.CODPROSAL = Prof.CODPROSAL  
LEFT OUTER JOIN INESPECIA Espe ON A.CODESPECI = Espe.CODESPECI  
LEFT OUTER JOIN HCREGEGRE I with(nolock) ON A.NUMINGRES = I.NUMINGRES
Outer apply 
(select TOP 1 INDICAPAC,IPCODPACI, NUMINGRES from HCHISPACA where IPCODPACI = J.IPCODPACI AND NUMINGRES = J.NUMINGRES order by FECHISPAC desc) as X 
order by B.DESCCAMAS ASC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el censo hospitalario en tiempo real, mostrando todas las camas actualmente ocupadas en el hospital con el paciente asignado, su ingreso, entidad aseguradora (EPS), centro de atención, unidad funcional, médico tratante, especialidad y días de estancia transcurridos. Integra información de camas (CHCAMASHO), estados de estancia (CHREGESTA), datos del paciente (INPACIENT), admisión (ADINGRESO), entidades pagadoras (INENTIDAD), profesionales de salud (INPROFSAL) y especialidades (INESPECIA) para construir una vista consolidada del estado de ocupación. Clasifica cada paciente en tres categorías: en la unidad, con pre-alta hospitalaria o con salida registrada, lo que permite al área de hospitalización, enfermería y admisiones conocer en todo momento quién está internado, en qué cama, cuántos días lleva y bajo qué aseguradora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CensoHospitalario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CensoHospitalario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un censo hospitalario listando las camas y su ocupación actual con datos del paciente, ingreso, entidad responsable, médico, especialidad y clasificación de estancia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros de estancia activos (REGESTADO = 1) en la tabla de estancias.; Cada estancia debe tener cama, paciente, centro de atención, unidad funcional, tipo de estancia, entidad del paciente e ingreso con entidad asociada (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen estancias con REGESTADO = 1 (estancia vigente).; La edad se calcula como diferencia en años entre la fecha de nacimiento del paciente y la fecha actual del sistema (Common.GETDATE()).; Los días transcurridos de estancia se obtienen mediante la función dbo.DiferenciaDias sobre la fecha de inicio de la estancia.; El código de paciente vacío se normaliza a cadena vacía mediante COALESCE/NULLIF.; La clasificación de tipo de estancia es mutuamente excluyente: pre-alta, en unidad o con salida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Censo hospitalario; Cama hospitalaria; Estancia; Ingreso hospitalario; Paciente; Entidad responsable (asegurador/pagador); Centro de atención; Unidad funcional; Médico tratante; Especialidad; Egreso hospitalario; Pre-alta hospitalaria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto ordenado por descripción de cama (ASC) con la información de cada cama ocupada cuyo registro de estancia esté activo (REGESTADO = 1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Última historia clínica del ingreso (HCHISPACA) tiene INDICAPAC = ''22'' → Clasifica la estancia como ''2 - Pre-alta hospitalaria'' else Si no existe registro de egreso (HCREGEGRE.NUMINGRES IS NULL) clasifica como ''1 - Pacientes en la unidad''; en caso contrario ''3 - Pacientes con salida''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DiferenciaDias; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INPacient; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHTIPESTA; dbo.INENTIDAD; dbo.ADINGRESO; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCREGEGRE; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalario';
-- GO
