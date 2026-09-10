CREATE TABLE [Billing].[InvoiceEntityCapitatedGroupers] (
    [Id]                       INT           IDENTITY (1, 1) NOT NULL,
    [InvoiceEntityCapitatedId] INT           NOT NULL,
    [GroupersId]               INT           NOT NULL,
    [Code]                     VARCHAR (20)  NOT NULL,
    [Description]              VARCHAR (100) NOT NULL,
    [UserMin]                  INT           NOT NULL,
    [UserMax]                  INT           NOT NULL,
    [ProjectCME]               NUMERIC (18)  NOT NULL,
    [TotalContract]            NUMERIC (18)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceEntityCapitatedGroupers_Groupers] FOREIGN KEY ([GroupersId]) REFERENCES [Contract].[Groupers] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitatedGroupers_InvoiceEntityCapitated] FOREIGN KEY ([InvoiceEntityCapitatedId]) REFERENCES [Billing].[InvoiceEntityCapitated] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total del contrato capitado del agrupador al momento de guardar la factura. Valor numérico (NUMERIC 18) que representa el valor contractual consolidado para facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'TotalContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'TotalContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'TotalContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor proyectado CME (Costo Medio por Evento) del agrupador al momento de guardar la factura. Referencia histórica del costo estimado capturado en el documento de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'ProjectCME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ProjectCME del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'ProjectCME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'ProjectCME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad máxima de usuarios/pacientes cubiertos por el agrupador al momento de guardar la factura. Límite superior de afiliados para ese grupo de servicios capitado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'UserMax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'UserMax del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'UserMax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'UserMax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad mínima de usuarios/pacientes cubiertos por el agrupador al momento de guardar la factura. Límite inferior de afiliados para garantizar viabilidad del contrato capitado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'UserMin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'UserMin del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'UserMin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'UserMin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del agrupador (grupo de procedimientos/servicios) al momento de guardar la factura. Identifica el nombre o denominación del grupo facturado bajo esquema capitado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del agrupador al momento de guardar la factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del agrupador (VARCHAR 20). Identificador alfanumérico que clasifica el grupo de servicios o procedimientos para facturación capitada y búsqueda en RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del agrupador', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del agrupador en la tabla Contract.Groupers. Referencia al grupo de procedimientos/servicios definido en contrato capitado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'GroupersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del agrupador', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'GroupersId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'GroupersId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la factura capitada (monto fijo) en Billing.InvoiceEntityCapitated. Vincula este registro al documento de facturación de atención con pago capitado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la factura de monto fijo (Capitada)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'InvoiceEntityCapitatedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupa los agrupadores tarifarios asociados a facturas de entidades en modalidad de capitación, definiendo rangos de usuarios, proyección de costos médicos esperados (CME) y valores totales contratados por agrupador.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro del agrupador capitado en la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceEntityCapitatedGroupers', @level2type = N'COLUMN', @level2name = N'Id';
