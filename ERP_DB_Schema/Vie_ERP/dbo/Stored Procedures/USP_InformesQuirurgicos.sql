CREATE PROCEDURE [dbo].[USP_InformesQuirurgicos] @fechaInicial AS DATETIME, 
                                                @fechaFinal AS   DATETIME
AS
     SET NOCOUNT ON;
     WITH PQ
          AS (SELECT TOP (100) PERCENT Q.AUTO AS ConsInforme, 
                                       GrA.Name AS GrupoAtencion, 
                                       EntA.Name AS Entidad, 
                                       Q.IPCODPACI AS IdPaciente, 
                                       Pa.IPNOMCOMP AS NombrePaciente, 
                                       Q.NUMINGRES AS NumIngreso, 
                                       Q.CODSERIPS AS CUPs, 
                                       C.DESSERIPS AS DescripcionProcedimiento, 
                                       Q.CODPROSAL AS CodMD, 
                                       P.NOMMEDICO AS NombreEspecialista, 
                                       P.CODESPEC1 AS CodEspecialidad, 
                                       E.DESESPECI AS Especialidad, 
                                       DX.NOMDIAGNO AS DxPRE, 
                                       DX2.NOMDIAGNO AS DxPOST, 
                                       Q.FECHORINI AS FechaInicial, 
                                       Q.FECHORFIN AS FechaFinal, 
                                       DATEDIFF(MINUTE, Q.FECHORINI, Q.FECHORFIN) AS TiempoQxMinutos,
                                       CASE
                                           WHEN Q.TIPOANEST = 1
                                           THEN 'LOCAL'
                                           WHEN Q.TIPOANEST = 2
                                           THEN 'REGIONAL'
                                           WHEN Q.TIPOANEST = 3
                                           THEN 'GENERAL'
                                           WHEN Q.TIPOANEST = 4
                                           THEN 'COMBINADA'
                                       END AS TipoAnestesia,
                                       CASE
                                           WHEN Q.URGECIRUG = 'TRUE'
                                           THEN 'URGENCIA'
                                           WHEN Q.URGECIRUG = 'FALSE'
                                           THEN 'PROGRAMADA'
                                       END AS TipoQx, 
                                       Q2.CODSERIPS AS Código,
                                       CASE
                                           WHEN Q2.QXPRINCIP = 'TRUE'
                                           THEN 'SI'
                                           WHEN Q2.QXPRINCIP = 'FALSE'
                                           THEN 'NO'
                                       END AS ProcedPpal, 
                                       C2.DESSERIPS AS Descripción, 
                                       Q2.CANTIDAQX AS Cantidad, 
                                       DATEDIFF(YY, Pa.IPFECNACI, [Common].[GETDATE]()) AS EdadAños, 
                                       RTRIM(LTRIM(dbo.Edad2(CONVERT(VARCHAR, Pa.IPFECNACI, 105), CONVERT(VARCHAR, [Common].[GETDATE](), 105)))) AS Edad, 
                                       INUBICACI.UBINOMBRE AS Barrio, 
                                       INUBICACI.DEPMUNCOD AS Municipio, 
                                       Pa.IPFECNACI AS Fnacimiento,
                                       CASE
                                           WHEN Pa.IPSEXOPAC = 2
                                           THEN 'Femenino'
                                           WHEN Pa.IPSEXOPAC = 1
                                           THEN 'Masculino'
                                       END AS Sexo, 
                                       dbo.Etarios(Pa.IPFECNACI, Q.FECHORINI) AS GrupoEtario, 
                                       Pa.IPTELEFON AS Tel1, 
                                       Pa.IPTELMOVI AS Tel2, 
                                       Q.SALACIRUG AS Sala, 
                                       Q.MATERADIC AS Materiales
              FROM HCQXINFOR AS Q
                   INNER JOIN INPROFSAL AS P ON P.CODPROSAL = Q.CODPROSAL
                   INNER JOIN INDIAGNOS AS DX2 ON Q.CODDIAPRE = DX2.CODDIAGNO
                   INNER JOIN INDIAGNOS AS DX ON Q.CODDIAPRE = DX.CODDIAGNO
                   INNER JOIN INESPECIA AS E ON P.CODESPEC1 = E.CODESPECI
                   INNER JOIN INCUPSIPS AS C ON C.CODSERIPS = Q.CODSERIPS
                   INNER JOIN HCQXREALI AS Q2 ON Q.NUMEFOLIO = Q2.NUMEFOLIO
                                                 AND Q.IPCODPACI = Q2.IPCODPACI
                                                 AND Q.NUMINGRES = Q2.NUMINGRES
                   INNER JOIN INCUPSIPS AS C2 ON C2.CODSERIPS = Q2.CODSERIPS
                   INNER JOIN INPACIENT AS Pa ON Q.IPCODPACI = Pa.IPCODPACI
                   INNER JOIN INUBICACI ON Pa.AUUBICACI = INUBICACI.AUUBICACI
                   INNER JOIN ADINGRESO AS Ing ON Q.IPCODPACI = Ing.IPCODPACI
                                                  AND Q.NUMINGRES = Ing.NUMINGRES
                   INNER JOIN Contract.CareGroup AS GrA ON Ing.GENCAREGROUP = GrA.Id
                   INNER JOIN Contract.HealthAdministrator AS EntA ON Ing.GENCONENTITY = EntA.Id
              WHERE(Q.FECHORINI >= @fechaInicial)
                   AND (Q.FECHORFIN < @fechaFinal))
          SELECT ConsInforme, 
                 DENSE_RANK() OVER(
                 ORDER BY ConsInforme) AS NumInformeQx, 
                 IdPaciente, 
                 NombrePaciente, 
                 EdadAños, 
                 Edad, 
                 Fnacimiento, 
                 Sexo, 
                 NumIngreso, 
                 CUPs, 
                 DescripcionProcedimiento, 
                 CodMD, 
                 NombreEspecialista, 
                 CodEspecialidad, 
                 Especialidad, 
                 DxPRE, 
                 DxPOST, 
                 FechaInicial, 
                 FechaFinal, 
                 TiempoQxMinutos, 
                 TipoAnestesia, 
                 TipoQx, 
                 Código, 
                 Descripción, 
                 ProcedPpal, 
                 Cantidad, 
                 ROW_NUMBER() OVER(PARTITION BY ConsInforme
                 ORDER BY ProcedPpal, 
                          Código) AS ConsecProcedimiento, 
                 Barrio, 
                 Municipio, 
                 GrupoEtario, 
                 Tel1, 
                 Tel2, 
                 Sala, 
                 Materiales, 
                 GrupoAtencion, 
                 Entidad
          FROM PQ
          ORDER BY ConsInforme, 
                   NumInformeQx;