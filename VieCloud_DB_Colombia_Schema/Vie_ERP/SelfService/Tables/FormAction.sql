CREATE TABLE [SelfService].[FormAction] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [FormId]   INT NOT NULL,
    [ActionId] INT NOT NULL,
    CONSTRAINT [PK_FormAction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FormAction_Action] FOREIGN KEY ([ActionId]) REFERENCES [SelfService].[Action] ([Id]),
    CONSTRAINT [FK_FormAction_Form] FOREIGN KEY ([FormId]) REFERENCES [SelfService].[Form] ([Id])
);




GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los formularios del autoservicio con las acciones permitidas en cada uno, definiendo qué operaciones puede realizar el usuario en cada formulario.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'FormAction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'FormAction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de relación formulario-acción.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'FormAction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'FormAction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al formulario del autoservicio al que se le asigna la acción.', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'FormAction', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'FormAction', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la acción u operación habilitada para ese formulario (por ejemplo: guardar, enviar, aprobar).', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'FormAction', @level2type = N'COLUMN', @level2name = N'ActionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'SelfService', @level1type = N'TABLE', @level1name = N'FormAction', @level2type = N'COLUMN', @level2name = N'ActionId';
