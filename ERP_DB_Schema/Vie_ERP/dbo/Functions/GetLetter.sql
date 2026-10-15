
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  FUNCTION [dbo].[GetLetter]
(	
	-- Add the parameters for the function here
	@ScheduleDetailId int
)
RETURNS Char(1) 

Begin

declare @Letter char(1)

	-- Add the SELECT statement with parameter references here
	 (select @letter = letter from Payroll.ScheduleDetail where id = @ScheduleDetailId )

	 return @Letter

end