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
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Card_ThirdParty];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_CashReceiptConcepts_Commision];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_CashReceiptConcepts_ICA];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_CashReceiptConcepts_RTF];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_RetentionConcepts_Commision];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_RetentionConcepts_ICA];


GO
ALTER TABLE [Treasury].[Cards] NOCHECK CONSTRAINT [FK_Cards_RetentionConcepts_RTF];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Cards__Code]
    ON [Treasury].[Cards]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: True-Activo, False-Inactivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id concepto de retencion de reteica', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de costo para la cuenta contable de la ICA, solo se solicita si la cuenta contable del concepto de ICA maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'ICACostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de retencion ICA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Concepto de retencion de retefuente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de costo para la cuenta contable de la Retencion en la fuente, solo se solicita si la cuenta contable del concepto de retencion en la fuente maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'RTFCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta de ICA', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptRTF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del concepto de retención Commision', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdRetentionConceptCommision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de costo para la cuenta contable de la comision, solo se solicita si la cuenta contable del concepto de comision maneja centro de costo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'CommisionCostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Comisión de concepto de recibo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdCashReceiptConceptCommision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se obtiene el centro de costo   1 - Centro de costo Unico  2 - Centro de costo Por Unidad Operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'GetCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'IdThirdParty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la tarjeta', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'Cards', @level2type = N'COLUMN', @level2name = N'Id';

