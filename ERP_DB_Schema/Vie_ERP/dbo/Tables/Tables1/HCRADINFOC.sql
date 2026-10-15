CREATE TABLE [dbo].[HCRADINFOC] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHFOLIO]     DATETIME                                                                         NOT NULL,
    [CODDIAGNO]     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [ESTADIO1]      CHAR (10)                                                                        NOT NULL,
    [ESTADIO2]      CHAR (10)                                                                        NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CONCOMITAN]    INT                                                                              NOT NULL,
    [CODCENATE]     INT                                                                              NULL,
    [CODIGOIPS]     CHAR (100)                                                                       NULL,
    [TIPORADIOTERA] INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCRADINFOC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADINFOC_ADCONTIPS] FOREIGN KEY ([CODIGOIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_HCRADINFOC_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCRADINFOC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCRADINFOC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCRADINFOC] NOCHECK CONSTRAINT [FK_HCRADINFOC_ADCONTIPS];


GO
ALTER TABLE [dbo].[HCRADINFOC] NOCHECK CONSTRAINT [FK_HCRADINFOC_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCRADINFOC] NOCHECK CONSTRAINT [FK_HCRADINFOC_INPACIENT];


GO
ALTER TABLE [dbo].[HCRADINFOC] NOCHECK CONSTRAINT [FK_HCRADINFOC_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Radioterapia 1-Radioterapia Externa 2 - Braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'TIPORADIOTERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código servicio Ips.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Centro atención 2 - Ips', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene Concomitancia  1-Si  2-No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CONCOMITAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Profesional Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estadio 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ESTADIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estadio 1  0->0  I->1  II->2  III>3  IV->4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ESTADIO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código Diagnóstico Principal de la Hc', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'FECHFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Identificaión Paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ID';

