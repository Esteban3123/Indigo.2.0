CREATE TABLE [Inventory].[PharmaceuticalForm] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)  NOT NULL,
    [Name]                         VARCHAR (100) NOT NULL,
    [CrystalMedicalForm]           CHAR (3)      NOT NULL,
    [Status]                       BIT           NOT NULL,
    [CreationUser]                 VARCHAR (20)  CONSTRAINT [DF_PharmaceuticalForm_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                 DATETIME      CONSTRAINT [DF_PharmaceuticalForm_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]             VARCHAR (20)  NULL,
    [ModificationDate]             DATETIME      NULL,
    [RequireStability]             BIT           NULL,
    [PharmaceuticalFormGroupingId] INT           NULL,
    CONSTRAINT [PK_PharmaceuticalForm__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalForm_PharmaceuticalFormGrouping] FOREIGN KEY ([PharmaceuticalFormGroupingId]) REFERENCES [Inventory].[PharmaceuticalFormGrouping] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalForm] NOCHECK CONSTRAINT [FK_PharmaceuticalForm_PharmaceuticalFormGrouping];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PharmaceuticalForm__Code]
    ON [Inventory].[PharmaceuticalForm]([Code] ASC);

