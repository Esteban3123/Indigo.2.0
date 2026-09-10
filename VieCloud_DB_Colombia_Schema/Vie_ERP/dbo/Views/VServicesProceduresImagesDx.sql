
CREATE VIEW [dbo].[VServicesProceduresImagesDx]
AS
	SELECT	CONCAT('HCORDIMAG', '-', A.AUTO) Id,
			'HCORDIMAG' EntityName,
			A.[AUTO] as Row,
			A.AuthorizationEventId,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(ISNULL(US.UFUCODIGO, D.UFUCODIGO)), ' - ', RTRIM(ISNULL(US.UFUDESCRI, D.UFUDESCRI))) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.NUMFOLINT AS FolioInterpreta,
			A.CODPROINT AS MedicoInterpreto,
			A.INTERPRET AS Interpretacion,
			A.CODPROSAL,
			CR.CODESPEC1,
			A.CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			ISNULL(S.UFUCODIGO, D.UFUCODIGO) AS UFUCODIGO,
			CR.CODIGONIT as NitMedico,
			CASE
				WHEN sod.Id is not null AND sod.IsDelete =0 THEN sod.Id
				WHEN sod.Id IS NOT NULL AND sod.IsDelete = 1 THEN NULL
				WHEN A.GENSERVICEORDER IS NOT NULL then   A.GENSERVICEORDER
			ELSE NULL END GENSERVICEORDER,
			sod.Id as IdServerOrderDetail,
			CASE WHEN  ESTSERIPS IN ('2','3','4','8') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulado' ELSE 'Estudios No Realizados' END AS Tipo,
			COALESCE(A.MEDREALEC, A.CODPROSAL) as MedicoRealizo,
			A.FECRECEXA as FechaRealizacion,
			CASE WHEN A.MEDREALEC IS NULL THEN 0 ELSE 1 END as Realizo,
			B.DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDIMAG A ON ing.NUMINGRES = A.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 	
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO	
	LEFT JOIN dbo.AGENSALAC S ON A.IdAGENSALAC = S.CODCONCEC
	LEFT JOIN dbo.INUNIFUNC US ON S.UFUCODIGO = US.UFUCODIGO
	LEFT JOIN dbo.INPROFSAL CR ON COALESCE(A.MEDREALEC, A.CODPROSAL)=CR.CODPROSAL 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDIMAG' and acj.EntityTap='INDlcgImagesDX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	left join billing.ServiceOrderDetail sod on A.IdServerOrderDetail = sod.Id
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
UNION ALL
	SELECT	CONCAT('HCORDIMAG', '-', A.AUTO) Id,
			'HCORDIMAG' EntityName,
			A.[AUTO] as Row,
			A.AuthorizationEventId,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,			
			CONCAT(RTRIM(ISNULL(US.UFUCODIGO, D.UFUCODIGO)), ' - ', RTRIM(ISNULL(US.UFUDESCRI, D.UFUDESCRI))) AS UnidadFuncional,						
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			NUMFOLINT AS FolioInterpreta,
			CODPROINT AS MedicoInterpreto,
			INTERPRET AS Interpretacion,
			A.CODPROSAL,
			CR.CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			ISNULL(S.UFUCODIGO, D.UFUCODIGO) AS UFUCODIGO,
			CR.CODIGONIT as NitMedico,
			CASE
				WHEN sod.Id is not null AND sod.IsDelete =0 THEN sod.Id
				WHEN sod.Id IS NOT NULL AND sod.IsDelete = 1 THEN NULL
				WHEN A.GENSERVICEORDER IS NOT NULL then   A.GENSERVICEORDER
			ELSE NULL END GENSERVICEORDER,
			sod.Id as IdServerOrderDetail,
			CASE WHEN  ESTSERIPS IN ('2','3','4','8') THEN 'Estudios Realizados' when ESTSERIPS = '6' then 'Anulado' ELSE 'Estudios No Realizados' END AS Tipo,
			COALESCE(A.MEDREALEC, A.CODPROSAL) as MedicoRealizo,
			A.FECRECEXA as FechaRealizacion,
			CASE WHEN A.MEDREALEC IS NULL THEN 0 ELSE 1 END as Realizo,
			B.DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.HCORDIMAG A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS = B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL = C.CODPROSAL 	
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
	LEFT JOIN dbo.AGENSALAC S ON A.IdAGENSALAC = S.CODCONCEC
	LEFT JOIN dbo.INUNIFUNC US ON S.UFUCODIGO = US.UFUCODIGO
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	LEFT JOIN dbo.INPROFSAL CR ON COALESCE(A.MEDREALEC, A.CODPROSAL)=CR.CODPROSAL
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd ON cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDIMAG' and acj.EntityTap='INDlcgImagesDX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	left join billing.ServiceOrderDetail sod on A.IdServerOrderDetail = sod.Id
	WHERE ING.IESTADOIN = 'C' AND  A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
GO

--NOTA: Despues de realizar algun Alter a la vista ejecutar los EXEC
--Refrescar metadatos: sin esto, [Tipo]=NULL en la vista del bot y Stella no procesa imágenes

--EXEC sp_refreshview 'dbo.VServicesProceduresImagesDx';
--EXEC sp_refreshview 'Billing.BotViewServicesProceduresImagesDX';

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todas las órdenes de imágenes diagnósticas (radiologías, ecografías, tomografías, resonancias y estudios similares) solicitadas en historia clínica, tanto para ingresos regulares de pacientes como para recién nacidos hospitalizados. Integra información del ingreso o admisión del paciente, el servicio CUPS solicitado, el médico solicitante y el médico que realizó el estudio, la unidad funcional o sala donde se ejecutó, el estado del estudio (realizado, no realizado o anulado), la interpretación registrada, los folios asociados y los datos de contrato y facturación (descripción CUPS contractual, justificaciones de control de cuenta y glosa). Sirve como fuente principal para reportería y auditoría de imágenes diagnósticas, liquidación de servicios y seguimiento de órdenes médicas de imagenología por paciente, ingreso y profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresImagesDx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresImagesDx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las órdenes de imágenes diagnósticas con datos de unidad funcional, profesional, contrato y justificaciones de facturación, distinguiendo estudios realizados, no realizados o anulados, tanto del ingreso del paciente como de ingresos de recién nacido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresImagesDx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de imagen debe estar asociada a un servicio CUPS, profesional y unidad funcional existentes (INNER JOIN).; En el segundo bloque, debe existir vínculo de ingreso a través de HCINGRESORECNAC y HCRECINAC (recién nacido).; La justificación se vincula sólo cuando EntityName=''HCORDIMAG'' y EntityTap=''INDlcgImagesDX''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresImagesDx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El médico que realiza se determina por COALESCE(MEDREALEC, CODPROSAL): si no hay ejecutor, se asume el solicitante.; La unidad funcional se prioriza desde la sala de agendamiento (AGENSALAC) y si no existe, se toma la de la orden.; Las órdenes con IsDelete=1 en ServiceOrderDetail no exponen GENSERVICEORDER (se anulan).; Sólo se incluyen órdenes con MANEXTPRO=0 (no manejo externo) o ingresos con TRATAESPECIA=3 (tratamiento especial).; El Id de la fila siempre se construye como ''HCORDIMAG-'' + AUTO.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresImagesDx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas; Estudios realizados/no realizados/anulados; Interpretación radiológica; Unidad funcional; Profesional de salud (solicitante e intérprete); Ingreso/admisión; Recién nacido (ingreso ligado al de la madre); Contrato CUPS y descripciones contractuales; Justificación de control de cuenta / facturación; Liquidación / SkipClearance; Tratamiento especial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresImagesDx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.VServicesProceduresImagesDx: Primer bloque: retorna órdenes de imagen del ingreso cuando A.MANEXTPRO=0 o ING.TRATAESPECIA=3.; [RETURN_RESULT] dbo.VServicesProceduresImagesDx: Segundo bloque: retorna órdenes asociadas a ingreso de recién nacido (HCINGRESORECNAC) cuando ING.IESTADOIN=''C'' AND A.MANEXTPRO=0, o cuando ING.TRATAESPECIA=3.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresImagesDx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS IN (''2'',''3'',''4'') → Tipo = ''Estudios Realizados'' else Si ESTSERIPS=''6'' → ''Anulado''; en otro caso → ''Estudios No Realizados''; si sod.Id IS NOT NULL AND sod.IsDelete=0 → GENSERVICEORDER = sod.Id (orden de servicio vigente) else Si sod.Id IS NOT NULL AND IsDelete=1 → NULL; si A.GENSERVICEORDER IS NOT NULL → A.GENSERVICEORDER; si no, NULL.; si A.MEDREALEC IS NULL → Realizo = 0 (estudio no realizado por médico ejecutor) else Realizo = 1; si acj.Id IS NULL → JustificationCodeName='''' y SkipLiquidation=0 else JustificationCodeName=Code+Description y SkipLiquidation=bjc.SkipClearance', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresImagesDx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDIMAG; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.AGENSALAC; dbo.HCINGRESORECNAC; dbo.HCRECINAC; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.AccountControlJustification; Billing.BillingJustificationControl; billing.ServiceOrderDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresImagesDx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresImagesDx';
GO
