CREATE TABLE [Billing].[RevenueControl] (
    [Id]                   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber]      CHAR (10)                                                                        NOT NULL,
    [PatientCode]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FolioQuantity]        TINYINT                                                                          NOT NULL,
    [TopEventFeeModerator] NUMERIC (18, 2)                                                                  CONSTRAINT [DF_RevenueControl_TopEventFeeModerator] DEFAULT ((0)) NOT NULL,
    [TopEventCopay]        NUMERIC (18, 2)                                                                  CONSTRAINT [DF_RevenueControl_TopEventCopay] DEFAULT ((0)) NOT NULL,
    [TopEventFeeRecovery]  NUMERIC (18, 2)                                                                  CONSTRAINT [DF_RevenueControl_TopEventFeeRecovery] DEFAULT ((0)) NOT NULL,
    [TimeStamp]            ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_RevenueControl__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

