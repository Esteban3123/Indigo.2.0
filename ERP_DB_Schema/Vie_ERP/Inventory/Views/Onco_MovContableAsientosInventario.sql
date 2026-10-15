
CREATE VIEW [Inventory].[Onco_MovContableAsientosInventario]
AS
     SELECT mov.Consecutive,
            CASE
                WHEN C.EntityName = 'AccountPayable'
                THEN ISNULL(CXP.EntityCode, c.EntityCode)
                ELSE c.EntityCode
            END AS EntityCode, 
            C.EntityId, 
            c.EntityName, 
            c.Detail, 
            puc.Number Cuenta, 
            puc.Name AS Nombre_Cuenta, 
            Mov.VoucherDate Fecha, 
            puc.Name NombCuenta, 
            Dmov.DebitValue, 
            Dmov.CreditValue
     FROM GeneralLedger.AccountingMovement AS C
          INNER JOIN GeneralLedger.JournalVouchers AS Mov WITH(NOLOCK) ON mov.AccountingMovementId = c.Id
          INNER JOIN GeneralLedger.JournalVoucherDetails AS Dmov WITH(NOLOCK) ON Dmov.IdAccounting = Mov.Id
          INNER JOIN GeneralLedger.MainAccounts AS puc WITH(NOLOCK) ON puc.Id = dmov.IdMainAccount
          LEFT JOIN Payments.AccountPayable AS CXP WITH(NOLOCK) ON C.EntityCode = CXP.Code
     WHERE mov.Id IN
     (
         SELECT mov.Id
         FROM GeneralLedger.AccountingMovement AS C
              INNER JOIN GeneralLedger.JournalVouchers AS Mov WITH(NOLOCK) ON mov.AccountingMovementId = c.Id
              INNER JOIN GeneralLedger.JournalVoucherDetails AS Dmov WITH(NOLOCK) ON Dmov.IdAccounting = Mov.Id
              INNER JOIN GeneralLedger.MainAccounts AS puc WITH(NOLOCK) ON puc.Id = dmov.IdMainAccount
              LEFT JOIN Payments.AccountPayable AS CXP WITH(NOLOCK) ON C.EntityCode = CXP.Code
         WHERE PUC.Number LIKE '14%'
               AND c.VoucherDate > '01-01-2020' ---and c.VoucherDate<'31-3-2020' --AND C.EntityName = 'PharmaceuticalDispensing'   
     );