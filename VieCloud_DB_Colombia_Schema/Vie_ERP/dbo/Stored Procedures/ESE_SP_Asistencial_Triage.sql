-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 13/05/2019,>  
-- Description: <Description, Registro de control paciente, triage y atención inicial de urgencias con todos los tiempos en cada proceso,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_Triage] @FechaIni DATETIME, 
                                                  @FechaFin DATETIME
AS
    BEGIN
        SELECT CASE B.IPTIPODOC
                   WHEN '1'
                   THEN 'CC'
                   WHEN '2'
                   THEN 'CE'
                   WHEN '3'
                   THEN 'TI'
                   WHEN '4'
                   THEN 'RC'
                   WHEN '5'
                   THEN 'PA'
                   WHEN '6'
                   THEN 'AS'
                   WHEN '7'
                   THEN 'MS'
                   WHEN '8'
                   THEN 'NU'
                   WHEN '9'
                   THEN 'CN'
                   WHEN '10'
                   THEN 'CD'
                   WHEN '11'
                   THEN 'SC'
                   ELSE 'PE'
               END 'Tipo Doc Paciente', 
               A.IPCODPACI 'Paciente', 
               B.IPPRINOMB 'Primer Nombre', 
               B.IPSEGNOMB 'Segundo Nombre', 
               B.IPPRIAPEL 'Primer apellido', 
               B.IPSEGAPEL 'Segundo Apellido', 
               B.IPFECNACI 'F Nacimiento', 
               DATEDIFF(YEAR, B.IPFECNACI, A.TRIAFECHA) 'Edad', 
               C.CODCONCEC 'Concecutivo CP', 
               C.CODUSUARI 'Cod Usu CP', 
               D.NOMUSUARI 'Usu CP', 
               C.IPFECLLEGA 'Fec CP', 
               A.FECHINITR 'F. Inicia Triage', 
               A.TRIAFECHA 'F. Fin Triage', 
               A.CODPROSAL 'Cod Prof Triage', 
               E.NOMUSUARI 'Prof Triage',
               CASE A.TRIAGECLA
                   WHEN 1
                   THEN '1.Reanimación'
                   WHEN 2
                   THEN '2.Emergencia'
                   WHEN 3
                   THEN '3.Urgencia Medica'
                   WHEN 4
                   THEN '4.Urgencia Diferida'
                   WHEN 5
                   THEN '5.No Urgente'
               END 'Triage', 
               A.TRIANUMER 'Consecutivo Triage', 
               A.NUMINGRES 'Ingreso', 
               F.FECHINIHI 'F Inicia Atención', 
               F.FECHFINH 'F Finaliza atención', 
               F.CODPROSAL 'Cod Prof Consultorio', 
               H.NOMUSUARI 'Prof Consultorio', 
               F.CODCENATE 'Cod Centro Atención', 
               I.NOMCENATE 'Centro Atención', 
               F.UFUCODIGO 'Cod Unidad Funcional', 
               J.UFUDESCRI 'Unidad Funcional', 
               A.NUMINGRES 'Ingreso', 
               L.Code 'Cod Entidad', 
               L.Name 'Entidad'
        FROM.ADTRIAGEU A
            JOIN.INPACIENT B WITH(NOLOCK) ON B.IPCODPACI = A.IPCODPACI
            LEFT JOIN.ADCONTURG C WITH(NOLOCK) ON C.CODCONCEC = A.CODCONCEC
            JOIN.SEGusuaru D WITH(NOLOCK) ON D.CODUSUARI = C.CODUSUARI
            JOIN.SEGusuaru E WITH(NOLOCK) ON E.CODUSUARI = A.CODPROSAL
            LEFT JOIN.HCURGING1 F WITH(NOLOCK) ON F.NUMINGRES = A.NUMINGRES
            LEFT JOIN.INUNIFUNC G WITH(NOLOCK) ON G.UFUCODIGO = F.UFUCODIGO
            LEFT JOIN.SEGusuaru H WITH(NOLOCK) ON H.CODUSUARI = F.CODPROSAL
            LEFT JOIN.ADCENATEN I WITH(NOLOCK) ON I.CODCENATE = F.CODCENATE
            LEFT JOIN.INUNIFUNC J WITH(NOLOCK) ON J.UFUCODIGO = F.UFUCODIGO
            LEFT JOIN.ADINGRESO K WITH(NOLOCK) ON K.NUMINGRES = A.NUMINGRES
            LEFT JOIN Contract.HealthAdministrator L WITH(NOLOCK) ON L.Id = K.GENCONENTITY
        WHERE C.IPFECLLEGA BETWEEN @FechaIni AND @FechaFin
              AND G.UFUTIPUNI IN(1);
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de control del proceso completo de atención en urgencias: triage y atención inicial. Para un rango de fechas dado, consolida en una sola consulta los tiempos y actores de cada etapa: llegada del paciente (control de presencia/llamado en urgencias), inicio y fin del triage con su clasificación de prioridad (1-Reanimación a 5-No Urgente), y apertura y cierre de la atención en consultorio. Combina datos del paciente (cédula, nombre, edad, tipo de documento), el profesional que realizó el triage, el profesional que atendió en consultorio, el centro de atención, la unidad funcional y la entidad aseguradora o EPS responsable del ingreso. Sirve como informe de gestión y auditoría de tiempos de respuesta en urgencias para verificar el cumplimiento de los estándares de atención oportuna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Triage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Triage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los pacientes atendidos en urgencias con sus tiempos de control de llegada, triage y atención inicial, junto con profesionales, centro de atención, unidad funcional y entidad responsable de pago, en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Triage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicio y fin) debe estar definido para filtrar la fecha de llegada del control de paciente.; Deben existir registros en triage de urgencias con paciente asociado y profesional responsable.; El JOIN obligatorio con INUNIFUNC (G) por la atención HCURGING1 implica que solo aparecen ingresos con unidad funcional registrada y de tipo urgencias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Triage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros cuya unidad funcional asociada a la atención de urgencias tenga UFUTIPUNI = 1 (tipo unidad de urgencias).; El periodo de reporte se delimita por la fecha de llegada del control de paciente (IPFECLLEGA) entre el rango parametrizado.; La edad del paciente se calcula como diferencia en años entre la fecha de nacimiento y la fecha de finalización del triage.; Cada fila combina un único triage con su control de paciente, ingreso, atención inicial de urgencias y entidad responsable de pago.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Triage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Triage de urgencias; Clasificación de triage (Reanimación, Emergencia, Urgencia Médica, Urgencia Diferida, No Urgente); Control de paciente / llegada a urgencias; Ingreso hospitalario; Atención inicial de urgencias; Profesional de salud; Centro de atención; Unidad funcional; Entidad administradora de salud (pagador); Tipo de documento de identidad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Triage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando IPFECLLEGA del control de urgencias está entre @FechaIni y @FechaFin y la unidad funcional de la atención (UFUTIPUNI) es 1, se devuelve fila con datos demográficos, triage, atención y entidad pagadora.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Triage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (1..11) → Se traduce a etiqueta de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC) else Cualquier otro valor se reporta como ''PE'' (pendiente/extranjero); si TRIAGECLA (1..5) → Se clasifica el triage en sus 5 niveles: 1.Reanimación, 2.Emergencia, 3.Urgencia Médica, 4.Urgencia Diferida, 5.No Urgente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Triage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADTRIAGEU; dbo.INPACIENT; dbo.ADCONTURG; dbo.SEGusuaru; dbo.HCURGING1; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.ADINGRESO; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Triage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Triage';
-- GO
