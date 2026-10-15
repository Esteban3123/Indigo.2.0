CREATE TABLE [dbo].[INDIAGNIH] (
    [IDETIPHIS] CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODDIAPRI] BIT MASKED WITH (FUNCTION = 'default()')                                         NOT NULL,
    [DIAINGEGR] CHAR (1)                                                                         NOT NULL,
    [TIPDIAGNO] CHAR (1)                                                                         NOT NULL,
    [CLADIAGNO] CHAR (2)                                                                         NOT NULL,
    [OBSDIAGNO] CHAR (250)                                                                       NOT NULL,
    [FECDIAGNO] DATETIME                                                                         NOT NULL,
    [FOLDIAGNO] INT                                                                              NOT NULL,
    [DIAESTADO] INT                                                                              NULL,
    [INDAUDFOR] NUMERIC (18)                                                                     NOT NULL,
    [PLANTDIAG] TEXT                                                                             NULL,
    [TRATA4505] INT                                                                              NULL,
    [FECHLEISH] DATETIME                                                                         NULL,
    [ESTADIO2]  CHAR (10)                                                                        NULL,
    [ESTADIO1]  CHAR (10)                                                                        NULL,
    [T1]        CHAR (2)                                                                         NULL,
    [T2]        CHAR (2)                                                                         NULL,
    [N1]        CHAR (2)                                                                         NULL,
    [N2]        CHAR (2)                                                                         NULL,
    [M]         CHAR (2)                                                                         NULL,
    CONSTRAINT [PK_INDIAGNIH] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [NUMINGRES] ASC, [IPCODPACI] ASC, [CODDIAGNO] ASC),
    CONSTRAINT [FK_INDIAGNIH_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_INDIAGNIH_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INDIAGNIH_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INDIAGNIH_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_INDIAGNIH_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_INDIAGNIH_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[INDIAGNIH] NOCHECK CONSTRAINT [FK_INDIAGNIH_ADcenaten];


GO
ALTER TABLE [dbo].[INDIAGNIH] NOCHECK CONSTRAINT [FK_INDIAGNIH_ADINGRESO];


GO
ALTER TABLE [dbo].[INDIAGNIH] NOCHECK CONSTRAINT [FK_INDIAGNIH_INDIAGNOS];


GO
ALTER TABLE [dbo].[INDIAGNIH] NOCHECK CONSTRAINT [FK_INDIAGNIH_INPACIENT];


GO
ALTER TABLE [dbo].[INDIAGNIH] NOCHECK CONSTRAINT [FK_INDIAGNIH_INPROFSAL];


GO
ALTER TABLE [dbo].[INDIAGNIH] NOCHECK CONSTRAINT [FK_INDIAGNIH_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para almancear el Estadio1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'ESTADIO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1: Activo  2: Descartado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'DIAESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase de Diagnostico  PR: Pre-operatorio  PO: Pos-operatorio  PP: Pre y Pos-Operatorio  HI: Hispatologico  NA: No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'CLADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Diagnostico  I: Impresion Diagnostica  C: Confirmado Nuevo  R: Confirmado Repetido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'TIPDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el Diagnostico es de Ingreso o Egreso  I: Ingreso  E: Egreso  A: Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDIAGNIH', @level2type = N'COLUMN', @level2name = N'DIAINGEGR';

