CREATE TABLE [Contract].[RateManual] (
    [Id]                           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)   NOT NULL,
    [Name]                         VARCHAR (100)  NOT NULL,
    [Type]                         TINYINT        NOT NULL,
    [RoundService]                 INT            NOT NULL,
    [ContractMinimumWageId]        INT            NULL,
    [PercentageNoBloodyRoom]       NUMERIC (5, 2) NULL,
    [MaterialNoBloodyIPSServiceId] INT            NULL,
    [Status]                       BIT            NOT NULL,
    [CreationUser]                 VARCHAR (20)   NOT NULL,
    [CreationDate]                 DATETIME       NOT NULL,
    [ModificationUser]             VARCHAR (20)   NULL,
    [ModificationDate]             DATETIME       NULL,
    [TimeStamp]                    ROWVERSION     NOT NULL,
    [LiquidateAllMIVIE]            BIT            NULL,
    CONSTRAINT [PK_RateManual__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RateManual_ContractMinimumWage] FOREIGN KEY ([ContractMinimumWageId]) REFERENCES [Contract].[ContractMinimumWage] ([Id]),
    CONSTRAINT [FK_RateManual_IPSService] FOREIGN KEY ([MaterialNoBloodyIPSServiceId]) REFERENCES [Contract].[IPSService] ([Id])
);


GO
ALTER TABLE [Contract].[RateManual] NOCHECK CONSTRAINT [FK_RateManual_ContractMinimumWage];


GO
ALTER TABLE [Contract].[RateManual] NOCHECK CONSTRAINT [FK_RateManual_IPSService];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va redondear los valores del contrato  1 -- Peso  10 -- Decima  100 -- Centecima  1000 -- Milesima    La formula que se usa es Round(valor / redondeo) * redondeo  Round(2005/ 10) * 10', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'RoundService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del manual tarifario  1 - ISS 2001  2 - ISS 2004  3 - SOAT', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'RateManual', @level2type = N'COLUMN', @level2name = N'Type';

