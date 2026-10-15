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