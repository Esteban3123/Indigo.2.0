CREATE FUNCTION [dbo].[Edad1DiaNeonato](@Paciente AS varchar(25), @FechaRegistro AS datetime)
RETURNS int
AS
BEGIN

    Declare @fecha datetime = NULL;
    Declare @Edad float = 2;

    -- Primero intentamos encontrar en HCRECINAC
    select @fecha = FECHANACIM from HCRECINAC where IPCODPACIHIJO = @Paciente;
    if @fecha IS NOT NULL
    begin
        set @Edad = (datediff(minute,@fecha,@FechaRegistro) / 1440) + 1
    end
    else
    begin
        -- Si no encontró en HCRECINAC, buscamos en INPACIENT
        select @fecha = IPFECNACI from INPACIENT  where IPCODPACI = @Paciente;

        if @fecha IS NOT NULL
        begin
            set @Edad = datediff(day,@fecha,@FechaRegistro);
        end
    end
    RETURN @Edad
 END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la edad en días de un paciente neonato a partir de su fecha de nacimiento y una fecha de registro dada. Primero busca la fecha de nacimiento real del recién nacido en el registro de nacimientos (HCRECINAC) usando el código del paciente hijo; si no la encuentra allí, la obtiene del maestro general de pacientes (INPACIENT). El cálculo para recién nacidos registrados en HCRECINAC se hace en minutos convertidos a días más uno (para considerar el primer día de vida), mientras que para los demás pacientes se calcula la diferencia directa en días. Se utiliza principalmente en contextos clínicos de neonatología para determinar la edad exacta en días de un recién nacido al momento de un evento o atención médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Edad1DiaNeonato';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Edad1DiaNeonato';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la edad en días de un paciente neonato a una fecha dada, priorizando la fecha de nacimiento del registro de recién nacido sobre la del maestro de pacientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad1DiaNeonato';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en HCRECINAC (como hijo) o en INPACIENT con fecha de nacimiento informada para obtener una edad real; en caso contrario retorna el valor por defecto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad1DiaNeonato';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'HCRECINAC tiene prioridad sobre INPACIENT como fuente de la fecha de nacimiento.; Para neonatos identificados en HCRECINAC, la edad mínima retornada es 1 (por el +1 al final del cálculo).; Cuando no hay datos de nacimiento, la función nunca falla: devuelve 2 como valor por defecto.; El cálculo neonatal usa granularidad de minutos para precisión, mientras que el general usa solo días.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad1DiaNeonato';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; recién nacido / neonato; fecha de nacimiento; edad en días; primer día de vida; hijo (paciente hijo en registro de recién nacido)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad1DiaNeonato';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Si HCRECINAC.FECHANACIM existe para el paciente: retorna (datediff(minute, fechaNac, fechaRegistro) / 1440) + 1, equivalente a días transcurridos más 1 para contar el primer día de vida.; [RETURN_RESULT] : Si no hay registro en HCRECINAC pero sí en INPACIENT con IPFECNACI: retorna datediff(day, fechaNac, fechaRegistro).; [RETURN_RESULT] : Si no se encuentra fecha de nacimiento en ninguna de las dos tablas: retorna el valor por defecto 2.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad1DiaNeonato';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe fecha de nacimiento en HCRECINAC para el paciente → Calcula edad en minutos/1440 + 1 (precisión de minutos, suma 1 día para incluir primer día de vida) else Busca en INPACIENT y, si existe, calcula diferencia directa en días', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad1DiaNeonato';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRECINAC; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad1DiaNeonato';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Edad1DiaNeonato';
GO
