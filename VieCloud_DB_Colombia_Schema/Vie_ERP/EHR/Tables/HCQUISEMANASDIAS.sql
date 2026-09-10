CREATE TABLE [EHR].[HCQUISEMANASDIAS] (
    [ID]             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCQUIMEDICAM] INT NOT NULL,
    [SEMANA]         INT NOT NULL,
    [DIA]            INT NOT NULL,
    CONSTRAINT [PK_HCQUIDIAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCQUIDIAS_HCQUIMEDICAM] FOREIGN KEY ([IDHCQUIMEDICAM]) REFERENCES [EHR].[HCQUIMEDICAM] ([ID]),
    CONSTRAINT [FK_HCQUISEMANASDIAS_HCQUISEMANASDIAS] FOREIGN KEY ([ID]) REFERENCES [EHR].[HCQUISEMANASDIAS] ([ID])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCQUISEMANASDIAS]
    ON [EHR].[HCQUISEMANASDIAS]([IDHCQUIMEDICAM] ASC, [SEMANA] ASC, [DIA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días específicos de cada semana en que se administra un medicamento dentro de un esquema de quimioterapia. Permite definir el cronograma semanal de aplicación de medicamentos oncológicos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de día de administración en el esquema de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al medicamento de quimioterapia asociado en la historia clínica, vincula este día con el fármaco del protocolo oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS', @level2type = N'COLUMN', @level2name = N'IDHCQUIMEDICAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS', @level2type = N'COLUMN', @level2name = N'IDHCQUIMEDICAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de semana del ciclo de quimioterapia en que se programa la administración del medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS', @level2type = N'COLUMN', @level2name = N'SEMANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS', @level2type = N'COLUMN', @level2name = N'SEMANA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del día dentro de la semana en que se aplica el medicamento quimioterapéutico, según el esquema del protocolo oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUISEMANASDIAS', @level2type = N'COLUMN', @level2name = N'DIA';
