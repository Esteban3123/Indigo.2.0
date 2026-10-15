CREATE TABLE [Maintenance].[MaintenanceContract] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)  NOT NULL,
    [ContractTypeId]             INT           NOT NULL,
    [OperatingUnitId]            INT           NOT NULL,
    [DocumentDate]               DATETIME      NULL,
    [InitialDate]                DATETIME      NOT NULL,
    [EndDate]                    DATETIME      NOT NULL,
    [Description]                VARCHAR (300) NOT NULL,
    [ContractNumber]             VARCHAR (100) NOT NULL,
    [SupplierId]                 INT           NOT NULL,
    [SupplierDistributionLineId] INT           NOT NULL,
    [Exclusivity]                BIT           NOT NULL,
    [SourceOrder]                TINYINT       NOT NULL,
    [OnlyGuarantee]              BIT           NOT NULL,
    [TechnicalSupervicion]       VARCHAR (50)  NULL,
    [SupervisionExecution]       VARCHAR (50)  NULL,
    [Clauses]                    VARCHAR (MAX) NULL,
    [Attachments]                VARCHAR (MAX) NULL,
    [Status]                     TINYINT       NOT NULL,
    [CreationUser]               VARCHAR (20)  NOT NULL,
    [CreationDate]               DATETIME      NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [ConfirmationUser]           VARCHAR (20)  NULL,
    [ConfirmationDate]           DATETIME      NULL,
    [AnnulmentUser]              VARCHAR (20)  NULL,
    [AnnulmentDate]              DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NOT NULL,
    CONSTRAINT [PK_MaintenanceContract__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__MaintenanceContract_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_MaintenanceContract_InventoryContractType] FOREIGN KEY ([ContractTypeId]) REFERENCES [Inventory].[InventoryContractType] ([Id]),
    CONSTRAINT [FK_MaintenanceContract_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_MaintenanceContract_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenanceContract] NOCHECK CONSTRAINT [FK__MaintenanceContract_OperatingUnit];


GO
ALTER TABLE [Maintenance].[MaintenanceContract] NOCHECK CONSTRAINT [FK_MaintenanceContract_InventoryContractType];


GO
ALTER TABLE [Maintenance].[MaintenanceContract] NOCHECK CONSTRAINT [FK_MaintenanceContract_Supplier];


GO
ALTER TABLE [Maintenance].[MaintenanceContract] NOCHECK CONSTRAINT [FK_MaintenanceContract_SuppliersDistributionLines];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:   1. Registrado  2. Confirmado   3. Anulado  4. Finalizado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anexos del contrato', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'Attachments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clausulas del contrato', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'Clauses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Supervicion de la ejecución', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'SupervisionExecution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Supervicion Técnica', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'TechnicalSupervicion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el contrato maneja garantia unica', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'OnlyGuarantee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen segun de la Cuantia  1 - Orden Simple  2 - Llamado Oferta  3 - Licitacion Publica', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'SourceOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el contrato maneja exclusividad ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'Exclusivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribucion del proveedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor con quien se establecio el contrato', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el numero del contrato', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del contrato', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final del contrato', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento con el cual se hará interfaces', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de contrato de inventarios', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'ContractTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del contrato', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del contrato de Mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceContract', @level2type = N'COLUMN', @level2name = N'Id';

