

--CREATE PROCEDURE [Glosas].[SP_REPORTE_DEVOLUCIONES_TRAZABILIDAD]
--DECLARE @FECINI AS DATETIME='2024-05-01';
--DECLARE @FECFIN AS DATETIME='2024-05-16';
-- AS

create view [Report].[UploadCubeVieRCMGlosaReturnsTraceability] AS

	WITH CTE_INGRESOS_FACTURAS AS
	(
		SELECT DD.Id AS [ID DET],DC.RadicatedConsecutive AS [RadicadoDevolucion], DD.InvoiceNumber AS FACTURA, DD.InvoiceDate AS [FechaFactura], DD.BalanceInvoice AS [ValorFactura],
			DD.RadicatedDate AS [FechaRadicacion], DC.DocumentDate AS [FechaRecepcionDevolucion],DC.RadicatedDate AS [FechaRadicadoDevolucion],DC.CustomerId 'CLIENTE',DD.RadicatedNumber AS [ConsecutivoRadicacion],
		Ingress 'INGRESO'  
		FROM Glosas.GlosaDevolutionsReceptionD AS DD WITH (nolock) INNER JOIN
		Glosas.GlosaDevolutionsReceptionC AS DC WITH (nolock) ON DD.GlosaDevolutionsReceptionCId = DC.Id
		WHERE YEAR(DC.DocumentDate)>=2022
		--CAST(DC.DocumentDate AS DATE) BETWEEN @FECINI AND @FECFIN

	 ),
	/*
	CTE_FACTURACION
	AS
	 (
	 SELECT FAC.AdmissionNumber ,FAC.InvoiceNumber,FAC.InvoiceDate [FechaFactura]  FROM Billing .Invoice AS FAC
	 INNER JOIN CTE_INGRESOS_FACTURAS AS ING ON ING.INGRESO =FAC.AdmissionNumber WHERE FAC.Status ='1'
	 ),
	*/
	CTE_RADICADO
	AS
	(
		SELECT DET.InvoiceNumber, MAX(CAST(DET.RadicatedNumber AS INT)) AS RadicatedNumber FROM Portfolio.RadicateInvoiceD  DET
		INNER JOIN CTE_INGRESOS_FACTURAS AS FAC ON DET.InvoiceNumber =FAC.FACTURA GROUP BY DET.InvoiceNumber --WHERE DET.State <> '4' 
	)

	SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,     
		CTE.[RadicadoDevolucion] 'RADICADO DEVOLUCION',--,
		CTE.FACTURA AS 'NRO FACTURA',-- [NroFactura], 
		CTE.[FechaFactura] 'FECHA FACTURA',--, 
		CTE.[ValorFactura] 'VALOR FACTURA',--, 
		CASE AR.PortfolioStatus 
			WHEN '1' THEN 'SIN RADICAR' 
			WHEN '2' THEN 'RADICADA SIN CONFIRMAR' 
			WHEN '3' THEN 'RADICADA ENTIDAD' 
			WHEN '7' THEN 'CERTIFICADA PARCIAL' 
			WHEN '8' THEN 'CERTIFICADA TOTAL' 
			WHEN '14' THEN 'DEVOLUCION FACTURA ' 
			WHEN '15' THEN 'TRASLADO COBRO JURÍDICO CONFIRMADO' END AS 'ESTADO CARTERA',-- [EstadoCartera],
		CTE.[FechaRadicacion] 'FECHA RADICADO',--, 
		CTE.[FechaRecepcionDevolucion] 'FECHA RECEPCION DEVOLUCION',--, 
		CTE.[ConsecutivoRadicacion] 'CONSECUTIVO RADICADO',--, 
		AR.Balance AS 'SALDO CARTERA',--[SaldoCartera], 
		CTE.[FechaRadicadoDevolucion] 'FECHA RADICACION DECOLUCION',--, 
		U.NOMUSUARI AS 'FACTURADOR',--[Facturador] , 
		E.Name AS 'ENTIDAD',--[Entidad], 
		CASE MV.TypeDevolution 
			WHEN '1' THEN 'Justificada' 
			WHEN '2' THEN 'Injustificada' 
			ELSE 'Sin Gestión' END AS 'TIPO DEVOLUCION',--[TipoDevolucion], 
		MV.CreationDate AS 'FECHA OPCION JUSTIFICA INJUSTIFICA',--[FechaOpcionJustificaInjustifica],
		C.Code as 'CODIGO CONCEPTO DEVOLUCION',--[CodigoConceptoDevolucion],
		C.NameSpecific AS 'CONCEPTO DEVOLUCION',--[ConceptoDevolucion], 
		MV.Comment AS 'MOTIVO EAPB',--[MotivoEAPB], 
		MV.Answer AS 'RESPUESTA IPS',--[RespuestaIPS], 
		MV.CreationUser AS 'USUARIO INGRESO DEVOLUCION',--[UsuarioIngresoDevolucion],
		U2.NOMUSUARI 'NOMBRE USUARIO INGRESO DEVOLUCION',--[NombreUsuarioIngresoDevolucion], 
		CAT.Name AS 'CATEGORIA',--[Categoria],
		CEN.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion],
		uf.ufucodigo AS 'CODIGO UNIDAD FUNCIONAL',--[CodigoUnidadFuncional],
		uf.ufudescri AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
		CTE.INGRESO AS 'NRO INGRESO',--[NroIngreso],
		CASE FAC.Status 
			WHEN 1 THEN 'FACTURADO' 
			ELSE 'ANULADA' END 'ESTADO FACTURA',--[EstadoFactura],
		/*FACA.InvoiceNumber 'NUEVA FACTURA',*/
		RAD.RadicatedNumber 'ULTIMO RADICADO',--[UltimoRadicado],
		rdt.radicateddate AS 'FECHA ILTIMO RADICADO',--[FechaUltimoRadicado]
		CTE.[FechaRecepcionDevolucion] [FECHA BUSQUEDA],
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM CTE_INGRESOS_FACTURAS AS CTE
	INNER JOIN Billing .Invoice AS FAC ON FAC.InvoiceNumber =CTE.FACTURA  
	INNER JOIN Glosas.GlosaMovementDevolutions AS MV WITH (nolock) ON CTE.[ID DET]  = MV.IdDevolutionsReceptionD LEFT OUTER JOIN
	Portfolio.AccountReceivable AS AR WITH (nolock) ON CTE.FACTURA  = AR.InvoiceNumber AND AR.AccountReceivableType = '2' LEFT OUTER JOIN
	dbo.SEGusuaru AS U WITH (nolock) ON AR.CreationUser = U.CODUSUARI LEFT OUTER JOIN
	Common.ConceptGlosas AS C WITH (nolock) ON C.Id = MV.IdConceptGlosa LEFT OUTER JOIN
	Common.Customer AS E WITH (nolock) ON CTE.CLIENTE = E.Id LEFT OUTER JOIN
	dbo.SEGusuaru AS U2 WITH (nolock) ON MV.CreationUser = U2.CODUSUARI
	--LEFT JOIN CTE_FACTURACION AS FACA ON FACA.AdmissionNumber =CTE.INGRESO AND FACA.InvoiceNumber <>CTE.FACTURA AND FACA.[FechaFactura] > CTE.[FechaFactura] 
	LEFT JOIN CTE_RADICADO AS RAD ON RAD.InvoiceNumber =CTE.FACTURA AND RAD.RadicatedNumber > CTE.[ConsecutivoRadicacion] 
	LEFT JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES =FAC.AdmissionNumber 
	LEFT JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE =ING.CODCENATE
	LEFT JOIN Billing.InvoiceCategories AS CAT ON CAT.Id = FAC.InvoiceCategoryId
	LEFT JOIN dbo.inunifunc AS uf ON ing.ufucodigo = uf.ufucodigo
	LEFT JOIN portfolio.radicateinvoiced AS rdt ON rad.invoicenumber = rdt.invoicenumber AND rad.radicatednumber = rdt.radicatednumber

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de trazabilidad de devoluciones de facturas glosadas que consolida información de la devolución, la factura, su estado de cartera, último radicado y datos administrativos (usuario, entidad, centro de atención, unidad funcional) desde 2022.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaReturnsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de recepciones de devoluciones de glosas con cabecera y detalle relacionados.; Cada detalle de devolución debe tener un movimiento de devolución asociado (INNER JOIN obligatorio).; La factura de la devolución debe existir en Billing.Invoice (INNER JOIN obligatorio).; Solo se consideran devoluciones cuya cabecera tenga año del DocumentDate >= 2022.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaReturnsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran cuentas por cobrar de tipo ''2'' (AccountReceivableType=''2'') para el estado y saldo de cartera.; El último radicado mostrado siempre es el máximo RadicatedNumber por factura (MAX(CAST(RadicatedNumber AS INT))) y mayor al consecutivo de radicación de la devolución.; Solo se incluyen devoluciones cuyo año de DocumentDate sea 2022 o posterior.; La fecha de actualización (ULT_ACTUAL) siempre se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; ID_COMPANY se obtiene del nombre de la base de datos truncado a 9 caracteres.; Solo se reportan devoluciones que tengan al menos un movimiento de devolución registrado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaReturnsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de glosa; Radicación de factura; Cartera / cuentas por cobrar; Estado de cartera; Concepto de glosa; Devolución justificada/injustificada; Entidad pagadora (cliente); Ingreso/admisión; Centro de atención; Unidad funcional; Facturador; Categoría de factura', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaReturnsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMGlosaReturnsTraceability: Devuelve un registro por cada detalle de devolución de glosa con su movimiento, factura, cartera (tipo ''2''), entidad, concepto de glosa y último radicado posterior al consecutivo de radicación de la devolución.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaReturnsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AR.PortfolioStatus IN (''1'',''2'',''3'',''7'',''8'',''14'',''15'') → Traduce el código a etiqueta legible: 1=SIN RADICAR, 2=RADICADA SIN CONFIRMAR, 3=RADICADA ENTIDAD, 7=CERTIFICADA PARCIAL, 8=CERTIFICADA TOTAL, 14=DEVOLUCION FACTURA, 15=TRASLADO COBRO JURÍDICO CONFIRMADO.; si MV.TypeDevolution = ''1'' / ''2'' / otro → Clasifica la devolución como ''Justificada'' (1), ''Injustificada'' (2) o ''Sin Gestión'' (cualquier otro valor). else Sin Gestión; si FAC.Status = 1 → Marca la factura como ''FACTURADO''. else ANULADA; si YEAR(DC.DocumentDate) >= 2022 → Incluye la recepción de devolución en la trazabilidad. else Se excluye del reporte.; si RAD.RadicatedNumber > CTE.ConsecutivoRadicacion → Asocia como ''ULTIMO RADICADO'' solo radicados posteriores al consecutivo de la devolución.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaReturnsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaDevolutionsReceptionD; Glosas.GlosaDevolutionsReceptionC; Portfolio.RadicateInvoiceD; Glosas.GlosaMovementDevolutions; Billing.Invoice; Portfolio.AccountReceivable; dbo.SEGusuaru; Common.ConceptGlosas; Common.Customer; dbo.ADINGRESO; dbo.ADCENATEN; Billing.InvoiceCategories; dbo.inunifunc', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaReturnsTraceability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaReturnsTraceability';
GO
