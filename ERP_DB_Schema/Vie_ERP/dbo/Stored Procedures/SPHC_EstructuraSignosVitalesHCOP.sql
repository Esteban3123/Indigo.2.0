
CREATE PROCEDURE [dbo].[SPHC_EstructuraSignosVitalesHCOP]
(
@Paciente Varchar(25),
@Ingreso Varchar(20),
@Folio Varchar(10)
)
AS
BEGIN
    DECLARE @SignosVitales as varchar(max)
    DECLARE @SignosVitalesPorFolio as varchar(max)

    -- Ejecutar las funciones escalares y almacenar los resultados en variables
    SET @SignosVitalesPorFolio = [dbo].SignosVitalesPorFolioConcatenados(@Paciente, @Ingreso, @Folio, 1)

    -- Verificar si ambas funciones retornan valores no nulos y no vacíos
    IF (@SignosVitalesPorFolio IS NOT NULL AND @SignosVitalesPorFolio <> '')
    BEGIN
        SET @SignosVitales = CONCAT('SIGNOS VITALES ÚLTIMAS 24 HORAS: ', CHAR(10), 
                                        @SignosVitalesPorFolio, CHAR(13))
    END
    ELSE
    BEGIN
        SET @SignosVitales = ''
    END

    SELECT @SignosVitales AS 'Signos vitales ult 24h'
END