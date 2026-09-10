

CREATE VIEW [dbo].[ViewDiagnosticImagesEMR]
AS

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
        A.OBSSERIPS AS Observacion,
        CASE A.ESTSERIPS
            WHEN '1' THEN 'requested'
            WHEN '2' THEN 'study_completed'
            WHEN '3' THEN 'image_processed'
            WHEN '4' THEN 'study_interpreted'
            WHEN '5' THEN 'forwarded'
            WHEN '6' THEN 'canceled'
            WHEN '7' THEN 'extramural'
            WHEN '8' THEN 'before_exam_completed'
            WHEN '9' THEN 'in_progress'
        END AS Estado,
        RTRIM(N.DESESPECI) AS Especialidad,
        RTRIM(A.CODCENATE) AS CodigoCentroAtencion,
        RTRIM(K.CODPROSAL) AS CodigoProfesional,
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
    INNER JOIN dbo.INPROFSAL K 
        ON A.CODPROSAL = K.CODPROSAL   
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
    WHERE A.ESTSERIPS = 2
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a consumo por sistemas de historia clínica electrónica (EMR/HIS) que consolida las órdenes de imágenes diagnósticas con estado "estudio completado" (ESTSERIPS = 2). Aplana datos demográficos del paciente, cama y unidad funcional asignada, servicio CUPS solicitado, profesional ordenante, diagnóstico CIE-10 y especialidad tratante. Incluye clasificación de urgencia/rutina, ciclo de estados del estudio y un indicador de consentimiento informado derivado de documentos adjuntos o planes documentales asociados al servicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para integración/EMR, las órdenes de imágenes diagnósticas con estudio completado, enriquecidas con datos del paciente, ingreso, ubicación, servicio, diagnóstico, profesional y estado del consentimiento informado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben tener un paciente válido en INPACIENT y un ingreso vigente en ADINGRESO con cama asignada en CHCAMASHO; El servicio (CODSERIPS) debe existir en INCUPSIPS y el diagnóstico en INDIAGNOS; La orden debe tener folio de historia clínica asociado en HCHISPACA y un profesional en INPROFSAL; El centro de atención y la unidad funcional deben estar parametrizados (ADcenaten, INUNIFUNC); Solo se consideran órdenes con estado del servicio igual a 2 (estudio completado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes de imágenes diagnósticas cuyo estado de servicio es 2 (study_completed); Cada orden expuesta tiene paciente, ingreso, cama, diagnóstico, centro de atención, unidad funcional, servicio CUPS, folio de historia clínica y profesional asociados (INNER JOIN obligatorio); El campo Resultado siempre se devuelve como 0 (BIT), no se calcula desde datos; El indicador de Consentimiento Informado siempre toma uno de tres valores discretos: 1, 2 o 3; El sexo siempre se normaliza a uno de tres valores: male, female u other; La especialidad, descripción contractual relacionada y sus joins son opcionales (LEFT JOIN), pueden venir nulos; El documento de consentimiento se identifica exclusivamente por TIPODOCUM = 7', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de imágenes diagnósticas; Paciente; Ingreso/Admisión hospitalaria; Cama hospitalaria; Unidad funcional; Servicio CUPS; Diagnóstico (CIE-10); Profesional de la salud; Especialidad médica; Centro de atención; Historia clínica / folio; Consentimiento informado; Descripción contractual del CUPS; Estado del estudio de imagen; Prioridad del examen (urgente/rutina)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDIMAG: Filtra ESTSERIPS = 2 → solo retorna órdenes de imagen cuyo estudio fue completado; [RETURN_RESULT] dbo.HCDOCUMAD: Cuando existe documento con TIPODOCUM=7 vinculado al mismo paciente/ingreso/folio/servicio → ConsentimientoInformado=3; [RETURN_RESULT] dbo.HCPLANDOCCUPS: Cuando no hay documento de consentimiento pero existen filas en HCPLANDOCCUPS/HCPLANDOC para el servicio → ConsentimientoInformado=2; si no existen → 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Sexo del paciente = ''1'' → se expone como ''male'' else si ''2'' → ''female''; cualquier otro valor → ''other''; si Prioridad del servicio = ''1'' → tipo de examen ''urgent'' else si ''2'' → ''routine''; otro valor → NULL; si Estado del servicio (1..9) → se mapea a etiquetas de negocio: requested, study_completed, image_processed, study_interpreted, forwarded, canceled, extramural, before_exam_completed, in_progress; si Existe documento asociado en HCDOCUMAD con TIPODOCUM=7 para el mismo paciente, ingreso, folio y servicio → ConsentimientoInformado = 3 (consentimiento firmado/registrado) else Si existe al menos un registro en HCPLANDOCCUPS/HCPLANDOC asociado al servicio → 2 (requerido pendiente); en caso contrario → 1 (no aplica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.INPACIENT; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.INDIAGNOS; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INPROFSAL; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCDOCUMAD; dbo.HCPLANDOCCUPS; dbo.HCPLANDOC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDiagnosticImagesEMR';
GO
