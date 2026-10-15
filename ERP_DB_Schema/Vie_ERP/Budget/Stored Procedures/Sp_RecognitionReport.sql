-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 26/02/2018
-- Description:	Sp encargado de listar los reconocimientos
-- =============================================
CREATE PROCEDURE [Budget].[Sp_RecognitionReport]
	@InitialDate date,
	@EndDate date,
	@BudgetaryValidityId int,
	@RecognitionCode varchar(20)
AS
BEGIN
	
	SET NOCOUNT ON

    select ROW_NUMBER() OVER(ORDER BY r.Code DESC) as Id,
	r.Code as RecognitionCode, 
	cast(r.DocumentDate as date) as DocumentDate, 
	t.Nit + ' - ' + t.Name as ThirdPartyDescription, 
	c.Code as CategoryCode, 
	c.Name as NameCategory, 
	f.Code + ' - ' + f.Name as FinancialSourceDescription,
	case r.RecognitonType when 1 then 'Reconocimiento' when 2 then 'Cuenta x Cobrar' else 'Vigencia Anterior' end as RecognitionTypeDescription,
	case r.Status when 1 then 'Registrado' when 2 then 'Confirmado' else 'Anulado' end as RecognitionStatus,
	SUM(rd.InitialValue) as InitialValue, 
	ISNULL(SUM(IIF(rmd.Nature = 1, rmd.Value, 0)),0) as DebitValue, 
	ISNULL(SUM(IIF(rmd.Nature = 2, rmd.Value, 0)),0) as CreditValue, 
	SUM(rd.InitialValue) + ISNULL(SUM(IIF(rmd.Nature = 2, rmd.Value, 0)),0) - ISNULL(SUM(IIF(rmd.Nature = 1, rmd.Value, 0)),0) as Balance
	from Budget.Recognition r
	inner join Budget.RecognitionDetail rd on rd.RecognitionId = r.Id
	inner join Common.ThirdParty t on t.Id = r.ThirdPartyId
	inner join Budget.Category c on c.Id = rd.CategoryId
	inner join Budget.FinancialSource f on f.Id = c.FinancialSourceId
	left outer join Budget.RecognitionModificationDetail rmd on rmd.RecognitionDetailId = rd.Id
	left outer join Budget.RecognitionModification rm on rm.Id = rmd.RecognitionModificationId and cast(rm.DocumentDate as date) BETWEEN @InitialDate AND @EndDate And rm.Status = 2
	where cast(r.DocumentDate as date) >= @InitialDate and cast(r.DocumentDate as date) <= @EndDate and r.BudgetaryValidityId = @BudgetaryValidityId and r.Code like @RecognitionCode + '%'
	group by r.Code, r.DocumentDate, t.Nit, t.Name, c.Code, c.Name, r.Status, r.RecognitonType, f.Code, f.Name

END