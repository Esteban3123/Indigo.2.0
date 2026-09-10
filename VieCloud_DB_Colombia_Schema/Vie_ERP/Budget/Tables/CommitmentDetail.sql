CREATE TABLE [Budget].[CommitmentDetail] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CommitmentId]            INT          NOT NULL,
    [AvailabilityDetailId]    INT          NULL,
    [CategoryId]              INT          NOT NULL,
    [RevenueTypeId]           INT          NOT NULL,
    [ExpiredDate]             DATETIME     NOT NULL,
    [InitialValue]            NUMERIC (18) NOT NULL,
    [DebitModificationValue]  NUMERIC (18) NOT NULL,
    [CreditModificationValue] NUMERIC (18) NOT NULL,
    [TotalCommitment]         NUMERIC (18) NOT NULL,
    [ExecutedValue]           NUMERIC (18) NOT NULL,
    [Balance]                 NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CommitmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Budget_CommitmentDetail_ValidateValues] CHECK ((([InitialValue]-[DebitModificationValue])+[CreditModificationValue])=[TotalCommitment] AND ([TotalCommitment]-[ExecutedValue])=[Balance] AND NOT ([TotalCommitment]<(0) OR [ExecutedValue]<(0) OR [Balance]<(0))),
    CONSTRAINT [FK_CommitmentDetail_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_CommitmentDetail_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_CommitmentDetail_Commitment] FOREIGN KEY ([CommitmentId]) REFERENCES [Budget].[Commitment] ([Id]),
    CONSTRAINT [FK_CommitmentDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id])
);


GO
ALTER TABLE [Budget].[CommitmentDetail] NOCHECK CONSTRAINT [CK_Budget_CommitmentDetail_ValidateValues];




GO
ALTER TABLE [Budget].[CommitmentDetail] NOCHECK CONSTRAINT [CK_Budget_CommitmentDetail_ValidateValues];


GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo disponible del compromiso presupuestal (NUMERIC 18). Calculado como TotalCommitment menos ExecutedValue. Representa recursos aún no ejecutados o pendientes de ejecución en el rubro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Saldo del compromiso  TotalCommitment - ExecutedValue ', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto ejecutado, devengado o gastado del compromiso presupuestal (NUMERIC 18). Valor real ejercido contra el presupuesto disponible del rubro o categoría.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor ejecutado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'ExecutedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del compromiso presupuestal (NUMERIC 18). Resultado de: InitialValue menos DebitModificaciones más CreditModificaciones. Base para cálculo de balance y ejecución.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'TotalCommitment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total del compromiso, el cual se calcula asi  InitialValue - DebitModificationValue + CreditModificationValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'TotalCommitment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'TotalCommitment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Incremento o ajuste de crédito al rubro presupuestal (NUMERIC 18). Suma de ampliaciones, traslados de entrada o ajustes positivos aplicados al compromiso inicial.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor modificacion credito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CreditModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reducción o ajuste de débito al rubro presupuestal (NUMERIC 18). Monto de reducciones, traslados de salida o ajustes negativos contra el compromiso inicial.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de modificacion debito al rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'DebitModificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto inicial asignado al compromiso presupuestal (NUMERIC 18). Presupuesto base antes de cualquier modificación, traslado o ajuste.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'InitialValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento o cierre del compromiso presupuestal (DATETIME). Plazo máximo para ejecución de gasto o disponibilidad de recursos en este rubro.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de ingreso, gasto o categoría financiera (FK → Budget.RevenueType). Clasifica el flujo presupuestal según naturaleza: gasto operativo, inversión, transferencia, etc.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de gastos', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del rubro o categoría presupuestal (FK → Budget.Category). Agrupa conceptos contables y clasificación de gasto: salarios, servicios, bienes, procedimientos, etc.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del rubro', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CategoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de disponibilidad presupuestal asociado (FK → Budget.AvailabilityDetail, opcional). Vincula a disponibilidad de fondos o límites de gasto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id detalle disponibilidad', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'AvailabilityDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del encabezado o cabecera del compromiso presupuestal (FK → Budget.Commitment). Agrupa todos los detalles de un mismo compromiso o contrato.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CommitmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cabecera compromiso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CommitmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'CommitmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle del compromiso presupuestal (INT IDENTITY). Clave primaria para trazabilidad de líneas de ejecución presupuestal.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del compromiso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de compromisos presupuestales: registra el desglose por categoría y tipo de ingreso de cada compromiso, con sus valores iniciales, modificaciones (débitos y créditos), total comprometido, valor ejecutado y saldo disponible.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CommitmentDetail';
