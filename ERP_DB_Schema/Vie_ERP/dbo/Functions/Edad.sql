



CREATE FUNCTION [dbo].[Edad] (@CadenaInicio as DATE, @CadenaFin as DATE)
RETURNS nvarchar (100)
AS
BEGIN

declare @AñoInicio int
declare @MesInicio int
declare @DiaInicio int
declare @AñoFin int
declare @MesFin int
declare @DiaFin int
declare @Años int
declare @Meses int
declare @Dias int
declare @FechaInicio datetime
declare @FechaFin datetime
declare @Texto nvarchar(100)
--Para comprobar las fechas
--if isdate(convert(varchar(20),@CadenaInicio))=0 return('La fecha de Nacimiento no es correcta')
--if isdate(convert(varchar(20),@CadenaFin))=0 return ('La fecha del sistema no es correcta')
--if datediff(dd, @CadenaInicio, @CadenaFin) = 0 return('0 años 0 meses 1 día')
--Asigna las cadenas a las fechas, inviertiéndolas si es necesario y cambiando el mensaje de salida.-
if datediff(dd, @CadenaInicio, @CadenaFin) = 0 return('0 años 0 meses 1 día')
if datediff(dd, @CadenaInicio, @CadenaFin) > 0
begin
set @FechaInicio = @CadenaInicio
set @FechaFin = @CadenaFin
set @Texto = ''
end
else
begin
set @FechaInicio = @CadenaFin
set @FechaFin = @CadenaInicio
set @Texto = ''
end
--Asigna los valores individuales de día, mes y año, para hacer los cálculos.-
set @DiaInicio = day(@FechaInicio)
set @MesInicio = month(@FechaInicio)
set @AñoInicio = year(@FechaInicio)

set @DiaFin = day(@FechaFin)
set @MesFin = month(@FechaFin)
set @AñoFin = year(@FechaFin)

--Comprueba si el día es menor o igual al de fin.-
if @DiaFin - @DiaInicio >= 0
begin
set @Dias = @DiaFin - @DiaInicio
end
--Si no, calcula la suma en días, desde el día de Inicio a fin de mes, mas los días de Fin, y le resta uno al mes de Fin.-
else
begin
set @Dias = (day(dateadd(mm,1,cast(('01/' + str(@MesInicio) + '/' + str(@AñoInicio)) as datetime)) - 1 )- @DiaInicio) + @DiaFin
set @MesFin = @MesFin - 1
end
--Lo mismo con el mes.-
if @MesFin - @MesInicio >= 0
begin
set @Meses = @MesFin - @MesInicio

end
else

begin
set @Meses = (@MesFin - @MesInicio) + 12
set @AñoFin = @AñoFin - 1
end

set @Años = @AñoFin - @AñoInicio
--A partir de aquí ya tenemos los valores del año, mes y día, lo siguiente es para presentarlo correctamente.-
declare @CadDia as varchar(20)
declare @CadMes as varchar(20)
declare @CadAño as varchar(20)

if @Dias = 0 set @CadDia = ''
if @Dias = 1 set @CadDia = ltrim(str(@Dias)) + ' día '
if @Dias > 1 set @CadDia = ltrim(str(@Dias)) + ' días '

if @Meses = 0 set @CadMes = ''
if @Meses = 1 set @CadMes = ltrim(str(@Meses)) + ' mes'
if @Meses > 1 set @CadMes = ltrim(str(@Meses)) + ' meses'

if @Años = 0
begin
set @CadAño = ''
if @Meses > 1 set @Texto = @Texto + ''
if @Meses = 0 and @Dias >1 set @Texto = @Texto + ''
end
if @Años = 1 set @CadAño = ltrim(str(@Años)) + ' año'
if @Años > 1
begin
set @CadAño = ltrim(str(@Años)) + ' años'
set @Texto = @Texto + ''
end

if @Meses <> 0
begin
if @Dias <> 0
begin
set @CadMes = @CadMes + '  '
if @Años <> 0 set @CadAño = @CadAño + ' '
end
else
if @Años <> 0 set @CadAño = @CadAño + '  '
end
else
if @Años <> 0 and @Dias <> 0 set @CadMes = @CadMes + '  '
--end

Set @Texto = @Texto + ' ' + @CadAño + @CadMes + @CadDia
RETURN @Texto
END