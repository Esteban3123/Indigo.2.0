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