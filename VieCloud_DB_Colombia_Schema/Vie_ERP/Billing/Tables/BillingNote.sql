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



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código QR bidimensional (VARCHAR 500) con datos codificados de factura electrónica (número, fecha, NIT, documentoAdquiriente, valorFactura, IVA, impuestos) para validación y representación gráfica DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Bidimensional QR del CUDE. Para la representación gráfica de las facturas electrónicas es requisito la generación de un código QR con la siguiente información:  NumFac: [NUMERO_DOCUMENTO]  FecFac: [FECHA_DOCUMENTO] en formato YYYYmmddHHMMssK  NitFac: [NIT FACTURADOR] sin puntos ni guiones  DocAdq: [NUMERO_ID_ADQUIRIENTE] sin puntos ni guiones  ValFac: [VALOR_FACTURA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValIva: [VALOR_IVA] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValOtroIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  ValFacIm: [VALOR_OTROS_IMPUESTOS] con punto decimal, con decimales a dos (2) dígitos, sin separadores de miles, ni símbolo pesos.  CUDE: [CUDE]', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'QR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'QR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del documento electrónico (VARCHAR 500, PII); identificador obligatorio para facturación electrónica DIAN en Colombia.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'CUDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Unico del Documento Electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'CUDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'CUDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre (VARCHAR 250) o tipo del documento generador (ej: factura, recibo, giro, RIPS); identifica la entidad de origen.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad desde la cual se genero el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del documento fuente (factura, ingreso, atención, etc.) que generó esta nota de ajuste.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento que genero el registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la unidad operativa/centro de atención donde se originó el documento (FK a OperatingUnit).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Operativa en la cual se realiza el documento de origen', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nota (TINYINT): 1=Nota débito, 2=Nota crédito; indica la naturaleza contable del ajuste facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza de la Nota     1. Debito    2. Credito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre (VARCHAR MAX) con el detalle, concepto o justificación de la nota de débito o crédito.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle de la Nota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del tercero/cliente acreedor o deudor (FK a ThirdParty); representa la parte contratante en la transacción de factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'CustomerPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tercero del cliente de la transacción', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'CustomerPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'CustomerPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se generó la nota de facturación; marca temporal del documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'NoteDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Nota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'NoteDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'NoteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 20) que identifica la nota de facturación; referencia humanamente legible del documento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Nota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la nota de facturación; clave primaria autoincrementable del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Nota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas de facturación (notas crédito o débito) emitidas a clientes o pagadores. Registra el encabezado de cada nota con su fecha, naturaleza (crédito/débito), unidad operativa, datos del cliente y los códigos electrónicos de validación DIAN (CUDE y QR).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingNote';
