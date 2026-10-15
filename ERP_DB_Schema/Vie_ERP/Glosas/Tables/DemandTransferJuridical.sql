CREATE TABLE [Glosas].[DemandTransferJuridical] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferJuridicalDebtCollectionCId] INT          NOT NULL,
    [FilingUnitSourceId]                 INT          NULL,
    [FilingUnitTargetId]                 INT          NULL,
    [LawyerId]                           INT          NULL,
    [DemandStatusId]                     INT          NULL,
    [CreationUser]                       VARCHAR (20) NOT NULL,
    [CreationDate]                       DATETIME     NOT NULL,
    CONSTRAINT [PK_DemandTransferJuridical] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien edito el traslado juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de estado de demanda', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'DemandStatusId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de abogado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'LawyerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de radicación destino', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'FilingUnitTargetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de radicación origen', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'FilingUnitSourceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del traslado juridico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'TransferJuridicalDebtCollectionCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'DemandTransferJuridical', @level2type = N'COLUMN', @level2name = N'Id';

