

CREATE view [Integrations].[BloodComponentsOrders_Mirth_Synch]
AS

SELECT
SUBSTRING(DB_NAME(),7,3) as CodeBD,
A.ID AS NumeroOrdenIndigo,
A.ID AS Id,
A.FECORDMED AS FechASolicitud, 
convert(varchar(20),YEAR(A.FECORDMED))+convert(varchar(20),MONTH(A.FECORDMED))+convert(varchar,DAY(A.FECORDMED)) AS FechaNumero,
convert(time, FECORDMED) as HoraOrden,
B.IPTIPODOC AS CodigoTipoDocumento,
cASe B.IPTIPODOC when 1 then 'Cédula de Ciudadanía' 
				 when 2 then 'Cédula de Extranjería'
				 when 3 then 'Tarjeta de Identidad'  
				 when 4 then 'Registro Civil'  
				 when 5 then 'PASaporte'  
				 when 6 then 'Adulto Sin Identificación'  
				 when 7 then 'Menor Sin Identificación' 
				 when 8 then 'Número único de identificación personal'  
				 when 9 then 'Certificado Nacido Vivo' 
				 when 10 then 'Carnet Diplomático (Aplica para extranjeros)' 
				 when 11 then 'Salvoconducto (Aplica para extranjeros)' 
				 when 12 then 'Permiso especial de Permanencia (Aplica para extranjeros)' END TipoDocumento,
RTRIM(B.IPCODPACI) AS Identificacion, 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
RTRIM(CA.CODCENATE) AS CodigoCentroAtencion,
ISNULL(cASe B.IPTIPOPAC when 0 then 'Contributivo' 
						when 1 then 'Subsidiado'
						when 2 then 'Vinculado'
						when 3 then 'Particular'
						when 5 then 'Desplazado Reg. Contributivo'
						when 6 then 'Desplazado Reg. Subsidiado'
						when 7 then 'Desplazado no ASegurado' END,'No Aplica') TipoPaciente,
cASe B.IPTIPOAFI when 0 then 'No Aplica' 
				 when 1 then 'Cotizante'
				 when 2 then 'Beneficiario'
				 when 3 then 'Adicional'
				 when 4 then 'Jub/Retirado'
				 when 5 then 'Pensionado' END TipoAfiliacion,
RTRIM(ltrim(CA.CODCENATE)) +' - '+ RTRIM(ltrim(CA.NOMCENATE)) AS NombreCentroAtencion,
CONVERT(DATE,B.IPFECNACI,102) AS FechaNacimiento, 
E.NUMINGRES AS Ingreso, 
A.NUMEFOLIO AS Folio,
A.OBSERVACI AS ObservacionServicio, 
A.CODDIAGNO AS CodigoDiagnostico,
RTRIM(I.NOMDIAGNO) AS Diagnostico, 
RTRIM(G.DESCCAMAS) AS Cama, 
b.IPDIRECCI AS Direccion,
b.IPTELEFON AS Telefono,
CASE b.IPSEXOPAC when 1 then 'MASculino' else 'Femenino' END AS Sexo,
b.IPRHSANGR AS Rh, 
b.IPGRUPSAN AS GrupoSanguineo,
B.CORELEPAC AS Correo,
b.IPTELMOVI AS Movil,
H.CODPROSAL AS CodigoMedico,
'Cedula' AS TipoIdentificacionMedico,
H.CODIGONIT AS IdentificacionMedico,
H.NOMMEDICO AS NombreMedico, 
B.IPPRINOMB AS PrimerNombre,
B.IPSEGNOMB AS SegundoNombre,
B.IPPRIAPEL AS PrimerApellido,
B.IPSEGAPEL AS SegundoApellido,
RTRIM(MUN.DEPMUNCOD) AS MunicipioCodigo,
RTRIM(MUN.MUNNOMBRE) AS MunicipioNombre,
RTRIM(ENT.CODENTIDA) AS EntidadPagadoraCodigo,
RTRIM(ENT.NOMENTIDA) AS EntidadPagadoraNombre,
(SELECT TOP 1 CODDIAGNO FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 ORDER BY CODDIAGNO ASc) AS CodigoDiagnosticoSegundo,
(SELECT TOP 1 DESCR.NOMDIAGNO FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 and DESCR.CODDIAGNO = COD.CODDIAGNO ORDER BY COD.CODDIAGNO ASc) AS NombreDiagnosticoSegundo,
CASE WHEN ((SELECT COUNT(CODDIAGNO) FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN 
(SELECT TOP 1 CODDIAGNO FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 ORDER BY CODDIAGNO desc) END AS CodigoDiagnosticoTercero,
CASE WHEN ((SELECT COUNT(CODDIAGNO) FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN 
(SELECT TOP 1 DESCR.NOMDIAGNO FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 and DESCR.CODDIAGNO = COD.CODDIAGNO ORDER BY COD.CODDIAGNO DESC) END AS NombreDiagnosticoTercero,
CASE A.SOLEXTRAMU WHEN 1 THEN 'Ambulatorio' else 'Hospitalaria' end  AS TipoOrden,
e.ICAUSAING AS CodigoCausaIngreso,
CASE e.ICAUSAING WHEN 1 THEN 'Heridos en combate' 
				 WHEN 2 THEN 'Enfermedad profesional ' 
				 WHEN 3 THEN 'Enfermedad general adulto ' 
				 WHEN 4 THEN ' Enfermedad general pediatria ' 
				 WHEN 5 THEN 'Odontología ' 
				 WHEN 6 THEN 'Accidente de transito' 
				 WHEN 7 THEN 'CatAStrofe/Fisalud ' 
				 WHEN 8 THEN 'Quemados ' 
				 WHEN 9 THEN 'Maternidad' 
				 WHEN 10 THEN 'Accidente Laboral' 
				 WHEN 11 THEN 'Cirugia Programada' END CausaIngreso,
ISNULL(B.IPESTRATO,0) AS Estrato,
H.MEDPRINOM AS MedicoPrimerNombre,
H.MEDSEGNOM AS MedicoSegundoNombre,
H.MEDPRIAPEL AS MedicoPrimerApellido,
H.MEDSEGAPEL AS MedicoSegundoApellido,
rtrim(ltrim(ESP.CODESPECI)) AS CodigoEspecialidad,
rtrim(ltrim(ESP.DESESPECI)) AS Especialidad,
CASE B.IPSEXOPAC WHEN 1 THEN 'No Aplica' ELSE CASE  RP.GESTACION WHEN 1 THEN 'Si' else 'No' END END AS Gestacion,
COM.CODCOMSAM AS CodigoComponente,  
CASE COM.CODCOMSAM WHEN '001' THEN 'LR' 
				   WHEN '002' THEN 'IR' else 'Por definir' END AS Hemocomponente,
COM.DESCOMSAM AS DescripcionComponente,
COM.MaximumContentHemocomponent AS VolumenMax,
BOL.TIPOSOLICI,
U.UFUCODIGO AS CodigoUnidad,
CASE BOL.TIPOSOLICI WHEN 1 THEN 'Reserva' 
					WHEN 2 THEN 'Transfusión' 
					WHEN 3 THEN 'Reserva y Transfusión' END AS TipoSolicitud,
BOL.ESTADO  AS Estado,
CASE BOL.estado WHEN 1 THEN 'Solicitud Reserva' 
				WHEN 2 THEN 'Solicitud de Transfusión' 
				WHEN 3 THEN 'Reserva sin Solicitud de Transfusión' 
				WHEN 4 THEN 'Reserva con Solicitud de Transfusión'
				WHEN 5 THEN 'Liberado' 
				WHEN 6 THEN 'Entregado' 
				WHEN 8 THEN 'Aplicado' 
				WHEN 9 THEN 'Anulado' 
				WHEN 10 THEN 'Descartado por salida de paciente' 
				WHEN 11 THEN 'Extramural' END EstadoBolsa,
A.PRIOTRAN as Prioridad,
A.SYNCMIRTH,
count(BOL.ID) as CantidadBolsas 
FROM  
dbo.HCORHEMCO AS A INNER JOIN 
dbo.HCORHEMBOL BOL on A.ID = BOL.HCORHEMCOID INNER JOIN
dbo.HCCOMSAN COM on BOL.COMSAMID = COM.ID INNER JOIN
dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
dbo.INENTIDAD AS ENT with(nolock)  ON ENT.CODENTIDA = E.CODENTIDA  INNER JOIN 
dbo.INPROFSAL AS H with(nolock)  ON BOL.PROFSOLRES =H.CODPROSAL  LEFT OUTER JOIN
dbo.INDIAGNOS AS I with(nolock)  ON A.CODDIAGNO = I.CODDIAGNO INNER JOIN
dbo.HCHISPACA AS HIS with(nolock) ON HIS.NUMINGRES = A.NUMINGRES AND HIS.NUMEFOLIO = A.NUMEFOLIO INNER JOIN
dbo.INESPECIA AS ESP with(nolock) ON ESP.CODESPECI = HIS.CODESPTRA INNER JOIN
INUBICACI AS UBI with(nolock)  ON B.AUUBICACI = UBI.AUUBICACI INNER JOIN
INMUNICIP AS MUN with(nolock)  ON UBI.DEPMUNCOD= MUN.DEPMUNCOD INNER JOIN 
ADCENATEN AS CA with(nolock) ON CA.CODCENATE = A.CODCENATE LEFT JOIN 
dbo.HCRIESGOSP AS RP with(nolock)  on RP.NUMINGRCES = A.NUMINGRES	LEFT JOIN
dbo.CHCAMASHO AS G with(nolock)  ON E.CODCAMACT = G.CODICAMAS LEFT JOIN
dbo.INUNIFUNC AS U WITH (nolock) ON BOL.UFUSOLRES = U.UFUCODIGO
WHERE BOL.ESTADO IN(1,2,3,4) AND (A.SYNCMIRTH IS NULL OR A.SYNCMIRTH = 0 )	AND A.INTERFAZ = 0
GROUP BY A.ID,A.FECORDMED,B.IPTIPODOC, B.IPCODPACI, B.IPNOMCOMP,CA.CODCENATE,CA.NOMCENATE,B.IPFECNACI,B.IPTIPOPAC,B.IPTIPOPAC,B.IPTIPOAFI,e.NUMINGRES,A.NUMEFOLIO,A.OBSERVACI,A.CODDIAGNO,I.NOMDIAGNO
	,G.DESCCAMAS,b.IPDIRECCI,b.IPTELEFON,b.IPSEXOPAC,b.IPRHSANGR,b.IPGRUPSAN,B.CORELEPAC,b.IPTELMOVI,H.CODPROSAL,H.CODIGONIT,H.NOMMEDICO
	,B.IPPRINOMB,B.IPSEGNOMB,B.IPPRIAPEL,B.IPSEGAPEL,MUN.DEPMUNCOD,MUN.MUNNOMBRE,ENT.CODENTIDA,ENT.NOMENTIDA, A.SOLEXTRAMU,e.ICAUSAING,B.IPESTRATO,H.MEDPRINOM 
	, H.MEDSEGNOM ,H.MEDPRIAPEL, H.MEDSEGAPEL,ESP.CODESPECI,ESP.DESESPECI,RP.GESTACION,COM.CODCOMSAM,COM.DESCOMSAM,COM.MaximumContentHemocomponent,BOL.TIPOSOLICI,U.UFUCODIGO,BOL.TIPOSOLICI,BOL.ESTADO,A.PRIOTRAN,A.SYNCMIRTH
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de sincronización con Mirth que consolida todas las órdenes de hemocomponentes (transfusiones de sangre y hemoderivados) generadas en historia clínica, junto con el detalle de cada bolsa solicitada o transfundida. Integra datos del paciente (identificación, nombre, grupo sanguíneo, RH, tipo de afiliación, entidad pagadora, municipio), del ingreso o admisión (número de ingreso, causa de ingreso, cama, centro de atención, unidad funcional), del médico solicitante (código, identificación, nombre, especialidad), de los diagnósticos principales y secundarios (CIE-10), y del componente sanguíneo (tipo de hemocomponente, volumen máximo, tipo y estado de la solicitud). Su propósito es exponer en tiempo real la información de órdenes de hemoterapia hacia el motor de integración Mirth, permitiendo la interoperabilidad con sistemas externos como bancos de sangre u otros aplicativos clínicos.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'BloodComponentsOrders_Mirth_Synch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'BloodComponentsOrders_Mirth_Synch';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone órdenes de transfusión de hemocomponentes pendientes de sincronizar hacia Mirth, consolidando datos clínicos, demográficos, médicos y de bolsas asociadas para integración con banco de sangre.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'BloodComponentsOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen órdenes de hemoterapia (HCORHEMCO) con bolsas asociadas (HCORHEMBOL); El paciente tiene ingreso vigente (ADINGRESO) y entidad pagadora válida (INENTIDAD); El profesional solicitante de la reserva existe en INPROFSAL; La historia clínica (HCHISPACA) tiene especialidad tratante registrada en INESPECIA; El paciente tiene ubicación geográfica registrada (INUBICACI/INMUNICIP)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'BloodComponentsOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se sincronizan órdenes no marcadas como ya enviadas (SYNCMIRTH 0/NULL) y no interfazadas (INTERFAZ=0); Solo bolsas en estados de reserva o transfusión activos (1-4) son consideradas; estados liberado/entregado/aplicado/anulado/descartado/extramural quedan excluidos; El segundo y tercer diagnóstico se obtienen exclusivamente de diagnósticos no principales (CODDIAPRI=0); El tercer diagnóstico solo se expone cuando hay estrictamente más de 2 diagnósticos no principales; TipoIdentificacionMedico siempre se asume ''Cedula'' (valor fijo); La cantidad de bolsas por orden se agrega vía COUNT(BOL.ID) agrupado por la orden', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'BloodComponentsOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de transfusión; Hemocomponente (LR/IR); Bolsa de sangre; Reserva de sangre; Transfusión; Paciente; Ingreso hospitalario; Diagnóstico CIE (principal y secundarios); Tipo de afiliación (Cotizante/Beneficiario/Pensionado…); Tipo de paciente (Contributivo/Subsidiado/Vinculado/Particular/Desplazado); Causa de ingreso (accidente tránsito, maternidad, quemados, etc.); Gestación; Grupo sanguíneo y Rh; Especialidad médica; Centro de atención; Entidad pagadora (EPS); Estrato socioeconómico; Prioridad de transfusión; Sincronización Mirth (interfaz HL7/integración banco de sangre); Orden ambulatoria vs hospitalaria', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'BloodComponentsOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Integrations.BloodComponentsOrders_Mirth_Synch: Solo retorna órdenes con BOL.ESTADO IN (1,2,3,4) (Solicitud Reserva, Solicitud Transfusión, Reserva sin/con Solicitud Transfusión), SYNCMIRTH IS NULL o 0, y INTERFAZ=0', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'BloodComponentsOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si BOL.ESTADO IN (1,2,3,4) AND (A.SYNCMIRTH IS NULL OR A.SYNCMIRTH=0) AND A.INTERFAZ=0 → La orden se incluye en el resultado para sincronización con Mirth else La orden se excluye (ya sincronizada, estado no elegible o ya interfazada); si A.SOLEXTRAMU = 1 → TipoOrden se clasifica como ''Ambulatorio'' else TipoOrden se clasifica como ''Hospitalaria''; si B.IPSEXOPAC = 1 (masculino) → Gestación se reporta como ''No Aplica'' else Gestación se deriva de RP.GESTACION (Si/No); si Existen más de 2 diagnósticos no principales (COUNT donde CODDIAPRI=0 > 2) → Se expone un tercer diagnóstico (CodigoDiagnosticoTercero/NombreDiagnosticoTercero) tomando el de mayor código else El tercer diagnóstico queda en NULL; si COM.CODCOMSAM = ''001'' → Hemocomponente=''LR'' else Si ''002'' → ''IR''; en otro caso ''Por definir''', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'BloodComponentsOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORHEMCO; dbo.HCORHEMBOL; dbo.HCCOMSAN; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.HCHISPACA; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADCENATEN; dbo.HCRIESGOSP; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.INDIAGNOH', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'BloodComponentsOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'BloodComponentsOrders_Mirth_Synch';
GO
