CREATE VIEW Inventory.ViewClosedMonthConciliationMovements AS
SELECT 
    cmmc.Id,
    cmmc.Year,
    cmmc.Month,
    cmmc.EntityName,
    ma.Number AS AccountNumber,
    cmmc.TotalDebitAccounting,
    cmmc.TotalCreditAccounting,
    cmmc.TotalDebitInventory,
    cmmc.TotalCreditInventory,
    cmmc.TotalDebitAccounting - cmmc.TotalDebitInventory AS DifferenceDebit,
    cmmc.TotalCreditAccounting - cmmc.TotalCreditInventory AS DifferenceCredit
FROM 
    Inventory.ClosedMonthModulesConciliation AS cmmc
INNER JOIN 
    GeneralLedger.MainAccounts AS ma 
ON 
    ma.Id = cmmc.MainAccountId;

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la conciliación mensual cerrada entre Contabilidad e Inventario, mostrando totales débito/crédito de ambos módulos y sus diferencias por cuenta contable y entidad.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthConciliationMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir registro de conciliación de cierre mensual con MainAccountId válido referenciado en el plan de cuentas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthConciliationMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen conciliaciones cuya cuenta contable existe en el plan de cuentas (INNER JOIN con MainAccounts).; La diferencia se calcula siempre como Contabilidad menos Inventario (signo positivo indica exceso en Contabilidad).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthConciliationMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación de cierre mensual; Contabilidad; Inventario; Cuenta contable; Débito; Crédito', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthConciliationMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Calcula DifferenceDebit = TotalDebitAccounting - TotalDebitInventory y DifferenceCredit = TotalCreditAccounting - TotalCreditInventory para evidenciar descuadres entre módulos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthConciliationMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ClosedMonthModulesConciliation; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthConciliationMovements';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewClosedMonthConciliationMovements';
GO
