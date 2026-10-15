CREATE TABLE [dbo].[AGACTMEDD] (
    [CODACTMED]                CHAR (3)      NOT NULL,
    [CODSERIPS]                CHAR (20)     NOT NULL,
    [DURACSERVI]               CHAR (6)      NULL,
    [DURAVARIAB]               BIT           NULL,
    [INDICACI]                 VARCHAR (MAX) NULL,
    [MOSTRARWEB]               BIT           NULL,
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT           NULL,
    [EXIGECONFCITA]            BIT           NULL,
    CONSTRAINT [PK_AGACTMEDD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGACTMEDD_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGACTMEDD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[AGACTMEDD] NOCHECK CONSTRAINT [FK_AGACTMEDD_AGACTIMED];


GO
ALTER TABLE [dbo].[AGACTMEDD] NOCHECK CONSTRAINT [FK_AGACTMEDD_INCUPSIPS];


GO
CREATE NONCLUSTERED INDEX [IX_AGACTMEDD]
    ON [dbo].[AGACTMEDD]([CODACTMED] ASC, [CODSERIPS] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si exige confirmacion de cita (aplica solo para tipo actividad de apoyo DX) y  si el CUPS está parametrizado en el campo "Listar este servicio en el dashboard de" del formulario de CUPS como "3 -Imágenes diagnosticas" o "12 - Procedimientos invasivos u Otros Procedimientos"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'EXIGECONFCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar en Web: 0 = No, 1 = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones medicas para actividades de tipo apoyo diagnostico y tratamientos especailes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'INDICACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'define si la duracion es variable o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'DURAVARIAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'duracion del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'DURACSERVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo servicio ips', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo consecutivo que identifica la actividad medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMEDD', @level2type = N'COLUMN', @level2name = N'CODACTMED';

