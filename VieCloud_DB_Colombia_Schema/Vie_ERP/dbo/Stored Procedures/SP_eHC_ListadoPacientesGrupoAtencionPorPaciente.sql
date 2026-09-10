
CREATE PROCEDURE [dbo].[SP_eHC_ListadoPacientesGrupoAtencionPorPaciente]
(
	@IdEntidades varchar(MAX),
	@Identificacion varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;
	 
	Select   CASE P.IPTIPODOC 
		 WHEN 1 THEN 'CC'
		 WHEN 2 THEN 'CE'
		 WHEN 3 THEN 'TI'
		 WHEN 4 THEN 'RC'
		 WHEN 5 THEN 'PA'
		 WHEN 6 THEN 'AS'
		 WHEN 7 THEN 'MS'
		 WHEN 8 THEN 'NU'
		 WHEN 9 THEN 'CN'
		 WHEN 10 THEN 'CD'
		 WHEN 11 THEN 'SC'
		 WHEN 12 THEN 'PE'
		 END AS TipoIdentificacion,  				
		 rtrim(A.IPCODPACI) as Identificacion,
		 rtrim(A.NUMINGRES) as Ingreso,
		 rtrim(P.IPNOMCOMP) as NombrePaciente,
		 E.Code + ' - ' + E.Name as Entidad,
		 [dbo].[Edad](convert(date,P.IPFECNACI),convert(date,getdate())) as Edad,
		 P.IPFECNACI as FechaNacimiento,
		 convert(int,0) as Generado
		FROM ADINGRESO A with(nolock)
		    INNER JOIN INPACIENT P with(nolock) ON A.IPCODPACI = P.IPCODPACI
			INNER JOIN Contract.HealthAdministrator E with(nolock) ON E.Id = A.GENCAREGROUP   
		WHERE A.IPCODPACI = @Identificacion AND A.GENCAREGROUP IN (SELECT Value FROM dbo.SplitString(@IdEntidades))  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los ingresos o episodios de atención de un paciente específico, filtrado por su número de identificación y por un conjunto de administradoras de salud (EPS, ARS o aseguradoras). Para cada ingreso retorna el tipo y número de documento del paciente, nombre completo, número de ingreso, entidad pagadora (código y nombre), edad calculada y fecha de nacimiento. Se usa para consultar el historial de ingresos de un paciente dentro de grupos de atención o contratos de salud determinados, combinando datos de admisiones, información del paciente y administradoras de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos de un paciente específico filtrados por un conjunto de administradoras/grupos de atención, devolviendo datos demográficos, edad calculada y entidad responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de identificación del paciente debe corresponder a un IPCODPACI existente en ADINGRESO/INPACIENT; La lista de IdEntidades debe ser una cadena parseable por dbo.SplitString y contener IDs válidos de Contract.HealthAdministrator; El paciente debe tener al menos un ingreso asociado a una entidad/grupo de atención dentro del listado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan ingresos cuyo grupo de atención (GENCAREGROUP) esté dentro del listado de entidades recibido; Solo se retornan registros del paciente cuya identificación coincide exactamente con el parámetro recibido; La edad se calcula siempre contra la fecha actual del servidor; El campo ''Generado'' siempre se devuelve como 0 (constante, no proviene de datos); Se requiere que el ingreso tenga paciente y administradora válidos (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Tipo de identificación; Administradora de salud (EPS/Pagador); Grupo de atención; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADINGRESO: Cuando A.IPCODPACI coincide con la identificación y A.GENCAREGROUP está en la lista de entidades parseada, se retorna fila con datos del paciente, ingreso y administradora', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de documento del paciente (IPTIPODOC) entre 1 y 12 → Se traduce el código numérico a su sigla (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE) else Se devuelve NULL como tipo de identificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencionPorPaciente';
-- GO
