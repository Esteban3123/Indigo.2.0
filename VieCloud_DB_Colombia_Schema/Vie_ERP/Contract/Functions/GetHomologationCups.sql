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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que resuelve la homologación entre un código CUPS de una entidad contratante y el servicio interno equivalente de la IPS, validando que dicha equivalencia sea tarifable para un grupo de atención, unidad funcional, especialidad profesional y fecha de servicio dados. Consulta la tabla de homologaciones CUPS-IPS para encontrar las correspondencias del código solicitado, determina la tarifa aplicable según el contrato y rango de fechas vigente, y evalúa si el servicio es quirúrgico con presentación especial llamando a funciones de valoración de procedimientos quirúrgicos. Retorna el resultado de la homologación con su estado de validez, los códigos internos y nombres legibles del servicio CUPS y del servicio IPS, permitiendo al proceso de facturación, liquidación de cuentas y RIPS mapear correctamente el código del asegurador con el servicio que presta la institución.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetHomologationCups';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'GetHomologationCups';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina los servicios IPS homólogos válidos para un código CUPS dado, según el grupo de atención, fecha de servicio y reglas tarifarias del contrato vigente.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetHomologationCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una definición de tarifa (CareGroupDefinitionRate) vigente para el grupo de atención en la fecha de servicio; Deben existir homologaciones registradas en CupsHomologation para la entidad CUPS consultada; El servicio IPS y la entidad CUPS deben estar definidos en sus catálogos maestros', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetHomologationCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La función siempre retorna al menos un registro: o las homologaciones válidas o un mensaje de error con StatusResult=0; Solo se evalúan homologaciones cuya CupsEntityId coincida con la solicitada; La búsqueda de tarifa se restringe a la vigencia donde InitialDate <= ServiceDate <= EndDate; Las reglas de DefinitionRateDetail se aplican jerárquicamente: específica (IPSService+CUPS) sobre general (CUPS, subgrupo, grupo, regla 5); Para servicios con presentación quirúrgica (=2) la validación depende exclusivamente del cálculo de valor por procedimiento quirúrgico; Una homologación con ConditionType=5 sin RateManualId no es aceptada', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetHomologationCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Homologación CUPS; Código CUPS (Clasificación Única de Procedimientos en Salud); Servicio IPS; Grupo de atención (CareGroup); Manual tarifario; Vigencia tarifaria; Tipo de liquidación; Procedimiento quirúrgico; Especialidad profesional; Unidad funcional; Fecha de servicio; Subgrupo y grupo CUPS', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetHomologationCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ListHomologation: Cuando la presentación del IPS = 2 y el tipo de manual del homologado coincide con el manual del servicio, si GetServiceValueBySurgicalProcedureService retorna StatusResult=1, se inserta la homologación como válida; [INSERT] @ListHomologation: Cuando hay RateManualId homologado y LiquidationType IN (2,3), se inserta solo si existe RateManualDetail con ese manual e IPSService; [INSERT] @ListHomologation: Cuando hay RateManualId homologado y LiquidationType no es 2 ni 3, se inserta la homologación directamente; [INSERT] @ListHomologation: Cuando no hay RateManualId pero existe DefinitionRateDetailId y ConditionType <> 5, se inserta la homologación; [DELETE] @ListHomologation: Cuando @ServiceType = @ManualType y existe coincidencia con el IPSServiceId solicitado, se eliminan todos los registros y se conserva solo el del IPSService coincidente; [INSERT] @ListHomologation: Cuando no se encontró ninguna homologación válida, se inserta un registro con StatusResult=0 y mensaje ''No se Encontraron Homologos para el item ... para el grupo de atención ...''', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetHomologationCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HomologationLiquidationType = 3 → Se busca el RateManualId vigente en RateManualValidityDetail según la fecha de servicio para sustituir el manual de tarifas; si IPSPresentation = 2 (presentación quirúrgica) → Se valida la homologación invocando GetServiceValueBySurgicalProcedureService con la lista de procedimientos quirúrgicos asociados else Se evalúan las reglas de manual tarifario y de DefinitionRateDetail/ConditionType; si IPSPresentation = 2 y HManualType = ServiceManual → Se construye el XML de servicios quirúrgicos y se valida con GetServiceValueBySurgicalProcedureService; si RateManualId no nulo y LiquidationType IN (2,3) → Se exige que exista RateManualDetail para el IPSService antes de aceptar la homologación else Si RateManualId no nulo se acepta directamente; si es nulo, se valida DefinitionRateDetail con ConditionType<>5; si Selección de DefinitionRateDetail → Se prioriza por RuleType ASC (1=IPSService+CUPS, 2=CUPS, 3=Subgrupo, 4=Grupo, 5=general) y ConditionType DESC, filtrando por tipo de manual del servicio; si @ServiceType = @ManualType y existe coincidencia con @IPSServiceId → Se filtra el resultado dejando únicamente la homologación que coincide con el IPSService solicitado; si Resultado vacío al final → Se devuelve un único registro con StatusResult=0 y mensaje de error indicando ítem y grupo de atención', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetHomologationCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.GetServiceValueBySurgicalProcedureService', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetHomologationCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroupDefinitionRate; Contract.CupsHomologation; Contract.CUPSEntity; Contract.IPSService; Contract.CupsSubgroup; Contract.DefinitionRateDetail; Contract.RateManual; Contract.RateManualValidityDetail; Contract.RateManualDetail; Contract.SurgicalProcedureService; Contract.CareGroup', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetHomologationCups';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'GetHomologationCups';
GO
