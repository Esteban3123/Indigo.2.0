
CREATE PROCEDURE [dbo].[SPHC_Laboratories_Results_EMR]
(
    @Paciente VARCHAR(20),
    @NumeroIngreso CHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        RTRIM(F.IPNOMCOMP) AS Nombres, 
        F.IPPRINOMB AS PrimerNombre,
        F.IPSEGNOMB AS SegundoNombre,
        F.IPPRIAPEL AS PrimerApellido,
        F.IPSEGAPEL AS SegundoApellido,
        RTRIM(F.CODIGONIT) AS NumeroDocumento,    
        RTRIM(E.DESSERIPS) AS Servicio,
        CASE A.PRISERIPS
            WHEN '1' THEN 'urgent'
            WHEN '2' THEN 'routine'
        END AS TipoExamen,
        IIF(ISNULL(x.CriticalResult, 0) = 1, 1, 0) AS Resultadocritico,
        '' AS RangoReferencia,
        A.FECORDMED AS FechaSolicitud,
        CD.Name AS DescripcionRelacionada,
        A.OBSSERIPS AS Observacion,
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
        ELSE 'unknown'
    END AS Estado
    FROM dbo.HCORDLABO A
    INNER JOIN dbo.INPACIENT F 
        ON A.IPCODPACI = F.IPCODPACI
    INNER JOIN dbo.INCUPSIPS E 
        ON A.CODSERIPS = E.CODSERIPS
    LEFT JOIN contract.CUPSEntityContractDescriptions CDD 
        ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
    LEFT JOIN contract.ContractDescriptions CD 
        ON CD.Id = CDD.ContractDescriptionId
    LEFT JOIN (
        SELECT DISTINCT
            ID.AUTOLABOR,
            ID.CriticalResult,
            ID.ValuesOutLimits
        FROM dbo.INTERCTRL AS IT
        LEFT JOIN dbo.INTERLABC IC 
            ON IT.ORDEN_INDIGO = IC.ORDEN_INDIGO
        INNER JOIN dbo.INTERLABD ID 
            ON IC.AUTO = ID.CODCONCEC 
           AND ID.AUTOLABOR = IT.AUTOLABOR
        WHERE ID.CriticalResult = 1
    ) x ON x.AUTOLABOR = A.AUTO
    WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @NumeroIngreso;

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que recupera los resultados de órdenes de laboratorio clínico para un paciente y número de ingreso específicos. Combina datos demográficos del paciente, el servicio CUPS solicitado, el estado de la orden (mapeado a valores estandarizados como `requested`, `result_delivered`, `canceled`, entre otros), y detecta si algún resultado es crítico consultando el detalle de analitos procesados en el sistema Indigo. También incorpora la descripción contractual asociada a la orden cuando está disponible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta consolidada de órdenes y resultados de laboratorio de un paciente en un ingreso específico, devolviendo datos demográficos, servicio solicitado, prioridad, estado normalizado y marca de resultado crítico desde la integración con el laboratorio externo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT y estar referenciado en HCORDLABO.; El servicio/CUPS debe existir en INCUPSIPS para poder describir el examen.; El paciente e ingreso enviados deben coincidir con registros en HCORDLABO; de lo contrario el resultado es vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven órdenes de laboratorio del paciente e ingreso indicados.; El indicador de resultado crítico únicamente se activa cuando existe al menos un analito con CriticalResult = 1 asociado a la orden vía la integración INTERCTRL/INTERLABC/INTERLABD.; El tipo de examen se normaliza a ''urgent''/''routine'' y queda NULL para otros códigos de prioridad.; Los estados de la orden se traducen a un vocabulario controlado en inglés; cualquier código desconocido se reporta como ''unknown''.; El rango de referencia siempre se devuelve vacío (no se calcula en este flujo).; Resultados se devuelven sin duplicados (DISTINCT).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Orden de laboratorio; Examen clínico (CUPS/IPS); Prioridad de examen (urgente/rutinario); Estado de orden de laboratorio; Resultado crítico; Contrato/descripción contractual asociada al CUPS; Integración con laboratorio externo (Indigo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando A.IPCODPACI = @Paciente y A.NUMINGRES = @NumeroIngreso, retorna las órdenes de laboratorio con datos del paciente, servicio, tipo de examen, estado y marca de resultado crítico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PRISERIPS = ''1'' → Tipo de examen ''urgent'' else Si ''2'' → ''routine''; si ESTSERIPS según valor 1..9 → Mapea a estados: requested, sample_collected, result_delivered, test_interpreted, forwarded, canceled, extramural, partially_collected_sample, nonconforming_sample else Cualquier otro valor → ''unknown''; si Existe registro en INTERLABD con CriticalResult = 1 vinculado a la orden → Resultadocritico = 1 else Resultadocritico = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INPACIENT; dbo.INCUPSIPS; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.INTERCTRL; dbo.INTERLABC; dbo.INTERLABD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Laboratories_Results_EMR';
-- GO
