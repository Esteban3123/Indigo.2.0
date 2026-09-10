CREATE PROCEDURE [dbo].[SPHC_ListarPatologiasPacientesNotificaciones]
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

--SELECT A.FECORDMED AS FechaSolicitud,RTRIM(C.IPCODPACI) AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente,
--       A.CODSERIPS AS CodigoServicio,RTRIM(B.DESSERIPS) AS DescripcionServicio,A.SERREAINT AS RealizaInterfaz,
--       A.NUMEFOLIO as Folio,A.NUMINGRES as Ingreso, RTRIM(D.NOMMEDICO) AS Medico , A.NOMARCPAT AS Archivo,RTRIM(n.DESESPECI) as Especialidad
       
--FROM HCORDPATO  as A INNER JOIN 
--INCUPSIPS AS B ON A.CODSERIPS =B.CODSERIPS INNER JOIN 
--INPACIENT AS C ON A.IPCODPACI =C.IPCODPACI INNER JOIN
--INPROFSAL AS D ON A.CODPROSAL =D.CODPROSAL 
--INNER JOIN INESPECIA AS N ON D.CODESPEC1 = N.CODESPECI
--INNER JOIN ADINGRESO AS I ON A.NUMINGRES=I.NUMINGRES

--WHERE ESTSERIPS='3' AND A.CODCENATE=@CentroAtencion AND I.UFUACTPAC=@UnidadFuncional 

    -- Insert statements for procedure here
SELECT A.FECORDMED AS FechaSolicitud,RTRIM(C.IPCODPACI) AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente,
       A.CODSERIPS AS CodigoServicio,RTRIM(B.DESSERIPS) AS DescripcionServicio,A.SERREAINT AS RealizaInterfaz,
       A.NUMEFOLIO as Folio,A.NUMINGRES as Ingreso, RTRIM(D.NOMMEDICO) AS Medico , A.NOMARCPAT AS Archivo,RTRIM(n.DESESPECI) as Especialidad
       
FROM HCORDPATO  as A INNER JOIN 
INCUPSIPS AS B ON A.CODSERIPS =B.CODSERIPS INNER JOIN 
INPACIENT AS C ON A.IPCODPACI =C.IPCODPACI INNER JOIN
INPROFSAL AS D ON A.CODPROSAL =D.CODPROSAL 
INNER JOIN INESPECIA AS N ON D.CODESPEC1 = N.CODESPECI
INNER JOIN ADINGRESO AS I ON A.NUMINGRES=I.NUMINGRES AND I.IESTADOIN IN ('', 'P', 'B') 

WHERE ESTSERIPS='3' AND A.CODCENATE=@CentroAtencion AND I.UFUACTPAC=@UnidadFuncional 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de patología e imágenes diagnósticas con resultado disponible (estado 3) que están pendientes de notificación al personal clínico, filtrando por centro de atención y unidad funcional activa. Combina las órdenes médicas de patología (HCORDPATO) con el catálogo de servicios CUPS (INCUPSIPS), los datos del paciente (INPACIENT), el médico solicitante y su especialidad (INPROFSAL, INESPECIA), y el ingreso hospitalario activo o en proceso (ADINGRESO, estados vacío, P o B). Devuelve por cada orden: fecha de solicitud, cédula y nombre del paciente, código y descripción del servicio diagnóstico, número de ingreso, folio, médico tratante, especialidad, nombre del archivo de resultado y si el servicio realiza interfaz externa. Se usa en el módulo de notificaciones clínicas para alertar al equipo asistencial sobre resultados de patología e imágenes ya disponibles que aún no han sido informados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPatologiasPacientesNotificaciones';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPatologiasPacientesNotificaciones';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de patología pendientes de notificación para pacientes activos en un centro de atención y unidad funcional dados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de patología deben estar relacionadas con un servicio CUPS, paciente, profesional de salud, especialidad e ingreso existentes.; El ingreso del paciente debe estar en estado vacío, ''P'' (pendiente) o ''B''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes cuyo servicio tiene estado ''3''.; Solo se consideran ingresos con estado en ('''', ''P'', ''B'').; El centro de atención de la orden debe coincidir con el parámetro recibido.; La unidad funcional actual del paciente en el ingreso debe coincidir con la unidad funcional recibida.; Se usa la primera especialidad (CODESPEC1) del profesional de salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de patología; Paciente; Servicio CUPS; Profesional de salud / Médico; Especialidad; Ingreso; Centro de atención; Unidad funcional; Notificación de resultados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDPATO: Devuelve órdenes de patología con ESTSERIPS=''3'' (estado de servicio específico), filtradas por centro de atención y unidad funcional actual del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPATO; dbo.INCUPSIPS; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPatologiasPacientesNotificaciones';
-- GO
