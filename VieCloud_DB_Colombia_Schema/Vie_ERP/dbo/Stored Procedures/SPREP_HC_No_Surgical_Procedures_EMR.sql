
CREATE PROCEDURE [dbo].[SPREP_HC_No_Surgical_Procedures_EMR]
(
    @Paciente varchar(20),
    @NumeroIngreso VARCHAR(20)
)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RTRIM(F.IPNOMCOMP) AS Nombre,
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
        A.FECORDMED AS FechaSolicitud,
        RTRIM(E.DESSERIPS) AS Servicio,
        A.CODSERIPS AS Codigo,
        RTRIM(B.NOMMEDICO) AS Medico,
        CASE A.PRISERIPS
            WHEN '1' THEN 'urgent'
            WHEN '2' THEN 'routine'
        END AS TipoExamen,
        CASE A.ESTSERIPS 
            WHEN '1' THEN 'ordered'
            WHEN '2' THEN 'completed'  
            WHEN '3' THEN 'interpreted' 
            WHEN '4' THEN 'no_interface' 
            WHEN '5' THEN 'voided'
        END AS Estado,
        RTRIM(D.UFUDESCRI) AS Unidad,
        RTRIM(H.DESCCAMAS) AS Cama,
        A.OBSSERIPS AS Observacion,
        A.EXREASITI AS ExamenSitio,
        RTRIM(N.DESESPECI) AS Especialidad,
        CD.Name AS DescripcionRelacionada,
        RTRIM(NOMDIAGNO) AS Diagnostico
    FROM dbo.HCORDPRON A
    INNER JOIN dbo.INPROFSAL B 
        ON A.CODPROSAL = B.CODPROSAL
    INNER JOIN dbo.INPACIENT F 
        ON A.IPCODPACI = F.IPCODPACI
    INNER JOIN dbo.INDIAGNOS I 
        ON A.CODDIAGNO = I.CODDIAGNO
    INNER JOIN dbo.ADINGRESO G 
        ON F.IPCODPACI = G.IPCODPACI
    INNER JOIN dbo.CHCAMASHO H 
        ON G.CODCAMACT = H.CODICAMAS 
    INNER JOIN dbo.ADcenaten C 
        ON A.CODCENATE = C.codcenate
    INNER JOIN dbo.INUNIFUNC D 
        ON A.UFUCODIGO = D.UFUCODIGO
    INNER JOIN dbo.INCUPSIPS E 
        ON A.CODSERIPS = E.CODSERIPS
    LEFT JOIN dbo.HCHISPACA J 
        ON A.NUMEFOLIO = J.NUMEFOLIO 
       AND A.IPCODPACI = J.IPCODPACI
    LEFT JOIN dbo.INESPECIA N 
        ON J.CODESPTRA = N.CODESPECI
    LEFT JOIN contract.CUPSEntityContractDescriptions CDD 
        ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
    LEFT JOIN contract.ContractDescriptions CD 
        ON CD.Id = CDD.ContractDescriptionId
    WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @NumeroIngreso;
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que recupera las órdenes médicas de procedimientos **no quirúrgicos** registradas en la historia clínica electrónica (EMR) para un paciente y número de ingreso específicos. Combina datos demográficos del paciente, el médico ordenador, el servicio CUPS solicitado, su estado (ordenado, completado, anulado, etc.), prioridad (urgente/rutina), cama, unidad funcional, diagnóstico CIE-10 y especialidad tratante, incluyendo opcionalmente descripciones contractuales asociadas al servicio ordenado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los procedimientos no quirúrgicos ordenados a un paciente en un ingreso específico, con datos demográficos, clínicos, profesional tratante, servicio, estado y diagnóstico asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT y tener órdenes en HCORDPRON con el número de ingreso indicado; El paciente debe tener al menos un registro en ADINGRESO con cama asignada en CHCAMASHO (INNER JOIN obliga ocupación de cama); Los códigos de profesional, diagnóstico, centro de atención, unidad funcional y servicio CUPS referidos en la orden deben existir en sus catálogos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes asociadas simultáneamente al paciente y número de ingreso recibidos; Los códigos internos de sexo, prioridad y estado se traducen siempre a una nomenclatura estándar en inglés (FHIR-like); La especialidad y la descripción de contrato son opcionales (LEFT JOIN); el resto de relaciones son obligatorias; El resultado siempre incluye cama actual del paciente al exigir join con CHCAMASHO vía ADINGRESO.CODCAMACT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión hospitalaria; Orden médica de procedimiento (no quirúrgico); Servicio CUPS; Profesional de la salud / Médico tratante; Diagnóstico CIE; Unidad funcional; Cama hospitalaria; Especialidad médica; Estado de la orden (ordered/completed/interpreted/no_interface/voided); Prioridad del examen (urgente/rutinario); Centro de atención; Descripción de contrato CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando A.IPCODPACI = @Paciente y A.NUMINGRES = @NumeroIngreso, devuelve filas con datos del paciente, orden, servicio, médico, unidad, cama, especialidad y diagnóstico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = ''1'' → Sexo = ''male'' else Si ''2'' → ''female''; en otro caso → ''other''; si PRISERIPS = ''1'' → TipoExamen = ''urgent'' else Si ''2'' → ''routine''; otros valores → NULL; si ESTSERIPS in (1..5) → Estado mapeado a ''ordered''/''completed''/''interpreted''/''no_interface''/''voided'' según valor else Otros valores → NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.INPROFSAL; dbo.INPACIENT; dbo.INDIAGNOS; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_No_Surgical_Procedures_EMR';
-- GO
