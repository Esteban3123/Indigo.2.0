
CREATE FUNCTION [dbo].[DiagnosticoPorFolioConcatenados](@CodigoPaciente as varchar(25), @NumIngreso as char(10), @NumFolio as varchar(10))
RETURNS varchar(max)
AS
BEGIN
    DECLARE @Diagnosticos as varchar(max)

SELECT 
	@Diagnosticos = STRING_AGG(
		concat(RTRIM(B.CODDIAGNO), ' - ', RTRIM(B.NOMDIAGNO)), ', ')
FROM INDIAGNOH A
LEFT OUTER JOIN INDIAGNOS B ON A.CODDIAGNO = B.CODDIAGNO
WHERE NUMINGRES = @NumIngreso AND IPCODPACI = @CodigoPaciente AND NUMEFOLIO = @NumFolio
    
    RETURN @Diagnosticos
END