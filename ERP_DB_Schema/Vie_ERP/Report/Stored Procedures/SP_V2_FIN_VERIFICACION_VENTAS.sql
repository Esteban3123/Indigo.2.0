
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