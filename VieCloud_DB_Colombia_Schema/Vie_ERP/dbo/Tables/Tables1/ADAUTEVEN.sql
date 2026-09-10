CREATE TABLE [dbo].[ADAUTEVEN] (
    [CODREGUNI]  NUMERIC (10)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODENTIDA]  CHAR (9)                                                                         NOT NULL,
    [FECREGEVE]  DATETIME                                                                         NOT NULL,
    [TIPREPENT]  CHAR (1)                                                                         NOT NULL,
    [NUMTELCON]  CHAR (12)                                                                        NULL,
    [NUMEXTTEL]  CHAR (6)                                                                         NULL,
    [FECHORINI]  DATETIME                                                                         NULL,
    [FECHORFIN]  DATETIME                                                                         NULL,
    [NOMCONENT]  CHAR (80)                                                                        NULL,
    [CARCONENT]  CHAR (50)                                                                        NULL,
    [NUMFAXCON]  CHAR (12)                                                                        NULL,
    [NUMEXTFAX]  CHAR (6)                                                                         NULL,
    [NUMINTENV]  CHAR (1)                                                                         NULL,
    [FECENVFAX]  DATETIME                                                                         NULL,
    [URLSERWEB]  CHAR (150)                                                                       NULL,
    [FECREGWEB]  DATETIME                                                                         NULL,
    [CODDOCALM]  NUMERIC (18)                                                                     NULL,
    [NUMVALDER]  CHAR (20)                                                                        NULL,
    [COMGENREG]  VARCHAR (2000)                                                                   NULL,
    [CODUSUARI]  CHAR (20)                                                                        NOT NULL,
    [CANSERAUT]  TINYINT                                                                          NULL,
    [ESTADO]     INT                                                                              NULL,
    [ADAUTSERID] INT                                                                              NULL,
    [TIPOTRAZA]  INT                                                                              NULL,
    [PACIENTNOT] BIT                                                                              NULL,
    [INFOPACI]   VARCHAR (4000)                                                                   NULL,
    CONSTRAINT [PK_ADAUTEVEN] PRIMARY KEY CLUSTERED ([CODREGUNI] ASC),
    CONSTRAINT [FK_ADAUTEVEN_ADDOCADIC] FOREIGN KEY ([CODDOCALM]) REFERENCES [dbo].[ADDOCADIC] ([CODDOCALM]),
    CONSTRAINT [FK_ADAUTEVEN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADAUTEVEN_INENTIDAD] FOREIGN KEY ([CODENTIDA]) REFERENCES [dbo].[INENTIDAD] ([CODENTIDA]),
    CONSTRAINT [FK_ADAUTEVEN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADAUTEVEN].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_ADAUTEVEN__IPCODPACI__NUMINGRES]
    ON [dbo].[ADAUTEVEN]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
ALTER INDEX [IX_ADAUTEVEN__IPCODPACI__NUMINGRES]
    ON [dbo].[ADAUTEVEN] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_ADAUTEVEN__ADAUTSERID__INC__TIPOTRAZA]
    ON [dbo].[ADAUTEVEN]([ADAUTSERID] ASC)
    INCLUDE([TIPOTRAZA]);


GO
ALTER INDEX [IX_ADAUTEVEN__ADAUTSERID__INC__TIPOTRAZA]
    ON [dbo].[ADAUTEVEN] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información, detalles o recomendaciones comunicados al paciente durante la autorización de servicios; VARCHAR(4000), búsqueda: orientación, educación, instrucciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'INFOPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información dada al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'INFOPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'INFOPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: paciente notificado (1=Sí, 0=No); estado de comunicación con paciente; VARCHAR tipo BIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'PACIENTNOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente notificado   Si: True-1  No: False-2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'PACIENTNOT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'PACIENTNOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de trazabilidad del evento autorizado: 1=Atención inicial urgencias, 2=Autorización de servicios; INT, cadena de auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'TIPOTRAZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el tipo de trazabilidad que agregó el evento:  1. Atención inicial de urgencias  2. Autorización de servicios  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'TIPOTRAZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'TIPOTRAZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de relación con registro de trazabilidad origen que generó el evento de autorización; INT, FK indirecto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'ADAUTSERID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el consecutivo que relaciona el registro con la trazavilidad desde  la cual fue agregado el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'ADAUTSERID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'ADAUTSERID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la autorización: 1=Autorizado, 2=No Autorizado, 3=Pendiente de Autorización; INT, búsqueda: aprobación, negación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autorizado = 1  No Autorizado = 2  Pedniente de Autorización = 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios, procedimientos o ítems autorizados en la solicitud; TINYINT, búsqueda: cupo, cantidad aprobada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CANSERAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad autorizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CANSERAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CANSERAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que registró o procesó el evento de autorización; CHAR(20), búsqueda: profesional, operador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentario, nota o texto libre del registro de autorización; VARCHAR(2000), observaciones, glosas, motivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'COMGENREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario General del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'COMGENREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'COMGENREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de validación de derechos o número de autorización del evento; CHAR(20), búsqueda: vigencia, cobertura, RIPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMVALDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Validacion de Derechos o Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMVALDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMVALDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del documento almacenado; nombre único del archivo físico adjunto; NUMERIC(18), FK→ADDOCADIC, búsqueda: soporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del documento almacenado - Representa el nombre del archivo fisico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODDOCALM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del evento vía portal web de la entidad; DATETIME, búsqueda: fecha ingreso, timestamp portal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECREGWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Registro WEB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECREGWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECREGWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL, dirección o endpoint del servicio web usado para registrar el evento; CHAR(150), búsqueda: portal, aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'URLSERWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URL Pagina WEB de Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'URLSERWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'URLSERWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se envió la autorización o evento por fax; DATETIME, búsqueda: envío, transmisión fax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECENVFAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Envio de FAX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECENVFAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECENVFAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos de envío del evento por fax; CHAR(1), reintento, reintentos fallidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMINTENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Intentos de Envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMINTENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMINTENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de extensión interna de la máquina fax de destino; CHAR(6), búsqueda: anexo, ramificación fax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMEXTFAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Extension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMEXTFAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMEXTFAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico de fax de contacto de la entidad; CHAR(12), búsqueda: teléfono fax, comunicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMFAXCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero fax', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMFAXCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMFAXCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo, rol o posición laboral de la persona de contacto en la entidad; CHAR(50), búsqueda: título, función', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CARCONENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo de la persona de contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CARCONENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CARCONENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la persona de contacto de la entidad para la autorización; CHAR(80), búsqueda: referente, gestor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NOMCONENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la Persona de Contacto de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NOMCONENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NOMCONENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de fin de la llamada telefónica de autorización o consulta; DATETIME, término, cierre llamada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECHORFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Final de la Llamada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECHORFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECHORFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la llamada telefónica de autorización o consulta; DATETIME, comienzo, duración llamada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECHORINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Inicial de la Llamada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECHORINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECHORINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de extensión interna del teléfono de contacto; CHAR(6), búsqueda: anexo, ramificación telefónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Extension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMEXTTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico de contacto de la entidad para la autorización; CHAR(12), búsqueda: teléfono, comunicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMTELCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico de Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMTELCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMTELCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de reporte/evento: 1=Llamada Telefónica, 2=Envío Fax, 3=Registro Página Web; CHAR(1), medio comunicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'TIPREPENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Reporte Entidad:  1 - Llamada Telefonica  2 - Envio de FAX  3 - Registro Pagina Web', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'TIPREPENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'TIPREPENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro del evento de autorización en el sistema; DATETIME, timestamp creación, auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECREGEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECREGEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'FECREGEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad (asegurador, EPS, IPS) vinculada al evento; CHAR(9), FK→INENTIDAD, búsqueda: institución', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso/atención/episodio del paciente; CHAR(10), FK→ADINGRESO, búsqueda: admisión, estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, cédula o documento de identificación (PII_Ofuscado); VARCHAR(25), búsqueda: identificación, cédula', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del registro de autorización/evento; NUMERIC(10) IDENTITY, PK, identificador secuencial del proceso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODREGUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Registro Unico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODREGUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN', @level2type = N'COLUMN', @level2name = N'CODREGUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de eventos y comunicaciones con entidades (aseguradoras, EPS, ARL) relacionados con un ingreso o admisión de un paciente. Guarda el historial de contactos, autorizaciones y notificaciones realizadas a cada entidad durante la gestión del ingreso.  Tabla en desuso por desarrollo de Dashboard Autorizacion Intrahospitalaria el 23/06/2026', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADAUTEVEN';
