

CREATE VIEW [dbo].[ViewSurgeriesPerformed]
AS
select	 CAST(RA.CONSECUQX AS VARCHAR) CONSECUQXId 
		,RA.CONSECUQX 
		,authorizationData.AuthorizationEventId
		,RA.NUMEFOLIO
		,RA.IPCODPACI
		,RA.NUMINGRES
		,RA.CODCENATE
		,RA.UFUCODIGO
		,RA.CODSERIPS
		,IPS.DESSERIPS
		,QXPRINCIP
		,CANTIDAQX
		,concat(ISNULL(RA.CODVIAABO,abo.CODVIAABO),' - ',ABO.DESVIAABO)  CODVIAABO
		,null as MedicoRealizo
		,inf.FECHORFIN as FechaRealizacion
		,0 as Realizo
		,IIF(RA.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
		,RA.GENSERVICEORDER
		,acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCQXREALI' EntityName,
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
		ISNULL(cd.Id, 0) ContractDescriptionId, 
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		'Procedimientos con Informe QX' AS Tipo
from dbo.HCQXREALI RA
inner join dbo.INCUPSIPS AS IPS on IPS.CODSERIPS = RA.CODSERIPS
inner join [dbo].[HCQXVIABO] ABO WITH(NOLOCK) ON ABO.CODVIAABO = ISNULL(RA.CODVIAABO,abo.CODVIAABO)
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = RA.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON RA.CONSECUQX =acj.EntityId and acj.EntityName='HCQXREALI' and acj.EntityTap='INDlcgProceduresQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id	
LEFT JOIN HCQXINFOR inf WITH(NOLOCK) on RA.IPCODPACI=inf.IPCODPACI and RA.NUMEFOLIO= inf.NUMEFOLIO and RA.NUMINGRES=inf.NUMINGRES
OUTER APPLY (
	SELECT TOP 1 proq.AuthorizationEventId
	FROM dbo.HCORDPROQ proq
	INNER JOIN [Authorization].[AuthorizationEvents] ae ON ae.Id = proq.AuthorizationEventId
	WHERE proq.NUMINGRES = RA.NUMINGRES
		AND proq.CODSERIPS = RA.CODSERIPS
		AND ae.Status = 4
	ORDER BY ae.AuthorizationDate DESC, ae.CreationDate DESC, ae.Id DESC
) authorizationData

union all 

select	CAST(CONSECUQX AS VARCHAR) CONSECUQXId,
		CONSECUQX,
		authorizationData.AuthorizationEventId,
		a.NUMEFOLIO,
		a.IPCODPACI,
		INGMH.NUMINGRES,
		a.CODCENATE
		,a.UFUCODIGO,
		IPS.CODSERIPS,
		IPS.DESSERIPS,
		QXPRINCIP,
		CANTIDAQX,
		concat(ISNULL(a.CODVIAABO,abo.CODVIAABO),' - ',ABO.DESVIAABO)  CODVIAABO
		,null as MedicoRealizo
		,inf.FECHORFIN as FechaRealizacion
		,0 as Realizo
		,IIF(a.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione
		,a.GENSERVICEORDER
		,acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCQXREALI' EntityName,
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
		ISNULL(cd.Id, 0) ContractDescriptionId, 
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		'Procedimientos con Informe QX' AS Tipo
from dbo.HCQXREALI a
inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
inner join dbo.INCUPSIPS AS IPS on IPS.CODSERIPS = a.CODSERIPS
inner join [dbo].[HCQXVIABO] ABO WITH(NOLOCK) ON ABO.CODVIAABO = ISNULL(a.CODVIAABO,abo.CODVIAABO)
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON a.CONSECUQX =acj.EntityId and acj.EntityName='HCQXREALI' and acj.EntityTap='INDlcgProceduresQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
LEFT JOIN HCQXINFOR inf WITH(NOLOCK) on a.IPCODPACI=inf.IPCODPACI and a.NUMEFOLIO= inf.NUMEFOLIO and a.NUMINGRES=inf.NUMINGRES
OUTER APPLY (
	SELECT TOP 1 proq.AuthorizationEventId
	FROM dbo.HCORDPROQ proq
	INNER JOIN [Authorization].[AuthorizationEvents] ae ON ae.Id = proq.AuthorizationEventId
	WHERE proq.NUMINGRES = a.NUMINGRES
		AND proq.CODSERIPS = a.CODSERIPS
		AND ae.Status = 4
	ORDER BY ae.AuthorizationDate DESC, ae.CreationDate DESC, ae.Id DESC
) authorizationData

UNION ALL

select	 CONCAT(RA.CONSECUQX,'-',1) CONSECUQXId
		,RA.CONSECUQX 
		,authorizationData.AuthorizationEventId
		,RA.NUMEFOLIO
		,RA.IPCODPACI
		,RA.NUMINGRES
		,RA.CODCENATE
		,RA.UFUCODIGO
		,RA.CODSERIPS
		,IPS.DESSERIPS
		,QXPRINCIP
		,CANTIDAQX
		,concat(ISNULL(RA.CODVIAABO,abo.CODVIAABO),' - ',ABO.DESVIAABO)  CODVIAABO
		,null as MedicoRealizo
		,inf.FECHORFIN as FechaRealizacion
		,0 as Realizo
		,IIF(RA.GENSERVICEORDER2 IS NULL, 0, 1) AS Seleccione
		,RA.GENSERVICEORDER2
		,acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCQXREALI' EntityName,
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
		ISNULL(cd.Id, 0) ContractDescriptionId, 
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		'Procedimientos con Informe QX' AS Tipo
from dbo.HCQXREALI RA
inner join dbo.INCUPSIPS AS IPS on IPS.CODSERIPS = RA.CODSERIPS
inner join [dbo].[HCQXVIABO] ABO WITH(NOLOCK) ON ABO.CODVIAABO = ISNULL(RA.CODVIAABO,abo.CODVIAABO)
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = RA.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON RA.CONSECUQX =acj.EntityId and acj.EntityName='HCQXREALI' and acj.EntityTap='INDlcgProceduresQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id	
LEFT JOIN HCQXINFOR inf WITH(NOLOCK) on RA.IPCODPACI=inf.IPCODPACI and RA.NUMEFOLIO= inf.NUMEFOLIO and RA.NUMINGRES=inf.NUMINGRES
OUTER APPLY (
	SELECT TOP 1 proq.AuthorizationEventId
	FROM dbo.HCORDPROQ proq
	INNER JOIN [Authorization].[AuthorizationEvents] ae ON ae.Id = proq.AuthorizationEventId
	WHERE proq.NUMINGRES = RA.NUMINGRES
		AND proq.CODSERIPS = RA.CODSERIPS
		AND ae.Status = 4
	ORDER BY ae.AuthorizationDate DESC, ae.CreationDate DESC, ae.Id DESC
) authorizationData
WHERE ABO.CODVIAABO ='02'

union all 

select	CONCAT(CONSECUQX,'-',2) CONSECUQXId,
		CONSECUQX ,
		authorizationData.AuthorizationEventId,
		a.NUMEFOLIO,
		a.IPCODPACI,
		INGMH.NUMINGRES,
		a.CODCENATE
		,a.UFUCODIGO,
		IPS.CODSERIPS,
		IPS.DESSERIPS,
		QXPRINCIP,
		CANTIDAQX,
		concat(ISNULL(a.CODVIAABO,abo.CODVIAABO),' - ',ABO.DESVIAABO)  CODVIAABO
		,null as MedicoRealizo
		,inf.FECHORFIN as FechaRealizacion
		,0 as Realizo
		,IIF(a.GENSERVICEORDER2 IS NULL, 0, 1) AS Seleccione
		,a.GENSERVICEORDER2
		,acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
		'HCQXREALI' EntityName,
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
		ISNULL(cd.Id, 0) ContractDescriptionId, 
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		'Procedimientos con Informe QX' AS Tipo
from dbo.HCQXREALI a
inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
inner join dbo.INCUPSIPS AS IPS on IPS.CODSERIPS = a.CODSERIPS
inner join [dbo].[HCQXVIABO] ABO WITH(NOLOCK) ON ABO.CODVIAABO = ISNULL(a.CODVIAABO,abo.CODVIAABO)
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON a.CONSECUQX =acj.EntityId and acj.EntityName='HCQXREALI' and acj.EntityTap='INDlcgProceduresQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
LEFT JOIN HCQXINFOR inf WITH(NOLOCK) on a.IPCODPACI=inf.IPCODPACI and a.NUMEFOLIO= inf.NUMEFOLIO and a.NUMINGRES=inf.NUMINGRES
OUTER APPLY (
	SELECT TOP 1 proq.AuthorizationEventId
	FROM dbo.HCORDPROQ proq
	INNER JOIN [Authorization].[AuthorizationEvents] ae ON ae.Id = proq.AuthorizationEventId
	WHERE proq.NUMINGRES = a.NUMINGRES
		AND proq.CODSERIPS = a.CODSERIPS
		AND ae.Status = 4
	ORDER BY ae.AuthorizationDate DESC, ae.CreationDate DESC, ae.Id DESC
) authorizationData
WHERE ABO.CODVIAABO ='02'

UNION ALL

	SELECT  CAST(A.AUTO AS VARCHAR) CONSECUQXId,
			A.AUTO CONSECUQX,
			A.AuthorizationEventId,
			A.NUMEFOLIO,
			A.IPCODPACI,
			A.NUMINGRES,
			A.CODCENATE,
			A.UFUCODIGO,
			ISNULL(MEN.CODSERIPS, A.CODSERIPS) AS CODSERIPS,
			ISNULL(RTRIM(cupsInf.DESSERIPS), B.DESSERIPS) AS DESSERIPS,
			0 QXPRINCIP,
			SUM(A.CANSERIPS) CANTIDAQX,
			'' CODVIAABO,
			NULL MedicoRealizo,
			Men.FECREAPRO FechaRealizacion,
			0 as Realizo,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			A.GENSERVICEORDER,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			'HCORDPROQ' EntityName,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			'Procedimientos Menores' AS Tipo
	FROM dbo.ADINGRESO ing
	JOIN dbo.HCORDPROQ A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI 
	JOIN dbo.HCINFPROM AS Men ON A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES And A.NUMEFOLIO = Men.NUMEFOLIO and A.CODSERIPS=Men.CODSERIPS
	LEFT JOIN dbo.INCUPSIPS cupsInf ON cupsInf.CODSERIPS = men.CODSERIPS
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPROQ' and acj.EntityTap='INDlcgProceduresQX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3 
	GROUP BY	Men.CODSERIPS,A.CODSERIPS,cupsInf.DESSERIPS,B.DESSERIPS,CODCONCEC,A.IPCODPACI, A.NUMINGRES,A.NUMEFOLIO,A.GENSERVICEORDER,i.IPNOMCOMP,
				cecd.Id, cd.Id, cd.Code, cd.Name,A.AUTO,A.AuthorizationEventId,A.CODCENATE,A.UFUCODIGO,Men.FECREAPRO,acj.Id,acj.CreationUser,
				acj.CreationDate,acj.JustificationId,bjc.Code,bjc.[Description],bjc.SkipClearance

UNION ALL
	
	SELECT	CAST(A.AUTO AS VARCHAR) CONSECUQXId,
			A.AUTO CONSECUQX,
			A.AuthorizationEventId,
			A.NUMEFOLIO,
			A.IPCODPACI,
			INGMH.NUMINGRES,
			A.CODCENATE,
			A.UFUCODIGO,
			ISNULL(MEN.CODSERIPS, A.CODSERIPS) AS CODSERIPS,
			ISNULL(RTRIM(cupsInf.DESSERIPS), B.DESSERIPS) AS DESSERIPS,
			0 QXPRINCIP,
			SUM(A.CANSERIPS) AS CANTIDAQX,
			'' CODVIAABO,
			NULL MedicoRealizo,
			Men.FECREAPRO FechaRealizacion,
			0 as Realizo,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			A.GENSERVICEORDER,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation,
			'HCORDPROQ' EntityName,	
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId, 
			ISNULL(cd.Id, 0) ContractDescriptionId, 
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			'Procedimientos Menores' AS Tipo
	FROM dbo.HCORDPROQ A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
	JOIN dbo.HCINFPROM AS Men ON A.IPCODPACI=Men.IPCODPACI AND A.NUMINGRES = Men.NUMINGRES And A.NUMEFOLIO = Men.NUMEFOLIO and A.CODSERIPS=Men.CODSERIPS
	LEFT JOIN dbo.INCUPSIPS cupsInf ON cupsInf.CODSERIPS = men.CODSERIPS
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPROQ' and acj.EntityTap='INDlcgProceduresQX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id	
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
	GROUP BY MEN.CODSERIPS,A.CODSERIPS,B.DESSERIPS,cupsInf.DESSERIPS,CODCONCEC,A.IPCODPACI, INGMH.NUMINGRES,A.NUMEFOLIO,A.GENSERVICEORDER, cecd.Id, cd.Id, cd.Code, cd.Name,
				A.AUTO,A.AuthorizationEventId,	A.IPCODPACI, A.CODCENATE, A.UFUCODIGO,Men.FECREAPRO,acj.Id,acj.CreationUser,acj.CreationDate
				,acj.CreationDate,acj.JustificationId,bjc.Code,bjc.[Description],bjc.SkipClearance
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todos los procedimientos quirúrgicos realizados a pacientes durante sus ingresos hospitalarios, incluyendo cirugías estándar y procedimientos de recién nacidos. Integra el registro de procedimientos quirúrgicos (HCQXREALI) con el catálogo de servicios CUPS (INCUPSIPS), las vías de administración (HCQXVIABO), el informe quirúrgico (HCQXINFOR) para obtener la fecha de finalización de la cirugía, y los conceptos de contrato (CUPSEntityContractDescriptions, ContractDescriptions) para determinar cómo se factura cada procedimiento. Adicionalmente incorpora las justificaciones de control de glosas y facturación (AccountControlJustification, BillingJustificationControl) que permiten sustentar o eximir un procedimiento del proceso de liquidación. Sirve para reportería quirúrgica, control de facturación de procedimientos en quirófano, auditoría de glosas y seguimiento de órdenes de servicio generadas, cubriendo tanto el ingreso principal del paciente como ingresos de recién nacidos vinculados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewSurgeriesPerformed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewSurgeriesPerformed';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista los procedimientos quirúrgicos realizados (con informe QX) y los procedimientos menores asociados a un ingreso, incluyendo casos de recién nacidos y datos de justificación de facturación y contrato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los procedimientos quirúrgicos en HCQXREALI deben tener una vía de abordaje válida en HCQXVIABO y un código CUPS existente en INCUPSIPS.; Para incluir procedimientos del recién nacido se requiere el vínculo madre-hijo en HCINGRESORECNAC y registro en HCRECINAC.; Los procedimientos menores (HCORDPROQ) deben tener un informe asociado en HCINFPROM por paciente, ingreso, folio y CUPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todas las filas exponen el literal ''Procedimientos con Informe QX'' o ''Procedimientos Menores'' como Tipo según su origen (HCQXREALI vs HCORDPROQ).; EntityName siempre es ''HCQXREALI'' para los bloques de cirugía y ''HCORDPROQ'' para procedimientos menores.; El emparejamiento con justificaciones de facturación siempre se hace con EntityTap=''INDlcgProceduresQX''.; Los procedimientos menores se entregan agregados (SUM de CANSERIPS) por las dimensiones del GROUP BY.; QXPRINCIP siempre se reporta como 0 para procedimientos menores.; Los IDs de descripción de contrato se devuelven como 0 (y código/nombre vacío) cuando no existe relación en CUPSEntityContractDescriptions/ContractDescriptions.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Procedimiento quirúrgico; Informe quirúrgico; Procedimiento menor; Vía de abordaje; Ingreso hospitalario; Recién nacido; CUPS; Orden de servicio; Justificación de facturación; Liquidación / SkipClearance; Descripción de contrato; Folio clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Devuelve cada procedimiento quirúrgico de HCQXREALI marcado como ''Procedimientos con Informe QX'', uniendo además los procedimientos asociados al ingreso del recién nacido a través de HCINGRESORECNAC.; [RETURN_RESULT] ResultSet: Cuando la vía de abordaje (HCQXVIABO.CODVIAABO) es ''02'', se duplica la fila del procedimiento usando GENSERVICEORDER2 y un CONSECUQXId con sufijo ''-1'' o ''-2'', para representar la segunda orden de servicio.; [RETURN_RESULT] ResultSet: Devuelve procedimientos menores de HCORDPROQ etiquetados como ''Procedimientos Menores'' solo cuando MANEXTPRO = 0 o el ingreso tiene TRATAESPECIA = 3, agrupando cantidades con SUM(CANSERIPS).; [RETURN_RESULT] ResultSet: Si existe registro en Billing.AccountControlJustification (acj.Id no nulo) para la entidad (''HCQXREALI'' o ''HCORDPROQ'') y EntityTap=''INDlcgProceduresQX'', se expone el código y descripción de la justificación y el flag SkipLiquidation; en caso contrario, se exponen vacío y 0.; [RETURN_RESULT] ResultSet: Marca Seleccione=1 cuando el procedimiento ya tiene una orden de servicio generada (GENSERVICEORDER/GENSERVICEORDER2 no nulo); de lo contrario 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ABO.CODVIAABO = ''02'' (vía de abordaje específica) → Se generan filas adicionales con GENSERVICEORDER2 y CONSECUQXId concatenado con ''-1'' o ''-2'', representando una segunda orden quirúrgica para esa vía. else Solo se entrega la fila base con GENSERVICEORDER.; si HCORDPROQ.MANEXTPRO = 0 OR ADINGRESO.TRATAESPECIA = 3 → El procedimiento menor se incluye en el resultado. else El procedimiento menor se excluye.; si RA.CODVIAABO IS NULL → Se utiliza la vía de abordaje del catálogo (ABO.CODVIAABO) como valor por defecto al unir y mostrar. else Se utiliza la vía de abordaje registrada en el procedimiento.; si acj.Id IS NULL (sin justificación de control de cuenta) → JustificationCodeName se devuelve vacío y SkipLiquidation = 0. else Se devuelve el código y descripción de la justificación junto con su flag SkipClearance.; si MEN.CODSERIPS no nulo en procedimientos menores → Se usa el CUPS y descripción del informe (HCINFPROM) en lugar del CUPS original de HCORDPROQ. else Se usa el CUPS y descripción de HCORDPROQ/INCUPSIPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXREALI; dbo.INCUPSIPS; dbo.HCQXVIABO; dbo.HCQXINFOR; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO; dbo.HCORDPROQ; dbo.INPACIENT; dbo.HCINFPROM; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.AccountControlJustification; Billing.BillingJustificationControl', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgeriesPerformed';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgeriesPerformed';
GO
