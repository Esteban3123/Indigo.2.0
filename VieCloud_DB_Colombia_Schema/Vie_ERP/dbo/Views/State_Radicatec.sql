

create VIEW [dbo].[State_Radicatec]
AS
SELECT DISTINCT 
                         RC.RadicatedConsecutive AS Consecutivo, C.Nit, C.Name AS Entidad, DET.ContractCode AS Contrato, 
                         RC.DocumentDate AS FechaDocumento, RC.ConfirmDate AS FechaConfirmacion, 
                         CASE WHEN RC.State = '1' THEN 'Sin Confirmar' WHEN RC.State = '2' THEN 'Confirmado' WHEN RC.State = '4' THEN 'Anulado' END AS Estado, 
                         CAST(DET.Total AS money) AS TotalRadicado
FROM            Portfolio.RadicateInvoiceC AS RC INNER JOIN
                         Common.Customer AS C ON C.Id = RC.CustomerId INNER JOIN
                             (SELECT        RadicateInvoiceCId, ContractCode, SUM(BalanceInvoice) AS Total
                               FROM            Portfolio.RadicateInvoiceD
                               GROUP BY RadicateInvoiceCId, ContractCode) AS DET ON RC.Id = DET.RadicateInvoiceCId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de los radicados de cobro (RadicateInvoiceC) consolidados por entidad pagadora y contrato. Muestra para cada radicado el consecutivo, NIT y nombre de la EPS o aseguradora, el código de contrato, las fechas de documento y confirmación, el estado del trámite (Sin Confirmar, Confirmado o Anulado) y el valor total radicado calculado como suma del saldo de las facturas del detalle (RadicateInvoiceD). Se usa para monitorear el avance de la facturación de cartera: cuántos radicados están pendientes de confirmar, cuáles fueron aprobados y cuáles fueron anulados por entidad y contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'State_Radicatec';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'State_Radicatec';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el estado consolidado de cada radicado de facturas de cartera, mostrando entidad pagadora, contrato, fechas, estado legible y total radicado por contrato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'State_Radicatec';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada radicado debe tener un cliente válido en Common.Customer (INNER JOIN por CustomerId); Deben existir detalles en Portfolio.RadicateInvoiceD para que el radicado aparezca (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'State_Radicatec';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El total radicado se calcula como la suma de BalanceInvoice agrupada por radicado y contrato; Solo se exponen radicados que tengan al menos un detalle asociado; El total se expresa siempre en tipo money; Los estados reconocidos son únicamente 1, 2 y 4; el estado ''3'' u otros no tienen etiqueta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'State_Radicatec';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicación de facturas; Cartera; Entidad pagadora (NIT); Contrato; Estado de radicado (Sin Confirmar/Confirmado/Anulado); Total radicado; Saldo de factura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'State_Radicatec';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.State_Radicatec: Devuelve una fila por combinación radicado+contrato sumando BalanceInvoice del detalle agrupado por RadicateInvoiceCId y ContractCode; [RETURN_RESULT] dbo.State_Radicatec: Traduce el código de estado: ''1'' → ''Sin Confirmar'', ''2'' → ''Confirmado'', ''4'' → ''Anulado''; otros valores quedan en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'State_Radicatec';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RC.State = ''1'' → Estado se reporta como ''Sin Confirmar''; si RC.State = ''2'' → Estado se reporta como ''Confirmado''; si RC.State = ''4'' → Estado se reporta como ''Anulado'' else Estado queda NULL para cualquier otro valor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'State_Radicatec';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Common.Customer; Portfolio.RadicateInvoiceD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'State_Radicatec';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'State_Radicatec';
GO
