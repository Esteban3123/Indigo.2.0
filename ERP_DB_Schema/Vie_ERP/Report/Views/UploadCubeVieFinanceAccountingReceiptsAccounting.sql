

--CREATE PROCEDURE [GeneralLedger].[SP_COMPROBANTES_CONTABLES_CONTABILIDAD]
--DECLARE	@FECINI Datetime='2024-05-01';
--declare	@FECFIN Datetime ='2024-05-02';
--AS

CREATE view [Report].[UploadCubeVieFinanceAccountingReceiptsAccounting] as

	WITH sedes AS 
	(
		SELECT DISTINCT
			cc.code,
			br.name 
		FROM payroll.costcenter AS cc
		INNER JOIN payroll.functionalunit AS uf ON  cc.code = uf.code
		INNER JOIN payroll.branchoffice AS br ON uf.branchofficeid = br.id
	)

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		ISNULL(sed.name,'N/A') 'SEDE',--[Sede],
		JV.Consecutive as 'NRO COMPROBANTE',--[NroComprobante],
		JVT.Code + ' - ' + JVT.Name 'TIPO COMPROBANTE',--[TipoComprobante],
		JV.VoucherDate 'FECHA COMPROBANTE',--[FechaComprobante], 
		case JV.Status 
			when 1 then 'Registrado' 
			when 2 then 'Confirmado' 
			when 3 then 'Anulado' end 'ESTADO',--[Estado],
		JV.EntityCode 'DOCUMENTO ORIGEN',--[DocumentoOrigen],
		JV.EntityName 'ORIGEN',--[Origen],
		MA.Number 'CUENTA CONTABLE',--[CuentaContable],
		MA.Name 'DESCRIPCION CUENTA',--[DescripcionCuenta],
		TP.Nit 'NIT',--[NIT],
		TP.Name 'TERCERO',--[Tercero] ,
		CC.Code AS 'CODIGO CENTRO COSTO',--[CodigoCentroCosto],
		CC.Name 'CENTRO COSTO',--[CentroCosto],
		JVD.DebitValue 'VALOR DEBITO',--[ValorDebito],
		JVD.CreditValue 'VALOR CREDITO',--[ValorCredito],
		JVD.Detail 'DETALLE',--[Detalle],
		jv.creationuser AS 'CODIGO USUARIO CREO',--[CodUsuarioCreo],
		pcre.fullname AS 'USUARIO CREO',--[UsuarioCreo],
		JV.ConfirmationUser 'CODIGO USUARIO CONFIRMO',--[CodUsuarioConfirmo], 
		PER.Fullname 'USUARIO CONFIRMO',--[UsuarioConfirmo],
		JV.ConfirmationDate 'FECHA CONFIRMACION',--[FechaConfirmacion]
	    CAST(JV.VoucherDate AS DATE) AS [FECHA BUSQUEDA],
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
		--into INDIGODWH.GeneralLedger.STG_REPORTE_COMPROBANTES_CONTABLES
	FROM GeneralLedger .JournalVouchers JV WITH (NOLOCK)
	JOIN GeneralLedger .JournalVoucherTypes AS JVT WITH (NOLOCK) ON JVT.Id =JV.IdJournalVoucher  AND jvt.id IN(033)
	JOIN GeneralLedger .JournalVoucherDetails AS JVD WITH (NOLOCK) ON JV.Id =JVD.IdAccounting 
	JOIN GeneralLedger .MainAccounts as MA WITH (NOLOCK) ON MA.Id =JVD.IdMainAccount 
	LEFT JOIN Security.[UserINT] AS USU WITH (NOLOCK) ON USU.UserCode =JV.ConfirmationUser 
	LEFT JOIN Security.PersonINT AS PER WITH (NOLOCK) ON PER.Id =USU.IdPerson 

	LEFT JOIN Security.[UserINT] AS ucre WITH (NOLOCK) ON jv.creationuser = ucre.usercode
	LEFT JOIN Security.PersonINT AS pcre WITH (NOLOCK) ON ucre.idperson = pcre.id

	LEFT JOIN Common .ThirdParty AS TP WITH (NOLOCK) ON TP.Id =JVD.IdThirdParty 
	LEFT JOIN Payroll .CostCenter AS CC WITH (NOLOCK) ON CC.Id =JVD.IdCostCenter 
	LEFT JOIN sedes AS sed ON cc.code = sed.code -- linea insertada  el 2024-01-22
	/* Lineas comentadas el 2024-01-22
	LEFT JOIN Payroll .FunctionalUnit FU WITH (NOLOCK) ON FU.CostCenterId =CC.Id 
	LEFT JOIN Payroll .BranchOffice BO  WITH (NOLOCK) ON BO.Id  =FU.BranchOfficeId
	*/
	WHERE jv.LegalBookId = 1 AND YEAR(JV.VoucherDate)>=2024
	--CAST(JV.VoucherDate AS DATE) BETWEEN @FECINI AND @FECFIN