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