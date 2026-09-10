CREATE TABLE [Billing].[RevenueControl] (
    [Id]                   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber]      CHAR (10)                                                                        NOT NULL,
    [PatientCode]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FolioQuantity]        TINYINT                                                                          NOT NULL,
    [TopEventFeeModerator] NUMERIC (18, 2)                                                                  CONSTRAINT [DF_RevenueControl_TopEventFeeModerator] DEFAULT ((0)) NOT NULL,
    [TopEventCopay]        NUMERIC (18, 2)                                                                  CONSTRAINT [DF_RevenueControl_TopEventCopay] DEFAULT ((0)) NOT NULL,
    [TopEventFeeRecovery]  NUMERIC (18, 2)                                                                  CONSTRAINT [DF_RevenueControl_TopEventFeeRecovery] DEFAULT ((0)) NOT NULL,
    [TimeStamp]            ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_RevenueControl__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Billing].[RevenueControl].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RevenueControl__AdmissionNumber]
    ON [Billing].[RevenueControl]([AdmissionNumber] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP SQL Server) que registra el instante exacto de creación, modificación o evento en el control de ingresos. Útil para trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope o límite máximo por evento de la cuota de recuperación. Aplicable solo a pacientes vinculados al sistema de salud. Valor numérico en pesos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventFeeRecovery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tope por evento de la cuota de recuperacion, Solo aplica a vinculados', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventFeeRecovery';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventFeeRecovery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope o límite máximo por evento del copago (participación del paciente en costos). Valor numérico en pesos, define responsabilidad financiera del afiliado por atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventCopay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tope por evento a copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventCopay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventCopay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tope o límite máximo por evento de la cuota moderadora (aporte del paciente). Valor numérico en pesos, aplicable a consultas y servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventFeeModerator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tope por evento de l Cuota moderadora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventFeeModerator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventFeeModerator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de folios (hojas) que comprende la orden de servicio o documento de facturación. Tipo TINYINT, rango 0-255.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'FolioQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de folios que va tener la orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'FolioQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'FolioQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (identificación PII enmascarada). Equivalente a cédula, documento o identificación. Proviene de tabla INPACIENT en Crystal Reports.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente, esto se saca de tabla de INPACIENT de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente en el centro de atención. Clave que vincula la atención a la facturación. Proviene de tabla ADINGRESO en Crystal Reports.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK IDENTITY) del registro de control de ingresos en la tabla RevenueControl. Generado automáticamente por SQL Server.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de control', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de ingresos y topes de cobro por admisión del paciente. Registra los límites máximos de cuota moderadora, copago y cuota de recuperación aplicables a cada ingreso, junto con la cantidad de folios asociados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl';
