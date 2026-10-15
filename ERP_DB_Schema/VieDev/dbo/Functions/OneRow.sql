-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[OneRow]
(
@source char(10),
@nameSchema varchar(100),
@nameTable varchar(100),
@column varchar(100),
@conditions varchar(3000)
)
RETURNS varchar(4000)
AS
BEGIN
declare @temporal varchar(1000)

 exec [dbo].[sp_OneRow] @source, @nameSchema, @nameTable ,@column, @conditions, @temporal

 return @temporal

END