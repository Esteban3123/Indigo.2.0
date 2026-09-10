

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la edad o el tiempo transcurrido entre dos fechas, expresando el resultado en años, meses y días en formato de texto legible (por ejemplo: ''35 años 4 meses 12 días''). Recibe una fecha de inicio (generalmente la fecha de nacimiento del paciente) y una fecha de fin (generalmente la fecha actual o la fecha de atención). Se utiliza principalmente para mostrar la edad del paciente en historias clínicas, admisiones, órdenes médicas y reportes asistenciales. Maneja correctamente diferencias de días y meses, incluyendo el ajuste por meses incompletos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Edad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Edad';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la diferencia entre dos fechas y la devuelve formateada como texto en años, meses y días (típicamente para expresar la edad de una persona).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Ambas fechas de entrada deben ser interpretables como DATE válidas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El cálculo siempre se realiza sobre el intervalo absoluto: si las fechas vienen invertidas, se reordenan internamente; Nunca retorna valores negativos: el resultado siempre representa una duración positiva; El formato de salida concatena años, meses y días en ese orden, separados por espacios; Si ambas fechas coinciden en el día, se devuelve un mínimo de ''1 día'' en lugar de cero', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Edad; Fecha de nacimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Cuando la diferencia en días entre las dos fechas es 0, retorna literal ''0 años 0 meses 1 día''; [RETURN_RESULT] (retorno escalar): En cualquier otro caso, retorna una cadena compuesta con los años, meses y días calculados, omitiendo los componentes con valor 0 y usando singular/plural según corresponda (día/días, mes/meses, año/años)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si datediff(dd, FechaInicio, FechaFin) = 0 → Retorna inmediatamente ''0 años 0 meses 1 día'' sin calcular nada más; si datediff(dd, FechaInicio, FechaFin) > 0 → Usa las fechas tal cual fueron recibidas para el cálculo else Invierte el orden de las fechas para garantizar que el cálculo se haga siempre de la menor a la mayor; si DíaFin - DíaInicio < 0 → Calcula los días sumando los días restantes del mes de inicio más los días del mes fin, y decrementa en 1 el mes fin (préstamo de día); si MesFin - MesInicio < 0 → Suma 12 a la diferencia de meses y decrementa en 1 el año fin (préstamo de mes); si Componente (años/meses/días) = 0 → Omite ese componente del texto de salida; si Componente = 1 vs > 1 → Aplica forma singular (''día'',''mes'',''año'') o plural (''días'',''meses'',''años'') al texto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad';
GO
