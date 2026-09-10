
CREATE PROCEDURE [dbo].[ESE_SP_Presupuesto_reconocimientos]

@FechaIni DateTime,
@FechaFin DateTime

AS

select  DISTINCT a.code,convert(numeric(18,0),b.value) valor,case when PatientType=1 then 'Contributivo'  when PatientType=2 then 'Subsidiado' when PatientType=3 then 'Vinculado'
when PatientType=4 then 'Particular' when PatientType=5 then 'Otro'   when PatientType=6 then 'Desplazado Reg. Contributivo'
when PatientType=7 then 'Desplazado Reg. Subsidiado'  when PatientType=8 then 'Desplazado No Asegurado'  end TipoPaciente
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los reconocimientos o recaudos de caja registrados en tesorería dentro de un rango de fechas, filtrando únicamente los recibos de caja confirmados (estado 2) que corresponden a conceptos de ingreso específicos (códigos 019 y 031). Cruza los recibos de caja y sus detalles con los anticipos de cartera, traslados y facturas activas asociadas, para determinar el valor recaudado y el tipo de paciente (régimen contributivo, subsidiado, vinculado, particular, desplazado, entre otros). Se utiliza para el seguimiento presupuestal de reconocimientos de ingresos en tesorería, permitiendo analizar lo efectivamente cobrado por tipo de paciente en un período determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Presupuesto_reconocimientos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Presupuesto_reconocimientos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los recibos de caja confirmados con conceptos de recaudo específicos en un rango de fechas, mostrando código, valor y tipo de paciente, para soporte de presupuesto de reconocimientos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Presupuesto_reconocimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere un rango de fechas (inicial y final) sobre la fecha del documento del recibo de caja.; Deben existir conceptos de recaudo con códigos ''019'' y ''031'' en el catálogo de conceptos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Presupuesto_reconocimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera recibos de caja con Status = 2 (recibos confirmados/aplicados).; Solo considera detalles cuyo concepto de recaudo tenga código ''019'' o ''031''.; Las facturas relacionadas se consideran únicamente si tienen status = 1.; El cruce con anticipos de cartera, transferencias y facturas se hace mediante LEFT JOIN, por lo que no es obligatorio que existan para incluir el recibo.; El valor se expone como numérico entero (sin decimales) por el convert a numeric(18,0).; Se eliminan duplicados con DISTINCT en el resultado final.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Presupuesto_reconocimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recibo de caja; Concepto de recaudo; Tipo de paciente (Contributivo, Subsidiado, Vinculado, Particular, Desplazado); Anticipo de cartera; Transferencia de cartera; Factura; Admisión; Presupuesto de reconocimientos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Presupuesto_reconocimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando Status del recibo = 2 y el concepto del detalle está en (''019'',''031'') y DocumentDate entre @FechaIni y @FechaFin, devuelve filas distintas con código del recibo, valor numérico y descripción del tipo de paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Presupuesto_reconocimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PatientType = 1 → Etiqueta como ''Contributivo''; si PatientType = 2 → Etiqueta como ''Subsidiado''; si PatientType = 3 → Etiqueta como ''Vinculado''; si PatientType = 4 → Etiqueta como ''Particular''; si PatientType = 5 → Etiqueta como ''Otro''; si PatientType = 6 → Etiqueta como ''Desplazado Reg. Contributivo''; si PatientType = 7 → Etiqueta como ''Desplazado Reg. Subsidiado''; si PatientType = 8 → Etiqueta como ''Desplazado No Asegurado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Presupuesto_reconocimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashReceipts; Treasury.CashReceiptDetails; Treasury.CashReceiptConcepts; Portfolio.PortfolioAdvance; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail; Billing.Invoice', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Presupuesto_reconocimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Presupuesto_reconocimientos';
-- GO
