CREATE FUNCTION [dbo].[udf_StripHTML3] (@HTMLText VARCHAR(MAX))
RETURNS VARCHAR(MAX) AS
BEGIN
    DECLARE @Contador INT
    DECLARE @LongCadena INT
    DECLARE @EnTag INT
    DECLARE @Res VARCHAR(MAX)
    DECLARE @TeHTML INT

   DECLARE @Start INT  
DECLARE @End INT  
DECLARE @Length INT  
SET @Start = CHARINDEX('<BODY>',@HTMLText)  
SET @End = CHARINDEX('</BODY>',@HTMLText,CHARINDEX('<',@HTMLText))  
SET @Length = (@End - @Start) + 1  
SET  @HTMLText = SUBSTRING ( @HTMLText ,@Start , @End )
return @HTMLText

END