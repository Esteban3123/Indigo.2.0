
CREATE VIEW [dbo].[IND_AD_HC_ExtranjerosAtendidosSIRE]
AS
SELECT DISTINCT 
                         i.NUMINGRES AS Ingreso, i.IPCODPACI AS [Documento Identificación], 
                         CASE p.iptipodoc WHEN '1' THEN 'Cédula_Ciudadanía' WHEN '2' THEN 'Cédula_Extranjería' WHEN '3' THEN 'Tarjeta_Identidad' WHEN '4' THEN 'Registro_Civil'
                          WHEN '5' THEN 'Pasporte' WHEN '6' THEN 'AdultoSinIdentificación' WHEN '7' THEN 'MenorSinIdentificación' END AS [Tipo de Documento], 
                         P.IPNOMCOMP AS Paciente, ge.DESCRIPCION AS [Grupo Especial], hc.NUMEFOLIO AS Folio, i.IFECHAING AS [Fecha Ingreso], 
                         i.UFUCODIGO AS [Unidad ó Servicio], u.UFUDESCRI AS [Descripción Servicio de Ingreso], i.FECHOSPIT AS [Fecha Ingreso Hospitalización], 
                         i.FECREGCRE AS [Fecha Egreso], i.CODDIAING AS [CIE-10], d.NOMDIAGNO AS Diagnóstico, 
                         CASE p.ipsexopac WHEN '1' THEN 'Masculino' WHEN '2' THEN 'Femenino' END AS [Sexo Paciente], P.IPFECNACI AS [Fecha Nacimiento], YEAR(GETDATE()) 
                         - YEAR(P.IPFECNACI) - 1 AS EdadEnAnosHoy, P.OBSERVACI AS Observaciones
FROM            dbo.ADINGRESO AS i INNER JOIN
                         dbo.INPACIENT AS P ON P.IPCODPACI = i.IPCODPACI INNER JOIN
                         dbo.INUNIFUNC AS u ON u.UFUCODIGO = i.UFUCODIGO INNER JOIN
                         dbo.INDIAGNOS AS d ON i.CODDIAING = d.CODDIAGNO INNER JOIN
                         dbo.HCHISPACA AS hc ON hc.IPCODPACI = i.IPCODPACI AND hc.NUMINGRES = i.NUMINGRES LEFT OUTER JOIN
                         dbo.ADPOBESPEPAC AS gep ON gep.IPCODPACI = P.IPCODPACI LEFT OUTER JOIN
                         dbo.ADPOBESPE AS ge ON ge.ID = gep.IDADPOBESPE
WHERE        (i.CODCENATE = '001') AND (i.IESTADOIN <> 'A') AND (i.IFECHAING >= '01/01/2015 00:00:00') AND (P.IPTIPODOC = '2') AND (hc.IDETIPHIS = 'HCURGING1')