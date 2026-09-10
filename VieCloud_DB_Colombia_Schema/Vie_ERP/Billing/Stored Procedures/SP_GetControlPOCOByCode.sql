
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que devuelve los datos ingreso
-- =============================================

CREATE PROCEDURE [Billing].[SP_GetControlPOCOByCode]
	@admissionCode varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	
	select rc.Id,
		rc.FolioQuantity,
		cg.LiquidationType,
		cg.ContractId,
		rcd.Id as RevenueControlDetailId,
		rcd.[Status] as RevenueControlDetailStatus,
		rcd.FolioOrder,
		i.InvoiceNumber,
		case when c.Code is null then '' else concat(c.Code, ' - ', c.ContractName) end as ContractCodeName
	from Billing.RevenueControl rc with(nolock)
	inner join [Billing].[RevenueControlDetail] rcd with(nolock) on rcd.RevenueControlId = rc.Id
	inner join [Contract].[CareGroup] cg with(nolock) on rcd.CareGroupId = cg.Id
	left join [Contract].[Contract] c with(nolock) on c.Id = cg.ContractId
	left join Billing.Invoice i with(nolock) on i.RevenueControlDetailId = rcd.Id And i.[Status] = 1
	where rc.AdmissionNumber = @admissionCode and rcd.IsMasterAccount <> 3

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el control de ingresos (topes de cobro y folios de facturación) asociado a un número de ingreso o admisión del paciente. Para cada folio del control, recupera su estado, orden, tipo de liquidación y el contrato de la entidad pagadora (EPS o aseguradora) al que pertenece, incluyendo el número de factura activa si ya fue emitida. Integra las tablas de control de ingresos, detalle de folios, grupos de atención, contratos y facturas para entregar en una sola consulta toda la información de facturación y liquidación de un ingreso específico. Se usa para cargar la ficha de control de cobro de una admisión en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetControlPOCOByCode';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetControlPOCOByCode';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta los folios de control de ingreso y su detalle de facturación asociados a una admisión, incluyendo contrato, grupo de atención y factura vigente, para visualización.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetControlPOCOByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un RevenueControl cuya AdmissionNumber coincida con el código de admisión recibido.; Cada RevenueControlDetail debe estar vinculado a un CareGroup existente (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetControlPOCOByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye siempre los detalles cuya IsMasterAccount = 3 (no se consideran en el listado).; Solo asocia facturas con Status = 1 (factura activa/vigente); otros estados no se devuelven aunque exista la fila de detalle.; Usa lecturas con NOLOCK en todas las tablas, permitiendo lecturas sucias.; La relación con Contract es opcional (LEFT JOIN): un detalle puede existir sin contrato visible.; La relación con Invoice es opcional: un detalle puede no tener factura emitida activa.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetControlPOCOByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Admisión; Control de ingresos; Folio de facturación; Tipo de liquidación; Contrato; Grupo de atención (CareGroup); Factura; Cuenta maestra (IsMasterAccount)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetControlPOCOByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de RevenueControl + RevenueControlDetail + CareGroup (+ Contract e Invoice opcionales) filtrando por AdmissionNumber = @admissionCode y rcd.IsMasterAccount <> 3.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetControlPOCOByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si c.Code IS NULL (no hay contrato asociado al CareGroup) → Devuelve cadena vacía '''' en ContractCodeName else Concatena el código y el nombre del contrato como ''Code - ContractName''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetControlPOCOByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControl; Billing.RevenueControlDetail; Contract.CareGroup; Contract.Contract; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetControlPOCOByCode';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetControlPOCOByCode';
-- GO
