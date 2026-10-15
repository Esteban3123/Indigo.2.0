CREATE TABLE [MixingStation].[Turn] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [State]               BIT           NOT NULL,
    [WeekFraction]        INT           NOT NULL,
    [Description]         VARCHAR (MAX) NULL,
    [InitialTimeWork]     DATETIME      NULL,
    [EndingTimeWork]      DATETIME      NULL,
    [InitialTimeDelivery] DATETIME      NULL,
    [EndingTimeDelivery]  DATETIME      NULL,
    [CreationUser]        VARCHAR (20)  CONSTRAINT [DF_Turn_CreationUser] DEFAULT ('999') NOT NULL,
    [CreationDate]        DATETIME      CONSTRAINT [DF_Turn_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20)  NULL,
    [ModificationDate]    DATETIME      NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    [Code]                VARCHAR (20)  NOT NULL,
    [Name]                VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Turn] PRIMARY KEY CLUSTERED ([Id] ASC)
);

