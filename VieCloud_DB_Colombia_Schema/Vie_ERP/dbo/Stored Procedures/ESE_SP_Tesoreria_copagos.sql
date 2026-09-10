
CREATE PROCEDURE [dbo].[ESE_SP_Tesoreria_copagos]

@FechaIni DateTime,
@FechaFin DateTime

AS

select  DISTINCT case when PatientType=1 then 'Contributivo'  when PatientType=2 then 'Subsidiado' when PatientType=3 then 'Vinculado'
when PatientType=4 then 'Particular' when PatientType=5 then 'Otro'   when PatientType=6 then 'Desplazado Reg. Contributivo'
when PatientType=7 then 'Desplazado Reg. Subsidiado'  when PatientType=8 then 'Desplazado No Asegurado'  end TipoPaciente, a.code,convert(numeric(18,0),b.value) valor
from [Treasury].[CashReceipts] a
inner join [Treasury].[CashReceiptDetails] b on a.Id=b.IdCashReceipt and a.Status=2 
inner join Treasury.CashReceiptConcepts  c on c.id=b.IdCashReceiptConcept  and c.code in ('019','031')
left join Portfolio.PortfolioAdvance d on d.CashReceiptDetailId=b.Id  and d.CashReceiptId=a.Id
left join [Portfolio].[PortfolioTransfer] e on e.PortfolioAdvanceId=d.id
left join [Portfolio].[PortfolioTransferDetail] f on f.PortfolioTrasferId=e.Id
left join Billing.Invoice h on h.AdmissionNumber=d.AdmissionNumber  and h.status=1  
---left join Portfolio.AccountReceivable g on g.Id=f.AccountReceivableId  and g.InvoiceNumber=h.InvoiceNumber
where a.DocumentDate>= @FechaIni  and  a.DocumentDate<= @FechaFin
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de copagos y cuotas moderadoras recaudados en tesorería durante un rango de fechas. Consulta los recibos de caja activos (estado 2) filtrando únicamente los conceptos de ingreso correspondientes a copago (códigos 019 y 031), y cruza esa información con los anticipos de cartera, los traslados de cartera y las facturas de venta asociadas al número de ingreso del paciente. Devuelve para cada cobro el tipo de régimen del paciente (Contributivo, Subsidiado, Particular, Desplazado, etc.), el número del recibo de caja y el valor recaudado. Se utiliza para cuadre y control de los pagos realizados directamente por pacientes en caja, en el marco del informe de copagos de una ESE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Tesoreria_copagos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Tesoreria_copagos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los recibos de caja confirmados del rango de fechas con conceptos de copago, mostrando tipo de paciente, código de recibo y valor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Tesoreria_copagos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proporcionar un rango de fechas (inicial y final) para filtrar por DocumentDate.; Existen registros en Treasury.CashReceiptConcepts con códigos ''019'' y ''031''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Tesoreria_copagos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran recibos de caja con Status=2 (confirmados/aplicados).; Solo se consideran detalles cuyo concepto de recaudo tenga código ''019'' o ''031''.; Solo se consideran facturas con Status=1 cuando hay cruce con anticipo de cartera.; El valor reportado se trunca/convierte a numeric(18,0) (sin decimales).; Las relaciones con cartera (PortfolioAdvance, PortfolioTransfer, PortfolioTransferDetail) y factura son LEFT JOIN: no excluyen recibos sin cruce.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Tesoreria_copagos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Copago; Tipo de paciente (régimen: contributivo, subsidiado, vinculado, particular, desplazado); Recibo de caja; Concepto de recaudo; Anticipo de cartera; Transferencia de cartera; Factura; Tesorería', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Tesoreria_copagos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas DISTINCT con TipoPaciente (decodificado de PatientType 1..8), código del recibo y valor numérico, para recibos con Status=2 cuyo concepto de detalle esté en (''019'',''031'') y DocumentDate dentro del rango.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Tesoreria_copagos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PatientType = 1 → TipoPaciente = ''Contributivo''; si PatientType = 2 → TipoPaciente = ''Subsidiado''; si PatientType = 3 → TipoPaciente = ''Vinculado''; si PatientType = 4 → TipoPaciente = ''Particular''; si PatientType = 5 → TipoPaciente = ''Otro''; si PatientType = 6 → TipoPaciente = ''Desplazado Reg. Contributivo''; si PatientType = 7 → TipoPaciente = ''Desplazado Reg. Subsidiado''; si PatientType = 8 → TipoPaciente = ''Desplazado No Asegurado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Tesoreria_copagos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptConcepts; Portfolio.PortfolioAdvance; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Tesoreria_copagos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Tesoreria_copagos';
-- GO
