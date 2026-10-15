
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
create FUNCTION [dbo].[CleanSpecialChars]
(
	@Cadena varchar(400)
)
RETURNS VARCHAR(400)
AS
BEGIN
-- Declare the return variable here
	DECLARE @resultado varchar(400)

	-- Add the T-SQL statements to compute the return value here
	set @resultado = REPLACE(@Cadena,'Ñ','N');
    set @resultado = REPLACE(@resultado,'á','a');
    set @resultado = REPLACE(@resultado,'Á','A');
    set @resultado = REPLACE(@resultado,'é','e');
    set @resultado = REPLACE(@resultado,'É','E');
    set @resultado = REPLACE(@resultado,'í','i');
    set @resultado = REPLACE(@resultado,'Í','I');
    set @resultado = REPLACE(@resultado,'ó','o');
    set @resultado = REPLACE(@resultado,'Ó','O');
    set @resultado = REPLACE(@resultado,'ú','u');
    set @resultado = REPLACE(@resultado,'Ú','U');	

	DECLARE @IncorrectCharLoc SMALLINT
	SET @IncorrectCharLoc = PATINDEX('%[^0-9A-Za-z ]%', @resultado)
	WHILE @IncorrectCharLoc > 0	BEGIN
		SET @resultado = STUFF(@resultado, @IncorrectCharLoc, 1, '')
		SET @IncorrectCharLoc = PATINDEX('%[^0-9A-Za-z ]%', @resultado)
	END

	set @resultado = @resultado
	-- Return the result of the function
	
	RETURN UPPER(@resultado)
END