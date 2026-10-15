CREATE PROCEDURE [dbo].[SPU_RecibosCajaPagares]
AS
     SELECT pa.Code AS CodigoAnticipo, 
            pa.DocumentDate AS FechaAnticipo, 
            pa.CreationUser AS CodFacturador, 
            Pe.Fullname AS NombreFacturador, 
            pa.Value AS ValorAnticipo, 
            pa.Balance AS SaldoAnticipo, 
            cr.Code AS CodigoReciboCaja, 
            pa.AdmissionNumber AS Ingreso, 
            cr.CreationUser AS UsuarioReciboCaja, 
            pa.Observations, 
            Treasury.CashReceiptConcepts.Code AS CodConcepto, 
            Treasury.CashReceiptConcepts.Name AS Concepto, 
            GeneralLedger.MainAccounts.Number AS CuentaContable, 
            Common.ThirdParty.Nit, 
            Common.ThirdParty.Name AS Tercero, 
            cr.Detail AS DetalleRecibo
     FROM Portfolio.PortfolioAdvance AS pa
          INNER JOIN Security.Person AS Pe
          INNER JOIN Security.[User] AS Us ON Pe.Id = Us.IdPerson ON pa.CreationUser = Us.UserCode
          INNER JOIN Treasury.CashReceiptDetails ON pa.CashReceiptDetailId = Treasury.CashReceiptDetails.Id
                                                    AND pa.CashReceiptDetailId = Treasury.CashReceiptDetails.Id
          INNER JOIN GeneralLedger.MainAccounts ON Treasury.CashReceiptDetails.IdMainAccount = GeneralLedger.MainAccounts.Id
                                                   AND Treasury.CashReceiptDetails.IdMainAccount = GeneralLedger.MainAccounts.Id
          INNER JOIN Treasury.CashReceiptConcepts ON Treasury.CashReceiptDetails.IdCashReceiptConcept = Treasury.CashReceiptConcepts.Id
                                                     AND Treasury.CashReceiptDetails.IdCashReceiptConcept = Treasury.CashReceiptConcepts.Id
          INNER JOIN Common.ThirdParty ON pa.ThirdPartyId = Common.ThirdParty.Id
                                          AND pa.ThirdPartyId = Common.ThirdParty.Id
                                          AND pa.ThirdPartyId = Common.ThirdParty.Id
          LEFT OUTER JOIN Treasury.CashReceipts AS cr ON cr.Id = pa.CashReceiptId
     WHERE Treasury.CashReceiptConcepts.Code = '045'
           OR Treasury.CashReceiptConcepts.Code = '06';
     SELECT *
     FROM Treasury.CashReceiptConcepts;