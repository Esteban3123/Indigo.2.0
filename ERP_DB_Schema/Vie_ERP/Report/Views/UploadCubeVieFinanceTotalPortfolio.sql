


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