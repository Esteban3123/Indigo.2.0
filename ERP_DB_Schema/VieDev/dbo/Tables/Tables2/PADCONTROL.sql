CREATE TABLE [dbo].[PADCONTROL] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]     INT                                                                              NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMEFOLIO]       CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [ESTADO]          TINYINT                                                                          NOT NULL,
    [FECHAREGISTRO]   DATETIME                                                                         NOT NULL,
    [CODUSUARIO]      CHAR (20)                                                                        NOT NULL,
    [MOTIVOSUSP]      VARCHAR (MAX)                                                                    NULL,
    [FECHASUSP]       DATETIME                                                                         NULL,
    [CODUSUARIOSUSP]  CHAR (20)                                                                        NULL,
    [TIPOATENCION]    INT                                                                              NULL,
    [IDREGISTROPADRE] INT                                                                              NULL,
    CONSTRAINT [PK_PHDCONTROL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PADCONTROL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_PHDCONTROL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PHDCONTROL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PHDCONTROL_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_PHDCONTROL_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_PHDCONTROL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[PADCONTROL] NOCHECK CONSTRAINT [FK_PADCONTROL_INPACIENT];


GO
ALTER TABLE [dbo].[PADCONTROL] NOCHECK CONSTRAINT [FK_PHDCONTROL_ADCENATEN];


GO
ALTER TABLE [dbo].[PADCONTROL] NOCHECK CONSTRAINT [FK_PHDCONTROL_ADINGRESO];


GO
ALTER TABLE [dbo].[PADCONTROL] NOCHECK CONSTRAINT [FK_PHDCONTROL_HCHISPACA];


GO
ALTER TABLE [dbo].[PADCONTROL] NOCHECK CONSTRAINT [FK_PHDCONTROL_INPROFSAL];


GO
ALTER TABLE [dbo].[PADCONTROL] NOCHECK CONSTRAINT [FK_PHDCONTROL_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guardamos el id del registro que se modificó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IDREGISTROPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion de atencion domiciliaria   1: Atención domiciliaria   2: Hospitalización en casa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Orden:  1 - Registrado  2 - Suspendida  3 - Suspendida a raiz de una modificación del PAD en el plan de manejo.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'ESTADO';

