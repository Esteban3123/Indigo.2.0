CREATE TABLE [Contract].[ContractDetailNovelty] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractDetailId] INT           NOT NULL,
    [NoveltyDate]      DATETIME      NOT NULL,
    [NoveltySource]    TINYINT       NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [Description]      VARCHAR (500) NULL,
    [Status]           TINYINT       NOT NULL,
    CONSTRAINT [PK_ContractDetailNovelty] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractDetailNovelty_ContractDetail] FOREIGN KEY ([ContractDetailId]) REFERENCES [Contract].[ContractDetail] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la novedad (TINYINT): 1=Aprobada, 2=Rechazada, 3=Pendiente, 4=En Proceso, 5=Terminada. Etapa actual del tramite de la novedad en el flujo de aprobación y gestión contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la novedad:  1. Aprobada  2. Rechazada  3. Pendiente  4. En Proceso  5. Terminada', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la novedad (VARCHAR 500, nullable). Contexto ampliado del cambio: motivo, impacto en cobertura, justificación o detalles adicionales del evento contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de la novedad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o título de la novedad (VARCHAR 200). Denominación corta del tipo de cambio: ampliación, suspensión, glosa, ajuste de cobertura, recalificación, etc.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la novedad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la novedad (TINYINT): 1=Cliente, 2=Prestador. Identifica quién reporta o genera el cambio: entidad contratante o proveedor de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'NoveltySource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen de la novedad:  1. Cliente  2. Prestador', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'NoveltySource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'NoveltySource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la novedad (DATETIME). Marca temporal de cuándo ocurrió el cambio, adición, modificación o evento en el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'NoveltyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la novedad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'NoveltyDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'NoveltyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del contrato (FK → Contract.ContractDetail.Id). Referencia a la línea o rubro del contrato afectado por la novedad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del contato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'ContractDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'ContractDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de novedad del contrato (PK, INT IDENTITY). Clave primaria para auditoría y trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novedades o cambios registrados sobre el detalle de un contrato: modificaciones, actualizaciones o eventos que afectan las condiciones pactadas en cada ítem contractual, incluyendo la fuente del cambio, su descripción y estado actual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetailNovelty';
