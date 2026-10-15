CREATE TABLE [dbo].[HCUNESPED] (
    [CODCONCEC]   INT           NOT NULL,
    [CODCAMPO]    CHAR (3)      NOT NULL,
    [DESEXAME]    VARCHAR (250) NOT NULL,
    [OBLIGATORIO] BIT           NOT NULL,
    CONSTRAINT [PK_HCUNESPED] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODCAMPO] ASC),
    CONSTRAINT [FK_HCUNESPED_HCUNESPEC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCUNESPEC] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[HCUNESPED] NOCHECK CONSTRAINT [FK_HCUNESPED_HCUNESPEC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica Si el campo es obligatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del examen fisico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'DESEXAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del campo del Examen Fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'CODCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCUNESPED', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

