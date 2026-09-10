CREATE TABLE [dbo].[HCPCECLAS] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [IDHCPCECONTROL] INT NOT NULL,
    [IDPLANVAL]      INT NOT NULL,
    [IDHCCLASENF]    INT NOT NULL,
    CONSTRAINT [PK_HCPCECLAS] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de clasificación de enfermería (FK a HCCLASENF); referencia cruzada que vincula la clase o categoría de atención de enfermería al control del plan de cuidados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HCCLASENF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'IDHCCLASENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del plan de valoración; clave foránea que referencia el plan de evaluación clínica, diagnóstico o de seguimiento del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del plan de valoración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'IDPLANVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control del plan de enfermería (FK a HCPCECONTROL); referencia al registro específico de monitoreo, seguimiento o punto de control dentro del plan de cuidados de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el id del control del plan de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'IDHCPCECONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial (INT IDENTITY); clave primaria autoincremental que identifica cada clasificación asociada a un control y plan de valoración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los controles de enfermería (historia clínica) con los planes de valoración y las clasificaciones de enfermería aplicadas durante la atención. Permite asociar cada registro de control de enfermería con el plan de valoración utilizado y la clase o categoría de enfermería correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPCECLAS';
