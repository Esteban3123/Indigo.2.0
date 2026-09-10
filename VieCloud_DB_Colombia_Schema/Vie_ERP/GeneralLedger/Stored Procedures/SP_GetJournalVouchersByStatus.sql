-- =============================================
-- Author:		Sergio Abraham Fernandez Cruz
-- Create date: 07-07-2014
-- Description:	sp para obtener todos los comprobantes contables por estado
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_GetJournalVouchersByStatus]
	@Status as int,
	@Month as int,
	@Year as int	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT jv.Id as Id,CAST(Jv.Consecutive as varchar(20)) as Code, COALESCE(jv.EntityName, '') as EntityName, Jv.VoucherDate as DocumentDate, COALESCE(Jv.Detail,'') as Detail, CAST(0 AS bit) as State,1 as DocumentType ,'Accounting' as Origin 
	from GeneralLedger.JournalVouchers as Jv where (Jv.Status = @Status and  month(Jv.VoucherDate ) = @Month and YEAR (Jv.VoucherDate ) = @Year)
	union 
	select tc.id as Id,tc.DocumentNumber as Code,'' as EntityName,tc.DocumentDate as DocumentDate,'' as Detail, CAST(0 AS bit) as State ,tc.DocumentType as DocumentType , 'TreasuryControl' as Origin
	from Treasury.TreasuryControl as tc where (month(tc.DocumentDate  ) = @Month and YEAR (tc.DocumentDate ) = @Year)
	union 
	select pc.id as Id,pc.DocumentNumber as Code,'' as EntityName,pc.DocumentDate as DocumentDate,'' as Detail, CAST(0 AS bit) as State ,pc.DocumentType as DocumentType ,'PortfolioControl' as Origin
	from Portfolio.PortfolioControl as pc where (month(pc.DocumentDate  ) = @Month and YEAR (pc.DocumentDate ) = @Year)
	union 
	select pc.id as Id,pc.DocumentNumber as Code,'' as EntityName,pc.DocumentDate as DocumentDate,'' as Detail, CAST(0 AS bit) as State ,pc.DocumentType as DocumentType , 'PaymentsControl' as Origin
	from Payments.PaymentsControl as pc where (month(pc.DocumentDate  ) = @Month and YEAR (pc.DocumentDate ) = @Year)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y consolida todos los comprobantes y documentos financieros de un mes y año específicos, filtrando los comprobantes contables (libro mayor) por su estado (aprobado, pendiente, anulado, etc.) y combinándolos con los documentos de tesorería, portafolio y pagos del mismo período. Devuelve una lista unificada de documentos financieros indicando su origen (contabilidad, tesorería, portafolio o pagos), útil para revisión contable, cuadre de cierres mensuales y auditoría de movimientos financieros por estado y período.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_GetJournalVouchersByStatus';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_GetJournalVouchersByStatus';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los comprobantes contables y documentos de tesorería, portafolio y pagos correspondientes a un mes y año dados, filtrando los comprobantes contables por estado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GetJournalVouchersByStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El estado, mes y año deben suministrarse como enteros válidos.; Las tablas de los módulos GeneralLedger, Treasury, Portfolio y Payments deben existir y ser accesibles.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GetJournalVouchersByStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El filtro por Status sólo se aplica a los comprobantes contables; los registros de Treasury, Portfolio y Payments se incluyen sin importar su estado.; Todos los registros se devuelven con State fijo en 0 (bit).; Cada fila se etiqueta con un Origin literal que identifica el módulo origen (''Accounting'',''TreasuryControl'',''PortfolioControl'',''PaymentsControl'').; Para los comprobantes contables, EntityName y Detail nulos se reemplazan por cadena vacía; para los demás orígenes EntityName y Detail siempre son cadena vacía.; Los comprobantes contables se entregan con DocumentType=1 fijo, mientras que los demás conservan el DocumentType original del registro.; El filtrado temporal se realiza por componentes month()/YEAR() de la fecha, no por rangos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GetJournalVouchersByStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Comprobante de diario; Tesorería; Portafolio; Pagos; Estado del comprobante; Tipo de documento', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GetJournalVouchersByStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] GeneralLedger.JournalVouchers: Devuelve los comprobantes cuyo Status coincide con el parámetro y cuya VoucherDate pertenece al mes y año indicados, marcados con Origin=''Accounting'' y DocumentType=1.; [RETURN_RESULT] Treasury.TreasuryControl: Devuelve los documentos cuya DocumentDate pertenece al mes y año indicados, con Origin=''TreasuryControl'' (sin filtro de estado).; [RETURN_RESULT] Portfolio.PortfolioControl: Devuelve los documentos cuya DocumentDate pertenece al mes y año indicados, con Origin=''PortfolioControl'' (sin filtro de estado).; [RETURN_RESULT] Payments.PaymentsControl: Devuelve los documentos cuya DocumentDate pertenece al mes y año indicados, con Origin=''PaymentsControl'' (sin filtro de estado).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GetJournalVouchersByStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; Treasury.TreasuryControl; Portfolio.PortfolioControl; Payments.PaymentsControl', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GetJournalVouchersByStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_GetJournalVouchersByStatus';
-- GO
