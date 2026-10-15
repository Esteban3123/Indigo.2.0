




/*******************************************************************************************************************
Nombre: [Report].[UploadCubeVieRCMTotalBillingPGP]
Tipo:Procedimiento Vista
Observacion:Cubo consolidado del proceso de facturación.
Profesional:Andres Cabrera
Fecha Creación:03-03-2024
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
Persona que modifico: Andrés Cabrera
Fecha: 22-04-2024
Observaciones: Se adiciona el estado de la factura en la variable Idunique, ya que cuando se anula la factura aparece duplicado
--------------------------------------

***********************************************************************************************************************************/


CREATE view [Report].[UploadCubeVieRCMTotalBillingPGP] AS

--****************CTE PARA SACAR LAS FACTURAS DEL MES DESEADO, DEBEN SER REGISTROS UNICOS DESDE CONTABILIDAD POR EL TIPO DE DOCUMENTO TIPO FACTURA******************************--

WITH 
CTE_COMPANY AS
(
SELECT
IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO036','116',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO039','50',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO040','29',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO041','68',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO043','27',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO045','11',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO046','11',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO047','11','')))))))) AS IdJournalVoucherInvoice,
--SELECT TOP 10 * FROM GeneralLedger.JournalVouchers WHERE EntityName='INVOICE'
IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO036','116',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO039','50',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO040','29',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO041','68',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO043','27',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO045','11',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO046','11',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO047','11','')))))))) AS IdJournalVoucherInvoiceEntityCapitated,
--SELECT TOP 10 * FROM GeneralLedger.JournalVouchers WHERE EntityName='InvoiceEntityCapitated'
IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO036','94',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO039','53',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO040','90',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO041','71',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO043','27',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO045','11',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO046','11',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO047','11','')))))))) AS IdJournalVoucherBasicBilling,
--SELECT TOP 10 * FROM GeneralLedger.JournalVouchers WHERE EntityName='BasicBilling'
IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO036','14',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO039','51',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO040','30',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO041','69',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO043','26',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO045','14',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO046','14',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO047','14','')))))))) AS IdJournalVoucherInvoiceAnulado,
--SELECT TOP 10 * FROM GeneralLedger.JournalVouchers WHERE EntityName='Invoice' AND IdJournalVoucher!=11
IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO036','',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO039','51',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO040','30',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO041','',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO043','',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO045','14',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO046','14',
	IIF(CAST(DB_NAME() AS VARCHAR(9))='INDIGO047','14','')))))))) AS IdJournalVoucherInvoiceEntityCapitatedAnulado
--SELECT TOP 10 * FROM GeneralLedger.JournalVouchers WHERE EntityName='InvoiceEntityCapitated' AND IdJournalVoucher!=11
),

---***************************************************************** SEGMENTO UNIDAD OPERATIVA ********************************************************************************---
CTE_UNIDAD_OPERATIVA
AS
(
SELECT 
U.Id,
UnitName AS [UNIDAD OPERATIVA],
C.Name AS [CIUDAD UNIDAD OPERATIVA]
FROM 
Common.OperatingUnit U INNER JOIN
Common.City C ON U.IdCity=C.Id
),


--*****************************************************************************************************************************************************************************--
--*************************************************************** SEGMENTO REGISTROS DE SERVICIOS GENERADAS *********************************************************************************--
--*****************************************************************************************************************************************************************************--


CTE_REGISTROS_SERVICIOS
AS
(
SELECT DISTINCT I.ID,I.AdmissionNumber,I.PatientCode,I.HealthAdministratorId,I.ThirdPartyId,I.CareGroupId,I.InvoiceNumber,I.InvoicedDate,
CAST(I.TotalInvoice AS NUMERIC) TotalInvoice,UN.[UNIDAD OPERATIVA],UN.[CIUDAD UNIDAD OPERATIVA]
FROM
Billing.Invoice I 
INNER JOIN CTE_UNIDAD_OPERATIVA UN ON I.OperatingUnitId=UN.Id
WHERE I.DocumentType = 5 AND I.Status =1 
--AND CAST(I.InvoicedDate AS DATE) BETWEEN CAST(common.getdate()-5 AS DATE) AND CAST(COMMON.GETDATE() AS DATE)
),

CTE_ALTA_MEDICA_REGISTROS_SERVICIOS AS
(
 SELECT
EGR.NUMINGRES 'INGRESO',
EGR.IPCODPACI 'IDENTIFICACION',
EGR.FECALTPAC 'FECHA ALTA MEDICA'
FROM
HCREGEGRE EGR 
INNER JOIN (SELECT EGR.NUMINGRES 'INGRESO' ,EGR.IPCODPACI 'IDENTIFICACION' ,MAX(EGR.FECALTPAC) 'FECHA ALTA MEDICA'
FROM DBO.HCREGEGRE EGR  
INNER JOIN CTE_REGISTROS_SERVICIOS AS ING ON EGR.NUMINGRES=ING.AdmissionNumber
GROUP BY EGR.NUMINGRES,EGR.IPCODPACI) AS G ON G.INGRESO=EGR.NUMINGRES AND G.[FECHA ALTA MEDICA]=EGR.FECALTPAC
),

--************************************************ CTE TRAE LOS DATOS DEL ALTA MEDICA TIPO SALIDA=12 ******************************************************************************--

CTE_SALIDA_REGISTRO_SERVICIOS
AS
(
	SELECT HIS.NUMINGRES 'INGRESO' ,HIS.IPCODPACI 'IDENTIFICACION',HIS.FECHISPAC 'FECHA SALIDA'  
	FROM HCHISPACA HIS 
	INNER JOIN CTE_REGISTROS_SERVICIOS AS ING  ON HIS.NUMINGRES=ING.AdmissionNumber
	INNER JOIN
	( SELECT HIS.NUMINGRES 'INGRESO' ,HIS.IPCODPACI 'IDENTIFICACION' ,MAX(HIS.ID) 'ID'  
	  FROM DBO.HCHISPACA HIS 
	  INNER JOIN CTE_REGISTROS_SERVICIOS AS ING  ON HIS.NUMINGRES=ING.AdmissionNumber  
	  WHERE HIS.INDICAPAC=12
	  GROUP BY HIS.NUMINGRES,HIS.IPCODPACI
	)  AS G ON G.INGRESO=HIS.NUMINGRES AND G.[ID]=HIS.ID
),

CTE_REGISTROS_TOTAL_UNICO_GLOBAL
AS
(
   SELECT DISTINCT 
   CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
   UNI.[UNIDAD OPERATIVA],
   UNI.[CIUDAD UNIDAD OPERATIVA],
   'OPERACIONAL' 'TIPO VENTA' ,'PGP' 'TIPO MODALIDAD',
   'FACTURADO' AS ESTADO, YEAR(UNI.InvoicedDate) AS AÑO, MONTH(UNI.InvoicedDate) AS MES,  DAY(UNI.InvoicedDate) AS DIA, TP.Nit AS NIT,
   TP.Name AS ENTIDAD, CG.Code AS [CODIGO GRUPO ATENCION], CG.Name AS [GRUPO ATENCION], CASE CG.EntityType WHEN 1 THEN 'EPS Contributivo' WHEN 2 THEN 'EPS Subsidiado'
   WHEN 3 THEN 'ET Vinculados Municipios' WHEN 4 THEN 'ET Vinculados Departamentos' WHEN 5 THEN 'ARL Riesgos Laborales' WHEN 6 THEN 'MP Medicina Prepagada'
   WHEN 7 THEN 'IPS Privada' WHEN 8 THEN 'IPS Publica' WHEN 9 THEN 'Regimen Especial' WHEN 10 THEN 'Accidentes de transito' WHEN 11 THEN 'Fosyga' WHEN 12 THEN 'Otros'
   WHEN 13 THEN 'Aseguradoras' WHEN 99 THEN 'Particulares' ELSE 'Otros' END 'REGIMEN',
   I.PatientCode AS IDENTIFICACION, RTRIM(PAC.IPNOMCOMP) AS PACIENTE , I.AdmissionNumber AS INGRESO, CAST(ING.IFECHAING AS DATE) AS [FECHA INGRESO],
   CASE WHEN ING.TIPOINGRE =1 THEN CAST(ING.IFECHAING AS DATE)  ELSE  CAST( ISNULL(ALT.[FECHA ALTA MEDICA],ISNULL(ALT2.[FECHA SALIDA],ING.FECHEGRESO)) AS DATE) END AS [FECHA EGRESO],
   I.InvoiceNumber AS [NRO FACTURA],CAST(I.InvoiceDate AS DATE) AS [FECHA FACTURA], CAST(I.InvoiceExpirationDate AS DATE) AS [FECHA VENCIMIENTO],
   CAST(I.TotalInvoice AS NUMERIC) AS [TOTAL FACTURA], CAST(I.ThirdPartySalesValue AS NUMERIC) AS [TOTAL VALOR ENTIDAD],CAST(I.TotalPatientSalesPrice AS NUMERIC) AS [TOTAL VALOR PACIENTE],
   CAST(I.ValueTax AS NUMERIC) AS [VALOR IVA],CAST(I.InvoiceValue AS NUMERIC) 'VALOR SIN IVA',
   CASE DocumentType WHEN 1 THEN 'Factura EAPB con Contrato' WHEN 2 THEN 'Factura EAPB Sin Contrato' WHEN 3 THEN 'Factura Particular' WHEN 4 THEN 'Factura Capitada'
   WHEN 5 THEN 'Control de Capitacion' WHEN 6 THEN 'Factura Basica' WHEN 7 THEN 'Factura de Venta de Productos' END 'TIPO FACTURA',ICT.Name 'CATERGORIA FACTURA',
   CASE ING.TIPOINGRE WHEN 1 THEN 'AMBULATORIO' WHEN 2 THEN 'HOSPITALARIO' END [TIPO INGRESO],FUI.NAME 'UNIDAD FUNCIONAL INGRESO',ISNULL(FUE.NAME,FUI.NAME) 'UNIDAD FUNCIONAL EGRESO',
   OutputDiagnosis 'CIE 10',DIA.NOMDIAGNO 'DIAGNOSTICO',SU.NOMUSUARI 'USUARIO FACTURO',NULL 'FECHA ANULACION',NULL 'USUARIO ANULO',
   NULL 'CAUSA ANULACION',CAST(PAC.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO', 'N/A' 'CODIGO COMPROBANTE','N/A' [TIPO COMPROBANTE],
   CASE MONTH(UNI.InvoicedDate) WHEN 1 THEN 'ENE' WHEN 2 THEN 'FEB' WHEN 3 THEN 'MAR' WHEN 4 THEN 'ABR' WHEN 5 THEN 'MAY' WHEN 6 THEN 'JUN' WHEN 7 THEN 'JUL' WHEN 8 THEN 'AGO'
   WHEN 9 THEN 'SEP' WHEN 10 THEN 'OCT' WHEN 11 THEN 'NOV' WHEN 12 THEN 'DIC' END 'MES AÑO',   
   (CASE DATENAME(dw,UNI.InvoicedDate) when 'Monday' then 'LUN' when 'Tuesday' then 'MAR' when 'Wednesday' then 'MIE' when 'Thursday' then 'JUE' when 'Friday' then 'VIE'
   when 'Saturday' then 'SAB' when 'Sunday' then 'DOM' END) 'DIA SEMANA', 1 'CANTIDAD',I.Observation AS 'OBSERVACION',
   I.ID InvoiceId , CAST(YEAR(UNI.InvoicedDate) AS CHAR(4)) + CAST(MONTH(UNI.InvoicedDate) AS CHAR(2))  + rtrim(CAST(DAY(UNI.InvoicedDate) AS CHAR(2))) + cast(I.Id as varchar) + cast('F' as char(1)) 'Idunique' ,
   CAST(I.InvoiceDate AS DATE) AS [FECHA BUSQUEDA],
   CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
   FROM Billing.Invoice AS I
   INNER JOIN CTE_REGISTROS_SERVICIOS AS UNI  ON UNI.ID =I.Id
   LEFT JOIN Billing.InvoiceDetail AS ID  ON ID.InvoiceId =I.Id
   LEFT JOIN Billing.ServiceOrderDetail AS SOD  ON SOD.Id =ID.ServiceOrderDetailId
   LEFT JOIN Billing .ServiceOrder AS SO WITH (NOLOCK) ON SO.Id =SOD.ServiceOrderId
   LEFT JOIN Payroll.FunctionalUnit AS FU  ON FU.Id = SOD.PerformsFunctionalUnitId
   LEFT JOIN Payroll.CostCenter AS COST  ON COST.Id =SOD.CostCenterId
   LEFT JOIN dbo.ADINGRESO AS ING  ON ING.NUMINGRES =I.AdmissionNumber 
   LEFT JOIN Common.ThirdParty AS TP ON TP.Id =I.ThirdPartyId 
   LEFT JOIN Contract.CareGroup AS CG ON CG.Id =I.CareGroupId 
   LEFT JOIN dbo.INPACIENT AS PAC ON PAC.IPCODPACI =I.PatientCode
   LEFT JOIN Contract.CUPSEntity AS CUPS ON CUPS.Id = SOD.CUPSEntityId
   LEFT JOIN Inventory.InventoryProduct AS IPR ON IPR.Id = SOD.ProductId
   LEFT JOIN Contract.CUPSEntityContractDescriptions AS DESCR  ON DESCR.Id  =SOD.CUPSEntityContractDescriptionId  AND SOD.CUPSEntityId=DESCR.CUPSEntityId 
   LEFT JOIN Contract.ContractDescriptions AS CD  ON CD.ID=DESCR.ContractDescriptionId
   LEFT JOIN Billing .ServiceOrderDetail AS SODP  ON SODP.Id =SOD.PackageServiceOrderDetailId 
   LEFT JOIN Contract.CUPSEntity AS CUPSP  ON CUPSP.Id = SODP.CUPSEntityId
   LEFT JOIN Billing .ServiceOrderDetail AS SODI  ON SODI.Id =SOD.IncludeServiceOrderDetailId 
   LEFT JOIN Contract.CUPSEntity AS CUPSI  ON CUPSI.Id = SODI.CUPSEntityId
   LEFT JOIN dbo.INPROFSAL AS MED  ON MED.CODPROSAL = SOD.PerformsHealthProfessionalCode
   LEFT JOIN dbo.INESPECIA AS ESPMED  ON ESPMED.CODESPECI = SOD.PerformsProfessionalSpecialty 
   LEFT JOIN Common.ThirdParty AS TPT  ON TPT.Id =SOD.PerformsHealthProfessionalThirdPartyId 
   LEFT JOIN Billing.InvoiceDetailSurgical AS IDS  ON IDS.InvoiceDetailId =ID.Id AND IDS.OnlyMedicalFees = '0'
   LEFT JOIN Contract.IPSService AS IPS  ON IPS.Id =IDS.IPSServiceId 
   LEFT JOIN dbo.INPROFSAL AS MEDQX  ON MEDQX.CODPROSAL = IDS.PerformsHealthProfessionalCode
   LEFT JOIN dbo.INESPECIA AS ESPQX  ON ESPQX.CODESPECI = MEDQX.CODESPEC1
   LEFT JOIN GeneralLedger.MainAccounts AS MA  ON MA.Id =ISNULL(IDS.IncomeMainAccountId,SOD.IncomeMainAccountId)
   LEFT JOIN Payroll.FunctionalUnit AS FUI  ON FUI.CODE = ING.UFUCODIGO
   LEFT JOIN Payroll.FunctionalUnit AS FUE  ON FUE.CODE = ING.UFUEGRMED
   LEFT JOIN DBO.INDIAGNOS AS DIA ON DIA.CODDIAGNO= I.OutputDiagnosis
   LEFT JOIN DBO.SEGusuaru SU WITH (NOLOCK) ON SU.CODUSUARI = i.InvoicedUser
   LEFT JOIN DBO.SEGusuaru SUA WITH (NOLOCK) ON SUA.CODUSUARI = i.InvoicedUser
   LEFT JOIN Billing.BillingReversalReason AS BRR ON BRR.Id=I.ReversalReasonId
   LEFT JOIN CTE_ALTA_MEDICA_REGISTROS_SERVICIOS AS ALT  ON ALT.INGRESO =I.AdmissionNumber
   LEFT JOIN CTE_SALIDA_REGISTRO_SERVICIOS AS ALT2  ON ALT2.INGRESO =I.AdmissionNumber
   LEFT JOIN Billing.InvoiceCategories ICT  ON ICT.Id = I.InvoiceCategoryId 
),

------***********************************************REGISTROS DE SERVICIOS ANULADOS *************************************************************************************---

CTE_REGISTROS_SERVICIOS_ANULADOS
AS
(
SELECT DISTINCT I.ID,I.AdmissionNumber,I.PatientCode,I.HealthAdministratorId,I.ThirdPartyId,I.CareGroupId,I.InvoiceNumber,I.InvoicedDate,
CAST(I.TotalInvoice AS NUMERIC) TotalInvoice,UN.[UNIDAD OPERATIVA],UN.[CIUDAD UNIDAD OPERATIVA]
FROM
Billing.Invoice I 
INNER JOIN CTE_UNIDAD_OPERATIVA UN ON I.OperatingUnitId=UN.Id
WHERE I.DocumentType = 5 AND I.Status =2 
--AND CAST(I.InvoicedDate AS DATE) BETWEEN CAST(common.getdate()-5 AS DATE) AND CAST(COMMON.GETDATE() AS DATE)
),

CTE_ALTA_MEDICA_REGISTROS_SERVICIOS_ANULADOS AS
(
 SELECT
EGR.NUMINGRES 'INGRESO',
EGR.IPCODPACI 'IDENTIFICACION',
EGR.FECALTPAC 'FECHA ALTA MEDICA'
FROM
HCREGEGRE EGR 
INNER JOIN (SELECT EGR.NUMINGRES 'INGRESO' ,EGR.IPCODPACI 'IDENTIFICACION' ,MAX(EGR.FECALTPAC) 'FECHA ALTA MEDICA'
FROM DBO.HCREGEGRE EGR  
INNER JOIN CTE_REGISTROS_SERVICIOS_ANULADOS AS ING ON EGR.NUMINGRES=ING.AdmissionNumber
GROUP BY EGR.NUMINGRES,EGR.IPCODPACI) AS G ON G.INGRESO=EGR.NUMINGRES AND G.[FECHA ALTA MEDICA]=EGR.FECALTPAC
),

--************************************************ CTE TRAE LOS DATOS DEL ALTA MEDICA TIPO SALIDA=12 ******************************************************************************--

CTE_SALIDA_REGISTRO_SERVICIOS_ANULADOS
AS
(
	SELECT HIS.NUMINGRES 'INGRESO' ,HIS.IPCODPACI 'IDENTIFICACION',HIS.FECHISPAC 'FECHA SALIDA'  
	FROM HCHISPACA HIS 
	INNER JOIN CTE_REGISTROS_SERVICIOS_ANULADOS AS ING  ON HIS.NUMINGRES=ING.AdmissionNumber
	INNER JOIN
	( SELECT HIS.NUMINGRES 'INGRESO' ,HIS.IPCODPACI 'IDENTIFICACION' ,MAX(HIS.ID) 'ID'  
	  FROM DBO.HCHISPACA HIS 
	  INNER JOIN CTE_REGISTROS_SERVICIOS_ANULADOS AS ING  ON HIS.NUMINGRES=ING.AdmissionNumber  
	  WHERE HIS.INDICAPAC=12
	  GROUP BY HIS.NUMINGRES,HIS.IPCODPACI
	)  AS G ON G.INGRESO=HIS.NUMINGRES AND G.[ID]=HIS.ID
),

CTE_REGISTROS_TOTAL_UNICO_GLOBAL_ANULADOS
AS
(
   SELECT DISTINCT 
   CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
   UNI.[UNIDAD OPERATIVA],
   UNI.[CIUDAD UNIDAD OPERATIVA],
   'OPERACIONAL' 'TIPO VENTA' ,'PGP' 'TIPO MODALIDAD',
   'ANULADOS' AS ESTADO, YEAR(UNI.InvoicedDate) AS AÑO, MONTH(UNI.InvoicedDate) AS MES,  DAY(UNI.InvoicedDate) AS DIA, TP.Nit AS NIT,
   TP.Name AS ENTIDAD, CG.Code AS [CODIGO GRUPO ATENCION], CG.Name AS [GRUPO ATENCION], CASE CG.EntityType WHEN 1 THEN 'EPS Contributivo' WHEN 2 THEN 'EPS Subsidiado'
   WHEN 3 THEN 'ET Vinculados Municipios' WHEN 4 THEN 'ET Vinculados Departamentos' WHEN 5 THEN 'ARL Riesgos Laborales' WHEN 6 THEN 'MP Medicina Prepagada'
   WHEN 7 THEN 'IPS Privada' WHEN 8 THEN 'IPS Publica' WHEN 9 THEN 'Regimen Especial' WHEN 10 THEN 'Accidentes de transito' WHEN 11 THEN 'Fosyga' WHEN 12 THEN 'Otros'
   WHEN 13 THEN 'Aseguradoras' WHEN 99 THEN 'Particulares' ELSE 'Otros' END 'REGIMEN',
   I.PatientCode AS IDENTIFICACION, RTRIM(PAC.IPNOMCOMP) AS PACIENTE , I.AdmissionNumber AS INGRESO, CAST(ING.IFECHAING AS DATE) AS [FECHA INGRESO],
   CASE WHEN ING.TIPOINGRE =1 THEN CAST(ING.IFECHAING AS DATE)  ELSE  CAST( ISNULL(ALT.[FECHA ALTA MEDICA],ISNULL(ALT2.[FECHA SALIDA],ING.FECHEGRESO)) AS DATE) END AS [FECHA EGRESO],
   I.InvoiceNumber AS [NRO FACTURA],CAST(I.InvoiceDate AS DATE) AS [FECHA FACTURA], CAST(I.InvoiceExpirationDate AS DATE) AS [FECHA VENCIMIENTO],
   CAST(I.TotalInvoice AS NUMERIC) AS [TOTAL FACTURA], CAST(I.ThirdPartySalesValue AS NUMERIC) AS [TOTAL VALOR ENTIDAD],CAST(I.TotalPatientSalesPrice AS NUMERIC) AS [TOTAL VALOR PACIENTE],
   CAST(I.ValueTax AS NUMERIC) AS [VALOR IVA],CAST(I.InvoiceValue AS NUMERIC) 'VALOR SIN IVA',
   CASE DocumentType WHEN 1 THEN 'Factura EAPB con Contrato' WHEN 2 THEN 'Factura EAPB Sin Contrato' WHEN 3 THEN 'Factura Particular' WHEN 4 THEN 'Factura Capitada'
   WHEN 5 THEN 'Control de Capitacion' WHEN 6 THEN 'Factura Basica' WHEN 7 THEN 'Factura de Venta de Productos' END 'TIPO FACTURA',ICT.Name 'CATERGORIA FACTURA',
   CASE ING.TIPOINGRE WHEN 1 THEN 'AMBULATORIO' WHEN 2 THEN 'HOSPITALARIO' END [TIPO INGRESO],FUI.NAME 'UNIDAD FUNCIONAL INGRESO',ISNULL(FUE.NAME,FUI.NAME) 'UNIDAD FUNCIONAL EGRESO',
   OutputDiagnosis 'CIE 10',DIA.NOMDIAGNO 'DIAGNOSTICO',SU.NOMUSUARI 'USUARIO FACTURO',NULL 'FECHA ANULACION',NULL 'USUARIO ANULO',
   NULL 'CAUSA ANULACION',CAST(PAC.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO', 'N/A' 'CODIGO COMPROBANTE','N/A' [TIPO COMPROBANTE],
   CASE MONTH(UNI.InvoicedDate) WHEN 1 THEN 'ENE' WHEN 2 THEN 'FEB' WHEN 3 THEN 'MAR' WHEN 4 THEN 'ABR' WHEN 5 THEN 'MAY' WHEN 6 THEN 'JUN' WHEN 7 THEN 'JUL' WHEN 8 THEN 'AGO'
   WHEN 9 THEN 'SEP' WHEN 10 THEN 'OCT' WHEN 11 THEN 'NOV' WHEN 12 THEN 'DIC' END 'MES AÑO',   
   (CASE DATENAME(dw,UNI.InvoicedDate) when 'Monday' then 'LUN' when 'Tuesday' then 'MAR' when 'Wednesday' then 'MIE' when 'Thursday' then 'JUE' when 'Friday' then 'VIE'
   when 'Saturday' then 'SAB' when 'Sunday' then 'DOM' END) 'DIA SEMANA', 1 'CANTIDAD',I.Observation AS 'OBSERVACION',
   I.ID InvoiceId , CAST(YEAR(UNI.InvoicedDate) AS CHAR(4)) + CAST(MONTH(UNI.InvoicedDate) AS CHAR(2))  + rtrim(CAST(DAY(UNI.InvoicedDate) AS CHAR(2))) + cast(I.Id as varchar) + cast('F' as char(1)) 'Idunique' ,
   CAST(I.InvoiceDate AS DATE) AS [FECHA BUSQUEDA],
   CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
   FROM Billing.Invoice AS I
   INNER JOIN CTE_REGISTROS_SERVICIOS_ANULADOS AS UNI  ON UNI.ID =I.Id
   LEFT JOIN Billing.InvoiceDetail AS ID  ON ID.InvoiceId =I.Id
   LEFT JOIN Billing.ServiceOrderDetail AS SOD  ON SOD.Id =ID.ServiceOrderDetailId
   LEFT JOIN Billing .ServiceOrder AS SO WITH (NOLOCK) ON SO.Id =SOD.ServiceOrderId
   LEFT JOIN Payroll.FunctionalUnit AS FU  ON FU.Id = SOD.PerformsFunctionalUnitId
   LEFT JOIN Payroll.CostCenter AS COST  ON COST.Id =SOD.CostCenterId
   LEFT JOIN dbo.ADINGRESO AS ING  ON ING.NUMINGRES =I.AdmissionNumber 
   LEFT JOIN Common.ThirdParty AS TP ON TP.Id =I.ThirdPartyId 
   LEFT JOIN Contract.CareGroup AS CG ON CG.Id =I.CareGroupId 
   LEFT JOIN dbo.INPACIENT AS PAC ON PAC.IPCODPACI =I.PatientCode
   LEFT JOIN Contract.CUPSEntity AS CUPS ON CUPS.Id = SOD.CUPSEntityId
   LEFT JOIN Inventory.InventoryProduct AS IPR ON IPR.Id = SOD.ProductId
   LEFT JOIN Contract.CUPSEntityContractDescriptions AS DESCR  ON DESCR.Id  =SOD.CUPSEntityContractDescriptionId  AND SOD.CUPSEntityId=DESCR.CUPSEntityId 
   LEFT JOIN Contract.ContractDescriptions AS CD  ON CD.ID=DESCR.ContractDescriptionId
   LEFT JOIN Billing .ServiceOrderDetail AS SODP  ON SODP.Id =SOD.PackageServiceOrderDetailId 
   LEFT JOIN Contract.CUPSEntity AS CUPSP  ON CUPSP.Id = SODP.CUPSEntityId
   LEFT JOIN Billing .ServiceOrderDetail AS SODI  ON SODI.Id =SOD.IncludeServiceOrderDetailId 
   LEFT JOIN Contract.CUPSEntity AS CUPSI  ON CUPSI.Id = SODI.CUPSEntityId
   LEFT JOIN dbo.INPROFSAL AS MED  ON MED.CODPROSAL = SOD.PerformsHealthProfessionalCode
   LEFT JOIN dbo.INESPECIA AS ESPMED  ON ESPMED.CODESPECI = SOD.PerformsProfessionalSpecialty 
   LEFT JOIN Common.ThirdParty AS TPT  ON TPT.Id =SOD.PerformsHealthProfessionalThirdPartyId 
   LEFT JOIN Billing.InvoiceDetailSurgical AS IDS  ON IDS.InvoiceDetailId =ID.Id AND IDS.OnlyMedicalFees = '0'
   LEFT JOIN Contract.IPSService AS IPS  ON IPS.Id =IDS.IPSServiceId 
   LEFT JOIN dbo.INPROFSAL AS MEDQX  ON MEDQX.CODPROSAL = IDS.PerformsHealthProfessionalCode
   LEFT JOIN dbo.INESPECIA AS ESPQX  ON ESPQX.CODESPECI = MEDQX.CODESPEC1
   LEFT JOIN GeneralLedger.MainAccounts AS MA  ON MA.Id =ISNULL(IDS.IncomeMainAccountId,SOD.IncomeMainAccountId)
   LEFT JOIN Payroll.FunctionalUnit AS FUI  ON FUI.CODE = ING.UFUCODIGO
   LEFT JOIN Payroll.FunctionalUnit AS FUE  ON FUE.CODE = ING.UFUEGRMED
   LEFT JOIN DBO.INDIAGNOS AS DIA ON DIA.CODDIAGNO= I.OutputDiagnosis
   LEFT JOIN DBO.SEGusuaru SU WITH (NOLOCK) ON SU.CODUSUARI = i.InvoicedUser
   LEFT JOIN DBO.SEGusuaru SUA WITH (NOLOCK) ON SUA.CODUSUARI = i.InvoicedUser
   LEFT JOIN Billing.BillingReversalReason AS BRR ON BRR.Id=I.ReversalReasonId
   LEFT JOIN CTE_ALTA_MEDICA_REGISTROS_SERVICIOS_ANULADOS AS ALT  ON ALT.INGRESO =I.AdmissionNumber
   LEFT JOIN CTE_SALIDA_REGISTRO_SERVICIOS_ANULADOS AS ALT2  ON ALT2.INGRESO =I.AdmissionNumber
   LEFT JOIN Billing.InvoiceCategories ICT  ON ICT.Id = I.InvoiceCategoryId 


)

SELECT * FROM CTE_REGISTROS_TOTAL_UNICO_GLOBAL
UNION ALL
SELECT * FROM CTE_REGISTROS_TOTAL_UNICO_GLOBAL_ANULADOS