CREATE TABLE [dbo].[HCUNESPEC] (
    [CODCONCEC]  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UFUCODIGO]  CHAR (10) NOT NULL,
    [CODEXAME]   CHAR (3)  NOT NULL,
    [TIPHISTORI] CHAR (1)  NOT NULL,
    CONSTRAINT [PK_HCUNIESPEC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCUNESPEC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCUNESPEC] NOCHECK CONSTRAINT [FK_HCUNESPEC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de la historia clinica parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'TIPHISTORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Examen Fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'CODEXAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del consecutivo de la tabla de examen fisico por unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPEC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

