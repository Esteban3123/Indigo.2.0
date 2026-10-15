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