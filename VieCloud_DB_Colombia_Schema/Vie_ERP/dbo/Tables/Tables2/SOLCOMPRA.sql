CREATE TABLE [dbo].[SOLCOMPRA] (
    [COMAUTON]  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTO]      INT            NULL,
    [COMFECHA]  SMALLDATETIME  NOT NULL,
    [COMESTADO] INT            NOT NULL,
    [COMOBSERV] VARCHAR (1000) NULL,
    [COMPRIORI] TINYINT        NULL,
    [CODUSUARI] CHAR (20)      NOT NULL,
    [UFUCODIGO] CHAR (10)      NOT NULL,
    [TITUAGRUS] VARCHAR (50)   NULL,
    [TIPOAGRUP] CHAR (1)       NULL,
    CONSTRAINT [PK_SOLCOM] PRIMARY KEY CLUSTERED ([COMAUTON] ASC),
    CONSTRAINT [FK_SOLCOMPRA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_SOLCOMPRA_SOLCOMPRA] FOREIGN KEY ([COMAUTON]) REFERENCES [dbo].[SOLCOMPRA] ([COMAUTON]),
    CONSTRAINT [FK_SOLCOMPRA_SOLTIPSOL] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[SOLTIPSOL] ([AUTO])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de agrupación de la solicitud de compra (CHAR 1): clasificación o categoría que agrupa items de la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'TIPOAGRUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el tipo de agrupacion de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'TIPOAGRUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'TIPOAGRUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Título o nombre descriptivo de la agrupación de items en la solicitud de compra (VARCHAR 50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'TITUAGRUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el titulo de la agrupacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'TITUAGRUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'TITUAGRUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Funcional que origina/realiza la solicitud de compra (FK a INUNIFUNC). Identifica centro de atención, servicio o departamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo de la unidad funcional que realiza la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que registra o gestiona la solicitud de compra (PII). Identifier del profesional o administrativo responsable (CHAR 20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de prioridad de la solicitud de compra (TINYINT): urgencia, normal, baja. Determina orden de procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMPRIORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el nivel de prioridad de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMPRIORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMPRIORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, notas o comentarios adicionales en la solicitud de compra (VARCHAR 1000). Requerimientos especiales, aclaraciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene las observaciones de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la solicitud de compra (INT): borrador, pendiente, aprobada, rechazada, procesada. Refleja etapa del flujo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el estado de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/registro de la solicitud de compra (SMALLDATETIME). Timestamp de la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la fecha de la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico de la tabla de tipos de solicitud (FK a SOLTIPSOL, INT). Ref cruzada a tipología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (IDENTITY) de cada solicitud de compra (INT). Primary Key de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la salicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA', @level2type = N'COLUMN', @level2name = N'COMAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitudes de compra registradas en el sistema, incluyendo su estado, prioridad, usuario solicitante y unidad funcional responsable. Permite gestionar y hacer seguimiento a los requerimientos de adquisición de bienes o servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOMPRA';
