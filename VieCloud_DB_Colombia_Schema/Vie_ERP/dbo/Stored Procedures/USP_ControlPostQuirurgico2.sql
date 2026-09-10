
--DROP PROCEDURE [dbo].[USP_ControlPostQuirurgico2]

CREATE PROCEDURE [dbo].[USP_ControlPostQuirurgico2] @FechaInicio      DATETIME, 
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
     WHERE IG.FECHEGRESO >= @FechaInicio
           AND IG.FECHEGRESO < @FechaTerminacion
           AND Q2.QXPRINCIP = 1
     ORDER BY IQ.FECHORINI;
     RETURN 0;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte post-quirúrgico que retorna, para un rango de fechas de egreso hospitalario, los datos de cada intervención quirúrgica principal realizada: paciente, cirujano, especialidad, tipo de cirugía (urgencia o programada), fechas de inicio y fin, procedimiento principal CUPS, patología, muestras, interconsultas posteriores, recomendaciones e EAPB aseguradora. Sirve para seguimiento y auditoría del período postoperatorio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las cirugías cuyo egreso ocurrió dentro de un rango de fechas, con datos del paciente, cirujano, procedimiento principal, interconsultas posteriores y recomendaciones, para seguimiento post-quirúrgico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un rango de fechas (inicio y terminación) para filtrar los egresos.; Deben existir registros consistentes entre la cirugía, el ingreso, el paciente, el profesional y su especialidad, y la administradora de salud contratante.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan ingresos cuya fecha de egreso esté dentro del rango [FechaInicio, FechaTerminacion).; Únicamente se considera el procedimiento marcado como principal de la cirugía (QXPRINCIP = 1).; Las recomendaciones médicas se toman exclusivamente de la historia clínica con indicador de paciente = 12.; Las interconsultas se vinculan al mismo ingreso pero con folio posterior al de la cirugía (NUMEFOLIO de interconsulta > NUMEFOLIO de la cirugía).; Se excluyen como interconsultas válidas los códigos CUPS: 890409, 890401, 890202-GO, 890466, 890436, 890426, 39140IA, 39140IB y 890408 (consultas/valoraciones generales).; Se requiere que el ingreso tenga EAPB/administradora de salud asociada y que el cirujano tenga especialidad registrada (INNER JOIN).; El paciente debe existir en el maestro de pacientes y el ingreso debe tener registro de egreso para ser incluido.; El resultado se entrega ordenado por fecha/hora de inicio de la cirugía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Cirugía / Procedimiento quirúrgico; Cirujano; Especialidad médica; Cirugía de urgencia vs programada; Procedimiento quirúrgico principal; Patología; Muestras quirúrgicas; Interconsulta; Recomendaciones médicas; Egreso hospitalario; EAPB / Administradora de salud; Control post-quirúrgico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando FECHEGRESO ∈ [@FechaInicio,@FechaTerminacion) y QXPRINCIP = 1 → devuelve fila con datos del paciente, cirugía, interconsulta y EAPB ordenada por FECHORINI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Bandera de urgencia de la cirugía = ''TRUE'' → Clasifica el procedimiento como ''URGENCIA'' else Si la bandera es ''FALSE'' lo clasifica como ''PROGRAMADA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCQXINFOR; dbo.INPACIENT; dbo.HCQXREALI; dbo.INCUPSIPS; dbo.ADINGRESO; Contract.HealthAdministrator; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCHISPACA; dbo.HCORDINTE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'USP_ControlPostQuirurgico2';
-- GO
