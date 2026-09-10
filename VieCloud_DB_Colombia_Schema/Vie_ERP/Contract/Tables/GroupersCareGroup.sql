CREATE TABLE [Contract].[GroupersCareGroup] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]  INT          NOT NULL,
    [GroupersId]   INT          NOT NULL,
    [EjectEvent]   INT          CONSTRAINT [DF_GroupersCareGroup_EjectEvent] DEFAULT ((0)) NOT NULL,
    [RealCME]      NUMERIC (18) CONSTRAINT [DF_GroupersCareGroup_RealCME] DEFAULT ((0)) NOT NULL,
    [TotalEject]   NUMERIC (18) CONSTRAINT [DF_GroupersCareGroup_TotalEject] DEFAULT ((0)) NOT NULL,
    [DocumentDate] DATE         NOT NULL,
    CONSTRAINT [PK_GroupersCareGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCareGroup_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_GroupersCareGroup_Groupers] FOREIGN KEY ([GroupersId]) REFERENCES [Contract].[Groupers] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento de asignación o procesamiento de agrupadores en el grupo de atención (DATE). Referencia de la fecha de vigencia o documento contable.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha del documento', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de expulsión o rechazo de eventos/registros del agrupador en el grupo de atención (NUMERIC 18). Cantidad acumulada de eventos excluidos o no procesados.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'TotalEject';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el total de expulsión', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'TotalEject';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'TotalEject';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número real de CME (Código de Morbilidad y Estadística) o unidades de valor asignadas al agrupador en el grupo de atención (NUMERIC 18). Cálculo base para facturación y análisis de costo.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'RealCME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el número de CME', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'RealCME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'RealCME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contador de eventos de expulsión o rechazo generados por el agrupador en el grupo de atención (INT, default 0). Registra cuántos eventos fueron rechazados o no procesados.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'EjectEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece evento de expulsión', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'EjectEvent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'EjectEvent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del agrupador o motor de agrupación de diagnósticos/procedimientos (INT). Referencia a Groupers para identificar el algoritmo o sistema de clasificación utilizado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'GroupersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Groupers', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'GroupersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'GroupersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del grupo de atención, unidad funcional o centro de atención asociado (INT). Referencia a CareGroup para relacionar con contrato, paciente o centro médico.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Care Group', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la relación agrupador-grupo de atención (INT IDENTITY). Clave primaria para registros de asignación y procesamiento de agrupadores en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre agrupadores de contrato y grupos de atención (care groups), registrando los eventos de eyección, el CME real y el total ejecutado por fecha de documento. Permite controlar la asociación entre agrupadores tarifarios o de facturación y los grupos de atención dentro de un contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'GroupersCareGroup';
