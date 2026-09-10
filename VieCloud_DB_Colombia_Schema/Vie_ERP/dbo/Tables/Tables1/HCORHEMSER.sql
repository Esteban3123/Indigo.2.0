CREATE TABLE [dbo].[HCORHEMSER] (
    [ID]                            INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCORHEMBOLID]                  INT       NULL,
    [HCORHEMCOID]                   INT       NULL,
    [CODSERIPS]                     CHAR (20) NOT NULL,
    [GENORDSER]                     BIT       NOT NULL,
    [ESTADO]                        TINYINT   NOT NULL,
    [TIPOSERVICIO]                  TINYINT   CONSTRAINT [DF_HCORHEMSER_TIPOSERVICIO] DEFAULT ((2)) NOT NULL,
    [ORDSERVICIOID]                 INT       NULL,
    [IDDESCRIPCIONRELACIONADA]      INT       NULL,
    [TraceabilityPaperworkEventsId] INT       NULL,
    [TraceabilityPaperworkId]       INT       NULL,
    CONSTRAINT [PK_HCORHEMSER] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de trazabilidad del trámite; documento maestro que registra el flujo y auditoría del procedimiento en hemoterapia (INT, FK opcional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la trazabilidad del tramite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de evento de trazabilidad del trámite; registro de hito/suceso específico en el ciclo de vida del procedimiento hemoterapéutico (INT, FK opcional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la trazabilidad del trámite de eventos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de descripción de relación con VIE ERP; vínculo a catálogo de CUPS/descripciones de contrato en contract.CUPSEntityContractDescriptions (INT, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de orden de servicio; referencia a la orden de sangre, transfusión o prueba hematológica generada (INT, FK opcional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ORDSERVICIOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de orden de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ORDSERVICIOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ORDSERVICIOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de servicio hemoterapéutico: 1=Rastreo de Anticuerpos, 2=Prueba Cruzada (reserva), 3=Transfusión (TINYINT, default=2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-> Rastreo Anticuerpos,   2-> Prueba Cruzada, En la Reserva   3-> Transfusión    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del servicio hemoterapéutico: 1=Solicitado, 2=Realizado, 3=Anulado, 4=Extramural (TINYINT, catálogo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del servicio  {1 -> Solicitado, 2-> Realizado, 3->Anulado, 4->Extramural}', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano; especifica si el servicio genera automáticamente una orden de servicio en el sistema (BIT, 0/1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'GENORDSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genera orden de servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'GENORDSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'GENORDSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del servicio en IPS/CUPS; código estandarizado para facturación y reporte RIPS (CHAR 20, requerido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera de solicitud de cobro; referencia al registro de rastreo de anticuerpos facturable (INT, FK opcional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera de solicitud para cobrar el servicio de rastreo de anticuerpos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de bolsa de sangre; vinculación a unidad de sangre cuando el cobro es por bolsa (pruebas cruzadas) (INT, FK opcional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la bolsa, cuando es un servicio que se cobra por cada bolsa (pruebas cruzadas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'HCORHEMBOLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable; clave primaria de la transacción de servicio hemoterapéutico (INT IDENTITY, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicios o procedimientos (CUPS) asociados a una orden de hemodinamia. Registra cada ítem de servicio solicitado dentro de una orden de hemodinamia, indicando si genera orden de servicio y su estado de procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER';
