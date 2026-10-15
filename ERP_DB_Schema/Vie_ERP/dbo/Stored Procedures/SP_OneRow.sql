
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SP_OneRow]
(
@source char(10),
@nameSchema varchar(100),
@nameTable varchar(100),
@column varchar(100),
@conditions varchar(3000)
)

AS
BEGIN

declare @tmp nvarchar(1000)


set @tmp = 'SELECT STRING_AGG(RTRIM('+ @column +'), '' + '') FROM INDIGO' + RTRIM(LTRIM(@source)) + '.' + @nameSchema + '.' + @nameTable + ' WHERE ' + @conditions  execute sp_executesql @tmp


END