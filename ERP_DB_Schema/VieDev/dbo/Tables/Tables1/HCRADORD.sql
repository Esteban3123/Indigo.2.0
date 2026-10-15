CREATE TABLE [dbo].[HCRADORD] (
    [ID]             INT                                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADINFOC]   INT                                                                           NOT NULL,
    [FECHORDEN]      DATETIME                                                                      NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODDIAGNO]      CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')  NOT NULL,
    [REGIONTRATA]    VARCHAR (100)                                                                 NOT NULL,
    [DOSTOTAL]       INT                                                                           NOT NULL,
    [DOSISPORFRACC]  INT                                                                           NOT NULL,
    [NUMEFRACC]      INT                                                                           NOT NULL,
    [PORCDOSTOTAUTO] INT                                                                           NULL,
    [OBSERVACION]    VARCHAR (2000)                                                                NULL,
    [IDHCORDPRON]    INT                                                                           NULL,
    [CODSERIPS]      CHAR (20)                                                                     NULL,
    CONSTRAINT [PK_HCRADINFOD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADINFOD_HCRADINFOC] FOREIGN KEY ([IDHCRADINFOC]) REFERENCES [dbo].[HCRADINFOC] ([ID]),
    CONSTRAINT [FK_HCRADINFOD_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCRADINFOD_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCRADORD] NOCHECK CONSTRAINT [FK_HCRADINFOD_HCRADINFOC];


GO
ALTER TABLE [dbo].[HCRADORD] NOCHECK CONSTRAINT [FK_HCRADINFOD_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCRADORD] NOCHECK CONSTRAINT [FK_HCRADINFOD_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion con la tabla de los procedimientos NoQx HCORDPRON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Iso dosis autorizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'PORCDOSTOTAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de fracciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'NUMEFRACC';

