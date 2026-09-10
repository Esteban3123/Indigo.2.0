-- =============================================  
-- Author:  <Author: William Suaza>  
-- ALTER date: <ALTER Date, 09/05/2019,>  
-- Description: <Description, Consulta todos los resultados positivos de estado de Gravindez,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_LAB_Result_Gravindez] @FechaIni DATETIME, 
                                                    @FechaFin DATETIME
AS
    BEGIN
        SELECT DISTINCT 
               I.VALOR 'Resultado', 
               A.INTERPRET 'Interpretación', 
               A.CODPROINT 'Cod Prof Interpretó', 
               PROF.NOMMEDICO 'Profesional Interpretó', 
               A.NUMFOLINT 'Folio Interpretó',
               CASE P.IPTIPODOC
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
               END AS Tipo_Documento, 
               A.IPCODPACI 'Doc Paciente', 
               P.IPNOMCOMP 'Paciente', 
               P.IPFECNACI 'Fec Nacimiento', 
               DATEDIFF(YEAR, P.IPFECNACI, [Common].[GETDATE]()) 'Edad Años', 
               P.IPTELEFON 'Tel 1', 
               P.IPTELMOVI 'Tel 2', 
               MU.MUNNOMBRE 'Municipio', 
               UB.UBINOMBRE 'Ubicación', 
               P.IPDIRECCI 'Dirección', 
               CA.NOMCENATE 'Centro Atención', 
               A.FECORDMED 'Fec Orden', 
               A.CODPROSAL 'Cod Prof Ordenó', 
               PO.NOMMEDICO 'Profesional Ordenó', 
               A.NUMINGRES 'Ingreso', 
               A.FECRECMUE 'Fec recolecta Muestra', 
               A.USURECMUE 'Cod U. Recolecta Muestra', 
               PR.NOMMEDICO 'U. Recolecta Muestra', 
               CTRL.FECGENERA 'Fecha Resultado', 
               CTRL.CODPROSAL 'Bacteriologa'
        FROM.HCORDLABO A
            LEFT JOIN.INTERLABD I WITH(NOLOCK) ON A.AUTO = I.AUTOLABOR
            JOIN.ADCENATEN CA WITH(NOLOCK) ON CA.CODCENATE = A.CODCENATE
            JOIN.INPROFSAL PO WITH(NOLOCK) ON PO.CODPROSAL = A.CODPROSAL
            JOIN.INPROFSAL PR WITH(NOLOCK) ON PR.CODPROSAL = A.USURECMUE
            LEFT JOIN.INPROFSAL PROF WITH(NOLOCK) ON PROF.CODPROSAL = A.CODPROINT
            JOIN.INPACIENT P WITH(NOLOCK) ON P.IPCODPACI = A.IPCODPACI
            JOIN.INUBICACI UB WITH(NOLOCK) ON UB.AUUBICACI = P.AUUBICACI
            JOIN.INMUNICIP MU WITH(NOLOCK) ON MU.DEPMUNCOD = UB.DEPMUNCOD
            LEFT JOIN.INTERCTRL CTRL WITH(NOLOCK) ON CTRL.AUTOLABOR = I.AUTOLABOR
            LEFT JOIN.INPROFSAL PB WITH(NOLOCK) ON PB.CODPROSAL = CTRL.CODPROSAL
        WHERE A.CODSERIPS = '904508'
              AND A.ESTSERIPS IN(3, 4)
            AND CAST(A.FECRECMUE AS DATE) BETWEEN @FechaIni AND @FechaFin
        UNION ALL
        SELECT DISTINCT 
               I.VALOR 'Resultado', 
               A.INTERPRET 'Interpretación', 
               A.CODPROINT 'Cod Prof Interpretó', 
               PROF.NOMMEDICO 'Profesional Interpretó', 
               A.NUMFOLINT 'Folio Interpretó',
               CASE P.IPTIPODOC
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
               END AS Tipo_Documento, 
               A.IPCODPACI 'Doc Paciente', 
               P.IPNOMCOMP 'Paciente', 
               P.IPFECNACI 'Fec Nacimiento', 
               DATEDIFF(YEAR, P.IPFECNACI, [Common].[GETDATE]()) 'Edad Años', 
               P.IPTELEFON 'Tel 1', 
               P.IPTELMOVI 'Tel 2', 
               MU.MUNNOMBRE 'Municipio', 
               UB.UBINOMBRE 'Ubicación', 
               P.IPDIRECCI 'Dirección', 
               CA.NOMCENATE 'Centro Atención', 
               A.FECORDMED 'Fec Orden', 
               A.CODPROSAL 'Cod Prof Ordenó', 
               PO.NOMMEDICO 'Profesional Ordenó', 
               A.NUMINGRES 'Ingreso', 
               A.FECRECMUE 'Fec recolecta Muestra', 
               A.USURECMUE 'Cod U. Recolecta Muestra', 
               PR.NOMMEDICO 'U. Recolecta Muestra', 
               CTRL.FECGENERA 'Fecha Resultado', 
               CTRL.CODPROSAL 'Bacteriologa'
        FROM.AMBORDLAB A
            LEFT JOIN.INTERLABD I WITH(NOLOCK) ON A.AUTO = I.AUTOLABOR
            JOIN.ADCENATEN CA WITH(NOLOCK) ON CA.CODCENATE = A.CODCENATE
            JOIN.INPROFSAL PO WITH(NOLOCK) ON PO.CODPROSAL = A.CODPROSAL
            JOIN.INPROFSAL PR WITH(NOLOCK) ON PR.CODPROSAL = A.USURECMUE
            LEFT JOIN.INPROFSAL PROF WITH(NOLOCK) ON PROF.CODPROSAL = A.CODPROINT
            JOIN.INPACIENT P WITH(NOLOCK) ON P.IPCODPACI = A.IPCODPACI
            JOIN.INUBICACI UB WITH(NOLOCK) ON UB.AUUBICACI = P.AUUBICACI
            JOIN.INMUNICIP MU WITH(NOLOCK) ON MU.DEPMUNCOD = UB.DEPMUNCOD
            LEFT JOIN.INTERCTRL CTRL WITH(NOLOCK) ON CTRL.AUTOLABOR = I.AUTOLABOR
            LEFT JOIN.INPROFSAL PB WITH(NOLOCK) ON PB.CODPROSAL = CTRL.CODPROSAL
        WHERE A.CODSERIPS = '904508'
              AND A.ESTSERIPS IN(3, 4)
            AND CAST(A.FECRECMUE AS DATE) BETWEEN @FechaIni AND @FechaFin;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de resultados positivos de pruebas de embarazo (test de gravidez, CUPS 904508) realizadas en el laboratorio clínico, filtrando por un rango de fechas de recolección de muestra. Combina órdenes de laboratorio de hospitalización (HCORDLABO) y de consulta ambulatoria (AMBORDLAB) con sus resultados analíticos (INTERLABD) e información de control de la muestra (INTERCTRL), enriqueciendo cada registro con datos completos de la paciente (identificación, tipo de documento, nombre, fecha de nacimiento, edad calculada, teléfonos, dirección, municipio y ubicación), el centro de atención donde se ordenó el examen, el profesional que ordenó la prueba, el usuario que recolectó la muestra, el profesional que interpretó el resultado y la bacterióloga responsable del informe. Se usa para vigilancia epidemiológica, seguimiento materno y auditoría de resultados de pruebas de gravidez en todos los puntos de atención de la institución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los resultados de pruebas de laboratorio de gravindez (código de servicio 904508) procesadas o interpretadas, tanto de órdenes hospitalarias como ambulatorias, dentro de un rango de fechas de recolección de muestra.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (FechaIni, FechaFin) debe estar definido y aplicarse sobre la fecha de recolección de muestra (FECRECMUE).; Deben existir profesionales válidos en INPROFSAL para los códigos de profesional que ordenó (CODPROSAL) y de usuario que recolectó la muestra (USURECMUE), pues se unen con JOIN obligatorio.; El paciente, centro de atención, ubicación y municipio deben existir en sus catálogos (INPACIENT, ADCENATEN, INUBICACI, INMUNICIP).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con código de servicio IPS ''904508'' (prueba de gravindez/embarazo).; Solo se consideran órdenes con estado de servicio (ESTSERIPS) 3 o 4.; El filtro de fechas se aplica sobre la fecha de recolección de muestra convertida a DATE, no sobre la fecha de orden ni de resultado.; Toda fila siempre traduce el tipo de documento del paciente a una etiqueta legible; nunca se devuelve el código numérico crudo.; La edad se calcula en años completos respecto a la fecha actual del sistema vía [Common].[GETDATE].; Se consultan tanto órdenes hospitalarias como ambulatorias, sin deduplicar entre ambos orígenes (UNION ALL).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Resultado de laboratorio; Prueba de gravindez/embarazo; Orden médica hospitalaria; Orden médica ambulatoria; Recolección de muestra; Interpretación de resultado; Profesional de salud (ordena, interpreta, recolecta, bacteriólogo); Paciente; Tipo de documento de identidad; Centro de atención; Ubicación geográfica (municipio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas DISTINCT de HCORDLABO y AMBORDLAB unidas con UNION ALL, filtrando por CODSERIPS=''904508'', ESTSERIPS IN (3,4) y FECRECMUE entre @FechaIni y @FechaFin.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si P.IPTIPODOC entre ''1'' y ''11'' → Mapea cada código a la etiqueta correspondiente (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC). else Cualquier otro valor se etiqueta como ''PE: Permiso especial de Permanencia''.; si Origen de la orden de laboratorio → Si la orden proviene de hospitalización (HCORDLABO) se incluye en la primera consulta; si es ambulatoria (AMBORDLAB) se incluye en la segunda; ambas se concatenan con UNION ALL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.AMBORDLAB; dbo.INTERLABD; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INPACIENT; dbo.INUBICACI; dbo.INMUNICIP; dbo.INTERCTRL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_Gravindez';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_Gravindez';
-- GO
