
CREATE VIEW [dbo].[VServicesProceduresNoQx]
AS
	SELECT	CONCAT('HCORDPRON', '-', A.AUTO) Id,
			'HCORDPRON' EntityName,
			A.[AUTO] as Row,
			A.AuthorizationEventId,
			A.CODSERIPS,
			--case when A.GENSERVICEORDER is null then CAST(0 as BIT) else CAST(1 as BIT) end AS Seleccione,
			--CAST(0 AS BIT) AS Seleccione,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			A.NUMINGRES,
			A.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER,
			case when A.ESTSERIPS = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
			COALESCE((	SELECT TOP 1 CODPROSAL 
						FROM dbo.HCINFPROM 
						where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC),A.MEDREALI, A.CODPROSAL) as MedicoRealizo,
			CASE WHEN A.ESTSERIPS = '5' THEN A.Fechaanulado
			ELSE
			COALESCE((SELECT TOP 1 FECREAPRO 
						FROM dbo.HCINFPROM 
						where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES AND NUMEFOLIO = A.NUMEFOLIO ORDER BY FECREAPRO ASC),ISNULL(A.FECHREALI, A.FECHAPRO), A.FECORDMED)
			END as FechaRealizacion,
			CASE WHEN (SELECT TOP 1 CODPROSAL FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC) IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
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
	JOIN dbo.HCORDPRON A ON ing.NUMINGRES = a.NUMINGRES
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPRON' and acj.EntityTap='INDlcgProceduresNoQX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3
UNION ALL
	SELECT CONCAT('HCORDPRON', '-', A.AUTO) Id,
			'HCORDPRON' EntityName,
			A.[AUTO] as Row,
			A.AuthorizationEventId,
			A.CODSERIPS, 
			--case when A.GENSERVICEORDER is null then CAST(0 as BIT) else CAST(1 as BIT) end AS Seleccione,
			--CAST(0 AS BIT) AS Seleccione,
			IIF(A.GENSERVICEORDER IS NULL, 0, 1) AS Seleccione,
			CONCAT(RTRIM(D.UFUCODIGO), ' - ', RTRIM(D.UFUDESCRI)) AS UnidadFuncional,
			CONCAT(RTRIM(C.CODIGONIT), ' - ', RTRIM(C.NOMMEDICO)) AS Medico,
			A.OBSSERIPS AS Observacion,
			A.FECORDMED AS Fecha,
			A.NUMEFOLIO AS Folio,
			A.CODPROSAL,
			CODESPEC1,
			CANSERIPS,
			INGMH.NUMINGRES,
			a.IPCODPACI,
			D.UFUCODIGO,
			C.CODIGONIT as NitMedico,
			A.GENSERVICEORDER, 
			case when A.ESTSERIPS = '1' then 'No Realizados' when A.ESTSERIPS = '5' then 'Anulados' else 'Realizados' end as Tipo,
			COALESCE((SELECT TOP 1 CODPROSAL 
					FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC),A.MEDREALI, A.CODPROSAL) as MedicoRealizo,
			CASE WHEN A.ESTSERIPS = '5' THEN A.Fechaanulado
			ELSE
			COALESCE((SELECT TOP 1 FECREAPRO 
					FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES AND NUMEFOLIO = A.NUMEFOLIO ORDER BY FECREAPRO ASC),ISNULL(A.FECHREALI, A.FECHAPRO), A.FECORDMED) 
			END as FechaRealizacion,
			CASE WHEN (SELECT TOP 1 CODPROSAL FROM dbo.HCINFPROM where IPCODPACI = A.IPCODPACI AND CODSERIPS = A.CODSERIPS AND NUMINGRES = A.NUMINGRES ORDER BY FECREAPRO ASC) IS NULL THEN 0 ELSE 1 END as Realizo,
			RTRIM(B.DESSERIPS) AS DESSERIPS,
			ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
			ISNULL(cd.Id, 0) ContractDescriptionId,
			ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
			acj.Id ACJustificationId,
			acj.CreationUser ACCreationUser,
			acj.CreationDate ACCreationDate,
			acj.JustificationId,		
			iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
			iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
	FROM dbo.HCORDPRON A 
	JOIN dbo.INCUPSIPS B ON A.CODSERIPS=B.CODSERIPS 
	JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
	JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	JOIN dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO
	JOIN dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
	JOIN dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO  
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = a.IDDESCRIPCIONRELACIONADA
	LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
	LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON A.AUTO =acj.EntityId and acj.EntityName='HCORDPRON' and acj.EntityTap='INDlcgProceduresNoQX'
	LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId= bjc.Id
	WHERE ING.IESTADOIN = 'C' AND A.MANEXTPRO = 0 OR ISNULL(ing.TRATAESPECIA, 0) = 3

	
UNION ALL
	
SELECT CONCAT('HCPLAOTRPROCUPS', '-', d.ID) Id,
		'HCPLAOTRPROCUPS' EntityName,
		d.ID [Row],
		CAST(NULL AS INT) AS AuthorizationEventId,
		d.CODSERIPS,
		IIF(d.GENSERVICEORDER IS NULL, 0, 1) Seleccione,
		CONCAT(RTRIM(u.UFUCODIGO), ' - ', RTRIM(u.UFUDESCRI)) UnidadFuncional,
		CONCAT(RTRIM(m.CODIGONIT), ' - ', RTRIM(m.NOMMEDICO)) Medico,
		CONCAT ('Notas otros procedimientos - Datos clínicos relevantes: ', c.ANALISIS) Observacion, -- Observacion Servicio IPS
		c.FECHAREGISTRO Fecha,
		c.NUMEFOLIO Folio,
		c.CODPROSAL,
		m.CODESPEC1,
		d.CANTIDAD CANSERIPS, -- Cantidad Servicio IPS - Aca tambien va el Numero de fracciones cuando el servicio es radioterapias
		c.NUMINGRES,
		c.IPCODPACI,
		u.UFUCODIGO,
		m.CODIGONIT NitMedico,
		d.GENSERVICEORDER,
		'Realizados' Tipo,
		c.CODPROSAL MedicoRealizo,
		c.FECHAREGISTRO FechaRealizacion,
		1 Realizo,   
		RTRIM(cups.DESSERIPS) DESSERIPS, --Descripcion del Procedimiento o Servicio
		ISNULL(cecd.Id, 0) CUPSEntityContractDescriptionId,
		ISNULL(cd.Id, 0) ContractDescriptionId,
		ISNULL(cd.Code + ' - ' + cd.Name, '') ContractDescriptionCodeName,
		acj.Id ACJustificationId,
		acj.CreationUser ACCreationUser,
		acj.CreationDate ACCreationDate,
		acj.JustificationId,		
		iif(acj.id is null,'',CONCAT(bjc.Code,' - ',bjc.[Description])) JustificationCodeName,
		iif(acj.id is null,0,bjc.SkipClearance) SkipLiquidation
FROM HCHISPACA h
JOIN HCPLAOTRPROC c ON h.ID = c.IDHCHISPACA
JOIN HCPLAOTRPROCUPS d ON c.ID = d.IDHCPLAOTRPROC
-------------
JOIN dbo.INUNIFUNC u ON h.UFUCODIGO = u.UFUCODIGO 
JOIN dbo.INPROFSAL m ON c.CODPROSAL = m.CODPROSAL 
JOIN dbo.INCUPSIPS cups ON d.CODSERIPS = cups.CODSERIPS 
-------------
LEFT JOIN Contract.CUPSEntityContractDescriptions cecd on cecd.Id = d.IDDESCRIPCIONRELACIONADA
LEFT JOIN Contract.ContractDescriptions cd on cd.Id = cecd.ContractDescriptionId
LEFT JOIN Billing.AccountControlJustification acj WITH(NOLOCK) ON d.ID = acj.EntityId and acj.EntityName='HCPLAOTRPROCUPS' and acj.EntityTap='INDlcgProceduresNoQX'
LEFT JOIN Billing.BillingJustificationControl bjc WITH(NOLOCK) on acj.JustificationId = bjc.Id
-------------
LEFT JOIN dbo.HCORDPRON a ON c.IPCODPACI = a.IPCODPACI AND c.NUMINGRES = a.NUMINGRES 
	AND d.CODSERIPS = a.CODSERIPS AND ISNULL(d.IDDESCRIPCIONRELACIONADA, 0) = ISNULL(a.IDDESCRIPCIONRELACIONADA, 0)
WHERE a.AUTO IS NULL AND d.FACTURAR = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida todos los procedimientos y servicios de salud NO quirúrgicos ordenados en la historia clínica del paciente, integrando órdenes médicas (HCORDPRON), admisiones (ADINGRESO), catálogo de servicios CUPS (INCUPSIPS), profesionales de la salud (INPROFSAL) y unidades funcionales (INUNIFUNC). Combina tres fuentes mediante UNION ALL: órdenes de ingresos regulares (urgencias, hospitalización, consulta externa), órdenes vinculadas a ingresos de recién nacidos, y procedimientos registrados en planes de otros servicios; permitiendo identificar para cada orden el estado (No Realizado, Realizado, Anulado), el médico que ordenó y el que realizó el procedimiento, la fecha de realización, el folio, la cantidad y la unidad funcional. Adicionalmente enriquece cada registro con la descripción del contrato (CUPS-entidad-contrato) y la justificación de control de facturación o glosa aplicada (código y descripción del motivo), lo que la hace útil para liquidación, facturación RIPS, auditoría de cuentas médicas y seguimiento de órdenes de procedimientos no quirúrgicos por ingreso o paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresNoQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VServicesProceduresNoQx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista las órdenes de procedimientos no quirúrgicos (de historia clínica e historia clínica pediátrica) con su estado, médico ordenador/realizador, descripción contractual y justificación de control de facturación, para liquidación, facturación RIPS y auditoría.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben estar vinculadas a un ingreso existente en ADINGRESO (primer bloque) o a un ingreso de recién nacido vía HCINGRESORECNAC/HCRECINAC (segundo bloque).; En el primer bloque, la orden debe tener MANEXTPRO = 0, o el ingreso debe ser de tratamiento especial tipo 3 (TRATAESPECIA = 3).; En el segundo bloque, el ingreso debe estar en estado ''C'' (IESTADOIN=''C'') con MANEXTPRO=0, o el ingreso debe tener TRATAESPECIA = 3.; En el tercer bloque (HCPLAOTRPROCUPS), el procedimiento debe estar marcado como facturable (FACTURAR = 1) y no debe existir una orden equivalente en HCORDPRON para el mismo paciente, ingreso, servicio y descripción relacionada.; Las tablas maestras INCUPSIPS, INPROFSAL e INUNIFUNC deben contener los códigos referenciados por la orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] VServicesProceduresNoQx: Devuelve filas tipificadas como ''No Realizados'' cuando ESTSERIPS=''1'', ''Anulados'' cuando ESTSERIPS=''5'' y ''Realizados'' en cualquier otro caso.; [RETURN_RESULT] VServicesProceduresNoQx: Marca Seleccione=1 cuando GENSERVICEORDER no es nulo (ya tiene orden de servicio generada) y 0 en caso contrario.; [RETURN_RESULT] VServicesProceduresNoQx: Marca Realizo=1 cuando existe al menos un registro en HCINFPROM para el paciente/servicio/ingreso; en caso contrario Realizo=0.; [RETURN_RESULT] VServicesProceduresNoQx: Cuando ESTSERIPS=''5'' (anulado) la FechaRealizacion es Fechaanulado; en caso contrario toma la primera FECREAPRO de HCINFPROM, o FECHREALI/FECHAPRO, o finalmente FECORDMED.; [RETURN_RESULT] VServicesProceduresNoQx: MedicoRealizo se obtiene del primer CODPROSAL en HCINFPROM por orden ascendente de FECREAPRO; si no existe, usa MEDREALI y como último recurso CODPROSAL de la orden.; [RETURN_RESULT] VServicesProceduresNoQx: Cuando no existe AccountControlJustification asociada (acj.id is null) JustificationCodeName se devuelve vacío y SkipLiquidation=0; en caso contrario se concatena Code-Description y se hereda SkipClearance del catálogo.; [RETURN_RESULT] VServicesProceduresNoQx: Para registros provenientes de HCPLAOTRPROCUPS se fija Tipo=''Realizados'', Realizo=1 y la observación se construye anteponiendo ''Notas otros procedimientos - Datos clínicos relevantes:'' al análisis.; [RETURN_RESULT] VServicesProceduresNoQx: La justificación de control de cuentas se vincula sólo cuando EntityName=''HCORDPRON'' o ''HCPLAOTRPROCUPS'' y EntityTap=''INDlcgProceduresNoQX''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTSERIPS = ''1'' → Tipo = ''No Realizados'' else Evaluar siguiente condición; si ESTSERIPS = ''5'' → Tipo = ''Anulados'' y FechaRealizacion = Fechaanulado else Tipo = ''Realizados'' y FechaRealizacion se calcula desde HCINFPROM o fechas de la orden; si Existe registro en HCINFPROM para el paciente/servicio/ingreso → Realizo = 1 y MedicoRealizo = CODPROSAL del primer informe else Realizo = 0 y MedicoRealizo = MEDREALI o CODPROSAL de la orden; si GENSERVICEORDER IS NULL → Seleccione = 0 else Seleccione = 1; si acj.Id IS NULL (sin justificación de control) → JustificationCodeName='''' y SkipLiquidation=0 else JustificationCodeName = Code+Description y SkipLiquidation = bjc.SkipClearance; si Bloque 1: A.MANEXTPRO = 0 OR ing.TRATAESPECIA = 3 → Incluir órdenes vinculadas directamente al ingreso del paciente; si Bloque 2: ING.IESTADOIN=''C'' AND A.MANEXTPRO=0 OR ing.TRATAESPECIA=3 → Incluir órdenes asociadas a ingresos de recién nacido (relación madre-hijo) cuyo ingreso está cerrado; si Bloque 3: a.AUTO IS NULL AND d.FACTURAR = 1 → Incluir procedimientos de planes de otros procedimientos que no tienen orden equivalente en HCORDPRON y son facturables', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresNoQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VServicesProceduresNoQx';
GO
