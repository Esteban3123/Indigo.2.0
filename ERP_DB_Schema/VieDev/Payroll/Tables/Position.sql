CREATE TABLE [Payroll].[Position] (
    [Id]                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20) NOT NULL,
    [Name]                       VARCHAR (80) NOT NULL,
    [PositionLevelId]            INT          NOT NULL,
    [MinHourAmount]              INT          NOT NULL,
    [MaxHourAmount]              INT          NOT NULL,
    [MinBasicSalary]             INT          NOT NULL,
    [MaxBasicSalary]             INT          NOT NULL,
    [ProfessionalRiskLevelId]    INT          NOT NULL,
    [Simulation]                 BIT          NOT NULL,
    [HandlesTurnsChart]          BIT          NOT NULL,
    [NightlyChargeAuthorization] BIT          NOT NULL,
    [RepresentationCost]         BIT          CONSTRAINT [DF_Position_RepresentationCost] DEFAULT ((0)) NOT NULL,
    [ResolutionNumber]           VARCHAR (50) NULL,
    [BeforeResolutionNumber]     VARCHAR (50) NULL,
    [State]                      BIT          NOT NULL,
    [CreationUser]               VARCHAR (20) NOT NULL,
    [CreationDate]               DATETIME     NOT NULL,
    [ModificationUser]           VARCHAR (20) NULL,
    [ModificationDate]           DATETIME     NULL,
    [TimeStamp]                  ROWVERSION   NOT NULL,
    [INSCode]                    VARCHAR (20) NULL,
    [CCSSCode]                   VARCHAR (20) NULL,
    CONSTRAINT [PK_PositionStructure] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Position_PositionLevel] FOREIGN KEY ([PositionLevelId]) REFERENCES [Payroll].[PositionLevel] ([Id]),
    CONSTRAINT [FK_Position_ProfessionalRisk] FOREIGN KEY ([ProfessionalRiskLevelId]) REFERENCES [Payroll].[ProfessionalRisk] ([Id])
);


GO
ALTER TABLE [Payroll].[Position] NOCHECK CONSTRAINT [FK_Position_PositionLevel];


GO
ALTER TABLE [Payroll].[Position] NOCHECK CONSTRAINT [FK_Position_ProfessionalRisk];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información de seguridad social, Código CCSS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'CCSSCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información de seguridad social, Código INS', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Position', @level2type = N'COLUMN', @level2name = N'INSCode';

