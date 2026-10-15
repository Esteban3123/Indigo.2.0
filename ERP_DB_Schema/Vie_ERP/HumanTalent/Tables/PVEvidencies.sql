CREATE TABLE [HumanTalent].[PVEvidencies] (
    [Id]                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionProfileId] INT          NOT NULL,
    [EvidenciesId]      INT          NOT NULL,
    [FrequencyId]       INT          NOT NULL,
    [CreationUser]      VARCHAR (20) NOT NULL,
    [CreationDate]      DATETIME     NOT NULL,
    [ModificationUser]  VARCHAR (20) NULL,
    [ModificationDate]  DATETIME     NULL,
    CONSTRAINT [PK__PVEviden__3214EC0748D7CA94] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PVEEvidencies] FOREIGN KEY ([EvidenciesId]) REFERENCES [StaffPick].[Evidencies] ([Id]),
    CONSTRAINT [fk_PVEPositionProfile] FOREIGN KEY ([PositionProfileId]) REFERENCES [HumanTalent].[PositionProfile] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVEvidencies] NOCHECK CONSTRAINT [fk_PVEEvidencies];


GO
ALTER TABLE [HumanTalent].[PVEvidencies] NOCHECK CONSTRAINT [fk_PVEPositionProfile];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la frecuencia', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'FrequencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de las Evidencias', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'EvidenciesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de perfil de la posición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'PositionProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id llave primaria', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVEvidencies', @level2type = N'COLUMN', @level2name = N'Id';

