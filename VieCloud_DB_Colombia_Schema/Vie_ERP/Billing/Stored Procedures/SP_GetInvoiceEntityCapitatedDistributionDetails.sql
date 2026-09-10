-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-15
-- Description:	Procedimiento que se encarga de obtener los controles de una distribución de Factura Monto Fijo
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetInvoiceEntityCapitatedDistributionDetails]
	@InvoiceEntityCapitatedId AS INT,
	@InvoiceEntityCapitatedDistributionId AS INT
AS
BEGIN

	DECLARE @DistributionStatus TINYINT = 1

	SELECT @DistributionStatus = iecd.Status
	FROM Billing.InvoiceEntityCapitatedDistribution iecd
	WHERE iecd.Id = @InvoiceEntityCapitatedDistributionId
	
	SELECT DISTINCT
		iecdd.Id AS Id,
		iecdd.InvoiceEntityCapitatedDistributionId,
		i.Id AS InvoiceId,
		i.HealthAdministratorId AS HealthAdministratorId,
		CONCAT(ha.Code, ' - ', ha.Name) AS HealthAdministratorName,
		i.InvoiceCategoryId,
		IIF(ic.Id IS NULL, '', CONCAT(ic.Code, ' - ', ic.Name)) AS InvoiceCategoryName,
		i.InvoiceNumber,
		i.InvoiceDate,
		i.ThirdPartySalesValue AS InvoiceValue,
		i.Status,
		i.AdmissionNumber
	FROM Billing.InvoiceEntityCapitated iec
	JOIN Billing.Invoice i ON i.DocumentType = 5 
		AND iec.CareGroupId = i.CareGroupId 
		AND CAST(i.InvoiceDate AS DATE) BETWEEN iec.InitialDate AND iec.EndDate
	JOIN Contract.HealthAdministrator ha ON i.HealthAdministratorId = ha.Id
	LEFT JOIN Billing.InvoiceCategories ic ON i.InvoiceCategoryId = ic.Id
	LEFT JOIN Billing.InvoiceEntityCapitatedDistributionDetail iecdd ON i.Id = iecdd.InvoiceId 
		AND iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId
	-- NO MOSTRAR LOS QUE YA ESTEN EN OTRA DISTRIBUCION --
	LEFT JOIN 
	(
		SELECT iecdi.Id, iecddi.InvoiceId
		FROM Billing.InvoiceEntityCapitatedDistribution iecdi
		JOIN Billing.InvoiceEntityCapitatedDistributionDetail iecddi ON iecdi.Status IN (1, 2)
			AND iecdi.Id = iecddi.InvoiceEntityCapitatedDistributionId	
	) iecd ON i.Id = iecd.InvoiceId
	WHERE iec.Id = @InvoiceEntityCapitatedId 		
		AND (iecd.InvoiceId IS NULL OR iecd.Id = @InvoiceEntityCapitatedDistributionId)
		AND 
		(
			(@DistributionStatus = 1 AND i.Status = 1)
			OR 
			(@DistributionStatus <> 1 AND iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId)
		)

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle de las facturas disponibles o ya asignadas para una distribución específica de una factura de capitación (monto fijo). Consulta las facturas de venta (tipo documento 5) emitidas dentro del período y grupo de atención de la capitación, cruzando con la administradora de salud (EPS/ARS) y la categoría de facturación. Filtra las facturas que ya estén incluidas en otra distribución activa o confirmada, evitando duplicados entre distribuciones. Se usa en el proceso de liquidación y reparto de capitación para asignar facturas individuales a una distribución capitada, mostrando el número de factura, fecha, valor cobrado al tercero, estado y número de admisión.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las facturas candidatas o ya asociadas a una distribución de factura de monto fijo (capitación), filtrando por grupo de cuidado, ventana de vigencia y excluyendo facturas que ya formen parte de otra distribución activa.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una InvoiceEntityCapitated con el Id recibido (define CareGroupId y rango InitialDate-EndDate).; El InvoiceEntityCapitatedDistributionId recibido determina el Status que dirige el filtro; si no existe, se asume Status=1 por defecto.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran facturas con DocumentType = 5 (facturas de capitación / monto fijo).; Una factura no puede aparecer asociada simultáneamente a dos distribuciones cuyo Status esté en (1,2): se filtra para mostrar solo la distribución actual.; La factura debe pertenecer al mismo CareGroupId que la entidad capitada y caer dentro de su ventana [InitialDate, EndDate].; Distribuciones con Status distinto de 1 son de solo lectura: solo muestran las facturas ya vinculadas, sin permitir nuevas candidatas.; Solo facturas con Status=1 son candidatas a ingresar en una distribución abierta.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura de capitación / monto fijo; Distribución de factura capitada; Grupo de cuidado (CareGroup); Administradora de salud (EPS/ARS); Categoría de factura; Período de vigencia de capitación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve facturas con DocumentType=5 (capitación) cuyo CareGroupId coincide con la entidad capitada y cuya InvoiceDate cae entre InitialDate y EndDate de InvoiceEntityCapitated.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @DistributionStatus = 1 (distribución en estado inicial/abierta) AND i.Status = 1 → Incluye facturas activas candidatas que aún no estén tomadas por otra distribución (iecd.InvoiceId IS NULL) o que ya pertenezcan a la distribución actual. else Cuando la distribución no está en estado 1, solo se devuelven las facturas que YA están vinculadas al detalle de esa distribución (iecdd.InvoiceEntityCapitatedDistributionId = @InvoiceEntityCapitatedDistributionId).; si Factura existe en otra InvoiceEntityCapitatedDistribution con Status IN (1,2) distinta de la actual → Se excluye del resultado (regla ''NO MOSTRAR LOS QUE YA ESTEN EN OTRA DISTRIBUCION''). else Se incluye si cumple las demás condiciones.; si i.InvoiceCategoryId resuelve a una categoría existente → Se concatena Code y Name como InvoiceCategoryName. else Devuelve cadena vacía como InvoiceCategoryName (IIF ic.Id IS NULL).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceEntityCapitatedDistribution; Billing.InvoiceEntityCapitated; Billing.Invoice; Contract.HealthAdministrator; Billing.InvoiceCategories; Billing.InvoiceEntityCapitatedDistributionDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInvoiceEntityCapitatedDistributionDetails';
-- GO
