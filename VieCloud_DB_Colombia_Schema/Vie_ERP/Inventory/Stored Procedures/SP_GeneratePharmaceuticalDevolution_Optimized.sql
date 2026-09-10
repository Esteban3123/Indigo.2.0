
-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 26/01/2016
-- Description:	Procedimiento almacena para guardar o confirmar las devoluciones
-- =============================================
CREATE PROCEDURE [Inventory].[SP_GeneratePharmaceuticalDevolution_Optimized] @DevolutionXml XML, 
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
        DECLARE @StateAdded VARCHAR(5)= 'Added', @StateDeleted VARCHAR(7)= 'Deleted', @StateModified VARCHAR(8)= 'Modified', @DocumentType11 TINYINT= 11;
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
                        FROM dbo.ADINGRESO ing
                             INNER JOIN @TableAnnulate ta ON ing.NUMINGRES = ta.Ingreso
                        WHERE IESTADOIN IN('F', 'C')
                    )
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'No se puede confirmar la devolución debido a que el ingreso esta facturado o cerrado' AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    ---- Ahora afecto el Kardex de crystal
                    IF NOT EXISTS
                    (
                        SELECT IDCONSECU
                        FROM dbo.INCONSECU
                        WHERE IDCONSECU = @__IDCONSECU
                    )
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'No existe un consecutivo para el Kardex(00000011) en Indigo Crystal' AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    DECLARE @ConsecutiveKardexAnnulate DECIMAL(18, 0);
                    --Actualizo para que quede el ultimo consecutvo
                    UPDATE dbo.INCONSECU
                      SET 
                          @ConsecutiveKardexAnnulate = CONNUMACT
                    WHERE IDCONSECU = @__IDCONSECU;
                    --select * from dbo.INCONSECU where IDCONSECU = '00000011'
                    SET @ConsecutiveKardexAnnulate-=1;
                    UPDATE @TableAnnulate
                      SET 
                          @ConsecutiveKardexAnnulate = ConsecutiveKardex = @ConsecutiveKardexAnnulate + 1;
                    UPDATE dbo.INCONSECU
                      SET 
                          CONNUMACT = @ConsecutiveKardexAnnulate + 1
                    WHERE IDCONSECU = @__IDCONSECU;
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
                        FROM dbo.HCDEVMEDD hc
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
                        FROM.HCDEVMEDC
                        WHERE CODCONCEC = @ConsecutiveCrystalCursor
                    );
                    IF NOT EXISTS
                    (
                        SELECT CODCONCEC
                        FROM.HCDEVMEDD
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
                                                FROM dbo.CHREGESTA
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND REGESTADO = @uno
                                            );
                                            DECLARE @MaxFECINIEST DATETIME=
                                            (
                                                SELECT MAX(FECINIEST)
                                                FROM dbo.CHREGESTA
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND REGESTADO = @uno
                                            );
                                            SET @IdStayOrigin =
                                            (
                                                SELECT ID
                                                FROM dbo.CHREGESTA
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND REGESTADO = 1
                                                      AND FECINIEST = @MinFECINIEST
                                            );
                                            SET @IdStayDestination =
                                            (
                                                SELECT ID
                                                FROM dbo.CHREGESTA
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND REGESTADO = 1
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
                                                FROM dbo.CHREGESTA
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
                                                 INNER JOIN dbo.CHCAMASHO ca ON re.CODICAMAS = ca.CODICAMAS
                                                 INNER JOIN dbo.INUNIFUNC uf ON uf.UFUCODIGO = ca.UFUCODIGO
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
                                                FROM dbo.CHREGEGRE
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
                                                FROM dbo.CHREGEGRE
                                                WHERE NUMINGRES = @AdmissionNumberCursor
                                                      AND FECEGRESO IS NOT NULL
                                            );
                                            --- Actualizo el estado de la estancia
                                            --update dbo.CHREGESTA set REGESTADO = 2, FECFINEST = @FechaEgresoPaciente, REGDIAEST = DATEDIFF(DAY, FECINIEST, @FechaEgresoPaciente) where NUMINGRES = @AdmissionNumberCursor and REGESTADO = 1
                                    END;
                            END;
                    END;

                    --******************************************************************************************************
                    IF NOT EXISTS
                    (
                        SELECT t.x.value('Id[1]', 'int')
                        FROM @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x)
                    )
                        BEGIN
                            SELECT '0' AS CodeMessage, 
                                   'Se Anulo Correctamente la Devolución' AS Message, 
                                   0 DevolutionId, 
                                   CAST(1 AS TINYINT) AS [Status];
                            RETURN;
                    END;
            END;
            IF EXISTS
            (
                SELECT t.x.value('Id[1]', 'int')
                FROM @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x)
            )
                BEGIN
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
                        FROM dbo.ADINGRESO
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
                        FROM dbo.ADINGRESO
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
                    IF NOT EXISTS
                    (
                        SELECT Id
                        FROM Common.ThirdParty
                        WHERE Nit = @CodePacient
                    )
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'El paciente ' + @CodePacient + ' no existe como tercero en Indigo Vie' AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    SET @IdThirdPartyPacient =
                    (
                        SELECT Id
                        FROM Common.ThirdParty
                        WHERE Nit = @CodePacient
                    );
                    IF NOT EXISTS
                    (
                        SELECT Id
                        FROM Inventory.SettingInventory
                        WHERE OperatingUnitId = @OperatingUnitId
                    )
                        BEGIN
                            SELECT '999' AS CodeMessage, 
                                   'No se ha creado una configuración para el modulo de inventarios' AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    IF NOT EXISTS
                    (
                        SELECT Id
                        FROM Inventory.SettingInventory
                        WHERE OperatingUnitId = @OperatingUnitId
                              AND [Year] = YEAR(@DocumentDate)
                              AND [Month] = MONTH(@DocumentDate)
                    )
                        BEGIN
                            DECLARE @DatePeriod VARCHAR(20)=
                            (
                                SELECT CAST(RIGHT('0' + [Month], 2) AS VARCHAR(2)) + '-' + CAST([Year] AS VARCHAR(4))
                                FROM Inventory.SettingInventory
                                WHERE OperatingUnitId = @OperatingUnitId
                            );
                            SELECT '999' AS CodeMessage, 
                                   'El periodo actual de inventario no coincide con la fecha del documento, Perido Actual de Inventario: ' + @DatePeriod AS Message, 
                                   0 DevolutionId, 
                                   CAST(3 AS TINYINT) AS [Status];
                            RETURN;
                    END;
                    IF ISNULL(@WarehouseId, 0) = 0
                        BEGIN --por aca entra cuando se hace desde dashboard
                            IF NOT EXISTS
                            (
                                SELECT Id
                                FROM Inventory.Warehouse
                                WHERE Code = @CodeNameWarehouse
                            )
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'El almacen ' + @CodeNameWarehouse + ' no esta homologado en Indigo Vie' AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            SET @WarehouseId =
                            (
                                SELECT Id
                                FROM Inventory.Warehouse
                                WHERE Code = @CodeNameWarehouse
                            );
                            IF NOT EXISTS
                            (
                                SELECT Id
                                FROM Inventory.WarehouseUser
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

                    -- Si es nuevo
                    IF @IdDevolution = 0
                        BEGIN
                            --- Obtenemos la secuencia numerica
                            IF @CodeDevolution IS NULL
                               OR @CodeDevolution = ''
                                BEGIN
                                    IF NOT EXISTS
                                    (
                                        SELECT Id
                                        FROM Inventory.InventorySequence
                                        WHERE IdForm = @IdForm
                                    )
                                        BEGIN
                                            SELECT '999' AS CodeMessage, 
                                                   'No existe secuencia numerica para el formulario de Devolución de Dispensación' AS Message, 
                                                   0 DevolutionId, 
                                                   CAST(3 AS TINYINT) AS [Status];
                                            RETURN;
                                    END;
                                    DECLARE @idSequenceDetail INT;
                                    DECLARE @pattern VARCHAR(300);
                                    DECLARE @NextS BIGINT;
                                    DECLARE @Scope VARCHAR(5);
                                    DECLARE @IdSequence INT;
                                    DECLARE @IdSequenceCommon INT;
                                    DECLARE @Prefix VARCHAR(20)= '';
                                    SELECT @IdSequence = Id, 
                                           @Scope = Scope, 
                                           @IdSequenceCommon = IdSequence
                                    FROM Inventory.InventorySequence
                                    WHERE IdForm = @IdForm;
                                    IF @Scope = 'O'
                                        BEGIN --- Secuencia por Prefijo
                                            SET @Prefix =
                                            (
                                                SELECT Prefix
                                                FROM Inventory.Warehouse
                                                WHERE Id = @WarehouseId
                                            );
                                            IF NOT EXISTS
                                            (
                                                SELECT Id
                                                FROM Inventory.InventorySequenceDetail
                                                WHERE InventorySequenceId = @IdSequence
                                                      AND Prefix = @Prefix
                                            )
                                                BEGIN
                                                    INSERT INTO Inventory.InventorySequenceDetail
                                                    (InventorySequenceId, 
                                                     IdSequense, 
                                                     IdOperatingUnit, 
                                                     Next, 
                                                     Prefix
                                                    )
                                                    VALUES
                                                    (@IdSequence, 
                                                     @IdSequenceCommon, 
                                                     NULL, 
                                                     1, 
                                                     @Prefix
                                                    );
                                            END;
                                            SELECT @pattern = cs.Pattern, 
                                                   @idSequenceDetail = psd.Id
                                            FROM Inventory.InventorySequenceDetail psd
                                                 INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense
                                            WHERE psd.InventorySequenceId = @IdSequence
                                                  AND psd.Prefix = @Prefix;
                                    END;
                                        ELSE
                                        BEGIN -- Secuencia por Unidad operativa
                                            SELECT @pattern = cs.Pattern, 
                                                   @idSequenceDetail = psd.Id
                                            FROM Inventory.InventorySequenceDetail psd
                                                 INNER JOIN Common.Sequense cs ON cs.Id = psd.IdSequense
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
                                FROM Billing.ServiceOrder so
                                     INNER JOIN Inventory.PharmaceuticalDispensing pd ON pd.Code = so.EntityCode
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.PharmaceuticalDispensingId = pd.Id
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddb ON pddb.PharmaceuticalDispensingDetailId = pdd.Id
                                     INNER JOIN @TableDetail td ON td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
                                WHERE so.[Status] > 1
                                      AND so.EntityName = 'PharmaceuticalDispensing'
                            )
                                BEGIN
                                    DECLARE @StringServiceOrder VARCHAR(MAX)=
                                    (
                                        SELECT so.Code + ', '
                                        FROM Billing.ServiceOrder so
                                             INNER JOIN Inventory.PharmaceuticalDispensing pd ON pd.Code = so.EntityCode
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.PharmaceuticalDispensingId = pd.Id
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddb ON pddb.PharmaceuticalDispensingDetailId = pdd.Id
                                             INNER JOIN @TableDetail td ON td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
                                        WHERE so.[Status] > 1
                                              AND so.EntityName = 'PharmaceuticalDispensing' FOR XML PATH('')
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
                                    WHERE Quantity > 0
                                          AND RowId >= @RowId
                                    ORDER BY RowId;
                                    SET @Rows = @@RowCount;
                                    IF @Rows = 0
                                        BREAK;
                                    DECLARE @IdServiceOrder_TmpQuantity INT;
                                    DECLARE @IdServiceOrderDetail_TmpQuantity INT;
                                    DECLARE @ProductId_TmpQuantity INT=
                                    (
                                        SELECT TOP 1 pdd.ProductId
                                        FROM Inventory.PharmaceuticalDispensingDetailBatchSerial pddb
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddb.PharmaceuticalDispensingDetailId
                                        WHERE pddb.Id = @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp
                                    );
                                    DECLARE @Quantity_TmpQuantity INT;
                                    DECLARE @QuantityReal_TmpQuantity INT;
                                    WHILE(@product_cursorQuantityTmp > 0)
                                        BEGIN
                                            SET @QuantityReal_TmpQuantity = 0;
                                            SELECT TOP 1 @IdServiceOrder_TmpQuantity = so.Id, 
                                                         @IdServiceOrderDetail_TmpQuantity = sod.Id, 
                                                         @Quantity_TmpQuantity = sodd.Quantity
                                            FROM
                                            (
                                                SELECT pdd.ProductId
                                                FROM Inventory.PharmaceuticalDispensingDetailBatchSerial pddb
                                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddb.PharmaceuticalDispensingDetailId
                                                WHERE pddb.Id = @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp
                                            ) AS tmpProduct
                                            INNER JOIN Billing.ServiceOrderDetail sod ON sod.ProductId = tmpProduct.ProductId
                                            INNER JOIN Payroll.FunctionalUnit fu ON fu.Id = sod.[PerformsFunctionalUnitId]
                                            INNER JOIN Billing.ServiceOrder so ON so.Id = sod.ServiceOrderId
                                                                                  AND so.AdmissionNumber = @AdmissionNumber
                                            INNER JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.ServiceOrderDetailId = sod.Id
                                                                                                      AND sodd.Quantity > 0
                                                                                                      AND sodd.DistributionType IN(1, 4)
                                            INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
                                                                                           AND rcd.[Status] = 1
                                            WHERE sod.Id NOT IN
                                            (
                                                SELECT IdServiceOrderDetail
                                                FROM @TableServiceOrderTmp
                                            )
                                            ORDER BY CASE fu.Code
                                                         WHEN LTRIM(RTRIM(@FunctionUnitCode))
                                                         THEN 1
                                                         ELSE 2
                                                     END, 
                                                     fu.Code;
                                            IF(@IdServiceOrder_TmpQuantity IS NOT NULL)
                                                BEGIN
                                                    IF(@Quantity_TmpQuantity >= @product_cursorQuantityTmp)
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
                    END; --- Fin While Cantidad

                                    SET @RowId+=1;
                    END;

                            --DECLARE product_cursor CURSOR FOR 
                            --SELECT PharmaceuticalDispensingDetailBatchSerialId, Quantity from @TableDetail where Quantity > 0
                            --OPEN product_cursor
                            --FETCH NEXT FROM product_cursor 
                            --INTO @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp, @product_cursorQuantityTmp
                            --WHILE @@FETCH_STATUS = 0
                            --BEGIN
                            --	declare @IdServiceOrder_TmpQuantity int
                            --	declare @IdServiceOrderDetail_TmpQuantity int
                            --	declare @ProductId_TmpQuantity int = (select top 1 pdd.ProductId from Inventory.PharmaceuticalDispensingDetailBatchSerial pddb inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = pddb.PharmaceuticalDispensingDetailId where pddb.Id = @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp)
                            --	declare @Quantity_TmpQuantity int
                            --	declare @QuantityReal_TmpQuantity int
                            --	while(@product_cursorQuantityTmp > 0)
                            --	begin
                            --		set @QuantityReal_TmpQuantity = 0
                            --		select top 1 @IdServiceOrder_TmpQuantity = so.Id, @IdServiceOrderDetail_TmpQuantity=sod.Id, @Quantity_TmpQuantity = sodd.Quantity
                            --		from (select pdd.ProductId from Inventory.PharmaceuticalDispensingDetailBatchSerial pddb
                            --		inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = pddb.PharmaceuticalDispensingDetailId where pddb.Id = @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp) as tmpProduct
                            --		inner join Billing.ServiceOrderDetail sod on sod.ProductId = tmpProduct.ProductId 
                            --		inner join Payroll.FunctionalUnit fu on fu.Id = sod.[PerformsFunctionalUnitId]
                            --		inner join Billing.ServiceOrder so on so.Id = sod.ServiceOrderId and so.AdmissionNumber = @AdmissionNumber
                            --		inner join Billing.ServiceOrderDetailDistribution sodd on sodd.ServiceOrderDetailId = sod.Id and sodd.Quantity > 0 and sodd.DistributionType in (1,4)
                            --		inner join Billing.RevenueControlDetail rcd on rcd.Id = sodd.RevenueControlDetailId and rcd.Status = 1
                            --		where sod.Id not in (select IdServiceOrderDetail from @TableServiceOrderTmp)
                            --		order by case fu.Code when ltrim(rtrim(@FunctionUnitCode)) then 1 else 2 end, fu.Code
                            --		if(@IdServiceOrder_TmpQuantity is not null) begin
                            --			if(@Quantity_TmpQuantity >= @product_cursorQuantityTmp)
                            --			begin
                            --				set @QuantityReal_TmpQuantity = @product_cursorQuantityTmp
                            --				set @product_cursorQuantityTmp = 0
                            --			end
                            --			else begin
                            --				set @QuantityReal_TmpQuantity = @Quantity_TmpQuantity
                            --				set @product_cursorQuantityTmp -= @Quantity_TmpQuantity
                            --			end
                            --			insert into @TableServiceOrderTmp
                            --			values(@IdServiceOrder_TmpQuantity,@IdServiceOrderDetail_TmpQuantity,@ProductId_TmpQuantity,@QuantityReal_TmpQuantity)
                            --		end
                            --		else begin
                            --			select '999' as CodeMessage, 'No se puede hacer la devolución porque el siguiente Producto no tiene cantidades en Facturacion para devolver: ' + (select Code + ' - ' + Name from Inventory.InventoryProduct where Id = @ProductId_TmpQuantity)   as Message, 0 DevolutionId, cast(3 as tinyint) as [Status]
                            --			return
                            --		end
                            --	end --- Fin While Cantidad
                            --	FETCH NEXT FROM product_cursor 
                            --	INTO @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp, @product_cursorQuantityTmp
                            --END 
                            --CLOSE product_cursor;
                            --DEALLOCATE product_cursor;
                            --- Valido que en las ordenes de servicio que tengo en tmp hayan cantidades para devolver
                            IF EXISTS
                            (
                                SELECT datos.IdServiceOrder
                                FROM
                                (
                                    SELECT so.IdServiceOrder, 
                                           so.IdProduct, 
                                           SUM(sod.InvoicedQuantity) AS InvoicedQuantity
                                    FROM @TableServiceOrderTmp so
                                         INNER JOIN Billing.ServiceOrderDetail sod ON so.IdServiceOrder = sod.ServiceOrderId
                                                                                      AND sod.ProductId = so.IdProduct
                                         INNER JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.ServiceOrderDetailId = sod.Id
                                                                                                   AND sodd.DistributionType IN(1, 4)
                                         INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
                                                                                        AND rcd.[Status] = @uno
                                    GROUP BY so.IdServiceOrder, 
                                             so.IdProduct
                                ) AS datos
                                INNER JOIN @TableServiceOrderTmp tstmp ON datos.IdServiceOrder = tstmp.IdServiceOrder
                                                                          AND datos.IdProduct = tstmp.IdProduct
                                WHERE datos.InvoicedQuantity < tstmp.Quantity
                            )
                                BEGIN
                                    DECLARE @StringItemsOrder VARCHAR(MAX)=
                                    (
                                        SELECT ip.Code + ', '
                                        FROM
                                        (
                                            SELECT so.IdServiceOrder, 
                                                   so.IdProduct, 
                                                   SUM(sod.InvoicedQuantity) AS InvoicedQuantity
                                            FROM @TableServiceOrderTmp so
                                                 INNER JOIN Billing.ServiceOrderDetail sod ON so.IdServiceOrder = sod.ServiceOrderId
                                                                                              AND sod.ProductId = so.IdProduct
                                                 INNER JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.ServiceOrderDetailId = sod.Id
                                                                                                           AND sodd.DistributionType IN(1, 4)
                                                 INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
                                                                                                AND rcd.[Status] = @uno
                                            GROUP BY so.IdServiceOrder, 
                                                     so.IdProduct
                                        ) AS datos
                                        INNER JOIN @TableServiceOrderTmp tstmp ON datos.IdServiceOrder = tstmp.IdServiceOrder
                                                                                  AND datos.IdProduct = tstmp.IdProduct
                                        INNER JOIN Inventory.InventoryProduct ip ON ip.Id = tstmp.IdProduct
                                        WHERE datos.InvoicedQuantity < tstmp.Quantity FOR XML PATH('')
                                    );
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
                                        FROM Billing.ServiceOrderDetail sod
                                             INNER JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.ServiceOrderDetailId = sod.Id
                                                                                                       AND sodd.DistributionType IN(1, 4)
                                             INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
                                                                                            AND rcd.[Status] = @uno
                                        WHERE sod.ServiceOrderId = @IdServiceOrderTmp
                                              AND sod.ProductId = @IdProductTmp
                                    )
                                        BEGIN
                                            SELECT '999' AS CodeMessage, 
                                                   'No se encontraron items para hacer la devolución (los items distribuidos no se tienen en cuenta para devolver) Producto: ' +
                                            (
                                                SELECT Code + ' - ' + Name
                                                FROM Inventory.InventoryProduct
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
                                    INSERT INTO @tmpServiceOrderDetail
                                           SELECT sod.Id, 
                                                  sod.InvoicedQuantity, 
                                                  sodd.Id
                                           FROM Billing.ServiceOrderDetail sod
                                                INNER JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.ServiceOrderDetailId = sod.Id
                                                                                                          AND sodd.DistributionType IN(1, 4)
                                                INNER JOIN Billing.RevenueControlDetail rcd ON rcd.Id = sodd.RevenueControlDetailId
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
                                            END;
                                            SET @__RowId+=1;
                    END;

                                    --DECLARE Detailquantity_cursor CURSOR FOR 
                                    --select sod.Id, sod.InvoicedQuantity, sodd.Id 
                                    --from Billing.ServiceOrderDetail sod 
                                    --inner join Billing.ServiceOrderDetailDistribution sodd on sodd.ServiceOrderDetailId = sod.Id and sodd.DistributionType in (1,4) 
                                    --inner join Billing.RevenueControlDetail rcd on rcd.Id = sodd.RevenueControlDetailId and rcd.Status = 1 
                                    --where sod.ServiceOrderId = @IdServiceOrderTmp and sod.ProductId = @IdProductTmp
                                    --OPEN Detailquantity_cursor
                                    --FETCH NEXT FROM Detailquantity_cursor 
                                    --INTO @IdServiceOrderDetailTmp, @InvoicedQuantityTmp, @IdServiceOrderDetailDistributionTmp
                                    --WHILE @@FETCH_STATUS = 0
                                    --BEGIN
                                    --	insert into @TableServiceOrderDetailDistributionId(Id) values(@IdServiceOrderDetailDistributionTmp)
                                    --	print '@QuantityTmp =' +cast(@QuantityTmp as varchar(20)) --+ ' @InvoicedQuantityTmp = '-- +cast(isnull( @InvoicedQuantityTmp,0) as varchar(20))
                                    --	if @InvoicedQuantityTmp > @QuantityTmp begin
                                    --		update Billing.ServiceOrderDetail set InvoicedQuantity -= @QuantityTmp, DevolutionQuantity += @QuantityTmp, GrandTotalSalesPrice = TotalSalesPrice * (InvoicedQuantity - @QuantityTmp) where Id = @IdServiceOrderDetailTmp
                                    --		update Billing.ServiceOrderDetailDistribution set Quantity -= @QuantityTmp, GrandTotalSalesPrice = sod.GrandTotalSalesPrice, ThirdPartySalesPrice = sod.GrandTotalSalesPrice, ThirdPartyPercentage = 100, ApplyRecoveryFee = 1, RecoveryFeeType = 2, SubTotalPatientSalesPrice = 0, PatientPercentage = 0
                                    --		from Billing.ServiceOrderDetailDistribution sodd
                                    --		inner join Billing.ServiceOrderDetail sod on sod.Id = sodd.ServiceOrderDetailId
                                    --		where sodd.Id = @IdServiceOrderDetailDistributionTmp
                                    --	end
                                    --	else begin
                                    --		update Billing.ServiceOrderDetail set DevolutionQuantity += InvoicedQuantity, @QuantityTmp -= InvoicedQuantity, InvoicedQuantity = 0, GrandTotalSalesPrice = 0  where Id = @IdServiceOrderDetailTmp
                                    --		delete from Billing.ServiceOrderDetailDistribution  where Id = @IdServiceOrderDetailDistributionTmp
                                    --	end
                                    --	FETCH NEXT FROM Detailquantity_cursor
                                    --	INTO @IdServiceOrderDetailTmp, @InvoicedQuantityTmp, @IdServiceOrderDetailDistributionTmp
                                    --END 
                                    --CLOSE Detailquantity_cursor
                                    --DEALLOCATE Detailquantity_cursor

                                    SET @RowId+=1;
                    END;

                            --DECLARE serviceDetail_cursor CURSOR FOR 
                            --SELECT IdServiceOrder, IdProduct, Quantity FROM @TableServiceOrderTmp --- Recorro los productos que tengo que descontar en cada orden de servicio
                            --OPEN serviceDetail_cursor
                            --FETCH NEXT FROM serviceDetail_cursor 
                            --INTO @IdServiceOrderTmp, @IdProductTmp, @QuantityTmp
                            --WHILE @@FETCH_STATUS = 0
                            --BEGIN
                            --	--Se valida que los items a devolver no esten distribuidos
                            --	if(select COUNT(*) from Billing.ServiceOrderDetail sod inner join Billing.ServiceOrderDetailDistribution sodd on sodd.ServiceOrderDetailId = sod.Id and sodd.DistributionType in (1,4) inner join Billing.RevenueControlDetail rcd on rcd.Id = sodd.RevenueControlDetailId and rcd.Status = 1 where sod.ServiceOrderId = @IdServiceOrderTmp and sod.ProductId = @IdProductTmp) =0 begin
                            --		select '999' as CodeMessage, 'No se encontraron items para hacer la devolución (los items distribuidos no se tienen en cuenta para devolver) Producto: ' + (select Code + ' - ' + Name from Inventory.InventoryProduct where Id = @IdProductTmp) as Message, 0 DevolutionId, cast(3 as tinyint) as [Status]
                            --		return
                            --	end
                            --	declare @IdServiceOrderDetailTmp int, @InvoicedQuantityTmp int, @IdServiceOrderDetailDistributionTmp int
                            --	DECLARE Detailquantity_cursor CURSOR FOR 
                            --	select sod.Id, sod.InvoicedQuantity, sodd.Id from Billing.ServiceOrderDetail sod inner join Billing.ServiceOrderDetailDistribution sodd on sodd.ServiceOrderDetailId = sod.Id and sodd.DistributionType in (1,4) inner join Billing.RevenueControlDetail rcd on rcd.Id = sodd.RevenueControlDetailId and rcd.Status = 1 where sod.ServiceOrderId = @IdServiceOrderTmp and sod.ProductId = @IdProductTmp
                            --	OPEN Detailquantity_cursor
                            --	FETCH NEXT FROM Detailquantity_cursor 
                            --	INTO @IdServiceOrderDetailTmp, @InvoicedQuantityTmp, @IdServiceOrderDetailDistributionTmp
                            --	WHILE @@FETCH_STATUS = 0
                            --	BEGIN
                            --		insert into @TableServiceOrderDetailDistributionId(Id) values(@IdServiceOrderDetailDistributionTmp)
                            --		print '@QuantityTmp =' +cast(@QuantityTmp as varchar(20)) --+ ' @InvoicedQuantityTmp = '-- +cast(isnull( @InvoicedQuantityTmp,0) as varchar(20))
                            --		if @InvoicedQuantityTmp > @QuantityTmp begin
                            --			update Billing.ServiceOrderDetail set InvoicedQuantity -= @QuantityTmp, DevolutionQuantity += @QuantityTmp, GrandTotalSalesPrice = TotalSalesPrice * (InvoicedQuantity - @QuantityTmp) where Id = @IdServiceOrderDetailTmp
                            --			update Billing.ServiceOrderDetailDistribution set Quantity -= @QuantityTmp, GrandTotalSalesPrice = sod.GrandTotalSalesPrice, ThirdPartySalesPrice = sod.GrandTotalSalesPrice, ThirdPartyPercentage = 100, ApplyRecoveryFee = 1, RecoveryFeeType = 2, SubTotalPatientSalesPrice = 0, PatientPercentage = 0
                            --			from Billing.ServiceOrderDetailDistribution sodd
                            --			inner join Billing.ServiceOrderDetail sod on sod.Id = sodd.ServiceOrderDetailId
                            --			where sodd.Id = @IdServiceOrderDetailDistributionTmp
                            --		end
                            --		else begin
                            --			update Billing.ServiceOrderDetail set DevolutionQuantity += InvoicedQuantity, @QuantityTmp -= InvoicedQuantity, InvoicedQuantity = 0, GrandTotalSalesPrice = 0  where Id = @IdServiceOrderDetailTmp
                            --			delete from Billing.ServiceOrderDetailDistribution  where Id = @IdServiceOrderDetailDistributionTmp
                            --		end
                            --		FETCH NEXT FROM Detailquantity_cursor
                            --		INTO @IdServiceOrderDetailTmp, @InvoicedQuantityTmp, @IdServiceOrderDetailDistributionTmp
                            --	END 
                            --	CLOSE Detailquantity_cursor
                            --	DEALLOCATE Detailquantity_cursor
                            --	FETCH NEXT FROM serviceDetail_cursor
                            --	INTO @IdServiceOrderTmp, @IdProductTmp, @QuantityTmp
                            --END 
                            --CLOSE serviceDetail_cursor
                            --DEALLOCATE serviceDetail_cursor
                            --- Disminuto la cantidad disponoble para devolver en la dispensacion
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

                            IF NOT EXISTS
                            (
                                SELECT Id
                                FROM Common.ThirdParty
                                WHERE Nit = @CodePacient
                            )
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'El paciente ' +
                                    (
                                        SELECT IPCODPACI
                                        FROM dbo.ADINGRESO
                                        WHERE NUMINGRES = @AdmissionNumber
                                    ) + ' no existe como Tercero en Indigo Vie' AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            DECLARE @PatientThirdPartyId INT=
                            (
                                SELECT Id
                                FROM Common.ThirdParty
                                WHERE Nit =
                                (
                                    SELECT IPCODPACI
                                    FROM dbo.ADINGRESO
                                    WHERE NUMINGRES = @AdmissionNumber
                                )
                            );

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
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pddbs.Id = td.PharmaceuticalDispensingDetailBatchSerialId
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                     INNER JOIN Inventory.PhysicalInventory ph ON ph.Id = pddbs.PhysicalInventoryId
                                     INNER JOIN Inventory.InventoryProduct ip ON ip.Id = pdd.ProductId
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
                                FROM Inventory.PharmaceuticalDispensingDevolutionDetail AS td
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pddbs.Id = td.PharmaceuticalDispensingDetailBatchSerialId
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                     INNER JOIN Inventory.PhysicalInventory ph ON ph.Id = pddbs.PhysicalInventoryId
                                     INNER JOIN Inventory.InventoryProduct ip ON ip.Id = pdd.ProductId
                                     INNER JOIN Inventory.Warehouse w ON pdd.WarehouseId = w.Id
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
                            INSERT INTO @tmpServiceOrderDetailDistribution
                                   SELECT DISTINCT 
                                          sodd.Id, 
                                          sodd.RevenueControlDetailId
                                   FROM @TableServiceOrderDetailDistributionId tdd
                                        INNER JOIN Billing.ServiceOrderDetailDistribution sodd ON sodd.Id = tdd.id;
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
                                         @IdRevenueControlDetailTmp, 
                                         @OperatingUnitId;
                                    SET @RowId+=1;
                    END;

                            --DECLARE updateRevenueDetail_cursor CURSOR FOR 
                            --select distinct sodd.RevenueControlDetailId from @TableServiceOrderDetailDistributionId tdd inner join Billing.ServiceOrderDetailDistribution sodd on sodd.Id = tdd.id
                            --OPEN updateRevenueDetail_cursor
                            --FETCH NEXT FROM updateRevenueDetail_cursor 
                            --INTO @IdRevenueControlDetailTmp
                            --WHILE @@FETCH_STATUS = 0
                            --BEGIN
                            --	insert into @TableMessageUpdateRevenue
                            --	exec [Billing].[SP_UpdateRevenueControlDetailValues] @IdRevenueControlDetailTmp, @OperatingUnitId
                            --	FETCH NEXT FROM updateRevenueDetail_cursor
                            --	INTO @IdRevenueControlDetailTmp
                            --END 
                            --CLOSE updateRevenueDetail_cursor
                            --DEALLOCATE updateRevenueDetail_cursor

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
                            IF NOT EXISTS
                            (
                                SELECT Id
                                FROM Inventory.SettingInventory
                                WHERE OperatingUnitId = @OperatingUnitId
                            )
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'No existe parametros de inventarios para la unidad operativa ' +
                                    (
                                        SELECT UnitName
                                        FROM Common.OperatingUnit
                                        WHERE Id = @OperatingUnitId
                                    ) AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            IF NOT EXISTS
                            (
                                SELECT Id
                                FROM GeneralLedger.LegalBook
                                WHERE OfficialBook = @Uno
                            )
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'No existe un libro oficial en el modulo de Contabilidad ' AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            DECLARE @SettingInventoryId INT, @IdJournalVoucherType INT, @PharmaceuticalDispensingGetThirdParty TINYINT, @PharmaceuticalDispensingThirdPartyId INT, @AssociateCostCenter TINYINT, @DiscountSalesMainAccountId INT;
                            SELECT @SettingInventoryId = Id, 
                                   @IdJournalVoucherType = SalesReturnJournalVoucherTypeId, 
                                   @PharmaceuticalDispensingGetThirdParty = PharmaceuticalDispensingGetThirdParty, 
                                   @PharmaceuticalDispensingThirdPartyId = PharmaceuticalDispensingThirdPartyId, 
                                   @AssociateCostCenter = AssociateCostCenter, 
                                   @DiscountSalesMainAccountId = DiscountSalesMainAccountId
                            FROM Inventory.SettingInventory
                            WHERE OperatingUnitId = @OperatingUnitId;
                            DECLARE @IdLegalBook INT=
                            (
                                SELECT Id
                                FROM GeneralLedger.LegalBook
                                WHERE OfficialBook = @Uno
                            );
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
                            IF EXISTS
                            (
                                SELECT td.PharmaceuticalDispensingDetailBatchSerialId
                                FROM @TableDetail td
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                     INNER JOIN Inventory.InventoryProduct ip ON ip.Id = pdd.ProductId
                                WHERE ProductGroupId IS NULL
                            )
                                BEGIN
                                    DECLARE @StringProductNoGroup VARCHAR(MAX)=
                                    (
                                        SELECT ip.Code + ', '
                                        FROM @TableDetail td
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                             INNER JOIN Inventory.InventoryProduct ip ON ip.Id = pdd.ProductId
                                        WHERE ProductGroupId IS NULL FOR XML PATH('')
                                    );
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
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                        INNER JOIN Payroll.FunctionalUnit f ON f.Id = pdd.FunctionalUnitId
                                        INNER JOIN Inventory.InventoryProduct ip ON ip.Id = pdd.ProductId
                                        INNER JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
                                        INNER JOIN GeneralLedger.MainAccounts ma ON ma.Id = pg.CounterpartCostConsignedInventoryId
                                        INNER JOIN Inventory.Warehouse w ON w.Id = pdd.WarehouseId
                                        INNER JOIN Common.Supplier s ON w.SupplierId = s.Id
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
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                        INNER JOIN Payroll.FunctionalUnit f ON f.Id = pdd.FunctionalUnitId
                                        INNER JOIN Inventory.InventoryProduct ip ON ip.Id = pdd.ProductId
                                        INNER JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
                                        INNER JOIN Payments.AccountPayableConcepts apc ON apc.Id = pg.InventoryAccountPayableConceptId
                                        INNER JOIN GeneralLedger.MainAccounts ma ON ma.Id = apc.IdAccount
                                        INNER JOIN Inventory.Warehouse w ON w.Id = pdd.WarehouseId
                                   WHERE w.WarehouseConsignment <> @Uno;

                            ---- Ahora valido que la unidad funcional exista en los parametros para poder sacar la cuenta del costo
                            IF EXISTS
                            (
                                SELECT td.PharmaceuticalDispensingDetailBatchSerialId
                                FROM @TableDetail td
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                     INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                     LEFT JOIN Inventory.SettingInventoryFunctionalUnit sif ON sif.FunctionalUnitId = pdd.FunctionalUnitId
                                                                                               AND sif.SettingInventoryId = @SettingInventoryId
                                WHERE sif.Id IS NULL
                            )
                                BEGIN
                                    DECLARE @StringFunctional VARCHAR(MAX)=
                                    (
                                        SELECT f.Code + ' ' + f.Name + ', '
                                        FROM @TableDetail td
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                                             INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                             INNER JOIN Payroll.FunctionalUnit f ON f.Id = pdd.FunctionalUnitId
                                             LEFT JOIN Inventory.SettingInventoryFunctionalUnit sif ON sif.FunctionalUnitId = pdd.FunctionalUnitId
                                                                                                       AND sif.SettingInventoryId = @SettingInventoryId
                                        WHERE sif.Id IS NULL FOR XML PATH('')
                                    );
                                    SELECT '999' AS CodeMessage, 
                                           'Las siguientes unidades funcionales no tienen parametrizada la cuenta del costo en parametros de Inventarios: ' + @StringFunctional AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            IF NOT EXISTS
                            (
                                SELECT t.Id
                                FROM Common.ThirdParty t
                                     INNER JOIN [Contract].HealthAdministrator h ON h.ThirdPartyId = t.Id
                                     INNER JOIN dbo.ADINGRESO ing ON ing.GENCONENTITY = h.Id
                                WHERE ing.NUMINGRES = @AdmissionNumber
                            )
                                BEGIN
                                    SELECT '999' AS CodeMessage, 
                                           'La entidad administradora asociada al ingreso no esta homologada en Indigo Vie' AS Message, 
                                           0 DevolutionId, 
                                           CAST(3 AS TINYINT) AS [Status];
                                    RETURN;
                            END;
                            DECLARE @IdThirdPartyHealthAdministrator INT=
                            (
                                SELECT h.ThirdPartyId
                                FROM Contract.HealthAdministrator h
                                     INNER JOIN dbo.ADINGRESO ing ON ing.GENCONENTITY = h.Id
                                WHERE ing.NUMINGRES = @AdmissionNumber
                            );

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
                                        INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                                        INNER JOIN Inventory.Warehouse w ON w.Id = pdd.WarehouseId
                                        INNER JOIN Inventory.InventoryProduct ip ON ip.Id = pdd.ProductId
                                        INNER JOIN Inventory.ProductGroup pg ON pg.Id = ip.ProductGroupId
                                        INNER JOIN Payroll.FunctionalUnit f ON f.Id = pdd.FunctionalUnitId
                                        INNER JOIN Inventory.SettingInventoryFunctionalUnit sif ON sif.FunctionalUnitId = pdd.FunctionalUnitId
                                                                                                   AND sif.SettingInventoryId = @SettingInventoryId
                                        INNER JOIN Inventory.SettingInventory si ON sif.SettingInventoryId = si.Id
                                        INNER JOIN Billing.SettingsBilling sb ON sb.IdOperatingUnit = si.OperatingUnitId
                                        INNER JOIN GeneralLedger.MainAccounts ma ON((sb.ApplyBasicBilling = 1
                                                                                     AND ma.Id = pg.InventoryCostMainAccountId)
                                                                                    OR (sb.ApplyBasicBilling = 0
                                                                                        AND ma.Id = sif.CostAccountId))
                                        INNER JOIN Contract.CareGroup cg ON cg.Id = pdd.CareGroupId
                                        LEFT JOIN Contract.Contract c ON c.Id = cg.ContractId
                                        LEFT JOIN Contract.HealthAdministrator h ON h.Id = c.HealthAdministratorId;

                            ---- Ahora verifico si cuando se hizo la dispensacion se le realizo descuento para hacer la devolucion del descuento tambien
                            --if(select count(*) from @TableDetail td inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs on td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = pddbs.PharmaceuticalDispensingDetailId where pdd.DiscountValue > 0 ) > 0 begin
                            --	declare @DisccountHandlesThirdParty bit, @DisccountHandlesCostCenter bit
                            --	select @DisccountHandlesThirdParty = HandlesThirdParty, @DisccountHandlesCostCenter = HandlesCostCenter from GeneralLedger.MainAccounts where Id = @DiscountSalesMainAccountId
                            --	--- inserto los detalles de descuento
                            --	--insert into @TableJournalVoucherDetail
                            --	--select 0, @DiscountSalesMainAccountId, case @DisccountHandlesThirdParty when 1 then case cg.CareGroupType when 1 then h.ThirdPartyId when 3 then @IdThirdPartyPacient else @IdThirdPartyHealthAdministrator end else null end as ThirdPartyId
                            --	--,case @DisccountHandlesCostCenter when 1 then case @AssociateCostCenter when 1 then f.CostCenterId when 2 then pg.CostCenterId when 3 then w.CostCenterId end else null end as CostCenterId
                            --	--,0 as DebitValue, ROUND(pdd.DiscountValue * td.Quantity,0) as CreditValue, '' as Detail, null, null, null, null
                            --	--from @TableDetail td inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs on td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
                            --	--inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = pddbs.PharmaceuticalDispensingDetailId
                            --	--inner join Inventory.Warehouse w on w.Id = pdd.WarehouseId
                            --	--inner join Inventory.InventoryProduct ip on ip.Id = pdd.ProductId
                            --	--inner join Inventory.ProductGroup pg on pg.Id = ip.ProductGroupId
                            --	--inner join Payroll.FunctionalUnit f on f.Id = pdd.FunctionalUnitId
                            --	--inner join Contract.CareGroup cg on cg.Id = pdd.CareGroupId
                            --	--left join Contract.Contract c on c.Id = cg.ContractId
                            --	--left join Contract.HealthAdministrator h on h.Id = c.HealthAdministratorId
                            --	--where pdd.DiscountValue > 0
                            --end
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
                            IF
                            (
                                SELECT COUNT(*)
                                FROM @TableResultJournal
                                WHERE CodeMessage <> 0
                            ) > 0
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
                                SELECT Message
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
                                        FROM dbo.INCONSECU
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
                                    WHERE IDCONSECU = @__IDCONSECU;
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
                                    INNER JOIN dbo.HCDEVMEDD hcd ON hcd.CODCONCEC = @ConsecutiveCrystal
                                                                    AND hcd.CODPRODUC = td.CodeProduct;

                                    --consulto si la devolucion tiene detalles con cantidades
                                    DECLARE @strUno VARCHAR(1)= '1', @strDos VARCHAR(1)= '2';
                                    IF NOT EXISTS
                                    (
                                        SELECT CODCONCEC
                                        FROM dbo.HCDEVMEDD
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
                                                                FROM dbo.CHREGESTA
                                                                WHERE NUMINGRES = @AdmissionNumber
                                                                      AND REGESTADO = @Uno
                                                            );
                                                            DECLARE @MaxFecini DATETIME=
                                                            (
                                                                SELECT MAX(FECINIEST)
                                                                FROM dbo.CHREGESTA
                                                                WHERE NUMINGRES = @AdmissionNumber
                                                                      AND REGESTADO = @Uno
                                                            );
                                                            SET @IdStayOrigin =
                                                            (
                                                                SELECT ID
                                                                FROM dbo.CHREGESTA
                                                                WHERE NUMINGRES = @AdmissionNumber
                                                                      AND REGESTADO = @Uno
                                                                      AND FECINIEST = @MinFecini
                                                            );
                                                            SET @IdStayDestination =
                                                            (
                                                                SELECT ID
                                                                FROM dbo.CHREGESTA
                                                                WHERE NUMINGRES = @AdmissionNumber
                                                                      AND REGESTADO = @Uno
                                                                      AND FECINIEST = @MaxFecini
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
                                                            DECLARE @__Feciniest DATETIME=
                                                            (
                                                                SELECT FECINIEST
                                                                FROM dbo.CHREGESTA
                                                                WHERE ID = @IdStayDestination
                                                            );
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
                                                            SELECT @UfuCodigoDestination = uf.UFUCODIGO, 
                                                                   @CodiCamaDestination = ca.CODICAMAS
                                                            FROM dbo.CHREGESTA re
                                                                 INNER JOIN dbo.CHCAMASHO ca ON re.CODICAMAS = ca.CODICAMAS
                                                                 INNER JOIN dbo.INUNIFUNC uf ON uf.UFUCODIGO = ca.UFUCODIGO
                                                            WHERE re.ID = @IdStayDestination;
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
                                                                FROM dbo.CHREGEGRE
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
                                                                FROM dbo.CHREGEGRE
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
                            FROM Security.[User] u
                                 INNER JOIN Security.Person p ON u.IdPerson = p.Id
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y confirma devoluciones farmacéuticas en el módulo de inventario, procesando tanto el registro de nuevas devoluciones como la anulación de devoluciones previas. Recibe los datos en formato XML (ítems a devolver y a anular) junto con el usuario que ejecuta la operación, y actualiza el stock del almacén revirtiendo las unidades dispensadas por lote o serial (PharmaceuticalDispensingDetailBatchSerial) hacia el inventario. Cruza información de dispensación (PharmaceuticalDispensing, PharmaceuticalDispensingDetail), órdenes de servicio de facturación (ServiceOrder, ServiceOrderDetail) y estados de estancia del paciente (CHREGESTA) para validar que la devolución sea coherente con el ciclo de atención, el ingreso hospitalario y la entidad pagadora (EPS/aseguradora). Es el punto de control para reversar medicamentos entregados a pacientes hospitalizados o de urgencias, afectando kardex de inventario, trazabilidad de lotes y la relación con la orden de facturación correspondiente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDevolution_Optimized';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDevolution_Optimized';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SavePhysicalInventoryKardex; Inventory.SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission; Billing.SP_UpdateRevenueControlDetailValues; GeneralLedger.SP_SaveJournalVoucher; Common.GETDATE; dbo.GetSequence', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_Optimized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INCONSECU; dbo.HCDEVMEDD; dbo.HCDEVMEDC; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.CHREGEGRE; dbo.HCFISIPRO; Common.ThirdParty; Common.OperatingUnit; Common.Sequense; Common.Supplier; Inventory.SettingInventory; Inventory.SettingInventoryFunctionalUnit; Inventory.Warehouse; Inventory.WarehouseUser; Inventory.InventorySequence; Inventory.InventorySequenceDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.ProductGroup; Billing.ServiceOrder; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailDistribution; Billing.RevenueControlDetail (+10 adicionales)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_Optimized';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_Optimized';
-- GO
