
CREATE view [Integrations].[LaboratoryOrders_Mirth_Synch] AS

SELECT
SUBSTRING(DB_NAME(),7,3) AS CodeBD
,A.[AUTO] AS ID
--,INTER.CODCONCEC AS NumeroOrdenIndigo
,MAX_INTER.CODCONCEC AS NumeroOrdenIndigo
,CONVERT(CHAR(8), GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SA Pacific Standard Time', 112) + REPLACE(CONVERT(CHAR(8), GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SA Pacific Standard Time', 108), ':', '') FechaActual
,A.FECORDMED AS FechaSolicitud
,CONVERT(CHAR(8), A.FECORDMED, 112) + REPLACE(CONVERT(CHAR(8), A.FECORDMED, 108), ':', '') FechaSolicitudCorta
/*,CASE B.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 5 THEN 'PA' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' WHEN 9 THEN 'CN' WHEN 10 THEN 'CD' WHEN 11 THEN 'SC' WHEN 12 THEN 'PE' WHEN 13 THEN 'PT' WHEN 14 THEN 'DE' WHEN 15 THEN 'SI' END AS CodigoTipoDocumento*/
,IDEN.SIGLA CodigoTipoDocumento
,A.CODSERIPS AS CodigoServicio
,CD.Code AS CodigoRelacionado 
,RTRIM(D.DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio
/*,CASE B.IPTIPODOC WHEN 1 THEN 'Cédula de Ciudadanía' WHEN 2 THEN 'Cédula de Extranjería' WHEN 3 THEN 'Tarjeta de Identidad'  WHEN 4 THEN 'Registro Civil'  WHEN 5 THEN 'Pasaporte'  WHEN 6 THEN 'Adulto Sin Identificación'  WHEN 7 THEN 'Menor Sin Identificación' WHEN 8 THEN 'Número único de identificación personal'  WHEN 9 THEN 'Certificado Nacido Vivo' WHEN 10 THEN 'Carnet Diplomático (Aplica para extranjeros)' WHEN 12 THEN 'Salvoconducto (Aplica para extranjeros)' WHEN 12 THEN 'Permiso especial de Permanencia (Aplica para extranjeros)' END TipoDocumento*/
,SUBSTRING(IDEN.NOMBRE, 6, LEN(NOMBRE)) TipoDocumento
,RTRIM(A.IPCODPACI) AS Identificacion
,RTRIM(B.IPNOMCOMP) AS NombrePaciente
,RTRIM(CA.CODCENATE) AS CodigoCentroAtencion
,ISNULL(CASE B.IPTIPOPAC WHEN 0 THEN 'Contributivo' WHEN 1 THEN  'Subsidiado' WHEN 2 THEN 'Vinculado' WHEN 3 THEN 'Particular' WHEN 5 THEN 'Desplazado Reg. Contributivo' WHEN 6 THEN 'Desplazado Reg. Subsidiado' WHEN 7 THEN 'Desplazado no Asegurado' END,'No Aplica') TipoPaciente
,CASE B.IPTIPOAFI WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Cotizante' WHEN 2 THEN 'Beneficiario' WHEN 3 THEN 'Adicional' WHEN 4 THEN 'Jub/Retirado' WHEN 5 THEN 'Pensionado' END TipoAfiliacion
,RTRIM(ltrim(CA.CODCENATE)) +' - '+ RTRIM(ltrim(CA.NOMCENATE)) AS NombreCentroAtencion
,B.IPFECNACI AS FechaNacimiento
,REPLACE(CAST(B.IPFECNACI AS DATE),'-','') FechaNacimientoCorta
,ISNULL(U.UFUCODIGO, '1111001') AS CodigoUnidad
,ISNULL(U.UFUDESCRI, 'URGENCIAS OBSERVACION') AS NombreUnidad
,TRIM(ISNULL(U.UFUCODIGO, '1111001')) + ' - ' + TRIM(ISNULL(U.UFUDESCRI, 'URGENCIAS OBSERVACION')) AS UnidadFuncional
,A.ESTSERIPS AS Estado
,A.NUMINGRES AS Ingreso
,A.NUMEFOLIO AS Folio
--,SUBSTRING(A.OBSSERIPS, 1, 120) AS ObservacionServicio
,REPLACE(A.OBSSERIPS, CHAR(13) + CHAR(10), ' ') ObservacionServicio
,ISNULL(CASE A.CODDIAGNO WHEN '   ' THEN 'na' else A.CODDIAGNO end,'') AS CodigoDiagnostico
,ISNULL(CASE RTRIM(I.NOMDIAGNO) WHEN '' THEN 'NA' ELSE RTRIM(I.NOMDIAGNO) END, '') AS Diagnostico
,ISNULL(G.DESCCAMAS, 'SIN_CAMA') AS Cama
,B.IPDIRECCI AS Direccion
,B.IPTELEFON AS Telefono
,CASE B.IPSEXOPAC WHEN 1 THEN 'Masculino' ELSE 'Femenino' END AS Sexo
,B.IPRHSANGR AS Rh
,B.IPGRUPSAN AS GrupoSanguineo
,B.CORELEPAC AS Correo
,B.IPTELMOVI AS Movil
,A.CODPROSAL AS CodigoMedico
,'Cedula' AS TipoIdentificacionMedico
,H.CODIGONIT AS IdentificacionMedico
,H.NOMMEDICO AS NombreMedico
,B.IPPRINOMB AS PrimerNombre
,CASE B.IPSEGNOMB WHEN '' THEN ' ' ELSE B.IPSEGNOMB END AS SegundoNombre
,B.IPPRIAPEL AS PrimerApellido
,B.IPSEGAPEL AS SegundoApellido
,RTRIM(MUN.DEPMUNCOD) AS MunicipioCodigo
,RTRIM(MUN.MUNNOMBRE) AS MunicipioNombre
,UBI.UBINOMBRE AS Ciudad
,RTRIM(ENT.Code) AS EntidadPagadoraCodigo
,ISNULL(T.Nit + '-' + T.DigitVerification, A.IPCODPACI) AS EntidadPagadoraNit
,RTRIM(ENT.Name) AS EntidadPagadoraNombre
,(SELECT TOP 1 CODDIAGNO FROM DBO.INDIAGNOH WHERE IPCODPACI = B.IPCODPACI AND NUMEFOLIO = A.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 ORDER BY CODDIAGNO asc) AS CodigoDiagnosticoSegundo
,(SELECT TOP 1 DESCR.NOMDIAGNO FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR WHERE IPCODPACI = B.IPCODPACI AND NUMEFOLIO = A.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 AND DESCR.CODDIAGNO = COD.CODDIAGNO ORDER BY COD.CODDIAGNO asc) AS NombreDiagnosticoSegundo
,CASE WHEN ((SELECT COUNT(CODDIAGNO) FROM DBO.INDIAGNOH WHERE IPCODPACI = B.IPCODPACI AND NUMEFOLIO = A.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN (SELECT TOP 1 CODDIAGNO FROM DBO.INDIAGNOH WHERE IPCODPACI = B.IPCODPACI AND NUMEFOLIO = A.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 ORDER BY CODDIAGNO desc) END AS CodigoDiagnosticoTercero
,CASE WHEN ((SELECT COUNT(CODDIAGNO) FROM DBO.INDIAGNOH WHERE IPCODPACI = B.IPCODPACI AND NUMEFOLIO = A.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN (SELECT TOP 1 DESCR.NOMDIAGNO FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR WHERE IPCODPACI = B.IPCODPACI AND NUMEFOLIO = A.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 AND DESCR.CODDIAGNO = COD.CODDIAGNO ORDER BY COD.CODDIAGNO DESC) END AS NombreDiagnosticoTercero
,CASE WHEN A.PRISERIPS = '1' THEN 'Urgente' else 'Rutinario' END AS Prioridad
,'Hospitalaria' AS TipoOrden
,e.ICAUSAING AS CodigoCausaIngreso
/*,CASE e.ICAUSAING WHEN 1 THEN 'Heridos en combate' WHEN 2 THEN 'Enfermedad profesional' WHEN 3 THEN 'Enfermedad general adulto' WHEN 4 THEN 'Enfermedad general pediatria' WHEN 5 THEN 'Odontología' WHEN 6 THEN 'Accidente de transito' WHEN 7 THEN 'Catastrofe/Fisalud' WHEN 8 THEN 'Quemados' WHEN 9 THEN 'Maternidad' WHEN 10 THEN 'Accidente Laboral' WHEN 11 THEN 'Cirugia Programada' END CausaIngreso*/
,COA.Name AS CausaIngreso
,A.FECRECMUE AS FechaRecoleccionMuestra
,CONVERT(CHAR(8), A.FECRECMUE, 112) + REPLACE(CONVERT(CHAR(8), A.FECRECMUE, 108), ':', '') FechaRecoleccionMuestraCorta
,A.USURECMUE AS CodigoUsuarioRecolectaMuestra
,TRIM(USU.CODUSUARI) + ' - '+ TRIM(USU.NOMUSUARI) AS UsuarioRecolectaMuestra
,CONVERT(CHAR(8), ISNULL(A.FECHASUGE, A.FECORDMED), 112) + REPLACE(CONVERT(CHAR(8), ISNULL(A.FECHASUGE, A.FECORDMED), 108), ':', '') FechaSugeridaMuestra
,ISNULL(B.IPESTRATO,0) AS Estrato
,H.MEDPRINOM AS MedicoPrimerNombre
,H.MEDSEGNOM AS MedicoSegundoNombre
,H.MEDPRIAPEL AS MedicoPrimerApellido
,H.MEDSEGAPEL AS MedicoSegundoApellido
,TRIM(ESP.CODESPECI) AS CodigoEspecialidad
,TRIM(ESP.DESESPECI) AS Especialidad
,CASE B.IPSEXOPAC WHEN 1 THEN 'No Aplica' ELSE CASE RP.GESTACION WHEN 1 THEN 'Si' ELSE 'No' END END AS Gestacion
--,A.CANSERIPS AS Cantidad
FROM
dbo.HCORDLABO AS A with(nolock)
INNER JOIN (SELECT AUTOLABOR, MAX(CODCONCEC) AS CODCONCEC FROM dbo.INTERDETA WHERE ORDTIP = 'INT' GROUP BY AUTOLABOR ) AS MAX_INTER ON A.AUTO = MAX_INTER.AUTOLABOR
--INNER JOIN dbo.INTERDETA AS INTER ON INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'INT'
INNER JOIN dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI
INNER JOIN dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS
INNER JOIN dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES
INNER JOIN dbo.ADTIPOIDENTIFICA AS IDEN WITH(NOLOCK) ON IDEN.CODIGO = B.IPTIPODOC
INNER JOIN dbo.CausesOfAttention AS COA WITH(NOLOCK) ON COA.Code = E.ICAUSAING
INNER JOIN Contract.HealthAdministrator ENT ON E.GENCONENTITY = ENT.Id
INNER JOIN Common.ThirdParty T ON T.Id = ENT.ThirdPartyId 
LEFT JOIN dbo.INUNIFUNC AS U with(nolock) ON U.UFUCODIGO = E.UFUAACTHOS
LEFT JOIN dbo.CHCAMASHO AS G with(nolock) ON E.CODCAMACT = G.CODICAMAS
INNER JOIN dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL = H.CODPROSAL
LEFT OUTER JOIN dbo.INDIAGNOS AS I with(nolock) ON A.CODDIAGNO = I.CODDIAGNO
LEFT JOIN dbo.HCHISPACA AS HIS with(nolock) ON HIS.NUMINGRES = A.NUMINGRES AND HIS.NUMEFOLIO = A.NUMEFOLIO
LEFT JOIN dbo.INESPECIA AS ESP with(nolock) ON ESP.CODESPECI = HIS.CODESPTRA
INNER JOIN INUBICACI UBI with(nolock) ON B.AUUBICACI = UBI.AUUBICACI
INNER JOIN INMUNICIP MUN with(nolock) ON UBI.DEPMUNCOD= MUN.DEPMUNCOD
INNER JOIN ADCENATEN CA with(nolock) ON CA.CODCENATE = A.CODCENATE
LEFT JOIN dbo.HCRIESGOSP AS RP with(nolock) ON RP.NUMINGRCES = A.NUMINGRES
LEFT JOIN SEGusuaru USU with(nolock) ON USU.CODUSUARI = A.USURECMUE
LEFT JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN contract.ContractDescriptions CD with(nolock) ON CD.Id = CDD.ContractDescriptionId
WHERE A.ESTSERIPS = 2 AND (A.SYNCMIRTH IS NULL)
AND A.FECORDMED BETWEEN DATEADD(MONTH, -1, Common.GETDATE()) AND Common.GETDATE()
AND A.CODSERIPS NOT IN ('911003','911005','911009','911011_J','911013','911015','911016','911017','911018','911019','911020','911021','911033') --Oferta BDS
	  
UNION 

SELECT
SUBSTRING(DB_NAME(),7,3) AS CodeBD
,A.[AUTO] AS ID
--,INTER.CODCONCEC AS NumeroOrdenIndigo
,MAX_INTER.CODCONCEC AS NumeroOrdenIndigo
,CONVERT(CHAR(8), GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SA Pacific Standard Time', 112) + REPLACE(CONVERT(CHAR(8), GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'SA Pacific Standard Time', 108), ':', '') FechaActual
,A.FECORDMED AS FechaSolicitud
,CONVERT(CHAR(8), A.FECORDMED, 112) + REPLACE(CONVERT(CHAR(8), A.FECORDMED, 108), ':', '') FechaSolicitudCorta
/*,CASE B.IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 5 THEN 'PA' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN 8 THEN 'NU' WHEN 9 THEN 'CN' WHEN 10 THEN 'CD' WHEN 11 THEN 'SC' WHEN 12 THEN 'PE' WHEN 13 THEN 'PT' WHEN 14 THEN 'DE' WHEN 15 THEN 'SI' END AS CodigoTipoDocumento*/
,IDEN.SIGLA AS CodigoTipoDocumento
,RTRIM(A.CODSERIPS) AS CodigoServicio
,CD.Code AS CodigoRelacionado 
,RTRIM(D.DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio
/*,CASE B.IPTIPODOC WHEN 1 THEN 'Cédula de Ciudadanía' WHEN 2 THEN 'Cédula de Extranjería' WHEN 3 THEN 'Tarjeta de Identidad' WHEN 4 THEN 'Registro Civil' WHEN 5 THEN 'Pasaporte' WHEN 6 THEN 'Adulto Sin Identificación' WHEN 7 THEN 'Menor Sin Identificación' WHEN 8 THEN 'Número único de identificación personal' WHEN 9 THEN 'Certificado Nacido Vivo' WHEN 10 THEN 'Carnet Diplomático (Aplica para extranjeros)' WHEN 12 THEN 'Salvoconducto (Aplica para extranjeros)' WHEN 12 THEN 'Permiso especial de Permanencia (Aplica para extranjeros)'  END TipoDocumento*/
,SUBSTRING(IDEN.NOMBRE, 6, LEN(NOMBRE)) AS TipoDocumento
,RTRIM(A.IPCODPACI) AS Identificacion
,RTRIM(B.IPNOMCOMP) AS NombrePaciente
,RTRIM(CA.CODCENATE) AS CodigoCentroAtencion
,ISNULL(CASE B.IPTIPOPAC WHEN 0 THEN 'Contributivo' WHEN 1 THEN  'Subsidiado' WHEN 2 THEN 'Vinculado' WHEN 3 THEN 'Particular' WHEN 5 THEN 'Desplazado Reg. Contributivo' WHEN 6 THEN 'Desplazado Reg. Subsidiado' WHEN 7 THEN 'Desplazado no Asegurado' END,'No Aplica') TipoPaciente
,CASE B.IPTIPOAFI WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Cotizante' WHEN 2 THEN 'Beneficiario' WHEN 3 THEN 'Adicional'WHEN 4 THEN 'Jub/Retirado' WHEN 5 THEN 'Pensionado' END TipoAfiliacion
,RTRIM(ltrim(CA.CODCENATE)) +' - '+ RTRIM(ltrim(CA.NOMCENATE)) AS NombreCentroAtencion
,B.IPFECNACI AS FechaNacimiento
,REPLACE(CAST(B.IPFECNACI AS DATE), '-', '') AS FechaNacimientoCorta
,E.UFUACTPAC AS CodigoUnidad
,U.UFUDESCRI AS NombreUnidad
,TRIM(U.UFUCODIGO) + ' - ' + TRIM(U.UFUDESCRI) AS UnidadFuncional
,A.ESTSERIPS AS Estado
,A.NUMINGRES AS Ingreso
,NULL AS Folio
--,SUBSTRING(A.OBSERVACI, 1, 120) AS ObservacionServicio
,REPLACE(A.OBSERVACI, CHAR(13) + CHAR(10), ' ') ObservacionServicio
,'NA' AS CodigoDiagnostico
,'NA' AS Diagnostico
,'##' AS Cama
,B.IPDIRECCI AS Direccion
,B.IPTELEFON AS Telefono
,CASE B.IPSEXOPAC WHEN 1 THEN 'Masculino' ELSE 'Femenino' END AS Sexo
,B.IPRHSANGR AS Rh
,B.IPGRUPSAN AS GrupoSanguineo
,B.CORELEPAC AS Correo
,B.IPTELMOVI AS Movil
,A.CODPROSAL AS CodigoMedico
,'Cedula' AS TipoIdentificacionMedico
,H.CODIGONIT AS IdentificacionMedico
,H.NOMMEDICO AS NombreMedico
,B.IPPRINOMB AS PrimerNombre
,CASE B.IPSEGNOMB WHEN '' THEN ' ' ELSE B.IPSEGNOMB END AS SegundoNombre
,B.IPPRIAPEL AS PrimerApellido
,B.IPSEGAPEL AS SegundoApellido
,RTRIM(MUN.DEPMUNCOD) AS MunicipioCodigo
,RTRIM(MUN.MUNNOMBRE) AS MunicipioNombre
,UBI.UBINOMBRE AS Ciudad
,RTRIM(ENT.Code) AS EntidadPagadoraCodigo
,ISNULL(T.Nit + '-' + T.DigitVerification, A.IPCODPACI) AS EntidadPagadoraNit
,RTRIM(ENT.Name) AS EntidadPagadoraNombre
,'' AS CodigoDiagnosticoSegundo
,'' AS NombreDiagnosticoSegundo
,'' CodigoDiagnosticoTercero
,'' AS NombreDiagnosticoTercero
,'Rutinario' Prioridad
,'Ambulatoria' AS TipoOrden
,e.ICAUSAING AS CodigoCausaIngreso
/*,CASE e.ICAUSAING WHEN 1 THEN 'Heridos en combate' WHEN 2 THEN 'Enfermedad profesional' WHEN 3 THEN 'Enfermedad general adulto' WHEN 4 THEN 'Enfermedad general pediatria' WHEN 5 THEN 'Odontología' WHEN 6 THEN 'Accidente de transito' WHEN 7 THEN 'Catastrofe/Fisalud' WHEN 8 THEN 'Quemados' WHEN 9 THEN 'Maternidad' WHEN 10 THEN 'Accidente Laboral' WHEN 11 THEN 'Cirugia Programada' END CausaIngreso*/
,COA.Name AS CausaIngreso
,A.FECRECMUE AS FechaRecoleccionMuestra
,CONVERT(CHAR(8), A.FECRECMUE, 112) + REPLACE(CONVERT(CHAR(8), A.FECRECMUE, 108), ':', '') FechaRecoleccionMuestraCorta
,A.USURECMUE AS CodigoUsuarioRecolectaMuestra
,TRIM(USU.CODUSUARI) + ' - '+ TRIM(USU.NOMUSUARI) AS UsuarioRecolectaMuestra
,CONVERT(CHAR(8), A.FECORDMED, 112) + REPLACE(CONVERT(CHAR(8), A.FECORDMED, 108), ':', '') FechaSugeridaMuestra																																						 
,ISNULL(B.IPESTRATO,0) AS Estrato
,H.MEDPRINOM AS MedicoPrimerNombre
,H.MEDSEGNOM AS MedicoSegundoNombre
,H.MEDPRIAPEL AS MedicoPrimerApellido
,H.MEDSEGAPEL AS MedicoSegundoApellido
,'' AS CodigoEspecialidad
,'' AS Especialidad
,CASE B.IPSEXOPAC WHEN 1 THEN 'No Aplica' ELSE CASE RP.GESTACION WHEN 1 THEN 'Si' ELSE 'No' END END AS Gestacion
--,A.CANSERIPS AS Cantidad
FROM
dbo.AMBORDLAB AS A with(nolock)
INNER JOIN (SELECT AUTOLABOR, MAX(CODCONCEC) AS CODCONCEC FROM dbo.INTERDETA WHERE ORDTIP = 'AMB' GROUP BY AUTOLABOR) AS MAX_INTER ON A.AUTO = MAX_INTER.AUTOLABOR
--INNER JOIN dbo.INTERDETA AS INTER ON INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'AMB'
INNER JOIN dbo.INPACIENT AS B with(nolock) ON A.IPCODPACI = B.IPCODPACI
INNER JOIN dbo.INCUPSIPS AS D with(nolock) ON A.CODSERIPS = D .CODSERIPS
INNER JOIN dbo.ADINGRESO AS E with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES
INNER JOIN dbo.ADTIPOIDENTIFICA AS IDEN WITH(NOLOCK) ON IDEN.CODIGO = B.IPTIPODOC
INNER JOIN dbo.CausesOfAttention AS COA WITH(NOLOCK) ON COA.Code = E.ICAUSAING
INNER JOIN Contract.HealthAdministrator ENT ON E.GENCONENTITY = ENT.Id
INNER JOIN Common.ThirdParty T ON T.Id = ENT.ThirdPartyId
INNER JOIN dbo.INUNIFUNC AS U with(nolock) ON E.UFUACTPAC = U.UFUCODIGO
INNER JOIN dbo.INPROFSAL AS H with(nolock) ON A.CODPROSAL = H.CODPROSAL
INNER JOIN INUBICACI UBI with(nolock) ON B.AUUBICACI = UBI.AUUBICACI
INNER JOIN INMUNICIP MUN with(nolock) ON UBI.DEPMUNCOD= MUN.DEPMUNCOD
INNER JOIN ADCENATEN CA WITH(NOLOCK) ON CA.CODCENATE = A.CODCENATE
LEFT JOIN dbo.HCRIESGOSP AS RP with(nolock) ON RP.NUMINGRCES = A.NUMINGRES
LEFT JOIN SEGusuaru USU with(nolock) ON USU.CODUSUARI = A.USURECMUE
LEFT JOIN contract.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
LEFT JOIN contract.ContractDescriptions CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId
WHERE A.ESTSERIPS = 2 AND (A.SYNCMIRTH IS NULL)
AND A.FECORDMED BETWEEN DATEADD(MONTH, -1, Common.GETDATE()) AND Common.GETDATE()
AND A.CODSERIPS NOT IN ('911003','911005','911009','911011_J','911013','911015','911016','911017','911018','911019','911020','911021','911033') --Oferta BDS
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de integración que consolida las órdenes de laboratorio clínico pendientes de sincronización con el motor de mensajería Mirth Connect. Combina datos de la orden médica (HCORDLABO), información demográfica del paciente (INPACIENT), el episodio de ingreso o admisión (ADINGRESO), el catálogo de servicios CUPS (INCUPSIPS), la unidad funcional solicitante (INUNIFUNC), el médico tratante, la entidad pagadora (EPS/aseguradora) y los diagnósticos CIE-10 principales, secundarios y terciarios del folio. Expone para cada examen de laboratorio solicitado: identificación y nombre del paciente, tipo y número de documento, fecha y prioridad de la orden, código y descripción del servicio, centro de atención, unidad funcional, número de ingreso, folio, cama asignada, datos de contacto, médico solicitante, entidad pagadora con NIT, causa de ingreso, fechas de recolección de muestra y estado de la orden. Es consumida por Mirth para enviar mensajes HL7 u otros protocolos de intercambio hacia el sistema de información del laboratorio externo o interno.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'LaboratoryOrders_Mirth_Synch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'LaboratoryOrders_Mirth_Synch';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone órdenes de laboratorio (hospitalarias y ambulatorias) pendientes de sincronizar al motor de integración Mirth, consolidando datos clínicos, demográficos y administrativos para su envío externo.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden debe tener estado de servicio igual a 2 (ESTSERIPS = 2); La orden no debe haber sido sincronizada previamente con Mirth (SYNCMIRTH IS NULL); La fecha de orden médica debe estar dentro del último mes respecto a Common.GETDATE(); El código de servicio (CODSERIPS) no debe pertenecer a la lista de códigos excluidos de la ''Oferta BDS'' (911003, 911005, 911009, 911011_J, 911013, 911015, 911016, 911017, 911018, 911019, 911020, 911021, 911033); Debe existir un registro en INTERDETA con ORDTIP=''INT'' (hospitalaria) o ''AMB'' (ambulatoria) asociado al AUTO de la orden; El paciente, ingreso, profesional de salud, centro de atención, ubicación geográfica, entidad pagadora (HealthAdministrator) y tercero deben existir (joins INNER)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes con estado de servicio = 2 y aún no sincronizadas con Mirth; Solo se incluyen órdenes con fecha de solicitud dentro del último mes; Nunca se incluyen los códigos de servicio de la ''Oferta BDS'' listados como excluidos; Para cada orden se selecciona únicamente el máximo CODCONCEC en INTERDETA agrupado por AUTOLABOR (un único número de orden Indigo por orden); CodeBD se deriva siempre de los caracteres 7 a 9 del nombre de la base de datos; Las fechas FechaActual, FechaSolicitudCorta, FechaRecoleccionMuestraCorta se entregan en formato yyyymmddhhmmss; FechaActual se calcula en zona horaria ''SA Pacific Standard Time'' a partir de UTC; La rama ambulatoria siempre marca TipoOrden=''Ambulatoria'', Prioridad=''Rutinario'' y sin diagnósticos/cama/folio; La identificación del médico siempre se reporta como TipoIdentificacionMedico=''Cedula''; Si el paciente es masculino, Gestacion siempre es ''No Aplica''', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Integrations.LaboratoryOrders_Mirth_Synch: Cuando ESTSERIPS=2, SYNCMIRTH IS NULL y FECORDMED en último mes, se expone la orden con TipoOrden=''Hospitalaria'' (origen HCORDLABO) o ''Ambulatoria'' (origen AMBORDLAB)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen dbo.HCORDLABO con INTERDETA.ORDTIP=''INT'' → Marca TipoOrden=''Hospitalaria'', incluye diagnóstico principal, segundo y tercer diagnóstico (de INDIAGNOH), cama, folio, especialidad y observación de OBSSERIPS; si Origen dbo.AMBORDLAB con INTERDETA.ORDTIP=''AMB'' → Marca TipoOrden=''Ambulatoria'', omite diagnósticos (NA), cama (##), folio (NULL) y especialidad; usa OBSERVACI como observación; si A.PRISERIPS = ''1'' (solo rama hospitalaria) → Prioridad=''Urgente'' else Prioridad=''Rutinario''; si B.IPSEXOPAC = 1 → Sexo=''Masculino'' y Gestacion=''No Aplica'' else Sexo=''Femenino'' y Gestacion depende de RP.GESTACION (1=''Si'', otro=''No''); si Conteo de diagnósticos no principales (CODDIAPRI=0) > 2 para el ingreso/folio → Se reporta CodigoDiagnosticoTercero y NombreDiagnosticoTercero (mayor CODDIAGNO) else Tercer diagnóstico queda NULL; si A.CODDIAGNO = ''   '' (vacío) → CodigoDiagnostico se reporta como ''na''; si U.UFUCODIGO es NULL (rama hospitalaria) → Se asume CodigoUnidad=''1111001'' y NombreUnidad=''URGENCIAS OBSERVACION'' por defecto; si T.Nit y T.DigitVerification no nulos → EntidadPagadoraNit = Nit + ''-'' + DigitVerification else EntidadPagadoraNit toma el valor del documento del paciente (A.IPCODPACI); si Mapeo de B.IPTIPOPAC (0–7) → Traduce a tipo de paciente (Contributivo, Subsidiado, Vinculado, Particular, Desplazado…); si no mapea, ''No Aplica''; si Mapeo de B.IPTIPOAFI (0–5) → Traduce a TipoAfiliacion (No Aplica, Cotizante, Beneficiario, Adicional, Jub/Retirado, Pensionado)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.AMBORDLAB; dbo.INTERDETA; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.ADTIPOIDENTIFICA; dbo.CausesOfAttention; Contract.HealthAdministrator; Common.ThirdParty; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INDIAGNOH; dbo.HCHISPACA; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADCENATEN; dbo.HCRIESGOSP; dbo.SEGusuaru; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch';
GO
