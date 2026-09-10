
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de ventas mensuales por cliente y centro de costo, desglosado mes a mes para el año 2016. Consulta los saldos contables del libro mayor filtrando por un rango de cuentas contables, tipo de persona (natural o jurídica) y libro contable legal, cruzando con los datos del tercero (NIT y nombre del cliente) y el centro de costo de nómina para identificar la sede (Neiva, Tunja, Pitalito o Florencia). El valor de cada mes se calcula como la diferencia invertida entre débitos y créditos, útil para analizar ingresos por ventas distribuidos geográficamente y por cliente a lo largo del año.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_Ventas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_Ventas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de ventas por tercero, centro de costo y sede, distribuyendo los saldos contables (débito menos crédito invertido) por mes del año 2016.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Ventas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas contables a consultar deben estar dentro del rango definido y excluir una cuenta específica; El tercero debe corresponder al tipo de persona indicado; Las cuentas deben pertenecer al libro legal indicado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Ventas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor presentado siempre se invierte multiplicando por -1 (convención de cuentas de ingreso/ventas con saldo crédito); Solo se consideran terceros del tipo de persona parametrizado; Se excluye siempre la cuenta indicada en el tercer parámetro del rango; La agrupación se realiza por tercero, centro de costo, mes y año', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Ventas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ventas; Tercero/Cliente (Nit); Centro de costo; Cuenta contable; Libro legal/contable; Saldo débito y crédito; Tipo de persona; Sede (Neiva, Tunja, Pitalito, Florencia); Periodo contable mensual', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Ventas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve por tercero (Nit, Nombre) y centro de costo el valor neto mensual calculado como (SUM(DebitValue)-SUM(CreditValue))*-1, abriendo una columna por cada mes de 2016', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Ventas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si sc.Month = ''1''..''12'' (CASE por cada mes) → Asigna el valor neto invertido a la columna correspondiente del mes (Enero..Diciembre 2016); si cco.Code LIKE ''N%'' / ''T%'' / ''P%'' / ''F%'' → Asigna sede Neiva / Tunja / Pitalito / Florencia respectivamente else Sede queda en NULL; si Mes = 2 además requiere Year = ''2016'' → Solo en febrero se valida explícitamente que el año sea 2016', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Ventas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; Common.ThirdParty; Payroll.CostCenter; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Ventas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_Ventas';
-- GO
