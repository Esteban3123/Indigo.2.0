CREATE TABLE [dbo].[AGTRACITA] (
    [AUTONUMER]                INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODAUTONU]                INT         NOT NULL,
    [CODCENATE]                CHAR (10)   NOT NULL,
    [CODPROORI]                CHAR (20)   NOT NULL,
    [CODPROMOD]                CHAR (20)   NOT NULL,
    [FECHINORI]                DATETIME    NOT NULL,
    [FECHFIORI]                DATETIME    NOT NULL,
    [FECHINMOD]                DATETIME    NOT NULL,
    [FECHFIMOD]                DATETIME    NOT NULL,
    [CODUSUMOD]                CHAR (20)   NOT NULL,
    [FECREGSIS]                DATETIME    NOT NULL,
    [ConsultationCode]         VARCHAR (6) NULL,
    [ModifiedConsultationCode] VARCHAR (6) NULL,
    CONSTRAINT [PK_AGTRACITA] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGTRACITA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[AGTRACITA] NOCHECK CONSTRAINT [FK_AGTRACITA_ADCENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del consultorio al cual fue modificada la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'ModifiedConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del consultorio original de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'ConsultationCode';

