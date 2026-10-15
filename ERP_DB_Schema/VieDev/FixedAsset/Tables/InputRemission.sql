CREATE TABLE [FixedAsset].[InputRemission] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)  NOT NULL,
    [RemisionDate]               DATE          NOT NULL,
    [IdAdquisitionType]          INT           NOT NULL,
    [IdSupplierDistributionLine] INT           NOT NULL,
    [RemisionNumber]             VARCHAR (20)  NOT NULL,
    [Detail]                     VARCHAR (300) NOT NULL,
    [Status]                     TINYINT       NOT NULL,
    [CreationUser]               VARCHAR (20)  NOT NULL,
    [CreationDate]               DATETIME      NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    [ConfirmationUser]           VARCHAR (20)  NULL,
    [ConfirmationDate]           DATETIME      NULL,
    [AnnulmentUser]              VARCHAR (20)  NULL,
    [AnnulmentDate]              DATETIME      NULL,
    [TimeStamp]                  ROWVERSION    NULL,
    CONSTRAINT [PK_InputRemission_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

