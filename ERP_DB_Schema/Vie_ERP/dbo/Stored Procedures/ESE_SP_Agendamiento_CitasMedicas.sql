CREATE PROCEDURE [dbo].[ESE_SP_Agendamiento_CitasMedicas] @FechaIni DATETIME, 
                                                         @FechaFin DATETIME
AS
    BEGIN
        SELECT CI.FECREGSIS AS F_Solicitud_Cita, 
               CI.FECHORAIN AS F_Cita, 
               CI.FECITADES AS F_Deseada_Paciente, 
               CI.FECHAOFERTADA AS F_Ofertada,
               CASE I.IPTIPODOC
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
               CI.IPCODPACI AS Identificacion, 
               I.IPPRIAPEL AS Primer_Apellido, 
               I.IPSEGAPEL AS Segundo_Apellido, 
               I.IPPRINOMB AS Primer_Nombre, 
               I.IPSEGNOMB AS Segundo_Nombre, 
               I.IPFECNACI AS Fecha_Nacimiento, 
               DATEDIFF(YEAR, i.IPFECNACI, CI.FECHORAIN) Edad_Paciente, 
               GA.Code AS Cod_Grupo_Atencion, 
               GA.Name AS Grupo_Atencion, 
               HA.Code AS Cod_Entidad, 
               HA.Name AS Entidad, 
               CI.CODUSUASI AS Cod_Usuario_Asig_Cita, 
               U.NOMUSUARI AS Usuario_Asig_Cita, 
               CI.CODPROSAL AS Cod_Profesional, 
               PROF.NOMMEDICO AS Profesional, 
               CI.CODCENATE AS Centro_Atencion, 
               CA.NOMCENATE AS Centro_Atencion, 
               CI.CODESPECI AS Cod_Especialidad, 
               ES.DESESPECI AS Especialidad, 
               CI.CODACTMED AS Cod_Actividad, 
               AC.DESACTMED AS Actividad,
               CASE CI.CODESTCIT
                   WHEN 0
                   THEN 'Asignada'
                   WHEN 1
                   THEN 'Cumplida'
                   WHEN 2
                   THEN 'Incumplida'
                   WHEN 3
                   THEN 'PreAsignada'
                   ELSE 'Cita Cancelada'
               END AS Estado,
               CASE Ci.CITAEXTRA
                   WHEN 0
                   THEN 'NO'
                   ELSE 'SI'
               END AS Cita_Extra,
               CASE CI.CODTIPSOL
                   WHEN 0
                   THEN 'Presencial'
                   ELSE 'Telefonica'
               END AS Modalidad_Solicitud, 
               CC.DESCAUCAN AS Causa_Cancelacion, 
               CI.OBSCAUCAN AS Observacion_Cancelacion, 
               CI.CANCELUSU AS Cod_Usuario_Cancela, 
               U.NOMUSUARI AS Usuario_Cancela,
               CASE CI.CODTIPCIT
                   WHEN 0
                   THEN 'Primera Vez'
                   WHEN 1
                   THEN 'Control'
                   ELSE 'Pos Operatorio'
               END AS Primera_Vez_Control, 
               AC.CODSERIPS AS Cod_CUPS, 
               CUPS.DESSERIPS AS CUPS
        FROM.AGASICITA AS CI
            INNER JOIN.INPACIENT AS I WITH(NOLOCK) ON I.IPCODPACI = CI.IPCODPACI
            INNER JOIN.SEGusuaru AS U WITH(NOLOCK) ON CI.CODUSUASI = U.CODUSUARI
            INNER JOIN.INPROFSAL AS PROF WITH(NOLOCK) ON CI.CODPROSAL = PROF.CODPROSAL
            INNER JOIN.INESPECIA AS ES WITH(NOLOCK) ON CI.CODESPECI = ES.CODESPECI
            INNER JOIN.AGACTIMED AS AC WITH(NOLOCK) ON CI.CODACTMED = AC.CODACTMED
            INNER JOIN.ADCENATEN AS CA WITH(NOLOCK) ON CI.CODCENATE = CA.CODCENATE
            INNER JOIN.INCUPSIPS AS CUPS WITH(NOLOCK) ON AC.CODSERIPS = CUPS.CODSERIPS
            LEFT OUTER JOIN.AGCAUCACE CC WITH(NOLOCK) ON CI.CODCAUCAN = CC.CODCAUCAN
            INNER JOIN Contract.CareGroup AS GA WITH(NOLOCK) ON I.GENCAREGROUP = GA.Id
            LEFT OUTER JOIN Contract.HealthAdministrator AS HA WITH(NOLOCK) ON i.GENCONENTITY = HA.Id
        WHERE FECREGSIS BETWEEN @FechaIni AND @FechaFin;
    END;