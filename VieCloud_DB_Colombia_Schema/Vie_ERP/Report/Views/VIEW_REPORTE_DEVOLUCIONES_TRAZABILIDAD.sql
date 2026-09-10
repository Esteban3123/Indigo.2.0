
CREATE VIEW [Report].[VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD] AS

WITH CTE_INGRESOS_FACTURAS
AS
  (
    SELECT DD.Id AS [ID DET],DC.RadicatedConsecutive AS [RADICADO DEVOLUCION], DD.InvoiceNumber AS FACTURA, DD.InvoiceDate AS [FECHA FACTURA], DD.BalanceInvoice AS [VALOR FACTURA],
	 DD.RadicatedDate AS [FECHA RADICACION], DC.DocumentDate AS [FECHA RECEPCION DEVOLUCION],DC.RadicatedDate AS [FECHA RADICADO DEVOLUCION],DC.CustomerId 'CLIENTE',DD.RadicatedNumber AS [CONSECUTIVO RADICACION],
	Ingress 'INGRESO'  FROM Glosas.GlosaDevolutionsReceptionD AS DD WITH (nolock) INNER JOIN
                         Glosas.GlosaDevolutionsReceptionC AS DC WITH (nolock) ON DD.GlosaDevolutionsReceptionCId = DC.Id
  ),

CTE_RADICADO
AS
 (
 SELECT DET.InvoiceNumber , MAX(DET.RadicatedNumber) AS RadicatedNumber FROM Portfolio .RadicateInvoiceD  DET
 INNER JOIN CTE_INGRESOS_FACTURAS AS FAC ON DET.InvoiceNumber =FAC.FACTURA WHERE DET.State <> '4' GROUP BY DET.InvoiceNumber
 )

SELECT        
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
CTE.[RADICADO DEVOLUCION],CTE.FACTURA, CTE.[FECHA FACTURA], CTE.[VALOR FACTURA],  
CASE P.IPTIPODOC    WHEN '1' THEN 'CEDULA DE CIUDADANIA' 
					WHEN '2' THEN 'CEDULA DE EXTRANJERIA' 
					WHEN '3' THEN 'TARJETA DE IDENTIDAD' 
					WHEN '4' THEN 'REGISTRO CIVIL' 
					WHEN '5' THEN 'PASAPORTE'
					WHEN '6' THEN 'ADULTO SIN IDENTIFICACION' 
					WHEN '7' THEN 'MENOR SIN IDENTIFICACION' 
					WHEN '8' THEN 'NUMERO UNICO DE IDENTIFICACIÒN' 
					WHEN '9' THEN 'CERTIFICADO NACIDO VIVO' 
					WHEN '10' THEN 'CARNET DIPLOMATICO'
					WHEN '11' THEN 'SALVOCONDUCTO' 
					WHEN '12' THEN 'PERMISO ESPECIAL DE PERMANENCIA'
					WHEN '13' THEN 'PERMISO TEMPORAL DE PERMANENCIA'
					WHEN '14' THEN 'DOCUMENTO EXTRANJERO'
					WHEN '15' THEN 'SIN IDENTIFICACION'
					END AS  'TIPO DOCUMENTO', 
FAC.PatientCode AS 'IDENTIFICACION PACIENTE', 
P.IPNOMCOMP AS 'NOMBRE PACIENTE',
CASE AR.PortfolioStatus WHEN '1' THEN 'SIN RADICAR' WHEN '2' THEN 'RADICADA SIN CONFIRMAR' WHEN '3' THEN 'RADICADA ENTIDAD' WHEN '7' THEN 'CERTIFICADA PARCIAL' WHEN '8' THEN 'CERTIFICADA TOTAL' WHEN
                          '14' THEN 'DEVOLUCION FACTURA ' WHEN '15' THEN 'TRASLADO COBRO JURÍDICO CONFIRMADO' END AS [ESTADO CARTERA], CTE.[FECHA RADICACION], 
                        CTE.[FECHA RECEPCION DEVOLUCION], 
                         CTE.[CONSECUTIVO RADICACION], AR.Balance AS [SALDO CARTERA], CTE.[FECHA RADICADO DEVOLUCION], 
                         U.NOMUSUARI AS FACTURADOR, E.Name AS ENTIDAD, CASE MV.TypeDevolution WHEN '1' THEN 'Justificada' WHEN '2' THEN 'Injustificada' ELSE 'Sin Gestión' END AS [TIPO DEVOLUCION], 
                         MV.CreationDate AS [FECHA OPCIÓN Justifica ó Injustifica],C.Code as [CODIGO CONCEPTO DEVOLUCION] ,C.NameSpecific AS [CONCEPTO DEVOLUCION], MV.Comment AS [MOTIVO EAPB], MV.Answer AS [RESPUESTA IPS], 
                         MV.CreationUser AS [USUARIO INGRESO DEV],U2.NOMUSUARI [NOMBRE USUARIO DEV],  CAT.Name AS 'CATEGORIA',CEN.NOMCENATE AS 'CENTRO DE ATENCION',
						 CTE.INGRESO ,CASE FAC.Status WHEN 1 THEN 'FACTURADO' ELSE 'ANULADA' END 'ESTADO FACTURA', 
						 RAD.RadicatedNumber 'ULTIMO RADICADO', 
						 1 as 'CANTIDAD',
						 CAST([FECHA RECEPCION DEVOLUCION] AS date) AS 'FECHA BUSQUEDA',
						 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM       CTE_INGRESOS_FACTURAS AS CTE
						 INNER JOIN Billing .Invoice AS FAC ON FAC.InvoiceNumber =CTE.FACTURA  
						 INNER JOIN
                         Glosas.GlosaMovementDevolutions AS MV WITH (nolock) ON CTE.[ID DET]  = MV.IdDevolutionsReceptionD LEFT OUTER JOIN
                         Portfolio.AccountReceivable AS AR WITH (nolock) ON CTE.FACTURA  = AR.InvoiceNumber AND AR.AccountReceivableType = '2' LEFT OUTER JOIN
                         dbo.SEGusuaru AS U WITH (nolock) ON AR.CreationUser = U.CODUSUARI LEFT OUTER JOIN
                         Common.ConceptGlosas AS C WITH (nolock) ON C.Id = MV.IdConceptGlosa LEFT OUTER JOIN
                         Common.Customer AS E WITH (nolock) ON CTE.CLIENTE = E.Id LEFT OUTER JOIN
						 dbo.SEGusuaru AS U2 WITH (nolock) ON MV.CreationUser = U2.CODUSUARI
						 LEFT JOIN CTE_RADICADO AS RAD ON RAD.InvoiceNumber =CTE.FACTURA AND RAD.RadicatedNumber > CTE.[CONSECUTIVO RADICACION] 
						 LEFT JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES =FAC.AdmissionNumber 
						 LEFT JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE =ING.CODCENATE
						 LEFT JOIN Billing.InvoiceCategories AS CAT ON CAT.Id = FAC.InvoiceCategoryId
						 LEFT JOIN dbo.INPACIENT AS P ON P.ipcodpaci = FAC.PatientCode
						 --WHERE CAST([FECHA RECEPCION DEVOLUCION] AS date)='2024-02-26'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para trazabilidad de devoluciones de glosas, orientada a consumo en informes de cartera y auditoría. Consolida, por factura, el ciclo completo de una devolución: recepción de la devolución, radicación, estado de cartera, tipo de devolución (justificada/injustificada), concepto de glosa, respuestas IPS y último radicado vigente. Cruza datos demográficos del paciente, entidad pagadora, facturador, centro de atención y categoría de factura, incluyendo el nombre de la empresa mediante `DB_NAME()` para entornos multitenant.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la trazabilidad de devoluciones de facturas glosadas, integrando datos de recepción, gestión (justificada/injustificada), estado de cartera, último radicado y datos del paciente/entidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las devoluciones deben estar registradas en GlosaDevolutionsReceptionC y su detalle en GlosaDevolutionsReceptionD vinculado por GlosaDevolutionsReceptionCId.; La factura del detalle (InvoiceNumber) debe existir en Billing.Invoice para aparecer en el reporte (INNER JOIN).; Debe existir al menos un movimiento de devolución en Glosas.GlosaMovementDevolutions asociado al detalle (IdDevolutionsReceptionD).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa la combinación detalle-de-devolución × movimiento-de-devolución de una factura existente en Billing.Invoice.; El campo ''CANTIDAD'' siempre es 1 (constante para conteo en el reporte).; ID_COMPANY corresponde al nombre de la base de datos actual truncado a 9 caracteres (DB_NAME()).; ULT_ACTUAL refleja la fecha/hora actual convertida a la zona horaria ''Pakistan Standard Time''.; Solo se considera cartera con AccountReceivableType=''2''; otros tipos quedan sin saldo ni estado de cartera.; El ''ÚLTIMO RADICADO'' solo se reporta si es estrictamente posterior al consecutivo de radicación de la devolución y proviene de radicados no anulados (State<>''4'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de glosa; Radicación de factura; Cartera / cuentas por cobrar; Concepto de glosa; Tipo de devolución (justificada/injustificada); Paciente; Entidad pagadora (EPS/aseguradora); Centro de atención; Categoría de factura; Tipo de documento de identificación; Facturador (usuario); Ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD: Devuelve una fila por cada movimiento de devolución (GlosaMovementDevolutions) asociado a un detalle de recepción de devolución cuya factura exista en Billing.Invoice.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si P.IPTIPODOC ∈ {''1''..''15''} → Traduce el código de tipo de documento del paciente a su descripción (Cédula, Pasaporte, Registro Civil, etc.).; si AR.PortfolioStatus ∈ {''1'',''2'',''3'',''7'',''8'',''14'',''15''} → Traduce el estado de cartera a su descripción (Sin radicar, Radicada entidad, Certificada parcial/total, Devolución factura, Traslado cobro jurídico, etc.).; si MV.TypeDevolution = ''1'' → Marca la devolución como ''Justificada''. else Si ''2'' → ''Injustificada''; cualquier otro valor → ''Sin Gestión''.; si FAC.Status = 1 → Estado de factura = ''FACTURADO''. else Estado de factura = ''ANULADA''.; si Portfolio.RadicateInvoiceD.State <> ''4'' → Considera el radicado para calcular el último RadicatedNumber por factura (CTE_RADICADO). else Excluye los radicados con State=''4'' (anulados/inactivos) del cálculo del último radicado.; si RAD.RadicatedNumber > CTE.[CONSECUTIVO RADICACION] → Asocia un ''ÚLTIMO RADICADO'' posterior a la radicación de la devolución; en caso contrario queda NULL.; si AR.AccountReceivableType = ''2'' → Solo enlaza cuentas por cobrar de tipo ''2'' (cartera de facturas) para obtener saldo y estado de cartera.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaDevolutionsReceptionD; Glosas.GlosaDevolutionsReceptionC; Portfolio.RadicateInvoiceD; Billing.Invoice; Glosas.GlosaMovementDevolutions; Portfolio.AccountReceivable; dbo.SEGusuaru; Common.ConceptGlosas; Common.Customer; dbo.ADINGRESO; dbo.ADCENATEN; Billing.InvoiceCategories; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_REPORTE_DEVOLUCIONES_TRAZABILIDAD';
GO
