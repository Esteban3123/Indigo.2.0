
CREATE PROCEDURE [dbo].[SPHC_Dyagnostics_Images_EMR]
(
    @Paciente varchar(20),
    @NumeroIngreso CHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        F.IPNOMCOMP AS Nombres,
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
        RTRIM(D.UFUDESCRI) AS Unidad,
        RTRIM(H.DESCCAMAS) AS Cama,
        RTRIM(E.DESSERIPS) AS Servicio,
        CASE A.PRISERIPS
            WHEN '1' THEN 'urgent'
            WHEN '2' THEN 'routine'
        END AS TipoExamen,
        CAST(0 AS BIT) AS Resultado,
        CASE A.ESTSERIPS
        WHEN '1' THEN 'requested'
        WHEN '2' THEN 'study_performed'
        WHEN '3' THEN 'image_processed'
        WHEN '4' THEN 'study_interpreted'
        WHEN '5' THEN 'Referred'
        WHEN '6' THEN 'canceled'
        WHEN '7' THEN 'extramural'
        WHEN '8' THEN 'Pre_exam_(Indira)'
        WHEN '9' THEN 'In Progress'
        ELSE 'unknown'
        END AS Estado,
        A.OBSSERIPS AS Observacion,
        RTRIM(N.DESESPECI) AS Especialidad,
        CD.Name AS DescripcionRelacionada,
        IIF(
            DM.CODSERIPS IS NOT NULL,
            CAST(3 AS INT),
            IIF(
                (SELECT COUNT(IDHCPLANDOC)
                 FROM HCPLANDOCCUPS PDC
                 INNER JOIN HCPLANDOC PD 
                     ON PDC.IDHCPLANDOC = PD.CODCONSEC
                 WHERE PDC.CODSERIPS = A.CODSERIPS) > 0,
                CAST(2 AS INT),
                CAST(1 AS INT)
            )
        ) AS ConsentimientoInformado,
        RTRIM(I.NOMDIAGNO) AS Diagnostico
    FROM dbo.HCORDIMAG A
    INNER JOIN dbo.INPACIENT F 
        ON A.IPCODPACI = F.IPCODPACI
    INNER JOIN dbo.ADINGRESO G 
        ON F.IPCODPACI = G.IPCODPACI
    INNER JOIN dbo.CHCAMASHO H 
        ON G.CODCAMACT = H.CODICAMAS
    INNER JOIN dbo.INDIAGNOS I 
        ON A.CODDIAGNO = I.CODDIAGNO
    INNER JOIN dbo.ADcenaten C 
        ON A.CODCENATE = C.codcenate
    INNER JOIN dbo.INUNIFUNC D 
        ON A.UFUCODIGO = D.UFUCODIGO
    INNER JOIN dbo.INCUPSIPS E 
        ON A.CODSERIPS = E.CODSERIPS
    INNER JOIN dbo.HCHISPACA J 
        ON A.NUMEFOLIO = J.NUMEFOLIO 
       AND A.IPCODPACI = J.IPCODPACI
    LEFT OUTER JOIN dbo.INESPECIA N 
        ON J.CODESPTRA = N.CODESPECI
    LEFT JOIN contract.CUPSEntityContractDescriptions CDD 
        ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
    LEFT JOIN contract.ContractDescriptions CD 
        ON CD.Id = CDD.ContractDescriptionId
    LEFT JOIN dbo.HCDOCUMAD DM 
        ON A.IPCODPACI = DM.IPCODPACI 
       AND A.NUMINGRES = DM.NUMINGRES 
       AND DM.CODSERIPS = A.CODSERIPS 
       AND DM.NUMEFOLIO = A.NUMEFOLIO 
       AND DM.TIPODOCUM = 7
    WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @NumeroIngreso;

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que recupera el detalle completo de las órdenes de imágenes diagnósticas (radiología, ecografías, tomografías, etc.) asociadas a un paciente y un ingreso hospitalario específico. Consolida datos demográficos del paciente, ubicación en cama y servicio, estado del examen (solicitado, realizado, interpretado, cancelado, entre otros), prioridad (urgente/rutina), diagnóstico CIE-10, especialidad tratante y descripción contractual CUPS. Además, determina el nivel de consentimiento informado verificando documentos adjuntos de tipo 7 o planes documentales vinculados al servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y devuelve la información consolidada de las órdenes de imágenes diagnósticas de un paciente en un ingreso específico, incluyendo datos demográficos, ubicación, estado del estudio, diagnóstico y nivel de consentimiento informado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en el maestro de pacientes y tener un ingreso registrado con cama asignada.; Debe existir al menos una orden de imagen diagnóstica asociada al paciente y número de ingreso.; La orden debe tener folio de historia clínica, unidad funcional, centro de atención, código de servicio (CUPS) y diagnóstico válidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran documentos de consentimiento con TIPODOCUM = 7.; El sexo siempre se normaliza a valores ''male'', ''female'' u ''other''.; El estado del estudio siempre se traduce a una etiqueta textual estándar; valores no contemplados se reportan como ''unknown''.; Resultado siempre se retorna como bit 0 (constante, no calculado).; Solo se retornan órdenes que tengan paciente, ingreso, cama, unidad funcional, centro de atención, servicio CUPS, folio de historia clínica y diagnóstico relacionados (INNER JOIN obligatorios).; La especialidad, descripción contractual y documento de consentimiento son opcionales (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión hospitalaria; Cama hospitalaria; Unidad funcional; Servicio CUPS; Orden de imagen diagnóstica; Estado del estudio de imagen; Especialidad médica; Diagnóstico (CIE); Historia clínica / folio; Consentimiento informado; Descripción contractual del CUPS; Tipo de examen (urgente/rutinario)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna un conjunto de filas con datos del paciente, orden de imagen, estado clínico, diagnóstico y nivel de consentimiento informado para el paciente e ingreso filtrados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = ''1'' → Sexo = ''male'' else Si ''2'' → ''female''; cualquier otro valor → ''other''; si PRISERIPS = ''1'' → TipoExamen = ''urgent'' else Si ''2'' → ''routine''; otros valores → NULL; si ESTSERIPS entre ''1'' y ''9'' → Mapea a estados clínicos: requested, study_performed, image_processed, study_interpreted, Referred, canceled, extramural, Pre_exam_(Indira), In Progress else ''unknown''; si Existe documento en HCDOCUMAD con TIPODOCUM=7 para el paciente/ingreso/servicio/folio → ConsentimientoInformado = 3 (firmado/registrado) else Si existe plan documental CUPS asociado al servicio en HCPLANDOCCUPS → 2 (requerido); en caso contrario → 1 (no aplica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.INDIAGNOS; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCDOCUMAD; dbo.HCPLANDOCCUPS; dbo.HCPLANDOC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Dyagnostics_Images_EMR';
-- GO
