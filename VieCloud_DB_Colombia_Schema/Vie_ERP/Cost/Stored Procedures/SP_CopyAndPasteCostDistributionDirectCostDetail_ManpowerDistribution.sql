-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2023-12-13
-- Description:	Procedimiento que se encarga de el Copy & Paste de los detalles de la distribucion de los elementos del costo con tipo de distribución mano de obra
-- =============================================
CREATE   PROCEDURE [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution]
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	DECLARE @GeneralExpenseId INT,
			@OfficialLegalBookId INT

	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		[ProductionCenterId] [int],
		[ProductionCenterCodeName] [varchar](500),
		[MainAccountId] [int],
		[MainAccountNumberName] [varchar](500),
		[CostCenterId] [int],
		[CostCenterCodeName] [varchar](500),
		[MeasurementUnitId] [int],
		[MeasurementUnitCodeName] [varchar](500),
		[AllowEditCostValue] [bit],
		[ThirdPartyId] [int],
		[ThirdPartyNitName] [varchar](500),
		[HandlesThirdParty] [bit],		
		[JobTitle] [varchar](50),
		[PositionId] [int],
		[PositionCodeName] [varchar](500),
		[HoursManpowerText] [varchar](50),
		[HoursManpower] [decimal](18, 2),
		[ManpowerHoursContractedText] [varchar](50),
		[ManpowerHoursContracted] [decimal](18, 2),
		[ProductId] [int],
		[ProductCodeName] [varchar](500),
		[Percentage] [numeric](18, 4),
		[CountText] [varchar](50),
		[Count] [numeric](18, 2),		
		[CostValueText] [varchar](50),
		[CostValue] [decimal](18, 2),
		[BaseValue] [decimal](18, 2),
		[IvaValue] [decimal](18, 2),
		[ValueText] [varchar](50),
		[Value] [numeric](18, 2),
		[Nature] [bit],
		[NatureText] [varchar](100),
		[EmployeeId] [int],
		[EmployeeNitName] [varchar](500),
		[Observation] [varchar](500),
		[RetentionId] [int],
		[BaseRetention] [numeric](18, 2),
		[InvoicedValue] [numeric](18, 2),
		[PayrollConcept] [varchar](500),
		[ProcessDate] [Datetime],
		--------------------------------
		StatusField INT DEFAULT(99), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

	/************************************* ASIGNACIÓN ************************************/
		SELECT 
			@GeneralExpenseId = t.x.value('GeneralExpenseId[1]','int')
		FROM @XmlObject.nodes('/Data') t(x)

		SELECT @OfficialLegalBookId = Id
		FROM GeneralLedger.LegalBook WITH (NOLOCK)
		WHERE OfficialBook = 1

		INSERT INTO @TableXmlObject
				(
					ProductionCenterCodeName, MainAccountNumberName, CostCenterCodeName, ThirdPartyNitName, 
					PositionCodeName, HoursManpowerText, ManpowerHoursContractedText, 
					CountText, ValueText
				)
				SELECT 
					t.x.value('Item0[1]','varchar(500)') as ProductionCenterCodeName,
					t.x.value('Item1[1]','varchar(500)') as MainAccountNumberName,
					t.x.value('Item2[1]','varchar(500)') as CostCenterCodeName,
					t.x.value('Item3[1]','varchar(500)') as ThirdPartyNitName,
					t.x.value('Item4[1]','varchar(500)') as PositionCodeName,
					REPLACE(REPLACE(t.x.value('Item5[1]','varchar(500)'), '.', ''), ',', '.') as HoursManpowerText,
					REPLACE(REPLACE(t.x.value('Item6[1]','varchar(500)'), '.', ''), ',', '.') as ManpowerHoursContractedText,
					1 CountText,
					REPLACE(REPLACE(t.x.value('Item7[1]','varchar(500)'), '.', ''), ',', '.') as ValueText
				FROM @XmlObject.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES ************************************/

		-------------------------- Centro de producción --------------------------  
		UPDATE tXml
			SET tXml.StatusField = IIF(cpc.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(cpc.Id IS NULL, 'El centro de producción con Código ' + tXml.ProductionCenterCodeName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.ProductionCenterId = cpc.Id,
				txml.ProductionCenterCodeName = CONCAT(cpc.Code, ' - ', cpc.Name)
		FROM @TableXmlObject tXml
		LEFT JOIN Cost.CostProductionCenter cpc WITH (NOLOCK) ON tXml.ProductionCenterCodeName = cpc.Code
		WHERE tXml.StatusField = 99

		-------------------------- Cuenta contable --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(ma.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(ma.Id IS NULL, 'La cuenta contable ' + tXml.MainAccountNumberName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.MainAccountId = ma.Id,
				txml.MainAccountNumberName = CONCAT(ma.Number, ' - ', ma.Name),
				tXml.HandlesThirdParty = ma.HandlesThirdParty
		FROM @TableXmlObject tXml
		LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tXml.MainAccountNumberName = ma.Number AND ma.LegalBookId = @OfficialLegalBookId
		WHERE tXml.StatusField = 99

		-------------------------- Centro de costo --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(cac.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(cac.Id IS NULL, 'El centro de costo ' + tXml.CostCenterCodeName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.CostCenterId = cac.Id,
				txml.CostCenterCodeName = CONCAT(cac.Code, ' - ', cac.Name)
		FROM @TableXmlObject tXml
		LEFT JOIN Payroll.CostCenter cac ON tXml.CostCenterCodeName = cac.Code
		WHERE tXml.StatusField = 99

		-------------------------- Tercero --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(tp.Id IS NULL OR tp.State = 0 AND tXml.HandlesThirdParty = 1, 999, tXml.StatusField),
				tXml.MessageField = 
					CASE 
						WHEN tp.Id IS NULL AND tXml.HandlesThirdParty = 1 THEN 'El tercero ' + tXml.ThirdPartyNitName + ' no existe'
						WHEN tp.State = 0 AND tXml.HandlesThirdParty = 1 THEN 'El tercero ' + tXml.ThirdPartyNitName + ' se encuentra inactivo'
						ELSE tXml.MessageField
					END,
				------------------------------
				tXml.ThirdPartyId = tp.Id,
				txml.ThirdPartyNitName = 
					CASE 
						WHEN tp.Id IS NOT NULL AND tp.State = 1 THEN CONCAT(tp.Nit, ' - ', tp.Name)
						ELSE tXml.ThirdPartyNitName
					END
		FROM @TableXmlObject tXml
		LEFT JOIN Common.ThirdParty tp ON tXml.ThirdPartyNitName = tp.Nit
		WHERE tXml.StatusField = 99

		-------------------------- Cargo --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(tp.Id IS NULL OR tp.State = 0, 999, tXml.StatusField),
				tXml.MessageField = IIF(tp.Id IS NULL, 'El cargo ' + tXml.PositionCodeName + ' no existe', IIF(tp.State = 0, 'El cargo ' + tXml.PositionCodeName + ' no esta activo', tXml.MessageField)),
				------------------------------
				tXml.PositionId = tp.Id,
				txml.PositionCodeName = CONCAT(tp.Code, ' - ', tp.Name)
		FROM @TableXmlObject tXml
		LEFT JOIN Payroll.Position tp ON tXml.PositionCodeName = tp.Code
		WHERE tXml.StatusField = 99

		-------------------------- Horas --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.HoursManpowerText AS DECIMAL(18, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.HoursManpowerText AS DECIMAL(18, 2)) IS NULL, 'El formato para las horas laboradas ' + tXml.HoursManpowerText + ' no es válido', tXml.MessageField),
				------------------------------
				tXml.HoursManpower = TRY_CAST(tXml.HoursManpowerText AS DECIMAL(18, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.ManpowerHoursContractedText AS DECIMAL(18, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.ManpowerHoursContractedText AS DECIMAL(18, 2)) IS NULL, 'El formato para las horas contratadas ' + tXml.ManpowerHoursContractedText + ' no es válido', tXml.MessageField),
				------------------------------
				tXml.ManpowerHoursContracted = TRY_CAST(tXml.ManpowerHoursContractedText AS DECIMAL(18, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99

		-------------------------- Total --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.ValueText AS DECIMAL(18, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.ValueText AS DECIMAL(18, 2)) IS NULL, 'El valor ' + tXml.ValueText+ ' no es válido', tXml.MessageField),
				------------------------------
				tXml.[Value] = TRY_CAST(tXml.ValueText AS DECIMAL(18, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99

		-------------------------- Valida asociación de los detalles al elemento del costo -------------------------- 

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = CONCAT('El centro de producción ', tXml.ProductionCenterCodeName, ' con la cuenta contable ', tXml.MainAccountNumberName, ' y centro de costo ', tXml.CostCenterCodeName, ' no se encuentra parametrizado en el elemento del costo seleccionado')
		FROM @TableXmlObject tXml
		LEFT JOIN Cost.CostDistributionBase cdb ON @GeneralExpenseId = cdb.GeneralExpenseId
		LEFT JOIN cost.CostDistributionBaseDetail cdbd 
			ON  cdb.Id = cdbd.DistributionBaseId
				AND tXml.ProductionCenterId = cdbd.ProductionCenterId
				AND tXml.MainAccountId = cdbd.MainAccountId
				AND ISNULL(tXml.CostCenterId, 0) = ISNULL(cdbd.CostCenterId, 0)
		WHERE tXml.StatusField = 99 AND cdbd.Id IS NULL

		UPDATE tXml
			SET tXml.StatusField = 0,
				--tXml.MeasurementUnitId = 41,
				tXml.CostValue = tXml.Value,
				tXml.[Count] = 1
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT DISTINCT
		ProductionCenterId,
		ProductionCenterCodeName,
		MainAccountId,
		MainAccountNumberName,
		CostCenterId,
		CostCenterCodeName,
		MeasurementUnitId,
		MeasurementUnitCodeName,
		ThirdPartyId,
		ThirdPartyNitName,
		HandlesThirdParty,
		JobTitle,
		PositionId,
		PositionCodeName,
		HoursManpower,
		ManpowerHoursContracted,
		ProductId,
		ProductCodeName,
		IIF([Percentage] IS NULL, 0, [Percentage]) Percentage,
		[Count],
		CostValue,
		BaseValue,
		IvaValue,
		Value,
		Nature,
		NatureText,
		EmployeeId,
		EmployeeNitName,
		Observation,
		RetentionId,
		BaseRetention,
		InvoicedValue,
		PayrollConcept,
		ProcessDate,
		--------------------
		StatusField,
		MessageField
	FROM @TableXmlObject
	WHERE StatusField <> 99
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la operación de copiar y pegar (Copy & Paste) los registros detallados de la distribución de costos directos con tipo de distribución por mano de obra, a partir de un objeto XML que contiene las filas a procesar. Valida la existencia y coherencia de las entidades involucradas —centro de producción, cuenta contable del libro oficial, centro de costo, tercero, cargo/posición, entre otros— marcando cada fila con un estado de error o éxito antes de insertarla. Toca las entidades de costos (Cost), contabilidad general (GeneralLedger) y nómina/RRHH (Payroll), siendo utilizado en el módulo de distribución de costos cuando el usuario necesita replicar masivamente detalles de mano de obra (horas trabajadas, horas contratadas, valor, cargo del empleado) desde una fuente copiada hacia un nuevo período o centro de costo destino.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y resuelve, a partir de un XML, los detalles de distribución del costo de tipo mano de obra (centro de producción, cuenta, centro de costo, tercero, cargo, horas y valor) contra los maestros, para retornarlos listos para pegar en el elemento del costo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /Data con GeneralExpenseId y nodos /Data/Row con Item0..Item7 representando código de centro de producción, cuenta contable, centro de costo, NIT del tercero, código del cargo, horas laboradas, horas contratadas y valor; Debe existir un libro contable oficial (OfficialBook=1) en GeneralLedger.LegalBook; Los valores numéricos del XML usan ''.'' como separador de miles y '','' como decimal; Debe existir el elemento del costo (GeneralExpenseId) con su base de distribución y detalles parametrizados en Cost.CostDistributionBase / CostDistributionBaseDetail', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores numéricos del XML se normalizan reemplazando ''.'' (separador de miles) y '','' (decimal) antes de convertirse a decimal; La cuenta contable se valida únicamente contra el libro contable oficial (OfficialBook=1); La validación de tercero solo aplica cuando la cuenta contable maneja terceros (HandlesThirdParty=1); Las filas válidas siempre se devuelven con Count=1 y CostValue igual al Value parseado; Solo se devuelven filas con StatusField distinto de 99 (procesadas u erradas), nunca pendientes; Las validaciones se aplican en cascada: solo se evalúan filas que aún tienen StatusField=99; Cualquier excepción captura el error y devuelve una fila con StatusField=999 y el mensaje + línea del error', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Centro de producción; Cuenta contable; Centro de costo; Tercero; Cargo; Horas laboradas; Horas contratadas; Elemento del costo; Distribución de costos; Mano de obra; Libro oficial contable; Manejo de terceros', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Centro de producción no encontrado por código → Marca fila con StatusField=999 y mensaje ''El centro de producción con Código X no existe''; si Cuenta contable no encontrada en el libro oficial (OfficialBook=1) → Marca fila con StatusField=999 y mensaje ''La cuenta contable X no existe''; si Centro de costo no encontrado por código en Payroll.CostCenter → Marca fila con StatusField=999 y mensaje ''El centro de costo X no existe''; si HandlesThirdParty=1 y tercero no existe → Marca fila con StatusField=999 y mensaje ''El tercero X no existe''; si HandlesThirdParty=1 y tercero existe pero State=0 → Marca fila con StatusField=999 y mensaje ''El tercero X se encuentra inactivo''; si Cargo no existe → Marca StatusField=999 con mensaje ''El cargo X no existe'' else Si Cargo State=0 marca StatusField=999 con mensaje ''El cargo X no esta activo''; si TRY_CAST de horas laboradas a decimal(18,2) es NULL → Marca StatusField=999 con mensaje ''El formato para las horas laboradas X no es válido''; si TRY_CAST de horas contratadas a decimal(18,2) es NULL → Marca StatusField=999 con mensaje ''El formato para las horas contratadas X no es válido''; si TRY_CAST del valor a decimal(18,2) es NULL → Marca StatusField=999 con mensaje ''El valor X no es válido''; si La combinación (centro de producción, cuenta contable, centro de costo) no está parametrizada en CostDistributionBaseDetail para el GeneralExpenseId recibido → Marca StatusField=999 con mensaje indicando que no está parametrizado en el elemento del costo seleccionado; si Fila pasa todas las validaciones (StatusField sigue en 99 al final) → Asigna StatusField=0, CostValue=Value y Count=1', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; Cost.CostProductionCenter; GeneralLedger.MainAccounts; Payroll.CostCenter; Common.ThirdParty; Payroll.Position; Cost.CostDistributionBase; cost.CostDistributionBaseDetail', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_ManpowerDistribution';
-- GO
