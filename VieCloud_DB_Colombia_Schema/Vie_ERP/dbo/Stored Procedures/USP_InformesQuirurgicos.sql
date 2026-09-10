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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de informes quirúrgicos para un rango de fechas dado (fecha inicial y fecha final). Consolida en un único resultado la información clínica y administrativa de cada acto quirúrgico: datos del paciente (cédula, nombre, edad, sexo, barrio, municipio, teléfonos), número de ingreso, grupo de atención y entidad contratante, procedimiento principal y secundarios realizados en quirófano (código CUPS y descripción), diagnósticos preoperatorio y posoperatorio (CIE-10), tipo de anestesia (local, regional, general o combinada), tipo de cirugía (urgencia o programada), hora de inicio y fin con duración en minutos, sala de cirugía, materiales radicados, especialista responsable y su especialidad. Para lograrlo combina el informe quirúrgico (HCQXINFOR), los procedimientos realizados en quirófano (HCQXREALI), el catálogo de servicios CUPS (INCUPSIPS), el maestro de pacientes (INPACIENT), el maestro de profesionales (INPROFSAL), el catálogo de especialidades (INESPECIA), los diagnósticos CIE-10 (INDIAGNOS), la ubicación geográfica del paciente (INUBICACI) y el ingreso hospitalario con su grupo de atención y entidad contratante (ADINGRESO). Se utiliza para auditoría quirúrgica, reportería gerencial y estadísticas de producción de cirugías en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_InformesQuirurgicos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_InformesQuirurgicos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un informe consolidado de procedimientos quirúrgicos realizados en un rango de fechas, con datos del paciente, especialista, diagnósticos, anestesia, tiempos y entidad responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_InformesQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas debe estar definido (fecha inicial y final).; Cada cirugía en HCQXINFOR debe tener profesional, diagnóstico pre, especialidad, CUPS, paciente, ubicación e ingreso asociado existentes (INNER JOIN).; El ingreso debe tener grupo de atención (CareGroup) y administradora de salud (HealthAdministrator) válidos.; Debe existir al menos un procedimiento realizado en HCQXREALI vinculado por folio, paciente e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_InformesQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El diagnóstico pre y post se resuelven ambos contra CODDIAPRE (no se usa código de diagnóstico postoperatorio independiente).; El tiempo quirúrgico se calcula como diferencia en minutos entre inicio y fin de cirugía.; La edad se calcula en años contra la fecha actual del sistema mediante [Common].[GETDATE]().; Cada procedimiento dentro de un mismo informe recibe un consecutivo ordenado priorizando el procedimiento principal.; Solo se incluyen cirugías con datos completos en todas las tablas relacionadas (INNER JOINs).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_InformesQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Informe quirúrgico; Procedimiento quirúrgico (CUPS); Diagnóstico pre y postoperatorio; Tipo de anestesia; Cirugía de urgencia vs programada; Procedimiento principal; Especialidad médica; Grupo etario; Grupo de atención; Administradora de salud (entidad); Ingreso hospitalario; Sala de cirugía; Materiales quirúrgicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_InformesQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna las cirugías cuyo FECHORINI >= @fechaInicial y FECHORFIN < @fechaFinal, ordenadas por consecutivo de informe y numeración densa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_InformesQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Q.TIPOANEST = 1/2/3/4 → Clasifica anestesia como LOCAL, REGIONAL, GENERAL o COMBINADA respectivamente else NULL; si Q.URGECIRUG = ''TRUE'' → Clasifica la cirugía como URGENCIA else Si ''FALSE'' la clasifica como PROGRAMADA; si Q2.QXPRINCIP = ''TRUE'' → Marca el procedimiento como principal (SI) else Si ''FALSE'' lo marca como NO principal; si Pa.IPSEXOPAC = 2 → Sexo = Femenino else Si = 1 entonces Masculino', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_InformesQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.Edad2; dbo.Etarios', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_InformesQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXINFOR; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INESPECIA; dbo.INCUPSIPS; dbo.HCQXREALI; dbo.INPACIENT; dbo.INUBICACI; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_InformesQuirurgicos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_InformesQuirurgicos';
-- GO
