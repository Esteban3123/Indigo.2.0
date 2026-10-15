-- Stored Procedure

-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-22
-- Description:	Procedimiento para anular dispensaciones farmaceuticas
-- =============================================
CREATE PROCEDURE [Inventory].[SP_AnnulateDashboardPharmacy_Output]
	@XmlAnnulateDashboard XML,
	@user VARCHAR(20),
	------------------------------------------------------
	@CodeMessageResult VARCHAR(20) OUTPUT, 
	@ErrorsValidationResult VARCHAR(MAX) OUTPUT, 
	@DispensingIdResult INT OUTPUT, 
	@DispensingCodeResult VARCHAR(20) OUTPUT, 
	@StatusResult TINYINT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE	@Rows INT,
			@RowId INT,
			-----------------------------------------------
			@HistoryTypeCrystal INT,
			@HistoryTypeNameCrystal VARCHAR(MAX),
			@ConsecutiveKardexDispesing VARCHAR(50)

	DECLARE @TableAnnulate TABLE
	(
		RowId INT IDENTITY(1,1) PRIMARY KEY,
		AdmissionNumber VARCHAR(20), 
		PatientCode VARCHAR(50), 
		CareCenterCode VARCHAR(50), 
		FunctionUnitCode VARCHAR(50), 
		ConsecutivePescription VARCHAR(50), 
		ConsecutiveInputs VARCHAR(50), 
		ConsecutivePharmacy VARCHAR(50), 
		Consecutivo VARCHAR(50), 
		Medico VARCHAR(100), 
		Producto VARCHAR(100)
	)

	BEGIN TRY

		INSERT INTO @TableAnnulate
			SELECT	t.x.value('Ingreso[1]','VARCHAR(20)'),
					t.x.value('CodigoPaciente[1]','VARCHAR(50)'),
					t.x.value('CareCenterCode[1]','VARCHAR(50)'),
					t.x.value('FunctionUnitCode[1]','VARCHAR(50)'),
					t.x.value('ConsecutivePescription[1]','VARCHAR(50)'),
					t.x.value('ConsecutiveInputs[1]','VARCHAR(50)'),
					t.x.value('ConsecutivePharmacy[1]','VARCHAR(50)'),
					t.x.value('Consecutivo[1]','VARCHAR(50)'),
					t.x.value('Medico[1]','VARCHAR(100)'),
					t.x.value('Producto[1]','VARCHAR(100)')
			FROM @XmlAnnulateDashboard.nodes('/Main/ViewDashboardPharmacyDetail') t(x)

		INSERT INTO @TableAnnulate
			SELECT	t.x.value('Ingreso[1]','VARCHAR(20)'),
					t.x.value('CodigoPaciente[1]','VARCHAR(50)'),
					t.x.value('CareCenterCode[1]','VARCHAR(50)'),
					t.x.value('FunctionUnitCode[1]','VARCHAR(50)'),
					t.x.value('ConsecutivePescription[1]','VARCHAR(50)'),
					t.x.value('ConsecutiveInputs[1]','VARCHAR(50)'),
					t.x.value('ConsecutivePharmacy[1]','VARCHAR(50)'),
					t.x.value('Consecutivo[1]','VARCHAR(50)'),
					t.x.value('Medico[1]','VARCHAR(100)'),
					t.x.value('Producto[1]','VARCHAR(100)')
			FROM @XmlAnnulateDashboard.nodes('/Main/ViewDashBoardPharmacy_SurgicalPackageDeatils') t(x)

		---------------------------------------------------------------------------------------------------------------

		SET @HistoryTypeCrystal = 9
		SET @HistoryTypeNameCrystal = 'Despacho Farmacia, Origen Solicitud: Anulacion Farmacia - Usuario:' + @user

		UPDATE t
			SET Medico = RTRIM(SUBSTRING(Medico,0,CHARINDEX(' ',Medico))),
				Producto = RTRIM(SUBSTRING(Producto,0,CHARINDEX(' ',Producto)))
		FROM @TableAnnulate t

		---------------------------------------------------------------------------------------------------------------

		SET @Rows = 1
		SET @RowId = 0

		WHILE @Rows > 0
		BEGIN
			SELECT TOP 1 
				@RowId = RowId
			FROM @TableAnnulate 
			WHERE RowId > @RowId 
				AND Producto IS NOT NULL 
			ORDER BY RowId

			SET @Rows = @@ROWCOUNT
			IF @Rows = 0 
				BREAK

			SET @ConsecutiveKardexDispesing = NEWID()

			--- Inserto en el Kardex de crystal
			INSERT INTO [dbo].[HCKARDPAC]
			(
				[NUMCONSEC],[IPCODPACI],[NUMINGRES],[CODCENATE],[UFUCODIGO],[CODPROSAL]
				,[CODPRODUC],[CANPRODUCT],[TIPREGIST],[HCPRESCRN],[HCSOLINSN],[HCCTRAPLN]
				,[HCCTRAPLM],[CODDOCUME],[FECREGKAR],[TIPORIREG],[DESMOVPRO],[JUSANULAC]
				,[CONSECFAR],[FECHAUTIL],[OBSERVACI]
			)
			SELECT	@ConsecutiveKardexDispesing - 1,PatientCode,AdmissionNumber,CareCenterCode,FunctionUnitCode,Medico
					,Producto,0,'4',ConsecutivePescription,ConsecutiveInputs,NULL
					,NULL,NULL,[Common].[GETDATE](),@HistoryTypeCrystal,@HistoryTypeNameCrystal,NULL
					,ConsecutivePharmacy,NULL,NULL
			FROM @TableAnnulate
			WHERE RowId = @RowId
		END

		UPDATE fd
			SET fd.CANPENPRO = 0,
				fd.PROESTADO = '3'
		FROM @TableAnnulate t
		JOIN dbo.HCFARMEPD fd WITH (NOLOCK) ON t.Consecutivo = fd.CODCONCEC AND t.Producto = fd.CODPRODUC

		UPDATE f
			SET f.ORDESTADO = '3'
		FROM @TableAnnulate t
		JOIN dbo.HCFARMEPC f WITH (NOLOCK) ON t.Consecutivo = f.CODCONCEC
		LEFT JOIN dbo.HCFARMEPD fd WITH (NOLOCK) ON f.CODCONCEC = fd.CODCONCEC AND fd.CANPEDPRO > 0 AND fd.PROESTADO <> '3'
		WHERE fd.CODCONCEC IS NOT NULL

		---------------------------------------------------------------------------------------------------------------

		SELECT	@CodeMessageResult = '0',
				@ErrorsValidationResult = 'Se anuló la Solicitud de Farmacia',
				@DispensingIdResult = 0,
				@DispensingCodeResult = '',
				@StatusResult = 1
	END TRY
	BEGIN CATCH
		SELECT	@CodeMessageResult = '999',
				@ErrorsValidationResult = CONCAT('SP_AnnulateDashboardPharmacy: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()),
				@DispensingIdResult = 0,
				@DispensingCodeResult = '',
				@StatusResult = 3
	END CATCH
END