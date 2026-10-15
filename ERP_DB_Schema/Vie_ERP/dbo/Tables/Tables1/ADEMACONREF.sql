CREATE TABLE [dbo].[ADEMACONREF] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCABECERA]    INT           NOT NULL,
    [NOMBRE]        VARCHAR (100) NULL,
    [NUMTELE]       VARCHAR (25)  NULL,
    [EMAILCONTACTO] CHAR (100)    NULL,
    [OBSERVA]       VARCHAR (500) NULL,
    [CORRCONTRARE]  BIT           NULL,
    CONSTRAINT [PK_ADEMACONREF] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADEMACONREF_ADPA3047E] FOREIGN KEY ([IDCABECERA]) REFERENCES [dbo].[ADPA3047E] ([AUTO])
);


GO
ALTER TABLE [dbo].[ADEMACONREF] NOCHECK CONSTRAINT [FK_ADEMACONREF_ADPA3047E];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me identifica si el correo es de contrarreferencia:  true -> Si  false -> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'CORRCONTRARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'OBSERVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Email Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'EMAILCONTACTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número Telefono del contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'NUMTELE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Contacto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera; campo que me tiene la relacion con la cabecera en este caso el campo AUTO de la tabla [ADPA3047E].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'IDCABECERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADEMACONREF', @level2type = N'COLUMN', @level2name = N'ID';

