

--CREATE OR ALTER PROCEDURE [Glosas].[SP_GLOSAS_POR_CONCEPTO]
--DECLARE @FECHAINI AS DATETIME='2024-05-01';
--DECLARE @FECHAFIN AS DATETIME='2024-05-30';
--AS

create view [Report].[UploadCubeVieRCMGlosaByconcept] AS

	SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		DG.InvoiceNumber AS 'NRO FACTURA',--[NroFactura], 
		CAST(G.InvoiceDate AS DATE) AS 'FECHA FACTURA',--[FechaFactura],
		G.InvoiceValueEntity AS 'VALOR FACTURA',--[ValorFactura],
		CAR.Balance AS 'SALDO CARTERA',--[SaldoCartera],
		C.Nit 'NIT',--[NIT],  
		C.Name AS 'ENTIDAD',--[Entidad],
		CO.Code AS 'CODIGO CONCEPTO',-- [CodigoConcepto],  
		CO.NameGeneral AS 'GENERAL',--[General],  
		CO.NameSpecific AS 'ESPECIFICO',-- [Especifico],  
		DG.RationaleGlosa AS 'COMENTARIO',--[Comentario],
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
		DE.MedicalCode AS 'NRO IDENTIFICACION PROFESIONAL ORDENO',--[NroIdentificacionProfesionalOrdeno],  
		DE.MedicalName AS 'PROFESIONAL ORDENO',--[ProfesionalOrdeno],  
		G.UserNameInvoice AS 'CODIGO FACTURADOR',--[CodigoFacturador],
		PER.Fullname AS 'FACTURADOR',--[Facturador],
		G.RadicatedNumber AS 'NRO RADICADO',--[NroRadicado], 
		CAST(G.RadicatedDate AS DATE) AS 'FECHA RADICADO',--[FechaRadicado],
		GC.RadicatedConsecutive   'NRO RECPECION OBJECION',--[NroRecepcionObjecion], 
		CAST(GC.RadicatedDate AS DATE) 'FECHA RECEPCION OBJECION',--[FechaRecepcionObjecion],  
		CAST(GC.DocumentDate AS DATE) AS 'FECHA OFICIO RADICACION GLOSA',--[FechaOficioRadicacionGlosa],
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
		RES.Name 'RESPUESTA TRAMITE',--[ResponsableTramite]
		CAST(GC.RadicatedDate AS DATE) [FECHA BUSQUEDA],
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
	LEFT JOIN Security.PersonINT PER ON PER.Identification =G.UserNameInvoice 
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
	LEFT JOIN Glosas .Responsible AS RES ON RES.Id =DG.ResponsibleId 
	WHERE YEAR(GC.RadicatedDate)>=2022
	--CAST(GC.RadicatedDate AS DATE) BETWEEN @FECHAINI AND @FECHAFIN
	--(G.InvoiceDate >= '2019-01-01 00:00:00')-- and CAR.Balance>0

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el detalle de glosas por concepto con datos de factura, cartera, paciente, conciliación y responsables, para alimentar un cubo analítico de RCM.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaByconcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las recepciones de objeción deben tener fecha de radicado con año >= 2022; Los detalles de objeción no deben estar en estado ''4'' (anulado/excluido) para incluirse en el cruce', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaByconcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor conciliado se calcula como la suma de ValueAcceptedIPSconciliation + ValueAcceptedEAPBconciliation; ID_COMPANY se deriva del nombre de la base de datos actual (DB_NAME); La fecha de última actualización se calcula convirtiendo GETDATE() a zona horaria ''Pakistan Standard Time''; Para cada factura sólo se toma la última conciliación (MAX(id) y MAX(ConciliationDate)) por número de factura; Se excluyen ítems de objeción con State = ''4''; Se excluyen cuentas por cobrar con AccountReceivableType = ''6''; Sólo se contemplan terceros con PersonType = ''2'' al cruzar con ThirdParty', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaByconcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Factura; Cartera / Saldo cartera; Conciliación de glosas; Concepto de glosa; Categoría de factura; Paciente; Centro de atención; Centro de costo; Profesional que ordenó; Facturador; Radicación de objeción; Valor glosado; Valor aceptado IPS / EAPB; Valor reiterado; Primera y segunda instancia de glosa; Ámbito ambulatorio/hospitalario; Tipo de tecnología (servicio/medicamento); Tipo CUPS; Responsable de trámite; EPS/EAPB / Entidad pagadora', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaByconcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un dataset por cada movimiento de glosa con año de RadicatedDate >= 2022, enriquecido con factura, cartera, paciente, conceptos, profesional, conciliación y centro de atención', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaByconcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DE.TypeServiceProduct = ''1'' → Tipo Tecnología = ''Servicio'' else Si =2 entonces ''Medicamento o Insumo''; si DE.TypeProcedure = ''1'' → Tipo CUPS = ''No quirurgico'' else 2=''Quirurgico'', 3=''Paquete'', 4=''NoAplica''; si ing.tipoingre = 1 → Ámbito = ''Ambulatorio'' else 2 = ''Hospitalario''; si T.PersonType = ''2'' → Sólo se enlaza ThirdParty cuando el tercero es persona jurídica; si CAR.AccountReceivableType <> ''6'' → Sólo se considera la cartera cuyo tipo de cuenta por cobrar no sea ''6''; si YEAR(GC.RadicatedDate) >= 2022 → Filtra recepciones de objeción radicadas desde 2022 en adelante', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaByconcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaMovementGlosa; Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Common.Customer; Security.PersonINT; Common.ConceptGlosas; Glosas.GlosaInvoiceDetail; Billing.Invoice; Billing.InvoiceCategories; Contract.CareGroup; Contract.Contract; Portfolio.AccountReceivable; Common.ThirdParty; dbo.INPACIENT; Glosas.ConciliationD; Glosas.ConciliationC; dbo.ADINGRESO; dbo.ADCENATEN; Glosas.Responsible', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaByconcept';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMGlosaByconcept';
GO
