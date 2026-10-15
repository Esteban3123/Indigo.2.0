CREATE TABLE [dbo].[IHPARAFEC] (
    [CODCONSEC] NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)    NOT NULL,
    [FECINIINV] DATETIME     NOT NULL,
    [FECFININV] DATETIME     NOT NULL,
    [CONFSUMIN] BIT          NOT NULL,
    [VEPRONPOS] CHAR (1)     NULL,
    CONSTRAINT [PK_IHPARAFEC] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_IHPARAFEC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[IHPARAFEC] NOCHECK CONSTRAINT [FK_IHPARAFEC_ADCENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro de verificacion de cantidades despachadas de productos NO POS  0: Ninguno  1: Advertir cuando se sobrepase la cantidad de producto justificada  2: No permitir el despacho de productos cuando se supere la cantidad justificada ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'VEPRONPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si realiza o no la confirmacion de suministros al ERP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CONFSUMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final Inicial Inventario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'FECFININV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial Inventario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'FECINIINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPARAFEC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

