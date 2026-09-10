
-- ===============================================================================================================
-- Author:		Juan Pablo Daza Medina
-- Create date: 2023-08-25
-- Description:	Trae las rentas exentas del empleado
-- ==============================================================================================================
CREATE VIEW [Common].[ViewEmployeeExemptIncome]
AS

		SELECT DISTINCT
			CONCAT(l.id, '-', jvt.Code) AS Id,
			tp.id AS ThirdPartyId,
			l.RegisterStatus,
			jvt.[name] AS VoucherType,
			jvt.Code AS VoucherCode,
			l.PayrollDateLiquidated AS DateLiquidation,
			l.TotalPaid AS MonthlyIncome,
			l.ExemptValueRetention AS ExemptIncomeValue,
			YEAR(l.PayrollDateLiquidated) AS YearLiquidated,
			0 AS isNew,
			'' AS Comments
		FROM GeneralLedger.JournalVouchers jv
		JOIN GeneralLedger.JournalVoucherDetails jvd WITH(NOLOCK)
			ON jvd.IdAccounting = jv.Id AND jvd.IdThirdParty IS NOT NULL
		JOIN Common.ThirdParty tp ON jvd.IdThirdParty = tp.id
		JOIN Payroll.Employee e ON tp.id = e.ThirdPartyId
		JOIN Payroll.Liquidation l ON e.id = l.EmployeeId
		JOIN GeneralLedger.JournalVoucherTypes jvt ON jv.IdJournalVoucher = jvt.id
		WHERE jv.LegalBookId = 1 

		UNION ALL

		SELECT 
			CAST(ei.Id as varchar),
			ei.ThirdPartyId,
			'C' RegisterStatus,
			ei.VoucherType VoucherType,
			ei.VoucherCode VoucherCode,
			ei.DateLiquidation DateLiquidation,
			ei.MonthlyIncome MonthlyIncome,
			ei.ExemptIncomeValue ExemptIncomeValue,
			YEAR(ei.DateLiquidation) YearLiquidated,
			0 isNew,
			ei.Comments Comments
		FROM [Common].[ExemptIncome] ei
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las rentas exentas (ingresos no gravables) de los empleados para el cálculo de retención en la fuente. Combina dos fuentes: por un lado, extrae los valores de ingreso total y valor exento directamente de las liquidaciones de nómina vinculadas a comprobantes contables del libro legal; por otro, incorpora los registros de ingresos exentos ingresados manualmente en la tabla de ingresos no gravables. El resultado unificado muestra, por empleado (tercero), el tipo y código de comprobante, la fecha de liquidación, el ingreso mensual, el valor exento y el año de liquidación, sirviendo como insumo para reportes de retención en la fuente y soporte de auditoría tributaria de nómina.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewEmployeeExemptIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewEmployeeExemptIncome';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica las rentas exentas de empleados provenientes de comprobantes contables del libro legal con los ingresos exentos registrados manualmente, para reportes de retención en la fuente y auditoría tributaria de nómina.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewEmployeeExemptIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las liquidaciones de nómina deben estar asociadas a un empleado con tercero válido (ThirdPartyId).; Los detalles de comprobantes contables deben tener IdThirdParty no nulo para considerarse.; Los comprobantes deben pertenecer al libro legal (LegalBookId = 1) para integrarse desde contabilidad.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewEmployeeExemptIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'YearLiquidated siempre se deriva con YEAR() sobre la fecha de liquidación correspondiente.; isNew siempre es 0 en ambos orígenes.; Comments es cadena vacía para registros contables y se conserva el original solo en los manuales.; Solo se consideran comprobantes del libro legal (LegalBookId = 1).; Se aplica DISTINCT al segmento contable para evitar duplicados por múltiples líneas de detalle.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewEmployeeExemptIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'renta exenta; ingreso exento; retención en la fuente; liquidación de nómina; comprobante contable; tercero; empleado; libro legal; ingreso mensual; auditoría tributaria', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewEmployeeExemptIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.ViewEmployeeExemptIncome: Devuelve registros contables solo cuando jv.LegalBookId = 1 y jvd.IdThirdParty IS NOT NULL, uniéndolos vía UNION ALL con todos los registros de Common.ExemptIncome.; [RETURN_RESULT] Common.ViewEmployeeExemptIncome: Para registros provenientes de ExemptIncome se asigna RegisterStatus = ''C'' fijo; para los de contabilidad se conserva el RegisterStatus de la liquidación.; [RETURN_RESULT] Common.ViewEmployeeExemptIncome: El Id del registro contable se construye como CONCAT(Liquidation.id, ''-'', VoucherType.Code); para ExemptIncome se usa el Id casteado a varchar.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewEmployeeExemptIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen contable: jv.LegalBookId = 1 y existe IdThirdParty en el detalle → Se trae la renta exenta desde la liquidación de nómina vinculada al tercero/empleado, usando ExemptValueRetention y TotalPaid. else Se omite del segmento contable; los registros manuales se agregan vía UNION ALL desde ExemptIncome sin filtros.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewEmployeeExemptIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; Common.ThirdParty; Payroll.Employee; Payroll.Liquidation; GeneralLedger.JournalVoucherTypes; Common.ExemptIncome', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewEmployeeExemptIncome';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewEmployeeExemptIncome';
GO
