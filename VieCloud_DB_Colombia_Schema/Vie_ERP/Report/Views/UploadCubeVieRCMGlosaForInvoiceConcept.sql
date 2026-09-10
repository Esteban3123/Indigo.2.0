
--CREATE PROCEDURE [Glosas].[SP_GLOSAS_POR_CONCEPTO_FACTURA]
--@FACTURA AS VARCHAR(22)
--AS
-- Compras medicamentos mes a mes    -- costo de lo que se ganstan total mes ames cunsumo -- Inventario valorizado mes a mes

create view [Report].[UploadCubeVieRCMGlosaForInvoiceConcept] AS

	SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		DG.InvoiceNumber AS 'NRO FACTURA',-- [NroFactura] , 
		CAST(G.InvoiceDate AS DATE) AS 'FECHA FACTURA',-- [FechaFactura],
		G.InvoiceValueEntity AS 'VALOR FACTURA',-- [ValorFactura],
		CAR.Balance AS 'SALDO CARTERA',-- [SaldoCartera],
		C.Nit 'NIT',--[NIT],  
		C.Name AS 'ENTIDAD',--[Entidad],
		CO.Code AS 'CODIGO CONCEPTO',--[CodigoConcepto],  
		CO.NameGeneral AS 'GENERAL',--[General],  
		CO.NameSpecific AS 'ESPECIFICO',--[Especifico],  
		DG.RationaleGlosa AS 'COEMNTARIO',--[Comentario],
		CASE 
			WHEN DE.TypeServiceProduct = '1' THEN 'Servicio'  
			WHEN DE.TypeServiceProduct = '2' THEN 'Medicamento o Insumo' 
			END AS 'TIPO TECNOLOGIA',--[TipoTecnologia], 
		CASE 
			WHEN DE.TypeProcedure = '1' THEN 'No quirurgico'
			WHEN DE.TypeProcedure = '2' THEN 'Quirurgico' 
			WHEN DE.TypeProcedure = '3' THEN 'Paquete'  
			WHEN DE.TypeProcedure = '4' THEN 'NoAplica' END AS 'TIPO CUPS',--[TipoCUP],
		DE.ServiceCode AS 'CODIGO SERVICIO',--[CodigoServicio],  
		DE.ServiceName AS 'SERVICIO',--[Servicio],  
		DE.UnitValue AS 'VALOR UNITARIO',--[ValorUnitario], 
		DE.Ammount AS 'CANTIDAD',--[Cantidad],
		DG.ValueGlosado AS 'VALOR GLOSADO',--[ValorGlosado],
		DE.MedicalCode AS 'NRO IDENTIFICACION PROFEIONAL ORDENO',--[NroIdentificacionProfesionalOrdeno],  
		DE.MedicalName AS 'PROFESIONAL ORDENO',--[ProfesionalOrdeno],  
		G.UserNameInvoice AS 'CODIGO FACTURADOR',--[CodigoFacturador],
		PER.Fullname AS 'FACTURADOR',--[Facturador],
		G.RadicatedNumber AS 'NRO RADICADO',--[NroRadicado], 
		CAST(G.RadicatedDate AS DATE) AS 'FECHA RADICADO',--[FechaRadicado],
		GC.RadicatedConsecutive   'NRO RECEPCION OBJECION',--[NroRecepcionObjecion], 
		CAST(GC.RadicatedDate AS DATE) 'FECHA RECEPCION OBJECION',--[FechaRecepcionObjecion],  
		CAST(GC.DocumentDate AS DATE) AS 'FECHA OFICIO RADICACION GLOSA ',-- [FechaOficioRadicacionGlosa],
		CAST(con.FechaConciliacion AS DATE) 'FECHA CONCILIACION',--[FechaConciliacion],
		DG.ValueAcceptedIPSconciliation AS 'VALOR ACEPTADO IPS',--[ValorAceptadoIPS], 
		DG.ValueAcceptedEAPBconciliation AS 'VALOR ACEPTADO EAPB',--[ValorAceptadoEAPB], 
		DG.ValueAcceptedIPSconciliation + DG.ValueAcceptedEAPBconciliation AS 'VALOR CONCILIADO',--[ValorConciliado],
		DG.ValuePendingConciliation AS 'VALOR PENDIENTE CONCILIAR',--[ValorPendienteConciliar], 
		DG.JustificationGlosaText AS 'JUSTIFICACION GLOSA',--[JustificacionGlosa], 
		CAT.Name AS 'CATEGORIA',--[Categoria],
		g.coordinationdateglosa AS 'FECHA CONFIRMACION RESPUESTA GLOSA FACTURADA',--[FechaConfirmacionRespuestaGlosaFacturada], 
		dg.valueacceptedfirstinstance AS 'VALOR ACEPTADO PRI INSTANCIA',--[ValorAceptadoPriInstancia], 
		dg.valuereiterated AS 'VALOR REITERADO',--[ValorReiterado], 
		dg.valueacceptedsecondinstance AS 'VALOR ACEPTADO SEG INSTANCIA',--[ValorAceptadoSegInstancia],
		F.PatientCode AS 'NRO IDENTIFICACION PACIENTE',--[NroIdentificacionPaciente], 
		p.IPNOMCOMP AS 'PACIENTE',--[Paciente], 
		DE.CostCenterCode AS 'CODIGO CENTRO COSTO',--[CodigoCentroCosto], 
		DE.CostCenterName AS 'CENTRO COSTO',--[CentroCosto],
		CASE ing.tipoingre 
			WHEN 1 THEN 'Ambulatorio' 
			WHEN 2 THEN 'Hospitalario' END AS 'AMBITO',--[Ambito],
		CEN.NOMCENATE AS 'CENTRO ATENCION',--[CentroAtencion],
		RES.Name 'RESPONSABLE TRAMITE',--[ResponsableTramite]
		CAST(G.InvoiceDate AS DATE) [FECHA BUSQUEDA],
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM Glosas.GlosaMovementGlosa AS DG
	INNER JOIN Glosas.GlosaPortfolioGlosada AS G 
	INNER JOIN Glosas.GlosaObjectionsReceptionD AS RG 
	INNER JOIN Glosas.GlosaObjectionsReceptionC AS GC 
	LEFT JOIN Common.Customer AS C 
		ON GC.CustomerId = C.Id
		ON RG.GlosaObjectionsReceptionCId = GC.Id
		ON G.Id = RG.PortfolioGlosaId
		AND RG.State <> '4'
		ON DG.InvoiceNumber = G.InvoiceNumber
	LEFT JOIN Security .PersonINT PER ON PER.Identification =G.UserNameInvoice 
	LEFT JOIN Common.ConceptGlosas AS CO ON DG.CodeGlosaId = CO.Id
	LEFT JOIN Glosas.GlosaInvoiceDetail AS DE ON DG.InvoiceDetailId = DE.Id
	LEFT JOIN Billing.Invoice AS F 
	LEFT JOIN Billing.InvoiceCategories AS CAT ON CAT.Id = F.InvoiceCategoryId
	LEFT JOIN Contract.CareGroup AS GA 
	LEFT JOIN Contract.Contract AS CONT ON CONT.Id = GA.ContractId	ON F.CareGroupId = GA.Id
	LEFT JOIN Portfolio.AccountReceivable AS CAR 
	LEFT OUTER JOIN Common.ThirdParty AS T ON T.Id = CAR.ThirdPartyId AND T.PersonType = '2' ON CAR.InvoiceNumber = F.InvoiceNumber AND CAR.AccountReceivableType <> '6' ON F.InvoiceNumber = DG.InvoiceNumber
	LEFT JOIN dbo.INPACIENT AS P ON P.ipcodpaci = F.PatientCode
	LEFT OUTER JOIN (SELECT max(c.id) as ID, D.InvoiceNumber as Factura, max(c.ConciliationDate) as FechaConciliacion
	FROM Glosas.ConciliationD as d
	inner join Glosas.ConciliationC as c on c.Id=d.ConciliationCId
	group by d.InvoiceNumber) as con on con.Factura=dg.InvoiceNumber
	LEFT JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES =G.IngressNumber 
	LEFT JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE =ING.CODCENATE 
	LEFT JOIN Glosas.Responsible AS RES ON RES.Id =DG.ResponsibleId 
	WHERE YEAR(G.InvoiceDate)>=2022
	--WHERE DG.InvoiceNumber=@FACTURA

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los datos de facturas con glosas (recepción, conciliación, cartera, paciente, contrato y responsable) desde 2022 para alimentar un cubo de reportes RCM por concepto de glosa.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaForInvoiceConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe la base de datos vigente accesible vía DB_NAME() para identificar la compañía.; Las recepciones de objeciones consideradas deben tener State distinto de ''4'' para ser incluidas.; Sólo se consideran facturas cuyo año de InvoiceDate sea 2022 o posterior.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaForInvoiceConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de compañía siempre proviene del nombre de la base de datos en ejecución (DB_NAME()).; El ''Valor Conciliado'' siempre se calcula como la suma de ValueAcceptedIPSconciliation + ValueAcceptedEAPBconciliation.; Sólo se incluyen cuentas por cobrar cuyo AccountReceivableType sea distinto de ''6''.; Sólo se vinculan terceros de cartera cuyo PersonType sea ''2'' (jurídicas).; Para cada factura se toma sólo la conciliación con el máximo Id y máxima fecha de conciliación.; La fecha de última actualización se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; Las fechas de factura, radicado, recepción de objeción, oficio y conciliación se entregan truncadas a DATE.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaForInvoiceConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Factura; Concepto de glosa; Recepción de objeción; Conciliación de glosa (IPS/EAPB); Valor glosado; Valor reiterado; Cartera / Saldo cartera; Paciente; Profesional que ordena; Centro de atención; Centro de costo; Categoría de factura; Contrato y grupo de atención; Ámbito ambulatorio/hospitalario; Tipo CUPS (quirúrgico/no quirúrgico/paquete); Responsable de trámite; Facturador; Radicado', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaForInvoiceConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMGlosaForInvoiceConcept: Devuelve una fila por cada movimiento de glosa cruzado con su recepción de objeción (State<>''4'') y factura, filtrando YEAR(InvoiceDate) >= 2022.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaForInvoiceConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DE.TypeServiceProduct = ''1'' → Clasifica el ítem como ''Servicio'' else Si TypeServiceProduct = ''2'' clasifica como ''Medicamento o Insumo''; si DE.TypeProcedure = ''1'' → Clasifica el CUPS como ''No quirurgico'' else ''2''=''Quirurgico'', ''3''=''Paquete'', ''4''=''NoAplica''; si ING.tipoingre = 1 → Ámbito de atención = ''Ambulatorio'' else Si tipoingre = 2 → ''Hospitalario''; si YEAR(G.InvoiceDate) >= 2022 → La factura glosada se incluye en el reporte else Se excluye del resultado; si RG.State <> ''4'' → Se considera la recepción de objeción asociada a la glosa de cartera else Se descarta el cruce con esa recepción', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaForInvoiceConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaMovementGlosa; Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Common.Customer; Security.PersonINT; Common.ConceptGlosas; Glosas.GlosaInvoiceDetail; Billing.Invoice; Billing.InvoiceCategories; Contract.CareGroup; Contract.Contract; Portfolio.AccountReceivable; Common.ThirdParty; dbo.INPACIENT; Glosas.ConciliationD; Glosas.ConciliationC; dbo.ADINGRESO; dbo.ADCENATEN; Glosas.Responsible', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaForInvoiceConcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaForInvoiceConcept';
GO
