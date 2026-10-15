

CREATE FUNCTION [dbo].[SplitString_QA] ( @stringToSplit VARCHAR(MAX), @delimiter CHAR(1) )
RETURNS
@returnList TABLE ([Value] [nvarchar] (1000))
AS
BEGIN
    DECLARE @xml XML
    SET @xml = CAST(('<X>' + REPLACE(@stringToSplit, @delimiter, '</X><X>') + '</X>') AS XML)

    INSERT INTO @returnList ([Value])
    SELECT N.value('.', 'NVARCHAR(1000)') AS [Value]
    FROM @xml.nodes('X') AS T(N)
    
    RETURN
END