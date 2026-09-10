
CREATE FUNCTION [Contract].[GetDefinitionRateDetailList]
(
    @DefinitionRateId INT,
    @CupsEntityId INT,
    @RateOptions TINYINT,
	@IPSServiceId INT
)
RETURNS @DefinitionRateDetails TABLE
(
	RowId Int primary key identity(1,1),
    Id INT,
    --Id INT PRIMARY KEY,
    ConditionType TINYINT,
    LogicalOperator TINYINT,
    ConditionType2 TINYINT,
    LiquidationType TINYINT NULL,
    ManualType TINYINT NULL,
	RateManualValidityId INT NULL,
    RateManualId INT NULL,
    AllowValueChange BIT,
    RateVariation DECIMAL(5, 2) NULL,
    SalesValue DECIMAL(18, 2) NULL,
    SalesValueWithSurcharge DECIMAL(18, 2) NULL
)
AS
BEGIN
    DECLARE @CupsSubGroupId INT,
            @CupsGroupId INT;

    IF @RateOptions = 0
    BEGIN --IPSService
        INSERT INTO @DefinitionRateDetails
        SELECT Id,
               ConditionType,
               LogicalOperator,
               ConditionType2,
               LiquidationType,
               ManualType,
			   RateManualValidityId,
               RateManualId,
               AllowValueChange,
               RateVariation,
               SalesValue,
               SalesValueWithSurcharge
        FROM [Contract].DefinitionRateDetail WITH (NOLOCK)
        WHERE DefinitionRateId = @DefinitionRateId
              AND IPSServiceId = @IPSServiceId
			  AND (CUPSEntityId is NULL OR CUPSEntityId = @CupsEntityId)
        ORDER BY [Weight] DESC;
    END;
	else IF @RateOptions = 1
    BEGIN --CUPS
        INSERT INTO @DefinitionRateDetails
        SELECT Id,
               ConditionType,
               LogicalOperator,
               ConditionType2,
               LiquidationType,
               ManualType,
			   RateManualValidityId,
               RateManualId,
               AllowValueChange,
               RateVariation,
               SalesValue,
               SalesValueWithSurcharge
        FROM [Contract].DefinitionRateDetail WITH (NOLOCK)
        WHERE DefinitionRateId = @DefinitionRateId
              AND CUPSEntityId = @CupsEntityId AND IPSServiceId is null
        ORDER BY [Weight] DESC;
    END;
    ELSE IF @RateOptions = 2
    BEGIN --SUBGROUP
        SELECT @CupsSubGroupId = CUPSSubGroupId
        FROM [Contract].CUPSEntity WITH (NOLOCK)
        WHERE Id = @CupsEntityId;
        INSERT INTO @DefinitionRateDetails
        SELECT Id,
               ConditionType,
               LogicalOperator,
               ConditionType2,
               LiquidationType,
               ManualType,
			   RateManualValidityId,
               RateManualId,
               AllowValueChange,
               RateVariation,
               SalesValue,
               SalesValueWithSurcharge
        FROM [Contract].DefinitionRateDetail WITH (NOLOCK)
        WHERE DefinitionRateId = @DefinitionRateId
              AND CUPSSubgroupId = @CupsSubGroupId
        ORDER BY [Weight] DESC;
    END;
    ELSE IF @RateOptions = 3
    BEGIN --GROUP
        SELECT @CupsGroupId = csg.CupsGroupId
        FROM [Contract].CUPSEntity ce WITH (NOLOCK)
            INNER JOIN [Contract].CupsSubgroup csg WITH (NOLOCK)
                ON ce.CUPSSubGroupId = csg.Id
        WHERE ce.Id = @CupsEntityId;
        INSERT INTO @DefinitionRateDetails
        SELECT Id,
               ConditionType,
               LogicalOperator,
               ConditionType2,
               LiquidationType,
               ManualType,
			   RateManualValidityId,
               RateManualId,
               AllowValueChange,
               RateVariation,
               SalesValue,
               SalesValueWithSurcharge
        FROM [Contract].DefinitionRateDetail WITH (NOLOCK)
        WHERE DefinitionRateId = @DefinitionRateId
              AND CUPSGroupId = @CupsGroupId
        ORDER BY [Weight] DESC;
    END;
    ELSE IF @RateOptions = 4
    BEGIN --GENERAL
        INSERT INTO @DefinitionRateDetails
        SELECT Id,
               ConditionType,
               LogicalOperator,
               ConditionType2,
               LiquidationType,
               ManualType,
			   RateManualValidityId,
               RateManualId,
               AllowValueChange,
               RateVariation,
               SalesValue,
               SalesValueWithSurcharge
        FROM [Contract].DefinitionRateDetail WITH (NOLOCK)
        WHERE DefinitionRateId = @DefinitionRateId
              AND RuleType = 5
        ORDER BY [Weight] DESC;
    END;

    RETURN;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que recupera la lista de reglas de detalle de una tarifa de contrato (DefinitionRateDetail) según el nivel de aplicación solicitado: servicio IPS específico, código CUPS, subgrupo CUPS, grupo CUPS o regla general. Recibe como parámetros el identificador de la tarifa, el servicio o CUPS de referencia y una opción de nivel (0=servicio IPS, 1=CUPS, 2=subgrupo, 3=grupo, 4=general), resolviendo subgrupo y grupo mediante consultas al catálogo CUPSEntity y CupsSubgroup cuando corresponde. Retorna las condiciones de liquidación, tipo de manual tarifario, variación de tarifa, valor de venta con y sin recargo, y si se permite cambio de valor, ordenadas por peso o prioridad descendente. Se utiliza en el motor de tarifación de contratos para determinar qué regla aplicar al liquidar un procedimiento o servicio de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetDefinitionRateDetailList';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetDefinitionRateDetailList';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los detalles de tarifa aplicables a un servicio según el nivel de granularidad (servicio IPS, CUPS específico, subgrupo, grupo o regla general), ordenados por peso descendente.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetDefinitionRateDetailList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una definición de tarifa identificada por el parámetro de entrada; Para opciones de subgrupo/grupo, el CUPSEntity debe existir y estar asociado a un subgrupo (y este a un grupo)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetDefinitionRateDetailList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las consultas usan NOLOCK (lecturas sucias permitidas); El orden de prioridad se preserva por Weight descendente; RuleType=5 representa la regla ''General''; La granularidad de aplicación es excluyente: IPSService, CUPS, Subgrupo, Grupo o General según el modo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetDefinitionRateDetailList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tarifa de contrato; Servicio IPS; CUPS (procedimiento en salud); Subgrupo CUPS; Grupo CUPS; Tarifario manual; Liquidación; Variación tarifaria; Valor de venta con recargo', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetDefinitionRateDetailList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetails: Si RateOptions=0 (IPSService): inserta detalles donde DefinitionRateId coincide, IPSServiceId coincide y CUPSEntityId es NULL o coincide con el parámetro; [INSERT] @DefinitionRateDetails: Si RateOptions=1 (CUPS): inserta detalles donde CUPSEntityId coincide y IPSServiceId es NULL; [INSERT] @DefinitionRateDetails: Si RateOptions=2 (SUBGROUP): resuelve el subgrupo del CUPSEntity y filtra por CUPSSubgroupId; [INSERT] @DefinitionRateDetails: Si RateOptions=3 (GROUP): resuelve el grupo vía CUPSEntity→CupsSubgroup y filtra por CUPSGroupId; [INSERT] @DefinitionRateDetails: Si RateOptions=4 (GENERAL): inserta detalles donde RuleType = 5; [RETURN_RESULT] @DefinitionRateDetails: Resultados siempre ordenados por Weight DESC al insertar', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetDefinitionRateDetailList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @RateOptions = 0 → Filtra por servicio IPS permitiendo CUPSEntityId nulo o coincidente; si @RateOptions = 1 → Filtra por CUPSEntityId exigiendo IPSServiceId nulo; si @RateOptions = 2 → Resuelve el subgrupo del CUPSEntity y filtra por CUPSSubgroupId; si @RateOptions = 3 → Resuelve el grupo (vía subgrupo) del CUPSEntity y filtra por CUPSGroupId; si @RateOptions = 4 → Filtra detalles cuya RuleType = 5 (regla general); si @RateOptions no coincide con 0..4 → ? else No se inserta nada y se retorna tabla vacía', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetDefinitionRateDetailList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.DefinitionRateDetail; Contract.CUPSEntity; Contract.CupsSubgroup', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetDefinitionRateDetailList';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetDefinitionRateDetailList';
GO
