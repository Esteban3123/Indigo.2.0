
CREATE VIEW [Inventory].[Onco_MovContableInventario]
AS
     SELECT CASE
                WHEN C.EntityName = 'AccountPayable'
                THEN ISNULL(CXP.EntityCode, c.EntityCode)
                ELSE c.EntityCode
            END AS EntityCode, 
            C.EntityId, 
            c.EntityName, 
            c.Detail, 
            puc.Number Cuenta, 
            Mov.VoucherDate Fecha, 
            puc.Name NombCuenta, 
            Dmov.DebitValue, 
            Dmov.CreditValue
     FROM GeneralLedger.AccountingMovement AS C
          INNER JOIN GeneralLedger.JournalVouchers AS Mov WITH(NOLOCK) ON mov.AccountingMovementId = c.Id
          INNER JOIN GeneralLedger.JournalVoucherDetails AS Dmov WITH(NOLOCK) ON Dmov.IdAccounting = Mov.Id
          INNER JOIN GeneralLedger.MainAccounts AS puc WITH(NOLOCK) ON puc.Id = dmov.IdMainAccount
          LEFT JOIN Payments.AccountPayable AS CXP WITH(NOLOCK) ON C.EntityCode = CXP.Code
     WHERE PUC.Number LIKE '14%'
           AND c.VoucherDate > '01-01-2020'; ---and c.VoucherDate<'31-3-2020' --AND C.EntityName = 'PharmaceuticalDispensing'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movimientos contables de inventario oncológico: consolida los comprobantes contables (vales/asientos) registrados desde enero de 2020 que afectan cuentas del grupo 14 (inventarios según el PUC colombiano). Cruza los movimientos contables con sus detalles de débito y crédito, las cuentas contables principales (PUC) y, cuando el origen del movimiento es una cuenta por pagar a proveedor, resuelve el código de la entidad desde la tabla de cuentas por pagar para mostrar correctamente el NIT o código del tercero. Sirve para reportería contable y de auditoría de inventarios (farmacia, oncología), permitiendo rastrear qué cuenta PUC se afectó, en qué fecha, con qué valor débito/crédito y a qué documento o proveedor corresponde cada movimiento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_MovContableInventario';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_MovContableInventario';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los movimientos contables de inventario (cuentas PUC clase 14) posteriores a 2020-01-01, mostrando débitos/créditos por comprobante y resolviendo el código de la entidad cuando corresponde a cuentas por pagar.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de movimientos contables enlazados a sus comprobantes de diario y a sus líneas de detalle.; El plan de cuentas (MainAccounts) debe estar poblado con números de cuenta tipo PUC.; Para entidades tipo ''AccountPayable'', debe existir un registro en Payments.AccountPayable cuyo Code coincida con el EntityCode del movimiento; de lo contrario se conserva el EntityCode original.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen cuentas contables del grupo PUC ''14%'' (inventarios).; Solo se incluyen comprobantes con fecha estrictamente mayor a 2020-01-01.; El cruce con cuentas por pagar es opcional (LEFT JOIN): no se filtran movimientos sin AP asociada.; Cada fila representa una línea débito/crédito de un comprobante contable asociada a una cuenta PUC de inventario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Movimiento contable; Comprobante de diario (voucher); Plan Único de Cuentas (PUC); Cuentas de inventario (clase 14); Cuentas por pagar; Débito/Crédito contable; Dispensación farmacéutica (referencia comentada)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve solo filas cuya cuenta contable cumpla PUC.Number LIKE ''14%'' (cuentas de inventario) y cuya fecha de comprobante (VoucherDate) sea posterior a 2020-01-01.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.EntityName = ''AccountPayable'' → El EntityCode expuesto se toma de Payments.AccountPayable.EntityCode (con fallback al EntityCode original si es NULL). else Se conserva el EntityCode original del movimiento contable.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.AccountingMovement; GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.MainAccounts; Payments.AccountPayable', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableInventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_MovContableInventario';
GO
