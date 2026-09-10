CREATE VIEW [dbo].[VServicesProceduresPathologies]
AS
	SELECT	CONCAT('HCORDPATO', '-', A.AUTO) Id,
			'HCORDPATO' EntityName,
			A.AUTO AS Row,
			A.AuthorizationEventId,
			A.CODSERIPS, 
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			NUMFOLINT AS FolioInterpreta,
			CODPROINT AS MedicoInterpreto,
			INTERPRET AS Interpretacion,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			CASE 
				WHEN ESTSERIPS IN ('2','3','4') THEN 'Realizados' 
				when ESTSERIPS = '6' then 'Anulados'
				when ESTSERIPS = '1' then 'Solicitados'
				ELSE 'No Realizados' 
			END AS Tipo,
			CASE WHEN A.PROFRESULTADO IS NULL THEN A.CODPROSAL ELSE A.PROFRESULTADO END as MedicoRealizo,
			CASE WHEN A.PROFRESULTADO IS NULL THEN COALESCE(A.FECRECEPMUES,A.FECORDMED) ELSE A.FECHARESULT END as FechaRealizacion,
			CASE WHEN A.PROFRESULTADO IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			i.IPNOMCOMP  as PersonName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDPATO A ON ing.NUMINGRES = A.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPATO' and acj.EntityTap='INDlcgPathology'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
UNION ALL
	SELECT	CONCAT('HCORDPATO', '-', A.AUTO) Id,
			'HCORDPATO' EntityName,
			A.AUTO AS Row,
			A.AuthorizationEventId,
			A.CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			NUMFOLINT AS FolioInterpreta,
			CODPROINT AS MedicoInterpreto,
			INTERPRET AS Interpretacion,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			CASE 
				WHEN ESTSERIPS IN ('2','3','4') THEN 'Realizados' 
				when ESTSERIPS = '6' then 'Anulados'
				when ESTSERIPS = '1' then 'Solicitados'
				ELSE 'No Realizados' END 
			AS Tipo,
			CASE WHEN A.PROFRESULTADO IS NULL THEN A.CODPROSAL ELSE A.PROFRESULTADO END as MedicoRealizo,
			CASE WHEN A.PROFRESULTADO IS NULL THEN COALESCE(A.FECRECEPMUES,A.FECORDMED) ELSE A.FECHARESULT END as FechaRealizacion,
			CASE WHEN A.PROFRESULTADO IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,		
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.HCORDPATO A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPATO' and acj.EntityTap='INDlcgPathology'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE ING.IESTADOIN = 'C' AND A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consolida todas las órdenes médicas de patología e imágenes diagnósticas (exámenes de laboratorio, estudios de imagen y procedimientos diagnósticos) asociadas a ingresos de pacientes, tanto para pacientes adultos como para recién nacidos. Integra datos del ingreso (ADINGRESO), la orden de patología (HCORDPATO), el servicio CUPS (INCUPSIPS), el profesional solicitante (INPROFSAL), la unidad funcional o servicio donde se ordenó (INUNIFUNC), los datos del paciente (INPACIENT), la descripción de contrato asociada al CUPS (CUPSEntityContractDescriptions, ContractDescriptions) y las justificaciones de control de cuenta o glosa (AccountControlJustification, BillingJustificationControl). Sirve para consultar el estado de las órdenes de patología e imágenes (solicitadas, realizadas, anuladas, no realizadas), quién las ordenó, quién las ejecutó, la fecha de realización, el folio, la interpretación, los conceptos de facturación por contrato y las justificaciones de glosa o control de liquidación, siendo el insumo principal para la gestión de resultados diagnósticos, facturación de servicios y auditoría de cuentas médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresPathologies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresPathologies';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las órdenes de patología/imágenes diagnósticas de un ingreso (propio o de recién nacido vinculado a la madre) con datos del paciente, profesional, unidad funcional, contrato y justificaciones de facturación, clasificando su estado y quién las realizó.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de patología debe estar relacionada con un ingreso existente (propio o de recién nacido a través de HCINGRESORECNAC).; Deben existir registros maestros del CUPS, profesional de salud, unidad funcional y paciente referenciados por la orden.; Para la rama de recién nacido, el ingreso debe estar en estado ''C'' (cerrado/egresado) según IESTADOIN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Identificador lógico siempre se construye como ''HCORDPATO-'' + AUTO y EntityName = ''HCORDPATO''.; Las justificaciones consultadas siempre corresponden a EntityName=''HCORDPATO'' y EntityTap=''INDlcgPathology''.; Se excluyen procedimientos manejados externamente (MANEXTPRO=1) salvo cuando el ingreso es tratamiento especial tipo 3 (TRATAESPECIA=3).; Cuando no existe descripción de contrato vinculada, los Ids de contrato se devuelven como 0 y el nombre como cadena vacía.; La fecha de realización nunca queda nula: si no hay fecha de resultado, usa recepción de muestra y, en su defecto, fecha de orden médica.; La segunda rama solo aplica a órdenes asociadas a ingresos de recién nacidos vinculados al ingreso de la madre.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de patología; Ingreso/Admisión hospitalaria; Recién nacido vinculado a la madre; Profesional de la salud; Unidad funcional; Paciente; CUPS; Descripción de contrato; Justificación de control de cuenta; Justificación de facturación / glosa; Estado de servicio (Solicitado, Realizado, Anulado, No Realizado); Folio de interpretación; Tratamiento especial; Procedimiento manejado externamente; Liquidación / SkipClearance', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve unión de órdenes de patología del ingreso del paciente y, adicionalmente, las órdenes asociadas al ingreso de un recién nacido vinculado a la madre vía HCINGRESORECNAC/HCRECINAC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS IN (''2'',''3'',''4'') → Tipo = ''Realizados'' else evaluar siguiente caso; si ESTSERIPS = ''6'' → Tipo = ''Anulados''; si ESTSERIPS = ''1'' → Tipo = ''Solicitados''; si ESTSERIPS distinto de 1,2,3,4,6 → Tipo = ''No Realizados''; si A.PROFRESULTADO IS NULL → MedicoRealizo = CODPROSAL, FechaRealizacion = COALESCE(FECRECEPMUES, FECORDMED), Realizo = 0 else MedicoRealizo = PROFRESULTADO, FechaRealizacion = FECHARESULT, Realizo = 1; si A.GENSERVICEORDER IS NULL → Seleccione = 0 else Seleccione = 1; si acj.Id IS NULL → JustificationCodeName = '''' y SkipLiquidation = 0 else JustificationCodeName = Code + '' - '' + Description y SkipLiquidation = bjc.SkipClearance; si Primera rama: A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA,0) = 3 → Incluye la orden con datos del paciente (IPNOMCOMP); si Segunda rama: ING.IESTADOIN = ''C'' AND A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA,0) = 3 → Incluye la orden mostrando PersonName como ''Hijo N'' usando NUMHIJREG del recién nacido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDPATO; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.INPACIENT; dbo.HCINGRESORECNAC; dbo.HCRECINAC; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.AccountControlJustification; Billing.BillingJustificationControl', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresPathologies';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresPathologies';
GO
