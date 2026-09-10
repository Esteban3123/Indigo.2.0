

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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para cargue a cubo BI, los cruces de anticipos de cartera contra cuentas por cobrar con sus datos de recibo de caja, factura, cliente, tercero, estado y usuarios involucrados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAdvancePaymentCrossingVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta contable asociada al cruce debe pertenecer al libro legal (LegalBookId = 1).; El cruce debe tener detalle (PortfolioTransferDetail) asociado a una cuenta por cobrar y a un anticipo de cartera.; El usuario creador del cruce debe existir en Security.UserINT.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAdvancePaymentCrossingVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan movimientos cuya cuenta contable pertenezca al libro legal principal (LegalBookId=1).; Cada fila representa un detalle de cruce (PortfolioTransferDetail) ligado obligatoriamente a una cuenta por cobrar, un anticipo y un tercero.; El cliente, la cuenta contable, la factura, la unidad operativa, el recibo de caja y el usuario que confirmó pueden ser nulos (LEFT JOIN), pero el detalle, la CxC, el anticipo y el tercero siempre existen.; La fecha del cruce se reporta truncada a DATE y se replica como FECHA BUSQUEDA.; El timestamp de última actualización siempre se calcula en zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAdvancePaymentCrossingVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Anticipo de cartera; Cruce de anticipo; Cuenta por cobrar; Factura; Recibo de caja; Cliente; Tercero; Cuenta contable; Unidad operativa; Traslado entre clientes; Saldo de factura; Libro legal contable', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAdvancePaymentCrossingVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieFinanceAdvancePaymentCrossingVsCxC: Devuelve únicamente cruces cuya cuenta contable cumpla cuenta.LegalBookId = 1.; [RETURN_RESULT] Report.UploadCubeVieFinanceAdvancePaymentCrossingVsCxC: Traduce TransferType: 1=''MismoCliente'', 2=''DiferenteCliente''.; [RETURN_RESULT] Report.UploadCubeVieFinanceAdvancePaymentCrossingVsCxC: Traduce Status del cruce: 1=''Registrado'', 2=''Confirmado'', 3=''Anulado'', 4=''Reversado''.; [RETURN_RESULT] Report.UploadCubeVieFinanceAdvancePaymentCrossingVsCxC: Marca el timestamp de actualización (ULT_ACTUAL) usando GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.; [RETURN_RESULT] Report.UploadCubeVieFinanceAdvancePaymentCrossingVsCxC: Identifica la compañía con CAST(DB_NAME() AS VARCHAR(9)) como ID_COMPANY.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAdvancePaymentCrossingVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CA.TransferType = 1 → Clasifica el traslado como ''MismoCliente'' else Si es 2 lo clasifica como ''DiferenteCliente''; si CA.Status = 1/2/3/4 → Etiqueta el estado del cruce como Registrado/Confirmado/Anulado/Reversado respectivamente; si cuenta.LegalBookId = 1 → Incluye el registro en la salida (filtra cuentas que no sean del libro legal principal) else Excluye el registro', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAdvancePaymentCrossingVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioTransfer; Common.Customer; GeneralLedger.MainAccounts; Portfolio.PortfolioTransferDetail; Portfolio.AccountReceivable; Common.ThirdParty; Portfolio.PortfolioAdvance; Security.UserINT; Security.PersonINT; Billing.Invoice; Common.OperatingUnit; Treasury.CashReceipts', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAdvancePaymentCrossingVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceAdvancePaymentCrossingVsCxC';
GO
