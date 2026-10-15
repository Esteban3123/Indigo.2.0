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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion con la tabla de los procedimientos NoQx HCORDPRON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Iso dosis autorizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'PORCDOSTOTAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de fracciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'NUMEFRACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis fraccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'DOSISPORFRACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'DOSTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Region del tratamiento ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'REGIONTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la orden ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'FECHORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Información de la radioterapia relación con la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'IDHCRADINFOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORD', @level2type = N'COLUMN', @level2name = N'ID';

