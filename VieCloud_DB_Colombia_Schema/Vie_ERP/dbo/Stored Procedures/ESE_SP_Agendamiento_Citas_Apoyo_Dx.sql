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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las citas de apoyo diagnóstico (laboratorio e imagenología) agendadas en un rango de fechas, consolidando en un solo resultado la información del paciente (cédula, nombre, fecha de nacimiento, edad, tipo de documento), la cita (fecha de solicitud, fecha deseada, fecha ofertada, hora, estado, tipo, modalidad presencial o telefónica, si es cita extra), el centro de atención, la actividad médica, el código CUPS del servicio, la causa de cancelación y el usuario que asignó o canceló la cita. Complementa el registro con datos del grupo de atención y entidad pagadora (EPS/aseguradora) del paciente, y cuando aplica, identifica el usuario y especialidad del profesional que recolectó la muestra de laboratorio o tomó la imagen diagnóstica. Se usa para reportería operativa y seguimiento de citas de apoyo diagnóstico ambulatorio, permitiendo auditar el cumplimiento, cancelaciones y trazabilidad de las órdenes de laboratorio e imágenes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta las citas de apoyo diagnóstico (laboratorio e imágenes) registradas en un rango de fechas, con datos del paciente, entidad, estado de la cita y profesional que recolecta muestra o toma imagen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin del rango deben ser provistas para filtrar por FECREGSIS (fecha de registro de la cita).; Las tablas maestras de paciente, usuario, actividad, centro de atención, CUPS, grupo de atención y entidad administradora deben estar pobladas para resolver descripciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El filtro temporal se aplica sobre la fecha de registro de la cita (FECREGSIS), no sobre la fecha de la cita en sí.; La asociación con orden de laboratorio (AMBORDLAB) y orden de imágenes (AMBORDIMA) se hace por el número de autorización/cita CODAUTONU = NUMCONCIT.; El cruce con Contract.HealthAdministrator y con causas de cancelación, órdenes de laboratorio e imagen es opcional (LEFT JOIN), por lo que las citas sin esos datos igual se incluyen.; La edad del paciente se calcula en años entre la fecha de nacimiento y la fecha/hora de inicio de la cita.; El procedimiento es de solo lectura: no realiza INSERT/UPDATE/DELETE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita de apoyo diagnóstico; Paciente; Tipo de documento de identidad; Grupo de atención; Entidad administradora de salud (EPS); Estado de cita (Asignada, Cumplida, Incumplida, PreAsignada, Cancelada); Cita extra; Modalidad de solicitud (Presencial/Telefónica); Causa y observación de cancelación; Tipo de cita (Primera vez, Control, Pos Operatorio); CUPS; Orden de laboratorio y recolección de muestra; Orden de imágenes diagnósticas; Especialidad del profesional; Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGASICITA: Devuelve un resultset DISTINCT de citas cuya FECREGSIS está entre @FechaIni y @FechaFin, enriquecido con datos de paciente, profesional, especialidad, centro y CUPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente → Se traduce el código a etiqueta de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC) else Si el código no coincide con 1-11, se etiqueta como ''PE: Permiso especial de Permanencia''; si CODESTCIT de la cita → 0=Asignada, 1=Cumplida, 2=Incumplida, 3=PreAsignada else Cualquier otro valor se reporta como ''Cita Cancelada''; si CITAEXTRA = 0 → Se marca la cita como ''NO'' extra else Se marca como ''SI'' extra; si CODTIPSOL = 0 → Modalidad de solicitud ''Presencial'' else Modalidad ''Telefonica''; si CODTIPCIT → 0=Primera Vez, 1=Control else Cualquier otro valor se reporta como ''Pos Operatorio''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INPACIENT; dbo.SEGusuaru; dbo.AGACTIMED; dbo.ADCENATEN; dbo.INCUPSIPS; dbo.AGCAUCACE; Contract.CareGroup; Contract.HealthAdministrator; dbo.AMBORDLAB; dbo.INPROFSAL; dbo.INESPECIA; dbo.AMBORDIMA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_Citas_Apoyo_Dx';
-- GO
