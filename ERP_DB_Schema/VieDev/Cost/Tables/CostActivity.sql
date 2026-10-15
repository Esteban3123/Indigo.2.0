CREATE TABLE [Cost].[CostActivity] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [OperatingUnitId]  INT           NOT NULL,
    [CUPSEntityId]     INT           NOT NULL,
    [InitialDate]      DATE          NOT NULL,
    [EndDate]          DATE          NOT NULL,
    [Description]      VARCHAR (500) NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_CostActivity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivity_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_CostActivity_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivity] NOCHECK CONSTRAINT [FK_CostActivity_CUPSEntity];


GO
ALTER TABLE [Cost].[CostActivity] NOCHECK CONSTRAINT [FK_CostActivity_OperatingUnit];

