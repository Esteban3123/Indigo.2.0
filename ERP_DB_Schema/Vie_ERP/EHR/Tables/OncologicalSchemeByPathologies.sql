CREATE TABLE [EHR].[OncologicalSchemeByPathologies] (
    [Id]                  INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologicalSchemeId] INT      NOT NULL,
    [PathologyId]         CHAR (4) NOT NULL,
    CONSTRAINT [PK_OncologicalSchemeByPathologies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologicalSchemeByPathologies_INDIAGNOS] FOREIGN KEY ([PathologyId]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_OncologicalSchemeByPathologies_OncologicalSchemeByPathologies] FOREIGN KEY ([OncologicalSchemeId]) REFERENCES [EHR].[OncologicalSchemes] ([Id])
);


GO
ALTER TABLE [EHR].[OncologicalSchemeByPathologies] NOCHECK CONSTRAINT [FK_OncologicalSchemeByPathologies_INDIAGNOS];


GO
ALTER TABLE [EHR].[OncologicalSchemeByPathologies] NOCHECK CONSTRAINT [FK_OncologicalSchemeByPathologies_OncologicalSchemeByPathologies];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Id patologia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'PathologyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de esquema oncológico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'OncologicalSchemeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologicalSchemeByPathologies', @level2type = N'COLUMN', @level2name = N'Id';

