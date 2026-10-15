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
CREATE NONCLUSTERED INDEX [IX_ServiceOrder_EntityCode_EntityName_Status]
    ON [Billing].[ServiceOrder]([EntityCode] ASC, [EntityName] ASC, [Status] ASC);


GO
CREATE NONCLUSTERED INDEX [Status]
    ON [Billing].[ServiceOrder]([Status] ASC)
    INCLUDE([EntityId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Documento   Registrado = 1  Facturado = 2  Anulado = 3', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si afecta inventario, este campo por lo general es true ya que lo vamos a dejar por defecto, esta bandera es para identificar que una orden de servicio generada desde dispensacion afecto o no el inventario fisico, esto lo hacemos por que solo cuando no se afecte el inventario fisico es que se puede anular una orden de servicio    Nota: cuando sea una orden de servicio con solo servicios este campo tambien estara en true pero no se tendra en cuenta para las validaciones ya que solo es para las ordenes que sean de productos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrder', @level2type = N'COLUMN', @level2name = N'AffectInventory';

