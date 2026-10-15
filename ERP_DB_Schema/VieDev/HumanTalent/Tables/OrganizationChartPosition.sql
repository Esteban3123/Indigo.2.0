CREATE TABLE [HumanTalent].[OrganizationChartPosition] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [PositionId]       INT           NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_OrganizationChartPosition] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrganizationChartPosition_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [HumanTalent].[OrganizationChartPosition] NOCHECK CONSTRAINT [FK_OrganizationChartPosition_Position];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  0 - Inactivo  1 - Activo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'OrganizationChartPosition', @level2type = N'COLUMN', @level2name = N'Status';

