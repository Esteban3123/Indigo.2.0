CREATE TABLE [Contract].[SurgeriesPercentageManual] (
    [Id]                         INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RateManualId]               INT            NOT NULL,
    [InterventionType]           TINYINT        NOT NULL,
    [MainHundredPercent]         BIT            NOT NULL,
    [SurgeonPercentage]          NUMERIC (5, 2) NOT NULL,
    [AnesthesiologistPercentage] NUMERIC (5, 2) NOT NULL,
    [AssistantPercentage]        NUMERIC (5, 2) NOT NULL,
    [RoomPercentage]             NUMERIC (5, 2) NOT NULL,
    [MaterialsPercentage]        NUMERIC (5, 2) NOT NULL,
    [PackagesPercentage]         NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_SurgeriesPercentageManual__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SurgeriesPercentageManual_RateManual] FOREIGN KEY ([RateManualId]) REFERENCES [Contract].[RateManual] ([Id])
);


GO
ALTER TABLE [Contract].[SurgeriesPercentageManual] NOCHECK CONSTRAINT [FK_SurgeriesPercentageManual_RateManual];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de intervencion quirurgica  2 - Bilateral  3 - MIVIE (Multiple Igual Via Igual Especialista)  4 - MDVIE (Multiple Diferente Via Igual Especialista)   5 - MIVDE (Multiple Igual Via Diferente Especialista)   6 - MDVDE (Multiple Diferente Via Diferente Especialista)   7 - PolitraumaIV (Politrauma Igual Via)   8 - PolitraumaDV (Politrauma Diferente Via) ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgeriesPercentageManual', @level2type = N'COLUMN', @level2name = N'InterventionType';

