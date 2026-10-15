
create function [dbo].[ObtenerFechaFormateada]
(
	@Fecha as DateTime
)
returns nvarchar(25)
as
BEGIN
	declare @dd nvarchar(2)
	declare @MM nvarchar(2)
	declare @yyyy nvarchar(4)
	declare @hh nvarchar(2)
	declare @mms nvarchar(2)


	set @yyyy = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 0 ,5)
	set @MM = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 6 ,8)
	set @dd = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 9 ,11)

	set @hh = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 12 ,14)
	set @mms = SUBSTRING(CONVERT(nvarchar, @Fecha , 120), 15 ,17)






	return  @dd + '/' + @MM + '/' + @yyyy + ' ' + @hh + ':' + @mms
END