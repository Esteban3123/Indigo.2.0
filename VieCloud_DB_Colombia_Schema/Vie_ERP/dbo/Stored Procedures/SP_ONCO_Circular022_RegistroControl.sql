CREATE PROCEDURE dbo.SP_ONCO_Circular022_RegistroControl
(
    @FechaInicial   DATE,
    @FechaFinal     DATE,
    @IDEntidadVIE   VARCHAR(20),
    @Empresa        VARCHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        TipoRegistro               = 1,
        TipoIDEntidadReportadora   = 'NI',
        NumeroIDEntidadReportadora = (SELECT INDNITEMP  FROM INEMPRESU  WHERE INDCODEMP = @Empresa),
        FechaInicialPeriodo        = CONVERT(CHAR(10), @FechaInicial, 23),
        FechaFinalPeriodo          = CONVERT(CHAR(10), @FechaFinal, 23),
        TotalRegistros = (
            SELECT COUNT(*)
            FROM HCONCOPREG
            WHERE IDEntidadVIE   = @IDEntidadVIE
              AND FECHACREACION >= @FechaInicial
              AND FECHACREACION <  DATEADD(DAY,1,@FechaFinal)
        );
END;

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el registro de control (cabecera) del reporte de Circular 022 de oncología, devolviendo identificación de la entidad reportadora, periodo y total de registros del periodo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_RegistroControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La empresa indicada debe existir en INEMPRESU para obtener un NIT no nulo.; El rango de fechas debe ser coherente (FechaInicial <= FechaFinal) para que el conteo sea válido.; Debe existir la entidad VIE indicada para que el conteo sea representativo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_RegistroControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El TipoRegistro siempre se reporta con valor fijo 1 (registro de control de cabecera).; El TipoIDEntidadReportadora siempre se reporta como ''NI'' (NIT).; Las fechas del periodo se entregan en formato ISO ''YYYY-MM-DD'' (estilo 23).; El conteo del periodo es inclusivo en ambos extremos: usa >= FechaInicial y < FechaFinal+1 día para abarcar todo el día final.; Solo se cuentan registros pertenecientes a la entidad VIE indicada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_RegistroControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Entidad reportadora; NIT empresa; Periodo de reporte; Registro de control Circular 022; Oncología', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_RegistroControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve siempre una fila con TipoRegistro=1, TipoIDEntidadReportadora=''NI'', NIT obtenido de INEMPRESU para la empresa, fechas del periodo formateadas y total de registros de HCONCOPREG filtrados por IDEntidadVIE y FECHACREACION dentro del rango [@FechaInicial, @FechaFinal].', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_RegistroControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INEMPRESU; dbo.HCONCOPREG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_RegistroControl';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_Circular022_RegistroControl';
-- GO
