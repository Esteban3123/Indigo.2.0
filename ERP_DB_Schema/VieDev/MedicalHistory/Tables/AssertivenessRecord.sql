CREATE TABLE [MedicalHistory].[AssertivenessRecord] (
    [RecordID]             INT            IDENTITY (1, 1) NOT NULL,
    [CreatedOn]            DATETIME       DEFAULT (getdate()) NULL,
    [ProfessionalUserCode] INT            NOT NULL,
    [PromptName]           NVARCHAR (255) NOT NULL,
    [PromptResponse]       NVARCHAR (MAX) NOT NULL,
    [Assertive]            BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([RecordID] ASC)
);

