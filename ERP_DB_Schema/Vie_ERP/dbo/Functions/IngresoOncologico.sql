

CREATE FUNCTION [dbo].[IngresoOncologico] 
(
	@Identificacion As varchar(25)
)
RETURNS varchar(10)
AS
Begin
		declare @Ingreso as varchar(10)

		--Consultamos el Ingreso con el que esta hospitalizado el paciente
		IF exists(  SELECT RTRIM(I.NUMINGRES)  FROM dbo.ADINGRESO I WITH(NOLOCK) 
								INNER JOIN dbo.CHREGESTA R WITH(NOLOCK) ON R.NUMINGRES = I.NUMINGRES  AND R.REGESTADO = 1
					WHERE I.IPCODPACI = @Identificacion 
				  ) 
		BEGIN
		--Asignamos el Ingreso con el que esta hospitalizado el paciente
					  SELECT @Ingreso = RTRIM(I.NUMINGRES) FROM dbo.ADINGRESO I WITH(NOLOCK) 
							 INNER JOIN dbo.CHREGESTA R WITH(NOLOCK) ON R.NUMINGRES = I.NUMINGRES  AND R.REGESTADO = 1
					  WHERE I.IPCODPACI = @Identificacion 
				 end  
		else BEGIN 
		--Asignamos el Ingreso que esta como oncologico en el ingreso que debe ser 1 
					  SELECT TOP 1 @Ingreso = NUMINGRES FROM ADINGRESO WHERE IPCODPACI = @Identificacion AND TRATAESPECIA = 3 AND IESTADOIN IN ('','P')
				
		end
				Return @Ingreso 
end