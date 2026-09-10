CREATE VIEW [Billing].[ViewListRecognitionEntranceReport]
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
			us.UserCode
	FROM Contract.CareGroup cg WITH (NOLOCK)
	JOIN Billing.RevenueRecognition rr WITH (NOLOCK) ON cg.Id = rr.CareGroupId
	JOIN Billing.RevenueRecognitionDetail rrd WITH (NOLOCK) ON rr.Id = rrd.RevenueRecognitionId
	JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON rr.JournalVoucherTypeReverseId = jvt.Id
	JOIN [Security].[User] us ON rr.CreationUser = us.Id
	WHERE rr.State = 2 And cg.LiquidationType In (1, 3)
	GROUP BY cg.Id, cg.Code, cg.Name, cg.OperativeUnitId, 
			rr.Id, rr.VoucherDate, jvt.Code, jvt.[Name], jvt.Description, us.UserCode
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista para el reporte de reconocimiento de ingresos por ingreso/entrada en facturación. Consolida, por cada proceso de reconocimiento de ingresos (causación contable), el grupo de atención del contrato, el comprobante contable de reversión asociado, el valor total facturado (suma de precio tercero, copago del paciente y descuentos), la cantidad de folios o líneas de control de ingresos involucradas, y el usuario que generó el proceso. Aplica únicamente a reconocimientos en estado 2 (causados/activos) y a grupos de atención con tipo de liquidación 1 o 3, combinando información de contratos, reconocimientos de ingresos, detalle de reconocimiento, tipos de comprobante contable y usuarios de seguridad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRecognitionEntranceReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListRecognitionEntranceReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los reconocimientos de ingresos por grupo de atención, totalizando valores y folios, para reportar las causaciones contables vigentes con liquidación por entrada.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntranceReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El reconocimiento de ingresos debe estar en estado 2 (rr.State = 2).; El grupo de atención debe tener LiquidationType en (1, 3).; Debe existir un tipo de comprobante contable asociado vía JournalVoucherTypeReverseId.; El usuario creador del reconocimiento debe existir en Security.User.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntranceReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan reconocimientos cuyo tipo de liquidación del grupo de atención sea 1 o 3 (liquidación por entrada).; El tipo de comprobante reportado corresponde al voucher de reverso (JournalVoucherTypeReverseId), no al de causación original.; El total por grupo de atención suma valores de tercero, paciente y descuento global del detalle.; Cada folio se cuenta una sola vez por reconocimiento (DISTINCT RevenueControlDetailId).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntranceReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento de ingresos; Grupo de atención; Tipo de liquidación; Comprobante contable de reverso; Folio de control de ingresos; Causación; Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntranceReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListRecognitionEntranceReport: Devuelve por reconocimiento la suma de ThirdPartySalesPrice + SubTotalPatientSalesPrice + GrandTotalDiscount como TotalCareGroup y el conteo distinto de RevenueControlDetailId como FolioQuantity.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntranceReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rr.State = 2 AND cg.LiquidationType IN (1, 3) → Incluye el reconocimiento en el reporte agregado por grupo de atención y voucher. else Se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntranceReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.CareGroup; Billing.RevenueRecognition; Billing.RevenueRecognitionDetail; GeneralLedger.JournalVoucherTypes; Security.User', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntranceReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListRecognitionEntranceReport';
GO
