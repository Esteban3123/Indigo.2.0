CREATE TABLE [dbo].[Contratos] (
    [Cedula]           VARCHAR (50) NULL,
    [CodigoGrupo]      INT          NULL,
    [SalarioBasico]    NUMERIC (18) NULL,
    [FechaFinContrato] DATE         NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Almacena información contractual de personas identificadas por cédula, asociadas a un grupo y con un salario básico y fecha de finalización de contrato. Puede relacionarse con módulos de nómina o gestión de recursos humanos dentro de un sistema de salud, registrando las condiciones salariales y vigencia del vínculo laboral.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Contratos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Contratos';
GO
