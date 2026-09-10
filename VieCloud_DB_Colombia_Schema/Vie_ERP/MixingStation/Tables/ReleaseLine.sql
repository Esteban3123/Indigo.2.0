CREATE TABLE [MixingStation].[ReleaseLine] (
    [Id]                     INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CampaignDetailId]       INT            NOT NULL,
    [WorkingAreaId]          INT            NOT NULL,
    [AirIgnitionTime]        DATETIME       NOT NULL,
    [CPIIgnitionTime]        DATETIME       NOT NULL,
    [EntryMixingStationTime] DATETIME       NOT NULL,
    [PreparationStartTime]   DATETIME       NOT NULL,
    [IsSterile]              BIT            NOT NULL,
    [AdequacyItem1]          BIT            NOT NULL,
    [AdequacyItem2]          BIT            NOT NULL,
    [AdequacyItem3]          BIT            NOT NULL,
    [AdequacyItem4]          BIT            NOT NULL,
    [AdequacyItem5]          BIT            NOT NULL,
    [AdequacyItem6]          BIT            NOT NULL,
    [AdequacyItem7]          BIT            NOT NULL,
    [AdequacyItem8]          BIT            NOT NULL,
    [ConditioningItem1]      BIT            NOT NULL,
    [ConditioningItem2]      BIT            NOT NULL,
    [ConditioningItem3]      BIT            NOT NULL,
    [ConditioningItem4]      BIT            NOT NULL,
    [ConditioningItem5]      BIT            NOT NULL,
    [ConditioningItem6]      BIT            NOT NULL,
    [ConditioningItem7]      BIT            NOT NULL,
    [ConditioningItem8]      BIT            NOT NULL,
    [ApplyItem9]             BIT            NOT NULL,
    [ApplyItem11]            BIT            NOT NULL,
    [ValueItem9]             DECIMAL (5, 2) NOT NULL,
    [ValueItem11]            DECIMAL (5, 2) NOT NULL,
    [Observation]            VARCHAR (500)  NOT NULL,
    [CreationUser]           VARCHAR (20)   NOT NULL,
    [CreationDate]           DATETIME       NOT NULL,
    [ModificationUser]       VARCHAR (20)   NULL,
    [ModificationDate]       DATETIME       NULL,
    [TimeStamp]              ROWVERSION     NOT NULL,
    CONSTRAINT [PK_ReleaseLine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReleaseLine_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_ReleaseLine_WorkingArea] FOREIGN KEY ([WorkingAreaId]) REFERENCES [MixingStation].[WorkingArea] ([Id]),
    CONSTRAINT [IX_ReleaseLine] UNIQUE NONCLUSTERED ([CampaignDetailId] ASC)
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación textual, notas de validación de la línea de liberación (VARCHAR 500, auditoria: CreationUser/Date, ModificationUser/Date)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Velocidad de la cabina de flujo laminar, rango 0-34 ft/min (DECIMAL 5,2), aplica si configuración lo requiere', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ValueItem11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'11. Especifique la velocidad de la cabina (Si aplica): 0 -> 34', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ValueItem11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ValueItem11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diferencial de presión de la cabina de trabajo, rango 0.2 a 3 Pa (DECIMAL 5,2), medición crítica de contención', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ValueItem9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'9. Especifique el diferencial de la presión de la cabina de trabajo: ranvo de 0.2 a 3', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ValueItem9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ValueItem9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si se debe validar y registrar velocidad de cabina según protocolo de liberación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ApplyItem11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'11. Especifique la velocidad de la cabina (Si aplica):', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ApplyItem11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ApplyItem11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si se debe validar y registrar diferencial de presión según protocolo de liberación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ApplyItem9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'9. Especifique el diferencial de la presión de la cabina de trabajo:', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ApplyItem9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ApplyItem9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de identificación del área con etiqueta/código del producto a iniciar en campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'8. ¿El área se encuentra identificada con el producto a iniciar?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de operatividad del sistema de aire acondicionado y cabina de flujo antes de inicio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'7. ¿El sistema de aire y la cabina se encuentran funcionando?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de contenedores de residuos/desechos vacíos y disponibles para usar', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'6. ¿Los recipientes correspondientes a residuos se encuentran libres de estos?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de registro de limpieza completado y firmado antes de liberación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'5. ¿Están diligenciados los registros de limpieza?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de equipos, utensilios e instrumental limpios, estériles e identificados', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'4. ¿Los equipos y utensilios a usar se encuentran limpios e identificados?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de limpieza visual del área de trabajo, libre de suciedad o contaminantes', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'3. ¿Se observa el áre limpia?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de documentación (registros, RIPS, formularios) del lote/campaña anterior removida', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'2. ¿Se encuentra documentación del lote o campaña anterior?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de área sin residuos de producto o insumos de lote/campaña anterior', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. ¿Se encuentra el área libre de producto o insumos del lote o campaña anterior?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de adecuación del área con identificación del producto a iniciar en campaña', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'8. ¿El área se encuentra identificada con el producto a iniciar?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de adecuación del sistema de aire y cabina funcionando correctamente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'7. ¿El sistema de aire y la cabina se encuentran funcionando?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de adecuación de contenedores de residuos vacíos y listos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'6. ¿Los recipientes correspondientes a residuos se encuentran libres de estos?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de adecuación de registros de limpieza completos y diligenciados', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'5. ¿Están diligenciados los registros de limpieza?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de adecuación de equipos, utensilios limpios, estériles e identificados', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'4. ¿Los equipos y utensilios a usar se encuentran limpios e identificados?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de adecuación de limpieza observable del área de trabajo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'3. ¿Se observa el áre limpia?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de adecuación, documentación del lote/campaña anterior removida completamente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'2. ¿Se encuentra documentación del lote o campaña anterior?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validación (BIT) de adecuación del área libre de producto o insumos previos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. ¿Se encuentra el área libre de producto o insumos del lote o campaña anterior?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de condición estéril certificada de la línea de liberación, crítico para procedimientos y productos farmacéuticos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'IsSterile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es estéril', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'IsSterile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'IsSterile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de líneas de liberación en la estación de mezcla (preparación de mezclas farmacéuticas/nutrición parenteral). Almacena los tiempos de encendido, acondicionamiento y verificación de ítems de adecuación y acondicionamiento del área de trabajo para cada campaña de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de línea de liberación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al detalle de la campaña de preparación a la que pertenece esta línea de liberación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del área de trabajo (cabina o zona) donde se realizó la preparación en la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'WorkingAreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'WorkingAreaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de encendido del sistema de aire (flujo laminar o campana de bioseguridad) previo a la preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AirIgnitionTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AirIgnitionTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de encendido del CPI (sistema de presión o equipo de acondicionamiento de ambiente) para la sesión de preparación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'CPIIgnitionTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'CPIIgnitionTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el preparador ingresó a la estación de mezcla para iniciar el proceso.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'EntryMixingStationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'EntryMixingStationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio efectivo de la preparación de la mezcla farmacéutica o nutricional.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'PreparationStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'PreparationStartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró o creó esta línea de liberación en el sistema.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el registro de la línea de liberación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación sobre este registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación realizada sobre el registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo interna del sistema para control de concurrencia y auditoría de cambios en el registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'TimeStamp';
