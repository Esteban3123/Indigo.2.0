
CREATE VIEW [dbo].[IND_ESTANCIA]
AS
SELECT DISTINCT 
                      TOP (100) PERCENT E.IPCODPACI AS Identificacion, I.NUMINGRES AS Ingreso, E.REGDIAEST AS DiasEstancia, U.UFUDESCRI AS UnidadFuncional, 
                      E.FECINIEST AS FechaInicialEstancia, E.FECFINEST AS FechaFinalEstancia
FROM         dbo.CHREGESTA AS E INNER JOIN
                      dbo.CHCAMASHO AS C ON C.CODICAMAS = E.CODICAMAS INNER JOIN
                      dbo.INUNIFUNC AS U ON U.UFUCODIGO = C.UFUCODIGO INNER JOIN
                      dbo.ADINGRESO AS I ON I.IPCODPACI = E.IPCODPACI
WHERE     (C.CODCENATE = '002') AND (E.FECINIEST >= '01/01/2012 00:00:00')
ORDER BY FechaInicialEstancia