-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 14/05/2019,>  
-- Description: <Description, HC de Primera Infancia, Infancia y adoslecencia>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_HC_PrimeraInfancia_Infancia_Adolescencia] @FechaIni DATETIME, 
                                                                                    @FechaFin DATETIME
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
               END AS Tipo_Documento, 
               A.IPCODPACI 'Doc Paciente', 
               C.UBINOMBRE 'Ubicación', 
               B.IPDIRECCI 'Dirección', 
               B.IPTELEFON 'Tel 1', 
               B.IPTELMOVI 'Tel 2', 
               FORMAT(B.IPFECNACI, 'dd/MM/yyyy') 'Fecha Nacimiento', 
               B.IPNOMCOMP 'Nombre Paciente',
               CASE B.IPSEXOPAC
                   WHEN 1
                   THEN 'M'
                   WHEN 2
                   THEN 'F'
               END 'Sexo', 
               CAST(D.PESOPACIE AS INT) AS 'Peso (gr)', 
               D.TALLAPACI 'Talla (cm)',
               CASE B.IPTIPOPAC
                   WHEN 1
                   THEN 'Contributivo'
                   WHEN 2
                   THEN 'Subsidiado'
                   WHEN 3
                   THEN 'Vinculado'
                   WHEN 4
                   THEN 'Particular'
                   WHEN 5
                   THEN 'Otro'
                   WHEN 6
                   THEN 'Desplazado Reg. Contributivo'
                   WHEN 7
                   THEN 'Reg. Subsidiado'
                   WHEN 8
                   THEN 'Desplazado No Asegurado'
               END 'Regimen', 
               B.CODENTIDA, 
               F.Code 'Cod Entidad', 
               F.Name 'Entidad', 
               G.NOMBRE 'Modelo HC', 
               FORMAT(A.FECHISPAC, 'dd/MM/yyyy') AS 'Fecha de La historia'
        FROM.HCHISPACA A
            JOIN.INPACIENT B WITH(NOLOCK) ON B.IPCODPACI = A.IPCODPACI
            JOIN.INUBICACI C WITH(NOLOCK) ON C.AUUBICACI = B.AUUBICACI
            LEFT JOIN.HCEXFISIC D WITH(NOLOCK) ON D.NUMINGRES = A.NUMINGRES
                                                  AND D.NUMEFOLIO = A.NUMEFOLIO
            JOIN dbo.ADINGRESO E WITH(NOLOCK) ON E.NUMINGRES = A.NUMINGRES
            JOIN Contract.HealthAdministrator F WITH(NOLOCK) ON F.Id = E.GENCONENTITY
            JOIN.PRMODELOHC G WITH(NOLOCK) ON G.ID = A.IDMODELOHC
        WHERE A.IDMODELOHC IN(7, 8, 9)
            AND A.FECHISPAC BETWEEN @FechaIni AND @FechaFin;
    END;