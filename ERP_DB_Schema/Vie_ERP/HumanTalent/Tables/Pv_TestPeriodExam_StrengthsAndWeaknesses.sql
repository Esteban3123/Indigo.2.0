CREATE TABLE [HumanTalent].[Pv_TestPeriodExam_StrengthsAndWeaknesses] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TestPeriodExamId] INT          NOT NULL,
    [Strengths]        VARCHAR (50) NULL,
    [Weaknesses]       VARCHAR (50) NULL,
    [CreationUser]     VARCHAR (50) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (50) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_Pv_TestPeriodExam_StrengthsAndWeaknesses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Pv_TestPeriodExam_StrengthsAndWeaknesses_TestPeriodExam] FOREIGN KEY ([TestPeriodExamId]) REFERENCES [HumanTalent].[TestPeriodExam] ([Id])
);


GO
ALTER TABLE [HumanTalent].[Pv_TestPeriodExam_StrengthsAndWeaknesses] NOCHECK CONSTRAINT [FK_Pv_TestPeriodExam_StrengthsAndWeaknesses_TestPeriodExam];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modificación del Usuario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creación de los Usuarios', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Debilidades', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Weaknesses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fortalezas', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Strengths';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de examen del período de prueba', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'TestPeriodExamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Pv_TestPeriodExam_StrengthsAndWeaknesses', @level2type = N'COLUMN', @level2name = N'Id';

