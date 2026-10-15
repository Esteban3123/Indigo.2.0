CREATE TABLE [InteropCost].[OrganizationalStructureOfCosts] (
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
    CONSTRAINT [PK_OrganizationalStructureOfCosts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrganizationalStructureOfCosts_OrganizationalStructureOfCosts] FOREIGN KEY ([ParentId]) REFERENCES [InteropCost].[OrganizationalStructureOfCosts] ([Id])
);


GO
ALTER TABLE [InteropCost].[OrganizationalStructureOfCosts] NOCHECK CONSTRAINT [FK_OrganizationalStructureOfCosts_OrganizationalStructureOfCosts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Status';

