CREATE TABLE [HumanTalent].[PVPositionsDocuments] (
    [Id]                   INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionsId]          INT          NOT NULL,
    [CheckListDocumentsId] INT          NOT NULL,
    [CreationUser]         VARCHAR (50) NOT NULL,
    [CreationDate]         DATETIME     NOT NULL,
    [ModificationUser]     VARCHAR (50) NULL,
    [ModificationDate]     DATETIME     NULL,
    CONSTRAINT [PK_PVPositionsDocuments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVPositionsDocuments_CheckListDocuments] FOREIGN KEY ([CheckListDocumentsId]) REFERENCES [HumanTalent].[CheckListDocuments] ([Id]),
    CONSTRAINT [FK_PVPositionsDocuments_Positions] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id])
);
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación de la relación posición-documento (DATETIME, nullable, auditoría de cambios)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'ModificationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modificó por última vez la relación (VARCHAR 50, nullable, auditoría de cambios)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'ModificationUser';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de asociación entre posición y documento requerido (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'CreationDate';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró o creó la relación entre posición y documento (VARCHAR 50, usuario del sistema, auditoría)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la posición laboral o cargo (FK a Positions). Referencia al puesto de trabajo, rol o plaza asociado al documento requerido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la posición ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'PositionsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'PositionsId';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de la relación entre posición y documento de checklist', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments', @level2type = N'COLUMN', @level2name = N'Id';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre cargos o posiciones de trabajo y los documentos requeridos en su lista de verificación (checklist). Registra qué documentos deben entregarse o cumplirse para cada posición del talento humano, incluyendo trazabilidad de creación y modificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPositionsDocuments';
