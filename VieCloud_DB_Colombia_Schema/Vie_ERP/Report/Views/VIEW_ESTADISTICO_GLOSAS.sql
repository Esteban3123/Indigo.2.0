/*******************************************************************************************************************
Nombre: [Report].[VIEW_ESTADISTICO_GLOSAS]
Tipo:Vista
Observacion:
Profesional:
Fecha:
---------------------------------------------------------------------------
Modificaciones
__________________________________________________________________________________________________________________________________________________________________
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:28-02-2024
Observaciones: Se agrega en la condicion de la tabla Portfolio.AccountReceivable para que no traiga en la columna
			  AccountReceivableType el tipo 4 y el 6, con esto se exonera los pagares y las cuotas moderadoras ya que no lo paga una instucion.
			  Esto solicitado en el ticket 15484
--------------------------------------------------------------------------------------------
Version 3
Persona que modifico:
Fecha:
Observaciones:
--------------------------------------------------------------------------------------------
***********************************************************************************************/

CREATE VIEW [Report].[VIEW_ESTADISTICO_GLOSAS] AS

SELECT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
T.Nit 'NIT', 
T.Name AS 'ENTIDAD', 
C.RadicatedConsecutive AS 'NRO RADICADO GLOSA', 
CAST(C.RadicatedDate AS DATE) AS 'FECHA RADICADO GLOSA', 
CAST(C.ConfirmDate AS DATE) AS 'FECHA CONFIRMACION RADICADO GLOSA', 
CASE WHEN C.State = 1 THEN 'Sin Confirmar' 
	 WHEN C.State = 2 THEN 'ConfirmadoRadicado' 
	 WHEN C.State = 3 THEN 'OficioConRespuesta' 
	 WHEN C.State = 4 THEN 'Anulada' END AS 'ESTADO RADICADO GLOSA', 
DG.InvoiceNumber AS 'FACTURA', CAST(DG.InvoiceDate AS DATE) AS 'FECHA FACTURA', DG.InvoiceValueEntity AS 'VALOR ENTIDAD', 
           DG.BalanceInvoice AS  'VALOR FACUTRA', DG.ValueGlosado AS 'VALOR GLOSADO', DG.ValueAcceptedFirstInstance AS 'VALOR ACEPTADO 1RA INS', DG.ValueReiterated AS 'VALOR REITERADO', DG.ValueAcceptedSecondInstance AS 'VALOR ACEPTADO 2DA INS', DG.ValueAcceptedIPSconciliation AS 'VALOR ACEPTADO IPS CONCILIACION', 
           DG.ValueAcceptedEAPBconciliation AS 'VALOR ACEPTADO EAPB CONCILIACION', DG.BalanceGlosa AS 'SALDO PENDIENTE DE CONCILIAR', DG.ValuePayments AS 'VALOR PAGO PARCIAL', tcj.LegalTransferValue AS 'COBRO JURIDICO', DG.RadicatedNumber AS 'NRO RADICADO ERP', CAST(DG.RadicatedDate AS DATE) AS 'FECHA RADICADO ERP', 
           CASE WHEN CC.State = 1 THEN 'Sin Confirmar' WHEN CC.State = 2 THEN 'Confirmado' END AS 'ESTADO CONCILIACION', CC.ConciliationConsecutive AS 'NRO CONCILIACION', C.Comment AS 'OBSERVACIONES', CAST(CC.ConciliationDate AS DATE) AS 'FECHA REGISTRO CONCILIACION', 
           CAST(CC.ConfirmDate AS DATE) AS 'FECHA CONFIRMACION CONCILIACION', CAST(CC.DocumentDate AS DATE) AS 'FECHA OFICIO CONCILIACION', CAST(C.DocumentDate AS DATE) AS 'FECHA OFICIO RADICADO GLOSA', CAST(C.DateRadicatedDocumentReply AS DATE) AS 'FECHA CONFIRMACION CONSECUTIVO RESPUESTA', CAST(DG.CoordinationDateGlosa AS DATE) AS 'FECHA CONFIRMACION RESPUESTA GLOSA FACTURA',
		    CAST(ing.IFECHAING AS DATE)  as 'FECHA INGRESO', CAST(eg.FECALTPAC AS DATE)  as 'FECHA EGRESO', CAT.Name AS 'CATEGORIA',CEN.NOMCENATE AS 'CENTRO DE ATENCION',
			1 as 'CANTIDAD',
			CAST(C.RadicatedDate AS date) AS 'FECHA BUSQUEDA',
			CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM   Glosas.GlosaPortfolioGlosada AS DG INNER JOIN
               (SELECT D.DocumentType, D.InvoiceNumber, D.GlosaObjectionsReceptionCId, D.PortfolioGlosaId
              FROM   Glosas.GlosaObjectionsReceptionD AS D INNER JOIN
                             (SELECT MAX(GlosaObjectionsReceptionCId) AS GlosaObjectionsReceptionCId, PortfolioGlosaId, InvoiceNumber
                            FROM   Glosas.GlosaObjectionsReceptionD
                            GROUP BY PortfolioGlosaId, InvoiceNumber) AS G ON G.GlosaObjectionsReceptionCId = D.GlosaObjectionsReceptionCId AND D.InvoiceNumber = G.InvoiceNumber AND D.PortfolioGlosaId = G.PortfolioGlosaId) AS G1 ON G1.PortfolioGlosaId = DG.Id INNER JOIN
           Glosas.GlosaObjectionsReceptionC AS C with (nolock) ON G1.GlosaObjectionsReceptionCId = C.Id INNER JOIN
           Common.Customer AS T with (nolock) ON C.CustomerId = T.Id LEFT OUTER JOIN
               (SELECT InvoiceNumber, MAX(ConciliationCId) AS ConciliationCId
              FROM   Glosas.ConciliationD
              GROUP BY InvoiceNumber) AS CD ON DG.InvoiceNumber = CD.InvoiceNumber LEFT OUTER JOIN
           Glosas.ConciliationC AS CC with (nolock) ON CD.ConciliationCId = CC.Id LEFT OUTER JOIN
           Glosas.TransferJuridicalDebtCollectionD AS tcj with (nolock) ON tcj.InvoiceNumber = DG.InvoiceNumber LEFT OUTER JOIN
		   Portfolio.AccountReceivable as ar with (nolock) on ar.InvoiceNumber=dg.InvoiceNumber and ar.Balance>'0' /*in v2*/and AccountReceivableType NOT IN (4,6)/*fn v2*/ left outer join
		   billing.invoice as i on i.invoicenumber=dg.invoicenumber
		   LEFT JOIN Billing.InvoiceCategories AS CAT ON CAT.Id = I.InvoiceCategoryId
		   left outer join
		   adingreso as ing on ing.NUMINGRES=i.AdmissionNumber left outer join
		   dbo.HCREGEGRE AS eg WITH (nolock) ON eg.numingres=i.AdmissionNumber 
		   LEFT JOIN DBO.ADCENATEN AS CEN ON CEN.CODCENATE =ING.CODCENATE 
		   --where DG.InvoiceNumber='FEVC3388'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte estadístico del proceso de glosas hospitalarias, orientada a seguimiento financiero y operativo. Consolida por factura los valores glosados, aceptados en primera y segunda instancia, conciliados (IPS y EAPB), pagos parciales y cobros jurídicos, junto con el estado del radicado y la conciliación. Incorpora datos de ingreso y egreso del paciente, centro de atención y categoría de factura. Excluye cuentas por cobrar de tipo pagaré y cuota moderadora (tipos 4 y 6).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista estadística que consolida la trazabilidad de glosas por factura: radicación, estado, valores objetados/aceptados/conciliados, conciliaciones, cobro jurídico, cuenta por cobrar e información del ingreso/egreso del paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de glosas en Glosas.GlosaPortfolioGlosada con detalle en Glosas.GlosaObjectionsReceptionD y cabecera en Glosas.GlosaObjectionsReceptionC.; Cliente (entidad pagadora) registrado en Common.Customer y referenciado por la cabecera de objeción.; Para enriquecer ingreso/egreso, la factura debe tener AdmissionNumber válido en billing.invoice cruzable con adingreso y dbo.HCREGEGRE.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Por cada (PortfolioGlosaId, InvoiceNumber) se reporta la última recepción de objeción registrada (MAX GlosaObjectionsReceptionCId).; Por cada InvoiceNumber se reporta la última conciliación registrada (MAX ConciliationCId).; Las cuentas por cobrar tipo 4 (pagarés) y 6 (cuotas moderadoras) nunca se incluyen porque no las paga una institución (regla v2, ticket 15484).; Las uniones con AccountReceivable, conciliación, cobro jurídico, factura, categoría, ingreso, egreso y centro de atención son LEFT JOIN: una glosa siempre se reporta aunque no tenga aún esos datos.; Las fechas se exponen truncadas a DATE (sin componente de hora) salvo ULT_ACTUAL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosa; Radicación de glosa; Objeción; Conciliación de glosa; Factura; Entidad pagadora (EPS/EAPB); Cuenta por cobrar (cartera); Pagaré; Cuota moderadora; Cobro jurídico; Categoría de factura; Ingreso hospitalario; Egreso hospitalario; Centro de atención', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.VIEW_ESTADISTICO_GLOSAS: Devuelve una fila por (PortfolioGlosaId, InvoiceNumber) tomando únicamente la última recepción de objeción (MAX(GlosaObjectionsReceptionCId)) y la última conciliación (MAX(ConciliationCId)) por factura.; [RETURN_RESULT] Report.VIEW_ESTADISTICO_GLOSAS: Solo se considera información de Portfolio.AccountReceivable cuando Balance>0 y AccountReceivableType NOT IN (4,6) (excluye pagarés y cuotas moderadoras según v2/ticket 15484).; [RETURN_RESULT] Report.VIEW_ESTADISTICO_GLOSAS: Traduce el estado del radicado de glosa: 1=''Sin Confirmar'', 2=''ConfirmadoRadicado'', 3=''OficioConRespuesta'', 4=''Anulada''.; [RETURN_RESULT] Report.VIEW_ESTADISTICO_GLOSAS: Traduce el estado de conciliación: 1=''Sin Confirmar'', 2=''Confirmado''.; [RETURN_RESULT] Report.VIEW_ESTADISTICO_GLOSAS: Marca cada fila con CANTIDAD=1 para conteos agregables y agrega ULT_ACTUAL como GETDATE() convertido a zona horaria ''Pakistan Standard Time''.; [RETURN_RESULT] Report.VIEW_ESTADISTICO_GLOSAS: Expone ID_COMPANY como DB_NAME() truncado a VARCHAR(9) para identificar la base/empresa origen del dato.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.State ∈ {1,2,3,4} → Mapea a etiqueta textual del estado del radicado de glosa (''Sin Confirmar''/''ConfirmadoRadicado''/''OficioConRespuesta''/''Anulada''). else NULL; si CC.State ∈ {1,2} → Mapea a etiqueta del estado de conciliación (''Sin Confirmar''/''Confirmado''). else NULL; si Portfolio.AccountReceivable.Balance>0 AND AccountReceivableType NOT IN (4,6) → Se vincula la cuenta por cobrar a la fila de la glosa. else Se excluyen pagarés (4) y cuotas moderadoras (6) y cuentas con saldo cero.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Glosas.GlosaPortfolioGlosada; Glosas.GlosaObjectionsReceptionD; Glosas.GlosaObjectionsReceptionC; Common.Customer; Glosas.ConciliationD; Glosas.ConciliationC; Glosas.TransferJuridicalDebtCollectionD; Portfolio.AccountReceivable; billing.invoice; Billing.InvoiceCategories; dbo.adingreso; dbo.HCREGEGRE; dbo.ADCENATEN', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_ESTADISTICO_GLOSAS';
GO
