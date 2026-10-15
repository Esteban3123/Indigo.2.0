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
ALTER TABLE [Glosas].[TransferJuridicalDebtCollectionC] NOCHECK CONSTRAINT [FK_TransferJuridicalDebtCollectionC_Customer];


GO
ALTER TABLE [Glosas].[TransferJuridicalDebtCollectionC] NOCHECK CONSTRAINT [FK_TransferJuridicalDebtCollectionC_DemandStatus];


GO
ALTER TABLE [Glosas].[TransferJuridicalDebtCollectionC] NOCHECK CONSTRAINT [FK_TransferJuridicalDebtCollectionC_Lawyer];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de reversion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ReversedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de reversion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ReversedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si reclasifica o no la cartera', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Reclassified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de estado de demanda', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DemandStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de abogado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'LawyerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de radicacion de destino', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'FilingUnitTargetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de radicacion de origen', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'FilingUnitSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Sin confirmar, 2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Comment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Oficio Ingresada por el usuario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha traslado juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'JuridicalTransferDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit del cliente', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Oficio ', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de oficio de traslado cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'JuridicalTransferConsecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de cabecera de traslado cobro juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'TransferJuridicalDebtCollectionC', @level2type = N'COLUMN', @level2name = N'Id';

