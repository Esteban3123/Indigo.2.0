
create view [Report].[UploadCubeVieRCMGlosaStatistician] AS

	SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		T.Nit 'NIT',--'NIT', 
		T.Name AS 'ENTIDAD',--'Entidad', 
		C.RadicatedConsecutive AS  'NRO RADICADO GLOSA',--'NroRadicacionGlosa', 
		CAST(C.RadicatedDate AS DATE) AS 'FECHA RADICADO GLOSA',--'FechaRadicadoGlosa', 
		CAST(C.ConfirmDate AS DATE) AS 'FECHA CONFIRMACION RADICADO',--'FechaConfirmacionRadicado', 
		CASE 
			WHEN C.State = 1 THEN 'Sin Confirmar' 
			WHEN C.State = 2 THEN 'ConfirmadoRadicado' 
			WHEN C.State = 3 THEN 'OficioConRespuesta' 
			WHEN C.State = 4 THEN 'Anulada' END AS 'ESTADO RADICACION GLOSA',--'EstadoRadicacionGlosa', 
		DG.InvoiceNumber AS 'NRO FACTURA',--'NroFactura', 
		CAST(DG.InvoiceDate AS DATE) AS 'FECHA FACTURA',--'FechaFactura', 
		DG.InvoiceValueEntity AS 'VALOR ENTIDAD',--'ValorEntidad', 
		DG.BalanceInvoice AS  'VALOR FACTURA',--'ValorFactura',
		DG.ValueGlosado AS 'VALOR GLOSADO',--'ValorGlosado', 
		DG.ValueAcceptedFirstInstance AS 'VALOR ACEPTADO PRI INSTANCIA',--'ValorAceptadoPriInstancia', 
		DG.ValueReiterated AS 'VALOR REITERADO',--'ValorReiterado', 
		DG.ValueAcceptedSecondInstance AS 'VALOR ACEPTADO SEG INSTANCIA',--'ValorAceptadoSegInstancia', 
		DG.ValueAcceptedIPSconciliation AS 'VALOR ACEPTADO IPS CONCILIACION',--'ValorAceptadoIPSConciliacion', 
		DG.ValueAcceptedEAPBconciliation AS 'VALOR ACEPTADO EAPB CONCILIACION',--'ValorAceptadoEAPBConciliacion', 
		DG.BalanceGlosa AS 'SALDO PENDIENTE CONCILIAR',--'SaldoPendienteConciliar', 
		DG.ValuePayments AS 'VALOR PAGO PARCIAL',--'ValorPagoParcial',
		tcj.LegalTransferValue AS 'COBRO JURIDICO',--'CobroJuridico', 
		DG.RadicatedNumber AS 'NRO RADICADO ERP',--'NroRadicadoERP', 
		CAST(DG.RadicatedDate AS DATE) AS 'FECHA RADICADO ERP',--'FechaRadicadoERP', 
		CASE 
			WHEN CC.State = 1 THEN 'Sin Confirmar' 
			WHEN CC.State = 2 THEN 'Confirmado' END AS 'ESTADO CONCILIACION',--'EstadoConciliacion', 
		CC.ConciliationConsecutive AS 'NRO CONCILIACION',--'NroConciliacion', 
		C.Comment AS 'OBSERVACIONES',--'Observaciones', 
		CAST(CC.ConciliationDate AS DATE) AS 'FECHA REGISTRO CONCILIACION',--'FechaRegistroConciliacion', 
		CAST(CC.ConfirmDate AS DATE) AS 'FECHA CONFIRMACION CONCILIACION',--'FechaConfirmacionConciliacion', 
		CAST(CC.DocumentDate AS DATE) AS 'FECHA OFICIO CONCILIACION',--'FechaOficioConciliacion', 
		CAST(C.DocumentDate AS DATE) AS 'FECHA OFICIO RADICADO GLOSA',--'FechaOficioRadicadoGlosa', 
		CAST(C.DateRadicatedDocumentReply AS DATE) AS 'FECHA CONFIRMACION CONSECUTIVO RESPUESTA',--'FechaConfirmacionConsecutivoRespuesta', 
		CAST(DG.CoordinationDateGlosa AS DATE) AS 'FECHA CONFIRMACION RESPUESTA GLOSA FACTURA',--'FechaConfirmacionRespuestaGlosaFactura',
		CAST(ing.IFECHAING AS DATE)  as 'FECHA INGRESO',--'FechaIngreso', 
		CAST(eg.FECALTPAC AS DATE)  as 'FECHA EGRESO',--'FechaEgreso',
		CAT.Name AS 'CATEGORIA',--'Categoria',
		CEN.NOMCENATE AS 'CENTRO ATENCION',--'CentroAtencion',
		CAST(C.RadicatedDate AS DATE) [FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM  Glosas.GlosaPortfolioGlosada AS DG 
	INNER JOIN (
		SELECT D.DocumentType, D.InvoiceNumber, D.GlosaObjectionsReceptionCId, D.PortfolioGlosaId
		FROM Glosas.GlosaObjectionsReceptionD AS D 
		INNER JOIN (
			SELECT MAX(GlosaObjectionsReceptionCId) AS GlosaObjectionsReceptionCId, PortfolioGlosaId, InvoiceNumber
			FROM  Glosas.GlosaObjectionsReceptionD
			GROUP BY PortfolioGlosaId, InvoiceNumber
		) AS G ON G.GlosaObjectionsReceptionCId = D.GlosaObjectionsReceptionCId AND D.InvoiceNumber = G.InvoiceNumber AND D.PortfolioGlosaId = G.PortfolioGlosaId
	) AS G1 ON G1.PortfolioGlosaId = DG.Id 
	INNER JOIN Glosas.GlosaObjectionsReceptionC AS C with (nolock) ON G1.GlosaObjectionsReceptionCId = C.Id 
	INNER JOIN Common.Customer AS T with (nolock) ON C.CustomerId = T.Id
	LEFT OUTER JOIN (
		SELECT InvoiceNumber, MAX(ConciliationCId) AS ConciliationCId
		FROM Glosas.ConciliationD
		GROUP BY InvoiceNumber
	) AS CD ON DG.InvoiceNumber = CD.InvoiceNumber 
	LEFT OUTER JOIN Glosas.ConciliationC AS CC with (nolock) ON CD.ConciliationCId = CC.Id 
	LEFT OUTER JOIN Glosas.TransferJuridicalDebtCollectionD AS tcj with (nolock) ON tcj.InvoiceNumber = DG.InvoiceNumber 
	LEFT OUTER JOIN Portfolio.AccountReceivable as ar with (nolock) on ar.InvoiceNumber=dg.InvoiceNumber and ar.Balance > '0' 
	left outer join billing.invoice as i on i.invoicenumber=dg.invoicenumber 
	LEFT JOIN Billing.InvoiceCategories AS CAT ON CAT.Id = I.InvoiceCategoryId 
	left outer join .adingreso as ing on ing.NUMINGRES=i.AdmissionNumber 
	left outer join dbo.HCREGEGRE AS eg WITH (nolock) ON eg.numingres=i.AdmissionNumber 
	LEFT JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE =ING.CODCENATE 
	--WHERE CAST(C.RadicatedDate AS DATE) between @FECHAINI AND @FECHAFIN