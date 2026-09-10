

CREATE VIEW [dbo].[ViewReviews]
AS

	SELECT A.ID
	,RTRIM(E.NOMCENATE) AS NOMCENATE
	,IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
	,A.NUMEFOLIO as Folio
	,B.CODPROSAL
	,B.CODIGONIT as NitMedico
	,B.NOMMEDICO as Medico
	,cast((case when ing.CODESPTRA is not null And ing.CODESPTRA = C.CODESPECI 
		then 1 else 0 end) as bit) as Tratante
	,ing.CODESPTRA
	,D.UFUCODIGO
	,RTRIM(D.UFUDESCRI) AS UFUDESCRI
	--,ing.CODESPTRA
	,C.CODESPECI
	,RTRIM(C.DESESPECI) AS DESESPECI
	,FECHISPAC
	,a.IPCODPACI
	,A.NUMINGRES
	,i.IPNOMCOMP  as PersonName
	,A.GENSERVICEORDER
	,s.CODSERIPSINTRA as CUPSManejo
	,s.IDDESCRIPCIONRELACIONADA_INTRA as CUPSEntityContractDescriptionIdManejo
	,contDesManejo.ContractDescriptionId as ContractDescriptionIdManejo
	,s.CODSERINT as CUPSInterconsulta
	,s.IDDESCRIPCIONRELACIONADA_INTER as CUPSEntityContractDescriptionIdInterconsulta
	,contDesInterConsulta.ContractDescriptionId as ContractDescriptionIdInterconsulta
	,s.CODSERCEX as CUPSControl
	,s.IDDESCRIPCIONRELACIONADA_CONS as CUPSEntityContractDescriptionIdControl
	,contDesControl.ContractDescriptionId as ContractDescriptionIdControl
	,cast(iif(ad.NUMEFOLIO is null,0,1) as bit) FirstEmergencyCare
	,iif(ad.NUMEFOLIO is null,NULL,s.CodServiceUrg)  as CupsEmergency
	,iif(ad.NUMEFOLIO is null,NULL,contDesEmergency.Id)  as CUPSEntityContractDescriptionIdEmergency
	,iif(ad.NUMEFOLIO is null,NULL,contDesEmergency.ContractDescriptionId)  as ContractDescriptionIdEmergency,
	acj.Id ACJustificationId,
	acj.CreationUser ACCreationUser,
	acj.CreationDate ACCreationDate,
	acj.JustificationId,
	iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
	iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
	'HCHISPACA' EntityName,
	authorizationData.AuthorizationEventId,
	D.UFUTIPUNI
	FROM dbo.HCHISPACA A  
	INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
	INNER JOIN dbo.INESPECIA C ON c.CODESPECI = ISNULL(A.CODESPTRA,B.CODESPEC1)	
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	INNER JOIN dbo.ADCENATEN E ON A.CODCENATE=E.CODCENATE
	inner join dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	inner join dbo.ADINGRESO ing on A.NUMINGRES = ing.NUMINGRES
	left join dbo.HCESPSERU s on s.CODESPECI = C.CODESPECI
	left join (SELECT NUMEFOLIO,NUMINGRES
				FROM ADATEINIU ad WITH(NOLOCK)
				GROUP BY NUMEFOLIO,NUMINGRES ) ad ON ad.NUMEFOLIO = A.NUMEFOLIO AND ad.NUMINGRES = A.NUMINGRES
	OUTER APPLY (
		SELECT TOP 1 authorizedEvent.Id AS AuthorizationEventId
		FROM dbo.ADATEINIU authorizationSource WITH(NOLOCK)
		INNER JOIN [Authorization].[AuthorizationEvents] sourceEvent WITH(NOLOCK)
			ON sourceEvent.Id = authorizationSource.AuthorizationEventId
		INNER JOIN [Authorization].[AuthorizationEvents] authorizedEvent WITH(NOLOCK)
			ON authorizedEvent.AuthorizationControlId = sourceEvent.AuthorizationControlId
			AND authorizedEvent.Status = 4
		WHERE authorizationSource.NUMEFOLIO = A.NUMEFOLIO
			AND authorizationSource.NUMINGRES = A.NUMINGRES
			AND NULLIF(LTRIM(RTRIM(authorizedEvent.AuthorizationCode)), '') IS NOT NULL
		ORDER BY authorizedEvent.AuthorizationDate DESC,
			authorizedEvent.CreationDate DESC,
			authorizedEvent.Id DESC
	) authorizationData
	left join Contract.CUPSEntityContractDescriptions contDesManejo WITH(NOLOCK) on contDesManejo.Id = s.IDDESCRIPCIONRELACIONADA_INTRA
	left join Contract.CUPSEntityContractDescriptions contDesInterConsulta WITH(NOLOCK) on contDesInterConsulta.Id = s.IDDESCRIPCIONRELACIONADA_INTER
	left join Contract.CUPSEntityContractDescriptions contDesControl WITH(NOLOCK) on contDesControl.Id = s.IDDESCRIPCIONRELACIONADA_CONS
	left join Contract.CUPSEntityContractDescriptions contDesEmergency WITH(NOLOCK) on  contDesEmergency.Id = s.IdRelatedDescription_Urg
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.ID=acj.EntityId and acj.EntityName='HCHISPACA' and acj.EntityTap='INDlcgValoraciones'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id

	union all

	SELECT A.ID
	,RTRIM(E.NOMCENATE) AS NOMCENATE	
	,IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
	,A.NUMEFOLIO as Folio
	,B.CODPROSAL
	,B.CODIGONIT as NitMedico
	,B.NOMMEDICO as Medico
	,cast((case when ing.CODESPTRA is not null And ing.CODESPTRA = C.CODESPECI 
		then 1 else 0 end) as bit) as Tratante
	,ing.CODESPTRA
	,D.UFUCODIGO
	,RTRIM(D.UFUDESCRI) AS UFUDESCRI
	--,ing.CODESPTRA
	,C.CODESPECI
	,RTRIM(C.DESESPECI) AS DESESPECI
	,a.FECHISPAC
	,a.IPCODPACI
	,INGMH.NUMINGRES
	,'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName
	,A.GENSERVICEORDER
	,s.CODSERIPSINTRA as CUPSManejo
	,s.IDDESCRIPCIONRELACIONADA_INTRA as CUPSEntityContractDescriptionIdManejo
	,contDesManejo.ContractDescriptionId as ContractDescriptionIdManejo
	,s.CODSERINT as CUPSInterconsulta
	,s.IDDESCRIPCIONRELACIONADA_INTER as CUPSEntityContractDescriptionIdInterconsulta
	,contDesInterConsulta.ContractDescriptionId as ContractDescriptionIdInterconsulta
	,s.CODSERCEX as CUPSControl
	,s.IDDESCRIPCIONRELACIONADA_CONS as CUPSEntityContractDescriptionIdControl
	,contDesControl.ContractDescriptionId as ContractDescriptionIdControl
	,cast(iif(ad.NUMEFOLIO is null,0,1) as bit) FirstEmergencyCare
	,iif(ad.NUMEFOLIO is null,NULL,s.CodServiceUrg)  as CupsEmergency
	,iif(ad.NUMEFOLIO is null,NULL,contDesEmergency.Id)  as CUPSEntityContractDescriptionIdEmergency
	,iif(ad.NUMEFOLIO is null,NULL,contDesEmergency.ContractDescriptionId)  as ContractDescriptionIdEmergency,
	acj.Id ACJustificationId,
	acj.CreationUser ACCreationUser,
	acj.CreationDate ACCreationDate,
	acj.JustificationId,
	iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
	iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
	'HCHISPACA' EntityName,
	authorizationData.AuthorizationEventId,
	D.UFUTIPUNI
	FROM dbo.HCHISPACA A  
	INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
	INNER JOIN dbo.INESPECIA C ON c.CODESPECI = ISNULL(A.CODESPTRA,B.CODESPEC1)	
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	INNER JOIN dbo.ADCENATEN E ON A.CODCENATE=E.CODCENATE
	inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
	left join dbo.HCESPSERU s on s.CODESPECI = C.CODESPECI
	left join (SELECT NUMEFOLIO,NUMINGRES
				FROM ADATEINIU ad WITH(NOLOCK)
				GROUP BY NUMEFOLIO,NUMINGRES ) ad ON ad.NUMEFOLIO = A.NUMEFOLIO AND ad.NUMINGRES = A.NUMINGRES
	OUTER APPLY (
		SELECT TOP 1 authorizedEvent.Id AS AuthorizationEventId
		FROM dbo.ADATEINIU authorizationSource WITH(NOLOCK)
		INNER JOIN [Authorization].[AuthorizationEvents] sourceEvent WITH(NOLOCK)
			ON sourceEvent.Id = authorizationSource.AuthorizationEventId
		INNER JOIN [Authorization].[AuthorizationEvents] authorizedEvent WITH(NOLOCK)
			ON authorizedEvent.AuthorizationControlId = sourceEvent.AuthorizationControlId
			AND authorizedEvent.Status = 4
		WHERE authorizationSource.NUMEFOLIO = A.NUMEFOLIO
			AND authorizationSource.NUMINGRES = A.NUMINGRES
			AND NULLIF(LTRIM(RTRIM(authorizedEvent.AuthorizationCode)), '') IS NOT NULL
		ORDER BY authorizedEvent.AuthorizationDate DESC,
			authorizedEvent.CreationDate DESC,
			authorizedEvent.Id DESC
	) authorizationData
	left join Contract.CUPSEntityContractDescriptions contDesManejo on contDesManejo.Id = s.IDDESCRIPCIONRELACIONADA_INTRA
	left join Contract.CUPSEntityContractDescriptions contDesInterConsulta on contDesInterConsulta.Id = s.IDDESCRIPCIONRELACIONADA_INTER
	left join Contract.CUPSEntityContractDescriptions contDesControl on contDesControl.Id = s.IDDESCRIPCIONRELACIONADA_CONS
	left join Contract.CUPSEntityContractDescriptions contDesEmergency WITH(NOLOCK) on  contDesEmergency.Id = s.IdRelatedDescription_Urg
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.ID=acj.EntityId and acj.EntityName='HCHISPACA' and acj.EntityTap='INDlcgValoraciones'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	where ING.IESTADOIN = 'C'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las valoraciones médicas (historias clínicas) registradas en el sistema, integrando datos del profesional de salud que realizó la atención, la especialidad médica, la unidad funcional, el centro de atención y el paciente. Combina dos escenarios: valoraciones de pacientes regulares y valoraciones asociadas a recién nacidos (hijos registrados en el módulo de maternidad), unificando ambos conjuntos mediante UNION ALL. Para cada valoración expone los códigos CUPS correspondientes según la especialidad (manejo intrahospitalario, interconsulta, control y urgencias), los identificadores de descripción de contrato vinculados a esos CUPS, un indicador de si la atención inició como urgencia (primer contacto de urgencias ATEI), el indicador de si ya se generó una orden de servicio, y la justificación de control de cuenta de facturación asociada al folio. Sirve principalmente para el módulo de revisión y liquidación de valoraciones médicas, permitiendo determinar qué servicios facturar, si aplica omisión de glosa y qué médico tratante realizó la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewReviews';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewReviews';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las valoraciones/notas clínicas (folios) de pacientes y de recién nacidos asociados, enriquecidas con datos de profesional, especialidad, unidad funcional, CUPS por tipo de atención y justificaciones de control de cuentas de facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewReviews';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en HCHISPACA con profesional (INPROFSAL), unidad funcional (INUNIFUNC), centro de atención (ADCENATEN) y paciente (INPACIENT) válidos.; La especialidad se resuelve por CODESPTRA del folio y, si es nula, por CODESPEC1 del profesional.; Para la rama de recién nacidos, debe existir vínculo en HCINGRESORECNAC y HCRECINAC y el ingreso del hijo debe estar en estado ''C'' (cerrado/confirmado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewReviews';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'EntityName siempre se entrega como constante ''HCHISPACA''.; La búsqueda de justificaciones se restringe a EntityName=''HCHISPACA'' y EntityTap=''INDlcgValoraciones''.; Cada folio del paciente titular se identifica con el nombre del paciente; cada folio del hijo recién nacido se identifica con ''Hijo ''+número de hijo.; Los campos de urgencias (CUPS y descripciones) son mutuamente consistentes: o todos tienen valor o todos son NULL, según exista atención inicial de urgencias.; La especialidad nunca queda sin resolver: siempre cae en CODESPTRA o, en su defecto, en CODESPEC1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewReviews';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Folio de historia clínica; Valoración médica (INDlcgValoraciones); Especialidad tratante; Profesional de la salud; Unidad funcional; Centro de atención; Ingreso/admisión; Recién nacido (hijo) asociado al ingreso de la madre; CUPS por tipo de atención: manejo intrahospitalario, interconsulta, control y urgencias; Primera atención de urgencias; Justificación de control de cuentas de facturación; Omisión de liquidación (SkipClearance)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewReviews';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewReviews: Devuelve una fila por cada folio de HCHISPACA (paciente titular) unida con la misma información para folios asociados a recién nacidos cuyo ingreso del hijo tiene IESTADOIN=''C''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewReviews';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.GENSERVICEORDER IS NULL → Seleccione = 0 else Seleccione = 1; si ing.CODESPTRA no es nulo y coincide con la especialidad resuelta (C.CODESPECI) → Tratante = 1 (el médico/especialidad es el tratante del ingreso) else Tratante = 0; si A.CODESPTRA del folio es nulo → Se usa la primera especialidad del profesional (B.CODESPEC1) para resolver INESPECIA else Se usa la especialidad del folio (A.CODESPTRA); si Existe registro en ADATEINIU para el folio e ingreso (ad.NUMEFOLIO no nulo) → FirstEmergencyCare=1 y se exponen CUPS/descripciones de urgencias else FirstEmergencyCare=0 y los campos de urgencias quedan en NULL; si Existe AccountControlJustification para la entidad (acj.id no nulo) → Se concatena código y descripción de la justificación y se expone SkipClearance del catálogo else JustificationCodeName='''' y SkipLiquidation=0; si Rama de recién nacidos: ING.IESTADOIN = ''C'' → Se incluyen los folios del recién nacido (PersonName=''Hijo ''+NUMHIJREG) en la vista else Se excluyen los folios cuyo ingreso del hijo no esté en estado ''C''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewReviews';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPROFSAL; dbo.INESPECIA; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INPACIENT; dbo.ADINGRESO; dbo.HCESPSERU; dbo.ADATEINIU; Contract.CUPSEntityContractDescriptions; Billing.AccountControlJustification; Billing.BillingJustificationControl; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewReviews';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewReviews';
GO
