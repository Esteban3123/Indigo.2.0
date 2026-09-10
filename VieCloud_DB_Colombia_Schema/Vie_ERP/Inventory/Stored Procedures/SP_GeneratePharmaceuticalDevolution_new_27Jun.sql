
-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 26/01/2016
-- Description:	Procedimiento almacena para guardar o confirmar las devoluciones
-- =============================================
CREATE PROCEDURE [Inventory].[SP_GeneratePharmaceuticalDevolution_new_27Jun] @DevolutionXml XML, 
                                                                            @AnnulateXml   XML, 
                                                                            @User          VARCHAR(20)
AS
    BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        DECLARE @IdDevolution INT, @CodeDevolution VARCHAR(20), @OperatingUnitId INT, @DocumentDate DATETIME, @WarehouseId INT, @CodeNameWarehouse VARCHAR(20), @AdmissionNumber VARCHAR(20), @CodePacient VARCHAR(20), @IdThirdPartyPacient INT, @Observation VARCHAR(300), @Status TINYINT, @CreationUser VARCHAR(20), @CreationDate DATE, @ModificationUser VARCHAR(20), @ModificationDate DATE, @ConfirmationUser VARCHAR(20), @ConfirmationDate DATE, @AnnulmentUser VARCHAR(20), @AnnulmentDate DATE;
        DECLARE @IsDashBoard INT, @DevolutionOrigin VARCHAR(2), @CareCenterCode VARCHAR(20), @FunctionUnitCode VARCHAR(20), @ConsecutiveCrystal INT;
        DECLARE @TableDetail TABLE
        (RowId                                       INT IDENTITY(1, 1) PRIMARY KEY CLUSTERED, 
         Id                                          INT, 
         PharmaceuticalDispensingDevolutionId        INT, 
         PharmaceuticalDispensingDetailBatchSerialId INT, 
         Quantity                                    INT, 
         CodeProduct                                 VARCHAR(100) NULL, 
         OrderedHealthProfessionalCode               VARCHAR(50) NULL, 
         EntityState                                 VARCHAR(50), 
         ConsecutiveKardex                           DECIMAL(18, 0)
        );
        DECLARE @tmpADINGRESO TABLE
        (RowId        INT IDENTITY(1, 1) PRIMARY KEY, 
         NUMINGRES    CHAR(10), 
         IPCODPACI    CHAR(15), 
         IESTADOIN    CHAR(1), 
         GENCONENTITY INT NULL
        );
        DECLARE @TableServiceOrderDetailDistributionId TABLE
        (RowId INT IDENTITY(1, 1) PRIMARY KEY CLUSTERED, 
         id    INT
        );
        DECLARE @MessageReturn VARCHAR(MAX)= '';
        DECLARE @PendingCount INT;
        DECLARE @IdStayOrigin INT;
        DECLARE @IdStayDestination INT;
        DECLARE @UfuCodigoDestination VARCHAR(20);
        DECLARE @CodiCamaDestination VARCHAR(20);
        DECLARE @FechaEgresoPaciente DATETIME;
        DECLARE @__IDCONSECU CHAR(8)= '00000011';
        DECLARE @Uno TINYINT= 1, @Cero TINYINT= 0, @tres TINYINT= 3;
        DECLARE @IdForm INT= 1516;
        DECLARE @StateAdded VARCHAR(5)= 'Added', @StateDeleted VARCHAR(7)= 'Deleted', @StateModified VARCHAR(8)= 'Modified', @DocumentType11 TINYINT= 11, @EntityNamePharmaceutical VARCHAR(24)= 'PharmaceuticalDispensing';
        BEGIN TRY

            /****** Realizo las anulaciones *****/

            IF EXISTS
            (
                SELECT t.x.value('Consecutivo[1]', 'varchar(20)')
                FROM @AnnulateXml.nodes('/ViewDashboardPharmacyDetailDevolution') t(x)
            )
                BEGIN
                    DECLARE @TableAnnulate TABLE
                    (RowId                    INT IDENTITY(1, 1) PRIMARY KEY CLUSTERED, 
                     Consecutivo              VARCHAR(20), 
                     CodigoPacienteDevolucion VARCHAR(20), 
                     Ingreso                  VARCHAR(20), 
                     CODCENATE                VARCHAR(20), 
                     UFUCODIGO                VARCHAR(20), 
                     CODPROSAL                VARCHAR(30), 
                     CODPRODUC                VARCHAR(30), 
                     CantidadDevuelta         INT, 
                     PROESTADO                TINYINT, 
                     FECRESGIS                DATETIME, 
                     CODUSUARI                VARCHAR(20), 
                     NOPOS                    BIT, 
                     Entidad                  VARCHAR(900), 
                     Producto                 VARCHAR(900), 
                     ContratoPlan             VARCHAR(900), 
                     Tipo                     TINYINT, 
                     CantidadPendiente        INT, 
                     Medico                   VARCHAR(900), 
                     Especialidad             VARCHAR(900), 
                     ConsecutiveKardex        DECIMAL(18, 0)
                    );
                    INSERT INTO @TableAnnulate
                           SELECT t.x.value('Consecutivo[1]', 'varchar(20)'), 
                                  t.x.value('CodigoPacienteDevolucion[1]', 'varchar(20)'), 
                                  t.x.value('Ingreso[1]', 'varchar(20)'), 
                                  t.x.value('CODCENATE[1]', 'varchar(20)'), 
                                  t.x.value('UFUCODIGO[1]', 'varchar(20)'), 
                                  t.x.value('CODPROSAL[1]', 'varchar(30)'), 
                                  t.x.value('CODPRODUC[1]', 'varchar(30)'), 
                                  t.x.value('CantidadDevuelta[1]', 'int'), 
                                  t.x.value('PROESTADO[1]', 'tinyint'), 
                                  t.x.value('FECRESGIS[1]', 'datetime'), 
                                  t.x.value('CODUSUARI[1]', 'varchar(20)'), 
                                  t.x.value('NOPOS[1]', 'bit'), 
                                  t.x.value('Entidad[1]', 'varchar(900)'), 
                                  t.x.value('Producto[1]', 'varchar(900)'), 
                                  t.x.value('ContratoPlan[1]', 'varchar(900)'), 
                                  t.x.value('Tipo[1]', 'tinyint'), 
                                  t.x.value('CantidadPendiente[1]', 'int'), 
                                  t.x.value('Medico[1]', 'varchar(900)'), 
                                  t.x.value('Especialidad[1]', 'varchar(900)'), 
                                  0
                           FROM @AnnulateXml.nodes('/ViewDashboardPharmacyDetailDevolution') t(x);
                    IF EXISTS
                    (
                        SELECT ing.NUMINGRES
                        FROM @tmpADINGRESO ing
                             INNER JOIN @TableAnnulate ta ON ing.NUMINGRES = ta.Ingreso
                        WHERE IESTADOIN IN('F', 'C')
                    )
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'No se puede confirmar la devolución debido a que el ingreso esta facturado o cerrado' AS [Message], 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    ---- Ahora afecto el Kardex de crystal
                    IF NOT EXISTS
                    (
                        SELECT IDCONSECU
                        FROM dbo.INCONSECU WITH(NOLOCK)
                        WHERE IDCONSECU = @__IDCONSECU
                    )
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'No existe un consecutivo para el Kardex(00000011) en Indigo Crystal' AS [Message], 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    DECLARE @ConsecutiveKardexAnnulate DECIMAL(18, 0);

                    --Actualizo para que quede el ultimo consecutvo
                    --Update dbo.INCONSECU Set @ConsecutiveKardexAnnulate = CONNUMACT += 1 Where IDCONSECU = @__IDCONSECU
                    --Update @TableAnnulate Set ConsecutiveKardex = @ConsecutiveKardexAnnulate - 1

                    UPDATE dbo.INCONSECU
                      SET 
                          @ConsecutiveKardexAnnulate = CONNUMACT = CONNUMACT
                    WHERE IDCONSECU = @__IDCONSECU;
                    SET @ConsecutiveKardexAnnulate-=1;
                    UPDATE @TableAnnulate
                      SET 
                          @ConsecutiveKardexAnnulate = ConsecutiveKardex = @ConsecutiveKardexAnnulate + 1;
                    UPDATE dbo.INCONSECU
                      SET 
                          CONNUMACT = @ConsecutiveKardexAnnulate + 1
                    WHERE IDCONSECU = @__IDCONSECU; --Esta lógica es muy rara para hacer algo sencillo
                    ---- Inserto en el Kardex de Crystal
                    INSERT INTO dbo.HCKARDPAC
                    (NUMCONSEC, 
                     IPCODPACI, 
                     NUMINGRES, 
                     CODCENATE, 
                     UFUCODIGO, 
                     CODPROSAL, 
                     CODPRODUC, 
                     CANPRODUCT, 
                     TIPREGIST, 
                     HCSOLINSN, 
                     FECREGKAR, 
                     TIPORIREG, 
                     DESMOVPRO
                    )
                           SELECT ConsecutiveKardex, 
                                  CodigoPacienteDevolucion, 
                                  Ingreso, 
                                  CODCENATE, 
                                  UFUCODIGO, 
                                  CODPROSAL, 
                                  CODPRODUC, 
                                  CantidadDevuelta, 
                                  '4', 
                                  '', 
                                  [Common].[GETDATE](), 
                                  9, 
                                  'Anulacion del Devolutivo en Farmacia, Origen Devolutivo: ' + UFUCODIGO + ' - Usuario: ' + @User
                           FROM @TableAnnulate;
                    UPDATE dbo.HCDEVMEDD
                      SET 
                          PROESTADO = '3'
                    FROM @TableAnnulate ta
                         INNER JOIN dbo.HCDEVMEDD hc ON ta.Consecutivo = hc.CODCONCEC
                                                        AND ta.CODPRODUC = hc.CODPRODUC;
                    IF NOT EXISTS
                    (
                        SELECT hc.CODCONCEC
                        FROM dbo.HCDEVMEDD hc WITH(NOLOCK)
                             INNER JOIN @TableAnnulate ta ON ta.Consecutivo = hc.CODCONCEC
                        WHERE hc.CANPENDIE > 0
                              AND hc.PROESTADO = @Uno
                    )
                        BEGIN -- Si todos los detalles ya fueron devueltos o anulados				
                            UPDATE dbo.HCDEVMEDC
                              SET 
                                  DEVESTADO = '3'
                            FROM dbo.HCDEVMEDC hc
                                 INNER JOIN @TableAnnulate ta ON ta.Consecutivo = hc.CODCONCEC; --where hc.CANPENDIE > 0 and hc.PROESTADO = 1
                    END;
                    --********************************PROCESO DE LIBERACION DE CAMA*****************************************

                    DECLARE @ConsecutiveCrystalCursor INT, @AdmissionNumberCursor VARCHAR(20);

                    --select @ConsecutiveCrystalCursor = A.Consecutivo, @AdmissionNumberCursor = A.Ingreso from (select top 1 Consecutivo, Ingreso from @TableAnnulate) AS A
                    SELECT TOP 1 @ConsecutiveCrystalCursor = Consecutivo, 
                                 @AdmissionNumberCursor = Ingreso
                    FROM @TableAnnulate
                    ORDER BY RowId;
                    SET @DevolutionOrigin =
                    (
                        SELECT ORIDEVMED
                        FROM.HCDEVMEDC WITH(NOLOCK)
                        WHERE CODCONCEC = @ConsecutiveCrystalCursor
                    );
                    IF NOT EXISTS
                    (
                        SELECT CODCONCEC
                        FROM.HCDEVMEDD WITH(NOLOCK)
                        WHERE CODCONCEC = @ConsecutiveCrystalCursor
                              AND PROESTADO = @uno
                    )
                        BEGIN
                            IF @DevolutionOrigin <> '3'
                               AND @DevolutionOrigin <> '4'
                                BEGIN
                                    IF @DevolutionOrigin = '1'
                                        BEGIN --- Si es un traslado de cama						
                                            DECLARE @MinFECINIEST DATETIME=
                                            (
                                                SELECT MIN(FECINIEST)
                                                FROM dbo.CHREGESTA WITH(NOLOCK)
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND REGESTADO = @uno
                                            );
                                            DECLARE @MaxFECINIEST DATETIME=
                                            (
                                                SELECT MAX(FECINIEST)
                                                FROM dbo.CHREGESTA WITH(NOLOCK)
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND REGESTADO = @uno
                                            );
                                            SET @IdStayOrigin =
                                            (
                                                SELECT ID
                                                FROM dbo.CHREGESTA WITH(NOLOCK)
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND REGESTADO = @Uno
                                                      AND FECINIEST = @MinFECINIEST
                                            );
                                            SET @IdStayDestination =
                                            (
                                                SELECT ID
                                                FROM dbo.CHREGESTA WITH(NOLOCK)
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND REGESTADO = @Uno
                                                      AND FECINIEST = @MaxFECINIEST
                                            );

                                            --- Actualizo la cama de origen para liberarla
                                            UPDATE dbo.CHCAMASHO
                                              SET 
                                                  ESTADCAMA = 1, 
                                                  CAMTRAMED = 0, 
                                                  CODCONCEC = NULL, 
                                                  CODAISLAM = NULL, 
                                                  CAMTIPANO = 0, 
                                                  CAMDEVMED = 0
                                            FROM dbo.CHREGESTA re
                                                 INNER JOIN dbo.CHCAMASHO ca ON re.CODICAMAS = ca.CODICAMAS
                                            WHERE re.ID = @IdStayOrigin;
                                            --- Actualizo la estancia de origen
                                            DECLARE @FECINIEST DATETIME=
                                            (
                                                SELECT FECINIEST
                                                FROM dbo.CHREGESTA WITH(NOLOCK)
                                                WHERE ID = @IdStayDestination
                                            );
                                            UPDATE dbo.CHREGESTA
                                              SET 
                                                  FECFINEST = @FECINIEST, 
                                                  REGESTADO = 2, 
                                                  REGDIAEST = DATEDIFF(DAY, FECINIEST, @FECINIEST)
                                            WHERE ID = @IdStayOrigin;
                                            --- Actualizo la cama de destino
                                            UPDATE dbo.CHCAMASHO
                                              SET 
                                                  CODCONCEC = NULL
                                            FROM dbo.CHREGESTA re
                                                 INNER JOIN dbo.CHCAMASHO ca ON re.CODICAMAS = ca.CODICAMAS
                                            WHERE re.ID = @IdStayDestination;
                                            --- Actualizo el ingreso para colocarle la cama actual del paciente y la unidad funcional							
                                            SELECT @UfuCodigoDestination = uf.UFUCODIGO, 
                                                   @CodiCamaDestination = ca.CODICAMAS
                                            FROM dbo.CHREGESTA re
                                                 INNER JOIN dbo.CHCAMASHO ca WITH(NOLOCK) ON re.CODICAMAS = ca.CODICAMAS
                                                 INNER JOIN dbo.INUNIFUNC uf WITH(NOLOCK) ON uf.UFUCODIGO = ca.UFUCODIGO
                                            WHERE re.ID = @IdStayDestination;
                                            UPDATE dbo.ADINGRESO
                                              SET 
                                                  UFUAACTHOS = @UfuCodigoDestination, 
                                                  UFUACTPAC = @UfuCodigoDestination, 
                                                  CODCAMACT = @CodiCamaDestination
                                            WHERE NUMINGRES = @AdmissionNumberCursor;
                                    END;
                                        ELSE
                                        BEGIN --- Si es Egreso de cama
                                            UPDATE dbo.CHCAMASHO
                                              SET 
                                                  ESTADCAMA = 2, 
                                                  CAMTRAMED = 0, 
                                                  CODCONCEC = NULL, 
                                                  CODAISLAM = NULL, 
                                                  CAMTIPANO = 0, 
                                                  CAMDEVMED = 0, 
                                                  CAMRECDEV = 1
                                            FROM dbo.CHREGESTA re
                                                 INNER JOIN dbo.CHCAMASHO ca ON re.CODICAMAS = ca.CODICAMAS
                                            WHERE re.NUMINGRES = @AdmissionNumberCursor
                                                  AND re.REGESTADO = @uno;
                                            IF NOT EXISTS
                                            (
                                                SELECT NUMINGRES
                                                FROM dbo.CHREGEGRE WITH(NOLOCK)
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND FECEGRESO IS NOT NULL
                                            )
                                                BEGIN
                                                    SELECT '999' AS CodeMessage, 
                                                           'La fecha del egreso del paciente no existe' AS Message, 
                                                           0 DevolutionId, 
                                                           CAST(3 AS TINYINT) AS [Status];
                                                    RETURN;
                                            END;
                                            SET @FechaEgresoPaciente =
                                            (
                                                SELECT FECEGRESO
                                                FROM dbo.CHREGEGRE WITH(NOLOCK)
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND FECEGRESO IS NOT NULL
                                            );
                                            --- Actualizo el estado de la estancia
                                            --update dbo.CHREGESTA set REGESTADO = 2, FECFINEST = @FechaEgresoPaciente, REGDIAEST = DATEDIFF(DAY, FECINIEST, @FechaEgresoPaciente) where NUMINGRES = @AdmissionNumberCursor and REGESTADO = 1
                                    END;
                            END;
                    END;

                    --******************************************************************************************************
                    IF
                    (
                        SELECT COUNT(*)
                        FROM @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x)
                    ) = 0
                        BEGIN
                            SELECT '0' AS CodeMessage, 
                                   'Se Anulo Correctamente la Devolución' AS Message, 
                                   0 DevolutionId, 
                                   CAST(1 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                            --if Not Exists (select t.x.value('Id[1]','int') from @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x)) begin
                            --	select '0' as CodeMessage, 'Se Anulo Correctamente la Devolución' as Message, 0 DevolutionId, cast(1 as tinyint) as [Status]
                            --	return
                            --end
            END;
            IF
            (
                SELECT COUNT(*)
                FROM @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x)
            ) > 0
                BEGIN
                    --if Exists (select t.x.value('Id[1]','int') from @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x)) begin
                    SELECT @IdDevolution = t.x.value('Id[1]', 'int'), 
                           @CodeDevolution = t.x.value('Code[1]', 'varchar(20)'), 
                           @OperatingUnitId = t.x.value('OperatingUnitId[1]', 'int'), 
                           @DocumentDate = CONVERT(DATETIME, t.x.value('DocumentDate[1]', 'varchar(19)'), 103), 
                           @WarehouseId = t.x.value('WarehouseId[1]', 'int'), 
                           @CodeNameWarehouse = t.x.value('CodeNameWarehouse[1]', 'varchar(20)'), 
                           @AdmissionNumber = t.x.value('AdmissionNumber[1]', 'varchar(10)'), 
                           @Observation = t.x.value('Observation[1]', 'varchar(300)'), 
                           @Status = t.x.value('Status[1]', 'tinyint'), 
                           @IsDashBoard = t.x.value('IsDashBoard[1]', 'bit'), ---- los campos de aca en adelante solo se solicitan cuando es desde dashboard
                           @DevolutionOrigin = t.x.value('DevolutionOrigin[1]', 'varchar(2)'), 
                           @CareCenterCode = t.x.value('CareCenterCode[1]', 'varchar(20)'), 
                           @FunctionUnitCode = t.x.value('FunctionUnitCode[1]', 'varchar(20)'), 
                           @ConsecutiveCrystal = t.x.value('ConsecutiveCrystal[1]', 'int')
                    FROM @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x);
                    IF EXISTS
                    (
                        SELECT NUMINGRES
                        FROM @tmpADINGRESO
                        WHERE NUMINGRES = @AdmissionNumber
                              AND IESTADOIN IN('F', 'C')
                    )
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'No se puede confirmar la devolución debido a que el ingreso esta facturado o cerrado' AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    INSERT INTO @tmpADINGRESO
                           SELECT NUMINGRES, 
                                  IPCODPACI, 
                                  IESTADOIN, 
                                  GENCONENTITY
                           FROM dbo.ADINGRESO WITH(NOLOCK)
                           WHERE NUMINGRES = @AdmissionNumber;
                    INSERT INTO @TableDetail
                           SELECT t.x.value('Id[1]', 'int'), 
                                  @IdDevolution, 
                                  t.x.value('PharmaceuticalDispensingDetailBatchSerialId[1]', 'int'), 
                                  t.x.value('Quantity[1]', 'int'), 
                                  t.x.value('CodeProduct[1]', 'varchar(100)'), --- Solo se solicita si es desde DashBoard
                                  t.x.value('OrderedHealthProfessionalCode[1]', 'varchar(50)'), 
                                  t.x.value('EntityState[1]', 'varchar(50)'), 
                                  0
                           FROM @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution/PharmaceuticalDispensingDevolutionDetail') t(x);
                    SET @CodePacient =
                    (
                        SELECT IPCODPACI
                        FROM @tmpADINGRESO
                        WHERE NUMINGRES = @AdmissionNumber
                    );
                    IF @CodePacient IS NULL
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'El ingreso ' + @AdmissionNumber + ' no existe en Indigo Crystal' AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    SET @IdThirdPartyPacient =
                    (
                        SELECT Id
                        FROM Common.ThirdParty WITH(NOLOCK)
                        WHERE Nit = @CodePacient
                    );
                    IF @IdThirdPartyPacient IS NULL
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'El paciente ' + @CodePacient + ' no existe como tercero en Indigo Vie' AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    --set @IdThirdPartyPacient = (select Id from Common.ThirdParty where Nit = @CodePacient)
                    DECLARE @SettingInventoryId INT, @IdJournalVoucherType INT, @PharmaceuticalDispensingGetThirdParty TINYINT, @PharmaceuticalDispensingThirdPartyId INT, @AssociateCostCenter TINYINT, @DiscountSalesMainAccountId INT;
                    SELECT @SettingInventoryId = Id, 
                           @IdJournalVoucherType = SalesReturnJournalVoucherTypeId, 
                           @PharmaceuticalDispensingGetThirdParty = PharmaceuticalDispensingGetThirdParty, 
                           @PharmaceuticalDispensingThirdPartyId = PharmaceuticalDispensingThirdPartyId, 
                           @AssociateCostCenter = AssociateCostCenter, 
                           @DiscountSalesMainAccountId = DiscountSalesMainAccountId
                    FROM Inventory.SettingInventory WITH(NOLOCK)
                    WHERE OperatingUnitId = @OperatingUnitId;
                    IF @SettingInventoryId IS NULL
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'No existe parametros de inventarios para la unidad operativa ' +
                            (
                                SELECT UnitName
                                FROM Common.OperatingUnit WITH(NOLOCK)
                                WHERE Id = @OperatingUnitId
                            ) AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    IF NOT EXISTS
                    (
                        SELECT Id
                        FROM Inventory.SettingInventory WITH(NOLOCK)
                        WHERE OperatingUnitId = @OperatingUnitId
                              AND [Year] = YEAR(@DocumentDate)
                              AND [Month] = MONTH(@DocumentDate)
                    )
                        BEGIN
                            DECLARE @DatePeriod VARCHAR(20)=
                            (
                                SELECT Concat(CAST(RIGHT('0' + [Month], 2) AS VARCHAR(2)), '-', CAST([Year] AS VARCHAR(4)))
                                FROM Inventory.SettingInventory WITH(NOLOCK)
                                WHERE OperatingUnitId = @OperatingUnitId
                            );
                            SELECT '999' AS CodeMessage, 
                                   'El periodo actual de inventario no coincide con la fecha del documento, Perido Actual de Inventario: ' + @DatePeriod AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    DECLARE @Prefix VARCHAR(20)= '';
                    IF ISNULL(@WarehouseId, 0) = 0
                        BEGIN --por aca entra cuando se hace desde dashboard				
                            SELECT @WarehouseId = Id, 
                                   @Prefix = Prefix
                            FROM Inventory.Warehouse WITH(NOLOCK)
                            WHERE Code = @CodeNameWarehouse;
                            IF @WarehouseId IS NULL
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'El almacen ' + @CodeNameWarehouse + ' no esta homologado en Indigo Vie' AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            --set @WarehouseId = (select Id from Inventory.Warehouse where Code = @CodeNameWarehouse)
                            IF NOT EXISTS
                            (
                                SELECT Id
                                FROM Inventory.WarehouseUser WITH(NOLOCK)
                                WHERE WarehouseId = @WarehouseId
                                      AND UserCode = @User
                            )
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'El usuario no tiene permisos para el almacen ' + @CodeNameWarehouse AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                    END;
                        ELSE
                        BEGIN
                            SELECT @Prefix = Prefix
                            FROM Inventory.Warehouse WITH(NOLOCK)
                            WHERE Id = @WarehouseId;
                    END;

                    -- Si es nuevo
                    IF @IdDevolution = 0
                        BEGIN
                            --- Obtenemos la secuencia numerica
                            IF ISNULL(@CodeDevolution, '') = ''
                                BEGIN
                                    DECLARE @idSequenceDetail INT, @pattern VARCHAR(300), @NextS BIGINT, @Scope VARCHAR(5), @IdSequence INT, @IdSequenceCommon INT;
                                    SELECT @IdSequence = Id, 
                                           @Scope = Scope, 
                                           @IdSequenceCommon = IdSequence
                                    FROM Inventory.InventorySequence WITH(NOLOCK)
                                    WHERE IdForm = @IdForm;
                                    IF @IdSequence IS NULL
                                        BEGIN
                                            SELECT '999' AS CodeMessage, 
                                                   'No existe secuencia numerica para el formulario de Devolución de Dispensación' AS Message, 
                                                   0 DevolutionId, 
                                                   CAST(3 AS TINYINT) AS [Status];
                                            RETURN;
                                    END;
                                    IF @Scope = 'O'
                                        BEGIN --- Secuencia por Prefijo
                                            SELECT @idSequenceDetail = Id
                                            FROM Inventory.InventorySequenceDetail WITH(NOLOCK)
                                            WHERE InventorySequenceId = @IdSequence
                                                  AND Prefix = @Prefix;
                                            IF @idSequenceDetail IS NULL
                                                BEGIN
                                                    INSERT INTO Inventory.InventorySequenceDetail
                                                    (InventorySequenceId, 
                                                     IdSequense, 
                                                     IdOperatingUnit, 
                                                     [Next], 
                                                     Prefix
                                                    )
                                                    VALUES
                                                    (@IdSequence, 
                                                     @IdSequenceCommon, 
                                                     NULL, 
                                                     1, 
                                                     @Prefix
                                                    );
                                                    SET @idSequenceDetail = SCOPE_IDENTITY();
                                            END;
                                            SELECT @pattern = cs.Pattern
                                            FROM Common.Sequense cs WITH(NOLOCK)
                                            WHERE cs.Id = @IdSequenceCommon;
                                    END;
                                        ELSE
                                        BEGIN -- Secuencia por Unidad operativa
                                            SELECT @pattern = cs.Pattern, 
                                                   @idSequenceDetail = psd.Id
                                            FROM Inventory.InventorySequenceDetail psd WITH(NOLOCK)
                                                 INNER JOIN Common.Sequense cs WITH(NOLOCK) ON cs.Id = psd.IdSequense
                                            WHERE psd.InventorySequenceId = @IdSequence
                                                  AND IdOperatingUnit = @OperatingUnitId;
                                    END;
                                    UPDATE Inventory.InventorySequenceDetail
                                      SET 
                                          @NextS = [Next]+=1
                                    WHERE Id = @idSequenceDetail;
                                    SELECT @CodeDevolution = dbo.GetSequence(@Prefix, @pattern, (@NextS - 1));

                                    --- Inserto en la tabla de control
                                    INSERT INTO Inventory.InventoryControlDocument
                                    (DocumentNumber, 
                                     DocumentType, 
                                     DocumentUser, 
                                     DocumentDate
                                    )
                                    VALUES
                                    (@CodeDevolution, 
                                     11, 
                                     @User, 
                                     [Common].[GETDATE]()
                                    );
                            END;
                            INSERT INTO Inventory.PharmaceuticalDispensingDevolution
                            (Code, 
                             OperatingUnitId, 
                             DocumentDate, 
                             WarehouseId, 
                             AdmissionNumber, 
                             Observation, 
                             [Status], 
                             CreationUser, 
                             CreationDate
                            )
                            VALUES
                            (@CodeDevolution, 
                             @OperatingUnitId, 
                             @DocumentDate, 
                             @WarehouseId, 
                             @AdmissionNumber, 
                             @Observation, 
                             @Status, 
                             @User, 
                             [Common].[GETDATE]()
                            );
                            SET @IdDevolution = SCOPE_IDENTITY();
                            DELETE FROM @TableDetail
                            WHERE EntityState = @StateDeleted;
                            INSERT INTO Inventory.PharmaceuticalDispensingDevolutionDetail
                            (PharmaceuticalDispensingDevolutionId, 
                             PharmaceuticalDispensingDetailBatchSerialId, 
                             Quantity
                            )
                                   SELECT @IdDevolution, 
                                          PharmaceuticalDispensingDetailBatchSerialId, 
                                          Quantity
                                   FROM @TableDetail
                                   WHERE EntityState = @StateAdded;
                    END;
                        ELSE
                        BEGIN
                            IF @Status = 3
                                BEGIN
                                    UPDATE Inventory.PharmaceuticalDispensingDevolution
                                      SET 
                                          AnnulmentUser = @User, 
                                          AnnulmentDate = [Common].[GETDATE](), 
                                          [Status] = 3
                                    WHERE Id = @IdDevolution;
                                    DELETE FROM Inventory.InventoryControlDocument
                                    WHERE DocumentNumber = @CodeDevolution
                                          AND DocumentType = @DocumentType11;
                                    SET @MessageReturn = 'Se anulo correctamente la Devolucion de Dispensacion';
                            END;
                                ELSE
                                BEGIN
                                    ---Inserto en los detalles 
                                    INSERT INTO Inventory.PharmaceuticalDispensingDevolutionDetail
                                    (PharmaceuticalDispensingDevolutionId, 
                                     PharmaceuticalDispensingDetailBatchSerialId, 
                                     Quantity
                                    )
                                           SELECT @IdDevolution, 
                                                  PharmaceuticalDispensingDetailBatchSerialId, 
                                                  Quantity
                                           FROM @TableDetail
                                           WHERE EntityState = @StateAdded;

                                    ---Actualizo los datos
                                    UPDATE Inventory.PharmaceuticalDispensingDevolutionDetail
                                      SET 
                                          PharmaceuticalDispensingDetailBatchSerialId = td.PharmaceuticalDispensingDetailBatchSerialId, 
                                          Quantity = td.Quantity
                                    FROM @TableDetail td
                                         INNER JOIN Inventory.PharmaceuticalDispensingDevolutionDetail pdd ON pdd.Id = td.Id
                                    WHERE EntityState = @StateModified;

                                    --Elimino los datos
                                    DELETE FROM Inventory.PharmaceuticalDispensingDevolutionDetail
                                    WHERE Id IN
                                    (
                                        SELECT Id
                                        FROM @TableDetail
                                        WHERE EntityState = @StateDeleted
                                    );
                                    DELETE FROM @TableDetail
                                    WHERE EntityState = @StateDeleted;

                                    -- Actualizo la cabecera de la devolucion
                                    UPDATE Inventory.PharmaceuticalDispensingDevolution
                                      SET 
                                          ModificationUser = @User, 
                                          ModificationDate = [Common].[GETDATE](), 
                                          [Status] = @Status
                                    WHERE Id = @IdDevolution;
                            END;
                    END;
                    IF @Status = 2
                        BEGIN

                            /******* CODIGO DE CONFIRMAR LA DEVOLUCION ******/

                            DECLARE @TableServiceOrderTmp TABLE
                            (RowId                INT IDENTITY(1, 1) PRIMARY KEY CLUSTERED, 
                             IdServiceOrder       INT, 
                             IdServiceOrderDetail INT, 
                             IdProduct            INT, 
                             Quantity             INT
                            );
                            --if (select count(*) from Billing.ServiceOrder where [Status] > 1 And EntityName = 'PharmaceuticalDispensing' And EntityCode in (select pd.Code from @TableDetail td inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddb on td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = pddb.PharmaceuticalDispensingDetailId inner join Inventory.PharmaceuticalDispensing pd on pd.Id = pdd.PharmaceuticalDispensingId)) > 0 begin
                            IF EXISTS
                            (
                                SELECT so.Id
                                FROM Billing.ServiceOrder so WITH(NOLOCK)
                                     INNER JOIN Inventory.PharmaceuticalDispensing pd WITH(NOLOCK) ON pd.Code = so.EntityCode
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.PharmaceuticalDispensingId = pd.Id
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddb WITH(NOLOCK) ON pddb.PharmaceuticalDispensingDetailId = pdd.Id
                                     INNER JOIN @TableDetail td ON td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
                                WHERE so.[Status] > @Uno
                                      AND so.EntityName = @EntityNamePharmaceutical
                            )
                                BEGIN
                                    DECLARE @StringServiceOrder VARCHAR(MAX)=
                                    (
                                        SELECT so.Code + ', '
                                        FROM Billing.ServiceOrder so WITH(NOLOCK)
                                             INNER JOIN Inventory.PharmaceuticalDispensing pd WITH(NOLOCK) ON pd.Code = so.EntityCode
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.PharmaceuticalDispensingId = pd.Id
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddb WITH(NOLOCK) ON pddb.PharmaceuticalDispensingDetailId = pdd.Id
                                             INNER JOIN @TableDetail td ON td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
                                        WHERE so.[Status] > @Uno
                                              AND so.EntityName = @EntityNamePharmaceutical FOR XML PATH('')
                                    );
                                    --declare @StringServiceOrder varchar(max) = (select Code + ', ' from Billing.ServiceOrder where [Status] > 1 and EntityName = 'PharmaceuticalDispensing' and EntityCode in (select pd.Code from @TableDetail td inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddb on td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = pddb.PharmaceuticalDispensingDetailId inner join Inventory.PharmaceuticalDispensing pd on pd.Id = pdd.PharmaceuticalDispensingId) for XML PATH(''))
                                    SELECT '999' AS CodeMessage, 
                                           'No se puede hacer la devolución porque las siguientes ordenes de servicio estan anuladas o facturadas: ' + @StringServiceOrder AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;

                            ---- Realizamos un recorrido para seleccionar los productos que vamos a devolver de los folios que esten activos y los productos que no esten distribuidos
                            DECLARE @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp INT, @product_cursorQuantityTmp INT;
                            DECLARE @RowId INT= 1, @Rows INT= 1;
                            WHILE @Rows > 0
                                BEGIN
                                    SELECT TOP 1 @RowId = RowId, 
                                                 @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp = PharmaceuticalDispensingDetailBatchSerialId, 
                                                 @product_cursorQuantityTmp = Quantity
                                    FROM @TableDetail
                                    WHERE Quantity > @Cero
                                          AND RowId >= @RowId
                                    ORDER BY RowId;
                                    SET @Rows = @@RowCount;
                                    IF @Rows = 0
                                        BREAK;
                                    DECLARE @IdServiceOrder_TmpQuantity INT, @IdServiceOrderDetail_TmpQuantity INT;
                                    DECLARE @ProductId_TmpQuantity INT=
                                    (
                                        SELECT TOP 1 pdd.ProductId
                                        FROM Inventory.PharmaceuticalDispensingDetailBatchSerial pddb WITH(NOLOCK)
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddb.PharmaceuticalDispensingDetailId
                                        WHERE pddb.Id = @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp
                                        ORDER BY pddb.Id
                                    );

                                    --New
                                    --Copiamos los datos a recorrer en una tabla temporal
                                    DECLARE @tmpServiceOrderPharmaceutical TABLE
                                    (RowId                                  INT IDENTITY(1, 1) PRIMARY KEY, 
                                     ServiceOrderId                         INT, 
                                     ServiceOrderDetailId                   INT, 
                                     ServiceOrderDetailDistributionQuantity INT, 
                                     FunctionalUnitCode                     VARCHAR(20) NOT NULL
                                    );
                                    DELETE FROM @tmpServiceOrderPharmaceutical;
                                    INSERT INTO @tmpServiceOrderPharmaceutical
                                           SELECT so.Id, 
                                                  sod.Id, 
                                                  sodd.Quantity, 
                                                  fu.Code
                                           FROM
                                           (
                                               SELECT pdd.ProductId
                                               FROM Inventory.PharmaceuticalDispensingDetailBatchSerial pddb WITH(NOLOCK)
                                                    INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddb.PharmaceuticalDispensingDetailId
                                               WHERE pddb.Id = @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp
                                           ) AS tmpProduct
                                           INNER JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.ProductId = tmpProduct.ProductId
                                           INNER JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) ON fu.Id = sod.[PerformsFunctionalUnitId]
                                           INNER JOIN Billing.ServiceOrder so WITH(NOLOCK) ON so.Id = sod.ServiceOrderId
                                                                                              AND so.AdmissionNumber = @AdmissionNumber
                                           INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
                                                                                                                  AND sodd.Quantity > @Cero
                                                                                                                  AND sodd.DistributionType IN(1, 4)
                                           INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
                                                                                          AND rcd.[Status] = @Uno
                                           ORDER BY so.Id, 
                                                    sod.Id, 
                                                    sodd.Quantity;
                                    DECLARE @Quantity_TmpQuantity INT;
                                    DECLARE @QuantityReal_TmpQuantity INT;
                                    WHILE @product_cursorQuantityTmp > 0
                                        BEGIN
                                            SELECT TOP 1 @IdServiceOrder_TmpQuantity = ServiceOrderId, 
                                                         @IdServiceOrderDetail_TmpQuantity = ServiceOrderDetailId, 
                                                         @Quantity_TmpQuantity = ServiceOrderDetailDistributionQuantity
                                            FROM @tmpServiceOrderPharmaceutical tmp
                                            WHERE NOT EXISTS
                                            (
                                                SELECT 1
                                                FROM @TableServiceOrderTmp
                                                WHERE IdServiceOrderDetail = tmp.ServiceOrderDetailId
                                            )
                                            ORDER BY CASE tmp.FunctionalUnitCode
                                                         WHEN LTRIM(RTRIM(@FunctionUnitCode))
                                                         THEN 1
                                                         ELSE 2
                                                     END, 
                                                     tmp.FunctionalUnitCode;
                                            SET @QuantityReal_TmpQuantity = 0;
                                            IF @IdServiceOrder_TmpQuantity IS NOT NULL
                                                BEGIN
                                                    IF @Quantity_TmpQuantity >= @product_cursorQuantityTmp
                                                        BEGIN
                                                            SET @QuantityReal_TmpQuantity = @product_cursorQuantityTmp;
                                                            SET @product_cursorQuantityTmp = 0;
                                                    END;
                                                        ELSE
                                                        BEGIN
                                                            SET @QuantityReal_TmpQuantity = @Quantity_TmpQuantity;
                                                            SET @product_cursorQuantityTmp-=@Quantity_TmpQuantity;
                                                    END;
                                                    INSERT INTO @TableServiceOrderTmp
                                                    VALUES
                                                    (@IdServiceOrder_TmpQuantity, 
                                                     @IdServiceOrderDetail_TmpQuantity, 
                                                     @ProductId_TmpQuantity, 
                                                     @QuantityReal_TmpQuantity
                                                    );
                                            END;
                                                ELSE
                                                BEGIN
                                                    SELECT '999' AS CodeMessage, 
                                                           'No se puede hacer la devolución porque el siguiente Producto no tiene cantidades en Facturacion para devolver: ' +
                                                    (
                                                        SELECT Code + ' - ' + Name
                                                        FROM Inventory.InventoryProduct
                                                        WHERE Id = @ProductId_TmpQuantity
                                                    ) AS Message, 
                                                           0 DevolutionId, 
                                                           CAST(3 AS TINYINT) AS [Status];
                                                    RETURN;
                                            END;
                    END;

                                    --Declare @Quantity_TmpQuantity int
                                    --Declare @QuantityReal_TmpQuantity int
                                    --While(@product_cursorQuantityTmp > 0)
                                    --Begin
                                    --	Set @QuantityReal_TmpQuantity = 0
                                    --	Select Top 1 @IdServiceOrder_TmpQuantity = so.Id, 
                                    --		@IdServiceOrderDetail_TmpQuantity = sod.Id, 
                                    --		@Quantity_TmpQuantity = sodd.Quantity
                                    --	From 
                                    --		(
                                    --			Select pdd.ProductId 
                                    --			From Inventory.PharmaceuticalDispensingDetailBatchSerial pddb With(Nolock)
                                    --			Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.Id = pddb.PharmaceuticalDispensingDetailId 
                                    --			Where pddb.Id = @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp
                                    --		) As tmpProduct
                                    --	Inner Join Billing.ServiceOrderDetail sod With(Nolock) on sod.ProductId = tmpProduct.ProductId 
                                    --	Inner Join Payroll.FunctionalUnit fu With(Nolock) on fu.Id = sod.[PerformsFunctionalUnitId]
                                    --	Inner Join Billing.ServiceOrder so With(Nolock) on so.Id = sod.ServiceOrderId and so.AdmissionNumber = @AdmissionNumber
                                    --	Inner Join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on sodd.ServiceOrderDetailId = sod.Id and sodd.Quantity > @Cero and sodd.DistributionType In (1,4)
                                    --	Inner Join Billing.RevenueControlDetail rcd With(Nolock) on rcd.Id = sodd.RevenueControlDetailId And rcd.[Status] = @Uno
                                    --	Where Not Exists (Select 1 From @TableServiceOrderTmp Where IdServiceOrderDetail = sod.Id)
                                    --	Order By Case fu.Code When Ltrim(Rtrim(@FunctionUnitCode)) Then 1 Else 2 End, fu.Code
                                    --	if(@IdServiceOrder_TmpQuantity is not null) begin
                                    --		if(@Quantity_TmpQuantity >= @product_cursorQuantityTmp)
                                    --		begin
                                    --			set @QuantityReal_TmpQuantity = @product_cursorQuantityTmp
                                    --			set @product_cursorQuantityTmp = 0
                                    --		end
                                    --		else begin
                                    --			set @QuantityReal_TmpQuantity = @Quantity_TmpQuantity
                                    --			set @product_cursorQuantityTmp -= @Quantity_TmpQuantity
                                    --		end
                                    --		insert into @TableServiceOrderTmp
                                    --		values(@IdServiceOrder_TmpQuantity,@IdServiceOrderDetail_TmpQuantity,@ProductId_TmpQuantity,@QuantityReal_TmpQuantity)
                                    --	end
                                    --	else begin
                                    --		select '999' as CodeMessage, 'No se puede hacer la devolución porque el siguiente Producto no tiene cantidades en Facturacion para devolver: ' + (select Code + ' - ' + Name from Inventory.InventoryProduct where Id = @ProductId_TmpQuantity)   as Message, 0 DevolutionId, cast(3 as tinyint) as [Status]
                                    --		Return
                                    --	end
                                    --end --- Fin While Cantidad

                                    SET @RowId+=1;
                    END;

                            --- Valido que en las ordenes de servicio que tengo en tmp hayan cantidades para devolver
                            DECLARE @StringItemsOrder VARCHAR(MAX)=
                            (
                                SELECT ip.Code + ', '
                                FROM
                                (
                                    SELECT so.IdServiceOrder, 
                                           so.IdProduct, 
                                           SUM(sod.InvoicedQuantity) AS InvoicedQuantity
                                    FROM @TableServiceOrderTmp so
                                         INNER JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON so.IdServiceOrder = sod.ServiceOrderId
                                                                                                   AND sod.ProductId = so.IdProduct
                                         INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
                                                                                                                AND sodd.DistributionType IN(1, 4)
                                         INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
                                                                                                     AND rcd.[Status] = @uno
                                    GROUP BY so.IdServiceOrder, 
                                             so.IdProduct
                                ) AS datos
                                INNER JOIN @TableServiceOrderTmp tstmp ON datos.IdServiceOrder = tstmp.IdServiceOrder
                                                                          AND datos.IdProduct = tstmp.IdProduct
                                INNER JOIN Inventory.InventoryProduct ip ON ip.Id = tstmp.IdProduct
                                WHERE datos.InvoicedQuantity < tstmp.Quantity FOR XML PATH('')
                            );
                            IF @StringItemsOrder IS NOT NULL
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'No se puede hacer la devolución porque los siguientes productos no tienen cantidad suficiente para devolver en facturacion: ' + @StringItemsOrder AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            DECLARE @IdServiceOrderTmp INT, @IdProductTmp INT, @QuantityTmp INT;
                            SET @RowId = 1;
                            SET @Rows = 1;
                            WHILE @Rows > 0
                                BEGIN
                                    SELECT TOP 1 @RowId = RowId, 
                                                 @IdServiceOrderTmp = IdServiceOrder, 
                                                 @IdProductTmp = IdProduct, 
                                                 @QuantityTmp = Quantity
                                    FROM @TableServiceOrderTmp
                                    WHERE RowId >= @RowId
                                    ORDER BY RowId;
                                    SET @Rows = @@RowCount;
                                    IF @Rows = 0
                                        BREAK;

                                    --Se valida que los items a devolver no esten distribuidos
                                    IF NOT EXISTS
                                    (
                                        SELECT sod.Id
                                        FROM Billing.ServiceOrderDetail sod WITH(NOLOCK)
                                             INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
                                                                                                                    AND sodd.DistributionType IN(1, 4)
                                             INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
                                                                                                         AND rcd.[Status] = @uno
                                        WHERE sod.ServiceOrderId = @IdServiceOrderTmp
                                              AND sod.ProductId = @IdProductTmp
                                    )
                                        BEGIN
                                            SELECT '999' AS CodeMessage, 
                                                   'No se encontraron items para hacer la devolución (los items distribuidos no se tienen en cuenta para devolver) Producto: ' +
                                            (
                                                SELECT Concat(Code, ' - ', [Name])
                                                FROM Inventory.InventoryProduct WITH(NOLOCK)
                                                WHERE Id = @IdProductTmp
                                            ) AS Message, 
                                                   0 DevolutionId, 
                                                   CAST(3 AS TINYINT) AS [Status];
                                            RETURN;
                                    END;
                                    DECLARE @IdServiceOrderDetailTmp INT, @InvoicedQuantityTmp INT, @IdServiceOrderDetailDistributionTmp INT;
                                    DECLARE @tmpServiceOrderDetail TABLE
                                    (RowId                            INT IDENTITY(1, 1) PRIMARY KEY CLUSTERED, 
                                     ServiceOrderDetailId             INT, 
                                     InvoicedQuantity                 INT, 
                                     ServiceOrderDetailDistributionId INT
                                    );
                                    DELETE FROM @tmpServiceOrderDetail;
                                    INSERT INTO @tmpServiceOrderDetail
                                           SELECT sod.Id, 
                                                  sod.InvoicedQuantity, 
                                                  sodd.Id
                                           FROM Billing.ServiceOrderDetail sod WITH(NOLOCK)
                                                INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
                                                                                                                       AND sodd.DistributionType IN(1, 4)
                                                INNER JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id = sodd.RevenueControlDetailId
                                                                                                            AND rcd.[Status] = @uno
                                           WHERE sod.ServiceOrderId = @IdServiceOrderTmp
                                                 AND sod.ProductId = @IdProductTmp;
                                    DECLARE @__Rows INT, @__RowId INT;
                                    SET @__RowId = 1;
                                    SET @__Rows = 1;
                                    WHILE @__Rows > 0
                                        BEGIN
                                            SELECT TOP 1 @__RowId = RowId, 
                                                         @IdServiceOrderDetailTmp = ServiceOrderDetailId, 
                                                         @InvoicedQuantityTmp = InvoicedQuantity, 
                                                         @IdServiceOrderDetailDistributionTmp = ServiceOrderDetailDistributionId
                                            FROM @tmpServiceOrderDetail
                                            WHERE RowId >= @__RowId
                                            ORDER BY RowId;
                                            SET @__Rows = @@RowCount;
                                            IF @__Rows = 0
                                                BREAK;
                                            INSERT INTO @TableServiceOrderDetailDistributionId(Id)
                                            VALUES(@IdServiceOrderDetailDistributionTmp);
                                            IF @InvoicedQuantityTmp > @QuantityTmp
                                                BEGIN
                                                    DECLARE @msgValidacion VARCHAR(MAX)=
                                                    (
                                                        SELECT Concat(ip.Code, ' - ', ip.[Name], ', ')
                                                        FROM Billing.ServiceOrderDetail sod WITH(NOLOCK)
                                                             INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON sod.ProductId = ip.Id
                                                        WHERE sod.Id = @IdServiceOrderDetailTmp
                                                              AND sod.InvoicedQuantity - @QuantityTmp < 0 FOR XML PATH('')
                                                    );
                                                    IF @msgValidacion IS NOT NULL
                                                        BEGIN
                                                            SELECT '999' AS CodeMessage, 
                                                                   'El valor de la devolución es mayor a la existente para el producto: ' + @msgValidacion AS [Message], 
                                                                   0 DevolutionId, 
                                                                   CAST(3 AS TINYINT) AS [Status];
                                                            RETURN;
                                                    END;
                                                    UPDATE Billing.ServiceOrderDetail
                                                      SET 
                                                          InvoicedQuantity-=@QuantityTmp, 
                                                          DevolutionQuantity+=@QuantityTmp, 
                                                          GrandTotalSalesPrice = TotalSalesPrice * (InvoicedQuantity - @QuantityTmp)
                                                    WHERE Id = @IdServiceOrderDetailTmp;
                                                    UPDATE Billing.ServiceOrderDetailDistribution
                                                      SET 
                                                          Quantity-=@QuantityTmp, 
                                                          GrandTotalSalesPrice = sod.GrandTotalSalesPrice, 
                                                          ThirdPartySalesPrice = sod.GrandTotalSalesPrice, 
                                                          ThirdPartyPercentage = 100, 
                                                          ApplyRecoveryFee = 1, 
                                                          RecoveryFeeType = 2, 
                                                          SubTotalPatientSalesPrice = 0, 
                                                          PatientPercentage = 0
                                                    FROM Billing.ServiceOrderDetailDistribution sodd
                                                         INNER JOIN Billing.ServiceOrderDetail sod ON sod.Id = sodd.ServiceOrderDetailId
                                                    WHERE sodd.Id = @IdServiceOrderDetailDistributionTmp;
                                                    UPDATE @tmpServiceOrderDetail
                                                      SET 
                                                          InvoicedQuantity-=@QuantityTmp
                                                    WHERE ServiceOrderDetailId = @IdServiceOrderDetailTmp;
                                            END;
                                                ELSE
                                                BEGIN
                                                    UPDATE Billing.ServiceOrderDetail
                                                      SET 
                                                          DevolutionQuantity+=InvoicedQuantity, 
                                                          @QuantityTmp-=InvoicedQuantity, 
                                                          InvoicedQuantity = 0, 
                                                          GrandTotalSalesPrice = 0
                                                    WHERE Id = @IdServiceOrderDetailTmp;
                                                    DELETE FROM Billing.ServiceOrderDetailDistribution
                                                    WHERE Id = @IdServiceOrderDetailDistributionTmp;
                                                    UPDATE @tmpServiceOrderDetail
                                                      SET 
                                                          InvoicedQuantity = 0
                                                    WHERE ServiceOrderDetailId = @IdServiceOrderDetailTmp;
                                            END;
                                            SET @__RowId+=1;
                    END;
                                    SET @RowId+=1;
                    END;

                            --- Disminuto la cantidad disponoble para devolver en la dispensacion
                            DECLARE @ProductNegative VARCHAR(MAX)=
                            (
                                SELECT DISTINCT 
                                       Concat(atc.Code, ' - ', atc.[Name], ', ')
                                FROM @TableDetail td
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddb WITH(NOLOCK) ON td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
                                     INNER JOIN Inventory.ATC atc WITH(NOLOCK) ON atc.Code = LTRIM(RTRIM(td.CodeProduct))
                                WHERE(pddb.OutstandingQuantity - td.Quantity) < 0 FOR XML PATH('')
                            );
                            IF @ProductNegative IS NOT NULL
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'Los siguientes productos ya se han devuelto: ' + @ProductNegative AS [Message], 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            UPDATE Inventory.PharmaceuticalDispensingDetailBatchSerial
                              SET 
                                  OutstandingQuantity-=td.Quantity
                            FROM @TableDetail td
                                 INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddb ON td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id;

                            --- Aumento la cantidad devuelta o retornada en la dispensacion
                            UPDATE Inventory.PharmaceuticalDispensingDetail
                              SET 
                                  ReturnedQuantity+=td.Quantity
                            FROM @TableDetail td
                                 INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddb ON td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
                                 INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddb.PharmaceuticalDispensingDetailId;

                            -- Valido que el paciente exista como tercero
	/*			if not exists(select id from Common.ThirdParty where Nit = @CodePacient) begin
					select '999' as CodeMessage, 'El paciente '+ (select IPCODPACI from dbo.ADINGRESO where NUMINGRES = @AdmissionNumber) +' no existe como Tercero en Indigo Vie' as Message, 0 DevolutionId, cast(3 as tinyint) as [Status]
					return
				end*/

                            DECLARE @PatientThirdPartyId INT=
                            (
                                SELECT Id
                                FROM Common.ThirdParty WITH(NOLOCK)
                                WHERE Nit = @CodePacient
                            );
                            IF @PatientThirdPartyId IS NULL
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'El paciente ' + @CodePacient + ' no existe como Tercero en Indigo Vie' AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;		
                            --Valido que el producto tenga costo promedio
                            --- Ahora aumento en el inventario fisico (con el mismo valor con el cual se dispenso)
                            DECLARE @KardexXml XML=
                            (
                                SELECT @PatientThirdPartyId AS ThirdPartyId, 
                                       ip.Id AS ProductId, 
                                       ph.BatchSerialId, 
                                       1 AS MovementType, 
                                       @WarehouseId AS WarehouseId, 
                                       SUM(td.Quantity) AS Quantity, 
                                       pdd.AverageCost AS Value, 
                                       1 AS AffectAverageCost
                                FROM @TableDetail AS td
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH(NOLOCK) ON pddbs.Id = td.PharmaceuticalDispensingDetailBatchSerialId
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                     INNER JOIN Inventory.PhysicalInventory ph WITH(NOLOCK) ON ph.Id = pddbs.PhysicalInventoryId
                                     INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = pdd.ProductId
                                GROUP BY ip.Id, 
                                         ph.BatchSerialId, 
                                         pdd.AverageCost FOR XML PATH('Kardex'), ELEMENTS
                            );
                            DECLARE @TableResultKardex TABLE
                            (CodeMessage VARCHAR(20), 
                             Message     VARCHAR(1000), 
                             [Status]    TINYINT
                            );
                            INSERT INTO @TableResultKardex
                            EXEC [Inventory].[SP_SavePhysicalInventoryKardex] 
                                 @KardexXml, 
                                 @IdDevolution, 
                                 @CodeDevolution, 
                                 'PharmaceuticalDispensingDevolution', 
                                 @User;
                            IF EXISTS
                            (
                                SELECT CodeMessage
                                FROM @TableResultKardex
                                WHERE [Status] = @tres
                            )
                                BEGIN
                                    SELECT CodeMessage, 
                                           Message, 
                                           0 AS DevolutionId, 
                                           [Status]
                                    FROM @TableResultKardex;
                                    RETURN;
                            END;

                            --- Actualizo las remisiones de inventario en consignación si las hubiera
                            DECLARE @RemissionXml XML=
                            (
                                SELECT td.Id AS EntityDetailId, 
                                       @OperatingUnitId AS OperatingUnitId, 
                                       pdd.FunctionalUnitId, 
                                       pdd.ProductId AS ProductId, 
                                       ph.BatchSerialId, 
                                       1 AS MovementType, 
                                       pdd.WarehouseId AS WarehouseId, 
                                       td.Quantity, 
                                       pdd.AverageCost AS Value, 
                                       pdd.PharmaceuticalDispensingId AS OriginId
                                FROM Inventory.PharmaceuticalDispensingDevolutionDetail AS td WITH(NOLOCK)
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH(NOLOCK) ON pddbs.Id = td.PharmaceuticalDispensingDetailBatchSerialId
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                     INNER JOIN Inventory.PhysicalInventory ph WITH(NOLOCK) ON ph.Id = pddbs.PhysicalInventoryId
                                     INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = pdd.ProductId
                                     INNER JOIN Inventory.Warehouse w WITH(NOLOCK) ON pdd.WarehouseId = w.Id
                                WHERE w.WarehouseConsignment = @Uno
                                      AND td.PharmaceuticalDispensingDevolutionId = @IdDevolution FOR XML PATH('Remission'), ELEMENTS
                            );
                            IF @RemissionXml IS NOT NULL
                                BEGIN
                                    DECLARE @MessageReturnRemission AS VARCHAR(MAX);
                                    EXEC [Inventory].[SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission] 
                                         @RemissionXml, 
                                         @IdDevolution, 
                                         @CodeDevolution, 
                                         'PharmaceuticalDispensingDevolution', 
                                         @User, 
                                         @MessageReturnRemission OUTPUT;
                                    IF(@MessageReturnRemission IS NOT NULL
                                       AND @MessageReturnRemission <> '')
                                        BEGIN
                                            SELECT '999' AS CodeMessage, 
                                                   @MessageReturnRemission AS [Message], 
                                                   0 AS DevolutionId, 
                                                   CAST(3 AS TINYINT) AS [Status];
                                            RETURN;
                                    END;
                            END;

                            ---Recalculo los folios que estan activos
                            DECLARE @IdRevenueControlDetailTmp INT;
                            DECLARE @TableMessageUpdateRevenue TABLE
                            (StatusResult  BIT, 
                             MessageResult VARCHAR(MAX)
                            );
                            SET @Rows = 1;
                            SET @RowId = 1;
                            DECLARE @tmpServiceOrderDetailDistribution TABLE
                            (RowId                  INT IDENTITY(1, 1) PRIMARY KEY CLUSTERED, 
                             Id                     INT, 
                             RevenueControlDetailId INT
                            );
                            DELETE FROM @tmpServiceOrderDetailDistribution;
                            INSERT INTO @tmpServiceOrderDetailDistribution
                                   SELECT DISTINCT 
                                          sodd.Id, 
                                          sodd.RevenueControlDetailId
                                   FROM @TableServiceOrderDetailDistributionId tdd
                                        INNER JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) ON sodd.Id = tdd.id;
                            WHILE @Rows > 0
                                BEGIN
                                    SELECT TOP 1 @RowId = RowId, 
                                                 @IdRevenueControlDetailTmp = RevenueControlDetailId
                                    FROM @tmpServiceOrderDetailDistribution
                                    WHERE RowId >= @RowId
                                    ORDER BY RowId;
                                    SET @Rows = @@ROWCOUNT;
                                    IF @Rows = 0
                                        BREAK;
                                    INSERT INTO @TableMessageUpdateRevenue
                                    EXEC [Billing].[SP_UpdateRevenueControlDetailValues] 
                                         @IdRevenueControlDetailTmp;
                                    SET @RowId+=1;
                    END;

                            /******* CREAMOS EL XML PARA GENERAR EL COMPROBANTE CONTABLE *******/

                            DECLARE @TableJournalVoucher TABLE
                            (Id                   INT, 
                             Consecutive          BIGINT, 
                             LegalBookId          INT, 
                             AccountingMovementId INT, 
                             IdJournalVoucher     INT, 
                             VoucherDate          VARCHAR(20), 
                             Imported             BIT, 
                             [Status]               TINYINT, 
                             Detail               VARCHAR(500), 
                             EntityCode           VARCHAR(20), 
                             EntityId             INT, 
                             EntityName           VARCHAR(250), 
                             IsClosedYear         BIT
                            );
                            --if Not Exists (select Id from Inventory.SettingInventory With(Nolock) where OperatingUnitId = @OperatingUnitId) begin
                            --	select '999' as CodeMessage, 'No existe parametros de inventarios para la unidad operativa ' + (select UnitName from Common.OperatingUnit where Id = @OperatingUnitId) as Message, 0 DevolutionId, cast(3 as tinyint) as [Status]
                            --	return
                            --end

                            DECLARE @IdLegalBook INT=
                            (
                                SELECT Id
                                FROM GeneralLedger.LegalBook WITH(NOLOCK)
                                WHERE OfficialBook = @Uno
                            );
                            IF @IdLegalBook IS NULL
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'No existe un libro oficial en el modulo de Contabilidad ' AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;

                            ---Inserto la cabecera del comprobante contable
                            INSERT INTO @TableJournalVoucher
                            (Id, 
                             Consecutive, 
                             LegalBookId, 
                             AccountingMovementId, 
                             IdJournalVoucher, 
                             VoucherDate, 
                             Imported, 
                             [Status], 
                             Detail, 
                             EntityCode, 
                             EntityId, 
                             EntityName, 
                             IsClosedYear
                            )
                            VALUES
                            (0, 
                             0, 
                             @IdLegalBook, 
                             0, 
                             @IdJournalVoucherType, 
                             @DocumentDate, 
                             0, 
                             2, 
                             'Devolucion de Dispensación ' + @CodeDevolution, 
                             @CodeDevolution, 
                             @IdDevolution, 
                             'PharmaceuticalDispensingDevolution', 
                             0
                            );
                            --- Valido que los productos de los detalles tengan grupo

                            DECLARE @StringProductNoGroup VARCHAR(MAX)=
                            (
                                SELECT ip.Code + ', '
                                FROM @TableDetail td
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH(NOLOCK) ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                     INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = pdd.ProductId
                                WHERE ProductGroupId IS NULL FOR XML PATH('')
                            );
                            IF @StringProductNoGroup IS NOT NULL
                                BEGIN
                                    --declare @StringProductNoGroup varchar(max) = (select ip.Code + ', ' from @TableDetail td 
                                    --	inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs on td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id 
                                    --	inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = pddbs.PharmaceuticalDispensingDetailId 
                                    --	inner join Inventory.InventoryProduct ip on ip.Id = pdd.ProductId where ProductGroupId is null for XML PATH(''))
                                    SELECT '999' AS CodeMessage, 
                                           'No se puede realizar la devolución debido a que los siguientes productos no poseen grupos: ' + @StringProductNoGroup AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            DECLARE @TableJournalVoucherDetail TABLE
                            (Id            INT, 
                             IdMainAccount INT, 
                             IdThirdParty  INT NULL, 
                             IdCostCenter  INT NULL, 
                             DebitValue    DECIMAL(18, 2), 
                             CreditValue   DECIMAL(18, 2), 
                             Detail        VARCHAR(500), 
                             IdRetention   INT, 
                             RetentionRate DECIMAL(5, 2), 
                             BaseValue     DECIMAL(18, 0), 
                             BillingValue  DECIMAL(18, 0)
                            );

                            /**** DETALLE DEBITO ****/
                            /*** IMPORTANTE! Las devoluciones se realizan al mismo valor por el cual se dispensaron ***/

                            ---- (inventario de almacenes en consignación)
                            INSERT INTO @TableJournalVoucherDetail
                                   SELECT 0, 
                                          ma.Id,
                                          CASE ma.HandlesThirdParty
                                              WHEN 1
                                              THEN s.IdThirdParty
                                              ELSE NULL
                                          END,
                                          CASE ma.HandlesCostCenter
                                              WHEN 1
                                              THEN CASE @AssociateCostCenter
                                                       WHEN 1
                                                       THEN f.CostCenterId
                                                       WHEN 2
                                                       THEN pg.CostCenterId
                                                       WHEN 3
                                                       THEN w.CostCenterId
                                                   END
                                              ELSE NULL
                                          END, 
                                          ROUND(pdd.AverageCost * td.Quantity, 0), 
                                          0, 
                                          'Generado desde la Devolución de Dispensacion ' + @CodeDevolution, 
                                          NULL, 
                                          NULL, 
                                          NULL, 
                                          NULL
                                   FROM @TableDetail td
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH(NOLOCK) ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                        INNER JOIN Payroll.FunctionalUnit f WITH(NOLOCK) ON f.Id = pdd.FunctionalUnitId
                                        INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = pdd.ProductId
                                        INNER JOIN Inventory.ProductGroup pg WITH(NOLOCK) ON ip.ProductGroupId = pg.Id
                                        INNER JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = pg.CounterpartCostConsignedInventoryId
                                        INNER JOIN Inventory.Warehouse w WITH(NOLOCK) ON w.Id = pdd.WarehouseId
                                        INNER JOIN Common.Supplier s WITH(NOLOCK) ON w.SupplierId = s.Id
                                   WHERE w.WarehouseConsignment = @Uno;

                            ---- (demás inventario)
                            INSERT INTO @TableJournalVoucherDetail
                                   SELECT 0, 
                                          ma.Id,
                                          CASE ma.HandlesThirdParty
                                              WHEN 1
                                              THEN CASE @PharmaceuticalDispensingGetThirdParty
                                                       WHEN 1
                                                       THEN @IdThirdPartyPacient
                                                       ELSE @PharmaceuticalDispensingThirdPartyId
                                                   END
                                              ELSE NULL
                                          END,
                                          CASE ma.HandlesCostCenter
                                              WHEN 1
                                              THEN CASE @AssociateCostCenter
                                                       WHEN 1
                                                       THEN f.CostCenterId
                                                       WHEN 2
                                                       THEN pg.CostCenterId
                                                       WHEN 3
                                                       THEN w.CostCenterId
                                                   END
                                              ELSE NULL
                                          END, 
                                          ROUND(pdd.AverageCost * td.Quantity, 0), 
                                          0, 
                                          'Generado desde la Devolución de Dispensacion ' + @CodeDevolution, 
                                          NULL, 
                                          NULL, 
                                          NULL, 
                                          NULL
                                   FROM @TableDetail td
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH(NOLOCK) ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                        INNER JOIN Payroll.FunctionalUnit f WITH(NOLOCK) ON f.Id = pdd.FunctionalUnitId
                                        INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = pdd.ProductId
                                        INNER JOIN Inventory.ProductGroup pg WITH(NOLOCK) ON ip.ProductGroupId = pg.Id
                                        INNER JOIN Payments.AccountPayableConcepts apc WITH(NOLOCK) ON apc.Id = pg.InventoryAccountPayableConceptId
                                        INNER JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON ma.Id = apc.IdAccount
                                        INNER JOIN Inventory.Warehouse w WITH(NOLOCK) ON w.Id = pdd.WarehouseId
                                   WHERE w.WarehouseConsignment <> @Uno;

                            ---- Ahora valido que la unidad funcional exista en los parametros para poder sacar la cuenta del costo
                            DECLARE @StringFunctional VARCHAR(MAX)=
                            (
                                SELECT f.Code + ' ' + f.[Name] + ', '
                                FROM @TableDetail td
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH(NOLOCK) ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                     INNER JOIN Payroll.FunctionalUnit f WITH(NOLOCK) ON f.Id = pdd.FunctionalUnitId
                                     LEFT JOIN Inventory.SettingInventoryFunctionalUnit sif WITH(NOLOCK) ON sif.FunctionalUnitId = pdd.FunctionalUnitId
                                                                                                            AND sif.SettingInventoryId = @SettingInventoryId
                                WHERE sif.Id IS NULL FOR XML PATH('')
                            );
                            IF @StringFunctional IS NOT NULL
                                BEGIN
                                    --declare @StringFunctional varchar(max) = (select f.Code + ' ' + f.Name + ', ' from @TableDetail td 
                                    --	inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs on td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id 
                                    --	inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = pddbs.PharmaceuticalDispensingDetailId 
                                    --	inner join Payroll.FunctionalUnit f on f.Id = pdd.FunctionalUnitId 
                                    --	left join Inventory.SettingInventoryFunctionalUnit sif on sif.FunctionalUnitId = pdd.FunctionalUnitId and sif.SettingInventoryId = @SettingInventoryId 
                                    --	where sif.Id is null for XML PATH(''))
                                    SELECT '999' AS CodeMessage, 
                                           'Las siguientes unidades funcionales no tienen parametrizada la cuenta del costo en parametros de Inventarios: ' + @StringFunctional AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            DECLARE @IdThirdPartyHealthAdministrator INT=
                            (
                                SELECT h.ThirdPartyId
                                FROM [Contract].HealthAdministrator h WITH(NOLOCK)
                                     INNER JOIN @tmpADINGRESO ing ON ing.GENCONENTITY = h.Id
                            );
                            IF @IdThirdPartyHealthAdministrator IS NULL
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'La entidad administradora asociada al ingreso no esta homologada en Indigo Vie' AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;

                            /**** DETALLE CREDITO ****/

                            INSERT INTO @TableJournalVoucherDetail
                                   SELECT 0, 
                                          ma.Id,
                                          CASE ma.HandlesThirdParty
                                              WHEN 1
                                              THEN CASE cg.CareGroupType
                                                       WHEN 1
                                                       THEN h.ThirdPartyId
                                                       WHEN 3
                                                       THEN @IdThirdPartyPacient
                                                       ELSE @IdThirdPartyHealthAdministrator
                                                   END
                                              ELSE NULL
                                          END AS ThirdPartyId,
                                          CASE ma.HandlesCostCenter
                                              WHEN 1
                                              THEN CASE @AssociateCostCenter
                                                       WHEN 1
                                                       THEN f.CostCenterId
                                                       WHEN 2
                                                       THEN pg.CostCenterId
                                                       WHEN 3
                                                       THEN w.CostCenterId
                                                   END
                                              ELSE NULL
                                          END AS CostCenterId, 
                                          0 AS DebitValue, 
                                          ROUND(pdd.AverageCost * td.Quantity, 0) AS CreditValue, 
                                          'Generado desde la Devolución de Dispensacion ' + @CodeDevolution AS Detail, 
                                          NULL, 
                                          NULL, 
                                          NULL, 
                                          NULL
                                   FROM @TableDetail td
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                        INNER JOIN Inventory.Warehouse w WITH(NOLOCK) ON w.Id = pdd.WarehouseId
                                        INNER JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON ip.Id = pdd.ProductId
                                        INNER JOIN Inventory.ProductGroup pg WITH(NOLOCK) ON pg.Id = ip.ProductGroupId
                                        INNER JOIN Payroll.FunctionalUnit f WITH(NOLOCK) ON f.Id = pdd.FunctionalUnitId
                                        INNER JOIN Inventory.SettingInventoryFunctionalUnit sif WITH(NOLOCK) ON sif.FunctionalUnitId = pdd.FunctionalUnitId
                                                                                                                AND sif.SettingInventoryId = @SettingInventoryId
                                        INNER JOIN Inventory.SettingInventory si WITH(NOLOCK) ON sif.SettingInventoryId = si.Id
                                        INNER JOIN Billing.SettingsBilling sb WITH(NOLOCK) ON sb.IdOperatingUnit = si.OperatingUnitId
                                        INNER JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON((sb.ApplyBasicBilling = 1
                                                                                                  AND ma.Id = pg.InventoryCostMainAccountId)
                                                                                                 OR (sb.ApplyBasicBilling = 0
                                                                                                     AND ma.Id = sif.CostAccountId))
                                        INNER JOIN Contract.CareGroup cg WITH(NOLOCK) ON cg.Id = pdd.CareGroupId
                                        LEFT JOIN Contract.[Contract] c WITH(NOLOCK) ON c.Id = cg.ContractId
                                        LEFT JOIN Contract.HealthAdministrator h WITH(NOLOCK) ON h.Id = c.HealthAdministratorId;

                            /******* Mando a ejecutar el Store Procedure de Cuentas Contables ***/

                            DECLARE @JournalVoucherXml XML=
                            (
                                SELECT *
                                FROM @TableJournalVoucher AS JournalVoucher
                                     INNER JOIN @TableJournalVoucherDetail AS JournalVoucherDetail ON JournalVoucher.Id = JournalVoucherDetail.Id FOR XML AUTO, ELEMENTS
                            );
                            DECLARE @TableResultJournal TABLE
                            (CodeMessage      VARCHAR(20), 
                             Message          VARCHAR(1000), 
                             IdJournalVoucher INT
                            );
                            INSERT INTO @TableResultJournal
                            EXEC GeneralLedger.SP_SaveJournalVoucher 
                                 @JournalVoucherXml, 
                                 @User;
                            IF EXISTS
                            (
                                SELECT 1
                                FROM @TableResultJournal
                                WHERE CodeMessage <> 0
                            )
                                BEGIN
                                    SELECT CodeMessage, 
                                           Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status]
                                    FROM @TableResultJournal;
                                    RETURN;
                            END;
                            --- Elimino el registro de la tabla de control
                            DELETE FROM Inventory.InventoryControlDocument
                            WHERE DocumentNumber = @CodeDevolution
                                  AND DocumentType = @DocumentType11;
                            --- Actualizo la auditoria basica
                            UPDATE Inventory.PharmaceuticalDispensingDevolution
                              SET 
                                  ConfirmationUser = @User, 
                                  ConfirmationDate = [Common].[GETDATE]()
                            WHERE Id = @IdDevolution;
                            SET @MessageReturn = 'Se confirmo correctamente la Devolucion ' + @CodeDevolution + ', ' +
                            (
                                SELECT [Message]
                                FROM @TableResultJournal
                                WHERE CodeMessage = 0
                            );

                            /******************** hago las operaciones necesarias para cuando se hace desde dashboard *****************/

                            IF(@IsDashBoard = 1)
                                BEGIN
                                    DECLARE @historyTypeName VARCHAR(200);
                                    IF @DevolutionOrigin = '1'
                                        BEGIN
                                            SET @historyTypeName = 'Aceptacion Devolutivo Farmacia , Origen Traslado Hospitalizacion - Usuario: ' + @User;
                                    END;
                                        ELSE
                                        IF @DevolutionOrigin = '3'
                                            BEGIN
                                                SET @historyTypeName = 'Aceptacion Devolutivo Farmacia , Origen Supencion Medicamentos (Enfermeria ) - Usuario: ' + @User;
                                        END;
                                            ELSE
                                            BEGIN
                                                SET @historyTypeName = 'Aceptacion Devolutivo Farmacia , Origen Egreso Hospitalizacion - Usuario: ' + @User;
                                        END;
                                    UPDATE dbo.HCFISIPRO
                                      SET 
                                          CANACTPRO-=td.Quantity
                                    FROM
                                    (
                                        SELECT CodeProduct, 
                                               SUM(Quantity) AS Quantity
                                        FROM @TableDetail
                                        GROUP BY CodeProduct
                                    ) td
                                    INNER JOIN dbo.HCFISIPRO ON NUMINGRES = @AdmissionNumber
                                                                AND CODCENATE = @CareCenterCode
                                                                AND UFUCODIGO = @FunctionUnitCode
                                                                AND CODPRODUC = td.CodeProduct;
                                    ---- Ahora afecto el Kardex de crystal
                                    IF NOT EXISTS
                                    (
                                        SELECT IDCONSECU
                                        FROM dbo.INCONSECU WITH(NOLOCK)
                                        WHERE IDCONSECU = @__IDCONSECU
                                    )
                                        BEGIN
                                            SELECT '999' AS CodeMessage, 
                                                   'No existe un consecutivo para el Kardex(00000011) en Indigo Crystal' AS Message, 
                                                   0 DevolutionId, 
                                                   CAST(3 AS TINYINT) AS [Status];
                                            RETURN;
                                    END;
                                    DECLARE @ConsecutiveKardex DECIMAL(18, 0);
                                    --Update dbo.INCONSECU Set @ConsecutiveKardex = CONNUMACT += 1 Where IDCONSECU = @__IDCONSECU
                                    --Update @TableDetail Set ConsecutiveKardex = @ConsecutiveKardex - 1

                                    UPDATE dbo.INCONSECU
                                      SET 
                                          @ConsecutiveKardex = CONNUMACT = CONNUMACT
                                    WHERE IDCONSECU = @__IDCONSECU;
                                    SET @ConsecutiveKardex-=1;
                                    UPDATE @TableDetail
                                      SET 
                                          @ConsecutiveKardex = ConsecutiveKardex = @ConsecutiveKardex + 1;
                                    UPDATE dbo.INCONSECU
                                      SET 
                                          CONNUMACT = @ConsecutiveKardex + 1
                                    WHERE IDCONSECU = @__IDCONSECU; --Esta lógica es muy rara para hacer algo sencillo
                                    ---- Inserto en el Kardex de Crystal
                                    INSERT INTO dbo.HCKARDPAC
                                    (NUMCONSEC, 
                                     IPCODPACI, 
                                     NUMINGRES, 
                                     CODCENATE, 
                                     UFUCODIGO, 
                                     CODPROSAL, 
                                     CODPRODUC, 
                                     CANPRODUCT, 
                                     TIPREGIST, 
                                     HCSOLINSN, 
                                     FECREGKAR, 
                                     TIPORIREG, 
                                     DESMOVPRO
                                    )
                                           SELECT ConsecutiveKardex, 
                                                  @CodePacient, 
                                                  @AdmissionNumber, 
                                                  @CareCenterCode, 
                                                  @FunctionUnitCode, 
                                                  OrderedHealthProfessionalCode, 
                                                  CodeProduct, 
                                                  Quantity, 
                                                  '2', 
                                                  '', 
                                                  [Common].[GETDATE](), 
                                                  18, 
                                                  @historyTypeName
                                           FROM @TableDetail;
                                    --- Actualizo los detalles de la devolucion de crystal

                                    UPDATE dbo.HCDEVMEDD
                                      SET 
                                          PROESTADO = CASE
                                                          WHEN(CANPENDIE - td.Quantity) = 0
                                                          THEN '2'
                                                          ELSE PROESTADO
                                                      END, 
                                          CANDEVOLV+=td.Quantity, 
                                          CANPENDIE-=td.Quantity
                                    FROM
                                    (
                                        SELECT CodeProduct, 
                                               SUM(Quantity) AS Quantity
                                        FROM @TableDetail
                                        GROUP BY CodeProduct
                                    ) td
                                    INNER JOIN dbo.HCDEVMEDD hcd WITH(NOLOCK) ON hcd.CODCONCEC = @ConsecutiveCrystal
                                                                                 AND hcd.CODPRODUC = td.CodeProduct;

                                    --consulto si la devolucion tiene detalles con cantidades
                                    DECLARE @strUno VARCHAR(1)= '1', @strDos VARCHAR(1)= '2';
                                    IF NOT EXISTS
                                    (
                                        SELECT CODCONCEC
                                        FROM dbo.HCDEVMEDD WITH(NOLOCK)
                                        WHERE CODCONCEC = @ConsecutiveCrystal
                                              AND CANPENDIE > @cero
                                              AND PROESTADO = @strUno
                                    )
                                        BEGIN
                                            --si la cantidad es pendinte es 0 actualizo el estado de la cabecera
                                            UPDATE dbo.HCDEVMEDC
                                              SET 
                                                  DEVESTADO = '2'
                                            WHERE CODCONCEC = @ConsecutiveCrystal;
                                            -- Ahora se hace el proceso de liberacion de camas
                                            IF @DevolutionOrigin <> '3'
                                               AND @DevolutionOrigin <> '4'
                                                BEGIN
                                                    IF @DevolutionOrigin = '1'
                                                        BEGIN --- Si es un traslado de cama

                                                            DECLARE @MinFecini DATETIME=
                                                            (
                                                                SELECT MIN(FECINIEST)
                                                                FROM dbo.CHREGESTA WITH(NOLOCK)
                                                                WHERE NUMINGRES = @AdmissionNumber
                                                                      AND REGESTADO = @Uno
                                                            );
                                                            DECLARE @MaxFecini DATETIME=
                                                            (
                                                                SELECT MAX(FECINIEST)
                                                                FROM dbo.CHREGESTA WITH(NOLOCK)
                                                                WHERE NUMINGRES = @AdmissionNumber
                                                                      AND REGESTADO = @Uno
                                                            );
                                                            SET @IdStayOrigin =
                                                            (
                                                                SELECT ID
                                                                FROM dbo.CHREGESTA WITH(NOLOCK)
                                                                WHERE NUMINGRES = @AdmissionNumber
                                                                      AND REGESTADO = @Uno
                                                                      AND FECINIEST = @MinFecini
                                                            );
                                                            --set @IdStayDestination = (
                                                            --	select ID from dbo.CHREGESTA  With(Nolock)
                                                            --	where NUMINGRES = @AdmissionNumber And REGESTADO = @Uno and FECINIEST = @MaxFecini
                                                            --)
                                                            DECLARE @__Feciniest DATETIME;
                                                            SELECT @IdStayDestination = ID, 
                                                                   @__Feciniest = FECINIEST, 
                                                                   @UfuCodigoDestination = uf.UFUCODIGO, 
                                                                   @CodiCamaDestination = ca.CODICAMAS
                                                            FROM dbo.CHREGESTA re WITH(NOLOCK)
                                                                 INNER JOIN dbo.CHCAMASHO ca WITH(NOLOCK) ON re.CODICAMAS = ca.CODICAMAS
                                                                 INNER JOIN dbo.INUNIFUNC uf WITH(NOLOCK) ON uf.UFUCODIGO = ca.UFUCODIGO
                                                            WHERE NUMINGRES = @AdmissionNumber
                                                                  AND REGESTADO = @Uno
                                                                  AND FECINIEST = @MaxFecini;

                                                            --- Actualizo la cama de origen para liberarla
                                                            UPDATE dbo.CHCAMASHO
                                                              SET 
                                                                  ESTADCAMA = 1, 
                                                                  CAMTRAMED = 0, 
                                                                  CODCONCEC = NULL, 
                                                                  CODAISLAM = NULL, 
                                                                  CAMTIPANO = 0, 
                                                                  CAMDEVMED = 0
                                                            FROM dbo.CHREGESTA re
                                                                 INNER JOIN dbo.CHCAMASHO ca ON re.CODICAMAS = ca.CODICAMAS
                                                            WHERE re.ID = @IdStayOrigin;
                                                            --- Actualizo la estancia de origen
                                                            --Declare @__Feciniest DateTime = (select FECINIEST from dbo.CHREGESTA With(Nolock) where ID = @IdStayDestination)
                                                            UPDATE dbo.CHREGESTA
                                                              SET 
                                                                  FECFINEST = @__Feciniest, 
                                                                  REGESTADO = 2, 
                                                                  REGDIAEST = DATEDIFF(DAY, FECINIEST, @__Feciniest)
                                                            WHERE ID = @IdStayOrigin;
                                                            --- Actualizo la cama de destino
                                                            UPDATE dbo.CHCAMASHO
                                                              SET 
                                                                  CODCONCEC = NULL
                                                            FROM dbo.CHREGESTA re
                                                                 INNER JOIN dbo.CHCAMASHO ca ON re.CODICAMAS = ca.CODICAMAS
                                                            WHERE re.ID = @IdStayDestination;
                                                            --- Actualizo el ingreso para colocarle la cama actual del paciente y la unidad funcional
                                                            --select @UfuCodigoDestination = uf.UFUCODIGO, @CodiCamaDestination = ca.CODICAMAS 
                                                            --From dbo.CHREGESTA re 
                                                            --inner join dbo.CHCAMASHO ca on re.CODICAMAS = ca.CODICAMAS 
                                                            --inner join dbo.INUNIFUNC uf on uf.UFUCODIGO = ca.UFUCODIGO 
                                                            --where re.ID = @IdStayDestination
                                                            UPDATE dbo.ADINGRESO
                                                              SET 
                                                                  UFUAACTHOS = @UfuCodigoDestination, 
                                                                  UFUACTPAC = @UfuCodigoDestination, 
                                                                  CODCAMACT = @CodiCamaDestination
                                                            WHERE NUMINGRES = @AdmissionNumber;
                                                    END;
                                                        ELSE
                                                        BEGIN --- Si es Egreso de cama
                                                            UPDATE dbo.CHCAMASHO
                                                              SET 
                                                                  ESTADCAMA = 2, 
                                                                  CAMTRAMED = 0, 
                                                                  CODCONCEC = NULL, 
                                                                  CODAISLAM = NULL, 
                                                                  CAMTIPANO = 0, 
                                                                  CAMDEVMED = 0, 
                                                                  CAMRECDEV = 1
                                                            FROM dbo.CHREGESTA re
                                                                 INNER JOIN dbo.CHCAMASHO ca ON re.CODICAMAS = ca.CODICAMAS
                                                            WHERE re.NUMINGRES = @AdmissionNumber
                                                                  AND re.REGESTADO = 1;
                                                            IF NOT EXISTS
                                                            (
                                                                SELECT NUMINGRES
                                                                FROM dbo.CHREGEGRE WITH(NOLOCK)
                                                                WHERE NUMINGRES = @AdmissionNumber
                                                                      AND FECEGRESO IS NOT NULL
                                                            )
                                                                BEGIN
                                                                    SELECT '999' AS CodeMessage, 
                                                                           'La fecha del egreso del paciente no existe' AS Message, 
                                                                           0 DevolutionId, 
                                                                           CAST(3 AS TINYINT) AS [Status];
                                                                    RETURN;
                                                            END;
                                                            SET @FechaEgresoPaciente =
                                                            (
                                                                SELECT FECEGRESO
                                                                FROM dbo.CHREGEGRE WITH(NOLOCK)
                                                                WHERE NUMINGRES = @AdmissionNumber
                                                                      AND FECEGRESO IS NOT NULL
                                                            );
                                                            --- Actualizo el estado de la estancia
                                                            --update dbo.CHREGESTA set REGESTADO = 2, FECFINEST = @FechaEgresoPaciente, REGDIAEST = DATEDIFF(DAY, FECINIEST, @FechaEgresoPaciente) where NUMINGRES = @AdmissionNumber and REGESTADO = 1
                                                    END;
                                            END;
                                    END;
                            END; --- Fin proceso de dash board
                    END --- Fin solo si se va a confirmar;
                        ELSE
                        BEGIN
                            SET @MessageReturn = 'Se guardo correctamente la Devolucion ' + @CodeDevolution;
                    END;
                    DECLARE @UserNameCreation VARCHAR(200)= '';
                    IF(@IdDevolution > 0)
                        BEGIN
                            DECLARE @userCode VARCHAR(20);
                            SELECT @userCode = CreationUser
                            FROM PharmaceuticalDispensingDevolution
                            WHERE id = @IdDevolution;
                            SELECT @UserNameCreation = ' *USERINDIGO* ' + u.UserCode + ' - ' + p.Fullname
                            FROM [Security].[User] u
                                 INNER JOIN [Security].Person p ON u.IdPerson = p.Id
                            WHERE u.UserCode = @userCode;
                    END;
                    ---- Falta devolver el mesaje con todo lo que creo
                    SELECT '0' AS CodeMessage, 
                           @MessageReturn + @UserNameCreation AS Message, 
                           @IdDevolution DevolutionId, 
                           CAST(1 AS TINYINT) AS [Status];
            END; --- Fin del if que valida si el Xml de Devoluciones viene lleno

        END TRY
        BEGIN CATCH
            SELECT CAST(ERROR_NUMBER() AS VARCHAR(10)) AS CodeMessage, 
                   'Ocurrio un error al generar la devolucion: ' + ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, 
                   0 DevolutionId, 
                   CAST(3 AS TINYINT) AS [Status];
            RETURN;
        END CATCH;
    END;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento almacenado que gestiona el registro y confirmación de devoluciones de medicamentos dispensados a pacientes hospitalizados, procesando dos flujos XML: uno para anulaciones y otro para nuevas devoluciones. Valida el estado del ingreso del paciente (no facturado ni cerrado), actualiza el kardex de Crystal (tabla `dbo.HCKARDPAC`) con movimientos tipo devolución/anulación, y mantiene el estado de las tablas de devoluciones `dbo.HCDEVMEDD` y `dbo.HCDEVMEDC`. También gestiona consecutivos de numeración e impacta inventario, facturación y contabilidad a través de sus dependencias.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_new_27Jun';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_new_27Jun';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SavePhysicalInventoryKardex; Inventory.SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission; Billing.SP_UpdateRevenueControlDetailValues; GeneralLedger.SP_SaveJournalVoucher; Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_new_27Jun';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCONSECU; dbo.HCDEVMEDD; dbo.HCDEVMEDC; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.CHREGEGRE; dbo.ADINGRESO; Common.ThirdParty; Inventory.SettingInventory; Common.OperatingUnit; Inventory.Warehouse; Inventory.WarehouseUser; Inventory.InventorySequence; Inventory.InventorySequenceDetail; Common.Sequense; Inventory.PharmaceuticalDispensingDevolution; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensing; Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.ATC; Inventory.ProductGroup; Inventory.SettingInventoryFunctionalUnit; Billing.ServiceOrder; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailDistribution; Billing.RevenueControlDetail (+12 adicionales)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_new_27Jun';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_new_27Jun';
-- GO
