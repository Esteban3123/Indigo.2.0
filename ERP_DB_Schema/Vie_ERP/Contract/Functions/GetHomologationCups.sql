CREATE FUNCTION [Contract].[GetHomologationCups]
(
    @CareGroupId INT,
    @CupsEntityId INT,
    @PerformsFunctionalUnitId INT,
    @PerformsProfessionalSpecialty CHAR(3),
    @ServiceDate DATETIME,
    @IPSServiceId INT = NULL,
    @ManualType INT = 0,
	@RIASId int = 0,
	@ContractDescriptionId int = 0
)
RETURNS @ListHomologation TABLE
(
    StatusResult BIT,
    MessageResult VARCHAR(255),
    CupsHomologationId INT NULL,
    IPSServiceId INT NULL,
    CupsEntityId INT NULL,
    CodeNameCupsEntity VARCHAR(320) NULL,
    CodeNameIpsService VARCHAR(320) NULL,
    Activated BIT
)
AS
BEGIN
    DECLARE @ServiceType INT;
	DECLARE @DefinitionRateId INT

	SELECT TOP 1 @DefinitionRateId = cdr.DefinitionRateId
	FROM Contract.CareGroupDefinitionRate cdr
	WHERE cdr.CareGroupId = @CareGroupId AND CAST(cdr.InitialDate AS DATE) <= CAST(@ServiceDate AS DATE) AND CAST(cdr.EndDate AS DATE) >= CAST(@ServiceDate AS DATE)
	
	declare @tmpListHomologation TABLE
	(
		StatusResult BIT,
		MessageResult VARCHAR(255),
		CupsHomologationId INT NULL,
		IPSServiceId INT NULL,
		CupsEntityId INT NULL,
		ServiceManual TINYINT,
		IPSPresentation TINYINT,
		CodeNameCupsEntity VARCHAR(320) NULL,
		CodeNameIpsService VARCHAR(320) NULL,
		Activated BIT
	)

	DELETE FROM @tmpListHomologation
	INSERT INTO @tmpListHomologation
    SELECT 1,
           '',
           ch.Id,
           ch.IPSServiceId,
           ch.CupsEntityId,
		   ips.ServiceManual,
		   ips.Presentation,
           CONCAT(ce.Code, ' - ', ce.[Description]),
           CONCAT(ips.Code, ' - ', ips.[Name]),
           0
    FROM [Contract].CupsHomologation ch WITH (NOLOCK)
    INNER JOIN [Contract].CUPSEntity ce WITH (NOLOCK) ON ch.CupsEntityId = ce.Id
    INNER JOIN [Contract].IPSService ips WITH (NOLOCK) ON ips.Id = ch.IPSServiceId
    WHERE ch.CupsEntityId = @CupsEntityId

	DECLARE @tmpCupsHomologationId INT,
		@tmpIPSServiceId INT,
		@tmpCupsEntityId INT,
		@tmpServiceManual TINYINT,
		@tmpIPSPresentation TINYINT,
		@tmpCodeNameCupsEntity VARCHAR(320),
		@tmpCodeNameIpsService VARCHAR(320),
		@tmpActivated BIT

	DECLARE homologation_cursor CURSOR FOR   
	SELECT CupsHomologationId, IPSServiceId, CupsEntityId, ServiceManual, IPSPresentation, CodeNameCupsEntity, CodeNameIpsService, Activated FROM @tmpListHomologation
  
	OPEN homologation_cursor
  
	FETCH NEXT FROM homologation_cursor INTO @tmpCupsHomologationId, @tmpIPSServiceId, @tmpCupsEntityId, @tmpServiceManual, @tmpIPSPresentation, @tmpCodeNameCupsEntity, @tmpCodeNameIpsService, @tmpActivated
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN

		DECLARE @CUPSSubGroupId INT, @CupsGroupId INT
		SELECT @CUPSSubGroupId = ce.CUPSSubGroupId, @CupsGroupId = cs.CupsGroupId
		FROM Contract.CUPSEntity ce
		JOIN Contract.CupsSubgroup cs ON ce.CUPSSubGroupId = cs.Id
		WHERE ce.Id = @tmpCupsEntityId

		DECLARE @HomologationRateManualId INT
			, @HomologationLiquidationType TINYINT
			, @HomologationRateManualValidityId INT
			, @DefinitionRateDetailId INT
			, @ConditionType TINYINT
			, @HManualType TINYINT
		
		SELECT TOP 1	@HomologationRateManualId = drd.RateManualId,
						@HomologationLiquidationType = drd.LiquidationType, 
						@HomologationRateManualValidityId = drd.RateManualValidityId,
						@DefinitionRateDetailId =drd.Id,
						@ConditionType = drd.ConditionType,
						@HManualType = rm.[Type]
		FROM Contract.DefinitionRateDetail drd
		LEFT JOIN Contract.RateManual rm on drd.RateManualId = rm.Id
		WHERE DefinitionRateId = @DefinitionRateId AND ISNULL(rm.[Type], @tmpServiceManual) = @tmpServiceManual AND (RuleType = 5 OR (
			(RuleType = 1 AND IPSServiceId = @tmpIPSServiceId AND drd.CUPSEntityId = @tmpCupsEntityId) OR
			(RuleType = 2 AND CUPSEntityId = @tmpCupsEntityId) OR 
			(RuleType = 3 AND CUPSSubgroupId = @CUPSSubGroupId) OR 
			(RuleType = 4 AND CUPSGroupId = @CupsGroupId)
		))
		ORDER BY drd.RuleType ASC, drd.ConditionType DESC

		IF @HomologationLiquidationType = 3
		BEGIN
			-- Tipo de liquidación por vigencia: Busamos la vigencia y con este el manual de tarifas
			SELECT TOP 1 @HomologationRateManualId = RateManualId, @HManualType =  rm.[Type]
			FROM Contract.RateManualValidityDetail rvd
			JOIN Contract.RateManual rm ON rvd.RateManualId = rm.Id
			WHERE RateManualValidityId = @HomologationRateManualValidityId AND InitialDate <= @ServiceDate AND EndDate >= @ServiceDate
		END

		IF @tmpIPSPresentation = 2
		BEGIN
			IF @HManualType = @tmpServiceManual
			BEGIN
				declare @surgicalListXml xml = (
					SELECT ROW_NUMBER() OVER(ORDER BY sp.Id ASC) as RowId,
						IPSServiceId,
						ServiceAmount,
						ips.ServiceClass as ClassService,
						0 as ValueItemServiceOrderDetail,
						'' as PerformsHealthProfessionalCode
					FROM Contract.SurgicalProcedureService sp
					JOIN Contract.IPSService ips ON sp.IPSServiceId = ips.Id
					WHERE IPSServiceParentId = @tmpIPSServiceId
					FOR XML PATH('ListSurgicalDefault')
				)

				DECLARE @SODXML XML = (
					SELECT 
						0 AS Id,
						@CupsEntityId AS CUPSEntityId,
						@CareGroupId as CareGroupId,
						@PerformsFunctionalUnitId as PerformsFunctionalUnitId,
						@PerformsProfessionalSpecialty as PerformsProfessionalSpecialty,
						@ServiceDate as ServiceDate,
						@tmpIPSServiceId as IPSServiceId,
						0 AS SurgicalInterventionType,
						0 AS SurchargeApply,
						0 AS CostValue,
						'' AS PerformsHealthProfessionalCode,
						0 AS PerformsHealthProfessionalThirdPartyId
					FOR XML PATH('ServiceOrderDetail')
				)
			
				declare @tmpSurgicalRateResult TABLE(StatusResult BIT)
				DELETE FROM @tmpSurgicalRateResult
				INSERT INTO @tmpSurgicalRateResult
				SELECT StatusResult
				FROM [Contract].[GetServiceValueBySurgicalProcedureService](@SODXML, '', @surgicalListXml)

				IF EXISTS (SELECT 1 FROM @tmpSurgicalRateResult WHERE StatusResult = 1)
				BEGIN
					INSERT INTO @ListHomologation VALUES (1, '', @tmpCupsHomologationId, @tmpIPSServiceId, @tmpCupsEntityId, @tmpCodeNameCupsEntity, @tmpCodeNameIpsService, @tmpActivated)
				END
			END
		END
		ELSE
		BEGIN
			IF @HomologationRateManualId IS NOT NULL
			BEGIN				
				IF @HomologationLiquidationType IN (2, 3)
				BEGIN
					IF EXISTS (SELECT 1 FROM Contract.RateManualDetail WHERE RateManualId = @HomologationRateManualId AND IPSServiceId = @tmpIPSServiceId)
					BEGIN
						INSERT INTO @ListHomologation VALUES (1, '', @tmpCupsHomologationId, @tmpIPSServiceId, @tmpCupsEntityId, @tmpCodeNameCupsEntity, @tmpCodeNameIpsService, @tmpActivated)
					END
				END
				ELSE
				BEGIN
					INSERT INTO @ListHomologation VALUES (1, '', @tmpCupsHomologationId, @tmpIPSServiceId, @tmpCupsEntityId, @tmpCodeNameCupsEntity, @tmpCodeNameIpsService, @tmpActivated)
				END
			END
			ELSE IF @DefinitionRateDetailId IS NOT NULL AND @ConditionType <> 5 BEGIN
				INSERT INTO @ListHomologation VALUES (1, '', @tmpCupsHomologationId, @tmpIPSServiceId, @tmpCupsEntityId, @tmpCodeNameCupsEntity, @tmpCodeNameIpsService, @tmpActivated)				 
			END
		END

		FETCH NEXT FROM homologation_cursor INTO @tmpCupsHomologationId, @tmpIPSServiceId, @tmpCupsEntityId, @tmpServiceManual, @tmpIPSPresentation, @tmpCodeNameCupsEntity, @tmpCodeNameIpsService, @tmpActivated
	END   
	CLOSE homologation_cursor
	DEALLOCATE homologation_cursor

    IF @ServiceType = @ManualType
    BEGIN
        IF
        (
            SELECT COUNT(CupsHomologationId)
            FROM @ListHomologation
            WHERE IPSServiceId = @IPSServiceId
        ) > 0
        BEGIN
            DECLARE @CupsHomologationId1 INT,
                    @IPSServiceId1 INT,
                    @CupsEntityId1 INT,
                    @CodeNameCups VARCHAR(320),
                    @CodeNameIps VARCHAR(320);

            SELECT @CupsHomologationId1 = CupsHomologationId,
                   @IPSServiceId1 = IPSServiceId,
                   @CupsEntityId1 = CupsEntityId,
                   @CodeNameCups = CodeNameCupsEntity,
                   @CodeNameIps = CodeNameIpsService
            FROM @ListHomologation
            WHERE IPSServiceId = @IPSServiceId;

            DELETE FROM @ListHomologation;

            INSERT INTO @ListHomologation
            VALUES
            (1, '', @CupsHomologationId1, @IPSServiceId1, @CupsEntityId1, @CodeNameCups, @CodeNameIps, 0);
        END;
    END;

    IF
    (
        SELECT COUNT(CupsHomologationId) FROM @ListHomologation
    ) = 0
    BEGIN
        DECLARE @ipsServiceFullName VARCHAR(200),
                @CareGroupCodeName VARCHAR(200);
        IF @IPSServiceId IS NOT NULL
           AND @IPSServiceId <> 0
        BEGIN
            SELECT @ipsServiceFullName = CONCAT(Code, ' - ', [Name])
            FROM [Contract].IPSService WITH (NOLOCK)
            WHERE Id = @IPSServiceId;
        END;
        ELSE
        BEGIN
            SELECT @ipsServiceFullName = CONCAT(Code, ' - ', [Description])
            FROM [Contract].CUPSEntity WITH (NOLOCK)
            WHERE Id = @CupsEntityId;
        END;
        SELECT @CareGroupCodeName = CONCAT(Code, ' - ', [Name])
        FROM [Contract].CareGroup WITH (NOLOCK)
        WHERE Id = @CareGroupId;

        INSERT INTO @ListHomologation
        VALUES
        (0,
         'No se Encontraron Homologos para el item ' + @ipsServiceFullName + ' para el grupo de atención '
         + @CareGroupCodeName, NULL, NULL, NULL, NULL, NULL, 0);
    END;

    RETURN;
END;