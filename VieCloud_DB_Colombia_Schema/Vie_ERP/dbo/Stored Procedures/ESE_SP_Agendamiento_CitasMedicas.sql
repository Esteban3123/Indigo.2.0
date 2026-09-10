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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de citas médicas agendadas en un rango de fechas dado. Consolida en una sola consulta toda la información relevante de cada cita: datos del paciente (tipo y número de documento, nombre completo, fecha de nacimiento y edad), datos del agendamiento (fecha de solicitud, fecha de la cita, fecha deseada por el paciente, fecha ofertada, estado, modalidad de solicitud, tipo de cita —primera vez, control o posoperatorio— y si es cita extra), datos del prestador (profesional de salud, especialidad, actividad médica, código CUPS), datos de la sede (centro de atención), datos del usuario que asignó o canceló la cita (con causa y observación de cancelación cuando aplica), y datos del aseguramiento del paciente (grupo de atención y entidad pagadora —EPS o aseguradora—). Se usa principalmente para auditoría, seguimiento operativo y reportería gerencial del proceso de agendamiento ambulatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de citas médicas agendadas en un rango de fechas, enriqueciendo datos del paciente, profesional, especialidad, entidad pagadora, estado de cita y código CUPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas maestras de pacientes, profesionales, especialidades, actividades médicas, centros de atención y CUPS deben existir y estar relacionadas correctamente con la cita.; Cada cita debe tener paciente, usuario asignador, profesional, especialidad, actividad médica, centro de atención y CUPS asociados (joins INNER).; El paciente debe tener un grupo de atención (CareGroup) válido en el contrato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad del paciente se calcula en años entre la fecha de nacimiento y la fecha/hora de la cita (no la fecha actual).; Solo se incluyen citas que tengan paciente, usuario asignador, profesional, especialidad, actividad médica, centro de atención, CUPS y grupo de atención (CareGroup) válidos; la entidad administradora de salud y la causa de cancelación son opcionales.; El filtro temporal aplica sobre la fecha de registro/solicitud de la cita (FECREGSIS), no sobre la fecha de la cita misma.; Todo estado de cita distinto de 0,1,2,3 se reporta como ''Cita Cancelada''.; Todo tipo de documento fuera del rango 1-11 se reporta como ''Permiso especial de Permanencia''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Agendamiento; Paciente; Tipo de documento de identificación; Profesional de salud; Especialidad médica; Actividad médica; Centro de atención; Grupo de atención (CareGroup); Entidad administradora de salud (EPS/ARS); Estado de cita (Asignada/Cumplida/Incumplida/PreAsignada/Cancelada); Causa de cancelación; Cita extra; Modalidad de solicitud (Presencial/Telefónica); Tipo de cita (Primera Vez/Control/Pos Operatorio); Código CUPS; Fecha deseada por paciente; Fecha ofertada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] AGASICITA: Devuelve resultset con citas cuyo FECREGSIS está entre @FechaIni y @FechaFin, junto con datos descriptivos del paciente, profesional, entidad y estado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC del paciente (1-11) → Mapea a etiqueta de tipo de documento (CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC) else Cualquier otro valor se etiqueta como ''PE: Permiso especial de Permanencia''; si CODESTCIT de la cita (0-3) → Mapea a estado: 0=Asignada, 1=Cumplida, 2=Incumplida, 3=PreAsignada else Cualquier otro valor se considera ''Cita Cancelada''; si CITAEXTRA = 0 → Cita_Extra=''NO'' else Cita_Extra=''SI''; si CODTIPSOL = 0 → Modalidad_Solicitud=''Presencial'' else Modalidad_Solicitud=''Telefonica''; si CODTIPCIT (0,1) → 0=Primera Vez, 1=Control else Cualquier otro valor=''Pos Operatorio''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INPACIENT; dbo.SEGusuaru; dbo.INPROFSAL; dbo.INESPECIA; dbo.AGACTIMED; dbo.ADCENATEN; dbo.INCUPSIPS; dbo.AGCAUCACE; Contract.CareGroup; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Agendamiento_CitasMedicas';
-- GO
