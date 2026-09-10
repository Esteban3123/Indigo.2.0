CREATE TABLE [dbo].[ADMPRUEC] (
    [ID]     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO] VARCHAR (3)  NOT NULL,
    [NOMBRE] VARCHAR (80) NOT NULL,
    [ESTADO] BIT          NOT NULL,
    CONSTRAINT [PK_ADMPRUEC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADMPRUEC_ADMPRUEC] FOREIGN KEY ([ID]) REFERENCES [dbo].[ADMPRUEC] ([ID])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de pruebas o exámenes clínicos disponibles en el sistema. Permite clasificar y referenciar los tipos de pruebas que se pueden solicitar o realizar en la admisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno autonumérico del registro de la prueba.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código corto que identifica la prueba o examen clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo de la prueba o examen clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la prueba está activa (1) o inactiva (0) en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADMPRUEC', @level2type = N'COLUMN', @level2name = N'ESTADO';
