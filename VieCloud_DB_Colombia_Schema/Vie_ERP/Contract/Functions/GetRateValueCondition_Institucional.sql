CREATE FUNCTION [Contract].[GetRateValueCondition_Institucional]
(
    @CupsEntityId INT,
    @CareGroupId INT,
    @FunctionalUnitId INT,
    @SpecialtyId CHAR(3),
    @ServiceDate DATETIME,
    @InitialService TINYINT,
	@RiasId int = 0,
	@ContractDescriptionId int = 0,
	@IPSServiceId INT
)
RETURNS @DefinitionRateDetailCondition TABLE
(
    StateResult BIT,
    MessageResult VARCHAR(max),
    ConditionType TINYINT,
    LogicalOperator TINYINT,
    ConditionType2 TINYINT,
    DefinitionRateDetailId INT,
    LiquidateionType TINYINT NULL,
    ManualType TINYINT NULL,
	RateManualValidityId INT NULL,
    RateManualId INT NULL,
    AllowValueChange BIT,
    RateVariation DECIMAL(5, 2) NULL,
    SalesValue DECIMAL(18, 2) NULL,
    SalesValueWithSurcharge DECIMAL(18, 2) NULL,
    DefinitionRateDetailConditionId INT NULL,
    LiquidateionTypeAux TINYINT NULL,
    ManualTypeAux TINYINT NULL,
	RateManualValidityIdAux INT NULL,
    RateManualIdAux INT NULL,
    RateVariationAux DECIMAL(5, 2) NULL,
    SalesValueAux DECIMAL(18, 2) NULL,
    SalesValueWithSurchargeAux DECIMAL(18, 2) NULL
)
AS
BEGIN

    DECLARE @CareGroupDefinitionRateId INT,
            @DefinitionRateId INT,
            @UnitType TINYINT;

    DECLARE @Rows INT,
            @RowId INT;

    SELECT @CareGroupDefinitionRateId = ISNULL(Id, 0),
           @DefinitionRateId = DefinitionRateId
    FROM [Contract].CareGroupDefinitionRate WITH (NOLOCK)
    WHERE CareGroupId = @CareGroupId
          AND InitialDate <= CAST(@ServiceDate AS DATE)
          AND EndDate >= CAST(@ServiceDate AS DATE);

	if exists (select 1 from Contract.IPSService where id = @IPSServiceId and ServiceClass <> 1) and @InitialService <> 0 begin
		INSERT INTO @DefinitionRateDetailCondition
        VALUES
        (0, 'No se puede consultar un detalle qx para tipo distinto a IPS ', 0,
         0  , 0, 0, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
		return;
	end

    IF @CareGroupDefinitionRateId = 0
    BEGIN
        INSERT INTO @DefinitionRateDetailCondition
        VALUES
        (0, 'No se encontro una definicion de tarifas en la fecha ' + CAST(CAST(@ServiceDate AS DATE) AS VARCHAR), 0,
         0  , 0, 0, NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
        RETURN;
    END;

    --RateDetails
    DECLARE @DefinitionRateDetails TABLE
    (
        RowId Int primary key identity(1,1),
		Id INT,
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
    );
    DECLARE @DefinitionRateDetailId INT,
            @ConditionType TINYINT,
            @LogicalOperator TINYINT,
            @ConditionType2 TINYINT,
            @LiquidationType TINYINT,
            @ManualType TINYINT,
			@RateManualValidityId INT,
            @RateManualId INT,
            @AllowValueChange BIT,
            @RateVariation DECIMAL(5, 2),
            @SalesValue DECIMAL(18, 2),
            @SalesValueWithSurcharge DECIMAL(18, 2);

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
    FROM [Contract].GetDefinitionRateDetailList(@DefinitionRateId, @CupsEntityId, @InitialService, @IPSServiceId);
    IF
    (
        SELECT COUNT(1) FROM @DefinitionRateDetails
    ) > 0
    BEGIN
		--insert into @DefinitionRateDetailCondition (MessageResult, ConditionType, DefinitionRateDetailId) values (
		--cast((select * from @DefinitionRateDetails for json auto) as varchar(max)), @InitialService, @DefinitionRateId
		--)

		--return

        SET @Rows = 1;
        SET @RowId = 1;

        WHILE @Rows > 0
        BEGIN

            SELECT TOP 1
                   @RowId = RowId,
                   @DefinitionRateDetailId = Id,
                   @ConditionType = ConditionType,
                   @LogicalOperator = LogicalOperator,
                   @ConditionType2 = ConditionType2,
                   @LiquidationType = LiquidationType,
                   @ManualType = ManualType,
				   @RateManualValidityId = RateManualValidityId,
                   @RateManualId = RateManualId,
                   @AllowValueChange = AllowValueChange,
                   @RateVariation = RateVariation,
                   @SalesValue = SalesValue,
                   @SalesValueWithSurcharge = SalesValueWithSurcharge
            FROM @DefinitionRateDetails
            WHERE RowId >= @RowId
            ORDER BY RowId;

            SET @Rows = @@ROWCOUNT;
            IF @Rows = 0
                BREAK;

            IF @LogicalOperator = 1
            BEGIN --Si el operador logico es ninguna, quiere decir que solo maneja una condicion
                IF @ConditionType = 5
                BEGIN --Si es ninguna la liquidacion esta en la misma tabla de detalle
                    INSERT INTO @DefinitionRateDetailCondition
                    VALUES
                    (1, '', @ConditionType, @LogicalOperator, @ConditionType2, @DefinitionRateDetailId,
                     @LiquidationType, @ManualType, @RateManualValidityId, @RateManualId, @AllowValueChange, @RateVariation, @SalesValue,
                     @AllowValueChange, NULL, NULL, NULL, NULL, NULL, @RateVariation, NULL, NULL);
                    RETURN;
                END;
                ELSE
                BEGIN
                    IF @ConditionType = 1 --Hour
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].DefinitionRateDetailCondition WITH (NOLOCK)
                        WHERE DefinitionRateDetailId = @DefinitionRateDetailId
                              AND StartTime <= CAST(@ServiceDate AS TIME)
                              AND EndTime >= CAST(@ServiceDate AS TIME);
                    ELSE IF @ConditionType = 2 --Specialty
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].DefinitionRateDetailCondition WITH (NOLOCK)
                        WHERE DefinitionRateDetailId = @DefinitionRateDetailId
                              AND SpecialtyId = @SpecialtyId;
                    ELSE IF @ConditionType = 3 --FunctionalUnit
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].DefinitionRateDetailCondition WITH (NOLOCK)
                        WHERE DefinitionRateDetailId = @DefinitionRateDetailId
                              AND FunctionalUnitId = @FunctionalUnitId;
                    ELSE IF @ConditionType = 4
                    BEGIN --UnitType						
                        SELECT @UnitType = UnitType
                        FROM Payroll.FunctionalUnit
                        WHERE Id = @FunctionalUnitId;

                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].DefinitionRateDetailCondition WITH (NOLOCK)
                        WHERE DefinitionRateDetailId = @DefinitionRateDetailId
                              AND UnitTypeId = @UnitType;
                    END;
					ELSE IF @ConditionType = 6 --RIAS
                    BEGIN 
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].DefinitionRateDetailCondition WITH (NOLOCK)
                        WHERE DefinitionRateDetailId = @DefinitionRateDetailId
                              AND RIASId = @RiasId;
                    END;
					ELSE IF @ConditionType = 7 --Description
                    BEGIN 
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].DefinitionRateDetailCondition WITH (NOLOCK)
                        WHERE DefinitionRateDetailId = @DefinitionRateDetailId
                              AND ContractDescriptionId = @ContractDescriptionId;
                    END;
                    IF
                    (
                        SELECT COUNT(1) FROM @DefinitionRateDetailCondition
                    ) > 0
                    BEGIN
                        RETURN;
                    END;
                END;
            END;
            ELSE
            BEGIN
                --Entonces maneja dos condiciones
                DECLARE @rateConditionList TABLE
                (
                    Id INT PRIMARY KEY,
                    Operator TINYINT,
                    Operator2 TINYINT,
                    StartTime TIME NULL,
                    EndTime TIME NULL,
                    SpecialtyId2 CHAR(3) NULL,
                    FunctionalUnitId2 INT NULL,
                    UnitTypeId2 TINYINT NULL,
                    StartTime2 TIME NULL,
                    EndTime2 TIME NULL,
                    SpecialtyId CHAR(3) NULL,
					FunctionalUnitId INT NULL,
                    LiquidationType TINYINT NULL,
                    ManualType TINYINT NULL,
					RateManualValidityId INT NULL,
                    RateManualId INT NULL,
                    RateVariation DECIMAL(5, 2) NULL,
                    SalesValue DECIMAL(18, 2) NULL,
                    SalesValueWithSurcharge DECIMAL(18, 2) NULL,
					RIASId2 INT NULL,
					RIASId INT NULL,
					ContractDescriptionId2 INT NULL,
					ContractDescriptionId INT NULL
                );

                INSERT INTO @rateConditionList
                SELECT Id,
                       Operator,
                       Operator2,
                       StartTime,
                       EndTime,
                       SpecialtyId2,
                       FunctionalUnitId2,
                       UnitTypeId2,
                       StartTime2,
                       EndTime2,
                       SpecialtyId,
					   FunctionalUnitId,
                       LiquidationType,
                       ManualType,
					   RateManualValidityId,
                       RateManualId,
                       RateVariation,
                       SalesValue,
                       SalesValueWithSurcharge,
					   RIASId2,
					   RIASId,
					   ContractDescriptionId2,
					   ContractDescriptionId
                FROM [Contract].DefinitionRateDetailCondition WITH (NOLOCK)
                WHERE DefinitionRateDetailId = @DefinitionRateDetailId;

                DECLARE @rateConditionListXml XML =
                        (
                            SELECT *
                            FROM @rateConditionList
                            FOR XML PATH('RateConditionList'), ELEMENTS
                        );

                IF @ConditionType = 1
                BEGIN --Horario
                    IF @ConditionType2 = 2
                    BEGIN --Horario y Especialidad
                        --Analizar bien estos condicionales						
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByTimeSpecialty](@SpecialtyId, @ServiceDate, @rateConditionListXml);
                    END;
                    ELSE IF @ConditionType2 = 3
                    BEGIN --Horario y Unidad Funcional
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByTimeFunctionalUnit](
                                                                              @ServiceDate,
                                                                              @FunctionalUnitId,
                                                                              @rateConditionListXml
                                                                          );
                    END;
                    ELSE IF @ConditionType2 = 4
                    BEGIN --Horario y Tipo de unidad
                        SELECT @UnitType = UnitType
                        FROM Payroll.FunctionalUnit
                        WHERE Id = @FunctionalUnitId;

                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByTimeUnitType](@ServiceDate, @UnitType, @rateConditionListXml);
                    END;
					ELSE IF @ConditionType2 = 6
                    BEGIN --Horario y RIAS
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByTimeRIAS](@ServiceDate, @RiasId, @rateConditionListXml);
                    END;
					ELSE IF @ConditionType2 = 7
                    BEGIN --Horario y Description
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByTimeContractDescription](@ServiceDate, @ContractDescriptionId, @rateConditionListXml);
                    END;
                END;
                ELSE IF @ConditionType = 2
                BEGIN --Especialidad
                    IF @ConditionType2 = 1
                    BEGIN --Especialidad y Horario
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionBySpecialtyTime](@SpecialtyId, @ServiceDate, @rateConditionListXml);
                    END;
                    ELSE IF @ConditionType2 = 3
                    BEGIN --Especialidad y Unidad Funcional
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionBySpecialtyFunctionalUnit](
                                                                                   @SpecialtyId,
                                                                                   @FunctionalUnitId,
                                                                                   @rateConditionListXml
                                                                               );
                    END;
                    ELSE IF @ConditionType2 = 4
                    BEGIN --Especialidad y Tipo de unidad
                        SELECT @UnitType = UnitType
                        FROM Payroll.FunctionalUnit
                        WHERE Id = @FunctionalUnitId;

                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionBySpecialtyUnitType](
                                                                             @SpecialtyId,
                                                                             @UnitType,
                                                                             @rateConditionListXml
                                                                         );
                    END;
					ELSE IF @ConditionType2 = 6
                    BEGIN --Especialidad y RIAS
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionBySpecialtyRIAS](
                                                                             @SpecialtyId,
                                                                             @RiasId,
                                                                             @rateConditionListXml
                                                                         );
                    END;
					ELSE IF @ConditionType2 = 7
                    BEGIN --Especialidad y Description
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionBySpecialtyDescription](
                                                                             @SpecialtyId,
                                                                             @ContractDescriptionId,
                                                                             @rateConditionListXml
                                                                         );
                    END;
                END;
                ELSE IF @ConditionType = 3
                BEGIN --Unidad Funcional
                    IF @ConditionType2 = 1
                    BEGIN --Unidad Funcional y Horario
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByFunctionalUnitTime](
                                                                              @FunctionalUnitId,
                                                                              @ServiceDate,
                                                                              @rateConditionListXml
                                                                          );
                    END;
                    ELSE IF @ConditionType2 = 2
                    BEGIN --Unidad Funcional y Especialidad
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByFunctionalUnitSpecialty](
                                                                                   @FunctionalUnitId,
                                                                                   @SpecialtyId,
                                                                                   @rateConditionListXml
                                                                               );
                    END;
                    ELSE IF @ConditionType2 = 4
                    BEGIN --Unidad Funcional y TIpo de unidad
                        SELECT @UnitType = UnitType
                        FROM Payroll.FunctionalUnit
                        WHERE Id = @FunctionalUnitId;

                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByFunctionalUnitUnitType](
                                                                                  @FunctionalUnitId,
                                                                                  @UnitType,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
					ELSE IF @ConditionType2 = 6
                    BEGIN --Unidad Funcional y RIAS
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByFunctionalUnitRIAS](
                                                                                  @FunctionalUnitId,
                                                                                  @RiasId,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
					ELSE IF @ConditionType2 = 7
                    BEGIN --Unidad Funcional y Description
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByFunctionalUnitDescription](
                                                                                  @FunctionalUnitId,
                                                                                  @ContractDescriptionId,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
                END;
                ELSE IF @ConditionType = 4
                BEGIN --Tipo de Unidad
                    SELECT @UnitType = UnitType
                    FROM Payroll.FunctionalUnit
                    WHERE Id = @FunctionalUnitId;

                    IF @ConditionType2 = 1
                    BEGIN --Tipo de Unidad y Horario
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByUnitTypeTime](@UnitType, @ServiceDate, @rateConditionListXml);
                    END;
                    ELSE IF @ConditionType2 = 2
                    BEGIN --Tipo de Unidad y Especialidad
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByUnitTypeSpecialty](
                                                                             @UnitType,
                                                                             @SpecialtyId,
                                                                             @rateConditionListXml
                                                                         );
                    END;
                    ELSE IF @ConditionType2 = 3
                    BEGIN --Tipo de Unidad y Unidad Funcional
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByUnitTypeFunctionalUnit](
                                                                                  @UnitType,
                                                                                  @FunctionalUnitId,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
					ELSE IF @ConditionType2 = 6
                    BEGIN --Tipo de Unidad y RIAS
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByUnitTypeRIAS](
                                                                                  @UnitType,
                                                                                  @RiasId,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
					ELSE IF @ConditionType2 = 7
                    BEGIN --Tipo de Unidad y Description
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByUnitTypeDescription](
                                                                                  @UnitType,
                                                                                  @ContractDescriptionId,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
                END;
				ELSE IF @ConditionType = 6
                BEGIN --RIAS
					IF @ConditionType2 = 1
                    BEGIN --RIAS y Horario
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByRIASTime](@RiasId, @ServiceDate, @rateConditionListXml);
                    END;
					ELSE IF @ConditionType2 = 2
                    BEGIN --RIAS y Especialidad
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByRIASSpecialty](
                                                                             @RiasId,
                                                                             @SpecialtyId,
                                                                             @rateConditionListXml
                                                                         );
                    END;
					ELSE IF @ConditionType2 = 3
                    BEGIN --RIAS y Unidad Funcional
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByRIASFunctionalUnit](
                                                                                   @RiasId,
                                                                                   @FunctionalUnitId,
                                                                                   @rateConditionListXml
                                                                               );
                    END;
					ELSE IF @ConditionType2 = 4
                    BEGIN --RIAS y Tipo de unidad
                        SELECT @UnitType = UnitType
                        FROM Payroll.FunctionalUnit
                        WHERE Id = @FunctionalUnitId;

                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByRIASUnitType](
                                                                                  @RiasId,
                                                                                  @UnitType,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
					ELSE IF @ConditionType2 = 7
                    BEGIN --RIAS y Description
                        SELECT @UnitType = UnitType
                        FROM Payroll.FunctionalUnit
                        WHERE Id = @FunctionalUnitId;

                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByRIASDescription](
                                                                                  @RiasId,
                                                                                  @ContractDescriptionId,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
				END;
				ELSE IF @ConditionType = 7
                BEGIN --Description
					IF @ConditionType2 = 1
                    BEGIN --Description y Horario
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByDescriptionTime](@ContractDescriptionId, @ServiceDate, @rateConditionListXml);
                    END;
					ELSE IF @ConditionType2 = 2
                    BEGIN --Description y Especialidad
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByDescriptionSpecialty](
                                                                             @ContractDescriptionId,
                                                                             @SpecialtyId,
                                                                             @rateConditionListXml
                                                                         );
                    END;
					ELSE IF @ConditionType2 = 3
                    BEGIN --Description y Unidad Funcional
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByDescriptionFunctionalUnit](
                                                                                   @ContractDescriptionId,
                                                                                   @FunctionalUnitId,
                                                                                   @rateConditionListXml
                                                                               );
                    END;
					ELSE IF @ConditionType2 = 4
                    BEGIN --Description y Tipo de unidad
                        SELECT @UnitType = UnitType
                        FROM Payroll.FunctionalUnit
                        WHERE Id = @FunctionalUnitId;

                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByDescriptionUnitType](
                                                                                  @ContractDescriptionId,
                                                                                  @UnitType,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
					ELSE IF @ConditionType2 = 6
                    BEGIN --Description y RIAS
                        INSERT INTO @DefinitionRateDetailCondition
                        SELECT TOP 1
                               1,
                               '',
                               @ConditionType,
                               @LogicalOperator,
                               @ConditionType2,
                               @DefinitionRateDetailId,
                               @LiquidationType,
                               @ManualType,
							   @RateManualValidityId,
                               @RateManualId,
                               @AllowValueChange,
                               @RateVariation,
                               @SalesValue,
                               @SalesValueWithSurcharge,
                               Id,
                               LiquidationType,
                               ManualType,
							   RateManualValidityId,
                               RateManualId,
                               RateVariation,
                               SalesValue,
                               SalesValueWithSurcharge
                        FROM [Contract].[GetConditionByDescriptionRIAS](
                                                                                  @ContractDescriptionId,
                                                                                  @RiasId,
                                                                                  @rateConditionListXml
                                                                              );
                    END;
				END;

                IF
                (
                    SELECT COUNT(1) FROM @DefinitionRateDetailCondition
                ) > 0
                BEGIN
                    RETURN;
                END;

            END;

            SET @RowId += 1;
        END;

    END;

    IF @InitialService = 4
    BEGIN
        DECLARE @CupsCodeName VARCHAR(200);
        SELECT @CupsCodeName = CONCAT(Code, ' - ', [Description])
        FROM [Contract].CUPSEntity WITH (NOLOCK)
        WHERE Id = @CupsEntityId;

        INSERT INTO @DefinitionRateDetailCondition
        VALUES
        (0, 'No se encontro una tarifa para el CUPS ' + @CupsCodeName, 0, 0, 0, 0, NULL, NULL, NULL, 0, NULL, NULL,
         NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,NULL, NULL);
    END;
    ELSE
    BEGIN
		SET @InitialService = @InitialService + 1 --Incremento las reglas
        INSERT INTO @DefinitionRateDetailCondition
        SELECT *
        FROM [Contract].[GetRateValueCondition_Institucional](
                                                   @CupsEntityId,
                                                   @CareGroupId,
                                                   @FunctionalUnitId,
                                                   @SpecialtyId,
                                                   @ServiceDate,
                                                   @InitialService,
												   @RiasId,
												   @ContractDescriptionId,
												   @IPSServiceId
                                               );

    END;

    RETURN;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que determina el valor tarifario aplicable a un servicio de salud institucional (IPS) dentro de un contrato, evaluando las condiciones de liquidación configuradas para un grupo de atención en una fecha de prestación específica. Consulta la vigencia tarifaria en CareGroupDefinitionRate para ubicar la definición de tarifas activa en la fecha del servicio, luego obtiene el detalle de reglas y condiciones de esa tarifa a través de GetDefinitionRateDetailList (filtrando por entidad CUPS, servicio IPS y si es servicio inicial o quirúrgico). Evalúa cada condición de tarifa —por hora, unidad funcional, especialidad, RIAS, grupo de atención, entre otras— aplicando operadores lógicos (AND/OR/ninguno) para devolver el tipo de liquidación, manual tarifario, variación de precio, valor de venta y recargo que corresponde cobrar. Es el motor de tarificación contractual institucional: se usa en la liquidación de cuentas, facturación y validación de valores pactados con aseguradoras para procedimientos, exámenes y servicios prestados por la IPS.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetRateValueCondition_Institucional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetRateValueCondition_Institucional';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve la tarifa institucional aplicable a un servicio evaluando las condiciones (horario, especialidad, unidad funcional, tipo de unidad, RIAS, descripción de contrato) del detalle de tarifa vigente para el grupo de atención y la fecha del servicio.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueCondition_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Contract.CareGroupDefinitionRate cuyo CareGroup coincida y cuya vigencia (InitialDate/EndDate) cubra la fecha del servicio.; Si el IPSService no es de clase 1 (ServiceClass<>1), el flag de servicio inicial debe ser 0; de lo contrario se rechaza por ser detalle quirúrgico no IPS.; La unidad funcional referida debe existir en Payroll.FunctionalUnit cuando se evalúan condiciones de tipo de unidad.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueCondition_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando existe IPSService con ServiceClass<>1 y @InitialService<>0, inserta fila de error con StateResult=0 y mensaje ''No se puede consultar un detalle qx para tipo distinto a IPS'' y termina.; [INSERT] @DefinitionRateDetailCondition: Cuando no existe CareGroupDefinitionRate vigente para la fecha (@CareGroupDefinitionRateId=0), inserta fila con StateResult=0 y mensaje ''No se encontro una definicion de tarifas en la fecha <ServiceDate>'' y termina.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=5 (ninguna condición), inserta directamente la liquidación del detalle con StateResult=1 y termina sin evaluar DefinitionRateDetailCondition.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=1 (Hora), inserta el TOP 1 de DefinitionRateDetailCondition cuyo rango StartTime/EndTime contenga la hora de @ServiceDate.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=2 (Especialidad), inserta el TOP 1 de DefinitionRateDetailCondition cuyo SpecialtyId=@SpecialtyId.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=3 (Unidad Funcional), inserta el TOP 1 de DefinitionRateDetailCondition cuyo FunctionalUnitId=@FunctionalUnitId.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=4 (Tipo de Unidad), obtiene el UnitType de la FunctionalUnit y filtra DefinitionRateDetailCondition por UnitTypeId.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=6 (RIAS), filtra DefinitionRateDetailCondition por RIASId=@RiasId.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=7 (Descripción de contrato), filtra DefinitionRateDetailCondition por ContractDescriptionId=@ContractDescriptionId.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator<>1 (dos condiciones), invoca la función GetConditionBy<X><Y> correspondiente a la combinación (ConditionType, ConditionType2) entre {Horario, Especialidad, Unidad Funcional, Tipo de Unidad, RIAS, Descripción} e inserta su primer resultado.; [INSERT] @DefinitionRateDetailCondition: Cuando @InitialService=4 y no se encontró tarifa, inserta fila con StateResult=0 y mensaje ''No se encontro una tarifa para el CUPS <Code - Description>''.; [INSERT] @DefinitionRateDetailCondition: Cuando @InitialService<>4 y no se halló coincidencia, incrementa @InitialService en 1 e invoca recursivamente la misma función para reintentar con la siguiente regla.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueCondition_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSService.ServiceClass<>1 y @InitialService<>0 → Aborta retornando error de detalle quirúrgico no permitido para tipo distinto a IPS; si No existe CareGroupDefinitionRate vigente en la fecha → Aborta retornando error ''No se encontro una definicion de tarifas...''; si LogicalOperator=1 (una sola condición) → Evalúa según ConditionType (1 Hora, 2 Especialidad, 3 UF, 4 Tipo Unidad, 5 Ninguna, 6 RIAS, 7 Descripción) sobre DefinitionRateDetailCondition else LogicalOperator distinto: evalúa pares de condiciones invocando funciones especializadas GetConditionBy<X><Y>; si ConditionType=5 con LogicalOperator=1 → Toma la liquidación directamente del detalle sin buscar condición adicional; si Tras procesar todas las filas no se obtuvo resultado y @InitialService=4 → Inserta error ''No se encontro una tarifa para el CUPS ...'' else Incrementa @InitialService y vuelve a llamarse recursivamente', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueCondition_Institucional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueCondition_Institucional';
GO
