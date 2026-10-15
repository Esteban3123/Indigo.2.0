-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [StaffPick].[SP_GetInterviewers]
	-- Add the parameters for the stored procedure here
	@Code varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.Id AS EmployeeId, P.FirstName AS FirstName, P.SecondName AS SecondName, P.FirstLastName AS FirstLastName, P.SecondLastName AS SecondLastName FROM [StaffPick].[CallForStaff] CFS
	INNER JOIN [StaffPick].[CallForStaffBossInterview] CFSBI ON CFS.Id = CFSBI.IdCallForStaff
	INNER JOIN [Payroll].[Employee] E ON CFSBI.IdEmployee = E.Id
	INNER JOIN [Common].[ThirdParty] T ON E.ThirdPartyId = T.Id
	INNER JOIN [Common].[Person] P ON T.PersonId = P.Id
	WHERE CFS.Code=@Code

END