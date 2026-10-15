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

