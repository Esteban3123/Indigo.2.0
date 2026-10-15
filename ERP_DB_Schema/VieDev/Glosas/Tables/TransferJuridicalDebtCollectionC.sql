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

