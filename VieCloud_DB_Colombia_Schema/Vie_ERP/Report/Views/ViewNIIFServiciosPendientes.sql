
    /*******************************************************************************************************************
Nombre: [Report].[ViewNIIFServiciosPendientes]
Tipo:Vista
Observacion:Servicios pendientes por facturar
Profesional:Andres Cabrera
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:12-03-2024
Ovservaciones:Se ajustan los campos de egreso
--------------------------------------
Vercion 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewNIIFServiciosPendientes] AS
WITH CTE_SERVICIOS_PENDIENTES
 AS
  (
select RCD.id,RCD.RevenueControlId,RCD.BillingAuthorizationId ,RCD.FolioOrder ,RCD.FolioType ,RCD.LiquidationType ,RCD.ContractEntityId ,RCD.HealthAdministratorId,
ING.FECHEGRESO,
FUN.UFUCODIGO+'-'+FUN.UFUDESCRI AS [UNIDAD FUNCIONAL DE EGRESO] ,
RCD.ThirdPartyId ,RCD.CareGroupId ,RCD.TotalFolio,RCD.Status ,RCD.StatusFolioId,RCD.InvoiceCategoryId ,RTRIM(T.Nit) 'NIT ENTIDAD' ,
CASE WHEN RCD.ContractEntityId IS NULL THEN NULL ELSE RTRIM(HA.Code) END 'CODIGO ENTIDAD',
CASE WHEN RCD.ContractEntityId IS NULL THEN RC.PatientCode ELSE RTRIM(HA.Name) END 'ENTIDAD',
CG.Code 'CODIGO GRUPO', RTRIM(CG.Name) 'GRUPO DE ATENCION',RC.AdmissionNumber 'INGRESO',ING.IFECHAING 'FECHA INGRESO', CASE PAC.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA' WHEN '2' THEN 'CEDULA DE EXTRANJERIA' WHEN '3' THEN 'TARJETA DE IDENTIDAD' WHEN '4' THEN 'REGISTRO CIVIL'
WHEN '5' THEN 'PASAPORTE' WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' WHEN '7' THEN 'MENOR SIN IDENTIFICACION' WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACION'
WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' WHEN '10' THEN 'CARNET DIPLOMATICO'WHEN '11' THEN 'SALVOCONDUCTO' WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' ELSE 'N/A' END AS 'TIPO IDENTIFICACION',
ISNULL(RC.PatientCode, 0) AS 'IDENTIFICACION',RTRIM(PAC.IPNOMCOMP) 'PACIENTE', CAST (PAC.IPFECNACI AS DATE) AS 'FECHA NACIMIENTO',
FLOOR((CAST(CONVERT(VARCHAR(8),ING.IFECHAING,112) AS INT) - CAST(CONVERT(VARCHAR(8), PAC.IPFECNACI, 112) AS INT)) / 10000) AS 'EDAD', 
CASE PAC.IPTIPOPAC WHEN  '1' THEN 'Contributivo' 
WHEN '2' THEN 'Subsidiado'
WHEN '3' THEN 'Vinculado'
WHEN '4' THEN 'Particular'
WHEN '5' THEN 'Otro'
WHEN '6' THEN 'Desplazado Reg. Contributivo' 
WHEN '7' THEN 'Desplazado Reg. Subsidiado'
WHEN '8' THEN 'Desplazado No Asegurado' END AS 'REGIMEN',
PAC.TIPCOBSAL AS 'TIPO PACIENTE',
ING.CODDIAING AS [DIAGNOSTICO],
DIA.NOMDIAGNO AS [DESC_DIAGNOSTICO],
CASE WHEN ING.TIPOINGRE=1 THEN 'AMBULATORIO' WHEN ING.TIPOINGRE=2 THEN 'HOSPITALARIO' END 'TIPO AMBITO',ING.IAUTORIZA 'AUTORIZACION'
FROM BILLING.REVENUECONTROLDETAIL AS RCD WITH (NOLOCK)
INNER JOIN BILLING.REVENUECONTROL AS RC WITH (NOLOCK) ON RCD.REVENUECONTROLID = RC.ID
INNER JOIN DBO.ADINGRESO AS ING WITH (NOLOCK) ON ING.NUMINGRES = RC.ADMISSIONNUMBER
INNER JOIN DBO.INPACIENT AS PAC WITH (NOLOCK) ON PAC.IPCODPACI = ING.IPCODPACI
LEFT JOIN dbo.INDIAGNOS AS DIA ON DIA.CODDIAGNO=ING.CODDIAING
INNER JOIN CONTRACT.CAREGROUP AS CG WITH (NOLOCK) ON CG.ID = RCD.CAREGROUPID
INNER JOIN CONTRACT.HEALTHADMINISTRATOR AS HA WITH (NOLOCK) ON HA.ID = RCD.HEALTHADMINISTRATORID
INNER JOIN COMMON.THIRDPARTY AS T WITH (NOLOCK) ON T .ID = RCD.ThirdPartyId
LEFT JOIN CONTRACT.CONTRACTENTITY AS E WITH (NOLOCK) ON E.ID = RCD.CONTRACTENTITYID
LEFT JOIN dbo.INUNIFUNC FUN ON ING.UFUEGRMED=FUN.UFUCODIGO
WHERE ING.IESTADOIN <> 'A' AND ING.IESTADOIN <> 'F' AND ING.IESTADOIN <> 'C'
),

CTE_ALTA_MEDICA
AS
(
  SELECT EGR.NUMINGRES ,EGR.IPCODPACI ,MAX(FECALTPAC) 'FECHA ALTA' FROM DBO.HCREGEGRE EGR
  INNER JOIN CTE_SERVICIOS_PENDIENTES AS PEN ON PEN.INGRESO =EGR.NUMINGRES 
  GROUP BY EGR.NUMINGRES ,EGR.IPCODPACI
),

CTE_CAMAS
AS
(
     SELECT FUN.UFUDESCRI 'UNIDAD FUNCIONAL' ,C.IPCODPACI 'IDENTIFICACION' ,C.NUMINGRES 'INGRESO',A.NUMCAMHOS 'CAMA',G.DESTIPEST 'TIPO ESTANCIA'  ,C.ID  'ID_ESTANCIA', 
	 A.CODICAMAS 'ID_CAMAS', CEN.NOMCENATE 'CENTRO DE ATENCION',CEN.LATITUD AS [UBICACION_LATITUD],
     CEN.LONGITUD AS [UBICACION_LONGITUD],A.CODCLAHAB ,A.CODCLACAM 
	 FROM dbo.CHCAMASHO A with (nolock)
	 INNER JOIN DBO.INUNIFUNC AS FUN with (nolock) ON A.UFUCODIGO =FUN.UFUCODIGO 
     INNER JOIN dbo.CHREGESTA C with (nolock) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
     INNER JOIN dbo.CHTIPESTA G with (nolock) ON G.CODTIPEST=C.CODTIPEST 
	 INNER JOIN DBO.ADCENATEN AS CEN with (nolock) ON CEN.CODCENATE =A.CODCENATE 

),

CTE_HISTORIAS_AMBULATORIAS
AS
(
  SELECT HIS.NUMINGRES ,HIS.IPCODPACI ,MAX(FECHISPAC) AS 'FECHA ALTA'    FROM  DBO.HCHISPACA HIS
  INNER JOIN CTE_SERVICIOS_PENDIENTES AS PEN ON PEN.INGRESO =HIS.NUMINGRES 
  WHERE HIS.GENCONEXT =1
  GROUP BY HIS.NUMINGRES ,HIS.IPCODPACI
),
CTE_AMBITO
AS
(
select 
ING.NUMINGRES, 
CASE UNF.UnitType   WHEN 1 THEN 'URGENCIAS' 
					WHEN 2 THEN 'HOSPITALIZACION'
					WHEN 3 THEN 'APOYO DIAGNOSTICO'
					WHEN 4 THEN 'APOYO TERAPEUTICO'
					WHEN 5 THEN 'UCI'
					WHEN 6 THEN 'UCI'
					WHEN 7 THEN 'UCI'
					WHEN 8 THEN 'UCI'
					WHEN 9 THEN 'UCI'
					WHEN 10 THEN 'UCI'
					WHEN 11 THEN 'UCI'
					WHEN 12 THEN 'UNIDAD RENAL'
					WHEN 13 THEN 'UNIDAD ONCOLOGICA' 
					WHEN 14 THEN 'UNIDAD MEDICINA NUCLEAR'
					WHEN 15 THEN 'AMBULATORIO'
					WHEN 16 THEN 'UNIDAD MENTAL'
					WHEN 17 THEN 'UNIDAD DE QUEMADOS'
					WHEN 18 THEN 'UNIDAD DE CUIDADOS PALATIVOS'
					WHEN 19 THEN 'CIRUGIA'
					WHEN 20 THEN 'LABORATORIOS'
					WHEN 21 THEN 'CARDIOLOGIA NO INVASIVA'
					WHEN 22 THEN 'CARDIOLOGIA INVASIVA'
					WHEN 23 THEN 'GINECO OBSTETRICIA'
					WHEN 24 THEN 'CONSULTA EXTERNA GINOCO OBSTETRICIA'
					WHEN 30 THEN 'OTRAS'
					WHEN 31 THEN 'CONSULTA PRIORITARIA' END AS [AMBITO DE INGRESO],
UNF.Code+'-'+UNF.Name [UNIDAD FUNCIONAL DE INGRESO],
CASE WHEN UNI.UFUCODIGO IS NULL AND UNF.Code='1112010' THEN '1112010 -CONSULTA EXTERNA C.E' ELSE UNI.UFUCODIGO+'-'+UNI.UFUDESCRI END AS [UNIDAD FUNCIONAL DE EGRESO],
 CASE WHEN UNI.UFUTIPUNI=1 THEN 'URGENCIAS' 
	  WHEN UNI.UFUTIPUNI=2 THEN 'HOSPITALIZACION'
	  WHEN UNI.UFUTIPUNI=3 THEN 'APOYO DIAGNOSTICO'
	  WHEN UNI.UFUTIPUNI=4 THEN 'APOYO TERAPEUTICO'
	  WHEN UNI.UFUTIPUNI=5 THEN 'UCI'
	  WHEN UNI.UFUTIPUNI=6 THEN 'UCI'
	  WHEN UNI.UFUTIPUNI=7 THEN 'UCI'
	  WHEN UNI.UFUTIPUNI=8 THEN 'UCI'
	  WHEN UNI.UFUTIPUNI=9 THEN 'UCI'
	  WHEN UNI.UFUTIPUNI=10 THEN 'UCI'
	  WHEN UNI.UFUTIPUNI=11 THEN 'UCI'
	  WHEN UNI.UFUTIPUNI=12 THEN 'UNIDAD RENAL'
	  WHEN UNI.UFUTIPUNI=13 THEN 'UNIDAD ONCOLOGICA' 
	  WHEN UNI.UFUTIPUNI=14 THEN 'UNIDAD MEDICINA NUCLEAR'
	  WHEN UNI.UFUTIPUNI=15 THEN 'AMBULATORIO'
	  WHEN UNI.UFUTIPUNI=16 THEN 'UNIDAD MENTAL'
	  WHEN UNI.UFUTIPUNI=17 THEN 'UNIDAD DE QUEMADOS'
	  WHEN UNI.UFUTIPUNI=18 THEN 'UNIDAD DE CUIDADOS PALATIVOS'
	  WHEN UNI.UFUTIPUNI=19 THEN 'CIRUGIA'
	  WHEN UNI.UFUTIPUNI=20 THEN 'LABORATORIOS'
	  WHEN UNI.UFUTIPUNI=21 THEN 'CARDIOLOGIA NO INVASIVA'
	  WHEN UNI.UFUTIPUNI=22 THEN 'CARDIOLOGIA INVASIVA'
	  WHEN UNI.UFUTIPUNI=23 THEN 'GINECO OBSTETRICIA'
	  WHEN UNI.UFUTIPUNI=24 THEN 'CONSULTA EXTERNA GINOCO OBSTETRICIA'
	  WHEN UNI.UFUTIPUNI=30 THEN 'OTRAS'
	  WHEN UNI.UFUTIPUNI=31 THEN 'CONSULTA PRIORITARIA' 
	  WHEN UNI.UFUCODIGO IS NULL AND UNF.Code='1112010' THEN 'AMBULATORIO' END  as [AMBITO DE EGRESO]
from 
DBO.ADINGRESO ING INNER JOIN
Payroll.FunctionalUnit UNF ON ING.UFUCODIGO=UNF.Code LEFT JOIN
dbo.HCREGEGRE EGR ON ING.NUMINGRES=EGR.NUMINGRES LEFT JOIN
dbo.INUNIFUNC UNI ON EGR.UFUCODIGO = UNI.UFUCODIGO
)

 SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
 CAM.[CENTRO DE ATENCION],
 RCD.[NIT ENTIDAD], 
 SUBSTRING(RTRIM(RCD.ENTIDAD),1,50) 'ENTIDAD',
 RCD.[CODIGO GRUPO],
 SUBSTRING(RTRIM(RCD.[GRUPO DE ATENCION]),1,50) 'GRUPO DE ATENCION',
 SUBSTRING(RTRIM(RCD.INGRESO),1,30) 'INGRESO',
 RCD.[FECHA INGRESO],
 RCD.FECHEGRESO AS [FECHA EGRESO],
 RCD.[TIPO IDENTIFICACION],
 RCD.IDENTIFICACION,
 RCD.PACIENTE ,
 RCD.[FECHA NACIMIENTO],
 RCD.[EDAD],
 RCD.[REGIMEN],
 RCD.[TIPO PACIENTE],
 'SIN FACTURA'[ESTADO FACTURA],''[NRO FACTURA] , '1900-01-01' [FECHA FACTURA] ,'0' [TOTAL FACTURA],'0' [VR TOTAL DESCUENTO] ,
 '0' [VR TOTAL ENTIDAD] ,
 '0' [VR TOTAL CUOTA RECUPERACION], 
 SOD.ServiceDate as 'CAB_ORD_FECHA_ORDEN',
 SOD.Id 'CAB_ORD_NRO',
 CONVERT(DATE,SOD.ServiceDate)AS 'FECHA DE SERVICIO',
 CONVERT(TIME,SOD.ServiceDate)AS 'HORA DE SERVICIO',
 CASE SOD.RecordType WHEN 1 THEN 'Servicios' WHEN 2 THEN 'Medicamentos' ELSE 'N/A' END 'DET_ORD_TIPO',
 ISNULL(GF.Name, GF2.Name) AS 'DET_ORD_GRUPO FACTURACION',ISNULL(CGR.Code + '-' + CGR.Name, PG.Code + '-' + PG.NAME) 'DET_ORD_GRUPO',  
 ISNULL(CSG.CODE + '-' + CSG.Name, PSG.Code + '-' + PSG.NAME) 'DET_ORD_SUBGRUPO',CASE SOD.Presentation WHEN 1 THEN 'No quirurgico' WHEN 2 THEN 'Quirurgico'
  WHEN 3 THEN 'Paquete' ELSE 'Producto' END 'DET_ORD_PRESENTACION', 
 ISNULL(CUPS.Code,'N/A') 'DET_ORD_CUPS',SUBSTRING(ISNULL(RTRIM(CUPS.Description),'N/A'),1,60) 'DET_ORD_DESCRIPCION_CUPS',
 ISNULL(RTRIM(IPS.Code),RTRIM(IPR.CODE)) 'DET_ORD_CODIGO_SERVICIO/PRODUCTO',SUBSTRING(ISNULL(RTRIM(IPS.Name),RTRIM(IPR.NAME)),1,60) 'DET_ORD_DESCRIPCION_SERVICIO/PRODUCTO',
 ISNULL(SERVICIOSIPSQ.Code, 'N/A') AS 'DET_ORDQX_SUBCODIGO', SUBSTRING(ISNULL(RTRIM(SERVICIOSIPSQ.Name), 'N/A'),1,60) AS 'DET_ORDQX_SUBNOMBRE','NO' 'DET VISUALIZA FACTURA',
 CASE SOD.IsPackage WHEN 1 THEN 'PAQUETE (INCLUYE OTROS SERVICIOS)' WHEN 0 THEN 'NO ES PAQUETE' ELSE 'N/A' END 'DET_ORD_PAQUETE',  
 CASE SOD.Packaging WHEN 1 THEN 'INCLUIDO EN PAQUETE' WHEN 0 THEN 'NO INCLUIDO EN PAQUETE' ELSE 'N/A' END 'DET_ORD_INCLUIDO_EN_PAQUETE',
 ISNULL(CUPS_PAQ.Code,'N/A') 'DET_ORD_CUPS_PAQUETE',ISNULL(RTRIM(CUPS_PAQ.Description),'N/A') 'DET_ORD_DESCRIPCION_CUPS_PAQUETE',
 CASE sod.SettlementType WHEN 1 THEN 'Por manual de tarifas' WHEN 2 THEN '% de otro servicio cargado' WHEN 3 THEN '100% incluido dentro de otro servicio (No se cobra nada)'
 WHEN 4 THEN '% del mismo servicio' ELSE 'N/A' END 'DET_ORD_TIPO_LIQUIDACION',
 ISNULL(CUPS_INC.Code,'N/A') 'DET_ORD_CUPS_INCLUIDO',SUBSTRING(ISNULL(RTRIM(CUPS_INC.Description),'N/A'),1,60) 'DET_ORD_DESCRIPCION_CUPS_INCLUIDO',
 FU.Code 'DET_ORD_CODIGO_UNIDAD_FUNCIONAL' ,RTRIM(FU.Name) 'DET_ORD_UNIDAD_FUNCIONAL_PRESTO_SERVICIO' ,COST.Code 'DET_ORD_COD_CENTRO_COSTO',  RTRIM(COST.Name) 'DET_ORD_CENTRO_COSTO',
 RTRIM(ISNULL(DQ.PERFORMSHEALTHPROFESSIONALCODE,ISNULL(MED.CODPROSAL,'000'))) 'DET_ORD_IDENTIFICACION_PROFESIONAL' ,RTRIM(ISNULL(MEDQX.NOMMEDICO,ISNULL(MED.NOMMEDICO,'N/A'))) 'DET_ORD_PROFESIONAL',
 RTRIM(ISNULL(ESPQX.DESESPECI,ISNULL(ESPMED.DESESPECI,'N/A'))) 'DET_ORD_ESPECIALIDAD',
 ISNULL(DQ.InvoicedQuantity,SOD.InvoicedQuantity) 'DET_ORD_CANTIDAD' ,ISNULL(DQ.RateManualSalePrice,SOD.RateManualSalePrice) 'DET_ORD_VALOR_SERVICIO' ,ISNULL(DQ.TOTALSALESPRICE,sod.TotalSalesPrice) 'DET_ORD_VALOR_UNITARIO' ,
 ISNULL(DQ.TOTALSALESPRICE,SOD.GrandTotalSalesPrice) 'DET_ORD_VALOR_TOTAL',
 CASE ISNULL(IPS.POS,IPR.POSProduct ) WHEN 1 THEN 'PBS' ELSE 'NO PBS' END 'DET_ORD_TECNOLOGIA_PBS',
 IIF(RCD.[TIPO AMBITO] ='HOSPITALARIO',
 ALT.[FECHA ALTA],ISNULL(AMB.[FECHA ALTA],RCD.[FECHA INGRESO])) 'FECHA ALTA MEDICA','' AS 'FECHA ANULACION', CAM.CAMA  'CAMA ACTIVA' ,
 SOD.ID 'ID_ORDE_DETALLE','' 'ID_FACTURA',RCD.HealthAdministratorId 'ID ENTIDAD','' 'TIPO REGISTRO' , CAST(SOD.ServiceDate AS DATE) 'FECHA SERVICIO BUSQUEDA',
 'NO' 'FACTURADO', 
 SOD.AuthorizationNumber 'AUTORIZACION SERVICIO',RCD.AUTORIZACION ,
 RCD.[DIAGNOSTICO],
 RCD.[DESC_DIAGNOSTICO],
 AMBI.[NUMINGRES],
 AMBI.[AMBITO DE INGRESO],
 AMBI.[UNIDAD FUNCIONAL DE INGRESO],
 ISNULL(AMBI.[UNIDAD FUNCIONAL DE EGRESO],RCD.[UNIDAD FUNCIONAL DE EGRESO]) AS [UNIDAD FUNCIONAL DE EGRESO],
 AMBI.[AMBITO DE EGRESO],
 CAM.[UBICACION_LATITUD],
 CAM.[UBICACION_LONGITUD],
 SO.CreationUser AS [USUARIO CREA],
 SUC.NOMUSUARI AS [NOM_USUARIO_CREA],
 SO.CreationDate AS [FECHA CREACION],
 CAT.ModificationUser AS [USUARIO MODIFICA],
 SU.NOMUSUARI AS [NOM_USUARIO_MODIFICA],
 CAT.ModificationDate AS [FECHA MODIFICA],
 CAST(SOD.ServiceDate AS date) AS 'FECHA BUSQUEDA',
 YEAR(SOD.ServiceDate) AS 'AÑO BUSQUEDA',
 MONTH(SOD.ServiceDate) AS 'MES BUSQUEDA',
 CONCAT(FORMAT(MONTH(SOD.ServiceDate), '00') ,' - ', 
	   CASE MONTH(SOD.ServiceDate) 
	    WHEN 1 THEN 'ENERO'
   	    WHEN 2 THEN 'FEBRERO'
	    WHEN 3 THEN 'MARZO'
	    WHEN 4 THEN 'ABRIL'
	    WHEN 5 THEN 'MAYO'
	    WHEN 6 THEN 'JUNIO'
	    WHEN 7 THEN 'JULIO'
	    WHEN 8 THEN 'AGOSTO'
	    WHEN 9 THEN 'SEPTIEMBRE'
	    WHEN 10 THEN 'OCTUBRE'
	    WHEN 11 THEN 'NOVIEMBRE'
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
 FROM CTE_SERVICIOS_PENDIENTES  AS RCD
 INNER JOIN BILLING.SERVICEORDERDETAILDISTRIBUTION AS SODD WITH (NOLOCK) ON RCD.ID = SODD.REVENUECONTROLDETAILID AND RCD.STATUS IN ('1', '3')
 INNER JOIN BILLING.SERVICEORDERDETAIL AS SOD WITH (NOLOCK) ON SODD.SERVICEORDERDETAILID = SOD.ID
 INNER JOIN Billing.ServiceOrder AS SO  ON SO.Id =SOD.ServiceOrderId
 
 INNER JOIN Payroll.FunctionalUnit AS FU WITH (NOLOCK) ON FU.Id = SOD.PerformsFunctionalUnitId
 LEFT JOIN Billing .ConceptsCausesStatusFolio AS CCSF WITH (NOLOCK) ON CCSF.Id =RCD.StatusFolioId 
 LEFT JOIN BILLING.INVOICECATEGORIES AS CAT WITH (NOLOCK) ON CAT.ID = RCD.INVOICECATEGORYID
  LEFT JOIN DBO.SEGusuaru SU  ON SU.CODUSUARI = CAT.ModificationUser
  LEFT JOIN DBO.SEGusuaru SUC  ON SUC.CODUSUARI = CAT.CreationUser
  INNER JOIN Billing.ServiceOrder AS SUMO  ON SUMO.ModificationUser =SU.CODUSUARI 
 LEFT JOIN CONTRACT.CUPSENTITY AS CUPS WITH (NOLOCK) ON CUPS.ID = SOD.CUPSENTITYID
 LEFT JOIN Contract.CupsSubgroup AS CSG WITH (NOLOCK) ON CUPS.CUPSSubGroupId=CSG.ID
 LEFT JOIN Contract.CupsGroup AS CGR WITH (NOLOCK) ON CGR.ID=CSG.CupsGroupId
 LEFT JOIN Contract.IPSService AS IPS WITH (NOLOCK) ON IPS.Id = SOD.IPSServiceId
 LEFT JOIN Billing .ServiceOrderDetail AS SOD_PAQ WITH (NOLOCK) ON SOD_PAQ.Id =SOD.PackageServiceOrderDetailId 
 LEFT JOIN Contract.CUPSEntity AS CUPS_PAQ WITH (NOLOCK) ON CUPS_PAQ.Id = SOD_PAQ.CUPSEntityId
 LEFT JOIN INVENTORY.INVENTORYPRODUCT AS IPR WITH (NOLOCK) ON IPR.ID = SOD.PRODUCTID AND IPR.Status = 1
 LEFT JOIN Inventory.ProductType AS PT WITH (NOLOCK) ON IPR.ProductTypeId = PT.Id
 LEFT JOIN Inventory.ProductGroup AS PG WITH (NOLOCK) ON PG.ID =IPR.ProductGroupId
 LEFT JOIN Inventory.ProductSubGroup AS PSG WITH (NOLOCK) ON PSG.ID =IPR.ProductSubGroupId
 LEFT JOIN dbo.INPROFSAL AS MED WITH (NOLOCK) ON MED.CODPROSAL = SOD.PerformsHealthProfessionalCode
 LEFT JOIN dbo.INESPECIA AS ESPMED WITH (NOLOCK) ON ESPMED.CODESPECI = MED.CODESPEC1
 LEFT JOIN Payroll.CostCenter AS COST WITH (NOLOCK) ON COST.Id =SOD.CostCenterId
 LEFT JOIN Billing .ServiceOrderDetail AS SOD_INC WITH (NOLOCK) ON SOD_INC.Id =SOD.IncludeServiceOrderDetailId
 LEFT JOIN Contract.CUPSEntity AS CUPS_INC WITH (NOLOCK) ON CUPS_INC.Id = SOD_INC.CUPSEntityId
 LEFT JOIN PAYROLL.FUNCTIONALUNIT AS UF WITH (NOLOCK) ON UF.ID = SOD.PERFORMSFUNCTIONALUNITID
 LEFT JOIN Billing.BillingGroup AS GF WITH (NOLOCK) ON GF.Id = CUPS.BillingGroupId
 LEFT JOIN Billing.BillingGroup AS GF2 WITH (NOLOCK) ON GF2.Id = IPR.BillingGroupId
 LEFT JOIN Billing.ServiceOrderDetailSurgical AS DQ WITH (NOLOCK) ON DQ.ServiceOrderDetailId = SOD.Id AND DQ.OnlyMedicalFees = '0'
 LEFT JOIN dbo.INPROFSAL AS MEDQX WITH (NOLOCK) ON MEDQX.CODPROSAL = DQ.PerformsHealthProfessionalCode
 LEFT JOIN dbo.INESPECIA AS ESPQX WITH (NOLOCK) ON ESPQX.CODESPECI = MEDQX.CODESPEC1
 LEFT JOIN Contract.IPSService AS SERVICIOSIPSQ WITH (NOLOCK) ON SERVICIOSIPSQ.Id = DQ.IPSServiceId
 LEFT JOIN CTE_ALTA_MEDICA AS ALT WITH (NOLOCK) ON ALT.NUMINGRES =RCD.INGRESO 
 LEFT JOIN CTE_CAMAS AS CAM WITH (NOLOCK) ON CAM.INGRESO =RCD.INGRESO 
 LEFT JOIN CTE_HISTORIAS_AMBULATORIAS AS AMB WITH (NOLOCK) ON AMB.NUMINGRES =RCD.INGRESO 
 LEFT JOIN CTE_AMBITO AMBI ON RCD.INGRESO=AMBI.NUMINGRES
 WHERE 
  SOD.IsDelete ='0'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a la gestión de cartera pendiente por facturar bajo normas NIIF. Consolida, por ingreso activo (excluyendo estados anulado, facturado y cancelado), información demográfica del paciente, entidad aseguradora, grupo de atención, detalle de cada servicio u orden (tipo, CUPS, profesional, cantidad, valores, paquetes), ámbito y unidades funcionales de ingreso/egreso, cama activa, fecha de alta médica y trazabilidad de usuario creador/modificador, permitiendo a tesorería y facturación identificar servicios prestados aún no facturados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosPendientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosPendientes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único conjunto los servicios prestados a pacientes que aún no han sido facturados, enriqueciéndolos con datos clínicos, administrativos, de ubicación y de ámbito de atención para reportes NIIF.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosPendientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los ingresos considerados deben tener IESTADOIN distinto de ''A'', ''F'' y ''C'' en DBO.ADINGRESO; Los detalles de orden de servicio deben tener IsDelete=''0'' en BILLING.SERVICEORDERDETAIL; El detalle de control de ingresos (RevenueControlDetail) debe estar en estado ''1'' o ''3'' para vincularse a la distribución de la orden; Para considerarse alta médica ambulatoria, HCHISPACA.GENCONEXT debe ser 1; Para camas activas en CHREGESTA se requiere REGESTADO = 1; Para detalle quirúrgico (ServiceOrderDetailSurgical) sólo se toma cuando OnlyMedicalFees = ''0''; Sólo se incluyen productos de inventario con Status = 1', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosPendientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los importes de factura ([TOTAL FACTURA], [VR TOTAL DESCUENTO], [VR TOTAL ENTIDAD], [VR TOTAL CUOTA RECUPERACION]) siempre se devuelven en 0 y la fecha de factura como ''1900-01-01'' porque la vista representa servicios aún no facturados; El campo ID_COMPANY se obtiene siempre de DB_NAME() truncado a 9 caracteres; ULT_ACTUAL siempre se calcula con GETDATE() convertido a ''Pakistan Standard Time''; El tipo de identificación se traduce de IPTIPODOC a su descripción textual (CC, CE, TI, RC, pasaporte, etc.) y devuelve ''N/A'' fuera del rango 1-12; El régimen del paciente (IPTIPOPAC) se traduce a Contributivo, Subsidiado, Vinculado, Particular, Otro o variantes de Desplazado; La edad se calcula como floor de la diferencia entre fecha de ingreso y fecha de nacimiento en formato AAAAMMDD dividido por 10000; Se aplica WITH (NOLOCK) en prácticamente todas las lecturas, asumiendo lecturas no consistentes pero no bloqueantes; Los campos de texto largo se truncan: ENTIDAD/GRUPO ATENCION a 50, INGRESO a 30, descripciones a 60 caracteres', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosPendientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewNIIFServiciosPendientes: Devuelve cada detalle de orden de servicio con estado ''SIN FACTURA'', valores monetarios facturados en 0 y campos de factura vacíos, marcando ''FACTURADO''=''NO'' y ''[ESTADO FACTURA]''=''SIN FACTURA''; [RETURN_RESULT] Report.ViewNIIFServiciosPendientes: Cuando RCD.[TIPO AMBITO]=''HOSPITALARIO'' la FECHA ALTA MEDICA toma el MAX(FECALTPAC) de HCREGEGRE; en caso contrario toma el MAX(FECHISPAC) de HCHISPACA (sólo registros con GENCONEXT=1) y si no existe usa la FECHA INGRESO', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosPendientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.IESTADOIN NOT IN (''A'',''F'',''C'') → El ingreso se incluye en el conjunto de servicios pendientes de facturar else Se excluye del reporte; si RCD.ContractEntityId IS NULL → Se reporta PatientCode como ''ENTIDAD'' y NULL como ''CODIGO ENTIDAD'' (paciente particular sin entidad responsable) else Se reportan código y nombre de la HealthAdministrator; si RCD.STATUS IN (''1'',''3'') → Se cruza el RevenueControlDetail con su distribución de orden de servicio para incluir los servicios else El detalle se descarta; si RCD.[TIPO AMBITO]=''HOSPITALARIO'' → FECHA ALTA MEDICA = alta del egreso hospitalario (HCREGEGRE) else FECHA ALTA MEDICA = última historia ambulatoria (HCHISPACA) o, en su defecto, la fecha de ingreso; si SOD.RecordType = 1 / 2 / otro → Tipo de orden se clasifica como ''Servicios'', ''Medicamentos'' o ''N/A'' respectivamente; si SOD.SettlementType IN (1..4) → Se etiqueta el tipo de liquidación: por manual de tarifas, % de otro servicio, 100% incluido en otro servicio o % del mismo servicio; si UNI.UFUCODIGO IS NULL AND UNF.Code=''1112010'' → Se asume ámbito y unidad funcional de egreso ''AMBULATORIO / 1112010 -CONSULTA EXTERNA C.E''; si ISNULL(IPS.POS, IPR.POSProduct)=1 → El servicio/producto se clasifica como ''PBS'' else Se clasifica como ''NO PBS''; si SOD.IsPackage=1 / 0 → Se marca como ''PAQUETE (INCLUYE OTROS SERVICIOS)'' o ''NO ES PAQUETE''; si SOD.Packaging=1 / 0 → Se marca como ''INCLUIDO EN PAQUETE'' o ''NO INCLUIDO EN PAQUETE''; si Existe ServiceOrderDetailSurgical (DQ) para el detalle → Se prefieren cantidad, precios, profesional y especialidad del registro quirúrgico sobre los del detalle no quirúrgico', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosPendientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFServiciosPendientes';
GO
