CREATE TABLE [Contract].[ContractDetail] (
    [Id]                               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractId]                       INT            NOT NULL,
    [ContractName]                     VARCHAR (100)  NOT NULL,
    [ContractNumber]                   VARCHAR (30)   NOT NULL,
    [Type]                             TINYINT        NOT NULL,
    [InitialDate]                      DATETIME       NOT NULL,
    [EndDate]                          DATETIME       NOT NULL,
    [BillingInitialDate]               DATETIME       NOT NULL,
    [BillingEndDate]                   DATETIME       NOT NULL,
    [Legalized]                        BIT            NOT NULL,
    [DateLegalization]                 DATETIME       NULL,
    [RadicatedBillingDate]             DATETIME       NOT NULL,
    [Observations]                     VARCHAR (MAX)  NULL,
    [PrintingMode]                     TINYINT        NOT NULL,
    [TerminationControl]               TINYINT        NOT NULL,
    [NotificationValueType]            TINYINT        NULL,
    [PercentageNotification]           NUMERIC (5, 2) NULL,
    [NotificationValue]                NUMERIC (18)   NULL,
    [NotificationTimeType]             TINYINT        NULL,
    [NotificationDays]                 INT            NULL,
    [PercentageApplyPaymentSoon]       NUMERIC (5, 2) CONSTRAINT [DF_ContractDetail_PercentageApplyPaymentSoon] DEFAULT ((0)) NOT NULL,
    [AgesPortfolioId]                  INT            NULL,
    [ValidRecord]                      BIT            NOT NULL,
    [PermanentObservationOfTheInvoice] VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_ContractDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractDetail_AgesPortfolio] FOREIGN KEY ([AgesPortfolioId]) REFERENCES [Portfolio].[AgesPortfolio] ([Id]),
    CONSTRAINT [FK_ContractDetail_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Contract].[Contract] ([Id])
);




GO





GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación permanente adjunta a la factura, anotación que aparece en el reporte de facturación y RIPS (VARCHAR MAX, PII_contexto)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PermanentObservationOfTheInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación permanente a la factura, observación que se relaciona en el reporte de la factura', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PermanentObservationOfTheInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PermanentObservationOfTheInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de registro válido/activo en el sistema (BIT: 0=inválido, 1=válido)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ValidRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registro válido', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ValidRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ValidRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de edad de cartera/antigüedad de deuda, habilitado tras asignar código (INT, FK referencia)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'AgesPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la edad de cartera, se habilita una vez se asigne un valor al campo código', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'AgesPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'AgesPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento por pronto pago aplicable al contrato (NUMERIC 5,2, default 0%)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PercentageApplyPaymentSoon';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje aplicar pronto pago', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PercentageApplyPaymentSoon';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PercentageApplyPaymentSoon';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de días para disparar notificación de control (INT, rango de anticipación)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NotificationDays', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de control por tiempo: 1=Ninguno, 2=Días de anterioridad (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationTimeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de notificacion para cuando es control por fecha  1 - Ninguno  2 - Dias de anterioridad', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationTimeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationTimeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de valor umbral para activar notificación de terminación (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de notificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje umbral para disparo de notificación contractual (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PercentageNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Notificación de porcentaje', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PercentageNotification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PercentageNotification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de notificación por valor: 1=Ninguna, 2=% contrato, 3=Valor fijo (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationValueType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de notificacion para cuando el control de terminacion es de valor  1 - Ninguna  2 - % del valor del contrato  3 - Valor fijo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationValueType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'NotificationValueType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio de terminación: 1=Ninguno, 2=Fecha, 3=Valor, 4=Fecha O Valor (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'TerminationControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control de terminacion del contrato  1- Ninguno  2 - Fecha Terminacion  3 - Valor contrato  4 - Fecha Terminacion o Valor Contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'TerminationControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'TerminationControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de impresión/generación de reportes: 1=Manual Tarifario, 2=CUPS, 3=Código RIPS, 4=Descripción, 5=CUPS+Descripción, 6=Descripción u CUPS (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PrintingMode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo de impresion de los reportes  1 - Manual Tarifario  2 - CUPS  3 - Codigo RIPS  4 - Descripción Relacionada  5 - CUPS Descripción Relacionada  6- Descripción relacionada ó CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PrintingMode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'PrintingMode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales del contrato, notas administrativas (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación/presentación oficial de facturas ante acreedor (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'RadicatedBillingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de radicación de facturas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'RadicatedBillingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'RadicatedBillingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que el contrato fue legalizado/firmado (DATETIME NULL)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'DateLegalization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de legalizacion del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'DateLegalization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'DateLegalization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de legalización contractual (BIT: 0=no legalizado, 1=legalizado)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Legalized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contrato esta legalizado ?', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Legalized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Legalized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final del período de facturación (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'BillingEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final de facturación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'BillingEndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'BillingEndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial del período de facturación (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'BillingInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicia de facturación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'BillingInitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'BillingInitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de terminación/vencimiento del contrato (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio/vigencia del contrato (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de contrato: 1=Nuevo, 2=Inclusión, 3=Otro Sí, 4=Adición, 5=Prórroga, 6=Otros (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de contrato:    1. Nuevo  2. Inclusión  3. Otro Si  4. Adición  5. Prorroga  6. Otros', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de identificación del contrato (VARCHAR 100)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre/descripción del contrato (VARCHAR 100)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del contrato', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/registro maestro del contrato (INT, FK Contract.Id)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'ContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la línea/detalle del contrato (INT IDENTITY PK)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los contratos comerciales o de prestación de servicios de salud. Contiene la información específica de cada contrato: vigencia, tipo, fechas de facturación, legalización, condiciones de notificación por vencimiento o valor, y observaciones para impresión en facturas.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractDetail';

GO
CREATE NONCLUSTERED INDEX [IX_ContractDetail_ContractId_ValidRecord]
    ON [Contract].[ContractDetail]([ContractId] ASC, [ValidRecord] ASC)
    INCLUDE([TerminationControl]);
