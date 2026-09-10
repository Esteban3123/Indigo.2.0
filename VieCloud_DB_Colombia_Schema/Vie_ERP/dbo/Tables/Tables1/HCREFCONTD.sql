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



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_HCREFCONTD__HCREFCONPID__FECHCONFIR]
    ON [dbo].[HCREFCONTD]([HCREFCONPID] ASC, [FECHCONFIR] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de entrega (BIT): 1=Correo enviado exitosamente vía servicio Windows, 0=No enviado; auditoria de comunicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CORRENVIADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me Identifica si el Correo ya se ha enviado mediante el Servicio Windows', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CORRENVIADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CORRENVIADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuerpo del mensaje de correo electrónico (VARCHAR MAX); contiene el texto parametrizado de la notificación sobre la referencia/remisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'MENSAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta columna me almacena el Mensaje que se debe enviar en el correo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'MENSAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'MENSAJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Línea de asunto del correo electrónico a enviar; componente de la notificación automatizada sobre la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ASUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta columna me almacena el Asunto que se debe enviar en el correo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ASUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ASUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT): 1/TRUE=Enviar correo (se encontraron plantillas parametrizadas y NO es seguimiento rutinario), 0/FALSE=No enviar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ENVIAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta Columna me Identifica, si se debe enviar o no el Correo  1 ó true Significa que si se debe enviar el correo, significa que si habian correos parametrizados y que NO es un seguimiento de la gestion de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ENVIAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ENVIAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha programada o registrada del seguimiento a la gestión de la referencia; marca hito en el ciclo de vida de la remisión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECSEGUIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECSEGUIMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECSEGUIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea auto-referencial (FK HCREFCONTD.ID) que vincula con el registro padre de seguimiento; permite trazabilidad jerárquica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'HCREFCONTDID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del seguimiento padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'HCREFCONTDID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'HCREFCONTDID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la IPS que finalmente aceptó al paciente (FK ADCONTIPS.CODIGOIPS); se registra solo si TIPENTIDAD diferente de IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'IPSACEPTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ips donde el paciente fue aceptado, solo se solicita este campo si en tipo de entidad la seleccion fue diferente a IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'IPSACEPTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'IPSACEPTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK RCMODSOLIC.Id) que referencia la modalidad o tipo de gestión aplicada a la solicitud de referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'RCMODSOLICID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modalidad de Gestión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'RCMODSOLICID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'RCMODSOLICID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del municipio (FK INMUNICIP.DEPMUNCOD) de la otra entidad, requerido cuando se selecciona TIPENTIDAD=4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'MUNOTRAENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'DEPMUNCOD Municipio de entidad, en caso de que en tipo de entidad se haya seleccionado otra entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'MUNOTRAENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'MUNOTRAENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción de otra entidad externa (cuando TIPENTIDAD=4), si no está catalogada en el maestro de instituciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'OTRAENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otra entidad en caso de que haya seleccionado otra entidad en campo tipentidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'OTRAENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'OTRAENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la IPS seleccionada (FK ADCONTIPS.CODIGOIPS) cuando TIPENTIDAD=1; identifica el prestador de servicios elegido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'IPSSELECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ips en caso de que haya seleccionado IPS en tipo de entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'IPSSELECCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'IPSSELECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de suspensión: 0=No suspendido, 1=Si suspendido; controla si la solicitud/referencia está activa o pausada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ESTADOSUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado que me permite saber si la solicitud de esa refecia  esta  suspendido si lo esta los detalles en este campo se colocan en 1 que es suspendido-.  0->No  1->Si  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ESTADOSUSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ESTADOSUSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del detalle de seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHMODIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHMODIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que realizó la última modificación de este registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario de Modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de seguimiento de referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHCREREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHCREREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHCREREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que registró inicialmente este detalle de seguimiento en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODUSUAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODUSUAREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODUSUAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas adicionales, comentarios o detalles sobre la gestión, aceptación o seguimiento de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación de la aceptación o respuesta a la referencia por parte de la entidad receptora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHCONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de confirmacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHCONFIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'FECHCONFIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de aceptación: 1=Acepta referencia, 2=No acepta, 3=Pendiente de respuesta, 4=No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'REGESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  1 - SI acepta   2 - No Acepta  3 - Pendiente   4 - No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'REGESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'REGESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono del contacto responsable; PII_Comunicación, usado para seguimiento de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono del contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la persona contacto, responsable de recibir o gestionar la referencia/remisión en la entidad receptora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'NOMCONTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la persona contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'NOMCONTACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'NOMCONTACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad (FK INENTIDAD.CODENTIDA); identificador de la institución, centro de atención o prestador involucrado en la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con el Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad receptora: 1=IPS (Instituto Prestador de Salud), 2=EAPB (Empresa Administradora de Planes de Beneficios), 3=CRUE (Centro de Regulación), 4=OTRA entidad externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'TIPENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Entidad : 1-IPS  2-EAPB  3-CRUE  4-OTRA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'TIPENTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'TIPENTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que relaciona con la referencia padre (HCREFCONP.AUTO); identifica la remisión/referencia principal asociada al seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'HCREFCONPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la Referencia Padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'HCREFCONPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'HCREFCONPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de cada detalle de seguimiento de referencia en el sistema de gestión de remisiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de contactos y entidades referenciadas en las contrarreferencias de historia clínica. Registra cada entidad (IPS u otra organización) contactada o seleccionada durante el proceso de referencia y contrarreferencia del paciente, incluyendo seguimientos, confirmaciones y comunicaciones enviadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTD';
