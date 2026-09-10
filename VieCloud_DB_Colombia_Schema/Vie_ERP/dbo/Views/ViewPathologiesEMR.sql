

CREATE VIEW [dbo].[ViewPathologiesEMR]
AS

SELECT
        RTRIM(F.IPNOMCOMP) AS Nombre,
        F.IPPRINOMB AS PrimerNombre,
        F.IPSEGNOMB AS SegundoNombre,
        F.IPPRIAPEL AS PrimerApellido,
        F.IPSEGAPEL AS SegundoApellido,
        F.CODIGONIT AS NumeroDocumento,
        F.IPFECNACI AS FechaNacimiento,
        RTRIM(A.CODCENATE) AS CodigoCentroAtencion,
        RTRIM(B.CODPROSAL) AS CodigoProfesional,
        CASE F.IPSEXOPAC 
            WHEN '1' THEN 'male' 
            WHEN '2' THEN 'female' 
            ELSE 'other' 
        END AS Sexo,
        RTRIM(D.UFUDESCRI) AS Unidad,
        RTRIM(H.DESCCAMAS) AS Cama,
        RTRIM(E.DESSERIPS) AS Servicio,
        A.FECORDMED AS FechaSolicitud,   
        CASE A.ESTSERIPS
            WHEN '1' THEN 'requested'
            WHEN '2' THEN 'request_sent'
            WHEN '3' THEN 'interconsultation_completed'
            WHEN '4' THEN 'extramural'
            WHEN '5' THEN 'canceled'
            WHEN '6' THEN 'pending_specialist_verification'
        END AS Estado,
        CD.Name AS DescripcionRelacionada,
        A.OBSSERIPS AS Observacion,
        CASE A.PRISERIPS
            WHEN '1' THEN 'urgent'
            WHEN '2' THEN 'routine'
        END AS TipoExamen,
        RTRIM(I.NOMDIAGNO) AS Diagnostico
    FROM dbo.HCORDPATO A
    INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
    INNER JOIN dbo.INPACIENT F ON A.IPCODPACI = F.IPCODPACI
    INNER JOIN dbo.ADINGRESO G ON F.IPCODPACI = G.IPCODPACI
    INNER JOIN dbo.INDIAGNOS I ON A.CODDIAGNO = I.CODDIAGNO
    INNER JOIN dbo.CHCAMASHO H ON G.CODCAMACT = H.CODICAMAS
    INNER JOIN dbo.ADcenaten C ON A.CODCENATE = C.codcenate
    INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
    INNER JOIN dbo.INCUPSIPS E ON A.CODSERIPS = E.CODSERIPS
    INNER JOIN dbo.HCHISPACA J ON A.NUMEFOLIO = J.NUMEFOLIO AND A.IPCODPACI = J.IPCODPACI   
    LEFT JOIN contract.CUPSEntityContractDescriptions CDD 
        ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
    LEFT JOIN contract.ContractDescriptions CD 
        ON CD.Id = CDD.ContractDescriptionId
    WHERE A.ESTSERIPS = 3
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consulta orientada a reporting y consumo externo (EMR) que expone las órdenes de patología e imágenes diagnósticas cuyo estado corresponde exclusivamente a "interconsultación completada" (ESTSERIPS = 3). Consolida datos demográficos del paciente, ubicación hospitalaria (cama y unidad funcional), profesional solicitante, servicio CUPS, diagnóstico CIE-10, prioridad del examen y descripción contractual asociada, permitiendo visualizar el resultado final de interconsultas diagnósticas dentro del historial clínico electrónico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone las órdenes de patología con interconsulta completada, enriquecidas con datos del paciente, profesional, ubicación hospitalaria, diagnóstico y descripción contractual asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de patología debe estar vinculada a un ingreso vigente del paciente con cama asignada (ADINGRESO.CODCAMACT); La orden debe tener folio de historia clínica existente en HCHISPACA para el mismo paciente; Deben existir registros maestros de profesional, paciente, diagnóstico, centro de atención, unidad funcional y servicio CUPS referenciados por la orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Únicamente se exponen órdenes con estado interconsultation_completed (ESTSERIPS = 3); El sexo se normaliza a vocabulario FHIR-like (male/female/other); La prioridad del examen se normaliza a urgent/routine; La descripción contractual relacionada es opcional (LEFT JOIN); puede ser nula sin excluir la orden; Toda orden listada tiene ingreso, cama, unidad funcional, servicio CUPS, diagnóstico y profesional asociados (INNER JOINs obligatorios)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Profesional de la salud; Orden de patología; Diagnóstico (CIE); Centro de atención; Unidad funcional; Cama hospitalaria; Servicio CUPS; Historia clínica (folio); Interconsulta; Prioridad de examen (urgente/rutina); Descripción contractual', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Solo retorna órdenes de patología cuando ESTSERIPS = 3 (interconsultation_completed)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = ''1'' → Sexo = ''male'' else Si ''2'' → ''female''; cualquier otro valor → ''other''; si ESTSERIPS = ''1''..''6'' → Mapea a estados: requested, request_sent, interconsultation_completed, extramural, canceled, pending_specialist_verification; si PRISERIPS = ''1'' → TipoExamen = ''urgent'' else Si ''2'' → ''routine''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPATO; dbo.INPROFSAL; dbo.INPACIENT; dbo.ADINGRESO; dbo.INDIAGNOS; dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPathologiesEMR';
GO
