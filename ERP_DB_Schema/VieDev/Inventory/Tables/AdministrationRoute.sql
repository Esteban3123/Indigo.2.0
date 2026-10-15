CREATE TABLE [Inventory].[AdministrationRoute] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalFormId]       INT           NULL,
    [Code]                       VARCHAR (20)  NOT NULL,
    [Name]                       VARCHAR (100) NOT NULL,
    [CrystalAdministrationRoute] VARCHAR (20)  NULL,
    [Status]                     BIT           NOT NULL,
    [CreationUser]               VARCHAR (20)  CONSTRAINT [DF_AdministrationRoute_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]               DATETIME      CONSTRAINT [DF_AdministrationRoute_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]           VARCHAR (20)  NULL,
    [ModificationDate]           DATETIME      NULL,
    CONSTRAINT [PK_AdministrationRoute] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdministrationRoute_PharmaceuticalForm] FOREIGN KEY ([PharmaceuticalFormId]) REFERENCES [Inventory].[PharmaceuticalForm] ([Id])
);


GO
ALTER TABLE [Inventory].[AdministrationRoute] NOCHECK CONSTRAINT [FK_AdministrationRoute_PharmaceuticalForm];

