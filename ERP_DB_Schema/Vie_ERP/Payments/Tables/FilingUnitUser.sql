CREATE TABLE [Payments].[FilingUnitUser] (
    [Id]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FillingUnitId] INT          NOT NULL,
    [UserId]        INT          NOT NULL,
    [UserCode]      VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_FilingUnitUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FilingUnitUser_FilingUnit] FOREIGN KEY ([FillingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id])
);


GO
ALTER TABLE [Payments].[FilingUnitUser] NOCHECK CONSTRAINT [FK_FilingUnitUser_FilingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario al que se le asigno la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FilingUnitUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del usuario al que se le asigno el permiso', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FilingUnitUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de radicacion', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FilingUnitUser', @level2type = N'COLUMN', @level2name = N'FillingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FilingUnitUser', @level2type = N'COLUMN', @level2name = N'Id';

