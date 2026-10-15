CREATE TABLE [HumanTalent].[PVResponsabilitesFunction] (
    [Id]                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionProfileId]          INT          NOT NULL,
    [PositionResponsabilitiesId] INT          NOT NULL,
    [CreationUser]               VARCHAR (20) NOT NULL,
    [CreationDate]               DATETIME     NOT NULL,
    [ModificationUser]           VARCHAR (20) NULL,
    [ModificationDate]           DATETIME     NULL,
    CONSTRAINT [PK__PVRespon__3214EC0730828599] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PVPositionProfile] FOREIGN KEY ([PositionProfileId]) REFERENCES [HumanTalent].[PositionProfile] ([Id]),
    CONSTRAINT [fk_PVPositionResponsabilities] FOREIGN KEY ([PositionResponsabilitiesId]) REFERENCES [StaffPick].[PositionResponsabilities] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVResponsabilitesFunction] NOCHECK CONSTRAINT [fk_PVPositionProfile];


GO
ALTER TABLE [HumanTalent].[PVResponsabilitesFunction] NOCHECK CONSTRAINT [fk_PVPositionResponsabilities];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVResponsabilitesFunction', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVResponsabilitesFunction', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVResponsabilitesFunction', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVResponsabilitesFunction', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de las responsabilidades del puesto', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVResponsabilitesFunction', @level2type = N'COLUMN', @level2name = N'PositionResponsabilitiesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la posición del perfil ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVResponsabilitesFunction', @level2type = N'COLUMN', @level2name = N'PositionProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVResponsabilitesFunction', @level2type = N'COLUMN', @level2name = N'Id';

