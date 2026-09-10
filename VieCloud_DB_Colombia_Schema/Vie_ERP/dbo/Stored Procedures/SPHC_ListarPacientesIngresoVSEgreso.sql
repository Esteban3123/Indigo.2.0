CREATE PROCEDURE  [dbo].[SPHC_ListarPacientesIngresoVSEgreso]
(
       -- Add the parameters for the stored procedure here
	@FechaInicial as date,
	@FechaFinal as date

)
AS
BEGIN

 SELECT A.CODICAMAS AS 'CODIGO CAMA', RTRIM(B.DESCCAMAS) AS 'DESCRIPCION CAMA',
   Z.Code AS 'CODIGO ENTIDAD', A.FECINIEST AS 'FECHA DE INICIO', dbo.DiferenciaDias(A.FECINIEST) AS 'DIAS TRANSCURRIDOS', A.CODTIPEST AS 'TIPO ESTANCIA', A.NUMINGRES AS INGRESO, 
   RTRIM(F.DESTIPEST) AS ESTANCIA, COALESCE (NULLIF (A.IPCODPACI, ''), '') AS 'CODIGO PACIENTE', RTRIM(C.IPNOMCOMP) AS 'NOMBRE PACIENTE', Z.Name AS 'CODIGO ENTIDAD', Z.Name AS 'NOMBRE ENTIDAD', D.NOMCENATE AS 'CENTRO DE ATENCION', E.UFUDESCRI AS 'UNIDAD FUNCIONAL', B.UFUCODIGO, 
   CASE A.FECFINEST WHEN '1900-01-01' THEN  DATEDIFF(DAY, A.FECINIEST ,  Common.GETDATE()) + 1  ELSE   DATEDIFF(DAY, A.FECINIEST ,  A.FECFINEST  ) + 1   END as DIASESTANCIA , Z.Name AS 'NOMBRE ENTIDAD' , K.Name AS 'NOMBRE ENTIDAD INGRESO', 
   rtrim(Prof.CODPROSAL) + ' - ' + rtrim(Prof.NOMMEDICO)  as Medico, 
   rtrim(Espe.CODESPECI) + ' - ' + rtrim(Espe.DESESPECI)  as Especialidad, 
   J.IFECHAING  as 'FECHA INGRESO', DATEDIFF(YEAR, C.IPFECNACI, Common.GETDATE()) as 'Edad'
   FROM dbo.CHREGESTA A 
			   INNER JOIN dbo.CHCAMASHO B ON A.CODICAMAS=B.CODICAMAS AND A.REGESTADO = 1 
			   INNER JOIN dbo.INPacient C ON A.IPCODPACI=C.IPCODPACI 
			   INNER JOIN dbo.ADcenaten D ON B.CODCENATE=D.CODCENATE 
			   INNER JOIN dbo.INUNIFUNC E ON B.UFUCODIGO=E.UFUCODIGO 
			   INNER JOIN dbo.CHTIPESTA F ON A.CODTIPEST=F.CODTIPEST 
			   INNER JOIN contract.healthadministrator Z ON Z.Id = C.GENCONENTITY 
			   INNER JOIN dbo.ADINGRESO J ON A.NUMINGRES=J.NUMINGRES 
			   LEFT OUTER JOIN dbo.INPROFSAL Prof ON A.CODPROSAL = Prof.CODPROSAL 
			   LEFT OUTER JOIN dbo.INESPECIA Espe ON A.CODESPECI = Espe.CODESPECI 
			   INNER JOIN contract.healthadministrator K ON J.GENCONENTITY = K.Id 
  WHERE  cast(A.FECINIEST as date) BETWEEN @FechaInicial AND @FechaFinal
   order by  [DESCRIPCION CAMA]  ASC  

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que se encuentran ingresados u hospitalizados en una cama, comparando su fecha de inicio de estancia con su fecha de egreso, dentro de un rango de fechas dado. Cruza el registro histórico de estados del paciente (cama asignada, tipo de estancia, médico tratante y especialidad) con los datos del paciente (nombre, cédula, edad, entidad aseguradora de afiliación y del ingreso), el centro de atención, la unidad funcional y el tipo de estancia. Calcula los días transcurridos y los días de estancia por cama, permitiendo identificar ocupación hospitalaria, pacientes activos versus egresados y seguimiento de estancias por EPS o aseguradora. Se usa en reportes de censo hospitalario, control de camas, auditoría de ingresos y análisis de rotación de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes con estancia hospitalaria activa cuya fecha de inicio cae en un rango dado, mostrando cama, entidad, médico, especialidad, días de estancia y datos del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas inicial y final deben proveerse para filtrar por FECINIEST; Debe existir el ingreso (ADINGRESO) asociado al registro de estancia; El paciente debe tener entidad de salud (GENCONENTITY) registrada en contract.healthadministrator, tanto a nivel paciente como a nivel ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran estancias con REGESTADO = 1 (activas); Los días de estancia siempre incluyen el día de inicio (suma +1); Se exigen relaciones con cama, paciente, centro de atención, unidad funcional, tipo de estancia, entidad de salud (paciente e ingreso) e ingreso; profesional y especialidad son opcionales; La edad se calcula en años completos entre fecha de nacimiento y fecha actual; El código de paciente nulo o vacío se normaliza a cadena vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; cama hospitalaria; estancia; ingreso; entidad de salud; centro de atención; unidad funcional; tipo de estancia; profesional de salud; especialidad médica; días de estancia; edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CHREGESTA: Devuelve solo registros con REGESTADO = 1 (estancia activa) y FECINIEST entre @FechaInicial y @FechaFinal, ordenados por descripción de cama ascendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.FECFINEST = ''1900-01-01'' (estancia sin fecha de finalización) → Días de estancia = DATEDIFF(día, FECINIEST, fecha actual) + 1 else Días de estancia = DATEDIFF(día, FECINIEST, FECFINEST) + 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.DiferenciaDias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INPacient; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHTIPESTA; contract.healthadministrator; dbo.ADINGRESO; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesIngresoVSEgreso';
-- GO
