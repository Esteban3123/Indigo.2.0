

CREATE view [ViewInternal].[VistaRadicados]
as (
select 
rc.RadicatedConsecutive as NumeroRadicado
,c.Nit as NitCliente
,c.Name as Cliente
,rc.RadicatedDate as FechaRadicado
,rc.DocumentDate as FechaDocumento
,case rc.State when 1 then 'Registrado' when 2 then 'Confirmado' end as Estado
,rc.RadicatedUser as UsuarioRadico
,rd.InvoiceNumber as Factura
,rd.InvoiceValueEntity as ValorEntidad
,rd.InvoiceValuePacient as ValorPaciente
,rd.IngressNumber as Ingreso
,rd.PatientCode as IdentificacionPaciente
,rd.PatientName as NombrePaciente
,rd.InvoiceDate as FechaFactura
from Portfolio.RadicateInvoiceC rc
inner join Portfolio.RadicateInvoiceD rd on rc.Id = rd.RadicateInvoiceCId
inner join Common.Customer c on c.Id = rc.CustomerId
where rd.State <> 4
)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de aplanado orientada a reporting y consulta operativa del proceso de radicación de cartera. Consolida el encabezado de cada radicado (consecutivo, fecha, estado y usuario) con el cliente pagador (NIT y nombre) y el detalle de cada factura presentada al cobro (número, valores entidad/paciente, ingreso y datos del paciente). Excluye los registros de detalle con estado 4, que corresponden a facturas anuladas o descartadas del trámite.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de facturas radicadas ante entidades pagadoras con sus datos de cliente, paciente, valores e identificación del radicado, excluyendo detalles anulados.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe relación entre la cabecera de radicado y su detalle (RadicateInvoiceCId); El radicado está asociado a un cliente válido en Common.Customer', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles de radicado cuyo State sea distinto de 4; Únicamente los estados 1 y 2 de la cabecera se traducen a etiqueta legible; cualquier otro queda en NULL; Cada fila resultante corresponde a una factura del detalle y hereda los datos del radicado y cliente', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Radicado de facturas; Cliente/Entidad pagadora; Factura; Valor entidad; Valor paciente; Ingreso del paciente; Identificación del paciente', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.RadicateInvoiceC: Devuelve cabecera de radicado unida con detalle y cliente, omitiendo detalles con State=4 (anulados/excluidos)', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rc.State = 1 → Se reporta el estado como ''Registrado'' else Si rc.State = 2 se reporta como ''Confirmado''; otros valores se muestran como NULL; si rd.State = 4 → Se excluye el detalle del resultado else Se incluye el detalle en el listado', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Common.Customer', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaRadicados';
GO
