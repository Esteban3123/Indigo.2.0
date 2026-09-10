
-- =============================================
-- Author:        Carlos Mario Arias Rubiano
-- Create date:   14/09/2018
-- Description:   Sp que se encarga de guardar la integración con medilaser de dispensación por paciente
--
-- Modified by:   Daniel Eduardo Arévalo
-- Modified Date: 28-07-2025
-- Description:   Se cambia la forma de obtener el consecutivo para que no utilice más la tabla de INCONSECU sino utilicé autonumérico
--
-- Cambio solicitado (28-07-2025): Migración de variables de tabla a tablas temporales (#temp tables) + índices
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SaveDispensingByPatientMedilaser]
    @XmlPharmaceutical xml,
    @XmlAnnulateDashboard xml,
    @XmlPrescription xml,
    @CodeUser varchar(20),
    @OperatingUnitId int
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ContainerCrystal VARCHAR(10);
    DECLARE @SkipAccountControlValidations BIT = 0;

    -- Consecutivo de ingreso (cambiado a varchar(10) en versión previa)
    DECLARE @CONNUMACT varchar(10);

    DECLARE @FunctionalUnitCode varchar(20);
    DECLARE @FunctionalUnitId int;
    DECLARE @HealthAdministratorId int;
    DECLARE @ThirdPartyIdHealthAdministrator int;
    DECLARE @ThirdPartyIdPatient int;
    DECLARE @MedicalFormulaId int;
    DECLARE @MedicalFormulaDetailId int;
    DECLARE @DispensingId int;
    DECLARE @DispensingCode varchar(20);
    DECLARE @RevenueControlDetailId int;

    -- Mensajería y resultados
    DECLARE @Validate bit = 0;
    DECLARE @MessageReturn varchar(max);
    DECLARE @CodeMessageResult varchar(20), @ErrorsValidationResult varchar(max), @DispensingIdResult int, @DispensingCodeResult varchar(20), @StatusResult tinyint;
    DECLARE @ResultStatus bit, @ResultMessageInvoice varchar(Max), @ResultMessage varchar(Max), @ResultInvoiceId int, @ResultInvoiceNumber varchar(20), @ResultXml xml;

    -- Variables de cabecera
    DECLARE @UFUCODIGO varchar(20), @UFUDESCRI varchar(60), @CODCENATE varchar(20), @NUMINGRES varchar(20), @IPCODPACI varchar(15), @IPTIPODOC int, @CODIGONIT varchar(15),
            @IPEXPEDIC varchar(40), @IPPRIAPEL varchar(20), @IPSEGAPEL varchar(20), @IPPRINOMB varchar(20), @IPSEGNOMB varchar(20), @IPNOMCOMP varchar(250),
            @CODEMPRES varchar(5), @IPTIPOPAC int, @IPTIPOAFI int, @CAPACIPAG int, @AUUBICACI varchar(20), @NIVCODIGO varchar(2),
            @IPDIRECCI varchar(max), @IPTELEFON varchar(max), @IPTELMOVI varchar(max), @IPFECNACI varchar(50), @CODACTIVI varchar(4), @IPSEXOPAC int, @IPESTADOC int, @TIPCOBSAL varchar(1),
            @ESTADOPAC bit, @INDAUDFOR numeric, @NUMCARPET varchar(15), @CODUSUCRE varchar(20), @FECREGCRE varchar(50), @GENCAREGROUP int, @GENCONENTITY int,
            @CareCenterCode varchar(20), @WareHouseId int, @CareGroupId int, @BillingAuthorizationId int, @Date varchar(50), @Number varchar(20), @IsManual bit, @IPSCode varchar(200),
            @FilePath VARCHAR(MAX), @NumingressDiagnostic varchar(4);

    DECLARE @PerformsHealthProfessionalThirdPartyId INT,
            @DateFormulation DATETIME;

    -------------------------------------------------------------------------
    -- Tablas temporales (reemplazo de variables de tabla) + ÍNDICES
    -------------------------------------------------------------------------
    -- #TableXml
    IF OBJECT_ID('tempdb..#TableXml') IS NOT NULL DROP TABLE #TableXml;
    CREATE TABLE #TableXml (
        AdmissionNumber varchar(20),
        DispensingIntegration tinyint,
        HealthAdministratorId int,
        CareGroupId int,
        FunctionalUnitId int
    );
    -- Índices: no requeridos; tabla pequeña solo para XML opcional

    -- #DispensingDetail
    IF OBJECT_ID('tempdb..#DispensingDetail') IS NOT NULL DROP TABLE #DispensingDetail;
    CREATE TABLE #DispensingDetail (
        AdmissionNumber varchar(20),
        EntityId int,
        EntityCode varchar(20),
        ContractCode varchar(20),
        PlanCode varchar(20),
        ProductType tinyint,
        ProductCode varchar(20) NOT NULL,
        ProductName varchar(100),
        RequestQuantity int,
        DeliveryQuantity int,
        PendingQuantity int,
        NoPos bit,
        MeasurementUnitCode varchar(20),
        HealthProfessionalCode varchar(20),
        HealthProfessionalName varchar(100),
        HealthProfessionalNit varchar(20),
        IdeTipHis varchar(10),
        NumFolio varchar(10),
        PatientCode varchar(20),
        SpecialtyCode varchar(20),
        SpecialtyName varchar(100),
        IsDeferred bit,
        TreatmentDays int,
        DiagnosticCode varchar(4),
        AuthorizationNumber varchar(20),
        IDMipres varchar(22)
    );
    -- Índices sugeridos
    CREATE CLUSTERED INDEX CIX_TableDetail_ProductCode ON #DispensingDetail(ProductCode);
    CREATE NONCLUSTERED INDEX IX_TableDetail_HealthProfessionalNit ON #DispensingDetail(HealthProfessionalNit);
    CREATE NONCLUSTERED INDEX IX_TableDetail_ProductCode_Cover
        ON #DispensingDetail(ProductCode)
        INCLUDE (ProductName, DeliveryQuantity, PendingQuantity);

    -- #TableDeferred
    IF OBJECT_ID('tempdb..#TableDeferred') IS NOT NULL DROP TABLE #TableDeferred;
    CREATE TABLE #TableDeferred (
        Id int,
        FirstDeliveryDate varchar(50),
        DeliveryQuantityDeferred int,
        Periodicity int,
        ProductCode varchar(20),
        Number int,
        DeliveryDate varchar(50),
        DeliveryQuantity int,
        PendingQuantity int
    );
    CREATE NONCLUSTERED INDEX IX_TableDeferred_ProductCode ON #TableDeferred(ProductCode);
    CREATE NONCLUSTERED INDEX IX_TableDeferred_Id ON #TableDeferred(Id);

    -- #RevenueControlDetailCrossingList
    IF OBJECT_ID('tempdb..#RevenueControlDetailCrossingList') IS NOT NULL DROP TABLE #RevenueControlDetailCrossingList;
    CREATE TABLE #RevenueControlDetailCrossingList (
        FolioOrder tinyint,
        FolioType tinyint,
        CareGroupId int,
        RevenueControlDetailId int,
        TotalPatientDiscount numeric,
        RevenueControlId int,
        OutputDate varchar(20),
        IsCutAccount bit,
        OutputDiagnosis varchar(100),
        InitialDate varchar(20),
        CutType int
    );
    -- Índices: no imprescindibles, tabla pequeña para generar XML de liquidación

    -- #TableDetailProducts
    IF OBJECT_ID('tempdb..#TableDetailProducts') IS NOT NULL DROP TABLE #TableDetailProducts;
    CREATE TABLE #TableDetailProducts (
        ProductCode varchar(20) NOT NULL,
        MedicalFormulaDetailId int NULL,
        ProductId int,
        BatchSerialId int,
        WarehouseId int,
        Quantity int,
        CreationUser varchar(20),
        CreationDate datetime NULL
    );
    CREATE NONCLUSTERED INDEX IX_TableDetailProducts_ProductCode ON #TableDetailProducts(ProductCode);
    CREATE NONCLUSTERED INDEX IX_TableDetailProducts_ProductId ON #TableDetailProducts(ProductId);

    -- #TempIdAndProductCode
    IF OBJECT_ID('tempdb..#TempIdAndProductCode') IS NOT NULL DROP TABLE #TempIdAndProductCode;
    CREATE TABLE #TempIdAndProductCode (
        Id int NOT NULL PRIMARY KEY,
        ProductCode varchar(20)
    );
    CREATE NONCLUSTERED INDEX IX_TempIdAndProductCode_ProductCode ON #TempIdAndProductCode(ProductCode);

    BEGIN TRY
        -- Validaciones de unidad operativa y parámetros
        IF @OperatingUnitId > 0
        BEGIN
            IF (SELECT COUNT(*) FROM Billing.SettingsBilling WHERE IdOperatingUnit = @OperatingUnitId) = 0
            BEGIN
                SELECT 999 AS Status, 'No existen parámetros de facturación para la unidad operativa escogida' AS Message, 0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
                RETURN;
            END
        END

        IF @OperatingUnitId > 0
        BEGIN
            IF (SELECT COUNT(*) FROM Billing.SettingsBilling WHERE IdOperatingUnit = @OperatingUnitId AND (FunctionalUnitId IS NULL OR HealthProfessionalCode IS NULL)) > 0
            BEGIN
                SELECT 999 AS Status, 'Debe diligenciar el segmento dispensación por paciente del formulario parámetros de facturación' AS Message, 0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
                RETURN;
            END

            SELECT @FunctionalUnitCode = f.Code, @FunctionalUnitId = f.Id
            FROM Billing.SettingsBilling s
            INNER JOIN Payroll.FunctionalUnit f ON f.Id = s.FunctionalUnitId
            WHERE s.IdOperatingUnit = @OperatingUnitId;
        END

        -- Cabecera desde XML
        SELECT
            @UFUCODIGO = t.x.value('UFUCODIGO[1]','varchar(20)'),
            @UFUDESCRI = t.x.value('UFUDESCRI[1]','varchar(60)'),
            @CODCENATE = t.x.value('CODCENATE[1]','varchar(20)'),
            @NUMINGRES = t.x.value('NUMINGRES[1]','varchar(20)'),
            @IPCODPACI = t.x.value('IPCODPACI[1]','varchar(15)'),
            @IPTIPODOC = t.x.value('IPTIPODOC[1]','int'),
            @CODIGONIT = t.x.value('CODIGONIT[1]','varchar(15)'),
            @IPEXPEDIC = t.x.value('IPEXPEDIC[1]','varchar(40)'),
            @IPPRIAPEL = t.x.value('IPPRIAPEL[1]','varchar(20)'),
            @IPSEGAPEL = t.x.value('IPSEGAPEL[1]','varchar(20)'),
            @IPPRINOMB = t.x.value('IPPRINOMB[1]','varchar(20)'),
            @IPSEGNOMB = t.x.value('IPSEGNOMB[1]','varchar(20)'),
            @IPNOMCOMP = t.x.value('IPNOMCOMP[1]','varchar(250)'),
            @CODEMPRES = t.x.value('CODEMPRES[1]','varchar(5)'),
            @IPTIPOPAC = t.x.value('IPTIPOPAC[1]','int'),
            @IPTIPOAFI = t.x.value('IPTIPOAFI[1]','int'),
            @CAPACIPAG = t.x.value('CAPACIPAG[1]','int'),
            @AUUBICACI = t.x.value('AUUBICACI[1]','varchar(20)'),
            @NIVCODIGO = t.x.value('NIVCODIGO[1]','varchar(2)'),
            @IPDIRECCI = t.x.value('IPDIRECCI[1]','varchar(max)'),
            @IPTELEFON = t.x.value('IPTELEFON[1]','varchar(max)'),
            @IPTELMOVI = t.x.value('IPTELMOVI[1]','varchar(max)'),
            @IPFECNACI = t.x.value('IPFECNACI[1]','varchar(50)'),
            @CODACTIVI = t.x.value('CODACTIVI[1]','varchar(4)'),
            @IPSEXOPAC = t.x.value('IPSEXOPAC[1]','int'),
            @IPESTADOC = t.x.value('IPESTADOC[1]','int'),
            @TIPCOBSAL = t.x.value('TIPCOBSAL[1]','varchar(1)'),
            @ESTADOPAC = t.x.value('ESTADOPAC[1]','bit'),
            @INDAUDFOR = t.x.value('INDAUDFOR[1]','numeric'),
            @NUMCARPET = t.x.value('NUMCARPET[1]','varchar(15)'),
            @CODUSUCRE = t.x.value('CODUSUCRE[1]','varchar(20)'),
            @IPSCode = t.x.value('IPSCode[1]','varchar(200)'),
            @FECREGCRE = t.x.value('FECREGCRE[1]','varchar(50)'),
            @GENCAREGROUP = t.x.value('GENCAREGROUP[1]','int'),
            @GENCONENTITY = t.x.value('GENCONENTITY[1]','int'),
            @CareCenterCode = t.x.value('CareCenterCode[1]','varchar(20)'),
            @WareHouseId = t.x.value('WareHouseId[1]','int'),
            @CareGroupId = t.x.value('CareGroupId[1]','int'),
            @BillingAuthorizationId = t.x.value('BillingAuthorizationId[1]','int'),
            @Date = t.x.value('Date[1]','datetime'),
            @Number = t.x.value('Number[1]','varchar(20)'),
            @IsManual = t.x.value('IsManual[1]','bit'),
            @FilePath = t.x.value('FilePath[1]', 'VARCHAR(MAX)'),
            @PerformsHealthProfessionalThirdPartyId = IIF(t.x.value('PerformsHealthProfessionalThirdPartyId[1]','int') = 0, NULL, t.x.value('PerformsHealthProfessionalThirdPartyId[1]','int')),
            @DateFormulation = t.x.value('DateFormulation[1]','datetime')
        FROM @XmlPrescription.nodes('/PrescriptionHeader') t(x);

        -- Detalle
        INSERT INTO #DispensingDetail (AdmissionNumber, EntityId, EntityCode, ContractCode, PlanCode, ProductType, ProductCode, ProductName, RequestQuantity, DeliveryQuantity,
                                  PendingQuantity, NoPos, MeasurementUnitCode, HealthProfessionalCode, HealthProfessionalName, HealthProfessionalNit, IdeTipHis, NumFolio,
                                  PatientCode, SpecialtyCode, SpecialtyName, IsDeferred, TreatmentDays, DiagnosticCode, AuthorizationNumber, IDMipres)
        SELECT 
            t.x.value('AdmissionNumber[1]','varchar(20)'),
            t.x.value('EntityId[1]','int'),
            t.x.value('EntityCode[1]','varchar(20)'),
            t.x.value('ContractCode[1]','varchar(20)'),
            t.x.value('PlanCode[1]','varchar(20)'),
            t.x.value('ProductType[1]','tinyint'),
            t.x.value('ProductCode[1]','varchar(20)'),            
            t.x.value('ProductName[1]','varchar(100)'),
            t.x.value('RequestQuantity[1]','int'),
            t.x.value('DeliveryQuantity[1]','int'),
            t.x.value('PendingQuantity[1]','int'),
            t.x.value('NoPos[1]','bit'),
            t.x.value('MeasurementUnitCode[1]','varchar(20)'),
            t.x.value('HealthProfessionalCode[1]','varchar(20)'),
            t.x.value('HealthProfessionalName[1]','varchar(100)'),
            t.x.value('HealthProfessionalNit[1]','varchar(20)'),
            t.x.value('IdeTipHis[1]','varchar(10)'),
            t.x.value('NumFolio[1]','varchar(10)'),
            t.x.value('PatientCode[1]','varchar(20)'),
            t.x.value('SpecialtyCode[1]','varchar(20)'),
            t.x.value('SpecialtyName[1]','varchar(100)'),
            t.x.value('IsDeferred[1]','bit'),
            t.x.value('TreatmentDays[1]','int'),
            t.x.value('DiagnosticCode[1]','varchar(4)'),
            t.x.value('AuthorizationNumber[1]','varchar(20)'),
            t.x.value('IDMipres[1]','varchar(22)')
        FROM @XmlPrescription.nodes('/PrescriptionHeader/PrescriptionDetail') t(x);

        -- Diferidos
        INSERT INTO #TableDeferred (Id, FirstDeliveryDate, DeliveryQuantityDeferred, Periodicity, ProductCode, Number, DeliveryDate, DeliveryQuantity, PendingQuantity)
        SELECT 
            t.x.value('Id[1]','int'),
            t.x.value('FirstDeliveryDate[1]','varchar(50)'),
            t.x.value('DeliveryQuantityDeferred[1]','int'),
            t.x.value('Periodicity[1]','int'),
            t.x.value('ProductCode[1]','varchar(20)'),
            t.x.value('Number[1]','int'),
            t.x.value('DeliveryDate[1]','varchar(50)'),
            t.x.value('DeliveryQuantity[1]','int'),
            t.x.value('PendingQuantity[1]','int')
        FROM @XmlPrescription.nodes('/PrescriptionHeader/PrescriptionDetail/PrescriptionDetailDeferred') t(x);

        -- Detalle de productos (lotes / series)
        INSERT INTO #TableDetailProducts (ProductCode, ProductId, BatchSerialId, WarehouseId, Quantity, CreationUser)
        SELECT 
            t.x.value('ProductCode[1]','varchar(20)'),
            t.x.value('ProductId[1]','int'),
            NULLIF(t.x.value('BatchSerialId[1]','int'), 0),
            t.x.value('WarehouseId[1]','int'),
            t.x.value('Quantity[1]','int'),
            t.x.value('CreationUser[1]','varchar(20)')
        FROM @XmlPrescription.nodes('/PrescriptionHeader/PrescriptionDetail/PrescriptionDetailBatchSerial') t(x);

        -- Entidad administradora y tercero
        SELECT @HealthAdministratorId = c.HealthAdministratorId, @ThirdPartyIdHealthAdministrator = ha.ThirdPartyId
        FROM Contract.CareGroup cg 
        INNER JOIN Contract.Contract c ON c.Id = cg.ContractId
        INNER JOIN Contract.HealthAdministrator ha ON ha.Id = c.HealthAdministratorId
        WHERE cg.Id = @CareGroupId;
        
        -- Validaciones de existencia de fórmula
        IF @IsManual = 0
        BEGIN 
            IF (SELECT COUNT(*) FROM Inventory.MedicalFormula WHERE Number = @Number) > 0
                SET @Validate = 1;
        END
        ELSE BEGIN
            IF (SELECT COUNT(*) FROM Inventory.MedicalFormula WHERE Number = @Number AND PatientCode = @IPCODPACI) > 0
                SET @Validate = 1;
        END
        
        -- Creación de paciente si aplica
        IF @Validate = 0 AND @IsManual = 0
        BEGIN
            IF NOT EXISTS(SELECT 1 FROM dbo.INPACIENT WHERE IPCODPACI = @IPCODPACI AND IPTIPODOC = @IPTIPODOC) 
            BEGIN
                SELECT TOP 1 @CODEMPRES = CODEMPRES FROM dbo.ADEMPRESA;
                SELECT TOP 1 @AUUBICACI = u.AUUBICACI FROM dbo.INUBICACI u;
                SELECT TOP 1 @CODACTIVI = codactivi FROM dbo.ADACTIVID;
                SELECT TOP 1 @NIVCODIGO = NIVCODIGO FROM dbo.ADNIVELES;
                SELECT TOP 1 @CODUSUCRE = CODUSUARI FROM dbo.SEGusuaru;

                INSERT INTO dbo.INPACIENT
                (IPCODPACI, IPTIPODOC, CODIGONIT, IPEXPEDIC, IPPRIAPEL, IPSEGAPEL, IPPRINOMB, IPSEGNOMB, IPNOMCOMP, CODEMPRES, IPTIPOPAC, IPTIPOAFI, CAPACIPAG, AUUBICACI, NIVCODIGO, 
                 IPDIRECCI, IPTELEFON, IPTELMOVI, IPFECNACI, CODACTIVI, IPSEXOPAC, IPESTADOC, TIPCOBSAL, ESTADOPAC, INDAUDFOR, NUMCARPET, CODUSUCRE, FECREGCRE, GENCAREGROUP, GENCONENTITY)            
                VALUES(@IPCODPACI, @IPTIPODOC, @CODIGONIT, @IPEXPEDIC, @IPPRIAPEL, @IPSEGAPEL, @IPPRINOMB, @IPSEGNOMB, @IPNOMCOMP, @CODEMPRES, @IPTIPOPAC, @IPTIPOAFI, @CAPACIPAG, @AUUBICACI, 
                       @NIVCODIGO, @IPDIRECCI, @IPTELEFON, @IPTELMOVI, @IPFECNACI, @CODACTIVI, @IPSEXOPAC, @IPESTADOC, @TIPCOBSAL, @ESTADOPAC, @INDAUDFOR, @NUMCARPET, @CODUSUCRE, 
                       @FECREGCRE, @GENCAREGROUP, @GENCONENTITY);            
            END
        END
        
        -- Dispensación
        IF @XmlPharmaceutical.exist('*') > 0
        BEGIN
            IF OBJECT_ID('tempdb..#PharmaceuticalDetail') IS NOT NULL DROP TABLE #PharmaceuticalDetail;
            CREATE TABLE #PharmaceuticalDetail (
                ProductId int,
                ProductCode varchar(20)
            );
            CREATE NONCLUSTERED INDEX IX_PharmaceuticalDetail_ProductCode ON #PharmaceuticalDetail(ProductCode);
            CREATE NONCLUSTERED INDEX IX_PharmaceuticalDetail_ProductId ON #PharmaceuticalDetail(ProductId);

            INSERT INTO #PharmaceuticalDetail (ProductId, ProductCode)
            SELECT  t.x.value('ProductId[1]','int'),
                    t.x.value('MedicamentCode[1]','varchar(20)')
            FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing/PharmaceuticalDispensingDetail') t(x);

            IF EXISTS
            (
                SELECT 1
                FROM #PharmaceuticalDetail pd
                LEFT JOIN Inventory.InventoryProduct ip ON pd.ProductId = ip.Id
                LEFT JOIN Inventory.ATC atc ON ip.ATCId = atc.Id
                LEFT JOIN Inventory.InventorySupplie ins ON ip.SupplieId = ins.Id
                LEFT JOIN #DispensingDetail d ON pd.ProductCode = d.ProductCode
                WHERE d.ProductCode IS NULL
                    OR NOT 
                    (
                        pd.ProductCode = ISNULL(ip.Code, '') 
                        OR
                        pd.ProductCode = ISNULL(atc.Code, '')
                        OR
                        pd.ProductCode = ISNULL(ins.Code, '')
                    )
            )
            BEGIN
                SELECT @ErrorsValidationResult = STUFF((
                    SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Medicamento: ', pd.ProductCode, ' -  Producto: ', ip.Code, ' - ATC / Insumo: ', ISNULL(atc.Code, ins.Code))
                    FROM #PharmaceuticalDetail pd
                    LEFT JOIN Inventory.InventoryProduct ip ON pd.ProductId = ip.Id
                    LEFT JOIN Inventory.ATC atc ON ip.ATCId = atc.Id
                    LEFT JOIN Inventory.InventorySupplie ins ON ip.SupplieId = ins.Id
                    LEFT JOIN #DispensingDetail d ON pd.ProductCode = d.ProductCode
                    WHERE d.ProductCode IS NULL
                        OR NOT 
                        (
                            pd.ProductCode = ISNULL(ip.Code, '') 
                            OR 
                            pd.ProductCode = ISNULL(atc.Code, '')
                            OR 
                            pd.ProductCode = ISNULL(ins.Code, '')
                        )
                    FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'');

                SELECT  999 AS Status, 
                        'Los siguientes codigos de medicamentos / insumos no corresponden con los productos: ' + CHAR(13) + CHAR(10) + ISNULL(@ErrorsValidationResult, '') AS Message, 
                        0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
                RETURN;
            END

            -- Consecutivo (lógica actual)
            SET @CONNUMACT = CONVERT(varchar(10), RIGHT(NEWID(),10));

            -- Código de entidad
            DECLARE @CODENTIDA varchar(20) = (SELECT TOP 1 CODENTIDA FROM .INENTIDAD);

            -- Ingreso ambulatorio
            INSERT INTO dbo.ADINGRESO(NUMINGRES, IPCODPACI, TIPOINGRE, IINGREPOR, ITIPORIES, ICAUSAING, CODENTIDA, IFECHAING, ILIQUIDAC,
                                      ICONTROLI, CODCENATE, UFUCODIGO, IESTADOIN, IREINGRES, UFUACTPAC, GENCAREGROUP, GENCONENTITY, CODUSUCRE, FECREGCRE, INDAUDFOR)
            VALUES(@CONNUMACT, @IPCODPACI, 1, 2, 1, 3, @CODENTIDA, @Date, 1, '', @CareCenterCode, @FunctionalUnitCode, '', 0, @FunctionalUnitCode, @CareGroupId, @HealthAdministratorId, @CodeUser, Common.GETDATE(), 0);

            -- XML opcional
            INSERT INTO #TableXml(AdmissionNumber, DispensingIntegration, HealthAdministratorId, CareGroupId, FunctionalUnitId) 
            VALUES(@CONNUMACT, 3, @HealthAdministratorId, @CareGroupId, @FunctionalUnitId);

            DECLARE @xml xml = (
                        SELECT AdmissionNumber, DispensingIntegration, HealthAdministratorId, CareGroupId, FunctionalUnitId FROM #TableXml AS TableXml
                        FOR XML AUTO, ELEMENTS);
        
            -- SP de dispensación
            EXEC Inventory.SP_GeneratePharmaceuticalDispensing_Output @XmlPharmaceutical, @XmlAnnulateDashboard, @CodeUser, @xml,
                 @CodeMessageResult OUTPUT, @ErrorsValidationResult OUTPUT, @DispensingIdResult OUTPUT, @DispensingCodeResult OUTPUT, @StatusResult OUTPUT;
        
            IF(@CodeMessageResult = '999')
            BEGIN
                SELECT 999 AS Status, @ErrorsValidationResult AS Message, 0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
                RETURN;
            END

            SET @DispensingId = @DispensingIdResult;
            SET @DispensingCode = @DispensingCodeResult;
            SET @MessageReturn = @ErrorsValidationResult;
        END

        -- #TempIdAndProductCode: ids de detalle
        IF @Validate = 0
        BEGIN
            IF @PerformsHealthProfessionalThirdPartyId IS NULL  BEGIN
                
                SELECT TOP 1 @PerformsHealthProfessionalThirdPartyId = th.id
                FROM #DispensingDetail td
                JOIN common.ThirdParty th WITH(NOLOCK) ON th.Nit = td.HealthProfessionalNit

                SET @DateFormulation = @Date

                SELECT @NumingressDiagnostic = AD.CODDIAING
                FROM dbo.ADINGRESO AD WITH(NOLOCK)
                WHERE AD.NUMINGRES = @NUMINGRES 
            END

            INSERT INTO [Inventory].[MedicalFormula](
                                            [Number],
                                            [PatientCode],
                                            [PatientFirstName],
                                            [PatientSecondName],
                                            [PatientFirstLastName], 
                                            [PatientSecondLastName],
                                            [PatientName],
                                            [AdmissionNumber],
                                            [CareCenterCode],
                                            [FunctionalUnidCode],
                                            [FunctionalUnitName], 
                                            [Date], 
                                            [CreationUser],
                                            [CreationDate],
                                            IPSCode,
                                            IsManual, 
                                            WarehouseId,
                                            CareGroupId,
                                            BillingAuthorizationId,
                                            DateFormulation,
                                            PerformsHealthProfessionalThirdPartyId)
            VALUES( @Number,
                    @IPCODPACI,
                    @IPPRINOMB,
                    @IPSEGNOMB,
                    @IPPRIAPEL, 
                    @IPSEGAPEL,
                    @IPNOMCOMP, 
                    @NUMINGRES,
                    @CareCenterCode,
                    @UFUCODIGO, 
                    @UFUDESCRI,
                    @Date,
                    @CodeUser,
                    Common.GETDATE(), 
                    @IPSCode,
                    @IsManual, 
                    @WareHouseId,
                    @CareGroupId,
                    @BillingAuthorizationId,@DateFormulation,@PerformsHealthProfessionalThirdPartyId);

            SET @MedicalFormulaId = SCOPE_IDENTITY();

            INSERT INTO Inventory.MedicalFormulaDetail(MedicalFormulaId, AdmissionNumber, EntityId, EntityCode, ContractCode, PlanCode, ProductType, ProductCode, ProductName, RequestQuantity, DeliveryQuantity, 
            PendingQuantity, NoPos, MeasurementUnitCode, HealthProfessionalCode, HealthProfessionalName, HealthProfessionalNit, IdeTipHis, NumFolio, 
            PatientCode, SpecialtyCode, SpecialtyName, IsDeferred,TreatmentDays,DiagnosticCode,AuthorizationNumber,IDMipres) 
            OUTPUT inserted.Id, inserted.ProductCode INTO #TempIdAndProductCode(Id, ProductCode)
            SELECT @MedicalFormulaId, td.AdmissionNumber, td.EntityId, td.EntityCode, td.ContractCode, td.PlanCode, 
                   td.ProductType,td.ProductCode, td.ProductName,
                   td.RequestQuantity, td.DeliveryQuantity, td.PendingQuantity, td.NoPos, td.MeasurementUnitCode, td.HealthProfessionalCode, td.HealthProfessionalName, 
                   td.HealthProfessionalNit, td.IdeTipHis, td.NumFolio, td.PatientCode, td.SpecialtyCode, td.SpecialtyName,
                   td.IsDeferred,td.TreatmentDays, IIF(ISNULL(td.DiagnosticCode,'') ='',@NumingressDiagnostic,td.DiagnosticCode),td.AuthorizationNumber,td.IDMipres
            FROM #DispensingDetail td;
            
            -- Movimientos
            INSERT INTO Inventory.MedicalFormulaDetailProducts(MedicalFormulaDetailId,ProductId,BatchSerialId,WarehouseId,Quantity,CreationUser, CreationDate)
            SELECT mfd.Id,tdp.ProductId,tdp.BatchSerialId,tdp.WarehouseId,tdp.Quantity,tdp.CreationUser, Common.GETDATE()            
            FROM #TableDetailProducts tdp
            INNER JOIN Inventory.MedicalFormulaDetail mfd ON tdp.ProductCode = mfd.ProductCode 
            WHERE mfd.MedicalFormulaId = @MedicalFormulaId;

            -- Diferidos
            IF (SELECT COUNT(*) FROM #TableDeferred) > 0
            BEGIN
                INSERT INTO Inventory.MedicalFormulaDetailDeferred(MedicalFormulaDetailId, FirstDeliveryDate, DeliveryQuantityDeferred, Periodicity, Number, 
                DeliveryDate, DeliveryQuantity, PendingQuantity)
                SELECT temp.Id, td.FirstDeliveryDate, td.DeliveryQuantityDeferred, td.Periodicity, td.Number, td.DeliveryDate, td.DeliveryQuantity, td.PendingQuantity
                FROM #TableDeferred td
                INNER JOIN #TempIdAndProductCode temp ON temp.ProductCode = td.ProductCode;

                UPDATE mfd SET mfd.DeliveryQuantity = mfdd.DeliveryQuantity - mfdd.PendingQuantity, 
                               mfd.PendingQuantity = mfdd.PendingQuantity,
                               mfd.RequestQuantity = mfdd.DeliveryQuantity
                FROM Inventory.MedicalFormulaDetail mfd
                INNER JOIN (
                    SELECT d2.MedicalFormulaDetailId, SUM(d2.DeliveryQuantity) DeliveryQuantity, SUM(d2.PendingQuantity) PendingQuantity
                    FROM Inventory.MedicalFormulaDetailDeferred d2
                    GROUP BY d2.MedicalFormulaDetailId
                ) mfdd ON mfdd.MedicalFormulaDetailId = mfd.Id
                WHERE mfd.MedicalFormulaId = @MedicalFormulaId;
            END

        END
        ELSE
        BEGIN
            UPDATE Inventory.MedicalFormula 
               SET ModificationUser = @CodeUser, ModificationDate = Common.GETDATE(), IPSCode = @IPSCode 
            WHERE Number = @Number;
            
            SELECT @MedicalFormulaId = Id FROM Inventory.MedicalFormula WHERE Number = @Number;

            IF EXISTS
            (
                SELECT 1
                FROM #DispensingDetail d
                LEFT JOIN Inventory.MedicalFormulaDetail mfd ON @MedicalFormulaId = mfd.MedicalFormulaId AND d.ProductCode = mfd.ProductCode
                WHERE mfd.Id IS NULL
            )
            BEGIN
                SELECT @ErrorsValidationResult = STUFF((
                    SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + d.ProductCode + ' - ' + d.ProductName
                    FROM #DispensingDetail d
                    LEFT JOIN Inventory.MedicalFormulaDetail mfd ON @MedicalFormulaId = mfd.MedicalFormulaId AND d.ProductCode = mfd.ProductCode
                    WHERE mfd.Id IS NULL
                    FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'');

                SELECT 999 AS Status, 
                       'Los siguientes productos no se encuentran agregados a la formula médica: ' + CHAR(13) + CHAR(10) + ISNULL(@ErrorsValidationResult, '') AS Message, 
                       0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
                RETURN;
            END

            IF EXISTS
            (
                SELECT 1
                FROM #DispensingDetail d
                JOIN Inventory.MedicalFormulaDetail mfd ON @MedicalFormulaId = mfd.MedicalFormulaId AND d.ProductCode = mfd.ProductCode
                WHERE d.DeliveryQuantity < 0
            )
            BEGIN
                SELECT @ErrorsValidationResult = STUFF((
                    SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', d.ProductCode, ' - ', d.ProductName, ' (Cantidad Solicitada: ', d.DeliveryQuantity, ' )')
                    FROM #DispensingDetail d
                    JOIN Inventory.MedicalFormulaDetail mfd ON @MedicalFormulaId = mfd.MedicalFormulaId AND d.ProductCode = mfd.ProductCode
                    WHERE d.DeliveryQuantity < 0
                    FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'');

                SELECT 999 AS Status, 
                       'La cantidad solicitada de los siguientes productos no es valida: ' + CHAR(13) + CHAR(10) + ISNULL(@ErrorsValidationResult, '') AS Message, 
                       0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
                RETURN;
            END

            IF EXISTS
            (
                SELECT 1
                FROM #DispensingDetail d
                JOIN Inventory.MedicalFormulaDetail mfd ON @MedicalFormulaId = mfd.MedicalFormulaId AND d.ProductCode = mfd.ProductCode
                WHERE d.DeliveryQuantity > mfd.PendingQuantity
            )
            BEGIN
                SELECT @ErrorsValidationResult = STUFF((
                    SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - ', d.ProductCode, ' - ', d.ProductName, ' (Cantidad Solicitada: ', d.DeliveryQuantity, ' - Cantidad Pendiente: ', mfd.PendingQuantity, ')')
                    FROM #DispensingDetail d
                    JOIN Inventory.MedicalFormulaDetail mfd ON @MedicalFormulaId = mfd.MedicalFormulaId AND d.ProductCode = mfd.ProductCode
                    WHERE d.DeliveryQuantity > mfd.PendingQuantity
                    FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'');

                SELECT 999 AS Status, 
                       'La cantidad solicitada de los siguientes productos supera la cantidad pendiente: ' + CHAR(13) + CHAR(10) + ISNULL(@ErrorsValidationResult, '') AS Message, 
                       0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
                RETURN;
            END
            
            INSERT INTO #TempIdAndProductCode(Id, ProductCode)
            SELECT mfd.Id, mfd.ProductCode
            FROM Inventory.MedicalFormulaDetail mfd
            WHERE mfd.MedicalFormulaId = @MedicalFormulaId;
            
            IF (SELECT COUNT(*) FROM #TableDeferred WHERE Id = 0) > 0
            BEGIN
                INSERT INTO Inventory.MedicalFormulaDetailDeferred(MedicalFormulaDetailId, FirstDeliveryDate, DeliveryQuantityDeferred, Periodicity, Number, 
                DeliveryDate, DeliveryQuantity, PendingQuantity)
                SELECT temp.Id, td.FirstDeliveryDate, td.DeliveryQuantityDeferred, td.Periodicity, td.Number, td.DeliveryDate, td.DeliveryQuantity, td.PendingQuantity
                FROM #TableDeferred td
                INNER JOIN #TempIdAndProductCode temp ON temp.ProductCode = td.ProductCode
                WHERE td.Id = 0;
            END

            IF (SELECT COUNT(*) FROM #TableDeferred) > 0
            BEGIN
                UPDATE d SET d.PendingQuantity = t.PendingQuantity
                FROM Inventory.MedicalFormulaDetailDeferred d
                INNER JOIN #TableDeferred t ON t.Id = d.Id;
            END
            
            UPDATE mfd SET mfd.DeliveryQuantity = mfd.DeliveryQuantity + td.DeliveryQuantity , 
                           mfd.PendingQuantity = CASE WHEN mfdd.MedicalFormulaDetailId IS NULL THEN mfd.PendingQuantity - td.DeliveryQuantity ELSE mfdd.PendingQuantity END,
                           mfd.RequestQuantity = mfd.RequestQuantity,
                           mfd.IsDeferred = CASE WHEN mfdd.MedicalFormulaDetailId IS NOT NULL THEN 1 ELSE 0 END
            FROM Inventory.MedicalFormulaDetail mfd
            INNER JOIN #DispensingDetail td ON td.ProductCode = mfd.ProductCode
            LEFT JOIN (
                SELECT d2.MedicalFormulaDetailId, SUM(d2.DeliveryQuantity) DeliveryQuantity, SUM(d2.PendingQuantity) PendingQuantity
                FROM Inventory.MedicalFormulaDetailDeferred d2
                GROUP BY d2.MedicalFormulaDetailId
            ) mfdd ON mfdd.MedicalFormulaDetailId = mfd.Id
            WHERE mfd.MedicalFormulaId = @MedicalFormulaId AND td.DeliveryQuantity > 0;

            INSERT INTO Inventory.MedicalFormulaDetailProducts(MedicalFormulaDetailId,ProductId,BatchSerialId,WarehouseId,Quantity,CreationUser, CreationDate)
            SELECT mfd.Id,tdp.ProductId,tdp.BatchSerialId,tdp.WarehouseId,tdp.Quantity,tdp.CreationUser, Common.GETDATE()            
            FROM #TableDetailProducts tdp
            INNER JOIN Inventory.MedicalFormulaDetail mfd ON tdp.ProductCode = mfd.ProductCode 
            WHERE mfd.MedicalFormulaId = @MedicalFormulaId;

            IF @XmlAnnulateDashboard.exist('*') > 0
            BEGIN
                IF OBJECT_ID('tempdb..#MedicalFormulaDetailAnnulled') IS NOT NULL DROP TABLE #MedicalFormulaDetailAnnulled;
                CREATE TABLE #MedicalFormulaDetailAnnulled (MedicalFormulaDetailId int, AnnulmentQuentity int);
                CREATE NONCLUSTERED INDEX IX_MedicalFormulaDetailAnnulled_MFDId ON #MedicalFormulaDetailAnnulled(MedicalFormulaDetailId);

                INSERT #MedicalFormulaDetailAnnulled
                SELECT t.x.value('MedicalFormulaDetailId[1]','int'), t.x.value('CantidadPendiente[1]','int')
                FROM @XmlAnnulateDashboard.nodes('/Main/ViewDashboardPharmacyDetail') t(x);

                UPDATE mfd 
                   SET PendingQuantity = 0, RequestQuantity = 0
                FROM Inventory.MedicalFormulaDetail mfd
                JOIN #MedicalFormulaDetailAnnulled a ON mfd.Id = a.MedicalFormulaDetailId
                WHERE mfd.DeliveryQuantity = 0 AND mfd.PendingQuantity > 0;

                UPDATE mfd 
                   SET PendingQuantity = 0
                FROM Inventory.MedicalFormulaDetail mfd
                JOIN #MedicalFormulaDetailAnnulled a ON mfd.Id = a.MedicalFormulaDetailId
                WHERE mfd.DeliveryQuantity > 0 AND mfd.PendingQuantity > 0;

                UPDATE mfd 
                   SET PendingQuantity = 0
                FROM Inventory.MedicalFormulaDetailDeferred mfd
                JOIN #MedicalFormulaDetailAnnulled a ON mfd.MedicalFormulaDetailId = a.MedicalFormulaDetailId
                WHERE mfd.DeliveryQuantity = 0 AND mfd.PendingQuantity > 0;

                UPDATE mfd 
                   SET PendingQuantity = 0, DeliveryQuantity = 0
                FROM Inventory.MedicalFormulaDetailDeferred mfd
                JOIN #MedicalFormulaDetailAnnulled a ON mfd.MedicalFormulaDetailId = a.MedicalFormulaDetailId
                WHERE mfd.DeliveryQuantity > 0 AND mfd.PendingQuantity > 0;
                
            END
        END

        -- Facturación si hubo dispensación
        IF @XmlPharmaceutical.exist('*') > 0
        BEGIN
            INSERT INTO Inventory.MedicalFormulaPharmaceuticalDispensing(MedicalFormulaId, PharmaceuticalDispensingId, PharmaceuticalDispensingCode)
            VALUES(@MedicalFormulaId, @DispensingId, @DispensingCode);
            
            DECLARE @InvoiceCategoryId int = (SELECT TOP 1 Id FROM Billing.InvoiceCategories);

            UPDATE rcd 
                SET rcd.ThirdPartyId = @ThirdPartyIdHealthAdministrator, 
                    rcd.BillingAuthorizationId = @BillingAuthorizationId, 
                    rcd.InvoiceCategoryId = @InvoiceCategoryId, 
                    rcd.OutputDate = IIF(YEAR(rcd.OutputDate) = 1900, Common.GETDATE(), rcd.OutputDate)
            FROM Billing.RevenueControl rc
            INNER JOIN Billing.RevenueControlDetail rcd ON rcd.RevenueControlId = rc.Id
            WHERE rc.AdmissionNumber = @CONNUMACT;
        
            INSERT INTO #RevenueControlDetailCrossingList
            (
                FolioOrder, FolioType, CareGroupId, RevenueControlDetailId, TotalPatientDiscount,RevenueControlId, OutputDate, IsCutAccount, OutputDiagnosis, InitialDate, CutType
            )
            SELECT rcd.FolioOrder, rcd.FolioType, rcd.CareGroupId, rcd.Id, rcd.TotalPatientWithDiscount, rc.Id, rcd.OutputDate, rcd.IsCutAccount, rcd.OutputDiagnosis, Common.GETDATE(), 1
            FROM Billing.RevenueControl rc
            INNER JOIN Billing.RevenueControlDetail rcd ON rcd.RevenueControlId = rc.Id
            WHERE rc.AdmissionNumber = @CONNUMACT;

            DECLARE @xmlLiquidation xml = 
            (
                SELECT  FolioOrder, 
                        FolioType, 
                        CareGroupId, 
                        RevenueControlDetailId, 
                        TotalPatientDiscount, 
                        RevenueControlId, 
                        ISNULL(OutputDate, Common.GETDATE()) OutputDate, 
                        IsCutAccount, 
                        OutputDiagnosis, 
                        InitialDate, 
                        CutType,
                        @FilePath FilePath
                FROM #RevenueControlDetailCrossingList AS RevenueControlDetailCrossingList
                FOR XML AUTO, ELEMENTS
            );

            SELECT @ThirdPartyIdPatient = Id FROM Common.ThirdParty WHERE Nit = @IPCODPACI;
            SET @ContainerCrystal = DB_NAME();

            EXEC Billing.SP_LiquidateFolio_Output @IPCODPACI, @CONNUMACT, @ContainerCrystal, @BillingAuthorizationId, @OperatingUnitId, @ThirdPartyIdPatient, @CodeUser, 1, @xmlLiquidation, @SkipAccountControlValidations,
                 @ResultStatus OUTPUT, @ResultMessageInvoice OUTPUT, @ResultMessage OUTPUT, @ResultXml OUTPUT;

            IF @ResultStatus = 0
            BEGIN
                SELECT 999 AS Status, @ResultMessage AS Message, 0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
                RETURN;
            END

            SELECT 
                @ResultInvoiceId = t.x.value('InvoiceId[1]','int'),
                @ResultInvoiceNumber = t.x.value('InvoiceNumber[1]','varchar(20)')
            FROM @ResultXml.nodes('/Data') t(x);

            IF ISNULL(@ResultInvoiceId, 0) = 0
            BEGIN
                SELECT 999 AS Status, 'Factura no generada' AS Message, 0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
                RETURN;
            END

            DECLARE @MedicalFormulaInvoiceId INT;

            INSERT INTO Inventory.MedicalFormulaInvoice (MedicalFormulaId, InvoiceId) 
            SELECT @MedicalFormulaId, @ResultInvoiceId;

            SET @MedicalFormulaInvoiceId = SCOPE_IDENTITY();

            ;WITH cte_MedicalFormulaDetail as ( SELECT  mfdp.ProductId,
                                                       mfd.MedicalFormulaId,
                                                       mfd.Id
                                                FROM Inventory.MedicalFormulaDetailProducts mfdp WITH(NOLOCK)
                                                JOIN Inventory.MedicalFormulaDetail mfd WITH(NOLOCK) ON mfdp.MedicalFormulaDetailId = mfd.Id
                                                WHERE mfd.MedicalFormulaId = @MedicalFormulaId
                                                GROUP BY mfdp.ProductId,mfd.MedicalFormulaId,mfd.Id)
            
            INSERT INTO [Inventory].[MedicalFormulaInvoiceDetail]([MedicalFormulaInvoiceId],[InvoiceDetailId],[MedicalFormulaDetailId])
            SELECT  mfi.Id, --MedicalFormulaInvoiceId
                    id.Id, --InvoiceDetailId
                    cte.Id --MedicalFormulaDetailId
            FROM Inventory.MedicalFormulaInvoice mfi WITH(NOLOCK)
            JOIN Billing.InvoiceDetail id WITH(NOLOCK) ON mfi.InvoiceId = id.InvoiceId
            JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
            JOIN cte_MedicalFormulaDetail cte WITH(NOLOCK) ON cte.MedicalFormulaId = mfi.MedicalFormulaId AND cte.ProductId = sod.ProductId
            WHERE mfi.Id = @MedicalFormulaInvoiceId;

            INSERT INTO Billing.MipresCode (ServiceOrderDetailId,Code,CreationUser,CreationDate,IdMipres)
            SELECT sod.Id,mfd.IDMipres,@CodeUser,common.GETDATE(),mfd.IDMipres
            FROM [Inventory].[MedicalFormulaInvoiceDetail] mfid WITH(NOLOCK)
            JOIN Billing.InvoiceDetail id WITH(NOLOCK) ON id.Id=mfid.InvoiceDetailId
            JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
            JOIN Inventory.MedicalFormulaDetail mfd WITH(NOLOCK) ON mfd.Id = mfid.MedicalFormulaDetailId
            WHERE mfid.MedicalFormulaInvoiceId = @MedicalFormulaInvoiceId AND ISNULL(mfd.IDMipres,'') <> '';

            SELECT TOP 1 @RevenueControlDetailId = rcd.Id
            FROM Billing.RevenueControl rc WITH(NOLOCK)
            INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.RevenueControlId = rc.Id
            WHERE rc.AdmissionNumber = @CONNUMACT;

            SELECT 0 AS Status, @ResultMessageInvoice + @MessageReturn AS Message, @DispensingId AS DispensingId, @DispensingCode AS DispensingCode, 
                   @ResultInvoiceId AS InvoiceId, @ResultInvoiceNumber AS InvoiceNumber, @RevenueControlDetailId AS RevenueControlDetailId, CAST(@CONNUMACT AS varchar(20)) AS AdmissionNumber;
        END
        ELSE BEGIN
            SELECT 0 AS Status, 'Se guardó la orden médica correctamente' AS Message, 0 AS DispensingId, '' AS DispensingCode, 
                   0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
        END

    END TRY
    BEGIN CATCH
        SELECT 999 AS Status, ERROR_MESSAGE() AS Message, 0 AS DispensingId, '' AS DispensingCode, 0 AS InvoiceId, '' AS InvoiceNumber, 0 AS RevenueControlDetailId, '' AS AdmissionNumber;
    END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que registra y guarda la dispensación de medicamentos por paciente proveniente de la integración con Medilaser (sistema externo de farmacia). Recibe información en formato XML con los datos del despacho farmacéutico (número de ingreso, medicamentos entregados, cantidades dispensadas y diferidas, profesional de salud prescriptor, diagnóstico y autorización), procesa el detalle de cada producto farmacéutico —incluyendo entregas diferidas o fraccionadas— y genera los registros correspondientes en el control de ingresos y facturación, cruzando con categorías de facturación, entidades aseguradoras (EPS/pagadores) y unidades funcionales. Permite a la institución registrar automáticamente las salidas de inventario de medicamentos dispensados a pacientes hospitalizados o ambulatorios, garantizando trazabilidad de la fórmula médica, cantidades entregadas versus pendientes, y vinculación con el proceso de facturación y control de cuentas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDispensingByPatientMedilaser';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDispensingByPatientMedilaser';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa la integración de dispensación por paciente con Medilaser: valida parámetros de facturación, registra/actualiza paciente, fórmula médica y su detalle, ejecuta dispensación farmacéutica, liquida el folio y genera la factura asociada.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDispensingByPatientMedilaser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Si @OperatingUnitId > 0, debe existir al menos un registro en Billing.SettingsBilling para esa unidad operativa.; Si @OperatingUnitId > 0, los campos FunctionalUnitId y HealthProfessionalCode de Billing.SettingsBilling deben estar diligenciados (no nulos).; El XML @XmlPrescription debe contener nodo /PrescriptionHeader con la cabecera del paciente y la fórmula.; Para crear nueva fórmula (no manual): no debe existir previamente una fila en Inventory.MedicalFormula con el mismo Number.; Para fórmula manual: no debe existir Inventory.MedicalFormula con el mismo Number y PatientCode.; El @CareGroupId debe corresponder a un CareGroup vinculado a un Contract con HealthAdministrator válido.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDispensingByPatientMedilaser';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDispensingByPatientMedilaser';
-- GO
