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