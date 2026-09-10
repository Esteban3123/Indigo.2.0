CREATE TABLE [EHR].[SchemesDrugs2] (
    [SchemesId]                    CHAR (20)       NOT NULL,
    [DrugCode]                     CHAR (20)       NOT NULL,
    [RouteOfAdministration]        VARCHAR (20)    NOT NULL,
    [Dose]                         NUMERIC (18, 2) NOT NULL,
    [MeasurementUnit]              VARCHAR (20)    NOT NULL,
    [DoseMaximum]                  NUMERIC (18, 2) NULL,
    [Days]                         VARCHAR (2000)  NOT NULL,
    [TypeFactor]                   INT             NOT NULL,
    [Exception]                    BIT             NOT NULL,
    [ExclusivePlaceAdministration] BIT             NOT NULL,
    [Indice]                       INT             NOT NULL,
    [InstructionsAdministration]   VARCHAR (MAX)   NULL,
    [DiluentDrugCode]              CHAR (20)       NULL,
    [FinalVolume]                  NUMERIC (18, 2) NULL,
    [QuantityDiluent]              INT             NULL,
    [HomeAdministration]           INT             NULL,
    [CostMinimumUnitMeasure]       DECIMAL (18, 2) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de detalle que almacena los medicamentos asociados a esquemas terapéuticos, registrando para cada fármaco la vía de administración, dosis (con máximo opcional), unidad de medida y días de administración. Incluye información de dilución (diluyente, volumen final, cantidad), instrucciones de administración, indicadores de excepción y lugar exclusivo de administración, además del costo mínimo por unidad de medida.', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'TABLE', @level1name=N'SchemesDrugs2';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'TABLE', @level1name=N'SchemesDrugs2';
GO
