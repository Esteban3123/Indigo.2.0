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
ALTER TABLE [Maintenance].[Responsible] NOCHECK CONSTRAINT [FK_Responsible_Branch];


GO
ALTER TABLE [Maintenance].[Responsible] NOCHECK CONSTRAINT [FK_Responsible_CostCenter];


GO
CREATE NONCLUSTERED INDEX [IX_Responsible__State]
    ON [Maintenance].[Responsible]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del responsable 1-Activo 0-Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'tipo de responsable 1-Administrativo 2-Asistencia', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'TypeResponsible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costro relacionado a el responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'IdCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la sucursal asociada a el responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'IdBranch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de vinculacion 1-Planta 2-Contratista 3- Paciente 4-Tercero', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'TypeEntailment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de los responsables', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de responsables', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'Responsible', @level2type = N'COLUMN', @level2name = N'Id';

