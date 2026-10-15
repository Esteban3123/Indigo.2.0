CREATE TABLE [Glasses].[OptometryVisualAcuityDetail] (
    [Id]                             INT IDENTITY (1, 1) NOT NULL,
    [idOptometryClinicalEvaluationC] INT NOT NULL,
    [Eye]                            INT NOT NULL,
    [FarAwayWithoutCorrection]       INT NOT NULL,
    [FarAwayWithCorrection]          INT NOT NULL,
    [Pinhole]                        INT NOT NULL,
    [ProximalWithoutCorrection]      INT NOT NULL,
    [ProximalWithCorrection]         INT NOT NULL,
    CONSTRAINT [PK_OptometryVisualAcuityDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryVisualAcuityDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([idOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);

