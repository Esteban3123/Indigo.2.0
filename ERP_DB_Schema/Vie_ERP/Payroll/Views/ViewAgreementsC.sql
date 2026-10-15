
CREATE VIEW [Payroll].[ViewAgreementsC]
AS
	SELECT	a.Id
		,a.Consecutive
		,a.GroupId
		,g.Code GroupCode
		,g.Name GroupName
		,a.EmployeeId
		,a.CompanyId		
		,cp.Nit CompanyNit
		,cp.Name CompanyName
		,a.ConceptId
		,con.Code AS ConceptCode
		,con.Name AS ConceptName
		,a.KindsAgreementsId
		,a.Comments
		,a.LiquidationType
		,a.TermType
		,a.AgreementValue
		,a.NumberShares
		,a.[State]
		,a.StartingDate
		,a.CurrentBalance
		,a.EndDateSuspend
		,a.CommentChangeState
		,a.PaidVacation
		,a.[TimeStamp]
		,bo.Id AS BranchOfficeID
		,bo.Code AS BranchOfficeCode
		,bo.[Name] AS BranchOffice
	FROM Payroll.AgreementsC AS a WITH (NOLOCK)
	JOIN Payroll.Employee AS e WITH (NOLOCK) ON a.EmployeeId = e.Id 
	JOIN Payroll.Company AS cp WITH (NOLOCK) ON a.CompanyId = cp.Id
	LEFT JOIN Payroll.Concept AS con WITH (NOLOCK) ON a.ConceptId = con.Id
	LEFT JOIN Payroll.[Group] g WITH (NOLOCK) ON a.GroupId = g.Id
	LEFT JOIN Payroll.Contract AS c WITH (NOLOCK) ON e.Id = c.EmployeeId AND c.Status = 1
	LEFT JOIN Payroll.FunctionalUnit AS fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
	LEFT JOIN Payroll.BranchOffice AS bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id