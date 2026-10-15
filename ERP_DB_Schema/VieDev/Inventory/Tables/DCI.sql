CREATE TABLE [Inventory].[DCI] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [DCICrystal]       VARCHAR (20)  NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_DCI_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_DCI_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [Combined]         BIT           CONSTRAINT [DF_DCI_Combined] DEFAULT ((0)) NOT NULL,
    [TypeWarning]      BIT           CONSTRAINT [DF__DCI__TypeWarning__4881F2D2] DEFAULT ((0)) NOT NULL,
    [AlternativeCode]  VARCHAR (20)  NULL,
    CONSTRAINT [PK_DCI__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo alternativo Usado en Colombia como el Codigo RIPS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCI', @level2type = N'COLUMN', @level2name = N'AlternativeCode';

