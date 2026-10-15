CREATE TABLE [MixingStation].[TechnicalConceptReadjustments] (
    [Id]                           INT           IDENTITY (1, 1) NOT NULL,
    [RequestPackageDetailStatusId] INT           NOT NULL,
    [TechnicalConceptDate]         DATETIME      NOT NULL,
    [PreviouslyReadjustment]       BIT           NOT NULL,
    [TechnicalConceptDateExpired]  DATETIME      NULL,
    [Temperature]                  VARCHAR (50)  NULL,
    [TechnicalConcept]             VARCHAR (500) NULL,
    CONSTRAINT [PK_TechnicalConceptReadjusments] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena si anteriormente ya se ha adecuado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TechnicalConceptReadjustments', @level2type = N'COLUMN', @level2name = N'PreviouslyReadjustment';

