CREATE TABLE [HumanTalent].[FrmPosition] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionId]          INT          NOT NULL,
    [CompetitionRoleId]   INT          NOT NULL,
    [PerformanceObjetive] INT          NOT NULL,
    [PerformanceClimate]  INT          NOT NULL,
    [WHSRolaId]           INT          NOT NULL,
    [Mobility]            INT          NOT NULL,
    [CreationUser]        VARCHAR (20) NOT NULL,
    [CreationDate]        DATETIME     NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    CONSTRAINT [PK__FrmPosit__3214EC07D305678E] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_CompetitionRoleFP] FOREIGN KEY ([CompetitionRoleId]) REFERENCES [Learning].[CompetitionRole] ([Id]),
    CONSTRAINT [FK_FrmPosition_FrmPosition] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [fk_WHSRolaIdFP] FOREIGN KEY ([WHSRolaId]) REFERENCES [WHS].[WHSRole] ([Id])
);


GO
ALTER TABLE [HumanTalent].[FrmPosition] NOCHECK CONSTRAINT [fk_CompetitionRoleFP];


GO
ALTER TABLE [HumanTalent].[FrmPosition] NOCHECK CONSTRAINT [FK_FrmPosition_FrmPosition];


GO
ALTER TABLE [HumanTalent].[FrmPosition] NOCHECK CONSTRAINT [fk_WHSRolaIdFP];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificació del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mobilidad: 1-Nacional, 2-Internacional, 3- No Aplica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'Mobility';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del rol de seguridad y salud en el trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'WHSRolaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objetivo para clima 1-SI, 0-NO', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PerformanceClimate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Objetivo para desempeño: 1-SI, 0-NO', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PerformanceObjetive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Rol de competencias', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cabecera de la tabla Payroll.Position', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'Id';

