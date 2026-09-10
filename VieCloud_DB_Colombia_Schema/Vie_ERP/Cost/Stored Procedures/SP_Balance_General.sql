

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Cost].[SP_Balance_General] 
	-- Add the parameters for the stored procedure here
	@Numero_cuenta1 int,
	@Numero_cuenta2 int,
	@Libro int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT c.Number AS Auxiliar, cco.Code AS CentroCosto, c.Name AS [Nombre auxiliar], cl.Code AS CodClase, cl.Name AS Clase, LEFT(c.Number, 2) AS Grupo,
               (SELECT Name
              FROM   GeneralLedger.MainAccounts
              WHERE (Number = LEFT(c.Number, 2)) AND (LegalBookId = '1')) AS [Nombre Grupo], LEFT(c.Number, 4) AS Cuenta,
               (SELECT Name
              FROM   GeneralLedger.MainAccounts AS MainAccounts_1
              WHERE (Number = LEFT(c.Number, 4)) AND (LegalBookId = '1')) AS [Nombre Cuenta], LEFT(c.Number, 6) AS SubCuenta,
               (SELECT Name
              FROM   GeneralLedger.MainAccounts AS MainAccounts_1
              WHERE (Number = LEFT(c.Number, 6)) AND (LegalBookId = '1')) AS [Nombre SubCuenta], t .Nit, t .Name AS [Nombre tercero], CASE WHEN sc.Month = '13' AND sc.Year = '2015' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) 
           - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS [Saldos Iniciales], CASE WHEN sc.Month = '1' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, 
           IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) 
           END AS Enero2016, CASE WHEN sc.Month = '2' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) 
           AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Febrero2016, CASE WHEN sc.Month = '3' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) 
           - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Marzo2016, CASE WHEN sc.Month = '4' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, 
           IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) 
           END AS Abril2016, CASE WHEN sc.Month = '5' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) 
           AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Mayo2016, CASE WHEN sc.Month = '6' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) 
           - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Junio2016, CASE WHEN sc.Month = '7' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, 
           IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) 
           END AS Julio2016, CASE WHEN sc.Month = '8' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) 
           AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Agosto2016, CASE WHEN sc.Month = '9' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) 
           - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Septiembre2016, CASE WHEN sc.Month = '10' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, 
           IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) 
           END AS Octubre2016, CASE WHEN sc.Month = '11' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) 
           - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Noviembre2016, CASE WHEN sc.Month = '12' AND sc.Year = '2016' THEN IIF(cl.Nature <> c.Nature, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), 
           CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY)) * - 1, IIF(c.Nature = 1, CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY), CAST((SUM(sc.CreditValue) - SUM(sc.DebitValue)) AS MONEY))) * IIF(cl.Nature = 2, - 1, 1) END AS Diciembre2016
FROM   GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) LEFT OUTER JOIN
           Common.ThirdParty AS t ON t .Id = sc.IdThirdParty LEFT OUTER JOIN
           GeneralLedger.MainAccounts AS c ON c.Id = sc.IdMainAccount INNER JOIN
           GeneralLedger.MainAccountClasses AS cl ON cl.Id = c.IdAccountClass LEFT OUTER JOIN
           Payroll.CostCenter AS cco ON cco.Id = sc.IdCostCenter
WHERE (c.Number BETWEEN @Numero_cuenta1 AND @Numero_cuenta2) AND (c.LegalBookId = @Libro)
GROUP BY c.Number, t .Nit, t .Name, cco.Code, sc.Month, c.Name, cl.Code, cl.Name, sc.Year, c.Nature, cl.Nature
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el Balance General contable de la organización desglosado por mes, mostrando los saldos de cada cuenta auxiliar del plan contable (auxiliar, subcuenta, cuenta, grupo y clase) junto con el centro de costo y el tercero (NIT y nombre) asociados a cada movimiento. Recibe como parámetros un rango de números de cuenta y el libro contable a consultar, y calcula los saldos débito/crédito mes a mes respetando la naturaleza de cada cuenta (débito o crédito) para presentar columnas de saldo inicial y un saldo por cada mes del año. Integra los saldos del libro mayor (GeneralLedgerBalance), la jerarquía de cuentas contables (MainAccounts y MainAccountClasses), los centros de costo de nómina (CostCenter) y los datos de terceros como proveedores o aseguradoras (ThirdParty), siendo el insumo principal para reportes financieros, estados de situación financiera y cierre contable.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_Balance_General';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_Balance_General';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un balance general por auxiliar, tercero y centro de costo, mostrando saldo inicial (cierre 2015) y movimientos mensuales de 2016, ajustando signos según la naturaleza débito/crédito de la cuenta y su clase.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Balance_General';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el libro contable indicado en GeneralLedger.MainAccounts.LegalBookId; Debe existir el libro legal ''1'' poblado en MainAccounts para resolver nombres de Grupo/Cuenta/SubCuenta; El rango de cuentas debe estar expresado numéricamente compatible con MainAccounts.Number', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Balance_General';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran cuentas dentro del rango [Numero_cuenta1, Numero_cuenta2] del libro contable indicado; Los nombres de Grupo (2 dígitos), Cuenta (4 dígitos) y SubCuenta (6 dígitos) se obtienen siempre del libro legal ''1'' (LegalBookId=1), independiente del libro consultado; El cierre del año previo se identifica con el período especial mes=13/año=2015 y se presenta como saldo inicial; El signo del saldo se ajusta según la naturaleza de la cuenta y de su clase (débito=1, crédito=2); Las columnas mensuales solo muestran datos para el año 2016 (Enero a Diciembre)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Balance_General';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Balance general; Plan único de cuentas (PUC); Naturaleza débito/crédito; Saldos iniciales; Tercero (NIT); Centro de costo; Libro legal contable; Clase de cuenta', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Balance_General';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un resultset con auxiliar, centro de costo, jerarquía de cuenta (Grupo/Cuenta/SubCuenta y nombres del libro 1), tercero y saldos por período (saldo inicial mes 13/2015 y meses 1..12 de 2016) filtrado por rango de cuentas y libro legal', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Balance_General';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sc.Month=''13'' AND sc.Year=''2015'' → El saldo calculado se reporta en la columna ''Saldos Iniciales''; si sc.Month=N AND sc.Year=''2016'' (N=1..12) → El saldo calculado se reporta en la columna del mes correspondiente de 2016 (Enero2016..Diciembre2016); si c.Nature = 1 (naturaleza débito) → Saldo = SUM(DebitValue) - SUM(CreditValue) else Saldo = SUM(CreditValue) - SUM(DebitValue); si cl.Nature <> c.Nature (naturaleza de la clase distinta a la de la cuenta) → El saldo se multiplica por -1 para invertir el signo; si cl.Nature = 2 (naturaleza crédito en la clase) → El resultado final se multiplica por -1', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Balance_General';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Common.ThirdParty; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Balance_General';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Balance_General';
-- GO
