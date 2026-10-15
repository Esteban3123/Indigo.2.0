CREATE TABLE [GeneralLedger].[RetentionConcepts] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)    NOT NULL,
    [Name]             VARCHAR (100)   NOT NULL,
    [Retention]        TINYINT         NOT NULL,
    [MinBase]          DECIMAL (18, 2) CONSTRAINT [DF_RetentionConcept_MinBase] DEFAULT ((0)) NOT NULL,
    [Rate]             DECIMAL (6, 3)  NOT NULL,
    [Status]           BIT             CONSTRAINT [DF_RetentionConcept_State] DEFAULT ((1)) NOT NULL,
    [TypeRounding]     TINYINT         NOT NULL,
    [CreationUser]     VARCHAR (20)    NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    [ModificationUser] VARCHAR (20)    NULL,
    [ModificationDate] DATETIME        NULL,
    [TimeStamp]        ROWVERSION      NOT NULL,
    [CalculationType]  TINYINT         CONSTRAINT [DF__Retention__Calcu__1A26F69C] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_RetentionConcepts__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de cálculo:       1 = Progresivo      2 = Lineal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConcepts', @level2type = N'COLUMN', @level2name = N'CalculationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Redondeo:  6 = 0.01  5 = 0.1  1= 1  2 = 10  3 = 100  4 = 1000', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConcepts', @level2type = N'COLUMN', @level2name = N'TypeRounding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de retención:       1 = Base      2 = Rangos       3 = Variable      4 = Por Ciudad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'RetentionConcepts', @level2type = N'COLUMN', @level2name = N'Retention';

