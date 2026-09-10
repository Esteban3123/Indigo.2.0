CREATE FUNCTION [Contract].[GetRateValueConditionSurgicalProcedures]
(
	@DefinitionRateDetailId INT,
    @FunctionalUnitId INT,
    @SpecialtyId CHAR(3),
    @ServiceDate DATETIME,
	@RiasId int = 0,
	@ContractDescriptionId int = 0
)
RETURNS @DefinitionRateDetailCondition TABLE
(
    StateResult BIT,
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

	--Variables que se obtiene de la tabla definitionRateDetail
	declare @ConditionType tinyint, @LogicalOperator tinyint, @ConditionType2 tinyint, @LiquidationType tinyint, @ManualType tinyint, @RateManualValidityId INT, @RateManualId int,
    @AllowValueChange bit, @RateVariation decimal(5, 2), @SalesValue decimal(18, 2), @SalesValueWithSurcharge decimal(18, 2);
	   
	--Se obtiene el detalle de la tarifa
	 select @ConditionType = ConditionType, @LogicalOperator = LogicalOperator, @ConditionType2 = ConditionType2, @LiquidationType = LiquidationType,
			@ManualType = ManualType, @RateManualValidityId = RateManualValidityId, @RateManualId = RateManualId, @AllowValueChange = AllowValueChange, 
			@RateVariation = RateVariation, @SalesValue = SalesValue, @SalesValueWithSurcharge = SalesValueWithSurcharge
     from Contract.DefinitionRateDetail with(nolock)
	 where Id = @DefinitionRateDetailId

    declare @UnitType tinyint;
	   
    IF @LogicalOperator = 1
    BEGIN --Si el operador logico es diferente a ninguna
        IF @ConditionType <> 5
        BEGIN
            IF @ConditionType = 1 --Hour
                INSERT INTO @DefinitionRateDetailCondition
                SELECT TOP 1
                        1,
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
            LiquidateionType TINYINT NULL,
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
    END;

    RETURN;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que determina el valor de tarifa aplicable a un procedimiento quirúrgico según las condiciones específicas definidas en un contrato. Recibe como parámetros el identificador del detalle de tarifa, la unidad funcional, la especialidad médica y la fecha/hora del servicio, y evalúa las condiciones configuradas (franja horaria, especialidad, unidad funcional, tipo de unidad, RIAS o descripción de contrato) para retornar el tipo de liquidación, el valor de venta con o sin recargo y la variación de tarifa que corresponde aplicar. Consulta primero el detalle de la tarifa en DefinitionRateDetail para conocer el tipo de condición y el operador lógico, y luego filtra las condiciones específicas en DefinitionRateDetailCondition para resolver si aplica una sola condición o la combinación de dos condiciones mediante operadores lógicos (AND/OR). Se utiliza en el proceso de liquidación y facturación de procedimientos quirúrgicos para determinar automáticamente el precio correcto según las reglas pactadas en el contrato con el pagador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetRateValueConditionSurgicalProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetRateValueConditionSurgicalProcedures';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resuelve la tarifa aplicable a un procedimiento quirúrgico evaluando una o dos condiciones (horario, especialidad, unidad funcional, tipo de unidad, RIAS o descripción de contrato) sobre un detalle de tarifa de contrato.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueConditionSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Contract.DefinitionRateDetail con el identificador recibido para obtener tipo de condición, operador lógico, tipo de liquidación y valores de tarifa.; Para condiciones que evalúan tipo de unidad, la unidad funcional recibida debe existir en Payroll.FunctionalUnit para resolver su UnitType.; Para evaluar por RIAS o por descripción de contrato deben proveerse los identificadores correspondientes (de lo contrario se usan 0 por defecto).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueConditionSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=1 (Hora), inserta la primera condición cuyo rango StartTime/EndTime contiene la hora de @ServiceDate.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=2 (Especialidad), inserta la primera condición cuya SpecialtyId coincide con la especialidad recibida.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=3 (Unidad Funcional), inserta la primera condición cuya FunctionalUnitId coincide con la unidad recibida.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=4 (Tipo de Unidad), resuelve el UnitType desde Payroll.FunctionalUnit e inserta la primera condición que coincida con UnitTypeId.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=6 (RIAS), inserta la primera condición cuya RIASId coincide con la recibida.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator=1 y ConditionType=7 (Descripción), inserta la primera condición cuya ContractDescriptionId coincide con la recibida.; [INSERT] @DefinitionRateDetailCondition: Cuando LogicalOperator<>1, se construye un XML con todas las condiciones del detalle y se delega la evaluación combinada a la función especializada según el par (ConditionType, ConditionType2), insertando solo la primera fila resultante.; [INSERT] @DefinitionRateDetailCondition: Si ConditionType=5 y LogicalOperator=1, no se inserta ninguna fila (no se evalúa ninguna condición).; [RETURN_RESULT] @DefinitionRateDetailCondition: Devuelve a lo sumo una fila con StateResult=1 y los valores de liquidación, manual, vigencia, variación y valor de venta de la condición coincidente; si no hay coincidencia retorna tabla vacía.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueConditionSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LogicalOperator = 1 (condición simple) → Evalúa una sola condición según ConditionType (1 Hora, 2 Especialidad, 3 Unidad Funcional, 4 Tipo de Unidad, 6 RIAS, 7 Descripción). else Evalúa dos condiciones combinadas mediante funciones GetConditionBy<X><Y> usando un XML con todas las condiciones del detalle.; si LogicalOperator=1 y ConditionType = 5 → No se ejecuta ninguna inserción (rama vacía).; si ConditionType = 1 (Horario) con LogicalOperator<>1 → Según ConditionType2 invoca GetConditionByTimeSpecialty (2), GetConditionByTimeFunctionalUnit (3), GetConditionByTimeUnitType (4), GetConditionByTimeRIAS (6) o GetConditionByTimeContractDescription (7).; si ConditionType = 2 (Especialidad) con LogicalOperator<>1 → Según ConditionType2 invoca GetConditionBySpecialtyTime (1), GetConditionBySpecialtyFunctionalUnit (3), GetConditionBySpecialtyUnitType (4), GetConditionBySpecialtyRIAS (6) o GetConditionBySpecialtyDescription (7).; si ConditionType = 3 (Unidad Funcional) con LogicalOperator<>1 → Según ConditionType2 invoca GetConditionByFunctionalUnitTime (1), GetConditionByFunctionalUnitSpecialty (2), GetConditionByFunctionalUnitUnitType (4), GetConditionByFunctionalUnitRIAS (6) o GetConditionByFunctionalUnitDescription (7).; si ConditionType = 4 (Tipo de Unidad) con LogicalOperator<>1 → Resuelve el UnitType de la Unidad Funcional y según ConditionType2 invoca GetConditionByUnitTypeTime (1), GetConditionByUnitTypeSpecialty (2), GetConditionByUnitTypeFunctionalUnit (3), GetConditionByUnitTypeRIAS (6) o GetConditionByUnitTypeDescription (7).; si ConditionType = 6 (RIAS) con LogicalOperator<>1 → Según ConditionType2 invoca GetConditionByRIASTime (1), GetConditionByRIASSpecialty (2), GetConditionByRIASFunctionalUnit (3), GetConditionByRIASUnitType (4) o GetConditionByRIASDescription (7).; si ConditionType = 7 (Descripción) con LogicalOperator<>1 → Según ConditionType2 invoca GetConditionByDescriptionTime (1), GetConditionByDescriptionSpecialty (2), GetConditionByDescriptionFunctionalUnit (3), GetConditionByDescriptionUnitType (4) o GetConditionByDescriptionRIAS (6).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueConditionSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetRateValueConditionSurgicalProcedures';
GO
