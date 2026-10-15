CREATE TABLE [Common].[PersonFreeTimeUse] (
    [Id]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]      INT NOT NULL,
    [FreeTimeUseId] INT NOT NULL,
    CONSTRAINT [PK_PersonFreeTimeUse__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonFreeTimeUse_FreeTimeUse] FOREIGN KEY ([FreeTimeUseId]) REFERENCES [Payroll].[FreeTimeUse] ([Id]),
    CONSTRAINT [FK_PersonFreeTimeUse_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [UQ_PersonFreeTimeUse__PersonId] UNIQUE NONCLUSTERED ([PersonId] ASC)
);


GO
ALTER TABLE [Common].[PersonFreeTimeUse] NOCHECK CONSTRAINT [FK_PersonFreeTimeUse_FreeTimeUse];


GO
ALTER TABLE [Common].[PersonFreeTimeUse] NOCHECK CONSTRAINT [FK_PersonFreeTimeUse_Person];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foranea a tabla uso del tiempo libre', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'FreeTimeUseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foranea a Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de usos del tiempo libre de la persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonFreeTimeUse', @level2type = N'COLUMN', @level2name = N'Id';

