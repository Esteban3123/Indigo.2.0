

CREATE VIEW [dbo].[ViewProceduresQx]
AS
	SELECT  ISNULL(MEN.CODSERIPS, A.CODSERIPS) AS CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			A.IPCODPACI, 
			A.NUMINGRES,
			CASE WHEN CODCONCEC IS NULL THEN 'Procedimientos Solicitados' WHEN NOT CODCONCEC IS NULL THEN 'Procedimientos Menores' END AS Tipo,
			SUM(A.CANSERIPS) AS TOTAL,
			ISNULL(RTRIM(cupsInf.DESSERIPS), B.DESSERIPS) AS DESSERIPS,
			A.NUMEFOLIO as NUMEFOLIO,
			0 as IsMultiple,
			A.GENSERVICEORDER,
			i.IPNOMCOMP  as PersonName, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDPROQ A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	LEFT JOIN dbo.HCINFPROM AS Men ON A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES And A.NUMEFOLIO = Men.NUMEFOLIO --A.CODSERIPS=Men.CODSERIPS and 
	LEFT JOIN dbo.INCUPSIPS cupsInf ON cupsInf.CODSERIPS = men.CODSERIPS
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE A.ESTSERIPS NOT IN (3,4,5) AND (A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3)
	GROUP BY ISNULL(MEN.CODSERIPS, A.CODSERIPS), 
				A.GENSERVICEORDER,
				A.IPCODPACI, 
				A.NUMINGRES,
				CODCONCEC,
				ISNULL(RTRIM(cupsInf.DESSERIPS), B.DESSERIPS),
				A.NUMEFOLIO,
				i.IPNOMCOMP, 
				cecd.Id, 
				cd.Id, 
				cd.Code, 
				cd.Name
UNION ALL
	SELECT	A.CODSERIPS,
			--case when A.GENSERVICEORDER is null then cast(0 as tinyint) else cast(1 as tinyint) end as Seleccione, 
			0 AS Seleccione,
			A.IPCODPACI, 
			A.NUMINGRES,
			'Procedimientos con Informe QX' AS Tipo,
			SUM(CANTIDAQX) AS TOTAL,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			A.NUMEFOLIO as NUMEFOLIO,
			case when (select count(*) from dbo.HCQXREALI where A.IPCODPACI=IPCODPACI AND A.NUMINGRES=NUMINGRES AND A.NUMEFOLIO=NUMEFOLIO) > 1 then 1 else 0 end as IsMultiple,
			A.GENSERVICEORDER,
			i.IPNOMCOMP  as PersonName, 
			0 CUPSEntityContractDescriptionId, 
			0 ContractDescriptionId, 
			'' ContractDescriptionCodeName
	FROM dbo.HCQXINFOR A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	JOIN dbo.HCQXREALI C ON A.IPCODPACI=C.IPCODPACI AND A.NUMINGRES=C.NUMINGRES AND A.NUMEFOLIO=C.NUMEFOLIO --AND A.CODSERIPS=C.CODSERIPS 
	GROUP BY A.CODSERIPS,B.DESSERIPS,A.IPCODPACI, A.NUMINGRES,A.NUMEFOLIO,A.GENSERVICEORDER,i.IPNOMCOMP
UNION ALL
	SELECT	ISNULL(MEN.CODSERIPS, A.CODSERIPS) AS CODSERIPS,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			A.IPCODPACI, 
			INGMH.NUMINGRES,
			CASE WHEN CODCONCEC IS NULL THEN 'Procedimientos Solicitados' WHEN NOT CODCONCEC IS NULL THEN 'Procedimientos Menores' END AS Tipo,
			SUM(A.CANSERIPS) AS TOTAL,
			ISNULL(RTRIM(cupsInf.DESSERIPS), B.DESSERIPS) AS DESSERIPS,
			A.NUMEFOLIO as NUMEFOLIO,
			0 as IsMultiple,
			A.GENSERVICEORDER,'
			Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName, 
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName
	FROM dbo.HCORDPROQ A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	LEFT JOIN dbo.HCINFPROM AS Men ON A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES And A.NUMEFOLIO = Men.NUMEFOLIO --A.CODSERIPS=Men.CODSERIPS and 
	LEFT JOIN dbo.INCUPSIPS cupsInf ON cupsInf.CODSERIPS = men.CODSERIPS
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	WHERE  ING.IESTADOIN = 'C' AND A.ESTSERIPS NOT IN (3,4,5) AND (A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3)
	GROUP BY ISNULL(MEN.CODSERIPS, A.CODSERIPS),
			A.IPCODPACI,
			INGMH.NUMINGRES,
			CODCONCEC,
			ISNULL(RTRIM(cupsInf.DESSERIPS), B.DESSERIPS),
			A.NUMEFOLIO,
			A.GENSERVICEORDER,
			rn.NUMHIJREG,
			cecd.Id, 
			cd.Id, 
			cd.Code, 
			cd.Name
UNION ALL
	SELECT	A.CODSERIPS,
			--case when A.GENSERVICEORDER is null then cast(0 as tinyint) else cast(1 as tinyint) end as Seleccione, 
			0 AS Seleccione,
			A.IPCODPACI, 
			INGMH.NUMINGRES,
			'Procedimientos con Informe QX' AS Tipo,
			SUM(CANTIDAQX) AS TOTAL,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			A.NUMEFOLIO as NUMEFOLIO,
			case when (select count(*) from dbo.HCQXREALI where A.IPCODPACI=IPCODPACI AND INGMH.NUMINGRES=NUMINGRES AND A.NUMEFOLIO=NUMEFOLIO) > 1 then 1 else 0 end as IsMultiple,
			A.GENSERVICEORDER,
			'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName, 
			0 CUPSEntityContractDescriptionId, 
			0 ContractDescriptionId, 
			'' ContractDescriptionCodeName
	FROM dbo.HCQXINFOR A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	JOIN dbo.HCQXREALI C ON A.IPCODPACI=C.IPCODPACI AND A.NUMINGRES=C.NUMINGRES AND A.NUMEFOLIO=C.NUMEFOLIO --AND A.CODSERIPS=C.CODSERIPS 
	WHERE ING.IESTADOIN = 'C' 
	GROUP BY A.CODSERIPS,B.DESSERIPS,A.IPCODPACI, INGMH.NUMINGRES,A.NUMEFOLIO,A.GENSERVICEORDER,rn.NUMHIJREG
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todos los procedimientos quirúrgicos y servicios de salud ordenados o realizados para un paciente durante un ingreso, integrando órdenes de procedimientos (HCORDPROQ), informes quirúrgicos (HCQXINFOR) y hallazgos clínicos (HCINFPROM). Clasifica cada procedimiento según su tipo: ''Procedimientos Solicitados'', ''Procedimientos Menores'' o ''Procedimientos con Informe QX'', e incluye el código y descripción del servicio CUPS, el nombre del paciente, el número de ingreso, la cantidad total solicitada o realizada, y la descripción del concepto de contrato asociado para facturación. También contempla procedimientos vinculados a recién nacidos, mostrando el ingreso de la madre pero identificando al hijo como persona. Se usa para consultar, visualizar y facturar los procedimientos quirúrgicos de un episodio de atención, incluyendo la trazabilidad contractual y la identificación de procedimientos múltiples.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewProceduresQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewProceduresQx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista los procedimientos quirúrgicos asociados a un ingreso (solicitados, menores y con informe QX), tanto del paciente principal como de recién nacidos vinculados a la madre, para gestión y selección de órdenes de servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de ingresos en ADINGRESO con órdenes en HCORDPROQ o informes en HCQXINFOR.; Para procedimientos del recién nacido, debe existir vínculo madre-hijo en HCINGRESORECNAC y registro en HCRECINAC, y el ingreso del hijo debe estar en estado ''C''.; Los códigos de servicio deben existir en INCUPSIPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca incluye órdenes con ESTSERIPS = 4.; Nunca incluye procedimientos con MANEXTPRO distinto de 0 salvo que el ingreso tenga TRATAESPECIA = 3.; Para procedimientos de recién nacidos solo se consideran ingresos del hijo en estado ''C''.; Si existe informe de procedimiento menor (HCINFPROM), prevalecen su código y descripción sobre los de la orden original.; Las cantidades se totalizan (SUM) por agrupación de paciente, ingreso, folio, servicio y orden de servicio.; Los procedimientos del recién nacido se etiquetan en PersonName como ''Hijo N'' usando NUMHIJREG.; Procedimientos provenientes de informe QX siempre llevan Seleccione = 0 y no exponen descripción de contrato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión hospitalaria; Procedimiento quirúrgico; Procedimientos solicitados; Procedimientos menores; Informe quirúrgico (QX); Orden de servicio; Folio de atención; CUPS; Descripción de contrato; Recién nacido / vínculo madre-hijo; Tratamiento especial; Estado del ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Vista de solo lectura: retorna filas agrupadas por código de servicio, ingreso, paciente y folio sin modificar datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CODCONCEC IS NULL en HCORDPROQ → Clasifica la fila como ''Procedimientos Solicitados'' else Clasifica como ''Procedimientos Menores''; si Existe registro en HCINFPROM (informe de procedimiento menor) para el paciente/ingreso/folio → Sustituye el código y descripción del servicio por los del informe (MEN.CODSERIPS / cupsInf.DESSERIPS) else Conserva el código y descripción originales de HCORDPROQ/INCUPSIPS; si A.GENSERVICEORDER IS NULL → Marca Seleccione = 0 (no seleccionable) else Marca Seleccione = 1; si Para informes QX, existe más de un registro en HCQXREALI para el mismo paciente/ingreso/folio → Marca IsMultiple = 1 else IsMultiple = 0; si A.ESTSERIPS = 4 → Excluye la orden del resultado (procedimiento anulado/cancelado); si A.MANEXTPRO <> 0 y TRATAESPECIA <> 3 → Excluye la orden (procedimiento manejado externamente y no es tratamiento especial tipo 3); si Para ingresos de recién nacido, ING.IESTADOIN <> ''C'' → Excluye los procedimientos del hijo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCORDPROQ; dbo.INCUPSIPS; dbo.INPACIENT; dbo.HCINFPROM; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; dbo.HCQXINFOR; dbo.HCQXREALI; dbo.HCINGRESORECNAC; dbo.HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewProceduresQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewProceduresQx';
GO
