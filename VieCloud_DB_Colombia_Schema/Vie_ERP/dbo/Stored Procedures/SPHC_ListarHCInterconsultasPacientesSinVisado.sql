
CREATE PROCEDURE [dbo].[SPHC_ListarHCInterconsultasPacientesSinVisado]
(
@CentroAtencion char(10),
@UnidadFuncional char(10),
@CodigoPaciente varchar(25),
@NumeroIngreso char(15)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
			CASE WHEN B.IDETIPHIS IS NULL THEN 'Historia' ELSE 'Interconsulta' END AS TipoServicio, 
			TIPHISPAC AS TipoReporte, 	 a.NUMEFOLIO AS Folio ,
			A.NUMINGRES AS Ingreso,A.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente,A.FECHISPAC AS FechaEvolucion,
	   RTRIM(M.NOMMEDICO) AS Medico,N.DESESPECI AS Especialidad

	FROM HCHISPACA AS A 
		INNER JOIN INPACIENT AS C ON A.IPCODPACI = C.IPCODPACI 
		INNER JOIN INPROFSAL AS M ON A.CODPROSAL = M.CODPROSAL  
		INNER JOIN INESPECIA AS N ON M.CODESPEC1 = N.CODESPECI
		LEFT OUTER JOIN ADINGRESO AS I ON A.NUMINGRES = I.NUMINGRES AND A.UFUCODIGO = I.UFUAACTMED 
		LEFT OUTER JOIN HCORDINTE AS B ON A.IPCODPACI =B.IPCODPACI AND A.NUMEFOLIO =B.NUMFOLINT AND B.ESTSERIPS ='3'
	WHERE  
		A.CODCENATE =@CentroAtencion AND A.UFUCODIGO =@UnidadFuncional  AND A.IPCODPACI =@CodigoPaciente AND A.NUMINGRES =@NumeroIngreso AND A.CODUSUARI IS NULL
	ORDER BY
		A.FECHISPAC DESC

		--AND A.UFUCODIGO =@UnidadFuncional
		

 END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las historias clínicas e interconsultas pendientes de visado (sin firma/aprobación) de un paciente en un ingreso específico, filtrando por centro de atención, unidad funcional, cédula del paciente y número de ingreso. Combina los folios clínicos (HCHISPACA) con los datos del paciente (INPACIENT), el profesional de salud tratante y su especialidad (INPROFSAL, INESPECIA), y determina si cada folio corresponde a una historia clínica o a una interconsulta según la existencia de una orden interna asociada (HCORDINTE) en estado ''3''. Se usa para que jefes de unidad o auditores de calidad identifiquen documentos clínicos que aún no han sido visados por el médico responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las historias clínicas e interconsultas de un paciente en un ingreso específico que aún no han sido visadas (sin usuario que las haya validado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT; El profesional de salud asociado debe existir en INPROFSAL con especialidad en INESPECIA; Deben proveerse centro de atención, unidad funcional, código de paciente y número de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan registros de historia clínica sin visar (CODUSUARI IS NULL); Solo se consideran órdenes de interconsulta con estado ''3'' para clasificar como Interconsulta; El resultado siempre se ordena por fecha de evolución descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Interconsulta; Paciente; Ingreso hospitalario; Visado/validación de historia; Profesional de salud; Especialidad médica; Centro de atención; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACA: Devuelve registros de historia/interconsulta del paciente filtrando por centro de atención, unidad funcional, paciente e ingreso, solo cuando CODUSUARI IS NULL (no visados), ordenados por FECHISPAC DESC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCORDINTE.IDETIPHIS IS NULL (no existe orden de interconsulta vinculada con ESTSERIPS=''3'') → Clasifica el registro como ''Historia'' else Clasifica el registro como ''Interconsulta''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO; dbo.HCORDINTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarHCInterconsultasPacientesSinVisado';
-- GO
