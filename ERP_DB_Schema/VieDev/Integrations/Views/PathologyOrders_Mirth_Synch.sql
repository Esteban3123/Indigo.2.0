
CREATE view [Integrations].[PathologyOrders_Mirth_Synch]
as

      SELECT  
	  A.[AUTO] as ID,
	  INTER.CODCONCEC as NumeroOrdenIndigo, 
	  A.FECORDMED AS FechaSolicitud, 
	  B.IPTIPODOC as CodigoTipoDocumento,
	  A.CODSERIPS AS CodigoServicio,
	  RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio,
	  case B.IPTIPODOC when 1 then 'Cédula de Ciudadanía' when 2 then 'Cédula de Extranjería'
	   when 3 then 'Tarjeta de Identidad'  when 4 then 'Registro Civil'  when 5 then 'Pasaporte'  when 6 then 'Adulto Sin Identificación'  when 7 then 'Menor Sin Identificación' 
	   when 8 then 'Número único de identificación personal'  when 9 then 'Certificado Nacido Vivo' when 10 then 'Carnet Diplomático (Aplica para extranjeros)' 
	   when 12 then 'Salvoconducto (Aplica para extranjeros)' when 12 then 'Permiso especial de Permanencia (Aplica para extranjeros)'  END TipoDocumento,
	  RTRIM(A.IPCODPACI) AS Identificacion, 
      RTRIM(B.IPNOMCOMP) AS NombrePaciente, 
	  RTRIM(CA.CODCENATE) AS CodigoCentroAtencion,
	  ISNULL(case B.IPTIPOPAC
				when 0 then 'Contributivo' 
				when 1 then  'Subsidiado'
				when 2 then 'Vinculado'
				when 3 then 'Particular'
				when 5 then 'Desplazado Reg. Contributivo'
				when 6 then 'Desplazado Reg. Subsidiado'
				when 7 then 'Desplazado no Asegurado' END,'No Aplica') TipoPaciente
				,
	case B.IPTIPOAFI when 0 then 'No Aplica' 
				when 1 then  'Cotizante'
				when 2 then 'Beneficiario'
				when 3 then 'Adicional'
				when 4 then 'Jub/Retirado'
				when 5 then 'Pensionado' END TipoAfiliacion,
	  RTRIM(ltrim(CA.CODCENATE)) +' - '+ RTRIM(ltrim(CA.NOMCENATE)) AS NombreCentroAtencion,
	  B.IPFECNACI AS FechaNacimiento, 
      U.UFUCODIGO AS CodigoUnidad,
	  RTRIM(ltrim(U.UFUCODIGO)) +' - '+ RTRIM(ltrim(U.UFUDESCRI)) as UnidadFuncional,
	  A.ESTSERIPS AS Estado,
	  A.NUMINGRES AS Ingreso, 
      A.NUMEFOLIO AS Folio,
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
	  'Cedula' as TipoIdentificacionMedico,
	  H.CODIGONIT as IdentificacionMedico,
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
	  e.ICAUSAING AS CodigoCausaIngreso,
	  case  e.ICAUSAING when 1 then 'Heridos en combate' when 2 then 'Enfermedad profesional ' when 3 then 'Enfermedad general adulto ' when 4 then ' Enfermedad general pediatria ' when 5 then 'Odontología ' when 6 then 'Accidente de transito' when 7 then 'Catastrofe/Fisalud ' when 8 then 'Quemados ' when 9 then 'Maternidad' when 10 then 'Accidente Laboral' when 11 then 'Cirugia Programada' END CausaIngreso,
	  --A.FECRECMUE as FechaRecoleccionMuestra,
	  --A.USURECMUE as CodigoUsuarioRecolectaMuestra,
	  --rtrim(ltrim(USU.CODUSUARI)) + ' - '+ rtrim(ltrim(USU.NOMUSUARI)) as UsuarioRecolectaMuestra,
	  ISNULL(B.IPESTRATO,0) as Estrato,
	  H.MEDPRINOM as MedicoPrimerNombre,
	  H.MEDSEGNOM as MedicoSegundoNombre,
	  H.MEDPRIAPEL as MedicoPrimerApellido,
	  H.MEDSEGAPEL as MedicoSegundoApellido,
	  rtrim(ltrim(ESP.CODESPECI)) as CodigoEspecialidad,
	  rtrim(ltrim(ESP.DESESPECI)) as Especialidad,
	  case B.IPSEXOPAC when 1 then 'No Aplica' else case  RP.GESTACION when 1 then 'Si' else 'No' END END as Gestacion,
	  A.CANSERIPS as Cantidad,
	  datediff(year,b.IPFECNACI ,common.GETDATE()) as Edad 
      FROM  dbo.HCORDPATO AS A with(nolock)  INNER JOIN 
	  dbo.INTERDETA AS INTER on INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'INT' INNER JOIN
      dbo.INPACIENT AS B with(nolock)  ON A.IPCODPACI = B.IPCODPACI INNER JOIN
      dbo.INCUPSIPS AS D with(nolock)  ON A.CODSERIPS = D .CODSERIPS INNER JOIN
      dbo.ADINGRESO AS E with(nolock)  ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES INNER JOIN
	  dbo.INENTIDAD AS ENT with(nolock)  ON ENT.CODENTIDA = E.CODENTIDA INNER JOIN
      dbo.INUNIFUNC AS U with(nolock)  ON A.UFUCODIGO  = U.UFUCODIGO left JOIN
      dbo.CHCAMASHO AS G with(nolock)  ON E.CODCAMACT = G.CODICAMAS INNER JOIN 
      dbo.INPROFSAL AS H with(nolock)  ON A.CODPROSAL =H.CODPROSAL  LEFT OUTER JOIN
      dbo.INDIAGNOS AS I with(nolock)  ON A.CODDIAGNO = I.CODDIAGNO INNER JOIN
	  dbo.HCHISPACA AS HIS with(nolock) ON HIS.NUMINGRES = A.NUMINGRES AND HIS.NUMEFOLIO = A.NUMEFOLIO INNER JOIN
	  dbo.INESPECIA AS ESP with(nolock) ON ESP.CODESPECI = HIS.CODESPTRA 
	  INNER JOIN INUBICACI UBI with(nolock)  ON B.AUUBICACI = UBI.AUUBICACI
	  INNER JOIN INMUNICIP MUN with(nolock)  ON UBI.DEPMUNCOD= MUN.DEPMUNCOD
	  INNER JOIN ADCENATEN CA with(nolock) ON CA.CODCENATE = A.CODCENATE
	  LEFT JOIN dbo.HCRIESGOSP AS RP with(nolock)  on RP.NUMINGRCES = A.NUMINGRES
	 -- LEFT JOIN SEGusuaru USU with(nolock) ON USU.CODUSUARI = A.USURECMUE 
	  LEFT JOIN contract.CUPSEntityContractDescriptions CDD with(nolock) on CDD.Id = A.IDDESCRIPCIONRELACIONADA
	  LEFT JOIN contract.ContractDescriptions  CD with(nolock) on CD.Id = CDD.ContractDescriptionId
	  WHERE A.ESTSERIPS = 2 -- ORDER BY A.FECORDMED DESC 
	  AND (SYNCMIRTH IS NULL OR SYNCMIRTH = 0 )

	  
	  UNION 

	  SELECT  
		A.[AUTO] as ID, 
		INTER.CODCONCEC as NumeroOrdenIndigo, 
		A.FECORDMED AS FechaSolicitud, 
	    B.IPTIPODOC as CodigoTipoDocumento,
		RTRIM(A.CODSERIPS) as CodigoServicio,
		RTRIM(D .DESSERIPS) + '. ' + isnull(CD.name,'') AS DescripcionServicio, 
	   case B.IPTIPODOC when 1 then 'Cédula de Ciudadanía' when 2 then 'Cédula de Extranjería'
	   when 3 then 'Tarjeta de Identidad'  when 4 then 'Registro Civil'  when 5 then 'Pasaporte'  when 6 then 'Adulto Sin Identificación'  when 7 then 'Menor Sin Identificación' 
	   when 8 then 'Número único de identificación personal'  when 9 then 'Certificado Nacido Vivo' when 10 then 'Carnet Diplomático (Aplica para extranjeros)' 
	   when 12 then 'Salvoconducto (Aplica para extranjeros)' when 12 then 'Permiso especial de Permanencia (Aplica para extranjeros)'  END TipoDocumento,
		RTRIM(A.IPCODPACI) AS Identificacion, 
		RTRIM(B.IPNOMCOMP) AS NombrePaciente,
		RTRIM(CA.CODCENATE) AS CodigoCentroAtencion,
			  ISNULL(case B.IPTIPOPAC
				when 0 then 'Contributivo' 
				when 1 then  'Subsidiado'
				when 2 then 'Vinculado'
				when 3 then 'Particular'
				when 5 then 'Desplazado Reg. Contributivo'
				when 6 then 'Desplazado Reg. Subsidiado'
				when 7 then 'Desplazado no Asegurado' END,'No Aplica') TipoPaciente
				,
	case B.IPTIPOAFI when 0 then 'No Aplica' 
				when 1 then  'Cotizante'
				when 2 then 'Beneficiario'
				when 3 then 'Adicional'
				when 4 then 'Jub/Retirado'
				when 5 then 'Pensionado' END TipoAfiliacion,
		RTRIM(ltrim(CA.CODCENATE)) +' - '+ RTRIM(ltrim(CA.NOMCENATE)) AS NombreCentroAtencion,
		B.IPFECNACI AS FechaNacimiento, 
		E.UFUACTPAC AS CodigoUnidad, 
        RTRIM(ltrim(U.UFUCODIGO)) +' - '+ RTRIM(ltrim(U.UFUDESCRI)) as UnidadFuncional, 
		A.ESTSERIPS AS Estado, 		
		A.NUMINGRES AS Ingreso, 
		NULL as Folio,
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
		'Cedula' as TipoIdentificacionMedico,
	    H.CODIGONIT as IdentificacionMedico,
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
		e.ICAUSAING AS CodigoCausaIngreso,
		case  e.ICAUSAING when 1 then 'Heridos en combate' when 2 then 'Enfermedad profesional ' when 3 then 'Enfermedad general adulto ' when 4 then ' Enfermedad general pediatria ' when 5 then 'Odontología ' when 6 then 'Accidente de transito' when 7 then 'Catastrofe/Fisalud ' when 8 then 'Quemados ' when 9 then 'Maternidad' when 10 then 'Accidente Laboral' when 11 then 'Cirugia Programada' END CausaIngreso,
		--A.FECRECMUE as FechaRecoleccionMuestra,
		--A.USURECMUE as CodigoUsuarioRecolectaMuestra,
		--rtrim(ltrim(USU.CODUSUARI)) + ' - '+ rtrim(ltrim(USU.NOMUSUARI)) as UsuarioRecolectaMuestra,
	    ISNULL(B.IPESTRATO,0) as Estrato,
		H.MEDPRINOM as MedicoPrimerNombre,
		H.MEDSEGNOM as MedicoSegundoNombre,
		H.MEDPRIAPEL as MedicoPrimerApellido,
		H.MEDSEGAPEL as MedicoSegundoApellido,
		'' as CodigoEspecialidad,
	    '' as Especialidad,
		case B.IPSEXOPAC when 1 then 'No Aplica' else case  RP.GESTACION when 1 then 'Si' else 'No' END END as Gestacion,
		 A.CANSERIPS as Cantidad,
		 datediff(year,b.IPFECNACI ,common.GETDATE()) as Edad 
	FROM  
		dbo.AMBORDPAT  AS A  with(nolock) 
		INNER JOIN dbo.INTERDETA AS INTER on INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'AMB' 
		INNER JOIN dbo.INPACIENT AS B  with(nolock) ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN dbo.INCUPSIPS AS D  with(nolock) ON A.CODSERIPS = D .CODSERIPS 
		INNER JOIN dbo.ADINGRESO AS E  with(nolock) ON A.IPCODPACI = E.IPCODPACI AND A.NUMINGRES = E.NUMINGRES 
		INNER JOIN dbo.INENTIDAD AS ENT  with(nolock) ON ENT.CODENTIDA = E.CODENTIDA 
		INNER JOIN dbo.INUNIFUNC AS U  with(nolock) ON E.UFUACTPAC = U.UFUCODIGO 
		INNER JOIN dbo.INPROFSAL AS H  with(nolock) ON A.CODPROSAL =H.CODPROSAL  
        INNER JOIN INUBICACI UBI  with(nolock) ON B.AUUBICACI = UBI.AUUBICACI 
		INNER JOIN INMUNICIP MUN  with(nolock) ON UBI.DEPMUNCOD= MUN.DEPMUNCOD 
		LEFT JOIN contract.CUPSEntityContractDescriptions CDD on CDD.Id = A.IDDESCRIPCIONRELACIONADA 
		LEFT JOIN contract.ContractDescriptions  CD on CD.Id = CDD.ContractDescriptionId 
	--	 LEFT JOIN SEGusuaru USU with(nolock) ON USU.CODUSUARI = A.USURECMUE 
		 LEFT JOIN dbo.HCRIESGOSP AS RP with(nolock)  on RP.NUMINGRCES = A.NUMINGRES
		INNER JOIN ADCENATEN CA ON CA.CODCENATE = A.CODCENATE 
	WHERE 
		A.ESTSERIPS=2 	AND (SYNCMIRTH IS NULL OR SYNCMIRTH = 0 )