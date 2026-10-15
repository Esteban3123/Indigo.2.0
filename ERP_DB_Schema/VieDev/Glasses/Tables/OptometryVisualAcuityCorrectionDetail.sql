CREATE TABLE [Glasses].[OptometryVisualAcuityCorrectionDetail] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [idOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [Sphere]                         VARCHAR (10) NOT NULL,
    [Cylinder]                       VARCHAR (10) NOT NULL,
    [Axis]                           VARCHAR (10) NOT NULL,
    [VisualAcuity]                   INT          NOT NULL,
    [Adition]                        VARCHAR (20) NULL,
    [Prism]                          VARCHAR (10) NOT NULL,
    [ProximalVision]                 INT          NOT NULL,
    CONSTRAINT [PK_OptometryVisualAcuityCorrectionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryVisualAcuityCorrectionDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([idOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);

