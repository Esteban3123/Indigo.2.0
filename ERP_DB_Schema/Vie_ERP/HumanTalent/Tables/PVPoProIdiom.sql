CREATE TABLE [HumanTalent].[PVPoProIdiom] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionProfileId]   INT          NOT NULL,
    [Idiom]               INT          NOT NULL,
    [ConversationalLevel] INT          NOT NULL,
    [WritingLevel]        INT          NOT NULL,
    [UnderstadingLevel]   INT          NOT NULL,
    [CreationUser]        VARCHAR (20) NOT NULL,
    [CreationDate]        DATETIME     NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    CONSTRAINT [PK__PVPoProI__3214EC0780D8D932] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PVPoProIdiomPositionProfile] FOREIGN KEY ([PositionProfileId]) REFERENCES [HumanTalent].[PositionProfile] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PVPoProIdiom] NOCHECK CONSTRAINT [fk_PVPoProIdiomPositionProfile];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de comprensión', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'UnderstadingLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de escritura', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'WritingLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel conversacional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'ConversationalLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Idioma', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'Idiom';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de perfil de posición', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'PositionProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PVPoProIdiom', @level2type = N'COLUMN', @level2name = N'Id';

