CREATE TABLE [InteropCost].[ProductionCenter] (
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
    CONSTRAINT [PK_ProductionCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionCenter_OrganizationalStructureOfCosts] FOREIGN KEY ([OrganizationalStructureOfCostId]) REFERENCES [InteropCost].[OrganizationalStructureOfCosts] ([Id])
);


GO
ALTER TABLE [InteropCost].[ProductionCenter] NOCHECK CONSTRAINT [FK_ProductionCenter_OrganizationalStructureOfCosts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenter', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de centro  1 - Operativo  2 - Administrativo  3 - Logístico', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'ProductionCenter', @level2type = N'COLUMN', @level2name = N'CenterType';

