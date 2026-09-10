
CREATE PROCEDURE [dbo].[SPREP_HC_Surgical_Procedures_EMR]
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
            WHEN '1' THEN 'emergency'
            WHEN '2' THEN 'urgent'
            WHEN '3' THEN 'normal'
            WHEN '4' THEN 'define_course_of_action'
        END AS TipoExamen,
        CASE A.ESTSERIPS
            WHEN '1' THEN 'requested'
            WHEN '2' THEN 'scheduled_room'  
            WHEN '3' THEN 'canceled' 
            WHEN '4' THEN 'result_reviewed' 
            WHEN '5' THEN 'voided' 
            WHEN '6' THEN 'scheduled_not_performed'
        END AS Estado,
        RTRIM(D.UFUDESCRI) AS Unidad,
        RTRIM(H.NUMCAMHOS) AS Cama,
        A.OBSSERIPS AS Observacion,
        RTRIM(N.DESESPECI) AS Especialidad,
        CD.Name AS DescripcionRelacionada,
        I.NOMDIAGNO AS Diagnostico
    FROM dbo.HCORDPROQ A
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
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento de reporte que recupera las órdenes de procedimientos quirúrgicos registradas en la historia clínica para un paciente y número de ingreso específicos. Consolida datos demográficos del paciente, información del médico solicitante, cama y unidad funcional asignada, el servicio CUPS ordenado con su prioridad y estado (solicitado, programado, cancelado, etc.), el diagnóstico CIE-10 asociado y la especialidad tratante. Está diseñado para alimentar vistas clínicas o reportes del EMR/HIS relacionados con cirugías durante una hospitalización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y devuelve los procedimientos quirúrgicos y servicios ordenados a un paciente durante un ingreso específico, junto con datos demográficos, médico tratante, diagnóstico, ubicación hospitalaria y descripción contractual asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT y tener al menos una orden en HCORDPROQ para el ingreso indicado.; El paciente debe tener un ingreso registrado en ADINGRESO con cama asignada en CHCAMASHO.; Los códigos de profesional, diagnóstico, centro de atención, unidad funcional y servicio CUPS referenciados en la orden deben existir en sus catálogos maestros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes que tengan profesional, paciente, diagnóstico, ingreso, cama, centro de atención, unidad funcional y servicio CUPS válidos (INNER JOIN obligatorio).; La historia clínica (HCHISPACA), la especialidad tratante y la descripción contractual asociada son opcionales (LEFT JOIN); su ausencia no excluye la orden.; Los códigos internos de sexo, prioridad y estado se exponen siempre como etiquetas estandarizadas en inglés hacia el consumidor (EMR).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión hospitalaria; Orden de procedimiento quirúrgico; Servicio CUPS; Profesional de la salud / Médico tratante; Diagnóstico (CIE); Especialidad médica; Unidad funcional; Cama hospitalaria; Centro de atención; Historia clínica / folio; Prioridad de atención (emergencia, urgente, normal); Estado de la orden (solicitada, programada, cancelada, anulada, revisada); Descripción contractual de CUPS; Sexo del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result_set: Devuelve una fila por cada orden de procedimiento del paciente e ingreso filtrados (WHERE A.IPCODPACI=@Paciente AND A.NUMINGRES=@NumeroIngreso), traduciendo códigos de sexo, prioridad y estado a etiquetas de negocio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = ''1'' → Sexo se reporta como ''male'' else ''2'' → ''female''; cualquier otro valor → ''other''; si PRISERIPS in (''1'',''2'',''3'',''4'') → Mapea prioridad de la orden a ''emergency'',''urgent'',''normal'' o ''define_course_of_action'' respectivamente else NULL si valor no contemplado; si ESTSERIPS in (''1''..''6'') → Mapea estado de la orden a ''requested'',''scheduled_room'',''canceled'',''result_reviewed'',''voided'',''scheduled_not_performed'' else NULL si valor no contemplado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPROQ; dbo.INPROFSAL; dbo.INPACIENT; dbo.INDIAGNOS; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Surgical_Procedures_EMR';
-- GO
