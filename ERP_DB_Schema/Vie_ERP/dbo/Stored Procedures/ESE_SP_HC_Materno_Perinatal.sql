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