

CREATE VIEW [dbo].[ViewSurgicalProcedureEMR]
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
        RTRIM(I.NOMDIAGNO) AS Diagnostico
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
    WHERE A.ESTSERIPS = 2
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta orientada a reporting y consumo por el módulo de Historia Clínica Electrónica (EMR). Expone las órdenes de procedimientos quirúrgicos/servicios CUPS que se encuentran en estado "scheduled_room" (ESTSERIPS = 2), combinando datos demográficos del paciente, identidad del médico solicitante, prioridad y estado de la orden, cama y unidad funcional asignadas, diagnóstico CIE-10 y especialidad. Incluye opcionalmente una descripción contractual asociada al servicio ordenado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las órdenes de procedimientos quirúrgicos programados en sala, consolidando datos del paciente, profesional, servicio, unidad, cama, especialidad, diagnóstico y descripción contractual asociada para uso en la historia clínica electrónica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes de procedimiento deben tener estado igual a 2 (programado en sala) para ser visibles.; El paciente debe tener un ingreso registrado y una cama asignada, ya que las uniones a ADINGRESO y CHCAMASHO son INNER JOIN.; La orden debe tener diagnóstico, profesional, centro de atención, unidad funcional y servicio CUPS válidos (INNER JOIN obligatorios).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes en estado ''scheduled_room'' (ESTSERIPS=2).; El sexo siempre se normaliza a uno de los valores HL7/FHIR: ''male'', ''female'' u ''other''.; Las prioridades y estados clínicos se traducen a códigos en inglés estandarizados.; La especialidad y la descripción contractual son opcionales (pueden ser NULL por LEFT JOIN).; Se eliminan espacios en blanco a la derecha (RTRIM) en nombres y descripciones presentadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Procedimiento quirúrgico; Orden médica; Profesional de la salud; Diagnóstico (CIE); Ingreso/Admisión; Cama hospitalaria; Unidad funcional; Centro de atención; Servicio CUPS; Especialidad médica; Historia clínica; Prioridad clínica (emergencia/urgente/normal); Estado de la orden (programado en sala, cancelado, anulado); Descripción contractual del CUPS; Sexo del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDPROQ: Filtra y devuelve únicamente las órdenes con ESTSERIPS = 2 (estado ''scheduled_room'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.IPSEXOPAC = ''1'' → Sexo se reporta como ''male'' else Si ''2'' → ''female''; cualquier otro valor → ''other''; si A.PRISERIPS in (''1'',''2'',''3'',''4'') → Mapea prioridad a ''emergency'',''urgent'',''normal'' o ''define_course_of_action'' respectivamente else NULL (no mapeado); si A.ESTSERIPS in (''1''..''6'') → Mapea estado a ''requested'',''scheduled_room'',''canceled'',''result_reviewed'',''voided'',''scheduled_not_performed'' else Por filtro WHERE solo aplica el valor 2 → ''scheduled_room''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPROQ; dbo.INPROFSAL; dbo.INPACIENT; dbo.INDIAGNOS; dbo.ADINGRESO; dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewSurgicalProcedureEMR';
GO
