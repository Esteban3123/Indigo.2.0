
-- =============================================  
-- Author:  Diego A. Roldán  
-- ALTER date: 2015-09-24  
-- Description: Ajusted de inventarios  
-- =============================================  

CREATE PROCEDURE [Inventory].[SP_SaveCentralMix] @TransferOrder XML, 
                                                @UserCode      VARCHAR(20)
AS
    BEGIN
        SET NOCOUNT ON;
        BEGIN TRY
            DECLARE @tmpTransferOrder TABLE
            (RowId                  INT IDENTITY(1, 1) PRIMARY KEY, 
             RowXml                 INT, 
             Id                     INT, 
             Code                   VARCHAR(20), 
             OperatingUnitId        INT, 
             DocumentDate           DATETIME, 
             OrderType              TINYINT, --Especifica el tipo de orden de traslado 1 - Traslado 2 - Consumo  
             DispatchTo             TINYINT, --Especifica hacia donde se va a despachar los items 1 - Almacen 2 - Unidad Funcional Este campo solo se solicita cuando el tipo de la orden es consumo ya que cuando es de tipo traslado este campo se llena por defecto en 1 - Almacen 

             SourceWarehouseId      INT NULL, --Especifica el Id del almacen de origen, este campo solo se llena si el tipo de la orden es de traslado  
             TargetWarehouseId      INT NULL, --Especifica el Id del Almacen de destino, Este solo se habilita si el tipo de orden es de traslado  
             TargetFunctionalUnitId INT NULL, 
             AdjustmentConceptId    INT NULL, --Especifica el concepto de movimiento de inventarios, este debe ser de tipo Traslado por consumo Este campo solo se solicita si el tipo de la orden es Consumo  
             ThirdPartyId           INT NULL, --Especifica el id del tercero, este solo se solicita si es de tipo de la orden es de Consumo  
             [Description]          VARCHAR(300) NULL, 
             [Status]               TINYINT, 
             AdmissionNumber        VARCHAR(20), 
             CareCenterCode         VARCHAR(20), 
             FunctionUnitCode       VARCHAR(20), 
             ConsecutivoFarmacia    INT, 
             Validado               BIT, 
             TransferOrderDetail    XML
            );
            INSERT INTO @tmpTransferOrder
                   SELECT t.x.value('Id[1]', 'Int'), 
                          t.x.value('RowXml[1]', 'Int'), 
                          t.x.value('Code[1]', 'Varchar(20)'), 
                          t.x.value('OperatingUnitId[1]', 'Int'), 
                          CONVERT(DATETIME, t.x.value('DocumentDate[1]', 'varchar(19)'), 103), 
                          t.x.value('OrderType[1]', 'Tinyint'), 
                          t.x.value('DispatchTo[1]', 'Tinyint'), 
                          t.x.value('SourceWarehouseId[1]', 'Int'), 
                          t.x.value('TargetWarehouseId[1]', 'Int'), 
                          t.x.value('TargetFunctionalUnitId[1]', 'Int'), 
                          t.x.value('AdjustmentConceptId[1]', 'Int'), 
                          t.x.value('ThirdPartyId[1]', 'Int'), 
                          t.x.value('Description[1]', 'Varchar(300)'), 
                          t.x.value('Status[1]', 'Tinyint'), 
                          t.x.value('AdmissionNumber[1]', 'Varchar(20)'), 
                          t.x.value('CareCenterCode[1]', 'Varchar(20)'), 
                          t.x.value('FunctionUnitCode[1]', 'Varchar(20)'), 
                          t.x.value('ConsecutivoFarmacia[1]', 'int'), 
                          t.x.value('Validado[1]', 'bit'), 
                          t.x.query('TransferOrderDetail')
                   FROM @TransferOrder.nodes('/TransferOrder') t(x);

            --Envío de datos del ingreso  

            SELECT FC.CODCONCEC AS 'NumeroAdmision', 
                   RTRIM(P.IPPRINOMB) AS 'PrimerNombre', 
                   RTRIM(p.IPSEGNOMB) AS 'SegundoNombre', 
                   RTRIM(p.IPPRIAPEL) AS 'PrimerApellido', 
                   RTRIM(p.IPSEGAPEL) AS 'SegundoApellido', 
                   RTRIM(p.IPCODPACI) AS 'Identificacion',
                   CASE IPSEXOPAC
                       WHEN 1
                       THEN 'M'
                       ELSE 'F'
                   END AS 'Sexo', 
                   IPFECNACI AS 'FechaNacimiento', 
                   RTRIM(IPGRUPSAN) + '' + RTRIM(IPRHSANGR) AS 'Tiposangre', 
                   RTRIM(IPTELMOVI) AS 'Telefono', 
                   RTRIM(IPDIRECCI) AS 'Direccion', 
                   'Colombiana' AS 'Nacionalidad', 
                   NULL AS 'Alergias', 
                   RTRIM(E.CODENTIDA) + ' - ' + RTRIM(E.NOMENTIDA) AS 'Seguro', 
                   NULL AS 'CodigoCama', 
                   RTRIM(PRO.NOMMEDICO) AS 'Medico', 
                   I.CODDIAING AS 'Diagnostico', 
                   NULL AS 'NumeroPoliza', 
                   NULL AS 'NumeroHistoria', 
                   NULL AS 'CodigoArea', 
                   1 AS 'Admitido', 
                   [Common].[GETDATE]() AS 'FechaRegistro', 
                   0 AS 'EstadoInterfaz', 
                   NULL AS 'DescripcionErrorInterfaz'
            FROM dbo.HCFARMEPC FC
                 INNER JOIN dbo.INPACIENT P ON FC.IPCODPACI = P.IPCODPACI
                 INNER JOIN dbo.INENTIDAD E ON E.CODENTIDA = P.CODENTIDA
                 INNER JOIN dbo.INPROFSAL PRO ON PRO.CODPROSAL = FC.CODPROSAL
                 INNER JOIN dbo.ADINGRESO I ON I.NUMINGRES = FC.NUMINGRES
                 INNER JOIN @tmpTransferOrder tor ON tor.AdmissionNumber = FC.NUMINGRES
                                                     AND tor.ConsecutivoFarmacia = FC.CODCONCEC;

            --SELECT * FROM dbo.ADINGRESO a   
            --INNER JOIN @tmpTransferOrder tor ON LTRIM(RTRIM(tor.AdmissionNumber)) = LTRIM(RTRIM(a.NUMINGRES))  

            INSERT INTO [dbo].[ITFIngresos]
            ([NoAdmision], 
             [PrimerNombre], 
             [SegundoNombre], 
             [PrimerApellido], 
             [SegundoApellido], 
             [Cedula], 
             [Sexo], 
             [FechaNacimiento], 
             [TipoSangre], 
             [Telefono], 
             [Direccion], 
             [Nacionalidad], 
             [Alergias], 
             [Seguro], 
             [CODCAMA], 
             [MedicoTratante], 
             [Diagnostico], 
             [NUMPOLIZA], 
             [NUMHISTORIA], 
             [INDOCUMENTADO], 
             [Num_Historia], 
             [COD_AREA], 
             [Adminitido], 
             [FechaRegistro], 
             [DescripcionError]
            )
                   SELECT FC.CODCONCEC AS 'NumeroAdmision', 
                          RTRIM(P.IPPRINOMB) AS 'PrimerNombre', 
                          RTRIM(p.IPSEGNOMB) AS 'SegundoNombre', 
                          RTRIM(p.IPPRIAPEL) AS 'PrimerApellido', 
                          RTRIM(p.IPSEGAPEL) AS 'SegundoApellido', 
                          RTRIM(p.IPCODPACI) AS 'Identificacion',
                          CASE IPSEXOPAC
                              WHEN 1
                              THEN 'M'
                              ELSE 'F'
                          END AS 'Sexo', 
                          IPFECNACI AS 'FechaNacimiento', 
                          RTRIM(IPGRUPSAN) + '' + RTRIM(IPRHSANGR) AS 'Tiposangre', 
                          RTRIM(IPTELMOVI) AS 'Telefono', 
                          RTRIM(IPDIRECCI) AS 'Direccion', 
                          'Colombiana' AS 'Nacionalidad', 
                          NULL AS 'Alergias', 
                          RTRIM(E.CODENTIDA) + ' - ' + RTRIM(E.NOMENTIDA) AS 'Seguro', 
                          NULL AS 'CodigoCama', 
                          RTRIM(PRO.NOMMEDICO) AS 'Medico', 
                          I.CODDIAING AS 'Diagnostico', 
                          NULL AS 'NumeroPoliza', 
                          NULL AS 'NumeroHistoria', 
                          NULL AS 'INDOCUMENTADO', 
                          NULL AS 'Num_Historia', 
                          NULL AS 'CodigoArea', 
                          1 AS 'Admitido', 
                          [Common].[GETDATE]() AS 'FechaRegistro',  
                          --0 as 'EstadoInterfaz',  
                          NULL AS 'DescripcionErrorInterfaz'
                   FROM dbo.HCFARMEPC FC
                        INNER JOIN dbo.INPACIENT P ON FC.IPCODPACI = P.IPCODPACI
                        INNER JOIN dbo.INENTIDAD E ON E.CODENTIDA = P.CODENTIDA
                        INNER JOIN dbo.INPROFSAL PRO ON PRO.CODPROSAL = FC.CODPROSAL
                        INNER JOIN dbo.ADINGRESO I ON I.NUMINGRES = FC.NUMINGRES
                        INNER JOIN @tmpTransferOrder tor ON tor.ConsecutivoFarmacia = FC.CODCONCEC;
            SELECT *
            FROM [dbo].[ITFIngresos];
            SELECT FC.CODCONCEC AS 'NumeroConsecutivo', 
                   FD.ID AS 'Item', 
                   FD.CODPRODUC AS 'Producto', 
                   FD.CANENTPRO AS 'cantidad', 
                   LOWER(U.ABRUNIMED) AS 'TipoConcentracion', 
                   NULL AS 'ViaAdministracion', 
                   NULL AS 'Observaciones', 
                   0 AS 'Descriptiva', 
                   NULL AS 'OrdenDescriptiva', 
                   ISNULL(FD.frecuenci, 0) AS 'Frecuencia', 
                   0 AS 'Requesicion', 
                   [Common].[GETDATE]() AS 'FechaRegistro', 
                   0 AS 'Suspension', 
                   PR.FECINIDOS AS 'FechaInicioTratamiento', 
                   PR.FECFINDOS AS 'FechaFinTratamiento', 
                   FD.ID AS 'ConsecutivoExterno', 
                   0 AS 'EstadoInterfaz', 
                   NULL AS 'DescripcionError', 
                   NULL AS 'Preparado', 
                   NULL AS 'notaEntrega'
            FROM dbo.HCFARMEPC FC
                 INNER JOIN dbo.HCFARMEPD FD ON FC.CODCONCEC = FD.CODCONCEC
                 INNER JOIN dbo.INUNIMEDI U ON U.CODUNIMED = FD.CODUNIMED
                 LEFT JOIN dbo.HCPRESCRA PR ON PR.NUMEFOLIO = FD.NUMEFOLIO
                                               AND PR.NUMINGRES = FD.NUMINGRES
                                               AND PR.CODPRODUC = FD.CODPRODUC
                 INNER JOIN @tmpTransferOrder tor ON tor.ConsecutivoFarmacia = FC.CODCONCEC;
            INSERT INTO [dbo].[ITFIngresosDet]
            ([NoAdmision], 
             [Item], 
             [IdServicio], 
             [Cantidad], 
             [IdTipoConcentracion], 
             [ViaAdmin], 
             [Observaciones], 
             [Descriptiva], 
             [OrdenDescriptiva], 
             [Frecuencia], 
             [Requisicion], 
             [FechaRegistro], 
             [Suspencion], 
             [FechaInicio], 
             [FechaFin], 
             [ConsecutivoExterno], 
             [Status], 
             [DescripcionError], 
             [Preparado], 
             [NotaEntrega]
            )
                   SELECT FC.CODCONCEC AS 'NumeroConsecutivo', 
                          FD.ID AS 'Item', 
                          FD.CODPRODUC AS 'Producto', 
                          FD.CANENTPRO AS 'cantidad', 
                          LOWER(U.ABRUNIMED) AS 'TipoConcentracion', 
                          NULL AS 'ViaAdministracion', 
                          NULL AS 'Observaciones', 
                          0 AS 'Descriptiva', 
                          NULL AS 'OrdenDescriptiva', 
                          ISNULL(FD.frecuenci, 0) AS 'Frecuencia', 
                          0 AS 'Requesicion', 
                          [Common].[GETDATE]() AS 'FechaRegistro', 
                          0 AS 'Suspension', 
                          PR.FECINIDOS AS 'FechaInicioTratamiento', 
                          PR.FECFINDOS AS 'FechaFinTratamiento', 
                          FD.ID AS 'ConsecutivoExterno', 
                          0 AS 'EstadoInterfaz', 
                          NULL AS 'DescripcionError', 
                          NULL AS 'Preparado', 
                          NULL AS 'notaEntrega'
                   FROM dbo.HCFARMEPC FC
                        INNER JOIN dbo.HCFARMEPD FD ON FC.CODCONCEC = FD.CODCONCEC
                        INNER JOIN dbo.INUNIMEDI U ON U.CODUNIMED = FD.CODUNIMED
                        LEFT JOIN dbo.HCPRESCRA PR ON PR.NUMEFOLIO = FD.NUMEFOLIO
                                                      AND PR.NUMINGRES = FD.NUMINGRES
                                                      AND PR.CODPRODUC = FD.CODPRODUC
                        LEFT OUTER JOIN @tmpTransferOrder tor ON tor.ConsecutivoFarmacia = FC.CODCONCEC;
            SELECT *
            FROM [dbo].[ITFIngresosDet];
            RETURN;

            --Declare @tmpTransferOrderDetail Table(  
            -- RowId Int Identity(1,1) Primary Key,  
            -- RowXml Int,  
            -- TransferOrderId Int,  
            -- InventoryRequestDetailId Int Null,  
            -- ProductId Int,  
            -- InventoryQuantity Int,  
            -- Quantity Int,  
            -- [Value] Decimal(18, 2),  
            -- [Description] Varchar(300),  
            -- TransferOrderDetailBatchSerial Xml  
            --)  
            --DECLARE @TagTransferOrder Varchar(4) = '1519'  
            ----Generación de secuencia numérica  
            --Declare @idSequenceDetail Int, @IdSequenceCommon Int, @pattern Varchar(300), @NextS Bigint, @Scope Varchar(5), @IdSequence Int  
            --Select @IdSequence = Id, @Scope = Scope,  
            -- @IdSequenceCommon = IdSequence  
            --From Inventory.InventorySequence With(Nolock)   
            --Where IdForm = @TagTransferOrder  
            --If @IdSequence Is Null Begin  
            -- Select Cast(0 As Bit) As StatusResult, 'No se ha creado secuencia numerica para el formulario de ordenes de traslado' As MessageResult  
            -- Return  
            --END  
            --DECLARE @OperatingUnitId int   
            --SELECT TOP 1 @OperatingUnitId = OperatingUnitId FROM @tmpTransferOrder  
            --If @Scope = 'OU' Begin  
            -- Select @pattern = cs.Pattern, @idSequenceDetail = isd.Id    
            -- From Inventory.InventorySequenceDetail isd With(Nolock)  
            -- Inner Join Common.Sequense cs With(Nolock) On cs.Id = isd.IdSequense   
            -- Where isd.InventorySequenceId = @IdSequence And IdOperatingUnit = @OperatingUnitId  
            --End  
            --Else Begin  
            -- Select @pattern = cs.Pattern, @idSequenceDetail = isd.Id    
            -- From Inventory.InventorySequenceDetail isd With(Nolock)   
            -- Inner Join Common.Sequense cs With(Nolock) On cs.Id = isd.IdSequense   
            -- Where isd.InventorySequenceId = @IdSequence  
            --End  
            --If @idSequenceDetail Is Null Begin  
            -- Select Cast(0 As Bit) As StatusResult,'La secuencia para Ordenes de Servicio no esta parametrizada' As MessageResult  
            -- Return  
            --END  
            --Declare @TransferQuantity Int  
            --SELECT @TransferQuantity = Count(1) FROM @tmpTransferOrder tto  
            --Update Inventory.InventorySequenceDetail Set @NextS = [Next] += @TransferQuantity Where Id = @idSequenceDetail  
            --DECLARE @NextFirst Bigint = @NextS - @TransferQuantity    

            DECLARE @Rows INT, @RowId INT;
            SET @Rows = 1;
            SET @RowId = 1;
            DECLARE @errorList VARCHAR(MAX)= '', @messageStock VARCHAR(MAX)= '';
            DECLARE @ToId INT, @ToCode VARCHAR(20), @ToOperatingUnitId INT, @ToDocumentDate DATETIME, @ToOrderType TINYINT, @ToDispatchTo TINYINT, @ToSourceWarehouseId INT, --Especifica el Id del almacen de origen, este campo solo se llena si el tipo de la orden es de traslado  
            @ToTargetWarehouseId INT, --Especifica el Id del Almacen de destino, Este solo se habilita si el tipo de orden es de traslado  
            @ToTargetFunctionalUnitId INT, @ToAdjustmentConceptId INT, @ToThirdPartyId INT, @ToDescription VARCHAR(300), @ToStatus TINYINT, @ToAdmissionNumber VARCHAR(20), @ToCareCenterCode VARCHAR(20), @ToFunctionUnitCode VARCHAR(20), @ToValidado BIT, @RowXml INT, @ValidaPreparacionMezcla BIT, @TransferOrderDetail XML;
            WHILE @Rows > 0
                BEGIN
                    SELECT TOP 1 @RowId = RowId, 
                                 @ToId = od.Id, 
                                 @ToCode = Code, 
                                 @ToOperatingUnitId = od.OperatingUnitId, 
                                 @ToDocumentDate = od.DocumentDate, 
                                 @ToOrderType = od.OrderType, 
                                 @ToDispatchTo = od.DispatchTo, 
                                 @ToSourceWarehouseId = od.SourceWarehouseId, 
                                 @ToTargetWarehouseId = od.TargetWarehouseId, 
                                 @ToTargetFunctionalUnitId = od.TargetFunctionalUnitId, 
                                 @ToAdjustmentConceptId = od.AdjustmentConceptId, 
                                 @ToThirdPartyId = od.ThirdPartyId, 
                                 @ToDescription = od.[Description], 
                                 @ToStatus = od.[Status], 
                                 @ToAdmissionNumber = od.AdmissionNumber, 
                                 @ToCareCenterCode = od.CareCenterCode, 
                                 @ToFunctionUnitCode = od.FunctionUnitCode, 
                                 @ToValidado = od.Validado, 
                                 @RowXml = od.RowId, 
                                 @TransferOrderDetail = od.TransferOrderDetail
                    FROM @tmpTransferOrder od
                    WHERE RowId >= @RowId
                    ORDER BY RowId;
                    SET @Rows = @@ROWCOUNT;
                    IF @Rows = 0
                        BREAK;

                    --SELECT @ToCode, @ToOperatingUnitId, @ToDocumentDate, @ToOrderType, @ToDispatchTo,  
                    -- @ToSourceWarehouseId, @ToTargetWarehouseId, @ToTargetFunctionalUnitId,  
                    -- @ToAdjustmentConceptId, @ToThirdPartyId, @ToDescription, @ToStatus  
                    --Select @ToCode = dbo.GetSequence('', @pattern, @NextFirst)  
                    --IF @ToCode = '__ERROR_MAXVALUE__'  
                    --BEGIN  
                    -- SELECT Cast(0 AS bit) As StatusResult, 'La secuencia alcanzo su valor maximo' AS MessageResult  
                    -- return  
                    --END  
                    --SELECT * FROM @tmpTransferOrder t(x) WHERE t.x.query('/TransferOrder/RowId')  

                    SET @ToCode = '';

                    --validar el parametro si necesita que los registros sean validados o no  
                    SELECT TOP 1 @ValidaPreparacionMezcla = VALIDAPREPARACIONMEZCLA
                    FROM dbo.HCUNITHIS WITH(NOLOCK)
                    WHERE CODTIPHIS <> 'ENF'
                          AND CODCENATE = @ToCareCenterCode
                          AND UFUCODIGO = @ToFunctionUnitCode;
                    IF @ValidaPreparacionMezcla IS NOT NULL
                       AND @ValidaPreparacionMezcla = 1
                       AND @ToValidado = 0
                        BEGIN
                            SET @errorList+='El ingreso (' + @ToAdmissionNumber + ') no se encuentra validado. ' + CHAR(13) + CHAR(10);
                            GOTO NEXTDOCUMENT;
                    END;
                    DECLARE @StatusResult BIT, @MessageResult VARCHAR(MAX), @TransferOrderId INT, @TransferOrderXml XML;

                    --DELETE FROM @tmpTransferOrderDetail  
                    --INSERT INTO @tmpTransferOrderDetail  
                    --(RowXml, TransferOrderId, InventoryRequestDetailId, ProductId, InventoryQuantity,  
                    --    Quantity, [Value], [Description], TransferOrderDetailBatchSerial)  
                    --SELECT t.x.value('RowXml[1]', 'Int'),  
                    -- t.x.value('TransferOrderId[1]', 'Int'),  
                    -- t.x.value('InventoryRequestDetailId[1]', 'Int'),  
                    -- t.x.value('ProductId[1]', 'Int'),  
                    -- t.x.value('InventoryQuantity[1]', 'Int'),  
                    -- t.x.value('Quantity[1]', 'Int'),  
                    -- t.x.value('Value[1]', 'Decimal(18, 2)'),  
                    -- t.x.value('Description[1]', 'Varchar(300)'),  
                    -- t.x.query('TransferOrderDetailBatchSerial')  
                    --FROM @TransferOrderDetail.nodes('/TransferOrderDetail') t(x)  
                    --Declare @todXml Xml = (  
                    -- SELECT *  
                    -- FROM (  
                    --  SELECT RowXml,   
                    --   TransferOrderId,  
                    --   InventoryRequestDetailId,  
                    --   ProductId,  
                    --   InventoryQuantity,  
                    --   Quantity,  
                    --   [Value],  
                    --   [Description] FROM @tmpTransferOrderDetail      
                    -- ) TransferOrderDetail    
                    -- CROSS APPLY (  
                    --  SELECT t.x.value('Id[1]', 'Int') AS Id,  
                    --   t.x.value('TransferOrderDetailId[1]', 'Int') AS TransferOrderDetailId,  
                    --   t.x.value('PhysicalInventoryId[1]', 'Int') AS PhysicalInventoryId,  
                    --   t.x.value('Quantity[1]', 'Int') AS Quantity,  
                    --   t.x.value('OutstandingQuantity[1]', 'Int') AS OutstandingQuantity  
                    --  FROM @TransferOrderDetail.nodes('/TransferOrderDetail/TransferOrderDetailBatchSerial') t(x)  
                    --  WHERE t.x.value('../RowXml[1]', 'Int') = TransferOrderDetail.RowXml  
                    -- ) TransferOrderDetailBatchSerial  
                    -- FOR XML AUTO, ELEMENTS  
                    --)    

                    SET @TransferOrderXml =
                    (
                        SELECT TransferOrder.*
                        FROM
                        (
                            SELECT @ToId AS Id, 
                                   @ToCode AS Code, 
                                   @ToOperatingUnitId AS OperatingUnitId, 
                                   @ToDocumentDate AS DocumentDate, 
                                   @ToOrderType AS OrderType, 
                                   @ToDispatchTo AS DispatchTo, 
                                   @ToSourceWarehouseId AS SourceWarehouseId, 
                                   @ToTargetWarehouseId AS TargetWarehouseId, 
                                   @ToTargetFunctionalUnitId AS TargetFunctionalUnitId, 
                                   @ToAdjustmentConceptId AS AdjustmentConceptId, 
                                   @ToThirdPartyId AS ThirdPartyId, 
                                   @ToDescription AS [Description], 
                                   @ToStatus AS [Status], 
                                   @ToAdmissionNumber AS AdmissionNumber, 
                                   @ToCareCenterCode AS CareCenterCode, 
                                   @ToFunctionUnitCode AS FunctionUnitCode, 
                                   @ToValidado AS Validado, 
                                   @UserCode AS CreationUser, 
                                   [Common].[GETDATE]() AS CreationDate
                        ) AS TransferOrder FOR XML AUTO, ELEMENTS
                    );  
                    --SET @TransferOrderXml.modify('insert sql:variable("@todXml") as last into (/TransferOrder)[1]')     
                    SET @TransferOrderXml.modify('insert sql:variable("@TransferOrderDetail") as last into (/TransferOrder)[1]');

                    --Guardando y confirmando la orden de traslado al almacén de mezclas  

                    EXEC [Inventory].[SP_SaveTransferOrder] 
                         @TransferOrderXml, 
                         @UserCode, 
                         @StatusResult OUTPUT, 
                         @MessageResult OUTPUT, 
                         @TransferOrderId OUTPUT;
                    IF @StatusResult = 0
                        BEGIN
                            SET @errorList+=@MessageResult + CHAR(13) + CHAR(10);
                            GOTO NEXTDOCUMENT;
                    END;
                    SELECT *
                    FROM Inventory.TransferOrder [to]
                         INNER JOIN Inventory.TransferOrderDetail tod ON tod.TransferOrderId = [to].Id
                         INNER JOIN Inventory.TransferOrderDetailBatchSerial todbs ON todbs.TransferOrderDetailId = tod.Id
                    WHERE [to].id = @TransferOrderId;

                    --DECLARE @resultTransferOrder Table(  
                    -- StatusResult Bit,  
                    -- MessageResult Varchar(Max)  
                    --)  
                    --DELETE FROM @resultTransferOrder WHERE 1 = 1  
                    --INSERT INTO @resultTransferOrder  
                    --EXEC [Inventory].[SP_ConfirmTransferOrder]  
                    -- @TransferOrderId,  
                    -- @UserCode  

                    DECLARE @StatusResultConfirm BIT, @MessageResultConfirm VARCHAR(MAX);
                    EXEC [Inventory].[SP_ConfirmTransferOrder_Output] 
                         @TransferOrderId, 
                         @UserCode, 
                         @StatusResultConfirm OUTPUT, 
                         @MessageResultConfirm OUTPUT;
                    IF @StatusResultConfirm = 0
                        BEGIN
                            SET @errorList+=@MessageResultConfirm + CHAR(13) + CHAR(10);
                            GOTO NEXTDOCUMENT;
                    END;
                    IF @MessageResultConfirm <> ''
                        BEGIN
                            SET @messageStock+=@MessageResultConfirm + CHAR(13) + CHAR(10);
                    END;
                    SELECT @StatusResultConfirm, 
                           @MessageResultConfirm;
                    NEXTDOCUMENT:  
                    --SET @NextFirst += 1  
                    SET @RowId+=1;
                END;
            IF @errorList <> ''
                BEGIN
                    SELECT CAST(0 AS BIT) AS StatusResult, 
                           @errorList AS MessageResult;
                    RETURN;
            END;

            --Set @StatusResult = 1  
            --Set @MessageResult = @messageStock  

            SELECT CAST(1 AS BIT) AS StatusResult, 
                   @messageStock AS MessageResult;
        END TRY
        BEGIN CATCH
            SELECT CAST(0 AS BIT) AS StatusResult, 
                   ERROR_MESSAGE() AS MessageResult;
        END CATCH;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que registra y procesa órdenes de mezcla centralizada (central mix) de farmacia a partir de un XML con los datos de la orden de traslado o consumo de inventario. Toma cada orden recibida, extrae el número de ingreso del paciente y el consecutivo de farmacia, y cruza con las tablas de historia clínica de farmacia (HCFARMEPC), datos maestros del paciente (INPACIENT), entidad aseguradora (INENTIDAD), profesional de salud (INPROFSAL) e ingreso hospitalario (ADINGRESO) para construir un perfil completo del paciente y su admisión. Con esa información consolidada, inserta un registro en la tabla de interfaz de ingresos (ITFIngresos) que alimenta la integración con el sistema externo de mezclas, incluyendo nombre completo, documento de identidad, sexo, fecha de nacimiento, tipo de sangre, contacto, seguro (EPS/aseguradora), médico tratante y diagnóstico. Existe para habilitar la trazabilidad y el envío de datos clínicos y demográficos del paciente hacia el módulo o sistema externo de preparación de mezclas intravenosas en farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCentralMix';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCentralMix';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa órdenes de traslado/consumo de inventario hacia central de mezclas, replicando datos del ingreso y detalle farmacéutico en tablas de interfaz, y delegando el guardado y confirmación de cada orden a procedimientos de inventario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCentralMix';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodos /TransferOrder con los campos esperados (Id, Code, OperatingUnitId, DocumentDate, OrderType, etc.).; DocumentDate debe venir en formato 103 (dd/mm/yyyy) para conversión a DATETIME.; Para validar la preparación de mezcla debe existir parametrización en dbo.HCUNITHIS para el CareCenterCode y FunctionUnitCode con CODTIPHIS distinto de ''ENF''.; AdmissionNumber y ConsecutivoFarmacia deben corresponder a un registro existente en dbo.HCFARMEPC para que se generen los datos del ingreso.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCentralMix';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada documento del XML se procesa de forma independiente: el fallo de uno no detiene el ciclo, sino que se acumula en @errorList.; Una orden solo se confirma si previamente se guardó con éxito (SP_SaveTransferOrder retornó StatusResult=1).; Si el centro/unidad funcional exige validación de preparación de mezcla, no se persiste la orden mientras Validado=0.; El sexo del paciente se normaliza a ''M'' si IPSEXOPAC=1, ''F'' en cualquier otro caso.; La nacionalidad del paciente se fuerza siempre a ''Colombiana'' en la interfaz.; FechaRegistro de los registros de interfaz se toma de Common.GETDATE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCentralMix';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.ITFIngresos: Por cada coincidencia entre el XML recibido y dbo.HCFARMEPC (por ConsecutivoFarmacia=CODCONCEC) se inserta un registro con datos demográficos del paciente, aseguradora, médico y diagnóstico del ingreso, marcado como Admitido=1.; [INSERT] dbo.ITFIngresosDet: Por cada detalle de pedido farmacéutico (HCFARMEPD) ligado al consecutivo recibido se inserta una línea con producto, cantidad, unidad, frecuencia y fechas de inicio/fin de tratamiento tomadas de HCPRESCRA, con EstadoInterfaz=0, Descriptiva=0, Suspension=0.; [EXECUTE] Inventory.TransferOrder: Para cada fila del XML se construye un XML consolidado con cabecera + detalle y se invoca Inventory.SP_SaveTransferOrder; si StatusResult=0 se acumula el error y se salta al siguiente documento.; [EXECUTE] Inventory.TransferOrder: Tras guardar exitosamente la orden, se invoca Inventory.SP_ConfirmTransferOrder_Output para confirmarla; si StatusResult=0 se acumula el error y se salta al siguiente documento.; [RETURN_RESULT] RESULT: Si @errorList no está vacío al finalizar el ciclo, retorna StatusResult=0 con la lista de errores; si no, retorna StatusResult=1 con @messageStock acumulado.; [RETURN_RESULT] RESULT: Ante cualquier excepción capturada, retorna StatusResult=0 y ERROR_MESSAGE() como MessageResult.; [RETURN_RESULT] RESULT: Antes del ciclo de procesamiento, devuelve los conjuntos completos de dbo.ITFIngresos y dbo.ITFIngresosDet, así como el detalle previo de cada orden insertada (TransferOrder + TransferOrderDetail + TransferOrderDetailBatchSerial).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCentralMix';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCUNITHIS.VALIDAPREPARACIONMEZCLA = 1 (parámetro existe y exige validación) y el documento llega con Validado=0 → Se acumula error ''El ingreso (...) no se encuentra validado.'' y se salta al siguiente documento (GOTO NEXTDOCUMENT) sin guardar la orden. else Continúa con la construcción del XML y la invocación de SP_SaveTransferOrder.; si SP_SaveTransferOrder retorna StatusResult=0 → Se concatena el MessageResult al @errorList y se salta al siguiente documento sin confirmar. else Se procede a confirmar la orden con SP_ConfirmTransferOrder_Output.; si SP_ConfirmTransferOrder_Output retorna StatusResult=0 → Se concatena el MessageResult al @errorList y se salta al siguiente documento. else Si MessageResultConfirm no está vacío, se acumula en @messageStock.; si @errorList <> '''' al finalizar el WHILE → Devuelve StatusResult=0 y la lista de errores como MessageResult, terminando. else Devuelve StatusResult=1 y @messageStock como MessageResult.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCentralMix';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SaveTransferOrder; Inventory.SP_ConfirmTransferOrder_Output; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCentralMix';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.INPACIENT; dbo.INENTIDAD; dbo.INPROFSAL; dbo.ADINGRESO; dbo.HCFARMEPD; dbo.INUNIMEDI; dbo.HCPRESCRA; dbo.HCUNITHIS; dbo.ITFIngresos; dbo.ITFIngresosDet; Inventory.TransferOrder; Inventory.TransferOrderDetail; Inventory.TransferOrderDetailBatchSerial', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCentralMix';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCentralMix';
-- GO
