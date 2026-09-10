
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de control postquirúrgico que lista todas las cirugías realizadas en un rango de fechas, mostrando por cada intervención: la identificación y nombre del paciente, el número de ingreso hospitalario, el cirujano responsable y su especialidad, si la cirugía fue de urgencia o programada, la fecha y hora de inicio y fin del procedimiento quirúrgico principal (CUPS), la patología preparatoria, el número de muestras enviadas a patología, las interconsultas solicitadas después de la cirugía, las recomendaciones o indicaciones médicas postoperatorias registradas en la historia clínica, la fecha de egreso y los teléfonos del paciente, y la EPS o aseguradora a la que pertenece. Integra el informe quirúrgico (HCQXINFOR), los procedimientos realizados en quirófano (HCQXREALI), la admisión del paciente (ADINGRESO), el maestro de pacientes (INPACIENT), profesionales de la salud (INPROFSAL), especialidades médicas (INESPECIA), el catálogo CUPS (INCUPSIPS), las interconsultas ordenadas (HCORDINTE) y la historia clínica (HCHISPACA), permitiendo al equipo médico y administrativo hacer seguimiento postoperatorio y verificar la continuidad del cuidado tras una intervención quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_ControlPostQuirurgico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'USP_ControlPostQuirurgico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Generar un listado de control post-quirúrgico con datos del paciente, cirugía principal, cirujano, especialidad, interconsultas posteriores, recomendaciones, egreso y EAPB, dentro de un rango de fechas de inicio de cirugía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe suministrarse un rango de fechas válido (inicio y fin) para filtrar las cirugías.; Deben existir maestros consistentes de paciente, ingreso, profesional, especialidad, CUPS y administradora de salud para que el ingreso aparezca en el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros del procedimiento quirúrgico marcado como principal (QXPRINCIP = 1).; Solo se incluyen cirugías cuya fecha/hora de inicio cae en el rango [FechaInicio, FechaTerminacion).; Las recomendaciones se obtienen únicamente de la historia clínica con indicador de paciente igual a 12.; Las interconsultas asociadas deben ser posteriores al folio quirúrgico (NUMEFOLIO mayor) y se excluyen códigos CUPS no clínicamente relevantes para el reporte: 890409, 890401, 890202-GO, 890466, 890436, 890426, 39140IA, 39140IB, 890408.; Cada cirugía reportada debe tener paciente, ingreso, EAPB contratante, profesional y especialidad asociados (joins internos obligatorios).; El listado se entrega ordenado cronológicamente por fecha de inicio de la cirugía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Cirugía / Procedimiento quirúrgico; Cirujano; Especialidad médica; Cirugía de urgencia vs programada; Patología quirúrgica; Muestras quirúrgicas; Interconsulta; Recomendaciones / Indicaciones médicas; Egreso hospitalario; EAPB / Administradora de salud (EPS); Procedimiento quirúrgico principal (CUPS)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de resultados con la información post-quirúrgica filtrada por rango de fechas de inicio de cirugía y procedimiento principal (QXPRINCIP=1), ordenado por FECHORINI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si URGECIRUG = ''TRUE'' → Se clasifica la cirugía como ''URGENCIA'' else Si URGECIRUG = ''FALSE'' se clasifica como ''PROGRAMADA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXINFOR; dbo.INPACIENT; dbo.HCQXREALI; dbo.INCUPSIPS; dbo.ADINGRESO; Contract.HealthAdministrator; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCHISPACA; dbo.HCORDINTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico';
-- GO
