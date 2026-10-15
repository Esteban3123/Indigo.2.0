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
CREATE NONCLUSTERED INDEX [IX_INDICE_PADCONTROL]
    ON [dbo].[PADCONTROL]([NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guardamos el id del registro que se modificó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IDREGISTROPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion de atencion domiciliaria   1: Atención domiciliaria   2: Hospitalización en casa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario de suspensión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUARIOSUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de suspensión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'FECHASUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de suspension de orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'MOTIVOSUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Orden:  1 - Registrado  2 - Suspendida  3 - Suspendida a raiz de una modificación del PAD en el plan de manejo.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo centro atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADCONTROL', @level2type = N'COLUMN', @level2name = N'ID';

