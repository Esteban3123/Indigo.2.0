CREATE FUNCTION [dbo].[fnCalculateDateFrecuencyCurrentPeriod] 
(
    @Year int,
    @Date datetime,
    @UnitFrequency int
)
RETURNS datetime
AS
BEGIN
    declare @month as int
    declare @day as int

    if @UnitFrequency = 5 begin
        return DATEFROMPARTS(@Year, 12, 30)
    end

    if @UnitFrequency = 6 begin
        if CEILING(MONTH(@Date)/6) = 0  begin
            return DATEFROMPARTS(@Year, 6, 30)
        end
        return DATEFROMPARTS(@Year, 12, 30)
    end

    if @UnitFrequency = 7 begin
        declare @trimestre as int = CEILING(MONTH(@Date)/4) + 1
            return DATEFROMPARTS(@Year, @trimestre * 3, 30)
    end

    return eomonth(@Date)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la fecha de fin del período vigente según la frecuencia de pago o liquidación configurada para un contrato o servicio. Recibe un año, una fecha de referencia y una unidad de frecuencia: si la frecuencia es anual (5), devuelve el 30 de diciembre del año indicado; si es semestral (6), devuelve el 30 de junio o 30 de diciembre según el semestre correspondiente; si es trimestral (7), devuelve el último día del trimestre activo; para cualquier otra frecuencia (mensual u otras), devuelve el último día del mes de la fecha dada. Se utiliza principalmente en procesos de facturación, glosas, contratos o liquidaciones periódicas donde es necesario determinar la fecha de corte del período en curso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnCalculateDateFrecuencyCurrentPeriod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnCalculateDateFrecuencyCurrentPeriod';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la fecha de corte (último día) del período vigente según la frecuencia indicada (anual, semestral, trimestral o mensual) para una fecha y año dados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalculateDateFrecuencyCurrentPeriod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El año recibido debe ser válido para DATEFROMPARTS (1-9999).; La fecha recibida debe ser una fecha válida para extraer mes con MONTH() y EOMONTH().', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalculateDateFrecuencyCurrentPeriod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha retornada siempre corresponde al año recibido cuando la frecuencia es 5, 6 o 7.; Para frecuencias 5, 6 y 7 siempre retorna día 30 (no usa último día real del mes).; Para cualquier frecuencia no codificada explícitamente se aplica corte mensual (EOMONTH).; Los códigos de frecuencia reconocidos son: 5=anual, 6=semestral, 7=trimestral.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalculateDateFrecuencyCurrentPeriod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'frecuencia de período; fecha de corte; período vigente; semestre; trimestre; facturación periódica; liquidación; contratos; glosas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalculateDateFrecuencyCurrentPeriod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando la frecuencia = 5 (anual), retorna 30 de diciembre del año indicado.; [RETURN_RESULT] N/A: Cuando la frecuencia = 6 (semestral) y CEILING(MONTH/6)=0, retorna 30 de junio del año; en caso contrario retorna 30 de diciembre del año.; [RETURN_RESULT] N/A: Cuando la frecuencia = 7 (trimestral), retorna el día 30 del mes calculado como (CEILING(MONTH/4)+1)*3 del año indicado.; [RETURN_RESULT] N/A: Cuando la frecuencia no es 5, 6 ni 7, retorna EOMONTH de la fecha recibida (último día del mes).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalculateDateFrecuencyCurrentPeriod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UnitFrequency = 5 → Retorna 30/12 del año indicado (corte anual).; si UnitFrequency = 6 → Determina el semestre: si CEILING(MONTH/6)=0 retorna 30/06; sino retorna 30/12 del año.; si UnitFrequency = 7 → Calcula trimestre con CEILING(MONTH/4)+1 y retorna día 30 del mes (trimestre*3).; si UnitFrequency distinto de 5, 6 y 7 → Retorna EOMONTH(fecha) como corte mensual por defecto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalculateDateFrecuencyCurrentPeriod';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnCalculateDateFrecuencyCurrentPeriod';
GO
