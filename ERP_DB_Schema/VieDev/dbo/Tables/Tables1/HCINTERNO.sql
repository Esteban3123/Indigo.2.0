CREATE TABLE [dbo].[HCINTERNO] (
    [IDETIPHIS]  CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]  NCHAR (10)                                                                       NOT NULL,
    [CONSFOLIO]  CHAR (10)                                                                        NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC]  DATETIME                                                                         NOT NULL,
    [TIPHISPAC]  CHAR (2)                                                                         NOT NULL,
    [DIAGNOSTI]  VARCHAR (MAX)                                                                    NULL,
    [ANTECEDE]   VARCHAR (MAX)                                                                    NULL,
    [SUBJETIVO]  VARCHAR (MAX)                                                                    NULL,
    [EXAMENFI]   VARCHAR (MAX)                                                                    NULL,
    [PARACLINI]  VARCHAR (MAX)                                                                    NULL,
    [ANALISIS]   VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')      NULL,
    [DIETAS]     VARCHAR (MAX)                                                                    NULL,
    [MEDICAMEN]  VARCHAR (MAX)                                                                    NULL,
    [MEZYLIQUI]  VARCHAR (MAX)                                                                    NULL,
    [INSUYDISPO] VARCHAR (MAX)                                                                    NULL,
    [LABORATOR]  VARCHAR (MAX)                                                                    NULL,
    [PATOLOGIA]  VARCHAR (MAX)                                                                    NULL,
    [IMAGENES]   VARCHAR (MAX)                                                                    NULL,
    [PROCEDIQX]  VARCHAR (MAX)                                                                    NULL,
    [PROCENOQX]  VARCHAR (MAX)                                                                    NULL,
    [INTERCONSU] VARCHAR (MAX)                                                                    NULL,
    [RECOMENDA]  VARCHAR (MAX)                                                                    NULL,
    [CALFINTER]  DECIMAL (18, 1)                                                                  NULL,
    [CODESPECI]  CHAR (3)                                                                         NULL,
    [NUMEFOIN]   NCHAR (10)                                                                       NULL,
    CONSTRAINT [PK_HCINTERNO_1] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC),
    CONSTRAINT [FK_HCINTERNO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINTERNO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCINTERNO] NOCHECK CONSTRAINT [FK_HCINTERNO_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINTERNO] NOCHECK CONSTRAINT [FK_HCINTERNO_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si la historia es de Ingreso o Evolucion  I: Ingreso  E: Evolucion  O: Otros modelos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';

