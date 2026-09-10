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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para anular dispensaciones farmacéuticas desde el tablero (dashboard) de farmacia. Recibe un XML con los detalles de los productos dispensados a anular (número de ingreso, código del paciente, centro de atención, unidad funcional, médico prescriptor y producto), registra el movimiento de anulación en el kárdex de medicamentos por paciente (HCKARDPAC) con cantidad cero y tipo de movimiento 4 (anulación), y actualiza el estado de los ítems de la solicitud de farmacia (HCFARMEPD) a anulado (estado 3), así como el estado global de la orden de farmacia (HCFARMEPC) cuando todos sus productos han sido anulados. Existe para revertir dispensaciones registradas erróneamente o canceladas por el equipo asistencial o de farmacia, dejando trazabilidad completa del movimiento en el kárdex.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Anula solicitudes de dispensación farmacéutica desde el dashboard, registrando el movimiento en el kárdex y marcando como anuladas las órdenes y sus detalles.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodos /Main/ViewDashboardPharmacyDetail o /Main/ViewDashBoardPharmacy_SurgicalPackageDeatils con los datos de las dispensaciones a anular.; Los campos Medico y Producto del XML deben contener un espacio para que el SUBSTRING/CHARINDEX recorten correctamente la primera palabra.; Deben existir registros en HCFARMEPD/HCFARMEPC con CODCONCEC coincidente con el Consecutivo recibido para que se efectúen las actualizaciones.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se generan movimientos de kárdex para filas con Producto no nulo.; Los movimientos de anulación siempre se registran con tipo de historia 9 y TIPREGIST=''4''.; La cantidad pendiente (CANPENPRO) de un detalle anulado siempre queda en 0 con estado ''3''.; Un encabezado solo se anula (ORDESTADO=''3'') si tiene detalles asociados con cantidad pedida >0 que no estén ya anulados.; Ante cualquier error, las variables de salida quedan con código ''999'' y estado 3, sin abortar la transacción explícitamente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Anulación de dispensación farmacéutica; Kárdex de paciente; Despacho de farmacia; Orden/pedido de medicamentos; Paquete quirúrgico; Ingreso/admisión del paciente; Centro de atención; Unidad funcional; Prescripción médica', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.HCKARDPAC: Por cada fila del XML con Producto IS NOT NULL se inserta un movimiento de kárdex con TIPREGIST=''4'', TIPORIREG=9 (Despacho Farmacia) y descripción ''Despacho Farmacia, Origen Solicitud: Anulacion Farmacia - Usuario:''+@user.; [UPDATE] dbo.HCFARMEPD: Para los detalles cuyo CODCONCEC y CODPRODUC coincidan con el Consecutivo y Producto del XML se establece CANPENPRO=0 y PROESTADO=''3'' (anulado).; [UPDATE] dbo.HCFARMEPC: Para encabezados cuyo CODCONCEC coincida con el Consecutivo del XML y tengan al menos un detalle con CANPEDPRO>0 y PROESTADO<>''3'', se actualiza ORDESTADO=''3'' (anulado).; [RETURN_RESULT] : En éxito retorna CodeMessage=''0'', mensaje ''Se anuló la Solicitud de Farmacia'' y Status=1; en error retorna CodeMessage=''999'', mensaje con ERROR_MESSAGE/ERROR_LINE y Status=3.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen filas pendientes en la tabla temporal con Producto IS NOT NULL → Itera fila por fila generando un consecutivo NEWID() e inserta un registro en HCKARDPAC else Sale del bucle WHILE (BREAK) y continúa con las actualizaciones de HCFARMEPD y HCFARMEPC; si Ocurre una excepción durante la ejecución (CATCH) → Devuelve código ''999'', concatena ERROR_MESSAGE y ERROR_LINE en el mensaje y fija Status=3 else Devuelve código ''0'', mensaje de éxito y Status=1', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.HCFARMEPC', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AnnulateDashboardPharmacy_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AnnulateDashboardPharmacy_Output';
-- GO
