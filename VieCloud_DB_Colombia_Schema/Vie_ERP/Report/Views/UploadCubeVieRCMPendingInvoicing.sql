
 /*******************************************************************************************************************
Nombre: [Report].[UploadCubeVieRCMPendingInvoicing]
Tipo:View
Observacion:Vista de facturación pendiente
Profesional:Andres Cabrera
Fecha:20-05-2024
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 1
Persona que modifico:Nilsson Miguel Galindo
Fecha:24-05-2024
Observaciones:Solo se ordena el codigo fuente
--------------------------------------
Version 2
Persona que modifico:
Fecha:
Observaciones:
***********************************************************************************************************************************/

CREATE view [Report].[UploadCubeVieRCMPendingInvoicing] AS

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
RTRIM(CEN.NOMCENATE) AS [CENTRO DE ATENCION],
'REGISTROS EVENTO' AS [TIPO VENTA],
CASE WHEN ING.IESTADOIN='' THEN 'ABIERTO' 
	 WHEN ING.IESTADOIN='P' THEN 'PARCIAL' END AS [ESTADO INGRESO],
CASE ING.TIPOINGRE WHEN 1 THEN 'AMBULATORIO' 
				   WHEN 2 THEN 'HOSPITALARIO' END [TIPO INGRESO], 
TP.Nit AS NIT,  
TP.Name AS ENTIDAD,  
CG.Code AS [CODIGO GRUPO ATENCION],  
CG.Name AS [GRUPO ATENCION], 
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
				   WHEN 11 THEN 'Fosyga' WHEN 12 THEN 'Otros'
				   WHEN 13 THEN 'Aseguradoras' 
				   WHEN 99 THEN 'Particulares' ELSE 'Otros' END AS REGIMEN,
PAC.IPCODPACI AS IDENTIFICACION,
RTRIM(PAC.IPNOMCOMP) AS PACIENTE,  
ING.NUMINGRES AS INGRESO,  
CAST(ING.IFECHAING AS DATE) AS [FECHA INGRESO],
CASE WHEN ING.TIPOINGRE =1 THEN CAST(ING.IFECHAING AS DATE) ELSE  CAST(ISNULL(G.[FECHA ALTA MEDICA],ING.FECHEGRESO) AS DATE) END AS [FECHA EGRESO],
NULL AS [NRO FACTURA], 
NULL AS [FECHA FACTURA], 
0 AS [TOTAL FACTURA],
CASE SOD.RecordType WHEN 1 THEN 'SERVICIOS' WHEN 2 THEN 'MEDICAMENTOS/INSUMOS' END AS [TIPO REGISTRO],
CASE CUPS.ServiceType WHEN 1 then 'Laboratorios'
 					  WHEN 2 then 'Patologias'
 					  WHEN 3 then 'Imagenes Diagnosticas'
 					  WHEN 4 then 'Procedimeintos no Qx'
 					  WHEN 5 then 'Procedimientos Qx'
 					  WHEN 6 then 'Interconsultas'
 					  WHEN 7 then 'Ninguno'
 					  WHEN 8 then 'Consulta Externa' ELSE 'Otro' END AS [TIPO SERVICIO],
CASE SOD.Presentation WHEN 1 THEN 'NO QUIRURGICO' 
 					  WHEN 2 THEN 'QUIRURGICO' 
 					  WHEN 3 THEN 'PAQUETE' ELSE 'NO QUIRURGICO' END AS PRESENTACION, 
ISNULL(BG.CODE  + ' - ' + BG.NAME,PG.CODE  + ' - ' + PG.NAME) AS [GRUPO FACTURACION],
CAST(SOD.ServiceDate AS DATE) AS [FECHA SERVICIO], 
ISNULL(CUPS.Code,IPR.Code) AS [CUPS/PRODUCTO],
ISNULL(CUPS.Description,IPR.Description ) AS DESCRIPCION, 
ISNULL(IPSC.Code, 'N/A') AS [CODIGO SERVICIO IPS],
SUBSTRING(ISNULL(RTRIM(IPSC.Name), 'N/A'),1,60) AS [DESCRIPCION SERVICIO IPS],
ISNULL(IPS.Code, 'N/A') AS [CODIGO DETALLE QX], 
SUBSTRING(ISNULL(RTRIM(IPS.Name), 'N/A'),1,60) AS [DESCRIPCION DETALLE QX], 
CASE IPS.ServiceClass WHEN 1 THEN 'Ninguno' 
					  WHEN 2 THEN 'Cirujano' 
					  WHEN 3 THEN 'Anestesiologo' 
					  WHEN 4 THEN 'Ayudante' 
					  WHEN 5 THEN 'Derecho Sala' 
					  WHEN 6 THEN 'Materiales Sutura'
					  WHEN 7 THEN 'Instrumentacion Quirurgica' ELSE 'Ninguno' END AS [TIPO SERVICIO IPS],
CD.Name AS [DECRIPCION RELACIONADA],
ISNULL(SODS.InvoicedQuantity,SOD.InvoicedQuantity) AS CANTIDAD,
ISNULL(SODS.RateManualSalePrice,SOD.RateManualSalePrice) AS [VALOR SERVICIO],
CAST(ISNULL(SOD.CostValue,0) AS NUMERIC) AS [COSTO SERVICIO],
ISNULL(SODS.RateManualSalePrice,SOD.TotalSalesPrice) AS [VALOR UNITARIO],
ISNULL(SODS.TotalSalesPrice,SOD.GrandTotalSalesPrice) AS [VALOR DETALLE TOTAL],
ISNULL(SODS.TotalSalesPrice,SOD.GrandTotalSalesPrice) AS [VALOR TOTAL POR FACTURA],
RCD.TOTALPATIENTWITHDISCOUNT AS [VALOR COBRADO PACIENTE],
RCD.VALUECOPAY AS [VALOR CUOTA RECUPERACION FOLIO],
RCD.VALUEFEEMODERATOR AS [VALOR CUOTA MODERADORA FOLIO],
RCD.OBSERVATION AS [OBSERVACIONES],
CAT.NAME AS [CATEGORIA FACTURA],
CASE RCD.STATUS WHEN '1' THEN 'REGISTRADO'	
				WHEN '2' THEN 'FACTURADO'
				WHEN '3' THEN 'BLOQUEADO' END AS [ESTADO],
CASE SOD.IsPackage WHEN 0 THEN 'NO' 
				   WHEN 1 THEN 'SI' END AS [ES PAQUETE],
CASE SOD.Packaging WHEN 0 THEN 'NO' WHEN 1 THEN 'SI' END AS [INCLUIDO EN PAQUETE],
CUPSP.Code 'CODIGO PAQUETE',
CUPSP.Description 'NOMBRE PAQUETE',
CASE SOD.SettlementType WHEN 3 THEN 'SI (No se cobra nada)' ELSE 'NO (Se cobra)' END AS [TIPO LIQUIDACION], 
CUPSI.Code 'CUPS QUE INCLUYE',
CUPSI.Description 'NOMBRE CUPS QUE INCLUYE', 
ISNULL(SODS.PerformsHealthProfessionalCode,ISNULL(MED.CODPROSAL,TPT.Nit)) AS [IDENTIFICACION PROFESIONAL],
RTRIM(ISNULL(MEDQX.NOMMEDICO,ISNULL(MED.NOMMEDICO,TPT.Name))) AS PROFESIONAL,
ISNULL(ESPQX.DESESPECI,ESPMED.DESESPECI) AS ESPECIALIDAD,
PAC.IPFECNACI AS [FECHA NACIMIENTO],
DATEDIFF(YEAR, PAC.IPFECNACI, ING.IFECHAING) AS [EDAD CONSULTA],
CASE RCD.RESPONSIBLERECOVERYFEE WHEN '1' THEN 'NINGUNO'
								WHEN '2' THEN 'PACIENTE'
								WHEN '3' THEN 'TERCERO' END AS [RESPONSABLE CUOTA RECUPERACION],
CASE PAC.TIPCOBSAL WHEN '1' THEN 'CONTRIBUTIVO'
				   WHEN '2' THEN 'SUBSIDIADO TOTAL'
				   WHEN '3' THEN 'SUBSIDIADO PARCIAL'
				   WHEN '4' THEN 'POBLACION POBRE SIN ASEGURAR CON SISBEN'
				   WHEN '5' THEN 'POBLACION POBRE SIN ASEGURAR SIN SISBEN'
				   WHEN '6' THEN 'DESPLAZADOS'
				   WHEN '7' THEN 'PLAN DE SALUD ADICIONAL'
				   WHEN '8' THEN 'OTROS' ELSE 'DESCONOCIDO' END AS [TIPO PACIENTE],
SU.NOMUSUARI AS [USUARIO CREO],
RCD.CREATIONDATE AS [FECHA CREACION],
SUSO.NOMUSUARI AS [USUARIO MODIFICO],
RCD.MODIFICATIONDATE AS [FECHA MODIFICO],
CASE SOD.SERVICETYPE WHEN '1' THEN 'SOAT'	
					 WHEN '2' THEN 'ISS'	
					 WHEN '3' THEN 'CUPS' ELSE 'PRODUCTO'END AS [CLASE SERVICIO],
ISNULL(CGF.Code + '-' + CGF.Name, PG.Code + '-' + PG.NAME) AS [GRUPO],
ISNULL(CSG.CODE + '-' + CSG.Name, PSG.Code + '-' + PSG.NAME) AS [SUBGRUPO],
SOD.AUTHORIZATIONNUMBER AS [AUTORIZACION],
FU.Code AS [CODIGO UNIDAD SOLICITO] ,
FU.Name AS 'UNIDAD SOLICITO', 
ISNULL(COSTQ.CODE,COST.CODE) AS [CODIGO CC CONTABILIZO],
ISNULL(COSTQ.Name,COST.Name) AS [CENTRO COSTO CONTABILIZO], 
MA.Number AS [NRO CUENTA CONTABILIZA],  
MA.Name AS [CUENTA CONTABILIZA],
IIF (ING.TIPOINGRE = 1 AND CUPS.ServiceType IN (1, 2, 3, 8) AND G.[FECHA ALTA MEDICA] IS NULL,CASE WHEN ING.IFECHAING IS NULL THEN 'SIN ALTA MEDICA' ELSE 'CON ALTA MEDICA' END
																							 ,CASE WHEN G.[FECHA ALTA MEDICA] IS NULL THEN 'SIN ALTA MEDICA' ELSE 'CON ALTA MEDICA' END) AS [INGRESO ALTA MEDICA],
UFI.UFUDESCRI AS [UNIDAD FUNCIONAL INGRESO],
IIF (ING.TIPOINGRE = 1,ISNULL(UFE.UFUDESCRI,UFI.UFUDESCRI),UFE.UFUDESCRI) AS [UNIDAD FUNCIONAL EGRESO],
CCSF.Name AS [ESTADO FOLIO],
ISNULL(ING.CODDIAING,ING.CODDIAEGR) AS [CIE 10],
DIA.NOMDIAGNO AS DIAGNOSTICO,
ISNULL(SUSOM.NOMUSUARI,'INDIGOBOT') AS [USUARIO CREO ORDEN],
CAST(SO.CreationDate AS DATE) AS [FECHA ORDEN],
YEAR(ING.IFECHAING) AS [AÑO INGRESO], 
MONTH(ING.IFECHAING) AS [MES INGRESO],
OU.UnitName [CIUDAD ORDENAMIENTO],
CAST(ING.IFECHAING AS DATE) AS [FECHA BUSQUEDA],
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM Contract.CareGroup CG
INNER JOIN Billing.RevenueControlDetail RCD ON CG.Id = RCD.CareGroupId
INNER JOIN Billing.RevenueControl RC ON RC.Id = RCD.RevenueControlId
INNER JOIN Billing.ServiceOrderDetailDistribution SODD ON RCD.Id=SODD.RevenueControlDetailId
INNER JOIN Billing.ServiceOrderDetail SOD ON SODD.ServiceOrderDetailId = SOD.Id 
INNER JOIN Payroll.FunctionalUnit fu ON SOD.PerformsFunctionalUnitId=fu.Id
INNER JOIN Common.ThirdParty AS TP ON TP.Id =RCD.ThirdPartyId
INNER JOIN DBO.INPACIENT AS PAC ON RC.PatientCode=PAC.IPCODPACI
INNER JOIN DBO.ADINGRESO AS ING ON RC.AdmissionNumber=ING.NUMINGRES
INNER JOIN DBO.INUNIFUNC AS UFI WITH (NOLOCK) ON ING.UFUCODIGO = UFI.UFUCODIGO
INNER JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE = ING.CODCENATE
INNER JOIN Payroll.CostCenter AS COST ON SOD.CostCenterId=COST.Id
LEFT JOIN Billing.ServiceOrder AS SO ON SOD.ServiceOrderId=SO.Id AND RC.AdmissionNumber=SO.AdmissionNumber
LEFT JOIN Contract.CUPSEntity CUPS on SOD.CUPSEntityId = CUPS.Id
LEFT JOIN Billing.BillingConcept bc on CUPS.BillingConceptId = bc.Id
LEFT JOIN Inventory.InventoryProduct ipr on sod.ProductId = ipr.Id
LEFT JOIN Inventory.ProductGroup pg ON ipr.ProductGroupId = pg.Id
LEFT JOIN Contract.CUPSEntityContractDescriptions AS DESCR ON DESCR.Id =SOD.CUPSEntityContractDescriptionId AND SOD.CUPSEntityId=DESCR.CUPSEntityId
LEFT JOIN Contract.ContractDescriptions AS CD ON CD.ID=DESCR.ContractDescriptionId
LEFT JOIN Billing.ServiceOrderDetailSurgical AS SODS  ON SODS.ServiceOrderDetailId =SOD.Id AND SODS.OnlyMedicalFees = '0' AND SODS.SurchargeApply <>1
LEFT JOIN Payroll.CostCenter AS COSTQ ON COSTQ.Id =SODS.CostCenterId
LEFT JOIN Contract.IPSService AS IPS ON IPS.Id =SODS.IPSServiceId
LEFT JOIN Billing.ServiceOrderDetail AS SODP  ON SODP.Id =SOD.PackageServiceOrderDetailId
LEFT JOIN Contract.CUPSEntity AS CUPSP ON CUPSP.Id = SODP.CUPSEntityId
LEFT JOIN Billing.ServiceOrderDetail AS SODI  ON SODI.Id =SOD.IncludeServiceOrderDetailId
LEFT JOIN Contract.CUPSEntity AS CUPSI ON CUPSI.Id = SODI.CUPSEntityId
LEFT JOIN dbo.INPROFSAL AS MED ON MED.CODPROSAL = SOD.PerformsHealthProfessionalCode
LEFT JOIN dbo.INESPECIA AS ESPMED ON ESPMED.CODESPECI = SOD.PerformsProfessionalSpecialty
LEFT JOIN Common.ThirdParty AS TPT ON TPT.Id =SOD.PerformsHealthProfessionalThirdPartyId
LEFT JOIN dbo.INPROFSAL AS MEDQX ON MEDQX.CODPROSAL = SODS.PerformsHealthProfessionalCode
LEFT JOIN dbo.INESPECIA AS ESPQX ON ESPQX.CODESPECI = MEDQX.CODESPEC1
LEFT JOIN GeneralLedger.MainAccounts AS MA ON MA.Id =ISNULL(SODS.IncomeMainAccountId,SOD.IncomeMainAccountId)
LEFT JOIN MedicalFees.HealthProfessionalContract HPC ON HPC.HealthProfessionalCode = ISNULL(MED.CODPROSAL,ISNULL(TPT.Nit,SOD.PerformsHealthProfessionalCode)) AND HPC.LiquidateDefault = 1
LEFT JOIN MedicalFees.MedicalFeesContract MFC ON MFC.Id = HPC.MedicalFeesContractId
LEFT JOIN DBO.SEGusuaru SU  ON SU.CODUSUARI = RCD.CREATIONUSER
LEFT JOIN DBO.SEGusuaru SUSO ON SUSO.CODUSUARI = RCD.MODIFICATIONUSER
LEFT JOIN DBO.SEGusuaru SUSOM  ON SUSOM.CODUSUARI = SO.CreationUser
LEFT JOIN Billing.BillingGroup as BG ON BG.Id=CUPS.BillingGroupId
LEFT JOIN Contract.IPSService AS IPSC ON IPSC.Id =SOD.IPSServiceId
LEFT JOIN Contract.DefinitionRateDetail DRD ON DRD.ID=SOD.DefinitionRateDetailId
LEFT JOIN DBO.INDIAGNOS AS DIA ON DIA.CODDIAGNO= ISNULL(ING.CODDIAING,ING.CODDIAEGR)
LEFT JOIN
 (
  SELECT EGR.NUMINGRES 'INGRESO',EGR.IPCODPACI 'IDENTIFICACION',EGR.FECALTPAC 'FECHA ALTA MEDICA'
    FROM
    HCREGEGRE EGR 
    INNER JOIN (SELECT EGR.NUMINGRES 'INGRESO' ,EGR.IPCODPACI 'IDENTIFICACION' ,MAX(EGR.FECALTPAC) 'FECHA ALTA MEDICA'
    FROM DBO.HCREGEGRE EGR 
    GROUP BY EGR.NUMINGRES,EGR.IPCODPACI) AS G ON G.INGRESO=EGR.NUMINGRES AND G.[FECHA ALTA MEDICA]=EGR.FECALTPAC
 ) as G ON G.INGRESO =RC.AdmissionNumber
 LEFT JOIN BILLING.INVOICECATEGORIES AS CAT WITH (NOLOCK) ON CAT.ID = RCD.INVOICECATEGORYID
 LEFT JOIN Contract.CupsSubgroup AS CSG WITH (NOLOCK) ON CUPS.CUPSSubGroupId=CSG.ID
 LEFT JOIN Contract.CupsGroup AS CGF WITH (NOLOCK) ON CGF.ID=CSG.CupsGroupId
 LEFT JOIN Inventory.ProductSubGroup AS PSG WITH (NOLOCK) ON PSG.ID =ipr.ProductSubGroupId
 LEFT JOIN Billing .ConceptsCausesStatusFolio AS CCSF WITH (NOLOCK) ON CCSF.Id =RCD.StatusFolioId
 LEFT JOIN DBO.INUNIFUNC AS UFE WITH (NOLOCK) ON ING.UFUEGRMED = UFE.UFUCODIGO
 LEFT JOIN Common.OperatingUnit AS OU ON OU.Id =SO.OperatingUnitId
WHERE RCD.Status in (1,3) AND sod.IsDelete = 0 AND sod.SettlementType != 3 AND sod.GrandTotalSalesPrice > 0 AND CG.LiquidationType In (1,3) AND ING.IESTADOIN IN (' ','P')
--and RC.AdmissionNumber= '815025'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada al cubo de datos RCM (Revenue Cycle Management) que expone los servicios y medicamentos/insumos pendientes de facturación. Consolida información de admisiones (ambulatorias y hospitalarias), folios en estado registrado o bloqueado (status 1 y 3), entidades pagadoras, grupos de atención, procedimientos CUPS, productos de inventario, profesionales, costos, cuentas contables y unidades funcionales, excluyendo ítems anulados, con liquidación tipo 3 o sin valor de venta. Sirve como fuente para análisis de cartera pendiente por facturar en herramientas de BI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicing';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida la facturación pendiente (registros de evento aún no facturados) por ingreso, paciente, entidad, servicios y profesionales, para alimentar un cubo de Reporting/RCM.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El folio (RevenueControlDetail) debe estar en estado 1 (REGISTRADO) o 3 (BLOQUEADO); excluye estado 2 (FACTURADO); El detalle de la orden de servicio no debe estar borrado lógicamente (sod.IsDelete = 0); El tipo de liquidación del detalle no puede ser 3 (''SI - No se cobra nada''); El valor total de venta del detalle debe ser mayor a 0 (sod.GrandTotalSalesPrice > 0); El grupo de atención (CareGroup) debe tener LiquidationType en (1,3); El ingreso debe estar en estado abierto ('' '') o parcial (''P''); excluye ingresos cerrados/facturados', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye explícitamente folios ya facturados (Status=2) y servicios marcados como no cobrables (SettlementType=3); Solo considera ítems con valor de venta positivo (GrandTotalSalesPrice > 0); Solo procesa ingresos vigentes (abiertos o parciales); Identifica el RÉGIMEN del grupo de atención mediante el catálogo EntityType (1..13, 99) con default ''Otros''; Cuando no existe usuario creador de la orden, se rotula como ''INDIGOBOT''; La fecha de actualización (ULT_ACTUAL) se calcula convirtiendo GETDATE() a zona horaria ''Pakistan Standard Time''; El ID de compañía se reporta como el nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres; Para alta médica del ingreso usa la MÁXIMA fecha FECALTPAC por (NUMINGRES, IPCODPACI) en HCREGEGRE; Los códigos/descripciones de servicio priorizan CUPS y caen a producto de inventario cuando el ítem es medicamento/insumo; La identificación del profesional prioriza la del detalle quirúrgico, luego la del médico ejecutor y finalmente el NIT del tercero', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMPendingInvoicing: Devuelve un set tabular con encabezados de negocio (centro de atención, entidad, paciente, ingreso, servicio, valores, profesional, etc.) para registros de facturación pendiente conforme al filtro WHERE.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.IESTADOIN = '''' → Etiqueta el ingreso como ''ABIERTO'' else Si IESTADOIN=''P'' se etiqueta ''PARCIAL''; si ING.TIPOINGRE = 1 (Ambulatorio) → La fecha de egreso se toma como la fecha de ingreso (IFECHAING) else Para hospitalario se toma ISNULL(G.[FECHA ALTA MEDICA], ING.FECHEGRESO); si ING.TIPOINGRE = 1 AND CUPS.ServiceType IN (1,2,3,8) AND G.[FECHA ALTA MEDICA] IS NULL → Marca ''INGRESO ALTA MEDICA'' evaluando ING.IFECHAING para determinar ''SIN ALTA MEDICA'' o ''CON ALTA MEDICA'' else Evalúa solo si G.[FECHA ALTA MEDICA] es nulo para asignar ''SIN/CON ALTA MEDICA''; si ING.TIPOINGRE = 1 (Ambulatorio) → La unidad funcional de egreso usa ISNULL(UFE, UFI), cayendo a la de ingreso si no hay egreso else Para hospitalario usa directamente UFE (UFUEGRMED); si ISNULL(BG..., PG...) en GRUPO FACTURACION → Si el ítem es un servicio CUPS toma BillingGroup; si es producto de inventario toma ProductGroup; si SODS.OnlyMedicalFees=''0'' AND SODS.SurchargeApply<>1 → Solo se trae detalle quirúrgico que NO sea exclusivo de honorarios médicos y sin recargo aplicado; si HPC.LiquidateDefault = 1 → Solo se enlaza el contrato de honorarios médicos marcado como liquidación por defecto del profesional', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Billing.RevenueControlDetail; Billing.RevenueControl; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Payroll.FunctionalUnit; Common.ThirdParty; dbo.INPACIENT; dbo.ADINGRESO; dbo.INUNIFUNC; dbo.ADCENATEN; Payroll.CostCenter; Billing.ServiceOrder; Contract.CUPSEntity; Billing.BillingConcept; Inventory.InventoryProduct; Inventory.ProductGroup; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.ServiceOrderDetailSurgical; Contract.IPSService; dbo.INPROFSAL; dbo.INESPECIA; GeneralLedger.MainAccounts; MedicalFees.HealthProfessionalContract; MedicalFees.MedicalFeesContract; dbo.SEGusuaru; Billing.BillingGroup; Contract.DefinitionRateDetail; dbo.INDIAGNOS (+7 adicionales)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicing';
GO
