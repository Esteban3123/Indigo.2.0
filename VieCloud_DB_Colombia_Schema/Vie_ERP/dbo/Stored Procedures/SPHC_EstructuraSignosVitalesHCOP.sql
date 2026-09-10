
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el bloque de texto de signos vitales de las últimas 24 horas para la historia clínica de un paciente hospitalizado. Recibe como parámetros la cédula del paciente, el número de ingreso y el folio de la historia clínica, y llama a la función SignosVitalesPorFolioConcatenados para obtener los registros de signos vitales asociados a ese folio. Si existen datos, devuelve el texto formateado con el encabezado ''SIGNOS VITALES ÚLTIMAS 24 HORAS''; de lo contrario, devuelve un valor vacío. Se usa para construir la sección de signos vitales en la vista o impresión de la historia clínica de pacientes en hospitalización (HCOP).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye un bloque de texto con los signos vitales de las últimas 24 horas de un paciente para mostrarlo en la historia clínica de hospitalización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La función escalar SignosVitalesPorFolioConcatenados debe existir y aceptar paciente, ingreso, folio y un indicador (1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna NULL: si no hay signos vitales, retorna cadena vacía.; El encabezado ''SIGNOS VITALES ÚLTIMAS 24 HORAS:'' solo se incluye cuando existe contenido real de signos vitales.; Siempre invoca la función con el indicador fijo 1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; folio; signos vitales; historia clínica de hospitalización (HCOP)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Si SignosVitalesPorFolioConcatenados retorna valor no nulo y distinto de vacío, devuelve el texto precedido por ''SIGNOS VITALES ÚLTIMAS 24 HORAS:'' seguido de salto de línea y el contenido; en caso contrario devuelve cadena vacía.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @SignosVitalesPorFolio IS NOT NULL AND @SignosVitalesPorFolio <> '''' → Arma cabecera ''SIGNOS VITALES ÚLTIMAS 24 HORAS:'' concatenada con el detalle de signos vitales else Devuelve cadena vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SignosVitalesPorFolioConcatenados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_EstructuraSignosVitalesHCOP';
-- GO
