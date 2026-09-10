
CREATE VIEW [Report].[ViewNIIFTotalFacturado] AS

WITH CTE_FACTURACION_TOTAL AS
 (
SELECT 
ING.CODCENATE AS [COD_CENTRO_ATENCION],
CEN.NOMCENATE AS [CENTRO_ATENCION],
F.Id ID_FACTURA ,
CASE F.DocumentType WHEN '1' THEN 'FACTURA EAPB CON CONTRATO'  
					WHEN '2' THEN 'FACTURA EAPB SIN CONTRATO' 
					WHEN '3' THEN 'FACTURA PARTICULAR'
					WHEN '4' THEN 'FACTURA CAPITA' 
					WHEN '5' THEN 'CONTROL CAPITACION' 
					WHEN '6' THEN 'NO OPERACIONAL' 
					WHEN '7' THEN 'FACTURA VENTA PRODUCTOS' END 'TIPO REGISTRO',
CASE PAC.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA' 
				   WHEN '2' THEN 'CEDULA DE EXTRANJERIA' 
				   WHEN '3' THEN 'TARJETA DE IDENTIDAD' 
				   WHEN '4' THEN 'REGISTRO CIVIL' 
				   WHEN '5' THEN 'PASAPORTE' 
				   WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' 
				   WHEN '7' THEN 'MENOR SIN IDENTIFICACION' 
				   WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACION' 
				   WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' 
				   WHEN '10' THEN 'CARNET DIPLOMATICO'
				   WHEN '11' THEN 'SALVOCONDUCTO' 
				   WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' 
				   WHEN '13' THEN 'PERMISO POR PROTECCION TEMPORAL'
				   WHEN '14' THEN 'DOCUMENTO EXTRANJERO'
				   WHEN '15' THEN 'SIN IDENTIFICACION' ELSE 'N/A' END AS 'TIPO IDENTIFICACION',
ISNULL(RTRIM(F.PatientCode), 0) AS 'IDENTIFICACION',
CASE WHEN RTRIM(PAC.IPNOMCOMP) IS NULL AND F.DOCUMENTTYPE LIKE '4' THEN 'N/A' WHEN PAC.IPNOMCOMP IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'N/A'
ELSE RTRIM(PAC.IPNOMCOMP) END AS  'PACIENTE' ,ISNULL(F.AdmissionNumber,'000') 'INGRESO',CAST(ISNULL(ING.IFECHAING,f.InitialDate) AS DATE)'FECHA INGRESO',CAST(ISNULL(ING.FECHEGRESO,ISNULL(ING.IFECHAING,f.InitialDate)) AS DATE) 'FECHA EGRESO' ,
F.InvoiceNumber 'NRO FACTURA' ,IIF(F.DOCUMENTTYPE=6, cast(f.InitialDate AS date), CAST(F.InvoiceDate AS DATETIME)) AS 'FECHA FACTURA',F.InvoiceExpirationDate AS 'FECHA VENCIMIENTO',
IIF(F.DOCUMENTTYPE=6, f.InvoiceValue, F.TotalInvoice) AS 'TOTAL FACTURA',F.ValueTax AS 'VALOR IMPUESTO',F.ThirdPartySalesValue AS 'VR TOTAL ENTIDAD',F.ThirdPartyDiscountValue AS 'VR TOTAL DESCUENTO',
F.TotalPatientSalesPrice AS 'VR TOTAL CUOTA RECUPERACION',F.PatientDiscount AS 'DESCUENTO CUOTA RECUPERACION',F.PatientPaidValue AS 'VR PAGADO PACIENTE',
F.ThirdPartyAccountReceivableValue AS 'VR CXC',F.PatientAccountReceivableValue AS 'VR CXC GENERADA A PACIENTE',
CASE F.STATUS WHEN '1' THEN 'FACTURADO' WHEN '2' THEN 'ANULADO' END AS 'ESTADO FACTURA',
RTRIM(T.Nit)+'-'+T.DigitVerification AS 'NIT ENTIDAD',
RTRIM(ISNULL(HA.Code,T.NIT)) 'CODIGO ENTIDAD' ,   RTRIM(ISNULL(HA.Name,T.NAME)) 'ENTIDAD' ,RTRIM(ISNULL(CG.Name,'N/A')) 'GRUPO DE ATENCION',
CASE WHEN HA.EntityType LIKE '1' THEN 'EPS CONTRIBUTIVO' WHEN HA.EntityType LIKE '2' THEN 'EPS SUBSIDIADO' WHEN HA.EntityType LIKE '3' THEN 'ET VINCULADO MUNICIPIO'
WHEN HA.EntityType LIKE '4' THEN 'ET VINCULADOS DAPARTAMENTO' WHEN HA.EntityType LIKE '5' THEN 'ARL RIESGO LABORALES' WHEN HA.EntityType LIKE '6' THEN 'MP MEDICINA PREPAGADA'
WHEN HA.EntityType LIKE '7' THEN 'IPS PRIVADA' WHEN HA.EntityType LIKE '8' THEN 'IPS PUBLICA' WHEN HA.EntityType LIKE '9' THEN 'REGIMEN ESPECIAL'WHEN HA.EntityType LIKE '10' THEN 'ACCIDENTE DE TRANSITO'
WHEN HA.EntityType LIKE '11' THEN 'FOSYGA' WHEN HA.EntityType LIKE '12' THEN 'OTROS' WHEN HA.EntityType IS NULL AND F.DOCUMENTTYPE LIKE '4' THEN 'N/A'
WHEN HA.EntityType IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'N/A' END AS [TIPO REGIMEN],
CASE WHEN ING.TIPOINGRE=1 THEN 'AMBULATORIO'  WHEN ING.TIPOINGRE=2 THEN 'HOSPITALARIO' WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '1' THEN 'FACTURA EAPB CON CONTRATO'
WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '2' THEN 'FACTURA EAPB SIN CONTRATO'WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '3' THEN 'FACTURA PARTICULAR'
WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '4' THEN 'FACTURA CAPITA' WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '5' THEN 'CONTROL CAPITACION'
WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'NO OPERACIONAL' WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '7' THEN 'FACTURA VENTA PRODUCTOS' END AS 'TIPO AMBITO',
USU.NOMUSUARI 'USUARIO FACTURO',CAST(F.InvoicedDate AS DATE) 'FECHA REGISTRO',ISNULL(DIA.CODDIAGNO,ISNULL(DIAG.CODDIAGNO,'N/A')) 'CODIGO CIE10',SUBSTRING(ISNULL(RTRIM(DIA.NOMDIAGNO),ISNULL(RTRIM(DIAG.NOMDIAGNO),'N/A')),1,80) 'DIAGNOSTICO',
CASE WHEN ING.IAUTORIZA IS NULL AND F.DOCUMENTTYPE LIKE '4' THEN 'N/A' WHEN ING.IAUTORIZA IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'N/A' 	WHEN ING.IAUTORIZA LIKE ' ' THEN 'N/A'
ELSE ING.IAUTORIZA END AS 'AUTORIZACION',  ISNULL(IIF (F.DOCUMENTTYPE=6, LTRIM(FU2.CODE) + ' - ' + FU2.NAME, 
ISNULL(FU.CODE + ' - ' +FU.Name, LTRIM(ING.UFUCODIGO) + ' - ' + UF.NAME)), 'N/A') AS [UNIDAD FUNCIONAL EGRESO],IIF (F.DOCUMENTTYPE=6, FU2.NAME, C.Name) AS  'CATEGORIA FACTURA',F.AnnulmentDate 'FECHA ANULACION',
iif(F.DOCUMENTTYPE=6, cast(f.InitialDate AS date), CAST(F.InvoiceDate AS DATE)) AS 'FECHA BUSQUEDA', USU2.NOMUSUARI 'USUARIO ANULO'
FROM 
Billing.Invoice AS F
LEFT JOIN Contract.HealthAdministrator AS HA ON HA.Id =F.HealthAdministratorId 
LEFT JOIN Contract.CareGroup AS CG ON CG.Id =F.CareGroupId 
LEFT JOIN Common.ThirdParty AS T ON T.Id = F.ThirdPartyId
LEFT JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES =F.AdmissionNumber 
LEFT JOIN dbo.ADCENATEN AS CEN ON CEN.CODCENATE=ING.CODCENATE
LEFT JOIN DBO.INPACIENT AS PAC ON PAC.IPCODPACI =F.PatientCode 
LEFT JOIN DBO.SEGusuaru AS USU ON USU.CODUSUARI =F.InvoicedUser 
LEFT JOIN DBO.INDIAGNOS AS DIA ON DIA.CODDIAGNO =ISNULL(ING.CODDIAEGR ,ING.CODDIAING)
LEFT JOIN Payroll.FunctionalUnit AS UF ON UF.Code  = ING.UFUCODIGO
LEFT JOIN Payroll.FunctionalUnit AS FU ON ING.UFUEGRMED=FU.CODE
LEFT JOIN Billing.BasicBilling AS BB ON BB.InvoiceId =F.Id
LEFT JOIN Payroll.FunctionalUnit AS FU2 ON BB.FunctionalUnitId =FU2.Id
LEFT JOIN Billing.InvoiceCategories AS C ON C.Id = F.InvoiceCategoryId
LEFT JOIN dbo.INDIAGNOS AS DIAG ON DIAG.CODDIAGNO = F.OutputDiagnosis
LEFT JOIN dbo.SEGusuaru AS USU2 ON USU2.CODUSUARI = F.AnnulmentUser
),

CTE_FACTURACION_TOTAL_ANULADA AS
( 
SELECT 
ING.CODCENATE AS [COD_CENTRO_ATENCION],
CEN.NOMCENATE AS [CENTRO_ATENCION],
F.Id ID_FACTURA ,
CASE F.DocumentType WHEN '1' THEN 'FACTURA EAPB CON CONTRATO'  
					WHEN '2' THEN 'FACTURA EAPB SIN CONTRATO'  
					WHEN '3' THEN 'FACTURA PARTICULAR'
					WHEN '4' THEN 'FACTURA CAPITA' 
					WHEN '5' THEN 'CONTROL CAPITACION' 
					WHEN '6' THEN 'NO OPERACIONAL' 
					WHEN '7' THEN 'FACTURA VENTA PRODUCTOS' END 'TIPO REGISTRO',
CASE PAC.IPTIPODOC WHEN '1' THEN 'CEDULA DE CIUDADANIA' WHEN '2' THEN 'CEDULA DE EXTRANJERIA' 
				   WHEN '3' THEN 'TARJETA DE IDENTIDAD' 
				   WHEN '4' THEN 'REGISTRO CIVIL' 
				   WHEN '5' THEN 'PASAPORTE' 
				   WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' 
				   WHEN '7' THEN 'MENOR SIN IDENTIFICACION' 
				   WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACION' 
				   WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' 
				   WHEN '10' THEN 'CARNET DIPLOMATICO'
				   WHEN '11' THEN 'SALVOCONDUCTO' 
				   WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA' 
				   WHEN '13' THEN 'PERMISO DE PROTECCION ESPECIAL'
				   WHEN '14' THEN 'DOCUMENTO EXTRANJERO'
				   WHEN '15' THEN 'SIN IDENTIFICACION'ELSE 'N/A' END AS 'TIPO IDENTIFICACION',
ISNULL(F.PatientCode, '0') AS 'IDENTIFICACION',
CASE WHEN RTRIM(PAC.IPNOMCOMP) IS NULL AND F.DOCUMENTTYPE LIKE '4' THEN 'N/A' 
	 WHEN PAC.IPNOMCOMP IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'N/A' ELSE RTRIM(PAC.IPNOMCOMP) END AS  
'PACIENTE' ,ISNULL(F.AdmissionNumber,'000') 'INGRESO',
CAST(ISNULL(ING.IFECHAING,f.InitialDate) AS DATE)'FECHA INGRESO',
CAST(ISNULL(ING.FECHEGRESO,ISNULL(ING.IFECHAING,f.InitialDate)) AS DATE) 'FECHA EGRESO' ,
F.InvoiceNumber 'NRO FACTURA',
CAST(F.AnnulmentDate AS DATETIME) AS 'FECHA FACTURA',
F.InvoiceExpirationDate AS 'FECHA VENCIMIENTO',
IIF(F.DOCUMENTTYPE=6,-f.InvoiceValue,-F.TotalInvoice) AS 'TOTAL FACTURA',
-F.ValueTax AS 'VALOR IMPUESTO',-F.ThirdPartySalesValue AS 'VR TOTAL ENTIDAD',-F.ThirdPartyDiscountValue AS 'VR TOTAL DESCUENTO',
-F.TotalPatientSalesPrice AS 'VR TOTAL CUOTA RECUPERACION',
-F.PatientDiscount AS 'DESCUENTO CUOTA RECUPERACION',
-F.PatientPaidValue AS 'VR PAGADO PACIENTE',
-F.ThirdPartyAccountReceivableValue AS 'VR CXC'
,-F.PatientAccountReceivableValue AS 'VR CXC GENERADA A PACIENTE',
CASE F.STATUS WHEN '1' THEN 'FACTURADO' WHEN '2' THEN 'ANULADO' END AS 'ESTADO FACTURA',
RTRIM(T.Nit)+'-'+T.DigitVerification AS 'NIT ENTIDAD',
RTRIM(ISNULL(HA.Code,T.NIT)) 'CODIGO ENTIDAD' ,   RTRIM(ISNULL(HA.Name,T.NAME)) 'ENTIDAD' ,RTRIM(ISNULL(CG.Name,'N/A')) 'GRUPO DE ATENCION',
CASE WHEN HA.EntityType LIKE '1' THEN 'EPS CONTRIBUTIVO' WHEN HA.EntityType LIKE '2' THEN 'EPS SUBSIDIADO' WHEN HA.EntityType LIKE '3' THEN 'ET VINCULADO MUNICIPIO'
WHEN HA.EntityType LIKE '4' THEN 'ET VINCULADOS DAPARTAMENTO' WHEN HA.EntityType LIKE '5' THEN 'ARL RIESGO LABORALES' WHEN HA.EntityType LIKE '6' THEN 'MP MEDICINA PREPAGADA'
WHEN HA.EntityType LIKE '7' THEN 'IPS PRIVADA' WHEN HA.EntityType LIKE '8' THEN 'IPS PUBLICA' WHEN HA.EntityType LIKE '9' THEN 'REGIMEN ESPECIAL'WHEN HA.EntityType LIKE '10' THEN 'ACCIDENTE DE TRANSITO'
WHEN HA.EntityType LIKE '11' THEN 'FOSYGA' WHEN HA.EntityType LIKE '12' THEN 'OTROS' WHEN HA.EntityType IS NULL AND F.DOCUMENTTYPE LIKE '4' THEN 'N/A'
WHEN HA.EntityType IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'N/A' END AS [TIPO REGIMEN],
CASE WHEN ING.TIPOINGRE=1 THEN 'AMBULATORIO'  WHEN ING.TIPOINGRE=2 THEN 'HOSPITALARIO' WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '1' THEN 'FACTURA EAPB CON CONTRATO'
WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '2' THEN 'FACTURA EAPB SIN CONTRATO'WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '3' THEN 'FACTURA PARTICULAR'
WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '4' THEN 'FACTURA CAPITA' WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '5' THEN 'CONTROL CAPITACION'
WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'NO OPERACIONAL' WHEN ING.TIPOINGRE IS NULL AND F.DOCUMENTTYPE LIKE '7' THEN 'FACTURA VENTA PRODUCTOS' END AS 'TIPO AMBITO',
USU.NOMUSUARI 'USUARIO FACTURO',CAST(F.InvoicedDate AS DATE) 'FECHA REGISTRO',ISNULL(DIA.CODDIAGNO,ISNULL(DIAG.CODDIAGNO,'N/A')) 'CODIGO CIE10',SUBSTRING(ISNULL(RTRIM(DIA.NOMDIAGNO),ISNULL(RTRIM(DIAG.NOMDIAGNO),'N/A')),1,80) 'DIAGNOSTICO',
CASE WHEN ING.IAUTORIZA IS NULL AND F.DOCUMENTTYPE LIKE '4' THEN 'N/A' WHEN ING.IAUTORIZA IS NULL AND F.DOCUMENTTYPE LIKE '6' THEN 'N/A' 	WHEN ING.IAUTORIZA LIKE ' ' THEN 'N/A'
ELSE ING.IAUTORIZA END AS 'AUTORIZACION',  ISNULL(IIF (F.DOCUMENTTYPE=6, LTRIM(FU2.CODE) + ' - ' + FU2.NAME, 
ISNULL(FU.CODE + ' - ' +FU.Name, LTRIM(ING.UFUCODIGO) + ' - ' + UF.NAME)), 'N/A') AS [UNIDAD FUNCIONAL EGRESO],IIF (F.DOCUMENTTYPE=6, FU2.NAME, C.Name) AS  'CATEGORIA FACTURA',F.AnnulmentDate 'FECHA ANULACION',
CAST(F.AnnulmentDate AS DATE) AS 'FECHA BUSQUEDA', USU2.NOMUSUARI 'USUARIO ANULO'
FROM 
Billing.Invoice AS F
LEFT JOIN Contract .HealthAdministrator AS HA ON HA.Id =F.HealthAdministratorId 
LEFT JOIN Contract .CareGroup AS CG ON CG.Id =F.CareGroupId 
LEFT JOIN Common.ThirdParty AS T ON T.Id = F.ThirdPartyId
LEFT JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES=F.AdmissionNumber 
LEFT JOIN dbo.ADCENATEN AS CEN ON CEN.CODCENATE=ING.CODCENATE
LEFT JOIN DBO.INPACIENT AS PAC ON PAC.IPCODPACI =F.PatientCode 
LEFT JOIN DBO.SEGusuaru AS USU ON USU.CODUSUARI =F.InvoicedUser 
LEFT JOIN DBO.INDIAGNOS AS DIA ON DIA.CODDIAGNO =ISNULL(ING.CODDIAEGR ,ING.CODDIAING )
LEFT JOIN Payroll.FunctionalUnit AS UF ON UF.Code  = ING.UFUCODIGO
LEFT JOIN Payroll.FunctionalUnit AS FU ON ING.UFUEGRMED=FU.CODE
LEFT JOIN Billing.BasicBilling AS BB ON BB.InvoiceId =F.Id
LEFT JOIN Payroll.FunctionalUnit AS FU2 ON BB.FunctionalUnitId =FU2.Id
LEFT JOIN Billing.InvoiceCategories AS C ON C.Id = F.InvoiceCategoryId
LEFT JOIN dbo.INDIAGNOS AS DIAG ON DIAG.CODDIAGNO = F.OutputDiagnosis
LEFT JOIN dbo.SEGusuaru AS USU2 ON USU2.CODUSUARI = F.AnnulmentUser
WHERE 
F.Status = 2
)

SELECT
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 CABF.*,
 YEAR([FECHA BUSQUEDA]) AS 'AÑO FECHA BUSQUEDA',
 MONTH([FECHA BUSQUEDA]) AS 'MES AÑO FECHA BUSQUEDA',
 CASE MONTH([FECHA BUSQUEDA]) 
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
	WHEN 12 THEN 'DICIEMBRE'
 END AS 'MES NOMBRE FECHA BUSQUEDA', 
 FORMAT(DAY([FECHA BUSQUEDA]), '00') AS 'DIA FECHA BUSQUEDA',
 CONCAT(FORMAT(MONTH([FECHA BUSQUEDA]), '00') ,' - ', 
	    CASE MONTH([FECHA BUSQUEDA]) 
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
			WHEN 12 THEN 'DICIEMBRE'
		END) MES_LABEL_BUSQUEDA,
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM
 CTE_FACTURACION_TOTAL CABF
UNION ALL
SELECT
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 CABFA.*,
 YEAR([FECHA BUSQUEDA]) AS 'AÑO FECHA BUSQUEDA',
 MONTH([FECHA BUSQUEDA]) AS 'MES AÑO FECHA BUSQUEDA',
 CASE MONTH([FECHA BUSQUEDA])  
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
	WHEN 12 THEN 'DICIEMBRE'
 END AS 'MES NOMBRE FECHA BUSQUEDA', 
 FORMAT(DAY([FECHA BUSQUEDA]), '00') AS 'DIA FECHA BUSQUEDA',
 CONCAT(FORMAT(MONTH([FECHA BUSQUEDA]), '00') ,' - ', 
	    CASE MONTH([FECHA BUSQUEDA]) 
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
			WHEN 12 THEN 'DICIEMBRE'
		END) MES_LABEL_BUSQUEDA,
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM
 CTE_FACTURACION_TOTAL_ANULADA CABFA
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para conciliación NIIF que consolida el total facturado incluyendo facturas vigentes y sus reversos por anulación. Combina dos CTE: una con facturas activas (valores positivos) y otra con facturas anuladas (valores negados), uniendo datos de ingresos, pacientes, entidades pagadoras, diagnósticos CIE-10 y unidades funcionales. Expone valores financieros desagregados (total factura, CXC, cuota recuperación, impuestos) clasificados por tipo de documento, régimen y ámbito de atención, orientado a reportes contables bajo estándares NIIF.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte NIIF que consolida la facturación total (activa y anulada con valores en negativo) con datos descriptivos de paciente, entidad, ingreso, diagnóstico y unidad funcional para análisis financiero.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en Billing.Invoice como fuente principal; Las claves de unión (HealthAdministratorId, CareGroupId, ThirdPartyId, AdmissionNumber, PatientCode, InvoicedUser, OutputDiagnosis, InvoiceCategoryId, AnnulmentUser) deben corresponder a los catálogos referenciados o se devuelven nulos por LEFT JOIN; DocumentType en Billing.Invoice debe estar en el dominio 1..7 para mapear ''TIPO REGISTRO''; valores fuera de rango devuelven NULL; Status de la factura debe ser 1 (FACTURADO) o 2 (ANULADO) para mapear ''ESTADO FACTURA''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda factura activa (Status 1 o 2) aparece al menos una vez con importes positivos; las anuladas (Status=2) aparecen dos veces: una positiva y una negativa, garantizando que la suma neta de una factura anulada sea cero; ID_COMPANY siempre se rellena con DB_NAME() truncado a 9 caracteres; ULT_ACTUAL siempre refleja GETDATE() convertido a zona horaria ''Pakistan Standard Time''; Diagnóstico se prioriza primero por ING.CODDIAEGR/CODDIAING (vía DIA) y como fallback por F.OutputDiagnosis (vía DIAG); si ambos faltan se reporta ''N/A''; Código/nombre de entidad prioriza HealthAdministrator y, si no existe, usa los del ThirdParty (T.NIT/T.NAME); El nombre del diagnóstico se trunca a 80 caracteres; Para facturas no anuladas la FECHA BUSQUEDA coincide con la FECHA FACTURA; para la fila anulada espejo la FECHA BUSQUEDA es la fecha de anulación', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación NIIF; Factura EAPB con/sin contrato; Factura particular; Factura cápita; Control de capitación; Factura no operacional; Venta de productos; Anulación de factura; Tipo de identificación del paciente; Régimen de afiliación (contributivo, subsidiado, ARL, medicina prepagada, FOSYGA); Ingreso ambulatorio/hospitalario; Centro de atención; Unidad funcional de egreso; Diagnóstico CIE-10; Autorización; Cuota de recuperación; Cuentas por cobrar (CxC) a entidad y a paciente; Copago/descuento al paciente; Impuesto (IVA); Grupo de atención contractual', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewNIIFTotalFacturado: Devuelve UNION ALL: una fila por cada factura en CTE_FACTURACION_TOTAL (todas las facturas con valores positivos) más una fila adicional por cada factura con Status=2 en CTE_FACTURACION_TOTAL_ANULADA (mismos datos pero con todos los importes negados y FECHA FACTURA = AnnulmentDate)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.DocumentType = 6 (NO OPERACIONAL) → TOTAL FACTURA usa F.InvoiceValue, FECHA FACTURA y FECHA BUSQUEDA usan f.InitialDate, UNIDAD FUNCIONAL EGRESO se construye con FU2 (Billing.BasicBilling.FunctionalUnitId) y CATEGORIA FACTURA = FU2.NAME else TOTAL FACTURA usa F.TotalInvoice, FECHA FACTURA usa F.InvoiceDate, UNIDAD FUNCIONAL EGRESO usa FU (ING.UFUEGRMED) o UF (ING.UFUCODIGO), y CATEGORIA FACTURA = C.Name (Billing.InvoiceCategories); si F.Status = 2 (factura anulada) → Se incluye además una fila con todos los importes monetarios negados (-TotalInvoice, -ValueTax, -ThirdPartySalesValue, -ThirdPartyDiscountValue, -TotalPatientSalesPrice, -PatientDiscount, -PatientPaidValue, -ThirdPartyAccountReceivableValue, -PatientAccountReceivableValue) y FECHA BUSQUEDA = AnnulmentDate; si HA.EntityType IS NULL y DocumentType IN (4,6) → TIPO REGIMEN = ''N/A'' (factura cápita o no operacional sin administradora) else Se mapea EntityType 1..12 a etiquetas (EPS CONTRIBUTIVO, EPS SUBSIDIADO, ARL, MP, IPS, FOSYGA, etc.); si ING.TIPOINGRE IS NULL → TIPO AMBITO se sustituye por la etiqueta del DocumentType (FACTURA EAPB, PARTICULAR, CAPITA, etc.) else TIPO AMBITO = ''AMBULATORIO'' (1) o ''HOSPITALARIO'' (2); si ING.IAUTORIZA IS NULL y DocumentType IN (4,6), o IAUTORIZA = '' '' → AUTORIZACION = ''N/A'' else AUTORIZACION = ING.IAUTORIZA; si PAC.IPNOMCOMP IS NULL y DocumentType IN (4,6) → PACIENTE = ''N/A'' else PACIENTE = RTRIM(PAC.IPNOMCOMP)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Contract.HealthAdministrator; Contract.CareGroup; Common.ThirdParty; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INPACIENT; dbo.SEGusuaru; dbo.INDIAGNOS; Payroll.FunctionalUnit; Billing.BasicBilling; Billing.InvoiceCategories', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewNIIFTotalFacturado';
GO
