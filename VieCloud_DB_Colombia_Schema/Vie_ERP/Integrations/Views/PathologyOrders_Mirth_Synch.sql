
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de integración que expone las órdenes médicas de patología e imágenes diagnósticas destinadas a sincronizarse con el motor de integración Mirth Connect. Consolida en una sola fila toda la información clínica y demográfica necesaria para transmitir cada orden al laboratorio externo: datos del paciente (cédula, nombre completo, tipo de documento, tipo de afiliación, fecha de nacimiento, sexo, grupo sanguíneo, dirección, teléfono, correo), datos del ingreso hospitalario (número de ingreso, folio, cama, unidad funcional, centro de atención, causa de ingreso), datos del servicio solicitado (código CUPS, descripción, estado, prioridad, cantidad, observaciones), diagnósticos principal y secundarios (CIE-10), datos del médico ordenador (cédula, nombre, especialidad), entidad pagadora (EPS o aseguradora) y municipio del paciente. Se filtra únicamente por órdenes que tienen una interfaz de tipo ''INT'' en la tabla de detalle de intercambio (INTERDETA), asegurando que solo se sincronicen las órdenes destinadas a la integración con sistemas externos de laboratorio o patología.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'PathologyOrders_Mirth_Synch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'PathologyOrders_Mirth_Synch';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone órdenes de patología (hospitalarias y ambulatorias) en estado válido y aún no sincronizadas con Mirth, consolidando datos clínicos, demográficos, de ingreso, médico y diagnósticos para su envío a integración externa.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'PathologyOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben estar relacionadas en INTERDETA con tipo ''INT'' (hospitalaria) o ''AMB'' (ambulatoria) mediante AUTOLABOR.; El paciente debe existir en INPACIENT y tener ubicación asociada (AUUBICACI) que se resuelva a un municipio.; El ingreso (ADINGRESO) debe existir para el par (IPCODPACI, NUMINGRES) y tener entidad pagadora válida en INENTIDAD.; El servicio (CODSERIPS) debe existir en INCUPSIPS y la unidad funcional en INUNIFUNC.; Para órdenes hospitalarias, debe existir historia clínica (HCHISPACA) con especialidad tratante registrada en INESPECIA.; El profesional de salud (CODPROSAL) debe estar registrado en INPROFSAL.; El centro de atención (CODCENATE) debe existir en ADCENATEN.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'PathologyOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes con estado de servicio igual a 2 (autorizado/válido para envío).; Solo se exponen órdenes no sincronizadas previamente con Mirth (SYNCMIRTH NULL o 0).; El tipo de identificación del médico siempre se reporta como ''Cedula''.; La edad se calcula en años completos contra common.GETDATE() a partir de IPFECNACI.; Pacientes masculinos siempre reportan Gestacion=''No Aplica''.; Estrato nulo se normaliza a 0 y TipoPaciente nulo a ''No Aplica''.; Las órdenes ambulatorias nunca exponen Folio, Cama, Diagnóstico ni Especialidad (vienen vacíos o NULL).; El diagnóstico secundario y terciario solo se calcula sobre INDIAGNOH con CODDIAPRI=0 (no principales).', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'PathologyOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Integrations.PathologyOrders_Mirth_Synch: Cuando ESTSERIPS=2 y (SYNCMIRTH IS NULL OR SYNCMIRTH=0), se retorna la orden como pendiente de sincronización hacia Mirth.; [RETURN_RESULT] Integrations.PathologyOrders_Mirth_Synch: Cuando la orden proviene de HCORDPATO con INTERDETA.ORDTIP=''INT'', se marca TipoOrden=''Hospitalaria'' y se calcula Prioridad como ''Urgente'' si PRISERIPS=''1'', sino ''Rutinario''.; [RETURN_RESULT] Integrations.PathologyOrders_Mirth_Synch: Cuando la orden proviene de AMBORDPAT con INTERDETA.ORDTIP=''AMB'', se marca TipoOrden=''Ambulatoria'' y Prioridad siempre como ''Rutinario''.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'PathologyOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = 2 AND (SYNCMIRTH IS NULL OR SYNCMIRTH = 0) → La orden se incluye en el resultado para ser sincronizada else Se excluye; si INTERDETA.ORDTIP = ''INT'' → Se procesa como orden hospitalaria desde HCORDPATO con folio, cama, diagnósticos secundarios y especialidad de la historia clínica else Si ORDTIP=''AMB'', se procesa como orden ambulatoria desde AMBORDPAT sin folio, cama, ni diagnósticos secundarios; si PRISERIPS = ''1'' (en rama hospitalaria) → Prioridad=''Urgente'' else Prioridad=''Rutinario''; si Conteo de INDIAGNOH con CODDIAPRI=0 para el ingreso > 2 → Se exponen CodigoDiagnosticoTercero y NombreDiagnosticoTercero (último por orden descendente) else Esos campos quedan en NULL; si B.IPSEXOPAC = 1 (Masculino) → Gestacion=''No Aplica'' else Si HCRIESGOSP.GESTACION=1 entonces ''Si'', sino ''No''; si Mapeo de IPTIPODOC, IPTIPOPAC, IPTIPOAFI, ICAUSAING e IPSEXOPAC → Se traducen códigos numéricos a etiquetas de dominio (tipo de documento, régimen, afiliación, causa de ingreso, sexo)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'PathologyOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPATO; dbo.AMBORDPAT; dbo.INTERDETA; dbo.INPACIENT; dbo.INCUPSIPS; dbo.ADINGRESO; dbo.INENTIDAD; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INDIAGNOH; dbo.HCHISPACA; dbo.INESPECIA; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADCENATEN; dbo.HCRIESGOSP; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'PathologyOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'PathologyOrders_Mirth_Synch';
GO
