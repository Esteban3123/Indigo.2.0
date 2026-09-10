CREATE TABLE [dbo].[ODONTOGREL] (
    [ID]           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROLVAL] INT NOT NULL,
    [IDCONTROLTRA] INT NOT NULL,
    CONSTRAINT [PK_ODONTOGREL__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGREL_ODONTOGCTRLTRA] FOREIGN KEY ([IDCONTROLTRA]) REFERENCES [dbo].[ODONTOGCTRLTRA] ([ID]),
    CONSTRAINT [FK_ODONTOGREL_ODONTOGCTRLVAL] FOREIGN KEY ([IDCONTROLVAL]) REFERENCES [dbo].[ODONTOGCTRLVAL] ([ID])
);




GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre controles de valoración y controles de tratamiento en la historia clínica odontológica. Vincula los registros de evaluación diagnóstica con los procedimientos de tratamiento dental aplicados al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGREL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGREL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de relación odontológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGREL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGREL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al control de valoración odontológica, evaluación o diagnóstico dental asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGREL', @level2type = N'COLUMN', @level2name = N'IDCONTROLVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGREL', @level2type = N'COLUMN', @level2name = N'IDCONTROLVAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al control de tratamiento odontológico, procedimiento o intervención dental asociada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGREL', @level2type = N'COLUMN', @level2name = N'IDCONTROLTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGREL', @level2type = N'COLUMN', @level2name = N'IDCONTROLTRA';
