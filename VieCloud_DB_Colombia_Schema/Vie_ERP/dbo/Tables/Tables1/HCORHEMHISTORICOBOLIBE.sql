CREATE TABLE [dbo].[HCORHEMHISTORICOBOLIBE] (
    [ID]           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCORHEMBOL] INT            NOT NULL,
    [HCORHEMCOID]  INT            NOT NULL,
    [COMSAMID]     INT            NOT NULL,
    [FECRESERVA]   DATETIME       NOT NULL,
    [BACRESPRES]   CHAR (20)      NOT NULL,
    [REAPRUECRU]   BIT            NOT NULL,
    [NUMBOLSA]     VARCHAR (20)   NOT NULL,
    [SELLOCALIDAD] VARCHAR (20)   NOT NULL,
    [GRUPOBOL]     TINYINT        NOT NULL,
    [FECHAEXPIRA]  DATETIME       NOT NULL,
    [PROFLIBERO]   CHAR (20)      NOT NULL,
    [MOTLIBRESID]  INT            NOT NULL,
    [MOTLIBRESDES] VARCHAR (5000) NOT NULL,
    [ASPECFISICO]  TINYINT        NULL,
    [TEMPENTREG]   INT            NULL,
    [AUTENTREGA]   BIT            NULL,
    CONSTRAINT [PK_HCORHEMHISTORICOBOLIBE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autorización de entrega de hemocomponente (BIT: 1=Sí/True autoriza entrega, 0=No/False rechaza); indica si el profesional de salud autoriza la entrega física de la bolsa al paciente o servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'AUTENTREGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si autoriza entrega:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'AUTENTREGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'AUTENTREGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Temperatura registrada de la bolsa de hemocomponente al momento de la entrega (INT, grados Celsius); control de cadena de frío en transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'TEMPENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temperatura de la bolsa en la entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'TEMPENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'TEMPENTREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación del aspecto físico/visual de la bolsa de hemocomponente (TINYINT: 1=Adecuado/Íntegro, 2=Inadecuado/Dañado); criterio de calidad para liberación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'ASPECFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aspecto fisico de la unidad (bolsa):   1=Adecuado   2=Inadecuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'ASPECFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'ASPECFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada y justificación del motivo por el cual se libera el hemocomponente reservado (VARCHAR 5000); comentarios clínicos o administrativos sobre la liberación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'MOTLIBRESDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Motivo de Liberación Hemocomponentes Reservados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'MOTLIBRESDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'MOTLIBRESDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del catálogo/motivo de liberación de hemocomponentes reservados (INT, FK); vincula con tabla maestra de motivos (rechazo médico, consumo, expiración, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'MOTLIBRESID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Motivo de Liberación Hemocomponentes Reservados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'MOTLIBRESID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'MOTLIBRESID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación o código del profesional de salud (Médico/Bacteriólogo) que autoriza y libera la bolsa al estado disponible (CHAR 20); trazabilidad del liberador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'PROFLIBERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que pasa la bolsa a estado liberado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'PROFLIBERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'PROFLIBERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de vencimiento/expiración del hemocomponente en la bolsa (DATETIME); límite de viabilidad transfusional según grupo y tipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'FECHAEXPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de expiración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'FECHAEXPIRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'FECHAEXPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo ABO-Rh de la bolsa de hemocomponente (TINYINT: 1=O+, 2=O-, 3=A+, 4=A-, 5=B+, 6=B-, 7=AB+, 8=AB-); tipificación eritrocitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'GRUPOBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo de bolsa:  Grupo de bolsa:  1 - A+  2 - A-  3 - B+  4 - B-  5 - AB+  6 - AB-  7 - O+  8 - O-', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'GRUPOBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'GRUPOBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador o sello de control de calidad asignado a la bolsa durante el procesamiento (VARCHAR 20); trazabilidad de banco de sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sello de calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de identificación de la bolsa de hemocomponente (VARCHAR 20); código de barras/lote para rastreo en transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'NUMBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'NUMBOLSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'NUMBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de realización de prueba cruzada/compatibilidad (BIT: 1=Sí se realiza, 0=No se realiza); requisito previo antes de transfusión con sangre total o concentrado eritrocitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'REAPRUECRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si realiza prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'REAPRUECRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'REAPRUECRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación o código del Bacteriólogo/profesional responsable de la reserva inicial del hemocomponente (CHAR 20); trazabilidad del responsable de reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'BACRESPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bacteriologo Responsable de Reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'BACRESPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'BACRESPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/reserva del hemocomponente para un paciente específico (DATETIME); marca temporal de bloqueo de bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'FECRESERVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'FECRESERVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'FECRESERVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificador del componente sanguíneo reservado (INT, FK HCORHEMCO); tipo de hemocomponente (glóbulos rojos, plaquetas, plasma, crioprecipitado, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'COMSAMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del componente sanguíneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'COMSAMID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'COMSAMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la solicitud/cabecera de hemocomponentes (INT, FK HCORHEMCO); vincula registro de detalle con orden médica de transfusión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que relaciona el ID de la tabla HCORHEMCO (Cabecera solictud de hemocomponentes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la bolsa de hemocomponente en tabla padre HCORHEMBOL (INT, FK); referencia a datos maestros de bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'IDHCORHEMBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID HCORHEMBOL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'IDHCORHEMBOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'IDHCORHEMBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único/autonumérico del registro histórico de bolsa de hemocomponente (INT IDENTITY, PK); clave primaria para trazabilidad de transacciones de liberación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Histórico de liberación de bolsas de sangre en el banco de sangre. Registra el detalle de cada bolsa liberada para transfusión: quién la liberó, el motivo de liberación, la reserva asociada, fecha de expiración y condiciones físicas al momento de la entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE';
