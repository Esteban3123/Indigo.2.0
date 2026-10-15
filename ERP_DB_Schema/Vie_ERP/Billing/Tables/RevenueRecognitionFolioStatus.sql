CREATE TABLE [Billing].[RevenueRecognitionFolioStatus] (
    [Id]                     INT IDENTITY (1, 1) NOT NULL,
    [RevenueControlDetailId] INT NOT NULL,
    [Status]                 INT NOT NULL,
    CONSTRAINT [PK_RevenueRecognitionFolioStatus] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el estado del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionFolioStatus', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de control de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionFolioStatus', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del estado del folio de reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionFolioStatus', @level2type = N'COLUMN', @level2name = N'Id';

