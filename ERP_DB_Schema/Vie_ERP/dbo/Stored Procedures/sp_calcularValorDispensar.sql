-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[sp_calcularValorDispensar]
(
@codigoProducto as varchar(20)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	declare @cantidadSolicitada as int = 10150
	declare @cantidadmaximausar as int

	declare @tablevalores as table(codigo varchar(20),cantidadDisponible int, cantidadusar int, presentacion int, totalusar int)
	insert into @tablevalores
	select top 1 CODIGO,CANTIDAD,cantidadusar,PRESENTACION,Totalusar  from (
		select *,@cantidadSolicitada / PRESENTACION as cantidadusar, 
		(@cantidadSolicitada / PRESENTACION) * PRESENTACION as Totalusar
		from Table_Inventario where CANTIDAD > 0 
	) as tmp where Totalusar <=@cantidadSolicitada  order by cantidadusar asc

	declare @totalusado int = (select SUM(totalusar ) from @tablevalores )

	declare @saldopendiente as int = @cantidadSolicitada - @totalusado

	select @saldopendiente 

	

END