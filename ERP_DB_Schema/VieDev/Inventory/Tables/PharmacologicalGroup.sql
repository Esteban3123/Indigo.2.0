CREATE TABLE [Inventory].[PharmacologicalGroup] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                        VARCHAR (20)  NOT NULL,
    [Name]                        VARCHAR (100) NOT NULL,
    [CrystalPharmacologicalGroup] VARCHAR (10)  NULL,
    [Status]                      BIT           NOT NULL,
    [CreationUser]                VARCHAR (20)  CONSTRAINT [DF_PharmacologicalGroup_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                DATETIME      CONSTRAINT [DF_PharmacologicalGroup_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]            VARCHAR (20)  NULL,
    [ModificationDate]            DATETIME      NULL,
    [TimeStamp]                   ROWVERSION    NOT NULL,
    CONSTRAINT [PK_PharmacologicalGroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

