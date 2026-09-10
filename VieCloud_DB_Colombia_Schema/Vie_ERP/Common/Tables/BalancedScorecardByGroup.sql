CREATE TABLE [Common].[BalancedScorecardByGroup] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UserGroup]           INT NOT NULL,
    [BalancedScorecardId] INT NOT NULL,
    CONSTRAINT [PK_BalancedScorecardByGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BalancedScorecardByGroup_BalancedScorecard] FOREIGN KEY ([BalancedScorecardId]) REFERENCES [Common].[BalancedScorecard] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Balanced Scorecard (cuadro de mando integral), clave foránea que referencia la tabla Common.BalancedScorecard. Representa el score card o tarjeta de desempeño asignado al grupo de usuarios. Tipo: INT, FK.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'BalancedScorecardId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Saldo en el score card', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'BalancedScorecardId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'BalancedScorecardId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de usuarios (rol, departamento o unidad funcional) que tiene acceso o está asociado al balanced scorecard. Referencia grupos de permisos, roles o centros de atención. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'UserGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo al que pertenece el usuario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'UserGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'UserGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la relación entre grupo de usuarios y scorecard. Clave primaria de la tabla. Tipo: INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona grupos de usuarios con los cuadros de mando integral (Balanced Scorecard) que tienen asignados, permitiendo controlar qué indicadores y tableros de gestión puede ver cada grupo dentro del sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup';
