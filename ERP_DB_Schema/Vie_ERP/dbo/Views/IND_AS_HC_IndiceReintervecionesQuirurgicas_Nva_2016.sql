CREATE VIEW [dbo].[IND_AS_HC_IndiceReintervecionesQuirurgicas_Nva_2016]
AS
     SELECT Q.IPCODPACI AS Identificacion, 
            P.IPNOMCOMP AS Nombre, 
            Q.NUMINGRES AS Ingreso, 
            Q.CODDIAPRE AS CodDiagnosticoPre, 
            D.NOMDIAGNO AS DiagnosticoPre, 
            Q.CODDIAPOS AS CodDiagnosticoPos, 
            D1.NOMDIAGNO AS DiagnosticoPos, 
            Q.CODSERIPS AS CódProcedimiento, 
            Pr.DESSERIPS AS Procedimiento, 
            Q.FECHORINI AS FechaCirugia, 
            Q.DESHALLOP AS Hallazgo, 
            Q.DESPROCED,
            CASE q.URGECIRUG
                WHEN 'True'
                THEN 'Si'
                WHEN 'False'
                THEN 'No'
            END AS CirugíaUrgente, 
            M.NOMMEDICO AS Cirujano, 
            ga.Name AS Entidad,
            CASE ga.EntityType
                WHEN '1'
                THEN 'EPS'
                WHEN '2'
                THEN 'EPS-S'
                WHEN '3'
                THEN 'Vinculados'
                WHEN '4'
                THEN 'ARL'
                WHEN '5'
                THEN 'ARL'
                WHEN '6'
                THEN 'IPSPrivada'
                WHEN '7'
                THEN 'IPSPublica'
                WHEN '8'
                THEN 'Fosyga'
                WHEN '9'
                THEN 'RegimenEspecial'
                WHEN '10'
                THEN 'SOAT'
                WHEN '11'
                THEN 'Otros'
                WHEN '12'
                THEN 'Otros'
            END AS TipoEntidad
     FROM dbo.HCQXINFOR AS Q
          INNER JOIN
     (
         SELECT IPCODPACI AS Identificacion, 
                COUNT(IPCODPACI) AS Cantidad
         FROM dbo.HCQXINFOR
         WHERE(FECHORINI BETWEEN '01/01/2015 00:00:00' AND '31/12/2015 23:59:59')
         GROUP BY IPCODPACI
         HAVING(COUNT(IPCODPACI) > 1)
     ) AS G ON G.Identificacion = Q.IPCODPACI
          INNER JOIN dbo.INPROFSAL AS M ON M.CODPROSAL = Q.CODPROSAL
          INNER JOIN dbo.INPACIENT AS P ON P.IPCODPACI = Q.IPCODPACI
          INNER JOIN dbo.INDIAGNOS AS D ON D.CODDIAGNO = Q.CODDIAPRE
          INNER JOIN dbo.ADINGRESO AS i ON i.NUMINGRES = Q.NUMINGRES
                                           AND i.IPCODPACI = Q.IPCODPACI
          LEFT OUTER JOIN dbo.INDIAGNOS AS D1 ON D1.CODDIAGNO = Q.CODDIAPOS
          LEFT OUTER JOIN dbo.INCUPSIPS AS Pr ON Pr.CODSERIPS = Q.CODSERIPS
          LEFT OUTER JOIN dbo.INENTIDAD AS e ON e.CODENTIDA = i.CODENTIDA
          LEFT OUTER JOIN Contract.CareGroup AS ga ON ga.Id = i.GENCAREGROUP
     WHERE(Q.CODCENATE = '001')
          AND (Q.FECHORINI >= '01/01/2016 00:00:00');