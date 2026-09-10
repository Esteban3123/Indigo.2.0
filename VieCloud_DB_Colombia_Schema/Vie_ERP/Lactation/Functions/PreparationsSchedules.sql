CREATE   FUNCTION Lactation.PreparationsSchedules
(
    @CenterCode varchar(10)
)
RETURNS nvarchar(MAX)
AS
BEGIN
    DECLARE @result nvarchar(MAX);

    SELECT @result =
        STRING_AGG(
            CHAR(9) +  -- tabulación inicial
            CONCAT(N'Preparación ', B.PreparationNumber, N': ',
                   CONVERT(varchar(5), B.InitialPreparationTime, 108),
                   N' - ',
                   CONVERT(varchar(5), B.FinalPreparationTime, 108)
            ),
            CHAR(13) + CHAR(10)  -- salto de línea
        )
        WITHIN GROUP (ORDER BY B.PreparationNumber)
    FROM Lactation.ParametersPreparations B
    WHERE EXISTS (
        SELECT 1
        FROM Lactation.ParametersConfiguration A
        JOIN Lactation.ParametersCareCenters C
             ON A.Id = C.ParametersConfigurationId
        WHERE A.Id = B.ParametersConfigurationId
          AND C.CenterCode = @CenterCode
    );

    RETURN @result;
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, como texto formateado y ordenado, el listado de horarios de preparación de lactario configurados para un centro de atención específico.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'PreparationsSchedules';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una configuración de lactario vinculada al centro de atención indicado en Lactation.ParametersCareCenters.; Deben existir registros de preparaciones en Lactation.ParametersPreparations asociados a dicha configuración para obtener un resultado no nulo.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'PreparationsSchedules';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las preparaciones devueltas pertenecen exclusivamente a la configuración asociada al centro de atención solicitado.; El listado se entrega ordenado ascendentemente por número de preparación.; Cada línea representa una preparación con su rango horario en formato HH:MM - HH:MM, separadas por salto de línea CRLF y precedidas por tabulación.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'PreparationsSchedules';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Lactancia; Preparaciones de lactario; Horarios de preparación; Centros de atención; Configuración de lactario', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'PreparationsSchedules';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Lactation.ParametersPreparations: Cuando existe una configuración de lactario vinculada al centro (CenterCode) vía ParametersCareCenters, retorna las preparaciones concatenadas con tabulación, número y rango horario inicial-final ordenadas por PreparationNumber; si no hay coincidencias, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'PreparationsSchedules';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Lactation.ParametersPreparations; Lactation.ParametersConfiguration; Lactation.ParametersCareCenters', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'PreparationsSchedules';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'PreparationsSchedules';
GO
