

/*******************************************************************************************************************
Nombre: [Report].[UploadCubeVieFinanceTotalPortfolio]
Tipo:Procedimiento Vista
Observacion:Cubo del proceso de clasificación de triage para urgencias.
Profesional:Andres Cabrera
Fecha Creación:30-04-2024
Profesional revisión:
Fecha Revisión:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico:
Fecha:
Observaciones:
--------------------------------------
Version 3
Persona que modifico:
Fecha:
Observaciones:
***********************************************************************************************************************************/
CREATE view [Report].[UploadCubeVieFinanceTotalPortfolio]
AS

WITH CTE_CARTERA
AS
(
  SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,CI.NAME 'CIUDAD',RTRIM(c.NOMCENATE) AS [CENTRO DE ATENCION],
		CASE ar.PortfolioStatus
			WHEN 1 THEN 'SIN RADICAR'
			WHEN 2 THEN 'RADICADA SIN CONFIRMAR'
			ELSE 'RADICADA ENTIDAD' END [ESTADO CARTERA],tp.Nit NIT,tp.Name [TERCERO],con.contractnumber AS [CONTRATO],	ISNULL(cg.Name,'SALDO INICIAL') [GRUPO DE ATENCION],
		       CASE CG.EntityType WHEN 1 THEN 'EPS Contributivo' 
					  WHEN 2 THEN 'EPS Subsidiado'
					  WHEN 3 THEN 'ET Vinculados Municipios' 
					  WHEN 4 THEN 'ET Vinculados Departamentos' 
					  WHEN 5 THEN 'ARL Riesgos Laborales' 
					  WHEN 6 THEN 'MP Medicina Prepagada'
					  WHEN 7 THEN 'IPS Privada' 
					  WHEN 8 THEN 'IPS Publica' 
					  WHEN 9 THEN 'Regimen Especial' 
					  WHEN 10 THEN 'Accidentes de transito' 
					  WHEN 11 THEN 'Fosyga' 
					  WHEN 12 THEN 'Otros'
					  WHEN 13 THEN 'Aseguradoras' 
					  WHEN 99 THEN 'Particulares' ELSE 'Otros' END [REGIMEN],
			CASE ar.AccountReceivableType 
			WHEN 1 THEN 'Facturación Básica' 
			WHEN 2 THEN  'Facturación Ley 100' 
			WHEN 3 THEN 'Impuestos Industria y Comercio'
			WHEN 4 THEN 'Pagarés' 
			WHEN  5 THEN 'Acuerdos de Pago' 
			WHEN 6 THEN 'Documento de Pago a Cuota Moderadora' 
			WHEN 7 THEN 'Factura de Producto'
			WHEN 8 THEN 'Impuesto Predial' END AS [TIPO DOCUMENTO],ar.InvoiceNumber [NRO FACTURA],CAST(AR.Value AS NUMERIC) [VALOR FACTURA],CAST(AR.Balance AS NUMERIC) [SALDO FACTURA],
		CAST(ar.AccountReceivableDate AS DATE) [FECHA FACTURA], ar.ThirdPartyId,CASE ar.Status	WHEN 3 THEN 'ANULADO' ELSE 'FACTURADO' END [ESTADO FACTURA],
		case ar.OpeningBalance 	when 1 then 'Si' ELSE 'No' END AS [SALDO INICIAL] ,		i.InvoicedUser +' - '+ SU.NOMUSUARI AS [USUARIO FACTURO],cat.Name [CATEGORIA],
		CAST(i.initialdate AS DATE) AS [FECHA INGRESO],	CAST(i.outputdate AS DATE) AS [FECHA EGRESO CORTE],DocumentType,AR.TimeStamp,pn.id 'IdNotaAnula',bb.ThirdPartyEntityCopayId

  FROM Portfolio.AccountReceivable ar WITH (NOLOCK)
  INNER JOIN Common.ThirdParty tp WITH (NOLOCK) ON tp.Id = ar.ThirdPartyId 
  LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON ar.CareGroupId = cg.Id
  LEFT JOIN contract.contract AS con ON cg.contractid = con.id
  LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON ar.InvoiceId = i.Id
  LEFT JOIN DBO.SEGusuaru SU WITH (NOLOCK) ON SU.CODUSUARI = i.InvoicedUser
  LEFT JOIN dbo.ADINGRESO a WITH(NOLOCK) ON i.AdmissionNumber = a.NUMINGRES
  LEFT JOIN dbo.ADCENATEN c WITH(NOLOCK) ON a.CODCENATE = c.CODCENATE
  --LEFT JOIN GeneralLedger.MainAccounts AS mar WITH (NOLOCK) ON mar.Id = ar.AccountWithoutRadicateId 
  --LEFT JOIN Portfolio.GetRegimes() pgr ON mar.Number = pgr.AccountNumber       
  LEFT JOIN Billing.InvoiceCategories cat WITH(NOLOCK) ON cat.id = i.InvoiceCategoryId
  LEFT JOIN Common.OperatingUnit OU WITH(NOLOCK) ON OU.ID=ar.OperatingUnitId
  LEFT JOIN Common.City CI WITH(NOLOCK) ON OU.IdCity=CI.Id
  LEFT JOIN Billing.BasicBilling AS BB ON BB.InvoiceId=i.Id
  LEFT JOIN Billing.BillingNoteDetail bnd ON BND.InvoiceId=i.id
  LEFT JOIN Billing.BillingNote bn on bnd.BillingNoteId =bn.Id
  LEFT JOIN Portfolio.PortfolioNote  PN ON PN.ID=bn.EntityId
  WHERE ar.Status <>3 AND I.DocumentType<>5 AND (PN.Id is null OR bb.ThirdPartyEntityCopayId is null)
  --and ar.InvoiceNumber in ('QC123855','COPQ104','CP596')
),

CTE_RADICACION
AS
(
   SELECT rid.InvoiceNumber, MIN(ri.Id) Id, MIN(rid.RadicatedNumber) RadicatedNumber
   FROM Portfolio.RadicateInvoiceC ri 
   INNER JOIN Portfolio.RadicateInvoiceD rid ON ri.Id = rid.RadicateInvoiceCId
   INNER JOIN CTE_CARTERA AS FAC ON FAC.[NRO FACTURA] =RID.InvoiceNumber 
   WHERE rid.State <>'4'
   GROUP BY rid.InvoiceNumber
),

CTE_DEVOLUCION
AS
(
  SELECT DEV.InvoiceNumber ,DEV.BalanceInvoice 'VALOR DEVOLUCION' ,CASE CAB.State WHEN  1 THEN 'DEVOLUCION SIN CONFIRMAR'
  WHEN 2 THEN 'DEVOLUCION CONFIRMADO RADICADO' WHEN 3 THEN 'DEVOLUCION OFICIO CON RESPUESTA ENVIADA' WHEN 4 THEN 'ANULADO' END 'ESTADO DEVOLUCION',TER.ID tercero
    FROM Glosas .GlosaDevolutionsReceptionD DEV 
    INNER JOIN
	(SELECT max(DEV.GlosaDevolutionsReceptionCId) IdCabeceraDev,DEV.InvoiceNumber 
     FROM Glosas .GlosaDevolutionsReceptionD DEV 
	 --WHERE dev.InvoiceNumber ='IND19626'
     GROUP BY DEV.InvoiceNumber
	) AS G ON G.IdCabeceraDev =DEV.GlosaDevolutionsReceptionCId AND G.InvoiceNumber =DEV.InvoiceNumber 
	INNER JOIN Glosas.GlosaDevolutionsReceptionC AS cab ON cab.Id =dev.GlosaDevolutionsReceptionCId
	INNER JOIN Common.Customer as cli on cli.Id =cab.CustomerId
	INNER JOIN Common.ThirdParty AS TER  ON TER.Id =CLI.ThirdPartyId
	where cab.State<>'4'
)

SELECT CAR.ID_COMPANY,CAR.CIUDAD,CAR.[CENTRO DE ATENCION],
       
   IIF(CAR.[SALDO FACTURA]=0 AND [ESTADO CARTERA] IN ('SIN RADICAR','RADICADA SIN CONFIRMAR'),'RADICADA ENTIDAD',
     IIF(car.[TIPO DOCUMENTO]='Documento de Pago a Cuota Moderadora' AND [ESTADO CARTERA] IN ('SIN RADICAR','RADICADA SIN CONFIRMAR'),'RADICADA ENTIDAD',
	    IIF(CAR.DocumentType=3 OR CAR.DocumentType=6,'RADICADA ENTIDAD',CAR.[ESTADO CARTERA]))) [ESTADO CARTERA],

iif(dev.InvoiceNumber is null,'No','Si') [DEVOLUCION],CAR.[NIT],CAR.[TERCERO],CAR.[CONTRATO],
CAR.[GRUPO DE ATENCION],CAR.[REGIMEN],car.[TIPO DOCUMENTO],CAR.[NRO FACTURA],CAR.[VALOR FACTURA],CAR.[SALDO FACTURA],CAR.[FECHA FACTURA],

IIF(CAR.DocumentType=3 OR CAR.DocumentType=6,CAR.[FECHA FACTURA],RI.CreationDate) [FECHA CREACION RADICADO],
IIF(CAR.DocumentType=3 OR CAR.DocumentType=6,CAR.[FECHA FACTURA],ri.DocumentDate) [FECHA DOCUMENTO RADICADO],
IIF(CAR.DocumentType=3 OR CAR.DocumentType=6,CAR.[FECHA FACTURA],ri.RadicatedDate) [FECHA RADICACION],

IIF(CAR.DocumentType=3 OR CAR.DocumentType=6,CAR.[FECHA FACTURA],
   IIF(CAR.[SALDO FACTURA]=0 AND [ESTADO CARTERA] IN ('SIN RADICAR','RADICADA SIN CONFIRMAR'),ISNULL(ri.ConfirmDate,ISNULL(ri.RadicatedDate,CAR.[FECHA FACTURA])),ri.ConfirmDate)) [FECHA CONFIRMACION RADICADO],
RI.RadicatedConsecutive [CONSECUTIVO RADICACION],

CAR.[ESTADO FACTURA],CAR.[SALDO INICIAL],CAR.[USUARIO FACTURO],TRIM(RI.creationuser) + ' - ' + SUR.NOMUSUARI AS [USUARIO RADICO],
CAR.[CATEGORIA],CAR.[FECHA INGRESO],CAR.[FECHA EGRESO CORTE],
YEAR(IIF(CAR.DocumentType=3 OR CAR.DocumentType=6,CAR.[FECHA FACTURA],
   IIF(CAR.[SALDO FACTURA]=0 AND [ESTADO CARTERA] IN ('SIN RADICAR','RADICADA SIN CONFIRMAR'),ISNULL(ri.ConfirmDate,ISNULL(ri.RadicatedDate,CAR.[FECHA FACTURA])),ri.ConfirmDate))) 'AÑO CONFIRMACION RAD',
MONTH(IIF(CAR.DocumentType=3 OR CAR.DocumentType=6,CAR.[FECHA FACTURA],
   IIF(CAR.[SALDO FACTURA]=0 AND [ESTADO CARTERA] IN ('SIN RADICAR','RADICADA SIN CONFIRMAR'),ISNULL(ri.ConfirmDate,ISNULL(ri.RadicatedDate,CAR.[FECHA FACTURA])),ri.ConfirmDate))) 'MES CONFIRMACION RAD',

YEAR(CAR.[FECHA FACTURA]) 'AÑO FACTURA', MONTH(CAR.[FECHA FACTURA]) 'MES FACTURA',
CAST(CAR.[FECHA FACTURA] AS DATE) AS [FECHA BUSQUEDA],
   YEAR(CAR.[FECHA FACTURA]) AS [AÑO BUSQUEDA],
   MONTH(CAR.[FECHA FACTURA]) AS [MES BUSQUEDA],  CAR.TimeStamp,
   CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

FROM CTE_CARTERA CAR
 LEFT JOIN CTE_RADICACION AS RIC ON RIC.InvoiceNumber =CAR.[NRO FACTURA]
 LEFT JOIN Portfolio.RadicateInvoiceC ri WITH (NOLOCK) ON ric.Id = ri.Id
 LEFT JOIN DBO.SEGusuaru SUR WITH (NOLOCK) ON SUR.CODUSUARI=ri.creationuser
 LEFT JOIN CTE_DEVOLUCION AS DEV  ON DEV.InvoiceNumber =CAR.[NRO FACTURA] AND DEV.tercero = CAR.ThirdPartyId 
 --WHERE CAR.DocumentType=1
 --WHERE [ESTADO CARTERA] IN ('SIN RADICAR','RADICADA SIN CONFIRMAR')
 --WHERE CAR.[SALDO FACTURA]=0
 --WHERE car.[TIPO DOCUMENTO]='Documento de Pago a Cuota Moderadora'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de cubo de cartera total para carga a herramienta OLAP/BI. Consolida, por factura de cobro, el estado de cartera (sin radicar, radicada sin confirmar, radicada entidad), información de radicación ante entidades pagadoras, devoluciones de glosas, régimen, tercero/contrato, valores y saldos, fechas clave (factura, radicado, confirmación) y datos de ingreso del paciente. Consume módulos de Portfolio, Billing, Contract, Glosas y tablas legacy HIS para ofrecer una vista aplanada destinada a reporting financiero de cartera por sede, ciudad y período.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la cartera total facturada con su estado de radicación y devolución de glosas, para alimentar el cubo financiero de cartera por entidad pagadora.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben existir en Portfolio.AccountReceivable con Status<>3 (no anuladas).; Las facturas deben tener DocumentType<>5 en Billing.Invoice.; Solo se incluyen registros donde no exista nota de cartera (PortfolioNote) o cuyo BasicBilling no tenga ThirdPartyEntityCopayId.; Para considerar radicación, los detalles en Portfolio.RadicateInvoiceD deben tener State<>''4'' (no anulado).; Para considerar devolución, GlosaDevolutionsReceptionC.State debe ser distinto de ''4'' (no anulado).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye facturas anuladas (Status=3) y documentos con DocumentType=5 en Billing.Invoice.; Excluye facturas vinculadas simultáneamente a una PortfolioNote y a un BasicBilling con ThirdPartyEntityCopayId no nulo.; Solo considera radicaciones y devoluciones cuyo estado no sea 4 (anulado).; Para cada factura toma la radicación con MIN(ri.Id) y MIN(rid.RadicatedNumber), garantizando un único radicado por factura.; Para cada factura toma la última cabecera de devolución (MAX(GlosaDevolutionsReceptionCId)) por InvoiceNumber.; Las facturas de DocumentType 3 o 6 se reportan siempre como ''RADICADA ENTIDAD'' con fechas de radicación iguales a la fecha de factura.; El timestamp ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; El ID_COMPANY se obtiene de DB_NAME() truncado a 9 caracteres.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cartera; Factura; Radicación de factura; Devolución de glosas; Grupo de atención; Régimen (EPS, ARL, Medicina Prepagada, IPS, Fosyga, Particulares); Cuota moderadora; Centro de atención; Tercero/Cliente pagador; Contrato; Saldo inicial; Acuerdo de pago; Pagaré', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieFinanceTotalPortfolio: Devuelve un dataset por factura de cartera con datos de tercero, contrato, grupo de atención, régimen, estado cartera, radicación, devolución y fechas/años/meses calculados para el cubo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.PortfolioStatus = 1 / 2 / otro → Clasifica [ESTADO CARTERA] como ''SIN RADICAR'', ''RADICADA SIN CONFIRMAR'' o ''RADICADA ENTIDAD''.; si CAR.[SALDO FACTURA]=0 AND [ESTADO CARTERA] IN (''SIN RADICAR'',''RADICADA SIN CONFIRMAR'') → Forzar [ESTADO CARTERA]=''RADICADA ENTIDAD''. else Mantener estado original o aplicar reglas siguientes.; si [TIPO DOCUMENTO]=''Documento de Pago a Cuota Moderadora'' AND [ESTADO CARTERA] IN (''SIN RADICAR'',''RADICADA SIN CONFIRMAR'') → Forzar [ESTADO CARTERA]=''RADICADA ENTIDAD''.; si CAR.DocumentType=3 OR CAR.DocumentType=6 → Forzar [ESTADO CARTERA]=''RADICADA ENTIDAD'' y usar [FECHA FACTURA] como fecha de creación, documento, radicación y confirmación del radicado.; si CAR.[SALDO FACTURA]=0 AND estado en SIN RADICAR/RADICADA SIN CONFIRMAR (rama de DocumentType<>3,6) → Fecha confirmación radicado = ISNULL(ri.ConfirmDate, ISNULL(ri.RadicatedDate, [FECHA FACTURA])).; si CG.EntityType (1..13,99) → Mapea a etiquetas de régimen (EPS Contributivo, Subsidiado, ARL, IPS Pública/Privada, Particulares, etc.).; si ar.AccountReceivableType (1..8) → Mapea a tipo de documento de cartera (Facturación Básica, Ley 100, Pagarés, Acuerdos de Pago, Cuota Moderadora, etc.).; si ar.Status=3 → Etiqueta como ''ANULADO'' (no aplica por filtro), de lo contrario ''FACTURADO''.; si cab.State (1..4) en GlosaDevolutionsReceptionC → Clasifica [ESTADO DEVOLUCION] como ''DEVOLUCION SIN CONFIRMAR'', ''CONFIRMADO RADICADO'', ''OFICIO CON RESPUESTA ENVIADA'' o ''ANULADO''.; si dev.InvoiceNumber IS NULL → [DEVOLUCION]=''No'' else [DEVOLUCION]=''Si''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Common.ThirdParty; Contract.CareGroup; contract.contract; Billing.Invoice; dbo.SEGusuaru; dbo.ADINGRESO; dbo.ADCENATEN; Billing.InvoiceCategories; Common.OperatingUnit; Common.City; Billing.BasicBilling; Billing.BillingNoteDetail; Billing.BillingNote; Portfolio.PortfolioNote; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Glosas.GlosaDevolutionsReceptionD; Glosas.GlosaDevolutionsReceptionC; Common.Customer', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieFinanceTotalPortfolio';
GO
