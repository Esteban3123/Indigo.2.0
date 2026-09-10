CREATE PROCEDURE [dbo].[SPHC_ListarLaboratoriosPacientesNotificaciones]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

--SELECT A.AUTO, A.FECORDMED AS FechaSolicitud ,RTRIM(C.IPCODPACI) AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente, A.CODSERIPS AS CodigoServicio,RTRIM(B.DESSERIPS)AS DescripcionServicio,
--A.NUMEFOLIO AS Folio ,A.NUMINGRES AS Ingreso,RTRIM (D.NOMMEDICO) AS Medico,A.NOMARCLAB AS Archivo,RTRIM(n.DESESPECI) as Especialidad, 0 AS AlertaLAB
--FROM HCORDLABO as A
--INNER JOIN INCUPSIPS AS B ON A.CODSERIPS =B.CODSERIPS
--INNER JOIN INPACIENT AS C ON A.IPCODPACI =C.IPCODPACI 
--INNER JOIN INPROFSAL  AS D ON A.CODPROSAL =D.CODPROSAL 
--INNER JOIN INESPECIA AS N ON D.CODESPEC1 = N.CODESPECI
--INNER JOIN ADINGRESO AS I ON A.NUMINGRES=I.NUMINGRES
--WHERE ESTSERIPS='3' AND A.CODCENATE=@CentroAtencion AND I.UFUACTPAC=@UnidadFuncional 

    -- Insert statements for procedure here
SELECT A.AUTO, A.FECORDMED AS FechaSolicitud ,RTRIM(C.IPCODPACI) AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente, A.CODSERIPS AS CodigoServicio,RTRIM(B.DESSERIPS)AS DescripcionServicio,
A.NUMEFOLIO AS Folio ,A.NUMINGRES AS Ingreso,RTRIM (D.NOMMEDICO) AS Medico,A.NOMARCLAB AS Archivo,RTRIM(n.DESESPECI) as Especialidad, 0 AS AlertaLAB
FROM HCORDLABO as A
INNER JOIN INCUPSIPS AS B ON A.CODSERIPS =B.CODSERIPS
INNER JOIN INPACIENT AS C ON A.IPCODPACI =C.IPCODPACI 
INNER JOIN INPROFSAL  AS D ON A.CODPROSAL =D.CODPROSAL 
INNER JOIN INESPECIA AS N ON D.CODESPEC1 = N.CODESPECI
INNER JOIN ADINGRESO AS I ON A.NUMINGRES=I.NUMINGRES AND I.IESTADOIN IN ('', 'P', 'B')
WHERE ESTSERIPS='3' AND A.CODCENATE=@CentroAtencion AND I.UFUACTPAC=@UnidadFuncional 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de laboratorio con resultados disponibles (estado de servicio = ''3'') pendientes de notificación para los pacientes actualmente hospitalizados o en atención (ingresos activos, programados o en observación) en un centro de atención y unidad funcional específicos. Combina las órdenes de laboratorio de la historia clínica (HCORDLABO) con el catálogo de servicios CUPS/IPS para obtener el nombre del examen, el maestro de pacientes para la cédula y nombre del paciente, el maestro de profesionales para el médico solicitante, el catálogo de especialidades médicas y los registros de ingreso para filtrar solo pacientes activos en la unidad funcional indicada. Se usa en el módulo de notificaciones clínicas para alertar al personal de enfermería o médico sobre resultados de laboratorio listos que aún no han sido comunicados al equipo tratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de laboratorio pendientes de notificar para los pacientes activos en un centro de atención y unidad funcional específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y la unidad funcional deben existir y corresponder a valores válidos en las tablas de ingresos y órdenes.; Las órdenes de laboratorio deben tener servicios IPS registrados en el catálogo de CUPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo AlertaLAB siempre se devuelve con valor 0 (sin alerta).; Únicamente se listan órdenes asociadas a un ingreso vigente (estado '''', ''P'' o ''B'').; Los textos de paciente, nombre, médico, servicio y especialidad se devuelven sin espacios a la derecha (RTRIM).; Solo se retornan órdenes con servicio IPS estado ''3'' del centro y unidad funcional indicados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de laboratorio; Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Profesional de la salud / Médico; Especialidad médica; Servicio IPS (CUPS); Notificaciones de laboratorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDLABO: Devuelve órdenes de laboratorio cuyo ESTSERIPS=''3'' (estado del servicio), filtradas por centro de atención y por unidad funcional actual del paciente en el ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ADINGRESO.IESTADOIN IN ('''', ''P'', ''B'') → Solo se consideran ingresos cuyo estado sea vacío, ''P'' o ''B'' (pacientes activos/pendientes); otros estados quedan excluidos del resultado.; si HCORDLABO.ESTSERIPS = ''3'' → Solo se incluyen órdenes de laboratorio en estado ''3'' (pendientes/notificables).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INCUPSIPS; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarLaboratoriosPacientesNotificaciones';
-- GO
