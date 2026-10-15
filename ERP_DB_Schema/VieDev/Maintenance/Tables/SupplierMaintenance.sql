CREATE TABLE [Maintenance].[SupplierMaintenance] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit]                 VARCHAR (15)  NOT NULL,
    [CompleteName]        VARCHAR (100) NOT NULL,
    [Names]               VARCHAR (50)  NOT NULL,
    [LastNames]           VARCHAR (50)  NULL,
    [TimeLimitDays]       INT           NOT NULL,
    [CodeCMMS]            VARCHAR (20)  NULL,
    [WebSite]             VARCHAR (80)  NULL,
    [IdCity]              INT           NOT NULL,
    [PermanentRetention]  BIT           NOT NULL,
    [NotIva]              BIT           NOT NULL,
    [Manufacturer]        BIT           NOT NULL,
    [Seller]              BIT           NOT NULL,
    [ResponsibleWarranty] BIT           NOT NULL,
    [Status]              BIT           NOT NULL,
    [CreationUser]        VARCHAR (20)  NOT NULL,
    [CreationDate]        DATETIME      NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    CONSTRAINT [PK_SupplierMaintenance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Supplier_City] FOREIGN KEY ([IdCity]) REFERENCES [Common].[City] ([Id])
);


GO
ALTER TABLE [Maintenance].[SupplierMaintenance] NOCHECK CONSTRAINT [FK_Supplier_City];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del fabricante', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'especifica si es responsable de la garantia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'ResponsibleWarranty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el proveedor es vendedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Seller';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es un fabricante.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Manufacturer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No incluye iva', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'NotIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Retencion permanente', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'PermanentRetention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la ciudad relacionada', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'IdCity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pagina web del proveedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'WebSite';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CMMS', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CodeCMMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de plazo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'TimeLimitDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apellidos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'LastNames';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombres', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Names';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Completo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'CompleteName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit del Proveedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'SupplierMaintenance', @level2type = N'COLUMN', @level2name = N'Id';

