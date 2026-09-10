

CREATE view [Integrations].[LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR]
as

      SELECT  
	  [AUTO] as ID,
	  A.FECORDMED AS FechaSolicitud, 
	  	  B.IPTIPODOC as CodigoTipoDocumento,
	  case B.IPTIPODOC when 1 then 'Cédula de Ciudadanía' when 2 then 'Cédula de Extranjería'
	   when 3 then 'Tarjeta de Identidad'  when 4 then 'Registro Civil'  when 5 then 'Pasaporte'  when 6 then 'Adulto Sin Identificación'  when 7 then 'Menor Sin Identificación' 
	   when 8 then 'Número único de identificación personal'  when 9 then 'Certificado Nacido Vivo' when 10 then 'Carnet Diplomático (Aplica para extranjeros)' 
	   when 12 then 'Salvoconducto (Aplica para extranjeros)' when 12 then 'Permiso especial de Permanencia (Aplica para extranjeros)'  END TipoDocumento,
	  RTRIM(A.IPCODPACI) AS Identificacion, 
      RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
	  RTRIM(CA.CODCENATE) AS CodigoCentroAtencion,
	  RTRIM(CA.NOMCENATE) AS NombreCentroAtencion,
	  B.IPFECNACI AS FechaNacimiento, 
      E.UFUACTPAC AS CodigoUnidad,
	  RTRIM(C.UFUDESCRI) AS DescripcionUnidad,
	  A.ESTSERIPS AS Estado,
	  A.NUMINGRES AS Ingreso, 
      A.NUMEFOLIO AS Folio,
	  A.CODSERIPS AS CodigoServicio,
	  RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio,
	  A.OBSSERIPS AS ObservacionServicio, 
	  A.CODDIAGNO AS CodigoDiagnostico,
	  RTRIM(I.NOMDIAGNO) as Diagnostico, 
	  RTRIM(G.DESCCAMAS) AS Cama, 
	  b.IPDIRECCI AS Direccion,
	  b.IPTELEFON AS Telefono,
	  case b.IPSEXOPAC   when 1 then 'Masculino' else 'Femenino' END as Sexo,
	  b.IPRHSANGR as Rh, 
	  b.IPGRUPSAN as GrupoSanguineo,
	  B.CORELEPAC as Correo,
      b.IPTELMOVI as Movil,
	  A.CODPROSAL as CodigoMedico,
	  H.NOMMEDICO  AS NombreMedico, 
	  B.IPPRINOMB as PrimerNombre,
	  B.IPSEGNOMB as SegundoNombre,
	  B.IPPRIAPEL as PrimerApellido,
	  B.IPSEGAPEL as SegundoApellido,
	  RTRIM(MUN.DEPMUNCOD) as MunicipioCodigo,
	  RTRIM(MUN.MUNNOMBRE) as MunicipioNombre,
	  RTRIM(ENT.CODENTIDA) as EntidadPagadoraCodigo,
	  RTRIM(ENT.NOMENTIDA) as EntidadPagadoraNombre,
	  (SELECT TOP 1 CODDIAGNO FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 ORDER BY CODDIAGNO asc) as CodigoDiagnosticoSegundo,
	  (SELECT TOP 1 DESCR.NOMDIAGNO FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 and DESCR.CODDIAGNO = COD.CODDIAGNO ORDER BY COD.CODDIAGNO asc) as NombreDiagnosticoSegundo,
	 CASE WHEN ((SELECT COUNT(CODDIAGNO) FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN 
			(SELECT TOP 1 CODDIAGNO FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 ORDER BY CODDIAGNO desc) END as CodigoDiagnosticoTercero,
	  CASE WHEN ((SELECT COUNT(CODDIAGNO) FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN 
			(SELECT TOP 1 DESCR.NOMDIAGNO FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 and DESCR.CODDIAGNO = COD.CODDIAGNO ORDER BY COD.CODDIAGNO DESC) END as NombreDiagnosticoTercero,
	  CASE WHEN A.PRISERIPS = '1' THEN 'Urgente' else 'Rutinario' END AS Prioridad,	  
	  'Hospitalaria' as TipoOrden,
	   '<?xml version="1.0" encoding="UTF‐8"?>
<OML_O21>
<!-- EJEMPLO DE MENSAJE PARA ORDENES OML^021 -->
<MSH>
<MSH.1>|</MSH.1>
<MSH.2>^~\&amp;</MSH.2>
<!-- MENSAJE ENVIADO POR VIE AL LIS -->
<MSH.3>
<HD.1>Indigo</HD.1>
</MSH.3>
<MSH.4>
<HD.1>Indigo Vie His</HD.1>
</MSH.4>
<!-- PARA EL LIS -->
<MSH.5>
<HD.1>LIS</HD.1>
</MSH.5>
<MSH.6>
<HD.1>Modulab</HD.1>
</MSH.6>
<!-- FECHA DEL MENSAJE -->
<MSH.7>
<TS.1>' + format(A.FECORDMED,'yyyyMMddhhmmss') + '</TS.1>
</MSH.7>
<!-- TIPO DE MENSAJE : ORDEN DE LAB -->
<MSH.9>
<MSG.1>OML</MSG.1>
<MSG.2>O21</MSG.2>
<MSG.3>OML_O21</MSG.3>
</MSH.9>
<!-- IDENTIFICADOR DEL MENSAJE -->
<MSH.10>'+ concat(REPLACE(DB_NAME(),'INDIGO','') , cast(A.AUTO as varchar(20))) +'</MSH.10>
<!-- MODO: PRODUCCION -->
<MSH.11>
<PT.1>P</PT.1>
</MSH.11>
<!-- VERSION DEL ESTANDAR -->
<MSH.12>
<VID.1>2.5</VID.1>
</MSH.12>
<!-- RESPONDER: SIEMPRE -->
<MSH.15>AL</MSH.15>
<MSH.16>NE</MSH.16>
</MSH>
<SFT>
<SFT.1>
<XON.1>Indigo Vie His</XON.1>
</SFT.1>
<SFT.2>Indigo</SFT.2>
<SFT.3>Vie His</SFT.3>
<SFT.4>version30.0</SFT.4>
</SFT>
<OML_O21.PATIENT>
<PID>
<PID.1>1</PID.1>
<!-- IDENTIFICADOR DEL PACIENTE -->
<PID.3>
<CX.1>'+ RTRIM(ltrim(B.IPCODPACI)) +'</CX.1>
<CX.4>
<HD.1>Vie HIS</HD.1>
</CX.4>
</PID.3>
<PID.3>
<CX.1>'+ RTRIM(ltrim(B.IPCODPACI)) +'</CX.1>
<CX.4> 
<HD.1>DNI</HD.1>
</CX.4>
</PID.3>
<!-- APELLIDO -->
<PID.5>
<XPN.1>
<!-- APELLIDO -->
<FN.1>'+RTRIM(ltrim(B.IPPRIAPEL)) + ' ' + RTRIM(ltrim(B.IPSEGAPEL)) +'</FN.1>
</XPN.1>
<!-- NOMBRES -->
<XPN.2>'+RTRIM(ltrim(B.IPPRINOMB)) + ' ' + RTRIM(ltrim(B.IPSEGNOMB))+'</XPN.2>
</PID.5>
<!-- FECHA DE NACIMIENTO -->
<PID.7>
<TS.1>'+format(B.IPFECNACI,'yyyyMMddhhmmss')+'</TS.1>
</PID.7>
<!-- SEXO -->
<PID.8>'+case B.IPSEXOPAC when 1 then 'M' else 'F' end+'</PID.8>
</PID>
<!-- TIPO DE VISITA -->
<OML_O21.PATIENT_VISIT>
<PV1>
<PV1.1>'+RTRIM(ltrim(E.NUMINGRES))+'</PV1.1>
<!-- AMBULATORIO: O -->
<PV1.2>A</PV1.2> 
<!-- UBICACION DEL PACIENTE -->
<PV1.3>
<PL.1>'+concat(RTRIM(ltrim(C.UFUCODIGO )),' ',RTRIM(ltrim(C.UFUDESCRI)))+'</PL.1>
</PV1.3>
<!-- ID DE VISITA -->
<PV1.19> 
<CX.1>'+RTRIM(ltrim(E.NUMINGRES))+'</CX.1> 
</PV1.19>
</PV1>
</OML_O21.PATIENT_VISIT> 
<!-- DATOS DE LA COBERTURA--> 
<OML_O21.INSURANCE>
<IN1>
<IN1.1>1</IN1.1>
<IN1.2>
<CE.1>'+RTRIM(ltrim(ENT.CODENTIDA))+'</CE.1>
<CE.2>'+RTRIM(ltrim(ENT.NOMENTIDA))+'</CE.2>
</IN1.2>
<!-- DE SER UN FINANCIADOR INCLUIR EL CUIT -->
<IN1.3>
<CX.1>0</CX.1>
</IN1.3>
<!-- NUMERO DE COBERTURA -->
<IN1.49>
<CX.1>'+RTRIM(ltrim(ENT.CODENTIDA))+'</CX.1>
</IN1.49>
</IN1>
</OML_O21.INSURANCE>
</OML_O21.PATIENT>
<!-- DATOS DE LA ORDEN ITEM # 1 -->
<OML_O21.ORDER>
<ORC>
<ORC.1>NW</ORC.1>
<!-- NUMERO DEL PEDIDO/RENGLON -->
<ORC.2>
<EI.1>'+ concat(REPLACE(DB_NAME(),'INDIGO','') , cast(A.AUTO as varchar(20)))  +'</EI.1>
<EI.2>vie His</EI.2>
</ORC.2>
<!-- NUMERO DEL PEDIDO -->
<ORC.4>
<EI.1>'+ concat(REPLACE(DB_NAME(),'INDIGO','') , cast(A.AUTO as varchar(20)))  +'</EI.1>
<EI.2>vie his</EI.2>
</ORC.4>
<!-- ESTADO DE LA ORDEN -2 muestra recolectada -->
<ORC.5>2</ORC.5>
<!-- FECHA DEL PEDIDO -->
<ORC.9>
<TS.1>'+ format(A.FECORDMED,'yyyyMMddhhmmss') +'</TS.1>
</ORC.9>
<!-- MEDICO SOLICITANTE -->
<ORC.12>
<!-- MATRICULA -->
<XCN.1>'+RTRIM(ltrim(H.CODPROSAL))+'</XCN.1>
<XCN.2>
<FN.1>'+RTRIM(ltrim(H.MEDPRINOM))+'</FN.1>
<FN.2>'+RTRIM(ltrim(H.MEDPRIAPEL))+'</FN.2>
</XCN.2>
<XCN.7>DR</XCN.7>
<XCN.10>MN</XCN.10>
</ORC.12>
</ORC>
<OML_O21.TIMING>
<TQ1>
<!-- CANTIDAD -->
<TQ1.2>
<CQ.1>'+CAST(A.CANSERIPS as varchar)+'</CQ.1>
</TQ1.2>
<!-- FECHA DE REALIZACION -->
<TQ1.7>
<TS.1>'+ format(A.FECORDMED,'yyyyMMddhhmmss') +'</TS.1>
</TQ1.7>
<!-- PRIORIDAD -->
<TQ1.9>
<CWE.1>U</CWE.1>
<CWE.2>Urgencia</CWE.2>
</TQ1.9>
</TQ1>
</OML_O21.TIMING> 
<!-- DETALLE DE LO SOLICITADO -->
<OML_O21.OBSERVATION_REQUEST>
<OBR>
<OBR.1>1</OBR.1>
<!-- NUMERO PEDIDO ‐ ITEM -->
<OBR.2>
<EI.1>'+ concat(REPLACE(DB_NAME(),'INDIGO','') , cast(A.AUTO as varchar(20)))  +'</EI.1>
<EI.2>Vie His</EI.2>
</OBR.2>
<!-- NUMERO PROTOCOLO ASIGNADO POR EL LIS -->
<OBR.3>
<EI.1></EI.1>
<EI.2></EI.2>
</OBR.3>
<!-- pruebas-->
<OBR.4>
<CE.1>'+RTRIM(ltrim(D.CODSERIPS))+'</CE.1>
<CE.2>'+RTRIM(ltrim(D.DESSERIPS))+'</CE.2>
</OBR.4>
</OBR>
<DG1>
<DG1.1>1</DG1.1>
<!-- PROBLEMA / DIAGNOSTICO -->
<DG1.3>
<CE.1>'+RTRIM(ltrim(I.CODDIAGNO))+'</CE.1>
<CE.2>'+RTRIM(ltrim(I.NOMDIAGNO))+'</CE.2>
</DG1.3>
<!-- TIPO DIAGNOSTICO -->
<DG1.6>W</DG1.6>
</DG1>
</OML_O21.OBSERVATION_REQUEST>
</OML_O21.ORDER>  
</OML_O21>' as MessageXML
      FROM  dbo.HCORDLABO AS A with(nolock)  INNER JOIN
      dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
      dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
      dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
	  dbo.INENTIDAD AS ENT with(nolock)  ON ENT.CODENTIDA = E.CODENTIDA INNER JOIN
      dbo.INUNIFUNC AS C with(nolock)  ON A.UFUCODIGO  = C.UFUCODIGO INNER JOIN
      dbo.CHCAMASHO AS G with(nolock)  ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
      dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
      dbo.INDIAGNOS AS I with(nolock)  ON A.CODDIAGNO = I.CODDIAGNO LEFT OUTER JOIN
      dbo.HCPARALELAB AS J with(nolock)  ON A.CODSERIPS = J.CODSERIPS AND J.CODCENATE=A.CODCENATE
	  INNER JOIN INUBICACI UBI with(nolock)  ON B.AUUBICACI = UBI.AUUBICACI
	  INNER JOIN INMUNICIP MUN with(nolock)  ON UBI.DEPMUNCOD= MUN.DEPMUNCOD
	  INNER JOIN ADCENATEN CA with(nolock) ON CA.CODCENATE = A.CODCENATE
	  LEFT JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA
	  LEFT JOIN contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
	  WHERE A.ESTSERIPS = 2 -- ORDER BY A.FECORDMED DESC

	  
	  UNION 

	  SELECT  
		[AUTO] as ID, 
		A.FECORDMED AS FechaSolicitud, 
			  B.IPTIPODOC as CodigoTipoDocumento,
	  case B.IPTIPODOC when 1 then 'Cédula de Ciudadanía' when 2 then 'Cédula de Extranjería'
	   when 3 then 'Tarjeta de Identidad'  when 4 then 'Registro Civil'  when 5 then 'Pasaporte'  when 6 then 'Adulto Sin Identificación'  when 7 then 'Menor Sin Identificación' 
	   when 8 then 'Número único de identificación personal'  when 9 then 'Certificado Nacido Vivo' when 10 then 'Carnet Diplomático (Aplica para extranjeros)' 
	   when 12 then 'Salvoconducto (Aplica para extranjeros)' when 12 then 'Permiso especial de Permanencia (Aplica para extranjeros)'  END TipoDocumento,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente,
		RTRIM(CA.CODCENATE) AS CodigoCentroAtencion,
		RTRIM(CA.NOMCENATE) AS NombreCentroAtencion,
		B.IPFECNACI AS FechaNacimiento, 
		E.UFUACTPAC AS CodigoUnidad, 
		RTRIM(C.UFUDESCRI)  AS DescripcionUnidad, 
		A.ESTSERIPS AS Estado, 		
		A.NUMINGRES AS Ingreso, 
		NULL as Folio,
		RTRIM(A.CODSERIPS) as CodigoServicio,
		RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, 
		A.OBSERVACI AS ObservacionServicio,
		'' AS CodigoDiagnostico,
		'' as Diagnostico,
		'' AS Cama,
		b.IPDIRECCI AS Direccion, 
		b.IPTELEFON AS Telefono,
		case b.IPSEXOPAC   when 1 then 'Masculino' else 'Femenino' END as Sexo,
		b.IPRHSANGR as Rh, 
		b.IPGRUPSAN as GrupoSanguineo, 
		b.CORELEPAC as Correo, 
		b.IPTELMOVI as Movil, 
		A.CODPROSAL as CodigoMedico,
		H.NOMMEDICO  AS NombreMedico, 
		B.IPPRINOMB as PrimerNombre,
		B.IPSEGNOMB as SegundoNombre,
		B.IPPRIAPEL as PrimerApellido,
		B.IPSEGAPEL as SegundoApellido,
		RTRIM(MUN.DEPMUNCOD) as MunicipioCodigo,
		RTRIM(MUN.MUNNOMBRE) as MunicipioNombre,
		RTRIM(ENT.CODENTIDA) as EntidadPagadoraCodigo,
		RTRIM(ENT.NOMENTIDA) as EntidadPagadoraNombre,
		'' as CodigoDiagnosticoSegundo, 
		'' as NombreDiagnosticoSegundo, 
		'' CodigoDiagnosticoTercero, 
		'' as NombreDiagnosticoTercero, 
		'Rutinario' Prioridad, 
		'Ambulatoria' as TipoOrden,
		 '<?xml version="1.0" encoding="UTF‐8"?>
<OML_O21>
<!-- EJEMPLO DE MENSAJE PARA ORDENES OML^021 -->
<MSH>
<MSH.1>|</MSH.1>
<MSH.2>^~\&amp;</MSH.2>
<!-- MENSAJE ENVIADO POR VIE AL LIS -->
<MSH.3>
<HD.1>Indigo</HD.1>
</MSH.3>
<MSH.4>
<HD.1>Indigo Vie His</HD.1>
</MSH.4>
<!-- PARA EL LIS -->
<MSH.5>
<HD.1>LIS</HD.1>
</MSH.5>
<MSH.6>
<HD.1>Modulab</HD.1>
</MSH.6>
<!-- FECHA DEL MENSAJE -->
<MSH.7>
<TS.1>' + format(A.FECORDMED,'yyyyMMddhhmmss') + '</TS.1>
</MSH.7>
<!-- TIPO DE MENSAJE : ORDEN DE LAB -->
<MSH.9>
<MSG.1>OML</MSG.1>
<MSG.2>O21</MSG.2>
<MSG.3>OML_O21</MSG.3>
</MSH.9>
<!-- IDENTIFICADOR DEL MENSAJE -->
<MSH.10>'+ concat(REPLACE(DB_NAME(),'INDIGO','') , cast(A.AUTO as varchar(20))) +'</MSH.10>
<!-- MODO: PRODUCCION -->
<MSH.11>
<PT.1>P</PT.1>
</MSH.11>
<!-- VERSION DEL ESTANDAR -->
<MSH.12>
<VID.1>2.5</VID.1>
</MSH.12>
<!-- RESPONDER: SIEMPRE -->
<MSH.15>AL</MSH.15>
<MSH.16>NE</MSH.16>
</MSH>
<SFT>
<SFT.1>
<XON.1>Indigo Vie His</XON.1>
</SFT.1>
<SFT.2>Indigo</SFT.2>
<SFT.3>Vie His</SFT.3>
<SFT.4>version30.0</SFT.4>
</SFT>
<OML_O21.PATIENT>
<PID>
<PID.1>1</PID.1>
<!-- IDENTIFICADOR DEL PACIENTE -->
<PID.3>
<CX.1>'+ RTRIM(ltrim(B.IPCODPACI)) +'</CX.1>
<CX.4>
<HD.1>Vie HIS</HD.1>
</CX.4>
</PID.3>
<PID.3>
<CX.1>'+ RTRIM(ltrim(B.IPCODPACI)) +'</CX.1>
<CX.4> 
<HD.1>DNI</HD.1>
</CX.4>
</PID.3>
<!-- APELLIDO -->
<PID.5>
<XPN.1>
<!-- APELLIDO -->
<FN.1>'+RTRIM(ltrim(B.IPPRIAPEL)) + ' ' + RTRIM(ltrim(B.IPSEGAPEL)) +'</FN.1>
</XPN.1>
<!-- NOMBRES -->
<XPN.2>'+RTRIM(ltrim(B.IPPRINOMB)) + ' ' + RTRIM(ltrim(B.IPSEGNOMB))+'</XPN.2>
</PID.5>
<!-- FECHA DE NACIMIENTO -->
<PID.7>
<TS.1>'+format(B.IPFECNACI,'yyyyMMddhhmmss')+'</TS.1>
</PID.7>
<!-- SEXO -->
<PID.8>'+case B.IPSEXOPAC when 1 then 'M' else 'F' end+'</PID.8>
</PID>
<!-- TIPO DE VISITA -->
<OML_O21.PATIENT_VISIT>
<PV1>
<PV1.1>'+RTRIM(ltrim(E.NUMINGRES))+'</PV1.1>
<!-- AMBULATORIO: O -->
<PV1.2>A</PV1.2> 
<!-- UBICACION DEL PACIENTE -->
<PV1.3>
<PL.1>'+concat(RTRIM(ltrim(C.UFUCODIGO )),' ',RTRIM(ltrim(C.UFUDESCRI)))+'</PL.1>
</PV1.3>
<!-- ID DE VISITA -->
<PV1.19> 
<CX.1>'+RTRIM(ltrim(E.NUMINGRES))+'</CX.1> 
</PV1.19>
</PV1>
</OML_O21.PATIENT_VISIT> 
<!-- DATOS DE LA COBERTURA--> 
<OML_O21.INSURANCE>
<IN1>
<IN1.1>1</IN1.1>
<IN1.2>
<CE.1>'+RTRIM(ltrim(ENT.CODENTIDA))+'</CE.1>
<CE.2>'+RTRIM(ltrim(ENT.NOMENTIDA))+'</CE.2>
</IN1.2>
<!-- DE SER UN FINANCIADOR INCLUIR EL CUIT -->
<IN1.3>
<CX.1>0</CX.1>
</IN1.3>
<!-- NUMERO DE COBERTURA -->
<IN1.49>
<CX.1>'+RTRIM(ltrim(ENT.CODENTIDA))+'</CX.1>
</IN1.49>
</IN1>
</OML_O21.INSURANCE>
</OML_O21.PATIENT>
<!-- DATOS DE LA ORDEN ITEM # 1 -->
<OML_O21.ORDER>
<ORC>
<ORC.1>NW</ORC.1>
<!-- NUMERO DEL PEDIDO/RENGLON -->
<ORC.2>
<EI.1>'+ concat(REPLACE(DB_NAME(),'INDIGO','') , cast(A.AUTO as varchar(20)))  +'</EI.1>
<EI.2>vie His</EI.2>
</ORC.2>
<!-- NUMERO DEL PEDIDO -->
<ORC.4>
<EI.1>'+ concat(REPLACE(DB_NAME(),'INDIGO','') , cast(A.AUTO as varchar(20)))  +'</EI.1>
<EI.2>vie his</EI.2>
</ORC.4>
<!-- ESTADO DE LA ORDEN -2 muestra recolectada -->
<ORC.5>2</ORC.5>
<!-- FECHA DEL PEDIDO -->
<ORC.9>
<TS.1>'+ format(A.FECORDMED,'yyyyMMddhhmmss') +'</TS.1>
</ORC.9>
<!-- MEDICO SOLICITANTE -->
<ORC.12>
<!-- MATRICULA -->
<XCN.1>'+RTRIM(ltrim(H.CODPROSAL))+'</XCN.1>
<XCN.2>
<FN.1>'+RTRIM(ltrim(H.MEDPRINOM))+'</FN.1>
<FN.2>'+RTRIM(ltrim(H.MEDPRIAPEL))+'</FN.2>
</XCN.2>
<XCN.7>DR</XCN.7>
<XCN.10>MN</XCN.10>
</ORC.12>
</ORC>
<OML_O21.TIMING>
<TQ1>
<!-- CANTIDAD -->
<TQ1.2>
<CQ.1>'+CAST(A.CANSERIPS as varchar)+'</CQ.1>
</TQ1.2>
<!-- FECHA DE REALIZACION -->
<TQ1.7>
<TS.1>'+ format(A.FECORDMED,'yyyyMMddhhmmss') +'</TS.1>
</TQ1.7>
<!-- PRIORIDAD -->
<TQ1.9>
<CWE.1>U</CWE.1>
<CWE.2>Urgencia</CWE.2>
</TQ1.9>
</TQ1>
</OML_O21.TIMING> 
<!-- DETALLE DE LO SOLICITADO -->
<OML_O21.OBSERVATION_REQUEST>
<OBR>
<OBR.1>1</OBR.1>
<!-- NUMERO PEDIDO ‐ ITEM -->
<OBR.2>
<EI.1>'+ concat(REPLACE(DB_NAME(),'INDIGO','') , cast(A.AUTO as varchar(20)))  +'</EI.1>
<EI.2>Vie His</EI.2>
</OBR.2>
<!-- NUMERO PROTOCOLO ASIGNADO POR EL LIS -->
<OBR.3>
<EI.1></EI.1>
<EI.2></EI.2>
</OBR.3>
<!-- pruebas -->
<OBR.4>
<CE.1>'+RTRIM(ltrim(D.CODSERIPS))+'</CE.1>
<CE.2>'+RTRIM(ltrim(D.DESSERIPS))+'</CE.2>
</OBR.4>
</OBR>
<DG1>
<DG1.1>1</DG1.1>
<!-- PROBLEMA / DIAGNOSTICO -->
<DG1.3>
<CE.1>'+(select top 1 RTRIM(ltrim(dd.CODDIAGNO)) from INDIAGNOH hd inner join INDIAGNOS dd on hd.IPCODPACI = A.IPCODPACI order by hd.FECDIAGNO desc)+'</CE.1>
<CE.2>'+(select top 1 RTRIM(ltrim(dd.NOMDIAGNO)) from INDIAGNOH hd inner join INDIAGNOS dd on hd.IPCODPACI = A.IPCODPACI order by hd.FECDIAGNO desc)+'</CE.2>
</DG1.3>
<!-- TIPO DIAGNOSTICO -->
<DG1.6>W</DG1.6>
</DG1>
</OML_O21.OBSERVATION_REQUEST>
</OML_O21.ORDER>  
</OML_O21>' as MessageXML
	FROM  
		dbo.AMBORDLAB AS A  with(nolock) 
		INNER JOIN dbo.INPACIENT AS B  with(nolock) ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN dbo.INCUPSIPS AS D  with(nolock) ON A.CODSERIPS = D .CODSERIPS 
		INNER JOIN dbo.ADINGRESO AS E  with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
		INNER JOIN dbo.INENTIDAD AS ENT  with(nolock) ON ENT.CODENTIDA = E.CODENTIDA 
		INNER JOIN dbo.INUNIFUNC AS C  with(nolock) ON E.UFUACTPAC = C.UFUCODIGO 
		INNER JOIN dbo.INPROFSAL AS H  with(nolock) ON A.CODPROSAL =H.CODPROSAL  
        INNER JOIN INUBICACI UBI  with(nolock) ON B.AUUBICACI = UBI.AUUBICACI 
		INNER JOIN INMUNICIP MUN  with(nolock) ON UBI.DEPMUNCOD= MUN.DEPMUNCOD 
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
		INNER JOIN ADCENATEN CA ON CA.CODCENATE = A.CODCENATE 
	WHERE 
		A.ESTSERIPS=2 		
	--ORDER BY A.FECORDMED DESC

/*
SELECT  
	  [AUTO] as ID,
	  A.FECORDMED AS FechaSolicitud, 
	  RTRIM(A.IPCODPACI) AS Identificacion, 
      RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
	  RTRIM(CA.CODCENATE) AS CodigoCentroAtencion,
	  RTRIM(CA.NOMCENATE) AS NombreCentroAtencion,
	  B.IPFECNACI AS FechaNacimiento, 
      E.UFUACTPAC AS CodigoUnidad,
	  RTRIM(C.UFUDESCRI) AS DescripcionUnidad,
	  A.ESTSERIPS AS Estado,
	  A.NUMINGRES AS Ingreso, 
      A.NUMEFOLIO AS Folio,
	  A.CODSERIPS AS CodigoServicio,
	  RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio,
	  A.OBSSERIPS AS ObservacionServicio, 
	  A.CODDIAGNO AS CodigoDiagnostico,
	  RTRIM(I.NOMDIAGNO) as Diagnostico, 
	  RTRIM(G.DESCCAMAS) AS Cama, 
	  b.IPDIRECCI AS Direccion,
	  b.IPTELEFON AS Telefono,
	  case b.IPSEXOPAC   when 1 then 'Masculino' else 'Femenino' END as Sexo,
	  b.IPRHSANGR as Rh, 
	  b.IPGRUPSAN as GrupoSanguineo,
	  B.CORELEPAC as Correo,
      b.IPTELMOVI as Movil,
	  A.CODPROSAL as CodigoMedico,
	  H.NOMMEDICO  AS NombreMedico, 
	  B.IPPRINOMB as PrimerNombre,
	  B.IPSEGNOMB as SegundoNombre,
	  B.IPPRIAPEL as PrimerApellido,
	  B.IPSEGAPEL as SegundoApellido,
	  RTRIM(MUN.DEPMUNCOD) as MunicipioCodigo,
	  RTRIM(MUN.MUNNOMBRE) as MunicipioNombre,
	  RTRIM(ENT.CODENTIDA) as EntidadPagadoraCodigo,
	  RTRIM(ENT.NOMENTIDA) as EntidadPagadoraNombre,
	  (SELECT TOP 1 CODDIAGNO FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 ORDER BY CODDIAGNO asc) as CodigoDiagnosticoSegundo,
	  (SELECT TOP 1 DESCR.NOMDIAGNO FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 and DESCR.CODDIAGNO = COD.CODDIAGNO ORDER BY COD.CODDIAGNO asc) as NombreDiagnosticoSegundo,
	 CASE WHEN ((SELECT COUNT(CODDIAGNO) FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN 
			(SELECT TOP 1 CODDIAGNO FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 ORDER BY CODDIAGNO desc) END as CodigoDiagnosticoTercero,
	  CASE WHEN ((SELECT COUNT(CODDIAGNO) FROM DBO.INDIAGNOH WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0) > 2) THEN 
			(SELECT TOP 1 DESCR.NOMDIAGNO FROM DBO.INDIAGNOH COD, DBO.INDIAGNOS DESCR WHERE IPCODPACI = b.IPCODPACI and NUMEFOLIO = a.NUMEFOLIO AND NUMINGRES = e.NUMINGRES AND CODDIAPRI = 0 and DESCR.CODDIAGNO = COD.CODDIAGNO ORDER BY COD.CODDIAGNO DESC) END as NombreDiagnosticoTercero,
	  CASE WHEN A.PRISERIPS = '1' THEN 'Urgente' else 'Rutinario' END AS Prioridad,	  
	  'Hospitalaria' as TipoOrden,
	'<?xml version="1.0" encoding="UTF-8"?>
<OML_O21>
	<!-- EJEMPLO DE MENSAJE PARA ORDENES OML^021 -->
	<MSH>
		<MSH.1>|</MSH.1>
		<MSH.2>^~\&amp;</MSH.2>
		<!-- MENSAJE ENVIADO POR EL BI AL LIS -->
		<MSH.3>
			<HD.1>CABA</HD.1>
		</MSH.3>
		<MSH.4>
			<HD.1>CESAC 5</HD.1>
		</MSH.4>
		<!-- PARA EL LIS -->
		<MSH.5>
			<HD.1>LIS</HD.1>
		</MSH.5>
		<MSH.6>
			<HD.1>GRIERSON</HD.1>
		</MSH.6>
		<!-- FECHA DEL MENSAJE -->
		<MSH.7>
			<TS.1>20161209073000</TS.1>
		</MSH.7>
		<!-- TIPO DE MENSAJE : ORDEN DE LAB -->
		<MSH.9>
			<MSG.1>OML</MSG.1>
			<MSG.2>O21</MSG.2>
			<MSG.3>OML_O21</MSG.3>
		</MSH.9>
		<!-- IDENTIFICADOR DEL MENSAJE -->
		<MSH.10>BI4102332</MSH.10>
		<!-- MODO: PRODUCCION -->
		<MSH.11>
			<PT.1>P</PT.1>
		</MSH.11>
		<!-- VERSION DEL ESTANDAR -->
		<MSH.12>
			<VID.1>2.5</VID.1>
		</MSH.12>
		<!-- RESPONDER: SIEMPRE -->
		<MSH.15>AL</MSH.15>
		<MSH.16>NE</MSH.16>
	</MSH>
	<SFT>
		<SFT.1>
			<XON.1>BUSINTER</XON.1>
		</SFT.1>
		<SFT.2>BIplatinum</SFT.2>
		<SFT.3>BISolution</SFT.3>
		<SFT.4>version1.0</SFT.4>
	</SFT>
	<OML_O21.PATIENT>
		<PID>
			<PID.1>1</PID.1>
			<!-- IDENTIFICADOR DEL PACIENTE -->
			<PID.3>
				<CX.1>2222</CX.1>
				<CX.4>
					<HD.1>CABA</HD.1>
				</CX.4>
			</PID.3>
			<PID.3>
				<CX.1>20202530</CX.1>
				<CX.4>
					<HD.1>DNI</HD.1>
				</CX.4>
			</PID.3>
			<!-- APELLIDO -->
			<PID.5>
				<XPN.1>
					<!-- APELLIDO -->
					<FN.1>GARCIA</FN.1>
				</XPN.1>
				<!-- NOMBRES -->
				<XPN.2>JUAN</XPN.2>
			</PID.5>
			<!-- FECHA DE NACIMIENTO -->
			<PID.7>
				<TS.1>19680511000000</TS.1>
			</PID.7>
			<!-- SEXO -->
			<PID.8>M</PID.8>
		</PID>
		<!-- TIPO DE VISITA -->
		<OML_O21.PATIENT_VISIT>
			<PV1>
				<PV1.1>1</PV1.1>
				<!-- AMBULATORIO: O -->
				<PV1.2>O</PV1.2>
				<!-- UBICACION DEL PACIENTE -->
				<PV1.3>
					<PL.1>CESAC 5</PL.1>
				</PV1.3>
				<!-- ID DE VISITA -->
				<PV1.19>
					<CX.1>HTR56780</CX.1>
				</PV1.19>
			</PV1>
		</OML_O21.PATIENT_VISIT>
		<!-- DATOS DE LA COBERTURA-->
		<OML_O21.INSURANCE>
			<IN1>
				<IN1.1>1</IN1.1>
				<IN1.2>
					<CE.1>CB</CE.1>
					<CE.2>COBERTURA PORTEÑA</CE.2>
				</IN1.2>
				<!-- DE SER UN FINANCIADOR INCLUIR EL CUIT -->
				<IN1.3>
					<CX.1>0</CX.1>
				</IN1.3>
				<!-- NUMERO DE COBERTURA -->
				<IN1.49>
					<CX.1>20202530</CX.1>
				</IN1.49>
			</IN1>
		</OML_O21.INSURANCE>
	</OML_O21.PATIENT>
	<!-- DATOS DE LA ORDEN ITEM # 1 -->
	<OML_O21.ORDER>
		<ORC>
			<ORC.1>NW</ORC.1>
			<!-- NUMERO DEL PEDIDO/RENGLON -->
			<ORC.2>
				<EI.1>BI123456-1</EI.1>
				<EI.2>CABA</EI.2>
			</ORC.2>
			<!-- NUMERO DEL PEDIDO -->
			<ORC.4>
				<EI.1>BI123456</EI.1>
				<EI.2>CABA</EI.2>
			</ORC.4>
			<!-- ESTADO DE LA ORDEN -->
			<ORC.5></ORC.5>
			<!-- FECHA DEL PEDIDO -->
			<ORC.9>
				<TS.1>20161202100913</TS.1>
			</ORC.9>
			<!-- MEDICO SOLICITANTE -->
			<ORC.12>
				<!-- MATRICULA -->
				<XCN.1>12345</XCN.1>
				<XCN.2>
					<FN.1>FERNANDEZ</FN.1>
					<FN.2>JUAN</FN.2>
				</XCN.2>
				<XCN.7>DR</XCN.7>
				<XCN.10>MN</XCN.10>
			</ORC.12>
		</ORC>
		<OML_O21.TIMING>
			<TQ1>
				<!-- CANTIDAD -->
				<TQ1.2>
					<CQ.1>1</CQ.1>
				</TQ1.2>
				<!-- FECHA DE REALIZACION -->
				<TQ1.7>
					<TS.1>20161211000000</TS.1>
				</TQ1.7>
				<!-- PRIORIDAD -->
				<TQ1.9>
					<CWE.1>R</CWE.1>
					<CWE.2>RUTINA</CWE.2>
				</TQ1.9>
			</TQ1>
		</OML_O21.TIMING>
		<!-- DETALLE DE LO SOLICITADO -->
		<OML_O21.OBSERVATION_REQUEST>
			<OBR>
				<OBR.1>1</OBR.1>
				<!-- NUMERO PEDIDO - ITEM -->
				<OBR.2>
					<EI.1>BI123456-1</EI.1>
					<EI.2>CABA</EI.2>
				</OBR.2>
				<!-- NUMERO PROTOCOLO ASIGNADO POR EL LIS -->
				<OBR.3>
					<EI.1></EI.1>
					<EI.2></EI.2>
				</OBR.3>
				<!-- DETERMINACION SOLICITADA CODIFICADA CON TERMINOLOGIA DE REFERENCIA -->
				<OBR.4>
					<CE.1>13301951000999114</CE.1>
					<CE.2>COLESTEROL TOTAL</CE.2>
				</OBR.4>
			</OBR>
			<DG1>
				<DG1.1>1</DG1.1>
				<!-- PROBLEMA / DIAGNOSTICO -->
				<DG1.3>
					<CE.1>32541000999110</CE.1>
					<CE.2>Hipercolesterolemias</CE.2>
				</DG1.3>
				<!-- TIPO DIAGNOSTICO -->
				<DG1.6>W</DG1.6>
			</DG1>
		</OML_O21.OBSERVATION_REQUEST>
	</OML_O21.ORDER>
</OML_O21>' as MessageXML
      FROM  dbo.HCORDLABO AS A with(nolock)  INNER JOIN
      dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
      dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
      dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
	  dbo.INENTIDAD AS ENT with(nolock)  ON ENT.CODENTIDA = E.CODENTIDA INNER JOIN
      dbo.INUNIFUNC AS C with(nolock)  ON A.UFUCODIGO  = C.UFUCODIGO INNER JOIN
      dbo.CHCAMASHO AS G with(nolock)  ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
      dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
      dbo.INDIAGNOS AS I with(nolock)  ON A.CODDIAGNO = I.CODDIAGNO LEFT OUTER JOIN
      dbo.HCPARALELAB AS J with(nolock)  ON A.CODSERIPS = J.CODSERIPS AND J.CODCENATE=A.CODCENATE
	  INNER JOIN INUBICACI UBI with(nolock)  ON B.AUUBICACI = UBI.AUUBICACI
	  INNER JOIN INMUNICIP MUN with(nolock)  ON UBI.DEPMUNCOD= MUN.DEPMUNCOD
	  INNER JOIN ADCENATEN CA with(nolock) ON CA.CODCENATE = A.CODCENATE
	  LEFT JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA
	  LEFT JOIN contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
	  WHERE A.ESTSERIPS = 2 -- ORDER BY A.FECORDMED DESC

	  union 

	  SELECT  
		[AUTO] as ID, 
		A.FECORDMED AS FechaSolicitud, 
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente,
		RTRIM(CA.CODCENATE) AS CodigoCentroAtencion,
		RTRIM(CA.NOMCENATE) AS NombreCentroAtencion,
		B.IPFECNACI AS FechaNacimiento, 
		E.UFUACTPAC AS CodigoUnidad, 
		RTRIM(C.UFUDESCRI)  AS DescripcionUnidad, 
		A.ESTSERIPS AS Estado, 		
		A.NUMINGRES AS Ingreso, 
		NULL as Folio,
		RTRIM(A.CODSERIPS) as CodigoServicio,
		RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, 
		A.OBSERVACI AS ObservacionServicio,
		'' AS CodigoDiagnostico,
		'' as Diagnostico,
		'' AS Cama,
		b.IPDIRECCI AS Direccion, 
		b.IPTELEFON AS Telefono,
		case b.IPSEXOPAC   when 1 then 'Masculino' else 'Femenino' END as Sexo,
		b.IPRHSANGR as Rh, 
		b.IPGRUPSAN as GrupoSanguineo, 
		b.CORELEPAC as Correo, 
		b.IPTELMOVI as Movil, 
		A.CODPROSAL as CodigoMedico,
		H.NOMMEDICO  AS NombreMedico, 
		B.IPPRINOMB as PrimerNombre,
		B.IPSEGNOMB as SegundoNombre,
		B.IPPRIAPEL as PrimerApellido,
		B.IPSEGAPEL as SegundoApellido,
		RTRIM(MUN.DEPMUNCOD) as MunicipioCodigo,
		RTRIM(MUN.MUNNOMBRE) as MunicipioNombre,
		RTRIM(ENT.CODENTIDA) as EntidadPagadoraCodigo,
		RTRIM(ENT.NOMENTIDA) as EntidadPagadoraNombre,
		'' as CodigoDiagnosticoSegundo, 
		'' as NombreDiagnosticoSegundo, 
		'' CodigoDiagnosticoTercero, 
		'' as NombreDiagnosticoTercero, 
		'Rutinario' Prioridad, 
		'Ambulatoria' as TipoOrden,
		'<?xml version="1.0" encoding="UTF-8"?>
<OML_O21>
	<!-- EJEMPLO DE MENSAJE PARA ORDENES OML^021 -->
	<MSH>
		<MSH.1>|</MSH.1>
		<MSH.2>^~\&amp;</MSH.2>
		<!-- MENSAJE ENVIADO POR EL BI AL LIS -->
		<MSH.3>
			<HD.1>CABA</HD.1>
		</MSH.3>
		<MSH.4>
			<HD.1>CESAC 5</HD.1>
		</MSH.4>
		<!-- PARA EL LIS -->
		<MSH.5>
			<HD.1>LIS</HD.1>
		</MSH.5>
		<MSH.6>
			<HD.1>GRIERSON</HD.1>
		</MSH.6>
		<!-- FECHA DEL MENSAJE -->
		<MSH.7>
			<TS.1>20161209073000</TS.1>
		</MSH.7>
		<!-- TIPO DE MENSAJE : ORDEN DE LAB -->
		<MSH.9>
			<MSG.1>OML</MSG.1>
			<MSG.2>O21</MSG.2>
			<MSG.3>OML_O21</MSG.3>
		</MSH.9>
		<!-- IDENTIFICADOR DEL MENSAJE -->
		<MSH.10>BI4102332</MSH.10>
		<!-- MODO: PRODUCCION -->
		<MSH.11>
			<PT.1>P</PT.1>
		</MSH.11>
		<!-- VERSION DEL ESTANDAR -->
		<MSH.12>
			<VID.1>2.5</VID.1>
		</MSH.12>
		<!-- RESPONDER: SIEMPRE -->
		<MSH.15>AL</MSH.15>
		<MSH.16>NE</MSH.16>
	</MSH>
	<SFT>
		<SFT.1>
			<XON.1>BUSINTER</XON.1>
		</SFT.1>
		<SFT.2>BIplatinum</SFT.2>
		<SFT.3>BISolution</SFT.3>
		<SFT.4>version1.0</SFT.4>
	</SFT>
	<OML_O21.PATIENT>
		<PID>
			<PID.1>1</PID.1>
			<!-- IDENTIFICADOR DEL PACIENTE -->
			<PID.3>
				<CX.1>2222</CX.1>
				<CX.4>
					<HD.1>CABA</HD.1>
				</CX.4>
			</PID.3>
			<PID.3>
				<CX.1>20202530</CX.1>
				<CX.4>
					<HD.1>DNI</HD.1>
				</CX.4>
			</PID.3>
			<!-- APELLIDO -->
			<PID.5>
				<XPN.1>
					<!-- APELLIDO -->
					<FN.1>GARCIA</FN.1>
				</XPN.1>
				<!-- NOMBRES -->
				<XPN.2>JUAN</XPN.2>
			</PID.5>
			<!-- FECHA DE NACIMIENTO -->
			<PID.7>
				<TS.1>19680511000000</TS.1>
			</PID.7>
			<!-- SEXO -->
			<PID.8>M</PID.8>
		</PID>
		<!-- TIPO DE VISITA -->
		<OML_O21.PATIENT_VISIT>
			<PV1>
				<PV1.1>1</PV1.1>
				<!-- AMBULATORIO: O -->
				<PV1.2>O</PV1.2>
				<!-- UBICACION DEL PACIENTE -->
				<PV1.3>
					<PL.1>CESAC 5</PL.1>
				</PV1.3>
				<!-- ID DE VISITA -->
				<PV1.19>
					<CX.1>HTR56780</CX.1>
				</PV1.19>
			</PV1>
		</OML_O21.PATIENT_VISIT>
		<!-- DATOS DE LA COBERTURA-->
		<OML_O21.INSURANCE>
			<IN1>
				<IN1.1>1</IN1.1>
				<IN1.2>
					<CE.1>CB</CE.1>
					<CE.2>COBERTURA PORTEÑA</CE.2>
				</IN1.2>
				<!-- DE SER UN FINANCIADOR INCLUIR EL CUIT -->
				<IN1.3>
					<CX.1>0</CX.1>
				</IN1.3>
				<!-- NUMERO DE COBERTURA -->
				<IN1.49>
					<CX.1>20202530</CX.1>
				</IN1.49>
			</IN1>
		</OML_O21.INSURANCE>
	</OML_O21.PATIENT>
	<!-- DATOS DE LA ORDEN ITEM # 1 -->
	<OML_O21.ORDER>
		<ORC>
			<ORC.1>NW</ORC.1>
			<!-- NUMERO DEL PEDIDO/RENGLON -->
			<ORC.2>
				<EI.1>BI123456-1</EI.1>
				<EI.2>CABA</EI.2>
			</ORC.2>
			<!-- NUMERO DEL PEDIDO -->
			<ORC.4>
				<EI.1>BI123456</EI.1>
				<EI.2>CABA</EI.2>
			</ORC.4>
			<!-- ESTADO DE LA ORDEN -->
			<ORC.5></ORC.5>
			<!-- FECHA DEL PEDIDO -->
			<ORC.9>
				<TS.1>20161202100913</TS.1>
			</ORC.9>
			<!-- MEDICO SOLICITANTE -->
			<ORC.12>
				<!-- MATRICULA -->
				<XCN.1>12345</XCN.1>
				<XCN.2>
					<FN.1>FERNANDEZ</FN.1>
					<FN.2>JUAN</FN.2>
				</XCN.2>
				<XCN.7>DR</XCN.7>
				<XCN.10>MN</XCN.10>
			</ORC.12>
		</ORC>
		<OML_O21.TIMING>
			<TQ1>
				<!-- CANTIDAD -->
				<TQ1.2>
					<CQ.1>1</CQ.1>
				</TQ1.2>
				<!-- FECHA DE REALIZACION -->
				<TQ1.7>
					<TS.1>20161211000000</TS.1>
				</TQ1.7>
				<!-- PRIORIDAD -->
				<TQ1.9>
					<CWE.1>R</CWE.1>
					<CWE.2>RUTINA</CWE.2>
				</TQ1.9>
			</TQ1>
		</OML_O21.TIMING>
		<!-- DETALLE DE LO SOLICITADO -->
		<OML_O21.OBSERVATION_REQUEST>
			<OBR>
				<OBR.1>1</OBR.1>
				<!-- NUMERO PEDIDO - ITEM -->
				<OBR.2>
					<EI.1>BI123456-1</EI.1>
					<EI.2>CABA</EI.2>
				</OBR.2>
				<!-- NUMERO PROTOCOLO ASIGNADO POR EL LIS -->
				<OBR.3>
					<EI.1></EI.1>
					<EI.2></EI.2>
				</OBR.3>
				<!-- DETERMINACION SOLICITADA CODIFICADA CON TERMINOLOGIA DE REFERENCIA -->
				<OBR.4>
					<CE.1>13301951000999114</CE.1>
					<CE.2>COLESTEROL TOTAL</CE.2>
				</OBR.4>
			</OBR>
			<DG1>
				<DG1.1>1</DG1.1>
				<!-- PROBLEMA / DIAGNOSTICO -->
				<DG1.3>
					<CE.1>32541000999110</CE.1>
					<CE.2>Hipercolesterolemias</CE.2>
				</DG1.3>
				<!-- TIPO DIAGNOSTICO -->
				<DG1.6>W</DG1.6>
			</DG1>
		</OML_O21.OBSERVATION_REQUEST>
	</OML_O21.ORDER>
</OML_O21>' as MessageXML
	FROM  
		dbo.AMBORDLAB AS A  with(nolock) 
		INNER JOIN dbo.INPACIENT AS B  with(nolock) ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN dbo.INCUPSIPS AS D  with(nolock) ON A.CODSERIPS = D .CODSERIPS 
		INNER JOIN dbo.ADINGRESO AS E  with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
		INNER JOIN dbo.INENTIDAD AS ENT  with(nolock) ON ENT.CODENTIDA = E.CODENTIDA 
		INNER JOIN dbo.INUNIFUNC AS C  with(nolock) ON E.UFUACTPAC = C.UFUCODIGO 
		INNER JOIN dbo.INPROFSAL AS H  with(nolock) ON A.CODPROSAL =H.CODPROSAL  
        INNER JOIN INUBICACI UBI  with(nolock) ON B.AUUBICACI = UBI.AUUBICACI 
		INNER JOIN INMUNICIP MUN  with(nolock) ON UBI.DEPMUNCOD= MUN.DEPMUNCOD 
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
		INNER JOIN ADCENATEN CA ON CA.CODCENATE = A.CODCENATE 
	WHERE 
		A.ESTSERIPS=2 		
	--ORDER BY A.FECORDMED DESC
	*/
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de sincronización de órdenes de laboratorio hospitalarias hacia el sistema LIS (Modulab) a través del motor de integración Mirth Connect. Consolida en una sola fila toda la información necesaria para cada examen de laboratorio solicitado: datos del paciente (cédula, documento, nombre, fecha de nacimiento, sexo, grupo sanguíneo, contacto, municipio), datos del ingreso o admisión hospitalaria (número de ingreso, folio, unidad funcional, cama), datos de la orden médica (fecha de solicitud, código y descripción del servicio CUPS, estado, prioridad urgente o rutinaria, observaciones, médico solicitante), diagnósticos principal y secundarios (CIE-10), y entidad pagadora o EPS. Adicionalmente genera automáticamente el mensaje HL7 v2.5 en formato XML (OML_O21) listo para ser consumido por Mirth, incluyendo segmentos MSH, PID, PV1, IN1 y ORC/OBR con los datos del paciente y la orden. Sirve como fuente de integración en tiempo real entre el HIS Indigo Vie y el laboratorio clínico externo, evitando la re-digitación de órdenes y garantizando la trazabilidad del examen desde la solicitud médica hasta el LIS.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone órdenes de laboratorio (hospitalarias y ambulatorias) en estado ''muestra recolectada'' enriquecidas con datos de paciente, ingreso, entidad pagadora, médico y diagnóstico, junto con un mensaje HL7 OML^O21 listo para enviar al LIS vía Mirth.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de laboratorio debe tener ESTSERIPS = 2 (estado ''muestra recolectada'') para ser publicada; El paciente debe existir en INPACIENT y tener ubicación (AUUBICACI) ligada a un municipio; El ingreso (ADINGRESO) debe existir y tener entidad pagadora válida en INENTIDAD; Para órdenes hospitalarias debe existir cama actual (CODCAMACT) en CHCAMASHO y unidad funcional asociada a la orden; El profesional solicitante (CODPROSAL) debe existir en INPROFSAL; El servicio/CUPS solicitado debe existir en INCUPSIPS; El centro de atención (CODCENATE) debe existir en ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se publican órdenes con estado 2 (muestra recolectada); cualquier otro estado queda excluido; El MessageXML siempre se construye como HL7 v2.5, mensaje OML^O21, modo Producción (PT.1=P), tipo de visita ambulatoria (PV1.2=''A'') y estado de orden ORC.5=2; El identificador del mensaje HL7 (MSH.10) y los EI del pedido se forman como concat(REPLACE(DB_NAME(),''INDIGO'',''''), AUTO), garantizando trazabilidad al ID interno; La cobertura/aseguradora se reporta tanto en IN1.2 como en IN1.49 usando el código de la entidad pagadora del ingreso; El diagnóstico secundario sólo considera registros con CODDIAPRI = 0 (no principales) en INDIAGNOH; Los nombres y códigos se entregan con RTRIM/LTRIM para evitar espacios en el mensaje HL7', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Paciente; Ingreso/Admisión; Entidad pagadora (aseguradora); Centro de atención; Unidad funcional; Cama hospitalaria; Profesional de la salud / Médico solicitante; Diagnóstico (CIE); Diagnóstico principal vs secundarios; CUPS / Servicio; Tipo de documento; Prioridad (Urgente/Rutinario); Tipo de orden (Hospitalaria/Ambulatoria); Mensajería HL7 OML^O21; Integración con LIS (Modulab) vía Mirth', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset HL7 OML^O21: Cuando A.ESTSERIPS = 2 sobre HCORDLABO, se emite una fila TipoOrden=''Hospitalaria'' con MessageXML OML^O21 incluyendo cama, folio y diagnóstico principal de la orden; [RETURN_RESULT] Resultset HL7 OML^O21: Cuando A.ESTSERIPS = 2 sobre AMBORDLAB, se emite una fila TipoOrden=''Ambulatoria'' con MessageXML OML^O21, Folio NULL, sin cama y con diagnóstico tomado del último registro de INDIAGNOH del paciente por FECDIAGNO descendente', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de la orden: HCORDLABO (hospitalaria) vs AMBORDLAB (ambulatoria) unidos vía UNION → Hospitalaria: incluye Folio, Cama, diagnóstico de la orden y diagnósticos secundario/terciario derivados de INDIAGNOH; prioridad según PRISERIPS else Ambulatoria: Folio NULL, sin Cama ni diagnósticos secundarios; prioridad fija ''Rutinario'' y diagnóstico tomado del último de INDIAGNOH del paciente; si A.PRISERIPS = ''1'' (sólo flujo hospitalario) → Prioridad = ''Urgente'' else Prioridad = ''Rutinario''; si B.IPSEXOPAC = 1 → Sexo = ''Masculino'' y PID.8 del HL7 = ''M'' else Sexo = ''Femenino'' y PID.8 = ''F''; si Conteo de INDIAGNOH del paciente/folio/ingreso con CODDIAPRI = 0 > 2 → Se completan CodigoDiagnosticoTercero y NombreDiagnosticoTercero con el CODDIAGNO mayor (ORDER BY DESC) else Tercer diagnóstico queda NULL; si Mapeo del tipo de documento por B.IPTIPODOC (1..12) → Se traduce a etiquetas como ''Cédula de Ciudadanía'', ''Pasaporte'', ''Registro Civil'', etc.; valores fuera del set producen NULL', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.AMBORDLAB; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INDIAGNOH; dbo.HCPARALELAB; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADCENATEN; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryOrders_Mirth_Synch2_pruebasPROMED_CR';
GO
