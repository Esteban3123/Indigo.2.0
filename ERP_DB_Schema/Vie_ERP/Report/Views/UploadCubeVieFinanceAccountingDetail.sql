
--DECLARE @FECHA_INICIAL DATE = '2024-01-01'
--DECLARE @FECHA_FINAL DATE = '2024-01-02'

CREATE view [Report].[UploadCubeVieFinanceAccountingDetail] as

--SELECT * FROM
--(
SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	'ODO' AS EMPRESA,
	year(jv.VoucherDate) as AÑO,
	case month(jv.VoucherDate)
		when '1' then 'ENERO'
		when '2' then 'FEBRERO'
		when '3' then 'MARZO'
		when '4' then 'ABRIL'
		when '5' then 'MAYO'
		when '6' then 'JUNIO'
		when '7' then 'JULIO'
		when '8' then 'AGOSTO'
		when '9' then 'SEPTIEMBRE'
		when '10' then 'OCTUBRE'
		when '11' then 'NOVIEMBRE'
		when '12' then 'DICIEMBRE'
	END AS MES,
	CASE LEFT(ma.Number,1) 
		WHEN '4' THEN 'INGRESOS'
		WHEN '5' THEN 'GASTOS'
		WHEN '6' THEN 'COSTOS'
	END AS CLASE,
	ma.Number AS CUENTA,
	ma.name AS NOMBRE_CUENTA,
	CASE ma.nature 
		WHEN 1 THEN 'Debito'
		WHEN 2 THEN 'Credito'
	END AS NATURALEZA,
	tp.Nit AS NIT,
	tp.name AS TERCERO,
	cc.code AS CODIGO_CENTRO,
	cc.name AS CENTRO_COSTO,
	jv.VoucherDate AS FECHA_DOCUMENTO,
	jv.Consecutive AS CODIGO_COMPROBANTE,
	CONCAT(jvt.code, ' - ', jvt.Name) AS TIPO_COMPROBANTE,
	jv.EntityCode AS CODIGO_DOCUMENTO_FUENTE,
	gend.Description AS NOMBRE_DOCUMENTO_FUENTE,
	CASE jv.status 
		WHEN 1 THEN 'Registrado'
		WHEN 2 THEN 'Confirmado'
		WHEN 3 THEN 'Anulado'
	ELSE '' END AS ESTADO,
	jv.Detail AS DETALLE,
	jvd.DebitValue AS DEBITO,
	jvd.CreditValue AS CREDITO,
	(jvd.CreditValue-jvd.DebitValue) AS NETO,
	CAST(jv.VoucherDate AS DATE) AS [FECHA BUSQUEDA],
	CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM GeneralLedger.JournalVouchers jv
	INNER JOIN GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
	INNER JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON jvd.IdThirdParty = tp.id
	LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON jvd.IdCostCenter = cc.id
	INNER JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.id
	LEFT JOIN Common.GetEntityNameDescriptions() gend ON jv.EntityName = gend.EntityName
WHERE jv.LegalBookId = 1 AND jv.IsClosedYear = 0 AND jv.Status IN(4, 2) 
	AND year(jv.VoucherDate)>=2024
	--CAST(jv.VoucherDate AS DATE) BETWEEN @FECHA_INICIAL AND @FECHA_FINAL
	AND left(ma.Number,1) in ('4','5','6')
	--) AS CONTABILIDAD


--UNION ALL
--SELECT
--	'UDC' AS EMPRESA,
--	year(jv.VoucherDate) as AÑO,
--	case month(jv.VoucherDate)
--		when '1' then 'ENERO'
--		when '2' then 'FEBRERO'
--		when '3' then 'MARZO'
--		when '4' then 'ABRIL'
--		when '5' then 'MAYO'
--		when '6' then 'JUNIO'
--		when '7' then 'JULIO'
--		when '8' then 'AGOSTO'
--		when '9' then 'SEPTIEMBRE'
--		when '10' then 'OCTUBRE'
--		when '11' then 'NOVIEMBRE'
--		when '12' then 'DICIEMBRE'
--	END AS MES,
--	CASE LEFT(ma.Number,1) 
--		WHEN '4' THEN 'INGRESOS'
--		WHEN '5' THEN 'GASTOS'
--		WHEN '6' THEN 'COSTOS'
--	END AS CLASE,
--	ma.Number AS CUENTA,
--	ma.name AS NOMBRE_CUENTA,
--	CASE ma.nature 
--		WHEN 1 THEN 'Debito'
--		WHEN 2 THEN 'Credito'
--	END AS NATURALEZA,
--	tp.Nit AS NIT,
--	tp.name AS TERCERO,
--	cc.code AS CODIGO_CENTRO,
--	cc.name AS CENTRO_COSTO,
--	jv.VoucherDate AS FECHA_DOCUMENTO,
--	jv.Consecutive AS CODIGO_COMPROBANTE,
--	CONCAT(jvt.code, ' - ', jvt.Name) AS TIPO_COMPROBANTE,
--	jv.EntityCode AS CODIGO_DOCUMENTO_FUENTE,
--	gend.Description AS NOMBRE_DOCUMENTO_FUENTE,
--	CASE jv.status 
--		WHEN 1 THEN 'Registrado'
--		WHEN 2 THEN 'Confirmado'
--		WHEN 3 THEN 'Anulado'
--	ELSE '' END AS ESTADO,
--	jv.Detail AS DETALLE,
--	jvd.DebitValue AS DEBITO,
--	jvd.CreditValue AS CREDITO,
--	(jvd.CreditValue-jvd.DebitValue) AS NETO
--FROM INDIGO888.GeneralLedger.JournalVouchers jv
--	INNER JOIN INDIGO888.GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
--	INNER JOIN INDIGO888.GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id
--	LEFT JOIN INDIGO888.Common.ThirdParty tp WITH (NOLOCK) ON jvd.IdThirdParty = tp.id
--	LEFT JOIN INDIGO888.Payroll.CostCenter cc WITH (NOLOCK) ON jvd.IdCostCenter = cc.id
--	INNER JOIN INDIGO888.GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.id
--	LEFT JOIN INDIGO888.Common.GetEntityNameDescriptions() gend ON jv.EntityName = gend.EntityName
--WHERE jv.LegalBookId = 1 AND jv.IsClosedYear = 0 AND jv.Status IN(4, 2) 
--	AND CAST(jv.VoucherDate AS DATE) BETWEEN @FECHA_INICIAL AND @FECHA_FINAL
--	AND left(ma.Number,1) in ('4','5','6')
--UNION ALL
--SELECT
--	'CCB' AS EMPRESA,
--	year(jv.VoucherDate) as AÑO,
--	case month(jv.VoucherDate)
--		when '1' then 'ENERO'
--		when '2' then 'FEBRERO'
--		when '3' then 'MARZO'
--		when '4' then 'ABRIL'
--		when '5' then 'MAYO'
--		when '6' then 'JUNIO'
--		when '7' then 'JULIO'
--		when '8' then 'AGOSTO'
--		when '9' then 'SEPTIEMBRE'
--		when '10' then 'OCTUBRE'
--		when '11' then 'NOVIEMBRE'
--		when '12' then 'DICIEMBRE'
--	END AS MES,
--	CASE LEFT(ma.Number,1) 
--		WHEN '4' THEN 'INGRESOS'
--		WHEN '5' THEN 'GASTOS'
--		WHEN '6' THEN 'COSTOS'
--	END AS CLASE,
--	ma.Number AS CUENTA,
--	ma.name AS NOMBRE_CUENTA,
--	CASE ma.nature 
--		WHEN 1 THEN 'Debito'
--		WHEN 2 THEN 'Credito'
--	END AS NATURALEZA,
--	tp.Nit AS NIT,
--	tp.name AS TERCERO,
--	cc.code AS CODIGO_CENTRO,
--	cc.name AS CENTRO_COSTO,
--	jv.VoucherDate AS FECHA_DOCUMENTO,
--	jv.Consecutive AS CODIGO_COMPROBANTE,
--	CONCAT(jvt.code, ' - ', jvt.Name) AS TIPO_COMPROBANTE,
--	jv.EntityCode AS CODIGO_DOCUMENTO_FUENTE,
--	gend.Description AS NOMBRE_DOCUMENTO_FUENTE,
--	CASE jv.status 
--		WHEN 1 THEN 'Registrado'
--		WHEN 2 THEN 'Confirmado'
--		WHEN 3 THEN 'Anulado'
--	ELSE '' END AS ESTADO,
--	jv.Detail AS DETALLE,
--	jvd.DebitValue AS DEBITO,
--	jvd.CreditValue AS CREDITO,
--	(jvd.CreditValue-jvd.DebitValue) AS NETO
--FROM INDIGO777.GeneralLedger.JournalVouchers jv
--	INNER JOIN INDIGO777.GeneralLedger.JournalVoucherDetails jvd WITH (NOLOCK) ON jv.Id = jvd.IdAccounting
--	INNER JOIN INDIGO777.GeneralLedger.MainAccounts ma WITH (NOLOCK) ON jvd.IdMainAccount = ma.Id
--	LEFT JOIN INDIGO777.Common.ThirdParty tp WITH (NOLOCK) ON jvd.IdThirdParty = tp.id
--	LEFT JOIN INDIGO777.Payroll.CostCenter cc WITH (NOLOCK) ON jvd.IdCostCenter = cc.id
--	INNER JOIN INDIGO777.GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.id
--	LEFT JOIN INDIGO777.Common.GetEntityNameDescriptions() gend ON jv.EntityName = gend.EntityName
--WHERE jv.LegalBookId = 1 AND jv.IsClosedYear = 0 AND jv.Status IN(4, 2) 
--	AND CAST(jv.VoucherDate AS DATE) BETWEEN @FECHA_INICIAL AND @FECHA_FINAL
--	AND left(ma.Number,1) in ('4','5','6')) AS CONTABILIDAD