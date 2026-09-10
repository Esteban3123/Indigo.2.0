
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista contable de movimientos e asientos de inventario oncológico. Consolida los comprobantes contables (asientos) relacionados con cuentas del plan único de cuentas (PUC) que inician con ''14'' (cuentas de inventario) generados desde el 1 de enero de 2020, cruzando el movimiento contable con sus detalles de débito y crédito, la cuenta contable asociada y, cuando el origen del movimiento es una cuenta por pagar a proveedor (AccountPayable), resuelve el código de entidad real desde el módulo de pagos. Sirve para reportería y auditoría contable del inventario oncológico, permitiendo rastrear qué asientos contables afectaron inventarios, con su fecha de comprobante, descripción del movimiento, valores débito/crédito y la cuenta PUC correspondiente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_MovContableAsientosInventario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_MovContableAsientosInventario';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los asientos contables (débitos/créditos) asociados a cuentas de inventario (PUC clase 14) con fecha posterior a 2020-01-01, resolviendo el código de entidad cuando corresponde a una cuenta por pagar.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableAsientosInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de movimientos contables en GeneralLedger.AccountingMovement con sus respectivos comprobantes y detalles; Las cuentas referenciadas deben existir en el plan de cuentas (MainAccounts); Para resolver entidades de cuentas por pagar, debe existir un registro en Payments.AccountPayable cuyo Code coincida con el EntityCode del movimiento', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableAsientosInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen comprobantes cuyos detalles afecten cuentas del PUC que inicien con ''14'' (clase de inventarios); Solo se incluyen comprobantes con fecha posterior al 01-01-2020; Si el movimiento es de tipo ''AccountPayable'' y existe coincidencia en Payments.AccountPayable, el EntityCode se sustituye por el código de la cuenta por pagar relacionada; Cada fila representa una línea débito/crédito de un comprobante contable filtrado a inventarios', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableAsientosInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Movimiento contable; Comprobante de diario; Asiento contable (débito/crédito); Plan Único de Cuentas (PUC); Cuentas por pagar; Inventario (cuentas que inician en 14)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableAsientosInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando puc.Number LIKE ''14%'' AND c.VoucherDate > ''2020-01-01'' → se retornan todas las líneas de detalle (débito/crédito) de los comprobantes que tengan al menos un detalle sobre cuentas de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableAsientosInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EntityName = ''AccountPayable'' → Se utiliza el código de la cuenta por pagar (CXP.EntityCode) como EntityCode si existe; en caso contrario se conserva el EntityCode del movimiento contable else Se devuelve el EntityCode original del movimiento contable', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableAsientosInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.AccountingMovement; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; Payments.AccountPayable', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableAsientosInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableAsientosInventario';
GO
