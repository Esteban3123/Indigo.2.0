CREATE TABLE [Maintenance].[Responsible] (
    [Id]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]            VARCHAR (25) NOT NULL,
    [Name]            VARCHAR (60) NOT NULL,
    [TypeEntailment]  CHAR (1)     NOT NULL,
    [IdBranch]        TINYINT      NOT NULL,
    [IdCostCenter]    INT          NOT NULL,
    [TypeResponsible] CHAR (1)     NOT NULL,
    [State]           BIT          NOT NULL,
    CONSTRAINT [PK_Responsible__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Responsible_Branch] FOREIGN KEY ([IdBranch]) REFERENCES [Maintenance].[Branch] ([Id]),
    CONSTRAINT [FK_Responsible_CostCenter] FOREIGN KEY ([IdCostCenter]) REFERENCES [Maintenance].[CostCenter] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_Responsible__State]
    ON [Maintenance].[Responsible]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del responsable: 1-Activo (habilitado para usar), 0-Inactivo (deshabilitado, sin operatividad)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del responsable 1-Activo 0-Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o rol del responsable: 1-Administrativo (gestión/supervisión), 2-Asistencia (soporte técnico/operativo)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'TypeResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tipo de responsable 1-Administrativo 2-Asistencia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'TypeResponsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'TypeResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del centro de costo asociado al responsable para imputación presupuestaria y contable (INT)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costro relacionado a el responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'IdCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la sucursal, sede o centro de atención donde está asignado el responsable (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'IdBranch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la sucursal asociada a el responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'IdBranch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'IdBranch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vinculación contractual del responsable: 1-Planta (empleado directo), 2-Contratista (tercero contratado), 3-Paciente, 4-Tercero', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'TypeEntailment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de vinculacion 1-Planta 2-Contratista 3- Paciente 4-Tercero', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'TypeEntailment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'TypeEntailment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del responsable (VARCHAR 60), profesional o entidad responsable de mantenimiento/gestión', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único (VARCHAR 25) del responsable, identificador corto para búsqueda y referencia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de los responsables', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) del responsable en la tabla de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de responsables', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de responsables o encargados de mantenimiento: personas o entidades asignadas a tareas de mantenimiento, asociadas a una sucursal y centro de costos, con su tipo de vinculación y estado activo/inactivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible';
