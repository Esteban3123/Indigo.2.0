CREATE VIEW [Glosas].[ViewGlosasCordination]
AS
SELECT 
	N0.Id
	,N0.State
	,N0.RadicatedConsecutive
	,c.Nit
	,c.Name
	,N0.DocumentNumber
	,N0.DocumentDate	
FROM(Glosas.GlosaObjectionsReceptionD N1 WITH(NOLOCK)
	INNER JOIN Glosas.GlosaPortfolioGlosada N2 WITH(NOLOCK) ON(N1.PortfolioGlosaId = N2.Id))
	INNER JOIN Glosas.GlosaObjectionsReceptionC N0 ON N0.Id = N1.GlosaObjectionsReceptionCId AND N0.State IN(N'1', N'2')
	INNER JOIN Common.Customer c ON c.Id = N0.CustomerId
	WHERE N2.State IN(N'2', N'3') AND (N1.DocumentType = 1)

UNION

SELECT 
	N0.Id
	,N0.State
	,N0.RadicatedConsecutive
	,c.Nit
	,c.Name
	,N0.DocumentNumber
	,N0.DocumentDate
FROM(Glosas.GlosaObjectionsReceptionD N3 WITH(NOLOCK)
    INNER JOIN Glosas.GlosaPortfolioGlosada N4 WITH(NOLOCK) ON(N3.PortfolioGlosaId = N4.Id))
    INNER JOIN Glosas.GlosaObjectionsReceptionC N0 ON N0.Id = N3.GlosaObjectionsReceptionCId AND N0.State IN(N'1', N'2')
	INNER JOIN Common.Customer c on c.Id = N0.CustomerId
	WHERE N4.State IN(N'5', N'6') AND (N3.DocumentType = 2)