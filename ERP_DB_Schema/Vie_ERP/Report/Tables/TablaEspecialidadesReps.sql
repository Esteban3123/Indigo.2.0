CREATE TABLE [Report].[TablaEspecialidadesReps] (
    [CodigoEspecialidad]      CHAR (5)   NOT NULL,
    [DescripcionEspecialidad] NCHAR (50) NULL,
    [CodigoReps]              CHAR (5)   NULL,
    CONSTRAINT [PK_TablaEspecialidadesReps] PRIMARY KEY CLUSTERED ([CodigoEspecialidad] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código REPS', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaEspecialidadesReps', @level2type = N'COLUMN', @level2name = N'CodigoReps';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la especialidad', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaEspecialidadesReps', @level2type = N'COLUMN', @level2name = N'DescripcionEspecialidad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaEspecialidadesReps', @level2type = N'COLUMN', @level2name = N'CodigoEspecialidad';

