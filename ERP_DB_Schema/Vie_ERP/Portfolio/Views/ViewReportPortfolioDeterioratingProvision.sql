CREATE VIEW [Portfolio].[ViewReportPortfolioDeterioratingProvision]
AS

SELECT ppd.Id, pp.CreationUser, pp.Code, pp.DocumentDate, pp.CourtDate, IIF(pp.DocumentType = 1,'Provisión','Deterioro') AS DocumentType, pp.[Description], CONCAT('Edad: De ', ap.InitialRange,' a ', ap.EndRange) AS AgeRange, ppd.InvoiceNumber, ppd.BalanceAccountReceivable, ppd.ConfirmDateAccountReceivable, ppd.Percentage, ppd.Value FROM 
[Portfolio].[PortfolioProvisionDetail] AS ppd
INNER JOIN  [Portfolio].[PortfolioProvision] AS pp ON pp.Id = ppd.PortfolioProvisionId
INNER JOIN  [Portfolio].[AgesPortfolio] AS ap ON ap.Id = ppd.AgesId