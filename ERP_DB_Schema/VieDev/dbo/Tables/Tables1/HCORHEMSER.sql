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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera de la trazabilidad del tramite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la trazabilidad del trámite de eventos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-> Rastreo Anticuerpos,   2-> Prueba Cruzada, En la Reserva   3-> Transfusión    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'TIPOSERVICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del servicio  {1 -> Solicitado, 2-> Realizado, 3->Anulado, 4->Extramural}', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMSER', @level2type = N'COLUMN', @level2name = N'ESTADO';

