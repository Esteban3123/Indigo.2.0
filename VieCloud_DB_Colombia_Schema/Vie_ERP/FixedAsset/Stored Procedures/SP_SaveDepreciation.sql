-- =============================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/02/2016
-- Description:	Genera y guarda la depreciación de los activos
-- =============================================================
CREATE   PROCEDURE [FixedAsset].[SP_SaveDepreciation] 
    @DepreciateMonth AS INT,
	@DepreciateYear AS INT,
	@CodeUser AS VARCHAR(20),
	@OperatingUnitId AS INT,
	@ModeConfirm AS BIT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Id INT,
			@Code VARCHAR(20),
			@ProcessDate date,
			------------------------------------------
			@Depreciation30Days BIT,
			@DateDepreciateInitial DATE,
			@DateDepreciateEnd DATE,
			@DaysDepreciateMonth INT,
			------------------------------------------
			@Message VARCHAR(MAX),
			@JournalVoucherType VARCHAR(MAX) = '',
			@ResultConsecutives VARCHAR(MAX) = '',
			------------------------------------------
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@JournalVoucherType_Output VARCHAR(MAX) = '',
			@ResultConsecutives_Output VARCHAR(MAX) = '',
			@FinalMonthDay INT
			
	-- depresia a 30 dias
	--Declare @Depreciation30Days INT 

	BEGIN TRY

		------------------------------------------------  VALIDACIONES ------------------------------------------------

		--se valida que todos los parámetros tengan el mismo periodo
		IF EXISTS
		(
			SELECT 1
			FROM FixedAsset.SettingFixedAsset sfa
			JOIN FixedAsset.SettingFixedAsset sfad ON YEAR(sfa.ProcessDate) <> YEAR(sfad.ProcessDate) OR MONTH(sfa.ProcessDate) <> MONTH(sfad.ProcessDate)
		)
		BEGIN
			SELECT 999 AS CodeMessage, 'Existen unidades operativas con periodos de activos diferentes' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END
		
		--Se valida que existan parametros en contabilidad
		IF NOT EXISTS (SELECT 1 FROM GeneralLedger.GeneralLedgerSettings WHERE IdOperatingUnit = @OperatingUnitId) BEGIN
			SELECT 999 AS CodeMessage, 'No existe parámetros de contabilidad' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END

		--Se valida que exista parámetros de activos fijos en la BD
		IF NOT EXISTS (SELECT 1 FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId)
		BEGIN
			SELECT 999 AS CodeMessage, 'No existe parámetros de activos fijos para realizar la depreciación/amortización' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END

		--Se valida que el mes que se va a depreciar sea el mismo mes que el que tiene parámetros de activos fijos
		IF EXISTS (
			SELECT 1
			FROM FixedAsset.SettingFixedAsset
			WHERE YEAR(ProcessDate) <> @DepreciateYear OR MONTH(ProcessDate) <> @DepreciateMonth
		) BEGIN
			SELECT 999 AS CodeMessage, 'El periodo que se va a depreciar/amortizar no es igual al periodo de proceso en los parámetros de activos fijos' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END
		
		--Se valida que al menos haya un registro en la tabla de physicalAsset para poder realizar las operaciones
		IF NOT EXISTS (
			SELECT 1
			FROM FixedAsset.FixedAssetPhysicalAsset pa 
			INNER JOIN FixedAsset.FixedAssetPhysicalAssetDetailBook padb on padb.PhysicalAssetId = pa.Id
			INNER JOIN GeneralLedger.LegalBook lb on lb.Id = padb.LegalBookId AND lb.[Status] = 1
			WHERE pa.HasOutput = 0
		) BEGIN
			SELECT 999 AS CodeMessage, 'No hay registros de activos para poder depreciar/amortizar' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END

		--Se valida que si se va a confirmar ya se haya calculado previamente la depreciación/amortización
		IF @ModeConfirm = 1 AND NOT EXISTS (
			SELECT 1
			FROM FixedAsset.FixedAssetDepreciation
			WHERE ClosingMonth = @DepreciateMonth AND ClosingYear = @DepreciateYear
		) BEGIN
			SELECT 999 AS CodeMessage, 'Para confirmar primero debe haber depreciado/amortizado' AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END

		--------------------------------------------------  CABECERA --------------------------------------------------

		SELECT	@Id = Id,
				@Code = Code
		FROM FixedAsset.FixedAssetDepreciation 
		WHERE ClosingMonth = @DepreciateMonth AND ClosingYear = @DepreciateYear

		--Se consulta si hay registros con el mes y el año enviados desde el form, si hay no se crea la cabecera
		IF @Id IS NULL
		BEGIN
			--Si se esta insertando por primera vez se consulta la secuencia numerica
			DECLARE @IsManual BIT,
					@IdForm INT = 1122
				
			EXEC Common.SP_GetSequence 210, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT 999 AS CodeMessage, REPLACE(@Message_Output, '{0}', 'Depreciación/Amortización') AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
				RETURN
			END

			--Se inserta en la cabecera de la depreciación
			INSERT INTO [FixedAsset].[FixedAssetDepreciation]
			   ([Code], [ClosingMonth], [ClosingYear], [ClosingDate], [Observation], [OperatingUnitId], [Status])
			VALUES
			   (@Code, @DepreciateMonth, @DepreciateYear, [Common].[GETDATE](), '', @OperatingUnitId, 1)

			--Obtengo el id de la cabecera
			SET @Id = SCOPE_IDENTITY()
		END

		------------------------------------------------  ASIGNACIONES ------------------------------------------------
		SET @Depreciation30Days = (select Depreciation30Days FROM FixedAsset.SettingFixedAsset WHERE OperatingUnitId = @OperatingUnitId) 
		SET @DateDepreciateInitial = DATEFROMPARTS(@DepreciateYear, @DepreciateMonth, 1)

		IF @Depreciation30Days = 1
		BEGIN
			SET @FinalMonthDay = DAY(EOMONTH(@DateDepreciateInitial))

			SET @DateDepreciateEnd = (DATEFROMPARTS(@DepreciateYear, @DepreciateMonth, @FinalMonthDay))
		END
		ELSE
		BEGIN

			SET @DateDepreciateEnd = DATEADD(MONTH, 1, @DateDepreciateInitial)

		END

		SELECT	@ProcessDate = ProcessDate,
				@DaysDepreciateMonth = IIF( Depreciation30Days = 1, 30, DATEDIFF(DAY, @DateDepreciateInitial, @DateDepreciateEnd))
				
		FROM FixedAsset.SettingFixedAsset 
		WHERE OperatingUnitId = @OperatingUnitId

		------------------------------------------------  CONFIRMACION ------------------------------------------------		
		
		--Se empieza la confirmación por validaciones de triggers
		IF @ModeConfirm = 1 --Si se está confirmando
		BEGIN
			--Se actualiza el estado de la depreciación
			UPDATE FixedAsset.FixedAssetDepreciation SET Status = 2 WHERE Id = @Id

		END

		--------------------------------------------  PROCESO DEPRECIACION --------------------------------------------

		EXEC [FixedAsset].[SP_SaveDepreciationDetail] 
			@Id,
			@ModeConfirm,
			@OperatingUnitId,
			@DateDepreciateInitial,
			@DateDepreciateEnd,
			@DaysDepreciateMonth,
			@CodeUser,
			---------------------------
			@Code_Output OUT, 
			@Message_Output OUT, 
			@JournalVoucherType_Output OUT, 
			@ResultConsecutives_Output OUT

		IF @Code_Output <> 0
		BEGIN
			SELECT 999 AS CodeMessage, @Message_Output AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END

		SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		SET @JournalVoucherType = ISNULL(@JournalVoucherType, '') + IIF(@JournalVoucherType_Output = '', '', IIF(ISNULL(@JournalVoucherType, '') = '', '', CHAR(13) + CHAR(10)) + @JournalVoucherType_Output)
		SET @ResultConsecutives = ISNULL(@ResultConsecutives, '') + IIF(@ResultConsecutives_Output = '', '', IIF(ISNULL(@ResultConsecutives, '') = '', '', CHAR(13) + CHAR(10)) + @ResultConsecutives_Output)

		--------------------------------------------  PROCESO AMORTIZACION --------------------------------------------

		EXEC [FixedAsset].[SP_SaveAmortizationDetail] 
			@Id,
			@ModeConfirm,
			@OperatingUnitId,
			@DateDepreciateInitial,
			@DateDepreciateEnd,
			@DaysDepreciateMonth,
			@CodeUser,
			---------------------------
			@Code_Output OUT, 
			@Message_Output OUT, 
			@JournalVoucherType_Output OUT, 
			@ResultConsecutives_Output OUT

		IF @Code_Output <> 0
		BEGIN
			SELECT 999 AS CodeMessage, @Message_Output AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
			RETURN
		END

		SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		SET @JournalVoucherType = ISNULL(@JournalVoucherType, '') + IIF(@JournalVoucherType_Output = '', '', IIF(ISNULL(@JournalVoucherType, '') = '', '', CHAR(13) + CHAR(10)) + @JournalVoucherType_Output)
		SET @ResultConsecutives = ISNULL(@ResultConsecutives, '') + IIF(@ResultConsecutives_Output = '', '', IIF(ISNULL(@ResultConsecutives, '') = '', '', CHAR(13) + CHAR(10)) + @ResultConsecutives_Output)

	    ------------------------------------------------  CONFIRMACION ------------------------------------------------			

		IF @ModeConfirm = 1 --Si se está confirmando
		BEGIN			
			--Se actualiza la fecha de proceso de todas la unidades operativas de parámetros de activos fijos agregandole un mes
			UPDATE FixedAsset.SettingFixedAsset SET ProcessDate = EOMONTH(DATEADD(MONTH,1,@ProcessDate))
		END

		--------------------------------------------------- RETORNO ---------------------------------------------------		
		
		SELECT	0 AS CodeMessage, 
				CONCAT('Se generó y se guardó correctamente la depreciación/amortización con código ', @Code) AS Message, 
				@Code AS Code, 
				@Id AS Id, 
				@JournalVoucherType AS JournalVoucherType, 
				@ResultConsecutives AS Consecutives	
	END TRY
	BEGIN CATCH
		DECLARE @error_message VARCHAR(MAX) = ERROR_MESSAGE()
		IF ERROR_NUMBER() = 51000
		BEGIN
			THROW 51000, @error_message, 1
		END
		ELSE
		BEGIN
			SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id, '' AS JournalVoucherType, '' AS Consecutives
		END
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y registra la depreciación o amortización mensual de activos fijos para una unidad operativa. Valida que el período a depreciar coincida con el período de proceso configurado en los parámetros de activos fijos, que existan configuraciones contables y activos físicos activos sin salida, y que la secuencia de numeración esté disponible. Crea o reutiliza el encabezado de depreciación del mes y año indicados, calculando las fechas y días del período con opción de depreciar a 30 días fijos o por días reales del mes. Opera sobre activos físicos con sus libros legales asociados y puede ejecutarse en modo cálculo preliminar o en modo confirmación definitiva.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDepreciation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_SaveDepreciation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y/o confirma el cierre mensual de depreciación y amortización de activos fijos para una unidad operativa, creando la cabecera del proceso y delegando el cálculo a procedimientos de detalle.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todas las unidades operativas en SettingFixedAsset deben tener el mismo periodo (año y mes) en ProcessDate.; Debe existir configuración contable en GeneralLedger.GeneralLedgerSettings para la unidad operativa.; Debe existir configuración de activos fijos en FixedAsset.SettingFixedAsset para la unidad operativa.; El periodo a depreciar (mes/año) debe coincidir con el ProcessDate registrado en los parámetros de activos fijos.; Debe existir al menos un activo físico sin salida (HasOutput=0) con detalle de libro asociado a un LegalBook activo (Status=1).; Para confirmar (ModeConfirm=1) debe existir previamente un registro de depreciación calculado para ese mes/año.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan activos físicos con HasOutput=0 y libros legales con Status=1.; El periodo del proceso siempre debe coincidir con ProcessDate de los parámetros de activos fijos.; La cabecera de depreciación es única por combinación (ClosingMonth, ClosingYear).; El avance de ProcessDate solo ocurre en modo confirmación y después de procesar tanto depreciación como amortización sin errores.; El cálculo de días depende del parámetro Depreciation30Days: 30 días fijos o días reales del mes.; La cabecera nueva siempre se crea con Status=1 (calculado) y pasa a Status=2 al confirmar.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Depreciación de activos fijos; Amortización; Cierre mensual de activos fijos; Parámetros de activos fijos; Libros contables (legales/fiscales/NIIF); Periodo de proceso (ProcessDate); Consecutivos de documentos; Comprobantes contables (JournalVoucher); Unidad operativa; Depreciación a 30 días', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] FixedAsset.FixedAssetDepreciation: Cuando no existe cabecera para el mes/año indicados, se obtiene un consecutivo vía Common.SP_GetSequence y se inserta la cabecera con Status=1 y ClosingDate=Common.GETDATE().; [UPDATE] FixedAsset.FixedAssetDepreciation: Cuando ModeConfirm=1, se actualiza el Status de la cabecera a 2 (confirmado) antes de ejecutar el detalle.; [UPDATE] FixedAsset.SettingFixedAsset: Cuando ModeConfirm=1 y los procesos de detalle terminan sin error, se avanza ProcessDate al fin de mes del mes siguiente al ProcessDate vigente, para todas las unidades operativas.; [RETURN_RESULT] N/A: Ante cualquier validación fallida o error en SP_SaveDepreciationDetail/SP_SaveAmortizationDetail, retorna un resultset con CodeMessage=999 y mensaje descriptivo.; [RETURN_RESULT] N/A: En éxito retorna CodeMessage=0 con el código de la depreciación, tipos de comprobante contable y consecutivos generados por los procesos de detalle.; [RAISERROR] N/A: Si ocurre una excepción con ERROR_NUMBER=51000, se relanza con THROW 51000 manteniendo el mensaje original.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Depreciation30Days = 1 en SettingFixedAsset → DateDepreciateEnd se fija al último día del mes (EOMONTH) y DaysDepreciateMonth=30. else DateDepreciateEnd se fija al primer día del mes siguiente y DaysDepreciateMonth=DATEDIFF entre inicio y fin.; si No existe cabecera FixedAssetDepreciation para (mes, año) → Se solicita consecutivo a Common.SP_GetSequence (TipoDoc 210, IdForm 1122) y se inserta nueva cabecera. else Se reutiliza la cabecera existente (Id, Code).; si ModeConfirm = 1 → Se cambia Status de la cabecera a 2 y, tras ejecutar los detalles, se avanza ProcessDate de todas las unidades operativas un mes. else Solo se ejecuta el cálculo (modo simulación/cálculo) sin cambio de estado ni avance de periodo.; si Code_Output <> 0 tras invocar SP_SaveDepreciationDetail o SP_SaveAmortizationDetail → Se aborta el flujo y se retorna error 999 con el mensaje del procedimiento hijo.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; FixedAsset.SP_SaveDepreciationDetail; FixedAsset.SP_SaveAmortizationDetail; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.SettingFixedAsset; GeneralLedger.GeneralLedgerSettings; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetPhysicalAssetDetailBook; GeneralLedger.LegalBook; FixedAsset.FixedAssetDepreciation', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_SaveDepreciation';
-- GO
