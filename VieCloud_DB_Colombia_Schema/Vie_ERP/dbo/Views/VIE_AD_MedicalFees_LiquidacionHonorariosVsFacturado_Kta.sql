
CREATE VIEW [dbo].[VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta]
AS
SELECT        TOP (100) PERCENT L.Code AS Codigo_Liquidacion, C.Id AS IDCausacion, LD.Id AS IDLiquidacion, LD.MedicalFeesCausationId, E.Name AS Entidad_Administradora, E1.Name AS Tercero, F.InvoiceNumber AS Factura, 
                         F.InvoiceDate AS Fecha_Factura, F.AdmissionNumber AS Ingreso, P.IPCODPACI AS Identificacion_Usuario, P.IPNOMCOMP AS Nombre_Usuario, CU.Code AS CUPS, CU.Description AS Descripcion_CUPS, 
                         S.Code AS CodigoServicio, S.Name AS NombreServicio, FD.InvoicedQuantity AS Cantidad_Cobrada, FD.GrandTotalSalesPrice AS Valor_Facturado_Entidad, C.TotalAmountPayable AS Valor_Pagar_Tercero, 
                         T.Name AS Tercero_Causa, CASE C.Status WHEN '1' THEN 'Causado' WHEN '2' THEN 'Liquidado' WHEN '3' THEN 'Confirmado' WHEN '4' THEN 'Anulado' END AS Estado_Causacion, 
                         CASE L.Status WHEN '1' THEN 'Registrado' WHEN '2' THEN 'Confirmado' WHEN '3' THEN 'Anulado' END AS Estado_Liquidacion, L.ConfirmationDate AS Fecha_Confirmacion_Liquidacion, PER.Fullname AS Facturador, 
                         D.CODICIE10 AS Diadnostico_CIE10, D.NOMDIAGNO AS Diagnostico_Egreso, CC.Name AS CentroCosto
FROM            Billing.InvoiceDetail AS FD INNER JOIN
                         Billing.Invoice AS F WITH (nolock) ON F.Id = FD.InvoiceId LEFT OUTER JOIN
                         MedicalFees.MedicalFeesCausation AS C WITH (nolock) ON C.InvoiceDetailId = FD.Id LEFT OUTER JOIN
                         MedicalFees.MedicalFeesLiquidationDetail AS LD WITH (nolock) ON LD.MedicalFeesCausationId = C.Id LEFT OUTER JOIN
                         Common.ThirdParty AS T WITH (nolock) ON T.Id = C.ThirdPartyId LEFT OUTER JOIN
                         Billing.ServiceOrderDetail AS OD WITH (nolock) ON OD.Id = FD.ServiceOrderDetailId LEFT OUTER JOIN
                         Contract.CUPSEntity AS CU WITH (nolock) ON CU.Id = OD.CUPSEntityId LEFT OUTER JOIN
                         MedicalFees.MedicalFeesLiquidation AS L WITH (nolock) ON L.Id = LD.MedicalFeesLiquidacionId LEFT OUTER JOIN
                         Contract.HealthAdministrator AS E WITH (nolock) ON E.Id = F.HealthAdministratorId LEFT OUTER JOIN
                         Common.ThirdParty AS E1 WITH (nolock) ON E1.Id = F.ThirdPartyId LEFT OUTER JOIN
                         Security.[User] AS U ON U.UserCode = F.InvoicedUser LEFT OUTER JOIN
                         Security.Person AS PER ON PER.Id = U.IdPerson LEFT OUTER JOIN
                         Contract.IPSService AS S WITH (nolock) ON S.Id = OD.IPSServiceId LEFT OUTER JOIN
                         dbo.INPACIENT AS P WITH (nolock) ON P.IPCODPACI = F.PatientCode LEFT OUTER JOIN
                         dbo.INDIAGNOS AS D WITH (nolock) ON D.CODICIE10 = F.OutputDiagnosis LEFT OUTER JOIN
                         Payroll.CostCenter AS CC WITH (nolock) ON CC.Id = OD.CostCenterId

WHERE        (F.InvoiceDate > '30/06/2019') AND (OD.CUPSEntityId IS NOT NULL)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que cruza la facturación emitida desde julio de 2019 (solo líneas con CUPS asignado) con las causaciones y liquidaciones de honorarios médicos, permitiendo comparar lo facturado a la entidad administradora contra el valor a pagar al tercero profesional. Consolida datos del paciente, diagnóstico de egreso CIE-10, servicio CUPS, centro de costo y estados de causación/liquidación, orientada a conciliación y auditoría del proceso de honorarios médicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta el comparativo entre honorarios médicos causados/liquidados a terceros y los valores facturados a la entidad administradora, con datos del paciente, factura, servicio CUPS y diagnóstico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe tener fecha posterior al 30/06/2019; El detalle de la orden de servicio debe tener un CUPSEntityId asociado (no nulo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros con fecha de factura posterior al 30/06/2019; Solo se incluyen ítems facturados ligados a un servicio CUPS configurado; La causación, liquidación, tercero, CUPS, entidad administradora, usuario facturador, paciente, diagnóstico y centro de costo se traen como LEFT JOIN, pudiendo no existir; La relación entre causación y factura se establece a través del detalle de factura (InvoiceDetailId); Cada causación puede vincularse a una liquidación a través de MedicalFeesLiquidationDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de honorarios médicos; Causación de honorarios; Factura; Entidad administradora de salud; Tercero; Paciente; CUPS; Servicio IPS; Diagnóstico CIE10; Centro de costo; Facturador; Ingreso (admisión)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve filas combinando facturación, causación y liquidación de honorarios médicos solo cuando F.InvoiceDate > ''30/06/2019'' y OD.CUPSEntityId IS NOT NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.Status = ''1'' → Estado de causación se traduce a ''Causado''; si C.Status = ''2'' → Estado de causación se traduce a ''Liquidado''; si C.Status = ''3'' → Estado de causación se traduce a ''Confirmado''; si C.Status = ''4'' → Estado de causación se traduce a ''Anulado''; si L.Status = ''1'' → Estado de liquidación se traduce a ''Registrado''; si L.Status = ''2'' → Estado de liquidación se traduce a ''Confirmado''; si L.Status = ''3'' → Estado de liquidación se traduce a ''Anulado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceDetail; Billing.Invoice; MedicalFees.MedicalFeesCausation; MedicalFees.MedicalFeesLiquidationDetail; Common.ThirdParty; Billing.ServiceOrderDetail; Contract.CUPSEntity; MedicalFees.MedicalFeesLiquidation; Contract.HealthAdministrator; Security.User; Security.Person; Contract.IPSService; dbo.INPACIENT; dbo.INDIAGNOS; Payroll.CostCenter', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_MedicalFees_LiquidacionHonorariosVsFacturado_Kta';
GO
