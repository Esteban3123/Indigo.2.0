



CREATE FUNCTION [dbo].[DuracionTotalRemision] (@Fecha1 as datetime, @Fecha2 as datetime,  @Fecha3 as datetime,  @Fecha4 as datetime, @Fecha5 as datetime, @FechaSuspension as datetime)
RETURNS varchar(max)
AS
BEGIN

declare @FechaFin datetime
declare @Tiempo varchar(max)
declare @r1 int
declare @r2 int
declare @r3 int


if @Fecha2 IS NULL
	begin 
		if @FechaSuspension IS NULL			
			set @FechaFin = GETDATE()			
		else			
			set @FechaFin = @FechaSuspension		
	end
else
	begin
		set @FechaFin = @Fecha2	
		if @Fecha3 IS NULL
			begin 
				if @FechaSuspension IS NULL			
					set @FechaFin = GETDATE()			
				else			
					set @FechaFin = @FechaSuspension	
			end
		else
			begin
				set @FechaFin = @Fecha3	
				if @Fecha4 IS NULL
					begin 
						if @FechaSuspension IS NULL			
							set @FechaFin = GETDATE()			
						else			
							set @FechaFin = @FechaSuspension
					end
				else
					begin
						set @FechaFin = @Fecha4	
						if @Fecha5 IS NULL
							begin 
								if @FechaSuspension IS NULL			
									set @FechaFin = GETDATE()			
								else			
									set @FechaFin = @FechaSuspension
							end
						else						
							set @FechaFin = @Fecha5								
					end
			end
	end

set @r1 = DateDiff(SECOND, @Fecha1, @FechaFin) / 86400
set @r2 = DateDiff(SECOND, @Fecha1, @FechaFin) / 3600
set @r3 = DateDiff(SECOND, @Fecha1, @FechaFin) / 60

set @Tiempo = Str(@r1 % 365) + ' días ' + Str( @r2 % 24) + ' horas ' + Str( @r3 % 60) + ' minutos '

RETURN @Tiempo

END