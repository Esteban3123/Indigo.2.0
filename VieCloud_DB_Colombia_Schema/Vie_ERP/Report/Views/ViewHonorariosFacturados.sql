/*******************************************************************************************************************
Nombre: [Report].[ViewHonorariosFacturados]
Tipo:Vista
Observacion:Honorarios medicos facturados 
Profesional:
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico:Nilsson Miguel Galiundo Lopez
Fecha:14-08-2023
Observaciones:ticket 11524
--------------------------------------
Version 3
Persona que modifico:Andres Felipe Gonzales
Fecha:04-03-2024
Observaciones:Se agregan nuevos campos [CODIGO DESCRIPCIÓN RELACIONADA], [DESCRIPCIÓN RELACIONADA], PROFESIONAL TOMA EXAMEN
*********************************************************************************************************************/

CREATE view [Report].[ViewHonorariosFacturados] as

WITH
CTE_LECTURA AS
(
SELECT 
A.GENSERVICEORDER,A.MEDREALEC AS [CODIGO PROFESIONAL LECTURA],PRO.NOMMEDICO AS [PROFESIONAL LECTURA],A.FECHLECT AS [FECHA LECTURA], PRO1.CODPROSAL AS [CODIGO PROFESIONAL TOMA EXAMEN], PRO1.NOMMEDICO AS [PROFESIONAL TOMA EXAMEN]
,A.IPCODPACI,A.AUTO,A.NUMINGRES
FROM 
dbo.AMBORDIMA A INNER JOIN
DBO.INPROFSAL PRO ON A.MEDREALEC=PRO.CODPROSAL AND A.AUTO=(SELECT MAX(B.AUTO) FROM dbo.AMBORDIMA B WHERE A.NUMINGRES=B.NUMINGRES)
INNER JOIN DBO.INPROFSAL PRO1 ON PRO1.CODUSUARI = A.USURECEXA
),
CTE_ANESTESIA AS
(
SELECT 
A.ServiceOrderDetailId,PRO.CODPROSAL,PRO.NOMMEDICO,A.OnlyMedicalFees,RateManualSalePrice,TotalSalesPrice
FROM 
Billing.ServiceOrderDetailSurgical A INNER JOIN
dbo.INPROFSAL PRO ON A.PerformsHealthProfessionalCode=PRO.CODPROSAL INNER JOIN
dbo.INESPECIA ES ON PRO.CODESPEC1=ES.CODESPECI AND ES.DESESPECI LIKE '%ANESTESIOLOGIA%'
)

SELECT        
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 L.Code AS Codigo_Liquidacion, 
 C.Id AS IDCausacion, 
 LD.Id AS IDLiquidacion, 
 LD.MedicalFeesCausationId,
 CG.Name AS Grupo_de_Atención,
 E.Name AS Entidad_Administradora, 
 E1.Name AS Tercero, 
 F.InvoiceNumber AS Factura, 
 F.InvoiceDate AS Fecha_Factura, 
 F.AdmissionNumber AS Ingreso, 
 P.IPCODPACI AS Identificacion_Usuario, 
 P.IPNOMCOMP AS Nombre_Usuario, 
 CU.Code AS CUPS, 
 CU.Description AS Descripcion_CUPS,
 CD.Code AS [CODIGO DESCRIPCIÓN RELACIONADA],
 CD.Name AS [DESCRIPCIÓN RELACIONADA],
 S.Code AS CodigoServicio, 
 S.Name AS NombreServicio, 
 FD.InvoicedQuantity AS Cantidad_Cobrada, 
 FD.GrandTotalSalesPrice AS Valor_Facturado_Entidad, 
 C.TotalAmountPayable AS Valor_Pagar_Tercero, 
 OD.PerformsHealthProfessionalCode AS Nit_Profesional, 
 T2.Name AS Profesional, 
 RTRIM(ANE.CODPROSAL)+' - '+ANE.NOMMEDICO AS ANESTESIOLOGO,ANE.TotalSalesPrice AS [ANESTESIA],
 T.Name AS Tercero_Causa, 
 CASE C.Status WHEN '1' THEN 'Causado' 
			   WHEN '2' THEN 'Liquidado' 
			   WHEN '3' THEN 'Confirmado' 
			   WHEN '4' THEN 'Anulado' END AS Estado_Causacion, 
CASE L.Status WHEN '1' THEN 'Registrado' 
			  WHEN '2' THEN 'Confirmado' 
			  WHEN '3' THEN 'Anulado' 
 END AS Estado_Liquidacion, 
 L.ConfirmationDate AS Fecha_Confirmacion_Liquidacion,
 LEC.[CODIGO PROFESIONAL LECTURA]+' - '+LEC.[PROFESIONAL LECTURA] AS [PROFESIONAL LECTURA],
 LEC.[FECHA LECTURA],
 LEC.[CODIGO PROFESIONAL TOMA EXAMEN]+' - '+LEC.[PROFESIONAL TOMA EXAMEN] AS [PROFESIONAL TOMA EXAMEN],
 D.CODICIE10 AS Diadnostico_CIE10, 
 D.NOMDIAGNO AS Diagnostico_Egreso, 
 CC.Name AS CentroCosto,
 1 as 'CANTIDAD',
  CAST(F.InvoiceDate AS date) AS 'FECHA BUSQUEDA',
  YEAR(F.InvoiceDate) AS 'AÑO BUSQUEDA',
  MONTH(F.InvoiceDate) AS 'MES BUSQUEDA',
  CONCAT(FORMAT(MONTH(F.InvoiceDate), '00') ,' - ', 
	   CASE MONTH(F.InvoiceDate) 
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
FROM           
 Billing.InvoiceDetail AS FD 
 INNER JOIN Billing.Invoice AS F ON F.Id = FD.InvoiceId 
 LEFT OUTER JOIN MedicalFees.MedicalFeesCausation AS C ON C.InvoiceDetailId = FD.Id 
 LEFT OUTER JOIN MedicalFees.MedicalFeesLiquidationDetail AS LD ON LD.MedicalFeesCausationId = C.Id 
 LEFT OUTER JOIN Common.ThirdParty AS T ON T.Id = C.ThirdPartyId 
 LEFT OUTER JOIN Billing.ServiceOrderDetail AS OD ON OD.Id = FD.ServiceOrderDetailId 
 LEFT OUTER JOIN Contract.CareGroup AS CG ON CG.Id = OD.CareGroupId
 LEFT OUTER JOIN Contract.CUPSEntity AS CU ON CU.Id = OD.CUPSEntityId
 LEFT OUTER JOIN Contract.CUPSEntityContractDescriptions CECD ON CECD.Id = OD.CUPSEntityContractDescriptionId
 LEFT OUTER JOIN Contract.ContractDescriptions CD ON CD.Id = CECD.ContractDescriptionId
 LEFT OUTER JOIN MedicalFees.MedicalFeesLiquidation AS L ON L.Id = LD.MedicalFeesLiquidacionId 
 LEFT OUTER JOIN Contract.HealthAdministrator AS E ON E.Id = F.HealthAdministratorId 
 LEFT OUTER JOIN Common.ThirdParty AS E1 ON E1.Id = F.ThirdPartyId 
 LEFT OUTER JOIN Contract.IPSService AS S ON S.Id = OD.IPSServiceId 
 LEFT OUTER JOIN dbo.INPACIENT AS P ON P.IPCODPACI = F.PatientCode 
 LEFT OUTER JOIN dbo.INDIAGNOS AS D ON D.CODICIE10 = F.OutputDiagnosis 
 LEFT OUTER JOIN Payroll.CostCenter AS CC ON CC.Id = OD.CostCenterId 
 LEFT OUTER JOIN Common.ThirdParty AS T2 ON T2.Nit = OD.PerformsHealthProfessionalCode LEFT JOIN
 CTE_LECTURA LEC ON OD.ServiceOrderId=LEC.GENSERVICEORDER LEFT JOIN
 CTE_ANESTESIA ANE ON OD.ServiceOrderId=ANE.ServiceOrderDetailId
 --WHERE        
  --OD.CUPSEntityId IS NOT NULL AND F.InvoiceDate BETWEEN '12/01/2023 00:00.000' AND '01/26/2024 23:59:59' --and p.IPCODPACI='1110306770'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida los honorarios médicos facturados, cruzando el detalle de facturas de venta con la causación y liquidación de honorarios, datos del paciente, entidad administradora (EPS), servicio CUPS, profesional ejecutante, anestesiólogo y profesional de lectura/toma de exámenes diagnósticos. Permite analizar el estado de causación y liquidación de honorarios por período, incluyendo dimensiones de fecha (año, mes), centro de costo y diagnóstico de egreso CIE-10, orientada a reportes financieros y de auditoría de pagos a profesionales.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola vista los honorarios médicos facturados, cruzando facturas, causaciones y liquidaciones con datos del paciente, profesional ejecutor, anestesiólogo, profesional de lectura/toma de examen, CUPS, contrato y diagnóstico de egreso.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas (Billing.Invoice) y sus detalles (Billing.InvoiceDetail) deben existir como base obligatoria del cruce (INNER JOIN).; Para enriquecer con causación/liquidación de honorarios, deben existir registros relacionados en MedicalFees.MedicalFeesCausation y MedicalFees.MedicalFeesLiquidationDetail/Liquidation (LEFT JOIN, no obligatorios).; Para que aparezca el profesional de lectura, en dbo.AMBORDIMA debe existir un registro cuyo AUTO sea el máximo por NUMINGRES y cuyo USURECEXA coincida con un CODUSUARI en INPROFSAL (toma de examen).; Para que aparezca el anestesiólogo, el profesional de Billing.ServiceOrderDetailSurgical debe tener especialidad cuya descripción contenga ''ANESTESIOLOGIA'' en INESPECIA.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY se obtiene de DB_NAME() truncado a VARCHAR(9), garantizando trazabilidad de la base origen.; Solo se considera la última orden de imágenes diagnósticas (MAX(AUTO) por NUMINGRES) para el cruce de profesional de lectura y de toma de examen.; El anestesiólogo reportado siempre pertenece a la especialidad cuya descripción incluye ''ANESTESIOLOGIA''.; El cruce de Tercero del profesional ejecutor se hace por NIT (Common.ThirdParty.Nit = OD.PerformsHealthProfessionalCode).; ULT_ACTUAL siempre se entrega convertida a la zona horaria ''Pakistan Standard Time''.; CANTIDAD se entrega siempre fija en 1 por línea de detalle de factura.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Honorarios médicos facturados; Causación de honorarios; Liquidación de honorarios; Factura de venta; Paciente / Identificación de usuario; CUPS / Servicio IPS; Profesional de lectura de examen; Profesional que toma el examen; Anestesiólogo / Anestesia; Diagnóstico de egreso (CIE-10); Entidad administradora de salud (EPS); Tercero pagador; Centro de costo; Grupo de atención; Descripción de contrato', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewHonorariosFacturados: Devuelve un set tabular con honorarios facturados; agrega columna fija CANTIDAD=1, fecha de actualización con conversión a zona horaria ''Pakistan Standard Time'' y desglose AÑO/MES/MES NOMBRE a partir de InvoiceDate.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.Status = ''1''/''2''/''3''/''4'' → Estado_Causacion se traduce a ''Causado''/''Liquidado''/''Confirmado''/''Anulado'' respectivamente else NULL; si L.Status = ''1''/''2''/''3'' → Estado_Liquidacion se traduce a ''Registrado''/''Confirmado''/''Anulado'' else NULL; si MONTH(F.InvoiceDate) entre 1 y 12 → Se concatena el número de mes con su nombre en español (ENERO..DICIEMBRE) en ''MES NOMBRE BUSQUEDA''; si En CTE_LECTURA: A.AUTO = (SELECT MAX(B.AUTO) FROM AMBORDIMA B WHERE A.NUMINGRES=B.NUMINGRES) → Solo se toma la última orden de imagen del ingreso para asociar el profesional de lectura y de toma de examen; si En CTE_ANESTESIA: ES.DESESPECI LIKE ''%ANESTESIOLOGIA%'' → Solo se incluyen profesionales cuya especialidad contiene ''ANESTESIOLOGIA'' como anestesiólogos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDIMA; dbo.INPROFSAL; dbo.INESPECIA; Billing.ServiceOrderDetailSurgical; Billing.InvoiceDetail; Billing.Invoice; Billing.ServiceOrderDetail; MedicalFees.MedicalFeesCausation; MedicalFees.MedicalFeesLiquidationDetail; MedicalFees.MedicalFeesLiquidation; Common.ThirdParty; Contract.CareGroup; Contract.CUPSEntity; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Contract.HealthAdministrator; Contract.IPSService; dbo.INPACIENT; dbo.INDIAGNOS; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewHonorariosFacturados';
GO
