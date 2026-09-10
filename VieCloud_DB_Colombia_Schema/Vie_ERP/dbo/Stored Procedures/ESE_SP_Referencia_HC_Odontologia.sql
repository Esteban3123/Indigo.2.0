Create PROCEDURE [dbo].[ESE_SP_Referencia_HC_Odontologia]
@FechaIni DateTime,
@FechaFin DateTime
AS

SELECT CASE C.IPTIPODOC  WHEN 1 THEN 'Cédula de Ciudadanía' When 2 Then 'Cédula de Extranjería' When 3 Then 'Tarjeta de Identidad' When 4 Then 'Registro Civil' When 5 Then 'Pasaporte' When 6 Then 'Adulto Sin Identificación' When 7 Then 'Menor Sin Identificación' When 8 Then 'Número único de identificación personal' When 9 Then 'Certificado Nacido Vivo' When 10 Then 'Carnet Diplomático' When 11 Then 'Salvoconducto' When 12 Then 'Permiso especial de Permanencia' END AS 'Tipo de ID del paciente',
			C.IPCODPACI AS 'Numero de id del paciente',C.IPPRIAPEL AS 'Apellido 1',C.IPSEGAPEL AS 'Apellido 2',C.IPPRINOMB AS 'Nombre 1',C.IPSEGNOMB AS 'Nombre 2',C.IPFECNACI AS 'Fecha de nacieminto', (cast(datediff(dd,IPFECNACI,[Common].[GETDATE]()) / 365.25 as int)) as 'Edad en años',
			CASE IPSEXOPAC WHEN 1 THEN 'Masculino' when 2 then 'Femenino' end as 'Sexo', D.NUMINGRES AS'Ingreso',
			E.Code AS 'Código Grupo de atención',E.Name AS 'Nombre Grupo de atención',F.CODENTIDA as 'Código Entidad',F.NOMENTIDA as 'Nombre entidad',
			case EntityType when 1 then 'EPS Contributivo' when 2 then 'EPS Subsidiado' when 3 then 'ET Vinculados Municipios' when 4 then 'ET Vinculados Departamentos' when 5 then 'ARL Riesgos Laborales' when 6 then 'MP Medicina Prepagada' when 7 then 'IPS Privada' when 8 then 'IPS Publica' when 9 then 'Regimen Especial' when 10 then 'Accidentes de transito' when 11 then 'Fosyga' when 12 then 'Otros' when 13 then 'Aseguradoras' when 99 then 'Particulares' end as 'Tipo de entidad',
			G.CODPROSAL AS 'Código del profesional',G.NOMMEDICO AS 'Nombre del profesional',
			I.CODDIAGNO AS 'Codigo Diagnostico principal',
			I.NOMDIAGNO AS 'Diagnostico principal'
From dbo.HCHISPACA  A
	INNER JOIN dbo.INPACIENT C WITH (NOLOCK) ON A.IPCODPACI = C.IPCODPACI 
	INNER JOIN dbo.HCREFCONT z WITH (NOLOCK) ON A.IPCODPACI = z.IPCODPACI AND a.NUMINGRES = z.NUMINGRES AND z.NUMEFOLIO = a.NUMEFOLIO
	INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES = D.NUMINGRES 
	INNER JOIN Contract.CareGroup E WITH (NOLOCK) ON E.Id = D.GENCAREGROUP 
	INNER JOIN dbo.INENTIDAD F WITH (NOLOCK) ON F.CODENTIDA = D.CODENTIDA 
	INNER JOIN dbo.INPROFSAL G WITH (NOLOCK) ON G.CODPROSAL = A.CODPROSAL 
	INNER JOIN dbo.INDIAGNOS  I WITH (NOLOCK) ON I.CODDIAGNO  = A.CODDIAGNO
	INNER  JOIN  dbo.HCRIESGOSP J  WITH (NOLOCK) ON J.NUMINGRCES = A.NUMINGRES 
Where  a.IDMODELOHC IN (33,28,42) AND  A.FECHISPAC  BETWEEN @FechaIni and @FechaFin
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera un reporte de historias clínicas odontológicas registradas en un rango de fechas, combinando datos del paciente (identificación, nombre, edad, sexo), del ingreso o admisión, del grupo de atención y entidad pagadora (EPS, ARL, particular, etc.), del profesional de salud tratante y del diagnóstico principal CIE-10. Filtra únicamente los folios correspondientes a modelos de historia clínica odontológica (IDs 28, 33 y 42) y exige que el ingreso tenga asociado un registro de referencia y contrarreferencia (HCREFCONT) y un factor de riesgo en salud pública (HCRIESGOSP). Se usa para seguimiento, auditoría y reportería de la atención odontológica prestada por la institución en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de historias clínicas odontológicas con referencia/contrarreferencia, exponiendo datos del paciente, ingreso, entidad, profesional y diagnóstico principal en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requieren fechas de inicio y fin para acotar FECHISPAC; Deben existir registros en HCHISPACA con IDMODELOHC en (33, 28, 42); Cada historia debe tener referencia/contrarreferencia asociada en HCREFCONT por paciente, ingreso y folio; Cada ingreso debe tener riesgos de salud pública registrados en HCRIESGOSP (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad se calcula como DATEDIFF(dd, fecha_nacimiento, hoy)/365.25 truncado a entero; Solo se reportan atenciones con referencia/contrarreferencia existente (HCREFCONT obligatoria por JOIN); Solo se reportan atenciones cuyo ingreso tiene riesgos en salud pública registrados; El diagnóstico reportado es el principal de la historia (CODDIAGNO de HCHISPACA); El reporte se restringe a modelos de historia clínica odontológicos (33, 28, 42)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica odontológica; Referencia y contrarreferencia; Paciente; Ingreso/admisión; Grupo de atención; Entidad responsable de pago; Tipo de entidad (EPS, ARL, IPS, Medicina Prepagada, Particulares); Profesional de la salud; Diagnóstico principal (CIE-10); Riesgos en salud pública; Tipo de documento de identificación; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas cuando A.IDMODELOHC IN (33,28,42) y A.FECHISPAC BETWEEN @FechaIni AND @FechaFin, traduciendo IPTIPODOC, IPSEXOPAC y EntityType a descripciones legibles', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.IDMODELOHC IN (33,28,42) → Incluye la historia clínica en el reporte (modelos asociados a odontología) else Excluye la historia; si C.IPTIPODOC entre 1 y 12 → Mapea cada código a su descripción de tipo de documento (CC, CE, TI, RC, Pasaporte, etc.); si IPSEXOPAC = 1 o 2 → Traduce a ''Masculino'' o ''Femenino'' respectivamente; si EntityType entre 1-13 o 99 → Traduce código a tipo de entidad (EPS Contributivo, Subsidiado, ARL, Particulares, etc.)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.HCREFCONT; dbo.ADINGRESO; Contract.CareGroup; dbo.INENTIDAD; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.HCRIESGOSP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_HC_Odontologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_HC_Odontologia';
-- GO
