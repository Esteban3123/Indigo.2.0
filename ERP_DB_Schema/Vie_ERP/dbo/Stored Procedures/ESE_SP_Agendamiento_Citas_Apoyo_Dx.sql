-- =============================================  
-- Author:  <Author,,William Suaza>  
-- ALTER date: <ALTER Date, 09/05/2019,>  
-- Description: <Description, Citas de apoyo Dx,>  
-- =============================================  
CREATE PROCEDURE [dbo].[ESE_SP_Agendamiento_Citas_Apoyo_Dx] @FechaIni DATETIME, 
                                                           @FechaFin DATETIME
AS
    BEGIN
        SELECT DISTINCT 
               CI.FECREGSIS AS F_Solicitud_Cita, 
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
               CI.CODCENATE AS Centro_Atencion, 
               CA.NOMCENATE AS Centro_Atencion, 
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
               CI.CODSERIPS AS Cod_CUPS, 
               CUPS.DESSERIPS AS CUPS, 
               LA.USURECMUE AS Cod_Recolecta_Muestra, 
               USL.NOMUSUARI Usuario_Recoleta_Muestra, 
               ESL.DESESPECI AS Especialidad_Recolecta_Muestra, 
               IM.USURECEXA AS Cod_Usuario_Toma_Imagen, 
               USI.NOMUSUARI AS Usuario_Toma_Imagen, 
               ESI.DESESPECI AS Especialidad_Toma_Imagen
        FROM AGASICITA AS CI
             JOIN.INPACIENT AS I WITH(NOLOCK) ON I.IPCODPACI = CI.IPCODPACI
             JOIN.SEGusuaru AS U WITH(NOLOCK) ON CI.CODUSUASI = U.CODUSUARI
             JOIN.AGACTIMED AS AC WITH(NOLOCK) ON CI.CODACTMED = AC.CODACTMED
             JOIN.ADCENATEN AS CA WITH(NOLOCK) ON CI.CODCENATE = CA.CODCENATE
             JOIN.INCUPSIPS AS CUPS WITH(NOLOCK) ON CI.CODSERIPS = CUPS.CODSERIPS
             LEFT JOIN.AGCAUCACE CC WITH(NOLOCK) ON CI.CODCAUCAN = CC.CODCAUCAN
             JOIN Contract.CareGroup AS GA WITH(NOLOCK) ON I.GENCAREGROUP = GA.Id
             LEFT JOIN Contract.HealthAdministrator AS HA WITH(NOLOCK) ON i.GENCONENTITY = HA.Id
             LEFT JOIN.AMBORDLAB AS LA WITH(NOLOCK) ON CI.CODAUTONU = LA.NUMCONCIT
             LEFT JOIN.SEGusuaru AS USL WITH(NOLOCK) ON LA.USURECMUE = USL.CODUSUARI
             LEFT JOIN.INPROFSAL AS PRL WITH(NOLOCK) ON LA.USURECMUE = PRL.CODPROSAL
             LEFT JOIN.INESPECIA AS ESL WITH(NOLOCK) ON PRL.CODESPEC1 = ESL.CODESPECI
             LEFT JOIN.AMBORDIMA AS IM WITH(NOLOCK) ON CI.CODAUTONU = IM.NUMCONCIT
             LEFT JOIN.SEGusuaru AS USI WITH(NOLOCK) ON IM.USURECEXA = USI.CODUSUARI
             LEFT JOIN.INPROFSAL AS PRI WITH(NOLOCK) ON IM.USURECEXA = PRI.CODPROSAL
             LEFT JOIN.INESPECIA AS ESI WITH(NOLOCK) ON PRI.CODESPEC1 = ESI.CODESPECI
        WHERE FECREGSIS BETWEEN @FechaIni AND @FechaFin;
    END;