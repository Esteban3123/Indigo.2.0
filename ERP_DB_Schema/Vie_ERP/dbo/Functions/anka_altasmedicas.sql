
create function [dbo].[anka_altasmedicas]( @datefrom datetime, @dateto datetime ) 
returns table 
	return SELECT     dbo.HCREGEGRE.NUMEFOLIO AS FOLIO, dbo.HCREGEGRE.NUMINGRES AS INGRESO, dbo.HCREGEGRE.FECALTPAC AS [ALTA MEDICA], 
                      dbo.HCREGEGRE.CODPROSAL AS [COD PROFESIONAL], dbo.INPROFSAL.NOMMEDICO AS [NOMBRE MEDICO], dbo.INPROFSAL.CODESPEC1 AS [COD ESPECIALIDAD],
                       dbo.INESPECIA.DESESPECI AS ESPECIALIDAD, dbo.INPROFSAL.CODIGONIT AS [NIT MEDICO]
			FROM         dbo.HCREGEGRE INNER JOIN
                      dbo.INPROFSAL ON dbo.HCREGEGRE.CODPROSAL = dbo.INPROFSAL.CODPROSAL INNER JOIN
                      dbo.INESPECIA ON dbo.INPROFSAL.CODESPEC1 = dbo.INESPECIA.CODESPECI
            where dbo.HCREGEGRE.FECALTPAC between @datefrom and @dateto;