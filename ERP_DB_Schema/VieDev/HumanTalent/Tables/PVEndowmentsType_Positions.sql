CREATE TABLE [HumanTalent].[PVEndowmentsType_Positions] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EndowmentsTypeId] INT          NOT NULL,
    [PositionsId]      INT          NOT NULL,
    [CreationUser]     VARCHAR (50) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    CONSTRAINT [PK_PVEndowmentsType_Positions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PVEndowmentsType_Positions_EndowmentsType] FOREIGN KEY ([EndowmentsTypeId]) REFERENCES [HumanTalent].[EndowmentsType] ([Id]),
    CONSTRAINT [FK_PVEndowmentsType_Positions_Position] FOREIGN KEY ([PositionsId]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVEndowmentsType_Positions] NOCHECK CONSTRAINT [FK_PVEndowmentsType_Positions_EndowmentsType];


GO
ALTER TABLE [HumanTalent].[PVEndowmentsType_Positions] NOCHECK CONSTRAINT [FK_PVEndowmentsType_Positions_Position];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecah de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsType_Positions', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creo el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsType_Positions', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la posición - cargo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsType_Positions', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tipo de Dotaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsType_Positions', @level2type = N'COLUMN', @level2name = N'EndowmentsTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEndowmentsType_Positions', @level2type = N'COLUMN', @level2name = N'Id';

