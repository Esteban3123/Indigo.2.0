
CREATE VIEW [dbo].[ViewServicesProceduresTherapy]
AS
	SELECT	CONCAT('HCPROCTER', '-', A.CODCONSEC) Id,
			'HCPROCTER' EntityName,
			A.CODCONSEC as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			RTRIM(D.UFUDESCRI) AS UnidadFuncional,
			RTRIM(C.NOMMEDICO) AS Medico,
			'' AS Observacion,
			FECHISPAC AS Fecha,
			'' AS Folio,
			CODESPEC1,
			1 AS CANSERIPS,
			NUMINGRES,
			IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			authorizationData.AuthorizationEventId,
			A.CODPROSAL as MedicoRealizo,
			FECHISPAC as FechaRealizacion,
			case when A.CODPROSAL is null then 0 else 1 end as Realizo,
			B.ARSCODIGO,
			A.CODPROSAL,
			TIPSERIPS,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			IIF(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			IIF(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			cd.Id AS ContractDescriptionId,
			cecd.Id AS CUPSEntityContractDescriptionId,
			IIF(A.IDDESCRIPCIONRELACIONADA IS NOT NULL, CONCAT(cd.Code, ' - ', cd.Name), NULL) CodeNameContractDescriptions
	FROM dbo.HCPROCTER A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	OUTER APPLY (
		SELECT TOP 1
			authorizedEvent.Id AS AuthorizationEventId
		FROM dbo.HCORDPRON pron WITH(NOLOCK)
		INNER JOIN [Authorization].[AuthorizationEvents] sourceEvent WITH(NOLOCK)
			ON sourceEvent.Id = pron.AuthorizationEventId
		INNER JOIN [Authorization].[AuthorizationEvents] authorizedEvent WITH(NOLOCK)
			ON authorizedEvent.AuthorizationControlId = sourceEvent.AuthorizationControlId
			AND authorizedEvent.Status = 4
		WHERE pron.NUMINGRES = A.NUMINGRES
			AND pron.IPCODPACI = A.IPCODPACI
			AND RTRIM(pron.CODSERIPS) = RTRIM(A.CODSERIPS)
			AND ISNULL(pron.ESTSERIPS, '') <> '5'
			AND NULLIF(LTRIM(RTRIM(authorizedEvent.AuthorizationCode)), '') IS NOT NULL
		ORDER BY authorizedEvent.AuthorizationDate DESC,
			authorizedEvent.CreationDate DESC,
			authorizedEvent.Id DESC
	) authorizationData
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON cecd.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd ON cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CODCONSEC=acj.EntityId and acj.EntityName='HCPROCTER' and acj.EntityTap='INDlcgTerapy'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
UNION ALL
	SELECT	CONCAT('HCPROCTER', '-', A.CODCONSEC) Id,
			'HCPROCTER' EntityName,
			A.CODCONSEC as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			RTRIM(D.UFUDESCRI) AS UnidadFuncional,
			RTRIM(C.NOMMEDICO) AS Medico,
			'' AS Observacion,
			a.FECHISPAC AS Fecha,
			'' AS Folio,
			CODESPEC1,
			1 AS CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			authorizationData.AuthorizationEventId,
			A.CODPROSAL as MedicoRealizo,
			a.FECHISPAC as FechaRealizacion,
			case when A.CODPROSAL is null then 0 else 1 end as Realizo,
			B.ARSCODIGO,
			A.CODPROSAL,
			TIPSERIPS,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			IIF(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			IIF(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			cd.Id AS ContractDescriptionId,
			cecd.Id AS CUPSEntityContractDescriptionId,
			IIF(A.IDDESCRIPCIONRELACIONADA IS NOT NULL, CONCAT(cd.Code, ' - ', cd.Name), NULL) CodeNameContractDescriptions
	FROM dbo.HCPROCTER A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO
	OUTER APPLY (
		SELECT TOP 1
			authorizedEvent.Id AS AuthorizationEventId
		FROM dbo.HCORDPRON pron WITH(NOLOCK)
		INNER JOIN [Authorization].[AuthorizationEvents] sourceEvent WITH(NOLOCK)
			ON sourceEvent.Id = pron.AuthorizationEventId
		INNER JOIN [Authorization].[AuthorizationEvents] authorizedEvent WITH(NOLOCK)
			ON authorizedEvent.AuthorizationControlId = sourceEvent.AuthorizationControlId
			AND authorizedEvent.Status = 4
		WHERE pron.NUMINGRES = A.NUMINGRES
			AND pron.IPCODPACI = A.IPCODPACI
			AND RTRIM(pron.CODSERIPS) = RTRIM(A.CODSERIPS)
			AND ISNULL(pron.ESTSERIPS, '') <> '5'
			AND NULLIF(LTRIM(RTRIM(authorizedEvent.AuthorizationCode)), '') IS NOT NULL
		ORDER BY authorizedEvent.AuthorizationDate DESC,
			authorizedEvent.CreationDate DESC,
			authorizedEvent.Id DESC
	) authorizationData
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON cecd.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd ON cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) on A.CODCONSEC=acj.EntityId and acj.EntityName='HCPROCTER' and acj.EntityTap='INDlcgTerapy'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE ING.IESTADOIN = 'C'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida los procedimientos, servicios y terapias registrados en historia clínica (HCPROCTER), enriquecidos con la descripción del servicio CUPS (INCUPSIPS), el nombre del profesional que lo realizó (INPROFSAL) y la unidad funcional o área de atención donde se ejecutó (INUNIFUNC). Incluye dos bloques: el primero cubre atenciones generales con número de ingreso directo, y el segundo cubre procedimientos asociados a recién nacidos (ingresos hijo vinculados a la madre), filtrando solo los ingresos cerrados. Adicionalmente expone la descripción de contrato relacionada al servicio (CUPSEntityContractDescriptions / ContractDescriptions) y la justificación de control de cuenta de facturación (AccountControlJustification / BillingJustificationControl), indicando si el ítem puede omitir liquidación o glosa. Se utiliza para la gestión y revisión de servicios ejecutados en terapia, facturación, control de glosas y generación de órdenes de servicio por paciente e ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewServicesProceduresTherapy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewServicesProceduresTherapy';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los procedimientos de terapia realizados a pacientes (incluyendo los asociados a recién nacidos vinculados al ingreso de la madre) junto con su justificación de control de cuentas y descripción contractual, para la facturación y generación de órdenes de servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada procedimiento de terapia debe tener servicio CUPS válido en INCUPSIPS, profesional válido en INPROFSAL y unidad funcional válida en INUNIFUNC (joins internos).; Para la rama de recién nacido, el ingreso debe tener correspondencia en HCINGRESORECNAC y HCRECINAC y existir en ADINGRESO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La justificación de control de cuentas se filtra siempre por EntityName=''HCPROCTER'' y EntityTap=''INDlcgTerapy'', acotando el contexto a procedimientos de terapia.; El identificador lógico de cada fila siempre se construye como ''HCPROCTER-{CODCONSEC}''.; La cantidad de servicio (CANSERIPS) siempre se reporta como 1 por registro.; Solo se incluyen procedimientos de recién nacido cuando el ingreso de la madre está en estado ''C''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimientos de terapia; Orden de servicio; Profesional de la salud; Unidad funcional; CUPS; Justificación de control de cuentas; Justificación de facturación / glosa; Descripción de contrato; Ingreso hospitalario; Recién nacido vinculado a ingreso de la madre; Liquidación (SkipClearance)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewServicesProceduresTherapy: Devuelve la unión de procedimientos de terapia del paciente directo y procedimientos del recién nacido cuyo ingreso de la madre tenga estado ''C'' (IESTADOIN = ''C'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.GENSERVICEORDER IS NULL → Seleccione = 0 (procedimiento no tiene orden de servicio generada) else Seleccione = 1; si A.CODPROSAL IS NULL → Realizo = 0 (no se ha registrado profesional que realizó el procedimiento) else Realizo = 1; si acj.Id IS NULL (no existe justificación de control de cuentas) → JustificationCodeName = '''' y SkipLiquidation = 0 else JustificationCodeName = Code - Description y SkipLiquidation = bjc.SkipClearance; si A.IDDESCRIPCIONRELACIONADA IS NOT NULL → CodeNameContractDescriptions = Code - Name de la descripción de contrato else CodeNameContractDescriptions = NULL; si Segunda rama del UNION ALL: ingreso del paciente está vinculado como hijo en HCINGRESORECNAC y el ingreso de la madre tiene IESTADOIN = ''C'' → Se incluyen también los procedimientos de terapia asociados al recién nacido bajo el ingreso de la madre cerrada/confirmada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPROCTER; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.AccountControlJustification; Billing.BillingJustificationControl; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresTherapy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresTherapy';
GO
