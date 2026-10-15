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
ALTER TABLE [MixingStation].[ReleaseLine] NOCHECK CONSTRAINT [FK_ReleaseLine_CampaignDetail];


GO
ALTER TABLE [MixingStation].[ReleaseLine] NOCHECK CONSTRAINT [FK_ReleaseLine_WorkingArea];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '11. Especifique la velocidad de la cabina (Si aplica): 0 -> 34', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ValueItem11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '9. Especifique el diferencial de la presión de la cabina de trabajo: ranvo de 0.2 a 3', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ValueItem9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '11. Especifique la velocidad de la cabina (Si aplica):', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ApplyItem11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '9. Especifique el diferencial de la presión de la cabina de trabajo:', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ApplyItem9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '8. ¿El área se encuentra identificada con el producto a iniciar?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '7. ¿El sistema de aire y la cabina se encuentran funcionando?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '6. ¿Los recipientes correspondientes a residuos se encuentran libres de estos?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '5. ¿Están diligenciados los registros de limpieza?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '4. ¿Los equipos y utensilios a usar se encuentran limpios e identificados?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '3. ¿Se observa el áre limpia?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '2. ¿Se encuentra documentación del lote o campaña anterior?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. ¿Se encuentra el área libre de producto o insumos del lote o campaña anterior?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'ConditioningItem1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '8. ¿El área se encuentra identificada con el producto a iniciar?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '7. ¿El sistema de aire y la cabina se encuentran funcionando?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '6. ¿Los recipientes correspondientes a residuos se encuentran libres de estos?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '5. ¿Están diligenciados los registros de limpieza?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '4. ¿Los equipos y utensilios a usar se encuentran limpios e identificados?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '3. ¿Se observa el áre limpia?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '2. ¿Se encuentra documentación del lote o campaña anterior?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1. ¿Se encuentra el área libre de producto o insumos del lote o campaña anterior?', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'AdequacyItem1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es estéril', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ReleaseLine', @level2type = N'COLUMN', @level2name = N'IsSterile';

