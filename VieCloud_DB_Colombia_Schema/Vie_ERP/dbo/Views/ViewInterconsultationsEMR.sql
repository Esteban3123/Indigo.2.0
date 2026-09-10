

CREATE VIEW [dbo].[ViewInterconsultationsEMR]
AS

SELECT
    -- RTRIM(IPNOMCOMP) AS Nombre,
    (RTRIM(F.IPPRIAPEL) + ' ' + RTRIM(F.IPSEGAPEL) + ', ' + RTRIM(F.IPPRINOMB) + ' ' + RTRIM(F.IPSEGNOMB)) AS Nombre,
    F.IPPRINOMB AS PrimerNombre,
    F.IPSEGNOMB AS SegundoNombre,
    F.IPPRIAPEL AS PrimerApellido,
    F.IPSEGAPEL AS SegundoApellido,
    F.CODIGONIT AS NumeroDocumento,
    F.IPFECNACI AS FechaNacimiento,
    CASE F.IPSEXOPAC 
        WHEN '1' THEN 'male' 
        WHEN '2' THEN 'female' 
        ELSE 'other' 
    END AS Sexo,
    RTRIM(D.UFUDESCRI) AS Unidad,
    RTRIM(DESCCAMAS) AS Cama,
    RTRIM(A.CODCENATE) AS CodigoCentroAtencion,
    RTRIM(B.CODPROSAL) AS CodigoProfesional,
    CASE A.PRISERIPS
        WHEN '1' THEN 'urgent'
        WHEN '2' THEN 'routine'
    END AS TipoExamen,
    CASE A.ESTSERIPS
        WHEN '1' THEN 'requested'
        WHEN '2' THEN 'request_sent'
        WHEN '3' THEN 'interconsultation_completed'
        WHEN '4' THEN 'extramural'
        WHEN '5' THEN 'canceled'
        WHEN '6' THEN 'pending_specialist_verification'
    END AS Estado,
    RTRIM(B.NOMMEDICO) AS Medico,
    RTRIM(Esp.DESESPECI) AS Especialidad,
    A.FECORDMED AS FechaSolicitud,
    A.OBSSERIPS AS Observaciones,
    RTRIM(N.DESESPECI) AS EspecialidadTratante
FROM dbo.HCORDINTE A
INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
INNER JOIN dbo.INPACIENT F ON A.IPCODPACI = F.IPCODPACI
INNER JOIN dbo.ADINGRESO G ON F.IPCODPACI = G.IPCODPACI
INNER JOIN dbo.CHCAMASHO H ON G.CODCAMACT = H.CODICAMAS
INNER JOIN dbo.ADcenaten C ON A.CODCENATE = C.codcenate
INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
INNER JOIN dbo.INESPECIA Esp ON A.CODESPECI = Esp.CODESPECI
INNER JOIN DBO.HCHISPACA J 
    ON A.NUMEFOLIO = J.NUMEFOLIO 
    AND A.IPCODPACI = J.IPCODPACI
LEFT JOIN dbo.INESPECIA N ON J.CODESPTRA = N.CODESPECI
WHERE A.ESTSERIPS = 6
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta/reporting que expone las interconsultas médicas filtradas exclusivamente por el estado 6 ("pending_specialist_verification"). Combina datos demográficos del paciente, su cama y unidad de hospitalización actual, el profesional solicitante, la especialidad requerida y la especialidad tratante, junto con el tipo de examen (urgente/rutina) y las observaciones de la orden. Sirve como fuente de información para gestionar interconsultas pendientes de verificación por el especialista en el módulo de historia clínica electrónica (EMR).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las interconsultas pendientes de verificación por especialista, enriquecidas con datos del paciente, ubicación, profesional solicitante y especialidades involucradas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe correspondencia del paciente en INPACIENT y un ingreso activo en ADINGRESO con cama asignada en CHCAMASHO.; La orden de interconsulta debe tener centro de atención, unidad funcional, especialidad y profesional válidos en sus respectivos catálogos.; Debe existir historia clínica (HCHISPACA) asociada al folio y paciente de la orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre completo se construye como ''PrimerApellido SegundoApellido, PrimerNombre SegundoNombre'' con espacios recortados.; Solo se exponen interconsultas en estado ''pendiente de verificación por especialista'' (ESTSERIPS=6).; La especialidad tratante puede ser nula (LEFT JOIN sobre la historia clínica), pero la especialidad solicitada de la orden siempre está presente.; Cada fila representa una interconsulta vinculada obligatoriamente a un ingreso con cama asignada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Interconsulta; Paciente; Ingreso hospitalario; Cama hospitalaria; Centro de atención; Unidad funcional; Especialidad médica; Especialidad tratante; Profesional de salud; Tipo de examen (urgente/rutinario); Estado de la solicitud (solicitada, enviada, completada, extramural, cancelada, pendiente verificación); Sexo del paciente; Folio de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDINTE: Solo retorna órdenes de interconsulta con ESTSERIPS = 6 (pendientes de verificación por especialista).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = ''1'' → Sexo se mapea a ''male'' else Si ''2'' → ''female''; cualquier otro → ''other''; si PRISERIPS = ''1'' → TipoExamen = ''urgent'' else Si ''2'' → ''routine''; otro → NULL; si ESTSERIPS = ''1''..''6'' → Estado mapeado a requested/request_sent/interconsultation_completed/extramural/canceled/pending_specialist_verification respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDINTE; dbo.INPROFSAL; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INESPECIA; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewInterconsultationsEMR';
GO
