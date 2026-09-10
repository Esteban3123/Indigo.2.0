CREATE TABLE [MedicalFees].[MedicalFeesSecuenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SequenseMedicalFeesId] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_SequenseMedicalFeesD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseMedicalFeesD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseMedicalFeesD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseMedicalFeesD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseMedicalFeesD_SequenseMedicalFeesC] FOREIGN KEY ([SequenseMedicalFeesId]) REFERENCES [MedicalFees].[MedicalFeesSecuence] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente número a generar en la secuencia de honorarios médicos; contador incremental (BIGINT) que registra el próximo correlativo a asignar en la numeración de facturas, recibos o documentos de cobro.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (centro de atención, sede, consultorio) asignada a la secuencia de honorarios; aplica solo cuando el ámbito es por Unidad Operativa (UO), de lo contrario es NULL. Referencia a Common.OperatingUnit.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la secuencia base o secuencia maestra en Common.Sequense; es la secuencia padre que controla la numeración general de documentos de cobro o aranceles médicos.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o secuencia de honorarios médicos padre (MedicalFees.MedicalFeesSecuence); vincula este detalle a su configuración principal de aranceles, tarifas o fees.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'SequenseMedicalFeesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'SequenseMedicalFeesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'SequenseMedicalFeesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de detalle de secuencia de honorarios médicos; clave primaria de esta tabla de asociación entre secuencias y unidades operativas.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las secuencias de tarifas médicas: registra cada paso o ítem dentro de una secuencia de honorarios médicos, indicando a qué unidad operativa pertenece y cuál es el siguiente número correlativo de la serie.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'MedicalFeesSecuenceDetail';
