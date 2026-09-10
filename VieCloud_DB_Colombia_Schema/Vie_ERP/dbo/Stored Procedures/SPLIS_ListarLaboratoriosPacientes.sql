CREATE PROCEDURE [dbo].[SPLIS_ListarLaboratoriosPacientes]
(
@Estado varchar(10),
@CentroAtencion varchar(100),
@ExamenEnSitio Bit
)
WITH RECOMPILE 
AS
BEGIN

	SET NOCOUNT ON;
 
  WITH Tmp_ConsultasOncologicas AS (
	  SELECT  A.AUTO, A.IDETIPHIS, A.FECORDMED, A.IPCODPACI, b.IPNOMCOMP, E.UFUACTPAC, c.UFUDESCRI AS 'UFUDESCRIC1', c2.UFUDESCRI AS 'UFUDESCRIc2', a.CODCENATE, a.ESTSERIPS, a.ESTALELAB, a.NUMINGRES,
	  a.NUMEFOLIO, a.CODSERIPS, d.DESSERIPS, A.OBSSERIPS, b.IPRHSANGR, b.IPGRUPSAN,
	  b.IPDIRECCI, b.IPTELEFON, b.IPSEXOPAC, b.CORELEPAC, b.IPTELMOVI,
	  A.UFUCODIGO, A.CODDIAGNO, A.CODPROSAL, IPTIPODOC, B.IPPRINOMB, B.IPSEGNOMB, B.IPPRIAPEL, B.IPSEGAPEL, B.IPFECNACI, B.IPTIPOPAC, B.IPTIPOAFI, A.CANSERIPS, D.CODGOCUPS,
	  a.CONCURRE, a.SERREAINT, ent.CODENTIDA, ent.NOMENTIDA, e.CODTIPPAC, a.PRISERIPS, a.FECHASUGE, e.CODCAMACT, b.AUUBICACI, a.IDDESCRIPCIONRELACIONADA, a.FECRECMUE, A.CODMOTIVOMUESTRANOCONFORME,
	  MUN.DEPMUNCOD, MUN.MUNNOMBRE, CA.NOMCENATE, H.NOMMEDICO, D.SERIPSPOS
	  FROM  dbo.HCORDLABO AS A WITH(NOLOCK)  
			  INNER JOIN INPACIENT AS B WITH(NOLOCK)  ON A.IPCODPACI = B.IPCODPACI 
			  INNER JOIN INCUPSIPS AS D WITH(NOLOCK)  ON A.CODSERIPS = D .CODSERIPS 
			  INNER JOIN ADINGRESO AS E WITH(NOLOCK)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES and E.IESTADOIN IN ('','P','C')
			  INNER JOIN INENTIDAD AS ENT WITH(NOLOCK)  ON ENT.CODENTIDA = E.CODENTIDA 
			  INNER JOIN INUNIFUNC AS C WITH(NOLOCK)  ON E.UFUACTPAC = C.UFUCODIGO 
			  INNER JOIN INUNIFUNC AS C2 WITH(NOLOCK)  ON A.UFUCODIGO = C2.UFUCODIGO
			  INNER JOIN INUBICACI UBI WITH(NOLOCK)  ON b.AUUBICACI = UBI.AUUBICACI
			  INNER JOIN INMUNICIP MUN WITH(NOLOCK)  ON UBI.DEPMUNCOD= MUN.DEPMUNCOD
			  INNER JOIN ADCENATEN CA WITH(NOLOCK) ON CA.CODCENATE = A.CODCENATE
			  INNER JOIN INPROFSAL AS H WITH(NOLOCK)  ON A.CODPROSAL =H.CODPROSAL  
	  WHERE  A.ESTSERIPS IN (SELECT Value FROM dbo.splitstring((@Estado))) and A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) and A.EXMREASIT = @ExamenEnSitio 
	)
 
     SELECT DISTINCT tmp.[AUTO],tmp.IDETIPHIS,tmp.FECORDMED AS 'FechaSolicitud', RTRIM(tmp.IPCODPACI) AS 'CodigoPaciente', 
      RTRIM(tmp.IPNOMCOMP) AS 'NombrePaciente', tmp.IPFECNACI AS 'FechaNacimiento', 
      tmp.UFUACTPAC AS 'CodigoUnidad', RTRIM(tmp.UFUDESCRIc1) AS 'DescripcionUnidad',tmp.UFUDESCRIc2 AS 'UnidadSolicitante', tmp.CODCENATE AS 'CodigoCentro',
	  CASE tmp.ESTSERIPS WHEN 1 THEN 'Solicitado' WHEN 2 THEN 'Muestra recolectada' WHEN 3 THEN 'PENDiente interpretación' WHEN 4 THEN 'Examen interpretado' WHEN 5 THEN 'Estudio remitido' 
	  WHEN 6 THEN 'Anulado' WHEN 7 THEN 'Extramural' WHEN 8 THEN 'Muestra recolectada parcialmente' WHEN 9 THEN 'Muestra no conforme' END AS 'Estado', 
	  tmp.ESTALELAB AS 'EstadoAlerta', tmp.NUMINGRES AS 'Ingreso', tmp.NUMEFOLIO AS 'Folio', RTRIM(tmp.CODSERIPS)+'-'+RTRIM(tmp.DESSERIPS) + '. ' + ISNULL(CD.name,'') AS 'DescripcionServicio', RTRIM(CD.name) AS 'DescripcionRelacionada', tmp.OBSSERIPS AS 'ObservacionServicio', RTRIM(tmp.CODSERIPS) AS 'CodigoServicio', 
	  RTRIM(I.NOMDIAGNO) AS 'NombreDiagnostico', RTRIM(G.DESCCAMAS) AS 'Cama', g.CODAISLAM AS 'TipoAislamiento',
      tmp.IPDIRECCI AS 'Direccion', tmp.IPTELEFON AS 'Telefono',tmp.IPSEXOPAC AS 'Sexo', tmp.IPRHSANGR AS 'Rh', tmp.IPGRUPSAN AS 'GrupoSanguineo', tmp.CORELEPAC,
      tmp.IPTELMOVI AS 'Movil', tmp.CORELEPAC AS 'Correo', tmp.NOMMEDICO  AS 'Medico', 
	  0 AS 'Minutos', CAST('' AS CHAR) AS 'Barra',  CAST('' AS bit) AS 'Marcar',
      tmp.CONCURRE AS 'Concurrencia', tmp.SERREAINT AS 'RealizaInterfaz', tmp.UFUCODIGO , tmp.CODDIAGNO, tmp.CODPROSAL, IPTIPODOC, tmp.IPPRINOMB, tmp.IPSEGNOMB, tmp.IPPRIAPEL, tmp.IPSEGAPEL, tmp.IPSEXOPAC, tmp.IPFECNACI, tmp.CODENTIDA, tmp.IPTIPOPAC, tmp.IPTIPOAFI, tmp.IPTELEFON, tmp.IPTELMOVI, tmp.IPDIRECCI, tmp.CANSERIPS, tmp.CODGOCUPS,
	  CASE tmp.IDETIPHIS WHEN 'CODIGOAZU' THEN 'Emergencia' ELSE 'Historia' END AS 'TipoSolicitud',
	  RTRIM(tmp.DEPMUNCOD) AS 'MunicipioCodigo', RTRIM(tmp.MUNNOMBRE) AS 'MunicipioNombre',
	  RTRIM(tmp.CODENTIDA) AS 'entidadpagadoraCodigo', RTRIM(tmp.NOMENTIDA) AS 'entidadpagadoraNombre', tmp.CODTIPPAC,
	  CASE WHEN tmp.PRISERIPS = '1' THEN 'Urgente' ELSE 'Rutinario' END AS 'Prioridad',
	  RP.GESTACION, CASE WHEN EG.FECALTPAC IS NULL THEN 'Pacientes en la unidad' ELSE 'Pacientes cON salida' END AS 'PacienteSalida',
	  'Autorización' AS 'Autorizacion',
	  tmp.AUTO EntityId, tmp.FECHASUGE, RTRIM(tmp.NOMCENATE) AS 'CentroAtencion', z.Color,
	  dbo.Edad(tmp.IPFECNACI, CommON.GETDATE())  AS Edad, tmp.FECRECMUE AS 'FechaRecoleccion',
	  tmp.CODMOTIVOMUESTRANOCONFORME AS 'CodigoMotivoMuestraNoConforme',
	  RTRIM(MA.DESMOTANU) AS 'DescripcionMotivoMuestraNoConforme',
	  RTRIM(CenRem.DESCENLAB) AS 'CentroRemision',
	  CASE tmp.SERIPSPOS WHEN 0 THEN 'No' WHEN 1 THEN 'Si' END AS 'FinanciadoUPC',
	  (SELECT TOP 1 cabe.AUTO FROM INTERCABE cabe WITH(NOLOCK) INNER join INTERDETA deta WITH(NOLOCK) ON cabe.AUTO = deta.CODCONCEC and cabe.NUMINGRES = tmp.NUMINGRES and  ORDTIP = 'INT' where cabe.IPCODPACI = tmp.IPCODPACI and cabe.NUMINGRES = tmp.NUMINGRES and deta.CODSERIPS = tmp.CODSERIPS ) AS 'IDMuestra'
	  FROM  Tmp_ConsultasOncologicas tmp 
			  LEFT JOIN  CHCAMASHO AS G WITH(NOLOCK)  ON tmp.CODCAMACT = G.CODICAMAS 
			  LEFT JOIN CHTIPOSAISLAMIENTOS Z WITH(NOLOCK) ON z.Id = g.CODAISLAM 
			  LEFT JOIN INDIAGNOS AS I WITH(NOLOCK)  ON tmp.CODDIAGNO = I.CODDIAGNO 
			  LEFT JOIN HCRIESGOSP AS RP WITH(NOLOCK) ON RP.NUMINGRCES = tmp.NUMINGRES
			  LEFT JOIN HCREGEGRE AS EG WITH(NOLOCK)  ON EG.NUMINGRES = tmp.NUMINGRES
			  LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = tmp.IDDESCRIPCIONRELACIONADA
			  LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId
			  LEFT JOIN HCMOANULB MA WITH(NOLOCK) ON MA.CODMOTANU = tmp.CODMOTIVOMUESTRANOCONFORME
			  LEFT JOIN HCLABREMI Rem WITH(NOLOCK) ON  rem.IPCODPACI = tmp.IPCODPACI and rem.NUMINGRES = tmp.NUMINGRES  and Rem.NUMEFOLIO = tmp.NUMEFOLIO and rem.CODSERIPS = tmp.CODSERIPS  and Rem.CODCENATE = tmp.CODCENATE AND Rem.UFUCODIGO = tmp.UFUCODIGO
			  LEFT JOIN HCCENLABU CenRem WITH(NOLOCK) ON CenRem.CODCENLAB = Rem.CODCENLAB 	  
	  ORDER BY tmp.FECORDMED DESC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de laboratorio clínico de pacientes activos o en proceso de atención, combinando datos de la orden (estado, folio, servicio CUPS, prioridad, fecha de solicitud y recolección de muestra) con información del paciente (cédula, nombre, fecha de nacimiento, sexo, grupo sanguíneo, dirección, teléfono, municipio de residencia), del ingreso hospitalario (número de ingreso, unidad funcional actual, cama, tipo de aislamiento, entidad pagadora/EPS) y del profesional solicitante. Permite filtrar por estado de la orden (solicitado, muestra recolectada, interpretado, anulado, etc.), por centro de atención y por si el examen se realiza en sitio o se remite a un laboratorio externo. Se usa en el módulo de laboratorio para que el personal asistencial y de toma de muestras visualice la lista de trabajo (worklist) de exámenes pendientes o en curso de los pacientes hospitalizados, en urgencias o consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de laboratorio de pacientes filtradas por estado(s), centro(s) de atención y si el examen es en sitio, enriqueciendo con datos demográficos, clínicos, ubicación, entidad pagadora, cama, diagnóstico y muestra asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@Estado debe ser una lista delimitada parseable por dbo.splitstring con valores válidos de ESTSERIPS (1-9); @CentroAtencion debe ser una lista delimitada parseable por dbo.splitstring con códigos de centro de atención existentes; @ExamenEnSitio debe coincidir con el flag EXMREASIT de la orden; Solo se consideran ingresos en estado vacío, ''P'' (pendiente) o ''C'' (cerrado) en ADINGRESO.IESTADOIN; La orden debe tener paciente, servicio CUPS, ingreso, entidad, unidad funcional actual y solicitante, ubicación con municipio, centro de atención y profesional de salud existentes (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes cuyo ingreso está activo/pendiente/cerrado (IESTADOIN en '''', ''P'', ''C''); El filtro de estado y centro siempre se aplica vía splitstring permitiendo múltiples valores; La edad se calcula dinámicamente con dbo.Edad respecto a Common.GetDate(); IDMuestra se obtiene como TOP 1 de INTERCABE/INTERDETA con ORDTIP=''INT'' coincidente en paciente, ingreso y servicio; Se usa NOLOCK en todas las lecturas (lectura sucia consistente con reportes); La consulta es de solo lectura; no modifica datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Entidad pagadora; Diagnóstico; Cama y tipo de aislamiento; Profesional de salud (médico); Prioridad de examen (urgente/rutinario); Muestra de laboratorio (recolección, no conforme, motivo de anulación); Centro de remisión de laboratorio; Financiación UPC; Gestación / riesgo en embarazo; Egreso del paciente; Tipo de historia (emergencia/historia); Examen en sitio / extramural; Servicio CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve órdenes de laboratorio ordenadas por FECORDMED DESC con estado traducido (1=Solicitado,…,9=Muestra no conforme), prioridad (''1''=Urgente, otro=Rutinario) y tipo de solicitud (''CODIGOAZU''=Emergencia, otro=Historia)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = 1..9 → Traduce a etiqueta textual del estado de la orden (Solicitado, Muestra recolectada, Pendiente interpretación, Examen interpretado, Estudio remitido, Anulado, Extramural, Muestra recolectada parcialmente, Muestra no conforme); si IDETIPHIS = ''CODIGOAZU'' → TipoSolicitud = ''Emergencia'' else TipoSolicitud = ''Historia''; si PRISERIPS = ''1'' → Prioridad = ''Urgente'' else Prioridad = ''Rutinario''; si HCREGEGRE.FECALTPAC IS NULL → PacienteSalida = ''Pacientes en la unidad'' else PacienteSalida = ''Pacientes con salida''; si SERIPSPOS = 0 / 1 → FinanciadoUPC = ''No'' / ''Si''; si ADINGRESO.IESTADOIN IN ('''',''P'',''C'') → Incluye el ingreso en el resultado else Excluye la orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; dbo.Edad; Common.GetDate', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADCENATEN; dbo.INPROFSAL; dbo.CHCAMASHO; dbo.CHTIPOSAISLAMIENTOS; dbo.INDIAGNOS; dbo.HCRIESGOSP; dbo.HCREGEGRE; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCMOANULB; dbo.HCLABREMI; dbo.HCCENLABU; dbo.INTERCABE; dbo.INTERDETA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientes';
-- GO
