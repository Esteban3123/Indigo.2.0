CREATE VIEW [Integrations].[LaboratoryPreOrders_Mirth_Synch] as

SELECT     
SUBSTRING(DB_NAME(), 7, 3) AS CodeBD, 
A.[AUTO] AS ID, 
INTER.CODCONCEC AS NumeroOrdenIndigo, 
A.FECORDMED AS FechaSolicitud, 
CASE B.IPTIPODOC WHEN 1 THEN 'CC' 
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
				 WHEN 13 THEN 'PT' 
				 WHEN 14 THEN 'DE' 
				 WHEN 15 THEN 'SI' END AS CodigoTipoDocumento, 
A.CODSERIPS AS CodigoServicio, 
CD.Code AS CodigoRelacionado, 
RTRIM(D .DESSERIPS)+ '. ' + isnull(CD.name, '') AS DescripcionServicio, 
CASE B.IPTIPODOC WHEN 1 THEN 'Cédula de Ciudadanía' 
				 WHEN 2 THEN 'Cédula de Extranjería' 
				 WHEN 3 THEN 'Tarjeta de Identidad' 
				 WHEN 4 THEN 'Registro Civil' 
				 WHEN 5 THEN 'Pasaporte' 
				 WHEN 6 THEN 'Adulto Sin Identificación'
                 WHEN 7 THEN 'Menor Sin Identificación' 
				 WHEN 8 THEN 'Número único de identificación personal' 
				 WHEN 9 THEN 'Certificado Nacido Vivo' 
				 WHEN 10 THEN 'Carnet Diplomático (Aplica para extranjeros)' 
				 WHEN 12 THEN 'Salvoconducto (Aplica para extranjeros)'
                 WHEN 12 THEN 'Permiso especial de Permanencia (Aplica para extranjeros)' END TipoDocumento, 
RTRIM(A.IPCODPACI) AS Identificacion, 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
RTRIM(CA.CODCENATE) AS CodigoCentroAtencion, 
ISNULL(CASE B.IPTIPOPAC WHEN 0 THEN 'Contributivo' 
						WHEN 1 THEN 'Subsidiado' 
						WHEN 2 THEN 'Vinculado' 
						WHEN 3 THEN 'Particular' 
						WHEN 5 THEN 'Desplazado Reg. Contributivo' 
						WHEN 6 THEN 'Desplazado Reg. Subsidiado'
                        WHEN 7 THEN 'Desplazado no Asegurado' END, 'No Aplica') TipoPaciente, 
CASE B.IPTIPOAFI WHEN 0 THEN 'No Aplica' 
				 WHEN 1 THEN 'Cotizante' 
				 WHEN 2 THEN 'Beneficiario' 
				 WHEN 3 THEN 'Adicional' 
				 WHEN 4 THEN 'Jub/Retirado' 
				 WHEN 5 THEN 'Pensionado' END TipoAfiliacion, 
RTRIM(ltrim(CA.CODCENATE)) + ' - ' + RTRIM(ltrim(CA.NOMCENATE)) AS NombreCentroAtencion, 
CA.NOMCENATE NomCentroAtencion,
B.IPFECNACI AS FechaNacimiento,
replace(cast(B.IPFECNACI as date),'-','') as FechaNacimientoCorta,
U.UFUCODIGO AS CodigoUnidad, 
U.UFUDESCRI NombreUnidad,
RTRIM(ltrim(U.UFUCODIGO)) + ' - ' + RTRIM(ltrim(U.UFUDESCRI)) AS UnidadFuncional, 
A.ESTSERIPS AS Estado, 
A.NUMINGRES AS Ingreso, 
A.NUMEFOLIO AS Folio, 
A.OBSSERIPS AS ObservacionServicio, 
CASE A.CODDIAGNO when '   ' then 'na' else A.CODDIAGNO end AS CodigoDiagnostico, 
CASE RTRIM(I.NOMDIAGNO) WHEN '' THEN 'NA' ELSE RTRIM(I.NOMDIAGNO) END AS Diagnostico, 
ISNULL(G.DESCCAMAS,'SIN_CAMA')AS CAMA,
b.IPDIRECCI AS Direccion, 
b.IPTELEFON AS Telefono, 
CASE b.IPSEXOPAC WHEN 1 THEN 'Masculino' ELSE 'Femenino' END AS Sexo, 
b.IPRHSANGR AS Rh, 
b.IPGRUPSAN AS GrupoSanguineo, 
B.CORELEPAC AS Correo, 
b.IPTELMOVI AS Movil, 
A.CODPROSAL AS CodigoMedico, 
'Cedula' AS TipoIdentificacionMedico, 
H.CODIGONIT AS IdentificacionMedico, 
H.NOMMEDICO AS NombreMedico, 
B.IPPRINOMB AS PrimerNombre, 
CASE B.IPSEGNOMB WHEN '' THEN 'NA' ELSE B.IPSEGNOMB END AS SegundoNombre,
B.IPPRIAPEL AS PrimerApellido, 
B.IPSEGAPEL AS SegundoApellido, 
RTRIM(MUN.DEPMUNCOD) AS MunicipioCodigo, 
RTRIM(MUN.MUNNOMBRE) AS MunicipioNombre, 
RTRIM(ENT.Code) AS EntidadPagadoraCodigo, 
ISNULL(T .Nit+'-'+t.DigitVerification,A.IPCODPACI) AS EntidadPagadoraNit, 
RTRIM(ENT.Name) AS EntidadPagadoraNombre,
(
SELECT TOP 1 CODDIAGNO 
FROM DBO.INDIAGNOH 
WHERE IPCODPACI = b.IPCODPACI AND NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0
ORDER BY CODDIAGNO ASC
) AS CodigoDiagnosticoSegundo,
(
SELECT TOP 1 DESCR.NOMDIAGNO
FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR
WHERE IPCODPACI = b.IPCODPACI AND NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 AND DESCR.CODDIAGNO = COD.CODDIAGNO
ORDER BY COD.CODDIAGNO ASC
) AS NombreDiagnosticoSegundo, 
CASE WHEN((SELECT COUNT(CODDIAGNO)
FROM DBO.INDIAGNOH
WHERE IPCODPACI = b.IPCODPACI AND NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN
(SELECT TOP 1 CODDIAGNO
FROM  DBO.INDIAGNOH
WHERE        IPCODPACI = b.IPCODPACI AND NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0
ORDER BY CODDIAGNO DESC) END AS CodigoDiagnosticoTercero, CASE WHEN
((SELECT COUNT(CODDIAGNO)
FROM DBO.INDIAGNOH
WHERE IPCODPACI = b.IPCODPACI AND NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN
(SELECT TOP 1 DESCR.NOMDIAGNO
FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR
WHERE IPCODPACI = b.IPCODPACI AND NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 AND DESCR.CODDIAGNO = COD.CODDIAGNO
ORDER BY COD.CODDIAGNO DESC) END AS NombreDiagnosticoTercero, 
CASE WHEN A.PRISERIPS = '1' THEN 'Urgente' ELSE 'Rutinario' END AS Prioridad, 
'Hospitalaria' AS TipoOrden, 
e.ICAUSAING AS CodigoCausaIngreso,
CASE e.ICAUSAING WHEN 1 THEN 'Heridos en combate' 
				 WHEN 2 THEN 'Enfermedad profesional ' 
				 WHEN 3 THEN 'Enfermedad general adulto ' 
				 WHEN 4 THEN ' Enfermedad general pediatria ' 
				 WHEN 5 THEN 'Odontología ' 
				 WHEN 6 THEN 'Accidente de transito'
				 WHEN 7 THEN 'Catastrofe/Fisalud '
				 WHEN 8 THEN 'Quemados '
				 WHEN 9 THEN 'Maternidad'
				 WHEN 10 THEN 'Accidente Laboral'
				 WHEN 11 THEN 'Cirugia Programada' END CausaIngreso, 
A.FECRECMUE AS FechaRecoleccionMuestra,
A.USURECMUE AS CodigoUsuarioRecolectaMuestra,
rtrim(ltrim(USU.CODUSUARI)) + ' - ' + rtrim(ltrim(USU.NOMUSUARI)) AS UsuarioRecolectaMuestra,
ISNULL(B.IPESTRATO, 0) AS Estrato,
H.MEDPRINOM AS MedicoPrimerNombre,
H.MEDSEGNOM AS MedicoSegundoNombre,
H.MEDPRIAPEL AS MedicoPrimerApellido,
H.MEDSEGAPEL AS MedicoSegundoApellido,
rtrim(ltrim(ESP.CODESPECI)) AS CodigoEspecialidad,
rtrim(ltrim(ESP.DESESPECI)) AS Especialidad,
CASE B.IPSEXOPAC WHEN 1 THEN 'No Aplica' ELSE CASE RP.GESTACION WHEN 1 THEN 'Si' ELSE 'No' END END AS Gestacion, 
A.CANSERIPS AS Cantidad,
SYNCMIRTH
FROM            
dbo.HCORDLABO AS A WITH (nolock) INNER JOIN
dbo.INTERDETA AS INTER ON INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'INT' INNER JOIN
dbo.INPACIENT AS B WITH (nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
dbo.INCUPSIPS AS D WITH (nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
dbo.ADINGRESO AS E WITH (nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
Contract.HealthAdministrator ENT ON E.GENCONENTITY = ENT.Id INNER JOIN
Common.ThirdParty T ON T .Id = ENT.ThirdPartyId INNER JOIN
dbo.INUNIFUNC AS U WITH (nolock) ON A.UFUCODIGO = U.UFUCODIGO LEFT JOIN
dbo.CHCAMASHO AS G WITH (nolock) ON E.CODCAMACT = G.CODICAMAS INNER JOIN
dbo.INPROFSAL AS H WITH (nolock) ON A.CODPROSAL = H.CODPROSAL LEFT OUTER JOIN
dbo.INDIAGNOS AS I WITH (nolock) ON A.CODDIAGNO = I.CODDIAGNO INNER JOIN
dbo.HCHISPACA AS HIS WITH (nolock) ON HIS.NUMINGRES = A.NUMINGRES AND HIS.NUMEFOLIO = A.NUMEFOLIO INNER JOIN
dbo.INESPECIA AS ESP WITH (nolock) ON ESP.CODESPECI = HIS.CODESPTRA INNER JOIN
INUBICACI UBI WITH (nolock) ON B.AUUBICACI = UBI.AUUBICACI INNER JOIN
INMUNICIP MUN WITH (nolock) ON UBI.DEPMUNCOD = MUN.DEPMUNCOD INNER JOIN
ADCENATEN CA WITH (nolock) ON CA.CODCENATE = A.CODCENATE LEFT JOIN
dbo.HCRIESGOSP AS RP WITH (nolock) ON RP.NUMINGRCES = A.NUMINGRES LEFT JOIN
SEGusuaru USU WITH (nolock) ON USU.CODUSUARI = A.USURECMUE LEFT JOIN
contract.CUPSEntityContractDescriptions CDD WITH (nolock) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN
contract.ContractDescriptions CD WITH (nolock) ON CD.Id = CDD.ContractDescriptionId
WHERE A.ESTSERIPS = 1
	  AND (SYNCMIRTH IS NULL OR SYNCMIRTH = 0)
	  AND A.FECORDMED >= '2023-11-28 09:00:00.000'
	  AND A.CODSERIPS NOT IN ('911003','911005','911009','911011_J','911013','911015','911016','911017','911018','911019','911020','911021','911033')

UNION

SELECT        
SUBSTRING(DB_NAME(), 7, 3) AS CodeBD, 
A.[AUTO] AS ID, 
INTER.CODCONCEC AS NumeroOrdenIndigo, 
A.FECORDMED AS FechaSolicitud, 
CASE B.IPTIPODOC WHEN 1 THEN 'CC' 
				 WHEN 2 THEN 'CE' 
				 WHEN 3 THEN 'TI' 
				 WHEN 4 THEN 'RC' 
				 WHEN 5 THEN 'PA' 
				 WHEN 6 THEN 'AS' 
				 WHEN 7 THEN 'MS' 
				 WHEN 8 THEN 'NU' 
				 WHEN 9 THEN 'CN' WHEN 10 THEN 'CD' WHEN
                          11 THEN 'SC' WHEN 12 THEN 'PE' WHEN 13 THEN 'PT' WHEN 14 THEN 'DE' WHEN 15 THEN 'SI' END AS CodigoTipoDocumento, RTRIM(A.CODSERIPS) AS CodigoServicio, CD.Code AS CodigoRelacionado, 
RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name, '') AS DescripcionServicio, 
CASE B.IPTIPODOC WHEN 1 THEN 'Cédula de Ciudadanía' 
				 WHEN 2 THEN 'Cédula de Extranjería' 
				 WHEN 3 THEN 'Tarjeta de Identidad' 
				 WHEN 4 THEN 'Registro Civil' 
				 WHEN 5 THEN 'Pasaporte' 
				 WHEN 6 THEN 'Adulto Sin Identificación'
                 WHEN 7 THEN 'Menor Sin Identificación' 
				 WHEN 8 THEN 'Número único de identificación personal' 
				 WHEN 9 THEN 'Certificado Nacido Vivo' 
				 WHEN 10 THEN 'Carnet Diplomático (Aplica para extranjeros)' 
				 WHEN 12 THEN 'Salvoconducto (Aplica para extranjeros)'
                 WHEN 12 THEN 'Permiso especial de Permanencia (Aplica para extranjeros)' END TipoDocumento, 
RTRIM(A.IPCODPACI) AS Identificacion, 
RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
RTRIM(CA.CODCENATE) AS CodigoCentroAtencion, 
ISNULL(CASE B.IPTIPOPAC WHEN 0 THEN 'Contributivo' 
						WHEN 1 THEN 'Subsidiado' 
						WHEN 2 THEN 'Vinculado' 
						WHEN 3 THEN 'Particular' 
						WHEN 5 THEN 'Desplazado Reg. Contributivo' 
						WHEN 6 THEN 'Desplazado Reg. Subsidiado'
                        WHEN 7 THEN 'Desplazado no Asegurado' END, 'No Aplica') TipoPaciente, 
CASE B.IPTIPOAFI WHEN 0 THEN 'No Aplica' 
				 WHEN 1 THEN 'Cotizante' 
				 WHEN 2 THEN 'Beneficiario' 
				 WHEN 3 THEN 'Adicional' 
				 WHEN 4 THEN 'Jub/Retirado' 
				 WHEN 5 THEN 'Pensionado' END TipoAfiliacion, 
RTRIM(ltrim(CA.CODCENATE)) + ' - ' + RTRIM(ltrim(CA.NOMCENATE)) AS NombreCentroAtencion, 
CA.NOMCENATE NomCentroAtencion,
B.IPFECNACI AS FechaNacimiento, 
replace(cast(B.IPFECNACI as date),'-','') as FechaNacimientoCorta,
E.UFUACTPAC AS CodigoUnidad, 
U.UFUDESCRI NombreUnidad,
RTRIM(ltrim(U.UFUCODIGO)) + ' - ' + RTRIM(ltrim(U.UFUDESCRI)) AS UnidadFuncional, 
A.ESTSERIPS AS Estado, 
A.NUMINGRES AS Ingreso, 
NULL AS Folio, 
A.OBSERVACI AS ObservacionServicio, 
'NA' AS CodigoDiagnostico, 
'NA' AS Diagnostico, 
'##' AS Cama, 
b.IPDIRECCI AS Direccion, 
b.IPTELEFON AS Telefono, 
CASE b.IPSEXOPAC WHEN 1 THEN 'Masculino' ELSE 'Femenino' END AS Sexo, 
b.IPRHSANGR AS Rh, 
b.IPGRUPSAN AS GrupoSanguineo, 
b.CORELEPAC AS Correo, 
b.IPTELMOVI AS Movil, 
A.CODPROSAL AS CodigoMedico, 
'Cedula' AS TipoIdentificacionMedico, 
H.CODIGONIT AS IdentificacionMedico, 
H.NOMMEDICO AS NombreMedico, 
B.IPPRINOMB AS PrimerNombre, 
CASE B.IPSEGNOMB WHEN NULL THEN '' ELSE B.IPSEGNOMB END AS SegundoNombre, 
B.IPPRIAPEL AS PrimerApellido, 
B.IPSEGAPEL AS SegundoApellido, 
RTRIM(MUN.DEPMUNCOD) AS MunicipioCodigo, 
RTRIM(MUN.MUNNOMBRE) AS MunicipioNombre, 
RTRIM(ENT.Code) AS EntidadPagadoraCodigo, 
ISNULL(T .Nit+'-'+t.DigitVerification,A.IPCODPACI) AS EntidadPagadoraNit, 
RTRIM(ENT.Name) AS EntidadPagadoraNombre, 
'' AS CodigoDiagnosticoSegundo, 
'' AS NombreDiagnosticoSegundo, 
'' CodigoDiagnosticoTercero, 
'' AS NombreDiagnosticoTercero, 
'Rutinario' Prioridad, 
'Ambulatoria' AS TipoOrden, 
e.ICAUSAING AS CodigoCausaIngreso, 
CASE e.ICAUSAING WHEN 1 THEN 'Heridos en combate' 
				 WHEN 2 THEN 'Enfermedad profesional ' 
				 WHEN 3 THEN 'Enfermedad general adulto ' 
				 WHEN 4 THEN ' Enfermedad general pediatria ' 
				 WHEN 5 THEN 'Odontología ' 
				 WHEN 6 THEN 'Accidente de transito' 
				 WHEN 7 THEN 'Catastrofe/Fisalud ' 
				 WHEN 8 THEN 'Quemados ' 
				 WHEN 9 THEN 'Maternidad' 
				 WHEN 10 THEN 'Accidente Laboral' 
				 WHEN 11 THEN 'Cirugia Programada' END CausaIngreso, 
A.FECRECMUE AS FechaRecoleccionMuestra, 
A.USURECMUE AS CodigoUsuarioRecolectaMuestra, 
rtrim(ltrim(USU.CODUSUARI)) + ' - ' + rtrim(ltrim(USU.NOMUSUARI)) AS UsuarioRecolectaMuestra, 
ISNULL(B.IPESTRATO, 0) AS Estrato, 
H.MEDPRINOM AS MedicoPrimerNombre, 
H.MEDSEGNOM AS MedicoSegundoNombre, 
H.MEDPRIAPEL AS MedicoPrimerApellido, 
H.MEDSEGAPEL AS MedicoSegundoApellido, 
'' AS CodigoEspecialidad, 
'' AS Especialidad, 
CASE B.IPSEXOPAC WHEN 1 THEN 'No Aplica' ELSE 
CASE RP.GESTACION WHEN 1 THEN 'Si' ELSE 'No' END END AS Gestacion, 
A.CANSERIPS AS Cantidad,
SYNCMIRTH
FROM            
dbo.AMBORDLAB AS A WITH (nolock) INNER JOIN
dbo.INTERDETA AS INTER ON INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'AMB' INNER JOIN
dbo.INPACIENT AS B WITH (nolock) ON A.IPCODPACI = B.IPCODPACI INNER JOIN
dbo.INCUPSIPS AS D WITH (nolock) ON A.CODSERIPS = D .CODSERIPS INNER JOIN
dbo.ADINGRESO AS E WITH (nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
Contract.HealthAdministrator ENT ON E.GENCONENTITY = ENT.Id INNER JOIN
Common.ThirdParty T ON T .Id = ENT.ThirdPartyId INNER JOIN
dbo.INUNIFUNC AS U WITH (nolock) ON E.UFUACTPAC = U.UFUCODIGO INNER JOIN
dbo.INPROFSAL AS H WITH (nolock) ON A.CODPROSAL = H.CODPROSAL INNER JOIN
INUBICACI UBI WITH (nolock) ON B.AUUBICACI = UBI.AUUBICACI INNER JOIN
INMUNICIP MUN WITH (nolock) ON UBI.DEPMUNCOD = MUN.DEPMUNCOD LEFT JOIN
contract.CUPSEntityContractDescriptions CDD ON CDD.Id = A.IDDESCRIPCIONRELACIONADA LEFT JOIN
contract.ContractDescriptions CD ON CD.Id = CDD.ContractDescriptionId LEFT JOIN
SEGusuaru USU WITH (nolock) ON USU.CODUSUARI = A.USURECMUE LEFT JOIN
dbo.HCRIESGOSP AS RP WITH (nolock) ON RP.NUMINGRCES = A.NUMINGRES INNER JOIN
ADCENATEN CA ON CA.CODCENATE = A.CODCENATE
WHERE A.ESTSERIPS = 1
	  AND (SYNCMIRTH IS NULL OR SYNCMIRTH = 0)
	  AND A.FECORDMED >= '2023-11-28 09:00:00.000'
	  AND A.CODSERIPS NOT IN ('911003','911005','911009','911011_J','911013','911015','911016','911017','911018','911019','911020','911021','911033')