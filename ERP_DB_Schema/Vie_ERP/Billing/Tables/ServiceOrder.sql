CREATE TABLE [Billing].[ServiceOrder] (
    [Id]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)                                                                     NOT NULL,
    [AdmissionNumber]  CHAR (10)                                                                        NOT NULL,
    [PatientCode]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [OrderDate]        DATETIME                                                                         NOT NULL,
    [AffectInventory]  BIT                                                                              CONSTRAINT [DF_ServiceOrder_AffectInventory] DEFAULT ((1)) NOT NULL,
    [EntityCode]       VARCHAR (20)                                                                     NULL,
    [EntityId]         INT                                                                              NULL,
    [EntityName]       VARCHAR (250)                                                                    NULL,
    [OperatingUnitId]  INT                                                                              NOT NULL,
    [Status]           TINYINT                                                                          NOT NULL,
    [CreationUser]     VARCHAR (20)                                                                     CONSTRAINT [DF_ServiceOrder_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME                                                                         CONSTRAINT [DF_ServiceOrder_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)                                                                     NULL,
    [ModificationDate] DATETIME                                                                         NULL,
    [InvoicedUser]     VARCHAR (20)                                                                     NULL,
    [InvoicedDate]     DATETIME                                                                         NULL,
    [AnnulmentUser]    VARCHAR (20)                                                                     NULL,
    [AnnulmentDate]    DATETIME                                                                         NULL,
    [TimeStamp]        ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_ServiceOrder__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceOrder_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Billing].[ServiceOrder] NOCHECK CONSTRAINT [FK_ServiceOrder_OperatingUnit];


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrder_AdmissionNumber]
    ON [Billing].[ServiceOrder]([AdmissionNumber] ASC);


GO
CREATE NONCLUSTERED INDEX [nci_wi_ServiceOrder_49BE05C9A135F8795BC9B39B34B06099]
    ON [Billing].[ServiceOrder]([PatientCode] ASC)
    INCLUDE([Status]);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrder_EntityCode_EntityName_Status]
    ON [Billing].[ServiceOrder]([EntityCode] ASC, [EntityName] ASC, [Status] ASC);


GO
CREATE NONCLUSTERED INDEX [Status]
    ON [Billing].[ServiceOrder]([Status] ASC)
    INCLUDE([EntityId]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ServiceOrder__Code]
    ON [Billing].[ServiceOrder]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Facturacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'InvoicedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que Facturo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'InvoicedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento   Registrado = 1  Facturado = 2  Anulado = 3', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la entidad quien genera el documento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si afecta inventario, este campo por lo general es true ya que lo vamos a dejar por defecto, esta bandera es para identificar que una orden de servicio generada desde dispensacion afecto o no el inventario fisico, esto lo hacemos por que solo cuando no se afecte el inventario fisico es que se puede anular una orden de servicio    Nota: cuando sea una orden de servicio con solo servicios este campo tambien estara en true pero no se tendra en cuenta para las validaciones ya que solo es para las ordenes que sean de productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'AffectInventory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente, esto se saca de tabla de INPACIENT de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'Id';

