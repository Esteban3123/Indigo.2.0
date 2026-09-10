
CREATE VIEW [Report].[ViewCarteraCruceAnticipoVsCxC] AS

SELECT  
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
RC.Code AS 'RECIBO DE CAJA',
CA.Code AS 'CODIGO CRUCE', 
CAST(CA.DocumentDate AS DATE) AS 'FECHA CRUCE', 
C.Nit + ' - ' + C.Name AS 'CLIENTE', 
T.Nit 'NIT', T.Name AS 'TERCERO', 
CASE CA.TransferType WHEN '1' THEN 'MismoCliente' 
					 WHEN '2' THEN 'DiferenteCliente' END AS 'TIPO TRASLADO', 
cuenta.Number AS 'CUENTA CONTABLE', 
CASE CA.Status WHEN '1' THEN 'Registrado' 
			   WHEN '2' THEN 'Confirmado' 
			   WHEN '3' THEN 'Anulado' 
			   WHEN '4' THEN 'Reversado' END AS 'ESTADO CRUCE', 
ca.observations AS 'OBSERVACION',
CAR.InvoiceNumber AS 'FACTURA', 
pa.AdmissionNumber AS 'INGRESO', 
CAST(CAR.AccountReceivableDate AS DATE) AS 'FECHA FACTURA', 
DT.Value AS 'VR CRUCE', 
CAR.Balance AS 'SALDO FACTURA', 
CA.CreationUser + '_' + RTRIM(U.NOMUSUARI)  AS 'USUARIO GENERO', 
F.TotalInvoice AS 'VR FACTURA',
1 as 'CANTIDAD',
CAST(CAR.AccountReceivableDate AS date) AS 'FECHA BUSQUEDA',
YEAR(CAR.AccountReceivableDate) AS 'AÑO FECHA BUSQUEDA',
MONTH(CAR.AccountReceivableDate) AS 'MES AÑO FECHA BUSQUEDA',
CASE MONTH(CAR.AccountReceivableDate)
WHEN 1 THEN 'ENERO'
WHEN 2 THEN 'FEBRERO'
WHEN 3 THEN 'MARZO'
WHEN 4 THEN 'ABRIL'
WHEN 5 THEN 'MAYO'
WHEN 6 THEN 'JUNIO'
WHEN 7 THEN 'JULIO'
WHEN 8 THEN 'AGOSTO'
WHEN 9 THEN 'SEPTIEMBRE'
WHEN 10 THEN 'OCTUBRE'
WHEN 11 THEN 'NOVIEMBRE'
WHEN 12 THEN 'DICIEMBRE'
END AS 'MES NOMBRE FECHA BUSQUEDA',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM            
Portfolio.PortfolioTransfer AS CA WITH (nolock) LEFT OUTER JOIN
Common.Customer AS C WITH (nolock) ON C.Id = CA.CustomerId LEFT OUTER JOIN
GeneralLedger.MainAccounts AS cuenta WITH (nolock) ON cuenta.Id = CA.MainAccountId INNER JOIN
Portfolio.PortfolioTransferDetail AS DT WITH (nolock) ON DT.PortfolioTrasferId = CA.Id INNER JOIN
Portfolio.AccountReceivable AS CAR ON CAR.Id = DT.AccountReceivableId INNER JOIN
Common.ThirdParty AS T WITH (nolock) ON T.Id = CAR.ThirdPartyId INNER JOIN
Portfolio.PortfolioAdvance AS pa ON pa.Id = CA.PortfolioAdvanceId INNER JOIN
DBO.SEGusuaru AS U WITH (nolock) ON U.CODUSUARI =CA.CreationUser  LEFT OUTER JOIN
Billing.Invoice AS F WITH (nolock) ON F.Id = CAR.InvoiceId LEFT OUTER JOIN
Common.OperatingUnit AS UO WITH (nolock) ON UO.Id = F.OperatingUnitId LEFT OUTER JOIN
Treasury.CashReceipts AS RC WITH (nolock) ON RC.Id = pa.CashReceiptId
WHERE (cuenta.LegalBookId = 1)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte que cruza anticipos de cartera con cuentas por cobrar, mostrando cómo cada traslado/transferencia de cartera (mismo o diferente cliente) impacta facturas específicas. Consolida datos del recibo de caja origen, el cliente pagador, el tercero, la cuenta contable (filtrada al libro legal 1), el estado del cruce, valores cruzados, saldos de factura y el número de ingreso del paciente. Incluye dimensiones temporales (año, mes nombre) y marca de última actualización para consumo en reportes de gestión de cartera.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que cruza los traslados/aplicaciones de anticipos de cartera contra las cuentas por cobrar (facturas), mostrando el recibo de caja origen, el cliente, tercero, valores, estado y datos contables del cruce.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta contable asociada al traslado (MainAccounts) debe pertenecer al libro legal con LegalBookId = 1.; Cada traslado debe tener al menos un detalle (PortfolioTransferDetail) y una cuenta por cobrar (AccountReceivable) asociada.; El traslado debe tener un anticipo de cartera (PortfolioAdvance) y un usuario creador válido en SEGusuaru.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan cruces contabilizados en el libro legal (LegalBookId=1).; Cada fila representa una unidad (CANTIDAD = 1) para conteos agregados en el reporte.; La fecha de última actualización (ULT_ACTUAL) se calcula con la zona horaria ''Pakistan Standard Time''.; El identificador de compañía se obtiene del nombre de la base de datos actual (DB_NAME()).; El campo CLIENTE concatena NIT y nombre del cliente; el USUARIO GENERO concatena el código y nombre del usuario creador.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Traslado de cartera; Anticipo de cartera; Cuenta por cobrar; Factura; Recibo de caja; Cliente; Tercero (NIT); Cuenta contable (PUC); Libro legal; Ingreso/Admisión; Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewCarteraCruceAnticipoVsCxC: Devuelve una fila por cada detalle de traslado de cartera (PortfolioTransferDetail) cruzado contra una cuenta por cobrar, filtrando solo cuentas contables del libro legal (LegalBookId=1).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CA.TransferType = ''1'' → Etiqueta el tipo de traslado como ''MismoCliente'' else Si TransferType=''2'' se etiqueta ''DiferenteCliente''; si CA.Status IN (''1'',''2'',''3'',''4'') → Traduce el estado del cruce a ''Registrado'', ''Confirmado'', ''Anulado'' o ''Reversado'' respectivamente; si cuenta.LegalBookId = 1 → Solo se incluyen cruces cuya cuenta contable pertenece al libro legal principal else Se excluyen los registros con cuentas de otros libros (NIIF u otros)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioTransfer; Common.Customer; GeneralLedger.MainAccounts; Portfolio.PortfolioTransferDetail; Portfolio.AccountReceivable; Common.ThirdParty; Portfolio.PortfolioAdvance; DBO.SEGusuaru; Billing.Invoice; Common.OperatingUnit; Treasury.CashReceipts', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewCarteraCruceAnticipoVsCxC';
GO
