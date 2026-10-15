
CREATE FUNCTION [dbo].[IngresoconEgresoCama] (@Cedula as varchar(25), @Ingreso as varchar(10))
RETURNS VARCHAR(10)
AS
BEGIN
declare @Compara varchar(10);
declare @IngresoFinal varchar(10)

   select @Compara=(SELECT MAX(NUMINGRES) AS NUMINGRES FROM CHREGEGRE WHERE IPCODPACI=@CEDULA GROUP BY IPCODPACI);
    IF @Ingreso = @Compara 
	      begin
		  SELECT 	@IngresoFinal=D.NUMINGRES
	   	   FROM   (SELECT C.IPCODPACI AS IPCODPACI, MIN(A.FECINIEST) AS FECINIEST, C.NUMINGRES AS NUMINGRES
                     FROM  CHREGESTA  AS A  INNER JOIN 
		             (SELECT MAX(numingres) AS numingres, ipcodpaci AS ipcodpaci
		                      from CHREGESTA where IPCODPACI = @Cedula
					  GROUP BY IPCODPACI) AS C ON @Cedula= A.IPCODPACI AND C.NUMINGRES = A.NUMINGRES GROUP BY C.IPCODPACI,C.NUMINGRES) as D INNER JOIN
		     (Select row_number()over (PARTITION by ipcodpaci order by ipcodpaci) as Autonumerico,IPCODPACI,FECEGRESO, MAX(NUMINGRES) AS NUMINGRES
                    from CHREGEGRE  where IPCODPACI = @Cedula
                    GROUP BY IPCODPACI,FECEGRESO,numingres) AS E ON E.Autonumerico = dbo.AutonumericoEgresoCama(@Cedula) and @Cedula = D.IPCODPACI
	                WHERE convert(date,D.FECINIEST,103)  BETWEEN convert(date,E.FECEGRESO,103)  AND  convert(date,E.FECEGRESO +15,103)  
	   end
	   ELSE if @Ingreso <> @Compara 
	   begin
	      SELECT 	  @IngresoFinal=D.NUMINGRES
	   	   FROM   (SELECT C.IPCODPACI AS IPCODPACI, MIN(A.FECINIEST) AS FECINIEST, C.NUMINGRES AS NUMINGRES
                          FROM  CHREGESTA  AS A INNER JOIN 
		                      (SELECT MAX(numingres) AS numingres, ipcodpaci AS ipcodpaci
		                               from CHREGESTA where IPCODPACI = @Cedula
					            GROUP BY IPCODPACI) AS C ON @Cedula= A.IPCODPACI AND C.NUMINGRES = A.NUMINGRES where C.IPCODPACI = @Cedula GROUP BY C.IPCODPACI,C.NUMINGRES) as D INNER JOIN
				   (SELECT  MAX(FECEGRESO) AS FECEGRESO, MAX(NUMINGRES) AS NUMINGRES 
                              from CHREGEGRE  where IPCODPACI = @Cedula
                    GROUP BY IPCODPACI) AS E ON @Cedula = D.IPCODPACI  
                          AND convert(date,D.FECINIEST,103)  BETWEEN convert(date,E.FECEGRESO,103)  AND  convert(date,E.FECEGRESO +15,103)  	
						  end 
RETURN @IngresoFinal

END