



--CREATE PROCEDURE [Glosas].[SP_REPORTE_DEVOLUCIONES]
--	@OperatingUnitCode VARCHAR(20),
	--@DateStart DATE,
	--@DateEnd DATE
--AS


create view [Report].[UploadCubeVieRCMGlosaReturns] AS

SELECT   CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	DC.RadicatedConsecutive AS 'RADICADO DEVOLUCION',--[RadicadoDevolucion], 
	DD.InvoiceNumber AS 'NRO FACTURA',--[NroFactura], 
	DD.InvoiceDate AS 'FECHA FACTURA',--[FechaFactura], 
	DD.BalanceInvoice AS 'VALOR FACTURA',--[ValorFactura], 
	CASE AR.PortfolioStatus 
		WHEN '1' THEN 'SIN RADICAR' 
		WHEN '2' THEN 'RADICADA SIN CONFIRMAR' 
		WHEN '3' THEN 'RADICADA ENTIDAD' 
		WHEN '7' THEN 'CERTIFICADA PARCIAL' 
		WHEN '8' THEN 'CERTIFICADA TOTAL' 
		WHEN '14' THEN 'DEVOLUCION FACTURA ' 
		WHEN '15' THEN 'TRASLADO COBRO JURÍDICO CONFIRMADO' END AS 'ESTADO CARTERA',--[EstadoCartera], 
	DD.RadicatedDate AS 'FECHA RADICACION',--[FechaRadicacion], 
	DC.DocumentDate AS 'FECHA RECEPCION DEVOLUCION',--[FechaRecepcionDevolucion], 
	DD.RadicatedNumber AS 'CONSECUTIVO RADICACION',--[ConsecutivoRadicacion], 
	AR.Balance AS 'SALDO CARTERA',--[SaldoCartera], 
	DC.RadicatedDate AS 'FECHA RADICACION DEVOLUCION',--[FechaRadicacionDevolucion], 
	U.NOMUSUARI AS  'FACTURADOR',--[Facturador], 
	ca.nomcenate AS 'CENTRO ATENCION',--[CentroAtencion], 
	uf.ufucodigo AS 'CODIGO UNIDAD FUNCIONAL',--[CodigoUnidadFuncional],
	uf.ufudescri AS 'UNIDAD FUNCIONAL',--[UnidadFuncional],
	E.Name AS 'ENTIDAD',--Entidad, 
	CASE MV.TypeDevolution 
		WHEN '1' THEN 'Justificada' 
		WHEN '2' THEN 'Injustificada' 
		ELSE 'Sin Gestión' END AS 'TIPO DEVOLUCION',--[TipoDevolucion], 
	MV.CreationDate AS 'FECHA OPCION JUSTIFICA INJUSTIFICA',--[FechaOpcionJustificaInjustifica],
	C.Code as 'CODIGO CONCEPTO DEVOLUCION',--[CodigoConceptoDevolucion] ,
	C.NameSpecific AS 'CONCEPTO DEVOLUCION',--[ConceptoDevolucion], 
	MV.Comment AS 'MOTIVO EAPB',--[MotivoEAPB], 
	MV.Answer AS 'RESPUESTA IPS',--[RespuestaIPS], 
	MV.CreationUser AS 'USUARIO REGISTRO DEVOLUCION',--[UsuarioRegistroDevolucion],
	U2.NOMUSUARI 'NOMBRE USUARIO INGRESO DEVOLUCION',--[NombreUsuarioIngresoDevolucion]
	CAST(DC.DocumentDate  AS DATE) [FECHA BUSQUEDA],
	CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

FROM Glosas.GlosaDevolutionsReceptionD AS DD WITH (nolock) 
INNER JOIN Glosas.GlosaDevolutionsReceptionC AS DC WITH (nolock) ON DD.GlosaDevolutionsReceptionCId = DC.Id FULL 
OUTER JOIN Glosas.GlosaMovementDevolutions AS MV WITH (nolock) ON DD.Id = MV.IdDevolutionsReceptionD 
LEFT OUTER JOIN Portfolio.AccountReceivable AS AR WITH (nolock) ON DD.InvoiceNumber = AR.InvoiceNumber AND AR.AccountReceivableType = '2' 
LEFT OUTER JOIN dbo.SEGusuaru AS U WITH (nolock) ON AR.CreationUser = U.CODUSUARI 
LEFT OUTER JOIN Common.ConceptGlosas AS C WITH (nolock) ON C.Id = MV.IdConceptGlosa 
LEFT OUTER JOIN Common.Customer AS E WITH (nolock) ON DC.CustomerId = E.Id 
LEFT OUTER JOIN dbo.SEGusuaru AS U2 WITH (nolock) ON MV.CreationUser = U2.CODUSUARI
LEFT JOIN billing.invoice AS inv ON dd.invoicenumber = inv.invoicenumber  
LEFT JOIN dbo.adingreso AS ing ON inv.admissionnumber = ing.numingres
LEFT JOIN dbo.adcenaten AS ca ON ing.codcenate = ca.codcenate
LEFT JOIN dbo.inunifunc AS uf ON ing.ufucodigo = uf.ufucodigo