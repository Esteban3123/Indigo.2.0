CREATE TABLE [Contract].[CupsSubgroup] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CupsGroupId]            INT           NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [Name]                   VARCHAR (300) NOT NULL,
    [Description]            VARCHAR (300) NULL,
    [Status]                 BIT           NOT NULL,
    [CreationUser]           VARCHAR (20)  NOT NULL,
    [CreationDate]           DATETIME      NOT NULL,
    [ModificationUser]       VARCHAR (20)  NULL,
    [ModificationDate]       DATETIME      NULL,
    [TimeStamp]              ROWVERSION    NOT NULL,
    [ShowOnImagingDashboard] BIT           NULL,
    [IdRisGrImage]           INT           NULL,
    CONSTRAINT [PK_CupsSubgroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CupsSubgroup_CupsGroup] FOREIGN KEY ([CupsGroupId]) REFERENCES [Contract].[CupsGroup] ([Id])
);


GO
ALTER TABLE [Contract].[CupsSubgroup] NOCHECK CONSTRAINT [FK_CupsSubgroup_CupsGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CupsSubgroup', @level2type = N'COLUMN', @level2name = N'Status';

