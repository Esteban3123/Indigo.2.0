
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