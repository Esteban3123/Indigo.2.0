
CREATE VIEW [dbo].[IND_AS_HP_PacientesHospitalizados_Nva]
AS
SELECT        P.IPFECNACI AS FechaNacimiento, I.NUMINGRES AS NumeroIngreso, E.IPCODPACI AS Identificación, P.IPNOMCOMP AS NombrePaciente, 
                         RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, P.IPFECNACI, 105), CONVERT(varchar, GETDATE(), 105)))) AS Edad, DAY(P.IPFECNACI) AS DíaCumpleaños, 
                         DATENAME(mm, P.IPFECNACI) AS MesCumpleaños, P.IPDIRECCI AS Direccion, P.IPTELEFON AS Telefono, P.IPTELMOVI AS Movil, C.DESCCAMAS AS Cama, 
                         CASE c.CODCLAHAB WHEN '1' THEN 'SALA OBSERVACION' WHEN '2' THEN 'SALA PROCEDIMIENTO' WHEN '3' THEN 'SALA RECUPERACION' WHEN '4' THEN
                          'HABITACION 1 CAMA' WHEN '5' THEN 'HABITACION 2 CAMAS' WHEN '6' THEN 'HABITACION 3 CAMAS' WHEN '7' THEN 'HABITACION 4 CAMAS' WHEN '8' THEN
                          'SUITE' WHEN '9' THEN 'HAB.ESPECIAL' WHEN '10' THEN 'UCI' WHEN '11' THEN 'OTRO' END AS ClaseHabitacion, TE.DESTIPEST AS [Tipo de Estancia], 
                         CASE C.CODAISLAM WHEN '1' THEN 'Aerosol' WHEN '2' THEN 'Contacto' WHEN '3' THEN 'Estandar' WHEN '4' THEN 'Gota' WHEN '1' THEN 'Protector' END AS
                          TipoAislamiento, 
                         CASE c.CODCLACAM WHEN '1' THEN 'ObservacionUrgencias' WHEN '2' THEN 'Recuperacion Post Qx' WHEN '3' THEN 'HOSPITALARIA' END AS ClaseCama, 
                         CONVERT(varchar, E.FECINIEST, 100) AS FechaCambioUnidad, CONVERT(varchar, I.IFECHAING, 100) AS FechaIngreso, I.CODCONTRA AS CodContrato, 
                         A.NOMENTIDA AS Entidad, I.CODPANATE AS PlanBen, UF.UFUCODIGO AS Cod_Servicio, UF.UFUDESCRI AS Servicio, D.CODDIAGNO AS CodDiag, 
                         V.NOMDIAGNO AS DiagnosticoPpal, dbo.Especialidades(dbo.MedicosEsp(dbo.MedicosHosp(E.IPCODPACI, E.NUMINGRES))) AS Especialidad, 
                         CASE WHEN P.IPSEXOPAC = '1' THEN 'Masculino' WHEN P.IPSEXOPAC = '2' THEN 'Femenino' END AS Sexo, I.IAUTORIZA AS NumeroAutorizacion, 
                         I.IOBSERVAC AS Observaciones, W.DISCDESCRI AS Discapacidad
FROM            dbo.CHCAMASHO AS C INNER JOIN
                         dbo.ADCENATEN AS CA ON CA.CODCENATE = C.CODCENATE INNER JOIN
                         dbo.INUNIFUNC AS UF ON UF.UFUCODIGO = C.UFUCODIGO INNER JOIN
                         dbo.CHREGESTA AS E ON E.CODICAMAS = C.CODICAMAS AND E.REGESTADO = '1' INNER JOIN
                         dbo.CHTIPESTA AS TE ON TE.CODTIPEST = E.CODTIPEST INNER JOIN
                         dbo.ADINGRESO AS I ON I.NUMINGRES = E.NUMINGRES INNER JOIN
                         dbo.INPACIENT AS P ON P.IPCODPACI = E.IPCODPACI INNER JOIN
                         dbo.COPLANBEF AS G ON G.CODPLANBE = I.CODPANATE INNER JOIN
                         dbo.INENTIDAD AS A ON A.CODENTIDA = I.CODENTIDA LEFT OUTER JOIN
                         dbo.INDIAGNOP AS D ON D.NUMINGRES = E.NUMINGRES AND D.IPCODPACI = E.IPCODPACI AND D.CODDIAPRI = 'True' LEFT OUTER JOIN
                         dbo.ADDISCAPACI AS W ON W.DISCCODIGO = P.DISCCODIGO LEFT OUTER JOIN
                         dbo.INDIAGNOS AS V ON V.CODDIAGNO = D.CODDIAGNO
WHERE        (C.CODCENATE = '001') AND (C.ESTADCAMA = '2')