CREATE PROCEDURE [dbo].[SPLIS_ListarLaboratoriosPacientesSolicitudes]
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
			  INNER JOIN INCUPSIPS AS D WITH(NOLOCK)  ON A.CODSERIPS = D .CODSERIPS AND SERIPSDASH = 1
			  INNER JOIN ADINGRESO AS E WITH(NOLOCK)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES and E.IESTADOIN IN ('','P','C')
			  INNER JOIN INENTIDAD AS ENT WITH(NOLOCK)  ON ENT.CODENTIDA = E.CODENTIDA 
			  INNER JOIN INUNIFUNC AS C WITH(NOLOCK)  ON E.UFUACTPAC = C.UFUCODIGO 
			  INNER JOIN INUNIFUNC AS C2 WITH(NOLOCK)  ON A.UFUCODIGO = C2.UFUCODIGO
			  INNER JOIN INUBICACI UBI WITH(NOLOCK)  ON b.AUUBICACI = UBI.AUUBICACI
			  INNER JOIN INMUNICIP MUN WITH(NOLOCK)  ON UBI.DEPMUNCOD= MUN.DEPMUNCOD
			  INNER JOIN ADCENATEN CA WITH(NOLOCK) ON CA.CODCENATE = A.CODCENATE
			  INNER JOIN INPROFSAL AS H WITH(NOLOCK)  ON A.CODPROSAL =H.CODPROSAL  
	  WHERE  
	  A.ESTSERIPS IN ('1','8','9') 
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
	  tmp.CODMOTIVOMUESTRANOCONFORME AS 'CodigoMotivoMuestraNoConforme',
	  RTRIM(MA.DESMOTANU) AS 'DescripcionMotivoMuestraNoConforme',
	  CASE tmp.SERIPSPOS WHEN 0 THEN 'No' WHEN 1 THEN 'Si' END AS 'FinanciadoUPC'
	  ,[dbo].[RiskFactorAlert](tmp.IPCODPACI,'',1) AS IconoRiesgos, [dbo].[RiskFactorAlert](tmp.IPCODPACI,tmp.NUMINGRES,2) AS IconoEscalas
    FROM  Tmp_ConsultasOncologicas tmp 
			  LEFT JOIN CHCAMASHO AS G WITH(NOLOCK)  ON tmp.CODCAMACT = G.CODICAMAS and g.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion))
			  LEFT JOIN CHTIPOSAISLAMIENTOS Z WITH(NOLOCK) ON z.Id = g.CODAISLAM 
			  LEFT JOIN INDIAGNOS AS I WITH(NOLOCK)  ON tmp.CODDIAGNO = I.CODDIAGNO 
			  LEFT JOIN HCRIESGOSP AS RP WITH(NOLOCK) ON RP.NUMINGRCES = tmp.NUMINGRES
			  LEFT JOIN HCREGEGRE AS EG WITH(NOLOCK)  ON EG.NUMINGRES = tmp.NUMINGRES
			  LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = tmp.IDDESCRIPCIONRELACIONADA
			  LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId
			   LEFT JOIN HCMOANULB MA WITH(NOLOCK) ON MA.CODMOTANU = tmp.CODMOTIVOMUESTRANOCONFORME
	ORDER BY tmp.FECORDMED DESC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de exámenes de laboratorio clínico pendientes o con muestra no conforme de los pacientes activos (ingresados, en urgencias o consulta), filtrando por centro de atención, estado de la orden y si el examen se realiza en sitio. Combina datos de órdenes de laboratorio (HCORDLABO), información demográfica del paciente (INPACIENT), el episodio de ingreso (ADINGRESO), la entidad pagadora o aseguradora (INENTIDAD), las unidades funcionales de atención y solicitante (INUNIFUNC), el servicio CUPS solicitado (INCUPSIPS), el diagnóstico CIE-10, la cama asignada, el municipio de residencia del paciente, el médico solicitante y el profesional de salud, para presentar en pantalla o reporte la lista de trabajo del laboratorio con estado de la muestra, prioridad (urgente/rutinario), financiación UPC, alertas de riesgo clínico y datos de contacto del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las solicitudes de laboratorio de pacientes (últimos 4 meses) con datos clínicos, administrativos y de muestra, filtradas por centro(s) de atención y modalidad en sitio, para tableros operativos del laboratorio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe ser una cadena tokenizable por dbo.splitstring.; Deben existir ingresos asociados al paciente con estado vacío, ''P'' o ''C'' para que la orden sea visible.; La orden debe tener fecha de orden médica dentro de los últimos 4 meses respecto a common.GETDATE().; El examen debe coincidir con la modalidad ''examen en sitio'' indicada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes activas en estados 1, 8 o 9 (excluye anuladas, interpretadas, remitidas, extramural).; La ventana temporal de visibilidad nunca excede 4 meses hacia atrás desde la fecha actual del sistema.; El filtro por centros de atención se aplica de forma consistente tanto a la orden como al cruce de camas.; Cada fila representa una orden única (SELECT DISTINCT) con datos consolidados de paciente, ingreso, diagnóstico, profesional, entidad pagadora y municipio.; La edad se calcula con la función dbo.Edad sobre la fecha de nacimiento y la fecha actual de common.GETDATE().; Los iconos de riesgo y escalas se obtienen de dbo.RiskFactorAlert con modos 1 (paciente) y 2 (ingreso).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de laboratorio; Estados de solicitud de laboratorio; Muestra no conforme / recolectada parcialmente; Centro de atención; Ingreso hospitalario; Cama y tipo de aislamiento; Diagnóstico; Profesional de salud (médico solicitante); Entidad pagadora; Tipo de afiliación / tipo de paciente; Prioridad (Urgente/Rutinario); Gestación / riesgos del paciente; Egreso del paciente; Financiación UPC; Autorización; Edad del paciente; Factores de riesgo y escalas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo órdenes con ESTSERIPS IN (''1'',''8'',''9'') (Solicitado, Muestra recolectada parcialmente, Muestra no conforme).; [RETURN_RESULT] resultset: Filtra órdenes cuyo CODCENATE pertenezca a la lista expandida de @CentroAtencion vía splitstring.; [RETURN_RESULT] resultset: Restringe FECORDMED al rango [hoy-4 meses, hoy] usando common.GETDATE().; [RETURN_RESULT] resultset: Solo incluye ingresos con IESTADOIN IN ('''',''P'',''C'').; [RETURN_RESULT] resultset: El JOIN con camas (CHCAMASHO) se restringe al mismo conjunto de centros de atención del parámetro.; [RETURN_RESULT] resultset: Resultados ordenados por FECORDMED descendente (más recientes primero).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = 1..9 → Traduce el código de estado a etiqueta legible (Solicitado, Muestra recolectada, Pendiente interpretación, Examen interpretado, Estudio remitido, Anulado, Extramural, Muestra recolectada parcialmente, Muestra no conforme).; si IDETIPHIS = ''CODIGOAZU'' → Marca la solicitud como ''Emergencia''. else Marca la solicitud como ''Historia''.; si PRISERIPS = ''1'' → Prioridad ''Urgente''. else Prioridad ''Rutinario''.; si FECALTPAC IS NULL en HCREGEGRE → Clasifica al paciente como ''Pacientes en la unidad''. else Clasifica al paciente como ''Pacientes con salida''.; si SERIPSPOS = 0 / 1 → Indica si el servicio es financiado por UPC (''No'' / ''Si'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring; common.GETDATE; dbo.Edad; dbo.RiskFactorAlert', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADCENATEN; dbo.INPROFSAL; dbo.CHCAMASHO; dbo.CHTIPOSAISLAMIENTOS; dbo.INDIAGNOS; dbo.HCRIESGOSP; dbo.HCREGEGRE; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCMOANULB', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarLaboratoriosPacientesSolicitudes';
-- GO
