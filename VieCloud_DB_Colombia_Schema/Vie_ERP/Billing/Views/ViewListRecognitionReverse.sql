CREATE VIEW [Billing].[ViewListRecognitionReverse]
AS
	SELECT	cg.Id CareGroupId,
			CONCAT(cg.Code, ' - ', cg.Name) as CareGroupCodeName,
			cg.OperativeUnitId,
			rr.Id as RecognitionId,
			rr.VoucherDate,
			CONCAT(jvt.Code, ' - ', jvt.[Name]) as JournalVoucherTypeCodeName,
			jvt.Description,
			SUM(rrd.ThirdPartySalesPrice + rrd.SubTotalPatientSalesPrice + rrd.GrandTotalDiscount) as TotalCareGroup,
			COUNT(DISTINCT rrd.RevenueControlDetailId) as FolioQuantity,
			us.UserCode,
			0 as StateOperation,
			'' as MessageInfo
	FROM Contract.CareGroup cg WITH (NOLOCK)
	JOIN Billing.RevenueRecognition rr WITH (NOLOCK) ON cg.Id = rr.CareGroupId
	JOIN Billing.RevenueRecognitionDetail rrd WITH (NOLOCK) ON rr.Id = rrd.RevenueRecognitionId
	JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON rr.JournalVoucherTypeId = jvt.Id
	JOIN [Security].[User] us ON rr.CreationUser = us.Id
	WHERE rr.State = 1 And cg.LiquidationType In (1, 3)
	GROUP BY cg.Id, cg.Code, cg.Name, cg.OperativeUnitId, 
			rr.Id, rr.VoucherDate, jvt.Code, jvt.[Name], jvt.Description, us.UserCode
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los reconocimientos de ingresos contables que están en estado activo (State=1) y son candidatos a ser reversados, filtrando únicamente los grupos de atención con tipos de liquidación 1 y 3. Consolida por cada reconocimiento el grupo de atención (código y nombre), la unidad operativa, la fecha del comprobante contable, el tipo de comprobante (voucher), el valor total del grupo (suma de precio tercero, precio paciente y descuentos), la cantidad de folios o líneas de detalle distintas, y el usuario que realizó el reconocimiento. Integra los datos del contrato (grupos de atención y sus reglas de liquidación), el encabezado y detalle del reconocimiento de ingresos, el tipo de comprobante contable y el usuario de seguridad. Sirve como fuente para la pantalla o reporte de reversión de reconocimiento de ingresos en el módulo de facturación, permitiendo identificar qué causaciones contables pueden ser anuladas o revertidas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRecognitionReverse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRecognitionReverse';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los reconocimientos de ingresos activos elegibles para reverso, agrupados por grupo de atención y comprobante contable, con su total monetario, cantidad de folios y usuario creador.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionReverse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir reconocimientos de ingresos (Billing.RevenueRecognition) con State = 1 vinculados a grupos de atención cuyo LiquidationType esté en (1, 3).; Cada reconocimiento debe tener detalles en Billing.RevenueRecognitionDetail, un tipo de comprobante en GeneralLedger.JournalVoucherTypes y un usuario creador válido en Security.User.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionReverse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan reconocimientos de ingresos cuyo State = 1 (activos/vigentes), candidatos a ser reversados.; Solo se incluyen grupos de atención cuya LiquidationType esté en (1, 3).; El total por grupo de atención se calcula como la suma de ThirdPartySalesPrice + SubTotalPatientSalesPrice + GrandTotalDiscount sobre los detalles del reconocimiento.; La cantidad de folios se obtiene contando RevenueControlDetailId distintos por reconocimiento.; StateOperation siempre se expone con valor 0 y MessageInfo como cadena vacía (placeholders para uso del consumidor).; La agregación se hace por reconocimiento de ingresos dentro de cada grupo de atención, tipo de voucher y usuario creador.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionReverse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo de atención (CareGroup); Reconocimiento de ingresos; Comprobante contable (Journal Voucher); Tipo de liquidación; Reverso de reconocimiento; Folio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionReverse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListRecognitionReverse: Devuelve únicamente filas donde rr.State = 1 AND cg.LiquidationType IN (1, 3), agrupadas por grupo de atención, reconocimiento, tipo de voucher y usuario, con totales y conteo de folios.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionReverse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Billing.RevenueRecognition; Billing.RevenueRecognitionDetail; GeneralLedger.JournalVoucherTypes; Security.User', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionReverse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionReverse';
GO
