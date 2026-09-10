CREATE PROCEDURE [dbo].[ESE_SP_LAB_Result_GravindezXPaciente] @Id VARCHAR(25)
AS
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
         AND A.IPCODPACI = @Id  
     --AND cast(A.FECRECMUE as date ) BETWEEN @FechaIni AND @FechaFin  

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
           AND A.ESTSERIPS IN(3, 4)-- AND cast(A.FECRECMUE as date ) BETWEEN @FechaIni AND @FechaFin  
         AND A.IPCODPACI = @Id;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el resultado del examen de laboratorio de prueba de embarazo (código CUPS 904508) para una paciente específica, identificada por su documento o cédula. Integra las órdenes médicas de laboratorio tanto de hospitalización (HCORDLABO) como de atención ambulatoria (AMBORDLAB) en un solo resultado, cruzando con los detalles del analito (INTERLABD), el control e interpretación del resultado (INTERCTRL), los datos demográficos y de contacto de la paciente (INPACIENT), su ubicación y municipio (INUBICACI, INMUNICIP), el centro de atención (ADCENATEN) y los profesionales involucrados: quien ordenó el examen, quien recolectó la muestra y quien interpretó el resultado (INPROFSAL). Solo retorna órdenes en estado procesado o entregado (estados 3 y 4). Se usa para obtener el historial de pruebas de embarazo de una paciente, incluyendo resultados, interpretación clínica, bacterióloga responsable y datos completos de identificación y contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta unificada de resultados de la prueba de laboratorio de gravindez (CUPS 904508) realizados a un paciente, combinando órdenes hospitalarias y ambulatorias con datos demográficos, profesionales y de la muestra.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente identificado debe existir en INPACIENT con ubicación válida (INUBICACI) y municipio (INMUNICIP).; Las órdenes deben tener centro de atención (ADCENATEN), profesional ordenante y usuario recolector registrados en INPROFSAL.; Solo se consideran órdenes del servicio CUPS ''904508'' (prueba de gravindez/embarazo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan estudios cuyo estado del servicio (ESTSERIPS) sea 3 o 4 (resultados procesados/validados), excluyendo otros estados.; El servicio consultado siempre es el CUPS 904508 (prueba de embarazo/gravindez).; La edad se calcula en años completos respecto a la fecha actual del sistema vía [Common].[GETDATE]().; Se consolidan en un único resultado las órdenes provenientes de hospitalización y de ambulatorio para el mismo paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Resultado de laboratorio; Prueba de gravindez/embarazo (CUPS 904508); Paciente; Tipo de documento de identificación; Profesional que ordena; Profesional que interpreta; Bacterióloga; Centro de atención; Recolección de muestra; Orden médica hospitalaria y ambulatoria; Ubicación y municipio del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCORDLABO+AMBORDLAB: Devuelve un resultset UNION ALL con resultados de laboratorio cuando A.CODSERIPS=''904508'' AND A.ESTSERIPS IN (3,4) AND A.IPCODPACI=@Id, tanto para órdenes de hospitalización como ambulatorias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si P.IPTIPODOC entre ''1''..''11'' → Traduce el código a etiqueta legible del tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC). else Cualquier otro valor se interpreta como ''PE: Permiso especial de Permanencia''.; si Origen de la orden de laboratorio → Si proviene de HCORDLABO (hospitalización) se incluye en el primer bloque; si proviene de AMBORDLAB (ambulatorio) se incluye en el segundo bloque del UNION ALL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCORDLABO; AMBORDLAB; INTERLABD; ADCENATEN; INPROFSAL; INPACIENT; INUBICACI; INMUNICIP; INTERCTRL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_LAB_Result_GravindezXPaciente';
-- GO
