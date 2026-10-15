
CREATE VIEW [dbo].[SeguimientoUrgencias]
AS
SELECT        A.NUMINGRES AS Ingreso, A.IPCODPACI AS CodigoPaciente, P.IPNOMCOMP AS nombrepaciente, A.IFECHAING AS FechaIngreso, A.IESTADOIN AS ESTADO, 
                         A.UFUCODIGO AS UnidadFuncional
FROM            dbo.ADINGRESO AS A INNER JOIN
                         dbo.INPACIENT AS P ON A.IPCODPACI = P.IPCODPACI
WHERE        (A.IFECHAING > '22/04/2014 14:00:00') AND (A.UFUCODIGO IN ('001', '043', '002', '003')) AND (A.IESTADOIN IN ('', 'F'))