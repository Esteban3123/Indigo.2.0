CREATE TABLE [EHR].[DrugSheetByCycle] (
    [Id]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DrugSheetId] NUMERIC (18) NOT NULL,
    [CycleNumber] INT          NULL,
    [WeekNumber]  INT          NULL,
    CONSTRAINT [PK_DrugSheetByCycle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DrugSheetByCycle_HCHOJAMED] FOREIGN KEY ([DrugSheetId]) REFERENCES [dbo].[HCHOJAMED] ([CONSECUTI])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la relación entre una hoja de medicamentos (prescripción oncológica o de quimioterapia) y los ciclos y semanas de tratamiento asociados. Permite organizar la administración de medicamentos por ciclo y semana dentro de un esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de ciclo en la hoja de medicamentos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la hoja de medicamentos (prescripción) a la que pertenece este ciclo, identificador de la orden de medicación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle', @level2type = N'COLUMN', @level2name = N'DrugSheetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle', @level2type = N'COLUMN', @level2name = N'DrugSheetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ciclo de tratamiento al que corresponde la administración del medicamento (por ejemplo, ciclo 1, ciclo 2).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle', @level2type = N'COLUMN', @level2name = N'CycleNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle', @level2type = N'COLUMN', @level2name = N'CycleNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la semana dentro del ciclo de tratamiento en que se debe administrar el medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle', @level2type = N'COLUMN', @level2name = N'WeekNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'DrugSheetByCycle', @level2type = N'COLUMN', @level2name = N'WeekNumber';
