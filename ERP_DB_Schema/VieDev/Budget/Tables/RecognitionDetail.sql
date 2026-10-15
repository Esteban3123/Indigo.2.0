CREATE TABLE [Budget].[RecognitionDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RecognitionId]           INT             NOT NULL,
    [CategoryId]              INT             NOT NULL,
    [RevenueTypeId]           INT             NOT NULL,
    [InitialValue]            NUMERIC (18, 2) NOT NULL,
    [DebitValueModification]  NUMERIC (18, 2) NOT NULL,
    [CreditValueModification] NUMERIC (18, 2) NOT NULL,
    [TotalRecognition]        NUMERIC (18, 2) NOT NULL,
    [ExecutedValue]           NUMERIC (18, 2) NOT NULL,
    [Balance]                 NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_RecognitionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Budget_RecognitionDetail_ValidateValues] CHECK ((([InitialValue]-[DebitValueModification])+[CreditValueModification])=[TotalRecognition] AND ([TotalRecognition]-[ExecutedValue])=[Balance]),
    CONSTRAINT [FK_RecognitionDetail_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_RecognitionDetail_Recognition] FOREIGN KEY ([RecognitionId]) REFERENCES [Budget].[Recognition] ([Id]),
    CONSTRAINT [FK_RecognitionDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id])
);


GO
ALTER TABLE [Budget].[RecognitionDetail] NOCHECK CONSTRAINT [CK_Budget_RecognitionDetail_ValidateValues];


GO
ALTER TABLE [Budget].[RecognitionDetail] NOCHECK CONSTRAINT [FK_RecognitionDetail_Category];


GO
ALTER TABLE [Budget].[RecognitionDetail] NOCHECK CONSTRAINT [FK_RecognitionDetail_Recognition];


GO
ALTER TABLE [Budget].[RecognitionDetail] NOCHECK CONSTRAINT [FK_RecognitionDetail_RevenueType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo del reconocimiento el cual se obtiene de la siguiente manera     TotalRecognition - ExecutedValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionDetail', @level2type = N'COLUMN', @level2name = N'Balance';

