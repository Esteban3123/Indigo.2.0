CREATE TABLE [dbo].[ADCONREFIPS] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCABECERA]    CHAR (100)    NOT NULL,
    [CODCENATE]     CHAR (10)     NOT NULL,
    [NOMBRE]        VARCHAR (100) NULL,
    [NUMTELE]       VARCHAR (25)  NULL,
    [EMAILCONTACTO] CHAR (100)    NULL,
    [OBSERVA]       VARCHAR (500) NULL,
    [CORRCONTRARE]  BIT           NULL,
    CONSTRAINT [PK_ADCONREFIPS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADCONREFIPS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADCONREFIPS_ADCONREFIPS] FOREIGN KEY ([IDCABECERA]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS])
);


GO
ALTER TABLE [dbo].[ADCONREFIPS] NOCHECK CONSTRAINT [FK_ADCONREFIPS_ADCENATEN];


GO
ALTER TABLE [dbo].[ADCONREFIPS] NOCHECK CONSTRAINT [FK_ADCONREFIPS_ADCONREFIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me identifica si el correo es de contrarreferencia:  true -> Si   false -> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'CORRCONTRARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'OBSERVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Email Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'EMAILCONTACTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número Telefono del contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'NUMTELE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera; campo que me tiene la relacion con la cabecera en este caso el campo  de la tabla [ADCONTIPS].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'IDCABECERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONREFIPS', @level2type = N'COLUMN', @level2name = N'ID';

