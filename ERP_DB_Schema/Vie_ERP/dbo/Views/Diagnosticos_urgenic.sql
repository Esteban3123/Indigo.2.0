

CREATE VIEW [dbo].[Diagnosticos_urgenic]
AS
SELECT     TOP (50) PERCENT dbo.ADINGRESO.IFECHAING AS f_ingreso, RTRIM(dbo.INPACIENT.IPPRINOMB) + ' ' + RTRIM(dbo.INPACIENT.IPSEGNOMB) 
                      + ' ' + RTRIM(dbo.INPACIENT.IPPRIAPEL) + ' ' + RTRIM(dbo.INPACIENT.IPSEGAPEL) AS Paciente, dbo.INDIAGNOH.NUMEFOLIO AS folio, 
                      dbo.INDIAGNOH.UFUCODIGO AS area, dbo.INDIAGNOH.NUMINGRES AS ingreso, dbo.INDIAGNOH.IPCODPACI AS id_paciente, dbo.INDIAGNOH.CODDIAGNO AS dx, 
                      dbo.INDIAGNOS.NOMDIAGNO AS nom_dx, dbo.INDIAGNOH.FECDIAGNO AS f_dx
FROM         dbo.INDIAGNOH INNER JOIN
                      dbo.INDIAGNOS ON dbo.INDIAGNOH.CODDIAGNO = dbo.INDIAGNOS.CODDIAGNO INNER JOIN
                      dbo.INPACIENT ON dbo.INDIAGNOH.IPCODPACI = dbo.INPACIENT.IPCODPACI INNER JOIN
                      dbo.ADINGRESO ON dbo.INDIAGNOH.NUMINGRES = dbo.ADINGRESO.NUMINGRES
WHERE     (dbo.INDIAGNOH.CODDIAGNO = 'K350') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'K351') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'K359') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'K36X') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'K37X') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'I212') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'I219') OR
                      (dbo.INDIAGNOH.CODDIAGNO = 'I242')
ORDER BY f_dx DESC