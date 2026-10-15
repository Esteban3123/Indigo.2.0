CREATE PROCEDURE [Integrations].[UpdateBloodComponentsOrder_Mirth_Synch]
(
    @OrderId as int
)
AS
BEGIN
    SET NOCOUNT ON
		update dbo.[HCORHEMCO] set SYNCMIRTH = 1 where ID = @OrderId 
		print '1'
END