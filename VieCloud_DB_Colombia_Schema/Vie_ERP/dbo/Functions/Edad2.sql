

CREATE FUNCTION [dbo].[Edad2] (@CadenaInicio as nvarchar(10), @CadenaFin as nvarchar(10))
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
--if isdate(@CadenaInicio)=0 return('La fecha de Nacimiento no es correcta')
--if isdate(@CadenaFin)=0 return ('La fecha del sistema no es correcta')
if datediff(dd, @CadenaInicio, @CadenaFin) = 0 return('0 años 0 meses 1 día')
--Asigna las cadenas a las fechas, inviertiéndolas si es necesario y cambiando el mensaje de salida.-
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
--A partir de qaquí ya tenemos los vlores del año, mes y día, lo siguiente es para presentarlo correctamente.-
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la edad o el tiempo transcurrido entre dos fechas, expresando el resultado en años, meses y días en formato legible para humanos (por ejemplo: ''35 años 4 meses 12 días''). Recibe como parámetros una fecha de inicio (generalmente fecha de nacimiento del paciente) y una fecha de fin (generalmente la fecha actual o fecha de atención). Se usa principalmente para determinar la edad del paciente en reportes clínicos, historias clínicas y procesos de admisión donde se requiere mostrar la edad exacta desagregada. Si las fechas están invertidas, ajusta automáticamente el cálculo sin generar error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Edad2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Edad2';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula y devuelve, en formato textual legible, la diferencia entre dos fechas expresada en años, meses y días (típicamente para mostrar la edad de una persona).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las dos cadenas de entrada deben ser convertibles a datetime (el código asume fechas válidas; las validaciones isdate están comentadas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre se expresa en años, meses y días, con singular/plural correcto según el valor.; Si ambas fechas son iguales en días, devuelve siempre ''0 años 0 meses 1 día'' (mínimo de 1 día).; El cálculo es independiente del orden cronológico de las fechas: invierte internamente si la primera es posterior a la segunda.; Componentes con valor 0 no se incluyen en el texto final (se omiten cadenas vacías).; Los meses nunca quedan negativos: se compensan restando un año.; Los días nunca quedan negativos: se compensan restando un mes y sumando los días del mes anterior.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'edad; fecha de nacimiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Cuando datediff(dd, inicio, fin)=0 retorna literal ''0 años 0 meses 1 día''; [RETURN_RESULT] (scalar return): En el resto de casos retorna una cadena concatenada con años/meses/días formateados según singular o plural', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Diferencia en días entre las dos fechas = 0 → Retorna inmediatamente ''0 años 0 meses 1 día''; si Fecha inicio posterior a fecha fin (datediff < 0) → Invierte las fechas para asegurar cálculo positivo else Usa el orden recibido; si Día de fin < día de inicio → Calcula días tomando los restantes del mes de inicio más los del mes fin, y resta 1 al mes fin else Días = diferencia directa de días; si Mes de fin < mes de inicio (tras ajuste por días) → Suma 12 a la diferencia de meses y resta 1 al año fin else Meses = diferencia directa; si Días=0 / =1 / >1 → Formatea como '''' / ''N día'' / ''N días''; si Meses=0 / =1 / >1 → Formatea como '''' / ''N mes'' / ''N meses''; si Años=0 / =1 / >1 → Formatea como '''' / ''N año'' / ''N años''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad2';
GO
