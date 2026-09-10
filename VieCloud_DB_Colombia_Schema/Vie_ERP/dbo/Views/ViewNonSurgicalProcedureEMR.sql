

CREATE VIEW [dbo].[ViewNonSurgicalProcedureEMR]
AS

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
        A.CODCENATE AS CodigoCentroAtencion,
        RTRIM(B.CODPROSAL) AS CodigoProfesional,
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
    WHERE A.ESTSERIPS = 2
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta orientada a reporting clínico que expone las órdenes médicas de procedimientos no quirúrgicos completados (estado = 2 / "completed") registradas en la historia clínica electrónica. Consolida datos demográficos del paciente, identificación del médico solicitante, servicio CUPS, prioridad (urgente/rutina), diagnóstico CIE-10, especialidad, cama y unidad funcional asignada, y descripción contractual relacionada. Está diseñada para consumo externo o integración con sistemas de historia clínica electrónica (EMR), filtrando exclusivamente procedimientos en estado completado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la información consolidada de procedimientos no quirúrgicos completados en la historia clínica del paciente, incluyendo datos demográficos, profesional tratante, servicio, ubicación, diagnóstico y descripción contractual asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de procedimiento debe estar en estado ''completado'' (ESTSERIPS=2); El paciente debe tener un ingreso registrado con cama asignada (INNER JOIN con ADINGRESO y CHCAMASHO); La orden debe tener diagnóstico, profesional, centro de atención, unidad funcional y servicio CUPS válidos en sus respectivos catálogos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda fila retornada corresponde a un procedimiento en estado ''completed''; Toda fila tiene un paciente con ingreso y cama asignada (no incluye pacientes ambulatorios sin cama); El sexo siempre se normaliza a uno de los valores: male, female, other; La especialidad y la descripción contractual son opcionales (LEFT JOIN), pueden ser NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden médica; Procedimiento no quirúrgico; Profesional de salud; Diagnóstico; Ingreso hospitalario; Cama hospitalaria; Centro de atención; Unidad funcional; Servicio CUPS; Especialidad; Historia clínica; Prioridad de examen (urgente/rutina); Estado del servicio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ViewNonSurgicalProcedureEMR: Sólo retorna órdenes con ESTSERIPS=2 (servicios completados); las órdenes en otros estados (ordered, interpreted, no_interface, voided) son excluidas pese a estar mapeadas en el CASE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.IPSEXOPAC = ''1'' → Sexo = ''male'' else Si ''2'' → ''female''; cualquier otro valor → ''other''; si A.PRISERIPS = ''1'' → TipoExamen = ''urgent'' else Si ''2'' → ''routine''; otro valor → NULL; si A.ESTSERIPS in (1..5) → Estado se mapea a ordered/completed/interpreted/no_interface/voided else Aunque el CASE contempla 5 estados, el WHERE filtra sólo ESTSERIPS=2 → siempre ''completed''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.INPROFSAL; dbo.INPACIENT; dbo.INDIAGNOS; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNonSurgicalProcedureEMR';
GO
