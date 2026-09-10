CREATE TABLE [Management].[MachineUpdatePackage] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [IdMachine]       INT      NOT NULL,
    [IdUpdatePackage] INT      NOT NULL,
    [UpgradeDate]     DATETIME NULL,
    [UpgradedDate]    DATETIME NULL,
    [Upgraded]        BIT      CONSTRAINT [DF_MachineUpdatePackage_Upgraded] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_MachineUpdatePackage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MachineUpdatePackage_MachineUpdatePackage] FOREIGN KEY ([IdMachine]) REFERENCES [Management].[Machines] ([Id]),
    CONSTRAINT [FK_MachineUpdatePackage_UpdatePackage] FOREIGN KEY ([IdUpdatePackage]) REFERENCES [Management].[UpdatePackage] ([Id])
);

