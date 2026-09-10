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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro de formulario de posición. Tipo: DATETIME. Auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificació del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que realizó la última modificación del registro. Tipo: VARCHAR(20). Auditoría, rastreo de cambios.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario que modifica el registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro de formulario de posición. Tipo: DATETIME. Auditoría, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que creó el registro de formulario de posición. Tipo: VARCHAR(20). Auditoría, responsabilidad.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario de creación del registro', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de movilidad laboral: 1=Nacional, 2=Internacional, 3=No aplica. Alcance geográfico del cargo o posición.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'Mobility';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mobilidad: 1-Nacional, 2-Internacional, 3- No Aplica', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'Mobility';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'Mobility';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del rol de Seguridad y Salud en el Trabajo (WHS/SST) asociado a la posición. Clave foránea a WHS.WHSRole. Responsabilidades de seguridad ocupacional.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'WHSRolaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rol de seguridad y salud en el trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'WHSRolaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'WHSRolaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (1=Sí, 0=No) que señala si el clima organizacional es un objetivo de desempeño para la posición. Evaluación de ambiente laboral.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PerformanceClimate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Objetivo para clima 1-SI, 0-NO', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PerformanceClimate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PerformanceClimate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (1=Sí, 0=No) que señala si hay objetivo de desempeño establecido para la posición. Gestión del desempeño, metas laborales.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PerformanceObjetive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Objetivo para desempeño: 1-SI, 0-NO', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PerformanceObjetive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PerformanceObjetive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del rol de competencias asociado a la posición. Clave foránea a Learning.CompetitionRole. Competencias requeridas, formación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Rol de competencias', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'CompetitionRoleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la posición o cargo padre. Clave foránea a Payroll.Position. Vínculo con estructura de nómina, cargos, empleados.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cabecera de la tabla Payroll.Position', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PositionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'PositionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de formulario de posición. Clave primaria IDENTITY(1,1). INT. Identificación principal de la tabla FrmPosition.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Formulario de posiciones o cargos del módulo de Talento Humano. Registra la configuración de cada cargo con sus roles de competencia, objetivos de desempeño, clima organizacional, rol en seguridad y salud en el trabajo (WHSRola), movilidad y auditoría de creación/modificación.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'FrmPosition';
