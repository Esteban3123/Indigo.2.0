CREATE TABLE [Inventory].[ProductSubGroup] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [HandlesBatch]     BIT           NOT NULL,
    [HandlesExpiry]    BIT           NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ProductSubGroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductSubGroup__Code]
    ON [Inventory].[ProductSubGroup]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (SQL Server) que registra automáticamente el instante exacto de creación, modificación o cambio de estado del subgrupo de producto. Se actualiza en cada evento de auditoría.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro del subgrupo de producto. Registro de auditoría para trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación (VARCHAR 20) del operador que realizó la última modificación o actualización del subgrupo de producto. Campo de auditoría.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación inicial del registro del subgrupo de producto. Marca el origen temporal del registro en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación (VARCHAR 20) del operador que creó originalmente el registro del subgrupo de producto. Campo de auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del subgrupo de producto: 1=Activo (disponible para uso en inventario), 0=Inactivo (deshabilitado). Controla la vigencia operativa del grupo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro 1 - Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si este subgrupo de producto requiere gestión de fecha de vencimiento o expiración. Aplica para productos farmacéuticos, alimentos, reactivos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'HandlesExpiry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'HandlesExpiry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'HandlesExpiry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si este subgrupo de producto requiere control y seguimiento de lotes o números de lote. Aplica para trazabilidad de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'HandlesBatch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maneja lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'HandlesBatch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'HandlesBatch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del subgrupo de producto (VARCHAR 100). Identificador legible del subgrupo dentro de la categorización de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del subgrupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único del subgrupo de producto (VARCHAR 20). Identificador de negocio para clasificación y búsqueda rápida de subgrupos en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del subgrupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de subgrupo de producto. Clave primaria surrogate de la tabla ProductSubGroup en Inventory.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de subgrupos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subgrupos de productos del inventario: clasificación secundaria de artículos (medicamentos, insumos, etc.) que indica si el subgrupo maneja lotes y fechas de vencimiento. Permite organizar el catálogo de inventario por categorías más específicas dentro de un grupo principal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductSubGroup';
