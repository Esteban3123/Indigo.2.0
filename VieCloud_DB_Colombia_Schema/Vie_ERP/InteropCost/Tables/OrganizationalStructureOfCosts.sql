CREATE TABLE [InteropCost].[OrganizationalStructureOfCosts] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [ParentId]         INT           NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_OrganizationalStructureOfCosts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrganizationalStructureOfCosts_OrganizationalStructureOfCosts] FOREIGN KEY ([ParentId]) REFERENCES [InteropCost].[OrganizationalStructureOfCosts] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (rowversion) que registra automáticamente el instante exacto de creación, modificación o cambio de estado en la estructura de costos. Utilizado para auditoría y concurrencia.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME en la que se realizó la última modificación o actualización de la estructura organizacional de costos. NULL si nunca fue editada.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que realizó la última modificación de la estructura organizacional de costos. Trazabilidad de cambios. NULL si nunca fue editada.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME de creación o registro inicial de la estructura organizacional de costos en el sistema.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario VARCHAR(20) que creó o registró la estructura organizacional de costos en el sistema. Trazabilidad de origen.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado BIT de la estructura organizacional (1=Activo, 0=Inactivo). Indica si la unidad de costo está vigente o dada de baja en procesos de facturación, RIPS y costeo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT (FK) de la estructura organizacional de costos padre. Establece la relación jerárquica padre-hijo para construir el árbol de centros de costo, unidades funcionales o departamentos. NULL si es nivel raíz.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'ParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la estructura organizacional padre', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'ParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'ParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre VARCHAR(100) descriptivo de la estructura organizacional de costos (ej: Centro de Atención, Unidad Funcional, Departamento, Servicio). Usado en reportes y visualización.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la estructura organizacional', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(20) único o identificador corto de la estructura organizacional de costos. Facilita búsquedas, RIPS y referencias en facturación y contabilidad.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la estructura organizacional', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico INT IDENTITY (clave primaria) único de cada estructura organizacional de costos. Referenciado como ParentId para construir jerarquía. PK para auditoría y FK.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la estructura organizacional de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estructura organizacional de costos: jerarquía de centros o unidades de costo de la institución (centros de costo, áreas, departamentos). Permite organizar los costos en niveles padre-hijo para reportería financiera y distribución de gastos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'OrganizationalStructureOfCosts';
