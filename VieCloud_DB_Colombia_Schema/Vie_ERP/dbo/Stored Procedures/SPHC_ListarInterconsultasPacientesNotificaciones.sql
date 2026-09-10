
CREATE PROCEDURE [dbo].[SPHC_ListarInterconsultasPacientesNotificaciones]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

--SELECT TIPHISPAC AS TipoReporte,
--	 a.NUMEFOLIO AS Folio ,A.NUMINGRES AS Ingreso,A.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente,A.FECHISPAC AS FechaEvolucion,
--	   RTRIM(M.NOMMEDICO) AS Medico,Rtrim(N.DESESPECI) AS Especialidad, Rtrim(E.DESCCAMAS) as NombreCama

--FROM HCHISPACA AS A 
--INNER JOIN INPACIENT AS C ON A.IPCODPACI =C.IPCODPACI 
--INNER JOIN INPROFSAL AS M ON M.CODPROSAL =A.CODPROSAL  
--INNER JOIN INESPECIA AS N ON M.CODESPEC1 = N.CODESPECI
--INNER JOIN ADINGRESO AS I ON A.NUMINGRES=I.NUMINGRES
--INNER JOIN HCORDINTE AS B ON A.IPCODPACI =B.IPCODPACI AND A.NUMEFOLIO =B.NUMFOLINT AND B.ESTSERIPS ='3'
--INNER JOIN CHCAMASHO AS E ON I.CODCAMACT = E.CODICAMAS
--WHERE  A.CODCENATE =@CentroAtencion  AND A.CODUSUARI IS NULL AND I.UFUACTPAC=@UnidadFuncional 

    -- Insert statements for procedure here
SELECT TIPHISPAC AS TipoReporte,
	 a.NUMEFOLIO AS Folio ,A.NUMINGRES AS Ingreso,A.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente,A.FECHISPAC AS FechaEvolucion,
	   RTRIM(M.NOMMEDICO) AS Medico,Rtrim(N.DESESPECI) AS Especialidad, Rtrim(E.DESCCAMAS) as NombreCama

FROM HCHISPACA AS A 
INNER JOIN INPACIENT AS C ON A.IPCODPACI =C.IPCODPACI 
INNER JOIN INPROFSAL AS M ON M.CODPROSAL =A.CODPROSAL  
INNER JOIN INESPECIA AS N ON M.CODESPEC1 = N.CODESPECI
INNER JOIN ADINGRESO AS I ON A.NUMINGRES=I.NUMINGRES AND I.IESTADOIN IN ('', 'P', 'B')
INNER JOIN HCORDINTE AS B ON A.IPCODPACI =B.IPCODPACI AND A.NUMEFOLIO =B.NUMFOLINT AND B.ESTSERIPS ='3'
INNER JOIN CHCAMASHO AS E ON I.CODCAMACT = E.CODICAMAS
WHERE  A.CODCENATE =@CentroAtencion  AND A.CODUSUARI IS NULL AND I.UFUACTPAC=@UnidadFuncional 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las interconsultas médicas pendientes de notificación para los pacientes actualmente hospitalizados en una unidad funcional y centro de atención específicos. Cruza las historias clínicas (HCHISPACA) con las órdenes médicas internas (HCORDINTE) que tienen estado ''3'' (interconsulta solicitada/pendiente), filtrando solo ingresos activos (ADINGRESO con estado vacío, ''P'' o ''B''). Enriquece el resultado con el nombre del paciente (INPACIENT), el médico tratante y su especialidad (INPROFSAL, INESPECIA), y la cama hospitalaria asignada (CHCAMASHO). Se utiliza para notificar a los especialistas sobre interconsultas pendientes de atender en pacientes hospitalizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarInterconsultasPacientesNotificaciones';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarInterconsultasPacientesNotificaciones';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista interconsultas pendientes de notificación para pacientes hospitalizados activos en un centro de atención y unidad funcional específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un ingreso con estado en ('''', ''P'', ''B'') (activo/pendiente); La interconsulta (HCORDINTE) debe estar en estado ESTSERIPS=''3''; La historia clínica (HCHISPACA) no debe tener usuario asignado (CODUSUARI IS NULL); El ingreso debe pertenecer a la unidad funcional indicada (UFUACTPAC); La historia debe pertenecer al centro de atención indicado (CODCENATE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna interconsultas con estado de servicio = ''3''; Solo considera ingresos cuyo estado esté en el conjunto ('''', ''P'', ''B''); Solo retorna registros de historia clínica sin usuario responsable asignado (pendientes de atender/notificar); Filtra siempre por centro de atención y unidad funcional actual del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Interconsulta; Paciente; Ingreso hospitalario; Historia clínica; Especialidad médica; Profesional de salud (médico); Cama hospitalaria; Centro de atención; Unidad funcional; Notificación de interconsulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACA: Devuelve interconsultas pendientes uniendo historia clínica, paciente, profesional, especialidad, ingreso, orden de interconsulta y cama, filtrando por centro, unidad funcional, estado de ingreso ('''', ''P'', ''B''), interconsulta con ESTSERIPS=''3'' y sin usuario asignado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO; dbo.HCORDINTE; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarInterconsultasPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarInterconsultasPacientesNotificaciones';
-- GO
