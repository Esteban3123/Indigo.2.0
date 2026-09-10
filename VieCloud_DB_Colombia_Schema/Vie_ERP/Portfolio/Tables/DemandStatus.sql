CREATE TABLE [Portfolio].[DemandStatus] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Description]      VARCHAR (100) NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_DemandStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de modificación (DATETIME, nullable): timestamp del último cambio al estado de demanda (descripción, código o activación); auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion del estado de demanda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario modificador (VARCHAR 20, nullable): login del usuario del sistema que actualizó por última vez el estado de demanda; trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifica el estado de demanda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME): timestamp cuando se insertó el estado de demanda en la tabla; auditoría e histórico de cambios de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del estado de demanda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario creador (VARCHAR 20): login o identificación del usuario del sistema que registró inicialmente el estado de demanda; trazabilidad de origen.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que crea el estado de demanda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de activación (BIT): 1=Activo (estado vigente en uso), 0=Inactivo (estado deshabilitado o deprecado); controla disponibilidad del estado para nuevas demandas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del estado de demanda: 0|Inactivo, 1|Activo ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 100) del estado de demanda: nombre completo o etiqueta legible del estado para reportes, interfaces y auditoría de cambios en demandas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del estado de demanda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (VARCHAR 20) del estado de demanda: identificador abreviado para búsqueda y clasificación de estados (ej: PENDIENTE, APROBADA, RECHAZADA, GLOSA).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de estado de demanda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del estado de demanda, clave primaria para referencias en requisiciones, solicitudes de atención o gestión de demandas en cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de estado de demanda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de estados posibles de la demanda en cartera o portafolio (por ejemplo: pendiente, aprobado, rechazado). Permite clasificar y controlar el flujo de estados de las solicitudes o casos de demanda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DemandStatus';
