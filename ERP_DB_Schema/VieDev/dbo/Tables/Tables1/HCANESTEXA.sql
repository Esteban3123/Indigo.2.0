CREATE TABLE [dbo].[HCANESTEXA] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]     CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]     CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [CODCENATE]     CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]     CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [EXAECG]        VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "ResultECG_Ofuscado", 0)')     NULL,
    [EXAHEMATO]     VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "ResultHemato_Ofuscado", 0)')   NULL,
    [EXABUN]        VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXACREATINI]   VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXAGLICEMI]    VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXAALBUMI]     VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXAHEMOG]      VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXAPROTROM]    VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXATROMBO]     VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')         NULL,
    [EXARXTORAX]    VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')        NULL,
    [EXAOTROS]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Result_Ofuscado", 0)')        NULL,
    [RIEMALLAM]     INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [RIEINTUBACION] BIT                                                                              NULL,
    [RIEASA]        INT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [RIEOTROS]      VARCHAR (MAX) MASKED WITH (FUNCTION = 'default()')                               NULL,
    [FECREGSIS]     DATETIME                                                                         CONSTRAINT [DF_HCANESTEXA_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [VIAAREA]       VARCHAR (400)                                                                    NULL,
    CONSTRAINT [PK_HCANESTEXA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCANESTEXA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANESTEXA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANESTEXA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANESTEXA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANESTEXA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCANESTEXA] NOCHECK CONSTRAINT [FK_HCANESTEXA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCANESTEXA] NOCHECK CONSTRAINT [FK_HCANESTEXA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANESTEXA] NOCHECK CONSTRAINT [FK_HCANESTEXA_INPACIENT];


GO
ALTER TABLE [dbo].[HCANESTEXA] NOCHECK CONSTRAINT [FK_HCANESTEXA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCANESTEXA] NOCHECK CONSTRAINT [FK_HCANESTEXA_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '11.0 Otros Exámenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAOTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '10.0 Rx de Torax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXARXTORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '9.0 Tiempo parcial de Tromboplastina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXATROMBO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '8.0 Tiempo de Protrombina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAPROTROM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '7.0 Hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAHEMOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '6.0 Albumina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAALBUMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '5.0 Glicemia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXAGLICEMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '4.0 Creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXACREATINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '3.0 BUN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANESTEXA', @level2type = N'COLUMN', @level2name = N'EXABUN';

