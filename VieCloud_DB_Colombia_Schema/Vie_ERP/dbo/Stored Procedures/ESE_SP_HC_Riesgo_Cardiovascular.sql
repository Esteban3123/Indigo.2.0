-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 09/05/2019,>  
-- Description: <Description, HC del modelo de riesgo Cardiovascular de la ESE sin variables parametrizadas,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_HC_Riesgo_Cardiovascular]
AS
    BEGIN
        SELECT HC.IPCODPACI AS Doc_Paciente, 
               I.IPNOMCOMP AS Nombre_Paciente, 
               HC.NUMINGRES AS Ingreso, 
               HC.NUMEFOLIO AS Folio, 
               HC.CODCENATE AS Cod_Centro_Atencion, 
               CA.NOMCENATE AS Centro_Atencion, 
               HC.CODPROSAL AS Cod_Profesional, 
               PR.NOMMEDICO AS Profesional, 
               HC.CODESPTRA AS Cod_Especialidad, 
               ES.DESESPECI AS Especialidad, 
               HC.FECHISPAC AS Fec_HC, 
               HC.CODDIAGNO AS Diagnostico_Principal
        FROM.HCHISPACA AS HC
            JOIN.INPACIENT AS I WITH(NOLOCK) ON I.IPCODPACI = HC.IPCODPACI
            JOIN.ADCENATEN AS CA WITH(NOLOCK) ON CA.CODCENATE = HC.CODCENATE
            JOIN.INPROFSAL AS PR WITH(NOLOCK) ON PR.CODPROSAL = HC.CODPROSAL
            JOIN.INESPECIA AS ES WITH(NOLOCK) ON ES.CODESPECI = HC.CODESPTRA
        WHERE IDMODELOHC = 14
              AND FECHISPAC BETWEEN '01/04/2019' AND '30/04/2019';
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extrae las historias clínicas del modelo de riesgo cardiovascular (modelo HC número 14) registradas durante abril de 2019. Combina datos del paciente (cédula y nombre), del ingreso, del folio de la historia clínica, del centro de atención, del profesional de la salud y de la especialidad, junto con el diagnóstico principal. Se usa para reportería y seguimiento del programa de riesgo cardiovascular de la ESE, permitiendo identificar qué pacientes fueron atendidos bajo ese modelo clínico en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_Riesgo_Cardiovascular';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_Riesgo_Cardiovascular';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las historias clínicas del modelo de riesgo cardiovascular registradas en abril de 2019, con datos de paciente, profesional, centro de atención, especialidad y diagnóstico principal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Riesgo_Cardiovascular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el modelo de historia clínica con IDMODELOHC = 14 (modelo de riesgo cardiovascular).; Las historias clínicas deben tener relación íntegra con paciente, centro de atención, profesional y especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Riesgo_Cardiovascular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros del modelo de HC con identificador 14 (riesgo cardiovascular).; El rango de fechas está fijo (abril de 2019), no parametrizable.; Se aplica NOLOCK sobre catálogos auxiliares, permitiendo lecturas sucias en pacientes, centros, profesionales y especialidades.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Riesgo_Cardiovascular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Riesgo cardiovascular; Paciente; Profesional de salud; Centro de atención; Especialidad médica; Diagnóstico principal; Ingreso; Folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Riesgo_Cardiovascular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve historias clínicas cuando IDMODELOHC = 14 y FECHISPAC entre 01/04/2019 y 30/04/2019.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Riesgo_Cardiovascular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCHISPACA; INPACIENT; ADCENATEN; INPROFSAL; INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Riesgo_Cardiovascular';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Riesgo_Cardiovascular';
-- GO
