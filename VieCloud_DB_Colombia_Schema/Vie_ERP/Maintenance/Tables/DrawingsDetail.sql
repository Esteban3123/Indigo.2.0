CREATE TABLE [Maintenance].[DrawingsDetail] (
    [Id]                   INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentReception] INT       NOT NULL,
    [Drawings]             CHAR (10) NOT NULL,
    CONSTRAINT [PK_DrawingsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DrawingsDetail_EquipmentRegistration] FOREIGN KEY ([IdEquipmentReception]) REFERENCES [Maintenance].[EquipmentRegistration] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de plano técnico seleccionado: 1=Eléctrico, 2=Electrónico, 3=Hidráulico, 4=Neumático, 5=Mecánico. Clasificación de documentación técnica del equipo médico o industrial. Char(10), clave para identificar especialidad de mantenimiento.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'Drawings';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plano Seleccionado 1-Electrico 2- Electronicos 3-Hidraulicos 4-Neumaticos 5-Mecanicos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'Drawings';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'Drawings';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la recepción del equipo en mantenimiento. Referencia a Maintenance.EquipmentRegistration(Id). Vincula el detalle de planos al registro de ingreso y acta de recepción del equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la recepcion del equipo detalle', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'IdEquipmentReception';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (Identity INT) de cada registro de detalle de planos. Clave primaria de la tabla DrawingsDetail. Secuencia de 1 en 1.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de detalle de planos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de planos o diagramas asociados a recepciones de equipos en mantenimiento. Registra qué planos técnicos (drawings) están vinculados a cada recepción de equipo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'DrawingsDetail';
