CREATE TABLE [Contract].[TechnicalNoteDetail] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TechnicalNoteId] INT NOT NULL,
    [GrouperId]       INT NOT NULL,
    CONSTRAINT [PK_TechnicalNoteDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechnicalNoteDetail_Groupers] FOREIGN KEY ([GrouperId]) REFERENCES [Contract].[Groupers] ([Id]),
    CONSTRAINT [FK_TechnicalNoteDetail_TechnicalNote] FOREIGN KEY ([TechnicalNoteId]) REFERENCES [Contract].[TechnicalNote] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TechnicalNoteDetail]
    ON [Contract].[TechnicalNoteDetail]([TechnicalNoteId] ASC, [GrouperId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Agrupador (FK a Contract.Groupers). Referencia al sistema de agrupación de servicios, procedimientos o diagnósticos para clasificación clínica y facturación. INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'GrouperId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Agrupador', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'GrouperId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'GrouperId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la Nota Técnica (FK a Contract.TechnicalNote). Vincula el detalle a la nota técnica padre que documenta hallazgos clínicos, justificaciones o comentarios profesionales. INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'TechnicalNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la nota tecnica', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'TechnicalNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'TechnicalNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro en TechnicalNoteDetail. Clave primaria clustered (IDENTITY 1,1) que identifica cada línea de detalle en la nota técnica. INT.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las notas técnicas de contratos: registra los agrupadores (grupos de servicios o tarifas) asociados a cada nota técnica, permitiendo definir qué conceptos o grupos cubre cada nota técnica dentro de un contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'TechnicalNoteDetail';
