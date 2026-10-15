CREATE TABLE [Cost].[CostOrganizationalStructureOfCosts] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [ParentId]         INT           NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [Level]            INT           CONSTRAINT [DF_CostOrganizationalStructureOfCosts_Level] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CostOrganizationalStructureOfCosts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostOrganizationalStructureOfCosts_CostOrganizationalStructureOfCosts] FOREIGN KEY ([ParentId]) REFERENCES [Cost].[CostOrganizationalStructureOfCosts] ([Id])
);


GO
ALTER TABLE [Cost].[CostOrganizationalStructureOfCosts] NOCHECK CONSTRAINT [FK_CostOrganizationalStructureOfCosts_CostOrganizationalStructureOfCosts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostOrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Status';

