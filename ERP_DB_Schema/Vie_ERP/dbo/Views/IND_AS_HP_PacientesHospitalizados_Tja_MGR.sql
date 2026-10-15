
CREATE VIEW [dbo].[IND_AS_HP_PacientesHospitalizados_Tja_MGR]
AS
SELECT        A.NOMENTIDA AS Entidad, c.UFUCODIGO AS UnidadFun, uf.UFUDESCRI AS Servicio, c.DESCCAMAS AS Cama, 
                         CASE c.CODCLAHAB WHEN '1' THEN 'SALA OBSERVACION' WHEN '2' THEN 'SALA PROCEDIMIENTO' WHEN '3' THEN 'SALA RECUPERACION' WHEN '4' THEN 'HABITACION 1 CAMA'
                          WHEN '5' THEN 'HABITACION 2 CAMAS' WHEN '6' THEN 'HABITACION 3 CAMAS' WHEN '7' THEN 'HABITACION 4 CAMAS' WHEN '8' THEN 'SUITE' WHEN '9' THEN 'HAB.ESPECIAL'
                          WHEN '10' THEN 'UCI' WHEN '11' THEN 'OTRO' END AS ClaseHabitacion, 
                         CASE c.CODCLACAM WHEN '1' THEN 'ObservacionUrgencias' WHEN '2' THEN 'Recuperacion Post Qx' WHEN '3' THEN 'HOSPITALARIA' END AS ClaseCama, 
                         CASE c.ESTADCAMA WHEN '1' THEN 'LIBRE' WHEN '2' THEN 'OCUPADA' END AS EstadoCama, E.NUMINGRES AS NumeroIngreso, E.IPCODPACI AS Identificación, 
                         P.IPNOMCOMP AS NombrePaciente, c.CODAISLAM AS CodAislamiento, E.FECINIEST AS FechaIngreso, 1 AS Cantidad, DATEDIFF(dd, E.FECINIEST, GETDATE()) 
                         AS DiasEstancia, D.CODDIAGNO AS CodDiagnostico, V.NOMDIAGNO AS NombreDiagnostico, D.CODDIAPRI AS [Es Principal], c.ESTADCAMA, 
                         dbo.ADINGRESO.CODENTIDA, dbo.ADINGRESO.IFECHAING AS Fecha_IngresoClinica, - DATEDIFF(yy, GETDATE(), P.IPFECNACI) AS Expr1, 
                         dbo.ADINGRESO.UFUINGMED, dbo.ADINGRESO.CODUSUCRE, dbo.ADINGRESO.CODCONTRA, P.IPTIPOAFI AS afiliacion, P.NIVCODIGO AS estrato, 
                         P.TIPCOBSAL AS cobertura, P.AUUBICACI, dbo.INMUNICIP.MUNNOMBRE AS [municipio ubicacion], RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), 
                         CONVERT(varchar, GETDATE(), 105)))) AS Edad, dbo.INESPECIA.DESESPECI AS tratante, TE.DESTIPEST AS [Tipo de Estancia]
FROM            dbo.INPACIENT AS P INNER JOIN
                         dbo.INUBICACI ON P.AUUBICACI = dbo.INUBICACI.AUUBICACI INNER JOIN
                         dbo.INMUNICIP ON dbo.INUBICACI.DEPMUNCOD = dbo.INMUNICIP.DEPMUNCOD LEFT OUTER JOIN
                         dbo.CHCAMASHO AS c INNER JOIN
                         dbo.ADCENATEN AS ca ON ca.CODCENATE = c.CODCENATE INNER JOIN
                         dbo.INUNIFUNC AS uf ON uf.UFUCODIGO = c.UFUCODIGO INNER JOIN
                         dbo.CHREGESTA AS E ON E.CODICAMAS = c.CODICAMAS AND E.REGESTADO = '1' INNER JOIN
                         dbo.CHTIPESTA AS TE ON TE.CODTIPEST = E.CODTIPEST INNER JOIN
                         dbo.ADINGRESO ON E.NUMINGRES = dbo.ADINGRESO.NUMINGRES LEFT OUTER JOIN
                         dbo.INESPECIA ON dbo.ADINGRESO.CODESPTRA = dbo.INESPECIA.CODESPECI ON P.IPCODPACI = E.IPCODPACI LEFT OUTER JOIN
                         dbo.INDIAGNOP AS D ON E.NUMINGRES = D.NUMINGRES AND E.IPCODPACI = D.IPCODPACI LEFT OUTER JOIN
                         dbo.INDIAGNOS AS V ON V.CODDIAGNO = D.CODDIAGNO LEFT OUTER JOIN
                         dbo.INENTIDAD AS A ON dbo.ADINGRESO.CODENTIDA = A.CODENTIDA
WHERE        (c.CODCENATE = '004') AND (c.ESTADCAMA = 2)