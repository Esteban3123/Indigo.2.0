CREATE TABLE [Inventory].[DrugInteraction] (
    [Id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParentDCIId] INT           NOT NULL,
    [DCIId]       INT           NULL,
    [RiskLevel]   TINYINT       NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [ATCEntityId] INT           NULL,
    CONSTRAINT [PK_DrugInteraction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DrugInteraction_ATCEntity] FOREIGN KEY ([ATCEntityId]) REFERENCES [Inventory].[ATCEntity] ([Id]),
    CONSTRAINT [FK_DrugInteraction_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_DrugInteraction_DCI_Parent] FOREIGN KEY ([ParentDCIId]) REFERENCES [Inventory].[DCI] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad ATC (Clasificación Anatómica, Terapéutica, Química) asociada a la interacción farmacológica; referencia FK a Inventory.ATCEntity', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la entidad del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la interacción entre medicamentos, síntomas, efectos adversos o contraindicaciones clínicas observadas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la interaccion con el medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de severidad de la interacción farmacológica: 1=Graves (riesgo alto, contraindicado), 2=Moderadas (requiere ajuste o monitoreo), 3=Leves (sin impacto clínico significativo)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'RiskLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el nivel de riesgo de la interaccion  1 - Graves  2 - Moderadas  3 - Leves', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'RiskLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'RiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del DCI (Denominación Común Internacional/fármaco) que interactúa con el medicamento padre; referencia FK a Inventory.DCI, puede ser nulo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del dci con el que interactua', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'DCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del DCI padre (fármaco principal) que presenta interacciones con otros medicamentos; referencia FK a Inventory.DCI, clave en la relación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'ParentDCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del dci padre, el cual tiene interaccion con otros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'ParentDCIId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'ParentDCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la interacción farmacológica registrada (INT IDENTITY, PK); clave primaria de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la interaccion del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de interacciones entre medicamentos (por Denominación Común Internacional - DCI), indicando el nivel de riesgo clínico y la descripción de la interacción. Permite identificar combinaciones peligrosas o contraindicadas entre fármacos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction';
