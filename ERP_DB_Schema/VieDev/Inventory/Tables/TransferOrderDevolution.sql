CREATE TABLE [Inventory].[TransferOrderDevolution] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [OperatingUnitId]  INT           NOT NULL,
    [DocumentDate]     DATETIME      NOT NULL,
    [TransferOrderId]  INT           NOT NULL,
    [Description]      VARCHAR (300) NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [ConfirmationUser] VARCHAR (20)  NULL,
    [ConfirmationDate] DATETIME      NULL,
    [AnnulmentUser]    VARCHAR (20)  NULL,
    [AnnulmentDate]    DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_TransferOrderDevolution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransferOrderDevolution_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_TransferOrderDevolution_TransferOrder] FOREIGN KEY ([TransferOrderId]) REFERENCES [Inventory].[TransferOrder] ([Id])
);


GO
ALTER TABLE [Inventory].[TransferOrderDevolution] NOCHECK CONSTRAINT [FK_TransferOrderDevolution_OperatingUnit];


GO
ALTER TABLE [Inventory].[TransferOrderDevolution] NOCHECK CONSTRAINT [FK_TransferOrderDevolution_TransferOrder];

