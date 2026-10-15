-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[SP_InsertVacationRequestDetail]
	-- Add the parameters for the stored procedure here
	    @IdVacationRequest INT,         -- IdVacationRequest - int
		@StatusRequest INT,         -- StatusRequest - tinyint
		@UserStatus VARCHAR(20),        -- UserStatus - varchar(20)
		@Comments VARCHAR(500)        -- Comments - varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT HumanTalent.VacationRequestDetail
	(
		IdVacationRequest,
		StatusRequest,
		UserStatus,
		DateStatus,
		Comments
	)
	VALUES
	(   @IdVacationRequest,         -- IdVacationRequest - int
		@StatusRequest,         -- StatusRequest - tinyint
		@UserStatus,        -- UserStatus - varchar(20)
		[Common].[GETDATE](), -- DateStatus - datetime
		@Comments         -- Comments - varchar(500)
    )

	--Update VacationRequest
	UPDATE HumanTalent.VacationRequest SET LastStatus = @StatusRequest WHERE Id = @IdVacationRequest;
END