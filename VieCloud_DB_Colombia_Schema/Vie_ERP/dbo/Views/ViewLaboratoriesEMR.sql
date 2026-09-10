

CREATE VIEW [dbo].[ViewLaboratoriesEMR]
AS

SELECT DISTINCT
        RTRIM(F.IPNOMCOMP)                           AS Nombres,
        F.IPPRINOMB                                  AS PrimerNombre,
        F.IPSEGNOMB                                  AS SegundoNombre,
        F.IPPRIAPEL                                  AS PrimerApellido,
        F.IPSEGAPEL                                  AS SegundoApellido,
        RTRIM(F.CODIGONIT)                           AS NumeroDocumento,
        RTRIM(E.DESSERIPS)                           AS Servicio,
        CASE A.PRISERIPS
            WHEN '1' THEN 'urgent'
            WHEN '2' THEN 'routine'
        END                                          AS TipoExamen,
        IIF(ISNULL(x.CriticalResult, 0) = 1, 1, 0)  AS Resultadocritico,
        ''                                           AS RangoReferencia,
        A.FECORDMED                                  AS FechaSolicitud,
        CD.Name                                      AS DescripcionRelacionada,
        A.OBSSERIPS                                  AS Observacion,
        CASE A.ESTSERIPS
            WHEN '1' THEN 'requested'
            WHEN '2' THEN 'sample_collected'
            WHEN '3' THEN 'result_delivered'
            WHEN '4' THEN 'test_interpreted'
            WHEN '5' THEN 'forwarded'
            WHEN '6' THEN 'canceled'
            WHEN '7' THEN 'extramural'
            WHEN '8' THEN 'partially_collected_sample'
            WHEN '9' THEN 'nonconforming_sample'
            ELSE            'unknown'
        END                                          AS Estado,
        x.ANALITO                                    AS Analito,
        x.VALOR                                      AS Valor,
        x.VALORMINIMO                                AS ValorMinimo,
        x.VALORMAXIMO                                AS ValorMaximo,
        x.OBSERVACION                                AS ObservacionResultado,
        x.CLASIFICACION                              AS Clasificacion,
        A.CODPROSAL AS CodigoProfesional,
        A.CODCENATE AS CodigoCentroAtencion
    FROM dbo.HCORDLABO A
    INNER JOIN dbo.INPACIENT F
        ON  A.IPCODPACI = F.IPCODPACI
    INNER JOIN dbo.INCUPSIPS E
        ON  A.CODSERIPS = E.CODSERIPS
    LEFT JOIN contract.CUPSEntityContractDescriptions CDD
        ON  CDD.Id = A.IDDESCRIPCIONRELACIONADA
    LEFT JOIN contract.ContractDescriptions CD
        ON  CD.Id = CDD.ContractDescriptionId
    LEFT JOIN (
        SELECT DISTINCT
            IT.AUTOLABOR,          
            ID.CriticalResult,
            ID.ANALITO,
            ID.VALOR,
            ID.VALORMINIMO,
            ID.VALORMAXIMO,
            ID.OBSERVACION,
            ID.CLASIFICACION         
        FROM dbo.INTERCTRL AS IT
        INNER JOIN dbo.INTERLABC IC     
            ON  IC.ORDEN_INDIGO = IT.ORDEN_INDIGO
        INNER JOIN dbo.INTERLABD ID
            ON  ID.CODCONCEC = IC.AUTO
            AND ID.AUTOLABOR = IT.AUTOLABOR
        -- WHERE ID.CriticalResult = 1
    ) x ON x.AUTOLABOR = A.AUTO
    WHERE A.ESTSERIPS IN (3)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a consulta y reporting del EMR que expone órdenes de laboratorio con estado "resultado entregado" (ESTSERIPS = 3). Consolida datos demográficos del paciente, el profesional solicitante, el servicio CUPS asociado y la descripción de contrato vinculada. Incluye indicador de resultado crítico proveniente del detalle de analitos (INTERLABD), y traduce códigos de sexo, tipo de examen y estado de la orden a etiquetas estandarizadas en inglés para consumo por sistemas externos o integraciones clínicas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone para EMR los exámenes de laboratorio con resultado entregado, junto con datos del paciente, profesional, servicio, contrato y marca de resultado crítico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de laboratorio debe estar en estado ''resultado entregado'' (ESTSERIPS = 3).; El paciente, el servicio CUPS y el profesional referenciados por la orden deben existir en sus maestros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes con ESTSERIPS = 3, por lo que el campo Estado siempre será ''result_delivered''.; Resultadocritico es siempre 0 o 1, nunca NULL (se fuerza con IIF/ISNULL).; RangoReferencia siempre se devuelve como cadena vacía.; El cruce con resultados críticos sólo considera filas de INTERLABD con CriticalResult = 1.; Se eliminan duplicados mediante SELECT DISTINCT.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Orden de laboratorio; Profesional de la salud; Servicio CUPS; Contrato / descripción contractual; Resultado crítico de laboratorio; Tipo de examen (urgente/rutinario); Estado de la orden de laboratorio; Sexo del paciente; Integración con laboratorio externo (Indigo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ViewLaboratoriesEMR: Retorna únicamente órdenes de laboratorio cuyo estado es 3 (resultado entregado), enriquecidas con datos demográficos del paciente, descripción del servicio, profesional, descripción contractual y bandera de resultado crítico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = ''1'' → Sexo = ''male'' else Si ''2'' → ''female''; cualquier otro valor → ''other''; si PRISERIPS = ''1'' → TipoExamen = ''urgent'' else Si ''2'' → ''routine''; otro → NULL; si Existe en INTERLABD un registro con CriticalResult = 1 ligado a la orden vía INTERCTRL/INTERLABC → Resultadocritico = 1 else Resultadocritico = 0; si ESTSERIPS de la orden → Se mapea a estados textuales: 1=requested, 2=sample_collected, 3=result_delivered, 4=test_interpreted, 5=forwarded, 6=canceled, 7=extramural, 8=partially_collected_sample, 9=nonconforming_sample else Cualquier otro valor → ''unknown''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INPROFSAL; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.INTERCTRL; dbo.INTERLABC; dbo.INTERLABD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewLaboratoriesEMR';
GO
