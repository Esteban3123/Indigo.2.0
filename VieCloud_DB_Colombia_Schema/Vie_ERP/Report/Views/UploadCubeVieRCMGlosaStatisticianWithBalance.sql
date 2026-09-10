

create view [Report].[UploadCubeVieRCMGlosaStatisticianWithBalance] AS

	SELECT  
		T.Nit 'NIT',--'NIT', 
		T.Name AS 'ENTIDAD',--'Entidad', 
		C.RadicatedConsecutive AS  'NRO RADICACION GLOSA',--'NroRadicacionGlosa', 
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
			WHEN CC.State = 2 THEN 'Confirmado' END AS 'EstadoConciliacion', 
		CC.ConciliationConsecutive AS 'NRO CONCILIACION',--'NroConciliacion', 
		C.Comment AS 'OBSERVACION',--'Observaciones', 
		CAST(CC.ConciliationDate AS DATE) AS 'FECHA REGISTRO CONCILIACION',--'FechaRegistroConciliacion', 
		CAST(CC.ConfirmDate AS DATE) AS 'FECHA CONFIRMACION CONCILIACION',--'FechaConfirmacionConciliacion', 
		CAST(CC.DocumentDate AS DATE) AS 'FECHA OFICIO CONCILIACION',--'FechaOficioConciliacion', 
		CAST(C.DocumentDate AS DATE) AS 'FECHA OFICIO RADICADO GLOSA',--'FechaOficioRadicadoGlosa', 
		CAST(C.DateRadicatedDocumentReply AS DATE) AS 'FECHA CONFIRMACION CONSECUTIVO RESPUESTA',--'FechaConfirmacionConsecutivoRespuesta', 
		CAST(DG.CoordinationDateGlosa AS DATE) AS 'FECHA CONFIRMACION RESPUESTA GLOSA FACTURA',--'FechaConfirmacionRespuestaGlosaFactura',
		CAST(ing.IFECHAING AS DATE)  as 'FECHA INGRESO',--'FechaIngreso', 
		CAST(eg.FECALTPAC AS DATE)  as 'FECHA EGRESO',--'FechaEgreso',
		CAT.Name AS 'CATEGORIA',--'Categoria',
		CEN.NOMCENATE AS 'CENTRO ATENCION',--'CentroAtencion'
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
	left outer join adingreso as ing on ing.NUMINGRES=i.AdmissionNumber 
	left outer join dbo.HCREGEGRE AS eg WITH (nolock) ON eg.numingres=i.AdmissionNumber 
	LEFT JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE =ING.CODCENATE 
	WHERE (C.State <> 4) AND (DG.BalanceGlosa <> '0')  and ar.Balance>'0'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada al cubo de datos (OLAP/BI) para la gestión de glosas RCM con saldo pendiente. Consolida, por factura, los valores glosados, aceptados en primera y segunda instancia, conciliados (IPS y EAPB), pagos parciales, cobro jurídico y saldo pendiente de conciliar, junto con las fechas clave de radicación, respuesta y conciliación de la objeción. Solo incluye radicaciones activas (estado ≠ Anulada) con saldo de glosa y cartera mayor a cero, enriqueciendo con categoría de factura, centro de atención, y fechas de ingreso y egreso hospitalario del paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida información de glosas con saldo pendiente para alimentar un cubo de análisis, cruzando datos de radicación, conciliación, cobro jurídico, facturación e ingreso/egreso del paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura glosada debe tener saldo de glosa diferente de cero (BalanceGlosa <> 0).; La cuenta por cobrar asociada a la factura debe tener saldo mayor a cero (AccountReceivable.Balance > 0).; La recepción de objeciones (GlosaObjectionsReceptionC) no debe estar en estado 4 (Anulada).; Debe existir al menos un detalle de objeción (GlosaObjectionsReceptionD) asociado al portafolio de glosa por número de factura.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan glosas no anuladas (C.State <> 4).; Solo se reportan facturas con saldo de glosa pendiente (BalanceGlosa <> 0) y cuenta por cobrar con saldo > 0.; Por cada combinación PortfolioGlosaId+InvoiceNumber se considera únicamente la recepción de objeción más reciente (MAX GlosaObjectionsReceptionCId).; Por cada InvoiceNumber se considera únicamente la conciliación más reciente (MAX ConciliationCId).; El timestamp ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Las fechas se entregan truncadas a tipo DATE (sin hora) excepto ULT_ACTUAL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Radicación de glosa; Conciliación de glosa; Objeción; Factura; Cartera / Cuenta por cobrar; Cobro jurídico; Entidad pagadora (EPS/EAPB); Categoría de factura; Centro de atención; Ingreso hospitalario; Egreso hospitalario; Saldo pendiente por conciliar; Valor aceptado primera/segunda instancia; Valor reiterado; Pago parcial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMGlosaStatisticianWithBalance: Devuelve una fila por factura glosada activa con saldo pendiente, tomando la última recepción de objeción (MAX GlosaObjectionsReceptionCId) y la última conciliación (MAX ConciliationCId) por número de factura.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.State = 1 → Estado de radicación de glosa = ''Sin Confirmar''; si C.State = 2 → Estado de radicación de glosa = ''ConfirmadoRadicado''; si C.State = 3 → Estado de radicación de glosa = ''OficioConRespuesta''; si C.State = 4 → Estado de radicación de glosa = ''Anulada'' (excluido por filtro WHERE C.State <> 4); si CC.State = 1 → Estado de conciliación = ''Sin Confirmar''; si CC.State = 2 → Estado de conciliación = ''Confirmado''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Common.Customer; Glosas.ConciliationD; Glosas.ConciliationC; Glosas.TransferJuridicalDebtCollectionD; Portfolio.AccountReceivable; Billing.Invoice; Billing.InvoiceCategories; dbo.ADINGRESO; dbo.HCREGEGRE; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatisticianWithBalance';
GO
