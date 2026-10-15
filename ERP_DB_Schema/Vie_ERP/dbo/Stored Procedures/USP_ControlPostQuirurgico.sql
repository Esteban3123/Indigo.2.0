
--DROP PROCEDURE [dbo].[USP_ControlPostQuirurgico]
CREATE PROCEDURE [dbo].[USP_ControlPostQuirurgico] @FechaInicio      DATETIME, 
                                                  @FechaTerminacion DATETIME
AS
     SELECT IQ.IPCODPACI AS IdPaciente, 
            PA.IPNOMCOMP AS NombrePaciente, 
            IQ.NUMINGRES AS Ingreso, 
            P.NOMMEDICO AS Cirujano, 
            E.DESESPECI AS Especialidad,
            CASE
                WHEN IQ.URGECIRUG = 'TRUE'
                THEN 'URGENCIA'
                WHEN IQ.URGECIRUG = 'FALSE'
                THEN 'PROGRAMADA'
            END AS Tipo, 
            IQ.FECHORINI AS FechaInicioQx, 
            IQ.FECHORFIN AS FechaTerminaQx, 
            C2.DESSERIPS AS ProcedimientoQxPpal, 
            IQ.PREPATOLO AS Patologia, 
            IQ.NUMCANMUE AS NumeroMuestras, 
            C3.CODSERIPS AS CodInterconsulta, 
            C3.DESSERIPS AS TipoInteconsulta, 
            IC.OBSSERIPS AS ObservacionInterconsulta, 
            HC.INDICAMED AS Recomendaciones, 
            IG.FECHEGRESO AS FechaEgreso, 
            PA.IPTELEFON AS Tel1, 
            PA.IPTELMOVI AS Tel2, 
            EPS.Name AS EAPB
     FROM HCQXINFOR AS IQ
          INNER JOIN INPACIENT AS PA ON IQ.IPCODPACI = PA.IPCODPACI
          INNER JOIN HCQXREALI AS Q2 ON IQ.NUMEFOLIO = Q2.NUMEFOLIO
                                        AND IQ.IPCODPACI = Q2.IPCODPACI
                                        AND IQ.NUMINGRES = Q2.NUMINGRES
          INNER JOIN INCUPSIPS AS C2 ON C2.CODSERIPS = Q2.CODSERIPS
          INNER JOIN ADINGRESO AS IG ON IQ.NUMINGRES = IG.NUMINGRES
          INNER JOIN Contract.HealthAdministrator AS EPS ON IG.GENCONENTITY = EPS.Id
          INNER JOIN INPROFSAL AS P ON P.CODPROSAL = IQ.CODPROSAL
          INNER JOIN INESPECIA AS E ON P.CODESPEC1 = E.CODESPECI
          LEFT OUTER JOIN HCHISPACA AS HC ON IQ.NUMINGRES = HC.NUMINGRES
                                             AND HC.INDICAPAC = 12
          LEFT OUTER JOIN HCORDINTE AS IC ON IQ.NUMINGRES = IC.NUMINGRES
                                             AND IC.NUMEFOLIO > IQ.NUMEFOLIO
          LEFT OUTER JOIN INCUPSIPS AS C3 ON C3.CODSERIPS = IC.CODSERIPS
                                             AND C3.CODSERIPS NOT IN('890409', '890401', '890202-GO', '890466', '890436', '890426', '39140IA', '39140IB', '890408')
     WHERE IQ.FECHORINI >= @FechaInicio
           AND IQ.FECHORINI < @FechaTerminacion
           AND Q2.QXPRINCIP = 1
     ORDER BY IQ.FECHORINI;
     RETURN 0;