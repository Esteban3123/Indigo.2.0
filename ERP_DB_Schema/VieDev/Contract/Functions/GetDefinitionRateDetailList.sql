
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