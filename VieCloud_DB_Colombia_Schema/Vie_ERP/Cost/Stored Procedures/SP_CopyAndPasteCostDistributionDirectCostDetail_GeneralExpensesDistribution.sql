-- =============================================
-- Author:		Andrea Pahola Coqueco Cuellar
-- Create date: 2023-12-13
-- Description:	Procedimiento que se encarga de el Copy & Paste de los detalles de la distribucion de los elementos del costo con tipo de distribución Gastos generales
-- =============================================
CREATE   PROCEDURE [Cost].[SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution]
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
		[Percentage] [numeric](18, 4) ,
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
			(ProductionCenterCodeName, MainAccountNumberName, CostCenterCodeName, ThirdPartyNitName, CountText, ValueText)
			SELECT 
				t.x.value('Item0[1]','varchar(500)') as ProductionCenterCodeName,
				t.x.value('Item1[1]','varchar(500)') as MainAccountNumberName,
				t.x.value('Item2[1]','varchar(500)') as CostCenterCodeName,
				t.x.value('Item3[1]','varchar(500)') as ThirdPartyNitName,
				1 CountText,
				REPLACE(REPLACE(t.x.value('Item4[1]','varchar(500)'), '.', ''), ',', '.') as ValueText
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
			SET tXml.StatusField = IIF(tp.Id IS NULL AND tXml.HandlesThirdParty = 1, 999, tXml.StatusField),
				tXml.MessageField = IIF(tp.Id IS NULL AND tXml.HandlesThirdParty = 1, 'El tercero ' + tXml.ThirdPartyNitName + ' no existe', tXml.MessageField),
				------------------------------
				tXml.ThirdPartyId = tp.Id,
				txml.ThirdPartyNitName = CONCAT(tp.Nit, ' - ', tp.Name)
		FROM @TableXmlObject tXml
		LEFT JOIN Common.ThirdParty tp ON tXml.ThirdPartyNitName = tp.Nit
		WHERE tXml.StatusField = 99
		
		-------------------------- Valor --------------------------  
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la operación de Copiar y Pegar (Copy & Paste) para los ítems del detalle de distribución de costos con tipo ''Gastos Generales'', a partir de un objeto XML que contiene las filas a procesar. Valida y resuelve cada columna del detalle: el centro de producción (contra Cost.CostProductionCenter), la cuenta contable del libro oficial (contra GeneralLedger.MainAccounts y LegalBook), el centro de costo (cuando la cuenta lo requiere), el tercero por NIT, y el valor monetario; marcando cada fila con un estado de éxito o error y un mensaje descriptivo. Una vez superadas las validaciones, inserta los registros aprobados como nuevos ítems en la distribución de gastos generales del período de costos indicado, facilitando la carga masiva de distribuciones desde interfaces de usuario tipo grilla o Excel.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y normaliza filas pegadas (XML) para la distribución de gastos generales en costos, marcando cada renglón como válido o inválido con su mensaje, antes de su carga masiva.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener un nodo /Data con GeneralExpenseId y nodos /Data/Row con Item0..Item4 (centro producción, cuenta contable, centro costo, tercero, valor); Debe existir un LegalBook marcado como OfficialBook = 1; El elemento de costo (GeneralExpenseId) debe tener parametrizada una base de distribución (CostDistributionBase) con sus detalles', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se aplican las validaciones siguientes a filas que aún tengan StatusField=99 (corte temprano por error previo); El valor numérico se interpreta con formato europeo: punto como separador de miles y coma como separador decimal; La cuenta contable se busca exclusivamente en el libro contable oficial (OfficialBook=1); La validación de tercero solo aplica cuando la cuenta contable maneja terceros; El procedimiento nunca lanza excepción al consumidor: cualquier error se captura y se devuelve como fila con StatusField=999; El resultado nunca incluye filas con StatusField=99 (estado intermedio)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Distribución de costos; Gastos generales; Centro de producción; Cuenta contable; Centro de costo; Tercero (NIT); Libro contable oficial; Elemento del costo; Copy & Paste de distribución; Manejo de terceros en cuenta contable', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Por cada /Data/Row del XML se inserta una fila con los textos crudos (centro producción, cuenta, centro costo, tercero, valor) y CountText=1; el valor se normaliza eliminando ''.'' y reemplazando '','' por ''.''; [UPDATE] @TableXmlObject: Si el código de centro de producción no existe en Cost.CostProductionCenter → StatusField=999 y mensaje ''El centro de producción con Código X no existe''; si existe se asigna Id y se reformatea como ''Code - Name''; [UPDATE] @TableXmlObject: Si la cuenta contable no existe en GeneralLedger.MainAccounts para el LegalBook oficial → StatusField=999 y mensaje ''La cuenta contable X no existe''; si existe se asigna Id, se reformatea ''Number - Name'' y se copia HandlesThirdParty; [UPDATE] @TableXmlObject: Si el centro de costo no existe en Payroll.CostCenter → StatusField=999 y mensaje ''El centro de costo X no existe''; si existe se asigna Id y ''Code - Name''; [UPDATE] @TableXmlObject: Si la cuenta contable maneja terceros (HandlesThirdParty=1) y el NIT no existe en Common.ThirdParty → StatusField=999 y mensaje ''El tercero X no existe''; [UPDATE] @TableXmlObject: Si TRY_CAST(ValueText AS DECIMAL(18,2)) es NULL → StatusField=999 y mensaje ''El valor X no es válido''; en caso contrario se asigna a Value; [UPDATE] @TableXmlObject: Si la combinación (centro producción, cuenta contable, centro costo) no está parametrizada en CostDistributionBaseDetail del GeneralExpenseId → StatusField=999 y mensaje indicando que no está parametrizado en el elemento del costo; [UPDATE] @TableXmlObject: Si la fila supera todas las validaciones (StatusField=99) → se marca StatusField=0, CostValue=Value y Count=1; [INSERT] @TableXmlObject: En CATCH se inserta una fila con StatusField=999 y MessageField = ERROR_MESSAGE() + '' Linea: '' + ERROR_LINE(); [RETURN_RESULT] RESULT: Devuelve DISTINCT de las filas con StatusField <> 99 (filas validadas u erróneas), incluyendo Percentage convertido a 0 si es NULL', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cuenta contable con HandlesThirdParty = 1 y tercero no encontrado → Marca la fila como inválida con mensaje de tercero inexistente else Omite la validación de tercero; si StatusField sigue en 99 tras todas las validaciones → Marca fila como exitosa (StatusField=0) y asigna CostValue=Value, Count=1 else Mantiene la fila con StatusField=999 y su mensaje de error; si Combinación centro producción + cuenta + centro costo no existe en CostDistributionBaseDetail del GeneralExpenseId → Marca la fila como no parametrizada en el elemento de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; Cost.CostProductionCenter; GeneralLedger.MainAccounts; Payroll.CostCenter; Common.ThirdParty; Cost.CostDistributionBase; Cost.CostDistributionBaseDetail', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteCostDistributionDirectCostDetail_GeneralExpensesDistribution';
-- GO
