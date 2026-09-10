
CREATE VIEW [dbo].[ViewServiceProceduresNursingProcedures]
AS
		SELECT	CONCAT('HCHOGASIN', '-', STRING_AGG(A.CONSECUTI,'-')) Id,
			'HCHOGASIN' EntityName,
			STRING_AGG(A.CONSECUTI,',') as Row,
			CASE 
				WHEN C.ACTFACTUR = '1' THEN COALESCE(NULLIF(C.CODSERIPS, ''), A.CODSERIPS)
				ELSE C.CODSERIPS
			END AS CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			RTRIM(F.UFUDESCRI) AS UnidadFuncional,
			RTRIM(G.NOMMEDICO) AS Medico,
			A.OBSERVACI AS Observacion,
			FECHAUTIL as Fecha,
			CODCENATE,
			A.CODPROSAL,
			E.CODESPEC1,
			A.IPCODPACI,
			NUMINGRES,
			F.UFUCODIGO,
			G.CODIGONIT as NitMedico,
			RTRIM(DESACTENF) AS Actividad,
			A.GENSERVICEORDER,
			authorizationData.AuthorizationEventId,
			COALESCE(A.CANACTENF,0) as CANSERIPS,
			CASE C.ACTFACTUR WHEN '1' THEN 'Facturable' WHEN '0' THEN 'No Facturable' END AS Tipo,
			D.DESSERIPS,
			C.CODACTENF AS CodigoActividad,
			i.IPNOMCOMP  as PersonName,
			STRING_AGG(acj.Id,',') ACJustificationId,
			acj.CreationUser ACCreationUser,
			CAST(acj.CreationDate AS DATE) ACCreationDate,
			acj.JustificationId,
			IIF(acj.JustificationId is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			IIF(acj.JustificationId is null,0,bjc.SkipClearance) SkipLiquidation,
			cd.Id ContractDescriptionId,
			ccd.Id CUPSEntityContractDescriptionId,
			IIF(A.IDDESCRIPCIONRELACIONADA IS NOT NULL, CONCAT(cd.Code, ' - ', cd.Name), NULL) CodeNameContractDescriptions
	FROM dbo.HCHOGASIN A 
	LEFT OUTER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
	INNER JOIN dbo.HCACTENFE C ON A.CODACTENF=C.CODACTENF 
	LEFT JOIN INCUPSIPS D ON D.CODSERIPS = CASE 
		WHEN C.ACTFACTUR = '1' THEN 
			CASE 
				WHEN ISNULL(C.CODSERIPS, '') <> '' THEN C.CODSERIPS
				ELSE A.CODSERIPS
			END
		ELSE C.CODSERIPS
	END
	LEFT OUTER JOIN dbo.INPROFSAL E ON A.CODPROSAL=E.CODPROSAL
	INNER JOIN dbo.INUNIFUNC F ON A.UFUCODIGO = F.UFUCODIGO
	INNER JOIN INPROFSAL G ON A.CODPROSAL = G.CODPROSAL
	INNER JOIN  dbo.INPACIENT i on A.IPCODPACI = i.IPCODPACI
	OUTER APPLY (
		SELECT TOP 1
			authorizedEvent.Id AS AuthorizationEventId
		FROM dbo.HCORDPRON pron WITH(NOLOCK)
		INNER JOIN [Authorization].[AuthorizationEvents] sourceEvent WITH(NOLOCK)
			ON sourceEvent.Id = pron.AuthorizationEventId
		INNER JOIN [Authorization].[AuthorizationEvents] authorizedEvent WITH(NOLOCK)
			ON authorizedEvent.AuthorizationControlId = sourceEvent.AuthorizationControlId
			AND authorizedEvent.Status = 4
		WHERE NULLIF(LTRIM(RTRIM(A.CODSERIPS)), '') IS NOT NULL
			AND pron.NUMINGRES = A.NUMINGRES
			AND pron.IPCODPACI = A.IPCODPACI
			AND RTRIM(pron.CODSERIPS) = RTRIM(A.CODSERIPS)
			AND ISNULL(pron.ESTSERIPS, '') <> '5'
			AND NULLIF(LTRIM(RTRIM(authorizedEvent.AuthorizationCode)), '') IS NOT NULL
		ORDER BY authorizedEvent.AuthorizationDate DESC,
			authorizedEvent.CreationDate DESC,
			authorizedEvent.Id DESC
	) authorizationData
	LEFT JOIN Contract.CUPSEntityContractDescriptions ccd ON ccd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd ON cd.Id = ccd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CONSECUTI=acj.EntityId and acj.EntityName='HCHOGASIN' and acj.EntityTap='INDlcgNurseProcedure'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	GROUP BY C.CODACTENF,RTRIM(DESACTENF),VALACTENF
		,ACTFACTUR,
		CASE 
				WHEN C.ACTFACTUR = '1' THEN COALESCE(NULLIF(C.CODSERIPS, ''), A.CODSERIPS)
				ELSE C.CODSERIPS
		END,
		TIPSERIPS,A.IPCODPACI
		,NUMINGRES,CODCENATE,F.UFUDESCRI,G.NOMMEDICO
		,A.OBSERVACI,FECHAUTIL,A.CODPROSAL,E.CODESPEC1
		,F.UFUCODIGO,G.CODIGONIT,A.GENSERVICEORDER,D.DESSERIPS,i.IPNOMCOMP
		,acj.CreationUser,CAST(acj.CreationDate AS DATE),acj.JustificationId
		,bjc.Code,bjc.[Description],bjc.SkipClearance,COALESCE(A.CANACTENF,0),
		cd.Id, ccd.Id, A.IDDESCRIPCIONRELACIONADA, cd.Code, cd.Name,
		authorizationData.AuthorizationEventId

UNION ALL
	SELECT	CONCAT('HCHOGASIN', '-',STRING_AGG(A.CONSECUTI,'-')) Id,
			'HCHOGASIN' EntityName,
			STRING_AGG(A.CONSECUTI,',') as Row,
			CASE 
				WHEN C.ACTFACTUR = '1' THEN COALESCE(NULLIF(C.CODSERIPS, ''), A.CODSERIPS)
				ELSE C.CODSERIPS
			END AS CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			RTRIM(F.UFUDESCRI) AS UnidadFuncional,
			RTRIM(G.NOMMEDICO) AS Medico,
			A.OBSERVACI AS Observacion,
			FECHAUTIL as Fecha,
			a.CODCENATE,
			A.CODPROSAL,
			E.CODESPEC1,
			A.IPCODPACI,
			INGMH.NUMINGRES,
			F.UFUCODIGO,
			G.CODIGONIT as NitMedico,
			RTRIM(DESACTENF) AS Actividad,
			A.GENSERVICEORDER,
			authorizationData.AuthorizationEventId,
			COALESCE(A.CANACTENF,0) as CANSERIPS,
			CASE C.ACTFACTUR WHEN '1' THEN 'Facturable' WHEN '0' THEN 'No Facturable' END AS Tipo,
			D.DESSERIPS,
			C.CODACTENF AS CodigoActividad,
			'Hijo '+ CAST(rn.NUMHIJREG as varchar(20)) as PersonName,
			STRING_AGG(acj.Id,',') ACJustificationId,
			acj.CreationUser ACCreationUser,
			CAST(acj.CreationDate AS DATE) ACCreationDate,
			acj.JustificationId,
			IIF(acj.JustificationId is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			IIF(acj.JustificationId is null,0,bjc.SkipClearance) SkipLiquidation,
			cd.Id ContractDescriptionId,
			ccd.Id CUPSEntityContractDescriptionId,
			IIF(A.IDDESCRIPCIONRELACIONADA IS NOT NULL, CONCAT(cd.Code, ' - ', cd.Name), NULL) CodeNameContractDescriptions
	FROM dbo.HCHOGASIN A 
	LEFT OUTER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
	INNER JOIN dbo.HCACTENFE C ON A.CODACTENF=C.CODACTENF 
	LEFT JOIN INCUPSIPS D ON D.CODSERIPS = CASE 
		WHEN C.ACTFACTUR = '1' THEN 
			CASE 
				WHEN ISNULL(C.CODSERIPS, '') <> '' THEN C.CODSERIPS
				ELSE A.CODSERIPS
			END
		ELSE C.CODSERIPS
	END
	LEFT OUTER JOIN dbo.INPROFSAL E ON A.CODPROSAL=E.CODPROSAL
	INNER JOIN dbo.INUNIFUNC F ON A.UFUCODIGO = F.UFUCODIGO
	INNER JOIN INPROFSAL G ON A.CODPROSAL = G.CODPROSAL
	INNER JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	INNER JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	INNER JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
	OUTER APPLY (
		SELECT TOP 1
			authorizedEvent.Id AS AuthorizationEventId
		FROM dbo.HCORDPRON pron WITH(NOLOCK)
		INNER JOIN [Authorization].[AuthorizationEvents] sourceEvent WITH(NOLOCK)
			ON sourceEvent.Id = pron.AuthorizationEventId
		INNER JOIN [Authorization].[AuthorizationEvents] authorizedEvent WITH(NOLOCK)
			ON authorizedEvent.AuthorizationControlId = sourceEvent.AuthorizationControlId
			AND authorizedEvent.Status = 4
		WHERE NULLIF(LTRIM(RTRIM(A.CODSERIPS)), '') IS NOT NULL
			AND pron.NUMINGRES = A.NUMINGRES
			AND pron.IPCODPACI = A.IPCODPACI
			AND RTRIM(pron.CODSERIPS) = RTRIM(A.CODSERIPS)
			AND ISNULL(pron.ESTSERIPS, '') <> '5'
			AND NULLIF(LTRIM(RTRIM(authorizedEvent.AuthorizationCode)), '') IS NOT NULL
		ORDER BY authorizedEvent.AuthorizationDate DESC,
			authorizedEvent.CreationDate DESC,
			authorizedEvent.Id DESC
	) authorizationData
	LEFT JOIN Contract.CUPSEntityContractDescriptions ccd ON ccd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd ON cd.Id = ccd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CONSECUTI=acj.EntityId and acj.EntityName='HCHOGASIN' and acj.EntityTap='INDlcgNurseProcedure'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE ING.IESTADOIN = 'C' 
	GROUP BY C.CODACTENF,RTRIM(DESACTENF),VALACTENF
		,ACTFACTUR,
		CASE 
				WHEN C.ACTFACTUR = '1' THEN COALESCE(NULLIF(C.CODSERIPS, ''), A.CODSERIPS)
				ELSE C.CODSERIPS
		END,
		TIPSERIPS,a.IPCODPACI
		,INGMH.NUMINGRES,a.CODCENATE,F.UFUDESCRI,G.NOMMEDICO
		,A.OBSERVACI,FECHAUTIL,A.CODPROSAL,E.CODESPEC1
		,F.UFUCODIGO,G.CODIGONIT,A.GENSERVICEORDER,CONSECUTI
		,D.DESSERIPS,rn.NUMHIJREG,acj.CreationUser,CAST(acj.CreationDate AS DATE)
		,acj.JustificationId,bjc.Code,bjc.[Description],bjc.SkipClearance,
		cd.Id, ccd.Id, A.IDDESCRIPCIONRELACIONADA, cd.Code, cd.Name,COALESCE(A.CANACTENF,0),
		authorizationData.AuthorizationEventId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los procedimientos y actividades de enfermería registrados durante la atención hospitalaria, integrando dos escenarios: atenciones de pacientes adultos (ingreso regular) y atenciones de recién nacidos (neonatos con ingreso hijo). Combina los consumos y actividades de enfermería (HCHOGASIN, HCACTENFE) con el catálogo de productos farmacéuticos (IHLISTPRO), el catálogo CUPS/servicios (INCUPSIPS), los datos del paciente (INPACIENT), el profesional de salud responsable (INPROFSAL) y la unidad funcional donde se prestó el servicio (INUNIFUNC). Enriquece cada registro con información contractual (descripción de contrato CUPS asociada) y con justificaciones de control de cuenta de facturación (glosas), indicando si la actividad es facturable o no facturable, el código CUPS aplicable, la cantidad ejecutada, observaciones clínicas y, en el caso de neonatos, el número de hijo registrado. Se utiliza principalmente en los módulos de liquidación de enfermería, revisión de cuentas y generación de RIPS, permitiendo identificar qué procedimientos de enfermería se cobran, a qué paciente o recién nacido corresponden, bajo qué contrato y si tienen justificación de glosa o exención de liquidación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewServiceProceduresNursingProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewServiceProceduresNursingProcedures';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida actividades/procedimientos de enfermería registrados en la historia clínica, enriquecidos con datos del paciente (incluyendo recién nacidos), profesional, unidad funcional, código CUPS facturable y justificaciones de control de cuenta para su gestión y facturación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServiceProceduresNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en HCHOGASIN con CODACTENF válido en HCACTENFE, profesional en INPROFSAL, unidad funcional en INUNIFUNC y paciente en INPACIENT; Para la rama de recién nacidos, el ingreso debe existir en HCINGRESORECNAC y HCRECINAC y el ingreso administrativo (ADINGRESO) debe estar en estado ''C'' (cerrado/confirmado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServiceProceduresNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El EntityName siempre se reporta como ''HCHOGASIN''; Las justificaciones de control de cuenta solo se vinculan cuando EntityName=''HCHOGASIN'' y EntityTap=''INDlcgNurseProcedure''; Cuando una actividad es no facturable, siempre prevalece el CODSERIPS del catálogo de actividades (HCACTENFE) sobre el del registro de consumo; La cantidad CANSERIPS nunca es nula (se reemplaza por 0 cuando no hay valor); La rama de recién nacidos solo incluye ingresos administrativamente en estado ''C''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServiceProceduresNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Actividad de enfermería; Procedimiento facturable / no facturable; Código CUPS / servicio IPS; Unidad funcional; Profesional de la salud (médico); Paciente; Ingreso hospitalario; Recién nacido (hijo de la madre); Orden de servicio; Justificación de control de cuenta; Justificación de facturación / glosa; Descripción de contrato; Liquidación (SkipClearance)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServiceProceduresNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas agrupadas por actividad de enfermería; agrega CONSECUTI con STRING_AGG generando un Id compuesto ''HCHOGASIN-<consecutivos>''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServiceProceduresNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.ACTFACTUR = ''1'' (actividad facturable) y C.CODSERIPS no es nulo ni vacío → Usa C.CODSERIPS como código de servicio IPS else Si la actividad es facturable pero C.CODSERIPS está vacío, usa A.CODSERIPS; si no es facturable, usa C.CODSERIPS; si C.ACTFACTUR = ''1'' → Marca el Tipo como ''Facturable'' else Si ACTFACTUR = ''0'', marca como ''No Facturable''; si A.GENSERVICEORDER IS NULL → Seleccione = 0 (no tiene orden de servicio generada) else Seleccione = 1; si acj.JustificationId IS NULL → JustificationCodeName vacío y SkipLiquidation = 0 else JustificationCodeName = ''Code - Description'' y SkipLiquidation toma bjc.SkipClearance; si A.IDDESCRIPCIONRELACIONADA IS NOT NULL → Expone CodeNameContractDescriptions concatenando código y nombre del contrato else CodeNameContractDescriptions = NULL; si Segunda rama UNION: ING.IESTADOIN = ''C'' y el ingreso corresponde a un recién nacido (NUMINGRESHIJO) → Identifica al paciente como ''Hijo <NUMHIJREG>'' en lugar del nombre del paciente titular', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServiceProceduresNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOGASIN; dbo.IHLISTPRO; dbo.HCACTENFE; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.INPACIENT; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.AccountControlJustification; Billing.BillingJustificationControl', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServiceProceduresNursingProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServiceProceduresNursingProcedures';
GO
