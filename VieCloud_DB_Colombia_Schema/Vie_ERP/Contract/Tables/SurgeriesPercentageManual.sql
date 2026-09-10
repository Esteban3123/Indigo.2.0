CREATE TABLE [Contract].[SurgeriesPercentageManual] (
    [Id]                         INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RateManualId]               INT            NOT NULL,
    [InterventionType]           TINYINT        NOT NULL,
    [MainHundredPercent]         BIT            NOT NULL,
    [SurgeonPercentage]          NUMERIC (5, 2) NOT NULL,
    [AnesthesiologistPercentage] NUMERIC (5, 2) NOT NULL,
    [AssistantPercentage]        NUMERIC (5, 2) NOT NULL,
    [RoomPercentage]             NUMERIC (5, 2) NOT NULL,
    [MaterialsPercentage]        NUMERIC (5, 2) NOT NULL,
    [PackagesPercentage]         NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_SurgeriesPercentageManual__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SurgeriesPercentageManual_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_SurgeriesPercentageManual__RateManualId]
    ON [Contract].[SurgeriesPercentageManual]([RateManualId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobro por paquetes quirúrgicos. Numérico(5,2). Componente de tarificación manual de procedimientos quirúrgicos en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'PackagesPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje que se va cobrar por paquetes', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'PackagesPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'PackagesPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobro por materiales e insumos quirúrgicos utilizados. Numérico(5,2). Componente de tarificación manual de procedimientos quirúrgicos en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'MaterialsPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje que se va cobrar por materiales', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'MaterialsPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'MaterialsPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobro por uso de sala de cirugía u operatoria. Numérico(5,2). Componente de tarificación manual de procedimientos quirúrgicos en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'RoomPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje que se va cobrar por sala', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'RoomPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'RoomPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobro por personal de ayudantía quirúrgica (asistentes). Numérico(5,2). Componente de tarificación manual de procedimientos quirúrgicos en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'AssistantPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje que se va cobrar por ayudantia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'AssistantPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'AssistantPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobro por servicio de anestesia y anestesiólogo. Numérico(5,2). Componente de tarificación manual de procedimientos quirúrgicos en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'AnesthesiologistPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje que se va cobrar por el anestesiologo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'AnesthesiologistPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'AnesthesiologistPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de cobro por honorarios del cirujano u operador principal. Numérico(5,2). Componente de tarificación manual de procedimientos quirúrgicos en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'SurgeonPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje que se cobrara por el cirujano', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'SurgeonPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'SurgeonPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: procedimiento principal o intervención quirúrgica al 100% de tarifa base contratada. BIT (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'MainHundredPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Procedimiento principal al 100%', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'MainHundredPercent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'MainHundredPercent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de intervención quirúrgica: 2=Bilateral, 3=MIVIE (Múltiple Igual Vía Igual Especialista), 4=MDVIE (Múltiple Diferente Vía Igual Especialista), 5=MIVDE (Múltiple Igual Vía Diferente Especialista), 6=MDVDE (Múltiple Diferente Vía Diferente Especialista), 7=PolitraumaIV (Politrauma Igual Vía), 8=PolitraumaDV (Politrauma Diferente Vía). TINYINT.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'InterventionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de intervencion quirurgica  2 - Bilateral  3 - MIVIE (Multiple Igual Via Igual Especialista)  4 - MDVIE (Multiple Diferente Via Igual Especialista)   5 - MIVDE (Multiple Igual Via Diferente Especialista)   6 - MDVDE (Multiple Diferente Via Diferente Especialista)   7 - PolitraumaIV (Politrauma Igual Via)   8 - PolitraumaDV (Politrauma Diferente Via) ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'InterventionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'InterventionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del manual de tarifas contractual al cual está asociado este conjunto de porcentajes de procedimiento quirúrgico. INT. Referencia: [Contract].[RateManual]([Id]).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del manual de tarifas al cual esta asociado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'RateManualId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'RateManualId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la fila de porcentajes manual de cirugías. INT IDENTITY. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabala de porcentajes', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentajes manuales de distribución de honorarios y costos quirúrgicos por tipo de intervención, asociados a una tarifa manual de contrato. Define cómo se reparte el valor de una cirugía entre cirujano, anestesiólogo, ayudante, sala, materiales y paquetes.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual';
