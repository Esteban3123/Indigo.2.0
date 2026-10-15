CREATE TABLE [Inventory].[DispensingByFunctionalUnit] (
    [Id]                         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FunctionalUnitId]           INT NOT NULL,
    [PharmaceuticalDispensingId] INT NOT NULL,
    CONSTRAINT [PK_Inventory_DispensingByFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC)
);

