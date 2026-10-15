
CREATE VIEW [dbo].[IND_AS_HC_Diabetes]
AS
SELECT DISTINCT NUMINGRES AS Ingreso, IPCODPACI AS Identificacion, CODDIAGNO AS CIE10
FROM            dbo.INDIAGNOH
WHERE        (CODDIAGNO BETWEEN 'E100' AND 'E129') AND (FECDIAGNO >= '01/01/2016 00:00:00') OR
                         (CODDIAGNO BETWEEN 'E140' AND 'E149') OR
                         (CODDIAGNO = 'E232') OR
                         (CODDIAGNO = 'N251') OR
                         (CODDIAGNO BETWEEN 'O240' AND 'O244') OR
                         (CODDIAGNO = 'O249') OR
                         (CODDIAGNO = 'P702')