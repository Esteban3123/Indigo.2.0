-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-10-22
-- Description:	Procedimiento que se encarga de el Copy & Paste de los centros de costo por sucursal y unidad funcional
-- =============================================
CREATE PROCEDURE [Billing].[SP_CopyAndPasteBillingConceptCostCenter] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON

	/************************************* VARIABLES ************************************/
	
	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		BranchOfficeId INT,
		BranchOfficeCode VARCHAR(500),
		BranchOfficeName VARCHAR(500),
		FunctionalUnitId INT,
		FunctionalUnitCode VARCHAR(500),
		FunctionalUnitName VARCHAR(500),
		CostCenterId INT,
		CostCenterCode VARCHAR(500),
		CostCenterName VARCHAR(500),
		--------------------------------
		StatusField INT DEFAULT(0), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY

		INSERT INTO @TableXmlObject
			(BranchOfficeCode, FunctionalUnitCode, CostCenterCode)
			SELECT	t.x.value('BranchOfficeCode[1]','varchar(500)') as BranchOfficeCode,
					t.x.value('FunctionalUnitCode[1]','varchar(500)') as FunctionalUnitCode,
					t.x.value('CostCenterCode[1]','varchar(500)') as CostCenterCode
			FROM @XmlObject.nodes('/Data') t(x)

		UPDATE tXml
			SET tXml.StatusField = IIF(bo.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(bo.Id IS NULL, 'La Sucursal con Código ' + tXml.BranchOfficeCode + ' no existe', tXml.MessageField),
				-------------------------------------------------------------------------------------------------------
				tXml.BranchOfficeId = bo.Id,
				tXml.BranchOfficeCode = bo.Code,
				tXml.BranchOfficeName = bo.Name
		FROM @TableXmlObject tXml
		LEFT JOIN Payroll.BranchOffice bo ON tXml.BranchOfficeCode = bo.Code
		WHERE tXml.StatusField = 0

		UPDATE tXml
			SET tXml.StatusField = IIF(fu.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(fu.Id IS NULL, 'La Unidad Funcional con Código ' + tXml.FunctionalUnitCode + ' no existe', tXml.MessageField),
				-------------------------------------------------------------------------------------------------------
				tXml.FunctionalUnitId = fu.Id,
				tXml.FunctionalUnitCode = fu.Code,
				tXml.FunctionalUnitName = fu.Name
		FROM @TableXmlObject tXml
		LEFT JOIN Payroll.FunctionalUnit fu ON tXml.FunctionalUnitCode = fu.Code
		WHERE tXml.StatusField = 0

		UPDATE tXml
			SET tXml.StatusField = IIF(cc.Id IS NULL, 999, tXml.StatusField),
				tXml.MessageField = IIF(cc.Id IS NULL, 'El Centro de Costo con Código ' + tXml.CostCenterCode + ' no existe', tXml.MessageField),
				-------------------------------------------------------------------------------------------------------
				tXml.CostCenterId = cc.Id,
				tXml.CostCenterCode = cc.Code,
				tXml.CostCenterName = cc.Name
		FROM @TableXmlObject tXml
		LEFT JOIN Payroll.CostCenter cc ON tXml.CostCenterCode = cc.Code
		WHERE tXml.StatusField = 0

	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
		VALUES (999, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)))
	END CATCH

	SELECT *
	FROM @TableXmlObject
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de facturación que copia y pega la configuración de conceptos de facturación asociados a centros de costo, dado un conjunto de combinaciones de sucursal, unidad funcional y centro de costo enviadas en formato XML. Valida que cada código de sucursal exista en el catálogo de sedes (Payroll.BranchOffice), que la unidad funcional exista en Payroll.FunctionalUnit y que el centro de costo exista en Payroll.CostCenter, marcando con error (código 999) y un mensaje descriptivo cualquier registro que no se encuentre. Retorna el resultado de cada ítem procesado indicando si fue validado correctamente o el motivo del fallo, lo que permite a la interfaz de usuario informar al operador qué combinaciones son válidas antes de aplicar la réplica masiva de conceptos de facturación entre centros de costo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y resuelve, a partir de un XML, los códigos de sucursal, unidad funcional y centro de costo contra los maestros de nómina, devolviendo cada registro con sus IDs resueltos o un estado/mensaje de error para soportar la operación de copiar/pegar centros de costo en conceptos de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener nodos /Data con elementos BranchOfficeCode, FunctionalUnitCode y CostCenterCode.; Las tablas maestras Payroll.BranchOffice, Payroll.FunctionalUnit y Payroll.CostCenter deben contener los códigos referenciados para que el registro se considere válido.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las validaciones de Sucursal, Unidad Funcional y Centro de Costo se ejecutan en cadena: solo se valida la siguiente entidad si StatusField sigue en 0 (la primera falla detiene validaciones posteriores en ese registro).; El código de error de negocio para registros inválidos es siempre 999.; Únicamente se resuelven IDs (BranchOfficeId, FunctionalUnitId, CostCenterId) cuando los códigos existen en sus tablas maestras de Payroll.; El procedimiento no persiste cambios en tablas físicas: solo opera sobre una tabla variable y devuelve el resultado al consumidor.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sucursal; Unidad Funcional; Centro de Costo; Concepto de Facturación; Copy & Paste de configuración', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableXmlObject: Carga inicial: por cada nodo /Data del XML inserta una fila con BranchOfficeCode, FunctionalUnitCode y CostCenterCode.; [INSERT] @TableXmlObject: En el bloque CATCH, inserta una fila con StatusField=999 y MessageField = ERROR_MESSAGE() + '' Linea: '' + ERROR_LINE() cuando ocurre una excepción.; [UPDATE] @TableXmlObject: Para filas con StatusField=0, si Payroll.BranchOffice.Id IS NULL al hacer LEFT JOIN por Code, fija StatusField=999 y MessageField=''La Sucursal con Código <code> no existe''; en caso contrario, completa BranchOfficeId/Code/Name.; [UPDATE] @TableXmlObject: Para filas con StatusField=0, si Payroll.FunctionalUnit.Id IS NULL al hacer LEFT JOIN por Code, fija StatusField=999 y MessageField=''La Unidad Funcional con Código <code> no existe''; en caso contrario, completa FunctionalUnitId/Code/Name.; [UPDATE] @TableXmlObject: Para filas con StatusField=0, si Payroll.CostCenter.Id IS NULL al hacer LEFT JOIN por Code, fija StatusField=999 y MessageField=''El Centro de Costo con Código <code> no existe''; en caso contrario, completa CostCenterId/Code/Name.; [RETURN_RESULT] @TableXmlObject: Al final retorna SELECT * de la tabla variable con los registros resueltos y su estado/mensaje.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La Sucursal (BranchOfficeCode) no existe en Payroll.BranchOffice → Marca StatusField=999 y MessageField indicando que la sucursal no existe; los siguientes UPDATE no procesan ese registro por filtro StatusField=0 else Asigna BranchOfficeId, Code y Name desde Payroll.BranchOffice; si La Unidad Funcional (FunctionalUnitCode) no existe en Payroll.FunctionalUnit y StatusField=0 → Marca StatusField=999 y MessageField indicando que la unidad funcional no existe else Asigna FunctionalUnitId, Code y Name desde Payroll.FunctionalUnit; si El Centro de Costo (CostCenterCode) no existe en Payroll.CostCenter y StatusField=0 → Marca StatusField=999 y MessageField indicando que el centro de costo no existe else Asigna CostCenterId, Code y Name desde Payroll.CostCenter; si Ocurre una excepción en el bloque TRY → El CATCH inserta una fila con StatusField=999 y MessageField = ERROR_MESSAGE() + línea del error', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.BranchOffice; Payroll.FunctionalUnit; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CopyAndPasteBillingConceptCostCenter';
-- GO
