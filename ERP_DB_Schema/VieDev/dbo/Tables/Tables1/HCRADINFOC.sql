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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiene Concomitancia  1-Si  2-No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'CONCOMITAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estadio 1  0->0  I->1  II->2  III>3  IV->4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADINFOC', @level2type = N'COLUMN', @level2name = N'ESTADIO1';

