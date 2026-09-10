CREATE PROCEDURE [dbo].[SPHC_ListarHCInterconsultasPacientesNotificaciones]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

--SELECT CASE WHEN B.IDETIPHIS IS NULL THEN 'Historia' ELSE 'Interconsulta' END AS TipoServicio, TIPHISPAC AS TipoReporte, C.IPFECNACI AS 'FechaNacimiento',
--	 a.NUMEFOLIO AS Folio ,A.NUMINGRES AS Ingreso,A.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente,A.FECHISPAC AS FechaEvolucion,
--	   RTRIM(M.NOMMEDICO) AS Medico,N.DESESPECI AS Especialidad,E.NUMCAMHOS As Cama, Rtrim(E.DESCCAMAS) as NombreCama
--FROM HCHISPACA AS A 
--	INNER JOIN INPACIENT AS C ON A.IPCODPACI = C.IPCODPACI 
--	INNER JOIN INPROFSAL AS M ON A.CODPROSAL = M.CODPROSAL  
--	INNER JOIN INESPECIA AS N ON M.CODESPEC1 = N.CODESPECI
--	INNER JOIN ADINGRESO AS I ON A.NUMINGRES = I.NUMINGRES AND I.IESTADOIN IN(' ','P')
--	LEFT JOIN CHCAMASHO AS E ON I.CODCAMACT = E.CODICAMAS
--	LEFT OUTER JOIN HCORDINTE AS B ON A.IPCODPACI =B.IPCODPACI 
--															AND A.NUMEFOLIO =B.NUMFOLINT 
--															AND B.ESTSERIPS ='3'
--WHERE  A.CODCENATE =@CentroAtencion 
--	   AND I.UFUACTPAC=@UnidadFuncional 
--	   AND A.CODUSUARI IS NULL

SELECT CASE WHEN B.IDETIPHIS IS NULL THEN 'Historia' ELSE 'Interconsulta' END AS TipoServicio, TIPHISPAC AS TipoReporte, C.IPFECNACI AS 'FechaNacimiento',
	 a.NUMEFOLIO AS Folio ,A.NUMINGRES AS Ingreso,A.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente,A.FECHISPAC AS FechaEvolucion,
	   RTRIM(M.NOMMEDICO) AS Medico,N.DESESPECI AS Especialidad,E.NUMCAMHOS As Cama, Rtrim(E.DESCCAMAS) as NombreCama
FROM HCHISPACA AS A 
	INNER JOIN INPACIENT AS C ON A.IPCODPACI = C.IPCODPACI 
	INNER JOIN INPROFSAL AS M ON A.CODPROSAL = M.CODPROSAL  
	INNER JOIN INESPECIA AS N ON M.CODESPEC1 = N.CODESPECI
	INNER JOIN ADINGRESO AS I ON A.NUMINGRES = I.NUMINGRES AND I.IESTADOIN IN(' ','P', 'B')
	LEFT JOIN CHCAMASHO AS E ON I.CODCAMACT = E.CODICAMAS
	LEFT OUTER JOIN HCORDINTE AS B ON A.IPCODPACI =B.IPCODPACI 
															AND A.NUMEFOLIO =B.NUMFOLINT 
															AND B.ESTSERIPS ='3'
WHERE  A.CODCENATE =@CentroAtencion 
	   AND I.UFUACTPAC=@UnidadFuncional 
	   AND A.CODUSUARI IS NULL
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las historias clínicas y notas de interconsulta pendientes de firma o notificación para los pacientes actualmente internados en una unidad funcional y centro de atención específicos. Combina los folios de historia clínica (HCHISPACA) con los datos del paciente (nombre, fecha de nacimiento), el médico tratante y su especialidad, la cama hospitalaria asignada y el número de ingreso, filtrando solo ingresos activos o en proceso. Distingue si cada folio corresponde a una ''Historia'' clínica o a una ''Interconsulta'' según si existe una orden médica interna asociada con estado de servicio ''3'' en HCORDINTE. Se utiliza para alimentar notificaciones o bandejas de trabajo médico, mostrando qué notas o interconsultas están abiertas y aún no han sido firmadas por un usuario (CODUSUARI IS NULL).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las evoluciones de historia clínica e interconsultas pendientes (sin usuario que las haya atendido) de pacientes hospitalizados activos en un centro de atención y unidad funcional específicos, para fines de notificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un código de centro de atención válido; Debe proporcionarse un código de unidad funcional válido; Las tablas maestras de pacientes, profesionales, especialidades e ingresos deben tener integridad referencial con la historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan evoluciones aún no firmadas/atendidas por usuario (CODUSUARI IS NULL); Solo se consideran ingresos en estado activo: en blanco, ''P'' o ''B''; La asociación con interconsulta solo aplica cuando su estado de servicio (ESTSERIPS) es ''3''; Se filtra siempre por el centro de atención y la unidad funcional actual del ingreso; La cama puede ser nula (LEFT JOIN) sin excluir el registro; Cada evolución pertenece a un paciente, un profesional con especialidad y un ingreso vigentes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Interconsulta; Paciente; Ingreso hospitalario; Profesional de salud / Médico; Especialidad médica; Cama hospitalaria; Centro de atención; Unidad funcional; Folio de evolución', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHISPACA: Cuando A.CODCENATE=@CentroAtencion AND I.UFUACTPAC=@UnidadFuncional AND A.CODUSUARI IS NULL AND I.IESTADOIN IN ('' '',''P'',''B'') → retorna conjunto con TipoServicio (Historia/Interconsulta), TipoReporte, datos del paciente, folio, ingreso, médico, especialidad y cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCORDINTE.IDETIPHIS IS NULL (no existe orden de interconsulta asociada al folio del paciente con estado ''3'') → El registro se etiqueta como TipoServicio=''Historia'' else Se etiqueta como TipoServicio=''Interconsulta''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.HCORDINTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesNotificaciones';
-- GO
