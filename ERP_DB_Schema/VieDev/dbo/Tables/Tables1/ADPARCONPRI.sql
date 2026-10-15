CREATE TABLE [dbo].[ADPARCONPRI] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]    CHAR (10) NOT NULL,
    [CODENTIDA]    CHAR (9)  NULL,
    [LUN]          BIT       NULL,
    [MAR]          BIT       NULL,
    [MIER]         BIT       NULL,
    [JUE]          BIT       NULL,
    [VIER]         BIT       NULL,
    [SAB]          BIT       NULL,
    [DOM]          BIT       NULL,
    [TRIAGECUATRO] INT       NULL,
    [TRIAGECINCO]  INT       NULL,
    [HORAS]        INT       NOT NULL,
    CONSTRAINT [PK_ADPARCONPRI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [Centro Atencion] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[ADPARCONPRI] NOCHECK CONSTRAINT [Centro Atencion];

