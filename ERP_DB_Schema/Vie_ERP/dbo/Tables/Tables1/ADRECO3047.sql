CREATE TABLE [dbo].[ADRECO3047] (
    [AUTO]      NUMERIC (18)   IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10)      NOT NULL,
    [CONPARENT] INT            NOT NULL,
    [CORREOENV] VARCHAR (60)   NOT NULL,
    [ASUNTOCOR] VARCHAR (100)  NOT NULL,
    [MENSAJECO] VARCHAR (5000) NOT NULL,
    [FECHAENVI] DATETIME       NOT NULL,
    [REPRIRENV] BIT            NULL,
    [RESEGRENV] BIT            NULL,
    [FEPRIRENV] DATETIME       NULL,
    [FESEGRENV] DATETIME       NULL,
    [NUMINFORM] CHAR (4)       NOT NULL,
    [CONSINFOR] NUMERIC (18)   NOT NULL,
    [TIPOREPOR] CHAR (1)       NULL,
    CONSTRAINT [PK_ADRECO3047] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Reporte  1: Atencion Inicial Urgencias;  2:Inconsistencias;  3:Solicitud de Servicios;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'TIPOREPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo del Informe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CONSINFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de informe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'NUMINFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Segundo Re Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FESEGRENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Primer Re Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FEPRIRENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Realiza el Segundo Re Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'RESEGRENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Realizo el primer Re Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'REPRIRENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'FECHAENVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'MENSAJECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Asunto del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'ASUNTOCOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo al que se va a enviar el correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CORREOENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la entidad parametrizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CONPARENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRECO3047', @level2type = N'COLUMN', @level2name = N'AUTO';

