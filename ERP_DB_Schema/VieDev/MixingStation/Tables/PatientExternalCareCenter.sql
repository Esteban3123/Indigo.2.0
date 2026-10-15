CREATE TABLE [MixingStation].[PatientExternalCareCenter] (
    [Id]                     INT                                                                         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdentificationNumber]   VARCHAR (25)                                                                NOT NULL,
    [IdentificationTypeId]   INT                                                                         NOT NULL,
    [Name]                   VARCHAR (300) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')     NOT NULL,
    [LastName]               VARCHAR (300) MASKED WITH (FUNCTION = 'partial(0, "LastName_Ofuscado", 0)') NULL,
    [GenderTypeId]           INT                                                                         NOT NULL,
    [Status]                 BIT                                                                         NOT NULL,
    [CreationUser]           VARCHAR (20)                                                                NOT NULL,
    [CreationDate]           DATETIME                                                                    NOT NULL,
    [ModificationUser]       VARCHAR (20)                                                                NULL,
    [ModificationDate]       DATETIME                                                                    NULL,
    [TimeStamp]              ROWVERSION                                                                  NOT NULL,
    [PatientMobileNumber]    VARCHAR (10)                                                                NULL,
    [PatientEmail]           VARCHAR (50)                                                                NULL,
    [ExternalFunctionalUnit] VARCHAR (100)                                                               NULL,
    [PatientBed]             VARCHAR (50)                                                                NULL,
    CONSTRAINT [PK_PatientExternalCareCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PatientExternalCareCenter_GenderTypes] FOREIGN KEY ([GenderTypeId]) REFERENCES [Admissions].[GenderTypes] ([Id]),
    CONSTRAINT [FK_PatientExternalCareCenter_IdentificationType] FOREIGN KEY ([IdentificationTypeId]) REFERENCES [dbo].[ADTIPOIDENTIFICA] ([ID])
);

