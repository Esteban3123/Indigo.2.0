

CREATE FUNCTION [dbo].[AutonumericoEgresoCama] (@Cedula as varchar(25))
RETURNS int 
AS
BEGIN

declare @Autonumerico int

select  distinct @Autonumerico= MAX(B.Autonumerico)-1 
	      FROM CHREGEGRE AS A INNER JOIN
	           (Select row_number()over (PARTITION by ipcodpaci order by ipcodpaci) as Autonumerico,IPCODPACI,FECEGRESO, MAX(NUMINGRES) AS NUMINGRES
                    from CHREGEGRE  
                    GROUP BY IPCODPACI,FECEGRESO,numingres) as B ON B.IPCODPACI = A.IPCODPACI
		WHERE B.IPCODPACI=@Cedula
RETURN @Autonumerico

END