CREATE TABLE [HumanTalent].[PositionProfile] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20)  NOT NULL,
    [Name]               VARCHAR (50)  NOT NULL,
    [Purpose]            VARCHAR (700) NOT NULL,
    [AreaId]             INT           NOT NULL,
    [Mobility]           INT           NOT NULL,
    [PRofessionalRiskId] INT           NOT NULL,
    [State]              INT           NULL,
    [CreationUser]       VARCHAR (20)  NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    CONSTRAINT [PK__Position__3214EC0773F04FB6] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_PPArea] FOREIGN KEY ([AreaId]) REFERENCES [HumanTalent].[Area] ([Id]),
    CONSTRAINT [fk_PPPRofessionalRisk] FOREIGN KEY ([PRofessionalRiskId]) REFERENCES [Payroll].[ProfessionalRisk] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PositionProfile] NOCHECK CONSTRAINT [fk_PPArea];


GO
ALTER TABLE [HumanTalent].[PositionProfile] NOCHECK CONSTRAINT [fk_PPPRofessionalRisk];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Riesgo profesional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'PRofessionalRiskId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Movilidad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'Mobility';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del área ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'AreaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Objetivo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'Purpose';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del perfil del cargo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la pisición del perfil ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PositionProfile', @level2type = N'COLUMN', @level2name = N'Id';

