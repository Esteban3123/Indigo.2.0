
CREATE function [dbo].[DiagnosticoPrincipal] (@NumIngreso as char(10), @CodigoPaciente as varchar(25))
RETURNS nvarchar (60)
AS
BEGIN
declare @DiagnosticoPrincipal as varchar(100)
	SELECT 
		 @DiagnosticoPrincipal =	RTRIM(B.CODDIAGNO) + ' - ' + RTRIM(B.NOMDIAGNO  )
		FROM  
			INDIAGNOP  A
			LEFT OUTER JOIN 
			INDIAGNOS B
			ON A.CODDIAGNO = B.CODDIAGNO 
		WHERE 
			NUMINGRES = @NumIngreso AND IPCODPACI =@CodigoPaciente AND CODDIAPRI = 1 
		
			ORDER BY A.FECDIAGNO DESC

 return @DiagnosticoPrincipal
END