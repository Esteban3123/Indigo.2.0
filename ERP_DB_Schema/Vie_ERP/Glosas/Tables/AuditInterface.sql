CREATE TABLE [Glosas].[AuditInterface] (
    [Id]             INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OptionRegister] VARCHAR (50) NOT NULL,
    [NumberGlosa]    VARCHAR (50) NOT NULL,
    [InvoiceNumber]  VARCHAR (50) NOT NULL,
    [ActionCode]     INT          NOT NULL,
    [Concept]        VARCHAR (50) NOT NULL,
    [NewConsecutive] VARCHAR (50) NOT NULL,
    [TimeStamp]      ROWVERSION   NOT NULL,
    [Date]           DATETIME     CONSTRAINT [DF_AuditInterface_Date] DEFAULT ([Common].[getdate]()) NULL,
    CONSTRAINT [PK_AuditInterface] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevo consecutivo', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'NewConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Concept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 Comprobante Contable  2 Nota Credito  3 Nota Debito', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'ActionCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de factura', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de glosa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'NumberGlosa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro de opciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'OptionRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'AuditInterface', @level2type = N'COLUMN', @level2name = N'Id';

