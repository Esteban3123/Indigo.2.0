

CREATE view [Report].[UploadCubeVieRCMPendingInvoicingPGP] AS

SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,RTRIM(CEN.NOMCENATE) AS 'CENTRO DE ATENCION','REGISTROS PGP' 'TIPO VENTA',
   CASE WHEN ING.IESTADOIN='' THEN 'ABIERTO' WHEN ING.IESTADOIN='P' THEN 'PARCIAL' END AS [ESTADO INGRESO],
   CASE ING.TIPOINGRE WHEN 1 THEN 'AMBULATORIO' WHEN 2 THEN 'HOSPITALARIO' END [TIPO INGRESO], TP.Nit AS NIT,  TP.Name AS ENTIDAD,  CG.Code AS [CODIGO GRUPO ATENCION],  CG.Name AS [GRUPO ATENCION], 
   CASE CG.EntityType WHEN 1 THEN 'EPS Contributivo' WHEN 2 THEN 'EPS Subsidiado'
   WHEN 3 THEN 'ET Vinculados Municipios' WHEN 4 THEN 'ET Vinculados Departamentos' WHEN 5 THEN 'ARL Riesgos Laborales' WHEN 6 THEN 'MP Medicina Prepagada'
   WHEN 7 THEN 'IPS Privada' WHEN 8 THEN 'IPS Publica' WHEN 9 THEN 'Regimen Especial' WHEN 10 THEN 'Accidentes de transito' WHEN 11 THEN 'Fosyga' WHEN 12 THEN 'Otros'
   WHEN 13 THEN 'Aseguradoras' WHEN 99 THEN 'Particulares' ELSE 'Otros' END 'REGIMEN',
   PAC.IPCODPACI AS IDENTIFICACION,
   RTRIM(PAC.IPNOMCOMP) AS PACIENTE,  ING.NUMINGRES AS INGRESO,  CAST(ING.IFECHAING AS DATE) AS [FECHA INGRESO],
   CASE WHEN ING.TIPOINGRE =1 THEN CAST(ING.IFECHAING AS DATE)  ELSE  CAST(ISNULL(G.[FECHA ALTA MEDICA],ING.FECHEGRESO) AS DATE) END AS [FECHA EGRESO],
   NULL AS [NRO FACTURA], NULL AS [FECHA FACTURA], 0 AS [TOTAL FACTURA],CASE SOD.RecordType WHEN 1 THEN 'SERVICIOS' WHEN 2 THEN 'MEDICAMENTOS/INSUMOS' END AS [TIPO REGISTRO],
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
 					   WHEN 3 THEN 'PAQUETE' ELSE 'NO QUIRURGICO' END AS PRESENTACION, ISNULL(BG.CODE  + ' - ' + BG.NAME,PG.CODE  + ' - ' + PG.NAME) 'GRUPO FACTURACION',
  CAST(SOD.ServiceDate AS DATE) AS [FECHA SERVICIO], 
  ISNULL(CUPS.Code,IPR.Code) AS [CUPS/PRODUCTO],
  ISNULL(CUPS.Description,IPR.Description ) AS DESCRIPCION, ISNULL(IPSC.Code, 'N/A') AS ' CODIGO SERVICIO IPS',SUBSTRING(ISNULL(RTRIM(IPSC.Name), 'N/A'),1,60) AS 'DESCRIPCION SERVICIO IPS',
  ISNULL(IPS.Code, 'N/A') AS 'CODIGO DETALLE QX', SUBSTRING(ISNULL(RTRIM(IPS.Name), 'N/A'),1,60) AS 'DESCRIPCION DETALLE QX', 
  CASE IPS.ServiceClass WHEN 1 THEN 'Ninguno' WHEN 2 THEN 'Cirujano' WHEN 3 THEN 'Anestesiologo' WHEN 4 THEN 'Ayudante' WHEN 5 THEN 'Derecho Sala' WHEN 6 THEN 'Materiales Sutura'
  WHEN 7 THEN 'Instrumentacion Quirurgica' ELSE 'Ninguno' end 'TIPO SERVICIO IPS',
  CD.Name 'DECRIPCION RELACIONADA',ISNULL(SODS.InvoicedQuantity,SOD.InvoicedQuantity) 'CANTIDAD',ISNULL(SODS.RateManualSalePrice,SOD.RateManualSalePrice) 'VALOR SERVICIO',
  CAST(ISNULL(SOD.CostValue,0) AS NUMERIC) 'COSTO SERVICIO',
  ISNULL(SODS.RateManualSalePrice,SOD.TotalSalesPrice) 'VALOR UNITARIO',
  ISNULL(SODS.TotalSalesPrice,SOD.GrandTotalSalesPrice) 'VALOR DETALLE TOTAL',ISNULL(SODS.TotalSalesPrice,SOD.GrandTotalSalesPrice) 'VALOR TOTAL POR FACTURA',
  RCD.TOTALPATIENTWITHDISCOUNT AS [VALOR COBRADO PACIENTE],RCD.VALUECOPAY AS [VALOR CUOTA RECUPERACION FOLIO],RCD.VALUEFEEMODERATOR AS [VALOR CUOTA MODERADORA FOLIO],
  RCD.OBSERVATION AS [OBSERVACIONES],CAT.NAME AS [CATEGORIA FACTURA],CASE RCD.STATUS WHEN '1' THEN 'REGISTRADO'	WHEN '2' THEN 'FACTURADO'WHEN '3' THEN 'BLOQUEADO' END AS [ESTADO],

  CASE SOD.IsPackage WHEN 0 THEN 'NO' WHEN 1 THEN 'SI' END 'ES PAQUETE',CASE SOD.Packaging WHEN 0 THEN 'NO' WHEN 1 THEN 'SI' END 'INCLUIDO EN PAQUETE',
  CUPSP.Code 'CODIGO PAQUETE',CUPSP.Description 'NOMBRE PAQUETE',CASE SOD.SettlementType WHEN 3 THEN 'SI (No se cobra nada)' ELSE 'NO (Se cobra)' END 'TIPO LIQUIDACION', 
  CUPSI.Code 'CUPS QUE INCLUYE',
  CUPSI.Description 'NOMBRE CUPS QUE INCLUYE', ISNULL(SODS.PerformsHealthProfessionalCode,ISNULL(MED.CODPROSAL,TPT.Nit))'IDENTIFICACION PROFESIONAL',
  RTRIM(ISNULL(MEDQX.NOMMEDICO,ISNULL(MED.NOMMEDICO,TPT.Name))) 'PROFESIONAL',ISNULL(ESPQX.DESESPECI,ESPMED.DESESPECI) 'ESPECIALIDAD',
  PAC.IPFECNACI AS [FECHA NACIMIENTO],DATEDIFF(YEAR, PAC.IPFECNACI, ING.IFECHAING) AS [EDAD CONSULTA],
  CASE RCD.RESPONSIBLERECOVERYFEE
				WHEN '1' THEN 'NINGUNO'
				WHEN '2' THEN 'PACIENTE'
				WHEN '3' THEN 'TERCERO' END AS [RESPONSABLE CUOTA RECUPERACION],
			CASE PAC.TIPCOBSAL
				WHEN '1' THEN 'CONTRIBUTIVO'
				WHEN '2' THEN 'SUBSIDIADO TOTAL'
				WHEN '3' THEN 'SUBSIDIADO PARCIAL'
				WHEN '4' THEN 'POBLACION POBRE SIN ASEGURAR CON SISBEN'
				WHEN '5' THEN 'POBLACION POBRE SIN ASEGURAR SIN SISBEN'
				WHEN '6' THEN 'DESPLAZADOS'
				WHEN '7' THEN 'PLAN DE SALUD ADICIONAL'
				WHEN '8' THEN 'OTROS'
				ELSE 'DESCONOCIDO' END AS [TIPO PACIENTE],SU.NOMUSUARI AS [USUARIO CREO],RCD.CREATIONDATE AS [FECHA CREACION],SUSO.NOMUSUARI AS [USUARIO MODIFICO],
			    RCD.MODIFICATIONDATE AS [FECHA MODIFICO],CASE SOD.SERVICETYPE WHEN '1' THEN 'SOAT'	WHEN '2' THEN 'ISS'	WHEN '3' THEN 'CUPS' ELSE 'PRODUCTO'END AS [CLASE SERVICIO],
				ISNULL(CGF.Code + '-' + CGF.Name, PG.Code + '-' + PG.NAME) [GRUPO],ISNULL(CSG.CODE + '-' + CSG.Name, PSG.Code + '-' + PSG.NAME) [SUBGRUPO],
				SOD.AUTHORIZATIONNUMBER AS [AUTORIZACION],FU.Code 'CODIGO UNIDAD SOLICITO' ,FU.Name 'UNIDAD SOLICITO', ISNULL(COSTQ.CODE,COST.CODE) 'CODIGO CC CONTABILIZO',ISNULL(COSTQ.Name,COST.Name) 'CENTRO COSTO CONTABILIZO', 
  MA.Number 'NRO CUENTA CONTABILIZA',  MA.Name 'CUENTA CONTABILIZA',
  IIF (ING.TIPOINGRE = 1 AND CUPS.ServiceType IN (1, 2, 3, 8) AND G.[FECHA ALTA MEDICA] IS NULL,CASE WHEN ING.IFECHAING IS NULL THEN 'SIN ALTA MEDICA' ELSE 'CON ALTA MEDICA' END, 
			CASE WHEN G.[FECHA ALTA MEDICA] IS NULL THEN 'SIN ALTA MEDICA' ELSE 'CON ALTA MEDICA' END) AS 'INGRESO ALTA MEDICA',
			UFI.UFUDESCRI AS [UNIDAD FUNCIONAL INGRESO],
			IIF (ING.TIPOINGRE = 1, ISNULL(UFE.UFUDESCRI,UFI.UFUDESCRI),UFE.UFUDESCRI) AS [UNIDAD FUNCIONAL EGRESO],
			CCSF.Name [ESTADO FOLIO],
			ISNULL(ING.CODDIAING,ING.CODDIAEGR) 'CIE 10',DIA.NOMDIAGNO 'DIAGNOSTICO',ISNULL(SUSOM.NOMUSUARI,'INDIGOBOT') 'USUARIO CREO ORDEN',CAST(SO.CreationDate AS DATE) 'FECHA ORDEN',
			YEAR(ING.IFECHAING) 'AÑO INGRESO', MONTH(ING.IFECHAING) 'MES INGRESO',OU.UnitName [CIUDAD ORDENAMIENTO],
			CAST(ING.IFECHAING AS DATE) as 'FECHA BUSQUEDA',
			CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
   
FROM Contract.CareGroup cg 
INNER JOIN Billing.RevenueControlDetail rcd  ON cg.Id = rcd.CareGroupId
INNER JOIN Billing.RevenueControl rc on rc.Id = rcd.RevenueControlId
INNER JOIN Billing.ServiceOrderDetailDistribution sodd  on sodd.RevenueControlDetailId = rcd.Id
INNER JOIN Billing.ServiceOrderDetail sod  on sodd.ServiceOrderDetailId = sod.Id 
INNER JOIN Billing.ServiceOrder AS SO  ON SO.Id =SOD.ServiceOrderId AND SO.AdmissionNumber=RC.AdmissionNumber
INNER JOIN Payroll.FunctionalUnit fu  on fu.Id = sod.PerformsFunctionalUnitId
INNER JOIN Common.ThirdParty AS TP  ON TP.Id =RCD.ThirdPartyId
INNER JOIN DBO.INPACIENT AS PAC ON PAC.IPCODPACI =RC.PatientCode
INNER JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES =RC.AdmissionNumber
INNER JOIN DBO.INUNIFUNC AS UFI WITH (NOLOCK) ON ING.UFUCODIGO = UFI.UFUCODIGO
INNER JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE = ING.CODCENATE
INNER JOIN Payroll.CostCenter AS COST ON COST.Id =SOD.CostCenterId
LEFT JOIN Contract.CUPSEntity CUPS on sod.CUPSEntityId = CUPS.Id
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
WHERE rcd.Status in (1,3) AND sod.IsDelete = 0 AND sod.SettlementType != 3 AND sod.GrandTotalSalesPrice > 0 AND cg.LiquidationType In (2,5) AND ING.IESTADOIN IN (' ','P')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada al cubo de datos RCM (Revenue Cycle Management) que consolida los servicios y medicamentos/insumos pendientes de facturación bajo modalidad PGP (pago global prospectivo), filtrando folios en estado registrado o bloqueado con ingresos abiertos o parciales. Aplana información de admisiones ambulatorias y hospitalarias, entidad pagadora, paciente, profesional, grupo de atención, tarifas, cuentas contables, unidades funcionales y clasificaciones CUPS/inventario. Excluye ítems eliminados, liquidados sin cobro y valores en cero, sirviendo como fuente para análisis de cartera pendiente por facturar.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicingPGP';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros pendientes de facturar: NRO FACTURA, FECHA FACTURA y TOTAL FACTURA siempre se devuelven NULL/0 (constantes literales).; El tipo de venta siempre es la constante ''REGISTROS PGP''.; Se excluyen siempre los servicios marcados como eliminados (sod.IsDelete=0).; Se excluyen los detalles cuyo SettlementType=3 (no se cobra) y los que tienen GrandTotalSalesPrice<=0.; Solo se consideran grupos de atención cuyo LiquidationType esté en (2,5) — esquema PGP.; Solo ingresos con IESTADOIN en ('' '',''P''), es decir abiertos o parciales.; El folio debe estar en estado REGISTRADO (1) o BLOQUEADO (3); los FACTURADOS (2) quedan fuera.; El detalle quirúrgico solo se vincula si OnlyMedicalFees=''0'' y SurchargeApply<>1.; El contrato de honorarios médicos se toma siempre con HPC.LiquidateDefault=1.; ID_COMPANY se trunca a 9 caracteres del nombre de la base de datos actual.; Cuando ServiceOrder se enlaza, se exige consistencia AdmissionNumber entre la orden y el control de ingresos (SO.AdmissionNumber=RC.AdmissionNumber).; La FECHA ALTA MEDICA se obtiene como la última (MAX) de HCREGEGRE por ingreso/paciente.; El timestamp ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de atención; Tipo de ingreso (ambulatorio/hospitalario); Estado del ingreso (abierto/parcial); Régimen del asegurador (EPS, ARL, MP, IPS, etc.); Grupo de atención (CareGroup); Folio de facturación (RevenueControl/Detail); Cuota moderadora; Cuota de recuperación; Copago; Responsable de cuota de recuperación; CUPS y servicios IPS; Paquete quirúrgico / liquidación por paquete; Honorarios médicos / contrato de profesional de salud; Categoría de factura; Estado del folio; Diagnóstico CIE-10; Alta médica; Unidad funcional de ingreso/egreso; Centro de costo contable; Cuenta contable de ingreso; Tipo de paciente (contributivo, subsidiado, desplazado, etc.); Liquidación PGP (capitación/pago global prospectivo); Autorización de servicio', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.TIPOINGRE = 1 (ambulatorio) → [FECHA EGRESO] = IFECHAING (fecha del ingreso) else [FECHA EGRESO] = ISNULL(FECHA ALTA MEDICA de HCREGEGRE, FECHEGRESO); si ING.TIPOINGRE = 1 AND CUPS.ServiceType IN (1,2,3,8) AND no existe FECHA ALTA MEDICA → Marca [INGRESO ALTA MEDICA] según IFECHAING (CON/SIN ALTA MEDICA) else Marca según existencia de FECHA ALTA MEDICA en HCREGEGRE; si ING.TIPOINGRE = 1 (ambulatorio) → [UNIDAD FUNCIONAL EGRESO] = ISNULL(UFE, UFI) (cae a unidad de ingreso si no hay egreso) else [UNIDAD FUNCIONAL EGRESO] = UFE (UFUEGRMED del ingreso); si SOD.SettlementType = 3 → Etiqueta ''SI (No se cobra nada)'' como tipo liquidación else Etiqueta ''NO (Se cobra)''; si Existe SODS (detalle quirúrgico con OnlyMedicalFees=0 y SurchargeApply<>1) → Toma cantidades, precios, profesional y centro de costo desde el detalle quirúrgico (SODS) else Toma valores desde el detalle de orden de servicio (SOD); si RCD.STATUS in (1,3) → Se incluye el folio (REGISTRADO o BLOQUEADO); excluye STATUS=2 FACTURADO', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Billing.RevenueControlDetail; Billing.RevenueControl; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Billing.ServiceOrder; Payroll.FunctionalUnit; Common.ThirdParty; DBO.INPACIENT; DBO.ADINGRESO; DBO.INUNIFUNC; DBO.ADCENATEN; Payroll.CostCenter; Contract.CUPSEntity; Billing.BillingConcept; Inventory.InventoryProduct; Inventory.ProductGroup; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.ServiceOrderDetailSurgical; Contract.IPSService; dbo.INPROFSAL; dbo.INESPECIA; GeneralLedger.MainAccounts; MedicalFees.HealthProfessionalContract; MedicalFees.MedicalFeesContract; DBO.SEGusuaru; Billing.BillingGroup; Contract.DefinitionRateDetail; DBO.INDIAGNOS (+7 adicionales)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicingPGP';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMPendingInvoicingPGP';
GO
