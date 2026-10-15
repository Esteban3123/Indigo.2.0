
CREATE view [Report].[ViewTotalVentas] as
SELECT
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 ((sc.Year * 100) + sc.Month) [ID_TIEMPO_VENTAS],
 SUM(((sc.DebitValue - sc.CreditValue) * -1)) VENTAS 
FROM  
 GeneralLedger.GeneralLedgerBalance AS sc  
 INNER JOIN Common.ThirdParty AS t  ON t .Id = sc.IdThirdParty 
 LEFT OUTER JOIN GeneralLedger.MainAccounts AS c  ON c.Id = sc.IdMainAccount 
WHERE
 (t .PersonType IN ('1','2')) AND (c.LegalBookId = '1') AND 
 (c.Number BETWEEN '41' AND '41999999')
GROUP BY
 ((sc.Year * 100) + sc.Month)