CREATE TABLE [Budget].[RecognitionDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RecognitionId]           INT             NOT NULL,
    [CategoryId]              INT             NOT NULL,
    [RevenueTypeId]           INT             NOT NULL,
    [InitialValue]            NUMERIC (18, 2) NOT NULL,
    [DebitValueModification]  NUMERIC (18, 2) NOT NULL,
    [CreditValueModification] NUMERIC (18, 2) NOT NULL,
    [TotalRecognition]        NUMERIC (18, 2) NOT NULL,
    [ExecutedValue]           NUMERIC (18, 2) NOT NULL,
    [Balance]                 NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_RecognitionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Budget_RecognitionDetail_ValidateValues] CHECK ((([InitialValue]-[DebitValueModification])+[CreditValueModification])=[TotalRecognition] AND ([TotalRecognition]-[ExecutedValue])=[Balance]),
    CONSTRAINT [FK_RecognitionDetail_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_RecognitionDetail_Recognition] FOREIGN KEY ([RecognitionId]) REFERENCES [Budget].[Recognition] ([Id]),
    CONSTRAINT [FK_RecognitionDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id])
);


GO
ALTER TABLE [Budget].[RecognitionDetail] NOCHECK CONSTRAINT [CK_Budget_RecognitionDetail_ValidateValues];




GO
ALTER TABLE [Budget].[RecognitionDetail] NOCHECK CONSTRAINT [CK_Budget_RecognitionDetail_ValidateValues];


GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente del reconocimiento de ingresos (NUMERIC 18,2). Se calcula como TotalRecognition menos ExecutedValue. Representa el valor aún no ejecutado o devengado del rubro presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el saldo del reconocimiento el cual se obtiene de la siguiente manera     TotalRecognition - ExecutedValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ejecutado o devengado del rubro de ingreso (NUMERIC 18,2). Monto ya facturado, cobrado o reconocido contablemente dentro del período presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor ejecutado del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total presupuestal reconocido (NUMERIC 18,2). Se obtiene: InitialValue - DebitValueModification + CreditValueModification. Suma neta del presupuesto inicial con ajustes (débitos y créditos) aplicados al rubro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'TotalRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total del presupesuto el cual se obtiene de la siguiente manera (InitialValue - DebitValueModification + CreditValueModification)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'TotalRecognition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'TotalRecognition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de modificación por crédito al rubro (NUMERIC 18,2). Aumento o ampliación presupuestal aplicada al reconocimiento de ingresos. Ajuste positivo al presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'CreditValueModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de modificacion credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'CreditValueModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'CreditValueModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de modificación por débito al rubro (NUMERIC 18,2). Reducción o disminución presupuestal aplicada al reconocimiento de ingresos. Ajuste negativo del presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'DebitValueModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'DebitValueModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'DebitValueModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor inicial del presupuesto de ingresos (NUMERIC 18,2). Monto base aprobado para el rubro antes de cualquier modificación o ajuste.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de ingreso o fuente de ingresos (INT, FK → Budget.RevenueType). Clasifica la naturaleza del ingreso: cuotas, aportes, transferencias, etc.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de ingreso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la categoría o rubro presupuestal (INT, FK → Budget.Category). Agrupa conceptos de ingresos para control y análisis presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'CategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'CategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'CategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del encabezado o cabecera de reconocimiento (INT, FK → Budget.Recognition). Vincula este detalle al registro maestro de reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera del reconocimiento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'RecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de reconocimiento (INT, PK IDENTITY). Clave primaria que identifica cada línea de detalle presupuestal dentro de un reconocimiento.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del reconocimiento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del reconocimiento presupuestal: registra por categoría y tipo de ingreso los valores iniciales, modificaciones (débitos y créditos), total reconocido, valor ejecutado y saldo disponible de cada reconocimiento de presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail';
