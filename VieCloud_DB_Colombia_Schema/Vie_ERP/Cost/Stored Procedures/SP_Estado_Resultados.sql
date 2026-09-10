
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Cost].[SP_Estado_Resultados] 
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
	SELECT CASE WHEN cco.Code LIKE 'N%' THEN 'Neiva' WHEN cco.Code LIKE 'F%' THEN 'Florencia' WHEN cco.Code LIKE 'T%' THEN 'Tunja' WHEN cco.Code LIKE 'P%' THEN 'Pitalito' WHEN cco.Code LIKE 'K%' THEN 'Facatativá' WHEN cco.Code LIKE 'M%' THEN 'Nacional' END AS Sede, RTRIM(c.Number) + RTRIM(cco.Code) 
           AS CuentaCentroCosto, c.Number AS Auxiliar, cco.Code AS CCosto, cco.Name AS [Centro Costo], c.Name AS [Nombre auxiliar], cl.Code AS CodClase, cl.Name AS Clase, LEFT(c.Number, 2) AS Grupo,
               (SELECT Name
              FROM   GeneralLedger.MainAccounts
              WHERE (Number = LEFT(c.Number, 2)) AND (LegalBookId = '1')) AS [Nombre Grupo], LEFT(c.Number, 4) AS Cuenta,
               (SELECT Name
              FROM   GeneralLedger.MainAccounts AS MainAccounts_1
              WHERE (Number = LEFT(c.Number, 4)) AND (LegalBookId = '1')) AS [Nombre Cuenta], LEFT(c.Number, 6) AS SubCuenta,
               (SELECT Name
              FROM   GeneralLedger.MainAccounts AS MainAccounts_1
              WHERE (Number = LEFT(c.Number, 6)) AND (LegalBookId = '1')) AS [Nombre SubCuenta], t.Nit, t.Name AS [Nombre tercero], CASE WHEN sc.Month = '1' AND sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Enero2016, CASE WHEN sc.Month = '2' AND 
           sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Febrero2016, CASE WHEN sc.Month = '3' AND sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Marzo2016, CASE WHEN sc.Month = '4' AND 
           sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Abril2016, CASE WHEN sc.Month = '5' AND sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Mayo2016, CASE WHEN sc.Month = '6' AND 
           sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Junio2016, CASE WHEN sc.Month = '7' AND sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Julio2016, CASE WHEN sc.Month = '8' AND 
           sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Agosto2016, CASE WHEN sc.Month = '9' AND sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Septiembre2016, CASE WHEN sc.Month = '10' AND 
           sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Octubre2016, CASE WHEN sc.Month = '11' AND sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Noviembre2016, CASE WHEN sc.Month = '12' AND 
           sc.Year = '2016' THEN CAST((SUM(sc.DebitValue) - SUM(sc.CreditValue)) AS MONEY) END AS Diciembre2016
FROM   GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) LEFT OUTER JOIN
           Common.ThirdParty AS t WITH (nolock) ON t.Id = sc.IdThirdParty LEFT OUTER JOIN
           GeneralLedger.MainAccounts AS c WITH (nolock) ON c.Id = sc.IdMainAccount INNER JOIN
           GeneralLedger.MainAccountClasses AS cl WITH (nolock) ON cl.Id = c.IdAccountClass LEFT OUTER JOIN
           Payroll.CostCenter AS cco WITH (nolock) ON cco.Id = sc.IdCostCenter
WHERE (c.Number BETWEEN @Numero_cuenta1 AND @Numero_cuenta2) AND (c.LegalBookId = @Libro)
GROUP BY c.Number, t.Nit, t.Name, cco.Code, sc.Month, c.Name, cl.Code, cl.Name, sc.Year, cco.Name
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el Estado de Resultados contable de la organización, desglosado mes a mes para el año 2016, filtrando por un rango de cuentas auxiliares y un libro contable legal específicos. Cruza los saldos del libro mayor (débitos y créditos) con las cuentas principales, clases de cuenta, centros de costo del módulo de nómina y terceros (proveedores, aseguradoras, etc.), para presentar ingresos y gastos por auxiliar contable, subcuenta, cuenta, grupo, clase, centro de costo y tercero. Clasifica cada registro según la sede de la organización (Neiva, Florencia, Tunja, Pitalito, Facatativá o Nacional) a partir del código del centro de costo. Es usado para reportería financiera y análisis de rentabilidad por área o dependencia.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_Estado_Resultados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_Estado_Resultados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de Estado de Resultados mensualizado (12 meses del año 2016) por cuenta contable, tercero y centro de costo, clasificando además la sede según el prefijo del código del centro de costo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Estado_Resultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de cuentas contables debe existir en GeneralLedger.MainAccounts para el libro legal indicado; El libro contable (LegalBookId) debe corresponder al filtro aplicado y a ''1'' para resolver nombres de grupo/cuenta/subcuenta; Deben existir saldos en GeneralLedger.GeneralLedgerBalance asociados a cuentas, terceros y centros de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Estado_Resultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor mensual reportado siempre se calcula como SUM(DebitValue) - SUM(CreditValue); Los nombres de Grupo (2 dígitos), Cuenta (4 dígitos) y SubCuenta (6 dígitos) se resuelven siempre contra el libro legal ''1'', independientemente del libro consultado; Solo se reportan valores del año 2016; cualquier otro año queda en NULL en todas las columnas mensuales; La sede se deriva exclusivamente de la primera letra del código del centro de costo; El identificador combinado de cuenta-centro de costo concatena Number y Code sin espacios sobrantes', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Estado_Resultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estado de Resultados; Plan Único de Cuentas (PUC); Centro de costo; Tercero; Sede; Libro contable legal; Saldo contable mensual (débito/crédito); Clase de cuenta; Auxiliar contable', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Estado_Resultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas agrupadas por cuenta, tercero, centro de costo, mes y año con la diferencia SUM(DebitValue)-SUM(CreditValue) distribuida en columnas Enero2016..Diciembre2016 cuando sc.Year=''2016'' y sc.Month coincide con el mes correspondiente', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Estado_Resultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cco.Code LIKE ''N%'' → Sede = ''Neiva''; si cco.Code LIKE ''F%'' → Sede = ''Florencia''; si cco.Code LIKE ''T%'' → Sede = ''Tunja''; si cco.Code LIKE ''P%'' → Sede = ''Pitalito''; si cco.Code LIKE ''K%'' → Sede = ''Facatativá''; si cco.Code LIKE ''M%'' → Sede = ''Nacional''; si sc.Month = N AND sc.Year = ''2016'' (para N=1..12) → Se calcula el valor neto del mes (débito - crédito) y se ubica en la columna del mes correspondiente; en caso contrario la celda queda en NULL', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Estado_Resultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; Common.ThirdParty; GeneralLedger.MainAccounts; GeneralLedger.MainAccountClasses; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Estado_Resultados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Estado_Resultados';
-- GO
