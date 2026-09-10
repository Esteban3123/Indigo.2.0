

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la duración total de una remisión médica expresada en días, horas y minutos. Recibe hasta cinco fechas de hito (inicio, cambios o renovaciones de la remisión) y una fecha de suspensión; determina la fecha de fin más reciente disponible —o usa la fecha actual si la remisión sigue vigente— y resta la fecha de inicio para obtener el tiempo transcurrido. Se usa para medir cuánto tiempo lleva activa o duró una remisión, ya sea en curso o finalizada por suspensión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DuracionTotalRemision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DuracionTotalRemision';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la duración total transcurrida de una remisión expresada en días, horas y minutos, tomando como fin la última fecha de avance disponible, la fecha de suspensión o la fecha actual si sigue vigente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DuracionTotalRemision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe suministrarse una fecha de inicio válida para poder calcular la diferencia de tiempo.; Las fechas de etapas posteriores deben respetar un orden cronológico esperado para que el cálculo sea coherente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DuracionTotalRemision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha fin se determina siempre como la última etapa registrada de forma secuencial (de la 2 a la 5).; Si una etapa intermedia no existe, no se consideran las posteriores aunque estén informadas.; Cuando no hay etapa posterior registrada, prevalece la fecha de suspensión sobre la fecha actual.; Si la remisión no tiene suspensión ni etapa final, se considera vigente y se mide hasta el momento actual (GETDATE()).; La duración se expresa siempre en formato ''días horas minutos'' usando módulos 365/24/60.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DuracionTotalRemision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Remisión; Suspensión de remisión; Duración/tiempo activo de remisión; Etapas/avances de la remisión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DuracionTotalRemision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna una cadena con el formato ''X días Y horas Z minutos'' calculada como la diferencia en segundos entre la fecha inicial y la fecha final determinada, descompuesta en módulos 365, 24 y 60.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DuracionTotalRemision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si La fecha de la segunda etapa es NULL → Toma como fin la fecha de suspensión si existe; en caso contrario usa GETDATE() else Avanza a evaluar la tercera etapa; si La fecha de la tercera etapa es NULL (con segunda etapa presente) → Toma como fin la fecha de suspensión si existe; en caso contrario usa GETDATE() else Avanza a evaluar la cuarta etapa; si La fecha de la cuarta etapa es NULL (con tercera etapa presente) → Toma como fin la fecha de suspensión si existe; en caso contrario usa GETDATE() else Avanza a evaluar la quinta etapa; si La fecha de la quinta etapa es NULL (con cuarta etapa presente) → Toma como fin la fecha de suspensión si existe; en caso contrario usa GETDATE() else Toma como fin la fecha de la quinta etapa (etapa final)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DuracionTotalRemision';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DuracionTotalRemision';
GO
