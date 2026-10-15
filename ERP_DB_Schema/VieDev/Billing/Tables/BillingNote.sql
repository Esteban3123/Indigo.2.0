CREATE TABLE [Billing].[BillingNote] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]            VARCHAR (20)  NOT NULL,
    [NoteDate]        DATETIME      NOT NULL,
    [CustomerPartyId] INT           NOT NULL,
    [Observations]    VARCHAR (MAX) NULL,
    [Nature]          TINYINT       NOT NULL,
    [OperatingUnitId] INT           NOT NULL,
    [EntityId]        INT           NOT NULL,
    [EntityName]      VARCHAR (250) NOT NULL,
    [CUDE]            VARCHAR (500) NULL,
    [QR]              VARCHAR (500) NULL,
    CONSTRAINT [PK_BillingNote] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingNote_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_BillingNote_ThirdParty] FOREIGN KEY ([CustomerPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Billing].[BillingNote] NOCHECK CONSTRAINT [FK_BillingNote_OperatingUnit];


GO
ALTER TABLE [Billing].[BillingNote] NOCHECK CONSTRAINT [FK_BillingNote_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Bidimensional QR del CUDE. Para la representación gráfica de las facturas electrónicas es requisito la generación de un código QR con la siguiente información:  NumFac: [NUMERO_DOCUMENTO]  FecFac: [FECHA_DOCUMENTO] en formato YYYYmmddHHMMssK  NitFac: [NIT FACTURADOR] sin puntos ni guiones  DocAdq: [NUMERO_ID_ADQUIRIENTE] sin puntos ni guiones  ValFac: [VALOR_FACTURA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValIva: [VALOR_IVA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValOtroIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValFacIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  CUDE: [CUDE]', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la Nota     1. Debito    2. Credito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Nature';

