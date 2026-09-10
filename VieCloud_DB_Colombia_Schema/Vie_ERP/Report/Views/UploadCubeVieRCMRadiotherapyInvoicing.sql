CREATE view [Report].[UploadCubeVieRCMRadiotherapyInvoicing] AS

SELECT EMPRESA, [CENTRO ATENCION], IDENTIFICACION, [NOMBRE PACIENTE], [GRUPO FACTURACION], [NRO DOCUMENTO],[FECHA DE INGRESO], [FECHA FACTURA/CONTROL], CUPS_CODIGO, [DESCRIPCION SERVICIO],
SUM(cast(PRECIO_VENTA_TOTAL as numeric)) AS TOTAL  , [INGRESO],[ULT_ACTUAL]

FROM 
(
SELECT 'ODO' AS EMPRESA,
	CASE WHEN ING.CODCENATE IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN '0' ELSE ING.CODCENATE
       END AS [CODIGO CENTRO],
       CASE WHEN CEN.NOMCENATE IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'DESCONOCIDO' ELSE CEN.NOMCENATE
       END AS [CENTRO ATENCION],
       CASE F.STATUS WHEN '1' THEN 'FACTURADO' WHEN '2' THEN 'ANULADO'
       END AS ESTADO_DOCUMENTO,
       CASE F.DOCUMENTTYPE
         WHEN '1' THEN 'FACTURA EAPB CON CONTRATO'
         WHEN '2' THEN 'FACTURA EAPB SIN CONTRATO'
         WHEN '3' THEN 'FACTURA PARTICULAR'
         WHEN '4' THEN 'FACTURA CAPITA'
         WHEN '5' THEN 'CONTROL CAPITACION'
         WHEN '6' THEN 'FACTURA BASICA'
         WHEN '7' THEN 'FACTURA VENTA PRODUCTOS'
       END AS [TIPO DOCUMENTO],
       LTRIM(ING.UFUCODIGO) + ' - ' + UF.UFUDESCRI AS [UNIDAD FUNCIONAL INGRESO],
       CASE ING.TIPOINGRE WHEN 1 THEN 'AMBULATORIO' WHEN 2 THEN 'HOSPITALARIO' ELSE 'DESCONOCIDO'
       END AS [TIPO INGRESO],
       T.Nit AS NIT,
	   EA.Name AS [ENTIDAD ADMINISTRADORA],
       CASE
           WHEN (GA.CODE + ' - ' + GA.NAME) IS NULL THEN 'NO APLICA'
           ELSE (GA.CODE + ' - ' + GA.NAME)
       END AS [GRUPO ATENCIÓN],
       CASE EA.EntityType
         WHEN 1 THEN 'EPS CONTRIBUTIVO'
         WHEN 2 THEN 'EPS SUBSIDIADO'
         WHEN 3 THEN 'ET VINCULADO MUNICIPIO'
         WHEN 4 THEN 'ET VINCULADOS DAPARTAMENTO'
         WHEN 5 THEN 'ARL RIESGO LABORALES'
         WHEN 6 THEN 'MP MEDICINA PREPAGADA'
         WHEN 7 THEN 'IPS PRIVADA'
         WHEN 8 THEN 'IPS PUBLICA'
         WHEN 9 THEN 'REGIMEN ESPECIAL'
         WHEN 10 THEN 'ACCIDENTE DE TRANSITO'
         WHEN 11 THEN 'FOSYGA'
         WHEN 12 THEN 'OTROS'
       END AS [TIPO REGIMEN EN FACTURACION],
          F.PatientCode AS IDENTIFICACION,
          P.IPNOMCOMP AS [NOMBRE PACIENTE],
          F.InvoiceNumber AS [NRO DOCUMENTO],
          F.AdmissionNumber AS INGRESO,
          CAST(F.InvoiceDate AS DATETIME) AS [FECHA FACTURA/CONTROL],
          C.Name AS CATEGORÍA,
          ISNULL(GF.Name, GF2.Name) AS [GRUPO FACTURACION],
          CASE PT.Class
              WHEN '2' THEN 'MEDICAMENTOS'
              WHEN '3' THEN 'INSUMOS'
              ELSE 'SERVICIOS'
          END AS [TIPO DE SERVICIO],
          ISNULL(CG.Code + '-' + CG.Name, PG.Code + '-' + PG.NAME) [GRUPO],
          ISNULL(CSG.CODE + '-' + CSG.Name, PSG.Code + '-' + PSG.NAME) [SUBGRUPO],
          COALESCE(CUPS.Code, SERVICIOSIPS.CODE, PR.CODE) CUPS_CODIGO,
		  CASE WHEN CDD.Name IS NOT NULL THEN CDD.Name
			   ELSE COALESCE(CUPS.Description, SERVICIOSIPS.NAME, PR.NAME) 
		  END [DESCRIPCION SERVICIO],
          CASE
              WHEN PR.CODE IS NULL THEN SERVICIOSIPS.CODE
              ELSE PR.CODE
          END AS CODIGO,
          CASE
              WHEN PR.NAME IS NULL THEN SERVICIOSIPS.NAME
              ELSE PR.NAME
          END AS DESCRIPCION,
          PR.CodeCUM [CODIGO CUM PRODUCTO],
          DF.InvoicedQuantity AS CANTIDAD_POR_ORDEN_SERVICIO,
          IIF(SERVICIOSIPSQ.Code IS NULL, DF.TotalSalesPrice, DQ.TOTALSALESPRICE) AS PRECIO_UNITARIO_VENTA,
          IIF(SERVICIOSIPSQ.Code IS NULL, DF.GrandTotalSalesPrice, DQ.TOTALSALESPRICE) AS PRECIO_VENTA_TOTAL,
          BO.Name AS [CENTRO ATENCION SERVICIO],
          LTRIM(FU.Code) + ' - ' + FU.Name AS [UNIDAD FUNCIONAL SERVICIO],
		  sod.servicedate AS [FECHA PRESTACION DEL SERVICIO],
          CASE
              WHEN DQ.PERFORMSHEALTHPROFESSIONALCODE IS NULL THEN ISNULL(SOD.PERFORMSHEALTHPROFESSIONALCODE, '000')
              ELSE DQ.PERFORMSHEALTHPROFESSIONALCODE
          END AS [CODIGO MEDICO],
          CASE
              WHEN RTRIM(MEDQX.NOMMEDICO) IS NULL THEN ISNULL(MED.NOMMEDICO, 'NO APLICA')
              ELSE RTRIM(MEDQX.NOMMEDICO)
          END AS [NOMBRE MEDICO],
          CASE
              WHEN ESPMED.DESESPECI IS NULL THEN ESPQX.DESESPECI
              ELSE ESPMED.DESESPECI
          END AS ESPECIALIDAD,
          YEAR(F.InvoicedDate) AS AÑO_FECHA_FACTURA,
          MONTH(F.InvoicedDate) AS MES_AÑO_FACTURA,
          DAY(F.InvoicedDate) AS DIA_FECHA_FACTURA,
          COST.Code [CODIGO CENTRO COSTO],
          COST.Name [CENTRO DE COSTO],
          CASE F.STATUS
              WHEN '1' THEN CAST(F.InvoiceDate AS DATETIME)
              WHEN '2' THEN CAST(F.AnnulmentDate AS DATETIME)
          END AS [FECHA BUSQUEDA],
          SOD.CostValue AS [COSTO PRODUCTO],
		  f.initialdate AS [FECHA DE INGRESO],
		  f.outputdate AS [FECHA DE EGRESO/CORTE],
		  CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM Billing.Invoice AS F WITH (NOLOCK)
   INNER JOIN Billing.InvoiceDetail AS DF WITH (NOLOCK) ON DF.InvoiceId = F.Id
   INNER JOIN Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = DF.ServiceOrderDetailId
   INNER JOIN Payroll.FunctionalUnit AS FU ON FU.Id = SOD.PerformsFunctionalUnitId
   INNER JOIN Payroll.BranchOffice AS BO WITH (NOLOCK) ON FU.BranchOfficeId =BO.Id
   INNER JOIN DBO.SEGusuaru AS U WITH (NOLOCK) ON U.CODUSUARI = F.InvoicedUser
   --INNER JOIN INDIGOSEC.Security.Person AS PER WITH (NOLOCK) ON PER.Id = U.IdPerson
   INNER JOIN dbo.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES = F.AdmissionNumber
   INNER JOIN dbo.INPACIENT AS P WITH (NOLOCK) ON P.IPCODPACI = F.PatientCode
   INNER JOIN Common.ThirdParty AS T WITH (NOLOCK) ON T.Id = F.ThirdPartyId
   INNER JOIN Contract.CareGroup AS GA WITH (NOLOCK) ON GA.Id = F.CareGroupId
   INNER JOIN Contract.HealthAdministrator AS EA WITH (NOLOCK) ON EA.Id = F.HealthAdministratorId
   INNER JOIN Billing.InvoiceCategories AS CAT WITH (NOLOCK) ON CAT.Id = F.InvoiceCategoryId
   LEFT JOIN Contract.Contract AS CC WITH (NOLOCK) ON GA.ContractId =CC.Id
   LEFT JOIN Contract.ContractDetail AS CD WITH (NOLOCK) ON CC.Id = CD.ContractId AND cd.validrecord = 1
   LEFT JOIN Contract.IPSService AS SERVICIOSIPS WITH (NOLOCK) ON SERVICIOSIPS.Id = SOD.IPSServiceId
   LEFT JOIN dbo.ADCENATEN AS CEN WITH (NOLOCK) ON CEN.CODCENATE = ING.CODCENATE
   LEFT JOIN Inventory.InventoryProduct AS PR WITH (NOLOCK) ON PR.Id = SOD.ProductId
   LEFT JOIN Inventory.ProductType AS PT ON PR.ProductTypeId = PT.Id
   LEFT JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.ID =PR.ProductGroupId
   LEFT JOIN Inventory.ProductSubGroup AS PSG WITH (NOLOCK) ON PSG.ID =PR.ProductSubGroupId
   LEFT JOIN Billing.InvoiceCategories AS C WITH (NOLOCK) ON C.Id = F.InvoiceCategoryId
   LEFT JOIN dbo.INUNIFUNC AS UF WITH (NOLOCK) ON UF.UFUCODIGO = ING.UFUCODIGO
   LEFT JOIN dbo.HCREGEGRE AS EH WITH (NOLOCK) ON EH.NUMINGRES = F.AdmissionNumber AND EH.IPCODPACI = F.PatientCode
   LEFT JOIN dbo.INPROFSAL AS MED WITH (NOLOCK) ON MED.CODPROSAL = SOD.PerformsHealthProfessionalCode
   LEFT JOIN dbo.INESPECIA AS ESPMED WITH (NOLOCK) ON ESPMED.CODESPECI = MED.CODESPEC1
   LEFT JOIN Billing.ServiceOrderDetailSurgical AS DQ WITH (NOLOCK) ON DQ.ServiceOrderDetailId = SOD.Id AND DQ.OnlyMedicalFees = '0'
   LEFT JOIN dbo.INPROFSAL AS MEDQX WITH (NOLOCK) ON MEDQX.CODPROSAL = DQ.PerformsHealthProfessionalCode
   LEFT JOIN dbo.INESPECIA AS ESPQX WITH (NOLOCK) ON ESPQX.CODESPECI = MEDQX.CODESPEC1
   LEFT JOIN Contract.IPSService AS SERVICIOSIPSQ WITH (NOLOCK) ON SERVICIOSIPSQ.Id = DQ.IPSServiceId
   LEFT JOIN Contract.CUPSEntity AS CUPS WITH (NOLOCK) ON CUPS.Id = SOD.CUPSEntityId
   LEFT JOIN Contract.CupsSubgroup AS CSG WITH (NOLOCK) ON CSG.Id =CUPS .CUPSSubGroupId
   LEFT JOIN Contract.CupsGroup AS CG WITH (NOLOCK) ON CG.Id =CSG.CupsGroupId
   LEFT JOIN Billing.BillingGroup AS GF WITH (NOLOCK) ON GF.Id = CUPS.BillingGroupId
   LEFT JOIN Billing.BillingGroup AS GF2 WITH (NOLOCK) ON GF2.Id = PR.BillingGroupId
   LEFT JOIN dbo.INUBICACI AS BB WITH (NOLOCK) ON BB.AUUBICACI = P.AUUBICACI
   LEFT JOIN dbo.INMUNICIP AS EE WITH (NOLOCK) ON EE.DEPMUNCOD = BB.DEPMUNCOD
   LEFT JOIN Contract.CUPSEntityContractDescriptions AS CECD WITH (NOLOCK) ON CECD.ID=SOD.CUPSEntityContractDescriptionId
   LEFT JOIN Contract.ContractDescriptions AS CDD WITH (NOLOCK) ON CECD.ContractDescriptionId =CDD.Id
   LEFT JOIN Payroll.CostCenter AS COST ON COST.Id =SOD.CostCenterId
   ) AS FACTURACION_DETALLADA

WHERE --CAST([FECHA BUSQUEDA] AS DATE) BETWEEN @FECHA_INICIO AND @FECHA_FIN
--AND 
CUPS_CODIGO IN ('922433','922441','922442','922443','922444','922446')
AND ESTADO_DOCUMENTO = 'FACTURADO'
GROUP BY EMPRESA, [CENTRO ATENCION], IDENTIFICACION, [NOMBRE PACIENTE], [GRUPO FACTURACION], [NRO DOCUMENTO],[FECHA DE INGRESO], [FECHA FACTURA/CONTROL], CUPS_CODIGO, [DESCRIPCION SERVICIO],
[INGRESO],[ULT_ACTUAL]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para el cubo de datos RCM (Revenue Cycle Management) orientada a facturación de radioterapia. Consolida y agrupa facturas en estado FACTURADO cuyo código CUPS corresponde exclusivamente a procedimientos de radioterapia (922433, 922441–922444, 922446), sumando el precio de venta total por paciente, documento, servicio y período. Integra información de admisiones, entidades administradoras, contratos, profesionales, centros de costo y productos para alimentar análisis de ingresos en herramientas OLAP.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMRadiotherapyInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMRadiotherapyInvoicing';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de carga al cubo de facturación que consolida los valores facturados (estado FACTURADO) por paciente, factura y CUPS, restringidos a los procedimientos de radioterapia.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMRadiotherapyInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existir facturas en Billing.Invoice con su detalle (InvoiceDetail) y orden de servicio (ServiceOrderDetail) consistentes por Id.; Cada factura debe tener admisión (ADINGRESO), paciente (INPACIENT), tercero (ThirdParty), grupo de atención (CareGroup), administradora de salud (HealthAdministrator) y categoría de factura existentes (INNER JOIN).; La unidad funcional del servicio (FunctionalUnit) y su sede (BranchOffice) deben existir.; El usuario que facturó debe existir en SEGusuaru.; Para que la línea aparezca, el código CUPS resuelto debe estar dentro de la lista de procedimientos de radioterapia y la factura debe estar en estado ''FACTURADO''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMRadiotherapyInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Empresa siempre se reporta como literal ''ODO''.; Solo se incluyen documentos cuyo estado es ''FACTURADO'' (Invoice.STATUS = ''1''); se excluyen anulados.; Solo se devuelven líneas cuyo CUPS_CODIGO pertenece al conjunto fijo de radioterapia: 922433, 922441, 922442, 922443, 922444, 922446.; El código CUPS se resuelve en cascada con COALESCE: primero CUPSEntity.Code, luego IPSService.Code, y por último InventoryProduct.Code.; La descripción del servicio prioriza la descripción de contrato (ContractDescriptions.Name) sobre la del CUPS, IPSService o producto.; Cuando existe detalle quirúrgico (ServiceOrderDetailSurgical) se usa su precio total y profesional; en su ausencia se usa el del InvoiceDetail/ServiceOrderDetail.; Se considera detalle quirúrgico únicamente cuando OnlyMedicalFees = ''0'' (no son honorarios médicos exclusivos).; Solo se consideran detalles de contrato vigentes (ContractDetail.validrecord = 1).; PRECIO_VENTA_TOTAL se agrega con SUM por las claves de agrupación (empresa, centro, paciente, factura, CUPS, descripción, ingreso).; ULT_ACTUAL siempre se calcula con la hora actual convertida a zona horaria ''Pakistan Standard Time''.; Si no existe centro de atención en el ingreso y el documento es tipo 6 (Factura básica), se asigna ''DESCONOCIDO'' / código ''0''.; El tipo de servicio se deriva de ProductType.Class: 2=MEDICAMENTOS, 3=INSUMOS, otro=SERVICIOS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMRadiotherapyInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación de radioterapia; Códigos CUPS de radioterapia; Paciente; Ingreso/admisión; Entidad administradora (EPS); Grupo de atención; Unidad funcional; Centro de atención; Profesional de la salud; Especialidad médica; Centro de costo; Servicios IPS; Estado de factura (Facturado/Anulado)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMRadiotherapyInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieRCMRadiotherapyInvoicing: Devuelve, por empresa/centro/paciente/factura/CUPS, el SUM(PRECIO_VENTA_TOTAL) filtrando CUPS_CODIGO IN (''922433'',''922441'',''922442'',''922443'',''922444'',''922446'') y ESTADO_DOCUMENTO = ''FACTURADO''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMRadiotherapyInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Payroll.FunctionalUnit; Payroll.BranchOffice; dbo.SEGusuaru; dbo.ADINGRESO; dbo.INPACIENT; Common.ThirdParty; Contract.CareGroup; Contract.HealthAdministrator; Billing.InvoiceCategories; Contract.Contract; Contract.ContractDetail; Contract.IPSService; dbo.ADCENATEN; Inventory.InventoryProduct; Inventory.ProductType; Inventory.ProductGroup; Inventory.ProductSubGroup; dbo.INUNIFUNC; dbo.HCREGEGRE; dbo.INPROFSAL; dbo.INESPECIA; Billing.ServiceOrderDetailSurgical; Contract.CUPSEntity; Contract.CupsSubgroup; Contract.CupsGroup; Billing.BillingGroup; dbo.INUBICACI (+4 adicionales)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMRadiotherapyInvoicing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieRCMRadiotherapyInvoicing';
GO
