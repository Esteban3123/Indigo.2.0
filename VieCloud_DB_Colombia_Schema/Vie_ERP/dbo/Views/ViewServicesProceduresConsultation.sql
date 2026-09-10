

CREATE VIEW [dbo].[ViewServicesProceduresConsultation]
AS
	SELECT	CONCAT('HCORDINTE', '-', A.AUTO) Id,
			'HCORDINTE' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NOT NULL, 1, 0) AS Seleccione,
			RTRIM(D.UFUDESCRI) AS UnidadFuncional,
			RTRIM(C.NOMMEDICO) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.NUMEFOLIO AS FolioSolicitud,
			A.CODPROSAL AS MedicoSolicitud,
			A.FECORDMED AS FechaSolicitud,
			A.NUMFOLINT AS FolioRealizado,
			CASE WHEN A.NUMFOLINT IS NULL THEN NULL ELSE A.CODPROINT END AS MedicoRealizado,
			CASE WHEN A.NUMFOLINT IS NULL THEN NULL ELSE A.FECHAINT END AS FechaRealizado,
			INTERPRET AS Interpretacion,
			CODESPEC1,
			CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			CASE 
				WHEN A.NUMFOLINT IS NULL THEN NULL
				ELSE COALESCE(A.FECHAINT, HV.FECHISPAC)
			END AS FechaRealizacion,			
			CASE WHEN A.CODPROINT IS NULL THEN 0 ELSE 1 END As Realizo,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId,
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			CASE WHEN A.NUMFOLINT IS NULL THEN 'Solicitadas' ELSE 'Con Respuesta' END AS Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,		
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			CAST(IIF(A.NUMFOLINT IS NOT NULL AND HV.ID IS NOT NULL, 1, 0) AS BIT) AS ExistsInViewReviews
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDINTE A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDINTE' and acj.EntityTap='INDlcgInterSearch'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	OUTER APPLY (
		SELECT TOP 1 H.ID, H.FECHISPAC
		FROM dbo.HCHISPACA H
		WHERE H.IPCODPACI = A.IPCODPACI
			AND H.NUMINGRES = A.NUMINGRES
			AND H.NUMEFOLIO = A.NUMFOLINT
		ORDER BY H.FECHISPAC ASC
	) HV
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
UNION ALL
	SELECT	CONCAT('HCORDINTE', '-', A.AUTO) Id,
			'HCORDINTE' EntityName,
			A.[AUTO] as Row,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NOT NULL, 1, 0) AS Seleccione,
			RTRIM(D.UFUDESCRI) AS UnidadFuncional,
			RTRIM(C.NOMMEDICO) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.NUMEFOLIO AS FolioSolicitud,
			A.CODPROSAL AS MedicoSolicitud,
			A.FECORDMED AS FechaSolicitud,
			A.NUMFOLINT AS FolioRealizado,
			CASE WHEN A.NUMFOLINT IS NULL THEN NULL ELSE A.CODPROINT END AS MedicoRealizado,
			CASE WHEN A.NUMFOLINT IS NULL THEN NULL ELSE A.FECHAINT END AS FechaRealizado,
			INTERPRET AS Interpretacion,
			CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			CASE 
				WHEN A.NUMFOLINT IS NULL THEN NULL
				ELSE COALESCE(A.FECHAINT, HV.FECHISPAC)
			END AS FechaRealizacion,				
			CASE WHEN A.CODPROINT IS NULL THEN 0 ELSE 1 END As Realizo,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId,
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			CASE WHEN A.NUMFOLINT IS NULL THEN 'Solicitadas' ELSE 'Con Respuesta' END AS Tipo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,		
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			CAST(IIF(A.NUMFOLINT IS NOT NULL AND HV.ID IS NOT NULL, 1, 0) AS BIT) AS ExistsInViewReviews
	FROM dbo.HCORDINTE A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDINTE' and acj.EntityTap='INDlcgInterSearch'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	OUTER APPLY (
		SELECT TOP 1 H.ID, H.FECHISPAC
		FROM dbo.HCHISPACA H
		WHERE H.IPCODPACI = A.IPCODPACI
			AND H.NUMINGRES = A.NUMINGRES
			AND H.NUMEFOLIO = A.NUMFOLINT
		ORDER BY H.FECHISPAC ASC
	) HV
	WHERE ING.IESTADOIN = 'C' AND A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas de servicios y procedimientos solicitados o realizados durante ingresos de pacientes (urgencias, hospitalización, consulta externa y recién nacidos). Consolida, por cada orden, el servicio CUPS solicitado, el médico solicitante y realizador, las fechas de solicitud y realización, la unidad funcional, las observaciones, el folio de solicitud y de respuesta, la descripción contractual del servicio (grupo y concepto de facturación), y las justificaciones de control de glosa o liquidación asociadas. Integra datos del ingreso (ADINGRESO), la orden médica (HCORDINTE), el catálogo de servicios CUPS (INCUPSIPS), el maestro de profesionales (INPROFSAL), las unidades funcionales (INUNIFUNC), las descripciones de contrato (CUPSEntityContractDescriptions / ContractDescriptions), las justificaciones de control de cuenta de facturación (AccountControlJustification / BillingJustificationControl) y la historia clínica (HCHISPACA). Incluye un segundo bloque para órdenes vinculadas a ingresos de recién nacidos. Se utiliza para consultar, auditar y gestionar servicios y procedimientos ordenados por el médico, tanto en estado ''Solicitadas'' como ''Con Respuesta'', con soporte para revisión de glosas y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewServicesProceduresConsultation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewServicesProceduresConsultation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las órdenes médicas internas (servicios/procedimientos) de un ingreso, indicando si están solicitadas o realizadas, su justificación de control de cuentas y vinculación contractual, incluyendo ingresos de recién nacidos asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden interna debe pertenecer a un ingreso existente en ADINGRESO (o a un ingreso hijo en HCINGRESORECNAC para el caso del recién nacido).; El servicio CUPS, profesional solicitante y unidad funcional deben existir en sus respectivos maestros (INCUPSIPS, INPROFSAL, INUNIFUNC).; Para el primer bloque: la orden debe tener MANEXTPRO=0 o el ingreso debe tener TRATAESPECIA=3.; Para el segundo bloque: el ingreso hijo debe estar en estado ''C'' (cerrado) y MANEXTPRO=0, o el ingreso debe tener TRATAESPECIA=3.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador lógico de cada fila se construye como ''HCORDINTE-'' + AUTO y la EntityName siempre es ''HCORDINTE''.; Si la orden no tiene folio de respuesta (NUMFOLINT NULL), no se exponen datos del médico/fecha de realización.; La justificación vinculada solo aplica cuando EntityName=''HCORDINTE'' y EntityTap=''INDlcgInterSearch''.; FechaRealizacion solo se calcula cuando hay folio de respuesta y prefiere FECHAINT sobre la fecha de la primera nota clínica asociada.; Las descripciones contractuales se exponen como 0/'''' cuando no hay relación CUPS-Entidad-Contrato.; Se aplican RTRIM a descripciones de unidad funcional, médico y servicio para limpiar espacios.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden médica interna; Servicios y procedimientos CUPS; Folio de solicitud y folio de realización; Médico solicitante y médico que realiza; Unidad funcional; Ingreso hospitalario; Tratamiento especial; Recién nacido (ingreso hijo); Historia clínica / nota clínica; Justificación de control de cuentas; Glosa / SkipClearance (omitir liquidación); Descripción de contrato', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve filas de HCORDINTE filtradas por (MANEXTPRO=0 OR TRATAESPECIA=3) y, en el bloque de recién nacido, además IESTADOIN=''C''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.GENSERVICEORDER IS NOT NULL → Marca Seleccione=1 (la orden ya generó orden de servicio) else Seleccione=0; si A.NUMFOLINT IS NULL → Tipo=''Solicitadas''; MedicoRealizado, FechaRealizado y FechaRealizacion quedan en NULL else Tipo=''Con Respuesta''; expone CODPROINT, FECHAINT y FechaRealizacion = COALESCE(FECHAINT, primera FECHISPAC de HCHISPACA); si A.CODPROINT IS NULL → Realizo=0 else Realizo=1; si acj.Id IS NULL → JustificationCodeName='''' y SkipLiquidation=0 else JustificationCodeName=Code - Description y SkipLiquidation=bjc.SkipClearance; si A.NUMFOLINT IS NOT NULL AND existe registro en HCHISPACA con mismo paciente, ingreso y folio → ExistsInViewReviews=1 else ExistsInViewReviews=0; si Origen del ingreso es un recién nacido (HCINGRESORECNAC/HCRECINAC) → Se incluye la orden usando el NUMINGRES del ingreso hijo y se exige IESTADOIN=''C'' else Se usa el flujo estándar contra ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDINTE; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.AccountControlJustification; Billing.BillingJustificationControl; dbo.HCHISPACA; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresConsultation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewServicesProceduresConsultation';
GO
