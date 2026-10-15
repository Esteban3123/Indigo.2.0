
CREATE FUNCTION [dbo].[DiferenciaDiasCamas] (@Ingreso char(10), @Paciente varchar(25))
 RETURNS int
 AS
	 BEGIN
	 declare @Dias int, @EstadoActivo int
	 --conocer el estado
	 SELECT @EstadoActivo = count(*) from  CHREGESTA WHERE IPCODPACI =@Paciente AND NUMINGRES =@Ingreso AND REGESTADO ='1'
	 IF @EstadoActivo = 1 
	 BEGIN
	
		SELECT 
				@Dias=	DATEDIFF(DAY, MIN(FECINIEST ),GETDATE()) 
		  FROM 
				CHREGESTA
		 WHERE
				IPCODPACI =@Paciente AND NUMINGRES =@Ingreso
	END
	ELSE
	BEGIN
		SELECT 
				@Dias=	DATEDIFF(DAY, MIN(FECINIEST ),MAX(FECFINEST)) 
		  FROM 
				CHREGESTA
		 WHERE
				IPCODPACI =@Paciente AND NUMINGRES =@Ingreso
	END
	return @Dias
 END