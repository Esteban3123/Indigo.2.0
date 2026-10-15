CREATE TABLE [dbo].[ExceptionLog] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [PatientCode]      NVARCHAR (50)  NULL,
    [AdmissionCode]    NVARCHAR (50)  NULL,
    [UniqueKey]        NVARCHAR (200) NULL,
    [Professional]     NVARCHAR (100) NULL,
    [DateCreation]     DATETIME       DEFAULT (getdate()) NULL,
    [ExceptionMessage] NVARCHAR (600) NULL,
    [Product]          NVARCHAR (50)  NULL,
    [Origin]           NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

