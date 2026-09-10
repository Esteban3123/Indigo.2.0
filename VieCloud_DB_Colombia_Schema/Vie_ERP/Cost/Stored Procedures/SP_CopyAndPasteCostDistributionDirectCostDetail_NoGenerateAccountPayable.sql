-- =============================================
-- Author:		Andres Steven Rojas Rodriguez
-- Create date: 2024-07-10
-- Description:	Procedimiento que se encarga de el Copy & Paste de los detalles de la distribucion de los elementos del costo con tipo de distribución mano de obra que NO generan CxP
-- =============================================
CREATE   PROCEDURE [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable]
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
		[Nature] [tinyint],
		[NatureText] [varchar](100),
		[ThirdPartyId] [int],
		[ThirdPartyNitName] [varchar](500),
		[EmployeeId] [int],
		[EmployeeNitName] [varchar](500),
		[PositionId] [int],
		[PositionCodeName] [varchar](500),
		[Observation] [varchar](500),
		[HoursManpowerText] [varchar](50),
		[HoursManpower] [decimal](18, 2),
		[ManpowerHoursContractedText] [varchar](50),
		[ManpowerHoursContracted] [numeric](18, 2),
		[ValueText] [varchar](50),
		[Value] [decimal](18, 2),
		[RetentionText] [varchar](50),
		[RetentionId] [int],
		[InvoicedValueText] [varchar](50),
		[InvoicedValue] [numeric](18, 2),
		[BaseRetentionText] [varchar](50),
		[BaseRetention] [numeric](18, 2),
		[PayrollConcept] [varchar](500),
		[ProcessDate] [Datetime],
		[ProductId] [int],
		[ProductCodeName] [varchar](500),
		[BaseValue] [decimal](18, 2),
		[Percentage] [numeric](18, 4),
		[MeasurementUnitId] [int],
		[MeasurementUnitCodeName] [varchar](500),
		[JobTitle] [varchar](50),
		[Count] [numeric](18, 2),
		[CostValue] [decimal](18, 2),
		[IvaValue] [decimal](18, 2),
		[HandlesThirdParty] [bit],
		[HandlesCostCenter] [bit], 
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
					MainAccountNumberName, Nature, ThirdPartyNitName, CostCenterCodeName, 
					Observation, ValueText, RetentionText, InvoicedValueText, BaseRetentionText,
					EmployeeNitName, PositionCodeName, HoursManpowerText, ManpowerHoursContractedText,
					PayrollConcept, ProcessDate
				)
				SELECT 
					t.x.value('Item0[1]','varchar(500)') as MainAccountNumberName,
					t.x.value('Item1[1]','varchar(500)') as Nature,
					t.x.value('Item2[1]','varchar(500)') as ThirdPartyNitName,
					t.x.value('Item3[1]','varchar(500)') as CostCenterCodeName,
					t.x.value('Item4[1]','varchar(500)') as Observation,
					t.x.value('Item5[1]','varchar(500)') as ValueText,
					t.x.value('Item6[1]','varchar(500)') as RetentionText,
					t.x.value('Item7[1]','varchar(500)') as InvoicedValueText,
					t.x.value('Item8[1]','varchar(500)') as BaseRetentionText,
					t.x.value('Item9[1]','varchar(500)') as EmployeeNitName,
					t.x.value('Item10[1]','varchar(500)') as PositionCodeName,
					t.x.value('Item11[1]','varchar(500)') as HoursManpowerText,
					t.x.value('Item12[1]','varchar(500)') as ManpowerHoursContractedText,
					t.x.value('Item13[1]','varchar(500)') as PayrollConcept,
					TRY_CONVERT(datetime, t.x.value('Item14[1]', 'varchar(10)'), 103) as ProcessDate
				FROM @XmlObject.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES ************************************/

		-------------------------- Cuenta contable --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(ma.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(ma.Id IS NULL, 'La cuenta contable ' + tXml.MainAccountNumberName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.MainAccountId = ma.Id,
				txml.MainAccountNumberName = CONCAT(ma.Number, ' - ', ma.Name),
				tXml.HandlesCostCenter = ma.HandlesCostCenter 
		FROM @TableXmlObject tXml
		LEFT JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON tXml.MainAccountNumberName = ma.Number AND ma.LegalBookId = @OfficialLegalBookId
		WHERE tXml.StatusField = 99

		-------------------------- Validar Centro de Costo obligatorio --------------------------  

		UPDATE tXml
			SET tXml.StatusField = 999,
				tXml.MessageField = 'La cuenta contable ' + tXml.MainAccountNumberName + ' requiere Centro de Costo y no se ha proporcionado'
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99
			AND tXml.HandlesCostCenter = 1
			AND (tXml.CostCenterCodeName IS NULL OR tXml.CostCenterCodeName = '')

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
		 AND tXml.HandlesCostCenter = 1 
		 AND (tXml.CostCenterCodeName IS NOT NULL AND tXml.CostCenterCodeName <> '') 

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

		-------------------------- Empleado --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(tp.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(tp.Id IS NULL, 'El empleado ' + tXml.EmployeeNitName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.EmployeeId = tp.Id,
				txml.EmployeeNitName = IIF(tp.Id IS NOT NULL, CONCAT(tp.Nit, ' - ', tp.Name), '') 
		FROM @TableXmlObject tXml
		LEFT JOIN Common.ThirdParty tp ON tXml.EmployeeNitName = tp.Nit
		WHERE tXml.StatusField = 99

		-------------------------- Naturaleza --------------------------  

		UPDATE tXml
		SET 
			tXml.StatusField = IIF(tXml.Nature <> 1 AND tXml.Nature <> 2, 999, tXml.StatusField),
			tXml.MessageField = IIF(tXml.Nature <> 1 AND tXml.Nature <> 2, 'La naturaleza ' + CAST(tXml.Nature AS varchar(1)) + ' no es valida', tXml.MessageField),
			tXml.NatureText = CASE
								WHEN tXml.Nature = 1 THEN 'DEBITO'
								WHEN tXml.Nature = 2 THEN 'CREDITO'
								ELSE NULL
							 END
		FROM @TableXmlObject tXml
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

		-------------------------- Valor --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.ValueText AS DECIMAL(18, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.ValueText AS DECIMAL(18, 2)) IS NULL, 'El valor ' + tXml.ValueText+ ' no es válido', tXml.MessageField),
				------------------------------
				tXml.[Value] = TRY_CAST(tXml.ValueText AS DECIMAL(18, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99

		-------------------------- Retención --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(rc.Id IS NULL, 999, tXml.StatusField),
			tXml.MessageField = IIF(rc.Id IS NULL, 'La retención ' + tXml.RetentionText+ ' no es válida', tXml.MessageField),
			------------------------------
			tXml.RetentionId = rc.Id
		FROM @TableXmlObject tXml
		LEFT JOIN GeneralLedger.RetentionConcepts rc ON tXml.RetentionText = rc.Code AND rc.Status = 1
		WHERE tXml.StatusField = 99 AND tXml.RetentionText <> '' AND tXml.RetentionText <> '0'
			   		 
		-------------------------- Valor Facturado --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.InvoicedValueText AS DECIMAL(10, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.InvoicedValueText AS DECIMAL(10, 2)) IS NULL, 'El valor facturado ' + tXml.InvoicedValueText+ ' no es válido', tXml.MessageField),
				------------------------------
				tXml.InvoicedValue = TRY_CAST(tXml.InvoicedValueText AS DECIMAL(10, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99 AND tXml.InvoicedValueText <> ''

		-------------------------- Base Retención --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.BaseRetentionText AS DECIMAL(11, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.BaseRetentionText AS DECIMAL(11, 2)) IS NULL, 'La base de la retención ' + tXml.BaseRetentionText+ ' no es válida', tXml.MessageField),
				------------------------------
				tXml.BaseRetention = TRY_CAST(tXml.BaseRetentionText AS DECIMAL(11, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99 AND tXml.BaseRetentionText <> ''

		-------------------------- Horas --------------------------  

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.HoursManpowerText AS DECIMAL(18, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.HoursManpowerText AS DECIMAL(18, 2)) IS NULL, 'El formato para las horas laboradas ' + tXml.HoursManpowerText + ' no es válido', tXml.MessageField),
				------------------------------
				tXml.HoursManpower = TRY_CAST(tXml.HoursManpowerText AS DECIMAL(18, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99 AND tXml.HoursManpowerText <> ''

		UPDATE tXml
			SET tXml.StatusField = IIF(TRY_CAST(tXml.ManpowerHoursContractedText AS DECIMAL(18, 2)) IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(TRY_CAST(tXml.ManpowerHoursContractedText AS DECIMAL(18, 2)) IS NULL, 'El formato para las horas contratadas ' + tXml.ManpowerHoursContractedText + ' no es válido', tXml.MessageField),
				------------------------------
				tXml.ManpowerHoursContracted = TRY_CAST(tXml.ManpowerHoursContractedText AS DECIMAL(18, 2))
		FROM @TableXmlObject tXml
		WHERE tXml.StatusField = 99 AND tXml.ManpowerHoursContractedText <> ''

				
		----------------------- Cambiar StatusField a 0 --------------------

		UPDATE tXml
			SET tXml.StatusField = 0
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
			Nature,
			NatureText,
			CostCenterId,
			CostCenterCodeName,
			ThirdPartyId,
			ThirdPartyNitName,
			EmployeeId,
			EmployeeNitName,
			PositionId,
			PositionCodeName,
			HoursManpower,
			ManpowerHoursContracted,
			Value,
			Observation,
			RetentionId,
			BaseRetention,
			InvoicedValue,
			PayrollConcept,
			ProcessDate,
			ProductId,
			ProductCodeName,
			BaseValue,
			[Percentage],
			MeasurementUnitId,
			MeasurementUnitCodeName,
			JobTitle,
			[Count],
			CostValue,
			IvaValue,
			HandlesThirdParty, 
			--------------------
			StatusField,
			MessageField
		FROM @TableXmlObject
		WHERE StatusField <> 99

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza el copiado y pegado (Copy & Paste) de líneas de detalle en la distribución de costos directos con tipo de distribución de mano de obra, exclusivamente para los casos que NO generan cuentas por pagar (CxP). Recibe un XML con los ítems a copiar, valida cada campo contra el libro oficial de contabilidad (LegalBook), las cuentas contables (MainAccounts), los centros de costo y los terceros, marcando cada fila con un estado de éxito o error y un mensaje descriptivo. Toca las entidades de costos directos, contabilidad general y nómina, siendo utilizado en el módulo de distribución de costos para replicar configuraciones de mano de obra ya existentes sin disparar el proceso de generación de obligaciones financieras (cuentas por pagar).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y normaliza filas XML de detalles de distribución de costos directos (mano de obra) que no generan cuentas por pagar, retornando estado y mensajes por fila.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /Data/Row con los nodos Item0..Item14; Debe existir un LegalBook marcado como OfficialBook = 1 en GeneralLedger.LegalBook; Cada fila empieza con StatusField = 99 (pendiente de validar) para participar en las validaciones', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se aplican validaciones a filas que mantengan StatusField=99; las marcadas en 999 quedan congeladas con su primer mensaje de error; Las cuentas contables se buscan exclusivamente en el libro oficial (OfficialBook=1); Los conceptos de retención considerados deben tener Status=1; El procedimiento es de solo lectura: no inserta ni actualiza tablas físicas, solo trabaja sobre una tabla en memoria; Las fechas se interpretan en formato 103 (dd/mm/yyyy) vía TRY_CONVERT; Una fila con HandlesThirdParty=0 omite las validaciones de existencia/estado del tercero', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos directos; Mano de obra; Cuenta contable (PUC); Centro de costo; Tercero; Empleado; Cargo/Posición; Naturaleza contable (débito/crédito); Retención tributaria; Base de retención; Valor facturado; Horas laboradas y horas contratadas; Concepto de nómina; Libro oficial contable; Cuentas por pagar (CxP)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN_RESULT: Devuelve el conjunto de filas validadas (StatusField <> 99): 0 si pasaron todas las validaciones, 999 con MessageField cuando alguna falló.; [RAISERROR] RETURN_RESULT: Si ocurre una excepción, se inserta una fila con StatusField=999 y MessageField=ERROR_MESSAGE() + '' Linea: '' + ERROR_LINE() y se retorna en el resultado.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cuenta contable no existe en MainAccounts del libro oficial → StatusField=999 y mensaje ''La cuenta contable X no existe'' else Asigna MainAccountId, normaliza nombre y toma HandlesCostCenter de la cuenta; si La cuenta contable tiene HandlesCostCenter=1 y no se proporcionó CostCenterCodeName → StatusField=999 con mensaje ''requiere Centro de Costo y no se ha proporcionado''; si Centro de costo no existe en Payroll.CostCenter (cuando la cuenta lo exige) → StatusField=999 con mensaje ''El centro de costo X no existe'' else Asigna CostCenterId y normaliza Code-Name; si HandlesThirdParty=1 y tercero no existe → StatusField=999 con ''El tercero X no existe'' else Si tercero existe pero State=0 y HandlesThirdParty=1 → 999 con ''se encuentra inactivo''; si activo → asigna ThirdPartyId y normaliza Nit-Name; si Empleado (ThirdParty por Nit) no existe → StatusField=999 con ''El empleado X no existe'' else Asigna EmployeeId y normaliza Nit-Name; si Nature distinto de 1 o 2 → StatusField=999 con ''La naturaleza X no es valida'' else NatureText=''DEBITO'' si Nature=1, ''CREDITO'' si Nature=2; si Cargo no existe o tiene State=0 en Payroll.Position → StatusField=999 con ''no existe'' o ''no esta activo'' else Asigna PositionId y normaliza Code-Name; si ValueText no convertible a DECIMAL(18,2) → StatusField=999 con ''El valor X no es válido'' else Asigna Value convertido; si RetentionText no vacío y distinto de ''0'' y no existe en RetentionConcepts con Status=1 → StatusField=999 con ''La retención X no es válida'' else Asigna RetentionId; si InvoicedValueText no vacío y no convertible a DECIMAL(10,2) → StatusField=999 con ''El valor facturado X no es válido'' else Asigna InvoicedValue; si BaseRetentionText no vacío y no convertible a DECIMAL(11,2) → StatusField=999 con ''La base de la retención X no es válida'' else Asigna BaseRetention; si HoursManpowerText/ManpowerHoursContractedText no vacíos y no convertibles a DECIMAL(18,2) → StatusField=999 con mensaje de formato inválido else Asigna las horas convertidas; si Al finalizar todas las validaciones, fila aún con StatusField=99 → StatusField=0 (válida)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.MainAccounts; Payroll.CostCenter; Common.ThirdParty; Payroll.Position; GeneralLedger.RetentionConcepts', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_NoGenerateAccountPayable';
-- GO
