CREATE TABLE [Application].[AppSettings] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Key]        VARCHAR (100) NOT NULL,
    [Value]      VARCHAR (MAX) NOT NULL,
    [Type]       VARCHAR (50)  NULL,
    [CreatedFor] VARCHAR (70)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

