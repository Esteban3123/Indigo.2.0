CREATE TABLE [Inventory].[ATCEntity] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [Name]                   VARCHAR (200) NOT NULL,
    [IdPharmacologicalGroup] INT           NOT NULL,
    [State]                  BIT           NOT NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ATCEntity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ATCEntity_PharmacologicalGroup] FOREIGN KEY ([IdPharmacologicalGroup]) REFERENCES [Inventory].[PharmacologicalGroup] ([Id])
);


GO
ALTER TABLE [Inventory].[ATCEntity] NOCHECK CONSTRAINT [FK_ATCEntity_PharmacologicalGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1. Activo  0. Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ATCEntity', @level2type = N'COLUMN', @level2name = N'State';

