-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [Integrations].[UpdateLaboratoryOrder_Mirth_Synch]
(
    @OrderId as int,
	@TypeOrder as varchar(20)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    if @TypeOrder = 'Ambulatoria' begin
		update dbo.AMBORDLAB set SYNCMIRTH = 1 where AUTO = @OrderId 
		print '1'
	end else if @TypeOrder = 'Hospitalaria' begin
		update dbo.HCORDLABO set SYNCMIRTH = 1 where AUTO = @OrderId 
		print '2'
	end

END
GO
GRANT EXECUTE
    ON OBJECT::[Integrations].[UpdateLaboratoryOrder_Mirth_Synch] TO [MIRTH_LOGIN]
    AS [dbo];

