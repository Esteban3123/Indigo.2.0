-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 23-01-2015
-- Description:	Store para crear ordenes de servicio
-- =============================================
CREATE PROCEDURE [Billing].[SP_GenerateServiceOrder_Test] @ServiceOrderXml AS XML, 
                                                         @User            VARCHAR(20)
AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @IdServiceOrder INT, @CodeServiceOrder VARCHAR(20), @AdmissionNumber VARCHAR(10), @PatientCode VARCHAR(15), @OrderDate DATETIME, @AffectInventory BIT, @EntityCode VARCHAR(20), @EntityId INT, @EntityName VARCHAR(250), @OperatingUnitId INT, @Status TINYINT;
        DECLARE @TableDetail TABLE
        (RowId                                  INT IDENTITY(1, 1) PRIMARY KEY, 
         IdRow                                  INT, 
         IdDetail                               INT, 
         ServiceOrderId                         INT NOT NULL, 
         CareGroupId                            INT NOT NULL, 
         HealthAdministratorId                  INT NULL, 
         ThirdPartyId                           INT NULL, 
         ServiceType                            TINYINT NOT NULL, 
         RecordType                             TINYINT NOT NULL, 
         CUPSEntityId                           INT NULL, 
         IPSServiceId                           INT NULL, 
         HospitalStayId                         INT NULL, 
         HospitalStayDetailId                   INT NULL, 
         ControlExternalConsultation            TINYINT NULL, 
         ControlExternalConsultationCode        NUMERIC(18, 0) NULL, 
         CUPSAssociateService                   BIT NOT NULL, 
         CodeAssociateService                   VARCHAR(50) NULL, 
         IsPackage                              BIT NOT NULL, 
         Packaging                              BIT NOT NULL, 
         PackageServiceOrderDetailId            INT NULL, 
         LiquidationType                        TINYINT NOT NULL, 
         Presentation                           TINYINT NULL, 
         ProductId                              INT NULL, 
         InvoicedQuantity                       INT NOT NULL, 
         SupplyQuantity                         INT NOT NULL, 
         DevolutionQuantity                     INT NOT NULL, 
         RateManualSalePrice                    NUMERIC(18, 0) NOT NULL, 
         CostValue                              NUMERIC(18, 2) NOT NULL, 
         ServiceDate                            DATETIME NOT NULL, 
         AuthorizationNumber                    VARCHAR(20) NULL, 
         PerformsFunctionalUnitId               INT NOT NULL, 
         PerformsHealthProfessionalCode         CHAR(20) NULL, 
         PerformsProfessionalSpecialty          CHAR(3) NULL, 
         PerformsHealthProfessionalThirdPartyId INT NULL, 
         BillingConceptId                       INT NULL, 
         CostCenterId                           INT NOT NULL, 
         SettlementType                         TINYINT NOT NULL, 
         IncludeServiceOrderDetailId            INT NULL, 
         IncludeServiceOrderDetailIdRow         INT NULL, 
         RecoveryRatio                          NUMERIC(5, 2) NULL, 
         RateManualId                           INT NULL, 
         RateManualType                         TINYINT NULL, 
         RateManualDetailId                     INT NULL, 
         DefinitionRateDetailId                 INT NULL, 
         DefinitionRateDetailConditionId        INT NULL, 
         SubTotalSalesPrice                     NUMERIC(18, 2) NOT NULL, 
         ThirdPartyDiscount                     NUMERIC(18, 0) NOT NULL, 
         ThirdPartyDiscountPercentage           NUMERIC(5, 2) NOT NULL, 
         TotalSalesPrice                        NUMERIC(18, 2) NOT NULL, 
         GrandTotalSalesPrice                   NUMERIC(18, 0) NOT NULL, 
         SurchargeApply                         BIT NOT NULL, 
         SurgicalInterventionType               TINYINT NULL, 
         SurgeryNumber                          TINYINT NOT NULL, 
         IsFirstEvent                           BIT NOT NULL, 
         IsAnnulled                             BIT NOT NULL, 
         IsDelete                               BIT NOT NULL, 
         IncomeMainAccountId                    INT NOT NULL, 
         EntityState                            VARCHAR(50), 
         ApplyRIAS                              VARCHAR(20) NULL, 
         RIASCupsId                             INT NULL, 
         RealizedQuantity                       INT NULL
        );
        DECLARE @TableSurgical TABLE
        (Id                                     INT NOT NULL, 
         ServiceOrderDetailIdRow                INT NOT NULL, 
         ServiceOrderDetailId                   INT NOT NULL, 
         IPSServiceId                           INT NOT NULL, 
         InvoicedQuantity                       INT NOT NULL, 
         LiquidationPercentage                  NUMERIC(5, 2) NOT NULL, 
         RateManualSalePrice                    NUMERIC(18, 0) NOT NULL, 
         TotalSalesPrice                        NUMERIC(18, 0) NOT NULL, 
         PerformsHealthProfessionalCode         CHAR(20) NULL, 
         PerformsHealthProfessionalThirdPartyId INT NULL, 
         CostValue                              NUMERIC(18, 2) NOT NULL, 
         BillingConceptId                       INT NOT NULL, 
         CostCenterId                           INT NOT NULL, 
         RateManualDetailSurgicalId             INT NULL, 
         SurchargeApply                         BIT NOT NULL, 
         OnlyMedicalFees                        BIT NOT NULL, 
         IncomeMainAccountId                    INT NOT NULL, 
         EntityState                            VARCHAR(50)
        );
        DECLARE @TableResult TABLE
        (CodeMessage    VARCHAR(20), 
         [Message]      VARCHAR(MAX), 
         ServiceOrderId INT, 
         [Status]       TINYINT
        ); --- Estado del mensaje 1 - Correcto 2 - Advertencia 3 - Error
        DECLARE @TableRevenueControlDetailRefresh TABLE
        (RowId INT IDENTITY(1, 1) PRIMARY KEY, 
         Id    INT
        ); --- Tabla temporal para guardar los Id de los Folios que se van a tocar en la orden para luego recalcularlos
        DECLARE @AnnulmentUser VARCHAR(20)= NULL;
        DECLARE @AnnulmentDate DATETIME= NULL;
        DECLARE @ModificationUser VARCHAR(20)= NULL;
        DECLARE @ModificationDate DATETIME= NULL;
        DECLARE @IdRevenueControlDetailPackage INT; -- Variable usada cuando se empaqueta y se desempaqueta, esta es para dejar los servicios en el mismo folio
        --Tabla para almacenar la info para la anulación de rias
        DECLARE @XmlAnnulmentRias TABLE
        (CupsCode   VARCHAR(20), 
         RIASCupsId INT
        );

        --Variables para la reversión de rias
        DECLARE @CodeMessageAnnulment VARCHAR(20), @MessageAnnulment VARCHAR(MAX);

        --Variable para generar el xml de anulación de rias
        DECLARE @generateXmlAnnulment XML;
        BEGIN TRY
            SELECT @IdServiceOrder = t.x.value('Id[1]', 'int'), 
                   @CodeServiceOrder = t.x.value('Code[1]', 'varchar(20)'), 
                   @AdmissionNumber = t.x.value('AdmissionNumber[1]', 'varchar(10)'), 
                   @PatientCode = t.x.value('PatientCode[1]', 'varchar(15)'),
        --	@OrderDate = convert(datetime, t.x.value('OrderDate[1]', 'nvarchar(19)'), 103),
                   @OrderDate = t.x.value('OrderDate[1]', 'datetime'), 
                   @AffectInventory = t.x.value('AffectInventory[1]', 'bit'), 
                   @EntityCode = t.x.value('EntityCode[1]', 'varchar(20)'), 
                   @EntityId = t.x.value('EntityId[1]', 'int'), 
                   @EntityName = t.x.value('EntityName[1]', 'varchar(250)'), 
                   @OperatingUnitId = t.x.value('OperatingUnitId[1]', 'int'), 
                   @Status = ISNULL(t.x.value('Status[1]', 'tinyint'), 0)
            FROM @ServiceOrderXml.nodes('/ServiceOrder') t(x);
            INSERT INTO @TableDetail
                   SELECT t.x.value('RowXml[1]', 'int'), 
                          t.x.value('Id[1]', 'int'), 
                          @IdServiceOrder, 
                          t.x.value('CareGroupId[1]', 'int'), 
                          t.x.value('HealthAdministratorId[1]', 'int'), 
                          t.x.value('ThirdPartyId[1]', 'int'), 
                          t.x.value('ServiceType[1]', 'tinyint'), 
                          t.x.value('RecordType[1]', 'tinyint'), 
                          t.x.value('CUPSEntityId[1]', 'int'), 
                          t.x.value('IPSServiceId[1]', 'int'), 
                          t.x.value('HospitalStayId[1]', 'int'), 
                          t.x.value('HospitalStayDetailId[1]', 'int'), 
                          t.x.value('ControlExternalConsultation[1]', 'tinyint'), 
                          t.x.value('ControlExternalConsultationCode[1]', 'numeric(18,0)'), 
                          t.x.value('CUPSAssociateService[1]', 'bit'), 
                          t.x.value('CodeAssociateService[1]', 'varchar(50)'), 
                          t.x.value('IsPackage[1]', 'bit'), 
                          t.x.value('Packaging[1]', 'bit'), 
                          t.x.value('PackageServiceOrderDetailId[1]', 'int'), 
                          t.x.value('LiquidationType[1]', 'tinyint'), 
                          t.x.value('Presentation[1]', 'tinyint'), 
                          t.x.value('ProductId[1]', 'int'), 
                          t.x.value('InvoicedQuantity[1]', 'int'), 
                          t.x.value('SupplyQuantity[1]', 'int'), 
                          t.x.value('DevolutionQuantity[1]', 'int'), 
                          t.x.value('RateManualSalePrice[1]', 'numeric(18,0)'), 
                          t.x.value('CostValue[1]', 'numeric(18,2)'),
--convert(datetime, t.x.value('ServiceDate[1]', 'nvarchar(19)'), 103),
                          t.x.value('ServiceDate[1]', 'datetime'), 
                          t.x.value('AuthorizationNumber[1]', 'varchar(20)'), 
                          t.x.value('PerformsFunctionalUnitId[1]', 'int'), 
                          t.x.value('PerformsHealthProfessionalCode[1]', 'varchar(20)'), 
                          t.x.value('PerformsProfessionalSpecialty[1]', 'varchar(3)'), 
                          t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'int'), 
                          t.x.value('BillingConceptId[1]', 'int'), 
                          t.x.value('CostCenterId[1]', 'int'), 
                          t.x.value('SettlementType[1]', 'tinyint'), 
                          t.x.value('IncludeServiceOrderDetailId[1]', 'int'), 
                          t.x.value('IncludeServiceOrderDetailIdRow[1]', 'int'), 
                          t.x.value('RecoveryRatio[1]', 'numeric(5,2)'), 
                          t.x.value('RateManualId[1]', 'int'), 
                          t.x.value('RateManualType[1]', 'tinyint'), 
                          t.x.value('RateManualDetailId[1]', 'int'), 
                          t.x.value('DefinitionRateDetailId[1]', 'int'), 
                          t.x.value('DefinitionRateDetailConditionId[1]', 'int'), 
                          t.x.value('SubTotalSalesPrice[1]', 'numeric(18,2)'), 
                          t.x.value('ThirdPartyDiscount[1]', 'numeric(18,0)'), 
                          t.x.value('ThirdPartyDiscountPercentage[1]', 'numeric(5,2)'), 
                          t.x.value('TotalSalesPrice[1]', 'numeric(18,2)'), 
                          t.x.value('GrandTotalSalesPrice[1]', 'numeric(18,0)'), 
                          t.x.value('SurchargeApply[1]', 'bit'), 
                          t.x.value('SurgicalInterventionType[1]', 'tinyint'), 
                          t.x.value('SurgeryNumber[1]', 'tinyint'), 
                          t.x.value('IsFirstEvent[1]', 'bit'), 
                          t.x.value('IsAnnulled[1]', 'bit'), 
                          t.x.value('IsDelete[1]', 'bit'), 
                          t.x.value('IncomeMainAccountId[1]', 'int'), 
                          t.x.value('EntityState[1]', 'varchar(50)'), 
                          t.x.value('ApplyRIAS[1]', 'varchar(20)'), 
                          t.x.value('RIASCupsId[1]', 'int'), 
                          t.x.value('RealizedQuantity[1]', 'int')
                   FROM @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail') t(x);
            INSERT INTO @TableSurgical
                   SELECT t.x.value('Id[1]', 'int'), 
                          t.x.value('ServiceOrderDetailIdRow[1]', 'int'), 
                          t.x.value('ServiceOrderDetailId[1]', 'int'), 
                          t.x.value('IPSServiceId[1]', 'int'), 
                          t.x.value('InvoicedQuantity[1]', 'int'), 
                          t.x.value('LiquidationPercentage[1]', 'numeric(5,2)'), 
                          t.x.value('RateManualSalePrice[1]', 'numeric(18,0)'), 
                          t.x.value('TotalSalesPrice[1]', 'numeric(18,0)'), 
                          t.x.value('PerformsHealthProfessionalCode[1]', 'varchar(20)'), 
                          t.x.value('PerformsHealthProfessionalThirdPartyId[1]', 'int'), 
                          t.x.value('CostValue[1]', 'numeric(18,2)'), 
                          t.x.value('BillingConceptId[1]', 'int'), 
                          t.x.value('CostCenterId[1]', 'int'), 
                          t.x.value('RateManualDetailSurgicalId[1]', 'int'), 
                          t.x.value('SurchargeApply[1]', 'bit'), 
                          t.x.value('OnlyMedicalFees[1]', 'bit'), 
                          t.x.value('IncomeMainAccountId[1]', 'int'), 
                          t.x.value('EntityState[1]', 'varchar(50)')
                   FROM @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail/ServiceOrderDetailSurgical') t(x)
                   WHERE t.x.value('Id[1]', 'int') IS NOT NULL;
            DECLARE @Uno TINYINT= 1, @Cero TINYINT= 0, @Dos TINYINT= 2, @Tres TINYINT= 3, @Cuatro TINYINT= 4, @Modified VARCHAR(8)= 'Modified', @Deleted VARCHAR(7)= 'Deleted', @Added VARCHAR(5)= 'Added', @Unchanged VARCHAR(9)= 'Unchanged', @Tag755 VARCHAR(3)= '755';

            -- Como el formulario de ordenes de servicio tambien valida que si lo va a eliminar y esta en un factura anulada simplemente cambia el valor de IsDelete a true y yo necesito es que venga marcado como eliminado en el EntityState
            UPDATE @TableDetail
              SET 
                  EntityState = 'Deleted'
            WHERE IsDelete = @Uno
                  AND EntityState = @Modified;

            --- VALIDACIONES ---
            DECLARE @AdmissionDate DATETIME;
            DECLARE @AdmissionStatus CHAR(1);
            SELECT @AdmissionDate = IFECHAING, 
                   @AdmissionStatus = IESTADOIN
            FROM dbo.ADINGRESO WITH(NOLOCK)
            WHERE NUMINGRES = @AdmissionNumber;
            IF CAST(@OrderDate AS DATE) < CAST(@AdmissionDate AS DATE)
                BEGIN
                    SELECT '999' AS CodeMessage, 
                           'La fecha de la orden es menor a la fecha de la admisión' AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            IF @AdmissionStatus = 'F'
                BEGIN
                    SELECT '999' AS CodeMessage, 
                           'No se puede crear la orden de servicio porque el ingreso ' + RTRIM(LTRIM(@AdmissionNumber)) + ' esta facturado' AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            IF @EntityName = 'PharmaceuticalDispensing'
               AND @Status = 3
                BEGIN
                    SELECT '999' AS CodeMessage, 
                           'No se puede anular una orden de servicio generada desde dispensación' AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            IF EXISTS
            (
                SELECT 1
                FROM @TableDetail td
                     INNER JOIN [Contract].CareGroup cg WITH(NOLOCK) ON td.CareGroupId = cg.Id
                     INNER JOIN [Contract].[Contract] c WITH(NOLOCK) ON c.Id = cg.ContractId
                WHERE c.[Status] = @Dos
                      OR c.[Status] = @Tres
            )
                BEGIN
                    DECLARE @StringContract VARCHAR(MAX);
                    SET @StringContract =
                    (
                        SELECT c.Code + ', '
                        FROM @TableDetail td
                             INNER JOIN [Contract].CareGroup cg WITH(NOLOCK) ON td.CareGroupId = cg.Id
                             INNER JOIN [Contract].[Contract] c ON c.Id = cg.ContractId
                        WHERE c.[Status] = @Dos
                              OR c.[Status] = @Tres FOR XML PATH('')
                    );
                    SELECT '999' AS CodeMessage, 
                           'Los siguientes Contratos estan suspendidos o terminados: ' + @StringContract AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            IF
            (
                SELECT COUNT(*)
                FROM @TableDetail
                WHERE ISPackage = @Uno
            ) > 0
            AND
            (
                SELECT COUNT(*)
                FROM @TableDetail
            ) > 1
                BEGIN
                    SELECT '999' AS CodeMessage, 
                           'Las ordenes de servicios que poseen items que empaquetan no pueden contener mas servicios' AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            --- Guardar los id de los folios que se pueden recalcular
            INSERT INTO @TableRevenueControlDetailRefresh
                   SELECT DISTINCT 
                          sodd.RevenueControlDetailId
                   FROM Billing.ServiceOrder so WITH(NOLOCK)
                        INNER JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.ServiceOrderId = so.Id
                        INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
                   WHERE so.Id = @IdServiceOrder;
            IF @IdServiceOrder = 0
                BEGIN
                    IF @CodeServiceOrder = ''
                        BEGIN
                            --genero la secuencia para la orden de servicio
                            DECLARE @idSequenceDetail INT;
                            DECLARE @pattern VARCHAR(300);
                            DECLARE @NextS BIGINT;
                            DECLARE @Scope VARCHAR(5);
                            DECLARE @IdSequence INT;
                            IF NOT EXISTS
                            (
                                SELECT 1
                                FROM Billing.BillingSequence WITH(NOLOCK)
                                WHERE IdForm = @Tag755
                            )
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'No se ha creado secuencia numerica para el formulario de ordenes de servicio' AS Message, 
                                           0 ServiceOrderId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            SELECT @IdSequence = Id, 
                                   @Scope = Scope
                            FROM Billing.BillingSequence WITH(NOLOCK)
                            WHERE IdForm = @Tag755;
                            IF @Scope = 'OU'
                                BEGIN
                                    SELECT @pattern = cs.Pattern, 
                                           @idSequenceDetail = psd.Id
                                    FROM Billing.BillingSequenceDetail psd WITH(NOLOCK)
                                         INNER JOIN Common.Sequense cs WITH(NOLOCK) ON cs.Id = psd.IdSequense
                                    WHERE psd.IdSequenseBillingC = @IdSequence
                                          AND IdOperatingUnit = @OperatingUnitId;
                            END;
                                ELSE
                                BEGIN
                                    SELECT @pattern = cs.Pattern, 
                                           @idSequenceDetail = psd.Id
                                    FROM Billing.BillingSequenceDetail psd WITH(NOLOCK)
                                         INNER JOIN Common.Sequense cs WITH(NOLOCK) ON cs.Id = psd.IdSequense
                                    WHERE psd.IdSequenseBillingC = @IdSequence;
                            END;
                            IF(@idSequenceDetail IS NULL)
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'La secuencia para Ordenes de Servicio no esta parametrizada' AS Message, 
                                           0 ServiceOrderId, 
                                           3 AS [Status];
                                    RETURN;
                            END;
                            UPDATE Billing.BillingSequenceDetail
                              SET 
                                  @NextS = [Next]+=1
                            WHERE Id = @idSequenceDetail;
                            SELECT @CodeServiceOrder = dbo.GetSequence('', @pattern, (@NextS - 1));
                    END;		
                    -- Le agrego el Entity Name si es creado desde el mismo form de ordenes de servicio
                    IF ISNULL(@EntityName, '') = ''
                        BEGIN
                            SET @EntityName = 'ServiceOrder';
                            SET @EntityCode = @CodeServiceOrder;
                    END;
            END;
                ELSE
                BEGIN -- Si la orden de servicio ya estaba creada
                    IF @Status = 3
                        BEGIN --Si estan anulando la orden
                            IF EXISTS
                            (
                                SELECT 1
                                FROM @TableDetail
                                WHERE HospitalStayId IS NOT NULL
                            )
                                BEGIN
                                    IF EXISTS
                                    (
                                        SELECT 1
                                        FROM @TableDetail td
                                             INNER JOIN dbo.CHREGESTADET chd WITH(NOLOCK) ON chd.ID = td.HospitalStayDetailId
                                             INNER JOIN dbo.CHREGESTADET chdtmp WITH(NOLOCK) ON chdtmp.IDCHREGESTA = chd.IDCHREGESTA
                                                                                                AND chdtmp.GENLIQUIDA > chd.GENLIQUIDA
                                    )
                                        BEGIN
                                            SELECT '999' AS CodeMessage, 
                                                   'No se puede anular la orden de servicio porque la estancia tiene otras liquidaciones de estancia con fechas superiores a esta' AS Message, 
                                                   0 ServiceOrderId, 
                                                   CAST(3 AS TINYINT) AS [Status];
                                            RETURN;
                                    END;
                                    DELETE det
                                    FROM dbo.CHREGESTADET det WITH(NOLOCK)
                                         INNER JOIN @TableDetail ON det.ID = HospitalStayDetailId;
                                    DECLARE @FechUltima DATE;
                                    SELECT TOP 1 @FechUltima = GENLIQUIDA
                                    FROM dbo.CHREGESTADET Det WITH(NOLOCK)
                                         INNER JOIN dbo.CHREGESTA ta WITH(NOLOCK) ON Det.IDCHREGESTA = ta.ID
                                    WHERE ta.NUMINGRES = @AdmissionNumber
                                    ORDER BY Det.ID DESC;

                                    --If @FechUltima Is Not Null Begin
                                    UPDATE dbo.ADINGRESO
                                      SET 
                                          GENULTLIQUI = @FechUltima
                                    WHERE NUMINGRES = @AdmissionNumber;
                                    --End
                                    --- Actializo el estado de la estancias 1 - sin liquidar 2 - Liquidado parcial
                                    UPDATE dbo.CHREGESTA
                                      SET 
                                          GENESTLIQ = data.GENESTLIQ
                                    FROM
                                    (
                                        SELECT ch.ID,
                                               CASE ISNULL(chd.IDCHREGESTA, 0)
                                                   WHEN 0
                                                   THEN 1
                                                   ELSE 2
                                               END AS GENESTLIQ
                                        FROM @TableDetail td
                                             INNER JOIN dbo.CHREGESTA ch WITH(NOLOCK) ON td.HospitalStayId = ch.ID
                                             LEFT JOIN dbo.CHREGESTADET chd WITH(NOLOCK) ON chd.IDCHREGESTA = ch.ID
                                        GROUP BY ch.ID, 
                                                 chd.IDCHREGESTA
                                    ) data
                                    INNER JOIN dbo.CHREGESTA chre WITH(NOLOCK) ON chre.ID = data.ID;
                            END;
                            IF EXISTS
                            (
                                SELECT 1
                                FROM @TableDetail td
                                     INNER JOIN Billing.ServiceOrderDetailDistribution sodd ON td.IdDetail = sodd.ServiceOrderDetailId
                                     INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
                                WHERE rcd.[Status] > @Uno
                            )
                                BEGIN
                                    DECLARE @StringRevenueDetailNoCanceled VARCHAR(MAX);
                                    SET @StringRevenueDetailNoCanceled =
                                    (
                                        SELECT 'Folio: ' + CAST(rcd.FolioOrder AS VARCHAR(5)) + ', '
                                        FROM @TableDetail td
                                             INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                                             INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
                                        WHERE rcd.[Status] > @Uno FOR XML PATH('')
                                    );
                                    SELECT '999' AS CodeMessage, 
                                           'La orden de servicio no se puede anular debido que tiene items en folios que estan bloqueados o anulados: ' + @StringRevenueDetailNoCanceled AS Message, 
                                           0 ServiceOrderId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            IF EXISTS
                            (
                                SELECT 1
                                FROM @TableDetail
                                WHERE IsPackage = @Uno
                            )
                                BEGIN --- si se esta anulando una orden de servicio que empaqueta, entonces creo los registros de los detalles que el empaqueto
                                    SET @IdRevenueControlDetailPackage =
                                    (
                                        SELECT TOP 1 sodd.RevenueControlDetailId
                                        FROM @TableDetail td
                                             INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                                    );
                                    INSERT INTO Billing.ServiceOrderDetailDistribution
                                    (RevenueControlDetailId, 
                                     ServiceOrderDetailId, 
                                     Quantity, 
                                     GrandTotalSalesPrice, 
                                     GrandTotalDiscount, 
                                     DistributionType, 
                                     ThirdPartySalesPrice, 
                                     ThirdPartyPercentage, 
                                     ApplyRecoveryFee, 
                                     RecoveryFeeType, 
                                     SubTotalPatientSalesPrice, 
                                     PatientPercentage, 
                                     LastCaregroupId
                                    )
                                           SELECT @IdRevenueControlDetailPackage, 
                                                  sod.Id, 
                                                  sod.InvoicedQuantity, 
                                                  sod.GrandTotalSalesPrice, 
                                                  sod.ThirdPartyDiscount * sod.InvoicedQuantity, 
                                                  1, 
                                                  sod.GrandTotalSalesPrice, 
                                                  100, 
                                                  1, 
                                                  1, 
                                                  0, 
                                                  0, 
                                                  sod.CareGroupId
                                           FROM @TableDetail td
                                                INNER JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON td.IdDetail = sod.PackageServiceOrderDetailId;
                            END;
                            DELETE sodd
                            FROM Billing.ServiceOrderDetailDistribution sodd
                                 INNER JOIN @TableDetail td ON sodd.ServiceOrderDetailId = td.IdDetail
                            WHERE td.IdDetail > 0;
                            IF @EntityName = 'AccountControl'
                                BEGIN
                                    --se generó por control de cuentas
                                    UPDATE dbo.HCORDLABO
                                      SET 
                                          GENSERVICEORDER = NULL
                                    WHERE GENSERVICEORDER = @IdServiceOrder;
                                    UPDATE dbo.HCORDIMAG
                                      SET 
                                          GENSERVICEORDER = NULL
                                    WHERE GENSERVICEORDER = @IdServiceOrder;
                                    UPDATE dbo.HCORDPATO
                                      SET 
                                          GENSERVICEORDER = NULL
                                    WHERE GENSERVICEORDER = @IdServiceOrder;
                                    UPDATE dbo.HCORDPRON
                                      SET 
                                          GENSERVICEORDER = NULL
                                    WHERE GENSERVICEORDER = @IdServiceOrder;
                                    UPDATE dbo.HCORDINTE
                                      SET 
                                          GENSERVICEORDER = NULL
                                    WHERE GENSERVICEORDER = @IdServiceOrder;
                                    UPDATE dbo.HCPROCTER
                                      SET 
                                          GENSERVICEORDER = NULL
                                    WHERE GENSERVICEORDER = @IdServiceOrder;
                                    UPDATE dbo.HCHOGASIN
                                      SET 
                                          GENSERVICEORDER = NULL
                                    WHERE GENSERVICEORDER = @IdServiceOrder;
                                    UPDATE dbo.HCCONOXIG
                                      SET 
                                          GENSERVICEORDER = NULL
                                    WHERE GENSERVICEORDER = @IdServiceOrder;
                                    UPDATE dbo.HCHISPACA
                                      SET 
                                          GENSERVICEORDER = NULL
                                    WHERE GENSERVICEORDER = @IdServiceOrder;
                            END;

                            --Se elimina lo que haya en esta tabla
                            DELETE FROM @XmlAnnulmentRias;

                            --Se inserta en la tabla temporal los registros que manejen rias
                            INSERT INTO @XmlAnnulmentRias
                            (CupsCode, 
                             RIASCupsId
                            )
                                   SELECT ce.Code, 
                                          td.RIASCupsId
                                   FROM @TableDetail td
                                        INNER JOIN Contract.CUPSEntity ce ON ce.Id = td.CUPSEntityId
                                   WHERE td.ApplyRIAS = 'True';

                            --Si hay datos para reversar
                            IF EXISTS
                            (
                                SELECT *
                                FROM @XmlAnnulmentRias
                            )
                                BEGIN
                                    --Se genera el xml para reversar
                                    SET @generateXmlAnnulment =
                                    (
                                        SELECT CupsCode, 
                                               RIASCupsId
                                        FROM @XmlAnnulmentRias AS RIASForPatient FOR XML AUTO, ELEMENTS
                                    );

                                    --Se ejecuta el proceso de reversión
                                    EXEC dbo.SP_RIASAnnulment 
                                         @generateXmlAnnulment, 
                                         @PatientCode, 
                                         @User, 
                                         @CodeMessageAnnulment OUTPUT, 
                                         @MessageAnnulment OUTPUT;

                                    --Si hay error ejecutando el sp
                                    IF @CodeMessageAnnulment = '999'
                                        BEGIN
                                            SELECT '999' AS CodeMessage, 
                                                   @MessageAnnulment AS Message, 
                                                   0 ServiceOrderId, 
                                                   CAST(3 AS TINYINT) AS [Status];
                                            RETURN;
                                    END;
                            END;

                            --- Cambio todos a Unchanged ya que estoy anulando, igual el va a insertar los nuevos que agrego en el Detail y en Surgical por que el Id = 0 y yo no valido el EntityState, pero en el Distribution no se va a insertar debido que yo solo agrego los que tienen EntityState = ADD
                            UPDATE @TableDetail
                              SET 
                                  EntityState = 'Unchanged';
                            SET @AnnulmentUser = @User;
                            SET @AnnulmentDate = [Common].[GETDATE]();
                    END;
                        ELSE
                        BEGIN
                            SET @ModificationUser = @User;
                            SET @ModificationDate = [Common].[GETDATE]();
                    END;
            END;
            --- Si hay Items para eliminar entonces los elimino
            IF EXISTS
            (
                SELECT 1
                FROM @TableDetail
                WHERE EntityState = @Deleted
            )
                BEGIN
                    --- Elimino todos los detalles que se estan con el estado Deleted
                    IF EXISTS
                    (
                        SELECT 1
                        FROM @TableDetail
                        WHERE EntityState = @Deleted
                              AND IsPackage = @Uno
                    )
                        BEGIN
                            DECLARE @StringIPSDelete VARCHAR(MAX);
                            SET @StringIPSDelete =
                            (
                                SELECT ips.Code + ', '
                                FROM @TableDetail td
                                     INNER JOIN [Contract].IPSService ips WITH(NOLOCK) ON ips.Id = td.IPSServiceId
                                WHERE EntityState = @Deleted
                                      AND IsPackage = @Uno FOR XML PATH('')
                            );
                            SELECT '999' AS CodeMessage, 
                                   'Los siguientes servicios no se pueden eliminar porque están empaquetados: ' + @StringIPSDelete AS Message, 
                                   0 ServiceOrderId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    IF EXISTS
                    (
                        SELECT 1
                        FROM @TableDetail td
                             INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                        WHERE EntityState = @Deleted
                              AND sodd.DistributionType <> @Uno
                              AND sodd.DistributionType <> @Cuatro
                    )
                        BEGIN
                            DECLARE @StringIPSDistribution VARCHAR(MAX);
                            SET @StringIPSDistribution =
                            (
                                SELECT ips.Code + ', '
                                FROM @TableDetail td
                                     INNER JOIN [Contract].IPSService ips WITH(NOLOCK) ON ips.Id = td.IPSServiceId
                                     INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                                WHERE EntityState = @Deleted
                                      AND sodd.DistributionType <> @Uno
                                      AND sodd.DistributionType <> @Cuatro FOR XML PATH('')
                            );
                            SELECT '999' AS CodeMessage, 
                                   'Los siguientes servicios no se pueden eliminar porque están distribuidos: ' + @StringIPSDistribution AS Message, 
                                   0 ServiceOrderId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    todo:
                    IF EXISTS
                    (
                        SELECT 1
                        FROM @TableDetail td
                             INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                             INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
                        WHERE EntityState = @Deleted
                              AND (rcd.[Status] = @Dos
                                   OR rcd.[Status] = @Tres)
                    )
                        BEGIN
                            DECLARE @StringRevenueDetail VARCHAR(MAX);
                            SET @StringRevenueDetail =
                            (
                                SELECT 'Folio: ' + rcd.FolioOrder + ' Servicio: ' + ips.Code + ' Fecha: ' + td.ServiceDate + CHAR(13) + CHAR(10)
                                FROM @TableDetail td
                                     INNER JOIN [Contract].IPSService ips WITH(NOLOCK) ON ips.Id = td.IPSServiceId
                                     INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                                     INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
                                WHERE EntityState = @Deleted
                                      AND (rcd.[Status] = @Dos
                                           OR rcd.[Status] = @Tres) FOR XML PATH('')
                            );
                            SELECT '999' AS CodeMessage, 
                                   'Los siguientes servicios no se pueden eliminar porque sus folios están facturados o bloqueados: ' + CHAR(13) + CHAR(10) + @StringRevenueDetail AS Message, 
                                   0 ServiceOrderId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    --delete from Billing.ServiceOrderDetailDistribution where Id in (select sodd.Id from @TableDetail td inner join Billing.ServiceOrderDetailDistribution sodd with(nolock) on td.IdDetail = sodd.ServiceOrderDetailId where EntityState = 'Deleted')
                    --delete from Billing.ServiceOrderDetailSurgical where ServiceOrderDetailId in (select IdDetail from @TableDetail where EntityState = 'Deleted' and IdDetail not in (select idsub.ServiceOrderDetailId from Billing.InvoiceDetail idsub with(nolock) inner join Billing.Invoice isub with(nolock) on idsub.InvoiceId = isub.Id and isub.Status = 2 and isub.AdmissionNumber = @AdmissionNumber) and IdDetail not in (select IncludeServiceOrderDetailId from Billing.ServiceOrderDetail with(nolock) where IncludeServiceOrderDetailId is not null))
                    --delete from Billing.ServiceOrderDetail where Id in (select IdDetail from @TableDetail where EntityState = 'Deleted' and IdDetail not in (select idsub.ServiceOrderDetailId from Billing.InvoiceDetail idsub with(nolock) inner join Billing.Invoice isub with(nolock) on idsub.InvoiceId = isub.Id and isub.Status = 2 and isub.AdmissionNumber = @AdmissionNumber) and IdDetail not in (select IncludeServiceOrderDetailId from Billing.ServiceOrderDetail with(nolock) where IncludeServiceOrderDetailId is not null))
                    --update Billing.ServiceOrderDetail set IsDelete = 1 where Id in (select IdDetail from @TableDetail where EntityState = 'Deleted' and (IdDetail in (select idsub.ServiceOrderDetailId from Billing.InvoiceDetail idsub with(nolock) inner join Billing.Invoice isub with(nolock) on idsub.InvoiceId = isub.Id and isub.Status = 2 and isub.AdmissionNumber = @AdmissionNumber) or IdDetail in (select IncludeServiceOrderDetailId from Billing.ServiceOrderDetail with(nolock) where IncludeServiceOrderDetailId is not null)))
                    --delete from @TableDetail where EntityState = 'Deleted'

                    DELETE sodd
                    FROM Billing.ServiceOrderDetailDistribution sodd
                         INNER JOIN @TableDetail td ON sodd.ServiceOrderDetailId = td.IdDetail
                    WHERE EntityState = @Deleted;
                    DELETE sodd
                    FROM Billing.ServiceOrderDetailDistribution sodd
                         JOIN Billing.ServiceOrderDetail sod ON sodd.ServiceOrderDetailId = sod.Id
                         INNER JOIN @TableDetail td ON sod.Id = td.IdDetail
                    WHERE EntityState = @Deleted
                          AND NOT EXISTS
                    (
                        SELECT 1
                        FROM Billing.InvoiceDetail idsub WITH(NOLOCK)
                             INNER JOIN Billing.Invoice isub WITH(NOLOCK) ON idsub.InvoiceId = isub.Id
                                                                             AND isub.[Status] = @Dos
                                                                             AND isub.AdmissionNumber = @AdmissionNumber
                        WHERE td.IdDetail = idsub.ServiceOrderDetailId
                    )
                        AND NOT EXISTS
                    (
                        SELECT 1
                        FROM Billing.ServiceOrderDetail WITH(NOLOCK)
                        WHERE IncludeServiceOrderDetailId IS NOT NULL
                              AND td.IdDetail = IncludeServiceOrderDetailId
                    );
                    DELETE sods
                    FROM Billing.ServiceOrderDetailSurgical sods
                         INNER JOIN @TableDetail td ON sods.ServiceOrderDetailId = td.IdDetail
                    WHERE EntityState = @Deleted
                          AND NOT EXISTS
                    (
                        SELECT 1
                        FROM Billing.InvoiceDetail idsub WITH(NOLOCK)
                             INNER JOIN Billing.Invoice isub WITH(NOLOCK) ON idsub.InvoiceId = isub.Id
                                                                             AND isub.[Status] = @Dos
                                                                             AND isub.AdmissionNumber = @AdmissionNumber
                        WHERE td.IdDetail = idsub.ServiceOrderDetailId
                    )
                        AND NOT EXISTS
                    (
                        SELECT 1
                        FROM Billing.ServiceOrderDetail WITH(NOLOCK)
                        WHERE IncludeServiceOrderDetailId IS NOT NULL
                              AND td.IdDetail = IncludeServiceOrderDetailId
                    );
                    DELETE sod
                    FROM Billing.ServiceOrderDetail sod
                         INNER JOIN @TableDetail td ON sod.Id = td.IdDetail
                    WHERE EntityState = @Deleted
                          AND NOT EXISTS
                    (
                        SELECT 1
                        FROM Billing.InvoiceDetail idsub WITH(NOLOCK)
                             INNER JOIN Billing.Invoice isub WITH(NOLOCK) ON idsub.InvoiceId = isub.Id
                                                                             AND isub.[Status] = @Dos --and isub.AdmissionNumber = @AdmissionNumber
                        WHERE td.IdDetail = idsub.ServiceOrderDetailId
                    )
                        AND NOT EXISTS
                    (
                        SELECT 1
                        FROM Billing.ServiceOrderDetail WITH(NOLOCK)
                        WHERE IncludeServiceOrderDetailId IS NOT NULL
                              AND td.IdDetail = IncludeServiceOrderDetailId
                    );
                    UPDATE sod
                      SET 
                          IsDelete = 1
                    FROM Billing.ServiceOrderDetail sod
                         INNER JOIN @TableDetail td ON sod.Id = td.IdDetail
                    WHERE EntityState = @Deleted
                          AND (EXISTS
                    (
                        SELECT 1
                        FROM Billing.InvoiceDetail idsub WITH(NOLOCK)
                             INNER JOIN Billing.Invoice isub WITH(NOLOCK) ON idsub.InvoiceId = isub.Id
                                                                             AND isub.[Status] = @Dos --and isub.AdmissionNumber = @AdmissionNumber
                        WHERE td.IdDetail = idsub.ServiceOrderDetailId
                    )
                        OR EXISTS
                    (
                        SELECT 1
                        FROM Billing.ServiceOrderDetail WITH(NOLOCK)
                        WHERE IncludeServiceOrderDetailId IS NOT NULL
                              AND td.IdDetail = IncludeServiceOrderDetailId
                    ));

                    --Se elimina lo que haya en esta tabla
                    DELETE FROM @XmlAnnulmentRias;

                    --Se inserta en la tabla temporal los registros que sean para eliminar y que manejen rias
                    INSERT INTO @XmlAnnulmentRias
                    (CupsCode, 
                     RIASCupsId
                    )
                           SELECT ce.Code, 
                                  td.RIASCupsId
                           FROM @TableDetail td
                                INNER JOIN Contract.CUPSEntity ce ON ce.Id = td.CUPSEntityId
                           WHERE td.EntityState = @Deleted
                                 AND td.ApplyRIAS = 'True';

                    --Si hay datos para reversar
                    IF EXISTS
                    (
                        SELECT *
                        FROM @XmlAnnulmentRias
                    )
                        BEGIN
                            --Se genera el xml para reversar
                            SET @generateXmlAnnulment =
                            (
                                SELECT CupsCode, 
                                       RIASCupsId
                                FROM @XmlAnnulmentRias AS RIASForPatient FOR XML AUTO, ELEMENTS
                            );

                            --Se ejecuta el proceso de reversión
                            EXEC dbo.SP_RIASAnnulment 
                                 @generateXmlAnnulment, 
                                 @PatientCode, 
                                 @User, 
                                 @CodeMessageAnnulment OUTPUT, 
                                 @MessageAnnulment OUTPUT;

                            --Si hay error ejecutando el sp
                            IF @CodeMessageAnnulment = '999'
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           @MessageAnnulment AS Message, 
                                           0 ServiceOrderId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                    END;
                    DELETE FROM @TableDetail
                    WHERE EntityState = @Deleted;
            END;

            --- Valido que todos los detalles tengan asociado un Concepto de facturacion
            IF EXISTS
            (
                SELECT 1
                FROM @TableDetail td
                     LEFT JOIN [Contract].CUPSEntity ce WITH(NOLOCK) ON ce.Id = td.CUPSEntityId
                     LEFT JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, ce.BillingConceptId)
                WHERE bc.Id IS NULL
                      AND td.CUPSEntityId IS NOT NULL
                      AND td.CUPSEntityId <> 0
            )
                BEGIN
                    DECLARE @StringIPSService VARCHAR(MAX);
                    SET @StringIPSService =
                    (
                        SELECT ce.Code + ', '
                        FROM @TableDetail td
                             LEFT JOIN [Contract].CUPSEntity ce WITH(NOLOCK) ON ce.Id = td.CUPSEntityId
                             LEFT JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, ce.BillingConceptId)
                        WHERE bc.Id IS NULL FOR XML PATH('')
                    );
                    SELECT '999' AS CodeMessage, 
                           'Los siguientes CUPS no tienen asociado un concepto de facturacion: ' + @StringIPSService AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;

            --- Valido que no haya un concepto de tipo facturacion basica
            IF EXISTS
            (
                SELECT 1
                FROM @TableDetail td
                     INNER JOIN [Contract].CUPSEntity ce WITH(NOLOCK) ON ce.Id = td.CUPSEntityId
                     INNER JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, ce.BillingConceptId)
                WHERE bc.ConceptType = @Uno
                      AND td.CUPSEntityId IS NOT NULL
                      AND td.CUPSEntityId <> 0
            )
                BEGIN
                    DECLARE @StringCodeCUPS VARCHAR(MAX);
                    SET @StringCodeCUPS =
                    (
                        SELECT ce.Code + ', '
                        FROM @TableDetail td
                             INNER JOIN [Contract].CUPSEntity ce WITH(NOLOCK) ON ce.Id = td.CUPSEntityId
                             INNER JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, ce.BillingConceptId)
                        WHERE bc.ConceptType = @Uno FOR XML PATH('')
                    );
                    SELECT '999' AS CodeMessage, 
                           'Los siguientes CUPS tienen un concepto de facturacion tipo básica: ' + @StringCodeCUPS AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;

            --- Actualizo la cuenta contable de ingreso
            UPDATE td
              SET 
                  IncomeMainAccountId = Billing.fnGetIncomeMainAccount(td.RecordType, cg.CareGroupType, bc.Id, bc.AccountingType, bc.EntityIncomeAccountId, bc.IndividualIncomeAccountId, pg.IncomeAccountId, f.UnitType), 
                  BillingConceptId = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, td.BillingConceptId)
            FROM @TableDetail td
                 INNER JOIN [Contract].CareGroup cg WITH(NOLOCK) ON td.CareGroupId = cg.Id
                 INNER JOIN Payroll.FunctionalUnit f WITH(NOLOCK) ON f.Id = td.PerformsFunctionalUnitId
                 LEFT JOIN [Contract].CUPSEntity ce WITH(NOLOCK) ON ce.Id = td.CUPSEntityId
                 LEFT JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bc.Id = IIF(ISNULL(td.ApplyRIAS, '') = 'True', ce.RIASBillingConceptId, ce.BillingConceptId)
                 LEFT JOIN Inventory.InventoryProduct ipr WITH(NOLOCK) ON ipr.Id = td.ProductId
                 LEFT JOIN Inventory.ProductGroup pg WITH(NOLOCK) ON pg.Id = ipr.ProductGroupId;

            --- Valido que todos los detalles de los servicios Qx tengan asociado un Concepto de facturacion
            IF EXISTS
            (
                SELECT 1
                FROM @TableSurgical ts
                     INNER JOIN [Contract].IPSService ips WITH(NOLOCK) ON ts.IPSServiceId = ips.Id
                WHERE ips.ServiceClass <> @Uno
                      AND ips.BillingConceptId IS NULL
            )
                BEGIN
                    DECLARE @StringIPSServiceQx VARCHAR(MAX);
                    SET @StringIPSServiceQx =
                    (
                        SELECT ips.Code + ', '
                        FROM @TableSurgical ts
                             INNER JOIN Contract.IPSService ips WITH(NOLOCK) ON ts.IPSServiceId = ips.Id
                        WHERE ips.BillingConceptId IS NULL FOR XML PATH('')
                    );
                    SELECT '999' AS CodeMessage, 
                           'Los siguientes Servicios IPS no tienen asociado un concepto de facturacion: ' + @StringIPSServiceQx AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            UPDATE ts
              SET 
                  IncomeMainAccountId = Billing.fnGetIncomeMainAccount(td.RecordType, cg.CareGroupType, bc.Id, bc.AccountingType, bc.EntityIncomeAccountId, bc.IndividualIncomeAccountId, NULL, f.UnitType)
            FROM @TableSurgical ts
                 INNER JOIN @TableDetail td ON td.IdRow = ts.ServiceOrderDetailIdRow
                 INNER JOIN [Contract].CareGroup cg WITH(NOLOCK) ON td.CareGroupId = cg.Id
                 INNER JOIN Payroll.FunctionalUnit f WITH(NOLOCK) ON f.Id = td.PerformsFunctionalUnitId
                 INNER JOIN [Contract].IPSService ips WITH(NOLOCK) ON ips.Id = ts.IPSServiceId
                 INNER JOIN Billing.BillingConcept bc WITH(NOLOCK) ON bc.Id = ips.BillingConceptId;
            IF @IdServiceOrder = 0
                BEGIN
                    -- ******************* Inserto la cabecera de la orden *************
                    INSERT INTO [Billing].[ServiceOrder]
                    ([Code], 
                     [AdmissionNumber], 
                     [PatientCode], 
                     [OrderDate], 
                     [AffectInventory], 
                     [EntityCode], 
                     [EntityId], 
                     [EntityName], 
                     [OperatingUnitId], 
                     [Status], 
                     [CreationUser], 
                     [CreationDate]
                    )
                    VALUES
                    (@CodeServiceOrder, 
                     @AdmissionNumber, 
                     @PatientCode, 
                     @OrderDate, 
                     @AffectInventory, 
                     @EntityCode, 
                     @EntityId, 
                     @EntityName, 
                     @OperatingUnitId, 
                     @Status, 
                     @User, 
                     [Common].[GETDATE]()
                    );
                    SET @IdServiceOrder = SCOPE_IDENTITY();
                    UPDATE @TableDetail
                      SET 
                          ServiceOrderId = @IdServiceOrder;
            END;
                ELSE
                BEGIN
                    ---- Actualizo el estado de la orden de servicio
                    UPDATE Billing.ServiceOrder
                      SET 
                          [Status] = @Status, 
                          ModificationUser = @ModificationUser, 
                          ModificationDate = @ModificationDate, 
                          AnnulmentUser = @AnnulmentUser, 
                          AnnulmentDate = @AnnulmentDate
                    WHERE Id = @IdServiceOrder;
            END;
            --********************** CRUD los detalles ****************************
            -- No elimino ya que se hace en la parte de arriba
            UPDATE Billing.ServiceOrderDetail
              SET 
                  ServiceOrderId = td.ServiceOrderId, 
                  CareGroupId = td.CareGroupId, 
                  HealthAdministratorId = td.HealthAdministratorId, 
                  ThirdPartyId = td.ThirdPartyId, 
                  ServiceType = td.ServiceType, 
                  RecordType = td.RecordType, 
                  CUPSEntityId = td.CUPSEntityId, 
                  IPSServiceId = td.IPSServiceId, 
                  HospitalStayId = td.HospitalStayId, 
                  HospitalStayDetailId = td.HospitalStayDetailId, 
                  ControlExternalConsultation = td.ControlExternalConsultation, 
                  ControlExternalConsultationCode = td.ControlExternalConsultationCode, 
                  CUPSAssociateService = td.CUPSAssociateService, 
                  CodeAssociateService = td.CodeAssociateService, 
                  IsPackage = td.IsPackage, 
                  Packaging = td.Packaging, 
                  PackageServiceOrderDetailId = td.PackageServiceOrderDetailId, 
                  LiquidationType = td.LiquidationType, 
                  Presentation = td.Presentation, 
                  ProductId = td.ProductId, 
                  InvoicedQuantity = td.InvoicedQuantity, 
                  SupplyQuantity = td.SupplyQuantity, 
                  DevolutionQuantity = td.DevolutionQuantity, 
                  RateManualSalePrice = td.RateManualSalePrice, 
                  CostValue = td.CostValue, 
                  ServiceDate = td.ServiceDate, 
                  AuthorizationNumber = td.AuthorizationNumber, 
                  PerformsFunctionalUnitId = td.PerformsFunctionalUnitId, 
                  PerformsHealthProfessionalCode = td.PerformsHealthProfessionalCode, 
                  PerformsProfessionalSpecialty = td.PerformsProfessionalSpecialty, 
                  PerformsHealthProfessionalThirdPartyId = td.PerformsHealthProfessionalThirdPartyId, 
                  BillingConceptId = td.BillingConceptId, 
                  CostCenterId = td.CostCenterId, 
                  SettlementType = td.SettlementType, 
                  IncludeServiceOrderDetailId = td.IncludeServiceOrderDetailId, 
                  RecoveryRatio = td.RecoveryRatio, 
                  RateManualId = td.RateManualId, 
                  RateManualType = td.RateManualType, 
                  RateManualDetailId = td.RateManualDetailId, 
                  DefinitionRateDetailId = td.DefinitionRateDetailId, 
                  DefinitionRateDetailConditionId = td.DefinitionRateDetailConditionId, 
                  SubTotalSalesPrice = td.SubTotalSalesPrice, 
                  ThirdPartyDiscount = td.ThirdPartyDiscount, 
                  ThirdPartyDiscountPercentage = td.ThirdPartyDiscountPercentage, 
                  TotalSalesPrice = td.TotalSalesPrice, 
                  GrandTotalSalesPrice = td.GrandTotalSalesPrice, 
                  SurchargeApply = td.SurchargeApply, 
                  SurgicalInterventionType = td.SurgicalInterventionType, 
                  SurgeryNumber = td.SurgeryNumber, 
                  IsFirstEvent = td.IsFirstEvent, 
                  IsAnnulled = td.IsAnnulled, 
                  IsDelete = td.IsDelete, 
                  IncomeMainAccountId = td.IncomeMainAccountId, 
                  ApplyRIAS = CASE
                                  WHEN td.ApplyRIAS = '-'
                                  THEN NULL
                                  ELSE CAST(td.ApplyRIAS AS BIT)
                              END, 
                  RIASCupsId = CASE
                                   WHEN td.RIASCupsId = 0
                                   THEN NULL
                                   ELSE td.RIASCupsId
                               END
            FROM @TableDetail td
                 INNER JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = td.IdDetail
                                                                           AND td.EntityState = @Modified;
            ---- inserto los detail
            DECLARE @IdRowTmp INT;
            DECLARE @Rows INT, @RowId INT;
            SET @Rows = 1;
            SET @RowId = 1;
            WHILE @Rows > 0
                BEGIN
                    SELECT TOP 1 @RowId = RowId, 
                                 @IdRowTmp = IdRow
                    FROM @TableDetail
                    WHERE RowId >= @RowId
                          AND IdDetail = 0
                    ORDER BY RowId;
                    SET @Rows = @@ROWCOUNT;
                    IF @Rows = 0
                        BREAK;
                    INSERT INTO Billing.ServiceOrderDetail
                    (ServiceOrderId, 
                     CareGroupId, 
                     HealthAdministratorId, 
                     ThirdPartyId, 
                     ServiceType, 
                     RecordType, 
                     CUPSEntityId, 
                     IPSServiceId, 
                     HospitalStayId, 
                     HospitalStayDetailId, 
                     ControlExternalConsultation, 
                     ControlExternalConsultationCode, 
                     CUPSAssociateService, 
                     CodeAssociateService, 
                     IsPackage, 
                     Packaging, 
                     PackageServiceOrderDetailId, 
                     LiquidationType, 
                     Presentation, 
                     ProductId, 
                     InvoicedQuantity, 
                     SupplyQuantity, 
                     DevolutionQuantity, 
                     RateManualSalePrice, 
                     CostValue, 
                     ServiceDate, 
                     AuthorizationNumber, 
                     PerformsFunctionalUnitId, 
                     PerformsHealthProfessionalCode, 
                     PerformsProfessionalSpecialty, 
                     PerformsHealthProfessionalThirdPartyId, 
                     BillingConceptId, 
                     CostCenterId, 
                     SettlementType, 
                     IncludeServiceOrderDetailId, 
                     RecoveryRatio, 
                     RateManualId, 
                     RateManualType, 
                     RateManualDetailId, 
                     DefinitionRateDetailId, 
                     DefinitionRateDetailConditionId, 
                     SubTotalSalesPrice, 
                     ThirdPartyDiscount, 
                     ThirdPartyDiscountPercentage, 
                     TotalSalesPrice, 
                     GrandTotalSalesPrice, 
                     SurchargeApply, 
                     SurgicalInterventionType, 
                     SurgeryNumber, 
                     IsFirstEvent, 
                     IsAnnulled, 
                     IsDelete, 
                     IncomeMainAccountId, 
                     ApplyRIAS, 
                     RIASCupsId
                    )
                           SELECT ServiceOrderId, 
                                  CareGroupId, 
                                  HealthAdministratorId, 
                                  ThirdPartyId, 
                                  ServiceType, 
                                  RecordType, 
                                  CUPSEntityId, 
                                  IPSServiceId, 
                                  HospitalStayId, 
                                  HospitalStayDetailId, 
                                  ControlExternalConsultation, 
                                  ControlExternalConsultationCode, 
                                  CUPSAssociateService, 
                                  CodeAssociateService, 
                                  IsPackage, 
                                  Packaging, 
                                  PackageServiceOrderDetailId, 
                                  LiquidationType, 
                                  Presentation, 
                                  ProductId, 
                                  InvoicedQuantity, 
                                  SupplyQuantity, 
                                  DevolutionQuantity, 
                                  RateManualSalePrice, 
                                  CostValue, 
                                  ServiceDate, 
                                  AuthorizationNumber, 
                                  PerformsFunctionalUnitId, 
                                  PerformsHealthProfessionalCode, 
                                  PerformsProfessionalSpecialty, 
                                  PerformsHealthProfessionalThirdPartyId, 
                                  BillingConceptId, 
                                  CostCenterId, 
                                  SettlementType, 
                                  IncludeServiceOrderDetailId, 
                                  RecoveryRatio, 
                                  RateManualId, 
                                  RateManualType, 
                                  RateManualDetailId, 
                                  DefinitionRateDetailId, 
                                  DefinitionRateDetailConditionId, 
                                  SubTotalSalesPrice, 
                                  ThirdPartyDiscount, 
                                  ThirdPartyDiscountPercentage, 
                                  TotalSalesPrice, 
                                  GrandTotalSalesPrice, 
                                  SurchargeApply, 
                                  SurgicalInterventionType, 
                                  SurgeryNumber, 
                                  IsFirstEvent, 
                                  IsAnnulled, 
                                  IsDelete, 
                                  IncomeMainAccountId,
                                  CASE
                                      WHEN ApplyRIAS = '-'
                                      THEN NULL
                                      ELSE CAST(ApplyRIAS AS BIT)
                                  END,
                                  CASE
                                      WHEN RIASCupsId = 0
                                      THEN NULL
                                      ELSE RIASCupsId
                                  END
                           FROM @TableDetail
                           WHERE IdRow = @IdRowTmp;
                    UPDATE @TableDetail
                      SET 
                          IdDetail = SCOPE_IDENTITY()
                    WHERE IdRow = @IdRowTmp;
                    SET @RowId+=1;
                END;

            --Tabla para almacenar la info para el proceso RIAS
            DECLARE @XmlProcessRias TABLE
            (CupsCode               VARCHAR(20), 
             RIASCupsId             INT, 
             Quantity               INT, 
             ServiceOrderDetailId   INT, 
             ValueCups              DECIMAL(18, 2), 
             HealthProfessionalCode VARCHAR(20), 
             RealizationDate        DATETIME, 
             RealizedQuantity       INT
            );

            --Se insertan los datos a la tabla de xml para el proceso de rias
            INSERT INTO @XmlProcessRias
            (CupsCode, 
             RIASCupsId, 
             Quantity, 
             ServiceOrderDetailId, 
             ValueCups, 
             HealthProfessionalCode, 
             RealizationDate, 
             RealizedQuantity
            )
                   SELECT ce.Code, 
                          td.RIASCupsId, 
                          td.InvoicedQuantity, 
                          td.IdDetail, 
                          td.GrandTotalSalesPrice, 
                          td.PerformsHealthProfessionalCode, 
                          td.ServiceDate, 
                          RealizedQuantity
                   FROM @TableDetail td
                        INNER JOIN Contract.CUPSEntity ce ON ce.Id = td.CUPSEntityId
                   WHERE td.EntityState = @Added
                         AND td.ApplyRIAS = 'True';

            --Si hay datos en la tabla xml se consume el sp del proceso rias
            IF EXISTS
            (
                SELECT *
                FROM @XmlProcessRias
            )
                BEGIN
                    --Variable xml para enviar al sp
                    DECLARE @GenerateXml XML=
                    (
                        SELECT CupsCode, 
                               RIASCupsId, 
                               Quantity, 
                               ServiceOrderDetailId, 
                               ValueCups, 
                               HealthProfessionalCode, 
                               RealizationDate, 
                               RealizedQuantity
                        FROM @XmlProcessRias AS RIASForPatient FOR XML AUTO, ELEMENTS
                    );

                    --Variables en donde se almacena el resultado del proceso RIAS
                    DECLARE @CodeMessageProcessRias VARCHAR(20), @MessageProcessRias VARCHAR(MAX);

                    --Se ejecuta el sp de inscripción
                    EXEC dbo.SP_RIASProcess 
                         @GenerateXml, 
                         @PatientCode, 
                         @AdmissionNumber, 
                         @User, 
                         @CodeMessageProcessRias OUTPUT, 
                         @MessageProcessRias OUTPUT;

                    --Si hay error ejecutando el sp
                    IF @CodeMessageProcessRias = '999'
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   @MessageProcessRias AS Message, 
                                   0 ServiceOrderId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
            END;

            --DECLARE serviceDetail_cursor CURSOR FOR 
            --SELECT IdRow FROM @TableDetail WHERE IdDetail = 0
            --OPEN serviceDetail_cursor
            --FETCH NEXT FROM serviceDetail_cursor 
            --INTO @IdRowTmp
            --WHILE @@FETCH_STATUS = 0
            --BEGIN
            --	INSERT INTO Billing.ServiceOrderDetail(ServiceOrderId,CareGroupId,HealthAdministratorId,ThirdPartyId,ServiceType,RecordType,CUPSEntityId,IPSServiceId,HospitalStayId,HospitalStayDetailId,ControlExternalConsultation,ControlExternalConsultationCode,CUPSAssociateService,CodeAssociateService,IsPackage,Packaging,PackageServiceOrderDetailId,LiquidationType,Presentation,ProductId,InvoicedQuantity,SupplyQuantity,DevolutionQuantity,RateManualSalePrice,CostValue,ServiceDate,AuthorizationNumber,PerformsFunctionalUnitId,PerformsHealthProfessionalCode,PerformsProfessionalSpecialty,PerformsHealthProfessionalThirdPartyId,BillingConceptId,CostCenterId,SettlementType,IncludeServiceOrderDetailId,RecoveryRatio,RateManualId,RateManualType,RateManualDetailId,DefinitionRateDetailId,DefinitionRateDetailConditionId,SubTotalSalesPrice,ThirdPartyDiscount,ThirdPartyDiscountPercentage,TotalSalesPrice,GrandTotalSalesPrice,SurchargeApply,SurgicalInterventionType,SurgeryNumber,IsFirstEvent,IsAnnulled,IsDelete,IncomeMainAccountId)
            --	select ServiceOrderId,CareGroupId,HealthAdministratorId,ThirdPartyId,ServiceType,RecordType,CUPSEntityId,IPSServiceId,HospitalStayId,HospitalStayDetailId,ControlExternalConsultation,ControlExternalConsultationCode,CUPSAssociateService,CodeAssociateService,IsPackage,Packaging,PackageServiceOrderDetailId,LiquidationType,Presentation,ProductId,InvoicedQuantity,SupplyQuantity,DevolutionQuantity,RateManualSalePrice,CostValue,ServiceDate,AuthorizationNumber,PerformsFunctionalUnitId,PerformsHealthProfessionalCode,PerformsProfessionalSpecialty,PerformsHealthProfessionalThirdPartyId,BillingConceptId,CostCenterId,SettlementType,IncludeServiceOrderDetailId,RecoveryRatio,RateManualId,RateManualType,RateManualDetailId,DefinitionRateDetailId,DefinitionRateDetailConditionId,SubTotalSalesPrice,ThirdPartyDiscount,ThirdPartyDiscountPercentage,TotalSalesPrice,GrandTotalSalesPrice,SurchargeApply,SurgicalInterventionType,SurgeryNumber,IsFirstEvent,IsAnnulled,IsDelete,IncomeMainAccountId from @TableDetail where IdRow = @IdRowTmp
            --	update @TableDetail set IdDetail = SCOPE_IDENTITY() where IdRow = @IdRowTmp
            --	FETCH NEXT FROM serviceDetail_cursor
            --	INTO @IdRowTmp
            --END 
            --CLOSE serviceDetail_cursor
            --DEALLOCATE serviceDetail_cursor
            --- actualizo los items que fueron incluidos dentro de otro servicio que un estaba en memoria

            UPDATE sod
              SET 
                  IncludeServiceOrderDetailId =
            (
                SELECT IdDetail
                FROM @TableDetail
                WHERE IdRow = td.IncludeServiceOrderDetailIdRow
            )
            FROM @TableDetail td
                 INNER JOIN Billing.ServiceOrderDetail sod ON td.IdDetail = sod.Id
            WHERE td.IncludeServiceOrderDetailIdRow IS NOT NULL;

            --****************** CRUD los surgical *******************
            UPDATE ts
              SET 
                  ServiceOrderDetailId = td.IdDetail
            FROM @TableDetail td
                 INNER JOIN @TableSurgical ts ON td.IdRow = ts.ServiceOrderDetailIdRow;
            DELETE sods
            FROM [Billing].[ServiceOrderDetailSurgical] sods
                 INNER JOIN @TableSurgical ts ON sods.Id = ts.Id
            WHERE ts.EntityState = @Deleted;
            DELETE FROM @TableSurgical
            WHERE EntityState = @Deleted;
            UPDATE Billing.ServiceOrderDetailSurgical
              SET 
                  [ServiceOrderDetailId] = ts.ServiceOrderDetailId, 
                  [IPSServiceId] = ts.IPSServiceId, 
                  [InvoicedQuantity] = ts.InvoicedQuantity, 
                  [LiquidationPercentage] = ts.LiquidationPercentage, 
                  [RateManualSalePrice] = ts.RateManualSalePrice, 
                  [TotalSalesPrice] = ts.TotalSalesPrice, 
                  [PerformsHealthProfessionalCode] = ts.PerformsHealthProfessionalCode, 
                  [PerformsHealthProfessionalThirdPartyId] = ts.PerformsHealthProfessionalThirdPartyId, 
                  [CostValue] = ts.CostValue, 
                  [BillingConceptId] = ts.BillingConceptId, 
                  [CostCenterId] = ts.CostCenterId, 
                  [RateManualDetailSurgicalId] = ts.RateManualDetailSurgicalId, 
                  [SurchargeApply] = ts.SurchargeApply, 
                  [OnlyMedicalFees] = ts.OnlyMedicalFees, 
                  [IncomeMainAccountId] = ts.IncomeMainAccountId
            FROM @TableSurgical ts
                 INNER JOIN Billing.ServiceOrderDetailSurgical sods ON ts.Id = sods.Id;
            INSERT INTO [Billing].[ServiceOrderDetailSurgical]
            (ServiceOrderDetailId, 
             IPSServiceId, 
             InvoicedQuantity, 
             LiquidationPercentage, 
             RateManualSalePrice, 
             TotalSalesPrice, 
             PerformsHealthProfessionalCode, 
             PerformsHealthProfessionalThirdPartyId, 
             CostValue, 
             BillingConceptId, 
             CostCenterId, 
             RateManualDetailSurgicalId, 
             SurchargeApply, 
             OnlyMedicalFees, 
             IncomeMainAccountId
            )
                   SELECT ServiceOrderDetailId, 
                          IPSServiceId, 
                          InvoicedQuantity, 
                          LiquidationPercentage, 
                          RateManualSalePrice, 
                          TotalSalesPrice, 
                          PerformsHealthProfessionalCode, 
                          PerformsHealthProfessionalThirdPartyId, 
                          CostValue, 
                          BillingConceptId, 
                          CostCenterId, 
                          RateManualDetailSurgicalId, 
                          SurchargeApply, 
                          OnlyMedicalFees, 
                          IncomeMainAccountId
                   FROM @TableSurgical
                   WHERE Id = 0;

            /****** Verifico si se esta empaquetando para leer los detalles  *******/

            IF EXISTS
            (
                SELECT 1
                FROM @TableDetail
                WHERE IsPackage = @Uno
                      AND EntityState = @Added
            )
                BEGIN
                    --Obtengo el folio en donde estan los items que se van a empaquetar, esto para dejar el paquete en el mismo folio
                    SET @IdRevenueControlDetailPackage =
                    (
                        SELECT TOP 1 sodd.RevenueControlDetailId
                        FROM Billing.ServiceOrderDetail sod WITH(NOLOCK)
                             INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON sod.Id = sodd.ServiceOrderDetailId
                        WHERE sod.Id IN
                        (
                            SELECT t.x.value('ItemPackageId[1]', 'int')
                            FROM @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail/ServiceOrderDetailPackage') t(x)
                        )
                    );
                    DECLARE @IdServiceOrderPackage INT=
                    (
                        SELECT IdDetail
                        FROM @TableDetail
                        WHERE IsPackage = 1
                    );
                    UPDATE Billing.ServiceOrderDetail
                      SET 
                          Packaging = 1, 
                          PackageServiceOrderDetailId = @IdServiceOrderPackage
                    WHERE Id IN
                    (
                        SELECT t.x.value('ItemPackageId[1]', 'int')
                        FROM @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail/ServiceOrderDetailPackage') t(x)
                    );
                    DELETE FROM Billing.ServiceOrderDetailDistribution
                    WHERE ServiceOrderDetailId IN
                    (
                        SELECT t.x.value('ItemPackageId[1]', 'int')
                        FROM @ServiceOrderXml.nodes('/ServiceOrder/ServiceOrderDetail/ServiceOrderDetailPackage') t(x)
                    );
            END;

            /*** SI el estado es registrado ***/

            IF @Status = 1
                BEGIN

                    /******** INSERTO Y ACTUALIZO EN EL DISTRIBUTION ******/

                    DECLARE @RevenueControlId INT;
                    IF EXISTS
                    (
                        SELECT 1
                        FROM Billing.RevenueControl WITH(NOLOCK)
                        WHERE AdmissionNumber = @AdmissionNumber
                    )
                        BEGIN
                            SET @RevenueControlId =
                            (
                                SELECT Id
                                FROM Billing.RevenueControl WITH(NOLOCK)
                                WHERE AdmissionNumber = @AdmissionNumber
                            );
                    END;
                        ELSE
                        BEGIN
                            INSERT INTO [Billing].[RevenueControl]
                            ([AdmissionNumber], 
                             [PatientCode], 
                             [FolioQuantity], 
                             [TopEventFeeModerator], 
                             [TopEventCopay], 
                             [TopEventFeeRecovery]
                            )
                            VALUES
                            (@AdmissionNumber, 
                             @PatientCode, 
                             1, 
                             0, 
                             0, 
                             0
                            );
                            SET @RevenueControlId = SCOPE_IDENTITY();
                    END;
            END;
            IF EXISTS
            (
                SELECT 1
                FROM @TableDetail
                WHERE IsPackage = @Uno
                      AND EntityState <> @Added
                      AND EntityState <> @Unchanged
            )
                BEGIN
                    DECLARE @StringIPSServicePackage VARCHAR(MAX);
                    SET @StringIPSServicePackage =
                    (
                        SELECT ips.Code + ', '
                        FROM @TableDetail td
                             INNER JOIN Contract.IPSService ips WITH(NOLOCK) ON td.IPSServiceId = ips.Id
                        WHERE td.IsPackage = @Uno
                              AND td.EntityState <> @Added
                              AND td.EntityState <> @Unchanged FOR XML PATH('')
                    );
                    SELECT '999' AS CodeMessage, 
                           'Los siguientes Servicios IPS no se pueden modificar ya que estan empaquetados: ' + @StringIPSServicePackage AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            IF EXISTS
            (
                SELECT 1
                FROM @TableDetail td
                     INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                WHERE sodd.DistributionType <> @Uno
                      AND sodd.DistributionType <> @Cuatro
                      AND td.EntityState <> @Unchanged
            )
                BEGIN
                    DECLARE @StringIPSServiceDistributed VARCHAR(MAX);
                    SET @StringIPSServiceDistributed =
                    (
                        SELECT ips.Code + ', '
                        FROM @TableDetail td
                             INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                             INNER JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = td.IPSServiceId
                        WHERE sodd.DistributionType <> @Uno
                              AND sodd.DistributionType <> @Cuatro
                              AND td.EntityState <> @Unchanged FOR XML PATH('')
                    );
                    SELECT '999' AS CodeMessage, 
                           'Los siguientes Servicios IPS no se pueden modificar ya que estan distribuidos: ' + @StringIPSServiceDistributed AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            IF EXISTS
            (
                SELECT 1
                FROM @TableDetail td
                     INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                     INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
                WHERE td.EntityState <> @Unchanged
                      AND rcd.[Status] > @Uno
            )
                BEGIN
                    DECLARE @StringIPSServiceFolio VARCHAR(MAX);
                    SET @StringIPSServiceFolio =
                    (
                        SELECT 'Servicio IPS: ' + ips.Code + ' - Folio: ' + rcd.FolioOrder + CHAR(13) + CHAR(10)
                        FROM @TableDetail td
                             INNER JOIN Contract.IPSService ips WITH(NOLOCK) ON ips.Id = td.IPSServiceId
                             INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON td.IdDetail = sodd.ServiceOrderDetailId
                             INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
                        WHERE td.EntityState <> @Unchanged
                              AND rcd.[Status] > @Uno FOR XML PATH('')
                    );
                    SELECT '999' AS CodeMessage, 
                           'Los siguientes Servicios IPS no se pueden modificar ya sus folios estan facturados o bloqueados: ' + CHAR(13) + CHAR(10) + @StringIPSServiceFolio AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            ---- Se actualiza el distribution
            UPDATE sodd
              SET 
                  GrandTotalSalesPrice = td.GrandTotalSalesPrice, 
                  GrandTotalDiscount = td.ThirdPartyDiscount * td.InvoicedQuantity, 
                  ThirdPartySalesPrice = td.GrandTotalSalesPrice, 
                  SubTotalPatientSalesPrice = 0, 
                  PatientPercentage = 0, 
                  Quantity = td.InvoicedQuantity, 
                  ThirdPartyPercentage = 100, 
                  LastCaregroupId = td.CareGroupId
            FROM @TableDetail td
                 INNER JOIN Billing.ServiceOrderDetailDistribution sodd ON td.IdDetail = sodd.ServiceOrderDetailId
            WHERE td.EntityState <> @Unchanged;
            ---- Inserto los folios que tengo que crear
            INSERT INTO Billing.RevenueControlDetail
            (RevenueControlId, 
             BillingAuthorizationId, 
             FolioOrder, 
             FolioType, 
             LiquidationType, 
             ContractEntityId, 
             HealthAdministratorId, 
             ThirdPartyId, 
             CareGroupId, 
             TotalFolio, 
             [Status], 
             CreationUser, 
             CreationDate
            )
                   SELECT @RevenueControlId, 
                          NULL, 
                          (
                   (
                       SELECT ISNULL(MAX(FolioOrder), 0)
                       FROM Billing.RevenueControlDetail WITH(NOLOCK)
                       WHERE RevenueControlId = @RevenueControlId
                   ) + 1), 
                          cg.CareGroupType, 
                          cg.LiquidationType, 
                          c.ContractEntityId, 
                          td.HealthAdministratorId, 
                          td.ThirdPartyId, 
                          cg.Id, 
                          0, 
                          1, 
                          @User, 
                          [Common].[GETDATE]()
                   FROM @TableDetail td
                        INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON td.CareGroupId = cg.Id
                        LEFT JOIN Contract.[Contract] c WITH(NOLOCK) ON c.Id = cg.ContractId
                        LEFT JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.CareGroupId = td.CareGroupId
                                                                                   AND rcd.ThirdPartyId = td.ThirdPartyId
                                                                                   AND rcd.[Status] = @Uno
                                                                                   AND rcd.RevenueControlId = @RevenueControlId
                   WHERE rcd.Id IS NULL
                         AND td.EntityState = @Added
                   GROUP BY cg.CareGroupType, 
                            cg.LiquidationType, 
                            c.ContractEntityId, 
                            td.HealthAdministratorId, 
                            td.ThirdPartyId, 
                            cg.Id;

            ---- Se inserta los nuevos distribution
            IF EXISTS
            (
                SELECT 1
                FROM @TableDetail
                WHERE IsPackage = @Uno
            )
                BEGIN --- Si estan empaquetando
                    INSERT INTO Billing.ServiceOrderDetailDistribution
                    (RevenueControlDetailId, 
                     ServiceOrderDetailId, 
                     Quantity, 
                     GrandTotalSalesPrice, 
                     GrandTotalDiscount, 
                     DistributionType, 
                     ThirdPartySalesPrice, 
                     ThirdPartyPercentage, 
                     ApplyRecoveryFee, 
                     RecoveryFeeType, 
                     SubTotalPatientSalesPrice, 
                     PatientPercentage, 
                     LastCaregroupId
                    )
                           SELECT @IdRevenueControlDetailPackage, 
                                  td.IdDetail, 
                                  td.InvoicedQuantity, 
                                  td.GrandTotalSalesPrice, 
                                  td.ThirdPartyDiscount * td.InvoicedQuantity, 
                                  1, 
                                  td.GrandTotalSalesPrice, 
                                  100, 
                                  1, 
                                  1, 
                                  0, 
                                  0, 
                                  td.CareGroupId
                           FROM @TableDetail td
                           WHERE td.EntityState = @Added;
            END;
                ELSE
                BEGIN
                    INSERT INTO Billing.ServiceOrderDetailDistribution
                    (RevenueControlDetailId, 
                     ServiceOrderDetailId, 
                     Quantity, 
                     GrandTotalSalesPrice, 
                     GrandTotalDiscount, 
                     DistributionType, 
                     ThirdPartySalesPrice, 
                     ThirdPartyPercentage, 
                     ApplyRecoveryFee, 
                     RecoveryFeeType, 
                     SubTotalPatientSalesPrice, 
                     PatientPercentage, 
                     LastCaregroupId
                    )
                           SELECT
                           (
                               SELECT TOP 1 Id
                               FROM Billing.RevenueControlDetail rcd WITH(NOLOCK)
                               WHERE rcd.CareGroupId = td.CareGroupId
                                     AND rcd.ThirdPartyId = td.ThirdPartyId
                                     AND rcd.[Status] = @Uno
                                     AND rcd.RevenueControlId = @RevenueControlId
                           ), 
                           td.IdDetail, 
                           td.InvoicedQuantity, 
                           td.GrandTotalSalesPrice, 
                           td.ThirdPartyDiscount * td.InvoicedQuantity, 
                           1, 
                           td.GrandTotalSalesPrice, 
                           100, 
                           1, 
                           1, 
                           0, 
                           0, 
                           td.CareGroupId
                           FROM @TableDetail td
                           WHERE td.EntityState = @Added;
            END;
            ---- Actualizo la cantidad de folios que tiene el ingreso
            UPDATE Billing.RevenueControl
              SET 
                  FolioQuantity =
            (
                SELECT COUNT(*)
                FROM Billing.RevenueControlDetail WITH(NOLOCK)
                WHERE RevenueControlId = @RevenueControlId
            )
            WHERE Id = @RevenueControlId;
            --Validamos que no exista algun reconocimiento de ingreso sin reversar enlazado a algun folio vacio
            IF EXISTS
            (
                SELECT 1
                FROM Billing.RevenueControlDetail rcd
                     JOIN Billing.RevenueRecognitionDetail rrd ON rcd.Id = rrd.RevenueControlDetailId
                     JOIN Billing.RevenueRecognition rr ON rrd.RevenueRecognitionId = rr.Id
                WHERE rcd.RevenueControlId = @RevenueControlId
                      AND rr.State <> 2
                      AND NOT EXISTS
                (
                    SELECT 1
                    FROM Billing.ServiceOrderDetailDistribution sodd
                    WHERE sodd.RevenueControlDetailId = rcd.Id
                )
            )
                BEGIN
                    SELECT '999' AS CodeMessage, 
                           'Existen reconocimientos de Ingresos sin reversar' AS Message, 
                           0 ServiceOrderId, 
                           CAST(3 AS TINYINT) AS [Status];
                    RETURN;
            END;
            --- Eliminamos los reconocimientos de ingresos relacionados con los folios vacios
            DELETE rrd
            FROM Billing.RevenueControlDetail rcd
                 JOIN Billing.RevenueRecognitionDetail rrd ON rcd.Id = rrd.RevenueControlDetailId
            WHERE rcd.RevenueControlId = @RevenueControlId
                  AND NOT EXISTS
            (
                SELECT 1
                FROM Billing.ServiceOrderDetailDistribution sodd
                WHERE sodd.RevenueControlDetailId = rcd.Id
            );
            --- Validar que folios quedaron vacios para eliminarlos
            DELETE FROM Billing.RevenueControlDetail
            WHERE RevenueControlId = @RevenueControlId
                  AND
            (
                SELECT COUNT(*)
                FROM Billing.ServiceOrderDetailDistribution
                WHERE RevenueControlDetailId = Billing.RevenueControlDetail.Id
            ) = 0;
            INSERT INTO @TableRevenueControlDetailRefresh
                   SELECT DISTINCT 
                          sodd.RevenueControlDetailId
                   FROM Billing.ServiceOrder so WITH(NOLOCK)
                        INNER JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.ServiceOrderId = so.Id
                        INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
                   WHERE so.Id = @IdServiceOrder;

            ---Recalculo los folios que estan activos
            DECLARE @IdRevenueControlDetailTmp INT;
            SET @Rows = 1;
            SET @RowId = 1;
            WHILE @Rows > 0
                BEGIN
                    SELECT DISTINCT TOP 1 @RowId = RowId, 
                                          @IdRevenueControlDetailTmp = Id
                    FROM @TableRevenueControlDetailRefresh
                    WHERE RowId >= @RowId
                          AND Id IS NOT NULL
                    ORDER BY RowId;
                    SET @Rows = @@ROWCOUNT;
                    IF @Rows = 0
                        BREAK;
                    EXEC [Billing].[SP_UpdateRevenueControlDetailValuesNoSelect] 
                         @IdRevenueControlDetailTmp;
                    SET @RowId+=1;
                END;

            --DECLARE updateRevenueDetail_cursor CURSOR FOR 
            --select distinct Id from @TableRevenueControlDetailRefresh where Id is not null
            ----select distinct rcd.Id from @TableDetail td inner join Billing.RevenueControlDetail rcd on rcd.CareGroupId = td.CareGroupId and rcd.ThirdPartyId = td.ThirdPartyId and rcd.Status = 1 and rcd.RevenueControlId = @RevenueControlId where td.EntityState <> 'Unchanged'
            --OPEN updateRevenueDetail_cursor
            --FETCH NEXT FROM updateRevenueDetail_cursor 
            --INTO @IdRevenueControlDetailTmp
            --WHILE @@FETCH_STATUS = 0
            --BEGIN
            --	print 'Revenue'
            --	print @IdRevenueControlDetailTmp
            --	exec [Billing].[SP_UpdateRevenueControlDetailValuesNoSelect] @IdRevenueControlDetailTmp
            --	FETCH NEXT FROM updateRevenueDetail_cursor
            --	INTO @IdRevenueControlDetailTmp
            --END 
            --CLOSE updateRevenueDetail_cursor
            --DEALLOCATE updateRevenueDetail_cursor
            INSERT INTO @TableResult
            VALUES
            ('0', 
             'Se guardo correctamente la orden de servicio ' + @CodeServiceOrder, 
             @IdServiceOrder, 
             1
            );

            --- Realizamos las validaciones de los contratos
            SELECT CodeMessage, 
                   [Message], 
                   ServiceOrderId, 
                   [Status]
            FROM @TableResult;
        END TRY
        BEGIN CATCH
            SELECT '0' CodeMessage, 
                   ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)) Message, 
                   0 AS ServiceOrderId, 
                   CAST(3 AS TINYINT) AS [Status];
        END CATCH;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera órdenes de servicio de facturación a partir de un XML de entrada que describe los servicios prestados a un paciente durante su ingreso (admisión). Valida el estado del ingreso en ADINGRESO, verifica el contrato y grupo de atención (CareGroup/Contract) aplicables, y persiste el encabezado en ServiceOrder junto con el detalle de ítems facturados (procedimientos, medicamentos, estancias) en ServiceOrderDetail y su distribución financiera entre asegurador y paciente en ServiceOrderDetailDistribution. También procesa servicios RIAS con sus códigos CUPS, cantidades y valores, y gestiona intervenciones quirúrgicas con su liquidación porcentual, siendo el punto central del ciclo de facturación de servicios de salud en la plataforma.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateServiceOrder_Test';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateServiceOrder_Test';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Crear, modificar o anular órdenes de servicio de facturación con sus detalles y detalles quirúrgicos a partir de un XML, gestionando folios de control de ingresos, distribución financiera, integración RIAS y validaciones de contratos, estancias y facturas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder_Test';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe traer el nodo /ServiceOrder con Id, Code, AdmissionNumber, PatientCode, OrderDate, EntityId, EntityName, OperatingUnitId y Status; los detalles vienen en /ServiceOrder/ServiceOrderDetail.; La admisión (dbo.ADINGRESO.NUMINGRES = @AdmissionNumber) debe existir y no estar en estado ''F'' (facturada).; La fecha de la orden (@OrderDate) debe ser >= fecha de admisión (IFECHAING).; Para crear una orden nueva (Id=0 sin Code) debe existir secuencia parametrizada en Billing.BillingSequence con IdForm=''755'' y su BillingSequenceDetail correspondiente al OperatingUnit cuando Scope=''OU''.; Los CareGroup referenciados deben pertenecer a contratos cuyo Status no sea 2 (suspendido) ni 3 (terminado).; Cada CUPSEntity de los detalles debe tener BillingConceptId (o RIASBillingConceptId si ApplyRIAS=''True'') asociado y dicho concepto no puede ser de tipo básico (ConceptType=1).; Los IPSService de detalles quirúrgicos (ServiceClass<>1) deben tener BillingConceptId asociado.; Si la orden contiene un ítem con IsPackage=1, no puede contener más servicios adicionales en la misma orden.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder_Test';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateServiceOrder_Test';
-- GO
