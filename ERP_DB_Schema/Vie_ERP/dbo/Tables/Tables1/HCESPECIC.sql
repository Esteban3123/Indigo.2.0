CREATE TABLE [dbo].[HCESPECIC] (
    [CODCONCEC] INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODESPECI] CHAR (3) NOT NULL,
    [CODEXAME]  CHAR (3) NOT NULL,
    CONSTRAINT [PK_HCESPECIC_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCESPECIC_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[HCESPECIC] NOCHECK CONSTRAINT [FK_HCESPECIC_INESPECIA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Examen Fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECIC', @level2type = N'COLUMN', @level2name = N'CODEXAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECIC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecuti', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECIC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

