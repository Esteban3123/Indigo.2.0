/*==============================================================================================================================
	Author: Julieth Cardenas
	BUG : 19106
	Sprint : ERP_Services\VF_week_30-32 (2024)
	==============================================================================================================================*/
CREATE TRIGGER [Treasury].[tgg_VoucherTransaction_ValidateMainAccount]
ON [Treasury].[VoucherTransaction]
AFTER INSERT, UPDATE
AS 
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM INSERTED vt
        JOIN Treasury.EntityBankAccounts eba 
            ON eba.Id = vt.IdEntityBankAccount
        WHERE vt.IdMainAccount <> eba.IdMainAccount
    )
    BEGIN
        ROLLBACK;
        THROW 51000, 'Error generado por control desde trigger. La cuenta contable de la cuenta bancaria no es igual a la que esta parametrizada.', 1;
    END;

    IF EXISTS (
        SELECT 1
        FROM INSERTED vt
        JOIN Treasury.CashRegisters cr 
            ON cr.Id = vt.IdCashRegister
        WHERE vt.IdMainAccount <> cr.IdMainAccount
    )
    BEGIN
        ROLLBACK;
        THROW 51000, 'Error generado por control desde trigger. La cuenta contable de la caja no es igual a la que esta parametrizada.', 1;
    END;

END;
