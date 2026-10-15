
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