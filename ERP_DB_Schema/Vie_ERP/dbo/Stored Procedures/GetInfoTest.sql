-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetInfoTest]
	@file as xml
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		declare @field1 varchar(100), @field2 varchar(100)
		declare @testing table (Id int , ServiceName varchar(150), invoicedValue money)
		declare @tableAVG table(Id int, Percentage decimal(5,2))
		declare @aux nvarchar(max) =  'select top 100 Id, ServiceName, invoicedValue from Glosas.GlosaInvoiceDetail'
		insert into @testing
		exec sp_executesql @aux
		insert into @tableAVG
		select Id, (invoicedValue * 100 / (select SUM(invoicedValue) from @testing))
		from @testing

		--select * from @testing

		select @field1 = t.x.value('field1[1]','varchar(100)') from @file.nodes('/test/file') t(x)

		select @field1

END