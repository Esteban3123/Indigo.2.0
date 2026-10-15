

CREATE FUNCTION [dbo].[CantNoPos] (@Ingreso as nvarchar(10),@Pruducto as nvarchar(20) )
RETURNS nvarchar (20)
AS
BEGIN

declare @Cant nvarchar(20)


select distinct @Cant= a.CANPEDPRO 
from  dbo.HCJUNOPOM as a INNER JOIN
 (      SELECT IPCODPACI, NUMINGRES, CODPRODUC, MAX(NUMEFOLIO) AS MaxFolio
                                    FROM            dbo.HCJUNOPOM
                                    WHERE       CODCENATE = '001'     
                                    GROUP BY IPCODPACI, NUMINGRES, CODPRODUC) as i on i.IPCODPACI = a.IPCODPACI AND i.NUMINGRES = a.NUMINGRES AND 
                         i.CODPRODUC = a.CODPRODUC AND i.MaxFolio = a.numefolio
where @Ingreso= a.NUMINGRES and @Pruducto = a.CODPRODUC

RETURN @Cant

END