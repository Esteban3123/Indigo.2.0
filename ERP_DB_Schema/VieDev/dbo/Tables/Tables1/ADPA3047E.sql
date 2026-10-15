CREATE TABLE [dbo].[ADPA3047E] (
    [CODCENATE]                   CHAR (10)    NOT NULL,
    [CODENTIDA]                   CHAR (9)     NOT NULL,
    [INDNUMTEL]                   CHAR (5)     NULL,
    [NUMTELCEN]                   CHAR (20)    NULL,
    [NUMEXTTEL]                   CHAR (6)     NULL,
    [URLSERWEB]                   CHAR (150)   NOT NULL,
    [CODUSUWEB]                   CHAR (50)    NOT NULL,
    [PASWORWEB]                   CHAR (50)    NOT NULL,
    [SERDEFENV]                   CHAR (1)     NOT NULL,
    [NUMTELLLA]                   CHAR (20)    NOT NULL,
    [INDTELLLA]                   CHAR (5)     NOT NULL,
    [EXTTELLLA]                   CHAR (6)     NOT NULL,
    [URLFORWEB]                   CHAR (150)   NOT NULL,
    [INSTREPOR]                   NCHAR (500)  NOT NULL,
    [INDAUDFOR]                   NUMERIC (18) NOT NULL,
    [AUTO]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PRIREENVI]                   INT          NULL,
    [SEGREENVI]                   INT          NULL,
    [ENVCORENT]                   BIT          NOT NULL,
    [ENTIEMAIL]                   CHAR (50)    NULL,
    [TriageStatus]                BIT          NULL,
    [IntervalOfTimeToSendEmails]  TINYINT      NULL,
    [ScaleTriage1]                BIT          NULL,
    [ScaleTriage2]                BIT          NULL,
    [ScaleTriage3]                BIT          NULL,
    [ScaleTriage4]                BIT          NULL,
    [ScaleTriage5]                BIT          NULL,
    [DateFirstExecution]          DATETIME     NULL,
    [DateLastExecution]           DATETIME     NULL,
    [DateSucessfulMailGeneration] DATETIME     NULL,
    CONSTRAINT [PK_ADPAR3047] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_ADPA3047E_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[ADPA3047E] NOCHECK CONSTRAINT [FK_ADPA3047E_ADCENATEN];


GO
CREATE NONCLUSTERED INDEX [IX_ADPA3047E__CODCENATE__CODENTIDA__SERDEFENV]
    ON [dbo].[ADPA3047E]([CODCENATE] ASC, [CODENTIDA] ASC, [SERDEFENV] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio que se utilizara por defecto para el reporte de los Formatos - Res 3047  1: FAX   2: E-Mail  3. Intercambio electronico  4: Llamada Telefonica  5: Formulario Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047E', @level2type = N'COLUMN', @level2name = N'SERDEFENV';

