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