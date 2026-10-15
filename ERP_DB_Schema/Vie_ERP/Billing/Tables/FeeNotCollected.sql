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
ALTER TABLE [Billing].[FeeNotCollected] NOCHECK CONSTRAINT [FK_FeeNotCollected_RevenueControlDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones adicionales por parte del facturador', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Recaudado en la EAPB (0 siempre que sea Sin aporte de cuota)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' 1.Pago realizado en EAPB.                   2.Sin aporte de cuota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'ReportType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del folio asociado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla Sin recaudo de cuota', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'FeeNotCollected', @level2type = N'COLUMN', @level2name = N'Id';

