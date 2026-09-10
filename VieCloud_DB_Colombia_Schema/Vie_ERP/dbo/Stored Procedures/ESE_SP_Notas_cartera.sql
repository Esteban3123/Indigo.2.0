-- =============================================
-- Author:		<Author,,yeny nunez>
-- Create date: <Create Date, 12/06/2019,>
-- Description:	<Description, Registro de notas cartera>
-- =============================================
CREATE PROCEDURE [dbo].[ESE_SP_Notas_cartera]
@FechaIni DateTime,
@FechaFin DateTime
AS
BEGIN

select  nota.Code as 'Num-nota', cli.Nit, cli.Name as 'Nom-cliente',  nota.NoteDate as 'Fecha-nota',nota.Observations as 'Observacion'   ,
   CASE nota.Nature WHEN '1' THEN 'Debito' WHEN '2' THEN 'Credito' END 'Naturaleza',
   CASE nota.Status WHEN '1' THEN 'Registrado' WHEN '2' THEN 'Confirmado' WHEN '3' THEN 'Anulado' END 'Estado',
   fact.InvoiceNumber as 'factura', fact.AccountReceivableDate as 'fecha factura',  notade.AdjusmentValue as 'valor'
from Portfolio.PortfolioNote nota

inner join Common.Customer cli   on nota.CustomerId =cli.Id
inner join Portfolio.PortfolioNoteAccountReceivableAdvance notade on nota.id=notade.PortfolioNoteId
inner join Portfolio.AccountReceivable fact on notade.AccountReceivableId=fact.Id

WHERE	nota.NoteDate BETWEEN @FechaIni AND @FechaFin 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera un reporte de notas de cartera (débito o crédito) registradas en un rango de fechas. Para cada nota muestra el número de nota, el NIT y nombre del cliente o entidad pagadora (EPS, aseguradora, empresa), la fecha, observaciones, naturaleza (débito/crédito), estado (registrado, confirmado o anulado), la factura o cuenta de cobro asociada y el valor del ajuste o anticipo aplicado. Se usa para el seguimiento y control de gestiones de cobro, acuerdos de pago y ajustes sobre cuentas por cobrar en el módulo de cartera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Notas_cartera';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Notas_cartera';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las notas de cartera (débito/crédito) con sus ajustes aplicados a cuentas por cobrar y datos del cliente, dentro de un rango de fechas de la nota.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Notas_cartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un rango de fechas (inicio y fin) para filtrar las notas por NoteDate.; Las notas deben estar vinculadas a un cliente, a un anticipo de nota y a una cuenta por cobrar (joins internos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Notas_cartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen notas de cartera con al menos un ajuste/anticipo aplicado a una cuenta por cobrar existente (INNER JOIN).; La naturaleza de la nota se restringe a Débito (1) o Crédito (2); otros valores quedan como NULL en la salida.; El estado de la nota se interpreta solo para los códigos 1 (Registrado), 2 (Confirmado) y 3 (Anulado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Notas_cartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Notas de cartera; Notas débito/crédito; Cliente (NIT); Cuenta por cobrar; Factura; Anticipo aplicado a cuenta por cobrar; Estado de nota (Registrado/Confirmado/Anulado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Notas_cartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando nota.NoteDate está entre las fechas dadas y existen relaciones con cliente, anticipo y cuenta por cobrar, retorna las notas con número, NIT, cliente, fecha, observación, naturaleza, estado, factura y valor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Notas_cartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si nota.Nature = ''1'' → Se etiqueta la naturaleza como ''Debito'' else Si Nature = ''2'' se etiqueta como ''Credito''; si nota.Status = ''1'' → Se etiqueta el estado como ''Registrado'' else Si Status = ''2'' ''Confirmado''; si Status = ''3'' ''Anulado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Notas_cartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNote; Common.Customer; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.AccountReceivable', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Notas_cartera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Notas_cartera';
-- GO
