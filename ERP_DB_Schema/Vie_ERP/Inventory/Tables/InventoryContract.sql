CREATE TABLE [Inventory].[InventoryContract] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [OperatingUnitId]            INT             NOT NULL,
    [ContractTypeId]             INT             NOT NULL,
    [InitialDate]                DATETIME        NOT NULL,
    [EndDate]                    DATETIME        NOT NULL,
    [SupplierId]                 INT             NOT NULL,
    [SupplierDistributionLineId] INT             NOT NULL,
    [ContractNumber]             VARCHAR (20)    NOT NULL,
    [Description]                VARCHAR (300)   NOT NULL,
    [PaymentMethod]              VARCHAR (200)   NOT NULL,
    [DeliveryMethod]             VARCHAR (200)   NULL,
    [DeliveryPlace]              VARCHAR (200)   NULL,
    [SourceOrder]                TINYINT         NOT NULL,
    [PurchaseProcess]            TINYINT         NOT NULL,
    [Exclusivity]                BIT             NOT NULL,
    [OnlyGuarantee]              BIT             NOT NULL,
    [TechnicalSupervicion]       VARCHAR (100)   NULL,
    [SupervisionExecution]       VARCHAR (100)   NULL,
    [Clauses]                    VARCHAR (MAX)   NULL,
    [Attachments]                VARCHAR (MAX)   NULL,
    [Availability]               VARCHAR (200)   NULL,
    [Resolution]                 VARCHAR (200)   NULL,
    [ResolutionDate]             DATE            NULL,
    [QuoteNumber]                VARCHAR (50)    NULL,
    [QuoteDate]                  DATE            NULL,
    [RecordNumber]               VARCHAR (50)    NULL,
    [RecordDate]                 DATE            NULL,
    [NegotiationType]            VARCHAR (200)   NULL,
    [Approved]                   VARCHAR (100)   NULL,
    [Deadline]                   DATE            NULL,
    [ValidityDate]               DATE            NULL,
    [Value]                      DECIMAL (18, 2) NOT NULL,
    [DiscountValue]              DECIMAL (18, 2) NOT NULL,
    [IvaValue]                   DECIMAL (18, 2) NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    NOT NULL,
    [CreationDate]               DATETIME        NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    [DocumentDate]               DATETIME        NULL,
    [ManageProducts]             BIT             CONSTRAINT [DF_InventoryContract_ManageProducts] DEFAULT ((1)) NOT NULL,
    [ModificationValue]          DECIMAL (18, 2) CONSTRAINT [DF_InventoryContract_ModificationValue] DEFAULT ((0)) NOT NULL,
    [CurrencyId]                 INT             CONSTRAINT [DF__Inventory__Curre__64C92BC5] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_InventoryContract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryContract_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_InventoryContract_InventoryContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Inventory].[InventoryContractType] ([Id]),
    CONSTRAINT [FK_InventoryContract_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_InventoryContract_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_InventoryContract_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_Currency];


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_InventoryContractType];


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_OperatingUnit];


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_Supplier];


GO
ALTER TABLE [Inventory].[InventoryContract] NOCHECK CONSTRAINT [FK_InventoryContract_SuppliersDistributionLines];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InventoryContract__Code]
    ON [Inventory].[InventoryContract]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de las modificaciones realizadas al contrato a través de otro si', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si maneja productos o solo es un contrato por un valor total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ManageProducts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento con el cual se hará interfaces', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:   1. Registrado  2. Confirmado   3. Anulado  4. Legalizado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del contrato, se obtiene tomando (Value - DiscountValue + IvaValue), o, es el valor del contrato si este no maneja productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del iva, es la sumatoria de todos los valores del iva de los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del descuento del contrato, es la sumatoria de todos los descuento de los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de el contrato, es la sumatoria de todos los valores de los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de vigencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ValidityDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha limite', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Deadline';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica por quien fue aprobado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Approved';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de negociacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'NegotiationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de acta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'RecordDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de acta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'RecordNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la cotizacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'QuoteDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de cotización', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'QuoteNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha resolución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ResolutionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Resolution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Disponibilidad del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Availability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anexos del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Attachments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clausulas del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Clauses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Supervicion de la ejecución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupervisionExecution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Supervicion Técnica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'TechnicalSupervicion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el contrato maneja garantia unica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'OnlyGuarantee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el contrato maneja exclusividad ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Exclusivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el proceso de compras  1 - Directa  2 - Negociacion Conjunta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'PurchaseProcess';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen segun de la Cuantia  1 - Orden Simple  2 - Llamado Oferta  3 - Licitacion Publica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SourceOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lugar de entrega de los pedidos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DeliveryPlace';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de entrega', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'DeliveryMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'forma de pago del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'PaymentMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el numero del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribucion del proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor con quien se establecio el contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de contrato de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryContract', @level2type = N'COLUMN', @level2name = N'Id';

