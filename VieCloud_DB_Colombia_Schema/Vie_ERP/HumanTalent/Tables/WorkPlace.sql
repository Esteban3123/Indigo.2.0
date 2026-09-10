CREATE TABLE [HumanTalent].[WorkPlace] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20) NOT NULL,
    [Name]             VARCHAR (80) NOT NULL,
    [BranchOfficeId]   INT          NOT NULL,
    [State]            BIT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_WorkPlace] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__WorkPlace__Branc__4C1B1787] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [UQ__WorkPlac__A25C5AA7C86C195B] UNIQUE NONCLUSTERED ([Code] ASC)
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro; marca temporal de auditoría, nulable si no hay cambios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del lugar de trabajo; auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro del lugar de trabajo; marca temporal de auditoría', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro del lugar de trabajo; auditoría de origen', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del lugar de trabajo: 1=Activo, 0=Inactivo; indicador booleano (BIT) de disponibilidad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la sucursal u oficina principal asociada; referencia a Payroll.BranchOffice', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Sucursal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 80) del lugar de trabajo, unidad funcional, sede, oficina o centro de atención', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Lugares de Trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del lugar de trabajo, unidad funcional o centro de atención; clave única no clusterizada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Lugares de Trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del lugar de trabajo, clave primaria de la tabla WorkPlace', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Lugares de Trabajo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugares o puestos de trabajo del módulo de Talento Humano. Registra los sitios físicos o funcionales donde se desempeña el personal, asociados a una sede o sucursal de la organización.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'WorkPlace';
