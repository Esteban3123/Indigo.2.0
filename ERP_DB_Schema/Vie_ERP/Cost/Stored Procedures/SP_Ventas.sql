
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Cost].[SP_Ventas] 
	-- Add the parameters for the stored procedure here
	@Numero_cuenta1 int,
	@Numero_cuenta2 int,
	@Numero_cuenta3 int,
	@Libro int,
	@Tp_Persona int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT t.Nit, t.Name AS Cliente, cco.Code AS CentroCosto, CASE sc.Month WHEN '1' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Enero 2016], CASE WHEN sc.Month = '2' AND sc.Year = '2016' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Febrero 2016], 
           CASE sc.Month WHEN '3' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Marzo 2016], CASE sc.Month WHEN '4' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Abril 2016], CASE sc.Month WHEN '5' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Mayo 2016], 
           CASE sc.Month WHEN '6' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Junio 2016], CASE sc.Month WHEN '7' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Julio 2016], CASE sc.Month WHEN '8' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Agosto 2016], 
           CASE sc.Month WHEN '9' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Septiembre 2016], CASE sc.Month WHEN '10' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Octubre 2016], CASE sc.Month WHEN '11' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) 
           * - 1 END AS [Noviembre 2016], CASE sc.Month WHEN '12' THEN ((SUM(sc.DebitValue) - SUM(sc.CreditValue))) * - 1 END AS [Diciembre 2016], 
           CASE WHEN cco.Code LIKE 'N%' THEN 'Neiva' WHEN cco.Code LIKE 'T%' THEN 'Tunja' WHEN cco.Code LIKE 'P%' THEN 'Pitalito' WHEN cco.Code LIKE 'F%' THEN 'Florencia' END AS Sede
FROM   GeneralLedger.GeneralLedgerBalance AS sc WITH (nolock) INNER JOIN
           Common.ThirdParty AS t ON t.Id = sc.IdThirdParty LEFT OUTER JOIN
           Payroll.CostCenter AS cco ON cco.Id = sc.IdCostCenter LEFT OUTER JOIN
           GeneralLedger.MainAccounts AS c ON c.Id = sc.IdMainAccount
WHERE (c.Number BETWEEN @Numero_cuenta1 AND @Numero_cuenta2) AND (c.Number <> @Numero_cuenta3) AND (t.PersonType = @Tp_Persona) AND (c.LegalBookId = @Libro)
GROUP BY t.Nit, t.Name, cco.Code, sc.Month, sc.Year
END