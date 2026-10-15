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

