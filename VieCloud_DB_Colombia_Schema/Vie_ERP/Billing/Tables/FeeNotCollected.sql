CREATE TABLE [Billing].[FeeNotCollected] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [RevenueControlDetailId] INT             NOT NULL,
    [ReportType]             TINYINT         NOT NULL,
    [Value]                  DECIMAL (20, 2) NOT NULL,
    [Observations]           VARCHAR (200)   NOT NULL,
    [CreationUser]           VARCHAR (20)    NOT NULL,
    [CreationDate]           DATETIME        NOT NULL,
    [TimeStamp]              ROWVERSION      NOT NULL,
    CONSTRAINT [PK_FeeNotCollected__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FeeNotCollected_RevenueControlDetail] FOREIGN KEY ([RevenueControlDetailId]) REFERENCES [Billing].[RevenueControlDetail] ([Id]),
    CONSTRAINT [UQ_FeeNotCollected_RevenueControlDetailId] UNIQUE NONCLUSTERED ([RevenueControlDetailId] ASC)
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo del registro (timestamp SQL Server). Marca automática de versión de fila para control concurrente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de tiempo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de no recaudo. Permite rastrear cuándo se registró la cuota sin cobrar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (identificación, login) que creó el registro de no recaudo. Auditoría de facturador/operario de billing.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones adicionales del facturador sobre causas del no recaudo, glosa, objeción o motivo de ausencia de aporte de cuota.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones adicionales por parte del facturador', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario no recaudado en la EAPB (aseguradora). Siempre 0 cuando es ''''Sin aporte de cuota''''; refleja la cartera pendiente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Recaudado en la EAPB (0 siempre que sea Sin aporte de cuota)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de reporte/clasificación: 1=Pago realizado en EAPB, 2=Sin aporte de cuota. Categoriza el motivo del no recaudo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'ReportType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' 1.Pago realizado en EAPB.                   2.Sin aporte de cuota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'ReportType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'ReportType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del folio/detalle de control de ingresos asociado. Referencia a RevenueControlDetail.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del folio asociado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de no recaudo de cuota. Clave primaria para búsquedas y auditoría.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla Sin recaudo de cuota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los honorarios o valores facturables que no pudieron ser cobrados, asociados al detalle de control de ingresos. Permite hacer seguimiento de glosas, cartera incobrable o conceptos no recaudados, con su tipo de reporte, monto, observaciones y datos de auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected';
