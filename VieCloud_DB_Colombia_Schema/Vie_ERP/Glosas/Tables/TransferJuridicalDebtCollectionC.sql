CREATE TABLE [Glosas].[TransferJuridicalDebtCollectionC] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [JuridicalTransferConsecutive] NUMERIC (18)  NOT NULL,
    [DocumentNumber]               VARCHAR (50)  NOT NULL,
    [CustomerId]                   INT           NOT NULL,
    [JuridicalTransferDate]        DATETIME      NOT NULL,
    [DocumentDate]                 DATETIME      NOT NULL,
    [Comment]                      VARCHAR (500) NULL,
    [State]                        CHAR (1)      NOT NULL,
    [CreationUser]                 VARCHAR (20)  CONSTRAINT [DF_TransferJuridicalDebtCollectionC_CreationUser] DEFAULT ((4545)) NOT NULL,
    [CreationDate]                 DATETIME      CONSTRAINT [DF_TransferJuridicalDebtCollectionC_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]             VARCHAR (20)  NULL,
    [ModificationDate]             DATETIME      NULL,
    [TimeStamp]                    ROWVERSION    NOT NULL,
    [FilingUnitSourceId]           INT           NULL,
    [FilingUnitTargetId]           INT           NULL,
    [LawyerId]                     INT           NULL,
    [DemandStatusId]               INT           NULL,
    [Reclassified]                 BIT           CONSTRAINT [DF_TransferJuridicalDebtCollectionC_Reclassified] DEFAULT ((1)) NOT NULL,
    [ReversedUser]                 VARCHAR (20)  NULL,
    [ReversedDate]                 DATETIME      NULL,
    CONSTRAINT [PK_TransferJuridicalDebtCollectionC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransferJuridicalDebtCollectionC_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_TransferJuridicalDebtCollectionC_DemandStatus] FOREIGN KEY ([DemandStatusId]) REFERENCES [Portfolio].[DemandStatus] ([Id]),
    CONSTRAINT [FK_TransferJuridicalDebtCollectionC_Lawyer] FOREIGN KEY ([LawyerId]) REFERENCES [Portfolio].[Lawyer] ([Id])
);




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_TransferJuridicalDebtCollectionC__DemandStatusId]
    ON [Glosas].[TransferJuridicalDebtCollectionC]([DemandStatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TransferJuridicalDebtCollectionC__LawyerId]
    ON [Glosas].[TransferJuridicalDebtCollectionC]([LawyerId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TransferJuridicalDebtCollectionC_Consecutive]
    ON [Glosas].[TransferJuridicalDebtCollectionC]([JuridicalTransferConsecutive] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se revirtió o anuló el traslado a cobro jurídico (DATETIME, auditoría de reversión)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ReversedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de reversion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ReversedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ReversedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que ejecutó la reversión del traslado a cobro jurídico (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ReversedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de reversion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ReversedUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ReversedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (1=Sí, 0=No) que señala si la cartera fue reclasificada tras el traslado jurídico (BIT)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Reclassified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si reclasifica o no la cartera', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Reclassified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Reclassified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del estado actual de la demanda asociada al traslado (FK Portfolio.DemandStatus, INT)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DemandStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de estado de demanda', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DemandStatusId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DemandStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del abogado o profesional jurídico asignado al cobro (FK Portfolio.Lawyer, INT)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'LawyerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de abogado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'LawyerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'LawyerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de radicación destino donde se transfiere el expediente jurídico (INT)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'FilingUnitTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de radicacion de destino', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'FilingUnitTargetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'FilingUnitTargetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de radicación origen desde donde se transfiere el expediente (INT)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'FilingUnitSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de radicacion de origen', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'FilingUnitSourceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'FilingUnitSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) que registra el instante exacto de creación, modificación o evento en la transferencia jurídica', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio registrado en el traslado a cobro jurídico (DATETIME, auditoría)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que realizó la última modificación del registro (VARCHAR 20, auditoría)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de transferencia jurídica (DATETIME, auditoría, default Common.getdate())', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que originó el registro de traslado a cobro jurídico (VARCHAR 20, auditoría, default 4545)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del traslado jurídico: 1=Sin confirmar, 2=Confirmado (CHAR 1, enumerado)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Sin confirmar, 2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o comentarios adicionales sobre el traslado a cobro jurídico (VARCHAR 500, opcional)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Comment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del oficio de traslado ingresada manualmente por el usuario (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Oficio Ingresada por el usuario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se efectuó el traslado a cobro jurídico de la glosa o cartera (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'JuridicalTransferDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha traslado juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'JuridicalTransferDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'JuridicalTransferDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador o NIT del cliente/empresa asociado al traslado jurídico (FK Common.Customer, INT)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit del cliente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de oficio o documento de transferencia jurídica (VARCHAR 50, PII)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Oficio ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo único del oficio de traslado a cobro jurídico (NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'JuridicalTransferConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de oficio de traslado cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'JuridicalTransferConsecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'JuridicalTransferConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) del registro cabecera de transferencia a cobro jurídico (INT PK)', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de cabecera de traslado cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de transferencias de deudas de glosas al proceso de cobro jurídico. Guarda el encabezado de cada traslado formal a cobro jurídico (demanda), incluyendo el cliente pagador, el abogado asignado, el estado de la demanda y la trazabilidad de creación, modificación y reversión del traslado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC';
