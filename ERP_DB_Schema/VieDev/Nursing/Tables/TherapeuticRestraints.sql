CREATE TABLE [Nursing].[TherapeuticRestraints] (
    [Id]                              INT           IDENTITY (1, 1) NOT NULL,
    [PatientCode]                     VARCHAR (25)  NOT NULL,
    [AdmissionNumber]                 CHAR (10)     NOT NULL,
    [HealthCenter]                    CHAR (10)     NOT NULL,
    [HealthUnit]                      CHAR (10)     NOT NULL,
    [OrderHealthcareProfessional]     CHAR (20)     NOT NULL,
    [OrderDate]                       DATETIME      NOT NULL,
    [Status]                          BIT           NOT NULL,
    [SuspendedHealthcareProfessional] CHAR (20)     NULL,
    [SuspendedDate]                   DATETIME      NULL,
    [CodeSuspendedJustification]      CHAR (4)      NULL,
    [SuspendedObservation]            VARCHAR (300) NULL,
    CONSTRAINT [PK_TherapeuticRestraints] PRIMARY KEY CLUSTERED ([Id] ASC)
);

