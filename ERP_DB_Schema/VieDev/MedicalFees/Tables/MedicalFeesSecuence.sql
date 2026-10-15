CREATE TABLE [MedicalFees].[MedicalFeesSecuence] (
    [Id]         INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]     VARCHAR (5) NOT NULL,
    [IsManual]   BIT         CONSTRAINT [DF_MedicalFeesSecuence_IdManual] DEFAULT ((1)) NOT NULL,
    [Scope]      VARCHAR (2) CONSTRAINT [DF_SequenseMedicalFeesC_Scope] DEFAULT ('O') NOT NULL,
    [Sequential] BIT         CONSTRAINT [DF_SequenseMedicalFeesC_Sequential] DEFAULT ((0)) NOT NULL,
    [Rate]       TINYINT     CONSTRAINT [DF_SequenseMedicalFeesC_Rate] DEFAULT ((3)) NOT NULL,
    CONSTRAINT [PK_SequenseMedicalFeesC] PRIMARY KEY CLUSTERED ([Id] ASC)
);

