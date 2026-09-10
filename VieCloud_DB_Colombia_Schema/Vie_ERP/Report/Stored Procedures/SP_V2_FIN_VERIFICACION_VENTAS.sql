
CREATE PROCEDURE [Report].[SP_V2_FIN_VERIFICACION_VENTAS] AS

SELECT DISTINCT 'ODO' 'EMPRESA', YEAR(JV.VoucherDate) 'AÑO',MONTH(JV.VoucherDate) 'MES','OPERACIONAL' 'TIPO VENTA', 'FACTURA EVENTO' 'TIPO MODALIDAD',
'FACTURADO' 'ESTADO',CAST(sum(JVD.CreditValue) AS NUMERIC) AS 'VALOR FACTURADO'
	FROM 
	GeneralLedger.JournalVouchers JV 
	INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD ON JV.Id=JVD.IdAccounting
	INNER JOIN Billing.Invoice AS F ON JV.EntityId=F.Id
	INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT WITH (NOLOCK) ON JV.IdJournalVoucher=JVT.Id
	WHERE JV.LegalBookId=1 AND jv.EntityName ='Invoice' AND JV.IdJournalVoucher=11 AND YEAR(CAST(JV.VoucherDate AS DATE))=YEAR(GETDATE())
	GROUP BY YEAR(JV.VoucherDate),MONTH(JV.VoucherDate)
UNION ALL
SELECT DISTINCT 'ODO' 'EMPRESA', YEAR(JV.VoucherDate) 'AÑO',MONTH(JV.VoucherDate) 'MES','OPERACIONAL' 'TIPO VENTA', 'FACTURA GLOBAL PGP' 'TIPO MODALIDAD',
'FACTURADO' 'ESTADO',CAST(sum(JVD.CreditValue) AS NUMERIC) AS 'VALOR FACTURADO'
	FROM 
	GeneralLedger.JournalVouchers JV 
	INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD ON JV.Id=JVD.IdAccounting
	INNER JOIN Billing.InvoiceEntityCapitated AS IEC ON IEC.Id=JV.EntityId
	INNER JOIN Billing.Invoice AS F ON F.Id=IEC.InvoiceId	
	INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JV.IdJournalVoucher=JVT.Id
	INNER JOIN GeneralLedger.MainAccounts AS MA ON JVD.IdMainAccount=MA.Id
	WHERE JV.LegalBookId=1 	AND jv.EntityName='InvoiceEntityCapitated'  AND JV.IdJournalVoucher=11 AND YEAR(CAST(JV.VoucherDate AS DATE))=YEAR(GETDATE())
	AND MA.Number BETWEEN '41000000' AND '41999999'
	GROUP BY YEAR(JV.VoucherDate),MONTH(JV.VoucherDate)
UNION ALL
SELECT  DISTINCT 'ODO' 'EMPRESA',YEAR(JV.VoucherDate) 'AÑO',MONTH(JV.VoucherDate) 'MES','NO OPERACIONAL' 'TIPO VENTA', 'FACTURA BASICA - COPAGO' 'TIPO MODALIDAD',
'FACTURADO' 'ESTADO',CAST(sum(JVD.CreditValue) AS NUMERIC) AS 'VALOR FACTURADO'
	FROM 
	GeneralLedger.JournalVouchers JV 
	INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD  ON JV.Id=JVD.IdAccounting
	INNER JOIN Billing.BasicBilling AS BB ON JV.EntityId=BB.Id
	INNER JOIN Billing.Invoice AS I ON BB.InvoiceId=I.Id
	INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JV.IdJournalVoucher=JVT.Id
	WHERE JV.LegalBookId =1 AND jv.EntityName ='BasicBilling'  AND JV.IdJournalVoucher=11 AND YEAR(CAST(JV.VoucherDate AS DATE))=YEAR(GETDATE())
    GROUP BY YEAR(JV.VoucherDate),MONTH(JV.VoucherDate)
UNION ALL
SELECT DISTINCT 'ODO' 'EMPRESA',YEAR(JV.VoucherDate) 'AÑO',MONTH(JV.VoucherDate) 'MES','OPERACIONAL' 'TIPO VENTA', 'FACTURA EVENTO' 'TIPO MODALIDAD',
'ANULADO' 'ESTADO',-CAST(sum(JVD.CreditValue) AS NUMERIC) AS 'VALOR FACTURADO'
	FROM 
	GeneralLedger.JournalVouchers JV 
	INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD ON JV.Id=JVD.IdAccounting
	INNER JOIN Billing.Invoice AS F ON JV.EntityId=F.Id
	INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JV.IdJournalVoucher=JVT.Id
	WHERE JV.LegalBookId =1 AND jv.EntityName ='Invoice 'AND JV.IdJournalVoucher=14 AND YEAR(CAST(JV.VoucherDate AS DATE))=YEAR(GETDATE())
    GROUP BY YEAR(JV.VoucherDate),MONTH(JV.VoucherDate)
UNION ALL
SELECT DISTINCT 'ODO' 'EMPRESA',YEAR(JV.VoucherDate) 'AÑO',MONTH(JV.VoucherDate) 'MES','OPERACIONAL' 'TIPO VENTA', 'FACTURA GLOBAL PGP' 'TIPO MODALIDAD',
'ANULADO' 'ESTADO',    -cast(sum(JVD.DebitValue) as numeric) AS 'VALOR FACTURADO'
	FROM 
	GeneralLedger.JournalVouchers JV 
	INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD ON JV.Id=JVD.IdAccounting
	INNER JOIN Billing.InvoiceEntityCapitated AS IEC ON IEC.Id=JV.EntityId
	INNER JOIN Billing.Invoice AS F ON F.Id=IEC.InvoiceId	
	INNER JOIN GeneralLedger.JournalVoucherTypes AS JVT ON JV.IdJournalVoucher=JVT.Id
	INNER JOIN GeneralLedger.MainAccounts AS MA ON JVD.IdMainAccount=MA.Id
	WHERE JV.LegalBookId =1 AND jv.EntityName ='InvoiceEntityCapitated' AND JV.IdJournalVoucher=14 AND YEAR(CAST(JV.VoucherDate AS DATE))=YEAR(GETDATE())
	AND MA.Number BETWEEN '41000000' AND '41999999'
    GROUP BY YEAR(JV.VoucherDate),MONTH(JV.VoucherDate)
UNION ALL
SELECT DISTINCT 'ODO' 'EMPRESA',YEAR(JV.VoucherDate) 'AÑO',MONTH(JV.VoucherDate) 'MES','NO OPERACIONAL' 'TIPO VENTA', 'FACTURA BASICA - COPAGO' 'TIPO MODALIDAD',
'ANULADO' 'ESTADO',    -cast(sum(JVD.CreditValue) as numeric) AS 'VALOR FACTURADO'
	FROM Billing.BasicBilling BB
	INNER JOIN Billing.Invoice AS I on I.Id=BB.InvoiceId
	INNER JOIN Billing.BillingNoteDetail AS BND ON BND.InvoiceId=I.id
	INNER JOIN Billing.BillingNote AS BN ON BN.ID=BND.BillingNoteId
	INNER JOIN Portfolio.PortfolioNote AS PN ON PN.Id=BN.EntityId and EntityName='PortfolioNote'
	INNER JOIN GeneralLedger.JournalVouchers JV ON JV.EntityId=PN.Id and JV.EntityCode=PN.Code and Jv.EntityName='PortfolioNote'
	INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD ON JV.Id=JVD.IdAccounting
	WHERE BB.Status=3 and BB.ThirdPartyEntityCopayId is not null and JV.LegalBookId =1 AND jv.EntityName ='PortfolioNote'  AND JV.IdJournalVoucher=21
	AND YEAR(CAST(JV.VoucherDate AS DATE))=YEAR(GETDATE())
	GROUP BY YEAR(JV.VoucherDate),MONTH(JV.VoucherDate)
	ORDER BY  YEAR(JV.VoucherDate),MONTH(JV.VoucherDate),[TIPO VENTA],[TIPO MODALIDAD]
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte financiero que consolida y verifica las ventas del año en curso para la empresa ODO, agrupadas por año y mes. Combina mediante UNION ALL seis segmentos: facturas por evento, facturas globales de capitación (PGP, cuentas 41xxxxxx) y facturación básica de copago, tanto en estado FACTURADO como ANULADO (con valor negativo). Los datos provienen del libro mayor (LegalBookId=1), cruzando comprobantes contables con los módulos de facturación y cartera para verificar los valores facturados operacionales y no operacionales.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida un reporte anual (año en curso) de verificación de ventas facturadas y anuladas por mes, separando facturas por evento, capitación PGP y básicas-copago, contrastando contabilizaciones con cuentas 41xxxxxx.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los comprobantes deben pertenecer al libro legal LegalBookId=1.; Deben existir tipos de comprobante con IdJournalVoucher 11 (causación/facturado), 14 (anulación) y 21 (nota de cartera).; La fecha del comprobante (VoucherDate) debe corresponder al año actual (YEAR(GETDATE())).; Para PGP se requiere que la cuenta principal (MainAccounts.Number) esté entre ''41000000'' y ''41999999''.; Para anulación de copago: BasicBilling.Status=3 y ThirdPartyEntityCopayId no nulo, con nota de facturación enlazada a PortfolioNote.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se contemplan movimientos del libro legal 1 (LegalBookId=1).; Los valores anulados se devuelven con signo negativo.; El reporte siempre se restringe al año en curso (YEAR(GETDATE())).; La empresa reportada se rotula constantemente como ''ODO''.; La agrupación es siempre por año y mes del VoucherDate.; Los ingresos PGP se reconocen únicamente sobre cuentas PUC clase 41 (ingresos operacionales).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Facturación por evento; Facturación capitada / Global PGP; Factura básica con copago; Anulación de factura; Nota de cartera (PortfolioNote); Comprobante contable / causación; Plan único de cuentas (PUC) clase 41 - ingresos operacionales; Libro legal contable; Copago de tercero', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando JV.IdJournalVoucher=11 y EntityName=''Invoice'' → retorna fila (''OPERACIONAL'',''FACTURA EVENTO'',''FACTURADO'') con SUM(CreditValue) por año/mes.; [RETURN_RESULT] resultset: Cuando JV.IdJournalVoucher=11, EntityName=''InvoiceEntityCapitated'' y MA.Number entre ''41000000'' y ''41999999'' → retorna fila (''OPERACIONAL'',''FACTURA GLOBAL PGP'',''FACTURADO'') con SUM(CreditValue).; [RETURN_RESULT] resultset: Cuando JV.IdJournalVoucher=11 y EntityName=''BasicBilling'' → retorna fila (''NO OPERACIONAL'',''FACTURA BASICA - COPAGO'',''FACTURADO'') con SUM(CreditValue).; [RETURN_RESULT] resultset: Cuando JV.IdJournalVoucher=14 y EntityName=''Invoice '' (con espacio) → retorna fila (''OPERACIONAL'',''FACTURA EVENTO'',''ANULADO'') con -SUM(CreditValue).; [RETURN_RESULT] resultset: Cuando JV.IdJournalVoucher=14, EntityName=''InvoiceEntityCapitated'' y MA.Number entre ''41000000'' y ''41999999'' → retorna fila (''OPERACIONAL'',''FACTURA GLOBAL PGP'',''ANULADO'') con -SUM(DebitValue).; [RETURN_RESULT] resultset: Cuando JV.IdJournalVoucher=21, EntityName=''PortfolioNote'', BasicBilling.Status=3 y ThirdPartyEntityCopayId no nulo → retorna fila (''NO OPERACIONAL'',''FACTURA BASICA - COPAGO'',''ANULADO'') con -SUM(CreditValue).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityName del comprobante (Invoice / InvoiceEntityCapitated / BasicBilling / PortfolioNote) → Selecciona la modalidad de venta (EVENTO, GLOBAL PGP o BASICA-COPAGO) y el join correspondiente.; si IdJournalVoucher = 11 vs 14 vs 21 → Clasifica el estado como FACTURADO (11) o ANULADO (14/21) e invierte el signo del valor para anulaciones.; si MA.Number BETWEEN ''41000000'' AND ''41999999'' → Restringe la sumatoria de PGP solo a cuentas de ingresos operacionales (clase 41). else Las modalidades distintas a PGP no aplican este filtro de cuenta.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.JournalVoucherTypes; GeneralLedger.MainAccounts; Billing.Invoice; Billing.InvoiceEntityCapitated; Billing.BasicBilling; Billing.BillingNote; Billing.BillingNoteDetail; Portfolio.PortfolioNote', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'PROCEDURE', @level1name=N'SP_V2_FIN_VERIFICACION_VENTAS';
-- GO
