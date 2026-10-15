

--CREATE OR ALTER PROCEDURE [Portfolio].[SP_CRUCE_ANTICIPO_CXC]
--declare	@FechaInicio Date='2024-05-01';
--declare	@FechaFin Date='2024-05-31';
--AS

CREATE view [Report].[UploadCubeVieFinanceAdvancePaymentCrossingVsCxC] as

	SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		UO.UnitName AS 'UNIDAD OPERATIVA',--[UnidadOperativa], 
		RC.Code AS 'NRO RECIBO CAJA',--[NroReciboCaja], 
		CA.Code AS 'CRUCE',--[Cruce], 
		CAST(CA.DocumentDate AS DATE) AS 'FECHA',--[Fecha], 
		C.Nit + ' - ' + C.Name AS 'CLIENTE',--[Cliente], 
		T.Nit AS 'NIT',--[NIT], 
		T.Name AS 'TERCERO',--[Tercero], 
		CASE CA.TransferType 
			WHEN '1' THEN 'MismoCliente' 
			WHEN '2' THEN 'DiferenteCliente' END AS 'TIPO TRASLADO',--[TipoTraslado], 
		cuenta.Number AS 'CUENTA CONTABLE',--CuentaContable, 
		CASE CA.Status 
			WHEN '1' THEN 'Registrado' 
			WHEN '2' THEN 'Confirmado' 
			WHEN '3' THEN 'Anulado' 
			WHEN '4' THEN 'Reversado' END AS 'ESTADO CRUCE',--[EstadoCruce], 
		ca.observations AS 'OBSERVACION',--[Observacion],
		pa.AdmissionNumber AS 'NRO INGRESO',--[NroIngreso], 
		CAR.InvoiceNumber AS 'NRO FACTURA',--[NroFactura], 
		CAST(CAR.AccountReceivableDate AS DATE) AS 'FECHA FACTURA',--[FechaFactura], 
		F.TotalInvoice AS 'VALOR FACTURA',--[ValorFactura],
		DT.Value AS 'VALOR CRUCE',--[ValorCruce], 
		CAR.Balance AS 'SALDO FACTURA',--[SaldoFactura], 
		RTRIM(CA.CreationUser) + ' - ' + RTRIM(per.Fullname) AS 'USUARIO',--[Usuario],
		RTRIM(pcon.identification) + ' - ' + RTRIM(pcon.fullname) AS 'USUARIO CONFIRMO',--[UsuarioConfirmo]
	    CAST(CA.DocumentDate AS DATE) AS [FECHA BUSQUEDA],
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
		--INTO INDIGODWH.PBI.STG_CRUCE_DE_ANTICIPO_CXC
	FROM Portfolio.PortfolioTransfer AS CA WITH (nolock) LEFT OUTER JOIN
	Common.Customer AS C WITH (nolock) ON C.Id = CA.CustomerId LEFT OUTER JOIN
	GeneralLedger.MainAccounts AS cuenta WITH (nolock) ON cuenta.Id = CA.MainAccountId INNER JOIN
	Portfolio.PortfolioTransferDetail AS DT WITH (nolock) ON DT.PortfolioTrasferId = CA.Id INNER JOIN
	Portfolio.AccountReceivable AS CAR ON CAR.Id = DT.AccountReceivableId INNER JOIN
	Common.ThirdParty AS T WITH (nolock) ON T.Id = CAR.ThirdPartyId INNER JOIN
	Portfolio.PortfolioAdvance AS pa ON pa.Id = CA.PortfolioAdvanceId INNER JOIN
	Security.[UserINT] AS u WITH (nolock) ON u.UserCode = CA.CreationUser LEFT OUTER JOIN
	Security.PersonINT AS per WITH (nolock) ON per.Id = u.IdPerson LEFT OUTER JOIN
	Billing.Invoice AS F WITH (nolock) ON F.Id = CAR.InvoiceId LEFT OUTER JOIN
	Common.OperatingUnit AS UO WITH (nolock) ON UO.Id = F.OperatingUnitId LEFT OUTER JOIN
	Treasury.CashReceipts AS RC WITH (nolock) ON RC.Id = pa.CashReceiptId
	LEFT OUTER JOIN Security.[UserINT] AS ucon WITH (nolock) ON ca.confirmationuser = ucon.usercode 
	LEFT OUTER JOIN Security.PersonINT AS pcon WITH (nolock) ON ucon.idperson = pcon.id
	WHERE (cuenta.LegalBookId = 1)
	--AND CAST(CA.DocumentDate AS DATE) BETWEEN @FechaInicio AND @FechaFin -- Esta linea no hace parte del query original 