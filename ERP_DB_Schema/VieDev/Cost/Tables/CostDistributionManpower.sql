CREATE TABLE [Cost].[CostDistributionManpower] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)    NOT NULL,
    [EmployeeId]                INT             NULL,
    [Description]               VARCHAR (300)   NOT NULL,
    [Year]                      INT             NOT NULL,
    [Month]                     INT             NOT NULL,
    [HoursWorked]               INT             NOT NULL,
    [TotalAccrued]              DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpower_TotalAccrued] DEFAULT ((0)) NOT NULL,
    [TotalProvision]            DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpower_TotalProvision] DEFAULT ((0)) NOT NULL,
    [TotalEmployerContribution] DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpower_TotalEmployerContribution] DEFAULT ((0)) NOT NULL,
    [TotalParafiscal]           DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpower_TotalParafiscal] DEFAULT ((0)) NOT NULL,
    [Status]                    BIT             NOT NULL,
    [CreationUser]              VARCHAR (20)    NOT NULL,
    [CreationDate]              DATETIME        NOT NULL,
    [ModificationUser]          VARCHAR (20)    NULL,
    [ModificationDate]          DATETIME        NULL,
    [TimeStamp]                 ROWVERSION      NOT NULL,
    [ThirdPartyId]              INT             NULL,
    [PositionId]                INT             NULL,
    [GroupId]                   INT             NULL,
    [ManpowerType]              TINYINT         CONSTRAINT [DF_CostDistributionManpower_ManpowerType] DEFAULT ((1)) NOT NULL,
    [EntityId]                  INT             NULL,
    [EntityCurrencyId]          INT             NULL,
    CONSTRAINT [PK_CostDistributionManpower] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionManpower_Currency] FOREIGN KEY ([EntityCurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_CostDistributionManpower_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_CostDistributionManpower_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_CostDistributionManpower_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_CostDistributionManpower_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionManpower] NOCHECK CONSTRAINT [FK_CostDistributionManpower_Employee];


GO
ALTER TABLE [Cost].[CostDistributionManpower] NOCHECK CONSTRAINT [FK_CostDistributionManpower_Group];


GO
ALTER TABLE [Cost].[CostDistributionManpower] NOCHECK CONSTRAINT [FK_CostDistributionManpower_Position];


GO
ALTER TABLE [Cost].[CostDistributionManpower] NOCHECK CONSTRAINT [FK_CostDistributionManpower_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda del documento origen', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpower', @level2type = N'COLUMN', @level2name = N'EntityCurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad fuente, si el tipo es:  - 1 (Empleado) la fuente es Payroll.Liquidation  - 2 (Contratista) la fuente es Payments.AccountPayable', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpower', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de distribución:  1 - Nómina  2 - Empleados Independientes (Contratistas)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpower', @level2type = N'COLUMN', @level2name = N'ManpowerType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpower', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpower', @level2type = N'COLUMN', @level2name = N'Status';

