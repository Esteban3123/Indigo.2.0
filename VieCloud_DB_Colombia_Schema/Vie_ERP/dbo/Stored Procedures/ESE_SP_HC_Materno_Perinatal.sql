-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 10/05/2019,>  
-- Description: <Description, Todas las HC de control perinatal ID = 11,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_HC_Materno_Perinatal]  
--@FechaIni datetime,  
--@FechaFin datetime  
AS
    BEGIN
        SELECT CASE B.IPTIPODOC
                   WHEN '1'
                   THEN 'CC: Cédula de Ciudadanía'
                   WHEN '2'
                   THEN 'CE: Cédula de Extranjería'
                   WHEN '3'
                   THEN 'TI: Tarjeta de Identidad'
                   WHEN '4'
                   THEN 'RC: Registro Civil'
                   WHEN '5'
                   THEN 'PA: Pasaporte'
                   WHEN '6'
                   THEN 'AS: Adulto Sin Identificación'
                   WHEN '7'
                   THEN 'MS: Menor Sin Identificación'
                   WHEN '8'
                   THEN 'NU: Número único de identificación personal'
                   WHEN '9'
                   THEN 'CN: Certificado Nacido Vivo'
                   WHEN '10'
                   THEN 'CD: Carnet Diplomático'
                   WHEN '11'
                   THEN 'SC: Salvoconducto'
                   ELSE 'PE: Permiso especial de Permanencia'
               END AS Tipo_Doc, 
               A.IPCODPACI 'Doc Paciente', 
               B.IPNOMCOMP 'Paciente', 
               B.IPDIRECCI AS 'Direccion', 
               FORMAT(B.IPFECNACI, 'dd/MM/yyyy') 'F Nacimiento', 
               C.CODENTIDA 'Cod Entidad', 
               D.NOMENTIDA 'Entidad',
               CASE G.TIPCITMED
                   WHEN 1
                   THEN 'Primera Vez'
                   WHEN 2
                   THEN 'Control'
               END 'Primera Vez - Control', 
               G.MOTCONSUL 'Motivo Consulta', 
               G.ENFACTUAL 'Enfermedad Actual', 
               G.ANALISISP 'Análisis', 
               G.INDICAMED 'Recomendaciones', 
               A.CODDIAGNO 'Cod Diagno', 
               F.NOMDIAGNO 'Diagnostico', 
               E.CANTPRENA 'N° Control Prenatal', 
               E.NOMSEMGES 'Edad Gest. (Semanas)', 
               E.RIESOBTET 'Riesgo Obst', 
               A.NUMEFOLIO 'Folio HC', 
               A.FECHISPAC 'F Historia', 
               A.NUMINGRES 'Ingreso', 
               A.CODCENATE 'Cod CA', 
               H.NOMCENATE 'Centro Atención', 
               A.CODPROSAL 'Cod Prof', 
               I.NOMMEDICO 'Profesional', 
               CAST(X.PESOPACIE AS INT) AS 'Peso (gr)', 
               X.TALLAPACI 'Talla (cm)', 
               HEMOGLO AS 'Hemoglobina', 
               format(FECHAHEMO, 'dd/MM/yyyy') AS 'Fecha hemoglobina', 
               FECULTMEN AS 'FUM'
        FROM.HCHISPACA A
            JOIN.INPACIENT B WITH(NOLOCK) ON B.IPCODPACI = A.IPCODPACI
            JOIN.ADINGRESO C WITH(NOLOCK) ON C.NUMINGRES = A.NUMINGRES
            JOIN.INENTIDAD D WITH(NOLOCK) ON D.CODENTIDA = C.CODENTIDA
            LEFT JOIN.HCANTGINE E WITH(NOLOCK) ON E.NUMINGRES = A.NUMINGRES
                                                  AND E.NUMEFOLIO = A.NUMEFOLIO
            JOIN.INDIAGNOS F WITH(NOLOCK) ON F.CODDIAGNO = A.CODDIAGNO
            JOIN.HCURGING1 G WITH(NOLOCK) ON G.NUMINGRES = A.NUMINGRES
                                             AND G.NUMEFOLIO = A.NUMEFOLIO
            JOIN.ADCENATEN H WITH(NOLOCK) ON H.CODCENATE = A.CODCENATE
            JOIN.INPROFSAL I WITH(NOLOCK) ON I.CODPROSAL = A.CODPROSAL
            LEFT JOIN.HCEXFISIC X WITH(NOLOCK) ON X.NUMINGRES = A.NUMINGRES
                                                  AND X.NUMEFOLIO = A.NUMEFOLIO
        WHERE A.IDMODELOHC = 11
              AND A.FECHISPAC BETWEEN '01/05/2019' AND '01/06/2019'; --@FechaIni AND @FechaFin  
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que extrae el listado completo de historias clínicas de control materno-perinatal (modelo de HC identificado como ID 11), combinando datos del paciente (cédula, nombre, dirección, fecha de nacimiento, tipo de documento), su aseguradora o entidad pagadora, el episodio de ingreso, el diagnóstico CIE-10, la nota clínica de urgencias (motivo de consulta, enfermedad actual, análisis y recomendaciones), los antecedentes gineco-obstétricos (número de control prenatal, edad gestacional en semanas, riesgo obstétrico, fecha de última menstruación), el examen físico (peso y talla), datos de hemoglobina, el centro de atención y el profesional de salud tratante. Sirve como informe operativo y de seguimiento del programa de maternidad segura y atención perinatal, integrando información de historias clínicas, admisiones, pacientes, entidades, diagnósticos, ginecología y examen físico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_Materno_Perinatal';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_HC_Materno_Perinatal';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las historias clínicas de control materno-perinatal (modelo HC = 11) con datos del paciente, entidad, diagnóstico, antecedentes gineco-obstétricos, examen físico y profesional tratante, en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Materno_Perinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en HCHISPACA con IDMODELOHC = 11; Integridad referencial entre ingreso, paciente, entidad, diagnóstico, centro de atención y profesional; Rango de fechas de historia clínica codificado (01/05/2019 a 01/06/2019)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Materno_Perinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan historias clínicas del modelo perinatal (IDMODELOHC = 11); Antecedentes gineco-obstétricos (HCANTGINE) y examen físico (HCEXFISIC) son opcionales (LEFT JOIN); su ausencia no excluye el registro; Paciente, ingreso, entidad, diagnóstico, urgencias/consulta, centro de atención y profesional son obligatorios (INNER JOIN); El rango de fechas está hardcodeado, no parametrizado; El peso del paciente se reporta truncado a entero (gramos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Materno_Perinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Control materno-perinatal; Tipo de documento de identidad; Paciente; Entidad / aseguradora; Ingreso; Diagnóstico; Antecedentes gineco-obstétricos; Edad gestacional; Riesgo obstétrico; Control prenatal; Centro de atención; Profesional de la salud; Examen físico (peso, talla); Hemoglobina; Fecha de última menstruación (FUM); Cita primera vez / control', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Materno_Perinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de resultados con HC de modelo 11 cuya FECHISPAC esté entre 01/05/2019 y 01/06/2019, mapeando IPTIPODOC a su descripción y TIPCITMED a ''Primera Vez''/''Control''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Materno_Perinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (valores 1..11) → Traduce el código a etiqueta del tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC) else Si no coincide ningún valor listado se etiqueta como ''PE: Permiso especial de Permanencia''; si TIPCITMED = 1 → Etiqueta la cita como ''Primera Vez'' else Si TIPCITMED = 2 se etiqueta como ''Control''; si A.IDMODELOHC = 11 AND FECHISPAC BETWEEN ''01/05/2019'' AND ''01/06/2019'' → Incluye el registro en el resultado else Lo excluye', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Materno_Perinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.HCANTGINE; dbo.INDIAGNOS; dbo.HCURGING1; dbo.ADCENATEN; dbo.INPROFSAL; dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Materno_Perinatal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_HC_Materno_Perinatal';
-- GO
