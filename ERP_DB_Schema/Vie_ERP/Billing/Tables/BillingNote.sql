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
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BillingNote__Code]
    ON [Billing].[BillingNote]([Code] ASC);

GO
CREATE TRIGGER [Billing].[TR_BillingNote_PreventDuplicateCode]
ON [Billing].[BillingNote]
FOR INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Verificar si existe algún código duplicado (excluyendo el registro recién insertado)
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        INNER JOIN [Billing].[BillingNote] bn ON i.Code = bn.Code AND i.Id <> bn.Id
    )
    BEGIN
        DECLARE @DuplicateCode VARCHAR(20);
        SELECT TOP 1 @DuplicateCode = i.Code 
        FROM inserted i
        INNER JOIN [Billing].[BillingNote] bn ON i.Code = bn.Code AND i.Id <> bn.Id;
        
        RAISERROR('No se puede insertar la nota electrónica. Ya existe una nota con el código: %s', 16, 1, @DuplicateCode);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Bidimensional QR del CUDE. Para la representación gráfica de las facturas electrónicas es requisito la generación de un código QR con la siguiente información:  NumFac: [NUMERO_DOCUMENTO]  FecFac: [FECHA_DOCUMENTO] en formato YYYYmmddHHMMssK  NitFac: [NIT FACTURADOR] sin puntos ni guiones  DocAdq: [NUMERO_ID_ADQUIRIENTE] sin puntos ni guiones  ValFac: [VALOR_FACTURA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValIva: [VALOR_IVA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValOtroIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValFacIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  CUDE: [CUDE]', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Unico del Documento Electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'CUDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad desde la cual se genero el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento que genero el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Operativa en la cual se realiza el documento de origen', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza de la Nota     1. Debito    2. Credito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de la Nota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero del cliente de la transacción', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'CustomerPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Nota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'NoteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Nota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Nota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Id';

