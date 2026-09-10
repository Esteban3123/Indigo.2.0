

CREATE VIEW [GeneralLedger].[VBalanceMainAccount]
as
select glb.[Year] as YearBalance, glb.[Month] as MonthBalance, sum(glb.DebitValue) as DebitValue, sum(glb.CreditValue) as CreditValue, ma.Number as NumberAccount, mac.Nature as Nature, ma.LegalBookId as LegalBookId, ma.Id as IdAccount  
from GeneralLedger.GeneralLedgerBalance glb with (nolock) 
inner join GeneralLedger.MainAccounts ma with (nolock)  on ma.Id = glb.IdMainAccount 
inner join GeneralLedger.MainAccountClasses mac with (nolock)  on mac.Id = ma.IdAccountClass
group by glb.Year,glb.Month,ma.Number,mac.Nature,ma.LegalBookId,ma.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Balance mensual por cuenta contable principal del libro mayor. Consolida los movimientos débito y crédito de cada cuenta mayor agrupados por año y mes, cruzando el plan de cuentas con su clase contable para obtener la naturaleza (débito o crédito) de cada cuenta. Sirve para reportería contable, cierre de período y análisis del balance general, mostrando el número de cuenta, el libro legal al que pertenece y los totales acumulados de cada período.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'VBalanceMainAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'VIEW', @level1name = N'VBalanceMainAccount';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los saldos contables (débito y crédito) por período (año/mes) y cuenta principal, exponiendo la naturaleza de la clase y el libro legal asociado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VBalanceMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas en GeneralLedgerBalance deben existir en MainAccounts.; Cada cuenta principal debe tener una clase válida en MainAccountClasses.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VBalanceMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo incluye cuentas que existen en el plan de cuentas (MainAccounts) y tengan una clase asociada en MainAccountClasses, por el uso de INNER JOIN.; Los valores de débito y crédito se totalizan agrupados por año, mes, cuenta, naturaleza y libro legal.; La naturaleza expuesta corresponde a la clase de la cuenta principal, no a la cuenta misma.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VBalanceMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Saldo contable; Cuenta principal (PUC); Naturaleza de cuenta (débito/crédito); Libro legal; Período contable (año/mes); Débito; Crédito', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VBalanceMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la suma de DebitValue y CreditValue agrupada por Year, Month, Number de cuenta, Nature de la clase, LegalBookId e Id de cuenta.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VBalanceMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VBalanceMainAccount';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'VIEW', @level1name=N'VBalanceMainAccount';
GO
