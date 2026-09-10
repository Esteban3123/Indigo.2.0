CREATE PROCEDURE [dbo].[SP_eHC_ListadoPacientesGrupoAtencion]
(
	@IdEntidades varchar(MAX),
	@FechaInicial datetime,
	@FechaFinal datetime
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
		WHERE A.IFECHAING BETWEEN @FechaInicial AND @FechaFinal AND A.GENCAREGROUP IN (SELECT Value FROM dbo.SplitString(@IdEntidades))  
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que tuvieron ingresos (urgencias, hospitalización, consulta externa u otras modalidades) dentro de un rango de fechas, filtrando por uno o más grupos de atención o administradoras de salud (EPS/aseguradoras). Para cada paciente devuelve el tipo y número de identificación (cédula, tarjeta de identidad, pasaporte, etc.), el número de ingreso, el nombre completo, la entidad pagadora (código y nombre), la edad calculada y la fecha de nacimiento. Se usa para generar listados de pacientes por grupo de atención en un período determinado, apoyando reportería clínica, auditoría de cuentas y seguimiento de población atendida por aseguradora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes ingresados en un rango de fechas, filtrados por uno o varios grupos de atención (administradoras de salud), con sus datos demográficos básicos y entidad responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El listado de identificadores de grupos de atención debe entregarse como cadena delimitada interpretable por dbo.SplitString.; Debe definirse un rango de fechas (inicial y final) para filtrar los ingresos.; Los pacientes deben existir en INPACIENT y los ingresos en ADINGRESO con un grupo de atención válido en Contract.HealthAdministrator.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos cuyo GENCAREGROUP pertenece a la lista de entidades suministrada.; Solo se incluyen ingresos con fecha (IFECHAING) dentro del rango [FechaInicial, FechaFinal].; El campo ''Generado'' siempre se devuelve como 0 (entero), indicando estado inicial no generado.; La edad se calcula contra la fecha actual del servidor (getdate()).; La entidad se presenta concatenando código y nombre con el separador '' - ''.; Solo se listan pacientes con correspondencia entre ADINGRESO, INPACIENT y Contract.HealthAdministrator (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Tipo de identificación; Administradora de salud / Entidad responsable; Grupo de atención; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un resultset con tipo y número de identificación, ingreso, nombre, entidad, edad calculada, fecha de nacimiento y un indicador ''Generado''=0 para cada paciente cuyo ingreso (IFECHAING) cae en el rango y cuyo GENCAREGROUP está en la lista recibida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (valor 1..12) → Mapea a códigos de tipo de documento: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE else NULL (tipo de documento no reconocido)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_eHC_ListadoPacientesGrupoAtencion';
-- GO
