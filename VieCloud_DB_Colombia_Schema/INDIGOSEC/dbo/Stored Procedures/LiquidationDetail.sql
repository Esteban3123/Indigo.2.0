-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LiquidationDetail]
	-- Add the parameters for the stored procedure here
	@PayrollId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Payroll].[LiquidationDetail].[PayrollDate], [Payroll].[LiquidationDetail].[ConceptTotalValue],
	[Payroll].[LiquidationDetail].[AccruedValue], [Payroll].[LiquidationDetail].[DeductedValue] 
	FROM [Payroll].[LiquidationDetail] WHERE [Payroll].[LiquidationDetail].[PayrollId] = @PayrollId;
END
