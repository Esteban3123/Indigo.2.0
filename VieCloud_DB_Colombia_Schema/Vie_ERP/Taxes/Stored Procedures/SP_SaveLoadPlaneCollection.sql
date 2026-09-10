
-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 25-08-2016
-- Description:	confirma el archivo plano del avaluo
-- =============================================
CREATE PROCEDURE [Taxes].[SP_SaveLoadPlaneCollection] @Data AS XML, 
                                                     @User AS VARCHAR(20)
AS
    BEGIN

        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;
        DECLARE @Records TABLE
        (Id     INT IDENTITY PRIMARY KEY NOT NULL, 
         record VARCHAR(200) INDEX IX1 CLUSTERED
        );
        --tabla para ir almacenando los codigos que se van usando
        DECLARE @RecordsCodes TABLE(Code VARCHAR(30) INDEX IX1 CLUSTERED);
        --variables para los registros
        DECLARE @CodeDeparment AS VARCHAR(2), @CodeCity AS VARCHAR(3), @Code AS VARCHAR(50), @Nit AS VARCHAR(MAX), @ThirdName AS VARCHAR(300);
        DECLARE @Addres AS VARCHAR(200), @Commune AS VARCHAR(5), @EconomicDestiny AS VARCHAR(5), @LandArea AS NUMERIC(18, 0), @BuiltArea AS NUMERIC(18, 0), @Appraisal AS NUMERIC(18, 0), @Validity AS INT;
        --variable para el proceso
        DECLARE @OrderOwner AS INT, @TaxesPropertyOwnerId AS INT, @thirdPartyId AS INT;
        DECLARE @TableRecords TABLE
        (Id              INT IDENTITY(1, 1), 
         CodeDeparment   VARCHAR(2), 
         CodeCity        VARCHAR(3), 
         Code            VARCHAR(15), 
         OrderOwner      INT, 
         ThirdName       VARCHAR(300), 
         Nit             VARCHAR(20) INDEX IX1 CLUSTERED, 
         Addres          VARCHAR(200), 
         Commune         VARCHAR(5), 
         EconomicDestiny VARCHAR(5), 
         LandArea        NUMERIC(18, 0), 
         BuiltArea       NUMERIC(18, 0), 
         Appraisal       NUMERIC(18, 0), 
         Validity        INT, 
         ThirdPartyId    INT
        );
        BEGIN TRY
            INSERT INTO @Records
                   SELECT XTags.value('.', 'varchar(200)') AS record
                   FROM @Data.nodes('/Data/Record') AS XTbl(XTags);
            INSERT INTO @TableRecords
                   SELECT SUBSTRING(record, 1, 2), 
                          SUBSTRING(record, 3, 3), 
                          SUBSTRING(record, 6, 15), 
                          SUBSTRING(record, 22, 3), 
                          SUBSTRING(record, 28, 33), 
                          CAST(CASE
                                   WHEN SUBSTRING(record, 63, 12) LIKE '%-%'
                                   THEN CAST(SUBSTRING(SUBSTRING(record, 63, 12), 0, CHARINDEX('-', SUBSTRING(record, 63, 12))) AS VARCHAR(20))
                                   ELSE CAST(SUBSTRING(record, 63, 12) AS VARCHAR(20))
                               END AS VARCHAR(20)), 
                          SUBSTRING(record, 75, 34), 
                          SUBSTRING(record, 109, 1), 
                          SUBSTRING(record, 110, 1), 
                          SUBSTRING(record, 111, 12), 
                          SUBSTRING(record, 123, 6), 
                          SUBSTRING(record, 129, 12), 
                          SUBSTRING(record, 146, 4), 
                          0
                   FROM @Records;
            UPDATE @TableRecords
              SET 
                  Nit = SUBSTRING(Nit, PATINDEX('%[^0]%', Nit), 100);
            DECLARE @IdUser INT=
            (
                SELECT Id
                FROM Security.[User]
                WHERE UserCode = @User
            );
            UPDATE @TableRecords
              SET 
                  ThirdPartyId = w.ThirdPartyId
            FROM @TableRecords r
                 INNER JOIN Taxes.SettingsTaxesWords w ON r.ThirdName LIKE w.Word;

            --select distinct r.Nit, r.ThirdName
            --from @TableRecords r
            --left join Common.Person p on p.IdentificationNumber = r.Nit
            --where r.Nit != '0' and p.Id is null and r.ThirdPartyId = 0 and r.Nit = '12101941'
            ---- Inserto los terceros que tienen cedula diferente a cero
            INSERT INTO Common.Person
            ([IdentificationNumber], 
             [IdentificationType], 
             [IdentificacionCityId], 
             [IdentificationExpeditionDate], 
             [MilitaryCardId], 
             [MilitaryCardNumber], 
             [FirstName], 
             [SecondName], 
             [FirstLastName], 
             [SecondLastName], 
             [BirthDate], 
             [BirthCityId], 
             [DeathDate], 
             [Gender], 
             [BloodGroup], 
             [RH], 
             [Fingerprint], 
             [SonNumber], 
             [Dependents], 
             [MaritalStatus], 
             [State]
            )
                   SELECT DISTINCT 
                          r.Nit, 
                          0, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                   (
                       SELECT TOP 1 ThirdName
                       FROM @TableRecords
                       WHERE Nit = r.Nit
                   ), 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          1
                   FROM @TableRecords r
                        LEFT JOIN Common.Person p ON p.IdentificationNumber = r.Nit
                   WHERE r.Nit != '000000000000'
                         AND p.Id IS NULL
                         AND r.ThirdPartyId = 0;

            --INSERT INTO Common.ThirdParty
            --SELECT DISTINCT
            --    p.Id,
            --    r.Nit,
            --    NULL,
            --    (
            --        SELECT TOP 1 ThirdName FROM @TableRecords WHERE Nit = r.Nit
            --    ),
            --    1,
            --    0,
            --    0,
            --    NULL,
            --    0,
            --    0,
            --    0,
            --    0,
            --    NULL,
            --    NULL,
            --    NULL,
            --    NULL,
            --    NULL,
            --    1,
            --    [Common].[GETDATE](),
            --    @IdUser
            --FROM @TableRecords r
            --    INNER JOIN Common.Person p
            --        ON p.IdentificationNumber = r.Nit
            --    LEFT JOIN Common.ThirdParty t
            --        ON t.Nit = r.Nit
            --WHERE r.Nit != '000000000000'
            --      AND t.Id IS NULL
            --      AND r.ThirdPartyId = 0;

            UPDATE @TableRecords
              SET 
                  ThirdPartyId = t.Id
            FROM @TableRecords r
                 INNER JOIN Common.Person p ON p.IdentificationNumber = r.Nit
                 INNER JOIN Common.ThirdParty t ON t.Nit = r.Nit
            WHERE r.Nit != '000000000000'
                  AND r.ThirdPartyId = 0;

            --- inserto los terceros que tienen el nit en cero y se insertan con numero catastral
            INSERT INTO Common.Person
            ([IdentificationNumber], 
             [IdentificationType], 
             [IdentificacionCityId], 
             [IdentificationExpeditionDate], 
             [MilitaryCardId], 
             [MilitaryCardNumber], 
             [FirstName], 
             [SecondName], 
             [FirstLastName], 
             [SecondLastName], 
             [BirthDate], 
             [BirthCityId], 
             [DeathDate], 
             [Gender], 
             [BloodGroup], 
             [RH], 
             [Fingerprint], 
             [SonNumber], 
             [Dependents], 
             [MaritalStatus], 
             [State]
            )
                   SELECT DISTINCT 
                          r.Code + RIGHT('0' + CAST(OrderOwner AS CHAR(2)), 2), 
                          0, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                   (
                       SELECT TOP 1 ThirdName
                       FROM @TableRecords
                       WHERE Nit = r.Nit
                   ), 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          NULL, 
                          1
                   FROM @TableRecords r
                        LEFT JOIN Common.Person p ON p.IdentificationNumber = r.Code + RIGHT('0' + CAST(OrderOwner AS CHAR(2)), 2)
                   WHERE r.Nit = '000000000000'
                         AND p.Id IS NULL
                         AND r.ThirdPartyId = 0;

            --INSERT INTO Common.ThirdParty
            --SELECT DISTINCT
            --    p.Id,
            --    r.Code + RIGHT('0' + CAST(OrderOwner AS CHAR(2)), 2),
            --    NULL,
            --    r.ThirdName,
            --    1,
            --    0,
            --    0,
            --    NULL,
            --    0,
            --    0,
            --    0,
            --    0,
            --    NULL,
            --    NULL,
            --    NULL,
            --    NULL,
            --    NULL,
            --    1,
            --    [Common].[GETDATE](),
            --    @IdUser
            --FROM @TableRecords r
            --    INNER JOIN Common.Person p
            --        ON p.IdentificationNumber = r.Code + RIGHT('0' + CAST(OrderOwner AS CHAR(2)), 2)
            --    LEFT JOIN Common.ThirdParty t
            --        ON t.Nit = r.Code + RIGHT('0' + CAST(OrderOwner AS CHAR(2)), 2)
            --WHERE r.Nit = '000000000000'
            --      AND t.Id IS NULL
            --      AND r.ThirdPartyId = 0;

            UPDATE @TableRecords
              SET 
                  ThirdPartyId = t.Id
            FROM @TableRecords r
                 INNER JOIN Common.Person p ON p.IdentificationNumber = r.Code + RIGHT('0' + CAST(OrderOwner AS CHAR(2)), 2)
                 INNER JOIN Common.ThirdParty t ON t.Nit = r.Code + RIGHT('0' + CAST(OrderOwner AS CHAR(2)), 2)
            WHERE r.Nit = '000000000000'
                  AND r.ThirdPartyId = 0;
            DECLARE @record AS VARCHAR(149);
            DECLARE record_cursor CURSOR
            FOR SELECT CodeDeparment, 
                       CodeCity, 
                       Code, 
                       OrderOwner, 
                       ThirdName, 
                       Nit, 
                       Addres, 
                       Commune, 
                       EconomicDestiny, 
                       LandArea, 
                       BuiltArea, 
                       Appraisal, 
                       Validity, 
                       ThirdPartyId
                FROM @TableRecords;
            OPEN record_cursor;
            FETCH NEXT FROM record_cursor INTO @CodeDeparment, @CodeCity, @Code, @OrderOwner, @ThirdName, @Nit, @Addres, @Commune, @EconomicDestiny, @LandArea, @BuiltArea, @Appraisal, @Validity, @thirdPartyId;
            WHILE @@FETCH_STATUS = 0
                BEGIN

                    --asignamos valores a las varibles
                    --set @CodeDeparment = SUBSTRING(@record,1,2)
                    --set @CodeCity = SUBSTRING (@record ,3,3)
                    --set @Code = SUBSTRING(@record,6,15)
                    --set @OrderOwner = SUBSTRING(@record,22,3)
                    --set @ThirdName = SUBSTRING(@record,28,33)
                    --set @Nit = SUBSTRING(@record,63,12)			
                    --set @Addres = SUBSTRING(@record,75,34)
                    --set @Commune = SUBSTRING(@record,109,1)
                    --set @EconomicDestiny = SUBSTRING(@record,110,1)
                    --set @LandArea = SUBSTRING(@record,111,12)
                    --set @BuiltArea = SUBSTRING(@record ,123,6)
                    --set @Appraisal = SUBSTRING(@record ,129,12)
                    --set @Validity = SUBSTRING(@record,146,4)
                    --busco en la tabla temporal que no exista el codigo con el que voy a trabajar
                    IF
                    (
                        SELECT COUNT(*)
                        FROM @RecordsCodes
                        WHERE Code = @Code
                    ) = 0
                        BEGIN

                            --ahora valido que el predio no exista en la tabla

                            IF
                            (
                                SELECT COUNT(*)
                                FROM Taxes.TaxesProperty
                                WHERE Code = @Code
                            ) = 0
                                BEGIN
                                    --inserto en las tres tablas				

                                    INSERT INTO [Taxes].[TaxesProperty]
                                    ([CodeDeparment], 
                                     [CodeCity], 
                                     [Code], 
                                     [Taxed], 
                                     [ThirdPartyId], 
                                     [Addres], 
                                     [Commune], 
                                     [EconomicDestiny], 
                                     [LandArea], 
                                     [BuiltArea], 
                                     [Appraisal], 
                                     [Latitude], 
                                     [Longitude], 
                                     [Status], 
                                     [CreationUser], 
                                     [CreationDate], 
                                     [ModificationUser], 
                                     [ModificationDate]
                                    )
                                    VALUES
                                    (@CodeDeparment, 
                                     @CodeCity, 
                                     @Code, 
                                     1, 
                                     @thirdPartyId, 
                                     @Addres, 
                                     @Commune, 
                                     @EconomicDestiny, 
                                     @LandArea, 
                                     @BuiltArea, 
                                     @Appraisal, 
                                     0, 
                                     0, 
                                     1, 
                                     @User, 
                                     [Common].[GETDATE](), 
                                     NULL, 
                                     NULL
                                    );
                                    DECLARE @TaxesPropertyId AS INT;
                                    SET @TaxesPropertyId = SCOPE_IDENTITY();
                                    INSERT INTO [Taxes].[TaxesPropertyAppraisal]
                                    ([TaxesPropertyId], 
                                     [Validity], 
                                     [Appraisal], 
                                     [CreationDate]
                                    )
                                    VALUES
                                    (@TaxesPropertyId, 
                                     @Validity, 
                                     @Appraisal, 
                                     [Common].[GETDATE]()
                                    );
                                    INSERT INTO [Taxes].[TaxesPropertyOwner]
                                    ([TaxesPropertyId], 
                                     [ThirdPartyId], 
                                     [OrderOwner], 
                                     [Percentage]
                                    )
                                    VALUES
                                    (@TaxesPropertyId, 
                                     @thirdPartyId, 
                                     @OrderOwner, 
                                     100
                                    );
                            END;
                                ELSE
                                BEGIN
                                    --si ya existe solo se actualiza el valor
                                    UPDATE Taxes.TaxesProperty
                                      SET 
                                          Appraisal = @Appraisal
                                    WHERE Code = @Code;
                                    SELECT @TaxesPropertyId = Id
                                    FROM Taxes.TaxesProperty
                                    WHERE Code = @Code;
                                    INSERT INTO [Taxes].[TaxesPropertyAppraisal]
                                    ([TaxesPropertyId], 
                                     [Validity], 
                                     [Appraisal], 
                                     [CreationDate]
                                    )
                                    VALUES
                                    (@TaxesPropertyId, 
                                     @Validity, 
                                     @Appraisal, 
                                     [Common].[GETDATE]()
                                    );

                                    --valido que no este creado el tercero en la tabla TaxesPropertiesOwner
                                    IF
                                    (
                                        SELECT COUNT(*)
                                        FROM Taxes.TaxesPropertyOwner po
                                             INNER JOIN Taxes.TaxesProperty p ON po.TaxesPropertyId = p.Id
                                             INNER JOIN Common.ThirdParty tp ON po.ThirdPartyId = tp.Id
                                        WHERE tp.Id = @thirdPartyId
                                              AND p.Code = @Code
                                    ) = 0
                                        BEGIN
                                            INSERT INTO [Taxes].[TaxesPropertyOwner]
                                            ([TaxesPropertyId], 
                                             [ThirdPartyId], 
                                             [OrderOwner], 
                                             [Percentage]
                                            )
                                            VALUES
                                            (@TaxesPropertyId, 
                                             @thirdPartyId, 
                                             @OrderOwner, 
                                             100
                                            );
                                    END;
                            END;
                    END;
                        ELSE
                        BEGIN

                            --si existe solo agrego el registro en la tabla taxesPropertieyOwner
                            IF
                            (
                                SELECT COUNT(*)
                                FROM Taxes.TaxesPropertyOwner po
                                     INNER JOIN Taxes.TaxesProperty p ON po.TaxesPropertyId = p.Id
                                     INNER JOIN Common.ThirdParty tp ON po.ThirdPartyId = tp.Id
                                WHERE tp.Id = @thirdPartyId
                                      AND p.Code = @Code
                            ) = 0
                                BEGIN
                                    SELECT @TaxesPropertyId = Id
                                    FROM Taxes.TaxesProperty
                                    WHERE Code = @Code;
                                    INSERT INTO [Taxes].[TaxesPropertyOwner]
                                    ([TaxesPropertyId], 
                                     [ThirdPartyId], 
                                     [OrderOwner], 
                                     [Percentage]
                                    )
                                    VALUES
                                    (@TaxesPropertyId, 
                                     @thirdPartyId, 
                                     @OrderOwner, 
                                     100
                                    );
                            END;
                    END;

                    --recorro los items para poner el orden y el porcentaje			
                    DECLARE @CountTaxesPropertyOwner AS INT;
                    SELECT @CountTaxesPropertyOwner = COUNT(*)
                    FROM Taxes.TaxesProperty tp
                         INNER JOIN Taxes.TaxesPropertyOwner po ON tp.Id = po.TaxesPropertyId
                    WHERE tp.Code = @Code;
                    IF(@CountTaxesPropertyOwner > 1)
                        BEGIN
                            DECLARE owner_cursor CURSOR
                            FOR SELECT po.Id
                                FROM Taxes.TaxesProperty tp
                                     INNER JOIN Taxes.TaxesPropertyOwner po ON tp.Id = po.TaxesPropertyId
                                WHERE tp.Code = @Code;
                            OPEN owner_cursor;
                            FETCH NEXT FROM owner_cursor INTO @TaxesPropertyOwnerId;
                            WHILE @@FETCH_STATUS = 0
                                BEGIN
                                    UPDATE Taxes.TaxesPropertyOwner
                                      SET 
                                          Percentage = 100.00 / @CountTaxesPropertyOwner
                                    WHERE Id = @TaxesPropertyOwnerId;
                                    FETCH NEXT FROM owner_cursor INTO @TaxesPropertyOwnerId;
                    END;
                            CLOSE owner_cursor;
                            DEALLOCATE owner_cursor;
                    END;

                    --agrego el codigo a la tabla temporal
                    INSERT INTO @RecordsCodes
                    VALUES(@Code);
                    INSERT INTO Taxes.ResultLoadPlaneCollection
                    VALUES
                    (1, 
                     'Ok', 
                     ''
                    );
                    FETCH NEXT FROM record_cursor INTO @CodeDeparment, @CodeCity, @Code, @OrderOwner, @ThirdName, @Nit, @Addres, @Commune, @EconomicDestiny, @LandArea, @BuiltArea, @Appraisal, @Validity, @thirdPartyId;
                END;
            CLOSE record_cursor;
            DEALLOCATE record_cursor;
            DELETE Taxes.ResultLoadPlaneCollection;
            SELECT 1 AS [Status], 
                   'Se confirmo correctamente el avaluo' AS [Message];
            RETURN;
        END TRY
        BEGIN CATCH
            SELECT 0 AS [Status], 
                   ERROR_MESSAGE() + ', Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) [Message];
        END CATCH;

        --select 0 as [Status], '' as [Message]
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que procesa la carga de un archivo plano de avalúos catastrales para la liquidación de impuestos prediales. Recibe los registros en formato XML, los descompone por posición fija para extraer código de predio, NIT del propietario, nombre del tercero, áreas, avalúo y vigencia. Cruza los nombres de terceros contra palabras clave configuradas en Taxes.SettingsTaxesWords para identificar automáticamente el tercero ya registrado; si el propietario no existe en Common.Person, lo registra como persona nueva usando el NIT como número de identificación. Está diseñado para el proceso de importación masiva de información catastral municipal y la creación automática de terceros/propietarios en el sistema tributario.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_SaveLoadPlaneCollection';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'PROCEDURE', @level1name = N'SP_SaveLoadPlaneCollection';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa un archivo plano XML de avalúos catastrales para crear/actualizar predios, propietarios y terceros en el sistema tributario, parseando registros de longitud fija y distribuyendo porcentajes de propiedad.', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener estructura /Data/Record con cadenas de longitud fija parseables por posiciones específicas (depto 1-2, ciudad 3-3, código 6-15, etc.); El usuario indicado debe existir en Security.User para resolver su Id; Las palabras de mapeo de terceros deben estar configuradas previamente en Taxes.SettingsTaxesWords', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El NIT se normaliza eliminando ceros a la izquierda mediante PATINDEX(''%[^0]%'', Nit); Los predios siempre se crean con Taxed=1, Status=1 y coordenadas 0,0; Un nuevo propietario siempre se inserta inicialmente con Percentage=100; el reparto equitativo se hace después si hay más de uno; Nunca se duplica un TaxesPropertyOwner para el mismo (predio, tercero): se valida con COUNT antes de insertar; Cuando el NIT viene en ceros, la identidad del tercero se sintetiza con código catastral + OrderOwner padded a 2 dígitos; Los errores no se propagan: el CATCH devuelve un resultset con Status=0 en lugar de relanzar', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'avalúo catastral; predio; propietario; tercero; NIT; código catastral; departamento; municipio; comuna; destino económico; área de terreno; área construida; vigencia; porcentaje de propiedad; impuesto predial', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Common.Person: Cuando r.Nit != ''000000000000'' y la persona no existe (p.Id IS NULL) y r.ThirdPartyId = 0, inserta una persona usando el NIT como IdentificationNumber y el ThirdName como FirstName, con State=1; [INSERT] Common.Person: Cuando r.Nit = ''000000000000'' y la persona no existe y r.ThirdPartyId = 0, inserta una persona usando como IdentificationNumber el código catastral concatenado con OrderOwner (padded a 2); [INSERT] Taxes.TaxesProperty: Cuando el código no está en @RecordsCodes y no existe en Taxes.TaxesProperty, inserta el predio con Taxed=1, Status=1, Latitude=0, Longitude=0 y CreationUser=usuario; [UPDATE] Taxes.TaxesProperty: Cuando el predio ya existe (mismo Code), actualiza únicamente el campo Appraisal con el nuevo valor; [INSERT] Taxes.TaxesPropertyAppraisal: Por cada predio procesado (nuevo o existente, en la primera vez que aparece el código en el lote) inserta un registro de avalúo con Validity y Appraisal del archivo; [INSERT] Taxes.TaxesPropertyOwner: Cuando el predio es nuevo, inserta el propietario con Percentage=100. Si el predio ya existía, sólo inserta si no hay un registro previo con ese ThirdPartyId y predio; [UPDATE] Taxes.TaxesPropertyOwner: Cuando un predio tiene más de un propietario (CountTaxesPropertyOwner > 1), recalcula Percentage = 100/N para todos sus propietarios; [INSERT] Taxes.ResultLoadPlaneCollection: Por cada registro procesado en el cursor inserta una fila con (1,''Ok'',''''); [DELETE] Taxes.ResultLoadPlaneCollection: Al finalizar el procesamiento del cursor, borra todos los registros de la tabla de resultados; [RETURN_RESULT] RESULT: Devuelve Status=1 y mensaje de éxito al terminar; en CATCH devuelve Status=0 con ERROR_MESSAGE() y línea del error', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SUBSTRING(record,63,12) contiene ''-'' → Toma sólo la parte previa al guion como Nit else Toma los 12 caracteres completos como Nit; si El ThirdName del registro coincide (LIKE) con alguna Word de Taxes.SettingsTaxesWords → Asigna directamente el ThirdPartyId configurado, omitiendo el alta de persona/tercero; si Nit != ''000000000000'' → Resuelve/crea tercero usando el NIT real como IdentificationNumber else Resuelve/crea tercero usando código catastral + OrderOwner como identificador sintético; si El Code aún no fue procesado en este lote (@RecordsCodes) y el predio no existe en Taxes.TaxesProperty → Crea predio + avalúo + propietario al 100% else Si el predio existe, actualiza avalúo e inserta propietario sólo si no estaba; si el code ya se procesó en el lote, solo agrega propietario faltante; si Cantidad de propietarios del predio > 1 → Redistribuye el porcentaje en partes iguales (100/N) entre todos los propietarios', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.User; Taxes.SettingsTaxesWords; Common.Person; Common.ThirdParty; Taxes.TaxesProperty; Taxes.TaxesPropertyOwner', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadPlaneCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Taxes', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLoadPlaneCollection';
-- GO
