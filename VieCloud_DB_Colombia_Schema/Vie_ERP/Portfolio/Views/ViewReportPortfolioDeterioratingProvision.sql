CREATE VIEW [Portfolio].[ViewReportPortfolioDeterioratingProvision]
AS

SELECT ppd.Id, pp.CreationUser, pp.Code, pp.DocumentDate, pp.CourtDate, IIF(pp.DocumentType = 1,'Provisión','Deterioro') AS DocumentType, pp.[Description], CONCAT('Edad: De ', ap.InitialRange,' a ', ap.EndRange) AS AgeRange, ppd.InvoiceNumber, ppd.BalanceAccountReceivable, ppd.ConfirmDateAccountReceivable, ppd.Percentage, ppd.Value FROM 
[Portfolio].[PortfolioProvisionDetail] AS ppd
INNER JOIN  [Portfolio].[PortfolioProvision] AS pp ON pp.Id = ppd.PortfolioProvisionId
INNER JOIN  [Portfolio].[AgesPortfolio] AS ap ON ap.Id = ppd.AgesId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de provisiones y deterioro de cartera por factura. Integra el encabezado del documento de provisión (código, fecha, tipo —Provisión o Deterioro—, fecha judicial y descripción), el rango de envejecimiento de la deuda (aging: tramo de días de vencimiento) y el detalle línea a línea de cada factura incluida en el cálculo, mostrando el saldo pendiente por cobrar, la fecha de confirmación de la cuenta por cobrar, el porcentaje aplicado y el valor provisionado o deteriorado. Sirve para reportes contables y de gestión de cartera que requieren visualizar el estado de cuentas de difícil cobro clasificadas por antigüedad de la deuda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewReportPortfolioDeterioratingProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'ViewReportPortfolioDeterioratingProvision';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para reportes, el detalle de provisiones/deterioros de cartera combinando encabezado del documento, línea de factura provisionada y el rango de aging aplicado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportPortfolioDeterioratingProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el encabezado en Portfolio.PortfolioProvision referenciado por el detalle.; Cada detalle debe tener un rango de aging válido en Portfolio.AgesPortfolio.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportPortfolioDeterioratingProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada línea de detalle de provisión está siempre asociada a un encabezado de provisión existente (INNER JOIN con PortfolioProvision).; Cada línea de detalle siempre tiene un rango de edad de cartera (aging) asignado (INNER JOIN con AgesPortfolio).; El tipo de documento se clasifica de forma binaria: 1 = Provisión; cualquier otro valor = Deterioro.; El rango de edad se presenta siempre en el formato ''Edad: De {InitialRange} a {EndRange}''.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportPortfolioDeterioratingProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Provisión de cartera; Deterioro de cartera; Cuentas por cobrar; Aging (rangos de edad de cartera); Factura; Saldo de cuenta por cobrar', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportPortfolioDeterioratingProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.PortfolioProvisionDetail: Devuelve un registro por cada detalle de provisión que tenga encabezado y rango de edad asociados, mostrando DocumentType como ''Provisión'' si pp.DocumentType = 1 y ''Deterioro'' en caso contrario.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportPortfolioDeterioratingProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pp.DocumentType = 1 → Se etiqueta el documento como ''Provisión'' else Se etiqueta como ''Deterioro''', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportPortfolioDeterioratingProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioProvisionDetail; Portfolio.PortfolioProvision; Portfolio.AgesPortfolio', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportPortfolioDeterioratingProvision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'ViewReportPortfolioDeterioratingProvision';
GO
