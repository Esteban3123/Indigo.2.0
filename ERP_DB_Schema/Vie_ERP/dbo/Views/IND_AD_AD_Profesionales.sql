
CREATE VIEW [dbo].[IND_AD_AD_Profesionales]
AS
SELECT        p.CODPROSAL AS Código, p.CODIGONIT AS Identificacion, p.NOMMEDICO AS Médico, p.IMDIRECCI AS Dirección, p.IMTELEFON AS Teléfono, 
                         p.IMTELMOVI AS Celular, p.TARJETAPR AS TarjetaProfesional, e.DESESPECI AS Especialidad1, E2.DESESPECI AS Especialidad2, 
                         e3.DESESPECI AS Especialidad3
FROM            dbo.INPROFSAL AS p INNER JOIN
                         dbo.INESPECIA AS e ON e.CODESPECI = p.CODESPEC1 LEFT OUTER JOIN
                         dbo.INESPECIA AS E2 ON E2.CODESPECI = p.CODESPEC2 LEFT OUTER JOIN
                         dbo.INESPECIA AS e3 ON e3.CODESPECI = p.CODESPEC3
WHERE        (p.ESTADOMED = '1') AND (p.CODPROSAL <> '996') AND (p.CODPROSAL <> '161') AND (p.CODPROSAL <> '998') AND (p.CODPROSAL <> '982') AND 
                         (p.CODPROSAL <> '997')