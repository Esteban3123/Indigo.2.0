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