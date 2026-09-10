CREATE TABLE [Treasury].[Cards] (
    [Id]                            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                          VARCHAR (20)  NOT NULL,
    [Name]                          VARCHAR (100) NOT NULL,
    [IdThirdParty]                  INT           NOT NULL,
    [GetCostCenter]                 TINYINT       CONSTRAINT [DF_Cards_GetCostCenter] DEFAULT ((1)) NOT NULL,
    [IdCashReceiptConceptCommision] INT           NOT NULL,
    [CommisionCostCenterId]         INT           NULL,
    [IdRetentionConceptCommision]   INT           NOT NULL,
    [IdCashReceiptConceptRTF]       INT           NOT NULL,
    [RTFCostCenterId]               INT           NULL,
    [IdRetentionConceptRTF]         INT           NOT NULL,
    [IdCashReceiptConceptICA]       INT           NOT NULL,
    [ICACostCenterId]               INT           NULL,
    [IdRetentionConceptICA]         INT           NOT NULL,
    [Status]                        BIT           CONSTRAINT [DF_Card_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                  VARCHAR (20)  NOT NULL,
    [CreationDate]                  DATETIME      NOT NULL,
    [ModificationUser]              VARCHAR (20)  NULL,
    [ModificationDate]              DATETIME      NULL,
    [TimeStamp]                     ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Cards__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Card_ThirdParty] FOREIGN KEY ([IdThirdParty]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_Cards_CashReceiptConcepts_Commision] FOREIGN KEY ([IdCashReceiptConceptCommision]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_Cards_CashReceiptConcepts_ICA] FOREIGN KEY ([IdCashReceiptConceptICA]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_Cards_CashReceiptConcepts_RTF] FOREIGN KEY ([IdCashReceiptConceptRTF]) REFERENCES [Treasury].[CashReceiptConcepts] ([Id]),
    CONSTRAINT [FK_Cards_RetentionConcepts_Commision] FOREIGN KEY ([IdRetentionConceptCommision]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_Cards_RetentionConcepts_ICA] FOREIGN KEY ([IdRetentionConceptICA]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_Cards_RetentionConcepts_RTF] FOREIGN KEY ([IdRetentionConceptRTF]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Cards__Code]
    ON [Treasury].[Cards]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP SQL Server) que registra automáticamente el instante exacto de creación, modificación o cambio de estado del registro de tarjeta. Usado para auditoría y control de versiones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se realizó la última modificación del registro de la tarjeta. NULL si nunca fue modificada desde su creación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que realizó la última modificación del registro. Null si el registro no ha sido modificado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el registro de la tarjeta en el sistema.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (VARCHAR 20) del usuario que creó el registro de la tarjeta en el sistema.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado booleano (BIT) del registro: 1=Activo, 0=Inactivo. Controla si la tarjeta está disponible para operaciones de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del concepto de retención para Impuesto de Industria y Comercio (ICA) asociado a la tarjeta. Referencia a [GeneralLedger].[RetentionConcepts].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id concepto de retencion de reteica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo (INT, nullable) asignado a la cuenta contable del concepto de ICA. Se completa solo si el concepto maneja distribución por centro de costo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ICACostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de costo para la cuenta contable de la ICA, solo se solicita si la cuenta contable del concepto de ICA maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ICACostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ICACostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del concepto de recibo de efectivo para ICA vinculado a la tarjeta. Referencia a [Treasury].[CashReceiptConcepts].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de retencion ICA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del concepto de retención en la Fuente (Retefuente) asociado a la tarjeta. Referencia a [GeneralLedger].[RetentionConcepts].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto de retencion de retefuente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo (INT, nullable) para la cuenta contable de Retencion en la Fuente. Se solicita solo si el concepto de Retefuente maneja centro de costo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'RTFCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de costo para la cuenta contable de la Retencion en la fuente, solo se solicita si la cuenta contable del concepto de retencion en la fuente maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'RTFCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'RTFCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del concepto de recibo de efectivo para Retencion en la Fuente asociado a la tarjeta. Referencia a [Treasury].[CashReceiptConcepts].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de ICA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptRTF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del concepto de retención de comisión aplicable a la tarjeta. Referencia a [GeneralLedger].[RetentionConcepts].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptCommision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del concepto de retención Commision', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptCommision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptCommision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de costo (INT, nullable) para la cuenta contable de comisión. Se incluye solo si el concepto de comisión requiere distribución por centro de costo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CommisionCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de costo para la cuenta contable de la comision, solo se solicita si la cuenta contable del concepto de comision maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CommisionCostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CommisionCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del concepto de recibo de efectivo para comisión de la tarjeta. Referencia a [Treasury].[CashReceiptConcepts].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptCommision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Comisión de concepto de recibo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptCommision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptCommision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estrategia de obtención del centro de costo (TINYINT): 1=Centro único (fijo), 2=Centro por unidad operativa. Determina cómo se asignan centros en operaciones.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'GetCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se obtiene el centro de costo   1 - Centro de costo Unico  2 - Centro de costo Por Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'GetCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'GetCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del tercero/entidad proveedora de la tarjeta. Referencia a [Common].[ThirdParty].', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdThirdParty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) de la tarjeta (ej: ''''Visa Tesorería'''', ''''MasterCard Operativa''''). Campo de búsqueda humana.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) de identificación de la tarjeta para búsquedas y referencias en operaciones de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY) del registro de tarjeta. Clave primaria con auto-incremento. Referenciado como FK en otras tablas de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tarjetas de crédito y débito configuradas para recaudo en tesorería. Define los conceptos contables, retenciones y centros de costo asociados a comisiones, RTF e ICA por cada tipo de tarjeta.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards';
