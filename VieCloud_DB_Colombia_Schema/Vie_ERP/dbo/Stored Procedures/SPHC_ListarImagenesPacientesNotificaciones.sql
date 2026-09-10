CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesNotificaciones]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

--SELECT AUTO, A.FECORDMED AS FechaSolicitud, C.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente, A.CODSERIPS AS CodigoServicio,
--	   RTRIM(B.DESSERIPS) AS DescripcionServicio ,A.SERREAINT AS RealizaInterfaz,
--	   A.NUMEFOLIO AS Folio,A.NUMINGRES AS Ingreso ,RTRIM(D.NOMMEDICO) AS Medico,A.NOMARCIMG AS Archivo,RTRIM(n.DESESPECI) as Especialidad
--	   ,'HCORDIMAG' AS 'Tabla'
--FROM HCORDIMAG  as A with(nolock) INNER JOIN 
--	INCUPSIPS AS B with(nolock) ON A.CODSERIPS =B.CODSERIPS INNER JOIN 
--	INPACIENT AS C with(nolock) ON A.IPCODPACI =C.IPCODPACI INNER JOIN 
--	INPROFSAL AS D with(nolock) ON A.CODPROSAL =D.CODPROSAL 
--	INNER JOIN INESPECIA AS N with(nolock) ON D.CODESPEC1 = N.CODESPECI
--	INNER JOIN ADINGRESO AS I with(nolock) ON A.NUMINGRES=I.NUMINGRES

--WHERE ESTSERIPS='3' AND A.CODCENATE=@CentroAtencion AND I.UFUACTPAC=@UnidadFuncional 

    -- Insert statements for procedure here
SELECT AUTO, A.FECORDMED AS FechaSolicitud, C.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente, A.CODSERIPS AS CodigoServicio,
	   RTRIM(B.DESSERIPS) AS DescripcionServicio ,A.SERREAINT AS RealizaInterfaz,
	   A.NUMEFOLIO AS Folio,A.NUMINGRES AS Ingreso ,RTRIM(D.NOMMEDICO) AS Medico,A.NOMARCIMG AS Archivo,RTRIM(n.DESESPECI) as Especialidad
	   ,'HCORDIMAG' AS 'Tabla'
FROM HCORDIMAG  as A with(nolock) INNER JOIN 
	INCUPSIPS AS B with(nolock) ON A.CODSERIPS =B.CODSERIPS INNER JOIN 
	INPACIENT AS C with(nolock) ON A.IPCODPACI =C.IPCODPACI INNER JOIN 
	INPROFSAL AS D with(nolock) ON A.CODPROSAL =D.CODPROSAL 
	INNER JOIN INESPECIA AS N with(nolock) ON D.CODESPEC1 = N.CODESPECI
	INNER JOIN ADINGRESO AS I with(nolock) ON A.NUMINGRES=I.NUMINGRES AND I.IESTADOIN IN ('', 'P', 'B')

WHERE ESTSERIPS='3' AND A.CODCENATE=@CentroAtencion AND I.UFUACTPAC=@UnidadFuncional 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiografías, ecografías, tomografías, resonancias, etc.) pendientes de notificación para un centro de atención y unidad funcional específicos. Filtra únicamente las órdenes con estado ''completado/disponible'' (ESTSERIPS=3) y cuyos ingresos se encuentren activos (estados vacío, P o B), cruzando la historia clínica de imágenes (HCORDIMAG) con el catálogo de servicios CUPS (INCUPSIPS), los datos del paciente (INPACIENT), el médico solicitante y su especialidad (INPROFSAL, INESPECIA) y el episodio de ingreso (ADINGRESO). Devuelve por cada orden: la identificación y nombre del paciente, el servicio solicitado, la fecha de la orden, el folio, el número de ingreso, el médico, el archivo de imagen generado y la especialidad, para alimentar paneles o alertas de notificación de resultados de imágenes al personal asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas pendientes de pacientes activos, filtradas por centro de atención y unidad funcional, para alimentar notificaciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y la unidad funcional deben existir y coincidir con registros en HCORDIMAG y ADINGRESO.; Las órdenes de imagen deben tener servicios IPS, pacientes, profesionales, especialidades e ingresos relacionados existentes (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes con ESTSERIPS=''3''.; Solo se retornan órdenes vinculadas a ingresos en estado '''', ''P'' o ''B''.; El resultado siempre se etiqueta con la constante ''HCORDIMAG'' como tabla origen.; Se filtra por centro de atención del registro de imagen y por unidad funcional actual del paciente en el ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imágenes diagnósticas; Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Profesional de salud / Médico; Especialidad; Servicio IPS (CUPS); Notificaciones de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDIMAG: Devuelve órdenes de imagen donde ESTSERIPS=''3'', el centro coincide y el ingreso está en estado ('''',''P'',''B'') y pertenece a la unidad funcional indicada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = ''3'' → Solo se consideran órdenes de imagen en ese estado de servicio IPS (pendientes/por realizar).; si ADINGRESO.IESTADOIN IN ('''', ''P'', ''B'') → Solo se incluyen órdenes cuyo ingreso esté en estos estados (activos/permitidos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INCUPSIPS; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarImagenesPacientesNotificaciones';
-- GO
