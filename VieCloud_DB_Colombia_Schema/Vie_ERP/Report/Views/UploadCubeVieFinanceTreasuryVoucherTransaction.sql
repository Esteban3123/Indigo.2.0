
--CREATE PROCEDURE [dbo].[ODO_Comprobantes_de_Egreso]
	-- Add the parameters for the stored procedure here

CREATE view [Report].[UploadCubeVieFinanceTreasuryVoucherTransaction] as

--DECLARE	@inidate DATE='2024-06-01';
--DECLARE	@enddate DATE='2024-06-30';


	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		vtra.code AS 'NRO DOCUMENTO',--[NroDocumento],
		vtra.documentdate AS 'FECHA DOCUMENTO',--[FechaDocumento],
		CASE vtra.voucherclass WHEN 1 THEN 'Pago' WHEN 2 THEN 'Reembolso' WHEN 3 THEN 'Traslado' END AS 'CLASE COMPROBANTE',--[ClaseComprobante],
		CASE vtra.expensetype WHEN 1 THEN 'Cuenta Bancaria' WHEN 2 THEN 'Caja Menor' WHEN 3 THEN 'Caja Mayor' WHEN 4 THEN 'Producto Bancario' END AS 'TIPO COMPROBANTE',--[TipoComprobante],
		RTRIM(third.nit) + ' - ' + RTRIM(third.name) AS 'TERCERO',--Tercero,
		eba.number AS 'NRO CUENTA',--[NroCuenta],
		casr.name AS 'CAJA',--[Caja],
		CASE vtra.paymentmethod WHEN 1 THEN 'Cheque' WHEN 2 THEN 'Nota debito' END AS 'METODO PAGO',--[MetodoPago],
		vtra.notenumber AS 'NRO NOTA',--[NroNota],
		ccpt.code  + ' - ' + ccpt.description AS 'CONCEPTO',--[Concepto],
		vtrad.value AS 'VALOR',--[Valor],
		CASE ccpt.nature WHEN 1 THEN 'Debito' WHEN 2 THEN 'Credito' END AS 'NATURALEZA',--[Naturaleza],
		cfc.code + ' - ' + cfc.nameconcept AS 'CONCEPTO FLUJO EFECTIVO',--[ConceptoFlujoEfectivo],
		macc.number + ' - ' + macc.name AS 'CUENTA CONTABLE',--[CuentaContable],
		cc.name AS 'CENTRO COSTO',--[CentroCosto],
		RTRIM(thi.nit) + ' - ' + RTRIM(thi.name) AS 'TERCERO II',--[TerceroII],
		acpa.billnumber AS 'FACTURA',--[Factura],
		acpa.expirationdate AS 'FECHA',--[Fecha],
		aps.share AS 'NRO CUOTA',--[NroCuota],
		dcb.advancedvalue AS 'VALOR PAGAR',--[ValorPagar],
		dcb.advancepercent AS 'PORCENTAJE PAGAR',--[PorcentajePagar],
		apc.code + ' - ' + apc.name AS 'CONCEPTO PAGO',--[ConceptoPago],
		usr.fullname AS 'FUNCIONARIO CREO',--[FuncionarioCreo],
		vtra.creationdate 'FECHA CREACION',--[FechaCreacion]
		CAST(vtra.documentdate AS DATE) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM treasury.vouchertransaction AS vtra
	LEFT JOIN common.thirdparty AS third ON vtra.idthirdparty = third.id
	LEFT JOIN treasury.entitybankaccounts AS eba ON vtra.identitybankaccount = eba.id
	LEFT JOIN treasury.cashregisters AS casr ON vtra.idcashregister = casr.id
	INNER JOIN treasury.vouchertransactiondetails AS vtrad ON vtra.id = vtrad.idvouchertransaction
	INNER JOIN generalledger.mainaccounts AS macc ON vtrad.idmainaccount = macc.id
	LEFT JOIN payroll.costcenter AS cc ON vtrad.idcostcenter = cc.id
	LEFT JOIN treasury.expenseconcepts AS ccpt ON vtrad.idexpenseconcept = ccpt.id
	LEFT JOIN treasury.cashflowconcept AS cfc ON vtrad.idcashflowconcept = cfc.id
	LEFT JOIN common.thirdparty AS thi ON vtrad.idthirdparty = thi.id
	lEFT JOIN treasury.dischargebill AS dcb ON vtrad.id = dcb.idvouchertransactiond
	LEFT JOIN payments.accountpayable AS acpa ON dcb.idaccountpayable = acpa.id
	LEFT JOIN payments.accountpayableconcepts AS apc ON dcb.idpaymentconcept = apc.id
	LEFT JOIN payments.accountpayableshares AS  aps ON dcb.idaccountpayableshare = aps.id
	INNER JOIN security.personINT as usr ON vtra.creationuser = usr.identification
	WHERE CAST(vtra.documentdate AS DATE)>='2022-01-01'
	--CAST(vtra.documentdate AS DATE) BETWEEN @inidate AND @enddate

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de transacciones de comprobantes de tesorería (egresos/pagos/reembolsos/traslados) enriquecido con tercero, cuenta bancaria, caja, concepto, cuenta contable y cuentas por pagar, para alimentar un cubo analítico financiero.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTreasuryVoucherTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen transacciones de comprobantes de tesorería con fecha de documento >= 2022-01-01.; Cada transacción tiene al menos un detalle (vouchertransactiondetails) con cuenta contable principal asociada.; El usuario creador de la transacción debe existir en security.personINT (identificación válida).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTreasuryVoucherTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de compañía (ID_COMPANY) corresponde al nombre de la base de datos actual truncado a 9 caracteres.; Las clases de comprobante válidas son 1=Pago, 2=Reembolso, 3=Traslado; cualquier otro valor produce NULL.; Los tipos de comprobante válidos son 1..4; otros valores producen NULL.; Los métodos de pago válidos son 1=Cheque, 2=Nota débito; otros valores producen NULL.; La naturaleza del concepto solo es Débito (1) o Crédito (2).; Tercero, cuenta bancaria, caja, concepto de gasto, concepto de flujo de efectivo, centro de costo, descargo de factura y cuenta por pagar son opcionales (LEFT JOIN), pero detalle, cuenta contable y usuario creador son obligatorios.; Solo se publican movimientos con fecha de documento desde 2022-01-01 en adelante.; La fecha de búsqueda se normaliza a tipo DATE (sin componente de tiempo).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTreasuryVoucherTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de tesorería; Pago; Reembolso; Traslado; Caja menor; Caja mayor; Cuenta bancaria; Producto bancario; Cheque; Nota débito; Concepto de gasto; Concepto de flujo de efectivo; Cuenta contable; Centro de costo; Cuenta por pagar; Cuota de cuenta por pagar; Factura; Tercero; NIT; Anticipo (advance); Naturaleza débito/crédito', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTreasuryVoucherTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieFinanceTreasuryVoucherTransaction: Solo retorna filas cuyo CAST(documentdate AS DATE) >= ''2022-01-01''.; [RETURN_RESULT] Report.UploadCubeVieFinanceTreasuryVoucherTransaction: Excluye transacciones sin detalle (INNER JOIN con vouchertransactiondetails) y sin cuenta contable principal (INNER JOIN con mainaccounts).; [RETURN_RESULT] Report.UploadCubeVieFinanceTreasuryVoucherTransaction: Excluye transacciones cuyo usuario creador no exista en security.personINT (INNER JOIN por identification).; [RETURN_RESULT] Report.UploadCubeVieFinanceTreasuryVoucherTransaction: Reporta la marca de actualización (ULT_ACTUAL) convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTreasuryVoucherTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si voucherclass = 1 / 2 / 3 → Etiqueta clase comprobante como ''Pago'' / ''Reembolso'' / ''Traslado'' respectivamente.; si expensetype = 1 / 2 / 3 / 4 → Etiqueta tipo comprobante como ''Cuenta Bancaria'' / ''Caja Menor'' / ''Caja Mayor'' / ''Producto Bancario''.; si paymentmethod = 1 / 2 → Etiqueta método de pago como ''Cheque'' / ''Nota debito''.; si expenseconcept.nature = 1 / 2 → Etiqueta naturaleza contable como ''Debito'' / ''Credito''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTreasuryVoucherTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'treasury.vouchertransaction; common.thirdparty; treasury.entitybankaccounts; treasury.cashregisters; treasury.vouchertransactiondetails; generalledger.mainaccounts; payroll.costcenter; treasury.expenseconcepts; treasury.cashflowconcept; treasury.dischargebill; payments.accountpayable; payments.accountpayableconcepts; payments.accountpayableshares; security.personINT', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTreasuryVoucherTransaction';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTreasuryVoucherTransaction';
GO
