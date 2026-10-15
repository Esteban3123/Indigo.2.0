CREATE TABLE [EHR].[OncologycalSchemeByDrugsPrescription] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCODCONCEC]           INT             NOT NULL,
    [DrugCode]              CHAR (20)       NOT NULL,
    [RouteOfAdministration] CHAR (2)        NOT NULL,
    [Dose]                  NUMERIC (18, 6) NOT NULL,
    [MeasurementUnit]       CHAR (3)        NOT NULL,
    [MaximumDose]           NUMERIC (18, 6) NULL,
    [Duration]              TINYINT         NOT NULL,
    [CycleFrequency]        VARCHAR (250)   NOT NULL,
    [Days]                  VARCHAR (100)   NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByDrugsPrescription] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByDrugsPrescription_IHLISTPRO] FOREIGN KEY ([DrugCode]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [EHR].[OncologycalSchemeByDrugsPrescription] NOCHECK CONSTRAINT [FK_OncologycalSchemeByDrugsPrescription_IHLISTPRO];

