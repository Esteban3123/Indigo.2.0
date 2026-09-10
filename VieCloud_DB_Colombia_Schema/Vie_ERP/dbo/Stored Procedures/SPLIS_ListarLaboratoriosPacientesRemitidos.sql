
CREATE PROCEDURE [dbo].[SPLIS_ListarLaboratoriosPacientesRemitidos]
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
	  MUN.DEPMUNCOD, MUN.MUNNOMBRE, CA.NOMCENATE, H.NOMMEDICO, D.SERIPSPOS, E.IESTADOIN
	  FROM  dbo.HCORDLABO AS A WITH(NOLOCK)  
			  INNER JOIN INPACIENT AS B WITH(NOLOCK)  ON A.IPCODPACI = B.IPCODPACI 
			  INNER JOIN INCUPSIPS AS D WITH(NOLOCK)  ON A.CODSERIPS = D .CODSERIPS 
			  INNER JOIN ADINGRESO AS E WITH(NOLOCK)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES and E.IESTADOIN IN ('','P','C','B')
			  INNER JOIN INENTIDAD AS ENT WITH(NOLOCK)  ON ENT.CODENTIDA = E.CODENTIDA 
			  INNER JOIN INUNIFUNC AS C WITH(NOLOCK)  ON E.UFUACTPAC = C.UFUCODIGO 
			  INNER JOIN INUNIFUNC AS C2 WITH(NOLOCK)  ON A.UFUCODIGO = C2.UFUCODIGO
			  INNER JOIN INUBICACI UBI WITH(NOLOCK)  ON b.AUUBICACI = UBI.AUUBICACI
			  INNER JOIN INMUNICIP MUN WITH(NOLOCK)  ON UBI.DEPMUNCOD= MUN.DEPMUNCOD
			  INNER JOIN ADCENATEN CA WITH(NOLOCK) ON CA.CODCENATE = A.CODCENATE
			  INNER JOIN INPROFSAL AS H WITH(NOLOCK)  ON A.CODPROSAL =H.CODPROSAL  
	  WHERE  
	  A.ESTSERIPS IN ('5') 
	  AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
	  AND A.EXMREASIT = @ExamenEnSitio 
	  AND A.FECORDMED BETWEEN  DATEADD(MONTH,-4,common.GETDATE()) AND common.GETDATE()  
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
	  dbo.Edad(tmp.IPFECNACI, Common.GETDATE())  AS Edad, tmp.FECRECMUE AS 'FechaRecoleccion',
	  RTRIM(CenRem.DESCENLAB) AS 'CentroRemision',
	  CASE tmp.SERIPSPOS WHEN 0 THEN 'No' WHEN 1 THEN 'Si' END AS 'FinanciadoUPC', RTRIM(tmp.IESTADOIN) AS EstadoIngreso
	  FROM  Tmp_ConsultasOncologicas tmp 
			  LEFT JOIN  CHCAMASHO AS G WITH(NOLOCK)  ON tmp.CODCAMACT = G.CODICAMAS and g.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion))
			  LEFT JOIN CHTIPOSAISLAMIENTOS Z WITH(NOLOCK) ON z.Id = g.CODAISLAM 
			  LEFT JOIN INDIAGNOS AS I WITH(NOLOCK)  ON tmp.CODDIAGNO = I.CODDIAGNO 
			  LEFT JOIN HCRIESGOSP AS RP WITH(NOLOCK) ON RP.NUMINGRCES = tmp.NUMINGRES
			  LEFT JOIN HCREGEGRE AS EG WITH(NOLOCK)  ON EG.NUMINGRES = tmp.NUMINGRES
			  LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = tmp.IDDESCRIPCIONRELACIONADA
			  LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId
			  LEFT JOIN HCLABREMI Rem WITH(NOLOCK) ON  rem.IPCODPACI = tmp.IPCODPACI and rem.NUMINGRES = tmp.NUMINGRES  and Rem.NUMEFOLIO = tmp.NUMEFOLIO and rem.CODSERIPS = tmp.CODSERIPS  and Rem.CODCENATE = tmp.CODCENATE AND Rem.UFUCODIGO = tmp.UFUCODIGO
			  LEFT JOIN HCCENLABU CenRem WITH(NOLOCK) ON CenRem.CODCENLAB = Rem.CODCENLAB 	  
	  ORDER BY tmp.FECORDMED DESC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los exámenes de laboratorio clínico que han sido remitidos a un centro externo (estado ''Estudio remitido''), filtrando por centro de atención, por si el examen se realiza en sitio o fuera, y opcionalmente por el estado del ingreso del paciente. Compone información de órdenes de laboratorio (HCORDLABO) con datos demográficos del paciente (INPACIENT), el episodio de ingreso u hospitalización vigente (ADINGRESO), la entidad pagadora o aseguradora (INENTIDAD), las unidades funcionales de atención y de solicitud (INUNIFUNC), el municipio de residencia del paciente (INUBICACI/INMUNICIP), el profesional que ordenó el examen, la cama asignada, el diagnóstico CIE-10, el centro de laboratorio al que fue remitida la muestra (HCLABREMI/HCCENLABU) y datos contractuales del servicio CUPS. Es utilizado por los módulos de seguimiento y gestión de laboratorio para visualizar el listado de pacientes con muestras remitidas a laboratorios externos, incluyendo prioridad, estado de alerta, datos de contacto del paciente, financiación UPC y semana de gestación cuando aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de laboratorio remitidas a centros externos de los últimos 4 meses, filtradas por centros de atención y modalidad en sitio, con datos clínicos, demográficos y administrativos del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe ser una lista delimitada parseable por dbo.splitstring.; Las órdenes deben tener estado de servicio IPS = ''5'' (Estudio remitido).; El ingreso asociado debe estar en estado '''', ''P'', ''C'' o ''B''.; La fecha de orden médica debe estar dentro de los últimos 4 meses respecto a common.GETDATE().', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna órdenes con estado de servicio remitido (ESTSERIPS=''5'').; Solo considera ingresos con IESTADOIN en ('''',''P'',''C'',''B'').; La ventana temporal está fijada en 4 meses hacia atrás desde common.GETDATE().; Las camas se filtran al mismo centro de atención que la orden.; Se aplica DISTINCT y ORDER BY fecha de orden descendente.; Se ejecuta con WITH RECOMPILE para regenerar plan en cada ejecución.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Estudio remitido; Centro de atención; Examen en sitio / extramural; Paciente / ingreso hospitalario; Entidad pagadora; Diagnóstico; Cama y aislamiento; Riesgo gestacional; Egreso del paciente; Prioridad clínica (urgente/rutinario); Financiación UPC; Centro de remisión de laboratorio; Muestra no conforme', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve el listado de exámenes de laboratorio con ESTSERIPS=''5'' (remitidos), del centro de atención indicado, con EXMREASIT igual al parámetro y FECORDMED en los últimos 4 meses, ordenado por FECORDMED DESC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS in (1..9) → Traduce el código a etiqueta textual: 1=Solicitado, 2=Muestra recolectada, 3=Pendiente interpretación, 4=Examen interpretado, 5=Estudio remitido, 6=Anulado, 7=Extramural, 8=Muestra recolectada parcialmente, 9=Muestra no conforme.; si IDETIPHIS = ''CODIGOAZU'' → Tipo de solicitud = ''Emergencia'' else Tipo de solicitud = ''Historia''; si PRISERIPS = ''1'' → Prioridad = ''Urgente'' else Prioridad = ''Rutinario''; si HCREGEGRE.FECALTPAC IS NULL → Paciente clasificado como ''Pacientes en la unidad'' else Paciente clasificado como ''Pacientes con salida''; si SERIPSPOS = 0 / 1 → Mapea a FinanciadoUPC: 0=''No'', 1=''Si''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADCENATEN; dbo.INPROFSAL; dbo.CHCAMASHO; dbo.CHTIPOSAISLAMIENTOS; dbo.INDIAGNOS; dbo.HCRIESGOSP; dbo.HCREGEGRE; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCLABREMI; dbo.HCCENLABU; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesRemitidos';
-- GO
