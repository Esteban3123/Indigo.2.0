CREATE TABLE [dbo].[HCORDPROI] (
    [IDETIPHIS]       CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]       NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]       DATETIME                                                                         NOT NULL,
    [CODSERIPS]       CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]       INT                                                                              NOT NULL,
    [OBSSERIPS]       CHAR (250)                                                                       NULL,
    [PRISERIPS]       CHAR (1)                                                                         NOT NULL,
    [ESTSERIPS]       CHAR (1)                                                                         NULL,
    [MANEXTPRO]       BIT                                                                              NOT NULL,
    [CODDIAGNO]       CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [INDAUDFOR]       NUMERIC (18)                                                                     NOT NULL,
    [LATERALIDAD]     TINYINT                                                                          NULL,
    [SOLICITASALA]    BIT                                                                              NULL,
    [OTROSMATERIALES] VARCHAR (5000)                                                                   NULL,
    [SOLICITAMATOST]  TINYINT                                                                          NULL,
    [AUTO]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_HCORDPROI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCORDPROI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDPROI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDPROI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]) ON UPDATE CASCADE,
    CONSTRAINT [FK_HCORDPROI_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDPROI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDPROI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCORDPROI] NOCHECK CONSTRAINT [FK_HCORDPROI_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDPROI] NOCHECK CONSTRAINT [FK_HCORDPROI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORDPROI] NOCHECK CONSTRAINT [FK_HCORDPROI_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDPROI] NOCHECK CONSTRAINT [FK_HCORDPROI_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDPROI] NOCHECK CONSTRAINT [FK_HCORDPROI_INPACIENT];


GO
ALTER TABLE [dbo].[HCORDPROI] NOCHECK CONSTRAINT [FK_HCORDPROI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si en el procedimiento qx se solicitó material de osteosintesis:   0=No solicitó   1=Solicitado   2=Entregado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROI', @level2type = N'COLUMN', @level2name = N'SOLICITAMATOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lateralidad:   0=No Aplica   1=Izquierda   3=Derecha   4=Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROI', @level2type = N'COLUMN', @level2name = N'LATERALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Muestra Procesada o Resultado Enviado  4: Resultado Revisado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROI', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROI', @level2type = N'COLUMN', @level2name = N'PRISERIPS';

