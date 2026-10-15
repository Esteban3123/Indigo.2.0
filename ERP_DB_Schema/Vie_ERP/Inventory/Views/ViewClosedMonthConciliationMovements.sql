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