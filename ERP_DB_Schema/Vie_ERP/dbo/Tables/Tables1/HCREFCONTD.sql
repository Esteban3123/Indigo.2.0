CREATE TABLE [dbo].[HCREFCONTD] (
    [ID]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCREFCONPID]    INT           NOT NULL,
    [TIPENTIDAD]     TINYINT       NOT NULL,
    [CODENTIDA]      CHAR (9)      NULL,
    [NOMCONTACT]     VARCHAR (200) NOT NULL,
    [TELEFONO]       VARCHAR (20)  NULL,
    [REGESTADO]      TINYINT       NOT NULL,
    [FECHCONFIR]     DATETIME      NULL,
    [OBSERVACI]      VARCHAR (500) NULL,
    [CODUSUAREG]     VARCHAR (20)  NULL,
    [FECHCREREG]     DATETIME      NULL,
    [CODUSUMOD]      VARCHAR (20)  NULL,
    [FECHMODIF]      DATETIME      NULL,
    [ESTADOSUSP]     INT           NULL,
    [IPSSELECCION]   CHAR (100)    NULL,
    [OTRAENTIDAD]    VARCHAR (500) NULL,
    [MUNOTRAENT]     CHAR (5)      NULL,
    [RCMODSOLICID]   INT           NULL,
    [IPSACEPTADO]    CHAR (100)    NULL,
    [HCREFCONTDID]   INT           NULL,
    [FECSEGUIMIENTO] DATETIME      NULL,
    [ENVIAR]         BIT           NULL,
    [ASUNTO]         VARCHAR (100) NULL,
    [MENSAJE]        VARCHAR (MAX) NULL,
    [CORRENVIADO]    BIT           NULL,
    CONSTRAINT [PK_HCREFCONTD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFCONTD_ADCONTIPS] FOREIGN KEY ([IPSSELECCION]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_HCREFCONTD_ADCONTIPS1] FOREIGN KEY ([IPSACEPTADO]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_HCREFCONTD_HCREFCONTD] FOREIGN KEY ([HCREFCONPID]) REFERENCES [dbo].[HCREFCONP] ([AUTO]),
    CONSTRAINT [FK_HCREFCONTD_HCREFCONTD1] FOREIGN KEY ([RCMODSOLICID]) REFERENCES [dbo].[RCMODSOLIC] ([Id]),
    CONSTRAINT [FK_HCREFCONTD_HCREFCONTD2] FOREIGN KEY ([HCREFCONTDID]) REFERENCES [dbo].[HCREFCONTD] ([ID]),
    CONSTRAINT [FK_HCREFCONTD_INMUNICIP] FOREIGN KEY ([MUNOTRAENT]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD])
);


GO
ALTER TABLE [dbo].[HCREFCONTD] NOCHECK CONSTRAINT [FK_HCREFCONTD_ADCONTIPS];


GO
ALTER TABLE [dbo].[HCREFCONTD] NOCHECK CONSTRAINT [FK_HCREFCONTD_ADCONTIPS1];


GO
ALTER TABLE [dbo].[HCREFCONTD] NOCHECK CONSTRAINT [FK_HCREFCONTD_HCREFCONTD];


GO
ALTER TABLE [dbo].[HCREFCONTD] NOCHECK CONSTRAINT [FK_HCREFCONTD_HCREFCONTD1];


GO
ALTER TABLE [dbo].[HCREFCONTD] NOCHECK CONSTRAINT [FK_HCREFCONTD_HCREFCONTD2];


GO
ALTER TABLE [dbo].[HCREFCONTD] NOCHECK CONSTRAINT [FK_HCREFCONTD_INMUNICIP];


GO
CREATE NONCLUSTERED INDEX [IX_HCREFCONTD__HCREFCONPID__FECHCONFIR]
    ON [dbo].[HCREFCONTD]([HCREFCONPID] ASC, [FECHCONFIR] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Me Identifica si el Correo ya se ha enviado mediante el Servicio Windows', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CORRENVIADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta columna me almacena el Mensaje que se debe enviar en el correo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'MENSAJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta columna me almacena el Asunto que se debe enviar en el correo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ASUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta Columna me Identifica, si se debe enviar o no el Correo  1 ó true Significa que si se debe enviar el correo, significa que si habian correos parametrizados y que NO es un seguimiento de la gestion de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ENVIAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECSEGUIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del seguimiento padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'HCREFCONTDID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ips donde el paciente fue aceptado, solo se solicita este campo si en tipo de entidad la seleccion fue diferente a IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'IPSACEPTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modalidad de Gestión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'RCMODSOLICID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DEPMUNCOD Municipio de entidad, en caso de que en tipo de entidad se haya seleccionado otra entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'MUNOTRAENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otra entidad en caso de que haya seleccionado otra entidad en campo tipentidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'OTRAENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ips en caso de que haya seleccionado IPS en tipo de entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'IPSSELECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado que me permite saber si la solicitud de esa refecia  esta  suspendido si lo esta los detalles en este campo se colocan en 1 que es suspendido-.  0->No  1->Si  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ESTADOSUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHMODIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario de Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHCREREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODUSUAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHCONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1 - SI acepta   2 - No Acepta  3 - Pendiente   4 - No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'REGESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono del contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la persona contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'NOMCONTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con el Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Entidad : 1-IPS  2-EAPB  3-CRUE  4-OTRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'TIPENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la Referencia Padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'HCREFCONPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ID';

