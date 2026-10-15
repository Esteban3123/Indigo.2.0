/****** Object:  StoredProcedure [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* ================================================================================================================
    [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement]
    Propósito: Este SP valida un movimiento contable antes de encolarlo para su procesamiento asíncrono.
   ============================================================================================================== */
CREATE PROCEDURE [GeneralLedger].[SP_CreateAndValidateJournalVoucherMovement]
    @JournalVoucherXml        XML,
    @CodeUser                VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CodeMessage  INT,
    @Message              VARCHAR(MAX) ,
    @AccountingMovementId INT          

    /* ************************************************************************************************************
        SECCIÓN 0 y 1: VARIABLES, TABLAS TEMPORALES Y EXTRACCIÓN DE CABECERA
       ************************************************************************************************************ */

    -- Se declara una tabla temporal para almacenar los detalles del XML.
    CREATE TABLE #DetailTemp (
        RowId           INT IDENTITY(1,1) PRIMARY KEY,
        Id              INT, 
        IdMainAccount   INT, 
        IdThirdParty    INT, 
        IdCostCenter    INT,
        DebitValue_Orig     DECIMAL(21,5), 
        CreditValue_Orig    DECIMAL(21,5),
        Detail              VARCHAR(MAX), 
        IdRetention         INT, 
        RetentionRate       DECIMAL(6,3),
        BaseValue_Orig      DECIMAL(18,2), 
        BillingValue_Orig   DECIMAL(18,2)
    );

    -- Se declaran las variables necesarias para la cabecera y el proceso de validación.
    DECLARE
        @LegalBookId            INT, 
        @IdJournalVoucherType   INT,
        @VoucherDate            DATETIME, 
        @Status                 TINYINT,
        @Detail                 VARCHAR(MAX),
        @EntityCode             VARCHAR(20), 
        @EntityId               INT, 
        @EntityName             VARCHAR(250),
        @OriginEntityName       VARCHAR(250),
        @IsClosedYear           TINYINT, 
        @OperatingUnitId        INT,
        @CurrencyId             INT, 
        @DateTRM                DATE,
        @OfficialLegalBookId    INT, 
        @BookCurrencyId         INT,
        @EntityNameConversion   VARCHAR(250), 
		@IdJournalVoucher INT;

		
    -- Se extraen los datos del encabezado del XML a las variables locales.
    SELECT
        @LegalBookId             = J.x.value('LegalBookId[1]' ,'int'),
        @IdJournalVoucherType    = J.x.value('IdJournalVoucher[1]','int'),
        @VoucherDate			 = J.x.value('VoucherDate[1]','datetime'),
        @Status                  = J.x.value('Status[1]' ,'tinyint'),
        @Detail                  = J.x.value('Detail[1]' ,'varchar(max)'),
        @EntityCode              = J.x.value('EntityCode[1]' ,'varchar(20)'),
        @EntityId                = J.x.value('EntityId[1]' ,'int'),
        @EntityName              = ISNULL(J.x.value('EntityName[1]','varchar(250)'),'JournalVouchers'),
        @OriginEntityName        = NULLIF(J.x.value('OriginEntityName[1]','varchar(250)'), ''),
        @IsClosedYear            = J.x.value('IsClosedYear[1]' ,'tinyint'),
        @OperatingUnitId         = J.x.value('OperatingUnitId[1]','int'),
        @CurrencyId              = J.x.value('CurrencyId[1]' ,'int'),
        @DateTRM                 = J.x.value('DateTRM[1]' ,'date'),
		@IdJournalVoucher = ISNULL(J.x.value('Id[1]','int'),0),
		@AccountingMovementId = ISNULL(J.x.value('AccountingMovementId[1]','int'), 0)
    FROM @JournalVoucherXml.nodes('/JournalVoucher') J(x);

    -- Normaliza el nombre de la entidad para la conversión de moneda.
    SET @EntityNameConversion =
        CASE 
            WHEN @EntityName IN ('AutomaticPortfolioTransfer','AutomaticPortfolioNote')
                 OR @EntityName LIKE 'Automatic%' THEN 'Invoice'
            ELSE @EntityName 
        END;

    IF @EntityName LIKE 'Automatic%' 
        SET @EntityName = SUBSTRING(@EntityName, 10, 250);

    -- Valores por defecto para Libro, Moneda y Fecha TRM si vienen nulos.
    SELECT 
        @OfficialLegalBookId = Id, 
        @BookCurrencyId = OfficialCurrencyId
    FROM GeneralLedger.LegalBook WITH (NOLOCK)
    WHERE OfficialBook = 1 AND Status = 1;

    SET @LegalBookId = ISNULL(@LegalBookId, @OfficialLegalBookId);
    SET @CurrencyId = IIF(ISNULL(@CurrencyId, 0) = 0, @BookCurrencyId, @CurrencyId)
    IF @DateTRM IS NULL OR @DateTRM ='' OR @DateTRM = '0001-01-01' BEGIN
		SET @DateTRM =cast( @VoucherDate as DATE)
	END


    /* ************************************************************************************************************
        SECCIÓN 2: VALIDACIONES GENERALES Y DE PERIODO
       ************************************************************************************************************ */

    BEGIN TRY
        -- 2.1 Valido que exista un Libro Oficial en el sistema.
        IF @OfficialLegalBookId IS NULL
        BEGIN
            SET @CodeMessage = 999; 
            SET @Message = 'No se puede procesar el comprobante porque no existe un libro oficial configurado en el sistema.';
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- 2.2 Valido que el año no esté cerrado para el libro contable.
        IF EXISTS (SELECT 1 FROM GeneralLedger.LegalBook lb WITH (NOLOCK) WHERE lb.Id = @LegalBookId AND YEAR(@VoucherDate) <= lb.LastYearClose)
        BEGIN
            SET @CodeMessage = 999; 
                   SET @Message = CONCAT('El año ', YEAR(@VoucherDate), ' se encuentra cerrado para el libro contable especificado.'); 
                   SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- 2.3 Valido que el mes contable esté abierto (si no es un movimiento de cierre).
        IF @IsClosedYear = 0 AND NOT EXISTS (SELECT 1 FROM GeneralLedger.ClosedMonth WITH (NOLOCK) WHERE Year = YEAR(@VoucherDate) AND Month = MONTH(@VoucherDate) AND Status = 1)
        BEGIN
            SET @CodeMessage = 999; 
            SET @Message = CONCAT('El periodo contable ', YEAR(@VoucherDate), '-', FORMAT(MONTH(@VoucherDate), '00'), ' no se encuentra abierto.');
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- 2.4 Valido que la fecha del comprobante no sea futura.
        IF CAST(@VoucherDate AS DATE) > CAST(GETDATE() AS DATE)
        BEGIN
            SET @CodeMessage = 999; 
            SET @Message = 'No se permiten comprobantes con fechas futuras.'; 
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- 2.5 Valido que exista una TRM si las monedas son diferentes.
        IF @CurrencyId <> @BookCurrencyId AND [Common].[CurrencyConverterByModule](1, @CurrencyId, @BookCurrencyId, NULL, @EntityNameConversion, @DateTRM) IS NULL
        BEGIN
            SET @CodeMessage = 999; 
            SET @Message = CONCAT('No existe una Tasa de Cambio (TRM) válida para la fecha ', CONVERT(VARCHAR, @DateTRM, 103), ' entre las monedas especificadas.');
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

    END TRY
    BEGIN CATCH
        -- Captura de errores inesperados durante la validación.
        SET @CodeMessage = 999; 
        SET @Message = CONCAT('Error inesperado en la Sección 2: ', ERROR_MESSAGE());
        SET @AccountingMovementId = 0;
        select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
		RETURN
    END CATCH

    --Verifica que el tipo del comprobante este creado
    IF NOT EXISTS (SELECT 1
                   FROM GeneralLedger.JournalVoucherTypes
                   WHERE Id = @IdJournalVoucherType
                     AND Status = 1)
    BEGIN
        SET @CodeMessage = 999;
        SET @Message      = 'el tipo de comprobante especificado no existe o está inactivo.';
        SET @AccountingMovementId = 0;
        select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
		RETURN
    END

    /*estimación de costos cerrada */
    IF EXISTS (SELECT 1
               FROM Cost.CostEstimationNative WITH (NOLOCK)
               WHERE Year  = YEAR(@VoucherDate)
                 AND Month = MONTH(@VoucherDate))
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = CONCAT('no se puede crear el comprobante porque existe una estimación de costos cerrada en ', YEAR(@VoucherDate), '-', RIGHT(CONCAT('00', MONTH(@VoucherDate)), 2));
        SET @AccountingMovementId = 0;
        select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
		RETURN
    END

    /* moneda activa */
    IF EXISTS (SELECT 1
               FROM Common.Currency c
               WHERE c.Id = @CurrencyId
                 AND c.State = 0)
    BEGIN
        SET @CodeMessage = 999;
        SET @Message = 'la moneda especificada está inactiva en el catálogo de monedas.';
        SET @AccountingMovementId = 0;
        select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
		RETURN
    END

    /* ************************************************************************************************************
        SECCIÓN 3: EXTRACCIÓN Y VALIDACIÓN DE DETALLES
       ************************************************************************************************************ */

    BEGIN TRY
        -- 3.1 Extraer los detalles del XML a la tabla temporal.
        INSERT INTO #DetailTemp (
            Id, IdMainAccount, IdThirdParty, IdCostCenter, DebitValue_Orig, CreditValue_Orig,
            Detail, IdRetention, RetentionRate, BaseValue_Orig, BillingValue_Orig
        )
        SELECT
            ISNULL(D.x.value('(Id/text())[1]', 'int'), 0),
            D.x.value('(IdMainAccount/text())[1]', 'int'),
            NULLIF(D.x.value('(IdThirdParty/text())[1]', 'int'), 0),
            NULLIF(D.x.value('(IdCostCenter/text())[1]', 'int'), 0),
            ISNULL(D.x.value('(DebitValue/text())[1]', 'decimal(21,5)'), 0),
            ISNULL(D.x.value('(CreditValue/text())[1]', 'decimal(21,5)'), 0),
            D.x.value('(Detail/text())[1]', 'varchar(max)'),
            D.x.value('(IdRetention/text())[1]', 'int'),
            D.x.value('(RetentionRate/text())[1]', 'decimal(6,3)'),
            ISNULL(D.x.value('(BaseValue/text())[1]', 'decimal(18,2)'), 0),
            ISNULL(D.x.value('(BillingValue/text())[1]', 'decimal(18,2)'), 0)
        FROM @JournalVoucherXml.nodes('/JournalVoucher/JournalVoucherDetail') D(x)
        WHERE ISNULL(D.x.value('(IsDelete/text())[1]', 'bit'), 0) = 0;

        -- Detecta si el comprobante no tiene detalles
        IF NOT EXISTS (SELECT 1 FROM #DetailTemp)
        BEGIN
            SET @CodeMessage = 999;
            SET @Message = 'el comprobante no tiene detalles.';
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- Ningún detalle puede tener ambos valores en cero
        IF EXISTS (SELECT 1
                   FROM #DetailTemp
                   WHERE ISNULL(DebitValue_Orig, 0) = 0
                     AND ISNULL(CreditValue_Orig, 0) = 0)
        BEGIN
            SET @CodeMessage = 999;
            SET @Message      = 'cada línea debe registrar un débito o un crédito mayor a cero.';
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- La base de una retención no puede superar el valor facturado
        IF EXISTS (SELECT 1
                   FROM #DetailTemp
                   WHERE ISNULL(BaseValue_Orig, 0) > ISNULL(BillingValue_Orig, 0))
        BEGIN
            SET @CodeMessage = 999;
            SET @Message      = 'la base de retención no puede superar el valor facturable de la línea.';
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- Si se informa IdRetention, se requiere tasa y base > 0
        IF EXISTS (SELECT 1
                   FROM #DetailTemp
                   WHERE ISNULL(IdRetention, 0) <> 0
                     AND (ISNULL(RetentionRate, 0) <= 0
                          OR ISNULL(BaseValue_Orig, 0) <= 0))
        BEGIN
            SET @CodeMessage = 999;
            SET @Message      = 'para aplicar una retención se requiere tasa y base mayores a cero.';
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- Detecta cuentas inexistentes para cualquier línea
        IF EXISTS (SELECT 1
                   FROM #DetailTemp dt
                   LEFT JOIN GeneralLedger.MainAccounts ma ON dt.IdMainAccount = ma.Id
                   WHERE ma.Id IS NULL)
        BEGIN
            SET @CodeMessage = 999;
            SET @Message      = 'una o más cuentas contables no existen en el plan de cuentas.';
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- Tercero inexistente
        IF EXISTS (SELECT 1
                   FROM #DetailTemp dt
                   WHERE dt.IdThirdParty IS NOT NULL
                     AND NOT EXISTS (SELECT 1 FROM Common.ThirdParty tp WHERE tp.Id = dt.IdThirdParty))
        BEGIN
            SET @CodeMessage = 999;
            SET @Message      = 'se indicó un tercero que no existe en la base de datos.';
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

        -- Centro de costo inexistente
        IF EXISTS (SELECT 1
                   FROM #DetailTemp dt
                   WHERE dt.IdCostCenter IS NOT NULL
                     AND NOT EXISTS (SELECT 1 FROM Payroll.CostCenter cc WHERE cc.Id = dt.IdCostCenter))
        BEGIN
            SET @CodeMessage = 999;
            SET @Message      = 'se indicó un centro de costo que no existe en la base de datos.';
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END


        -- 3.2 Realizar todas las validaciones de negocio sobre los detalles en un solo bloque.
        DECLARE @ValidationErrors VARCHAR(MAX);

        WITH DetailValidations AS (
            SELECT
                -- Obligatoriedad
                CASE WHEN ma.HandlesThirdParty = 1 AND dt.IdThirdParty IS NULL 
                    THEN 'La cuenta ' + ma.Number + ' requiere un tercero.' ELSE NULL END AS MissingThirdParty,
                CASE WHEN ma.HandlesCostCenter = 1 AND dt.IdCostCenter IS NULL 
                    THEN 'La cuenta ' + ma.Number + ' requiere un centro de costo.' ELSE NULL END AS MissingCostCenter,
                -- Estado de Cuentas
                CASE WHEN ma.AllowsMovement = 0 
                    THEN 'La cuenta ' + ma.Number + ' no permite movimientos.' ELSE NULL END AS NoMovementAllowed,
                CASE WHEN ma.Status = 0 
                    THEN 'La cuenta ' + ma.Number + ' está inactiva.' ELSE NULL END AS InactiveAccount,
                CASE WHEN ma.LegalBookId <> @LegalBookId AND @LegalBookId = @OfficialLegalBookId
                    THEN 'La cuenta ' + ma.Number + ' no pertenece al libro contable: ' + lb.Code + ' - ' + lb.Name + '.' ELSE NULL END  As AccountInvalidBook,
				CASE WHEN @LegalBookId <> @OfficialLegalBookId AND (SELECT ma.LegalBookId  
																	FROM GeneralLedger.HomologationAccount ha 
																	JOIN GeneralLedger.MainAccounts ma ON ma.Id = ha.MainAccountId
																	WHERE ha.OfficialMainAccountId = dt.IdMainAccount) <> @LegalBookId
					THEN 'La cuenta ' + ma.Number + ' no pertenece al libro contable: ' + lb.Code + ' - ' + lb.Name + '.' ELSE NULL END  As AccountHomologatedInvalidBook,
				CASE WHEN @IsClosedYear = 0  AND ma.RetencionType > 0 AND (dt.IdRetention IS NULL OR dt.RetentionRate IS NULL OR dt.BaseValue_Orig IS NULL) AND @EntityName NOT IN ('VoucherTransaction', 'TreasuryNote', 'PortfolioTransfer', 'PayrollLiquidation')
					THEN 'La cuenta ' + ma.Number + ' - ' + ma.Name + ' maneja retención y no tiene información completa de la retención.' ELSE NULL END AS MissingRetentionInfo,
                -- Estado de Terceros y CC
                CASE WHEN tp.Id IS NOT NULL AND tp.State = 0 
                    THEN tp.Nit + ' El tercero ' + tp.Name + ' está inactivo.' ELSE NULL END AS InactiveThirdParty,
                CASE WHEN cc.Id IS NOT NULL AND cc.State = 0 
                    THEN 'El centro de costo ' + cc.Name + ' está inactivo.' ELSE NULL END AS InactiveCostCenter,
                -- Valores
                CASE WHEN dt.DebitValue_Orig < 0 OR dt.CreditValue_Orig < 0 
                    THEN 'No se permiten valores negativos en los detalles.' ELSE NULL END AS NegativeValue,
                CASE WHEN dt.DebitValue_Orig > 0 AND dt.CreditValue_Orig > 0 
                    THEN 'Un detalle no puede ser débito y crédito a la vez.' ELSE NULL END AS DebitAndCredit,
                -- Lógica de Restricciones (
                CASE
                    WHEN ma.HandlesThirdPartyRestriction = 1 
                        AND dt.IdThirdParty IS NOT NULL 
                        AND EXISTS (SELECT 1 FROM GeneralLedger.MainAccountRestrictions r WHERE r.MainAccountId = ma.Id AND r.ItemType = 2 AND r.AllItems = 1 AND r.RestrictionType = 2) 
                        AND NOT EXISTS (SELECT 1 FROM GeneralLedger.MainAccountRestrictions r WHERE r.MainAccountId = ma.Id AND r.ItemType = 2 AND r.ThirdPartyId = dt.IdThirdParty AND r.RestrictionType = 1)
                    THEN 'El tercero ' + tp.Name + ' tiene una restricción con la cuenta ' + ma.Number + '.'
                    ELSE NULL
                END AS ThirdPartyRestriction,
                CASE
                    WHEN ma.HandlesCostCenterRestriction = 1 
                        AND dt.IdCostCenter IS NOT NULL 
                        AND EXISTS (SELECT 1 FROM GeneralLedger.MainAccountRestrictions r WHERE r.MainAccountId = ma.Id AND r.ItemType = 1 AND r.AllItems = 1 AND r.RestrictionType = 2) 
                        AND NOT EXISTS (SELECT 1 FROM GeneralLedger.MainAccountRestrictions r WHERE r.MainAccountId = ma.Id AND r.ItemType = 1 AND r.CostCenterId = dt.IdCostCenter AND r.RestrictionType = 1)
                    THEN 'El centro de costo ' + cc.Name + ' tiene una restricción con la cuenta ' + ma.Number + '.'
                    ELSE NULL
                END AS CostCenterRestriction
            FROM #DetailTemp dt
            JOIN GeneralLedger.MainAccounts ma ON dt.IdMainAccount = ma.Id
            LEFT JOIN Common.ThirdParty tp ON dt.IdThirdParty = tp.Id
            LEFT JOIN Payroll.CostCenter cc ON dt.IdCostCenter = cc.Id
            LEFT JOIN GeneralLedger.LegalBook lb on lb.Id = @LegalBookId
        )
        SELECT @ValidationErrors = STRING_AGG(ErrorMessage, ' ')
        FROM (
            SELECT MissingThirdParty FROM DetailValidations WHERE MissingThirdParty IS NOT NULL
            UNION ALL
            SELECT MissingCostCenter FROM DetailValidations WHERE MissingCostCenter IS NOT NULL
            UNION ALL
            SELECT NoMovementAllowed FROM DetailValidations WHERE NoMovementAllowed IS NOT NULL
            UNION ALL
            SELECT InactiveAccount FROM DetailValidations WHERE InactiveAccount IS NOT NULL
            UNION ALL
            SELECT InactiveThirdParty FROM DetailValidations WHERE InactiveThirdParty IS NOT NULL
            UNION ALL
            SELECT InactiveCostCenter FROM DetailValidations WHERE InactiveCostCenter IS NOT NULL
            UNION ALL
            SELECT NegativeValue FROM DetailValidations WHERE NegativeValue IS NOT NULL
            UNION ALL
            SELECT DebitAndCredit FROM DetailValidations WHERE DebitAndCredit IS NOT NULL
            UNION ALL
            SELECT ThirdPartyRestriction FROM DetailValidations WHERE ThirdPartyRestriction IS NOT NULL
            UNION ALL
            SELECT CostCenterRestriction FROM DetailValidations WHERE CostCenterRestriction IS NOT NULL
            UNION ALL
            SELECT AccountInvalidBook FROM DetailValidations WHERE AccountInvalidBook IS NOT NULL
			UNION ALL
			SELECT AccountHomologatedInvalidBook FROM DetailValidations WHERE AccountHomologatedInvalidBook IS NOT NULL
			UNION ALL
			SELECT MissingRetentionInfo FROM DetailValidations WHERE MissingRetentionInfo IS NOT NULL
        ) AS AllErrors(ErrorMessage);

        -- 3.3 Si se encontró algún error, se retorna el mensaje consolidado.
        IF @ValidationErrors IS NOT NULL
        BEGIN
            SET @CodeMessage = 999; 
            SET @Message = 'Errores en los detalles del comprobante: ' + @ValidationErrors;
            SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

    END TRY
    BEGIN CATCH
        SET @CodeMessage = 999; 
        SET @Message = CONCAT('Error inesperado en la Sección 3: ', ERROR_MESSAGE());
        SET @AccountingMovementId = 0;
        select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
		RETURN
    END CATCH

    /* ************************************************************************************************************
        SECCIÓN 4: VALIDACIÓN DE BALANCE (SUMAS IGUALES)
       ************************************************************************************************************ */

    BEGIN TRY
        -- 4.1 Calcular la tasa de conversión. Si las monedas son iguales, la tasa es 1.
        DECLARE @ConversionRate DECIMAL(21, 10);
        IF @CurrencyId = @BookCurrencyId
            SET @ConversionRate = 1.0;
        ELSE
            SET @ConversionRate = [Common].[CurrencyConverterByModule](1, @CurrencyId, @BookCurrencyId, NULL, @EntityNameConversion, @DateTRM);

        -- 4.2 Aplicar la conversión y sumar los totales.
        DECLARE @TotalDebit DECIMAL(21, 5), @TotalCredit DECIMAL(21, 5);
        SELECT
            @TotalDebit = SUM(dt.DebitValue_Orig * @ConversionRate),
            @TotalCredit = SUM(dt.CreditValue_Orig * @ConversionRate)
        FROM #DetailTemp dt;

        -- 4.3 Validar que los totales (débito y crédito) sean iguales, aplicando la regla de redondeo a -1.
        IF ROUND(ISNULL(@TotalDebit, 0), -1) <> ROUND(ISNULL(@TotalCredit, 0), -1)
        BEGIN
            SELECT
                @CodeMessage = 999;
                SET @Message = CONCAT('El comprobante está desbalanceado. Total Débitos: ', FORMAT(ISNULL(@TotalDebit, 0), 'N2'), ', Total Créditos: ', FORMAT(ISNULL(@TotalCredit, 0), 'N2'));
                SET @AccountingMovementId = 0;
            select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
			RETURN
        END

    END TRY
    BEGIN CATCH
        SET @CodeMessage = 999; 
        SET @Message = CONCAT('Error inesperado en la Sección 4: ', ERROR_MESSAGE());
        SET @AccountingMovementId = 0;
        select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
		RETURN
    END CATCH

    /* ************************************************************************************************************
        SECCIÓN 5: INSERCIÓN DEL MOVIMIENTO EN LA COLA
       ************************************************************************************************************ */

    BEGIN TRY
		--Si apenas se esta creando el comprobante se inserta el registro del movimiento
		IF @AccountingMovementId = 0
		BEGIN
			-- 5.1 Si todas las validaciones pasaron, se inserta el registro en la tabla AccountingMovement.
			INSERT INTO GeneralLedger.AccountingMovement (
				LegalBookId,
				JournalVoucherTypeId,
				VoucherDate,
				Detail,
				EntityCode,
				EntityId,
				EntityName,
				JournalVoucherXml,
				CreationUser,
				CreationDate
			)
			VALUES (
				@LegalBookId,
				@IdJournalVoucherType,
				@VoucherDate,
				SUBSTRING(@Detail, 1, 500),
				@EntityCode,
				@EntityId,
				@EntityName,
				@JournalVoucherXml,
				@CodeUser,
				[Common].[GETDATE]()
			);
		END
		ELSE
		BEGIN
			UPDATE GeneralLedger.AccountingMovement WITH (ROWLOCK)
			SET JournalVoucherXml = @JournalVoucherXml
			WHERE ID = @AccountingMovementId
		END

		-- 5.2 Se obtiene el ID del movimiento recién creado para devolverlo como salida.
		SET @AccountingMovementId = 0;

		-- 5.3 Se establecen los mensajes de éxito.
		SET @CodeMessage = 0;  
		SET @Message = 'Movimiento contable validado y encolado para procesamiento.';

		select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
		RETURN

    END TRY
    BEGIN CATCH
        SET @CodeMessage = 999;
        SET @Message = CONCAT('Error inesperado al insertar el movimiento en la cola: ', ERROR_MESSAGE());
        SET @AccountingMovementId = 0;

        select @CodeMessage AS CodeMessage,  @Message AS [Message], @AccountingMovementId AS IdJournalVoucher;
        RETURN
    END CATCH
END
