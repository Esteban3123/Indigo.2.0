
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting financiero-contable diseñada para alimentar un cubo OLAP (BI). Aplana el detalle de asientos contables del libro mayor legal (libro 1, año ≥ 2024, estados confirmado/aprobado, año no cerrado) filtrando únicamente cuentas de ingresos (4), gastos (5) y costos (6). Enriquece cada línea con cuenta, tercero, centro de costo, tipo y documento fuente, exponiendo débito, crédito y neto junto con marca de última actualización para carga incremental al cubo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle contable de movimientos de cuentas de resultado (ingresos, gastos y costos) del libro legal principal desde 2024 en adelante, formateado para alimentar un cubo financiero corporativo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un comprobante en GeneralLedger.JournalVouchers con LegalBookId=1, IsClosedYear=0 y Status en (2,4) para retornar filas.; Las tablas maestras (MainAccounts, JournalVoucherTypes) deben tener correspondencia con los detalles para que el INNER JOIN no descarte registros.; La función Common.GetEntityNameDescriptions debe estar disponible para resolver el nombre del documento fuente.; La base de datos debe soportar la zona horaria ''Pakistan Standard Time'' para el cálculo de ULT_ACTUAL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen movimientos del libro legal principal (LegalBookId = 1).; Se excluyen comprobantes de años contablemente cerrados (IsClosedYear = 0).; Solo se incluyen comprobantes con Status ∈ {2, 4}; se omiten registros en estado 1 (Registrado), 3 (Anulado) u otros.; Solo se exponen cuentas de resultado: las que comienzan en ''4'' (Ingresos), ''5'' (Gastos) o ''6'' (Costos); se excluyen activos/pasivos/patrimonio.; Únicamente se reportan movimientos con VoucherDate del año 2024 o posterior.; El campo NETO siempre se calcula como CreditValue - DebitValue.; ID_COMPANY se deriva dinámicamente del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres y EMPRESA se fija como ''ODO''.; ULT_ACTUAL refleja la fecha/hora actual convertida a la zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Asiento de diario; Plan de cuentas (PUC); Naturaleza débito/crédito; Tercero (NIT); Centro de costo; Tipo de comprobante; Libro legal; Cierre de año contable; Clases contables: Ingresos, Gastos, Costos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieFinanceAccountingDetail: Cuando jv.LegalBookId=1 AND jv.IsClosedYear=0 AND jv.Status IN (4,2) AND year(jv.VoucherDate)>=2024 AND LEFT(ma.Number,1) IN (''4'',''5'',''6''), se retorna una fila por cada detalle del comprobante con sus valores débito/crédito, neto, tercero, centro de costo y metadatos del comprobante.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEFT(ma.Number,1) = ''4'' → Clase = ''INGRESOS''; si LEFT(ma.Number,1) = ''5'' → Clase = ''GASTOS''; si LEFT(ma.Number,1) = ''6'' → Clase = ''COSTOS''; si ma.nature = 1 → Naturaleza = ''Debito'' else Si ma.nature = 2 entonces ''Credito''; si jv.status = 1/2/3 → Estado mapeado a ''Registrado''/''Confirmado''/''Anulado'' respectivamente else cadena vacía; si month(jv.VoucherDate) entre 1 y 12 → Se traduce a nombre del mes en español (ENERO..DICIEMBRE)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; Common.ThirdParty; Payroll.CostCenter; GeneralLedger.JournalVoucherTypes; Common.GetEntityNameDescriptions', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAccountingDetail';
GO
