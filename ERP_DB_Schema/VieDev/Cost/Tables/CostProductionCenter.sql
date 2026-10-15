CREATE TABLE [Cost].[CostProductionCenter] (
    [Id]                              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                            VARCHAR (20)    NOT NULL,
    [Name]                            VARCHAR (100)   NOT NULL,
    [OrganizationalStructureOfCostId] INT             NOT NULL,
    [Area]                            NUMERIC (18, 2) NOT NULL,
    [CenterType]                      TINYINT         NOT NULL,
    [CancellationCostMainAccountId]   INT             NULL,
    [Description]                     VARCHAR (300)   NOT NULL,
    [Status]                          BIT             NOT NULL,
    [CreationUser]                    VARCHAR (20)    NOT NULL,
    [CreationDate]                    DATETIME        NOT NULL,
    [ModificationUser]                VARCHAR (20)    NULL,
    [ModificationDate]                DATETIME        NULL,
    [TimeStamp]                       ROWVERSION      NOT NULL,
    [CategoryId]                      INT             NULL,
    CONSTRAINT [PK_CostProductionCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostProductionCenter_CostOrganizationalStructureOfCosts] FOREIGN KEY ([OrganizationalStructureOfCostId]) REFERENCES [Cost].[CostOrganizationalStructureOfCosts] ([Id]),
    CONSTRAINT [FK_CostProductionCenter_CostProductionCenterCategory] FOREIGN KEY ([CategoryId]) REFERENCES [Cost].[CostProductionCenterCategory] ([Id]),
    CONSTRAINT [FK_CostProductionCenter_MainAccounts] FOREIGN KEY ([CancellationCostMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Cost].[CostProductionCenter] NOCHECK CONSTRAINT [FK_CostProductionCenter_CostOrganizationalStructureOfCosts];


GO
ALTER TABLE [Cost].[CostProductionCenter] NOCHECK CONSTRAINT [FK_CostProductionCenter_CostProductionCenterCategory];


GO
ALTER TABLE [Cost].[CostProductionCenter] NOCHECK CONSTRAINT [FK_CostProductionCenter_MainAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Categoría del centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenter', @level2type = N'COLUMN', @level2name = N'CategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenter', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenter', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de centro  1 - Operativo  2 - Administrativo  3 - Logístico', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostProductionCenter', @level2type = N'COLUMN', @level2name = N'CenterType';

