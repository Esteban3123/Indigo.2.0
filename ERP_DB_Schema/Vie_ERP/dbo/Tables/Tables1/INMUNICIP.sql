CREATE TABLE [dbo].[INMUNICIP] (
    [DEPMUNCOD] CHAR (5)     NOT NULL,
    [DEPCODIGO] CHAR (2)     NOT NULL,
    [MUNCODIGO] CHAR (3)     NOT NULL,
    [MUNNOMBRE] CHAR (40)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INMunicip] PRIMARY KEY CLUSTERED ([DEPMUNCOD] ASC),
    CONSTRAINT [FK_INMunicip_INDepatar] FOREIGN KEY ([DEPCODIGO]) REFERENCES [dbo].[INDEPARTA] ([depcodigo])
);


GO
ALTER TABLE [dbo].[INMUNICIP] NOCHECK CONSTRAINT [FK_INMunicip_INDepatar];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'MUNNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'MUNCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'DEPCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Municipio y Departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INMUNICIP', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';

