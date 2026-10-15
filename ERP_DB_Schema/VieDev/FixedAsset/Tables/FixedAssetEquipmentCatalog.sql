CREATE TABLE [FixedAsset].[FixedAssetEquipmentCatalog] (
    [Id]                                INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                              VARCHAR (20) NOT NULL,
    [Description]                       VARCHAR (50) NOT NULL,
    [Active]                            BIT          NOT NULL,
    [IdIngressAccountingAccount]        INT          NOT NULL,
    [IdIngressLeasingAccountingAccount] INT          NOT NULL,
    [IdDebitLoanAccountingAccount]      INT          NOT NULL,
    [IdCreaditLoanAccountingAccount]    INT          NOT NULL,
    [IdDepreciationAccountingAccount]   INT          NOT NULL,
    [Status]                            BIT          NOT NULL,
    [CreationUser]                      VARCHAR (20) NOT NULL,
    [CreationDate]                      DATETIME     NOT NULL,
    [ModificationUser]                  VARCHAR (20) NULL,
    [ModificationDate]                  DATETIME     NULL,
    [TimeStamp]                         ROWVERSION   NOT NULL,
    CONSTRAINT [PK_FixedAssetEquipmentCatalog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

