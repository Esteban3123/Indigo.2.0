-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 13/05/2019,>  
-- Description: <Description, Consulta los registros de los consentimientos informados adjunto,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_Consentimientos_Informados]
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
                   THEN 'RC'
                   WHEN '9'
                   THEN 'CN'
                   WHEN '10'
                   THEN 'CD'
                   WHEN '11'
                   THEN 'SC'
                   ELSE 'PE'
               END AS Tipo_Documento, 
               A.IPCODPACI 'Doc Paciente', 
               B.IPPRINOMB 'Primer Nombre', 
               B.IPSEGNOMB 'Segundo nombre', 
               B.IPPRIAPEL 'Primer Apellido', 
               B.IPSEGAPEL 'Segundo apellido', 
               A.NUMINGRES 'Ingreso', 
               A.FECPROCES 'F. Adjunto', 
               A.CODCENATE 'Cod. Centro Atención', 
               C.NOMCENATE 'Centro Atención', 
               A.UFUCODIGO 'Cod. Unidad Funcional', 
               D.UFUDESCRI 'Unidad funcional', 
               A.CODUSUARI 'Cod Usuario adjunta', 
               E.NOMUSUARI
        FROM.HCDOCUMAD A
            JOIN.INPACIENT B WITH(NOLOCK) ON B.IPCODPACI = A.IPCODPACI
            JOIN.ADCENATEN C WITH(NOLOCK) ON C.CODCENATE = A.CODCENATE
            JOIN.INUNIFUNC D WITH(NOLOCK) ON D.UFUCODIGO = A.UFUCODIGO
            JOIN.SEGusuaru E WITH(NOLOCK) ON E.CODUSUARI = A.CODUSUARI
        WHERE A.FECPROCES BETWEEN '01/05/2019' AND '30/05/2019'
              AND A.TIPODOCUM = 7;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los consentimientos informados adjuntos a historias clínicas de pacientes (tipo de documento 7), combinando información del paciente (nombre completo, tipo y número de documento/cédula), del ingreso, del centro de atención y de la unidad funcional donde se cargó el archivo, junto con el usuario que lo adjuntó. Integra los documentos adjuntos de la historia clínica (HCDOCUMAD) con el maestro de pacientes (INPACIENT), los centros de atención (ADCENATEN), las unidades funcionales (INUNIFUNC) y los usuarios del sistema (SEGusuaru). Sirve para auditar y reportar qué consentimientos informados han sido digitalizados y asociados a atenciones, identificando quién los cargó, en qué sede y servicio, y en qué fecha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los consentimientos informados adjuntos en un periodo, mostrando datos del paciente, ingreso, centro de atención, unidad funcional y usuario que los cargó.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de documentos adjuntos clasificados con tipo de documento 7 (consentimiento informado); Integridad referencial entre el documento adjunto y paciente, centro de atención, unidad funcional y usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran documentos cuyo tipo es 7 (consentimiento informado); El rango de fechas está fijo en mayo de 2019 (hardcodeado); Todo paciente listado tiene un tipo de documento normalizado (CC, CE, TI, RC, PA, AS, MS, CN, CD, SC o PE); Solo aparecen documentos con paciente, centro de atención, unidad funcional y usuario existentes (joins internos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consentimiento informado; Paciente; Ingreso; Centro de atención; Unidad funcional; Tipo de documento de identidad; Usuario que adjunta documento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando FECPROCES está entre 01/05/2019 y 30/05/2019 y TIPODOCUM = 7, retorna los datos del consentimiento con paciente, centro, unidad funcional y usuario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de documento del paciente (IPTIPODOC) 1..11 → Mapea a códigos de tipo documento: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=RC, 9=CN, 10=CD, 11=SC else Asigna ''PE'' como tipo de documento por defecto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCDOCUMAD; INPACIENT; ADCENATEN; INUNIFUNC; SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Consentimientos_Informados';
-- GO
