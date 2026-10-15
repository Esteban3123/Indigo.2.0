CREATE TABLE [Billing].[ElectronicDocument] (
    [Id]              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId] INT             NOT NULL,
    [CustomerPartyId] INT             NOT NULL,
    [EntityId]        INT             NOT NULL,
    [EntityName]      VARCHAR (250)   NOT NULL,
    [DocumentDate]    DATETIME        NOT NULL,
    [DocumentType]    TINYINT         NOT NULL,
    [Status]          TINYINT         NOT NULL,
    [CreationDate]    DATETIME        NOT NULL,
    [ShippingDate]    DATETIME        NULL,
    [Container]       VARCHAR (50)    NOT NULL,
    [FilePath]        VARCHAR (250)   NOT NULL,
    [Prefix]          VARCHAR (5)     NULL,
    [DocumentNumber]  VARCHAR (20)    NOT NULL,
    [CUFE]            VARCHAR (500)   NULL,
    [DianVersion]     DECIMAL (18, 2) CONSTRAINT [DF_ElectronicDocument_DianVersion] DEFAULT ((1)) NOT NULL,
    [ValidationDate]  DATETIME        NULL,
    [Retry]           INT             CONSTRAINT [DF_ElectronicDocument_Retry] DEFAULT ((0)) NOT NULL,
    [ZipKey]          VARCHAR (500)   NULL,
    [Year]            INT             CONSTRAINT [DF_ElectronicDocument_Year] DEFAULT ((2019)) NOT NULL,
    [Consecutive]     INT             CONSTRAINT [DF_ElectronicDocument_Consecutive] DEFAULT ((0)) NOT NULL,
    [HttpContent]     VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_ElectronicDocument] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicDocument_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_ElectronicDocument_ThirdParty] FOREIGN KEY ([CustomerPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Billing].[ElectronicDocument] NOCHECK CONSTRAINT [FK_ElectronicDocument_OperatingUnit];


GO
ALTER TABLE [Billing].[ElectronicDocument] NOCHECK CONSTRAINT [FK_ElectronicDocument_ThirdParty];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InvoiceId]
    ON [Billing].[ElectronicDocument]([EntityId] ASC, [EntityName] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_ElectronicDocument_Year]
    ON [Billing].[ElectronicDocument]([Year] ASC)
    INCLUDE([DocumentType], [Consecutive]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contenido estructurado en un formato especifico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'HttpContent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el estado actual del registro ante la DIAN      0. Invalida (Los detalles de la factura no concuerdan)      1. Registrada (Registro Creado)      2. Enviada (Factura enviada y recibida)      3. Validada (Factura valida)      4. Validacion Fallida (Factura invalida)      ----------------------------------------------      66. Reenvío Obligatorio (Volver a generar el XML y enviar)      77. Pendiente (Documento de otra versión)      88. Pendiente (En proceso de envío)      99. Pendiente (En proceso de validación)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipos de Documentos      
1. Factura EAPB con Contrato      
2. Factura EAPB Sin Contrato      
3. Factura Particular      
4. Factura Capitada      
5. Control de Capitacion      
6. Factura Basica      
7. Factura de Venta de Productos      
--------------------------------      
91. Nota Credito Validacion Previa
92. Nota Debito Validacion Previa
93. Nota Credito Validacion Previa - Facturas version anterior
94. Nota Debito Validacion Previa - Facturas version anterior
98. Nota Debito
99. Nota Credito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocument', @level2type = N'COLUMN', @level2name = N'DocumentType';

