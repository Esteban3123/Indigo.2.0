
CREATE PROCEDURE [dbo].[SPREP_HC_Interconsultations_EMR]
(
    @Paciente VARCHAR(20),
    @NumeroIngreso CHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RTRIM(IPNOMCOMP) AS Nombre,
        RTRIM(F.IPPRINOMB) AS PrimerNombre,
        RTRIM(F.IPSEGNOMB) AS SegundoNombre,
        RTRIM(F.IPPRIAPEL) AS PrimerApellido,
        RTRIM(F.IPSEGAPEL) AS SegundoApellido,
        F.CODIGONIT AS NumeroDocumento,
        F.IPFECNACI AS FechaNacimiento,
        CASE F.IPSEXOPAC 
            WHEN '1' THEN 'male' 
            WHEN '2' THEN 'female' 
            ELSE 'other' 
        END AS Sexo,
        RTRIM(D.UFUDESCRI) AS Unidad,
        RTRIM(DESCCAMAS) AS Cama,
        CASE A.PRISERIPS
            WHEN '1' THEN 'urgent'
            WHEN '2' THEN 'routine'
        END AS TipoExamen,
        CASE A.ESTSERIPS
            WHEN '1' THEN 'requested'
            WHEN '2' THEN 'request_sent'
            WHEN '3' THEN 'completed'
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
    WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @NumeroIngreso
    
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte que recupera el detalle completo de interconsultas médicas asociadas a un paciente y su número de ingreso hospitalario. Consolida datos demográficos del paciente, cama asignada, especialidad solicitada y especialidad tratante, junto con el médico solicitante, tipo de urgencia (urgente/rutina) y estado del flujo de la interconsulta (solicitada, enviada, completada, cancelada, entre otros).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de interconsultas/órdenes médicas de un paciente durante un ingreso hospitalario, con datos demográficos, ubicación, médico solicitante, especialidad y estado de la solicitud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT y tener un ingreso registrado en ADINGRESO con cama asignada en CHCAMASHO.; Debe existir al menos una orden de interconsulta (HCORDINTE) para el paciente y número de ingreso indicados.; La orden debe tener historia clínica asociada en HCHISPACA por NUMEFOLIO e IPCODPACI.; Profesional, centro de atención, unidad funcional y especialidad solicitada deben existir en sus catálogos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan interconsultas que tienen historia clínica (HCHISPACA) asociada por folio y paciente (INNER JOIN).; Solo se incluyen órdenes con cama actual asignada al paciente en el ingreso (INNER JOIN ADINGRESO/CHCAMASHO).; La especialidad tratante (HCHISPACA.CODESPTRA) es opcional y puede no resolverse (LEFT JOIN a INESPECIA).; Los códigos de sexo, prioridad y estado se traducen a vocabulario clínico estandarizado en inglés (FHIR-like).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Interconsulta; Orden médica; Especialidad médica; Especialidad tratante; Profesional de la salud; Unidad funcional; Cama hospitalaria; Prioridad de atención (urgente/rutina); Estado de solicitud clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDINTE: Cuando A.IPCODPACI=@Paciente y A.NUMINGRES=@NumeroIngreso, devuelve resultset con datos del paciente, ubicación, orden e interconsulta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.IPSEXOPAC = ''1'' → Sexo = ''male'' else Si ''2'' → ''female''; cualquier otro valor → ''other''; si A.PRISERIPS = ''1'' → TipoExamen = ''urgent'' else Si ''2'' → ''routine''; otros valores → NULL; si A.ESTSERIPS in (''1''..''6'') → Mapea Estado a: requested/request_sent/completed/extramural/canceled/pending_specialist_verification else Otros valores → NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDINTE; dbo.INPROFSAL; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INESPECIA; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Interconsultations_EMR';
-- GO
