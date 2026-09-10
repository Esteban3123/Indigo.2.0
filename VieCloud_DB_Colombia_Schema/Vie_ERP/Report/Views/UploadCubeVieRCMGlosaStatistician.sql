
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a la carga de un cubo analítico con estadísticas del proceso de glosas RCM. Consolida, por factura, el ciclo completo de glosa: radicación de objeciones, conciliaciones (primera y segunda instancia, IPS/EAPB), cobro jurídico, cartera pendiente y valores aceptados/reiterados. Enriquece la información financiera con datos clínicos de admisión y egreso hospitalario, categoría de factura y centro de atención, facilitando análisis multidimensional de recuperación de cartera glosada por entidad pagadora.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida información de glosas radicadas, sus conciliaciones, valores facturados/aceptados, traslados a cobro jurídico y datos de admisión/egreso del paciente, para alimentar un cubo estadístico de RCM.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Glosas.GlosaPortfolioGlosada vinculados a Glosas.GlosaObjectionsReceptionD/C (INNER JOIN obligatorio).; Cada factura glosada debe tener un cliente válido en Common.Customer (INNER JOIN sobre CustomerId).; Los valores de C.State deben estar entre 1 y 4, y CC.State entre 1 y 2 para que la decodificación textual no devuelva NULL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Para cada par (PortfolioGlosaId, InvoiceNumber) se selecciona únicamente la recepción de objeción con MAX(GlosaObjectionsReceptionCId), es decir la más reciente.; Para cada InvoiceNumber se considera únicamente la conciliación con MAX(ConciliationCId), es decir la más reciente.; El campo ID_COMPANY refleja la base de datos en ejecución (DB_NAME) truncada a 9 caracteres.; El campo ULT_ACTUAL se calcula con la hora actual convertida a la zona ''Pakistan Standard Time''.; El filtro ar.Balance > ''0'' restringe el cruce con AccountReceivable a cuentas con saldo pendiente.; FECHA BUSQUEDA es siempre igual a la fecha de radicado de la glosa (C.RadicatedDate).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Radicación de glosa; Conciliación de glosa; Objeción; Factura; Entidad pagadora (EPS/EAPB); Cobro jurídico; Cartera / Cuenta por cobrar; Ingreso hospitalario; Egreso hospitalario; Centro de atención; Categoría de factura; Valor aceptado primera/segunda instancia; Saldo pendiente conciliar; Pago parcial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMGlosaStatistician: Devuelve una fila por combinación (factura glosada, última recepción de objeción por PortfolioGlosaId+InvoiceNumber, última conciliación por InvoiceNumber), enriquecida con datos de cliente, factura, categoría, ingreso, egreso y centro de atención.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.State = 1 → ESTADO RADICACION GLOSA = ''Sin Confirmar''; si C.State = 2 → ESTADO RADICACION GLOSA = ''ConfirmadoRadicado''; si C.State = 3 → ESTADO RADICACION GLOSA = ''OficioConRespuesta''; si C.State = 4 → ESTADO RADICACION GLOSA = ''Anulada''; si CC.State = 1 → ESTADO CONCILIACION = ''Sin Confirmar''; si CC.State = 2 → ESTADO CONCILIACION = ''Confirmado''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Common.Customer; Glosas.ConciliationD; Glosas.ConciliationC; Glosas.TransferJuridicalDebtCollectionD; Portfolio.AccountReceivable; billing.invoice; Billing.InvoiceCategories; dbo.adingreso; dbo.HCREGEGRE; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaStatistician';
GO
