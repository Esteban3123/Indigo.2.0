CREATE TABLE [Obstetrics].[Ultrasound] (
    [Id]                                  INT             IDENTITY (1, 1) NOT NULL,
    [IdPerinatalMaternalAssessmentC]      INT             NOT NULL,
    [UltrasoundDate]                      DATETIME        NOT NULL,
    [WeeksAccordingToUltrasound]          DECIMAL (18, 1) NOT NULL,
    [CurrentDate]                         DATETIME        NOT NULL,
    [WeeksToToday]                        DECIMAL (18, 1) NOT NULL,
    [EstimatedDateOfDeliveryByUltrasound] DATETIME        NOT NULL,
    [Percentile]                          DECIMAL (18, 2) NULL,
    [Weight]                              INT             NULL,
    [Size]                                DECIMAL (18, 2) NULL,
    [AmnioticFluidIndex]                  DECIMAL (18, 2) NULL,
    [MaximumVerticalPocket]               DECIMAL (18, 2) NULL,
    [Observations]                        VARCHAR (100)   NULL,
    CONSTRAINT [PK_Ultrasound] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ultrasound_PerinatalMaternalAssessmentC] FOREIGN KEY ([IdPerinatalMaternalAssessmentC]) REFERENCES [Obstetrics].[PerinatalMaternalAssessmentC] ([Id])
);


GO
ALTER TABLE [Obstetrics].[Ultrasound] NOCHECK CONSTRAINT [FK_Ultrasound_PerinatalMaternalAssessmentC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bolsillo vertical máximo (BVM) en cms', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'MaximumVerticalPocket';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice de líquido amniótico (ILA) en cms', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'AmnioticFluidIndex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Size';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso (gramos)', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Percentil', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'Percentile';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha probable de parto por ecografía', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'EstimatedDateOfDeliveryByUltrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas a hoy', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'WeeksToToday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha actual', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'CurrentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas según ecografía', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'WeeksAccordingToUltrasound';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ecografía', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound', @level2type = N'COLUMN', @level2name = N'UltrasoundDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena los datos de la ecografía de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound';


GO
EXECUTE sp_addextendedproperty @name = N'Description', @value = N'Guarda los datos de la ecografía de evaluación de paraclínicos', @level0type = N'SCHEMA', @level0name = N'Obstetrics', @level1type = N'TABLE', @level1name = N'Ultrasound';

