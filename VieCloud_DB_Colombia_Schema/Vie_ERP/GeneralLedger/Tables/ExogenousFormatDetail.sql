CREATE TABLE [GeneralLedger].[ExogenousFormatDetail] (
    [Id]                INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ExogenousFormatId] INT     NOT NULL,
    [Concept]           INT     NULL,
    [ConceptType]       TINYINT NULL,
    [MainAccountId]     INT     NOT NULL,
    [Nature]            TINYINT NOT NULL,
    [ThirdPartyBy]      TINYINT NULL,
    CONSTRAINT [PK_ExogenousFormatDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExogenousFormatDetail_ExogenousFormat] FOREIGN KEY ([ExogenousFormatId]) REFERENCES [GeneralLedger].[ExogenousFormat] ([Id]),
    CONSTRAINT [FK_ExogenousFormatDetail_MainAccount] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del tercero/acreedor/deudor en formato exógeno: 1=Documento contable (voucher, asiento), 2=Documento origen (factura, recibo, comprobante)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica de donde se obtendrá el tercero  1 - Documento contable  2 - Documento origen', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza contable para extracción de datos: 1=Débito, 2=Crédito, 3=Saldo puntual, 4=Saldo acumulado, 5=Base gravable (impuestos), 6=Valor facturado (monto total)', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza de la cual obtener la información:  1 - Débito  2 - Crédito  3 - Saldo  4 - Saldo Acumulado  5 - Base Gravable  6 - Valor Facturado', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal parametrizada (FK a MainAccounts), utilizada en asientos y estados financieros', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable parametrizada', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto del formato exógeno, categoría o clasificación del dato a extraer', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna del formato, si este lo maneja', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto específico del formato exógeno, valor o elemento de información a mapear', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Concept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto del formato, si este lo maneja', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Concept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Concept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formato exógeno padre (FK a ExogenousFormat), define estructura de importación de datos externos', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ExogenousFormatId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ExogenousFormatId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'ExogenousFormatId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria, identificador único del detalle/línea del formato exógeno', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del formato', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los formatos exógenos del libro mayor general. Guarda la configuración de cada concepto de información exógena (DIAN), especificando la cuenta contable principal, la naturaleza del movimiento y cómo se identifica al tercero para cada formato de reporte tributario.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'ExogenousFormatDetail';
