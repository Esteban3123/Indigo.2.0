CREATE TABLE [dbo].[SOLORDCOM] (
    [ORDCONSEC]  VARCHAR (50)   NOT NULL,
    [CODUSUARI]  CHAR (20)      NOT NULL,
    [ORDENFECH]  DATETIME       NOT NULL,
    [AUTOCOTI]   INT            NOT NULL,
    [ORDDESCUE]  NUMERIC (5, 2) NOT NULL,
    [ORDESTADO]  TINYINT        NULL,
    [ORDFACTURA] VARCHAR (50)   NULL,
    [ORDFECENT]  DATETIME       NULL,
    [ORDMOTPAR]  VARCHAR (50)   NULL,
    [ORDARCFAC]  VARCHAR (50)   NULL,
    [PROVAUTO]   NUMERIC (18)   NOT NULL,
    [CODCENATE]  CHAR (10)      NULL,
    CONSTRAINT [PK_SOLORDCOM_1] PRIMARY KEY CLUSTERED ([ORDCONSEC] ASC),
    CONSTRAINT [FK_SOLORDCOM_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_SOLORDCOM_SOLPROVEE] FOREIGN KEY ([PROVAUTO]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);


GO
ALTER TABLE [dbo].[SOLORDCOM] NOCHECK CONSTRAINT [FK_SOLORDCOM_ADCENATEN];


GO
ALTER TABLE [dbo].[SOLORDCOM] NOCHECK CONSTRAINT [FK_SOLORDCOM_SOLPROVEE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'PROVAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factura de venta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDARCFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el motivo de la solicitud parcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDMOTPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDFECENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la factura de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDFACTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el estado de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el descuento de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDDESCUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la fecha de la orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDENFECH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el consecutivo del orden de compra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLORDCOM', @level2type = N'COLUMN', @level2name = N'ORDCONSEC';

