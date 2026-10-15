-- =============================================
-- Author:		Sergio Abraham Fernandez Cruz
-- Create date: 07-07-2014
-- Description:	sp para obtener todos los comprobantes contables por estado
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_GetJournalVouchersByStatus]
	@Status as int,
	@Month as int,
	@Year as int	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT jv.Id as Id,CAST(Jv.Consecutive as varchar(20)) as Code, COALESCE(jv.EntityName, '') as EntityName, Jv.VoucherDate as DocumentDate, COALESCE(Jv.Detail,'') as Detail, CAST(0 AS bit) as State,1 as DocumentType ,'Accounting' as Origin 
	from GeneralLedger.JournalVouchers as Jv where (Jv.Status = @Status and  month(Jv.VoucherDate ) = @Month and YEAR (Jv.VoucherDate ) = @Year)
	union 
	select tc.id as Id,tc.DocumentNumber as Code,'' as EntityName,tc.DocumentDate as DocumentDate,'' as Detail, CAST(0 AS bit) as State ,tc.DocumentType as DocumentType , 'TreasuryControl' as Origin
	from Treasury.TreasuryControl as tc where (month(tc.DocumentDate  ) = @Month and YEAR (tc.DocumentDate ) = @Year)
	union 
	select pc.id as Id,pc.DocumentNumber as Code,'' as EntityName,pc.DocumentDate as DocumentDate,'' as Detail, CAST(0 AS bit) as State ,pc.DocumentType as DocumentType ,'PortfolioControl' as Origin
	from Portfolio.PortfolioControl as pc where (month(pc.DocumentDate  ) = @Month and YEAR (pc.DocumentDate ) = @Year)
	union 
	select pc.id as Id,pc.DocumentNumber as Code,'' as EntityName,pc.DocumentDate as DocumentDate,'' as Detail, CAST(0 AS bit) as State ,pc.DocumentType as DocumentType , 'PaymentsControl' as Origin
	from Payments.PaymentsControl as pc where (month(pc.DocumentDate  ) = @Month and YEAR (pc.DocumentDate ) = @Year)
END