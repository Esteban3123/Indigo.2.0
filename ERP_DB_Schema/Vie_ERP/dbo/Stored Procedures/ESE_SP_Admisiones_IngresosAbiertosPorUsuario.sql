CREATE PROCEDURE [dbo].[ESE_SP_Admisiones_IngresosAbiertosPorUsuario] @UserName VARCHAR(250)
AS
     SELECT DISTINCT 
            I.CODCENATE AS CodCentro, 
            RTRIM(cen.NOMCENATE) AS CentroAtencion, 
            RTRIM(i.NUMINGRES) AS Ingreso, 
            ga.Code AS CodGrupoAtencion, 
            ga.Name AS GrupoAtención, 
            RTRIM(ea.Name) AS Entidad, 
            RTRIM(i.IPCODPACI) AS Identificacion, 
            SUBSTRING(RTRIM(p.IPNOMCOMP), 1, 40) AS Paciente, 
            i.IFECHAING AS Fecha_Ingreso,
            CASE i.IESTADOIN
                WHEN '  '
                THEN 'Sin Confirmar'
                WHEN 'F'
                THEN 'Confirmado'
                WHEN 'A'
                THEN 'Anulado'
                WHEN 'C'
                THEN 'Cerrado'
                WHEN 'P'
                THEN 'Facturado Parcial'
            END AS Estado, 
            RTRIM(uf.UFUDESCRI) AS UnidadFuncional,
            CASE
                WHEN SEG2.CODUSUARI IS NULL
                THEN uu.CODUSUARI
                ELSE SEG2.CODUSUARI
            END AS CodUsuarioCrea,
            CASE
                WHEN SEG2.NOMUSUARI IS NULL
                THEN uu.NOMUSUARI
                ELSE SEG2.NOMUSUARI
            END AS UsuarioCrea,
            CASE i.TIPOINGRE
                WHEN 1
                THEN 'Ambulatorio'
                WHEN 2
                THEN 'Hospitalario'
            END AS TipoIngreso, 
            '1' AS Cantidad
     FROM.ADINGRESO AS i WITH(NOLOCK)
         INNER JOIN.INUNIFUNC AS uf WITH(NOLOCK) ON uf.UFUCODIGO = i.UFUCODIGO
         INNER JOIN.ADCENATEN AS Cen ON Cen.CODCENATE = i.CODCENATE
         LEFT OUTER JOIN Contract.CareGroup AS ga WITH(NOLOCK) ON ga.Id = i.GENCAREGROUP
         LEFT OUTER JOIN Security.[User] AS u ON u.UserCode = i.CODUSUCRE
         LEFT OUTER JOIN Security.[User] AS um ON u.UserCode = i.codusumod
         LEFT OUTER JOIN Security.Person AS per ON per.Id = u.IdPerson
         LEFT OUTER JOIN Security.Person AS PERM ON PERM.Id = um.IdPerson
         LEFT OUTER JOIN.HCHISPACA AS HC WITH(NOLOCK) ON HC.NUMINGRES = i.NUMINGRES
                                                         AND HC.IPCODPACI = HC.IPCODPACI
                                                         AND HC.TIPHISPAC = 'i'
         LEFT OUTER JOIN.INPACIENT AS p WITH(NOLOCK) ON p.IPCODPACI = i.IPCODPACI
         LEFT OUTER JOIN Contract.HealthAdministrator AS ea WITH(NOLOCK) ON ea.Id = i.GENCONENTITY
         LEFT OUTER JOIN Common.City AS ci WITH(NOLOCK) ON ci.Id = p.GENEXPEDITIONCITY
         LEFT OUTER JOIN.HCURGING1 AS HCU WITH(NOLOCK) ON HCU.NUMINGRES = HC.NUMINGRES
                                                          AND HCU.IPCODPACI = HC.IPCODPACI
                                                          AND HCU.NUMEFOLIO = HC.NUMEFOLIO
         LEFT OUTER JOIN
     (
         SELECT IPCODPACI, 
                NUMINGRES, 
                MAX(NUMEFOLIO) AS Folio
         FROM.INDIAGNOP
         WHERE(CODDIAPRI = 'True')
         GROUP BY NUMINGRES, 
                  IPCODPACI
     ) AS DX ON DX.IPCODPACI = HCU.IPCODPACI
                AND DX.NUMINGRES = HCU.NUMINGRES
                AND DX.Folio = HC.NUMEFOLIO
         LEFT OUTER JOIN.INDIAGNOS AS CIE10 WITH(NOLOCK) ON CIE10.CODDIAGNO = HC.CODDIAGNO
         LEFT OUTER JOIN.HCURGEVO1 AS HCU1 WITH(NOLOCK) ON HCU.NUMINGRES = HC.NUMINGRES
                                                           AND HCU1.IPCODPACI = HC.IPCODPACI
                                                           AND HCU1.NUMEFOLIO = HC.NUMEFOLIO
         LEFT OUTER JOIN.ADINGRESO AS I2 WITH(NOLOCK) ON I2.NUMINGRES = i.NUMINGRES
         LEFT OUTER JOIN.INUNIFUNC AS D WITH(NOLOCK) ON I2.UFUAACTHOS = D.UFUCODIGO
         LEFT OUTER JOIN.HCREGEGRE AS em WITH(NOLOCK) ON em.IPCODPACI = HC.IPCODPACI
                                                         AND em.NUMINGRES = HC.NUMINGRES
         LEFT OUTER JOIN.SEGusuaru AS uu WITH(NOLOCK) ON uu.CODUSUARI = i.CODUSUCRE
         LEFT OUTER JOIN.INUBICACI AS BB ON BB.AUUBICACI = P.AUUBICACI
         LEFT OUTER JOIN.INMUNICIP AS EE ON EE.DEPMUNCOD = BB.DEPMUNCOD
         LEFT OUTER JOIN.ADTRIAGEU AS AD ON AD.NUMINGRES = I.NUMINGRES
         LEFT OUTER JOIN.ADCONTURG AS ADCO ON AD.CODCONCEC = ADCO.CODCONCEC
         LEFT OUTER JOIN.SEGusuaru AS SEG2 ON SEG2.CODUSUARI = ADCO.CODUSUARI
     WHERE(i.IESTADOIN <> 'F')
          AND (i.IESTADOIN <> 'A')
          AND (i.IESTADOIN <> 'C')
          AND ((SEG2.NOMUSUARI IS NULL
                AND uu.CODUSUARI = @UserName)
               OR ((uu.CODUSUARI IS NULL
                    AND SEG2.CODUSUARI = @UserName)));